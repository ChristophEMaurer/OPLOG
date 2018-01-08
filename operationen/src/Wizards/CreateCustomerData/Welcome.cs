using System;
using System.Windows.Forms;
using System.Globalization;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.CreateCustomerData
{
    public partial class Welcome : CreateCustomerDataWizardPage
    {
        public Welcome(BusinessLayer businessLayer)
            : base(businessLayer, "Wizards_CreateCustomerData_Welcome")
        {
            InitializeComponent();
        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = string.Format(CultureInfo.InvariantCulture, GetText("text"), GetNextText());
        }

        protected override string PageName
        {
            get
            {
                return GetText("pagename");
            }
        }
    }
}

