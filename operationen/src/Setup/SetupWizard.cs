using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Microsoft.Win32;

using Windows.Forms.Wizard;

namespace Operationen.Setup
{
    public partial class SetupWizard : WizardMaster
    {
        protected internal Hashtable _htData = new Hashtable();

        public SetupWizard(string[] args)
            : base(args)
        {
            InitializeComponent();

            if (UpdateMode)
            {
                _htData[SetupWizardPage.AcceptLicense] = false;
                _htData[SetupWizardPage.ProgramFolder] = GetPreviousInstallationFolder();

                AddPage(new Welcome());
                AddPage(new License());
                AddPage(new UpdateLocations());
                AddPage(new Summary());
                AddPage(new Completed());
            }
            else
            {
                _htData[SetupWizardPage.AcceptLicense] = false;
                _htData[SetupWizardPage.InstallationMode] = SetupWizardPage.ModeSingleUser;
                _htData[SetupWizardPage.ProgramFolder] = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
                    + System.IO.Path.DirectorySeparatorChar + SetupData.ProgramName;
                _htData[SetupWizardPage.DatabaseFolder] = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
                    + System.IO.Path.DirectorySeparatorChar + SetupData.ProgramName;

                AddPage(new Welcome());
                AddPage(new License());
                AddPage(new ClientOrServer());
                AddPage(new Locations());
                AddPage(new Summary());
                AddPage(new Completed());
            }
        }

        protected override string GetText(string formName, string id)
        {
            throw new Exception("Setup ist noch nicht lokalisiert, GetText() nicht benutzen");
        }

        /// <summary>
        /// Find a previous installation and return that path, so that we can point at the right path
        /// in an update.
        /// </summary>
        /// <returns></returns>
        protected string GetPreviousInstallationFolder()
        {
            string key = "";

            // Default program installation path for update when no previous version is found.
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + System.IO.Path.DirectorySeparatorChar + SetupData.SetupFileName;

            try
            {
                RegistryKey hklm = Registry.LocalMachine;

                key = "SOFTWARE\\" + SetupData.REG_KEY_LOGBUCH;

                // if the new folder exists, use that one
                RegistryKey logbuch = hklm.OpenSubKey(key);
                if (logbuch != null)
                {
                    string s = (string)logbuch.GetValue(SetupData.REG_ENTRY_PROGRAM_FOLDER);
                    // program folder very basic check. Muss irgendeinen Wert haben. c:\tmp reicht schon!
                    if (s.Length > 5)
                    {
                        folder = s;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return folder;
        }

        protected string ProgramName
        {
            get { return SetupData.ProgramName; }
        }

        public override string Title
        {
            get
            {
                if (isUpdate)
                {
                    return ProgramName + " Update Assistent";
                }
                else
                {
                    return ProgramName + " Setup Assistent";
                }
            }
        }

        protected override void OnPreClose()
        {
            if ((bool)_htData[SetupWizardPage.Success])
            {
                try
                {
                    string filename = (string)_htData[SetupWizardPage.ProgramFolder] + "\\" + SetupData.ProgramExeFileName;

                    System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();

                    info.WorkingDirectory = (string)_htData[SetupWizardPage.ProgramFolder];
                    info.FileName = filename;

                    System.Diagnostics.Process.Start(info);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public override string FinishText
        {
            get { return "Fertigstellen"; }
        }

        public override string CloseText
        {
            get { return "Schlieﬂen"; }
        }
    }
}
