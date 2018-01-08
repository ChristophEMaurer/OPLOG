using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ExportRichtlinien
{
    public partial class Summary : ExportRichtlinienWizardPage
    {
        private const string FormName = "Wizards_ExportRichtlinien_Summary";

        public Summary(BusinessLayer b)
            : base(b)
        {
            InitializeComponent();
        }
        protected override string PageName
        {
            get { return GetText(FormName, "pagename"); }
        }

        private void Summary_Load(object sender, EventArgs e)
        {
            Hashtable data = Data;

            DataRow row = _businessLayer.GetGebiet((int)Data[ExportRichtlinienWizardPage.ID_Gebiete]);

            lblText.Text = string.Format(CultureInfo.InvariantCulture, GetText(FormName, "text"),
                (string)row["Gebiet"], 
                (string)Data[ExportRichtlinienWizardPage.FileName],
                Wizard.FinishText);
        }

        protected override bool OnFinish()
        {
            /* bool success = */ ExportRichtlinien(null,
                (int)Data[ExportRichtlinienWizardPage.ID_Gebiete],
                (string)Data[ExportRichtlinienWizardPage.FileName]);

            // Egal ob geklappt oder nicht, anschlieﬂend kann man nur noch 
            // 'Schlieﬂen' klicken
            return true;
        }

        private bool ExportRichtlinien(
            ProgressBar progressBar, 
            int ID_Gebiete, 
            string fileName)
        {
            Cursor = Cursors.WaitCursor;

            RichtlinienExporter exporter = new RichtlinienExporter(_businessLayer, progressBar);
            exporter.Initialize(ID_Gebiete, fileName);

            bool success = exporter.Export();

            SetSuccess(success);

            Cursor = Cursors.Default;

            return success;
        }
    }
}
