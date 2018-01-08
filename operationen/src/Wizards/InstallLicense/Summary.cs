using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.InstallLicense
{
    public partial class Summary : InstallLicenseWizardPage
    {
        public Summary(BusinessLayer b)
            : base(b, "Wizards_InstallLicense_Summary")
        {
            InitializeComponent();
        }
        protected override string PageName
        {
            get
            {
                return GetText("pageName");
            }
        }

        private void Summary_Load(object sender, EventArgs e)
        {
            string msg = string.Format(CultureInfo.InvariantCulture, GetText("summary"), (string)Data[InstallLicenseWizardPage.FileName], Wizard.FinishText);

            lblInfo.Text = msg;
        }

        protected override bool OnFinish()
        {
            InstallLicense(progressBar,
                (string)Data[InstallLicenseWizardPage.FileName]);

            // Egal ob geklappt oder nicht, anschlieﬂend kann man nur noch 
            // 'Schlieﬂen' klicken
            return true;
        }

        private bool InstallLicense(
            ProgressBar progressBar, 
            string fileName)
        {
            LicenseInstaller installer = new LicenseInstaller(_businessLayer, progressBar);
            installer.Initialize(fileName);

            bool success = installer.Install();

            SetSuccess(success);

            return success;
        }
    }
}
