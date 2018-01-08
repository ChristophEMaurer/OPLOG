using System;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Setup
{
    public partial class ClientOrServer : SetupWizardPage
    {
        public ClientOrServer()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            lblInfo.Text = "Wählen Sie nun, ob Sie das Programm alleine"
                + " oder mit anderen zusammen benutzen:";
        }

        protected override string PageName
        {
            get { return "Installationsart"; }
        }
        protected override string Header2
        {
            get
            {
                return "Wählen Sie die Installationsart";
            }
        }

        protected override void OnDeactivate()
        {
            Hashtable data = Data;

            if (radStandalone.Checked)
            {
                data[InstallationMode] = ModeSingleUser;
            }
            else if (radMultiMany.Checked)
            {
                data[InstallationMode] = ModeMultiMany;
            }
            else if (radMultiOneProgram.Checked)
            {
                data[InstallationMode] = ModeMultiOneProgram;
            }
            else 
            {
                data[InstallationMode] = ModeMultiOneShortcut;
            }

            if (radStandalone.Checked)
            {
                data[DatabaseFolder] = data[ProgramFolder];
            }
        }

        protected override void OnActivate()
        {
            Hashtable data = Data;

            int installationType = (int)data[InstallationMode];

            radStandalone.Checked = radMultiMany.Checked = radMultiOneProgram.Checked = radMultiOneShortcut.Checked = false;

            if (installationType == ModeSingleUser)
            {
                radStandalone.Checked = true;
            }
            else if (installationType == ModeMultiMany)
            {
                radMultiMany.Checked = true;
            }
            else if (installationType == ModeMultiOneProgram)
            {
                radMultiOneProgram.Checked = true;
            }
            else
            {
                radMultiOneShortcut.Checked = true;
            }
        }
    }
}
