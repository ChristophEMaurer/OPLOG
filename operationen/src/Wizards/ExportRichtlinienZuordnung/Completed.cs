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
    public partial class Completed : ExportRichtlinienZuordnungWizardPage
    {
        public Completed(BusinessLayer b)
            : base(b, "Wizards_ExportRichtlinienZuordnung_Completed")
        {
            InitializeComponent();
        }

        protected override string PageName
        {
            get
            {
                return GetText("PageName");
            }
        }

        protected override void OnActivate()
        {
            if (GetSuccess())
            {
                lblInfo.Text = string.Format(CultureInfo.InvariantCulture, GetText("success"), 
                    (string)Data[ExportRichtlinienZuordnungWizardPage.FileName]);
            }
            else
            {
                lblInfo.Text = GetText("failure");
            }
        }
    }
}

