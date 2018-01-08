using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Diagnostics;

using Utility;
using CMaurer.Operationen.AppFramework;
/*
 * http://bytes.com/topic/c-sharp/answers/252488-tooltips-listview-header
 * 
 headerHandle = (IntPtr)SendMessage((int)Handle, LVM_GETHEADER,0,0);

protected override void WndProc(ref Message m)
{
if (m.Msg==WM_NOTIFY )
{
NMHDR nm =(NMHDR) m.GetLParam(typeof(NMHDR));
if (nm.code==HDN_ITEMCHANGEDW)
{
int right=0;
int left=0;
TOOLINFO ti=new TOOLINFO();
ti.cbSize=Marshal.SizeOf(ti);
ti.uFlags=TTF_SUBCLASS;
ti.hWnd=headerHandle;
for (int i=0; i<this.Columns.Count; i++)
{
if (i<this.Columns.Count)
right+=this.Columns[i].Width;
else
right=this.Width;
ti.uID=i;
if (i<this.Columns.Count)
ti.pszText=this.Columns[i].Text;
else
ti.pszText="";
Rectangle rect= new System.Drawing.Rectangle(left,0,right,20);
ti.rect=rect;
// SendMessage(toolTipHandle, TTM_DELTOOLW, 0, ref ti);
SendMessage(toolTipHandle, TTM_ADDTOOLW, 0, ref ti);
left=right;
}
}
}
base.WndProc (ref m);
 * */
namespace Operationen.Weiterbildungzeitraum
{
    public partial class WeiterbildungszeitraumView : OperationenForm
    {
        private const int MaxTimeSpanYears = 50;
        internal const int PanelLeftRightOffset = 50;
        private int SnapOffset = 10;
        public const int SplitterWidth = 15;
        private int _mouseMoveMaxX = 0;
        private int _ID_Chirurgen = -1;

        private System.Collections.Generic.List<HSeparator> _separators = new List<HSeparator>();
        private System.Collections.Generic.List<SnapLine> _snapLines = new List<SnapLine>();

        private Color _snapColor = BusinessLayer.InfoColor;

        public WeiterbildungszeitraumView(BusinessLayer businessLayer, ArrayList args)
            : base(businessLayer)
        {
            _ID_Chirurgen = (int) args[0];

            InitializeComponent();

            SetInfoText(lblInfo, string.Format(GetText("info"), Command_RichtlinienVergleichView));
            AddLinkLabelLink(lblInfo, Command_RichtlinienVergleichView, Command_RichtlinienVergleichView);

            InitDateFields();

            txtDateFrom.DateBox.Enabled = false;
            txtDateTo.DateBox.Enabled = false;

            _bIgnoreControlEvents = true;

            PopulateChirurgen(cbChirurgen, _ID_Chirurgen);

            this.Text = AppTitle(GetText("title"));

            this.DefaultListViewProperties(lvGrid);
            lvGrid.Columns.Add(GetText("nr"), 80);
            lvGrid.Columns.Add(GetText("von"), 120);
            lvGrid.Columns.Add(GetText("bis"), -2);

            LoadUserData();

            _bIgnoreControlEvents = false;
        }

        internal int MouseMoveMaxX
        {
            get { return _mouseMoveMaxX; }
        }

        private bool ValidateDatumVonBis()
        {
            bool success = true;
            string message = EINGABEFEHLER;

            if (txtDateFrom.Text.Length == 0)
            {
                message += GetTextControlMissingText(lblDateFrom);
                success = false;
            }
            if (txtDateFrom.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtDateFrom.Text))
            {
                message += GetTextControlInvalidDate(lblDateFrom);
                success = false;
            }
            if (txtDateTo.Text.Length == 0)
            {
                message += GetTextControlMissingText(lblDateTo);
                success = false;
            }
            if (txtDateTo.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtDateTo.Text))
            {
                message += GetTextControlInvalidDate(lblDateTo);
                success = false;
            }

            if (success)
            {
                DateTime dateFrom;
                DateTime dateTo;

                GetVonBisDatum(txtDateFrom, txtDateTo, out dateFrom, out dateTo);

                if (dateFrom.CompareTo(dateTo) >= 0)
                {
                    message += string.Format(CultureInfo.InvariantCulture, GetText("err_toDateBeforeFromDate"), lblDateTo.Text, txtDateTo.Text, lblDateFrom.Text, txtDateFrom.Text);
                    success = false;
                }
                if (    (dateTo.Subtract(dateFrom).Days >= 365 * MaxTimeSpanYears)
                    ||  (dateFrom.Subtract(dateTo).Days >= 365 * MaxTimeSpanYears))
                {
                    // Mehr als 50 Jahre sind nicht erlaubt
                    message += string.Format(CultureInfo.InvariantCulture, GetText("err_datesTooFarApart"), lblDateFrom.Text, lblDateTo.Text, MaxTimeSpanYears);
                    success = false;
                }

                if (_separators.Count > 0)
                {
                    DateTime dtTemp;

                    dtTemp = _separators[0].Date;

                    if (dateFrom.CompareTo(dtTemp) >= 0)
                    {
                        //
                        // Anfangsdatum darf nicht >= dem ersten Separator sein
                        //
                        string strTemp = Utility.Tools.DateTime2DateString(dtTemp);
                        message += string.Format(CultureInfo.InvariantCulture, GetText("err_fromDateBehindBar"), lblDateFrom.Text, txtDateFrom.Text, strTemp);
                        success = false;
                    }

                    dtTemp = _separators[_separators.Count - 1].Date;
                    if (dateTo.CompareTo(dtTemp) <= 0)
                    {
                        //
                        // Endedatum darf nicht <= dem letzten Separator sein
                        //
                        string strTemp = Utility.Tools.DateTime2DateString(dtTemp);
                        message += string.Format(CultureInfo.InvariantCulture, GetText("err_toDateBeforeBar"), lblDateTo.Text, txtDateTo.Text, strTemp);
                        success = false;
                    }
                }

                if (!success)
                {
                    MessageBox(message);
                    //LoadUserData();
                }
            }

            return success;
        }

        private void FormResized()
        {
            _mouseMoveMaxX = pnlGrid.Width - PanelLeftRightOffset;

            for (int i = 0; i < _separators.Count; i++)
            {
                _separators[i].MouseMoveMaxX = _mouseMoveMaxX;
                SetPixelByDate(_separators[i]);
            }

            CreateSnapLines();
        }

        private void AddSplitter()
        {
            if (_separators.Count < 10)
            {
                if (Tools.DateIsValidGermanDate(txtDateFrom.Text))
                {
                    DateTime date = Tools.InputTextDate2DateTime(txtDateFrom.Text);

                    HSeparator h = new HSeparator(this, _separators.Count, date);
                    h.Left = PanelLeftRightOffset;
                    h.Width = SplitterWidth;
                    h.Name = (_separators.Count + 1).ToString();
                    h.Label.Top = pnlGrid.Top - h.Label.Height;
                    h.MouseMoveMaxX = _mouseMoveMaxX;

                    _separators.Add(h);
                    pnlGrid.Controls.Add(h);
                    grpGrid.Controls.Add(h.Label);
                    grpGrid.Text = _separators.Count + " " + GetText("zeitpunkteVorhanden");

                    ReorderSeparators();
                }
            }
        }

        private void AddSplitter(DateTime date)
        {
                HSeparator h = new HSeparator(this, _separators.Count, date);
                h.Width = SplitterWidth;
                h.Name = (_separators.Count + 1).ToString();
                h.Label.Top = pnlGrid.Top - h.Label.Height;
                h.MouseMoveMaxX = _mouseMoveMaxX;

                _separators.Add(h);
                pnlGrid.Controls.Add(h);
                grpGrid.Controls.Add(h.Label);
                grpGrid.Text = _separators.Count + " " + GetText("zeitpunkteVorhanden");

                SetPixelByDate(h);
                ReorderSeparators();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            AddSplitter();
        }

        /// <summary>
        /// x0 - x1 entspricht y0 - y1
        /// x entspricht dann wieviel y?
        /// </summary>
        private float LinearInterpolation(float x0, float x1, float y0, float y1, float x)
        {
            return y0 + (x - x0) * ((y1 - y0) / (x1-x0));
        }

        private int DateToPixel(DateTime dtFrom, DateTime dtTo, DateTime date)
        {
            int totalDays = dtTo.Subtract(dtFrom).Days;
            int days = date.Subtract(dtFrom).Days;

            return (int)LinearInterpolation(0, totalDays, PanelLeftRightOffset, pnlGrid.Width - PanelLeftRightOffset, days);
        }

        private int DaysToPixel(DateTime dtFrom, DateTime dtTo, int days)
        {
            int totalDays = dtTo.Subtract(dtFrom).Days;

            return (int)LinearInterpolation(0, totalDays, PanelLeftRightOffset, pnlGrid.Width - PanelLeftRightOffset, days);
        }

        internal int SnapToGrid(HSeparator separator, int pixel)
        {
            if (chkSnap.Checked)
            {
                pixel = SnapToDate(separator, pixel);
            }
            else
            {
                SetDateByPixel(separator, pixel);
            }

            separator.Left = pixel;

            return pixel;
        }

        private DateTime AdvanceSnapDate(DateTime date)
        {
            DateTime nextDate = date.AddMonths(1);

            return nextDate;
        }

        internal void SetDateByPixel(HSeparator separator, int pixel)
        {
            DateTime dateFrom;
            DateTime dateTo;

            if (TryGetVonBisDatum(txtDateFrom, txtDateTo, out dateFrom, out dateTo))
            {
                DateTime date = dateFrom;

                int totalDays = dateTo.Subtract(dateFrom).Days;

                int days = (int)LinearInterpolation(PanelLeftRightOffset, pnlGrid.Width - PanelLeftRightOffset, 0, totalDays, pixel);
                separator.Date = dateFrom.AddDays(days);
            }
        }

        private void SetPixelByDate(HSeparator separator)
        {
            DateTime dateFrom;
            DateTime dateTo;

            if (TryGetVonBisDatum(txtDateFrom, txtDateTo, out dateFrom, out dateTo))
            {
                int pixel = DateToPixel(dateFrom, dateTo, separator.Date);
                separator.Left = pixel;
            }
        }

        private int SnapToDate(HSeparator separator, int x)
        {
            int snappedX = x;
            DateTime dateFrom;
            DateTime dateTo;

            if (TryGetVonBisDatum(txtDateFrom, txtDateTo, out dateFrom, out dateTo))
            {
                DateTime date = dateFrom;
                while (date.CompareTo(dateTo) < 0)
                {
                    int pos = DateToPixel(dateFrom, dateTo, date);
                    if (Math.Abs(x - pos) < SnapOffset)
                    {
                        snappedX = pos;
                        separator.Date = date;
                        break;
                    }
                    date = date.AddMonths(1);
                }
            }

            return snappedX;
        }

        private void WeiterbildungszeitraumView_Resize(object sender, EventArgs e)
        {
            FormResized();
        }

        private void RemoveSnapLines()
        {
            for (int i = 0; i < _snapLines.Count; i++)
            {
                pnlGrid.Controls.Remove(_snapLines[i]);
                grpGrid.Controls.Remove(_snapLines[i].Label);
            }

            //
            // Should not be needed...
            // 
            _snapLines.Clear();
        }


        private void CreateSnapLine(int days, int totalDays, DateTime date)
        {
            int x = (int)LinearInterpolation(0, totalDays, PanelLeftRightOffset, pnlGrid.Width - PanelLeftRightOffset, days);

            SnapLine snapDate = new SnapLine(date, x, Height);
            snapDate.Label.ForeColor = _snapColor;

            _snapLines.Add(snapDate);
            pnlGrid.Controls.Add(snapDate);

            snapDate.Label.Left = x - 10;
            snapDate.Label.Top = pnlGrid.Top + pnlGrid.Height;
            snapDate.Label.Text = Tools.DateTime2DateStringYY(date);

            grpGrid.Controls.Add(snapDate.Label);
        }

        private void CreateSnapLines()
        {
            RemoveSnapLines();

            DateTime dateFrom;
            DateTime dateTo;

            if (TryGetVonBisDatum(txtDateFrom, txtDateTo, out dateFrom, out dateTo))
            {
                DateTime date = dateFrom;
                int totalDays = dateTo.Subtract(dateFrom).Days;
                date = date.AddYears(1);

                CreateSnapLine(0, totalDays, dateFrom);

                while (date.CompareTo(dateTo) < 0)
                {
                    int days = date.Subtract(dateFrom).Days;

                    CreateSnapLine(days, totalDays, date);

                    date = date.AddYears(1);
                }
                CreateSnapLine(totalDays, totalDays, dateTo);
            }
        }

        private void WeiterbildungszeitraumView_ResizeEnd(object sender, EventArgs e)
        {
            FormResized();
        }

        private void DeleteAllSeparators()
        {
            //
            // Removing an item changes the index of all other, so we cannot iterate through the index
            //
            while (_separators.Count > 0)
            {
                RemoveSeparator(_separators[0]);
            }
            PopulateListViewGrid();
        }

        private void cmdDeleteAll_Click(object sender, EventArgs e)
        {
            DeleteAllSeparators();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        internal void RemoveSeparator(HSeparator separator)
        {
            grpGrid.Controls.Remove(separator.Label);
            pnlGrid.Controls.Remove(separator);

            Debug.Assert(_separators.Contains(separator));

            _separators.Remove(separator);

            ReorderSeparators();
        }

        internal void ReorderSeparators()
        {
            _separators.Sort();

            int count = 1;
            foreach (HSeparator item in _separators)
            {
                item.Name = count.ToString();
                item.Refresh();
                count++;
            }

            grpGrid.Text = _separators.Count + " " + GetText("zeitpunkteVorhanden");

            PopulateListViewGrid();
        }

        private void PopulateListViewGrid()
        {
            DateTime dateFrom;

            if (TryGetDatum(txtDateFrom, out dateFrom))
            {

                ListViewItem lvi;
                DateTime dtFrom = dateFrom;
                DateTime dtTo;
                lvGrid.Items.Clear();

                if (_separators.Count > 0)
                {
                    int count = 1;
                    foreach (HSeparator separator in _separators)
                    {
                        dtTo = separator.Date.AddDays(-1);

                        lvi = new ListViewItem(count.ToString());
                        lvi.SubItems.Add(Tools.DateTime2DateString(dtFrom));
                        lvi.SubItems.Add(Tools.DateTime2DateString(dtTo));

                        dtFrom = separator.Date;

                        lvGrid.Items.Add(lvi);

                        count++;
                    }

                    dtTo = Tools.InputTextDate2DateTime(txtDateTo.Text);

                    lvi = new ListViewItem(count.ToString());
                    lvi.SubItems.Add(Tools.DateTime2DateString(dtFrom));
                    lvi.SubItems.Add(Tools.DateTime2DateString(dtTo));

                    lvGrid.Items.Add(lvi);
                }
            }
        }

        private void DateChanged()
        {
            if (ValidateDatumVonBis())
            {
                for (int i = 0; i < _separators.Count; i++)
                {
                    SetPixelByDate(_separators[i]);
                }
                CreateSnapLines();
            }
        }


        #region UserDataStorage
        private void SaveUserData()
        {
            if (_separators.Count > 0)
            {
                List<DateTime> wbzr = new List<DateTime>();

                wbzr.Add(Tools.InputTextDate2DateTime(txtDateFrom.Text));
                for (int i = 0; i < _separators.Count; i++)
                {
                    wbzr.Add(_separators[i].Date);
                }
                wbzr.Add(Tools.InputTextDate2DateTime(txtDateTo.Text));
                wbzr.Sort();

                BusinessLayer.SaveUserSettings(GlobalConstants.SectionWbzr, GlobalConstants.KeyWbzrCount, wbzr.Count.ToString());

                // 
                // _separators
                //
                for (int i = 0; i < wbzr.Count; i++)
                {
                    string strI = (1 + i).ToString();
                    string strDate = Tools.DateTime2DateString(wbzr[i]);
                    BusinessLayer.SaveUserSettings(GlobalConstants.SectionWbzr, strI, strDate);
                }
            }
            else
            {
                //
                // Delete all separators
                //
                BusinessLayer.DeleteUserSettings(GlobalConstants.SectionWbzr);

                List<DateTime> wbzr = new List<DateTime>();

                wbzr.Add(Tools.InputTextDate2DateTime(txtDateFrom.Text));
                for (int i = 0; i < _separators.Count; i++)
                {
                    wbzr.Add(_separators[i].Date);
                }
                wbzr.Add(Tools.InputTextDate2DateTime(txtDateTo.Text));
                wbzr.Sort();

                BusinessLayer.SaveUserSettings(GlobalConstants.SectionWbzr, GlobalConstants.KeyWbzrCount, wbzr.Count.ToString());

                // 
                // _separators
                //
                for (int i = 0; i < wbzr.Count; i++)
                {
                    string strI = (1 + i).ToString();
                    string strDate = Tools.DateTime2DateString(wbzr[i]);
                    BusinessLayer.SaveUserSettings(GlobalConstants.SectionWbzr, strI, strDate);
                }
            }

            LoadUserData();
        }

        private void InitDateFields()
        {
            txtDateFrom.Text = Tools.DateTime2DateString(new DateTime(DateTime.Today.Year, 1, 1));
            txtDateTo.Text = Tools.DateTime2DateString(new DateTime(DateTime.Today.Year + 3, 12, 31));
        }

        private void LoadUserData()
        {
            DeleteAllSeparators();

            List<DateTime> wbzr = BusinessLayer.GetWeiterbildungsZeitraeume(_ID_Chirurgen);

            if (wbzr.Count > 0)
            {
                string strDate = Tools.DateTime2DateString(wbzr[0]);
                txtDateFrom.Text = strDate;
                strDate = Tools.DateTime2DateString(wbzr[wbzr.Count - 1]);
                txtDateTo.Text = strDate;

                for (int i = 1; i < wbzr.Count - 1; i++)
                {
                    DateTime date = wbzr[i];
                    AddSplitter(date);
                }
            }
            else
            {
                InitDateFields();
            }

            ReorderSeparators();

            grpGrid.Text = _separators.Count + " " + GetText("zeitpunkteVorhanden");
            FormResized();

            CreateSnapLines();
        }

        #endregion
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (ValidateDatumVonBis())
            {
                XableAllButtonsForLongOperation(null, false);
                SaveUserData();
                XableAllButtonsForLongOperation(null, true);
            }
        }

        private void ChirurgChanged()
        {
            _ID_Chirurgen = ConvertToInt32(cbChirurgen.SelectedValue);
            LoadUserData();
        }

        private void cbChirurgen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_bIgnoreControlEvents)
            {
                ChirurgChanged();
            }
        }

        private void lblInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OperationenLogbuchView.TheMainWindow.StartMenuItem(e.Link.LinkData);
        }

        private void txtDateFrom_Leave(object sender, EventArgs e)
        {
            DateChanged();
        }

        private void txtDateTo_Leave(object sender, EventArgs e)
        {
            DateChanged();
        }

        private void txtDateFrom_DateChanged(object sender, EventArgs e)
        {
            DateChanged();
        }

        private void txtDateTo_DateChanged(object sender, EventArgs e)
        {
            DateChanged();
        }

        private void WeiterbildungszeitraumView_Load(object sender, EventArgs e)
        {

        }
    }
}
