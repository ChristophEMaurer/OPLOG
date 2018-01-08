using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.CreateCustomerData
{
    public partial class CreateCustomerDataWizard : OperationenWizardMaster
    {
        private const string FormName = "Wizards_CreateCustomerData_Wizard";

        public CreateCustomerDataWizard(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();

            _htData[CreateCustomerDataWizardPage.KEY_CD_KRANKENHAUS] = "";
            _htData[CreateCustomerDataWizardPage.KEY_CD_PLZ] = "";
            _htData[CreateCustomerDataWizardPage.KEY_CD_STRASSE] = "";

            AddPage(new Welcome(businessLayer));
            AddPage(new BasicData(businessLayer));
            AddPage(new Summary(businessLayer));
            AddPage(new Completed(businessLayer));
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
