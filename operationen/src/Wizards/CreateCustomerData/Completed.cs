using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.CreateCustomerData
{
    public partial class Completed : CreateCustomerDataWizardPage
    {
        public Completed(BusinessLayer businessLayer)
            : base(businessLayer, "Wizards_CreateCustomerData_completed")
        {
            InitializeComponent();
        }

        protected override string PageName
        {
            get
            {
                return GetText("pagename");
            }
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

