using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;
using Utility;

namespace Operationen.Wizards.Chirurg
{
    public partial class BasicData : ChirurgWizardPage
    {
        public BasicData()
        {
        }

        public BasicData(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();

            lblInfo.ForeColor = BusinessLayer.InfoColor;

            cbAnrede.Items.Add("Herr");
            cbAnrede.Items.Add("Frau");
            cbAnrede.SelectedIndex = 0;

            PopulateTitel();

            lblInfo.Text = "Das Kennwort eines neu angelegten Benutzers ist 'start'."
                    + " Nach dem ersten Anmelden muss das Kennwort geändert werden.";
        }
        protected override string PageName
        {
            get { return "Benutzer Daten"; } 
        }
        protected override string Header1
        {
            get
            {
                return "Geben Sie die Daten des Arztes ein";
            }
        }

        private bool LeavePage(bool validateInput)
        {
            bool success = true;

            if (validateInput)
            {
                success = ValidateInput();
            }

            if (success)
            {
                DataRow row = (DataRow)Data[ChirurgWizardPage.Chirurg];

                row["Anrede"] = this.cbAnrede.Text;
                row["Nachname"] = this.txtNachname.Text;
                row["Vorname"] = this.txtVorname.Text;
                row["UserID"] = this.txtUserID.Text;
                row["ID_ChirurgenFunktionen"] = cbTitel.SelectedValue;
            }

            return success;
        }

        private bool ValidateInput()
        {
            bool bSuccess = true;
            string strMessage = "Hinweis:";

            if (this.cbAnrede.Text.Length == 0)
            {
                strMessage = strMessage + "\n- '" + lblAnrede.Text + "' muss ausgefüllt werden";
                bSuccess = false;
            }
            if (cbTitel.SelectedValue == null)
            {
                strMessage += "\n- '" + lblTitel.Text + "' muss ausgewählt werden";
                bSuccess = false;
            }
            if (this.txtNachname.Text.Length == 0)
            {
                strMessage = strMessage + "\n- '" + lblNachname.Text + "' muss ausgefüllt werden";
                bSuccess = false;
            }
            if (this.txtVorname.Text.Length == 0)
            {
                strMessage = strMessage + "\n- '" + lblVorname.Text + "' muss ausgefüllt werden";
                bSuccess = false;
            }
            if (this.txtUserID.Text.Length == 0)
            {
                strMessage = strMessage + "\n- '" + lblAnmeldename.Text + "' muss ausgefüllt werden";
                bSuccess = false;
            }
            else
            {
                if (_businessLayer.GetCountUserID(txtUserID.Text) > 0)
                {
                    strMessage += "\n- der Anmeldename '" + txtUserID.Text + "' ist bereits vergeben.";
                    bSuccess = false;
                }
            }

            if (!bSuccess)
            {
                _businessLayer.MessageBox(strMessage);
            }

            return bSuccess;
        }

        protected override bool OnPreNext()
        {
            return LeavePage(true);
        }
        protected override bool OnPreBack()
        {
            return LeavePage(false);
        }

        protected override void OnActivate()
        {
            DataRow row = (DataRow)Data[ChirurgWizardPage.Chirurg];

            cbAnrede.Text = (string)row["Anrede"];
            txtNachname.Text = (string)row["Nachname"];
            txtVorname.Text = (string)row["Vorname"];
            txtUserID.Text = (string)row["UserID"];
            cbTitel.SelectedValue = ConvertToInt32(row["ID_ChirurgenFunktionen"]);
        }

        private void PopulateTitel()
        {
            DataView dv = _businessLayer.GetChirurgenFunktionen();

            cbTitel.ValueMember = "ID_ChirurgenFunktionen";
            cbTitel.DisplayMember = "Funktion";
            cbTitel.DataSource = dv;
        }

        private void txtNachname_TextChanged(object sender, EventArgs e)
        {
            // wenn noch keine userid eingetragen wurde,
            // dann generieren wir eine aus dem Nachnamen.
            string strOld = txtNachname.Text;
            string strNew = Tools.LastName2LogOnName(strOld);

            txtUserID.Text = strNew;
        }

        protected override void SetInitialFocus()
        {
            cbAnrede.Focus();
        }

        private void BasicData_Load(object sender, EventArgs e)
        {

        }
    }
}
