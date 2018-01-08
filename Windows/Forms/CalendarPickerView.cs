using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Utility;

namespace Windows.Forms
{
    public partial class CalendarPickerView : DatabaseForm
    {
        private DateTime _oDate;

        public CalendarPickerView(string strDate)
        {
            InitializeComponent();
            if (Tools.DateIsValidGermanDate(strDate))
            {
                _oDate = Tools.InputTextDate2DateTime(strDate);
                calendar.SetDate(_oDate);
            }
        }

        public CalendarPickerView() : this( "")
        {
        }

        protected override string GetFormNameForResourceTexts()
        {
            return this.Name;
        }

        public override void DebugPrintControlContents(Control control)
        {
            if (control is TextBox)
            {
                string text = control.Text;

                AppFramework.Debugging.DebugLogging.WriteLine(AppFramework.Debugging.DebugLogging.DebugFlagGuiContents,
                    "Contents of '" + control.Name + "': " + text);
            }
        }

        public override void DebugPrintControlClicked(Control control)
        {
            string text = BuildFullControlName(control);

            AppFramework.Debugging.DebugLogging.WriteLine(AppFramework.Debugging.DebugLogging.DebugFlagGuiCommand,
                "Clicked: " + text);
        }

        public DateTime SelectedDate
        {
            get { return _oDate; }
        }
        
        protected override void OKClicked()
        {
            Control2Object();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            OKClicked();
        }

        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            _oDate = e.Start;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close(); 
        }

        private void CalendarPickerView_Load(object sender, EventArgs e)
        {
            _oDate = calendar.SelectionStart;
        }

        private void txtYearDec_Click(object sender, EventArgs e)
        {
            this.calendar.SetDate(calendar.SelectionStart.AddYears(-1));
        }

        private void cmdYearInc_Click(object sender, EventArgs e)
        {
            this.calendar.SetDate(calendar.SelectionStart.AddYears(1));
        }

        private void txtYearDec10_Click(object sender, EventArgs e)
        {
            this.calendar.SetDate(calendar.SelectionStart.AddYears(-10));
        }

        private void cmdYearInc10_Click(object sender, EventArgs e)
        {
            this.calendar.SetDate(calendar.SelectionStart.AddYears(10));
        }

        protected override void Control2Object()
        {
            _oDate = calendar.SelectionStart;
        }
    }
}