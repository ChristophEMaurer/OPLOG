using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.InstallLicense
{
    public partial class InstallLicenseWizard : OperationenWizardMaster
    {
        private const string FormName = "Wizards_InstallLicense_Wizard";

        public InstallLicenseWizard(BusinessLayer b)
            : base(b)
        {
            InitializeComponent();

            _htData[InstallLicenseWizardPage.FileName] = "";

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
