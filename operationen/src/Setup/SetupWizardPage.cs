using System;
using System.Reflection;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Setup
{
    public partial class SetupWizardPage : WizardPage
    {
        public static Image _image;

        /// <summary>
        /// program and data in one folder
        /// </summary>
        public const int ModeSingleUser = 1;

        /// <summary>
        /// Multiuser. One data folder, and program installed on each PC
        /// </summary>
        public const int ModeMultiMany = 2;

        /// <summary>
        /// Server installation: one program folder, one data folder
        /// </summary>
        public const int ModeMultiOneProgram = 3;

        /// <summary>
        /// Server installation: create only the shortcuts
        /// </summary>
        public const int ModeMultiOneShortcut = 4;

        public const string InstallationMode = "InstallationMode";
        public const string ProgramFolder = "ProgramFolder";
        public const string DatabaseFolder = "DatabaseFolder";
        public const string AcceptLicense = "AcceptLicense";

        public const string Success = "Success";

        public SetupWizardPage()
        {
            InitializeComponent();
        }

        protected void DefaultListViewProperties(ListView lv)
        {
            lv.View = View.Details;
            lv.FullRowSelect = true;
            lv.ShowItemToolTips = true;
            lv.GridLines = false;
            lv.HideSelection = false;
        }
        protected Hashtable Data
        {
            get
            {
                return ((SetupWizard)Wizard)._htData;
            }
        }

        protected string ProgramName
        {
            get { return SetupData.ProgramName; }
        }

        protected override Image Image
        {
            get
            {
                if (_image == null)
                {
                    try
                    {
                        // Diese Datei ist im Verzeichnis Setup/Images/plogbuch.jpg
                        // Wenn man bei ihren Eigenshaften nachsieht, muss man 
                        // "embedded resource" einstellen, dann landet sie in der .exe und
                        // man kann sie so wie hier auslesen.
                        Assembly assembly = Assembly.GetExecutingAssembly();
                        Stream stream = assembly.GetManifestResourceStream(
                            "Setup.Images.Surgeon.png");

                        _image = new Bitmap(stream);
                    }
                    catch
                    {
                    }
                }
                return _image;
            }
        }
    }
}
