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

namespace Operationen.Wizards.ImportChirurg
{
    public partial class Summary : ImportChirurgWizardPage
    {
        public Summary(BusinessLayer b)
            : base(b, "Wizards_ImportChirurg_Summary")
        {
            InitializeComponent();

            progressBar.Visible = false;
            Tools.SetProgressBarStyleMarqueeOnSupportedPlatforms(progressBar);
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
            Hashtable data = Data;

            lblProgress.Text = "";

            string text = string.Format(CultureInfo.InvariantCulture, GetText("summary"),
                (string)Data[ImportChirurgWizardPage.FileName], Wizard.FinishText);

            lblSummary.Text = text;
        }

        protected override bool OnFinish()
        {
            /* bool success = */ ImportChirurg(progressBar,
                (string)Data[ImportChirurgWizardPage.FileName]);

            // Egal ob geklappt oder nicht, anschlieﬂend kann man nur noch 
            // 'Schlieﬂen' klicken
            return true;
        }

        private bool ImportChirurg(
            ProgressBar progressBar, 
            string fileName)
        {
            Wizard.FormMayClose(false);
            Wizard.DisableAll();

            ChirurgImporterSQL importer = new ChirurgImporterSQL(_businessLayer, progressBar, lblProgress);

            importer.Initialize(fileName);

            bool success = false;

            try
            {
                success = importer.Import();
            }
            catch (Exception e)
            {
                _businessLayer.MessageBox(e.Message);
            }

            SetSuccess(success);

            Wizard.FormMayClose(true);

            return success;
        }
    }
}
