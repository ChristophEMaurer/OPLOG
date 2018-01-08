using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Utility;
using Windows.Forms;

namespace Operationen
{
	/// <summary>
	/// Zusammenfassung für UserChangePasswordView.
	/// </summary>
    public class UserSetPasswordView : OperationenForm
	{
        private System.Windows.Forms.Label lblNewPassword;
        private Windows.Forms.OplProtectedTextBox txtNewPassword;
        private System.Windows.Forms.Label lblNewPassword2;
        private Windows.Forms.OplProtectedTextBox txtNewPassword2;
        private Windows.Forms.OplButton cmdOK;
        private Windows.Forms.OplButton cmdCancel;

        private OplListView lvChirurgen;

        private GroupBox grpUsers;
        private GroupBox grpLogon;

        int _nID_Chirurgen;
        string _strNewPassword;


		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public UserSetPasswordView(BusinessLayer businessLayer)
            : base(businessLayer)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserSetPasswordView));
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.txtNewPassword = new Windows.Forms.OplProtectedTextBox();
            this.lblNewPassword2 = new System.Windows.Forms.Label();
            this.txtNewPassword2 = new Windows.Forms.OplProtectedTextBox();
            this.cmdOK = new Windows.Forms.OplButton();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.lvChirurgen = new Windows.Forms.OplListView();
            this.grpUsers = new System.Windows.Forms.GroupBox();
            this.grpLogon = new System.Windows.Forms.GroupBox();
            this.grpUsers.SuspendLayout();
            this.grpLogon.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNewPassword
            // 
            resources.ApplyResources(this.lblNewPassword, "lblNewPassword");
            this.lblNewPassword.Name = "lblNewPassword";
            // 
            // txtNewPassword
            // 
            resources.ApplyResources(this.txtNewPassword, "txtNewPassword");
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.ProtectContents = true;
            // 
            // lblNewPassword2
            // 
            resources.ApplyResources(this.lblNewPassword2, "lblNewPassword2");
            this.lblNewPassword2.Name = "lblNewPassword2";
            // 
            // txtNewPassword2
            // 
            resources.ApplyResources(this.txtNewPassword2, "txtNewPassword2");
            this.txtNewPassword2.Name = "txtNewPassword2";
            this.txtNewPassword2.ProtectContents = true;
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.SecurityManager = null;
            this.cmdOK.UserRight = null;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
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
            // lvChirurgen
            // 
            resources.ApplyResources(this.lvChirurgen, "lvChirurgen");
            this.lvChirurgen.DoubleClickActivation = false;
            this.lvChirurgen.Name = "lvChirurgen";
            this.lvChirurgen.UseCompatibleStateImageBehavior = false;
            // 
            // grpUsers
            // 
            resources.ApplyResources(this.grpUsers, "grpUsers");
            this.grpUsers.Controls.Add(this.lvChirurgen);
            this.grpUsers.Name = "grpUsers";
            this.grpUsers.TabStop = false;
            // 
            // grpLogon
            // 
            resources.ApplyResources(this.grpLogon, "grpLogon");
            this.grpLogon.Controls.Add(this.lblNewPassword);
            this.grpLogon.Controls.Add(this.txtNewPassword);
            this.grpLogon.Controls.Add(this.lblNewPassword2);
            this.grpLogon.Controls.Add(this.txtNewPassword2);
            this.grpLogon.Name = "grpLogon";
            this.grpLogon.TabStop = false;
            // 
            // UserSetPasswordView
            // 
            this.AcceptButton = this.cmdOK;
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.grpLogon);
            this.Controls.Add(this.grpUsers);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Name = "UserSetPasswordView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.UserSetPasswordView_Load);
            this.grpUsers.ResumeLayout(false);
            this.grpLogon.ResumeLayout(false);
            this.grpLogon.PerformLayout();
            this.ResumeLayout(false);

        }

        void cmdCancel_Click(object sender, EventArgs e)
        {
            base.CancelClicked();
        }

        void cmdOK_Click(object sender, EventArgs e)
        {
            int nID_Chirurgen = this.GetFirstSelectedTag(lvChirurgen, true, GetText(grpUsers.Text));

            if (nID_Chirurgen != -1)
            {
                base.OKClicked();
            }
        }
		#endregion

        private void UserSetPasswordView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));

            PopulateChirurgen(lvChirurgen, true);
        }

        protected override bool ValidateInput()
        {
            bool success = true;
            string strMessage = EINGABEFEHLER;

            if (this.txtNewPassword.Text.Length == 0)
            {
                success = false;
                strMessage += GetTextControlMissingText(lblNewPassword);
            }
            if (this.txtNewPassword.Text != txtNewPassword2.Text)
            {
                success = false;
                strMessage += "\r- " + GetText("error1");
            }

            if (success)
            {
                success = Confirm(GetText("confirm1"));
            }
            else
            {
                MessageBox(strMessage);
            }

            return success;
        }

        protected override void Control2Object() 
        {
            _nID_Chirurgen = this.GetFirstSelectedTag(lvChirurgen, true);
            _strNewPassword = txtNewPassword2.Text;
        }

        protected override void Object2Control() {}

        protected override void SaveObject() 
        {
            BusinessLayer.UpdatePassword(_nID_Chirurgen, _strNewPassword);
        }
    }
}

