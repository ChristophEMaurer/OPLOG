using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ImportOperationenMobile
{
    public partial class Completed : ImportOperationenMobileWizardPage
    {
        private const string FormName = "Wizards_ImportOperationenMobile_Completed";

        public Completed(BusinessLayer b)
            : base(b)
        {
            InitializeComponent();
        }

        protected override string PageName
        {
            get
            {
                return GetText(FormName, "pagename");
            }
        }

        protected override void OnActivate()
        {
            string fileName = (string)Data[ImportOperationenMobileWizardPage.FileName];

            if (GetSuccess())
            {
                lblInfo.Text = GetText(FormName, "completed_success");
            }
            else
            {
                lblInfo.Text = GetText(FormName, "completed_error");
            }
        }
    }
}

