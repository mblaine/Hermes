using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Microsoft.Exchange.WebServices.Data;

namespace Hermes
{
    public class Hermes : IDisposable
    {
        private NotifyIcon icon;
        private ContextMenu menu;
        private Timer timer;
        private Form dialog;
        private List<EmailMessage> messages;
        private int lastUnreadCount = 0;
        private MenuItem menucheckNow;
        private MenuItem menuviewMail;
        private MenuItem menusettings;
        private MenuItem menulogInOut;

        private ExchangeService service;

        public Hermes()
        {
            ExceptionLogger.BeforeExceptionLog += beforeExceptionLog;
            ExceptionLogger.AfterExceptionLog += afterExceptionLog;

            icon = new NotifyIcon();
            icon.Text = "Hermes - Not logged in";
            icon.Icon = Properties.Resources.unknown;
            
            menu = new ContextMenu();
            menucheckNow = menu.MenuItems.Add("&Check Now", menuCheckNow_Click);
            menuviewMail = menu.MenuItems.Add("&View Mail", menuViewMail_Click);
            menusettings = menu.MenuItems.Add("&Settings", menuSettings_Click);
            menu.MenuItems.Add("-");
            menulogInOut = menu.MenuItems.Add("&Log In", menuLogInOut_Click);
            menu.MenuItems.Add("E&xit", menuExit_Click);

            if (!Settings.Store.ContainsKey("url"))
                Settings.Store["url"] = "https://.../ews/Exchange.asmx";
            if (!Settings.Store.ContainsKey("username"))
                Settings.Store["username"] = "";
            if (!Settings.Store.ContainsKey("password"))
                Settings.Store["password"] = "";
            if (!Settings.Store.ContainsKey("remember-password"))
                Settings.Store["remember-password"] = "false";
            if (!Settings.Store.ContainsKey("show-balloon"))
                Settings.Store["show-balloon"] = "true";
            if (!Settings.Store.ContainsKey("auto-log-in"))
                Settings.Store["auto-log-in"] = "true";
            if (!Settings.Store.ContainsKey("interval"))
                Settings.Store["interval"] = "300";

            if (bool.Parse(Settings.Store["remember-password"]) && !String.IsNullOrEmpty(Settings.Store["password"]))
                Settings.TempStore["password"] = UTF8Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(Settings.Store["password"]), null, DataProtectionScope.CurrentUser));
            else
                Settings.TempStore["password"] = "";

            icon.ContextMenu = menu;
            icon.Visible = true;

            timer = new Timer();
            timer.Tick += timer_Tick;

            EnableMenus();

            if(bool.Parse(Settings.Store["auto-log-in"]))
                LogIn();

        }

        public void Dispose()
        {
            if (dialog != null)
            {
                dialog.Dispose();
                dialog = null;
            }
            if (menu != null)
            {
                menu.Dispose();
                menu = null;
            }
            if (icon != null)
            {
                icon.Dispose();
                icon = null;
            }
            if (timer != null)
            {
                StopChecking();
                timer.Dispose();
                timer = null;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            UpdateNotification();
        }

        private void LogIn()
        {
            if (String.IsNullOrEmpty(Settings.TempStore["password"]))
            {
                menuSettings_Click(null, null);
                return;
            }

            service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);
            service.Url = new Uri(Settings.Store["url"]);
            service.Credentials = new WebCredentials(Settings.Store["username"], Settings.TempStore["password"]);

            menulogInOut.Text = "&Log Out";
            messages = new List<EmailMessage>();
            EnableMenus();
            UpdateNotification();
            StartChecking();
        }

        private void LogOut()
        {
            StopChecking();
            service = null;
            messages = null;
            EnableMenus();
            icon.Icon = Properties.Resources.unknown;
            icon.Text = "Hermes - Not logged in";
            menulogInOut.Text = "&Log In";
        }

        private void StartChecking()
        {
            StopChecking();
            int interval = int.Parse(Settings.Store["interval"]);
            if (interval > 0)
            {
                timer.Interval = interval * 1000;
                timer.Start();
            }
        }

        private void StopChecking()
        {
            if (timer.Enabled)
                timer.Stop();
        }

        private void EnableMenus(bool enable = true)
        {
            menucheckNow.Enabled = enable ? service != null : false;
            menuviewMail.Enabled = enable ? service != null : false;
            menusettings.Enabled = enable;
            menulogInOut.Enabled = enable;
        }

        private void UpdateNotification()
        {
            icon.Text += " - Working...";
            int newUnreadCount = 0;
            try
            {
                newUnreadCount = GetUnreadCount();
            }
            catch (ServiceRequestException ex)
            {
                icon.ShowBalloonTip(3000, "Error", "Error retrieving mail: " + ex.Message, ToolTipIcon.Error);
                LogOut();
                icon.Icon = Properties.Resources.error;
                return;
            }
            
            String status = String.Format("You have {0} unread message{1}. ", newUnreadCount, newUnreadCount == 1 ? "" : "s");

            if (newUnreadCount > lastUnreadCount && bool.Parse(Settings.Store["show-balloon"]))
            {
                icon.ShowBalloonTip(3000, "Unread mail", status, ToolTipIcon.Info);
            }
            lastUnreadCount = newUnreadCount;

            if (lastUnreadCount == 0)
            {
                icon.Text = "You have no new mail. ";
                icon.Icon = Properties.Resources.none;
            }
            else
            {
                icon.Text = status;
                icon.Icon = Properties.Resources.unread;
            }
            icon.Text += DateTime.Now.ToString("(h:mm tt)");
        }

        private int GetUnreadCount()
        {
            if (service == null)
                return 0;

            FindItemsResults<Item> results = service.FindItems(WellKnownFolderName.Inbox, new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, false), new ItemView(30));
            messages.Clear();
            if (results.Items.Count > 0)
            {
                service.LoadPropertiesForItems(results, PropertySet.FirstClassProperties);
                foreach (EmailMessage m in results)
                    messages.Add(m);
            }

            return results.TotalCount;
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            StopChecking();
            LogOut();
            Application.Exit();
        }

        private void menuCheckNow_Click(object sender, EventArgs e)
        {
            StopChecking();
            UpdateNotification();
            StartChecking();
        }

        private void menuViewMail_Click(object sender, EventArgs e)
        {
            StopChecking();
            EnableMenus(false);
            dialog = new MailViewer(messages);
            dialog.FormClosed += viewer_FormClosed;
            dialog.Show();
        }

        private void menuSettings_Click(object sender, EventArgs e)
        {
            StopChecking();
            EnableMenus(false);
            dialog = new EditSettings();
            dialog.FormClosed += settings_FormClosed;
            dialog.Show();
        }

        private void menuLogInOut_Click(object sender, EventArgs e)
        {
            if (service == null)
                LogIn();
            else
                LogOut();
        }

        private void viewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            dialog.Dispose();
            dialog = null;
            EnableMenus();
            UpdateNotification();
            StartChecking();
        }


        private void settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            bool cancel = ((EditSettings)dialog).Cancel;
            dialog.Dispose();
            dialog = null;
            EnableMenus();
            if (!cancel && bool.Parse(Settings.Store["auto-log-in"]))
            {
                if (service != null)
                    LogOut();
                LogIn();
            }
            else
            {
                StartChecking();
            }
        }

        private void beforeExceptionLog(object sender, EventArgs e)
        {
            LogOut();
            if(icon != null)
                icon.Icon = Properties.Resources.error;
        }

        private void afterExceptionLog(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
