using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Utility;
using CMaurer.Operationen.AppFramework;

namespace Operationen
{
    /// <summary>
    /// Es können beliebig überlappende und auseinanderliegende Zeiträume eingegeben worden sein als Planzahlen.
    /// Unten gibt man genau einen Zeitraum ein. 1111 bedeutet Zeitraum für Opscode 1
    /// 
    /// 1, 5:   liegen ganz draußen
    /// 2, 4:   überlappen
    /// 3:      liegt ganz drin
    /// 
    /// Oben (Plan):    
    ///     11111 22222  3333333         444444  55555555
    ///                3333             3333333
    /// Unten (Ist):
    ///             xxxxxxxxxxxxxxxxxxxxxxxx
    ///             
    /// 1. Welche OPSCodes sollen nun gezählt werden, wenn der Zeitraum xxxx eingegeben wurde?
    ///     Es werden alle OPSCodes genommen, deren Zeitraum ganz im angegebenen Filter-Zeitraum liegen.
    /// 
    /// 2. Wie wird die Soll-Zahl für einen OPSCode im Zeitraum xxxx ermittelt, da ein OPSCode mehrfach in 
    ///     verschiedenen Zeiträumen vorkommen kann?
    ///     Es werden alle Soll Zahlen gleicher OPSCodes addiert, deren Zeitraum ganz im angegebenen Filter-Zeitraum liegen.
    /// 
    /// 3. Wie wird die Ist-Zahl berechnet?
    ///     Das ist einfach: Der Zeitpunkt der Operation muss innerhalb des Filter-Zeitraumes liegen.
    /// 
    /// </summary>
    public partial class PlanOperationenView : OperationenForm
    {

        private const int ColumnIndexBalkenGrafik = 5;
        private bool _printTest;

        public PlanOperationenView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();

            this.Text = AppTitle(GetText("title"));

            cmdNewPlanOperation.SetSecurity(businessLayer, "PlanOperationenView.edit");
            cmdDeletePlanOperation.SetSecurity(businessLayer, "PlanOperationenView.edit");
            cmdEditPlanOperation.SetSecurity(businessLayer, "PlanOperationenView.edit");

            lblInfo.SetSecurity(businessLayer, "PlanOperationVergleichView.edit");
            SetInfoText(lblInfo, string.Format(GetText("info"), Command_PlanOperationVergleichView));
            if (UserHasRight("PlanOperationVergleichView.edit"))
            {
                AddLinkLabelLink(lblInfo, Command_PlanOperationVergleichView, Command_PlanOperationVergleichView);
            }

            PopulateOPFunktionen(cbChirurgenOPFunktionen, true);
            CheckQuelleDefaultSettings(chkIntern, chkExtern);
            PopulateForm();
        }

        private void PlanOperationen_Load(object sender, EventArgs e)
        {
        }

        private void ProgressStart()
        {
            Cursor = Cursors.WaitCursor;
        }
        private void ProgressStop()
        {
            Cursor = Cursors.Default;
        }

        private void cmdNewPlanOperation_Click(object sender, EventArgs e)
        {
            if (UserHasRight("PlanOperationenView.edit"))
            {
                int nID_Chirurgen = ConvertToInt32(cbChirurgen.SelectedValue);

                if (nID_Chirurgen != -1)
                {
                    PlanOperationView dlg = new PlanOperationView(BusinessLayer, nID_Chirurgen, null);

                    if (DialogResult.Cancel != dlg.ShowDialog())
                    {
                        ProgressStart();
                        this.PopulatePlanOperationen();
                        ProgressStop();
                    }
                }
            }
        }
        private void PopulatePlanOperationen()
        {
            int nID_Chirurgen = ConvertToInt32(cbChirurgen.SelectedValue);

            if (nID_Chirurgen != -1)
            {
                PopulatePlanOperationen(nID_Chirurgen);
            }
        }
        private void InitPlanOperationen()
        {
            lvPlanOperationen.Clear();

            DefaultListViewProperties(lvPlanOperationen);

            lvPlanOperationen.Columns.Add(GetText("col_von"), 70, HorizontalAlignment.Left);
            lvPlanOperationen.Columns.Add(GetText("col_bis"), 70, HorizontalAlignment.Left);
            lvPlanOperationen.Columns.Add(GetText("col_anzahl"), 50, HorizontalAlignment.Left);
            lvPlanOperationen.Columns.Add(GetText("col_opsKode"), 80, HorizontalAlignment.Left);
            lvPlanOperationen.Columns.Add(GetText("col_auto"), -2, HorizontalAlignment.Left);
        }


        private void PopulateForm()
        {
            InitPlanOperationen();
            InitTest();
            PopulateChirurgen(cbChirurgen);
        }

        private void PopulatePlanOperationen(int nID_Chirurgen)
        {
            DataView dv = BusinessLayer.GetPlanOperationen(nID_Chirurgen);

            ProgressStart();
            lvPlanOperationen.Items.Clear();
            lvPlanOperationen.BeginUpdate();
            foreach (DataRow dataRow in dv.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem(Tools.DBNullableDateTime2DateString(dataRow["DatumVon"]));
                lvi.Tag = ConvertToInt32(dataRow["ID_PlanOperationen"]);
                lvi.SubItems.Add(Tools.DBNullableDateTime2DateString(dataRow["DatumBis"]));
                lvi.SubItems.Add(dataRow["Anzahl"].ToString());

                string strOperation = (string)dataRow["Operation"];
                lvi.SubItems.Add(strOperation);
                AddMatchingOpsText(lvi, strOperation);
                lvPlanOperationen.Items.Add(lvi);
            }
            lvPlanOperationen.EndUpdate();
            ProgressStop();

            SetGroupBoxText(lvPlanOperationen, grpPlanOperationen, GetText("grpPlan"));
        }

        private void cmdDeletePlanOperation_Click(object sender, EventArgs e)
        {
            if (UserHasRight("PlanOperationenView.edit"))
            {
                if (lvPlanOperationen.SelectedItems.Count > 0)
                {
                    if (Confirm(GetText("confirmDelete")))
                    {
                        foreach (ListViewItem lvi in lvPlanOperationen.SelectedItems)
                        {
                            int nID_PlanOperationen = (int)lvi.Tag;
                            if (nID_PlanOperationen != -1)
                            {
                                if (!BusinessLayer.DeletePlanOperation(nID_PlanOperationen))
                                {
                                    break;
                                }
                                lvPlanOperationen.Items.Remove(lvi);
                            }
                        }
                        int nID_Chirurgen = ConvertToInt32(cbChirurgen.SelectedValue);

                        if (nID_Chirurgen != -1)
                        {
                            this.PopulateTest(nID_Chirurgen);
                        }
                    }
                }
                else
                {
                    MessageBox(GetTextSelectionNone());
                }
            }
        }

        private void SetGroupBoxTextTest()
        {
            SetGroupBoxText(lvTest, grpTest, GetText("grpSummen"));
        }

        private void PopulateTest(int nID_Chirurgen)
        {
            DateTime dtFrom = Tools.DatabaseDateTimeMinValue;
            DateTime dtTo = Tools.DatabaseDateTimeMaxValue;

            if (txtDatumVon.Text.Length > 0)
            {
                dtFrom = Tools.InputTextDate2DateTime(txtDatumVon.Text);
            }
            if (txtDatumBis.Text.Length > 0)
            {
                dtTo = Tools.InputTextDate2DateTimeEnd(txtDatumBis.Text);
            }

            CheckQuelleCorrectDumbSettings(chkIntern, chkExtern);
            int quelle = GetInternExternFlag(chkIntern.Checked, chkExtern.Checked);

            DataView oPlanOperationenArten = BusinessLayer.GetPlanOperationenArten(nID_Chirurgen, dtFrom, dtTo);

            lvTest.Items.Clear();
            lvTest.BeginUpdate();
            foreach (DataRow oPlanOperationArt in oPlanOperationenArten.Table.Rows)
            {
                string sOperation = (string)oPlanOperationArt["Operation"];

                long nPlanAnzahl = BusinessLayer.GetPlanOperationenSumme(nID_Chirurgen, sOperation, dtFrom, dtTo);
                //
                // Man darf nicht ALLE nehmen, entweder nur 1. Operateur oder einstellbar!!!
                //
                // 23.03.2010: nur Operateur zählt
                //long nIstAnzahl = BusinessLayer.GetChirurgenOperationenAnzahl(nID_Chirurgen, BusinessLayer.ID_Alle, quelle, sOperation, dtFrom, dtTo);
                int ID_OPFunktionen = ConvertToInt32(cbChirurgenOPFunktionen.SelectedValue);

                long nIstAnzahl = BusinessLayer.GetChirurgenOperationenAnzahl(nID_Chirurgen, ID_OPFunktionen, quelle, sOperation, dtFrom, dtTo);

                string data = string.Format("{0}|{1}", nIstAnzahl, nPlanAnzahl);

                ListViewItem lvi = new ListViewItem(sOperation);
                AddMatchingOpsText(lvi, sOperation);
                lvi.SubItems.Add(nIstAnzahl.ToString());
                lvi.SubItems.Add(nPlanAnzahl.ToString());
                lvi.SubItems.Add(string.Format("{0}%", ProzentFromBalkenGrafikData(data).ToString()));
                lvi.SubItems.Add(data);

                lvTest.Items.Add(lvi);
            }
            lvTest.EndUpdate();
            SetGroupBoxTextTest();
        }
        private void cmdVon_Click(object sender, EventArgs e)
        {
            Windows.Forms.CalendarPickerView dlg = new Windows.Forms.CalendarPickerView(txtDatumVon.Text);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                this.txtDatumVon.Text = Tools.DBNullableDateTime2DateString(dlg.SelectedDate);
            }
        }

        private void cmdBis_Click(object sender, EventArgs e)
        {
            Windows.Forms.CalendarPickerView dlg = new Windows.Forms.CalendarPickerView(txtDatumBis.Text);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                this.txtDatumBis.Text = Tools.DBNullableDateTime2DateString(dlg.SelectedDate);
            }
        }

        private void cbChirurgen_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nID_Chirurgen = ConvertToInt32(cbChirurgen.SelectedValue);

            if (nID_Chirurgen != -1)
            {
                PopulatePlanOperationen(nID_Chirurgen);
                lvTest.Items.Clear();
                SetGroupBoxTextTest();
            }
        }

        private void InitTest()
        {
            lvTest.Columns.Add(GetText("col_prozedur"), 90, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("col_auto"), 180, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("col_ist"), 50, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("col_soll"), 50, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("col_prozent"), 70, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("col_prozent"), -2, HorizontalAlignment.Left);

            lvTest.SetBalkenColumnIndex(ColumnIndexBalkenGrafik);

            lvTest.ShowItemToolTips = true;
        }

        protected override bool ValidateInput()
        {
            return ValidateDatumVonBis(lblDatumVon, txtDatumVon.Text, lblDatumBis, txtDatumBis.Text);
        }

        private void cmdIstSoll_Click(object sender, EventArgs e)
        {
            int nID_Chirurgen = ConvertToInt32(cbChirurgen.SelectedValue);

            if (nID_Chirurgen != -1)
            {
                if (ValidateInput())
                {
                    ProgressStart();
                    PopulateTest(nID_Chirurgen);
                    ProgressStop();
                }
            }
        }

        private void lvPlanOperationen_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem lvi = GetFirstSelectedLVI(lvPlanOperationen);
            if (lvi != null)
            {
                string datumVon = lvi.Text;
                string datumBis = lvi.SubItems[1].Text;

                txtDatumVon.Text = datumVon;
                txtDatumBis.Text = datumBis;
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lblInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OperationenLogbuchView.TheMainWindow.StartMenuItem(e.Link.LinkData);
        }

#region Print
        protected override string PrintHTMLFilter()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(GetText("operateur"));
            sb.Append(" ");
            sb.Append(cbChirurgen.Text);
            sb.Append("<br/>");

            if (_printTest)
            {
                sb.Append(GetDateVonBisHTMLFilterText(txtDatumVon.Text, txtDatumBis.Text));
                sb.Append(GetQuelleInternExternText(chkIntern.Checked, chkExtern.Checked));
            }

            return sb.ToString();
        }

        protected override string PrintHTMLGetText(ListViewItem lvi, int index)
        {
            string s = lvi.SubItems[index].Text;

            if (_printTest && (index == ColumnIndexBalkenGrafik))
            {
                s = MakeHtmlBalkenGrafik(s);
            }
            else
            {
                s = MakeHTML(s);
            }

            return s;
        }

#endregion

        private void cmdPrintTest_Click(object sender, EventArgs e)
        {
            _printTest = true;

            ContextMenuStrip cms = CreateContextMenu(ModePrintListViewBrowser | ModePrintListViewBrowserSelectedColumns);
            cms.Show(cmdPrintTest, new Point(10, 10));
        }

        protected override void PrintListViewBrowser_Click(object sender, EventArgs e)
        {
            if (_printTest)
            {
                PrintListView(lvTest, GlobalConstants.KeyPrintLinesDefaultView);
            }
            else
            {
                PrintListView(lvPlanOperationen, GlobalConstants.KeyPrintLinesDefaultView);
            }
        }
        protected override void PrintListViewBrowserSelectedColumns_Click(object sender, EventArgs e)
        {
            if (_printTest)
            {
                PrintListViewSelectedColumns(lvTest, GlobalConstants.KeyPrintLinesDefaultView);
            }
            else
            {
                PrintListViewSelectedColumns(lvPlanOperationen, GlobalConstants.KeyPrintLinesDefaultView);
            }
        }

        private void cmdPrintPlanOperationen_Click(object sender, EventArgs e)
        {
            _printTest = false;
            ContextMenuStrip cms = CreateContextMenu(ModePrintListViewBrowser | ModePrintListViewBrowserSelectedColumns);
            cms.Show(cmdPrintPlanOperationen, new Point(10, 10));
        }

        private void cmdEditPlanOperation_Click(object sender, EventArgs e)
        {
            if (UserHasRight("PlanOperationenView.edit"))
            {
                int nID_Chirurgen = ConvertToInt32(cbChirurgen.SelectedValue);
                int nID_PlanOperationen = GetFirstSelectedTag(lvPlanOperationen, true, null);

                if ((nID_Chirurgen != -1) && (nID_PlanOperationen != -1))
                {
                    DataRow row = BusinessLayer.GetPlanOperation(nID_PlanOperationen);
                    PlanOperationView dlg = new PlanOperationView(BusinessLayer, nID_Chirurgen, row);

                    if (DialogResult.Cancel != dlg.ShowDialog())
                    {
                        ProgressStart();
                        this.PopulatePlanOperationen();
                        ProgressStop();
                    }
                }
            }
        }

        private void AddMatchingOpsText(ListViewItem lvi, string opsKode)
        {
            string text = GetMatchingOpsText(opsKode);

            lvi.SubItems.Add(text);
            lvi.ToolTipText = text;
        }
    }
}

