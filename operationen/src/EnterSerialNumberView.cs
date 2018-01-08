using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Globalization;

using Windows.Forms;

using Operationen.Wizards.CreateCustomerData;

namespace Operationen
{
	/// <summary>
    /// Zusammenfassung für EnterSerialNumberView.
	/// </summary>
    public class EnterSerialNumberView : OperationenForm
	{
        private System.Windows.Forms.Label lblVorname;
        private Windows.Forms.OplTextBox txtVorname;
        private Windows.Forms.OplButton cmdOK;
        private Windows.Forms.OplButton cmdCancel;

        private GroupBox grpUser;
        private OplTextBox txtNachname;
        private Label lblNachname;
        private OplProtectedTextBox txtSerialnumber;
        private Label lblSerial;

        private DataRow _chirurg;
        private string _serialNumber = "";
        private OplCheckBox chkSerialAutomatic;
        private LinkLabel llData;
        private Label lblInfo;

        /// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public EnterSerialNumberView(BusinessLayer businessLayer, DataRow chirurg) :
            base(businessLayer)
		{
            _chirurg = chirurg;

			InitializeComponent();

            txtNachname.Enabled = false;
            txtNachname.ReadOnly = true;

            txtVorname.Enabled = false;
            txtVorname.ReadOnly = true;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnterSerialNumberView));
            this.lblVorname = new System.Windows.Forms.Label();
            this.txtVorname = new Windows.Forms.OplTextBox();
            this.cmdOK = new Windows.Forms.OplButton();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.grpUser = new System.Windows.Forms.GroupBox();
            this.chkSerialAutomatic = new Windows.Forms.OplCheckBox();
            this.txtSerialnumber = new Windows.Forms.OplProtectedTextBox();
            this.lblSerial = new System.Windows.Forms.Label();
            this.txtNachname = new Windows.Forms.OplTextBox();
            this.lblNachname = new System.Windows.Forms.Label();
            this.llData = new System.Windows.Forms.LinkLabel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.grpUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblVorname
            // 
            resources.ApplyResources(this.lblVorname, "lblVorname");
            this.lblVorname.Name = "lblVorname";
            // 
            // txtVorname
            // 
            resources.ApplyResources(this.txtVorname, "txtVorname");
            this.txtVorname.Name = "txtVorname";
            this.txtVorname.ProtectContents = false;
            this.txtVorname.ReadOnly = true;
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
            // grpUser
            // 
            resources.ApplyResources(this.grpUser, "grpUser");
            this.grpUser.Controls.Add(this.chkSerialAutomatic);
            this.grpUser.Controls.Add(this.txtSerialnumber);
            this.grpUser.Controls.Add(this.lblSerial);
            this.grpUser.Controls.Add(this.txtNachname);
            this.grpUser.Controls.Add(this.lblNachname);
            this.grpUser.Controls.Add(this.lblVorname);
            this.grpUser.Controls.Add(this.txtVorname);
            this.grpUser.Name = "grpUser";
            this.grpUser.TabStop = false;
            // 
            // chkSerialAutomatic
            // 
            resources.ApplyResources(this.chkSerialAutomatic, "chkSerialAutomatic");
            this.chkSerialAutomatic.Name = "chkSerialAutomatic";
            this.chkSerialAutomatic.UseVisualStyleBackColor = true;
            this.chkSerialAutomatic.CheckedChanged += new System.EventHandler(this.chkSerialAutomatic_CheckedChanged);
            // 
            // txtSerialnumber
            // 
            resources.ApplyResources(this.txtSerialnumber, "txtSerialnumber");
            this.txtSerialnumber.Name = "txtSerialnumber";
            this.txtSerialnumber.ProtectContents = true;
            // 
            // lblSerial
            // 
            resources.ApplyResources(this.lblSerial, "lblSerial");
            this.lblSerial.Name = "lblSerial";
            // 
            // txtNachname
            // 
            resources.ApplyResources(this.txtNachname, "txtNachname");
            this.txtNachname.Name = "txtNachname";
            this.txtNachname.ProtectContents = false;
            this.txtNachname.ReadOnly = true;
            // 
            // lblNachname
            // 
            resources.ApplyResources(this.lblNachname, "lblNachname");
            this.lblNachname.Name = "lblNachname";
            // 
            // llData
            // 
            resources.ApplyResources(this.llData, "llData");
            this.llData.Name = "llData";
            this.llData.TabStop = true;
            this.llData.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llData_LinkClicked);
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            // 
            // EnterSerialNumberView
            // 
            this.AcceptButton = this.cmdOK;
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.llData);
            this.Controls.Add(this.grpUser);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Name = "EnterSerialNumberView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.EnterSerialNumberView_Load);
            this.Shown += new System.EventHandler(this.EnterSerialNumberView_Shown);
            this.grpUser.ResumeLayout(false);
            this.grpUser.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		#endregion

        public string SerialNumber
        {
            get { return _serialNumber; }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override bool ValidateInput()
        {
            bool success = true;

            OperationenSerial.SerialLogic sl = new OperationenSerial.SerialLogic();

            string serial = txtSerialnumber.Text;
            if (!sl.ValidateSerialNumber(serial))
            {
                success = false;
                MessageBox(GetText("invalidSerial"));
            }

            // Hier kommt auch eine MessageBox heraus
            success = success && BusinessLayer.CheckSerial(_chirurg, serial);

            return success;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            OKClicked();
        }

        protected override void SaveObject()
        {
            _serialNumber = txtSerialnumber.Text;
        }

        private void EnterSerialNumberView_Shown(object sender, EventArgs e)
        {
            txtSerialnumber.Focus();
        }

        private void EnterSerialNumberView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("enterSerial"));

            string msg = string.Format(CultureInfo.InvariantCulture, GetText("serialNotice")
                , Command_AboutBox, cmdOK.Text);

            SetInfoText(lblInfo, msg);

            Object2Control();
        }
        protected override void Object2Control()
        {
            this.txtNachname.Text = (string)_chirurg["Nachname"];
            this.txtVorname.Text = (string)_chirurg["Vorname"];

            chkSerialAutomatic.Checked = false;
            int countUnused = BusinessLayer.GetCountUnusedSerialNumbers();
            if (countUnused > 0)
            {
                chkSerialAutomatic.Enabled = true;
            }
            else
            {
                chkSerialAutomatic.Enabled = false;
                chkSerialAutomatic.Text += GetText("noneLeft");
            }
        }

        private void CheckSerialAutomaticChanged()
        {
            if (chkSerialAutomatic.Checked)
            {
                string serial = BusinessLayer.GetFirstUnusedSerialNumber();
                if (!string.IsNullOrEmpty(serial))
                {
                    txtSerialnumber.Text = serial;
                    txtSerialnumber.Enabled = false;
                }
            }
            else
            {
                txtSerialnumber.Clear();
                txtSerialnumber.Enabled = true;
            }
        }

        private void chkSerialAutomatic_CheckedChanged(object sender, EventArgs e)
        {
            CheckSerialAutomaticChanged();
        }

        private void DisplayTransferredData()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict.Add(GetText("serialNumber"), txtSerialnumber.Text);
            dict.Add(GetText("version"), BusinessLayer.BareVersionString);
            dict.Add(GetText("computername"), System.Environment.MachineName);
            dict.Add(GetText("clinic"), BusinessLayer.GetConfigValue(CreateCustomerDataWizardPage.KEY_CD_KRANKENHAUS));
            dict.Add(GetText("zip"), BusinessLayer.GetConfigValue(CreateCustomerDataWizardPage.KEY_CD_PLZ));
            dict.Add(GetText("street"), BusinessLayer.GetConfigValue(CreateCustomerDataWizardPage.KEY_CD_STRASSE));
            dict.Add(GetText("lastName"), (string)_chirurg["Nachname"]);
            dict.Add(GetText("firstName"), (string)_chirurg["Vorname"]);

            new TextBoxView(BusinessLayer, GetText("data"), dict).ShowDialog();
        }

        private void llData_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                DisplayTransferredData();
            }
            catch (Exception ex)
            {
                MessageBox(ex.Message);
            }
        }
    }
}
