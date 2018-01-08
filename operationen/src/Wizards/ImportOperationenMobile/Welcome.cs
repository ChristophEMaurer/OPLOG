using System;
using System.Windows.Forms;
using Windows.Forms.Wizard;

namespace Operationen.Wizards.ImportOperationenMobile
{
    public partial class Welcome : ImportOperationenMobileWizardPage
    {
        private const string FormName = "Wizards_ImportOperationenMobile_Welcome";

        public Welcome(BusinessLayer b)
            : base(b)
        {
            InitializeComponent();
        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = GetText(FormName, "text");
        }

        protected override string PageName
        {
            get { return GetText(FormName, "pagename"); }
        }
    }
}

