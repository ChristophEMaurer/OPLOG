using System;
using System.Windows.Forms;
using System.Globalization;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.InstallLicense
{
    public partial class Welcome : InstallLicenseWizardPage
    {
        public Welcome(BusinessLayer b)
            : base(b, "Wizards_InstallLicense_Welcome")
        {
            InitializeComponent();
        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            string msg = string.Format(CultureInfo.InvariantCulture, GetText("info"), GetNextText());

            lblWelcome.Text = msg;
        }

        protected override string PageName
        {
            get { return GetText("pageName"); }
        }
    }
}

