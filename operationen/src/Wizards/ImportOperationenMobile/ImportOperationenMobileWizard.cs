using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ImportOperationenMobile
{
    public partial class ImportOperationenMobileWizard : OperationenWizardMaster
    {
        private const string FormName = "Wizards_ImportOperationenMobile_wizard";

        public ImportOperationenMobileWizard(BusinessLayer b)
            : base(b)
        {
            InitializeComponent();

            _htData[ImportOperationenMobileWizardPage.FileName] = "";
            _htData[ImportOperationenMobileWizardPage.ID_Chirurgen] = -1;
            _htData[ImportOperationenMobileWizardPage.SelectedIndex] = 0;

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
