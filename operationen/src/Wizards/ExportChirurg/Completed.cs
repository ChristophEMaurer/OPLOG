using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ExportChirurg
{
    public partial class Completed : ExportChirurgWizardPage
    {
        private const string FormName = "Wizards_ExportChirurg_Completed";

        public Completed(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();
        }

        protected override string PageName
        {
            get
            {
                return GetText(FormName, "pagename");
            }
        }

        protected override void OnActivate()
        {
            string fileName = (string)Data[ExportChirurgWizardPage.FileName];
            DataRow row = _businessLayer.GetChirurg(ConvertToInt32(Data[ExportChirurgWizardPage.ID_Chirurgen]));

            if (GetSuccess())
            {
                lblInfo.Text = string.Format(CultureInfo.InvariantCulture, GetText(FormName, "info"),
                    (string)row["Vorname"], (string)row["Nachname"], fileName);
            }
            else
            {
                lblInfo.Text = string.Format(CultureInfo.InvariantCulture, GetText(FormName, "error"),
                    (string)row["Vorname"], (string)row["Nachname"], fileName);
            }
        }
    }
}

