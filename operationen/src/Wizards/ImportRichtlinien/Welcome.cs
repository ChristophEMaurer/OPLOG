using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ImportRichtlinien
{
    public partial class Welcome : ImportRichtlinienWizardPage
    {
        public Welcome(BusinessLayer b)
            : base(b, "Wizards_ImportRichtlinien_Welcome")
        {
            InitializeComponent();


            lblWelcome.Text = string.Format(CultureInfo.InvariantCulture, GetText("info"), GetNextText());
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

