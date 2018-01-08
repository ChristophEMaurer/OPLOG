using System.Reflection;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ExportChirurg
{
    public partial class ExportChirurgWizardPage : OperationenWizardPage
    {
        public const string FileName = "FileName";
        public const string ID_Chirurgen = "ID_Chirurgen";
        public const string SelectedIndex = "SelectedIndex";

        public ExportChirurgWizardPage()
        {
        }

        public ExportChirurgWizardPage(BusinessLayer b)
            : base(b)
        {
            InitializeComponent();
        }
    }
}
