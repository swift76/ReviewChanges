using System;
using System.Windows.Forms;

namespace ReviewChanges
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
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
            try
            {
                dataAccess = new BaseDataAccess(textLogin.Text, textPassword.Text, checkRealTest.Checked);
                FormMain mainWin = new FormMain();
                mainWin.LoginForm = this;
                mainWin.DataAccess = dataAccess;
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
