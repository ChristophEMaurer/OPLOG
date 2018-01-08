using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ImportChirurg
{
    public partial class ImportChirurgWizard : OperationenWizardMaster
    {
        private const string FormName = "Wizards_ImportChirurg_Wizard";

        public ImportChirurgWizard(BusinessLayer b)
            : base(b)
        {
            InitializeComponent();

            _htData[ImportChirurgWizardPage.FileName] = "";

            AddPage(new Welcome(b));
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
