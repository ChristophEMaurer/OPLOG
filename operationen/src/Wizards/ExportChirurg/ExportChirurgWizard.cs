using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ExportChirurg
{
    public partial class ExportChirurgWizard : OperationenWizardMaster
    {
        private const string FormName = "Wizards_ExportChirurg_wizard";

        public ExportChirurgWizard(BusinessLayer b)
            : base(b)
        {
            InitializeComponent();

            _htData[ExportChirurgWizardPage.FileName] = "";
            _htData[ExportChirurgWizardPage.ID_Chirurgen] = -1;
            _htData[ExportChirurgWizardPage.SelectedIndex] = 0;

            AddPage(new Welcome(b));
            AddPage(new SelectChirurg(b));
            AddPage(new SelectFile(b));
            AddPage(new Summary(b));
            AddPage(new Completed(b));
        }

        public override string Title
        {
            get
            {
                return GetText(FormName, "title");
            }
        }
    }
}
