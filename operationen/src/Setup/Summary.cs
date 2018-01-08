using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Setup
{
    public partial class Summary : SetupWizardPage
    {
        public Summary()
        {
            InitializeComponent();
        }

        protected override string PageName
        {
            get { return "Zusammenfassung"; }
        }

        protected override bool OnFinish()
        {
            if (Wizard.UpdateMode)
            {
                bool success = InstallUpdate(
                    progressBar,
                    (string)Data[SetupWizardPage.ProgramFolder]);
            }
            else
            {
                bool success = InstallProgram(
                    progressBar,
                    (int)Data[SetupWizardPage.InstallationMode],
                    (string)Data[SetupWizardPage.ProgramFolder],
                    (string)Data[SetupWizardPage.DatabaseFolder]);
            }

            // Egal ob geklappt oder nicht, anschlieﬂend kann man nur noch 
            // 'Schlieﬂen' klicken
            return true;
        }

        private bool InstallUpdate(ProgressBar progressBar, string programFolder)
        {
            Installer installer = new Installer(progressBar, programFolder);

            bool success = installer.Install();

            Data[SetupWizardPage.Success] = success;

            return success;
        }

        private bool InstallProgram(ProgressBar progressBar, 
            int installationMode, 
            string programFolder, 
            string databaseFolder)
        {
            Installer installer = new Installer(progressBar, false, installationMode, programFolder, databaseFolder);
            
            bool success = installer.Install();

            Data[SetupWizardPage.Success] = success;

            return success;
        }
        protected override void OnActivate()
        {
            progressBar.Visible = false;

            StringBuilder sb = new StringBuilder();

            if (Wizard.UpdateMode)
            {
                sb.Append("Zusammenfassung:"
                    + Environment.NewLine
                    + Environment.NewLine);

                sb.Append("Programmverzeichnis:");
                sb.Append(Environment.NewLine);
                sb.Append("    " + (string)Data[SetupWizardPage.ProgramFolder]);
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append("Klicken Sie auf '" + Wizard.FinishText + "', um den Vorgang abzuschlieﬂen.");
            }
            else
            {
                sb.Append("Zusammenfassung:"
                    + Environment.NewLine
                    + Environment.NewLine
                    + "Installationsart:"
                    + Environment.NewLine);

                int installationType = (int)Data[SetupWizardPage.InstallationMode];
                if (installationType == SetupWizardPage.ModeSingleUser)
                {
                    sb.Append("    Ein Benutzer: Ich benutze das Programm alleine und greife als einziger auf die Daten zu.");
                    sb.Append(Environment.NewLine);
                    sb.Append(Environment.NewLine);
                    sb.Append("Installationsverzeichnis:");
                    sb.Append(Environment.NewLine);
                    sb.Append("    " + (string)Data[SetupWizardPage.ProgramFolder]);
                }
                else
                {
                    sb.Append("    Mehrere Benutzer: Ich benutze das Programm mit anderen zusammen und wir greifen alle auf dieselbe Daten zu.");
                    sb.Append(Environment.NewLine);
                    sb.Append(Environment.NewLine);
                    sb.Append("Programmverzeichnis:");
                    sb.Append(Environment.NewLine);
                    sb.Append("    " + (string)Data[SetupWizardPage.ProgramFolder]);
                    sb.Append(Environment.NewLine);
                    sb.Append(Environment.NewLine);
                    sb.Append("Verzeichnis f¸r gemeinsame Daten:");
                    sb.Append(Environment.NewLine);
                    sb.Append("    " + (string)Data[SetupWizardPage.DatabaseFolder]);
                    sb.Append(Environment.NewLine);
                    sb.Append(Environment.NewLine);
                }
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append("Klicken Sie auf '" + Wizard.FinishText + "', um den Vorgang abzuschlieﬂen.");

            }
            txtSummary.Text = sb.ToString();
        }
    }
}
