using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ImportRichtlinienZuordnung
{
    public partial class ImportRichtlinienZuordnungWizard : OperationenWizardMaster
    {
        private const string FormName = "Wizards_ImportRichtlinienZuordnung_Wizard";

        public ImportRichtlinienZuordnungWizard(BusinessLayer b)
            : base(b)
        {
            InitializeComponent();

            _htData[ImportRichtlinienZuordnungWizardPage.FileName] = "";
            _htData[ImportRichtlinienZuordnungWizardPage.ID_Gebiete] = -1;
            _htData[ImportRichtlinienZuordnungWizardPage.SelectedIndex] = 0;

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
