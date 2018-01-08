using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Security.Principal;

using Windows.Forms.Wizard;

namespace Operationen.Setup
{
    public partial class Locations : SetupWizardPage
    {
        public Locations()
        {
            InitializeComponent();

            lblInfo.Text = "Wenn Sie einen UNC-Pfad angegeben ('\\\\Computername\\Verzeichnis')"
             + " müssen Sie jetzt Zugriff auf das Verzeichnis haben.";
        }

        protected override string PageName
        {
            get
            {
                return "Installationsverzeichnis";
            }
        }

        protected override string Header2
        {
            get
            {
                return "Wählen Sie die Installationsverzeichnisse";
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
                data[DatabaseFolder] = txtDatabaseDirectory.Text;
            }

            return success;
        }

        private bool TestWriteAccessToFolder(string folder)
        {
            bool success = false;

            try
            {
                Directory.CreateDirectory(folder);

                success = true;
            }
            catch
            {
                MessageBox.Show(string.Format("Sie haben keine ausreichenden Rechte, um in das Verzeichnis \r\r'{0}'\r\rzu schreiben."
                    + "\rSie müssen Administrator sein, um die Installation durchzuführen. Wenden Sie sich mit dieser Meldung an Ihren Systemadministrator.", folder), ProgramName);
            }

            return success;
        }

        private bool ValidateInput()
        {
            bool success = false;

            Hashtable data = Data;

            int installationType = (int) data[InstallationMode];
            string programFolder = txtProgramDirectory.Text;
            string databaseFolder = txtDatabaseDirectory.Text;

            if (installationType != ModeSingleUser)
            {
                if (programFolder == databaseFolder)
                {
                    MessageBox.Show("Sie haben die Installationsart"
                        + Environment.NewLine
                        + Environment.NewLine
                        + "'Mehrere Benutzer verwenden dieselbe Daten'"
                        + Environment.NewLine
                        + Environment.NewLine
                        + "ausgewählt. Das Programmverzeichnis und das Datenverzeichnis "
                        + "dürfen nicht identisch sein."
                        , ProgramName);
                    goto _exit;
                }
            }

            try
            {
                WindowsIdentity wi = WindowsIdentity.GetCurrent();
                WindowsPrincipal wp = new WindowsPrincipal(wi);

                // This checks for local administrator rights if you in a Domain
                if (!wp.IsInRole(WindowsBuiltInRole.Administrator))
                {
                    MessageBox.Show("Sie müssen Administrator sein, um die Installation durchzuführen."
                     + "\rBrechen sie die Installation ab und wenden Sie sich mit dieser Meldung an Ihren Systemadministrator.", ProgramName);
                }

            }
            catch
            {
            }

            
            //
            // Test whether we have write permission in the program and data folder
            //
            if (!TestWriteAccessToFolder(txtProgramDirectory.Text + Path.DirectorySeparatorChar + SetupData.ProgramFolderSubdirectories[0]))
            {
                goto _exit;
            }
            if (!TestWriteAccessToFolder(txtDatabaseDirectory.Text + Path.DirectorySeparatorChar + SetupData.DatabaseFolderSubdirectories[0]))
            {
                goto _exit;
            }

            success = true;

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

            txtProgramDirectory.Text = (string)data[ProgramFolder];

            int installationType = (int)data[InstallationMode];

            if (installationType == ModeSingleUser)
            {
                cmdDatabaseDirectory.Enabled = false;
                txtDatabaseDirectory.ReadOnly = true;
                txtDatabaseDirectory.Text = txtProgramDirectory.Text;
            }
            else
            {
                cmdDatabaseDirectory.Enabled = true;
                txtDatabaseDirectory.ReadOnly = false;
                txtDatabaseDirectory.Text = (string)data[DatabaseFolder];
            }
        }

        protected override void SetInitialFocus()
        {
            lblTitle.Focus();
        }

        private void ProgramDirectoryChanged()
        {
            Hashtable data = Data;

            int installationType = (int)data[InstallationMode];
            if (installationType == ModeSingleUser)
            {
                txtDatabaseDirectory.Text = txtProgramDirectory.Text;
            }
        }

        private void cmdDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            dlg.RootFolder = Environment.SpecialFolder.MyComputer;
            dlg.SelectedPath = txtProgramDirectory.Text;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                txtProgramDirectory.Text = dlg.SelectedPath;
                ProgramDirectoryChanged();
            }
        }

        private void cmdDatabaseDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            dlg.RootFolder = Environment.SpecialFolder.MyComputer;
            dlg.SelectedPath = txtDatabaseDirectory.Text;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                txtDatabaseDirectory.Text = dlg.SelectedPath;
            }
        }

        private void txtProgramDirectory_TextChanged(object sender, EventArgs e)
        {
            ProgramDirectoryChanged();
        }

        private void Locations_Load(object sender, EventArgs e)
        {

        }
    }
}
