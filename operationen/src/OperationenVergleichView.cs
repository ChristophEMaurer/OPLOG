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
    public partial class OperationenVergleichView : OperationenForm
    {
        private int _clickedColumnIndex;
        List<string> _opsCodes;

        public OperationenVergleichView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();
        }

        private void OperationenVergleichView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));
            SetInfoText(lblInfo, string.Format(GetText("info"), cmdSaveOpsCodes.Text, grpOperationen.Text, lblOPS.Text));

            cmdAbort.Enabled = false;

            InitializeListViews();
            InitializeListViewData();

            LoadOpsCodes();

            entfernenToolStripMenuItem.Text = GetText("context_delete");
            abbrechenToolStripMenuItem.Text = GetText("context_cancel");

            PopulateOPFunktionen(cbChirurgenOPFunktionen, true);
            CheckQuelleDefaultSettings(chkIntern, chkExtern);
        }

        private void LoadOpsCodes()
        {
            _opsCodes = new List<string>();

            DataView dv = BusinessLayer.GetUserSettings(BusinessLayer.CurrentUser_ID_Chirurgen, GlobalConstants.SectionAuswertungenOpsCode);

            foreach (DataRow row in dv.Table.Rows)
            {
                string opsCode = (string) row["Key"];
                AddColumn(opsCode);
            }
        }

        private void InitializeListViews()
        {
            lvOperationen.Clear();
            DefaultListViewProperties(lvOperationen);
            lvOperationen.Columns.Add(GetText("colOpscode"), 80, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("colOpstext"), -2, HorizontalAlignment.Left);
        }

        private void InitializeListViewData()
        {
            lvData.Clear();
            DefaultListViewProperties(lvData);
            SetWatermark(lvData);

            lvData.Columns.Add(GetText("colName"), 180, HorizontalAlignment.Left);

            ListViewItem lvi = new ListViewItem("");
            lvi.Tag = -1;
            lvData.Items.Add(lvi);

            DataView dvChirurgen = BusinessLayer.GetChirurgen();

            foreach (DataRow chirurg in dvChirurgen.Table.Rows)
            {
                string name = chirurg["Nachname"] + ", " + chirurg["Vorname"];
                int ID_Chirurgen = (int)chirurg["ID_Chirurgen"];

                lvi = new ListViewItem(name);
                lvi.Tag = ID_Chirurgen;
                lvData.Items.Add(lvi);
            }
        }

        /// <summary>
        /// In der ersten Zeile stehen die OPSTexte der OPSCodes.
        /// </summary>
        /// <param name="opsKode">der opscode, zu dem der OPSText gesucht wird</param>
        /// <param name="index">0-based index in die listvie columns</param>
        private void SetMatchingOpsText(string opsKode, int index)
        {
            string text = GetMatchingOpsText(opsKode);

            lvData.Items[0].SubItems[index].Text = text;
        }

        private bool ValidateDatumVonBis()
        {
            bool bSuccess = true;
            string strMessage = EINGABEFEHLER;

            if (txtDatumVon.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtDatumVon.Text))
            {
                strMessage += string.Format(CultureInfo.InvariantCulture, GetText(FormName, "controlBadFormat"), lblDatumVon.Text);
                bSuccess = false;
            }
            if (txtDatumBis.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtDatumBis.Text))
            {
                strMessage += string.Format(CultureInfo.InvariantCulture, GetText(FormName, "controlBadFormat"), lblDatumBis.Text);
                bSuccess = false;
            }

            if (!bSuccess)
            {
                MessageBox(strMessage);
            }

            return bSuccess;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void PopulateData()
        {
            if (ValidateDatumVonBis())
            {
                for (int i = 0; i < _opsCodes.Count; i++)
                {
                    Application.DoEvents();
                    if (Abort)
                    {
                        break;
                    }
                    CalculateColumn(i);
                }
            }
        }

        private void AddColumn(string opsCode)
        {
            _opsCodes.Add(opsCode);
            lvData.Columns.Add(opsCode, 100, HorizontalAlignment.Left);

            foreach (ListViewItem lvi in lvData.Items)
            {
                lvi.SubItems.Add("");
            }

            if (string.IsNullOrEmpty(lvData.Items[0].Text))
            {
                lvData.Items[0].Text = GetText("autoText");
            }

            SetMatchingOpsText(opsCode, lvData.Columns.Count - 1);
        }

        /// <summary>
        /// Remove one column
        /// </summary>
        /// <param name="index">0-based index into the columns</param>
        private void RemoveColumn(int index)
        {
            lvData.Columns.RemoveAt(index);
            _opsCodes.RemoveAt(index - 1);

            foreach (ListViewItem lvi in lvData.Items)
            {
                lvi.SubItems.RemoveAt(index);
            }
        }


        /// <summary>
        /// Calculate the count-data for one column.
        /// </summary>
        /// <param name="index">0-based index into array _opsCodes</param>
        private void CalculateColumn(int index)
        {
            if (ValidateDatumVonBis())
            {
                DateTime? von;
                DateTime? bis;

                GetVonBisDatum(txtDatumVon, txtDatumBis, out von, out bis);

                int quelle = GetInternExternFlag(chkIntern.Checked, chkExtern.Checked);
                int ID_OPFunktionen = ConvertToInt32(cbChirurgenOPFunktionen.SelectedValue);

                int i = 0;
                foreach (ListViewItem lvi in lvData.Items)
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

                    int ID_Chirurgen = (int)lvi.Tag;
                    if (ID_Chirurgen != -1)
                    {
                        string opsCode = _opsCodes[index];

                        //
                        // Get all operations of one surgeon for this one code
                        //
                        long count = BusinessLayer.GetIstOperationenCountForChirurg(ID_Chirurgen, von, bis, opsCode, quelle, ID_OPFunktionen);
                        lvi.SubItems[index + 1].Text = count.ToString();
                    }
                }
            }
        }

        private void cmdAnzeigen_Click(object sender, EventArgs e)
        {
            XableAllButtonsForLongOperation(cmdAbort, false);

            CheckQuelleCorrectDumbSettings(chkIntern, chkExtern);
            PopulateData();

            XableAllButtonsForLongOperation(cmdAbort, true);
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtDatumVon.Text = "";
            txtDatumBis.Text = "";
        }

        protected override string PrintHTMLFilter()
        {
            return PrintHTMLFilter(txtDatumVon.Text, txtDatumBis.Text, chkIntern.Checked, chkExtern.Checked, cbChirurgenOPFunktionen.Text);
        }
        
        private void cmdPrint_Click(object sender, EventArgs e)
        {
            ContextMenuStrip cmsPrint = CreateContextMenu(ModePrintListViewBrowser | ModePrintListViewBrowserSelectedColumns);
            cmsPrint.Show(cmdPrint, new Point(10, 10));
        }

        protected override void PrintListViewBrowser_Click(object sender, EventArgs e)
        {
            PrintListView(lvData, GlobalConstants.KeyPrintLinesOperationenVergleichView, null, 0, 0);
        }

        protected override void PrintListViewBrowserSelectedColumns_Click(object sender, EventArgs e)
        {
            PrintListViewSelectedColumns(lvData, GlobalConstants.KeyPrintLinesOperationenVergleichView);
        }

        private void cmdOPSCode_Click(object sender, EventArgs e)
        {
            if (!_bIgnoreControlEvents)
            {
                _bIgnoreControlEvents = true;
                PopulateListOperationen(txtOperation.Text, 0, 50);
                _bIgnoreControlEvents = false;
            }
        }

        private void PopulateListOperationen(string ops, int requiredMinimumOpsLength, int top)
        {
            lvOperationen.Items.Clear();

            if (ops != null && ops.Length > requiredMinimumOpsLength)
            {
                DataView dv = BusinessLayer.SuggestGetOPSKodeTextForKodeOrText(ops, top);

                lvOperationen.BeginUpdate();
                foreach (DataRow row in dv.Table.Rows)
                {
                    ListViewItem lvi = new ListViewItem((string)row["OPSKode"]);
                    lvi.SubItems.Add((string)row["OPSText"]);
                    lvOperationen.Items.Add(lvi);
                }
                lvOperationen.EndUpdate();
            }
        }

        /// <summary>
        /// Add one column to the list box with the specified ops code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAddOPSCode_Click(object sender, EventArgs e)
        {
            string text = txtOperation.Text;

            if (!string.IsNullOrEmpty(text) && "1234567890".IndexOf(text[0]) >= 0)
            {
                //
                // Im Textfeld muss ein OPSKode stehen, das ist aber schwierig festzustellen:
                // Der erste Buchstabe muss eine Ziffer 0123456789 sein.
                //
                XableAllButtonsForLongOperation(null, false);

                AddColumn(txtOperation.Text);
                CalculateColumn(_opsCodes.Count - 1);

                XableAllButtonsForLongOperation(null, true);
            }
            else
            {
                string msg = GetText("errorNoOpscode");
                MessageBox(msg);
            }
        }

        private void entfernenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveColumn(_clickedColumnIndex);
        }

        private void lvData_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ColumnClickEventArgs ev = (ColumnClickEventArgs)e;
            _clickedColumnIndex = ev.Column;

            if (_clickedColumnIndex > 0)
            {
                int left = 0;
                for (int i = 0; i < _clickedColumnIndex; i++)
                {
                    left += lvData.Columns[i].Width;
                }
                contextMenuColumn.Show(lvData, new Point(left, 20));
            }
        }

        private void abbrechenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // do nothing
        }

        private void lvData_Click(object sender, EventArgs e)
        {
        }

        private void cmdSaveOpsCodes_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            BusinessLayer.DeleteUserSettings(BusinessLayer.CurrentUser_ID_Chirurgen, GlobalConstants.SectionAuswertungenOpsCode);
            foreach (string opsCode in _opsCodes)
            {
                BusinessLayer.InsertUserSettings(BusinessLayer.CurrentUser_ID_Chirurgen, GlobalConstants.SectionAuswertungenOpsCode, opsCode, "1");
            }

            Cursor = Cursors.Default;
        }

        private void lvOperationen_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem lvi = GetFirstSelectedLVI(lvOperationen);
            if (lvi != null)
            {
                _bIgnoreControlEvents = true;

                string code = lvi.SubItems[0].Text;
                int index = code.IndexOf(".");
                if (index > 0)
                {
                    code = code.Substring(0, index);
                }
                txtOperation.Text = code;

                _bIgnoreControlEvents = false;
            }
        }

        private void cmdAbort_Click(object sender, EventArgs e)
        {
            Abort = true;
        }
    }
}

