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

using Windows.Forms;
using Utility;
using CMaurer.Operationen.AppFramework;

namespace Operationen
{
    public partial class RichtlinienVergleichOverviewView : OperationenForm
    {
        private const int ColumnIndexBalkenGrafik = 5;

        public RichtlinienVergleichOverviewView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();

            InitTest();

            SetInfoText(lblInfo, string.Format(GetText("info"), grpFilter.Text, cmdVergleich.Text));

            _bIgnoreControlEvents = true;

            BusinessLayer.PopulateGebiete(cbGebiete);
            InitRichtlinien(lvRichtlinien);

            _bIgnoreControlEvents = false;

            PopulateRichtlinien();

            CheckQuelleDefaultSettings(chkIntern, chkExtern);
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

        private void RichtlinienVergleichOverviewView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));
        }

        protected override bool ValidateInput()
        {
            return ValidateDatumVonBis(lblDatumVon, txtDatumVon.Text, lblDatumBis, txtDatumBis.Text);
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

        private void PopulateRichtlinien()
        {
            int nID_Gebiete = ConvertToInt32(cbGebiete.SelectedValue);
            PopulateRichtlinien(lvRichtlinien, nID_Gebiete, grpRichlinien, GetText("richtlinien"));

            lvTest.Items.Clear();
        }

        private void cmdVergleich_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Cursor = Cursors.WaitCursor;
                XableAllButtonsForLongOperation(cmdAbort, false);
                
                DateTime? dtFrom = GetDateTimeFromTextBox(txtDatumVon);
                DateTime? dtTo = GetDateTimeFromTextBox(txtDatumBis);

                CheckQuelleCorrectDumbSettings(chkIntern, chkExtern);
                int quelle = GetInternExternFlag(chkIntern.Checked, chkExtern.Checked);

                PopulateTest(quelle, dtFrom, dtTo);

                XableAllButtonsForLongOperation(cmdAbort, true);
                Cursor = Cursors.Default;
            }
        }

        private void GruppiereAnzahlExtern(DataView dvManuell)
        {
            //
            // Bei den vorab erfassten eingegebenen kann es mehrfache Einträge für dieselbe Richtlinie geben,
            // etwa mit unterschiedlichem Ort/Datum,
            // diese müssen erstmal zusammengefasst werden
            // Hier sind alle Einträge für genau eine Richtlinien
            //
            for (int i = 0; i < dvManuell.Table.Rows.Count; i++)
            {
                if (Abort)
                {
                    break;
                }
                DataRow row1 = dvManuell.Table.Rows[i];

                int j = i + 1;
                while (j < dvManuell.Table.Rows.Count)
                {
                    if (Abort)
                    {
                        break;
                    }
                    DataRow row2 = dvManuell.Table.Rows[j];

                    // Wenn etwas addiert wird, müssen Ort und Datum wegfallen
                    // mySQL: count() ist long ,sonst int
                    row1["Anzahl"] = Convert.ToInt32(Convert.ToInt64(row1["Anzahl"]) + Convert.ToInt64(row2["Anzahl"]));
                    row1["Ort"] = "";
                    row1["Datum"] = DBNull.Value;
                    dvManuell.Table.Rows.RemoveAt(j);
                }
            }
        }

        private void GruppiereAnzahlAuto(DataView dv)
        {
            //
            // Alle üfer Regeln oder fest zugeordneten zusammenfassen.
            // Mit UNION ALL kammen genau zwei Datensätze
            // Hier sind alle Einträge für genau eine Richtlinien
            //
            for (int i = 0; i < dv.Table.Rows.Count; i++)
            {
                if (Abort)
                {
                    break;
                }
                DataRow row1 = dv.Table.Rows[i];

                int j = i + 1;
                while (j < dv.Table.Rows.Count)
                {
                    if (Abort)
                    {
                        break;
                    }
                    DataRow row2 = dv.Table.Rows[j];

                    // Wenn etwas addiert wird, müssen Ort und Datum wegfallen
                    // mySQL: count() ist long ,sonst int
                    row1["Anzahl"] = Convert.ToInt32(Convert.ToInt64(row1["Anzahl"]) + Convert.ToInt64(row2["Anzahl"]));
                    dv.Table.Rows.RemoveAt(j);
                }
            }
        }

        // Werte aus dvManuell werden zu _dataView addiert.
        // Sind sie nicht vorhanden, werden sie ans Ende angehängt.
        // Alle Zeilen sind für genau eine Richtlinie
        private void MergeIstZahlen(DataView dvAuto, DataView dvManuell)
        {
            GruppiereAnzahlAuto(dvAuto);
            GruppiereAnzahlExtern(dvManuell);

            // gleiche Addieren
            foreach (DataRow rowAuto in dvAuto.Table.Rows)
            {
                for (int i = 0; i < dvManuell.Table.Rows.Count; i++)
                {
                    if (Abort)
                    {
                        break;
                    }
                    DataRow rowManuell = dvManuell.Table.Rows[i];

                    // Sehr schlecht: bei mySQL ist count() long, by Access nur int
                    rowAuto["Anzahl"] = Convert.ToInt32(Convert.ToInt64(rowAuto["Anzahl"]) + Convert.ToInt64(rowManuell["Anzahl"]));
                    dvManuell.Table.Rows.RemoveAt(i);
                }
            }
        }

        private void InitTest()
        {
            lvTest.Clear();
            SetWatermark(lvTest);

            lvTest.Columns.Add(GetText("nachname"), 120, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("vorname"), 120, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("ist"), 70, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("soll"), 70, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("prozent"), 90, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("prozent"), -2, HorizontalAlignment.Left);

            lvTest.SetBalkenColumnIndex(ColumnIndexBalkenGrafik);
        }

        private void PopulateTest(int quelle, DateTime? von, DateTime? bis)
        {
            long summe = 0;

            Abort = false;

            lvTest.Items.Clear();

            // Keine Meldung ausgeben, dieser Event kommt zweimal und beim ersten Mal ist nix selektiert
            int ID_Richtlinien = GetFirstSelectedTag(lvRichtlinien);
            ListViewItem lviRichtlinien = GetFirstSelectedLVI(lvRichtlinien);

            if (lviRichtlinien == null || ID_Richtlinien == -1)
            {
                goto _exit;
            }

            int richtzahl = 0;
            
            if (!RichtlinienGetRichtzahl(lviRichtlinien, out richtzahl))
            {
                goto _exit;
            }

            DataView chirurgen = BusinessLayer.GetChirurgen();

            lvTest.BeginUpdate();
            // Man soll den Fortschritt sehen, da es soviele Zeilen wie Chirurgen gibt, ist das ok.
            // 15.07.2009: Das flackert bei vielen Ärzten wie blöd!
            int i = 0;
            foreach (DataRow chirurg in chirurgen.Table.Rows)
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

                int ID_Chirurgen = ConvertToInt32(chirurg["ID_Chirurgen"]);

                DataView dataView = BusinessLayer.GetRichtlinienOPSummenChirurgRichtlinie(ID_Chirurgen, ID_Richtlinien, quelle, von, bis);
                Application.DoEvents();
                if (Abort)
                {
                    break;
                }

                DataView dvManuell = BusinessLayer.GetChirurgenRichtlinienRichtlinie(ID_Chirurgen, ID_Richtlinien, von, bis);
                Application.DoEvents();
                if (Abort)
                {
                    break;
                }

                // Manuelle Werte müssen zusammengefügt werden mit den automatisch zugeordneten
                MergeIstZahlen(dataView, dvManuell);

                foreach (DataRow row in dataView.Table.Rows)
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
                    long anzahl = Convert.ToInt64(row["Anzahl"]);
                    summe += anzahl;

                    ListViewItem lvi = new ListViewItem((string)chirurg["Nachname"]);
                        
                    lvi.SubItems.Add((string)chirurg["Vorname"]);
                    lvi.SubItems.Add(string.Format("{0,3}", anzahl));
                    lvi.SubItems.Add(richtzahl.ToString());
                    lvi.SubItems.Add(string.Format("{0,3}%", (int)(anzahl * 100.0 / richtzahl)));
                    lvi.SubItems.Add(string.Format("{0,3}|{1}", row["Anzahl"], richtzahl));

                    lvTest.Items.Add(lvi);
                    Application.DoEvents();
                }
            }
            lvTest.EndUpdate();

            txtSumme.Text = summe.ToString();

            _exit:;
        }

        private void cbGebiete_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateRichtlinien();
        }

        private void lvRichtlinien_Click(object sender, EventArgs e)
        {
            cmdVergleich_Click(null, null);
        }

        #region Printing
        protected override string PrintHTMLFilter()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(GetQuelleInternExternText(chkIntern.Checked, chkExtern.Checked));
            sb.Append("<br/>");
            sb.Append(MakeSafeHTML(GetText("gebiet") + ": " + cbGebiete.Text));
            sb.Append("<br/>");
            sb.Append(GetDateVonBisHTMLFilterText(txtDatumVon.Text, txtDatumBis.Text));

            sb.Append(MakeSafeHTML(GetText("weiterbildungsrichtlinie") + ": "));
            if (lvRichtlinien.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lvRichtlinien.SelectedItems[0];
                sb.Append(MakeSafeHTML(GetText("nr") + " "));
                sb.Append(MakeSafeHTML(lvi.SubItems[0].Text.Trim()));
                sb.Append(MakeSafeHTML(", " + GetText("richtzahl") + " "));
                sb.Append(MakeSafeHTML(lvi.SubItems[1].Text.Trim()));
                sb.Append(", '");
                sb.Append(MakeSafeHTML(lvi.SubItems[2].Text.Trim()));
                sb.Append("'");
            }
            else
            {
                sb.Append(MakeSafeHTML(GetText("keineAusgewaehlt")));
            }
            sb.Append("<br/>");
            sb.Append(lblSumme.Text);
            sb.Append(" ");
            sb.Append(txtSumme.Text);

            return sb.ToString();
        }
        private void cmdPrint_Click(object sender, EventArgs e)
        {
            ContextMenuStrip cms = CreateContextMenu(ModePrintListViewBrowser | ModePrintListViewBrowserSelectedColumns);
            cms.Show(cmdPrint, new Point(10, 10));
        }
        protected override void PrintListViewBrowser_Click(object sender, EventArgs e)
        {
            PrintListView(lvTest, GlobalConstants.KeyPrintLinesRichtlinienVergleichOverviewView);
        }

        protected override void PrintListViewBrowserSelectedColumns_Click(object sender, EventArgs e)
        {
            PrintListViewSelectedColumns(lvTest, GlobalConstants.KeyPrintLinesRichtlinienVergleichOverviewView);
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
        #endregion

        private void cmdAbort_Click(object sender, EventArgs e)
        {
            Abort = true;
        }
    }
}

