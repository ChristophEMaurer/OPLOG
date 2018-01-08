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
    public partial class OperationenView : OperationenForm
    {
        public OperationenView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();

            cmdCancel.Enabled = false;
        }

        private void OperationenView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));

            InitializeListViewOperationen();

            PopulateOPFunktionen(cbChirurgenOPFunktionen, true);

            CheckQuelleDefaultSettings(chkIntern, chkExtern);
        }

        private void InitializeListViewOperationen()
        {
            DefaultListViewProperties(lvOperationen);
            SetWatermark(lvOperationen);

            lvOperationen.Columns.Add(GetText("fallzahl"), 80, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("datum"), 80, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("von"), 50, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("bis"), 50, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("name"), 80, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("vorname"), 80, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("funktion"), 80, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("klinische"), 120, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("ergebnisse"), 120, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("opskode"), 80, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("bezeichnung"), -2, HorizontalAlignment.Left);
        }

        private bool ValidateDatumVonBis()
        {
            bool bSuccess = true;
            string strMessage = EINGABEFEHLER;

            if (txtDatumVon.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtDatumVon.Text))
            {
                strMessage += GetTextControlInvalid(lblDatumVon);
                bSuccess = false;
            }
            if (txtDatumBis.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtDatumBis.Text))
            {
                strMessage += GetTextControlInvalid(lblDatumBis); 
                bSuccess = false;
            }

            if (!bSuccess)
            {
                MessageBox(strMessage);
            }

            return bSuccess;
        }

        private void FillListViewOperationen(DateTime? von, DateTime? bis, string ops, int quelle, int ID_OPFunktionen)
        {
            DataView dataview = BusinessLayer.GetIstOperationen(von, bis, ops, quelle, ID_OPFunktionen);

            lvOperationen.Items.Clear();
            lvOperationen.BeginUpdate();

            int i = 0;
            foreach (DataRow dataRow in dataview.Table.Rows)
            {
                i++;
                if (i % BusinessLayer.AbortLoopCount == 0)
                {
                    //
                    // Update something but not the listview, this causes excessive flickering
                    //
                    SetGroupBoxText(lvOperationen, grpOperationen, GetText("prozeduren"));
                    Application.DoEvents();
                }
                if (Abort)
                {
                    break;
                }

                ListViewItem lvi = new ListViewItem((string)dataRow["Fallzahl"]);
                lvi.Tag = ConvertToInt32(dataRow["ID_ChirurgenOperationen"]);
                lvi.ToolTipText = "[" + (string)dataRow["OPSKode"] + "] " + (string)dataRow["OPSText"];
                lvi.SubItems.Add(Tools.DBNullableDateTime2SortableDateString(dataRow["Datum"]));
                lvi.SubItems.Add(Tools.DBNullableDateTime2TimeString(dataRow["Zeit"]));
                lvi.SubItems.Add(Tools.DBNullableDateTime2TimeString(dataRow["ZeitBis"]));
                lvi.SubItems.Add(Tools.DBNullableString2String(dataRow["Nachname"]));
                lvi.SubItems.Add(Tools.DBNullableString2String(dataRow["Vorname"]));
                lvi.SubItems.Add((string)dataRow["Beschreibung"]);
                lvi.SubItems.Add((string)dataRow["KlinischeErgebnisseTyp"]);
                lvi.SubItems.Add((string)dataRow["KlinischeErgebnisse"]);
                lvi.SubItems.Add((string)dataRow["OPSKode"]);
                lvi.SubItems.Add((string)dataRow["OPSText"]);
                lvOperationen.Items.Add(lvi);
            }
            lvOperationen.EndUpdate();

            SetGroupBoxText(lvOperationen, grpOperationen, "Prozeduren");
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void PopulateOperationen()
        {
            Abort = false;

            if (ValidateDatumVonBis())
            {
                DateTime? von;
                DateTime? bis;

                GetVonBisDatum(txtDatumVon, txtDatumBis, out von, out bis);

                bool readData = true;
                int quelle = GetInternExternFlag(chkIntern.Checked, chkExtern.Checked);
                int ID_OPFunktionen = ConvertToInt32(cbChirurgenOPFunktionen.SelectedValue);

                long count = BusinessLayer.GetIstOperationenCount(von, bis, txtFilterOPS.Text, quelle, ID_OPFunktionen);

                if (count > 5000)
                {
                    string msg = string.Format(CultureInfo.InvariantCulture, GetText("confirmMany"), count);
                    if (!Confirm(msg))
                    {
                        readData = false;
                    }
                }
                if (readData)
                {

                    //
                    // No wait cursor because we can click 'Cancel'
                    //

                    //Cursor = Cursors.WaitCursor;
                    XableAllButtonsForLongOperation(cmdCancel, false);

                    FillListViewOperationen(von, bis, txtFilterOPS.Text, quelle, ID_OPFunktionen);

                    //Cursor = Cursors.Default;
                    XableAllButtonsForLongOperation(cmdCancel, true);
                }
            }
        }

        private void cmdAnzeigen_Click(object sender, EventArgs e)
        {
            CheckQuelleCorrectDumbSettings(chkIntern, chkExtern);

            PopulateOperationen();
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtDatumVon.Text = "";
            txtDatumBis.Text = "";
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            base.SearchOPSKodeOrOPSText(lvOperationen, 9, txtSearchOPS.Text, 10, txtSearchOPS.Text);
        }

        protected override string PrintHTMLFilter()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(GetDateVonBisHTMLFilterText(txtDatumVon.Text, txtDatumBis.Text));
            sb.Append(GetQuelleInternExternText(chkIntern.Checked, chkExtern.Checked));
            if (txtFilterOPS.Text.Length > 0)
            {
                sb.Append("<br/>");
                string text = string.Format(CultureInfo.InvariantCulture, GetText("printFilter1"), MakeSafeHTML(txtFilterOPS.Text));
                sb.Append(text);
            }

            return sb.ToString();
        }
        
        private void cmdPrint_Click(object sender, EventArgs e)
        {
            ContextMenuStrip cms = CreateContextMenu(ModePrintListViewBrowser | ModePrintListViewBrowserSelectedColumns);
            cms.Show(cmdPrint, new Point(10, 10));
        }

        protected override void PrintListViewBrowser_Click(object sender, EventArgs e)
        {
            PrintListView(lvOperationen, GlobalConstants.KeyPrintLinesOperationenView);
        }

        protected override void PrintListViewBrowserSelectedColumns_Click(object sender, EventArgs e)
        {
            PrintListViewSelectedColumns(lvOperationen, GlobalConstants.KeyPrintLinesOperationenView);
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Abort = true;
        }
    }
}

