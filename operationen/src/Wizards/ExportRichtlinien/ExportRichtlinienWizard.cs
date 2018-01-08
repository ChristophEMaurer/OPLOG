using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ExportRichtlinien
{
    public partial class ExportRichtlinienWizard : OperationenWizardMaster
    {
        private const string FormName = "Wizards_ExportRichtlinien_wizard";

        public ExportRichtlinienWizard(BusinessLayer b)
            : base(b)
        {
            InitializeComponent();

            _htData[ExportRichtlinienWizardPage.FileName] = "";
            _htData[ExportRichtlinienWizardPage.ID_Gebiete] = -1;
            _htData[ExportRichtlinienWizardPage.SelectedIndex] = 0;

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

