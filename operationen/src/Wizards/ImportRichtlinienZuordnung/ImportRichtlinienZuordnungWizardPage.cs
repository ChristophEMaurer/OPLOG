using System;
using System.Reflection;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ImportRichtlinienZuordnung
{
    public partial class ImportRichtlinienZuordnungWizardPage : OperationenWizardPage
    {
        public const string FileName = "FileName";
        public const string SelectedIndex = "SelectedIndex";
        public const string ID_Gebiete = "ID_Gebiete";

        public ImportRichtlinienZuordnungWizardPage()
        {
        }

        public ImportRichtlinienZuordnungWizardPage(BusinessLayer b, string formNameForResourceText)
            : base(b, formNameForResourceText)
        {
            InitializeComponent();
        }
    }
}
