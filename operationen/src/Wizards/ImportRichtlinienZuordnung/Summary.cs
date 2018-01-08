using System;
using System.IO;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using Windows.Forms.Wizard;
using Utility;

namespace Operationen.Wizards.ImportRichtlinienZuordnung
{
    public partial class Summary : ImportRichtlinienZuordnungWizardPage
    {
        public Summary(BusinessLayer b)
            : base(b, "Wizards_ImportRichtlinienZuordnung_Summary")
        {
            InitializeComponent();
        }
        protected override string PageName
        {
            get { return GetText("pageName"); }
        }

        private string ReadGebietFromFile()
        {
            string line = GetText("msg1");
            StreamReader reader = null;
            string fileName = (string)Data[ImportRichtlinienZuordnungWizardPage.FileName];

            try
            {
                reader = new StreamReader(fileName, Encoding.Unicode);

                // Erste Zeile enth‰lt die Signatur
                if (_businessLayer.CheckTextFileSignature(reader, BusinessLayer.FileSignatureOPSKodesRichtlinien, RichtlinienZuordnungImporter.Version))
                {
                    line = reader.ReadLine();
                    string[] arLine = line.Split('|');
                    if (arLine.Length == 2)
                    {
                        line = arLine[1];
                    }
                }
            }
            catch
            {
                _businessLayer.MessageBox(string.Format(GetText("msg2"), fileName));
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return line;
        }

        private void Summary_Load(object sender, EventArgs e)
        {
            DataRow row = _businessLayer.GetGebiet((int)Data[ImportRichtlinienZuordnungWizardPage.ID_Gebiete]);

            string msg = string.Format(CultureInfo.InvariantCulture, GetText("summary"),
                (string)row["Gebiet"],
                (string)Data[ImportRichtlinienZuordnungWizardPage.FileName],
                ReadGebietFromFile(),
                Wizard.FinishText);

            txtInfo.Text = msg;
        }

        protected override bool OnFinish()
        {
            if (_businessLayer.CheckTextFileSignature((string)Data[ImportRichtlinienZuordnungWizardPage.FileName], 
                BusinessLayer.FileSignatureOPSKodesRichtlinien, RichtlinienZuordnungImporter.Version))
            {
                RichtlinienValidateView dlg = new RichtlinienValidateView(_businessLayer,
                    (int)Data[ImportRichtlinienZuordnungWizardPage.ID_Gebiete],
                    (string)Data[ImportRichtlinienZuordnungWizardPage.FileName]);

                if (DialogResult.OK == dlg.ShowDialog())
                {
                    Import(progressBar,
                        (int)Data[ImportRichtlinienZuordnungWizardPage.ID_Gebiete],
                        (string)Data[ImportRichtlinienZuordnungWizardPage.FileName]);
                }
            }

            // Egal ob geklappt oder nicht, anschlieﬂend kann man nur noch 
            // 'Schlieﬂen' klicken
            return true;
        }

        private bool Import(
            ProgressBar progressBar, 
            int ID_Gebiete, 
            string fileName)
        {
            Wizard.DisableAll();

            RichtlinienZuordnungImporter importer = new RichtlinienZuordnungImporter(_businessLayer, progressBar);
            importer.Initialize(ID_Gebiete, fileName);

            bool success = importer.Import();

            SetSuccess(success);

            return success;
        }
    }
}
