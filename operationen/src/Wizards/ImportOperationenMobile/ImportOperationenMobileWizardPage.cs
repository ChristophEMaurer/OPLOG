using System;
using System.Reflection;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ImportOperationenMobile
{
    public partial class ImportOperationenMobileWizardPage : OperationenWizardPage
    {
        public const string FileName = "FileName";
        public const string ID_Chirurgen = "ID_Chirurgen";
        public const string SelectedIndex = "SelectedIndex";

        public ImportOperationenMobileWizardPage()
        {
        }

        public ImportOperationenMobileWizardPage(BusinessLayer b)
            : base(b)
        {
            InitializeComponent();
        }
    }
}
