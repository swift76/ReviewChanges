using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ReviewChangesNew
{
    public class BaseDataAccess : IDisposable
    {
        public SqlConnection ActiveConnection { get; set; }
        public string ActiveConnectionString { get; set; }

        public BaseDataAccess(string login, string password, KeyValuePair<string, string> connectionString)
        {
            SqlConnectionStringBuilder build = new SqlConnectionStringBuilder(connectionString.Value);
            build.Pooling = false;
            build.PersistSecurityInfo = true;
            build.UserID = login;
            build.Password = password;
            ActiveConnection = new SqlConnection(build.ConnectionString);
            ActiveConnection.Open();
            ActiveConnectionString = connectionString.Key;
        }

        ~BaseDataAccess()
        {
            DisposeConnection();
        }

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

        public void UpdateSubdirectory(string path, string subdirectory)
        {
            using (SqlTransaction trans = ActiveConnection.BeginTransaction())
                try
                {
                    using (SqlCommand cmd = new SqlCommand("ahsp_Update_SUBDIRECTORY", ActiveConnection, trans))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PATH", SqlDbType.NVarChar, 300).Value = path;
                        cmd.Parameters.Add("@SUBDIRECTORY", SqlDbType.VarChar, -1).Value = subdirectory;
                        cmd.ExecuteNonQuery();
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

        public void SaveScriptDB(string file, string fileContent, string subdirectory)
        {
            using (SqlTransaction trans = ActiveConnection.BeginTransaction())
                try
                {
                    using (SqlCommand cmd = new SqlCommand("ahsp_Delete_SCRIPT", ActiveConnection, trans))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PATH", SqlDbType.NVarChar, 300).Value = file;
                        cmd.ExecuteNonQuery();
                    }
                    if (fileContent.Length > 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("ahsp_Insert_SCRIPT", ActiveConnection, trans))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@PATH", SqlDbType.NVarChar, 300).Value = file;
                            cmd.Parameters.Add("@BODY", SqlDbType.VarChar, -1).Value = fileContent;
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

        public void ImportConstantsFileDB(string extension, List<ConstantEntity> entities = null)
        {
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

        public List<ScriptEntity> GetSavedScripts()
        {
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
            return savedScripts;
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
        public List<SQLScriptEntity> GetSavedSQLScripts()
        {
            List<SQLScriptEntity> savedSQLScripts = new List<SQLScriptEntity>();
            using (SqlCommand cmd = new SqlCommand("ahsp_GetSQLScriptList", ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SQLScriptEntity SQLscript = new SQLScriptEntity()
                        {
                            NAME = reader.GetString(0),
                            BODY = reader.GetString(1)
                        };
                        savedSQLScripts.Add(SQLscript);
                    }
                }
            }
            return savedSQLScripts;
        }

        public void DeleteSQLScript(string fileName)
        {
            using (SqlCommand cmd = new SqlCommand("ahsp_Delete_SQLSCRIPT", ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@NAME", SqlDbType.NVarChar, 300).Value = fileName;
                cmd.ExecuteNonQuery();
            }
        }

        public void SaveSQLScriptDB(string fileName, string fileContent)
        {
            using (SqlTransaction trans = ActiveConnection.BeginTransaction())
                try
                {
                    using (SqlCommand cmd = new SqlCommand("ahsp_Delete_SQLSCRIPT", ActiveConnection, trans))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@NAME", SqlDbType.NVarChar, 300).Value = fileName;
                        cmd.ExecuteNonQuery();
                    }
                    if (fileContent.Length > 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("ahsp_Insert_SQLSCRIPT", ActiveConnection, trans))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@NAME", SqlDbType.NVarChar, 300).Value = fileName;
                            cmd.Parameters.Add("@BODY", SqlDbType.VarChar, -1).Value = fileContent;
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
    }
}
