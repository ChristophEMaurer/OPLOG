namespace Operationen
{
    partial class ClientServerView
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientServerView));
            this.lblIpAddresses = new System.Windows.Forms.Label();
            this.lbIpAddresses = new System.Windows.Forms.ComboBox();
            this.txtPort = new Windows.Forms.OplTextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.cmdClose = new Windows.Forms.OplButton();
            this.cmdReceive = new Windows.Forms.OplButton();
            this.cmdSend = new Windows.Forms.OplButton();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.cmdAbort = new Windows.Forms.OplButton();
            this.grpComputer = new System.Windows.Forms.GroupBox();
            this.grpSendReceive = new System.Windows.Forms.GroupBox();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.lblFileName = new System.Windows.Forms.Label();
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.grpComputer.SuspendLayout();
            this.grpSendReceive.SuspendLayout();
            this.grpStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblIpAddresses
            // 
            resources.ApplyResources(this.lblIpAddresses, "lblIpAddresses");
            this.lblIpAddresses.Name = "lblIpAddresses";
            // 
            // lbIpAddresses
            // 
            this.lbIpAddresses.FormattingEnabled = true;
            resources.ApplyResources(this.lbIpAddresses, "lbIpAddresses");
            this.lbIpAddresses.Name = "lbIpAddresses";
            // 
            // txtPort
            // 
            resources.ApplyResources(this.txtPort, "txtPort");
            this.txtPort.Name = "txtPort";
            this.txtPort.ProtectContents = false;
            // 
            // lblPort
            // 
            resources.ApplyResources(this.lblPort, "lblPort");
            this.lblPort.Name = "lblPort";
            // 
            // cmdClose
            // 
            resources.ApplyResources(this.cmdClose, "cmdClose");
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.SecurityManager = null;
            this.cmdClose.UserRight = null;
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdReceive
            // 
            resources.ApplyResources(this.cmdReceive, "cmdReceive");
            this.cmdReceive.Name = "cmdReceive";
            this.cmdReceive.SecurityManager = null;
            this.cmdReceive.UserRight = null;
            this.cmdReceive.UseVisualStyleBackColor = true;
            this.cmdReceive.Click += new System.EventHandler(this.cmdReceive_Click);
            // 
            // cmdSend
            // 
            resources.ApplyResources(this.cmdSend, "cmdSend");
            this.cmdSend.Name = "cmdSend";
            this.cmdSend.SecurityManager = null;
            this.cmdSend.UserRight = null;
            this.cmdSend.UseVisualStyleBackColor = true;
            this.cmdSend.Click += new System.EventHandler(this.cmdSend_Click);
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            // 
            // lblStatus
            // 
            resources.ApplyResources(this.lblStatus, "lblStatus");
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblStatus.Name = "lblStatus";
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            // 
            // cmdAbort
            // 
            resources.ApplyResources(this.cmdAbort, "cmdAbort");
            this.cmdAbort.Name = "cmdAbort";
            this.cmdAbort.SecurityManager = null;
            this.cmdAbort.UserRight = null;
            this.cmdAbort.UseVisualStyleBackColor = true;
            this.cmdAbort.Click += new System.EventHandler(this.cmdAbort_Click);
            // 
            // grpComputer
            // 
            resources.ApplyResources(this.grpComputer, "grpComputer");
            this.grpComputer.Controls.Add(this.lblIpAddresses);
            this.grpComputer.Controls.Add(this.lbIpAddresses);
            this.grpComputer.Controls.Add(this.txtPort);
            this.grpComputer.Controls.Add(this.lblPort);
            this.grpComputer.Name = "grpComputer";
            this.grpComputer.TabStop = false;
            // 
            // grpSendReceive
            // 
            resources.ApplyResources(this.grpSendReceive, "grpSendReceive");
            this.grpSendReceive.Controls.Add(this.txtFileName);
            this.grpSendReceive.Controls.Add(this.lblFileName);
            this.grpSendReceive.Controls.Add(this.cmdReceive);
            this.grpSendReceive.Controls.Add(this.cmdSend);
            this.grpSendReceive.Name = "grpSendReceive";
            this.grpSendReceive.TabStop = false;
            // 
            // txtFileName
            // 
            resources.ApplyResources(this.txtFileName, "txtFileName");
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            // 
            // lblFileName
            // 
            resources.ApplyResources(this.lblFileName, "lblFileName");
            this.lblFileName.Name = "lblFileName";
            // 
            // grpStatus
            // 
            resources.ApplyResources(this.grpStatus, "grpStatus");
            this.grpStatus.Controls.Add(this.progressBar);
            this.grpStatus.Controls.Add(this.lblStatus);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.TabStop = false;
            // 
            // ClientServerView
            // 
            this.CancelButton = this.cmdClose;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.grpStatus);
            this.Controls.Add(this.grpSendReceive);
            this.Controls.Add(this.grpComputer);
            this.Controls.Add(this.cmdAbort);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.cmdClose);
            this.Name = "ClientServerView";
            this.Load += new System.EventHandler(this.ClientServerView_Load);
            this.grpComputer.ResumeLayout(false);
            this.grpComputer.PerformLayout();
            this.grpSendReceive.ResumeLayout(false);
            this.grpSendReceive.PerformLayout();
            this.grpStatus.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblIpAddresses;
        private System.Windows.Forms.ComboBox lbIpAddresses;
        private Windows.Forms.OplTextBox txtPort;
        private System.Windows.Forms.Label lblPort;
        private Windows.Forms.OplButton cmdClose;
        private Windows.Forms.OplButton cmdReceive;
        private Windows.Forms.OplButton cmdSend;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar progressBar;
        private Windows.Forms.OplButton cmdAbort;
        private System.Windows.Forms.GroupBox grpComputer;
        private System.Windows.Forms.GroupBox grpSendReceive;
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label lblFileName;
    }
}
