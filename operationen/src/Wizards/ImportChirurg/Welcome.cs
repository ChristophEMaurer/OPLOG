using System;
using System.Windows.Forms;
using System.Globalization;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ImportChirurg
{
    public partial class Welcome : ImportChirurgWizardPage
    {
        public Welcome(BusinessLayer b)
            : base(b, "Wizards_ImportChirurg_Welcome")
        {
            InitializeComponent();
        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = string.Format(CultureInfo.InvariantCulture, GetText("welcome"), GetNextText());
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

