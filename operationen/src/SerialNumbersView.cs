using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections;

using Utility;
using Windows.Forms;

namespace Operationen
{
    public partial class SerialNumbersView : OperationenForm
    {
        private bool _ignoreRights = false;

        public SerialNumbersView(BusinessLayer businessLayer)
            : this(businessLayer, false)
        {
        }

        public SerialNumbersView(BusinessLayer businessLayer, bool ignoreRights)
            : base(businessLayer)
        {
            _ignoreRights = ignoreRights;

            InitializeComponent();

            if (!_ignoreRights)
            {
                txtData.Enabled =
                cmdDelete.Enabled =
                cmdInsert.Enabled = UserHasRight("SerialNumbersView.edit");
            }
        }

        private void InitSerialNumbers()
        {
            DefaultListViewProperties(lvData);

            lvData.Columns.Add(GetText("col_serialnumber"), 300, HorizontalAlignment.Left);
            lvData.Columns.Add(GetText("col_usedby"), -2, HorizontalAlignment.Left);
        }

        private void PopulateSerialNumbers()
        {
            lvData.Items.Clear();
            lvData.BeginUpdate();

            DataView dataview = BusinessLayer.GetChirurgenSerialNumbers();
            foreach (DataRow dataRow in dataview.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem((string)dataRow["Lizenzdaten"]);
                lvi.Tag = "";
                string name = (string)dataRow["Nachname"];
                if (!string.IsNullOrEmpty((string)dataRow["Vorname"]))
                {
                    name = name + ", " + (string)dataRow["Vorname"];
                }
                lvi.SubItems.Add(name);
                lvData.Items.Add(lvi);
            }

            dataview = BusinessLayer.GetSerialNumbers();
            string free = GetText("lv_serial_free");
            foreach (DataRow dataRow in dataview.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem((string)dataRow["SerialNumber"]);
                lvi.Tag = (string)dataRow["SerialNumber"];
                lvi.SubItems.Add(free);
                lvData.Items.Add(lvi);
            }

            lvData.EndUpdate();

            txtData.Clear();
        }

        private void SerialNumbersView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));

            SetInfoText(lblInfo, string.Format(GetText("info"), cmdInsert.Text));
            SetInfoText(lblInfoData, string.Format(GetText("info_data"), cmdInsert.Text));

            InitSerialNumbers();
            PopulateSerialNumbers();
        }

        protected override bool ValidateInput()
        {
            bool bSuccess = true;
            string strMessage = EINGABEFEHLER;

            if (txtData.Text.Length <= 0)
            {
                strMessage += this.GetTextControlMissingText(grpNew);
                bSuccess = false;
            }

            if (!bSuccess)
            {
                MessageBox(strMessage);
            }

            return bSuccess;
        }

        /// <summary>
        /// Parse a bunch of serial numbers which have been pasted into a text box,
        /// split them into single serial numbers and add to database.
        /// </summary>
        /// <param name="data"></param>
        private bool AddSerialNumbers(string data)
        {
            bool success = false;

            // strip all white spaces and replace by one single space
            string cleanData = Regex.Replace(data, @"(\s\s+)", @" ").Trim();

            string[] arData = cleanData.Split(' ');

            StringBuilder sb = new StringBuilder(string.Format(GetText("confirm_insert"), arData.Length));
            sb.Append("\r\r");

            int remainder;
            int max = arData.Length;

            // Don't display more than 30 numbers or the message box will blow up
            if (max > 30)
            {
                max = 30;
            }

            for (int i = 0; i < max; i++)
            {
                // Display 3 serial numbers in one line
                // First one start in a new line already
                // more than three and Windows wraps them
                Math.DivRem(i, 3, out remainder);
                if (remainder == 0)
                {
                    sb.Append("\r");
                }
                else
                {
                    sb.Append(", ");
                }
                sb.Append(arData[i]);
            }

            if (arData.Length > max)
            {
                // if we cut off some of the numbers, display ... at the end
                sb.Append(", ...");
            }

            if (Confirm(sb.ToString()))
            {
                int countUsed;
                int countUnused;

                DataRow row = BusinessLayer.CreateDataRowSerialNumbers();

                Cursor = Cursors.WaitCursor;

                for (int i = 0; i < arData.Length; i++)
                {
                    string serial = arData[i];

                    if (!BusinessLayer.ValidateSerialFormat(serial))
                    {
                        MessageBox(string.Format(GetText("serial_badformat"), serial));
                        goto exit;
                    }

                    BusinessLayer.GetCountSerialNumber(arData[i], out countUsed, out countUnused);
                    if (countUsed > 0)
                    {
                        //
                        // Exit. If we add the same bunch twice we would have to click OK for each one of them
                        // This is very annoying.
                        //
                        MessageBox(string.Format(GetText("serial_used"), serial));
                        goto exit;
                    }
                    if (countUnused > 0)
                    {
                        //
                        // Exit. If we add the same bunch twice we would have to click OK for each one of them
                        // This is very annoying.
                        //
                        MessageBox(string.Format(GetText("serial_unused"), serial));
                        goto exit;
                    }

                    row["SerialNumber"] = serial;
                    int n = BusinessLayer.InsertSerialNumber(row);
                    if (n < 0)
                    {
                        goto exit;
                    }
                }
                success = true;
            }
            exit:

            Cursor = Cursors.Default;

            return success;
        }

        private void EnableControls(bool enable)
        {
            txtData.Enabled =
            cmdCancel.Enabled =
            cmdDelete.Enabled =
            cmdInsert.Enabled = enable;
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                EnableControls(false);
                string data = txtData.Text;
                if (AddSerialNumbers(data))
                {
                    txtData.Clear();
                    PopulateSerialNumbers();
                }
                if (_ignoreRights)
                {
                    EnableControls(true);
                }
                else
                {
                    EnableControls(UserHasRight("SerialNumbersView.edit"));
                    cmdCancel.Enabled = true;
                }
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            int count = lvData.SelectedItems.Count;

            if (count > 0)
            {
                if (Confirm(GetTextConfirmDeleteSimple(count)))
                {
                    foreach (ListViewItem lvi in lvData.SelectedItems)
                    {
                        string serial = (string)lvi.Tag;

                        if (!string.IsNullOrEmpty(serial))
                        {
                            // Ignore return value, continue deleting on error.
                            // Delete as many as you can.
                            BusinessLayer.DeleteSerialNumber(serial);
                        }
                    }
                    PopulateSerialNumbers();
                }
            }
            else
            {
                MessageBox(GetTextSelectionNone());
            }
        }
    }
}