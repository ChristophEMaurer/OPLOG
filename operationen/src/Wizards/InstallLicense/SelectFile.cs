using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.InstallLicense
{
    public partial class SelectFile : InstallLicenseWizardPage
    {
        public SelectFile(BusinessLayer b)
            : base(b, "Wizards_InstallLicense_SelectFile")
        {
            InitializeComponent();
        }
        protected override string PageName
        {
            get { return "Lizenz auswählen"; } 
        }
        protected override string Header1
        {
            get
            {
                return "Wählen Sie die Lizenz aus";
            }
        }
        protected override string Header2
        {
            get
            {
                return "Diese Lizenz wird installiert";
            }
        }

        private void cmdFileName_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "*.xml|*.xml";
            //dlg.FileName = "*license.xml";
            if (DialogResult.OK == dlg.ShowDialog())
            {
                txtFileName.Text = dlg.FileName;
            }
        }
        private bool LeavePage(bool validateInput)
        {
            bool success = true;

            if (validateInput)
            {
                success = ValidateInput();
            }

            if (success)
            {
                Hashtable data = Data;

                data[FileName] = txtFileName.Text;
            }

            return success;
        }

        private bool ValidateInput()
        {
            bool success = true;

            string fileName = txtFileName.Text;

            if (fileName.Length == 0)
            {
                _businessLayer.MessageBox("Sie müssen eine Datei auswählen!"); // TOGO
                success = false;
                goto _exit;
            }

        _exit:
            return success;
        }

        protected override bool OnPreNext()
        {
            return LeavePage(true);
        }
        protected override bool OnPreBack()
        {
            return LeavePage(false);
        }

        protected override void OnActivate()
        {
            Hashtable data = Data;

            txtFileName.Text = (string)data[FileName];
        }
    }
}
