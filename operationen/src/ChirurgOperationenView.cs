using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Globalization;

using Utility;
using Windows.Forms;
using Operationen;
using CMaurer.Operationen.AppFramework;

namespace Operationen
{
    public partial class ChirurgOperationenView : OperationenForm
    {
        /// <summary>
        /// Enthält die Datümer für die Gebiete des aktuellen Chirurgen
        /// </summary>
        private Dictionary<int, DateTime?> _gebieteVon = new Dictionary<int, DateTime?>();
        private Dictionary<int, DateTime?> _gebieteBis = new Dictionary<int, DateTime?>();

        /// <summary>
        /// Enthält den aktuell ausgewählten Chirurg
        /// </summary>
        private DataRow _oChirurg;

        private OperationenLogbuchView mainWindow;
        private bool _printOperationen;

        public ChirurgOperationenView(BusinessLayer businessLayer, ArrayList args)
            : base(businessLayer)
        {
            mainWindow = (OperationenLogbuchView)args[0];

            InitializeComponent();

            cmdNew.SetSecurity(BusinessLayer, "ChirurgOperationenView.cmdNew");
            cmdDelete.SetSecurity(BusinessLayer, "ChirurgOperationenView.cmdDelete");
            cmdAssignRichtlinie.SetSecurity(BusinessLayer, "ChirurgOperationenView.cmdAssignRichtlinie");
            cmdRemoveRichtlinie.SetSecurity(BusinessLayer, "ChirurgOperationenView.cmdRemoveRichtlinie");
        }

        private void PopulateGebiete()
        {
            DataView dv = BusinessLayer.GetGebiete();

            cbGebiete.ValueMember = "ID_Gebiete";
            cbGebiete.DisplayMember = "Gebiet";
            cbGebiete.DataSource = dv;
        }

        private void ChirurgOperationenViewNeu_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));

            cmdAssignRichtlinie.Text = GetText("cmdAssignRichtlinie");
            cmdDelete.Text = GetText("cmdDelete");
            cmdNew.Text = GetText("cmdNew");
            cmdRemoveRichtlinie.Text = GetText("cmdRemoveRichtlinie");

            SetInfoText(lblInfoSummary, string.Format(CultureInfo.InvariantCulture, GetText("info_summary"), Command_OptionsView));

            _bIgnoreControlEvents = true;

            PopulateChirurgen(cbChirurgen);
            PopulateOPFunktionen();
            PopulateGebiete();

            InitializeListViewOperationen();

            CheckQuelleDefaultSettings(chkIntern, chkExtern);

            _bIgnoreControlEvents = false;

            // Aktuell ausgewählten _oChirurg holen.
            ChirurgChanged();
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

        private void PopulateOPFunktionen()
        {
            DataView dv = BusinessLayer.GetOPFunktionen(true);

            cbOPFunktionen.ValueMember = "ID_OPFunktionen";
            cbOPFunktionen.DisplayMember = "Beschreibung";
            cbOPFunktionen.DataSource = dv;
            cbOPFunktionen.SelectedValue = (int)OP_FUNCTION.OP_FUNCTION_OP;
        }

        private void FillListViewOverview(int quelle)
        {
            int nChirugen = ConvertToInt32(_oChirurg["ID_Chirurgen"]);
            int nID_OPFunktionen = ConvertToInt32(cbOPFunktionen.SelectedValue);

            DateTime? von = null;
            DateTime? bis = null;

            GetVonBisDatum(txtDatumVon, txtDatumBis, out von, out bis);

            DataView dataview = BusinessLayer.GetChirurgOperationenOverview(nChirugen, nID_OPFunktionen, quelle, von, bis);

            lvOverview.Clear();

            lvOverview.BeginUpdate();
            DefaultListViewProperties(lvOverview);

            lvOverview.Columns.Add(GetText("Anzahl"), 150, HorizontalAlignment.Left);
            lvOverview.Columns.Add(GetText("OPSKode"), 80, HorizontalAlignment.Left);
            lvOverview.Columns.Add(GetText("Bezeichnung"), -2, HorizontalAlignment.Left);

            long summe = 0;
            foreach (DataRow dataRow in dataview.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem(dataRow["Anzahl"].ToString());
                lvi.ToolTipText = "[" + (string)dataRow["OPSKode"] + "] " + (string)dataRow["OPSText"];
                lvi.SubItems.Add((string)dataRow["OPSKode"]);
                lvi.SubItems.Add((string)dataRow["OPSText"]);

                lvOverview.Items.Add(lvi);

                summe += Convert.ToInt64(dataRow["Anzahl"]);
            }
            lvOverview.EndUpdate();

            txtSumme.Text = summe.ToString();
        }

        private void InitializeListViewOperationen()
        {
            DefaultListViewProperties(lvOperationen);

            lvOperationen.Columns.Add(GetText("Datum"), 80, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("Fallzahl"), 80, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("OPSKode"), 80, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("Bezeichnung"), 320, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("Nr"), 40, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("Richtlinie"), 120, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("Gebiet"), -2, HorizontalAlignment.Left);
        }
        private bool ValidateDatumVonBis()
        {
            return ValidateDatumVonBis(lblDatumVon, txtDatumVon.Text, lblDatumBis, txtDatumBis.Text);
        }

        private void FillListViewOperationen(int quelle)
        {
            int nChirugen = ConvertToInt32(_oChirurg["ID_Chirurgen"]);
            int nID_OPFunktionen = ConvertToInt32(cbOPFunktionen.SelectedValue);

            DateTime? von = null;
            DateTime? bis = null;

            GetVonBisDatum(txtDatumVon, txtDatumBis, out von, out bis);

            DataView dataview = BusinessLayer.GetIstOperationenChirurgMitRichtlinien(nChirugen, txtOPS.Text, nID_OPFunktionen, quelle, true,
                von, bis);

            lvOperationen.Items.Clear();
            lvOperationen.BeginUpdate();
            for (int i = 0; i < dataview.Table.Rows.Count; i++)
            {
                DataRow dataRow = dataview.Table.Rows[i];
                ListViewItem lvi = new ListViewItem(Tools.DBNullableDateTime2SortableDateString(dataRow["Datum"]));
                lvi.Tag = ConvertToInt32(dataRow["ID_ChirurgenOperationen"]);
                lvi.ToolTipText = "[" + (string)dataRow["Kode"] + "] " + (string)dataRow["Name"];
                lvi.SubItems.Add((string)dataRow["Fallzahl"]);
                lvi.SubItems.Add((string)dataRow["Kode"]);
                lvi.SubItems.Add((string)dataRow["Name"]);
                lvi.SubItems.Add(Tools.DBNullableInt2String(dataRow["LfdNummer"]));
                lvi.SubItems.Add(Tools.DBNullableString2String(dataRow["UntBehMethode"]));
                lvi.SubItems.Add(Tools.DBNullableString2String(dataRow["Gebiet"]));
                lvOperationen.Items.Add(lvi);
            }
            lvOperationen.EndUpdate();

            SetGroupBoxText(lvOperationen, grpOperationen, "Prozeduren");
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cmdNew_Click(object sender, EventArgs e)
        {
            mainWindow.OpenWindow("OperationenEditView.edit", typeof(OperationenEditView));
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            int nCount = lvOperationen.SelectedItems.Count;

            if (nCount > 0)
            {
                if (Confirm(string.Format(GetText("confirmDeleteOperation"), nCount)))
                {
                    Cursor = Cursors.WaitCursor;

                    try
                    {
                        BusinessLayer.OpenDatabaseForImport();
                        foreach (ListViewItem lvi in lvOperationen.SelectedItems)
                        {
                            int nID_ChirurgenOperationen = (int)lvi.Tag;
                            if (nID_ChirurgenOperationen != -1)
                            {
                                if (!BusinessLayer.DeleteChirurgenOperationen(nID_ChirurgenOperationen))
                                {
                                    break;
                                }
                            }
                        }
                    }
                    finally
                    {
                        BusinessLayer.CloseDatabaseForImport();
                    }

                    PopulateOperationen();
                    Cursor = Cursors.Default;
                }
            }
            else
            {
                MessageBox(GetTextSelectionNone());
            }
        }

        private void cmdPrintAll_Click(object sender, EventArgs e)
        {
            PrintIstOperationen dlg = new PrintIstOperationen(
                BusinessLayer, _oChirurg,
                txtDatumVon.Text, txtDatumBis.Text);

            dlg.ShowDialog();
        }

        private void GetChirurgenGebiete()
        {
            int ID_Chirurgen = ConvertToInt32(cbChirurgen.SelectedValue);

            _gebieteVon.Clear();
            _gebieteBis.Clear();

            DataView dv = BusinessLayer.GetChirurgenGebiete(ID_Chirurgen);

            foreach (DataRow row in dv.Table.Rows)
            {
                DateTime? von = (row["GebietVon"] == DBNull.Value) ? null : (DateTime?)row["GebietVon"];
                DateTime? bis = (row["GebietBis"] == DBNull.Value) ? null : (DateTime?)row["GebietBis"];

                _gebieteVon[ConvertToInt32(row["ID_Gebiete"])] = von;
                _gebieteBis[ConvertToInt32(row["ID_Gebiete"])] = bis;
            }
        }

        private void PopulateOperationen()
        {
            if (ValidateDatumVonBis())
            {
                Cursor = Cursors.WaitCursor;
                XableAllButtonsForLongOperation(null, false);

                CheckQuelleCorrectDumbSettings(chkIntern, chkExtern);
                int quelle = GetInternExternFlag(chkIntern.Checked, chkExtern.Checked);

                FillListViewOperationen(quelle);
                FillListViewOverview(quelle);

                XableAllButtonsForLongOperation(null, true);
                Cursor = Cursors.Default;
            }
        }

        private void ChirurgChanged()
        {
            int ID_Chirurgen = ConvertToInt32(cbChirurgen.SelectedValue);
            _oChirurg = BusinessLayer.DatabaseLayer.GetChirurg(ID_Chirurgen);

            // Datümer für die Gebiete des Chirurgen holen
            GetChirurgenGebiete();

            // Datum von-bis des aktuellen Chirurgen des aktuellen Gebietes setzen.
            PopulateDatumVonBis();
        }

        private void cbChirurgen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_bIgnoreControlEvents)
            {
                ChirurgChanged();
            }
        }

        private void cmdAnzeigen_Click(object sender, EventArgs e)
        {
            if (!_bIgnoreControlEvents)
            {
                PopulateOperationen();
            }
        }

        private void cbGebiete_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_bIgnoreControlEvents)
            {
                PopulateDatumVonBis();
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtDatumVon.Text = "";
            txtDatumBis.Text = "";
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            base.SearchOPSKodeOrOPSText(lvOperationen, 2, txtOPSSearch.Text, 3, txtOPSSearch.Text);
        }


        private void cmdAssignRichtlinie_Click(object sender, EventArgs e)
        {
            int nCount = lvOperationen.SelectedItems.Count;

            if (nCount > 0)
            {
                RichtlinieSelectView dlg = new RichtlinieSelectView(BusinessLayer);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    if (dlg.ID_Richtlinien != -1)
                    {
                        Cursor = Cursors.WaitCursor;
                        foreach (int i in lvOperationen.SelectedIndices)
                        {
                            int nID_ChirurgenOperationen = (int)lvOperationen.Items[i].Tag;

                            DataRow dataRow = BusinessLayer.GetChirurgenOperationenRecord(nID_ChirurgenOperationen);
                            dataRow["ID_Richtlinien"] = dlg.ID_Richtlinien;
                            BusinessLayer.UpdateChirurgenOperationenRichtlinie(dataRow);
                        }
                        Cursor = Cursors.Default;
                    }
                }
            }
            else
            {
                MessageBox(GetTextSelectionNone());
            }
        }

        private void cmdRemoveRichtlinie_Click(object sender, EventArgs e)
        {
            int nCount = lvOperationen.SelectedItems.Count;

            if (nCount > 0)
            {
                string message = string.Format(GetText("confirmDeleteZuordnung"), lvOperationen.SelectedIndices.Count);

                if (Confirm(message))
                {
                    Cursor = Cursors.WaitCursor;
                    foreach (int i in lvOperationen.SelectedIndices)
                    {
                        int nID_ChirurgenOperationen = (int)lvOperationen.Items[i].Tag;

                        DataRow dataRow = BusinessLayer.GetChirurgenOperationenRecord(nID_ChirurgenOperationen);
                        dataRow["ID_Richtlinien"] = DBNull.Value;
                        BusinessLayer.UpdateChirurgenOperationenRichtlinie(dataRow);
                    }
                    Cursor = Cursors.Default;
                }
            }
            else
            {
                MessageBox(GetTextSelectionNone());
            }
        }

        private void cmdPrintOperationen_Click(object sender, EventArgs e)
        {
            _printOperationen = true;
            ContextMenuStrip cms = CreateContextMenu(ModePrintListViewBrowser | ModePrintListViewBrowserSelectedColumns);
            cms.Show(cmdPrintOperationen, new Point(10, 10));
        }

        private void cmdPrintOverview_Click(object sender, EventArgs e)
        {
            _printOperationen = false;
            ContextMenuStrip cms = CreateContextMenu(ModePrintListViewBrowser | ModePrintListViewBrowserSelectedColumns);
            cms.Show(cmdPrintOverview, new Point(10, 10));
        }
        protected override void PrintListViewBrowser_Click(object sender, EventArgs e)
        {
            if (_printOperationen)
            {
                PrintListView(lvOperationen, GlobalConstants.KeyPrintLinesDefaultString);
            }
            else
            {
                PrintListView(lvOverview, GlobalConstants.KeyPrintLinesDefaultString);
            }
        }
        protected override void PrintListViewBrowserSelectedColumns_Click(object sender, EventArgs e)
        {
            if (_printOperationen)
            {
                PrintListViewSelectedColumns(lvOperationen, GlobalConstants.KeyPrintLinesDefaultString);
            }
            else
            {
                PrintListViewSelectedColumns(lvOverview, GlobalConstants.KeyPrintLinesDefaultString);
            }
        }
        protected override string PrintHTMLFilter()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(PrintHTMLFilter(txtDatumVon.Text, txtDatumBis.Text, chkIntern.Checked, chkExtern.Checked, cbOPFunktionen.Text));
            sb.Append(lblChirurg.Text + " " + cbChirurgen.Text);
            if (txtOPS.Text.Length > 0)
            {
                sb.Append("<br/>");
                sb.Append(string.Format(CultureInfo.InvariantCulture, GetText("filter_opscode"), MakeSafeHTML(txtOPS.Text)));
            }
            if (!_printOperationen)
            {
                sb.Append("<br/>");
                sb.Append(lblSumme.Text + " " + txtSumme.Text);
            }

            return sb.ToString();
        }
    }
}

