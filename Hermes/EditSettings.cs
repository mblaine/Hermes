using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Hermes
{
    public partial class EditSettings : Form
    {
        public bool Cancel { get; private set; }

        public EditSettings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
            txtUrl.Text = Settings.Store["url"];
            txtUsername.Text = Settings.Store["username"];
            txtPassword.Text = Settings.TempStore["password"];
            chkRememberPassword.Checked = bool.Parse(Settings.Store["remember-password"]);
            chkShowBalloon.Checked = bool.Parse(Settings.Store["show-balloon"]);
            chkAutoLogIn.Checked = bool.Parse(Settings.Store["auto-log-in"]);
            txtInterval.Text = Settings.Store["interval"];
        }

        private void EditSettings_Activated(object sender, EventArgs e)
        {
            txtPassword.Focus();
            txtPassword.SelectAll();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            uint interval;
            if (!uint.TryParse(txtInterval.Text, out interval))
            {
                MessageBox.Show("How often to check for mail must be a postive integer, or zero to disable automatic checking.");
                return;
            }

            Settings.Store["url"] = txtUrl.Text;
            Settings.Store["username"] = txtUsername.Text;
            Settings.Store["remember-password"] = chkRememberPassword.Checked.ToString().ToLower();
            if (chkRememberPassword.Checked)
            {
                Settings.TempStore["password"] = txtPassword.Text;
                Settings.Store["password"] = Convert.ToBase64String(ProtectedData.Protect(UTF8Encoding.UTF8.GetBytes(txtPassword.Text), null, DataProtectionScope.CurrentUser));
            }
            else
            {
                Settings.TempStore["password"] = txtPassword.Text;
                Settings.Store["password"] = "";
            }
            
            Settings.Store["show-balloon"] = chkShowBalloon.Checked.ToString().ToLower();
            Settings.Store["auto-log-in"] = chkAutoLogIn.Checked.ToString().ToLower();
            Settings.Store["interval"] = interval.ToString();

            Settings.Save();
            Cancel = false;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel = true;
            Close();
        }
    }
}
