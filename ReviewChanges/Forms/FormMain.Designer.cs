namespace ReviewChanges
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
            this.buttonMergeCompare = new System.Windows.Forms.Button();
            this.buttonImportScript = new System.Windows.Forms.Button();
            this.buttonImportConstants = new System.Windows.Forms.Button();
            this.labelProgress = new System.Windows.Forms.Label();
            this.panelData = new System.Windows.Forms.Panel();
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
            this.checkOpenEditor = new System.Windows.Forms.CheckBox();
            this.panelCommand.SuspendLayout();
            this.panelData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataReview)).BeginInit();
            this.changeContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCommand
            // 
            this.panelCommand.Controls.Add(this.checkOpenEditor);
            this.panelCommand.Controls.Add(this.buttonMergeCompare);
            this.panelCommand.Controls.Add(this.buttonImportScript);
            this.panelCommand.Controls.Add(this.buttonImportConstants);
            this.panelCommand.Controls.Add(this.labelProgress);
            this.panelCommand.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelCommand.Location = new System.Drawing.Point(0, 545);
            this.panelCommand.Name = "panelCommand";
            this.panelCommand.Size = new System.Drawing.Size(794, 40);
            this.panelCommand.TabIndex = 0;
            // 
            // buttonMergeCompare
            // 
            this.buttonMergeCompare.Location = new System.Drawing.Point(405, 5);
            this.buttonMergeCompare.Name = "buttonMergeCompare";
            this.buttonMergeCompare.Size = new System.Drawing.Size(180, 32);
            this.buttonMergeCompare.TabIndex = 3;
            this.buttonMergeCompare.Text = "Merge Constants, Compare Scripts";
            this.buttonMergeCompare.UseVisualStyleBackColor = true;
            this.buttonMergeCompare.Click += new System.EventHandler(this.buttonMergeCompare_Click);
            // 
            // buttonImportScript
            // 
            this.buttonImportScript.Location = new System.Drawing.Point(205, 5);
            this.buttonImportScript.Name = "buttonImportScript";
            this.buttonImportScript.Size = new System.Drawing.Size(180, 32);
            this.buttonImportScript.TabIndex = 2;
            this.buttonImportScript.Text = "Import Script";
            this.buttonImportScript.UseVisualStyleBackColor = true;
            this.buttonImportScript.Click += new System.EventHandler(this.buttonImportScript_Click);
            // 
            // buttonImportConstants
            // 
            this.buttonImportConstants.Location = new System.Drawing.Point(5, 5);
            this.buttonImportConstants.Name = "buttonImportConstants";
            this.buttonImportConstants.Size = new System.Drawing.Size(180, 32);
            this.buttonImportConstants.TabIndex = 1;
            this.buttonImportConstants.Text = "Import Constants";
            this.buttonImportConstants.UseVisualStyleBackColor = true;
            this.buttonImportConstants.Click += new System.EventHandler(this.buttonImportConstants_Click);
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(12, 18);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(0, 13);
            this.labelProgress.TabIndex = 0;
            // 
            // panelData
            // 
            this.panelData.AutoScroll = true;
            this.panelData.Controls.Add(this.dataReview);
            this.panelData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelData.Location = new System.Drawing.Point(0, 0);
            this.panelData.Name = "panelData";
            this.panelData.Size = new System.Drawing.Size(794, 545);
            this.panelData.TabIndex = 1;
            // 
            // dataReview
            // 
            this.dataReview.AllowUserToAddRows = false;
            this.dataReview.AllowUserToDeleteRows = false;
            this.dataReview.AllowUserToOrderColumns = true;
            this.dataReview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataReview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataReview.ContextMenuStrip = this.changeContextMenu;
            this.dataReview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataReview.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataReview.Location = new System.Drawing.Point(0, 0);
            this.dataReview.Name = "dataReview";
            this.dataReview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataReview.Size = new System.Drawing.Size(794, 545);
            this.dataReview.TabIndex = 0;
            // 
            // changeContextMenu
            // 
            this.changeContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importChangedScriptToolStripMenuItem,
            this.openCurrentScriptToolStripMenuItem,
            this.openVersionScriptToolStripMenuItem,
            this.openBothScriptsToolStripMenuItem,
            this.deleteScriptFromDatabaseToolStripMenuItem,
            this.openBothTestAndRealToolStripMenuItem,
            this.compareChangesInTestAndRealScriptsMenuItem,
            this.copyTestScriptToRealToolStripMenuItem,
            this.migrateAllFromTestToRealToolStripMenuItem});
            this.changeContextMenu.Name = "changeContextMenu";
            this.changeContextMenu.Size = new System.Drawing.Size(251, 202);
            // 
            // importChangedScriptToolStripMenuItem
            // 
            this.importChangedScriptToolStripMenuItem.Name = "importChangedScriptToolStripMenuItem";
            this.importChangedScriptToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.importChangedScriptToolStripMenuItem.Text = "Import changed";
            this.importChangedScriptToolStripMenuItem.Click += new System.EventHandler(this.importChangedScriptToolStripMenuItem_Click);
            // 
            // openCurrentScriptToolStripMenuItem
            // 
            this.openCurrentScriptToolStripMenuItem.Name = "openCurrentScriptToolStripMenuItem";
            this.openCurrentScriptToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.openCurrentScriptToolStripMenuItem.Text = "Open current";
            this.openCurrentScriptToolStripMenuItem.Click += new System.EventHandler(this.openCurrentScriptToolStripMenuItem_Click);
            // 
            // openVersionScriptToolStripMenuItem
            // 
            this.openVersionScriptToolStripMenuItem.Name = "openVersionScriptToolStripMenuItem";
            this.openVersionScriptToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.openVersionScriptToolStripMenuItem.Text = "Open version";
            this.openVersionScriptToolStripMenuItem.Click += new System.EventHandler(this.openVersionScriptToolStripMenuItem_Click);
            // 
            // openBothScriptsToolStripMenuItem
            // 
            this.openBothScriptsToolStripMenuItem.Name = "openBothScriptsToolStripMenuItem";
            this.openBothScriptsToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.openBothScriptsToolStripMenuItem.Text = "Open both current and version";
            this.openBothScriptsToolStripMenuItem.Click += new System.EventHandler(this.openBothScriptsToolStripMenuItem_Click);
            // 
            // deleteScriptFromDatabaseToolStripMenuItem
            // 
            this.deleteScriptFromDatabaseToolStripMenuItem.Name = "deleteScriptFromDatabaseToolStripMenuItem";
            this.deleteScriptFromDatabaseToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.deleteScriptFromDatabaseToolStripMenuItem.Text = "Delete from database";
            this.deleteScriptFromDatabaseToolStripMenuItem.Click += new System.EventHandler(this.deleteScriptFromDatabaseToolStripMenuItem_Click);
            // 
            // openBothTestAndRealToolStripMenuItem
            // 
            this.openBothTestAndRealToolStripMenuItem.Name = "openBothTestAndRealToolStripMenuItem";
            this.openBothTestAndRealToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.openBothTestAndRealToolStripMenuItem.Text = "Open both test and real";
            this.openBothTestAndRealToolStripMenuItem.Click += new System.EventHandler(this.openBothTestAndRealToolStripMenuItem_Click);
            // 
            // compareChangesInTestAndRealScriptsMenuItem
            // 
            this.compareChangesInTestAndRealScriptsMenuItem.Name = "compareChangesInTestAndRealScriptsMenuItem";
            this.compareChangesInTestAndRealScriptsMenuItem.Size = new System.Drawing.Size(250, 22);
            this.compareChangesInTestAndRealScriptsMenuItem.Text = "Compare changes in test and real";
            this.compareChangesInTestAndRealScriptsMenuItem.Click += new System.EventHandler(this.compareChangesInTestAndRealScriptsMenuItem_Click);
            // 
            // copyTestScriptToRealToolStripMenuItem
            // 
            this.copyTestScriptToRealToolStripMenuItem.Name = "copyTestScriptToRealToolStripMenuItem";
            this.copyTestScriptToRealToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.copyTestScriptToRealToolStripMenuItem.Text = "Copy test to real";
            this.copyTestScriptToRealToolStripMenuItem.Click += new System.EventHandler(this.copyTestScriptToRealToolStripMenuItem_Click);
            // 
            // migrateAllFromTestToRealToolStripMenuItem
            // 
            this.migrateAllFromTestToRealToolStripMenuItem.Name = "migrateAllFromTestToRealToolStripMenuItem";
            this.migrateAllFromTestToRealToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.migrateAllFromTestToRealToolStripMenuItem.Text = "Migrate all from test to real";
            this.migrateAllFromTestToRealToolStripMenuItem.Click += new System.EventHandler(this.migrateAllFromTestToRealToolStripMenuItem_Click);
            // 
            // checkOpenEditor
            // 
            this.checkOpenEditor.AutoSize = true;
            this.checkOpenEditor.Location = new System.Drawing.Point(605, 14);
            this.checkOpenEditor.Name = "checkOpenEditor";
            this.checkOpenEditor.Size = new System.Drawing.Size(162, 17);
            this.checkOpenEditor.TabIndex = 4;
            this.checkOpenEditor.Text = "Open editor when comparing";
            this.checkOpenEditor.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 585);
            this.Controls.Add(this.panelData);
            this.Controls.Add(this.panelCommand);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Review Changes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.panelCommand.ResumeLayout(false);
            this.panelCommand.PerformLayout();
            this.panelData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataReview)).EndInit();
            this.changeContextMenu.ResumeLayout(false);
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
    }
}

