using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ImportChirurg
{
    public partial class Completed : ImportChirurgWizardPage
    {
        public Completed(BusinessLayer b)
            : base(b, "Wizards_ImportChirurg_Completed")
        {
            InitializeComponent();
        }

        protected override string PageName
        {
            get
            {
                return GetText("pageName");
            }
        }

        protected override void OnActivate()
        {
            string fileName = (string)Data[ImportChirurgWizardPage.FileName];

            if (GetSuccess())
            {
                lblInfo.Text = GetText("msg1");
            }
            else
            {
                lblInfo.Text = GetText("msg2");
            }
        }
    }
}

