using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Windows.Forms;

namespace Operationen
{
	/// <summary>
	/// Zusammenfassung für UserChangePasswordView.
	/// </summary>
    public class UserChangePasswordView : OperationenForm
	{
        private System.Windows.Forms.Label label1;
        private Windows.Forms.OplProtectedTextBox txtOldPassword;
        private System.Windows.Forms.Label label2;
        private Windows.Forms.OplProtectedTextBox txtNewPassword1;
        private System.Windows.Forms.Label label3;
        private Windows.Forms.OplProtectedTextBox txtNewPassword2;
        private Windows.Forms.OplButton cmdOK;
        private Windows.Forms.OplButton cmdCancel;
        
        private GroupBox grpData;
        private TextBox txtUserID;
        private Label label4;
        
        /// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public UserChangePasswordView(BusinessLayer businessLayer) :
            base(businessLayer)
		{
			InitializeComponent();
		}

		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserChangePasswordView));
            this.label1 = new System.Windows.Forms.Label();
            this.txtOldPassword = new Windows.Forms.OplProtectedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNewPassword1 = new Windows.Forms.OplProtectedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNewPassword2 = new Windows.Forms.OplProtectedTextBox();
            this.cmdOK = new Windows.Forms.OplButton();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.grpData = new System.Windows.Forms.GroupBox();
            this.txtUserID = new Windows.Forms.OplTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.grpData.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtOldPassword
            // 
            resources.ApplyResources(this.txtOldPassword, "txtOldPassword");
            this.txtOldPassword.Name = "txtOldPassword";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtNewPassword1
            // 
            this.txtNewPassword1.AcceptsReturn = true;
            resources.ApplyResources(this.txtNewPassword1, "txtNewPassword1");
            this.txtNewPassword1.Name = "txtNewPassword1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtNewPassword2
            // 
            resources.ApplyResources(this.txtNewPassword2, "txtNewPassword2");
            this.txtNewPassword2.Name = "txtNewPassword2";
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // grpData
            // 
            resources.ApplyResources(this.grpData, "grpData");
            this.grpData.Controls.Add(this.txtUserID);
            this.grpData.Controls.Add(this.label4);
            this.grpData.Controls.Add(this.label1);
            this.grpData.Controls.Add(this.txtOldPassword);
            this.grpData.Controls.Add(this.label2);
            this.grpData.Controls.Add(this.txtNewPassword2);
            this.grpData.Controls.Add(this.txtNewPassword1);
            this.grpData.Controls.Add(this.label3);
            this.grpData.Name = "grpData";
            this.grpData.TabStop = false;
            // 
            // txtUserID
            // 
            resources.ApplyResources(this.txtUserID, "txtUserID");
            this.txtUserID.Name = "txtUserID";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // UserChangePasswordView
            // 
            this.AcceptButton = this.cmdOK;
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.grpData);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Name = "UserChangePasswordView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.UserChangePasswordView_Load);
            this.Shown += new System.EventHandler(this.UserChangePasswordView_Shown);
            this.grpData.ResumeLayout(false);
            this.grpData.PerformLayout();
            this.ResumeLayout(false);

        }
		#endregion

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override bool ValidateInput()
        {
            bool success = false;

            if (txtNewPassword1.Text.Length == 0)
            {
                MessageBox(GetText("error1"));
                goto Exit;
            }
            if (txtNewPassword1.Text != txtNewPassword2.Text)
            {
                MessageBox(GetText("error2"));
                txtNewPassword1.Text = "";
                txtNewPassword2.Text = "";
                txtNewPassword1.Focus();
                goto Exit;
            }
            if (txtNewPassword1.Text == txtOldPassword.Text)
            {
                MessageBox(GetText("error3"));
                txtNewPassword1.Text = "";
                txtNewPassword2.Text = "";
                txtNewPassword1.Focus();
                goto Exit;
            }

            success = true;

            Exit: ;

            return success;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                if (BusinessLayer.CheckUserAndPassword(BusinessLayer.CurrentUser_UserID, txtOldPassword.Text))
                {
                    if (BusinessLayer.UpdatePassword(BusinessLayer.CurrentUser_UserID, txtNewPassword1.Text))
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }
                else
                {
                    MessageBox(GetText("error4"));
                    txtOldPassword.Text = "";
                    txtNewPassword1.Text = "";
                    txtNewPassword2.Text = "";
                    txtOldPassword.Focus();
                }
            }
        }

        private void UserChangePasswordView_Shown(object sender, EventArgs e)
        {
            txtOldPassword.Focus();
        }

        private void UserChangePasswordView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));
            Object2Control();
        }
        protected override void Object2Control()
        {
            this.txtUserID.Text = BusinessLayer.CurrentUser_UserID;
        }
    }
}
