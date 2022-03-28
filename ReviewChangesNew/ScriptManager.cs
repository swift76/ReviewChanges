using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;

namespace ReviewChangesNew
{
    public class ScriptManager
    {
        public string BaseScriptPath { get; set; }
        public string VersionPath { get; set; }
        public string LocalBeginComment { get; set; }
        public string LocalEndComment { get; set; }
        public BaseDataAccess dataAccess { get; set; }
        public List<ReportItem> report { get; set; }

        public ScriptManager(BaseDataAccess dataAccess, Settings appSettings)
        {
            string bankPrefix = appSettings.BankCode;
            this.LocalBeginComment = $"'''{bankPrefix} BEGIN";
            this.LocalEndComment = $"'''{bankPrefix} END";
            this.dataAccess = dataAccess;
            this.BaseScriptPath = appSettings.BaseScriptPath;
            this.VersionPath = appSettings.VersionPath;
            this.report = new List<ReportItem>(); 
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
            dataAccess.SaveScriptDB(file, text, subdirectory);
        }

        private void ImportConstantsFile(string extension, List<ConstantEntity> entities = null)
        {
            if (entities == null)
                entities = ParseConstants(GetFileContents(string.Format(@"{0}\constant.{1}", BaseScriptPath, extension)));
            dataAccess.ImportConstantsFileDB(extension, entities);
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

        public string ParseScript(string text)
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

        public void CompareAndMerge(bool onlyChanged, out string path)
        {
            if (!Directory.Exists(VersionPath))
                throw new ApplicationException("Versions directory is wrong");

            string[] directories = Directory.GetDirectories(VersionPath);
            if (directories.Length == 0)
                throw new ApplicationException("Versions directory is empty");

            path = string.Format(@"{0}\BankScript", directories.OrderByDescending(d => d.ToUpper()).First());

            if (!Directory.Exists(path))
                throw new ApplicationException("Latest version doesn't contain scripts");

            report = new List<ReportItem>();

            MergeConstants(path, "ARM");
            MergeConstants(path, "DEF");

            List<ScriptEntity> savedScripts = new List<ScriptEntity>();
            savedScripts = dataAccess.GetSavedScripts();

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
        }

        public string GetFileContents(string file)
        {
            return File.ReadAllText(file, Encoding.GetEncoding(1252));
        }

        private void MergeConstants(string path, string extension)
        {
            List<ConstantEntity> versionEntities = ParseConstants(GetFileContents(string.Format(@"{0}\constant.{1}", path, extension)));
            List<ConstantEntity> savedEntities = dataAccess.GetDBConstants(extension);
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

            using (StreamWriter writer = (new StreamWriter(string.Format(@"{0}\constant.{1}", BaseScriptPath, extension), false, Encoding.GetEncoding(1252))))
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
    }
}
