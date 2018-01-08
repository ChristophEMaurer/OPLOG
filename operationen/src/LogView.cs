using System;
using System.Data;
using System.Windows.Forms;
using System.Globalization;

using Utility;
using Windows.Forms;

namespace Operationen
{
    public partial class LogView : OperationenForm
    {
        public LogView(BusinessLayer businessLayer) :
            base(businessLayer)
        {
            InitializeComponent();

            cmdDelete.SetSecurity(BusinessLayer, "LogView.cmdDelete");
            cmdDeleteList.SetSecurity(BusinessLayer, "LogView.cmdDelete");

            this.Text = AppTitle(GetText("title"));

            cmdDelete.Text = GetText("cmdDelete");
            cmdDeleteList.Text = GetText("cmdDeleteList");
        }

        private void Search()
        {
            if (ValidateInput())
            {
                PopulateLogTable(txtNumRecords.Text, txtUser.Text, txtVon.Text, txtBis.Text, txtAktion.Text, txtMessage.Text);
            }
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void InitLogTable()
        {
            DefaultListViewProperties(lvLogTable);

            lvLogTable.Clear();
            lvLogTable.Columns.Add(GetText("zeit"), 100, HorizontalAlignment.Left);
            lvLogTable.Columns.Add(GetText("benutzer"), 100, HorizontalAlignment.Left);
            lvLogTable.Columns.Add(GetText("aktion"), 100, HorizontalAlignment.Left);
            lvLogTable.Columns.Add(GetText("text"), -2, HorizontalAlignment.Left);
        }

        private void PopulateLogTable(string strNumRecords, string strUser, string strVon, string strBis, string strAktion, string strMessage)
        {
            Cursor = Cursors.WaitCursor;

            DataView dataview = BusinessLayer.GetLogTable(strNumRecords, strUser, strVon, strBis, strAktion, strMessage);

            lvLogTable.Items.Clear();
            lvLogTable.BeginUpdate();

            DataTable dataTable = dataview.Table;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow dataRow = dataTable.Rows[i];
                ListViewItem lvi = new ListViewItem(Tools.DBNullableDateTime2DateTimeString(dataRow["TimeStamp"]));
                lvi.Tag = (int)dataRow["ID_LogTable"];
                lvi.SubItems.Add((string)dataRow["User"]);
                lvi.SubItems.Add((string)dataRow["Action"]);
                lvi.SubItems.Add((string)dataRow["Message"]);

                lvLogTable.Items.Add(lvi);
            }
            lvLogTable.EndUpdate();

            SetGroupBoxText(lvLogTable, grpData, GetText("anzahlEintraege"));
            Cursor = Cursors.Default;
        }


        private void LogView_Load(object sender, EventArgs e)
        {
            InitLogTable();
        }

        protected override bool ValidateInput()
        {
            bool fSuccess = true;

            int nNumRecords;
            string strMessage = EINGABEFEHLER;

            if (txtNumRecords.Text.Length > 0 && !Int32.TryParse(txtNumRecords.Text, out nNumRecords))
            {
                strMessage += GetTextControlInvalid(lblNumRecords);
                fSuccess = false;
            }
            if (txtVon.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtVon.Text))
            {
                strMessage += GetTextControlInvalidDate(lblVon);
                fSuccess = false;
            }
            if (txtBis.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtBis.Text))
            {
                strMessage += GetTextControlInvalidDate(lblBis);
                fSuccess = false;
            }
            if (!fSuccess)
            {
                MessageBox(strMessage);
            }

            return fSuccess;
        }

        protected  void CancelClicked(object sender, EventArgs e)
        {
            base.CancelClicked();
        }

        private void cmdDateVon_Click(object sender, EventArgs e)
        {
            Windows.Forms.CalendarPickerView dlg = new Windows.Forms.CalendarPickerView(txtVon.Text);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                txtVon.Text = Tools.DBNullableDateTime2DateString(dlg.SelectedDate);
            }
        }

        private void cmdDateBis_Click(object sender, EventArgs e)
        {
            Windows.Forms.CalendarPickerView dlg = new Windows.Forms.CalendarPickerView(txtBis.Text);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                txtBis.Text = Tools.DBNullableDateTime2DateString(dlg.SelectedDate);
            }
        }

        private void cmdClearFields_Click(object sender, EventArgs e)
        {
            txtVon.Text = "";
            txtBis.Text = "";
            txtNumRecords.Text = "";
            txtUser.Text = "";
            txtAktion.Text = "";
            txtMessage.Text = "";
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (Confirm(GetText("confirmDelete1")))
            {
                if (Confirm(GetText("confirmDelete2")))
                {
                    BusinessLayer.DeleteLogTable();
                    lvLogTable.Items.Clear();
                }
            }
        }

        private void cmdDeleteList_Click(object sender, EventArgs e)
        {
            if (Confirm(GetText("confirmDelete3")))
            {
                foreach (ListViewItem lvi in lvLogTable.Items)
                {
                    int ID_LogTable = (int)lvi.Tag;
                    if (!BusinessLayer.DeleteLogTable(ID_LogTable))
                    {
                        break;
                    }
                }
                Search();
            }
        }
    }
}