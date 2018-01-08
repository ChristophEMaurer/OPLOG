using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using Windows.Forms.Wizard;
using Utility;

namespace Operationen.Wizards.ImportOperationenMobile
{
    public partial class Summary : ImportOperationenMobileWizardPage
    {
        private const string FormName = "Wizards_ImportOperationenMobile_Summary";

        public Summary(BusinessLayer b)
            : base(b)
        {
            InitializeComponent();

            progressBar.Visible = false;
            Tools.SetProgressBarStyleMarqueeOnSupportedPlatforms(progressBar);
        }

        protected override string PageName
        {
            get
            {
                return GetText(FormName, "pagename");
            }
        }

        private void Summary_Load(object sender, EventArgs e)
        {
            Hashtable data = Data;

            lblProgress.Text = "";

            DataRow row = _businessLayer.GetChirurg((int)Data[ImportOperationenMobileWizardPage.ID_Chirurgen]);

            lblSummary.Text = string.Format(CultureInfo.InvariantCulture, GetText(FormName, "text"),
                (string)row["Vorname"], (string)row["Nachname"],
                (string)Data[ImportOperationenMobileWizardPage.FileName], Wizard.FinishText);
            ;
        }

        protected override bool OnFinish()
        {
            bool success = ImportOperationen(progressBar,
                (string)Data[ImportOperationenMobileWizardPage.FileName],
                (int)Data[ImportOperationenMobileWizardPage.ID_Chirurgen]
                );

            // Egal ob geklappt oder nicht, anschlieﬂend kann man nur noch 
            // 'Schlieﬂen' klicken
            return true;
        }


        private bool ImportOperationen(
            ProgressBar progressBar, 
            string fileName,
            int ID_Chirurgen
            )
        {
            Wizard.FormMayClose(false);
            Wizard.DisableAll();

            OperationenImporterMobileSQL importer = new OperationenImporterMobileSQL(_businessLayer, progressBar, lblProgress);

            importer.Initialize(fileName, ID_Chirurgen);

            bool success = importer.Import();

            SetSuccess(success);

            Wizard.FormMayClose(true);

            return success;
        }
    }
}
