using System;
using System.Collections;
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
    public partial class PlanOperationVergleichView : OperationenForm
    {
        private const int ColumnIndexBalkenGrafik = 4;

        public PlanOperationVergleichView()
        {
        }

        public PlanOperationVergleichView(BusinessLayer businessLayer) : base(businessLayer)
        {
            InitializeComponent();

            CheckQuelleDefaultSettings(chkIntern, chkExtern);

            this.Text = AppTitle(GetText("title"));

            lblInfo.SetSecurity(businessLayer, "PlanOperationenView.view");
            SetInfoText(lblInfo, string.Format(GetText("info"), Command_PlanOperationenView));
            if (UserHasRight("PlanOperationenView.view"))
            {
                AddLinkLabelLink(lblInfo, Command_PlanOperationenView, Command_PlanOperationenView);
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
            cb.SelectedValue = BusinessLayer.ID_Alle;
        }

        private void PlanOperationVergleichView_Load(object sender, EventArgs e)
        {
            PopulateOPFunktionen(cbChirurgenOPFunktionen);

            InitTest();
        }

        private void InitTest()
        {
            SetWatermark(lvTest);
            lvTest.Columns.Add(GetText("operateur"), 120, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("ist"), 80, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("soll"), 80, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("prozent"), 70, HorizontalAlignment.Left);
            
            // Balkengrafik
            lvTest.Columns.Add(GetText("prozent"), -2, HorizontalAlignment.Left);

            lvTest.SetBalkenColumnIndex(ColumnIndexBalkenGrafik);
        }

        private void PopulateTest(string sOperation, int nID_OPFunktionen, int quelle)
        {
            DateTime? dtVon;
            DateTime? dtBis;

            GetVonBisDatum(txtDatumVon, txtDatumBis, out dtVon, out dtBis, true);

            lvTest.Items.Clear();

            DataView oChirurgen = BusinessLayer.GetChirurgen();

            long summeIst = 0;
            int i = 0;
            foreach (DataRow oChirurg in oChirurgen.Table.Rows)
            {
                int nID_Chirurgen = ConvertToInt32(oChirurg["ID_Chirurgen"]);
                long nIstAnzahl = 0;
                long nPlanAnzahl = 0;

                i++;
                if (i % BusinessLayer.AbortLoopCount == 0)
                {
                    Application.DoEvents();
                }

                nIstAnzahl = BusinessLayer.GetChirurgenOperationenAnzahl(nID_Chirurgen, nID_OPFunktionen, quelle, sOperation, dtVon, dtBis);
                summeIst += nIstAnzahl;
                nPlanAnzahl = BusinessLayer.GetPlanOperationenSumme(nID_Chirurgen, sOperation, dtVon, dtBis);

                string data = string.Format("{0}|{1}", nIstAnzahl, nPlanAnzahl);

                ListViewItem lvi = new ListViewItem((string)oChirurg["Nachname"]);
                lvi.SubItems.Add(nIstAnzahl.ToString());
                lvi.SubItems.Add(nPlanAnzahl.ToString());
                lvi.SubItems.Add(string.Format("{0}%", ProzentFromBalkenGrafikData(data).ToString()));

                // Balkengrafik Daten
                lvi.SubItems.Add(data);

                lvTest.Items.Add(lvi);
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
            string strMsg = EINGABEFEHLER;

            if (txtDatumVon.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtDatumVon.Text))
            {
                bSuccess = false;
                strMsg += GetTextControlInvalidDate(lblVon);
            }
            if (txtDatumBis.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtDatumBis.Text))
            {
                bSuccess = false;
                strMsg += GetTextControlInvalidDate(lblBis);
            }

            if (!bSuccess)
            {
                MessageBox(strMsg);
            }

            return bSuccess;
        }

        private void lblInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OperationenLogbuchView.TheMainWindow.StartMenuItem(e.Link.LinkData);
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            ContextMenuStrip cms = CreateContextMenu(ModePrintListViewBrowser | ModePrintListViewBrowserSelectedColumns);
            cms.Show(cmdPrint, new Point(10, 10));
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

        protected override void PrintListViewBrowser_Click(object sender, EventArgs e)
        {
            PrintListView(lvTest, GlobalConstants.KeyPrintLinesDefaultString);
        }
        protected override void PrintListViewBrowserSelectedColumns_Click(object sender, EventArgs e)
        {
            PrintListViewSelectedColumns(lvTest, GlobalConstants.KeyPrintLinesDefaultString);
        }
        protected override string PrintHTMLFilter()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(PrintHTMLFilter(txtDatumVon.Text, txtDatumBis.Text, chkIntern.Checked, chkExtern.Checked, cbChirurgenOPFunktionen.Text));
            if (txtOperation.Text.Length > 0)
            {
                string text = string.Format(CultureInfo.InvariantCulture, GetText("printFilter1"), MakeSafeHTML(txtOperation.Text));

                sb.Append(text);
            }
            sb.Append("<br/>");
            sb.Append(lblGesamtIst.Text + txtGesamtIst.Text);

            return sb.ToString();
        }
    }
}

