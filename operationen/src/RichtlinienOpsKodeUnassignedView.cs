using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Globalization;

using Utility;

namespace Operationen
{
    public partial class RichtlinienOpsKodeUnassignedView : OperationenForm
    {
        private int _ID_Gebiete = -1;
        private string _opsKode = "";

        private DataView _printDataView = null;
        private bool _bPrintRichtlinien;
        private bool _bPrintRichtlinienOpsCodes;
        private bool _bPrintMissing;
        private int _lfdNr;

        public RichtlinienOpsKodeUnassignedView(BusinessLayer businessLayer)
            : this(businessLayer, -1, "")
        {
        }

        public RichtlinienOpsKodeUnassignedView(BusinessLayer businessLayer, int ID_Gebiete, string opsKode)
            : base(businessLayer)
        {
            _ID_Gebiete = ID_Gebiete;
            _opsKode = opsKode;

            ShowWaitCursorDuringPopulate = true;

            InitializeComponent();

            InitRichtlinienOpsKodes();
            InitRichtlinien();
            InitializeListViewMissing();
        }

        private void RichtlinienOpsKodeUnassignedView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));

            base._bIgnoreControlEvents = true;

            cmdPrintRichtlinien.Enabled = false;

            txtOpsKode.Enabled =
            cmdDelete.Enabled =
            cmdInsert.Enabled = UserHasRight("RichtlinienOpsKodeUnassignedView.edit");

            SetInfoText(lblInfo, string.Format(GetText("info"), lblOpsKode.Text, Command_OperationenKatalogView));
            if (UserHasRight("OperationenKatalogView.edit"))
            {
                AddLinkLabelLink(lblInfo, Command_OperationenKatalogView, Command_OperationenKatalogView);
            }

            SetInfoText(lblInfoListe, GetText("info2"));

            radSortRichtlinie.Checked = true;
            radSortKode.Checked = false;

            PopulateGebiete();

            if (_ID_Gebiete != -1)
            {
                cbGebiete.SelectedValue = _ID_Gebiete;
                txtOpsKode.Text = _opsKode;
            }

            _bIgnoreControlEvents = false;

            GebietChanged();
        }

        private void PopulateGebiete()
        {
            DataView dv = BusinessLayer.GetGebiete();

            cbGebiete.ValueMember = "ID_Gebiete";
            cbGebiete.DisplayMember = "Gebiet";
            cbGebiete.DataSource = dv;
        }

        private void InitRichtlinienOpsKodes()
        {
            lvRichtlinienOpsCodes.Clear();

            DefaultListViewProperties(lvRichtlinienOpsCodes);

            lvRichtlinienOpsCodes.Columns.Add(GetText("opscode"), 70, HorizontalAlignment.Left);
            lvRichtlinienOpsCodes.Columns.Add(GetText("opstext"), 200, HorizontalAlignment.Left);
            lvRichtlinienOpsCodes.Columns.Add(GetText("nr"), 30, HorizontalAlignment.Left);
            lvRichtlinienOpsCodes.Columns.Add(GetText("richtzahl"), 60, HorizontalAlignment.Left);
            lvRichtlinienOpsCodes.Columns.Add(GetText("methode"), -2, HorizontalAlignment.Left);
        }

        private void PopulateRichtlinienOpsKodes(int nID_Gebiete)
        {
            Cursor = Cursors.WaitCursor;
            lvRichtlinienOpsCodes.Items.Clear();
            lvRichtlinienOpsCodes.BeginUpdate();
            Application.DoEvents();

            _printDataView = BusinessLayer.GetRichtlinienOpsKodes(nID_Gebiete, radSortKode.Checked);

            if (chkRichtlinie.Checked)
            {
                FilterRichtlinienOpsKodesRichtlinie(true);
            }
            else
            {
                FilterRichtlinienOpsKodesCore(null);
            }

            cmdPrintRichtlinien.Enabled = true;
            lvRichtlinienOpsCodes.EndUpdate();
            SetGroupBoxText(lvRichtlinienOpsCodes, grpZuordnungen, GetText("grpZuordnungen"));
        }

        private void FilterRichtlinienOpsKodesRichtlinie(bool core)
        {
            ListViewItem lvi = GetFirstSelectedLVI(lvRichtlinien, false);
            if (lvi != null)
            {
                string lfdNummer = lvi.SubItems[0].Text;
                if (core)
                {
                    FilterRichtlinienOpsKodesCore(lfdNummer);
                }
                else
                {
                    FilterRichtlinienOpsKodes(lfdNummer);
                }
            }
        }

        private void FilterRichtlinienOpsKodes(string filterLfdNummer)
        {
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            lvRichtlinienOpsCodes.Items.Clear();
            lvRichtlinienOpsCodes.BeginUpdate();

            FilterRichtlinienOpsKodesCore(filterLfdNummer);

            cmdPrintRichtlinien.Enabled = true;
            lvRichtlinienOpsCodes.EndUpdate();
            SetGroupBoxText(lvRichtlinienOpsCodes, grpZuordnungen, GetText("grpZuordnungen"));

            Cursor = Cursors.Default;

        }

        private void FilterRichtlinienOpsKodesCore(string filterLfdNummer)
        {
            int nFilterLfdNummer = Convert.ToInt32(filterLfdNummer);

            foreach (DataRow dataRow in _printDataView.Table.Rows)
            {
                int lfdNummer = (int)dataRow["LfdNummer"];
                if (string.IsNullOrEmpty(filterLfdNummer) || (nFilterLfdNummer == lfdNummer))
                {
                    ListViewItem lvi = new ListViewItem((string)dataRow["OPS-Kode"]);
                    lvi.Tag = ConvertToInt32(dataRow["ID_RichtlinienOpsKodes"]);

                    if (dataRow["OPS-Text"] == DBNull.Value)
                    {
                        lvi.SubItems.Add("");
                    }
                    else
                    {
                        lvi.SubItems.Add((string)dataRow["OPS-Text"]);
                    }
                    lvi.SubItems.Add(dataRow["LfdNummer"].ToString());

                    string strRichtzahl = dataRow["Richtzahl"].ToString();
                    AddRichtzahl(lvi, strRichtzahl);

                    string s = (string)dataRow["UntBehMethode"];
                    AddRichtlinie(lvi, s, true);

                    lvRichtlinienOpsCodes.Items.Add(lvi);
                }
            }
        }

        private void InitRichtlinien()
        {
            DefaultListViewProperties(lvRichtlinien);

            lvRichtlinien.Clear();
            lvRichtlinien.Columns.Add(GetText("nr"), 50, HorizontalAlignment.Left);
            lvRichtlinien.Columns.Add(GetText("richtzahl"), 80, HorizontalAlignment.Left);
            lvRichtlinien.Columns.Add(GetText("methode"), -2, HorizontalAlignment.Left);
        }

        protected override bool ValidateInput()
        {
            bool bSuccess = true;
            string strMessage = EINGABEFEHLER;

            if (-1 == GetFirstSelectedTag(lvRichtlinien, false))
            {
                bSuccess = false;
                strMessage += GetTextControlNeedsSelection(grpRichtlinien);
            }

            if (txtOpsKode.Text.Length == 0)
            {
                bSuccess = false;
                strMessage += GetTextControlMissingText(lblOpsKode);
            }
            else
            {
                // Überprüfen, ob dieser eingegebene OPS-Kode überhaupt vorkommt: txtOpsKode
                if (BusinessLayer.OPSKodePatternExists(txtOpsKode.Text) == 0)
                {
                    bSuccess = false;
                    string text = string.Format(CultureInfo.InvariantCulture, GetText("errorOps"), 
                        lblOpsKode.Text, txtOpsKode.Text);

                    strMessage += "\n- " + text;
                }
            }

            if (!bSuccess)
            {
                MessageBox(strMessage);
            }

            return bSuccess;
        }

        private void PopulateRichtlinien(int nID_Gebiete)
        {
            DataView dv = BusinessLayer.GetRichtlinien(nID_Gebiete);

            lvRichtlinien.Items.Clear();
            lvRichtlinien.BeginUpdate();

            foreach (DataRow dataRow in dv.Table.Rows)
            {
                // LfdNummer
                ListViewItem lvi = new ListViewItem(dataRow["LfdNummer"].ToString());
                lvi.Tag = ConvertToInt32(dataRow["ID_Richtlinien"]);

                // Richtzahl: "", "50 oder "BK" (Basiskenntnisse)
                AddRichtzahl(lvi, dataRow["Richtzahl"].ToString());

                // Methode
                string s = (string)dataRow["UntBehMethode"];
                AddRichtlinie(lvi, s, true);

                lvRichtlinien.Items.Add(lvi);
            }
            lvRichtlinien.EndUpdate();

            SetGroupBoxText(lvRichtlinien, grpRichtlinien, GetText("grpRichtlinien"));
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            //
            // Den ersten ausgewähltn nehmen, dieser erhält anschließend den Fokus
            // nachdem alle gleöscht wurden
            //
            string msg = GetTextConfirmDelete(lvRichtlinienOpsCodes.SelectedItems.Count);
            if (Confirm(msg))
            {
                if (lvRichtlinienOpsCodes.SelectedItems.Count > 0)
                {
                    Cursor = Cursors.WaitCursor;

                    int nFirstSelectedIndex = lvRichtlinienOpsCodes.SelectedItems[0].Index;

                    bool deleted = false;

                    try
                    {
                        BusinessLayer.OpenDatabaseForImport();
                        foreach (ListViewItem lvi in lvRichtlinienOpsCodes.SelectedItems)
                        {
                            int nID = (int)lvi.Tag;
                            if (nID != -1)
                            {
                                if (!BusinessLayer.DeleteRichtlinienOpsKodes(nID))
                                {
                                    break;
                                }
                                deleted = true;
                            }
                        }
                    }
                    finally
                    {
                        BusinessLayer.CloseDatabaseForImport();
                    }

                    if (deleted)
                    {
                        int nID_Gebiete = ConvertToInt32(cbGebiete.SelectedValue);
                        PopulateRichtlinienOpsKodes(nID_Gebiete);

                        if (nFirstSelectedIndex < lvRichtlinienOpsCodes.Items.Count)
                        {
                            // 
                            // nFirstSelectedIndex wurde ja gelöscht, 
                            // aber der Fpcus soll in der Nähe sein.
                            // Wenn man nur einen löscht, steht man anschließen auf dem Eintrag, der
                            // dahinter war.
                            //
                            lvRichtlinienOpsCodes.SelectedIndices.Add(nFirstSelectedIndex);
                            lvRichtlinienOpsCodes.EnsureVisible(nFirstSelectedIndex);
                        }
                    }

                    Cursor = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Check that this entry does not already exist.
        /// </summary>
        /// <param name="ID_Richtlinien"></param>
        /// <param name="opsKode"></param>
        /// <returns></returns>
        private bool CheckInput(int ID_Richtlinien, string opsKode)
        {
            string msg = EINGABEFEHLER;
            bool success = true;

            if (BusinessLayer.GetRichtlinienOpsKodesCount(ID_Richtlinien, opsKode) > 0)
            {
                msg += "\n- '" + GetText("entry_exists");
                MessageBox(msg);
                success = false;
                goto _exit;
            }

            //
            // Get redundant codes in respect to this new one
            // display only the first few to not explode the message box
            //
            DataView dv = BusinessLayer.GetRichtlinienOpsKodesSubset(ID_Richtlinien, opsKode);
            if (dv.Table.Rows.Count > 0)
            {
                msg += "\n- '" + GetText("msg_subset");
                int i = 0;
                foreach (DataRow row in dv.Table.Rows)
                {
                    i++;
                    if (i > 10)
                    {
                        msg += "\n    - ...";
                        break;
                    }
                    else
                    {
                        msg += "\n    - '" + (string)row["OPSKode"] + "'";
                    }
                }
                msg += "\n\n- '" + GetText("msg_continue");
                success = false;
            }

            // Get the existing codes that would make this new one redundant
            dv = BusinessLayer.GetRichtlinienOpsKodesSuperset(ID_Richtlinien, opsKode);
            if (dv.Table.Rows.Count > 0)
            {
                msg += "\n- '" + GetText("msg_superset");
                foreach (DataRow row in dv.Table.Rows)
                {
                    msg += "\n    - '" + (string)row["OPSKode"] + "'";
                }
                msg += "\n\n- '" + GetText("msg_continue");
                success = false;
            }

            if (!success)
            {
                success = Confirm(msg);
            }

            _exit:
            return success;
        }


        /// <summary>
        /// Zuordnung OPS-Kode/Richtlinie einfügen.
        /// Der OPS-Kode, den man einfügt, soll anschließend selektiert und sichtbar sein.
        /// Es wird nach dem Neufüllen der OPSCode/LfdNummer gesucht und selektiert.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInsert_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                string lfdNummer = "";
                if (lvRichtlinien.SelectedItems.Count > 0)
                {
                    lfdNummer = lvRichtlinien.SelectedItems[0].SubItems[0].Text;
                }

                int nID_Richtlinien = GetFirstSelectedTag(lvRichtlinien, true);
                if (nID_Richtlinien != -1)
                {
                    int nID_Gebiete = ConvertToInt32(cbGebiete.SelectedValue);

                    if (CheckInput(nID_Richtlinien, txtOpsKode.Text))
                    {
                        DataRow oDataRow = BusinessLayer.CreateDataRowRichtlinienOpsKodes();
                        oDataRow["ID_Richtlinien"] = nID_Richtlinien;
                        oDataRow["OPS-Kode"] = txtOpsKode.Text;

                        Cursor = Cursors.WaitCursor;
                        BusinessLayer.InsertRichtlinienOpsKodes(oDataRow);
                        PopulateRichtlinienOpsKodes(nID_Gebiete);
                        Cursor = Cursors.Default;

                        if (lvRichtlinienOpsCodes.Items.Count > 0 && !string.IsNullOrEmpty(lfdNummer))
                        {
                            foreach (ListViewItem lvi in lvRichtlinienOpsCodes.Items)
                            {
                                if (lvi.SubItems[0].Text == txtOpsKode.Text && lvi.SubItems[2].Text == lfdNummer)
                                {
                                    lvRichtlinienOpsCodes.SelectedItems.Clear();
                                    lvRichtlinienOpsCodes.SelectedIndices.Add(lvi.Index);
                                    lvRichtlinienOpsCodes.EnsureVisible(lvi.Index);
                                    break;
                                }
                            }
                        }
                    }
                }
                txtOpsKode.Focus();
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PopulateRichtlinienOpsKodes()
        {
            if (!_bIgnoreControlEvents)
            {
                int nID_Gebiete = ConvertToInt32(cbGebiete.SelectedValue);

                Cursor = Cursors.WaitCursor;

                Application.DoEvents();

                PopulateRichtlinienOpsKodes(nID_Gebiete);

                Cursor = Cursors.Default;
            }
        }

        private string GetTextGrpMissing()
        {
            string text = string.Format(CultureInfo.InvariantCulture, GetText("grpMissing"), cbGebiete.Text);
            return text;
        }

        private string GetTextGrpRichtlinienPrint()
        {
            string text = string.Format(CultureInfo.InvariantCulture, GetText("grpRichtlinien2"), cbGebiete.Text);
            return text;
        }
        private string GetTextGrpZuordnungenPrint()
        {
            string text = string.Format(CultureInfo.InvariantCulture, GetText("grpZuordnungenPrint"), cbGebiete.Text);
            return text;
        }

        private void GebietChanged()
        {
            if (!_bIgnoreControlEvents)
            {
                int nID_Gebiete = ConvertToInt32(cbGebiete.SelectedValue);

                Cursor = Cursors.WaitCursor;

                // alle Listen leeren, damit sie alle auf einmal geleert werden und nicht langsam hintereinander.
                ClearItemsAndSetGroupBoxText(lvRichtlinienOpsCodes, grpZuordnungen, GetText("grpZuordnungen"));
                ClearItemsAndSetGroupBoxText(lvRichtlinien, grpRichtlinien, GetText("grpRichtlinien"));
                ClearItemsAndSetGroupBoxText(lvMissing, grpMissing, GetTextGrpMissing());

                Application.DoEvents();

                PopulateRichtlinienOpsKodes(nID_Gebiete);
                PopulateRichtlinien(nID_Gebiete);

                Cursor = Cursors.Default;
            }
        }

        private void cbGebiete_SelectedIndexChanged(object sender, EventArgs e)
        {
            GebietChanged();
        }


        #region Printing

        override protected void PrintAll(PrintPageEventArgs ev)
        {
            int nLine;
            string line;

            // Calculate the number of lines per page.
            int nLinesPerPage = Convert.ToInt32(ev.MarginBounds.Height / _printFont.GetHeight(ev.Graphics));

            nLine = 1;
            line = BusinessLayer.AppAndVersionStringForPrinting;
            PrintLine(ev, nLine++, line);

            line = GetTextPrintHeader(DateTime.Now, _currentPageIndex);
            PrintLine(ev, nLine++, line);

            if (_bPrintRichtlinien)
            {
                line = string.Format(CultureInfo.InvariantCulture, GetText("printFilter1"), cbGebiete.Text);
                PrintLine(ev, nLine++, line);

                nLine++;
                nLine++;

                line = GetText("printFilter2");
                PrintLine(ev, nLine++, line);

                // -1 weil irgendwie die unterste Zeile abgeschnitten war
                int nMaxIndex = _dataRowIndex + nLinesPerPage - nLine - 1;
                if (nMaxIndex >= lvRichtlinien.Items.Count)
                {
                    nMaxIndex = lvRichtlinien.Items.Count;
                }
                for (; _dataRowIndex < nMaxIndex; _dataRowIndex++)
                {
                    ListViewItem lvi = lvRichtlinien.Items[_dataRowIndex];

                    string strMethode = lvi.SubItems[2].Text;
                    strMethode = strMethode.Replace("\r\n", " ");

                    line = string.Format("{0}  {1,3}  {2}",
                        lvi.SubItems[0].Text,
                        BusinessLayer.FormatRichtzahl(lvi.SubItems[1].Text),
                        strMethode
                        );

                    PrintLine(ev, nLine++, line);
                }
                _currentPageIndex++;
                ev.HasMorePages = true;

                if (_dataRowIndex == lvRichtlinien.Items.Count)
                {
                    _bPrintRichtlinien = false;
                    _dataRowIndex = 0;
                    //
                    // Jetzt kommen zu Zuordnungen dran
                    //
                }
            }
            else if (_bPrintRichtlinienOpsCodes)
            {
                line = GetTextGrpMissing();// "Zuordnungen von OPS-Kodes zu Weiterbildungs-Richtlinien für Facharztgebiet " + cbGebiete.Text;
                PrintLine(ev, nLine++, line);

                nLine++;
                nLine++;

                line = GetText("printFilter3");
                PrintLine(ev, nLine++, line);

                // -1 weil irgendwie die unterste Zeile abgeschnitten war
                int nMaxIndex = _dataRowIndex + nLinesPerPage - nLine - 1;
                if (nMaxIndex >= _printDataView.Table.Rows.Count)
                {
                    nMaxIndex = _printDataView.Table.Rows.Count;
                }

                for (; _dataRowIndex < nMaxIndex; _dataRowIndex++)
                {
                    DataRow row = _printDataView.Table.Rows[_dataRowIndex];

                    string strOPSKode = (string)row["OPS-Kode"];
                    string strOPSText = "";
                    if (row["OPS-Text"] != DBNull.Value)
                    {
                        strOPSText = (string)row["OPS-Text"];
                    }

                    string strMethode = (string)row["UntBehMethode"];
                    strMethode = strMethode.Replace("\r\n", " ");

                    //
                    // Was heißt das G in der Formatierung?
                    //
                    line = string.Format("{0,3:G} {1} {2}  {3,2:G} {4,3:G}  {5}",
                        _lfdNr++,
                        Tools.CutString(strOPSKode + "          ", 10),
                        Tools.CutString(strOPSText + "                              ", 30),
                        row["LfdNummer"].ToString(),
                        BusinessLayer.FormatRichtzahl(row["Richtzahl"].ToString()),
                        strMethode
                        );

                    PrintLine(ev, nLine++, line);
                }
                _currentPageIndex++;
                ev.HasMorePages = _dataRowIndex < _printDataView.Table.Rows.Count;
            }
        }

        protected override void PrintForm_Click(object sender, EventArgs e)
        {
            PrintForm();
        }

        protected override void PrintListViewBrowser_Click(object sender, EventArgs e)
        {
            if (_bPrintRichtlinien)
            {
                PrintListView(lvRichtlinien, "");
            }
            else if (_bPrintRichtlinienOpsCodes)
            {
                PrintListView(lvRichtlinienOpsCodes, "");
            }
            else if (_bPrintMissing)
            {
                PrintListView(lvMissing, "");
            }
        }

        protected override void PrintListViewBrowserSelectedColumns_Click(object sender, EventArgs e)
        {
            if (_bPrintRichtlinien)
            {
                PrintListViewSelectedColumns(lvRichtlinien, "");
            }
            else if (_bPrintRichtlinienOpsCodes)
            {
                PrintListViewSelectedColumns(lvRichtlinienOpsCodes, "");
            }
            else if (_bPrintMissing)
            {
                PrintListViewSelectedColumns(lvMissing, "");
            }
        }

        private void cmdPrintRichtlinien_Click(object sender, EventArgs e)
        {
            //
            // Erst die Richtlinie drucken, sonst weiß man nicht, worum es geht.
            //
            _lfdNr = 1;
            _bPrintRichtlinienOpsCodes = false;
            _bPrintRichtlinien = true;
            _bPrintMissing = false;

            ContextMenuStrip cmsPrint = CreateContextMenu(ModePrintForm | ModePrintListViewBrowser | ModePrintListViewBrowserSelectedColumns);
            cmsPrint.Show(cmdPrintRichtlinien, new Point(10, 10));
        }

        private void cmdPrintMissing_Click(object sender, EventArgs e)
        {
            _bPrintRichtlinienOpsCodes = false;
            _bPrintRichtlinien = false;
            _bPrintMissing = true;

            ContextMenuStrip cmsPrint = CreateContextMenu(ModePrintListViewBrowser | ModePrintListViewBrowserSelectedColumns);
            cmsPrint.Show(cmdPrintMissing, new Point(10, 10));
        }

        private void cmdPrintZuordnungen_Click(object sender, EventArgs e)
        {
            _bPrintRichtlinienOpsCodes = true;
            _bPrintRichtlinien = false;
            _bPrintMissing = false;

            ContextMenuStrip cmsPrint = CreateContextMenu(ModePrintListViewBrowser | ModePrintListViewBrowserSelectedColumns);
            cmsPrint.Show(cmdPrintZuordnungen, new Point(10, 10));
        }

        protected override string PrintHTMLHeaderLine1()
        {
            string text = "";

            if (_bPrintRichtlinien)
            {
                text = GetTextGrpRichtlinienPrint();
            }
            else if (_bPrintRichtlinienOpsCodes)
            {
                text = GetTextGrpZuordnungenPrint();
            }
            else if (_bPrintMissing)
            {
                text = GetTextGrpMissing();
            }

            return MakeSafeHTML(text);
        }

        #endregion   


        private void ResetSort()
        {
            //
            // Wenn man auf eine Spalte klickt, erscheint ein kleines Dreick, das den
            // Sortorder anzeigt.
            // Wenn man dann auf eine RadioButton für eine andere Sortierung klickt
            // muss man die Sortiert erst ganz entfernen sonst bleibt das Dreieck und die
            // zuletzt angeklickte Sortierung
            //
            lvRichtlinienOpsCodes.Sorting = SortOrder.None;
            lvRichtlinienOpsCodes.Sortable = false;

            lvRichtlinienOpsCodes.Sortable = true;
            lvRichtlinienOpsCodes.Sorting = SortOrder.Ascending;
        }

        private void radSortKode_CheckedChanged(object sender, EventArgs e)
        {
            if (radSortKode.Checked)
            {
                ResetSort();
                PopulateRichtlinienOpsKodes();
            }
        }

        private void radSortRichtlinie_CheckedChanged(object sender, EventArgs e)
        {
            if (radSortRichtlinie.Checked)
            {
                ResetSort();
                PopulateRichtlinienOpsKodes();
            }
        }

        private void lblInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OperationenLogbuchView.TheMainWindow.StartMenuItem(e.Link.LinkData);
        }

        private void PopulateMissingRichtlinien()
        {
            Cursor = Cursors.WaitCursor;

            Application.DoEvents();

            int nID_Gebiete = ConvertToInt32(cbGebiete.SelectedValue);

            DateTime? dtFrom = GetDateTimeFromTextBox(txtDatumVon);
            DateTime? dtTo = GetDateTimeFromTextBox(txtDatumBis);

            lvMissing.Items.Clear();
            Application.DoEvents();

            DataView dv = BusinessLayer.GetMissingRichtlinienOPsAlle(nID_Gebiete, BusinessLayer.OperationQuelleAlle, dtFrom, dtTo);

            lvMissing.BeginUpdate();
            foreach (DataRow dataRow in dv.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem((string)dataRow["OPSKode"]);
                lvi.SubItems.Add((string)dataRow["OPSText"]);
                lvMissing.Items.Add(lvi);
            }
            lvMissing.EndUpdate();

            SetGroupBoxText(lvMissing, grpMissing, GetTextGrpMissing());

            Cursor = Cursors.Default;
        }

        private void InitializeListViewMissing()
        {
            DefaultListViewProperties(lvMissing);

            lvMissing.Columns.Add(GetText("opscode"), 80, HorizontalAlignment.Left);
            lvMissing.Columns.Add(GetText("opstext"), -2, HorizontalAlignment.Left);
        }

        private void cmdMissing_Click(object sender, EventArgs e)
        {
            PopulateMissingRichtlinien();
        }

        private void lvMissing_DoubleClick(object sender, EventArgs e)
        {
            string s = lvMissing.SelectedItems[0].SubItems[0].Text;
            if (s.Length > 5)
            {
                s = s.Substring(0, 5);
            }
            txtOpsKode.Text = s;
        }

        private void chkRichtlinie_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRichtlinie.Checked)
            {
                FilterRichtlinienOpsKodesRichtlinie(false);
            }
            else
            {
                FilterRichtlinienOpsKodes(null);
            }
        }

        private void lvRichtlinien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkRichtlinie.Checked)
            {
                FilterRichtlinienOpsKodesRichtlinie(false);
            }
        }

    }
}

