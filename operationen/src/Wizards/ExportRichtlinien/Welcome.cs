using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ExportRichtlinien
{
    public partial class Welcome : ExportRichtlinienWizardPage
    {
        private const string FormName = "Wizards_ExportRichtlinien_Welcome";

        public Welcome(BusinessLayer b)
            : base(b)
        {
            InitializeComponent();
        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = string.Format(CultureInfo.InvariantCulture, GetText(FormName, "text"), GetNextText());
        }

        protected override string PageName
        {
            get { return GetText(FormName, "pagename"); }
        }
    }
}

