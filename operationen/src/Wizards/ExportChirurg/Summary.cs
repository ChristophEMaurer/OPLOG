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

namespace Operationen.Wizards.ExportChirurg
{
    public partial class Summary : ExportChirurgWizardPage
    {
        private const string FormName = "Wizards_ExportChirurg_Summary";

        public Summary(BusinessLayer b)
            : base(b)
        {
            InitializeComponent();

            progressBar.Visible = false;
            Tools.SetProgressBarStyleMarqueeOnSupportedPlatforms(progressBar);
        }
        protected override string PageName
        {
            get { return GetText(FormName, "pagename"); }
        }

        private void Summary_Load(object sender, EventArgs e)
        {
            Hashtable data = Data;

            DataRow row = _businessLayer.GetChirurg((int)Data[ExportChirurgWizardPage.ID_Chirurgen]);

            lblProgress.Text = "";

            lblSummary.Text = string.Format(CultureInfo.InvariantCulture, GetText(FormName, "text"),
                (string)row["Vorname"], (string)row["Nachname"], 
                (string)Data[ExportChirurgWizardPage.FileName], Wizard.FinishText);
        }

        protected override bool OnFinish()
        {
            bool success = ExportChirurg(progressBar,
                (int)Data[ExportChirurgWizardPage.ID_Chirurgen],
                (string)Data[ExportChirurgWizardPage.FileName]
                );

            // Egal ob geklappt oder nicht, anschlieﬂend kann man nur noch 
            // 'Schlieﬂen' klicken
            return true;
        }

        private bool ExportChirurg(
            ProgressBar progressBar,
            int         nID_Chirurgen,
            string      fileName)
        {
            Wizard.FormMayClose(false);
            Wizard.DisableAll();

            ChirurgExporterSQL exporter = new ChirurgExporterSQL(_businessLayer, progressBar, lblProgress);

            exporter.Initialize(nID_Chirurgen, fileName);

            bool success = exporter.Export();

            SetSuccess(success);

            Wizard.FormMayClose(true);

            return success;
        }
    }
}
