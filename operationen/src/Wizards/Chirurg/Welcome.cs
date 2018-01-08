using System;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.Chirurg
{
    public partial class Welcome : ChirurgWizardPage
    {
        public Welcome(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();
        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = "Willkommen beim 'Arzt anlegen' Assistenten."
                + Environment.NewLine
                + Environment.NewLine
                + "Dieser Assistent hilft Ihnen, den ersten Arzt im System anzulegen, "
                + "damit Sie sich anschließend anmelden können."
                + Environment.NewLine
                + Environment.NewLine 
                + "Klicken Sie auf 'Weiter' um mit dem Vorgang zu beginnen."
                ;
        }

        protected override string PageName
        {
            get
            {
                return "Willkommen";
            }
        }
    }
}

