using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ReviewChangesNew
{
    public class SQLScriptManager
    {
        public BaseDataAccess dataAccess { get; set; }
        private ScriptManager script { get; set; }

        public SQLScriptManager(BaseDataAccess dataAccess, ScriptManager script)
        {
            this.dataAccess = dataAccess;
            this.script = script;
        }
        private bool ParseSQLScript(string text, string objectName)
        {
            Regex createobject = new Regex($"CREATE.[A-Za-z]*?\\s({objectName})", RegexOptions.Compiled);
            if (createobject.IsMatch(text))
            {
                return true;
            }
            else
                return false;
        }

        public void SaveSQLScript(string file)
        {
            int position = file.IndexOf(@"BankScript\");
            if (position == -1)
                throw new ApplicationException("Script from wrong destination selected");

            string text = script.ParseScript(script.GetFileContents(file));
            file = file.Substring(position + 11);
            string fileName = Path.GetFileName(file);
            dataAccess.SaveSQLScriptDB(fileName, text);
        }

        public List<ReportItem> CompareSQLScript(bool onlyChanged, out string path)
        {
            if (!Directory.Exists(script.VersionPath))
                throw new ApplicationException("Versions directory is wrong");

            string[] directories = Directory.GetDirectories(script.VersionPath);
            if (directories.Length == 0)
                throw new ApplicationException("Versions directory is empty");

            path = string.Format(@"{0}\SQLScript", directories.OrderByDescending(d => d.ToUpper()).First());
            string[] fileNames = Directory.GetFiles(path, "*.sql");

            if (!Directory.Exists(path))
                throw new ApplicationException("Latest version doesn't contain scripts");

            List<SQLScriptEntity> savedSQLScripts = new List<SQLScriptEntity>();
            savedSQLScripts = dataAccess.GetSavedSQLScripts();

            List<ReportItem> report = new List<ReportItem>();
            int i = 0;
            foreach (SQLScriptEntity entity in savedSQLScripts)
            {
                string status = "";
                foreach (string file in fileNames)
                {
                    string filepath = string.Format(@"{0}\{1}", path, file);
                    string entityName = entity.NAME;
                    if (File.Exists(file))
                    {
                        if (ParseSQLScript(script.GetFileContents(file), entityName.Substring(0, entityName.LastIndexOf('.'))))
                            status = "CHANGED";
                        else
                            status = "UNCHANGED";
                    }
                    else
                        status = "UNAVAILABLE";
                }
                if (!onlyChanged || status == "CHANGED")
                {
                    report.Add(new ReportItem()
                    {
                        Value = entity.NAME,
                        Status = status,
                        Row = ++i
                    });
                }
            }
            return report;
        }
    }
}
