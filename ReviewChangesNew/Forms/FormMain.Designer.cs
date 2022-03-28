
namespace ReviewChangesNew
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelCommand = new System.Windows.Forms.Panel();
            this.CompareSQLScript = new System.Windows.Forms.Button();
            this.ImportSQLScript = new System.Windows.Forms.Button();
            this.checkOpenEditor = new System.Windows.Forms.CheckBox();
            this.buttonMergeCompare = new System.Windows.Forms.Button();
            this.buttonImportScript = new System.Windows.Forms.Button();
            this.buttonImportConstants = new System.Windows.Forms.Button();
            this.labelProgress = new System.Windows.Forms.Label();
            this.panelData = new System.Windows.Forms.Panel();
            this.panelView = new System.Windows.Forms.Panel();
            this.dataReview = new System.Windows.Forms.DataGridView();
            this.changeContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.importChangedScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCurrentScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openVersionScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openBothScriptsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteScriptFromDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openBothTestAndRealToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compareChangesInTestAndRealScriptsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyTestScriptToRealToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.migrateAllFromTestToRealToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setSubdirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.comboStatus = new System.Windows.Forms.ComboBox();
            this.labelPath = new System.Windows.Forms.Label();
            this.labelSearch = new System.Windows.Forms.Label();
            this.SearchButton = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.panelCommand.SuspendLayout();
            this.panelData.SuspendLayout();
            this.panelView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataReview)).BeginInit();
            this.changeContextMenu.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCommand
            // 
            this.panelCommand.Controls.Add(this.CompareSQLScript);
            this.panelCommand.Controls.Add(this.ImportSQLScript);
            this.panelCommand.Controls.Add(this.checkOpenEditor);
            this.panelCommand.Controls.Add(this.buttonMergeCompare);
            this.panelCommand.Controls.Add(this.buttonImportScript);
            this.panelCommand.Controls.Add(this.buttonImportConstants);
            this.panelCommand.Controls.Add(this.labelProgress);
            this.panelCommand.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelCommand.Location = new System.Drawing.Point(0, 539);
            this.panelCommand.Margin = new System.Windows.Forms.Padding(4);
            this.panelCommand.Name = "panelCommand";
            this.panelCommand.Size = new System.Drawing.Size(944, 46);
            this.panelCommand.TabIndex = 0;
            // 
            // CompareSQLScript
            // 
            this.CompareSQLScript.Location = new System.Drawing.Point(482, 5);
            this.CompareSQLScript.Margin = new System.Windows.Forms.Padding(2);
            this.CompareSQLScript.Name = "CompareSQLScript";
            this.CompareSQLScript.Size = new System.Drawing.Size(127, 34);
            this.CompareSQLScript.TabIndex = 5;
            this.CompareSQLScript.Text = "Compare SQL Script";
            this.CompareSQLScript.UseVisualStyleBackColor = true;
            this.CompareSQLScript.Click += new System.EventHandler(this.buttonCompareSQLScript_Click);
            // 
            // ImportSQLScript
            // 
            this.ImportSQLScript.Location = new System.Drawing.Point(363, 5);
            this.ImportSQLScript.Margin = new System.Windows.Forms.Padding(2);
            this.ImportSQLScript.Name = "ImportSQLScript";
            this.ImportSQLScript.Size = new System.Drawing.Size(114, 35);
            this.ImportSQLScript.TabIndex = 1;
            this.ImportSQLScript.Text = "Import SQL Script";
            this.ImportSQLScript.UseVisualStyleBackColor = true;
            this.ImportSQLScript.Click += new System.EventHandler(this.ImportSQLScript_Click);
            // 
            // checkOpenEditor
            // 
            this.checkOpenEditor.AutoSize = true;
            this.checkOpenEditor.Location = new System.Drawing.Point(611, 15);
            this.checkOpenEditor.Margin = new System.Windows.Forms.Padding(4);
            this.checkOpenEditor.Name = "checkOpenEditor";
            this.checkOpenEditor.Size = new System.Drawing.Size(266, 26);
            this.checkOpenEditor.TabIndex = 4;
            this.checkOpenEditor.Text = "Open editor when comparing";
            this.checkOpenEditor.UseVisualStyleBackColor = true;
            // 
            // buttonMergeCompare
            // 
            this.buttonMergeCompare.Location = new System.Drawing.Point(204, 5);
            this.buttonMergeCompare.Margin = new System.Windows.Forms.Padding(4);
            this.buttonMergeCompare.Name = "buttonMergeCompare";
            this.buttonMergeCompare.Size = new System.Drawing.Size(154, 36);
            this.buttonMergeCompare.TabIndex = 3;
            this.buttonMergeCompare.Text = "Merge Constants, Scripts";
            this.buttonMergeCompare.UseVisualStyleBackColor = true;
            this.buttonMergeCompare.Click += new System.EventHandler(this.buttonMergeCompare_Click);
            // 
            // buttonImportScript
            // 
            this.buttonImportScript.Location = new System.Drawing.Point(116, 5);
            this.buttonImportScript.Margin = new System.Windows.Forms.Padding(4);
            this.buttonImportScript.Name = "buttonImportScript";
            this.buttonImportScript.Size = new System.Drawing.Size(84, 37);
            this.buttonImportScript.TabIndex = 2;
            this.buttonImportScript.Text = "Import Script";
            this.buttonImportScript.UseVisualStyleBackColor = true;
            this.buttonImportScript.Click += new System.EventHandler(this.buttonImportScript_Click);
            // 
            // buttonImportConstants
            // 
            this.buttonImportConstants.Location = new System.Drawing.Point(4, 5);
            this.buttonImportConstants.Margin = new System.Windows.Forms.Padding(4);
            this.buttonImportConstants.Name = "buttonImportConstants";
            this.buttonImportConstants.Size = new System.Drawing.Size(108, 36);
            this.buttonImportConstants.TabIndex = 1;
            this.buttonImportConstants.Text = "Import Constants";
            this.buttonImportConstants.UseVisualStyleBackColor = true;
            this.buttonImportConstants.Click += new System.EventHandler(this.buttonImportConstants_Click);
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(14, 21);
            this.labelProgress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(0, 22);
            this.labelProgress.TabIndex = 0;
            // 
            // panelData
            // 
            this.panelData.AutoScroll = true;
            this.panelData.Controls.Add(this.panelView);
            this.panelData.Controls.Add(this.panelSearch);
            this.panelData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelData.Location = new System.Drawing.Point(0, 0);
            this.panelData.Margin = new System.Windows.Forms.Padding(4);
            this.panelData.Name = "panelData";
            this.panelData.Size = new System.Drawing.Size(944, 539);
            this.panelData.TabIndex = 1;
            // 
            // panelView
            // 
            this.panelView.Controls.Add(this.dataReview);
            this.panelView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelView.Location = new System.Drawing.Point(0, 31);
            this.panelView.Name = "panelView";
            this.panelView.Size = new System.Drawing.Size(944, 508);
            this.panelView.TabIndex = 11;
            // 
            // dataReview
            // 
            this.dataReview.AllowUserToAddRows = false;
            this.dataReview.AllowUserToDeleteRows = false;
            this.dataReview.AllowUserToOrderColumns = true;
            this.dataReview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataReview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataReview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataReview.ContextMenuStrip = this.changeContextMenu;
            this.dataReview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataReview.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataReview.Location = new System.Drawing.Point(0, 0);
            this.dataReview.Margin = new System.Windows.Forms.Padding(4);
            this.dataReview.Name = "dataReview";
            this.dataReview.RowHeadersWidth = 62;
            this.dataReview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataReview.Size = new System.Drawing.Size(944, 508);
            this.dataReview.TabIndex = 0;
            this.dataReview.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataReview_CellEndEdit);
            this.dataReview.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataReview_KeyDown);
            // 
            // changeContextMenu
            // 
            this.changeContextMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.changeContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importChangedScriptToolStripMenuItem,
            this.openCurrentScriptToolStripMenuItem,
            this.openVersionScriptToolStripMenuItem,
            this.openBothScriptsToolStripMenuItem,
            this.deleteScriptFromDatabaseToolStripMenuItem,
            this.openBothTestAndRealToolStripMenuItem,
            this.compareChangesInTestAndRealScriptsMenuItem,
            this.copyTestScriptToRealToolStripMenuItem,
            this.migrateAllFromTestToRealToolStripMenuItem,
            this.setSubdirectoryToolStripMenuItem});
            this.changeContextMenu.Name = "changeContextMenu";
            this.changeContextMenu.Size = new System.Drawing.Size(349, 324);
            // 
            // importChangedScriptToolStripMenuItem
            // 
            this.importChangedScriptToolStripMenuItem.Name = "importChangedScriptToolStripMenuItem";
            this.importChangedScriptToolStripMenuItem.Size = new System.Drawing.Size(348, 32);
            this.importChangedScriptToolStripMenuItem.Text = "Import changed";
            this.importChangedScriptToolStripMenuItem.Click += new System.EventHandler(this.importChangedScriptToolStripMenuItem_Click);
            // 
            // openCurrentScriptToolStripMenuItem
            // 
            this.openCurrentScriptToolStripMenuItem.Name = "openCurrentScriptToolStripMenuItem";
            this.openCurrentScriptToolStripMenuItem.Size = new System.Drawing.Size(348, 32);
            this.openCurrentScriptToolStripMenuItem.Text = "Open current";
            this.openCurrentScriptToolStripMenuItem.Click += new System.EventHandler(this.openCurrentScriptToolStripMenuItem_Click);
            // 
            // openVersionScriptToolStripMenuItem
            // 
            this.openVersionScriptToolStripMenuItem.Name = "openVersionScriptToolStripMenuItem";
            this.openVersionScriptToolStripMenuItem.Size = new System.Drawing.Size(348, 32);
            this.openVersionScriptToolStripMenuItem.Text = "Open version";
            this.openVersionScriptToolStripMenuItem.Click += new System.EventHandler(this.openVersionScriptToolStripMenuItem_Click);
            // 
            // openBothScriptsToolStripMenuItem
            // 
            this.openBothScriptsToolStripMenuItem.Name = "openBothScriptsToolStripMenuItem";
            this.openBothScriptsToolStripMenuItem.Size = new System.Drawing.Size(348, 32);
            this.openBothScriptsToolStripMenuItem.Text = "Open both current and version";
            this.openBothScriptsToolStripMenuItem.Click += new System.EventHandler(this.openBothScriptsToolStripMenuItem_Click);
            // 
            // deleteScriptFromDatabaseToolStripMenuItem
            // 
            this.deleteScriptFromDatabaseToolStripMenuItem.Name = "deleteScriptFromDatabaseToolStripMenuItem";
            this.deleteScriptFromDatabaseToolStripMenuItem.Size = new System.Drawing.Size(348, 32);
            this.deleteScriptFromDatabaseToolStripMenuItem.Text = "Delete from database";
            this.deleteScriptFromDatabaseToolStripMenuItem.Click += new System.EventHandler(this.deleteScriptFromDatabaseToolStripMenuItem_Click);
            // 
            // openBothTestAndRealToolStripMenuItem
            // 
            this.openBothTestAndRealToolStripMenuItem.Name = "openBothTestAndRealToolStripMenuItem";
            this.openBothTestAndRealToolStripMenuItem.Size = new System.Drawing.Size(348, 32);
            this.openBothTestAndRealToolStripMenuItem.Text = "Open both test and real";
            this.openBothTestAndRealToolStripMenuItem.Click += new System.EventHandler(this.openBothTestAndRealToolStripMenuItem_Click);
            // 
            // compareChangesInTestAndRealScriptsMenuItem
            // 
            this.compareChangesInTestAndRealScriptsMenuItem.Name = "compareChangesInTestAndRealScriptsMenuItem";
            this.compareChangesInTestAndRealScriptsMenuItem.Size = new System.Drawing.Size(348, 32);
            this.compareChangesInTestAndRealScriptsMenuItem.Text = "Compare changes in test and real";
            this.compareChangesInTestAndRealScriptsMenuItem.Click += new System.EventHandler(this.compareChangesInTestAndRealScriptsMenuItem_Click);
            // 
            // copyTestScriptToRealToolStripMenuItem
            // 
            this.copyTestScriptToRealToolStripMenuItem.Name = "copyTestScriptToRealToolStripMenuItem";
            this.copyTestScriptToRealToolStripMenuItem.Size = new System.Drawing.Size(348, 32);
            this.copyTestScriptToRealToolStripMenuItem.Text = "Copy test to real";
            this.copyTestScriptToRealToolStripMenuItem.Click += new System.EventHandler(this.copyTestScriptToRealToolStripMenuItem_Click);
            // 
            // migrateAllFromTestToRealToolStripMenuItem
            // 
            this.migrateAllFromTestToRealToolStripMenuItem.Name = "migrateAllFromTestToRealToolStripMenuItem";
            this.migrateAllFromTestToRealToolStripMenuItem.Size = new System.Drawing.Size(348, 32);
            this.migrateAllFromTestToRealToolStripMenuItem.Text = "Migrate all from test to real";
            this.migrateAllFromTestToRealToolStripMenuItem.Click += new System.EventHandler(this.migrateAllFromTestToRealToolStripMenuItem_Click);
            // 
            // setSubdirectoryToolStripMenuItem
            // 
            this.setSubdirectoryToolStripMenuItem.Name = "setSubdirectoryToolStripMenuItem";
            this.setSubdirectoryToolStripMenuItem.Size = new System.Drawing.Size(348, 32);
            this.setSubdirectoryToolStripMenuItem.Text = "Set subdirectory ";
            this.setSubdirectoryToolStripMenuItem.Click += new System.EventHandler(this.setSubdirectoryToolStripMenuItem_Click);
            // 
            // panelSearch
            // 
            this.panelSearch.Controls.Add(this.comboStatus);
            this.panelSearch.Controls.Add(this.labelPath);
            this.panelSearch.Controls.Add(this.labelSearch);
            this.panelSearch.Controls.Add(this.SearchButton);
            this.panelSearch.Controls.Add(this.txtPath);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 0);
            this.panelSearch.Margin = new System.Windows.Forms.Padding(5);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(944, 31);
            this.panelSearch.TabIndex = 10;
            // 
            // comboStatus
            // 
            this.comboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboStatus.FormattingEnabled = true;
            this.comboStatus.Items.AddRange(new object[] {
            "",
            "CHANGED",
            "UNCHANGED",
            "UNAVAILABLE"});
            this.comboStatus.Location = new System.Drawing.Point(46, 4);
            this.comboStatus.Name = "comboStatus";
            this.comboStatus.Size = new System.Drawing.Size(160, 30);
            this.comboStatus.TabIndex = 12;
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new System.Drawing.Point(216, 7);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(47, 22);
            this.labelPath.TabIndex = 11;
            this.labelPath.Text = "Path";
            // 
            // labelSearch
            // 
            this.labelSearch.AutoSize = true;
            this.labelSearch.Location = new System.Drawing.Point(6, 7);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(61, 22);
            this.labelSearch.TabIndex = 10;
            this.labelSearch.Text = "Status";
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(420, 3);
            this.SearchButton.Margin = new System.Windows.Forms.Padding(2);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(108, 23);
            this.SearchButton.TabIndex = 6;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.Search_Click);
            // 
            // txtPath
            // 
            this.txtPath.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtPath.Location = new System.Drawing.Point(248, 4);
            this.txtPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(160, 28);
            this.txtPath.TabIndex = 8;
            this.txtPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 585);
            this.Controls.Add(this.panelData);
            this.Controls.Add(this.panelCommand);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Review Changes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.panelCommand.ResumeLayout(false);
            this.panelCommand.PerformLayout();
            this.panelData.ResumeLayout(false);
            this.panelView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataReview)).EndInit();
            this.changeContextMenu.ResumeLayout(false);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelCommand;
        private System.Windows.Forms.Panel panelData;
        private System.Windows.Forms.DataGridView dataReview;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.Button buttonMergeCompare;
        private System.Windows.Forms.Button buttonImportScript;
        private System.Windows.Forms.Button buttonImportConstants;
        private System.Windows.Forms.ContextMenuStrip changeContextMenu;
        private System.Windows.Forms.ToolStripMenuItem importChangedScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCurrentScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openVersionScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteScriptFromDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openBothScriptsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openBothTestAndRealToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyTestScriptToRealToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compareChangesInTestAndRealScriptsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem migrateAllFromTestToRealToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkOpenEditor;
        private System.Windows.Forms.Button ImportSQLScript;
        private System.Windows.Forms.Button CompareSQLScript;
        private System.Windows.Forms.ToolStripMenuItem setSubdirectoryToolStripMenuItem;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.ComboBox comboStatus;
        private System.Windows.Forms.Panel panelView;
    }
}