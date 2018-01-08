using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Diagnostics;

using Windows.Forms;
using Utility;
using Operationen.Weiterbildungzeitraum;
using CMaurer.Operationen.AppFramework;

namespace Operationen
{
    public partial class RichtlinienSollIstView : OperationenForm
    {
        /*
         * lvRichtlinien
         * 
         * Zeilen: lvi.Tag = ID_Richtlinien oder -1 bei der ersten Zeile
         *          * 
         * Der Listview enthält immer zwei Spalten für jeden Zeitraum
         * 
         * Die linke Spalte enthält den Soll Wert, .tag = ID_GebieteSoll
         * Die rechte Spalte enthält den Ist Wert, .tag = ID_GebieteSoll
         */

        private int _clickedColumnIndex = -1;
        private bool _printTest;

        private int _zuordnungenColumnIndex = -1;
        private int _zuordnungenRowIndex = -1;

        public RichtlinienSollIstView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();

            cmdAdd.Text = GetText("cmdAdd");
            cmdUpdate.Text = GetText("cmdUpdate");
            cmdDelete.Text = GetText("cmdDelete");
            contextMenuColumn.Items[0].Text = GetText("cmdDelete");

            cmdAdd.SetSecurity(BusinessLayer, "RichtlinienSollIstView.cmdAdd");
            cmdUpdate.SetSecurity(BusinessLayer, "RichtlinienSollIstView.cmdUpdate");
            cmdDelete.SetSecurity(BusinessLayer, "RichtlinienSollIstView.cmdDelete");

            DefaultListViewProperties(lvTest);
            lvTest.Clear();
            lvTest.Columns.Add(GetText("nr"), 30, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("wbrlnr"), 80, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("richtzahl"), 60, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("opscode"), 80, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("bezeichnung"), 200, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("datum"), 70, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("opscode"), 80, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("auto"), -2, HorizontalAlignment.Left);

            DefaultListViewProperties(lvRichtlinien);
            lvRichtlinien.SubItemClicked += new OplListView.SubItemEventHandler(lvRichtlinien_SubItemClicked);
            lvRichtlinien.SubItemEndEditing += new OplListView.SubItemEndEditingEventHandler(lvRichtlinien_SubItemEndEditing);

            txtTextBox.Visible = false;

            CheckQuelleDefaultSettings(chkIntern, chkExtern);
        }

        void lvRichtlinien_SubItemEndEditing(object sender, OplListView.SubItemEndEditingEventArgs e)
        {
            int soll;

            if (Int32.TryParse(e.DisplayText, out soll))
            {
                ListViewItem lvi = e.Item;

                int ID_GebieteSoll = (int)lvRichtlinien.Columns[e.SubItem].Tag;
                int ID_Richtlinie = (int) lvi.Tag;

                BusinessLayer.SetRichtlinienSoll(ID_GebieteSoll, ID_Richtlinie, soll);
            }
            else
            {
                e.DisplayText = "";
            }
        }

        void lvRichtlinien_SubItemClicked(object sender, OplListView.SubItemEventArgs e)
        {
            if (IsSollIstColumn(e.SubItem))
            {
                if (e.SubItem % 2 == 1)
                {
                    //
                    // Ungerader Index ist Index der Soll Spalte, nur die kann man editieren
                    //
                    lvRichtlinien.StartEditing(txtTextBox, e.Item, e.SubItem);
                }
                else
                {
                    //
                    // Ist-Spalte
                    //
                    int columnIndex = e.SubItem;
                    int rowIndex = e.Item.Index;

                    //
                    // Das wird für einmal Klicken öfters aufgerufen, daher nur einmal die Daten holen
                    //
                    if ((columnIndex != _zuordnungenColumnIndex) || (rowIndex != _zuordnungenRowIndex))
                    {
                        _zuordnungenRowIndex = rowIndex;
                        _zuordnungenColumnIndex = columnIndex;
                        lvTest.Items.Clear();
                        if (!string.IsNullOrEmpty(e.Item.SubItems[e.SubItem].Text))
                        {
                            PopulateZuordnungen(columnIndex, rowIndex);
                        }
                    }
                }
            }
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

        private void PopulateRichtlinien()
        {
            Abort = false;

            XableAllButtonsForLongOperation(cmdStop, false);

            lvTest.Items.Clear();
            CheckQuelleCorrectDumbSettings(chkIntern, chkExtern);
            int nID_Gebiete = ConvertToInt32(cbGebiete.SelectedValue);
            int nID_Chirurgen = ConvertToInt32(cbChirurgen.SelectedValue);

            PopulateRichtlinien(nID_Gebiete, nID_Chirurgen);

            XableAllButtonsForLongOperation(cmdStop, true);
        }

        private void PopulateRichtlinien(int nID_Gebiete, int nID_Chirurgen)
        {
            try
            {
                BusinessLayer.Progress += new ProgressCallback(BusinessLayer_Progress);

                lvRichtlinien.Clear();
                DefaultListViewProperties(lvRichtlinien);
                lvRichtlinien.InPlaceEditing = true;

                //
                // Erst nur drei Spalten mit den Richtlinien erzeugen
                // Spalten von einer Richtlinie
                //
                lvRichtlinien.Columns.Add(GetText("nr"), 40, HorizontalAlignment.Left);
                lvRichtlinien.Columns.Add(GetText("richtzahl"), 60, HorizontalAlignment.Left);
                lvRichtlinien.Columns.Add(GetText("bezeichnung"), -2, HorizontalAlignment.Left);

                lvRichtlinien.BeginUpdate();

                //
                // Zeilen füllen
                //

                DataView richtlinien = BusinessLayer.GetRichtlinien(nID_Gebiete);
                foreach (DataRow richtlinie in richtlinien.Table.Rows)
                {
                    int ID_Richtlinien = ConvertToInt32(richtlinie["ID_Richtlinien"]);

                    int lfdNummer = ConvertToInt32(richtlinie["LfdNummer"]);

                    ListViewItem lvi = new ListViewItem(lfdNummer.ToString());
                    lvi.Tag = ID_Richtlinien;

                    // Richtzahl: "", "50 oder "BK" (Basiskenntnisse)
                    AddRichtzahl(lvi, richtlinie["Richtzahl"].ToString());
                    AddRichtlinie(lvi, (string)richtlinie["UntBehMethode"], true);

                    lvRichtlinien.Items.Add(lvi);
                }

                //
                // Zusätzliche Spalten erzeugen, immer zwei mit Soll und Ist
                //

                //
                // Erst alle Spalten erzeugen, das sieht sonst doof aus
                // Absteigend sortiert nach Von-Datum
                //
                DataView gebieteSoll = BusinessLayer.GetGebieteSoll(nID_Chirurgen, nID_Gebiete);
                foreach (DataRow gebietSoll in gebieteSoll.Table.Rows)
                {
                    Application.DoEvents();
                    InsertColumn(gebietSoll);
                }

                lvRichtlinien.EndUpdate();

                //
                // Wenn alle Spalten da sind, die Ist-Zahlen berechnen und einsetzen
                //
                // Anfangen bei der hintersten Ist-Spalte
                //
                int index = lvRichtlinien.Columns.Count - 5;
                foreach (DataRow gebietSoll in gebieteSoll.Table.Rows)
                {
                    Application.DoEvents();
                    if (Abort)
                    {
                        break;
                    }

                    CalculateIst(index, gebietSoll);
                    index -= 2;
                }

                //
                // Soll-Zahlen
                //
                CalculateSoll();

                SetGroupBoxText(lvRichtlinien, grpRichtlinien, GetText("grpRichtlinien"));
            }
            finally
            {
                BusinessLayer.Progress -= new ProgressCallback(BusinessLayer_Progress);
            }
        }

        private void RichtlinienVergleichView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));

            string info = string.Format(CultureInfo.InvariantCulture, GetText("info"),
               cmdAdd.Text, cmdUpdate.Text, contextMenuColumn.Items[0].Text);

            SetInfoText(lblInfo, info);

            cmdStop.Enabled = false;

            _bIgnoreControlEvents = true;

            PopulateChirurgen(cbChirurgen);
            PopulateGebiete();

            _bIgnoreControlEvents = false;
        }

        private void PopulateGebiete()
        {
            DataView dv = BusinessLayer.GetGebiete();

            cbGebiete.ValueMember = "ID_Gebiete";
            cbGebiete.DisplayMember = "Gebiet";
            cbGebiete.DataSource = dv;
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

        #region Printing

        private void cmdPrintTest_Click(object sender, EventArgs e)
        {
            _printTest = true;
            ContextMenuStrip cmsPrint = CreateContextMenu(ModePrintListViewBrowser | ModePrintListViewBrowserSelectedColumns);
            cmsPrint.Show(cmdPrintTest, new Point(10, 10));
        }

        protected override string PrintHTMLFilter()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(GetDateVonBisHTMLFilterText(txtDatumVon.Text, txtDatumBis.Text));
            sb.Append(GetQuelleInternExternText(chkIntern.Checked, chkExtern.Checked));
            sb.Append("<br/>");
            sb.Append(MakeSafeHTML(GetText("operateur") + ": '" + cbChirurgen.Text) + "'");
            sb.Append("<br/>");
            sb.Append(MakeSafeHTML(GetText("gebiet") + ": '" + cbGebiete.Text) + "'");

            sb.Append("<br/>");
            if (_printTest)
            {
                sb.Append(MakeSafeHTML(grpTest.Text));
                ListViewItem lvi = GetFirstSelectedLVI(lvRichtlinien);
                if (lvi != null)
                {
                    string temp = " " + GetText("wbrl")
                        + ": " + lvi.SubItems[lvRichtlinien.Columns.Count - 2].Text.Trim()
                        + " - '" + lvi.SubItems[lvRichtlinien.Columns.Count - 1].Text + "'";

                    sb.Append(MakeSafeHTML(temp));
                }
            }
            else
            {
                sb.Append(MakeSafeHTML(grpRichtlinien.Text));
            }

            return sb.ToString();
        }

        override protected void PrintListViewBrowser_Click(object sender, EventArgs e)
        {
            if (_printTest)
            {
                PrintListView(lvTest, GlobalConstants.KeyPrintLinesDefaultString);
            }
            else
            {
                PrintListView(lvRichtlinien, GlobalConstants.KeyPrintLinesDefaultString);
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
                PrintListViewSelectedColumns(lvRichtlinien, GlobalConstants.KeyPrintLinesDefaultString);
            }
        }

        #endregion

        private void cmdStop_Click(object sender, EventArgs e)
        {
            Abort = true;
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (UserHasRight("RichtlinienSollIstView.cmdAdd"))
            {
                InsertColumn();
            }
        }

        private void InsertColumn()
        {
            if ((lvRichtlinien.Columns.Count > 0)
                && ValidateDatumVonBis(lblDatumVon, txtDatumVon.Text, lblDatumBis, txtDatumBis.Text, false))
            {
                DateTime? dtVon = null;
                DateTime? dtBis = null;

                GetVonBisDatum(txtDatumVon, txtDatumBis, out dtVon, out dtBis, false);

                int nID_Gebiete = ConvertToInt32(cbGebiete.SelectedValue);
                int nID_Chirurgen = ConvertToInt32(cbChirurgen.SelectedValue);

                DataRow row = BusinessLayer.CreateDataRowGebieteSoll();
                row["ID_Chirurgen"] = nID_Chirurgen;
                row["ID_Gebiete"] = nID_Gebiete;
                row["Von"] = dtVon.Value;
                row["Bis"] = dtBis.Value;

                int id = BusinessLayer.InsertGebieteSoll(row);

                //
                // Man könnte auch row["ID_GebieteSoll"] = id;
                //
                // machen, aber bei allen Datenbanken???
                //
                row["ID_GebieteSoll"] = id;
                InsertColumn(row);
                CalculateIst(0, row);
                CalculateSoll(0);
            }
        }

        private void InsertColumn(DataRow gebietSoll)
        {
            _clickedColumnIndex = -1;

            int ID_GebieteSoll = ConvertToInt32(gebietSoll["ID_GebieteSoll"]);
            DateTime dtVon = (DateTime)gebietSoll["Von"];
            DateTime dtBis = (DateTime)gebietSoll["Bis"];

            string von = Tools.DBDateTime2DateString(dtVon);
            string bis = Tools.DBDateTime2DateString(dtBis);

            //
            // Bis-Spalte
            //
            lvRichtlinien.Columns.Insert(0, MakeColumnText(bis, false), 100, HorizontalAlignment.Right);
            lvRichtlinien.Columns[0].Tag = ID_GebieteSoll;

            //
            // Von-Spalte
            //
            lvRichtlinien.Columns.Insert(0, MakeColumnText(von, true), 100, HorizontalAlignment.Right);
            lvRichtlinien.Columns[0].Tag = ID_GebieteSoll;

            foreach (ListViewItem lvi in lvRichtlinien.Items)
            {
                //
                // Keine break hier, sonst sind die Spalten kaputt!!
                //
                Application.DoEvents();

                ListViewItem.ListViewSubItem lvsi;
                int ID_Richtlinien = (int)lvi.Tag;

                lvsi = new ListViewItem.ListViewSubItem(lvi, "");
                lvi.SubItems.Insert(0, lvsi);
                lvsi = new ListViewItem.ListViewSubItem(lvi, "");
                lvi.SubItems.Insert(0, lvsi);
            }
        }

        private void CalculateSoll(int columnIndex)
        {
            int ID_GebieteSoll = (int)lvRichtlinien.Columns[columnIndex].Tag;

            foreach (ListViewItem lvi in lvRichtlinien.Items)
            {
                Application.DoEvents();
                if (Abort)
                {
                    break;
                }

                int ID_Richtlinien = (int)lvi.Tag;

                DataRow richtlinieSoll = BusinessLayer.GetRichtlinienSoll(ID_GebieteSoll, ID_Richtlinien);
                if (richtlinieSoll != null)
                {
                    lvi.SubItems[columnIndex].Text = richtlinieSoll["Soll"].ToString();
                }
            }
        }

        /// <summary>
        /// Soll-Zahlen holen und einsetzen
        /// </summary>
        private void CalculateSoll()
        {
            for (int i = 1; i < lvRichtlinien.Columns.Count - 3; i += 2)
            {
                Application.DoEvents();
                if (Abort)
                {
                    break;
                }
                CalculateSoll(i);
            }
        }

        private void cmdVergleich_Click(object sender, EventArgs e)
        {
            PopulateRichtlinien();
        }

        private bool IsSollIstColumn(int index)
        {
            return (lvRichtlinien.Columns.Count > 2) && (index < lvRichtlinien.Columns.Count - 3);
        }

        /// <summary>
        /// Merken, auf welche Spalte man geklickt hatte.
        /// Außerdem das Datum der beiden Spalten in die Datum-Textfelder stellen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvRichtlinien_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ColumnClickEventArgs ev = (ColumnClickEventArgs)e;
            _clickedColumnIndex = ev.Column;

            int indexIst = _clickedColumnIndex / 2 * 2;
            if ((lvRichtlinien.Columns.Count > 3) && (_clickedColumnIndex < lvRichtlinien.Columns.Count - 3))
            {
                int ID_GebieteSoll = (int)lvRichtlinien.Columns[indexIst].Tag;
                DataRow gebieteSoll = BusinessLayer.GetGebieteSoll(ID_GebieteSoll);
                if (gebieteSoll != null)
                {
                    txtDatumVon.Text = Tools.DBDateTime2DateString(gebieteSoll["von"]);
                    txtDatumBis.Text = Tools.DBDateTime2DateString(gebieteSoll["bis"]);
                }
            }

            if (IsSollIstColumn(_clickedColumnIndex))
            {
                //
                // Das Menü darf nur erscheinen, wenn nicht gerade gerechnet wird
                //
                if (MayFormClose)
                {
                    //
                    // Man hat auf eine Spalte geklickt, die Ist/Soll Werte enthält. das sind alle Spalten vor den letzten drei Spalten.
                    //
                    int left = 0;
                    for (int i = 0; i < _clickedColumnIndex; i++)
                    {
                        left += lvRichtlinien.Columns[i].Width;
                    }
                    contextMenuColumn.Items[0].Enabled = UserHasRight("RichtlinienSollIstView.cmdDelete");
                    contextMenuColumn.Show(lvRichtlinien, new Point(left, 20));
                }
            }
        }

        /// <summary>
        /// Remove one column
        /// </summary>
        /// <param name="index">0-based index into the columns</param>
        private void RemoveColumn(int index)
        {
            _clickedColumnIndex = -1;

            int ID_GebieteSoll = (int)lvRichtlinien.Columns[index].Tag;

            BusinessLayer.DeleteGebieteSoll(ID_GebieteSoll);

            if (index % 2 == 0)
            {
                //
                // Gerade: die linke der beiden angeklickt: diese und die rechts davon löschen
                //
                lvRichtlinien.Columns.RemoveAt(index);
                lvRichtlinien.Columns.RemoveAt(index);
            }
            else
            {
                //
                // Gerade: die rechte der beiden angeklickt; diese und die links davon löschen
                //
                lvRichtlinien.Columns.RemoveAt(index);
                lvRichtlinien.Columns.RemoveAt(index - 1);
            }

            foreach (ListViewItem lvi in lvRichtlinien.Items)
            {
                if (index % 2 == 0)
                {
                    lvi.SubItems.RemoveAt(index);
                    lvi.SubItems.RemoveAt(index);
                }
                else
                {
                    lvi.SubItems.RemoveAt(index);
                    lvi.SubItems.RemoveAt(index - 1);
                }
            }
        }

        private void entfernenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UserHasRight("RichtlinienSollIstView.cmdDelete"))
            {
                RemoveColumn(_clickedColumnIndex);
            }
        }

        private void abbrechenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // nix tun
        }

        private void SetIstValue(DataRow row, int index)
        {
            //
            //  0       1       2       3       4       5           6
            //  Soll    Ist     Soll    Ist     Nr      Richtzahl   Methode 
            //
            string anzahl = string.Format(CultureInfo.InvariantCulture, "{0,3}", Convert.ToInt64(row["Anzahl"]));

            //
            // Laufende Nummer suchen
            //
            string rowLfdNummer = row["LfdNummer"].ToString();
            int colLfdNummer = lvRichtlinien.Columns.Count - 3;

            foreach (ListViewItem lvi in lvRichtlinien.Items)
            {
                Application.DoEvents();
                if (Abort)
                {
                    break;
                }

                int ID_Richtlinien = (int)lvi.Tag;
                if (ID_Richtlinien != -1)
                {
                    string lviLfdNummer = lvi.SubItems[colLfdNummer].Text.Trim();
                    if (lviLfdNummer.Equals(rowLfdNummer))
                    {
                        //
                        // Wenn man vorher alle Richtlinien einfügt, kommt man immer hierher
                        //
                        lvi.SubItems[index].Text = anzahl;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Berechne die Ist-Zahlen für einen Zeitraum
        /// </summary>
        /// <param name="index">Der Index der linken der beiden Spalten: links=Ist, rechts=Soll</param>
        private void CalculateIst(int index, DataRow gebieteSoll)
        {
            CheckQuelleCorrectDumbSettings(chkIntern, chkExtern);
            int nID_Gebiete = ConvertToInt32(cbGebiete.SelectedValue);
            int nID_Chirurgen = ConvertToInt32(cbChirurgen.SelectedValue);
            int quelle = BusinessLayer.GetInternExternFlag(chkIntern.Checked, chkExtern.Checked);

            int ID_GebieteSoll = ConvertToInt32(gebieteSoll["ID_GebieteSoll"]);
            DateTime dtVon = (DateTime)gebieteSoll["Von"];
            DateTime dtBis = (DateTime)gebieteSoll["Bis"];

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

            lvRichtlinien.BeginUpdate();
            foreach (DataRow row in dataView.Table.Rows)
            {
                Application.DoEvents();
                if (Abort)
                {
                    break;
                }
                SetIstValue(row, index);
            }
            lvRichtlinien.EndUpdate();
        }

        private string MakeColumnText(string date, bool istColumn)
        {
            string text;

            if (istColumn)
            {
                text = GetText("ist") + ": " + date;
            }
            else
            {
                text = GetText("soll") + ": " + date;
            }

            return text;
        }

        private void UpdateDate()
        {
            if (((_clickedColumnIndex >= 0) && (_clickedColumnIndex < lvRichtlinien.Columns.Count - 3))
                && ValidateDatumVonBis(lblDatumVon, txtDatumVon.Text, lblDatumBis, txtDatumBis.Text, false))
            {
                DateTime? dtVon = null;
                DateTime? dtBis = null;

                GetVonBisDatum(txtDatumVon, txtDatumBis, out dtVon, out dtBis, false);

                int ID_GebieteSoll = (int)lvRichtlinien.Columns[_clickedColumnIndex].Tag;

                DataRow row = BusinessLayer.GetGebieteSoll(ID_GebieteSoll);
                row["Von"] = dtVon;
                row["Bis"] = dtBis;

                BusinessLayer.UpdateGebieteSoll(row);
                
                int indexIst = _clickedColumnIndex / 2 * 2;
                lvRichtlinien.Columns[indexIst].Text = MakeColumnText(txtDatumVon.Text, true);
                lvRichtlinien.Columns[indexIst + 1].Text = MakeColumnText(txtDatumBis.Text, false);
            }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (UserHasRight("RichtlinienSollIstView.cmdUpdate"))
            {
                UpdateDate();
            }
        }

        private void PopulateZuordnungen(int columnIndex, int rowIndex)
        {
            Abort = false;
            int ID_GebieteSoll = (int)lvRichtlinien.Columns[columnIndex].Tag;
            DataRow gebieteSoll = BusinessLayer.GetGebieteSoll(ID_GebieteSoll);

            DateTime? dtVon = (DateTime)gebieteSoll["Von"];
            DateTime? dtBis = (DateTime)gebieteSoll["Bis"];

            CheckQuelleCorrectDumbSettings(chkIntern, chkExtern);
            int nID_Gebiete = ConvertToInt32(cbGebiete.SelectedValue);
            int nID_Chirurgen = ConvertToInt32(cbChirurgen.SelectedValue);
            int quelle = BusinessLayer.GetInternExternFlag(chkIntern.Checked, chkExtern.Checked);

            XableAllButtonsForLongOperation(cmdStop, false);
            PopulateZuordnungen(nID_Gebiete, nID_Chirurgen, quelle, dtVon, dtBis, columnIndex, rowIndex);
            XableAllButtonsForLongOperation(cmdStop, true);

        }

        private void PopulateZuordnungen(int nID_Gebiete, int nID_Chirurgen, int quelle, DateTime? dtVon, DateTime? dtBis, int columnIndex, int rowIndex)
        {
            // dvAuto sind die, die automatisch zugeordnet werden
            DataView dvAuto = BusinessLayer.GetRichtlinienOPs(nID_Gebiete, nID_Chirurgen, quelle, dtVon, dtBis, true);

            // dvManuell sind die, bei denen für eine Richtlinie manuell ein Wert eingegeben wurde
            DataView dvManuell = BusinessLayer.GetChirurgenRichtlinien(nID_Chirurgen, nID_Gebiete, dtVon, dtBis);

            // automatischen Zuordnungen
            BusinessLayer.AuswertungenMergeRichtzahlen(dvAuto, dvManuell, true);

            lvTest.Items.Clear();
            lvTest.BeginUpdate();

            int count = 1;
            foreach (DataRow dataRow in dvAuto.Table.Rows)
            {
                Application.DoEvents();
                if (Abort)
                {
                    break;
                }

                string rowLfdNummer = dataRow["LfdNummer"].ToString();
                string lviLfdNummer = lvRichtlinien.Items[rowIndex].SubItems[lvRichtlinien.Columns.Count - 3].Text.Trim();

                if (lviLfdNummer.Equals(rowLfdNummer))
                {
                    ListViewItem lvi = new ListViewItem(count.ToString(CultureInfo.InvariantCulture));
                    lvi.SubItems.Add(dataRow["LfdNummer"].ToString());

                    // Richtzahl: "", "50 oder "BK" (Basiskenntnisse)
                    AddRichtzahl(lvi, dataRow["Richtzahl"].ToString());

                    lvi.SubItems.Add((string)dataRow["RichtlinieOPSKode"]);

                    // Methode
                    AddRichtlinie(lvi, (string)dataRow["UntBehMethode"], true);

                    lvi.SubItems.Add(Tools.DBNullableDateTime2DateString(dataRow["Datum"]));

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

                    lvTest.Items.Add(lvi);
                    count++;
                }
            }
            lvTest.EndUpdate();
            SetGroupBoxText(lvTest, grpTest, GetText("grpTest"));
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            _printTest = false;
            ContextMenuStrip cmsPrint = CreateContextMenu(ModePrintListViewBrowser | ModePrintListViewBrowserSelectedColumns);
            cmsPrint.Show(cmdPrint, new Point(10, 10));
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
             if ((_clickedColumnIndex >= 0) && (_clickedColumnIndex < lvRichtlinien.Columns.Count - 3))
             {
                 RemoveColumn(_clickedColumnIndex);
             }
        }

        private void cbChirurgen_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvRichtlinien.Clear();
        }

        private void cbGebiete_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvRichtlinien.Clear();
        }

        private void grpFilter_Enter(object sender, EventArgs e)
        {

        }

        private void lvRichtlinien_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

