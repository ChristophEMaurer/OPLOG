using System;
//using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Globalization;

using Utility;
using Windows.Forms;

namespace Operationen
{
    public partial class NotizenView : OperationenForm
    {
        private int _printID_Chirurgen = -1;
        private DataView _dataView;
        private DataRow _chirurg;
        private DataRow _notiz;

        public NotizenView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();
        }

        private void InitNotizen()
        {
            DefaultListViewProperties(lvNotizen);

            lvNotizen.Columns.Add(GetText("beginn"), 80, HorizontalAlignment.Left);
            lvNotizen.Columns.Add(GetText("ende"), 80, HorizontalAlignment.Left);
            lvNotizen.Columns.Add(GetText("art"), 100, HorizontalAlignment.Left);
            lvNotizen.Columns.Add(GetText("vermerk"), -2, HorizontalAlignment.Left);
        }

        private void PopulateNotizen()
        {
            cmdApply.Enabled = false;
            ClearTextBoxes();

            int ID_Chirurgen = -1;

            if (cbFilterChirurgen.Items.Count > 0)
            {
                ID_Chirurgen = ConvertToInt32(cbFilterChirurgen.SelectedValue);
            }

            lvNotizen.Items.Clear();
            if (ID_Chirurgen != -1)
            {
                DataView dataview = null;

                dataview = BusinessLayer.GetNotizen(ID_Chirurgen);

                if (dataview != null)
                {
                    lvNotizen.Items.Clear();
                    lvNotizen.BeginUpdate();
                    foreach (DataRow dataRow in dataview.Table.Rows)
                    {
                        ListViewItem lvi = new ListViewItem(Tools.DBNullableDateTime2DateString(dataRow["Datum"]));
                        lvi.Tag = ConvertToInt32(dataRow["ID_Notizen"]);
                        lvi.SubItems.Add(Tools.DBNullableDateTime2DateString(dataRow["Ende"]));
                        lvi.SubItems.Add((string)dataRow["Text"]);
                        lvi.SubItems.Add((string)dataRow["Notiz"]);

                        lvNotizen.Items.Add(lvi);
                    }
                    lvNotizen.EndUpdate();
                }
            }
        }

        private void PopulateNotizTypen()
        {
            // Zum Eingeben: ohne "alle"
            DataView dv = BusinessLayer.GetNotizTypen(false);
            cbNotizTypen.ValueMember = "ID_NotizTypen";
            cbNotizTypen.DisplayMember = "Text";
            cbNotizTypen.DataSource = dv;
        }

        private void NotizenView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));

            SetInfoText(lblInfo, string.Format(CultureInfo.InvariantCulture, GetText("info"), Command_NotizTypenView));

            if (UserHasRight("NotizTypenView.view"))
            {
                AddLinkLabelLink(lblInfo, Command_NotizTypenView, Command_NotizTypenView);
            }

            _bIgnoreControlEvents = true;

            PopulateChirurgen(cbFilterChirurgen);
            PopulateNotizTypen();
            InitNotizen();

            _bIgnoreControlEvents = false;

            PopulateNotizen();
        }

        protected override bool ValidateInput()
        {
            bool bSuccess = true;
            string strMessage = EINGABEFEHLER;

            if (txtNotiz.Text.Length <= 0)
            {
                strMessage += GetTextControlMissingText(lblNotiz);
                bSuccess = false;
            }
            if (txtBeginn.Text.Length <= 0)
            {
                strMessage += GetTextControlMissingText(lblBeginn);
                bSuccess = false;
            }
            else
            {
                if (!Tools.DateIsValidGermanDate(txtBeginn.Text))
                {
                    strMessage += GetTextControlInvalidDate(lblBeginn);
                    bSuccess = false;
                }
            }
            if (txtEnde.Text.Length > 0)
            {
                if (!Tools.DateIsValidGermanDate(txtEnde.Text))
                {
                    strMessage += GetTextControlInvalidDate(lblEnde);
                    bSuccess = false;
                }
            }
            if (cbNotizTypen.SelectedIndex == -1)
            {
                strMessage += GetTextItemNeedsSelection(GetText("vermerkart"));
                bSuccess = false;
            }

            if (!bSuccess)
            {
                MessageBox(strMessage);
            }

            return bSuccess;
        }

        protected override void Control2Object()
        {
            _notiz["Datum"] = Tools.InputTextDate2NullableDatabaseDateTime(txtBeginn.Text);
            _notiz["Ende"] = Tools.InputTextDate2NullableDatabaseDateTime(txtEnde.Text);
            _notiz["Notiz"] = txtNotiz.Text;
            _notiz["ID_NotizTypen"] = ConvertToInt32(cbNotizTypen.SelectedValue);
            _notiz["ID_Chirurgen"] = ConvertToInt32(cbFilterChirurgen.SelectedValue);
        }

        protected override void Object2Control()
        {
            txtBeginn.Text = Tools.DBNullableDateTime2DateString(_notiz["Datum"]);
            txtEnde.Text = Tools.DBNullableDateTime2DateString(_notiz["Ende"]);
            txtNotiz.Text = (string)_notiz["Notiz"];
            cbNotizTypen.SelectedValue = ConvertToInt32(_notiz["ID_NotizTypen"]);
        }

        private void ClearTextBoxes()
        {
            txtBeginn.Text = "";
            txtEnde.Text = "";
            txtNotiz.Text = "";
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                _notiz = BusinessLayer.CreateDataRowNotiz();

                Control2Object();
                BusinessLayer.InsertNotiz(_notiz);
                PopulateNotizen();
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            int nCount = lvNotizen.SelectedItems.Count;

            if (nCount > 0)
            {
                string msg = GetTextConfirmDelete(nCount);
                if (Confirm(msg))
                {
                    Cursor = Cursors.WaitCursor;

                    foreach (ListViewItem lvi in lvNotizen.SelectedItems)
                    {
                        int nID = (int)lvi.Tag;

                        if (nID != -1)
                        {
                            BusinessLayer.DeleteNotiz(nID);
                        }
                    }
                    PopulateNotizen();

                    Cursor = Cursors.Default;
                }
            }
            else
            {
                MessageBox(GetTextSelectionNone());
            }
        }

        private void lvNotizen_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID_Notizen = (int)GetFirstSelectedTag(lvNotizen);

            if (ID_Notizen != -1)
            {
                _notiz = BusinessLayer.GetNotiz(ID_Notizen);
                Object2Control();
                cmdApply.Enabled = true;
            }
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Control2Object();
                if (BusinessLayer.UpdateNotiz(_notiz))
                {
                    PopulateNotizen();
                }
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }

        private void NotizTypChanged()
        {
            if (!_bIgnoreControlEvents)
            {
                PopulateNotizen();
            }
        }

        private void cbNotizTypen_SelectedIndexChanged(object sender, EventArgs e)
        {
            NotizTypChanged();
        }

        private void cbChirurgen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_bIgnoreControlEvents)
            {
                PopulateNotizen();
            }
        }

        private void lblInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OperationenLogbuchView.TheMainWindow.StartMenuItem(e.Link.LinkData);
        }

        private void lblVermerkart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OperationenLogbuchView.TheMainWindow.StartMenuItem(e.Link.LinkData);
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            ContextMenuStrip cms = CreateContextMenu(ModePrintForm | ModePrintListViewBrowser | ModePrintListViewBrowserSelectedColumns);
            cms.Show(cmdPrint, new Point(10, 10));
        }

        protected override void PrintListViewBrowser_Click(object sender, EventArgs e)
        {
            PrintListView(lvNotizen, "");
        }

        protected override void PrintListViewBrowserSelectedColumns_Click(object sender, EventArgs e)
        {
            PrintListViewSelectedColumns(lvNotizen, "");
        }

        protected override void PrintForm_Click(object sender, EventArgs e)
        {
            int ID_Chirurgen = -1;

            _printID_Chirurgen = -1;

            if (cbFilterChirurgen.Items.Count > 0)
            {
                ID_Chirurgen = ConvertToInt32(cbFilterChirurgen.SelectedValue);
                if (ID_Chirurgen != -1)
                {
                    _printID_Chirurgen = ID_Chirurgen;
                    PrintForm();
                }
            }
        }

        #region print

        override protected void PrintDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            _currentPageIndex = 1;

            _chirurg = BusinessLayer.GetChirurg(_printID_Chirurgen);
            _dataView = BusinessLayer.GetNotizen(_printID_Chirurgen);

            _dataRowIndex = 0;
        }

        override protected void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Set the page margins
            Rectangle rPageMargins = new Rectangle(e.MarginBounds.Location, e.MarginBounds.Size);

            // Make sure nothing gets printed in the margins
            e.Graphics.SetClip(rPageMargins);

            PrintAll(e);
        }

        override protected void PrintDocument_EndPrint(object sender, PrintEventArgs e)
        {
            _currentPageIndex = 1;
            _dataView = null;
        }

        private string ForceDateString(object o)
        {
            string s = "   ---    ";

            if (o != DBNull.Value)
            {
                s = Tools.DBNullableDateTime2DateString(o);
            }

            return s;
        }

        override protected void PrintAll(PrintPageEventArgs ev)
        {
            int nLine;
            string line = null;

            // Calculate the number of lines per page.
            int nLinesPerPage = Convert.ToInt32(ev.MarginBounds.Height / _printFont.GetHeight(ev.Graphics));

            nLine = 1;
            line = BusinessLayer.AppAndVersionStringForPrinting;
            PrintLine(ev, nLine++, line);

            line = GetTextPrintHeader(DateTime.Now, _currentPageIndex);
            PrintLine(ev, nLine++, line);

            line = string.Format(CultureInfo.InvariantCulture, GetText("printFilter1"),
                    _chirurg["Nachname"].ToString(), _chirurg["Vorname"].ToString());

            PrintLine(ev, nLine++, line);

            nLine++;
            nLine++;

            line = GetText("printFilter2");
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

                line = String.Format("{0,4:G}", _dataRowIndex + 1) + ": "
                    + ForceDateString(dataRow["Datum"]) + " "
                    + ForceDateString(dataRow["Ende"]) + " "
                    + (string)dataRow["Text"] + " "
                    + Tools.MultipleLineText2SingleLineText((string)dataRow["Notiz"]);
                PrintLine(ev, nLine++, line);
            }

            _currentPageIndex++;
            ev.HasMorePages = _dataRowIndex < _dataView.Table.Rows.Count;
        }


        #endregion
    }
}

