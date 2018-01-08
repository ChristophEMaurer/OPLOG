using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ExportRichtlinienZuordnung
{
    public partial class ExportRichtlinienZuordnungWizard : OperationenWizardMaster
    {
        private const string FormName = "Wizards_ExportRichtlinienZuordnung_Wizard";

        public ExportRichtlinienZuordnungWizard(BusinessLayer b)
            : base(b)
        {
            InitializeComponent();

            _htData[ExportRichtlinienZuordnungWizardPage.FileName] = "";
            _htData[ExportRichtlinienZuordnungWizardPage.ID_Gebiete] = -1;
            _htData[ExportRichtlinienZuordnungWizardPage.SelectedIndex] = 0;

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
