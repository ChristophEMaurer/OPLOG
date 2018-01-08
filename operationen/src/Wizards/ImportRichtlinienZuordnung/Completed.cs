using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ImportRichtlinienZuordnung
{
    public partial class Completed : ImportRichtlinienZuordnungWizardPage
    {
        public Completed(BusinessLayer b)
            : base(b, "Wizards_ImportRichtlinienZuordnung_Completed")
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

