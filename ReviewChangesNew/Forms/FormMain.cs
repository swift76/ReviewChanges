using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using System.Diagnostics;
using System.Data.SqlClient;

namespace ReviewChangesNew
{
    public partial class FormMain : Form
    {
        public FormLogin loginForm { get; set; }
        public BaseDataAccess dataAccess { get; set; }
        private ScriptManager Script { get; set; }
        private SQLScriptManager sqlScript { get; set; }
        private string mergeTool { get; set; }
        private string previousSubdirectoryValue { get; set; }

        Settings appSettings;

        public FormMain()
        {
            InitializeComponent();

            mergeTool = ConfigurationManager.AppSettings["MergeTool"];
        }

        public FormMain(Settings appSettings, BaseDataAccess dataAccess)
        {
            InitializeComponent();
            this.appSettings = appSettings;
            this.dataAccess = dataAccess;
            this.Script = new ScriptManager(this.dataAccess, this.appSettings);
            this.sqlScript = new SQLScriptManager(this.dataAccess, this.Script);
            mergeTool = appSettings.MergeTool;
            this.Text = $"Reveiw Changes({dataAccess.ActiveConnectionString})";
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Exit Application?", "Exit", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
            dataAccess.DisposeConnection();
            loginForm.Close();
        }

        private void buttonImportConstants_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Import Constants?", "Review Changes", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    Script.SaveConstants();
                    MessageBox.Show("Import Constants succeeded");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Cursor = Cursors.Arrow;
                }
            }
        }

        private void buttonImportScript_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = Script.BaseScriptPath;
            fileDialog.Filter = "AS files (*.as)|*.as";
            fileDialog.RestoreDirectory = true;
            fileDialog.FileName = string.Empty;
            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    Script.SaveScript(fileDialog.FileName);
                    MessageBox.Show("Import Script succeeded");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Cursor = Cursors.Arrow;
                }
            }
        }

        private void buttonMergeCompare_Click(object sender, EventArgs e)
        {
            try
            {
                bool onlyChanged = (MessageBox.Show("Show changed only?", "Review Changes", MessageBoxButtons.YesNo) == DialogResult.Yes);
                Cursor = Cursors.WaitCursor;
                string path;
                Script.CompareAndMerge(onlyChanged, out path);
                dataReview.DataSource = dataReview.DataSource = ApplySearchInGrid();
                dataReview.Tag = path;
                MessageBox.Show("Merge Constants and Compare Scripts succeeded");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void buttonCompareSQLScript_Click(object sender, EventArgs e)
        {
            try
            {
                bool onlyChanged = (MessageBox.Show("Show changed only?", "Review Changes", MessageBoxButtons.YesNo) == DialogResult.Yes);
                Cursor = Cursors.WaitCursor;
                string path;
                dataReview.DataSource = sqlScript.CompareSQLScript(onlyChanged, out path);
                dataReview.Tag = path;
                MessageBox.Show("Compare SQL Scripts succeeded");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void importChangedScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataReview != null && dataReview.SelectedRows.Count == 1 && GetStringCellValue("Status") == "CHANGED")
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    Script.SaveScript(string.Format(@"{0}\{1}", dataReview.Tag.ToString(), GetStringCellValue("Value")), GetStringCellValue("Subdirectory"));
                    MessageBox.Show("Import Script succeeded");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Cursor = Cursors.Arrow;
                }
            }
        }

        public string GetStringCellValue(string column_name, int row_index = -1)
        {
            DataGridViewRow row;
            if (row_index == -1)
                row = dataReview.SelectedRows[0];
            else
                row = dataReview.Rows[row_index];
            if (row.Cells[column_name].Value == DBNull.Value || row.Cells[column_name].Value == null)
                return string.Empty;
            else
                return row.Cells[column_name].Value.ToString();
        }

        private void openCurrentScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataReview != null && dataReview.SelectedRows.Count == 1)
            {
                if (dataAccess.ActiveConnection.ConnectionString == "real")
                    new Process { StartInfo = new ProcessStartInfo(GetRealFileName()) { UseShellExecute = true } }.Start();
                else
                    new Process { StartInfo = new ProcessStartInfo(GetTestFileName()) { UseShellExecute = true } }.Start();
            }
        }

        private void openVersionScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataReview != null && dataReview.SelectedRows.Count == 1)
                new Process { StartInfo = new ProcessStartInfo(GetVersionFileName()) { UseShellExecute = true } }.Start();
        }

        private void deleteScriptFromDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataReview != null && dataReview.SelectedRows.Count == 1 && MessageBox.Show("Are you sure?", "Delete script from database", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dataAccess.DeleteScript(GetStringCellValue("Value"));
                if (dataAccess.ActiveConnection.ConnectionString == "real")
                    File.Delete(GetRealFileName());
                else
                    File.Delete(GetTestFileName());
            }
        }

        private void openBothScriptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataReview != null && dataReview.SelectedRows.Count == 1)
            {
                if (checkOpenEditor.Checked)
                {
                    openCurrentScriptToolStripMenuItem_Click(sender, e);
                    openVersionScriptToolStripMenuItem_Click(sender, e);
                }
                try
                {
                    string currentFile;
                    if (dataAccess.ActiveConnection.ConnectionString == "real")
                        currentFile = GetRealFileName();
                    else
                        currentFile = GetTestFileName();
                    Process.Start(mergeTool, string.Format("\"{0}\" \"{1}\"", currentFile, GetVersionFileName()));
                }
                catch
                {
                }
            }
        }

        private void openBothTestAndRealToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataReview != null && dataReview.SelectedRows.Count == 1)
            {
                string testFile = GetTestFileName();
                string realFile = GetRealFileName();
                if (checkOpenEditor.Checked)
                {
                    new Process { StartInfo = new ProcessStartInfo(testFile) { UseShellExecute = true } }.Start();
                    new Process { StartInfo = new ProcessStartInfo(realFile) { UseShellExecute = true } }.Start();
                }
                try
                {
                    Process.Start(mergeTool, string.Format("{0} \"{1}\"", testFile, realFile));
                }
                catch
                {
                }
            }
        }

        private void copyTestScriptToRealToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataReview != null && dataReview.SelectedRows.Count == 1 && MessageBox.Show("Are you sure?", "Copy test script to real", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string testFile = GetTestFileName();
                string realFile = GetRealFileName();
                ResetFileReadOnly(realFile);
                File.Copy(testFile, realFile, true);
            }
        }

        private void compareChangesInTestAndRealScriptsMenuItem_Click(object sender, EventArgs e)
        {
            if (GetChangesFromScript(GetTestFileName()) == GetChangesFromScript(GetRealFileName()))
                MessageBox.Show("Changes are IDENTICAL", "Comparison result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Changes are DIFFERENT", "Comparison result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private string GetFileName(int row_index = -1)
        {
            string fileName = GetStringCellValue("Value", row_index);
            string subdirectory = GetStringCellValue("Subdirectory", row_index);
            int folderPosition = fileName.LastIndexOf("\\");
            fileName = fileName.Substring(folderPosition + 1);
            if (!string.IsNullOrWhiteSpace(subdirectory))
                fileName = $"{subdirectory}\\{fileName}";
            return fileName;
        }

        private string GetTestFileName(int row_index = -1)
        {
            return $"{Directory.GetDirectories(Script.BaseScriptPath).First(d => d.EndsWith("_TEST"))}\\Scripts\\Changes\\{GetFileName(row_index)}";
        }

        private string GetRealFileName(int row_index = -1)
        {
            return $"{Directory.GetDirectories(Script.BaseScriptPath).First(d => d.EndsWith("_REAL"))}\\Scripts\\{GetFileName(row_index)}";
        }

        private string GetVersionFileName()
        {
            return $"{Directory.GetDirectories(Script.VersionPath).OrderByDescending(d => d.ToUpper()).First()}\\BankScript\\{GetStringCellValue("Value")}";
        }

        private string GetChangesFromScript(string fileName)
        {
            string[] fileRows = File.ReadAllLines(fileName);
            StringBuilder builder = new StringBuilder();
            bool isComment = false;
            for (int i = 0; i < fileRows.Length; i++)
            {
                string row = fileRows[i].Trim();
                if (row.ToUpper() == Script.LocalBeginComment)
                {
                    isComment = true;
                }
                else if (row.ToUpper() == Script.LocalEndComment)
                {
                    isComment = false;
                }
                else if (isComment)
                {
                    if (row != string.Empty)
                        builder.AppendLine(row);
                }
            }
            return builder.ToString();
        }

        private void migrateAllFromTestToRealToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataReview != null && dataReview.RowCount > 0 && MessageBox.Show("Are you sure?", "Migrate all from test to real", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                for (int i = 0; i < dataReview.RowCount; i++)
                {
                    try
                    {
                        string testFile = GetTestFileName(i);
                        string realFile = GetRealFileName(i);

                        if (GetChangesFromScript(testFile) == GetChangesFromScript(realFile))
                        {
                            ResetFileReadOnly(realFile);
                            File.Copy(testFile, realFile, true);
                            Script.SaveScript(string.Format(@"{0}\{1}", dataReview.Tag.ToString(), GetStringCellValue("Value", i), GetStringCellValue("Subdirectory", i)));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                buttonMergeCompare_Click(sender, e);
            }
        }

        private void ResetFileReadOnly(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            if (fileInfo.IsReadOnly)
                fileInfo.IsReadOnly = false;
        }

        private void ImportSQLScript_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = Script.BaseScriptPath;
            fileDialog.Filter = "AS files (*.sql)|*.sql";
            fileDialog.RestoreDirectory = true;
            fileDialog.FileName = string.Empty;
            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    sqlScript.SaveSQLScript(fileDialog.FileName);
                    MessageBox.Show("Import SQL Script succeeded");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Cursor = Cursors.Arrow;
                }
            }
        }

        private void setSubdirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedRow = dataReview.CurrentRow.Index;
            int SubdirectoryIndex = dataReview.Columns.IndexOf(dataReview.Columns["Subdirectory"]);
            DataGridViewCell cell = dataReview.Rows[selectedRow].Cells[SubdirectoryIndex];
            previousSubdirectoryValue = cell.Value?.ToString();
            dataReview.CurrentCell = cell;
            dataReview.BeginEdit(true);               
        }

        private void Search_Click(object sender, EventArgs e)
        {
            dataReview.DataSource = ApplySearchInGrid();
        }

        public List<ReportItem> ApplySearchInGrid()
        {
            string status = comboStatus.SelectedItem?.ToString();
            string path = txtPath.Text?.Trim();
            List<ReportItem> reportItems = new List<ReportItem>();
            foreach (ReportItem item in Script.report)
            {
                if (item.Status.StartsWith(string.IsNullOrEmpty(status) ? string.Empty : status) 
                    && item.Value.ToUpper().Contains(string.IsNullOrEmpty(path) ? string.Empty : path.ToUpper()))
                {
                    reportItems.Add(item);
                }
            }

            return reportItems;
        }

        private void dataReview_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int selectedRow = dataReview.CurrentRow.Index;
            int subdirectoryIndex = dataReview.Columns.IndexOf(dataReview.Columns["Subdirectory"]);
            int pathindex = dataReview.Columns.IndexOf(dataReview.Columns["Value"]);
            string value = dataReview[pathindex, selectedRow].Value.ToString();
            if (MessageBox.Show($"Are you sure you want to update subdirectory of {value}?", "Review Changes", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string subdirectory = dataReview[subdirectoryIndex, selectedRow].Value?.ToString().Trim();
                dataAccess.UpdateSubdirectory(value, subdirectory == string.Empty ? null : subdirectory);
            }
            else
            {
                dataReview[subdirectoryIndex, selectedRow].Value = previousSubdirectoryValue;
            }
        }

        private void connectionButton_Click(object sender, EventArgs e)
        {
            string newConnection;
            string newConnectionName;
            if (dataAccess.ActiveConnectionString == "real")
            {
                newConnection = loginForm.connectionStrings["test"];
                newConnectionName = "test";
            }
            else
            {
                newConnection = loginForm.connectionStrings["real"];
                newConnectionName = "real";
            }

            SqlConnectionStringBuilder build = new SqlConnectionStringBuilder(newConnection);
            dataAccess.ActiveConnection = new SqlConnection(build.ConnectionString);
            dataAccess.ActiveConnection.Open();
            this.Text = $"Reveiw Changes ({newConnectionName})";
        }
    }
}
