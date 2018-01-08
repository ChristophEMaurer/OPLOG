using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ImportOPS
{
    public partial class Summary : ImportOPSWizardPage
    {
        public Summary(BusinessLayer b)
            : base(b, "Wizards_ImportOPS_Summary")
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

        private void Summary_Load(object sender, EventArgs e)
        {
            string msg = string.Format(CultureInfo.InvariantCulture, GetText("summary"),
                (string)Data[ImportOPSWizardPage.FileName],
                (string)Data[ImportOPSWizardPage.Format],
                 Wizard.FinishText);

            txtInfo.Text = msg;
        }

        protected override bool OnFinish()
        {
            bool success = ImportOPS((string)Data[ImportOPSWizardPage.FileName], (string)Data[ImportOPSWizardPage.Format]);

            // Egal ob geklappt oder nicht, anschlieﬂend kann man nur noch 
            // 'Schlieﬂen' klicken
            return true;
        }

        private bool ImportOPS(string fileName, string format)
        {
            OperationenDifferenceView dlg = new OperationenDifferenceView(_businessLayer, fileName, format);

            //
            // This will call OperationenDifferenceView_Shown()
            //
            dlg.ShowDialog();

            SetSuccess(true);

            return true;
        }
    }
}
