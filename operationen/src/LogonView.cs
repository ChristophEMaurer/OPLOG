using System;
using System.Windows.Forms;
using System.Globalization;

using Windows.Forms;

namespace Operationen
{
    public class LogonView : OperationenForm
	{
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Windows.Forms.OplTextBox txtUser;
        private Windows.Forms.OplProtectedTextBox txtPassword;
        private Windows.Forms.OplButton cmdLogon;
        private Windows.Forms.OplButton cmdCancel;

        private GroupBox groupBox1;
        private Label lblInfo;

		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public LogonView(BusinessLayer businessLayer) :
            base(businessLayer)
		{
			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			InitializeComponent();
		}

		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogonView));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUser = new Windows.Forms.OplTextBox();
            this.txtPassword = new Windows.Forms.OplProtectedTextBox();
            this.cmdLogon = new Windows.Forms.OplButton();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtUser
            // 
            resources.ApplyResources(this.txtUser, "txtUser");
            this.txtUser.Name = "txtUser";
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            // 
            // cmdLogon
            // 
            resources.ApplyResources(this.cmdLogon, "cmdLogon");
            this.cmdLogon.Name = "cmdLogon";
            this.cmdLogon.SecurityManager = null;
            this.cmdLogon.UserRight = null;
            this.cmdLogon.Click += new System.EventHandler(this.cmdLogon_Click);
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.SecurityManager = null;
            this.cmdCancel.UserRight = null;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.txtUser);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            // 
            // LogonView
            // 
            this.AcceptButton = this.cmdLogon;
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdLogon);
            this.MaximizeBox = false;
            this.Name = "LogonView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.LogonView_Load);
            this.Shown += new System.EventHandler(this.LogonView_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        void cmdCancel_Click(object sender, EventArgs e)
        {
            base.CancelClicked();
        }
		#endregion

        private void cmdLogon_Click(object sender, System.EventArgs e)
        {
            if (BusinessLayer.Login(this.txtUser.Text, this.txtPassword.Text))
            {
                if (BusinessLayer.CurrentUserMustChangePassword)
                {
                    UserChangePasswordView dlg = new UserChangePasswordView(BusinessLayer);
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                MessageBox(GetText("error"));
                txtPassword.Text = "";
            }
        }

        private void LogonView_Shown(object sender, EventArgs e)
        {
            txtUser.Focus();

#if DEBUG
            txtUser.Text = "mustermann";
            txtPassword.Text = "start";
            cmdLogon_Click(null, null);
#endif
        }

        private void LogonView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));
            SetInfoText(lblInfo, GetText("info"));
        }

        protected void CancelClicked(object sender, EventArgs e)
        {
            base.CancelClicked();
        }
    }
}
