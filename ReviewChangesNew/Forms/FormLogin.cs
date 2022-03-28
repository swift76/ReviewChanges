using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ReviewChangesNew
{
    public partial class FormLogin : Form
    {
        Settings appSettings;
        Dictionary<string, string> connectionStrings;

        public FormLogin()
        {
            InitializeComponent();
        }

        public FormLogin(Settings appSettings, Dictionary<string, string> connectionStrings)
        {
            InitializeComponent();
            this.appSettings = appSettings;
            this.connectionStrings = connectionStrings;
        }

        private void control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (e.Control)
                    buttonOK_Click(buttonOK, new EventArgs());
                else
                    SendKeys.Send("{TAB}");
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            BaseDataAccess dataAccess = null;
            string connectionString = checkRealTest.Checked ? connectionStrings["real"] : connectionStrings["test"];
            try
            {
                dataAccess = new BaseDataAccess(textLogin.Text, textPassword.Text, connectionString);
                FormMain mainWin = new FormMain(appSettings, dataAccess);
                mainWin.loginForm = this;
                mainWin.dataAccess = dataAccess;
                mainWin.Show();
                Hide();
            }
            catch (Exception ex)
            {
                if (dataAccess != null)
                    dataAccess.Dispose();
                MessageBox.Show(ex.Message);
                textLogin.Focus();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
