namespace Hermes
{
    partial class MailViewer
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
            this.lstUnread = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSubject = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSent = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCC = new System.Windows.Forms.Label();
            this.wbBody = new System.Windows.Forms.WebBrowser();
            this.btnMarkRead = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstUnread
            // 
            this.lstUnread.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstUnread.CheckOnClick = true;
            this.lstUnread.FormattingEnabled = true;
            this.lstUnread.Location = new System.Drawing.Point(12, 42);
            this.lstUnread.Name = "lstUnread";
            this.lstUnread.Size = new System.Drawing.Size(143, 394);
            this.lstUnread.TabIndex = 1;
            this.lstUnread.SelectedIndexChanged += new System.EventHandler(this.lstUnread_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(161, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Subject:";
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Location = new System.Drawing.Point(213, 12);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(10, 13);
            this.lblSubject.TabIndex = 3;
            this.lblSubject.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Sent:";
            // 
            // lblSent
            // 
            this.lblSent.AutoSize = true;
            this.lblSent.Location = new System.Drawing.Point(213, 32);
            this.lblSent.Name = "lblSent";
            this.lblSent.Size = new System.Drawing.Size(10, 13);
            this.lblSent.TabIndex = 5;
            this.lblSent.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(161, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "From:";
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(213, 52);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(10, 13);
            this.lblFrom.TabIndex = 7;
            this.lblFrom.Text = "-";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(161, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "To:";
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(213, 72);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(10, 13);
            this.lblTo.TabIndex = 9;
            this.lblTo.Text = "-";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(161, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "CC:";
            // 
            // lblCC
            // 
            this.lblCC.AutoSize = true;
            this.lblCC.Location = new System.Drawing.Point(213, 92);
            this.lblCC.Name = "lblCC";
            this.lblCC.Size = new System.Drawing.Size(10, 13);
            this.lblCC.TabIndex = 11;
            this.lblCC.Text = "-";
            // 
            // wbBody
            // 
            this.wbBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wbBody.Location = new System.Drawing.Point(161, 112);
            this.wbBody.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbBody.Name = "wbBody";
            this.wbBody.Size = new System.Drawing.Size(514, 324);
            this.wbBody.TabIndex = 12;
            // 
            // btnMarkRead
            // 
            this.btnMarkRead.Location = new System.Drawing.Point(12, 12);
            this.btnMarkRead.Name = "btnMarkRead";
            this.btnMarkRead.Size = new System.Drawing.Size(143, 23);
            this.btnMarkRead.TabIndex = 13;
            this.btnMarkRead.Text = "Mark checked as read";
            this.btnMarkRead.UseVisualStyleBackColor = true;
            this.btnMarkRead.Click += new System.EventHandler(this.btnMarkRead_Click);
            // 
            // MailViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 452);
            this.Controls.Add(this.btnMarkRead);
            this.Controls.Add(this.wbBody);
            this.Controls.Add(this.lblCC);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblSent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblSubject);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstUnread);
            this.Icon = global::Hermes.Properties.Resources.icon;
            this.Name = "MailViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hermes - Unread Mail";
            this.Load += new System.EventHandler(this.MailViewer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox lstUnread;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCC;
        private System.Windows.Forms.WebBrowser wbBody;
        private System.Windows.Forms.Button btnMarkRead;
    }
}