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
    public partial class Completed : ExportRichtlinienWizardPage
    {
        private const string FormName = "Wizards_ExportRichtlinien_Completed";

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
            if (GetSuccess())
            {
                lblInfo.Text = string.Format(CultureInfo.InvariantCulture, GetText(FormName, "text"),
                    (string)Data[ExportRichtlinienWizardPage.FileName]);
            }
            else
            {
                lblInfo.Text = GetText(FormName, "error");
            }
        }
    }
}

