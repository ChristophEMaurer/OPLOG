using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Globalization;

using Windows.Forms;
using Utility;
using Operationen;
using CMaurer.Operationen.AppFramework;

namespace Operationen
{
    /// <summary>
    /// Mit "Anzeigen" werden alle Datensätze gelesen und gemerkt. 
    /// Genau diese gemerkten Datensätze werden dann mit "Drucken" gedruckt.
    /// </summary>
    public partial class GesamtOperationenView : OperationenForm
    {
        private DataView _dataView;

        private string groupOperationenText;

        public GesamtOperationenView()
        {
            groupOperationenText = GetText("grpText");
        }

        public GesamtOperationenView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();

            cmdAbort.Enabled = false;

            CheckQuelleDefaultSettings(chkIntern, chkExtern);

            cmdPrint.Enabled = false;
            radSortRichtlinie.Checked = false;
            radSortOPSKode.Checked = true;

            SetInfoText(lblInfo, 
                string.Format(CultureInfo.InvariantCulture, GetText("info"),
                    Command_RichtlinienOpsKodeUnassignedView));

            this.Text = AppTitle(GetText("title"));
            grpOperationen.Text = groupOperationenText;
        }

        private void PopulateGebiete()
        {
            lvGebiete.Clear();
            DefaultListViewProperties(lvGebiete);

            lvGebiete.Columns.Add(GetText("gebiete"), -2, HorizontalAlignment.Left);

            DataView dv = BusinessLayer.GetGebiete(true);

            lvGebiete.BeginUpdate();
            foreach (DataRow dataRow in dv.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem((string)dataRow["Gebiet"]);
                lvi.Tag = ConvertToInt32(dataRow["ID_Gebiete"]);
                string toolTip = (string)dataRow["Bemerkung"];
                if (((string)dataRow["Herkunft"]).Length > 0)
                {
                    toolTip += ", " + (string)dataRow["Herkunft"];
                }
                lvi.ToolTipText = toolTip;

                lvGebiete.Items.Add(lvi);
            }
            if (lvGebiete.Items.Count > 0)
            {
                lvGebiete.SelectedIndices.Clear();
                lvGebiete.SelectedIndices.Add(0);
            }

            lvGebiete.EndUpdate();
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

        private void InitOperationen()
        {
            DefaultListViewProperties(lvOperationen);
            SetWatermark(lvOperationen);

            lvOperationen.Columns.Add(GetText("no"), 80, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("gebiet"), 140, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("anzahl"), 50, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("opsCode"), 80, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("bezeichnung"), -2, HorizontalAlignment.Left);
        }

        private void PopulateOperationen()
        {
            int i;

            List<int> arSelectedGebiete = new List<int>();

            int ID_OPFunktionen = ConvertToInt32(cbOperationenOPFunktionen.SelectedValue);

            DateTime? von;
            DateTime? bis;

            GetVonBisDatum(txtDatumVon, txtDatumBis, out von, out bis);

            foreach (int index in lvGebiete.SelectedIndices)
            {
                arSelectedGebiete.Add((int)lvGebiete.Items[index].Tag);
            }

            CheckQuelleCorrectDumbSettings(chkIntern, chkExtern);
            int quelle = GetInternExternFlag(chkIntern.Checked, chkExtern.Checked);

            DataView dv = BusinessLayer.GetAusgefuehrteOperationenFuerGebiete(arSelectedGebiete, quelle, von, bis, ID_OPFunktionen);

            DataTable table = dv.Table;

            //
            // Alle ID_ChirurgenOperationen, die durch den JOIN mehrfach auftreten, entfernen
            // Das dauert mit zwei geschachtelten for Schleifen quadratische Zeit, und das dauert unendlich lange.
            // Daher erst sortieren und dann in linearer Zeit die doppelten entfernen
            //

            //
            // Nach ID_ChirurgenOperationen sortieren und doppelte herauswerfen
            //
            table.DefaultView.Sort = "[ID_ChirurgenOperationen] asc";
            DataTable table_id = table.DefaultView.ToTable();
            i = 0;
            while (i < table_id.Rows.Count)
            {
                if (i % BusinessLayer.AbortLoopCount == 0)
                {
                    Application.DoEvents();
                }
                if (Abort)
                {
                    break;
                }

                int j = i + 1;
                if (j < table_id.Rows.Count)
                {
                    DataRow rowi = table_id.Rows[i];
                    DataRow rowj = table_id.Rows[j];

                    if (rowi["ID_ChirurgenOperationen"] == rowj["ID_ChirurgenOperationen"])
                    {
                        table_id.Rows.RemoveAt(j);
                    }
                    else
                    {
                        i = j;
                    }
                }
                else
                {
                    break;
                }
            }

            //
            // Nach OPSKode sortieren und doppelte addieren und löschen
            //

            table_id.DefaultView.Sort = "[OPSKode] asc";
            DataTable table_OPSKode = table_id.DefaultView.ToTable();
            i = 0;
            while (i < table_OPSKode.Rows.Count)
            {
                if (i % BusinessLayer.AbortLoopCount == 0)
                {
                    Application.DoEvents();
                }
                if (Abort)
                {
                    break;
                } 

                int j = i + 1;
                if (j < table_OPSKode.Rows.Count)
                {
                    DataRow rowi = table_OPSKode.Rows[i];
                    DataRow rowj = table_OPSKode.Rows[j];

                    if ((string)rowi["OPSKode"] == (string)rowj["OPSKode"])
                    {
                        // Noch sind alle Zeilen mit Anzahl = 1, also rowi++
                        rowi["Anzahl"] = Convert.ToInt64(rowi["Anzahl"]) + 1;
                        table_OPSKode.Rows.RemoveAt(j);
                    }
                    else
                    {
                        i = j;
                    }
                }
                else
                {
                    break;
                }
            }

            DataTable table_last = table_OPSKode;

            // Wenn nach Richtlinien gruppiert werden sollte, dann dieses jetzt tun:
            // Die Anzahl aller Zeilen mit derselben ID_Richtlinien addieren, und bei der 
            // einen Zeile, die dann übrigbleibt, OPSKode/OPSBezeichnung auf "" setzen, da
            // diese Werte ja jetzt die Summen von allen OPSKodes/Bezeichnungen sind, und 
            // da zeigen wir daher nix an.
            if (radSortRichtlinie.Checked && chkGroupRichtlinie.Checked)
            {
                table_OPSKode.DefaultView.Sort = "[ID_Richtlinien] asc";
                table_last = table_OPSKode.DefaultView.ToTable();

                i = 0;
                while (i < table_last.Rows.Count)
                {
                    if (i % BusinessLayer.AbortLoopCount == 0)
                    {
                        Application.DoEvents();
                    }
                    if (Abort)
                    {
                        break;
                    }
                    
                    int j = i + 1;
                    if (j < table_last.Rows.Count)
                    {
                        DataRow rowi = table_last.Rows[i];
                        DataRow rowj = table_last.Rows[j];

                        if (ConvertToInt32(rowi["ID_Richtlinien"]) == ConvertToInt32((rowj["ID_Richtlinien"])))
                        {
                            rowi["Anzahl"] = Convert.ToInt64(rowi["Anzahl"]) + Convert.ToInt64(rowj["Anzahl"]);
                            rowi["OPSKode"] = "";
                            rowi["OPSText"] = "";
                            table_last.Rows.RemoveAt(j);
                        }
                        else
                        {
                            i = j;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (radSortRichtlinie.Checked)
            {
                table_last.DefaultView.Sort = "ID_Gebiete, LfdNummer, OPSKode";
            }
            else
            {
                table_last.DefaultView.Sort = "OPSKode, ID_Gebiete, LfdNummer";
            }

            DataTable tableSorted = table_last.DefaultView.ToTable();

            _dataView = new DataView(tableSorted);

            long gesamtAnzahl = 0;
            lvOperationen.Items.Clear();
            lvOperationen.BeginUpdate();
            i = 0;
            foreach (DataRow dataRow in tableSorted.Rows)
            {
                i++;
                if (i % BusinessLayer.AbortLoopCount == 0)
                {
                    SetGroupBoxText(lvOperationen, grpOperationen, groupOperationenText);
                    Application.DoEvents();
                }
                if (Abort)
                {
                    break;
                }
                
                ListViewItem lvi = new ListViewItem(dataRow["LfdNummer"].ToString());
                lvi.ToolTipText = GetText("richtzahl") + " " + dataRow["Richtzahl"].ToString() + ": " + (string)dataRow["UntBehMethode"];
                lvi.SubItems.Add((string)dataRow["Gebiet"]);
                lvi.SubItems.Add(dataRow["Anzahl"].ToString());
                lvi.SubItems.Add((string)dataRow["OPSKode"]);
                lvi.SubItems.Add((string)dataRow["OPSText"]);

                lvOperationen.Items.Add(lvi);

                gesamtAnzahl += Convert.ToInt64(dataRow["Anzahl"]);
            }
            lvOperationen.EndUpdate();

            SetGroupBoxText(lvOperationen, grpOperationen, groupOperationenText);
            txtGesamtanzahl.Text = gesamtAnzahl.ToString();
            txtSummeSelektion.Text = "";
        }

        private void GesamtOperationenView_Load(object sender, EventArgs e)
        {
            PopulateGebiete();
            PopulateOPFunktionen(cbOperationenOPFunktionen, true);
            InitOperationen();
        }

        protected override bool ValidateInput()
        {
            bool bSuccess = true;
            string strMsg = EINGABEFEHLER;

            if (txtDatumVon.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtDatumVon.Text))
            {
                bSuccess = false;
                strMsg += GetTextControlInvalid(lblVon);
            }
            if (txtDatumBis.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtDatumBis.Text))
            {
                bSuccess = false;
                strMsg += GetTextControlInvalid(lblBis);
            }

            if (lvGebiete.SelectedIndices.Count == 0)
            {
                bSuccess = false;
                strMsg += GetTextItemNeedsSelection(GetText("gebiet"));
            }

            if (!bSuccess)
            {
                MessageBox(strMsg);
            }

            return bSuccess;
        }

        private void cmdAnzeigenOperationen_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                //
                // No wait cursor, we can click 'Abort'
                //
                Cursor = Cursors.WaitCursor;
                XableAllButtonsForLongOperation(cmdAbort, false);

                PopulateOperationen();

                XableAllButtonsForLongOperation(cmdAbort, true);
                Cursor = Cursors.Default;
            }
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            base.SearchOPSKodeOrOPSText(lvOperationen, 3, txtFilterOPS.Text, 4, txtFilterOPS.Text);
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

            line = GetText("title");// "Ausgeführte Prozeduren Gesamt";
            PrintLine(ev, nLine++, line);

            line = "";
            foreach (ListViewItem lvi in lvGebiete.SelectedItems)
            {
                if (line.Length > 0)
                {
                    line += ", ";
                }
                line += lvi.Text;
            }
            line = GetText("filterGebiete") + line;
            PrintLine(ev, nLine++, line);

            line = GetText("filterFunktion") + cbOperationenOPFunktionen.Text;
            PrintLine(ev, nLine++, line);

            line = GetText("filterTotal") + txtGesamtanzahl.Text;
            PrintLine(ev, nLine++, line);

            line = GetTextPrintFromTo(txtDatumVon.Text, txtDatumBis.Text);
            if (line.Length > 0)
            {
                PrintLine(ev, nLine++, line);
            }

            nLine++;
            nLine++;

            line = GetText("print_head");
            PrintLine(ev, nLine++, line);


            // -1 weil irgendwie die unterste Zeile abgeschnitten war
            int nMaxIndex = _dataRowIndex + nLinesPerPage - nLine - 1;
            if (nMaxIndex >= _dataView.Table.Rows.Count)
            {
                nMaxIndex = _dataView.Table.Rows.Count;
            }

            for (; _dataRowIndex < nMaxIndex; _dataRowIndex++)
            {
                DataRow dataRow = _dataView.Table.Rows[_dataRowIndex];

                line = String.Format("{0,4:G}: {1,4:G} {2} {3,4:G} {4} {5}"
                    , _dataRowIndex + 1
                    , dataRow["LfdNummer"].ToString()
                    , Utility.Tools.CutString((string)dataRow["Gebiet"] + new string(' ', 20), 20, false)
                    , dataRow["Anzahl"].ToString()
                    , Utility.Tools.CutString((string)dataRow["OPSKode"] + "            ", 10, false)
                    , (string)dataRow["OPSText"]
                    );

                PrintLine(ev, nLine++, line);
            }

            _currentPageIndex++;
            ev.HasMorePages = _dataRowIndex < _dataView.Table.Rows.Count;
        }

        #endregion

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            ContextMenuStrip cms = CreateContextMenu(ModePrintForm | ModePrintListViewBrowser | ModePrintListViewBrowserSelectedColumns);
            cms.Show(cmdPrint, new Point(10, 10));
        }

        protected override void PrintForm_Click(object sender, EventArgs e)
        {
            PrintForm();
        }

        protected override void PrintListViewBrowser_Click(object sender, EventArgs e)
        {
            PrintListView(lvOperationen, GlobalConstants.KeyPrintLinesDefaultString);
        }

        protected override string PrintHTMLFilter()
        {
            return PrintHTMLFilter(txtDatumVon.Text, txtDatumBis.Text, chkIntern.Checked, chkExtern.Checked, cbOperationenOPFunktionen.Text);
        }

        protected override void PrintListViewBrowserSelectedColumns_Click(object sender, EventArgs e)
        {
            PrintListViewSelectedColumns(lvOperationen, GlobalConstants.KeyPrintLinesDefaultString);
        }
        private void radSortNr_CheckedChanged(object sender, EventArgs e)
        {
            chkGroupRichtlinie.Enabled = true;
        }

        private void radSortOPSKode_CheckedChanged(object sender, EventArgs e)
        {
            chkGroupRichtlinie.Enabled = false;
        }

        private void lvOperationen_Click(object sender, EventArgs e)
        {
            int summe = 0;

            foreach (ListViewItem lvi in lvOperationen.SelectedItems)
            {
                int anzahl = Int32.Parse(lvi.SubItems[2].Text);
                summe += anzahl;
            }
            txtSummeSelektion.Text = summe.ToString();
        }

        private void cmdAbort_Click(object sender, EventArgs e)
        {
            Abort = true;
        }
    }
}


