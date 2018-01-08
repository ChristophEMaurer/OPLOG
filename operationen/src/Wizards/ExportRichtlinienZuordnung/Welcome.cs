using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ExportRichtlinienZuordnung
{
    public partial class Welcome : ExportRichtlinienZuordnungWizardPage
    {
        public Welcome(BusinessLayer b)
            : base(b, "Wizards_ExportRichtlinienZuordnung_Welcome")
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

