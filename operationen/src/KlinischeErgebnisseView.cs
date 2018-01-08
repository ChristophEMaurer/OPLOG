using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Windows.Forms;
using Utility;
using Operationen;
using CMaurer.Operationen.AppFramework;

namespace Operationen
{
    public partial class KlinischeErgebnisseView : OperationenForm
    {
        public KlinischeErgebnisseView()
        {
        }

        public KlinischeErgebnisseView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();

            this.Text = AppTitle(GetText("title"));

            InitTest();

            PopulateOPFunktionen(cbChirurgenOPFunktionen, true);
            PopulateKlinischeErgebnisseTypen(cbKlinischeErgebnisseTypen, true);

            CheckQuelleDefaultSettings(chkIntern, chkExtern);
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void InitTest()
        {
            SetWatermark(lvTest);

            lvTest.Columns.Add(GetText("surgeon"), 100, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("actual"), 60, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("percent"), 70, HorizontalAlignment.Left);
            
            // Balkengrafik
            lvTest.Columns.Add(GetText("Prozent"), -2, HorizontalAlignment.Left);

            lvTest.SetBalkenColumnIndex(3);
        }

        private void PopulateTest(string sOperation, int nID_OPFunktionen, int quelle, int ID_KlinischeErgebnisseTypen)
        {
            DateTime? dtVon;
            DateTime? dtBis;

            GetVonBisDatum(txtDatumVon, txtDatumBis, out dtVon, out dtBis);

            lvTest.Items.Clear();
            lvTest.BeginUpdate();

            DataView oChirurgen = BusinessLayer.GetChirurgen();

            long summeIst = 0;
            foreach (DataRow oChirurg in oChirurgen.Table.Rows)
            {
                int nID_Chirurgen = ConvertToInt32(oChirurg["ID_Chirurgen"]);
                long nIstAnzahl = 0;

                nIstAnzahl = BusinessLayer.GetKlinischeErgebnisseAnzahl(nID_Chirurgen, nID_OPFunktionen, ID_KlinischeErgebnisseTypen, quelle, sOperation, dtVon, dtBis);
                summeIst += nIstAnzahl;

                ListViewItem lvi = new ListViewItem((string)oChirurg["Nachname"]);
                lvi.SubItems.Add(nIstAnzahl.ToString());

                // Prozent: ist noch unbekannt
                lvi.SubItems.Add("%");

                // Balkengrafik Daten
                lvi.SubItems.Add(nIstAnzahl.ToString());

                lvTest.Items.Add(lvi);
            }

            // Balkengrafik enthält: Ist/MAX
            // MAX ist das höchste Ist von allen Chirurgen
            foreach (ListViewItem lvi in lvTest.Items)
            {
                string s = lvi.SubItems[3].Text + "|" + summeIst.ToString();

                lvi.SubItems[2].Text = string.Format("{0}%", ProzentFromBalkenGrafikData(s).ToString());
                lvi.SubItems[3].Text = s;
            }

            lvTest.EndUpdate();

            txtGesamt.Text = summeIst.ToString();
        }

        private void cmdVergleich_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Cursor = Cursors.WaitCursor;

                CheckQuelleCorrectDumbSettings(chkIntern, chkExtern);

                int ID_OPFunktionen = ConvertToInt32(cbChirurgenOPFunktionen.SelectedValue);
                int ID_KlinischeErgebnisseTypen = ConvertToInt32(cbKlinischeErgebnisseTypen.SelectedValue);
                int quelle = GetInternExternFlag(chkIntern.Checked, chkExtern.Checked);
                PopulateTest(txtOPSKode.Text, ID_OPFunktionen, quelle, ID_KlinischeErgebnisseTypen);
                
                Cursor = Cursors.Default;
            }
        }

        protected override bool ValidateInput()
        {
            bool bSuccess = true;
            string msg = EINGABEFEHLER;

            if (txtDatumVon.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtDatumVon.Text))
            {
                bSuccess = false;
                msg += GetTextControlInvalidDate(lblVon);
            }
            if (txtDatumBis.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtDatumBis.Text))
            {
                bSuccess = false;
                msg += GetTextControlInvalidDate(lblBis);
            }

            if (!bSuccess)
            {
                MessageBox(msg);
            }

            return bSuccess;
        }

        protected override string PrintHTMLFilter()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(PrintHTMLFilter(txtDatumVon.Text, txtDatumBis.Text, chkIntern.Checked, chkExtern.Checked, cbChirurgenOPFunktionen.Text));
            sb.Append(lblKlinischeErgebnisseTypen.Text + MakeSafeHTML(cbKlinischeErgebnisseTypen.Text));

            return sb.ToString();
        }

        protected override string PrintHTMLSummary()
        {
            return lblGesamt.Text + " " + txtGesamt.Text;
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            ContextMenuStrip cms = CreateContextMenu(ModePrintListViewBrowser | ModePrintListViewBrowserSelectedColumns);
            cms.Show(cmdPrint, new Point(10, 10));
        }

        protected override void PrintListViewBrowser_Click(object sender, EventArgs e)
        {
            PrintListView(lvTest, GlobalConstants.KeyPrintLinesKlinischeErgebnisseView);
        }
        protected override void PrintListViewBrowserSelectedColumns_Click(object sender, EventArgs e)
        {
            PrintListViewSelectedColumns(lvTest, GlobalConstants.KeyPrintLinesKlinischeErgebnisseView);
        }

        protected override string PrintHTMLGetText(ListViewItem lvi, int index)
        {
            string s = lvi.SubItems[index].Text;

            if (index == 3)
            {
                s = MakeHtmlBalkenGrafik(s);
            }
            else
            {
                s = MakeHTML(s);
            }

            return s;
        }

        private void KlinischeErgebnisseView_Load(object sender, EventArgs e)
        {
        }
    }
}

