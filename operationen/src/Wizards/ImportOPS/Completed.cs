using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ImportOPS
{
    public partial class Completed : ImportOPSWizardPage
    {
        public Completed(BusinessLayer b)
            : base(b, "Wizards_ImportOPS_Completed")
        {
            InitializeComponent();
        }

        protected override string PageName
        {
            get { return GetText("pagename"); }
        }

        protected override void OnActivate()
        {
            if (GetSuccess())
            {
                lblInfo.Text = GetText("success");
            }
            else
            {
                lblInfo.Text = GetText("error");
            }
        }
    }
}

