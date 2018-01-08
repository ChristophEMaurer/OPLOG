using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Setup
{
    public partial class UpdateLocations : SetupWizardPage
    {
        public UpdateLocations()
        {
            InitializeComponent();
        }

        protected override string PageName
        {
            get
            {
                return "Programmverzeichnis";
            }
        }

        protected override string Header2
        {
            get
            {
                return "Wählen Sie das Programmverzeichnis";
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

                data[ProgramFolder] = txtProgramDirectory.Text;
            }

            return success;
        }

        private bool ValidateInput()
        {
            bool success = true;

            string programFileName = txtProgramDirectory.Text + "\\" + SetupData.ProgramExeFileName;

            if (!File.Exists(programFileName))
            {
                MessageBox.Show("Das Programm"
                    + "\r\r'" + programFileName + "'"
                    + "\r\rexistiert nicht. Es kann daher auch nicht aktualisiert werden."
                    + "\rSie müssen das Verzeichnis auswählen, in dem das Programm installiert wurde.",
                    ProgramName);
                success = false;
            }

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

            txtProgramDirectory.Text = (string)data[ProgramFolder];

            lblInfo.Text = "Wählen Sie Verzeichnis, in dem das Programm installiert ist.";
        }

        protected override void SetInitialFocus()
        {
            lblInfo.Focus();
        }

        private void cmdDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            dlg.RootFolder = Environment.SpecialFolder.MyComputer;
            //dlg.ShowNewFolderButton = false;
            dlg.SelectedPath = txtProgramDirectory.Text;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                txtProgramDirectory.Text = dlg.SelectedPath;
            }
        }

    }
}
