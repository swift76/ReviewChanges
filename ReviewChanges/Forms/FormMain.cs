using System;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using System.Diagnostics;

namespace ReviewChanges
{
    public partial class FormMain : Form
    {
        public FormLogin LoginForm { get; set; }
        public BaseDataAccess DataAccess { get; set; }
        public string MergeTool { get; set; }

        public FormMain()
        {
            InitializeComponent();

            MergeTool = ConfigurationManager.AppSettings["MergeTool"];
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Exit Application?", "Exit", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
            DataAccess.DisposeConnection();
            LoginForm.Close();
        }

        private void buttonImportConstants_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Import Constants?", "Review Changes", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    DataAccess.SaveConstants();
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
            fileDialog.InitialDirectory = DataAccess.BaseScriptPath;
            fileDialog.Filter = "AS files (*.as)|*.as";
            fileDialog.RestoreDirectory = true;
            fileDialog.FileName = string.Empty;
            if (fileDialog.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    DataAccess.SaveScript(fileDialog.FileName);
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
                dataReview.DataSource = DataAccess.CompareAndMerge(onlyChanged, out path);
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

        private void importChangedScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataReview != null && dataReview.SelectedRows.Count == 1 && GetStringCellValue("Status") == "CHANGED")
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    DataAccess.SaveScript(string.Format(@"{0}\{1}", dataReview.Tag.ToString(), GetStringCellValue("Value"), GetStringCellValue("Subdirectory")));
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
                if (DataAccess.IsRealTest)
                    Process.Start(GetRealFileName());
                else
                    Process.Start(GetTestFileName());
            }
        }

        private void openVersionScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataReview != null && dataReview.SelectedRows.Count == 1)
                Process.Start(GetVersionFileName());
        }

        private void deleteScriptFromDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataReview != null && dataReview.SelectedRows.Count == 1 && MessageBox.Show("Are you sure?", "Delete script from database", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataAccess.DeleteScript(GetStringCellValue("Value"));
                if (DataAccess.IsRealTest)
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
                    if (DataAccess.IsRealTest)
                        currentFile = GetRealFileName();
                    else
                        currentFile = GetTestFileName();
                    Process.Start(MergeTool, string.Format("\"{0}\" \"{1}\"", currentFile, GetVersionFileName()));
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
                    Process.Start(testFile);
                    Process.Start(realFile);
                }
                try
                {
                    Process.Start(MergeTool, string.Format("{0} \"{1}\"", testFile, realFile));
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
            return $"{Directory.GetDirectories(DataAccess.BaseScriptPath).First(d => d.EndsWith("_TEST"))}\\Scripts\\Changes\\{GetFileName(row_index)}";
        }

        private string GetRealFileName(int row_index = -1)
        {
            return $"{Directory.GetDirectories(DataAccess.BaseScriptPath).First(d => d.EndsWith("_REAL"))}\\Scripts\\{GetFileName(row_index)}";
        }

        private string GetVersionFileName()
        {
            return $"{Directory.GetDirectories(DataAccess.VersionPath).OrderByDescending(d => d.ToUpper()).First()}\\BankScript\\{GetStringCellValue("Value")}";
        }

        private string GetChangesFromScript(string fileName)
        {
            string[] fileRows = File.ReadAllLines(fileName);
            StringBuilder builder = new StringBuilder();
            bool isComment = false;
            for (int i = 0; i < fileRows.Length; i++)
            {
                string row = fileRows[i].Trim();
                if (row.ToUpper() == DataAccess.LocalBeginComment)
                {
                    isComment = true;
                }
                else if (row.ToUpper() == DataAccess.LocalEndComment)
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
                            DataAccess.SaveScript(string.Format(@"{0}\{1}", dataReview.Tag.ToString(), GetStringCellValue("Value", i), GetStringCellValue("Subdirectory", i)));
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
    }
}
