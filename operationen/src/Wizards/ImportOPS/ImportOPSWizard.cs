using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ImportOPS
{
    public partial class ImportOPSWizard : OperationenWizardMaster
    {
        private const string FormName = "Wizards_ImportOPS_wizard";

        public ImportOPSWizard(BusinessLayer b)
            : base(b)
        {
            InitializeComponent();

            _htData[ImportOPSWizardPage.FileName] = "";
            _htData[ImportOPSWizardPage.Format] = "";

            AddPage(new Welcome(b));
            AddPage(new SelectFile(b));
            AddPage(new SelectVersion(b));
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
