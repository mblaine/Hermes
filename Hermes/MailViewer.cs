using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Exchange.WebServices.Data;

namespace Hermes
{
    public partial class MailViewer : Form
    {
        private List<EmailMessage> messages;

        public MailViewer(List<EmailMessage> messages)
        {
            InitializeComponent();
            this.messages = messages;
        }

        private void MailViewer_Load(object sender, EventArgs e)
        {
            if (messages == null)
            {
                MessageBox.Show("Error: Messages is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            for (int i = 0; i < messages.Count; i++)
                lstUnread.Items.Add(messages[i].Subject ?? "<no subject>");
        }

        private void lstUnread_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstUnread.SelectedIndex < 0 || lstUnread.SelectedIndex >= messages.Count)
            {
                //MessageBox.Show(String.Format("Error: Index out of range. Index is: {0}. Array count is: {0}.", lstUnread.SelectedIndex, messages.Count), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            EmailMessage m = messages[lstUnread.SelectedIndex];
            lblSubject.Text = m.Subject;
            lblSent.Text = m.DateTimeSent.ToString("M/d/yyy h:mm:ss tt");
            lblFrom.Text = m.From.Address;
            lblTo.Text = m.DisplayTo;
            lblCC.Text = m.DisplayCc;
            wbBody.DocumentText = m.Body;
        }

        private void btnMarkRead_Click(object sender, EventArgs e)
        {
            foreach (int i in lstUnread.CheckedIndices)
            {
                messages[i].IsRead = true;
                messages[i].Update(ConflictResolutionMode.AutoResolve);
            }
            this.Close();
        }
    }
}
