using System;
using System.Reflection;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.InstallLicense
{
    public partial class InstallLicenseWizardPage : OperationenWizardPage
    {
        public const string FileName = "FileName";

        public InstallLicenseWizardPage()
        {
        }

        public InstallLicenseWizardPage(BusinessLayer b, string formName)
            : base(b, formName)
        {
            InitializeComponent();
        }
    }
}
