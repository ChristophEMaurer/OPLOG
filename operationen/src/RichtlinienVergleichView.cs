using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;
using System.Diagnostics;
using System.Globalization;

using Windows.Forms;
using Utility;
using Operationen.Weiterbildungzeitraum;
using CMaurer.Operationen.AppFramework;

namespace Operationen
{
    public partial class RichtlinienVergleichView : OperationenForm
    {
        private Dictionary<int, DateTime?> _gebieteVon = new Dictionary<int, DateTime?>();
        private Dictionary<int, DateTime?> _gebieteBis = new Dictionary<int, DateTime?>();
        private List<DateTime> _wbzr = null;
        private DataView _richtlinien = null;
        private bool _printTest;

        public RichtlinienVergleichView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();

            lvRichtlinien.OnHScroll += new OplListView.HScrollDelegate(lvRichtlinien_OnHScroll);
            cmdAssignOPSRichtlinie.SetSecurity(BusinessLayer, "RichtlinienVergleichView.cmdAssignOPSRichtlinie");
            cmdAssignRichtlinie.SetSecurity(BusinessLayer, "RichtlinienVergleichView.cmdAssignRichtlinie");

            CheckQuelleDefaultSettings(chkIntern, chkExtern);
        }

        void BusinessLayer_Progress(ProgressEventArgs e)
        {
            Application.DoEvents();
            if (Abort)
            {
                e.Cancel = true;
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

        private void InitRichtlinien()
        {
            DefaultListViewProperties(lvRichtlinien);

            lvRichtlinien.Clear();
            lvRichtlinien.Columns.Add(GetText("nr"), 30, HorizontalAlignment.Left);
            lvRichtlinien.Columns.Add(GetText("richtzahl"), 60, HorizontalAlignment.Left);
            lvRichtlinien.Columns.Add(GetText("opscode"), 80, HorizontalAlignment.Left);
            lvRichtlinien.Columns.Add(GetText("bezeichnung"), 200, HorizontalAlignment.Left);
            lvRichtlinien.Columns.Add(GetText("datum"), 70, HorizontalAlignment.Left);
            lvRichtlinien.Columns.Add(GetText("opscode"), 80, HorizontalAlignment.Left);
            lvRichtlinien.Columns.Add(GetText("auto"), -2, HorizontalAlignment.Left);
        }

        DateTime MakeEndDate(List<DateTime> wbzr, int index)
        {
            //
            // 0    1   2   3
            // x1   x2  x3  x4
            //
            // 0: x1 bis x2-1
            // 1: x2 bis x3-1
            // 2: x3 bis x4
            //
            DateTime endDate;

            if (index < wbzr.Count - 2)
            {
                endDate = wbzr[index + 1].AddDays(-1);
            }
            else
            {
                endDate = wbzr[index + 1];
            }

            return endDate;
        }

        private void InitTest(List<DateTime> wbzr)
        {
            lvTest.Clear();
            lvTest.View = View.Details;
            lvTest.FullRowSelect = true;
            lvTest.MultiSelect = false;
            SetWatermark(lvTest);

            if (wbzr != null && wbzr.Count > 0)
            {
                //
                // 0    1        N-1
                // Ist1 Ist2 ... IstN Summe Richtzahl   Nr  Methode
                //

                for (int i = 0; i < wbzr.Count - 1; i++)
                {
                    DateTime dtFrom = wbzr[i];
                    DateTime dtTo = MakeEndDate(wbzr, i);
                    string header = string.Format(CultureInfo.InvariantCulture, "{0}-{1}",
                        Tools.DateTime2DateStringMMYY(dtFrom),
                        Tools.DateTime2DateStringMMYY(dtTo));

                    lvTest.Columns.Add(header, 100, HorizontalAlignment.Left);
                }
                lvTest.Columns.Add(GetText("summe"), 50, HorizontalAlignment.Left);
            }
            else
            {
                //
                // Ist  Richtzahl   Nr  Methode
                //
                lvTest.Columns.Add(GetText("ist"), 50, HorizontalAlignment.Left);
            }
            lvTest.Columns.Add(GetText("richtzahl"), 80, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("nr"), 50, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("methode"), -2, HorizontalAlignment.Left);
        }

        private void PopulateMissingRichtlinien(int nID_Gebiete, int nID_Chirurgen, int quelle, DateTime? dtVon, DateTime? dtBis)
        {
            lvMissing.Items.Clear();
            Application.DoEvents();
            
            DataView dv = BusinessLayer.GetMissingRichtlinienOPs(nID_Gebiete, nID_Chirurgen, quelle, dtVon, dtBis);

            lvMissing.BeginUpdate();
            int i = 0;
            foreach (DataRow dataRow in dv.Table.Rows)
            {
                i++;
                if (i % BusinessLayer.AbortLoopCount == 0)
                {
                    Application.DoEvents();
                }
                if (Abort)
                {
                    break;
                }

                ListViewItem lvi = new ListViewItem(Tools.DBNullableDateTime2DateString(dataRow["Datum"]));
                lvi.Tag = ConvertToInt32(dataRow["ID_ChirurgenOperationen"]);
                lvi.SubItems.Add((string)dataRow["OPSKode"]);
                lvi.SubItems.Add((string)dataRow["OPSText"]);
                lvMissing.Items.Add(lvi);
            }
            lvMissing.EndUpdate();

            SetGroupBoxText(lvMissing, grpMissing, GetText("grpMissing"));
        }

        private void InitializeListViewMissing()
        {
            DefaultListViewProperties(lvMissing);

            lvMissing.Columns.Add(GetText("datum"), 70, HorizontalAlignment.Left);
            lvMissing.Columns.Add(GetText("opscode"), 80, HorizontalAlignment.Left);
            lvMissing.Columns.Add(GetText("opstext"), -2, HorizontalAlignment.Left);
        }

        private void PopulateRichtlinien(int nID_Gebiete, int nID_Chirurgen, int quelle, DateTime? dtVon, DateTime? dtBis, bool bAll)
        {
            // dv sind die, die automatisch zugeordnet sind
            DataView dvAuto = BusinessLayer.GetRichtlinienOPs(nID_Gebiete, nID_Chirurgen, quelle, dtVon, dtBis, bAll);
            
            // dvManuell sind die, bei denen für eine Richtlinie manuell ein Wert eingegeben wurde
            DataView dvManuell = BusinessLayer.GetChirurgenRichtlinien(nID_Chirurgen, nID_Gebiete, dtVon, dtBis);
            if (!bAll)
            {
                BusinessLayer.AuswertungenGruppiereAnzahl(dvManuell);
            }

            // automatischen Zuordnungen
            BusinessLayer.AuswertungenMergeRichtzahlen(dvAuto, dvManuell, bAll);

            lvRichtlinien.Items.Clear();
            lvRichtlinien.BeginUpdate();

            int count = 0;
            foreach (DataRow dataRow in dvAuto.Table.Rows)
            {
                count++;
                if (count % BusinessLayer.AbortLoopCount == 0)
                {
                    Application.DoEvents();
                }
                if (Abort)
                {
                    break;
                }

                ListViewItem lvi = new ListViewItem(dataRow["LfdNummer"].ToString());

                // Richtzahl: "", "50 oder "BK" (Basiskenntnisse)
                AddRichtzahl(lvi, dataRow["Richtzahl"].ToString());

                lvi.SubItems.Add((string)dataRow["RichtlinieOPSKode"]);

                // Methode
                AddRichtlinie(lvi, (string)dataRow["UntBehMethode"], true);

                if (bAll)
                {
                    lvi.SubItems.Add(Tools.DBNullableDateTime2DateString(dataRow["Datum"]));
                }
                else
                {
                    lvi.SubItems.Add("");
                }

                string s = "";
                if (dataRow["OPSKode"] != DBNull.Value)
                {
                    s = (string)dataRow["OPSKode"];
                }
                lvi.SubItems.Add(s);

                s = "";
                if (dataRow["OPSText"] != DBNull.Value)
                {
                    s = (string)dataRow["OPSText"];
                }
                lvi.SubItems.Add(s);

                lvRichtlinien.Items.Add(lvi);
            }
            lvRichtlinien.EndUpdate();
            SetGroupBoxText(lvRichtlinien, grpRichtlinien, GetText("grpRichtlinien"));
        }

        private void RichtlinienVergleichView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));

            cmdAssignOPSRichtlinie.Text = GetText("cmdAssignOPSRichtlinie");
            cmdAssignRichtlinie.Text = GetText("cmdAssignRichtlinie");

            _bIgnoreControlEvents = true;

            cmdPrintTest.Enabled = false;
            cmdPrintBDC.Enabled = false;

            cmdStop.Enabled = false;
            cmdStop2.Enabled = false;

            PopulateChirurgen(cbChirurgen);
            PopulateGebiete();

            InitRichtlinien();
            InitTest(null);
            InitializeListViewMissing();

            _bIgnoreControlEvents = false;

            GetChirurgenGebiete();
            PopulateDatumVonBis();

            int ID_Gebiete = ConvertToInt32(cbGebiete.SelectedValue);
            _richtlinien = BusinessLayer.GetRichtlinien(ID_Gebiete);

            ColumnResized(3);
        }

        private void GetChirurgenGebiete()
        {
            int ID_Chirurgen = ConvertToInt32(cbChirurgen.SelectedValue);

            DataView dv = BusinessLayer.GetChirurgenGebiete(ID_Chirurgen);

            _gebieteVon.Clear();
            _gebieteBis.Clear();

            foreach (DataRow row in dv.Table.Rows)
            {
                DateTime? von = (row["GebietVon"] == DBNull.Value) ? null : (DateTime?)row["GebietVon"];
                DateTime? bis = (row["GebietBis"] == DBNull.Value) ? null : (DateTime?)row["GebietBis"];

                _gebieteVon[ConvertToInt32(row["ID_Gebiete"])] = von;
                _gebieteBis[ConvertToInt32(row["ID_Gebiete"])] = bis;
            }
        }

        private void PopulateGebiete()
        {
            DataView dv = BusinessLayer.GetGebiete();

            cbGebiete.ValueMember = "ID_Gebiete";
            cbGebiete.DisplayMember = "Gebiet";
            cbGebiete.DataSource = dv;
        }



        private void PopulateTest(int nID_Gebiete, int nID_Chirurgen, int quelle, DateTime? dtVon, DateTime? dtBis)
        {
            try
            {
                BusinessLayer.Progress += new ProgressCallback(BusinessLayer_Progress);

                InitTest(null);

                if (chkAlleRichtlinien.Checked)
                {
                    //
                    // Nut 'Ist' Spalte
                    //
                    AddRichtlinienToTest(1);
                }

                //
                // Die automatische zugeordneten und die Operationen, die fest dieser Richtlinie zugeordnet sind
                // Sortiert nach LfdNummer
                //
                DataView dataView = BusinessLayer.GetRichtlinienOPSummen(nID_Gebiete, nID_Chirurgen, quelle, dtVon, dtBis);

                //
                // Die vorab erfassten erfüllten Richtzahlen
                // Sortiert nach Datum
                //
                DataView dvManuell = BusinessLayer.GetChirurgenRichtlinienSummen(nID_Chirurgen, nID_Gebiete, dtVon, dtBis);

                //
                // Manuelle Werte müssen zusammengefügt werden mit den automatisch zugeordneten
                //
                BusinessLayer.AuswertungenMergeIstZahlen(dataView, dvManuell);

                lvTest.BeginUpdate();
                int count = 0;
                foreach (DataRow row in dataView.Table.Rows)
                {
                    count++;
                    if (count % BusinessLayer.AbortLoopCount == 0)
                    {
                        Application.DoEvents();
                    }
                    if (Abort)
                    {
                        break;
                    }

                    AddToTest(row, 0, 1, false);
                }
                lvTest.EndUpdate();

                cmdPrintTest.Enabled = true;
                cmdPrintBDC.Enabled = true;
            }
            finally
            {
                BusinessLayer.Progress -= new ProgressCallback(BusinessLayer_Progress);
            }
        }

        /// <summary>
        /// Add to an existing row or create a new row
        /// </summary>
        /// <param name="row">Daten</param>
        /// <param name="index">0- based index</param>
        /// <param name="numZeitraeume">Anzahl der Zeitraeume</param>
        private void AddToTest(DataRow row, int index, int numZeitraeume, bool istZeitraum)
        {
            //
            //  0         1         2         3
            //  Ist       Richtzahl Nr        Methode
            //  Zeitraum1 Zeitraum2 Zeitraum3 Summe Richtzahl Nr Methode
            //
            ListViewItem lvi = null;
            bool found = false;
            string anzahl = string.Format(CultureInfo.InvariantCulture, "{0,3}", Convert.ToInt64(row["Anzahl"]));

            //
            // Laufende Nummer suchen
            //
            string rowLfdNummer = row["LfdNummer"].ToString();

            foreach (ListViewItem x in lvTest.Items)
            {
                int colLfdNummer = istZeitraum ? (numZeitraeume + 2) : 2;

                string lviLfdNummer = x.SubItems[colLfdNummer].Text.Trim();
                if (lviLfdNummer.Equals(rowLfdNummer))
                {
                    //
                    // Wenn man vorher alle Richtlinien einfügt, kommt man immer hierher
                    //
                    found = true;
                    lvi = x;
                    break;
                }
            }

            if (!found)
            {
                //
                // Wenn man vorher alle Richtlinien einfügt, kommt man nie hierher
                //

                //
                // Nicht gefunden: neue Zeile einfügen
                //

                //
                // numZeitraeume Spalten erzeugen
                // und Wert in Spalte index eintragen
                //
                lvi = new ListViewItem("");
                for (int i = 1; i < numZeitraeume; i++)
                {
                    lvi.SubItems.Add("");
                }

                if (istZeitraum)
                {
                    // Summe
                    lvi.SubItems.Add("");
                }
                lvi.SubItems.Add(BusinessLayer.FormatRichtzahl(row["Richtzahl"].ToString()));
                lvi.SubItems.Add(string.Format("{0,2}", ConvertToInt32(row["LfdNummer"])));
                AddRichtlinie(lvi, (string)row["UntBehMethode"], true);

                lvTest.Items.Add(lvi);
            }

            lvi.SubItems[index].Text = anzahl;
        }

        private void CalculateSumme(int numZeitraeume)
        {
            foreach (ListViewItem lvi in lvTest.Items)
            {
                int summe = 0;

                for (int i = 0; i < numZeitraeume; i++)
                {
                    string text = lvi.SubItems[i].Text.Trim();
                    if (text.Length > 0)
                    {
                        summe += Tools.ConvertToInt32(text);
                    }
                }
                lvi.SubItems[numZeitraeume].Text = string.Format("{0,3}", summe);
            }
        }

        private void AddRichtlinienToTest(int numColumns)
        {
            DataView dv = _richtlinien;

            if (dv != null)
            {
                foreach (DataRow row in dv.Table.Rows)
                {
                    //
                    // numColumns Spalten erzeugen
                    //
                    ListViewItem lvi = new ListViewItem();
                    for (int i = 1; i < numColumns; i++)
                    {
                        lvi.SubItems.Add("");
                    }

                    lvi.SubItems.Add(BusinessLayer.FormatRichtzahl(row["Richtzahl"].ToString()));
                    lvi.SubItems.Add(string.Format("{0,2}", ConvertToInt32(row["LfdNummer"])));
                    AddRichtlinie(lvi, (string)row["UntBehMethode"], true);

                    lvTest.Items.Add(lvi);
                }
            }
        }

        private void PopulateTest(int nID_Gebiete, int nID_Chirurgen, int quelle, List<DateTime> wbzr)
        {
            InitTest(wbzr);

            if (chkAlleRichtlinien.Checked)
            {
                //
                // wbzr.Count-1 Zeitraeume und 'Summe'
                //
                AddRichtlinienToTest(wbzr.Count);
            }

            for (int i = 0; i < wbzr.Count - 1; i++)
            {
                Application.DoEvents();
                if (Abort)
                {
                    break;
                }

                DateTime dtFrom = wbzr[i];
                DateTime dtTo = MakeEndDate(wbzr, i);

                //
                // Die automatische zugeordneten und die Operationen, die fest dieser Richtlinie zugeordnet sind
                //
                DataView dataView = BusinessLayer.GetRichtlinienOPSummen(
                    nID_Gebiete, nID_Chirurgen, quelle, dtFrom, dtTo);

                //
                // Die vorab erfassten erfüllten Richtzahlen
                //
                DataView dvManuell = BusinessLayer.GetChirurgenRichtlinienSummen(nID_Chirurgen, nID_Gebiete, dtFrom, dtTo);

                //
                // Manuelle Werte müssen zusammengefügt werden mit den automatisch zugeordneten
                //
                BusinessLayer.AuswertungenMergeIstZahlen(dataView, dvManuell);

                foreach (DataRow row in dataView.Table.Rows)
                {
                    AddToTest(row, i, wbzr.Count - 1, true);
                }
            }
            if (wbzr != null && wbzr.Count > 0)
            {
                CalculateSumme(wbzr.Count - 1);
            }

            cmdPrintTest.Enabled = true;
            cmdPrintBDC.Enabled = true;
        }

        private void cmdVergleich_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                //Cursor = Cursors.WaitCursor;
                XableAllButtonsForLongOperation(cmdStop, cmdStop2, false);

                DateTime? dtFrom = null;
                DateTime? dtTo = null;

                CheckQuelleCorrectDumbSettings(chkIntern, chkExtern);
                int nID_Gebiete = ConvertToInt32(cbGebiete.SelectedValue);
                int nID_Chirurgen = ConvertToInt32(cbChirurgen.SelectedValue);
                int quelle = BusinessLayer.GetInternExternFlag(chkIntern.Checked, chkExtern.Checked);

                if (chkZeitraum.Checked)
                {
                    _wbzr = BusinessLayer.GetWeiterbildungsZeitraeume(nID_Chirurgen);
                    if (_wbzr.Count > 0)
                    {
                        dtFrom = _wbzr[0];
                        dtTo = _wbzr[_wbzr.Count - 1];
                    }
                    PopulateRichtlinien(nID_Gebiete, nID_Chirurgen, quelle, dtFrom, dtTo, chkDate.Checked);
                    PopulateTest(nID_Gebiete, nID_Chirurgen, quelle, _wbzr);
                }
                else
                {
                    if (txtDatumVon.Text.Length > 0)
                    {
                        dtFrom = Tools.InputTextDate2DateTime(txtDatumVon.Text);
                    }
                    if (txtDatumBis.Text.Length > 0)
                    {
                        dtTo = Tools.InputTextDate2DateTimeEnd(txtDatumBis.Text);
                    }

                    _wbzr = null;
                    PopulateRichtlinien(nID_Gebiete, nID_Chirurgen, quelle, dtFrom, dtTo, chkDate.Checked);
                    PopulateTest(nID_Gebiete, nID_Chirurgen, quelle, dtFrom, dtTo);
                }

                XableAllButtonsForLongOperation(cmdStop, cmdStop2, true);
                //Cursor = Cursors.Default;
            }
        }

        protected override bool ValidateInput()
        {
            bool bSuccess = true;
            string strMsg = EINGABEFEHLER;

            if (txtDatumVon.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtDatumVon.Text))
            {
                bSuccess = false;
                strMsg += GetTextControlInvalidDate(lblDatumVon);
            }
            if (txtDatumBis.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtDatumBis.Text))
            {
                bSuccess = false;
                strMsg += GetTextControlInvalidDate(lblDatumBis);
            }

            if (!bSuccess)
            {
                MessageBox(strMsg);
            }

            return bSuccess;
        }

        private void cmdDateVon_Click(object sender, EventArgs e)
        {
            Windows.Forms.CalendarPickerView dlg = new Windows.Forms.CalendarPickerView(txtDatumVon.Text);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                txtDatumVon.Text = Tools.DBNullableDateTime2DateString(dlg.SelectedDate);
            }
        }

        private void cmdDateBis_Click(object sender, EventArgs e)
        {
            Windows.Forms.CalendarPickerView dlg = new Windows.Forms.CalendarPickerView(txtDatumBis.Text);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                txtDatumBis.Text = Tools.DBNullableDateTime2DateString(dlg.SelectedDate);
            }
        }

        private void ChirurgChanged()
        {
            if (!_bIgnoreControlEvents)
            {
                GetChirurgenGebiete();
                PopulateDatumVonBis();
            }
        }

        private void PopulateDatumVonBis()
        {
            // gemerkte Werte zu diesem Gebiet holen und in die Textboxen stellen
            int ID_Gebiete = ConvertToInt32(cbGebiete.SelectedValue);

            if (_gebieteVon.ContainsKey(ID_Gebiete))
            {
                txtDatumVon.Text = Tools.NullableDateTime2DateString(_gebieteVon[ID_Gebiete]);
            }
            else
            {
                txtDatumVon.Text = "";
            }

            if (_gebieteBis.ContainsKey(ID_Gebiete))
            {
                txtDatumBis.Text = Tools.NullableDateTime2DateString(_gebieteBis[ID_Gebiete]);
            }
            else
            {
                txtDatumBis.Text = "";
            }
        }

        private void cbGebiete_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_bIgnoreControlEvents)
            {
                PopulateDatumVonBis();
                int ID_Gebiete = ConvertToInt32(cbGebiete.SelectedValue);
                _richtlinien = BusinessLayer.GetRichtlinien(ID_Gebiete);
            }
        }

        private void cbChirurgen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_bIgnoreControlEvents)
            {
                ChirurgChanged();
            }
        }

        private void cmdAssignRichtlinie_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = GetFirstSelectedLVI(lvMissing, true);

            if (lvi != null)
            {
                int nID_Gebiete = ConvertToInt32(this.cbGebiete.SelectedValue);

                RichtlinieSelectView dlg = new RichtlinieSelectView(BusinessLayer, nID_Gebiete);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    if (dlg.ID_Richtlinien != -1)
                    {
                        foreach (int i in lvMissing.SelectedIndices)
                        {
                            int nID_ChirurgenOperationen = (int)lvMissing.Items[i].Tag;

                            DataRow dataRow = BusinessLayer.GetChirurgenOperationenRecord(nID_ChirurgenOperationen);
                            dataRow["ID_Richtlinien"] = dlg.ID_Richtlinien;
                            BusinessLayer.UpdateChirurgenOperationenRichtlinie(dataRow);
                        }
                    }
                }
            }
        }

        private void cmdAssignOPSRichtlinie_Click(object sender, EventArgs e)
        {
            int nID_Gebiete = ConvertToInt32(this.cbGebiete.SelectedValue);
            ListViewItem lvi = GetFirstSelectedLVI(lvMissing, true);

            if (lvi != null)
            {
                string opsKode = lvi.SubItems[1].Text;

                RichtlinienOpsKodeUnassignedView dlg = new RichtlinienOpsKodeUnassignedView(BusinessLayer, nID_Gebiete, opsKode);
                dlg.ShowDialog();
            }
        }

        private void cmdPrintTest_Click(object sender, EventArgs e)
        {
            _printTest = true;
            ContextMenuStrip cmsPrint = CreateContextMenu(ModePrintForm | ModePrintListViewBrowser | ModePrintListViewBrowserBdc | ModePrintListViewBrowserSelectedColumns);
            cmsPrint.Show(cmdPrintTest, new Point(10, 10));
        }

        protected override string PrintHTMLFilter()
        {
            StringBuilder sb = new StringBuilder();

            if (chkZeitraum.Checked)
            {
                sb.Append(MakeSafeHTML(GetText("mehrereZeitraeume")));
                sb.Append("<br/>");
            }
            else
            {
                sb.Append(GetDateVonBisHTMLFilterText(txtDatumVon.Text, txtDatumBis.Text));
            }
            sb.Append(GetQuelleInternExternText(chkIntern.Checked, chkExtern.Checked));
            sb.Append("<br/>");
            sb.Append(MakeSafeHTML(GetText("operateur") + ": '"  + cbChirurgen.Text) + "'");
            sb.Append("<br/>");
            sb.Append(MakeSafeHTML(GetText("gebiet") + ": '" + cbGebiete.Text) + "'");

            return sb.ToString();
        }

        override protected void PrintForm_Click(object sender, EventArgs e)
        {
            PrintForm();
        }
        override protected void PrintListViewBrowser_Click(object sender, EventArgs e)
        {
            if (_printTest)
            {
                PrintListView(lvTest, GlobalConstants.KeyPrintLinesDefaultString);
            }
            else
            {
                PrintListView(lvMissing, GlobalConstants.KeyPrintLinesDefaultString);
            }
        }
        override protected void PrintListViewBrowserSelectedColumns_Click(object sender, EventArgs e)
        {
            if (_printTest)
            {
                PrintListViewSelectedColumns(lvTest, GlobalConstants.KeyPrintLinesDefaultString);
            }
            else
            {
                PrintListViewSelectedColumns(lvMissing, GlobalConstants.KeyPrintLinesDefaultString);
            }
        }
        override protected void PrintListViewBrowserBdc_Click(object sender, EventArgs e)
        {
            CreateBDCIstSoll();
        }

        #region Printing

        override protected void PrintAll(PrintPageEventArgs ev)
        {
            int nLine;
            string line;

            // Calculate the number of lines per page.
            int nLinesPerPage = ConvertToInt32(ev.MarginBounds.Height / _printFont.GetHeight(ev.Graphics));

            nLine = 1;
            line = BusinessLayer.AppAndVersionStringForPrinting;
            PrintLine(ev, nLine++, line);

            line = GetTextPrintHeader(DateTime.Now, _currentPageIndex);
            PrintLine(ev, nLine++, line);

            line = GetText("printFilter1");
            PrintLine(ev, nLine++, line);

            line = GetText("operateur") + " " + cbChirurgen.Text;
            PrintLine(ev, nLine++, line);

            line = "";
            if (_wbzr == null || _wbzr.Count == 0)
            {
                line += GetTextPrintFromTo(txtDatumVon.Text, txtDatumBis.Text);
                if (line.Length > 0)
                {
                    PrintLine(ev, nLine++, line);
                }
            }
            else
            {
                line += GetTextPrintFromTo(Tools.DateTime2DateString(_wbzr[0]), Tools.DateTime2DateString(_wbzr[_wbzr.Count - 1]));
                if (line.Length > 0)
                {
                    PrintLine(ev, nLine++, line);
                }
            }

            nLine++;

            if (_wbzr == null || _wbzr.Count == 0)
            {
                line = GetText("printFilter2");
            }
            else
            {
                for (int i = 0; i < _wbzr.Count - 1; i++)
                {
                    line = string.Format("{0,2}: ", i + 1);
                    line = line + Tools.DateTime2DateString(_wbzr[i]);
                    line = line + "-";
                    DateTime end = MakeEndDate(_wbzr, i);
                    line = line + Tools.DateTime2DateString(end);
                    PrintLine(ev, nLine++, line);
                }
                nLine++;
                line = "";
                for (int i = 0; i < _wbzr.Count - 1; i++)
                {
                    line = line + string.Format("{0,4}  ", i + 1);
                }
                line = line + GetText("printFilter3");
            }
            PrintLine(ev, nLine++, line);
            nLine++;

            // -1 weil irgendwie die unterste Zeile abgeschnitten war
            int nMaxIndex = _dataRowIndex + nLinesPerPage - nLine - 1;
            if (nMaxIndex >= lvTest.Items.Count)
            {
                nMaxIndex = lvTest.Items.Count;
            }

            for (; _dataRowIndex < nMaxIndex; _dataRowIndex++)
            {
                ListViewItem lvi = lvTest.Items[_dataRowIndex];

                string strMethode = lvi.SubItems[lvi.SubItems.Count - 1].Text;// (string)row["UntBehMethode"];
                strMethode = strMethode.Replace("\r\n", " ");

                if (_wbzr == null || _wbzr.Count == 0)
                {
                    line = string.Format("{0,4}  {1} {2,3}  {3}",
                          lvi.SubItems[0].Text,
                          lvi.SubItems[1].Text,
                          lvi.SubItems[2].Text,
                          strMethode
                        );
                }
                else
                {
                    line = "";
                    for (int i = 0; i < _wbzr.Count - 1; i++)
                    {
                        line = line + string.Format("{0,4}  ", lvi.SubItems[i].Text);
                    }
                    line = line + string.Format(" {0,4} {1} {2,3}  {3}",
                      lvi.SubItems[_wbzr.Count - 1].Text,
                      lvi.SubItems[_wbzr.Count].Text,
                      lvi.SubItems[_wbzr.Count + 1].Text,
                      strMethode
                    );
                }

                PrintLine(ev, nLine++, line);
            }

            _currentPageIndex++;
            ev.HasMorePages = _dataRowIndex < lvTest.Items.Count;
        }

        #endregion

        /// <summary>
        /// Erzeugt eine HTML-Datei mit allen Richtlinien des ausgewählten Gebietes und füllt die berechneten Ist-Zahlen ein,
        /// wenn es zu einer Richtlinien solche Zahlen gibt.
        /// Diesen Ausdruck gibt es nur auf Deutsch!!!
        /// </summary>
        private void CreateBDCIstSoll()
        {
            string bk = "<p>BK = Basiskenntnisse. Bei diesen Eingriffen ist keine Mindesteingriffzahl festgelegt.</p>";

            StringBuilder header = new StringBuilder(string.Format(@"
                            <div>
                                <span style=""font-size:1.2em;"">WEITERBILDUNGSINHALTE {0}</span>
                                <br />
                                <br />
                                <span style=""font-size:1.5em; color:#9ed8d2""><b>Untersuchungen und Eingriffe</b></span>
                                <br />
                                <br />
                                <table width=""100%"" border=""1"" >
                                  
                                  <tr>
                                        <td bgcolor=""#003768""><font color=""white""><b>Untersuchungs- und Behandlungsmethoden</b></font></td>
                                        <td bgcolor=""#003768"" align=""center""><font color=""white""><b>Richtzahl</b></font></td>"
                                  , cbGebiete.Text.ToUpper()));

            string dateFormat = @"<td bgcolor=""#003768""><font color=""white""><b>Stand f&uuml;r den Zeitraum {0}</b></font></td>";
            if (_wbzr == null || _wbzr.Count == 0)
            {
                string datum = "(";
                if (txtDatumVon.Text.Length == 0)
                {
                    datum += "...&nbsp;bis&nbsp;";
                }
                else
                {
                    datum += txtDatumVon.Text + "&nbsp;bis&nbsp;";
                }
                if (txtDatumBis.Text.Length == 0)
                {
                    datum += "...";
                }
                else
                {
                    datum += txtDatumBis.Text;
                }
                datum += ")";
                header.Append(string.Format(dateFormat, datum));
            }
            else
            {
                for (int i = 0; i < _wbzr.Count - 1; i++)
                {
                    string datum = "(";
                    datum += Tools.DateTime2DateString(_wbzr[i]) + "-" + Tools.DateTime2DateString(MakeEndDate(_wbzr, i));
                    datum += ")";
                    header.Append(string.Format(dateFormat, datum));
                    header.Append(Environment.NewLine);
                }
            }
            header.Append("</tr>");

#if DEBUG
            string fileName = "d:\\bdc.html";
#else
            string fileName = Path.GetTempFileName() + ".html";
#endif

            DataView dv = _richtlinien;

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("<p>Drucken Sie diese Seite(n) hochkant mit der Druckfunktion Ihres Internet Browsers aus."
                        + " Die Anzahl der Zeilen pro Seite k&ouml;nnen Sie unter 'Extras > Optionen' einstellen.</p>");

                writer.WriteLine(string.Format(@"<html><head><title>{0} - Untersuchungen und Eingriffe</title></head><body style=""font-family:arial; font-size:0.9em;"">", AppTitle()));
                writer.WriteLine(header);

                int lines = 0;
                string strLinesPerPage = BusinessLayer.GetUserSettingsString(GlobalConstants.SectionPrint, GlobalConstants.KeyPrintLinesBDCWeiterbildung);
                int linesPerPage = 0;
                Int32.TryParse(strLinesPerPage, out linesPerPage);
                if (linesPerPage <= 0)
                {
                    linesPerPage = GlobalConstants.KeyPrintLinesDefault;
                }

                foreach (DataRow richtlinie in dv.Table.Rows)
                {
                    lines++;
                    if (lines > linesPerPage)
                    {
                        // Neue Seite anfangen
                        writer.WriteLine("</table></div>");
                        writer.WriteLine(bk);
                        writer.WriteLine("<p style=\"page-break-before:always\">" + header);
                        lines = 1;
                    }

                    // Richtzahl: -1 BK - Basiskenntnis, 0 = Überschrift, sonst: echte Richtzahl
                    if (ConvertToInt32(richtlinie["Richtzahl"]) == 0)
                    {
                        // Überschrift
                        int colspan;

                        if (_wbzr == null || _wbzr.Count == 0)
                        {
                            colspan = 3;
                        }
                        else
                        {
                            colspan = _wbzr.Count + 1;
                        }
                        writer.WriteLine(string.Format(CultureInfo.InvariantCulture, 
                                    @"<tr>
                                        <td bgcolor=""D4DAE8"" colspan=""{0}""><b>" + MakeHTML((string)richtlinie["UntBehMethode"]) + @"</b></td>
                                      </tr>", colspan));
                    }
                    else
                    {
                        // UntBehMethode mit Richtzahl oder Basiskenntnis
                        writer.Write(@"
                                    <tr>
                                        <td>" + MakeHTML((string)richtlinie["UntBehMethode"]) + @"</td>
                                        <td>" + BusinessLayer.FormatRichtzahl(richtlinie["Richtzahl"].ToString()).Trim() + @"</td>
                        ");

                        bool found = false;
                        foreach (ListViewItem lvi in lvTest.Items)
                        {
                            if (_wbzr == null || _wbzr.Count == 0)
                            {
                                if (Tools.ConvertToInt32(lvi.SubItems[2].Text) == ConvertToInt32(richtlinie["LfdNummer"]))
                                {
                                    writer.WriteLine("<td>" + HtmlEnsureNonBreakableSpace(lvi.SubItems[0].Text) + "</td>");
                                    found = true;
                                    break;
                                }
                            }
                            else
                            {
                                if (Tools.ConvertToInt32(lvi.SubItems[_wbzr.Count + 1].Text) == ConvertToInt32(richtlinie["LfdNummer"]))
                                {
                                    for (int i = 0; i < _wbzr.Count - 1; i++)
                                    {
                                        writer.WriteLine("<td>" + HtmlEnsureNonBreakableSpace(lvi.SubItems[i].Text) + "</td>");
                                    }
                                    found = true;
                                    break;
                                }
                            }
                        }
                        if (!found)
                        {
                            if (_wbzr == null || _wbzr.Count == 0)
                            {
                                writer.WriteLine("<td>&nbsp;</td>");
                            }
                            else
                            {
                                for (int i = 0; i < _wbzr.Count - 1; i++)
                                {
                                    writer.WriteLine("<td>&nbsp;</td>");
                                }
                            }
                        }
                        writer.WriteLine("</tr>");
                    }
                }

                writer.WriteLine("</table></div>");
                writer.WriteLine(bk);
                writer.WriteLine("</body></html>");
            }

            PrintHTMLLaunchFile(fileName);
        }


        private void cmdPrintBDC_Click(object sender, EventArgs e)
        {
            CreateBDCIstSoll();
        }

        private void cmdMissing_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                XableAllButtonsForLongOperation(cmdStop, cmdStop2, false);

                DateTime? dtFrom = null;
                DateTime? dtTo = null;

                CheckQuelleCorrectDumbSettings(chkIntern, chkExtern);
                int nID_Gebiete = ConvertToInt32(cbGebiete.SelectedValue);
                int nID_Chirurgen = ConvertToInt32(cbChirurgen.SelectedValue);
                int quelle = GetInternExternFlag(chkIntern.Checked, chkExtern.Checked);

                GetVonBisDatum(txtDatumVon, txtDatumBis, out dtFrom, out dtTo);

                PopulateMissingRichtlinien(nID_Gebiete, nID_Chirurgen, quelle, dtFrom, dtTo);

                XableAllButtonsForLongOperation(cmdStop, cmdStop2, true);
            }
        }

        private void cmdZeitraum_Click(object sender, EventArgs e)
        {
            ArrayList args = new ArrayList();
            int nID_Chirurgen = ConvertToInt32(cbChirurgen.SelectedValue);
            args.Add(nID_Chirurgen);

            OperationenLogbuchView.TheMainWindow.OpenWindowWithAdditionalParameters(typeof(WeiterbildungszeitraumView), args);
        }

        private void cmdStop_Click(object sender, EventArgs e)
        {
            Abort = true;
        }

        private void chkZeitraum_CheckedChanged(object sender, EventArgs e)
        {
            txtDatumVon.Enabled = !chkZeitraum.Checked;
            txtDatumBis.Enabled = !chkZeitraum.Checked;
        }

        int GetWidth(ListView listView, int columnIndexFrom, int columnIndexTo)
        {
            int totalWidth = 0;

            for (int i = columnIndexFrom; i <= columnIndexTo; i++)
            {
                totalWidth += listView.Columns[i].Width;
            }

            return totalWidth;
        }

        private void ColumnResized(int columnIndex)
        {
            if (!_bIgnoreControlEvents && (columnIndex <= 3))
            {
                int x = lvRichtlinien.HScrollPos;
                int newWidth = GetWidth(lvRichtlinien, 0, 3) - x;

                if ((newWidth <= 10) || (lvRichtlinien.Width - 10 <= newWidth))
                {
                    if (lblProzedur.Visible)
                    {
                        lblProzedur.Visible = lblRichtlinie.Visible = false;
                    }
                }
                else
                {
                    if (!lblProzedur.Visible)
                    {
                        lblProzedur.Visible = lblRichtlinie.Visible = true;
                    }
                    lblRichtlinie.Width = newWidth - 2;
                    lblProzedur.Left = lblRichtlinie.Right + 4;
                    lblProzedur.Width = lvRichtlinien.Width - newWidth - 2;
                }
            }
        }
        private void lvRichtlinien_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            ColumnResized(e.ColumnIndex);
        }

        private void lvRichtlinien_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            ColumnResized(e.ColumnIndex);
        }

        void lvRichtlinien_OnHScroll(ref Message m)
        {
            ColumnResized(3);
        }

        private void cmdPrintMissing_Click(object sender, EventArgs e)
        {
            _printTest = false;
            ContextMenuStrip cms = CreateContextMenu(ModePrintListViewBrowser | ModePrintListViewBrowserSelectedColumns);
            cms.Show(cmdPrintMissing, new Point(10, 10));
        }
    }
}

