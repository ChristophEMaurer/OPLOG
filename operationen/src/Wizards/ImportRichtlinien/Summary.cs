using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ImportRichtlinien
{
    public partial class Summary : ImportRichtlinienWizardPage
    {
        public Summary(BusinessLayer businessLayer)
            : base(businessLayer, "Wizards_ImportRichtlinien_Summary")
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
            DataRow row = _businessLayer.GetGebiet((int)Data[ImportRichtlinienWizardPage.ID_Gebiete]);

            lblInfo.Text = string.Format(CultureInfo.InvariantCulture, GetText("summary"),
                (string)row["Gebiet"],
                (string)Data[ImportRichtlinienWizardPage.FileName],
                Wizard.FinishText);
        }

        protected override bool OnFinish()
        {
            ImportRichtlinien(progressBar,
                (int)Data[ImportRichtlinienWizardPage.ID_Gebiete],
                (string)Data[ImportRichtlinienWizardPage.FileName]);

            // Egal ob geklappt oder nicht, anschlieﬂend kann man nur noch 
            // 'Schlieﬂen' klicken
            return true;
        }

        private bool ImportRichtlinien(
            ProgressBar progressBar, 
            int ID_Gebiete, 
            string fileName)
        {
            Wizard.DisableAll();

            RichtlinienImporter importer = new RichtlinienImporter(_businessLayer, progressBar);
            importer.Initialize(ID_Gebiete, fileName);

            bool success = importer.Import();

            SetSuccess(success);

            return success;
        }
    }
}
