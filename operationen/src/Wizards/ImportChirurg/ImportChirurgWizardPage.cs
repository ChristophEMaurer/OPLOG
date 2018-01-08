using System;
using System.Reflection;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ImportChirurg
{
    public partial class ImportChirurgWizardPage : OperationenWizardPage
    {
        public const string FileName = "FileName";

        public ImportChirurgWizardPage()
        {
        }

        public ImportChirurgWizardPage(BusinessLayer b, string formNameForResourceTexts)
            : base(b, formNameForResourceTexts)
        {
            InitializeComponent();
        }
    }
}
