using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ImportRichtlinienZuordnung
{
    public partial class Welcome : ImportRichtlinienZuordnungWizardPage
    {
        public Welcome(BusinessLayer b)
            : base(b, "Wizards_ImportRichtlinienZuordnung_Welcome")
        {
            InitializeComponent();

            string msg = string.Format(CultureInfo.InvariantCulture, GetText("info"), GetNextText());
            lblWelcome.Text = msg;
        }

        protected override string PageName
        {
            get
            {
                return GetText("pageName");
            }
        }
    }
}

