using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using Windows.Forms;
using Utility;
using Operationen;
using CMaurer.Operationen.AppFramework;

namespace Operationen
{
    public partial class PlanOperationVergleichIstView : OperationenForm
    {
        private const int ColumnIndexBalkenGrafik = 3;

        public PlanOperationVergleichIstView()
        {
        }

        public PlanOperationVergleichIstView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();

            InitListOperationen();

            this.Text = AppTitle(GetText("title"));

            SetInfoText(lblInfo, string.Format(GetText("info"), lblGesamtIst.Text, lblFilterOPS.Text));

            CheckQuelleDefaultSettings(chkIntern, chkExtern);
        }

        private void InitListOperationen()
        {
            lvOperationen.Clear();
            DefaultListViewProperties(lvOperationen);

            lvOperationen.Columns.Add(GetText("OPSKode"), 80, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("Bezeichnung"), -2, HorizontalAlignment.Left);
        }

        private void PopulateListOperationen(string ops, int top)
        {
            lvOperationen.Items.Clear();

            if (ops != null && ops.Length > 0)
            {
                DataView dv = BusinessLayer.SuggestGetOPSKodeTextForKodeOrText(ops, top);

                lvOperationen.BeginUpdate();
                foreach (DataRow row in dv.Table.Rows)
                {
                    ListViewItem lvi = new ListViewItem((string)row["OPSKode"]);
                    lvi.SubItems.Add((string)row["OPSText"]);
                    lvOperationen.Items.Add(lvi);
                }
                lvOperationen.EndUpdate();
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void PopulateOPFunktionen(ComboBox cb)
        {
            DataView dv = BusinessLayer.GetOPFunktionen(true);

            cb.ValueMember = "ID_OPFunktionen";
            cb.DisplayMember = "Beschreibung";
            cb.DataSource = dv;
            cb.SelectedValue = (int)OP_FUNCTION.OP_FUNCTION_OP;
        }

        private void PlanOperationVergleichViewIst_Load(object sender, EventArgs e)
        {
            PopulateOPFunktionen(cbChirurgenOPFunktionen);

            InitTest();
        }

        private void InitTest()
        {
            SetWatermark(lvTest);

            lvTest.Columns.Add(GetText("operateur"), 100, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("ist"), 60, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("prozent"), 70, HorizontalAlignment.Left);

            // Balkengrafik
            lvTest.Columns.Add(GetText("prozent"), -2, HorizontalAlignment.Left);

            lvTest.SetBalkenColumnIndex(ColumnIndexBalkenGrafik);
        }

        private void PopulateTest(string sOperation, int nID_OPFunktionen, int quelle)
        {
            DateTime? dtVon = GetDateTimeFromTextBox(txtDatumVon);
            DateTime? dtBis = GetDateTimeFromTextBox(txtDatumBis);

            lvTest.Items.Clear();

            DataView oChirurgen = BusinessLayer.GetChirurgen();

            long summeIst = 0;
            foreach (DataRow oChirurg in oChirurgen.Table.Rows)
            {
                int nID_Chirurgen = ConvertToInt32(oChirurg["ID_Chirurgen"]);
                long nIstAnzahl = 0;

                nIstAnzahl = BusinessLayer.GetChirurgenOperationenAnzahl(nID_Chirurgen, nID_OPFunktionen, quelle, sOperation, dtVon, dtBis);
                summeIst += nIstAnzahl;

                ListViewItem lvi = new ListViewItem((string)oChirurg["Nachname"]);
                lvi.SubItems.Add(nIstAnzahl.ToString());
                lvi.SubItems.Add("%");

                // Balkengrafik Daten
                lvi.SubItems.Add(nIstAnzahl.ToString());

                lvTest.Items.Add(lvi);
            }

            // Balkengrafik enthält: Ist/MAX
            // MAX ist das höchste Ist von allen Chirurgen
            foreach (ListViewItem lvi in lvTest.Items)
            {
                string s = string.Format("{0}|{1}", lvi.SubItems[3].Text, summeIst);

                lvi.SubItems[2].Text = string.Format("{0}%", ProzentFromBalkenGrafikData(s));
                lvi.SubItems[3].Text = s;
            }

            txtGesamtIst.Text = summeIst.ToString();
        }

        private void cmdVergleich_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Cursor = Cursors.WaitCursor;
                XableAllButtonsForLongOperation(null, false);

                CheckQuelleCorrectDumbSettings(chkIntern, chkExtern);

                int nID_OPFunktionen = ConvertToInt32(cbChirurgenOPFunktionen.SelectedValue);
                int quelle = GetInternExternFlag(chkIntern.Checked, chkExtern.Checked);
                PopulateTest(txtOperation.Text, nID_OPFunktionen, quelle);

                XableAllButtonsForLongOperation(null, true);
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

            // Wenn hier etwas eingegeben wurde, muss es ein Code sine und kein Text
            if (txtOperation.Text.Length > 0 && txtOperation.Text.IndexOfAny("1234567890".ToCharArray()) == -1)
            {
                bSuccess = false;
                msg += GetTextControlInvalid(lblFilterOPS);
            }

            if (!bSuccess)
            {
                MessageBox(msg);
            }

            return bSuccess;
        }

        private void txtOperation_TextChanged(object sender, EventArgs e)
        {
            if (!_bIgnoreControlEvents)
            {
                _bIgnoreControlEvents = true;
                PopulateListOperationen(txtOperation.Text, 50);
                _bIgnoreControlEvents = false;
            }
        }

        private void lvOperationen_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvOperationen.SelectedItems)
            {
                _bIgnoreControlEvents = true;

                string code = lvi.SubItems[0].Text;
                int index = code.IndexOf(".");
                if (index > 0)
                {
                    code = code.Substring(0, index);
                }
                txtOperation.Text = code;

                _bIgnoreControlEvents = false;
                break;
            }
        }

        protected override string PrintHTMLFilter()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(GetDateVonBisHTMLFilterText(txtDatumVon.Text, txtDatumBis.Text));
            sb.Append(GetQuelleInternExternText(chkIntern.Checked, chkExtern.Checked));
            if (txtOperation.Text.Length > 0)
            {
                sb.Append("<br/>");
                string text = string.Format(CultureInfo.InvariantCulture, GetText("printFilter1"), txtOperation.Text);

                sb.Append(text);
            }
            sb.Append("<br/>");
            sb.Append(GetText("funktion") + ": " + MakeSafeHTML(cbChirurgenOPFunktionen.Text));

            return sb.ToString();
        }

        protected override string PrintHTMLSummary()
        {
            return lblGesamtIst.Text + " " + txtGesamtIst.Text;
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            ContextMenuStrip cms = CreateContextMenu(ModePrintListViewBrowser | ModePrintListViewBrowserSelectedColumns);
            cms.Show(cmdPrint, new Point(10, 10));
        }

        protected override void PrintListViewBrowser_Click(object sender, EventArgs e)
        {
            PrintListView(lvTest, GlobalConstants.KeyPrintLinesPlanOperationVergleichIstView);
        }
        protected override void PrintListViewBrowserSelectedColumns_Click(object sender, EventArgs e)
        {
            PrintListViewSelectedColumns(lvTest, GlobalConstants.KeyPrintLinesPlanOperationVergleichIstView);
        }

        protected override string PrintHTMLGetText(ListViewItem lvi, int index)
        {
            string s = lvi.SubItems[index].Text;

            if (index == ColumnIndexBalkenGrafik)
            {
                s = MakeHtmlBalkenGrafik(s);
            }
            else
            {
                s = MakeHTML(s);
            }

            return s;
        }
    }
}

