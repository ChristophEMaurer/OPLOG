using System;
//using System.Collections;
using System.Collections.Generic;
//using System.ComponentModel;
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
    public partial class OPDauerFortschrittView : OperationenForm
    {
        int _opsKodeIndex = -1;

        public OPDauerFortschrittView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            _bIgnoreControlEvents = true;

            InitializeComponent();

            cmdAbort.Enabled = false;

            radAlle.Checked = false;
            radJahr.Checked = true;
            radMonate.Checked = false;
            txtMonate.Text = "6";
            txtMonate.Enabled = false;

            this.Text = AppTitle(GetText("title"));
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

        private void OPDauerFortschrittView_Load(object sender, EventArgs e)
        {
            PopulateOPFunktionen(cbChirurgenOPFunktionen);
            PopulateChirurgen(cbChirurgen);

            _bIgnoreControlEvents = false;

            SetInfoText();

            CheckQuelleDefaultSettings(chkIntern, chkExtern);
        }

        private void InitTest(OplListView lv, bool bDurchschnitt)
        {
            lv.Clear();
            lv.View = View.Details;
            lv.FullRowSelect = true;

            // Selection ist völlig egal, aber zum Testen kann man so mehrere Zeilen markieren und den Median überprüfem
            lv.MultiSelect = true;
            SetWatermark(lv);

            if (bDurchschnitt)
            {
                lv.Columns.Add(GetText("von"), 80, HorizontalAlignment.Left);
                lv.Columns.Add(GetText("bis"), 80, HorizontalAlignment.Left);
                lv.Columns.Add(GetText("anzahl"), 50, HorizontalAlignment.Left);
                lv.Columns.Add(GetText("durchschnitt"), 180, HorizontalAlignment.Center);
                lv.Columns.Add(GetText("untererMedian"), 100, HorizontalAlignment.Center);
                lv.Columns.Add(GetText("obererMedian"), 100, HorizontalAlignment.Center);
                lv.Columns.Add(GetText("opsCode"), 100, HorizontalAlignment.Left);
                lv.Columns.Add(GetText("opsText"), -2, HorizontalAlignment.Left);
                _opsKodeIndex = 6;
            }
            else
            {
                lv.Columns.Add(GetText("datum"), 100, HorizontalAlignment.Left);
                lv.Columns.Add(GetText("von"), 100, HorizontalAlignment.Left);
                lv.Columns.Add(GetText("bis"), 100, HorizontalAlignment.Left);
                lv.Columns.Add(GetText("dauer"), 100, HorizontalAlignment.Left);
                lv.Columns.Add(GetText("opsCode"), 100, HorizontalAlignment.Left);
                lv.Columns.Add(GetText("opsText"), -2, HorizontalAlignment.Left);
                _opsKodeIndex = 4;
            }
        }

        //
        // Berechnet die Dauer in Minuten zwschen zwei Zeiten:
        // Wenn die EndeZeit vor der BeginnZeit liegt, wird die Endezeit auf dem
        // nächsten Tag vermutet und über Mitternacht gewrappt.
        //
        // Beachten: Datum: 01.01.2001  Begin: 22:30    Ende: 00:25
        //
        private TimeSpan CalculateMinutes(DataRow row)
        {
            DateTime dtDatum = (DateTime)row["Datum"];
            DateTime dtZeit = (DateTime)row["Zeit"];
            DateTime dtZeitBis = (DateTime)row["ZeitBis"];

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

            return span;
        }

        //
        // Berechnet die Dauer in Minuten zwschen zwei Zeiten:
        // Wenn die EndeZeit vor der BeginnZeit liegt, wird die Endezeit auf dem
        // nächsten Tag vermutet und über Mitternacht gewrappt.
        //
        private TimeSpan CalculateMinutes(DataView dv)
        {
            TimeSpan timeSpan = new TimeSpan();

            // Beachten: Datum: 01.01.2001  Begin: 22:30    Ende: 00:25

            foreach (DataRow row in dv.Table.Rows)
            {
                TimeSpan span = CalculateMinutes(row);

                timeSpan = timeSpan.Add(span);
            }

            return timeSpan;
        }

        private DateTime StundenUltimo(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);

        }

        //
        // Für alle OPS-Kodes, die gleich sind und in einem bestimmten Zeitraum liegen, 
        // die Dauer addieren und nur einen übriglassen
        // Für jede Zeile gilt:
        // Der Wert liegt zwischen DurchschnittBeginn und DurchschnittEnde
        // Innerhalb einer solchen Zeitspanne soll nicht der Curchschnitt sondern der Median berechnet werden.
        // Median: ein Wert, so dass 50% der Werte größer und 50% der Werte kleiner sind als dieser.
        // Bei eine ungeraden Anzahl von Werten ist es also genau der Wert in der Mitte.
        // Bei einer geraden Anzahl heißt es 'unterer Median' und 'oberer Median', es sind die beiden in der Mitte
        /*
            Wenn die Werte sortiert sind, dann gilt:
            Für den Median muss man nicht alle Werte im Zugriff haben. Man schiebt abwechselnd den oberen und dann wieder
            den unteren eins weiter, damit ergibt sich automatisch das richtige Ergebnis:
                1uo   
                1u  2o
                1   2uo 3
                1   2u  3o  4
                1   2   3uo 4   5
                1   2   3u  4o  5   6
                1   2   3   4uo 5   6   7
                1   2   3   4u  5o  6   7   8
                1   2   3   4   5uo 6   7   8   9
                1   2   3   4   5u  6o  7   8   9   10
                1   2   3   4   5   6uo 7   8   9   10  11
         
            Leider ist das bei uns nicht der Fall. Man muss sich also alle Werte in einem Zeitraum merken, und anschließen den 
            Median nehmen.
         */
        private void DurchschnittProMonate(DataTable table, DateTime? dtBeginn, int spanMonths)
        {
            TimeSpan durchschnitt;

            //
            // In das Resultset werden jetzt dynamisch einige neue Spalten eingefügt, die werden
            // genau hier erstmals gefüllt.
            // Die Datensätze müssen sortiert sein nach OPSKode, Datum
            // Es wird immer der Durchschnitt über spanMonths Monate gebildet.
            // Das erste Jahr beginnt bei dtBeginn
            //
            // Anzahl ist die Anzahl der Zeilen, die zu einer zusammengefasst wurden.
            // Summe ist die Summe der Zeiten der Zeilen, die zu einer zusammengefasst wurden
            // Durchschnitt ist Summe / Anzahl, eigentlich redundant
            //
            table.Columns.Add(new DataColumn("DurchschnittBeginn", typeof(DateTime)));
            table.Columns.Add(new DataColumn("DurchschnittEnde", typeof(DateTime)));
            // Anzahl, über die der Durchschnitt gebildet wird.
            table.Columns.Add(new DataColumn("Anzahl", typeof(int)));
            table.Columns.Add(new DataColumn("Summe", typeof(TimeSpan)));
            table.Columns.Add(new DataColumn("Durchschnitt", typeof(TimeSpan)));
            table.Columns.Add(new DataColumn("MedianLow", typeof(TimeSpan)));
            table.Columns.Add(new DataColumn("MedianHigh", typeof(TimeSpan)));

            DateTime von = dtBeginn.Value;
            DateTime bis = StundenUltimo(von.AddMonths(spanMonths).AddDays(-1));

            for (int i = 0; i < table.Rows.Count; i++)
            {
                List<TimeSpan> list = new List<TimeSpan>();
                DataRow row1 = table.Rows[i];

                string opsKode = (string)row1["OPS-Kode"];
                int count = 1;
                TimeSpan dauerGesamt = CalculateMinutes(row1);

                list.Add(dauerGesamt);

                DateTime datum = (DateTime)row1["Datum"];

                //
                // Zeitraum solange im Intervall weitersetzen, bis die aktuelle
                // Operation in einem drin liegt.
                // Er wird von seiner aktuellen Position verschoben,
                // bis er um das Datum liegt.
                // Das von Datum muss bei Mitternacht anfangen,
                // da bis-Datum muss bei 23:59:59 aufhören, wenn
                // bis nur 31.12.2007,00:00:00 ist wäre dann
                // ein Datum am 31.12.2007,10:00:00 wegen der Uhrzeit größer als das bis-Datum
                //
                if (datum > bis)
                {
                    while (datum > bis)
                    {
                        von = bis.AddDays(1);
                        bis = StundenUltimo(von.AddMonths(spanMonths).AddDays(-1));
                    }
                }
                if (datum < von)
                {
                    while (datum < von)
                    {
                        bis = StundenUltimo(von.AddDays(-1));
                        von = von.AddMonths(-spanMonths);
                    }
                }

                row1["DurchschnittBeginn"] = von;
                row1["DurchschnittEnde"] = bis;

                int j = i + 1;
                while (j < table.Rows.Count)
                {
                    DataRow row2 = table.Rows[j];

                    if ((string)row1["OPS-Kode"] != (string)row2["OPS-Kode"])
                    {
                        break;
                    }
                    if ((DateTime)row2["Datum"] < von || (DateTime)row2["Datum"] > bis)
                    {
                        break;
                    }

                    //
                    // alle in diesem Zeitraum gehören zusammen
                    //
                    count++;
                    TimeSpan dauer = CalculateMinutes(row2);
                    dauerGesamt = dauerGesamt.Add(dauer);
                    table.Rows.Remove(row2);

                    list.Add(dauer);
                }

                // count ist die Anzahl der OPs, die zusammengefasst wurden
                // dauer enthält die Gesamtdauer
                // jetzt den Durchschnitt bilden
                // dauerGesamt muss man sich merken, damit man später den Gesamtdurchschnitt bilden kann
                row1["Summe"] = dauerGesamt;
                row1["Anzahl"] = count;
                durchschnitt = new TimeSpan(dauerGesamt.Ticks / count);
                row1["Durchschnitt"] = durchschnitt;

                //
                // Median
                //
                list.Sort();
                int listCount = list.Count;
                if ((listCount & 1) == 1)
                {
                    // ungerade Anzahl
                    int index = list.Count / 2;
                    row1["MedianLow"] = list[index];
                    row1["MedianHigh"] = list[index];
                }
                else
                {
                    // gerade Anzahl
                    // Es sind mindesten 2 Einträge, ist es nur einer ist die Anzahl ungerade
                    int index = list.Count / 2 - 1;
                    row1["MedianLow"] = list[index++];
                    row1["MedianHigh"] = list[index];
                }
            }
        }

        /// <summary>
        /// Man bildet den Durchschnitt über n Operationen:
        /// Durchschnitt = (x1 + x2 + x3 + ... + xn) / n
        /// 
        /// Fehler vorher: einige Zeilen werden zwischendurch zusammengefasst und deren Durchschnitt berechnet:
        ///   (x2 + x3) / 2
        /// 
        /// Das ergab dann insgesamt 
        /// (x1 + ((x2 + x3) / 2) + ... + xn) / n, und da kommt natürlich etwas anderes heraus!
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="nID_Chirurgen"></param>
        /// <param name="nID_OPFunktionen"></param>
        private void PopulateTest(OplListView lv, int nID_Chirurgen, int nID_OPFunktionen, int quelle)
        {
            bool bDurchschnitt = radJahr.Checked || radMonate.Checked;
            DateTime? dtVon;
            DateTime? dtBis;
            int nMonate = 12;
            TimeSpan gesamtDurchschnitt = new TimeSpan(0);

            Abort = false;

            GetVonBisDatum(txtDatumVon, txtDatumBis, out dtVon, out dtBis, true);

            if (radMonate.Checked)
            {
                Int32.TryParse(txtMonate.Text, out nMonate);
            }

            XableAllButtonsForLongOperation(cmdAbort, false);

            DataView dv = BusinessLayer.GetOPZeiten(nID_Chirurgen, nID_OPFunktionen, dtVon, dtBis,
                quelle, txtOPSKode.Text, "[OPS-Kode], Datum");

            //
            // initialCount  ist Anzahl aller Operationen mit mehrfachen OPSKodes
            //
            int initialCount = dv.Table.Rows.Count;

            InitTest(lvTest, bDurchschnitt);

            if (bDurchschnitt)
            {
                DurchschnittProMonate(dv.Table, dtVon, nMonate);
            }

            //
            // Summe über die Dauer von-bis von allen Operationen
            //
            lv.Items.Clear();
            lv.BeginUpdate();
            int i = 0;
            foreach (DataRow row in dv.Table.Rows)
            {
                ListViewItem lvi = null;

                i++;
                if (i % BusinessLayer.AbortLoopCount == 0)
                {
                    if (bDurchschnitt)
                    {
                        SetGroupBoxText(lv, grpTest, GetText("grpDauerDurchschnitt"));
                    }
                    else
                    {
                        SetGroupBoxText(lv, grpTest,  GetText("grpDauer"));
                    }
                    Application.DoEvents();
                }
                if (Abort)
                {
                    break;
                }

                if (bDurchschnitt)
                {
                    TimeSpan ts;
                    String temp;

                    // Es wurden ein paar Zeilen zu einer zusammengefasst mit einem eigenen Wert.
                    // Für die Berechnung des Gesmatdurchschnitts braucht man die Gesamtzeit
                    lvi = new ListViewItem(Tools.DBNullableDateTime2SortableDateString(row["DurchschnittBeginn"]));
                    lvi.SubItems.Add(Tools.DBNullableDateTime2SortableDateString(row["DurchschnittEnde"]));
                    lvi.SubItems.Add(row["Anzahl"].ToString());

                    ts = (TimeSpan)row["Durchschnitt"];
                    temp = string.Format("{0:00}:{1:00}", ts.Hours, ts.Minutes);
                    lvi.SubItems.Add(temp);

                    ts = (TimeSpan)row["MedianLow"];
                    temp = string.Format("{0:00}:{1:00}", ts.Hours, ts.Minutes);
                    lvi.SubItems.Add(temp);

                    ts = (TimeSpan)row["MedianHigh"];
                    temp = string.Format("{0:00}:{1:00}", ts.Hours, ts.Minutes);
                    lvi.SubItems.Add(temp);

                    TimeSpan summe = (TimeSpan)row["Summe"];
                    gesamtDurchschnitt = new TimeSpan(gesamtDurchschnitt.Ticks + summe.Ticks);
                }
                else
                {
                    // Jeder Eintrag ist genau eine Operation: Summe von allen bilden und durch die Anzahl teilen
                    lvi = new ListViewItem(Tools.DBNullableDateTime2SortableDateString(row["Datum"]));
                    lvi.SubItems.Add(Tools.DBNullableDateTime2TimeString(row["Zeit"]));
                    lvi.SubItems.Add(Tools.DBNullableDateTime2TimeString(row["ZeitBis"]));
                    TimeSpan dauer = CalculateMinutes(row);
                    string zeitDisplay = string.Format("{0:00}:{1:00}", dauer.Hours, dauer.Minutes);
                    lvi.SubItems.Add(zeitDisplay);
                    gesamtDurchschnitt = new TimeSpan(gesamtDurchschnitt.Ticks + dauer.Ticks);
                }

                lvi.SubItems.Add((string)row["OPS-Kode"]);
                lvi.SubItems.Add((string)row["OPS-Text"]);

                lv.Items.Add(lvi);
            }
            lv.EndUpdate();

            XableAllButtonsForLongOperation(cmdAbort, true);

            if (dv.Table.Rows.Count > 0)
            {
                gesamtDurchschnitt = new TimeSpan(gesamtDurchschnitt.Ticks / initialCount);
            }
            txtGesamt.Text = string.Format("{0:00}:{1:00}", gesamtDurchschnitt.Hours, gesamtDurchschnitt.Minutes);

            if (bDurchschnitt)
            {
                SetGroupBoxText(lv, grpTest, GetText("grpDauerDurchschnitt"));
            }
            else
            {
                SetGroupBoxText(lv, grpTest, GetText("grpDauer"));
            }
        }

        private void cmdVergleich_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                //
                // No wait cursor because we can click 'Abort'
                //
                CheckQuelleCorrectDumbSettings(chkIntern, chkExtern);

                int nID_OPFunktionen = ConvertToInt32(cbChirurgenOPFunktionen.SelectedValue);
                int nID_Chirurgen = ConvertToInt32(cbChirurgen.SelectedValue);
                int quelle = GetInternExternFlag(chkIntern.Checked, chkExtern.Checked);

                PopulateTest(lvTest, nID_Chirurgen, nID_OPFunktionen, quelle);
            }
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
            if (txtMonate.Text.Length > 0)
            {
                int nMonate;
                if (!Int32.TryParse(txtMonate.Text, out nMonate))
                {
                    bSuccess = false;

                    strMsg += GetTextControlInvalid(ExtractControlText(radMonate) + " " + lblMonate.Text);
                }
                else
                {
                    if (nMonate < 1 || nMonate > 12)
                    {
                        bSuccess = false;
                        strMsg += GetTextControlInvalid(ExtractControlText(radMonate) + " " + lblMonate.Text);
                    }
                }
            }
            if (!bSuccess)
            {
                MessageBox(strMsg);
            }

            return bSuccess;
        }

        private void radAlle_CheckedChanged(object sender, EventArgs e)
        {
            txtMonate.Enabled = false;
        }

        private void radJahr_CheckedChanged(object sender, EventArgs e)
        {
            txtMonate.Enabled = false;
        }

        private void radMonate_CheckedChanged(object sender, EventArgs e)
        {
            txtMonate.Enabled = true;
        }

        private void SetInfoText()
        {
            if (!_bIgnoreControlEvents)
            {
                string text = string.Format(CultureInfo.InvariantCulture, GetText("info"), lblOPSKode.Text);
                SetInfoText(lblInfo, text);
            }
        }

        private void lvTest_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_opsKodeIndex != -1)
            {
                string s = lvTest.SelectedItems[0].SubItems[_opsKodeIndex].Text;
                if (s.Length > 5)
                {
                    s = s.Substring(0, 5);
                }
                txtOPSKode.Text = s;
            }
        }

        private void cmdAbort_Click(object sender, EventArgs e)
        {
            Abort = true;
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            ContextMenuStrip cms = CreateContextMenu(ModePrintListViewBrowser | ModePrintListViewBrowserSelectedColumns);
            cms.Show(cmdPrint, new Point(10, 10));
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
            sb.Append(GetText("printOperateur") + ": " + cbChirurgen.Text);
            if (txtOPSKode.Text.Length > 0)
            {
                sb.Append("<br/>");
                sb.Append(string.Format(CultureInfo.InvariantCulture, GetText("printFilter1"), MakeSafeHTML(txtOPSKode.Text)));
            }
            sb.Append("<br/>");
            sb.Append(lblTotal.Text + " " + txtGesamt.Text);

            return sb.ToString();
        }
    }
}


