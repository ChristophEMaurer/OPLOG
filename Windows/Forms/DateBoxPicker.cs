using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Utility;

namespace Windows.Forms
{
    public partial class DateBoxPicker : UserControl
    {
        public delegate void DateChangedCallback(object sender, EventArgs e);

        [EditorBrowsable(EditorBrowsableState.Always)]
        public event DateChangedCallback DateChanged;

        public DateBoxPicker()
        {
            InitializeComponent();
        }

        protected void OnDateChangedEvent(EventArgs e)
        {
            if (DateChanged != null)
            {
                DateChanged(this, e);
            }
        }

        private void cmdDate_Click(object sender, EventArgs e)
        {
            Windows.Forms.CalendarPickerView dlg = new Windows.Forms.CalendarPickerView(txtDate.Text);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                txtDate.Text = Tools.DBNullableDateTime2DateString(dlg.SelectedDate);
                OnDateChangedEvent(null);
            }
        }
        public override string Text
        {
            set { txtDate.Text = value; }
            get { return txtDate.Text; }
        }

        public void Clear()
        {
            txtDate.Clear();
        }

        public TextBox DateBox
        {
            get { return txtDate; }
        }
    }
}
