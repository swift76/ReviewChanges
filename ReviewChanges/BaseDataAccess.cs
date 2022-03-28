using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.IO;

namespace ReviewChanges
{
    public class BaseDataAccess : IDisposable
    {
        public string BaseScriptPath { get; set; }
        public string VersionPath { get; set; }
        public string LocalBeginComment { get; set; }
        public string LocalEndComment { get; set; }
        public bool IsRealTest { get; set; }

        public BaseDataAccess(string login, string password, bool isRealTest)
        {
            string configurationName = isRealTest ? "real" : "test";
            SqlConnectionStringBuilder build = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings[configurationName].ConnectionString);
            build.Pooling = false;
            build.PersistSecurityInfo = true;
            build.UserID = login;
            build.Password = password;
            ActiveConnection = new SqlConnection(build.ConnectionString);
            ActiveConnection.Open();
            BaseScriptPath = ConfigurationManager.AppSettings["BaseScriptPath"];
            VersionPath = ConfigurationManager.AppSettings["VersionPath"];
            string bankPrefix = ConfigurationManager.AppSettings["BankCode"];
            LocalBeginComment = $"'''{bankPrefix} BEGIN";
            LocalEndComment = $"'''{bankPrefix} END";
            IsRealTest = isRealTest;
        }

        ~BaseDataAccess()
        {
            DisposeConnection();
        }

        public SqlConnection ActiveConnection { get; set; }

        public void Dispose()
        {
            DisposeConnection();
            GC.SuppressFinalize(this);
        }

        public void DisposeConnection()
        {
            if (ActiveConnection != null)
            {
                ActiveConnection.Close();
                ActiveConnection = null;
            }
        }

        public void SaveConstants()
        {
            if (!Directory.Exists(BaseScriptPath))
                throw new ApplicationException("Base scripts directory is wrong");

            ImportConstantsFile("ARM");
            ImportConstantsFile("DEF");
        }

        public void SaveScript(string file, string subdirectory = null)
        {
            int position = file.IndexOf(@"BankScript\");
            if (position == -1)
                throw new ApplicationException("Script from wrong destination selected");

            string text = ParseScript(GetFileContents(file));
            file = file.Substring(position + 11);
            using (SqlTransaction trans = ActiveConnection.BeginTransaction())
                try
                {
                    using (SqlCommand cmd = new SqlCommand("ahsp_Delete_SCRIPT", ActiveConnection, trans))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PATH", SqlDbType.NVarChar, 300).Value = file;
                        cmd.ExecuteNonQuery();
                    }
                    if (text.Length > 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("ahsp_Insert_SCRIPT", ActiveConnection, trans))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@PATH", SqlDbType.NVarChar, 300).Value = file;
                            cmd.Parameters.Add("@BODY", SqlDbType.VarChar, -1).Value = text;
                            cmd.Parameters.Add("@SUBDIRECTORY", SqlDbType.VarChar, 255).Value = subdirectory;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        trans.Rollback();
                    }
                    catch
                    {
                    }
                    throw new ApplicationException(ex.Message);
                }
        }

        public void DeleteScript(string file)
        {
            using (SqlCommand cmd = new SqlCommand("ahsp_Delete_SCRIPT", ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PATH", SqlDbType.NVarChar, 300).Value = file;
                cmd.ExecuteNonQuery();
            }
        }

        private void ImportConstantsFile(string extension, List<ConstantEntity> entities = null)
        {
            if (entities == null)
                entities = ParseConstants(GetFileContents(string.Format(@"{0}\constant.{1}", BaseScriptPath, extension)));
            using (SqlTransaction trans = ActiveConnection.BeginTransaction())
                try
                {
                    using (SqlCommand cmd = new SqlCommand(string.Format("ahsp_Delete_CONSTANT_{0}", extension), ActiveConnection, trans))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }
                    foreach (ConstantEntity entity in entities)
                    {
                        using (SqlCommand cmd = new SqlCommand(string.Format("ahsp_Insert_CONSTANT_{0}", extension), ActiveConnection, trans))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@CODE", SqlDbType.VarChar, 100).Value = entity.CODE;
                            cmd.Parameters.Add("@VALUE", SqlDbType.VarChar, 1000).Value = entity.VALUE;
                            cmd.Parameters.Add("@LOCAL", SqlDbType.Bit).Value = entity.LOCAL;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        trans.Rollback();
                    }
                    catch
                    {
                    }
                    throw new ApplicationException(ex.Message);
                }
        }

        private List<ConstantEntity> ParseConstants(string text)
        {
            List<ConstantEntity> result = new List<ConstantEntity>();
            string[] constantLines = ParseFileByRows(text, true);
            bool isLocalBlock = false;
            for (int i = 0; i < constantLines.Length; i++)
            {
                if (constantLines[i] != LocalBeginComment && constantLines[i] != LocalEndComment)
                {
                    int valuePosition = constantLines[i].IndexOf("=");
                    ConstantEntity entity = new ConstantEntity();
                    entity.CODE = constantLines[i].Substring(0, valuePosition).Trim();
                    if (result.FirstOrDefault(e => e.CODE == entity.CODE) == null)
                    {
                        entity.LOCAL = isLocalBlock;
                        entity.VALUE = constantLines[i].Substring(valuePosition + 1).Trim();
                        result.Add(entity);
                    }
                }
                else
                    isLocalBlock = !isLocalBlock;
            }
            return result;
        }

        private string ParseScript(string text)
        {
            string[] scriptLines = ParseFileByRows(text, false);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < scriptLines.Length; i++)
                builder.AppendLine(scriptLines[i]);
            return builder.ToString();
        }

        private string[] ParseFileByRows(string text, bool isConstant)
        {
            string[] result = text.Split('\n');
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = result[i].Trim();
                if (result[i] != LocalBeginComment && result[i] != LocalEndComment)
                {
                    int commentPosition = result[i].IndexOf("'");
                    if (commentPosition > -1)
                    {
                        if (result[i].Length == commentPosition)
                            result[i] = string.Empty;
                        else
                        {
                            if (isConstant && result[i].Substring(0, commentPosition).Count(c => c == '=') == 1 && result[i].Substring(0, commentPosition).Count(c => c == '"') == 0)
                                commentPosition = -1;
                            else
                                while (commentPosition > -1 && result[i].Substring(0, commentPosition).Count(c => c == '"') % 2 == 1)
                                    commentPosition = result[i].IndexOf("'", commentPosition + 1);
                            if (commentPosition > -1)
                                result[i] = result[i].Substring(0, commentPosition).Trim();
                        }
                    }
                }
            }
            return result.Where(l => l != string.Empty).ToArray();
        }

        private string GetFileContents(string file)
        {
            return File.ReadAllText(file, Encoding.Default);
        }

        public List<ReportItem> CompareAndMerge(bool onlyChanged, out string path)
        {
            if (!Directory.Exists(VersionPath))
                throw new ApplicationException("Versions directory is wrong");

            string[] directories = Directory.GetDirectories(VersionPath);
            if (directories.Length == 0)
                throw new ApplicationException("Versions directory is empty");

            path = string.Format(@"{0}\BankScript", directories.OrderByDescending(d => d.ToUpper()).First());

            if (!Directory.Exists(path))
                throw new ApplicationException("Latest version doesn't contain scripts");
            
            MergeConstants(path, "ARM");
            MergeConstants(path, "DEF");

            List<ScriptEntity> savedScripts = new List<ScriptEntity>();
            using (SqlCommand cmd = new SqlCommand("ahsp_GetScriptList", ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ScriptEntity script = new ScriptEntity()
                        {
                            PATH = reader.GetString(0),
                            BODY = reader.GetString(1)
                        };
                        if (!reader.IsDBNull(2))
                        {
                            script.SUBDIRECTORY = reader.GetString(2);
                        }
                        savedScripts.Add(script);
                    }
                }
            }
            List<ReportItem> report = new List<ReportItem>();
            int i = 0;
            foreach (ScriptEntity entity in savedScripts)
            {
                string file = string.Format(@"{0}\{1}", path, entity.PATH);
                string status;
                if (File.Exists(file))
                {
                    if (entity.BODY == ParseScript(GetFileContents(file)))
                        status = "UNCHANGED";
                    else
                        status = "CHANGED";
                }
                else
                    status = "UNAVAILABLE";

                if (!onlyChanged || status == "CHANGED")
                {
                    report.Add(new ReportItem()
                    {
                        Value = entity.PATH,
                        Subdirectory = entity.SUBDIRECTORY,
                        Status = status,
                        Row = ++i
                    });
                }
            }
            return report;
        }

        private void MergeConstants(string path, string extension)
        {
            List<ConstantEntity> versionEntities = ParseConstants(GetFileContents(string.Format(@"{0}\constant.{1}", path, extension)));
            List<ConstantEntity> savedEntities = GetDBConstants(extension);
            List<ConstantEntity> result = new List<ConstantEntity>();
            foreach (ConstantEntity savedEntity in savedEntities)
            {
                ConstantEntity versionEntity = versionEntities.FirstOrDefault(e => e.CODE == savedEntity.CODE);
                if (savedEntity.LOCAL)
                {
                    result.Add(savedEntity);
                    if (versionEntity != null)
                        versionEntities.Remove(versionEntity);
                }
                else if (versionEntity == null)
                    result.Add(savedEntity);
            }
            foreach (ConstantEntity versionEntity in versionEntities)
                result.Add(versionEntity);

            using (StreamWriter writer = (new StreamWriter(string.Format(@"{0}\constant.{1}", BaseScriptPath, extension), false, Encoding.Default)))
            {
                List<ConstantEntity> localEntities = result.Where(item => item.LOCAL).ToList();
                if (localEntities != null && localEntities.Count > 0)
                {
                    writer.WriteLine(LocalBeginComment);
                    foreach (ConstantEntity entity in localEntities)
                        writer.WriteLine("{0} = {1}", entity.CODE, entity.VALUE);
                    writer.WriteLine(LocalEndComment);
                }
                List<ConstantEntity> globalEntities = result.Where(item => !item.LOCAL).ToList();
                foreach (ConstantEntity entity in globalEntities)
                    writer.WriteLine("{0} = {1}", entity.CODE, entity.VALUE);
            }

            ImportConstantsFile(extension, result);
        }

        public List<ConstantEntity> GetDBConstants(string extension)
        {
            List<ConstantEntity> result = new List<ConstantEntity>();
            using (SqlCommand cmd = new SqlCommand(string.Format("ahsp_Get{0}ConstantList", extension), ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new ConstantEntity()
                        {
                            CODE = reader.GetString(0),
                            VALUE = reader.GetString(1),
                            LOCAL = reader.GetBoolean(2)
                        });
                    }
                }
            }
            return result;
        }
    }
}
