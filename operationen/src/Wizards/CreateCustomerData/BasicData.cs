using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using Windows.Forms.Wizard;
using Utility;

namespace Operationen.Wizards.CreateCustomerData
{
    public partial class BasicData : CreateCustomerDataWizardPage
    {
        public BasicData()
        {
        }

        public BasicData(BusinessLayer businessLayer)
            : base(businessLayer, "Wizards_CreateCustomerData_BasicData")
        {
            InitializeComponent();

            lblInfo.ForeColor = BusinessLayer.InfoColor;
            lblInfo.Text = GetText("info");
        }

        protected override string PageName
        {
            get { return GetText("pagename"); } 
        }
        protected override string Header1
        {
            get
            {
                return GetText("header1");
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
                Data[CreateCustomerDataWizardPage.KEY_CD_KRANKENHAUS] = txtKrankenhaus.Text;
                Data[CreateCustomerDataWizardPage.KEY_CD_PLZ] = txtPLZ.Text;
                Data[CreateCustomerDataWizardPage.KEY_CD_STRASSE] = txtStrasse.Text;
            }

            return success;
        }

        private bool ValidateInput()
        {
            bool bSuccess = true;
            string strMessage = EingabeFehler;

            if (this.txtKrankenhaus.Text.Length == 0)
            {
                strMessage += GetTextControlMissingText(lblKrankenhaus);
                bSuccess = false;
            }
            if (this.txtKrankenhaus.Text.IndexOf("|") != -1)
            {
                strMessage += GetTextNotAllowed(lblKrankenhaus.Text, "|");
                bSuccess = false;
            }
            if (this.txtStrasse.Text.Length == 0)
            {
                strMessage += GetTextControlMissingText(lblStrasse);
                bSuccess = false;
            }
            if (this.txtStrasse.Text.IndexOf("|") != -1)
            {
                strMessage += GetTextNotAllowed(lblStrasse.Text, "|");
                bSuccess = false;
            }
            if (this.txtPLZ.Text.Length == 0)
            {
                strMessage += GetTextControlMissingText(lblPLZ);
                bSuccess = false;
            }
            if (this.txtPLZ.Text.IndexOf("|") != -1)
            {
                strMessage += GetTextNotAllowed(lblPLZ.Text, "|");
                bSuccess = false;
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
            txtKrankenhaus.Text = (string)Data[CreateCustomerDataWizardPage.KEY_CD_KRANKENHAUS];
            txtPLZ.Text = (string)Data[CreateCustomerDataWizardPage.KEY_CD_PLZ];
            txtStrasse.Text = (string)Data[CreateCustomerDataWizardPage.KEY_CD_STRASSE];
        }

        protected override void SetInitialFocus()
        {
            txtKrankenhaus.Focus();
        }
    }
}
