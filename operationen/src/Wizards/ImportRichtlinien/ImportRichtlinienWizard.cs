using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ImportRichtlinien
{
    public partial class ImportRichtlinienWizard : OperationenWizardMaster
    {
        private const string FormName = "Wizards_ImportRichtlinien_Wizard";

        public ImportRichtlinienWizard(BusinessLayer b)
            : base(b)
        {
            InitializeComponent();

            _htData[ImportRichtlinienWizardPage.FileName] = "";
            _htData[ImportRichtlinienWizardPage.ID_Gebiete] = -1;
            _htData[ImportRichtlinienWizardPage.SelectedIndex] = 0;

            AddPage(new Welcome(b));
            AddPage(new SelectGebiet(b));
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
