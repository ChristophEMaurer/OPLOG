using System;
using System.Reflection;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ImportOPS
{
    public partial class ImportOPSWizardPage : OperationenWizardPage
    {
        public const string FileName = "FileName";
        public const string Format = "Format";

        public const string Format2009 = "2009";
        public const string Format2010 = "2010";
        public const string Format2013Xml = "2013Xml";

        public ImportOPSWizardPage()
        {
        }

        public ImportOPSWizardPage(BusinessLayer b, string formNameForResourceTexts)
            : base(b, formNameForResourceTexts)
        {
            InitializeComponent();
        }
    }
}
