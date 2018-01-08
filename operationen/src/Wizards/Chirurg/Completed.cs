using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.Chirurg
{
    public partial class Completed : ChirurgWizardPage
    {
        public Completed(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();
        }

        protected override string PageName
        {
            get
            {
                return "Fertigstellung";
            }
        }

        protected override void OnActivate()
        {
            if (GetSuccess())
            {
                DataRow row = (DataRow)Data[ChirurgWizardPage.Chirurg];

                lblInfo.Text = "Der Arzt wurde erfolgreich angelegt."
                    + Environment.NewLine
                    + Environment.NewLine
                    + "Melden Sie sich jetzt mit den folgenden Benutzerdaten an:"
                    + Environment.NewLine
                    + Environment.NewLine
                    + "Anmeldename: " + (string)row["UserID"]
                    + Environment.NewLine
                    + "Password:    start"
                    ;
            }
            else
            {
                lblInfo.Text = "Der Arzt konnte nicht angelegt werden.";
            }
        }
    }
}

