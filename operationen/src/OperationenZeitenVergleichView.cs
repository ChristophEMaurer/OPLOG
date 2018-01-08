using System;
using System.Collections;
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
    public partial class OperationenZeitenVergleichView : OperationenForm
    {
        private const int ColumnIndexBalkenGrafik = 4;

        public OperationenZeitenVergleichView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();

            this.Text = AppTitle(GetText("title"));

            CheckQuelleDefaultSettings(chkIntern, chkExtern);
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void OperationenZeitenVergleichView_Load(object sender, EventArgs e)
        {
            PopulateOPFunktionen(cbChirurgenOPFunktionen, true);

            SetInfoText(lblInfo, GetText("info"));

            InitTest();
        }

        private void InitTest()
        {
            lvTest.Clear();
            //SetWatermark(lvTest);

            lvTest.Columns.Add(GetText("nachname"), 100, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("vorname"), 100, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("zeit"), 120, HorizontalAlignment.Left);
            lvTest.Columns.Add(GetText("prozent"), 70, HorizontalAlignment.Left);

            // Balkengrafik
            lvTest.Columns.Add(GetText("prozent"), -2, HorizontalAlignment.Left);
            lvTest.SetBalkenColumnIndex(ColumnIndexBalkenGrafik);

        }

        private TimeSpan CalculateMinutes(DataView dv)
        {
            TimeSpan timeSpan = new TimeSpan();

            //
            // Beachten: Datum: 01.01.2001  Begin: 22:30    Ende: 00:25
            //
            // Fallzahl
            // 1234         01.01.2001  10:00   12:00
            // 1234         01.01.2001  10:00   12:00 von allen mit derselben Fallzahl, die Zeit nur einmal nehmen.
            // 1234         01.01.2001  10:00   12:00
            //
            for (int i = 0; i < dv.Table.Rows.Count; i++)
            {
                DataRow row = dv.Table.Rows[i];

                string fallzahl = (string)row["Fallzahl"];
                DateTime dtDatum = (DateTime) row["Datum"];
                DateTime dtZeit = (DateTime) row["Zeit"];
                DateTime dtZeitBis = (DateTime) row["ZeitBis"];

                DateTime dtBeginn = new DateTime(dtDatum.Year, dtDatum.Month, dtDatum.Day, dtZeit.Hour, dtZeit.Minute, dtZeit.Second);
                DateTime dtEnde = new DateTime(dtDatum.Year, dtDatum.Month, dtDatum.Day, dtZeitBis.Hour, dtZeitBis.Minute, dtZeitBis.Second);

                if (dtBeginn <= dtEnde)
                {
                    // 01.01.2001-22:00  bis  01.01.2001-23:00
                }
                else
                {
                    // 01.01.2001-22:00  bis  01.01.2001-00:30: 
                    // Das Ende Datum einen Tag weiter setzen, dann passt es.
                    dtEnde = dtEnde.AddDays(1);
                }

                TimeSpan span = new TimeSpan(dtEnde.Ticks - dtBeginn.Ticks);
                timeSpan = timeSpan.Add(span);

                // es ist nach Fallzahl sortiert, also kommen gleich immer hintereinander.
                int j = i + 1;
                while (j < dv.Table.Rows.Count)
                {
                    DataRow row2 = dv.Table.Rows[j];
                    string fallzahl2 = (string)row2["Fallzahl"];

                    if (fallzahl == fallzahl2)
                    {
                        dv.Table.Rows.RemoveAt(j);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return timeSpan;
        }

        /// <summary>
        /// Zu einer Zeile aus der .csv Importdatei können für eine Fallzahl mehrere ChirurugenOperationen erzeugt werden 
        /// für einen Chirurgen. Diese Zeiten dürfen nicht addiert werden, sondern für die eine
        /// Fallzahl darf nur eine Zeitspanne von diesen genommen werden.
        /// </summary>
        /// <param name="nID_OPFunktionen"></param>
        /// <param name="quelle"></param>
        private void PopulateTest(int nID_OPFunktionen, int quelle)
        {
            // TODO nach Zeiten sortieren
            const string KeyTimeSpan = "__timespan__";
            DateTime? dtVon;
            DateTime? dtBis;

            GetVonBisDatum(txtDatumVon, txtDatumBis, out dtVon, out dtBis);

            DataView chirurgen = BusinessLayer.GetChirurgen();
            chirurgen.Table.Columns.Add(new DataColumn(KeyTimeSpan, typeof(long)));

            TimeSpan summe = new TimeSpan();

            // Erst alle durchlaufen und die Zeit berechnen und eintragen
            foreach (DataRow chirurg in chirurgen.Table.Rows)
            {
                int nID_Chirurgen = ConvertToInt32(chirurg["ID_Chirurgen"]);

                DataView dv = BusinessLayer.GetOPZeiten(nID_Chirurgen, nID_OPFunktionen, dtVon, dtBis, quelle);
                TimeSpan timeSpan = CalculateMinutes(dv);
                chirurg[KeyTimeSpan] = timeSpan.Ticks;

                summe = summe.Add(timeSpan);
            }

            lvTest.Items.Clear();
            foreach (DataRow chirurg in chirurgen.Table.Rows)
            {
                TimeSpan ts = new TimeSpan((long)chirurg[KeyTimeSpan]);

                ListViewItem lvi = new ListViewItem((string)chirurg["Nachname"]);
                lvi.SubItems.Add((string)chirurg["Vorname"]);
                lvi.SubItems.Add(string.Format("{0} T, {1:00} St, {2:00} Min", ts.Days, ts.Hours, ts.Minutes));

                long ist = (long) chirurg[KeyTimeSpan];
                long soll = summe.Ticks;

                string data = string.Format("{0}|{1}", ist, soll);

                lvi.SubItems.Add(string.Format("{0}%", ProzentFromBalkenGrafikData(data).ToString()));

                // Balkengrafik enthält: Ist/MAX
                // MAX ist das höchste Ist von allen Chirurgen
                lvi.SubItems.Add(data);

                lvTest.Items.Add(lvi);
            }

            string summeAsString = "";

            if (summe.Days > 0)
            {
                summeAsString = summe.Days + " " + GetText("tage") + ", ";
            }
            summeAsString = summeAsString + " " + summe.Hours + " " + GetText("stunden") + ", " + summe.Minutes + " " + GetText("minuten");
            txtGesamtIst.Text = summeAsString;
        }

        private void cmdVergleich_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Cursor = Cursors.WaitCursor;

                CheckQuelleCorrectDumbSettings(chkIntern, chkExtern);

                int nID_OPFunktionen = ConvertToInt32(cbChirurgenOPFunktionen.SelectedValue);
                int quelle = GetInternExternFlag(chkIntern.Checked, chkExtern.Checked);
                PopulateTest(nID_OPFunktionen, quelle);

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

        protected override string PrintHTMLHeaderLine1()
        {
            return MakeSafeHTML(GetText("title"));
        }
        
        protected override string PrintHTMLFilter()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(GetDateVonBisHTMLFilterText(txtDatumVon.Text, txtDatumBis.Text));
            sb.Append(GetQuelleInternExternText(chkIntern.Checked, chkExtern.Checked));
            sb.Append("<br/>");
            sb.Append(GetText("funktion") + ": " + MakeSafeHTML(cbChirurgenOPFunktionen.Text));
            sb.Append("<br/>");
            sb.Append(MakeSafeHTML(lblGesamt.Text + txtGesamtIst.Text));

            return sb.ToString();
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

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            ContextMenuStrip cms = CreateContextMenu(ModePrintListViewBrowser | ModePrintListViewBrowserSelectedColumns);
            cms.Show(cmdPrint, new Point(10, 10));
        }

        protected override void PrintListViewBrowser_Click(object sender, EventArgs e)
        {
            PrintListView(lvTest, GlobalConstants.KeyPrintLinesOperationenZeitenVergleichView);
        }
        protected override void PrintListViewBrowserSelectedColumns_Click(object sender, EventArgs e)
        {
            PrintListViewSelectedColumns(lvTest, GlobalConstants.KeyPrintLinesOperationenZeitenVergleichView);
        }
    }
}

