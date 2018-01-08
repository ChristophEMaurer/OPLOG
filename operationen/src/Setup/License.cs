using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Setup
{
    public partial class License : SetupWizardPage
    {
        public License()
        {
            InitializeComponent();
        }

        protected override string PageName
        {
            get { return "Lizenzvereinbarung"; }
        }

        protected override void OnActivate()
        {
            string filename = Application.StartupPath + "\\License.txt";


            lblInfo.Text = "Sie müssen die Lizenzvereinbarung komplett durchlesen und akzeptieren, "
             + " ansonsten kann die Installation nicht fortgesetzt werden.";

            chkAcceptLicense.Text = "Ich habe die Lizenzvereinbarung komplett gelesen und akzeptiere die Bedingungen des Lizenzvertrages.";
            chkAcceptLicense.Checked = false;

            if (!System.IO.File.Exists(filename))
            {
                MessageBox.Show(string.Format("Die Lizenzdatei\n'{0}'\nfehlt.", filename), ProgramName);
                chkAcceptLicense.Enabled = false;
                goto Exit;
            }

            StreamReader reader = null;
            string line = null;

            try
            {
                reader = new StreamReader(filename, Encoding.Unicode);
                line = reader.ReadToEnd();
                txtText.Text = line;
            }
            catch
            {
                MessageBox.Show(string.Format("Die Lizenzdatei\n'{0}'\nkonnte nicht gelesen werden.", filename), ProgramName);
                chkAcceptLicense.Enabled = false;
                goto Exit;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            Exit:;
        }

        protected override bool OnPreNext()
        {
            bool success = false;

            if (chkAcceptLicense.Checked)
            {
                Data[AcceptLicense] = true;
                success = true;
            }
            else
            {
                MessageBox.Show("Sie müssen die Lizenzvereinbarung akzeptieren, ansonsten kann die Installation"
                + Environment.NewLine + "nicht fortgesetzt werden.");
            }

            return success;
        }
    }
}

