using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ExportRichtlinienZuordnung
{
    public partial class Summary : ExportRichtlinienZuordnungWizardPage
    {
        public Summary(BusinessLayer b)
            : base(b, "Wizards_ExportRichtlinienZuordnung_Summary")
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

        private void Summary_Load(object sender, EventArgs e)
        {
            Hashtable data = Data;

            DataRow row = _businessLayer.GetGebiet((int)Data[ExportRichtlinienZuordnungWizardPage.ID_Gebiete]);

            lblText.Text = String.Format(CultureInfo.InvariantCulture, GetText("text"), 
                (string)row["Gebiet"], 
                (string)Data[ExportRichtlinienZuordnungWizardPage.FileName],
                Wizard.FinishText);
        }

        protected override bool OnFinish()
        {
            bool success = Export(null,
                (int)Data[ExportRichtlinienZuordnungWizardPage.ID_Gebiete],
                (string)Data[ExportRichtlinienZuordnungWizardPage.FileName]);

            // Egal ob geklappt oder nicht, anschlieﬂend kann man nur noch 
            // 'Schlieﬂen' klicken
            return true;
        }

        private bool Export(
            ProgressBar progressBar, 
            int ID_Gebiete, 
            string fileName)
        {
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            RichtlinienExporterZuordnung exporter = new RichtlinienExporterZuordnung(_businessLayer, progressBar);
            exporter.Initialize(ID_Gebiete, fileName);

            bool success = exporter.Export();

            SetSuccess(success);

            Cursor = Cursors.Default;

            return success;
        }
    }
}
