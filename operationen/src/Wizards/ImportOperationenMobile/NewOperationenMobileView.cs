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

namespace Operationen.Wizards.ImportOperationenMobile
{
    /// <summary>
    /// Dieses ist nur ein View. Alle Operationen werden übergeben und angezeigt.
    /// Alle aúsgewählten werden zurückgegeben.
    /// </summary>
    public partial class NewOperationenMobileView : OperationenForm
    {
        private DataView _alleOperationen;
        private DataView _selectedOperationen;
        private DateTime? _dtMaxOP;
        private bool _insertMultiple = false;

        public NewOperationenMobileView(BusinessLayer b, DataView alleOperationen, DateTime? dtMax) 
            : base(b)
        {
            _alleOperationen = alleOperationen;

            _dtMaxOP = dtMax;

            InitializeComponent();
        }

        protected override string GetFormNameForResourceTexts()
        {
            return "Wizards_ImportOperationenMobile_NewOperationenMobileView";
        }

        public DataView GetSelectedOperationen()
        {
            return _selectedOperationen;
        }

        public bool GetInsertMultiple()
        {
            return _insertMultiple;
        }

        private void NewOperationenMobileView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));

            SetInfoText(lblInfo, string.Format(GetText("info"), cmdOK.Text, chkForce.Text));

            txtDateLatest.Text = Tools.DBNullableDateTime2DateString(_dtMaxOP);

            InitializeListViewOperationen();
            FillListViewOperationen();
        }

        private void InitializeListViewOperationen()
        {
            DefaultListViewProperties(lvOperationen);

            lvOperationen.Columns.Add(GetText("fallzahl"), 80, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("ext_int"), 80, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("datum"), 80, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("von"), 50, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("bis"), 50, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("funktion"), 80, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("opskode"), 80, HorizontalAlignment.Left);
            lvOperationen.Columns.Add(GetText("bezeichnung"), -2, HorizontalAlignment.Left);
        }

        private bool ValidateDatumVonBis()
        {
            bool bSuccess = true;
            string strMessage = EINGABEFEHLER;

            if (txtDatumVon.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtDatumVon.Text))
            {
                strMessage += string.Format(CultureInfo.InvariantCulture, GetText("err_format"),
                    lblDatumVon.Text);
                bSuccess = false;
            }
            if (txtDatumBis.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtDatumBis.Text))
            {
                strMessage += string.Format(CultureInfo.InvariantCulture, GetText("err_format"),
                    lblDatumBis.Text);
                bSuccess = false;
            }

            if (!bSuccess)
            {
                MessageBox(strMessage);
            }

            return bSuccess;
        }

        private void FillListViewOperationen()
        {
            DataView dataview = _alleOperationen;

            lvOperationen.Items.Clear();
            lvOperationen.BeginUpdate();
            foreach (DataRow dataRow in dataview.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem((string)dataRow["Fallzahl"]);
                lvi.Tag = dataRow;
                lvi.ToolTipText = "[" + (string)dataRow["OPSKode"] + "] " + (string)dataRow["OPSText"];
                if (ConvertToInt32(dataRow["Quelle"]) == BusinessLayer.OperationQuelleExtern)
                {
                    lvi.SubItems.Add(GetText("extern"));
                }
                else if (ConvertToInt32(dataRow["Quelle"]) == BusinessLayer.OperationQuelleIntern)
                {
                    lvi.SubItems.Add(GetText("intern"));
                }

                lvi.SubItems.Add(Tools.DBNullableDateTime2DateString(dataRow["Datum"]));
                lvi.SubItems.Add(Tools.DBNullableDateTime2TimeString(dataRow["Zeit"]));
                lvi.SubItems.Add(Tools.DBNullableDateTime2TimeString(dataRow["ZeitBis"]));

                DataRow row = BusinessLayer.GetOPFunktion(ConvertToInt32(dataRow["ID_OPFunktionen"]));
                string beschreibung = (string)row["Beschreibung"];
                lvi.SubItems.Add(beschreibung);

                lvi.SubItems.Add((string)dataRow["OPSKode"]);
                lvi.SubItems.Add((string)dataRow["OPSText"]);
                lvOperationen.Items.Add(lvi);
            }
            lvOperationen.EndUpdate();

            SetGroupBoxText(lvOperationen, grpOperationen, GetText("allops"));
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void PopulateOperationen()
        {
            if (ValidateDatumVonBis())
            {
                Cursor = Cursors.WaitCursor;

                FillListViewOperationen();

                Cursor = Cursors.Default;
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtDatumVon.Clear();
            txtDatumBis.Clear();
        }

        private void EnableControls()
        {
            txtDatumVon.Enabled =
            txtDatumBis.Enabled =
                radDateRange.Checked;


        }

        private void radAll_CheckedChanged(object sender, EventArgs e)
        {
            cmdOK.Enabled = cmdCancel.Enabled = false;

            Cursor = Cursors.WaitCursor;

            EnableControls();

            // Alle selektieren
            lvOperationen.SelectedIndices.Clear();
            for (int i = 0; i < lvOperationen.Items.Count; i++)
            {
                lvOperationen.SelectedIndices.Add(i);
                Application.DoEvents();
            }

            cmdOK.Enabled = cmdCancel.Enabled = true;
            Cursor = Cursors.Default;
        }

        private void radAllSelected_CheckedChanged(object sender, EventArgs e)
        {
            EnableControls();
        }

        private void radDateRange_CheckedChanged(object sender, EventArgs e)
        {
            EnableControls();
        }

        private void radNewerByDate_CheckedChanged(object sender, EventArgs e)
        {
            EnableControls();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (!Confirm(string.Format(GetText("confirm_insert"), grpOperationen.Text)))
            {
                goto exit;
            }

            if (chkForce.Checked)
            {
                if (!Confirm(GetText("confirm")))
                {
                    goto exit;
                }
            }

            DataTable dt = _alleOperationen.Table.Clone();
            foreach (ListViewItem lvi in lvOperationen.SelectedItems)
            {
                DataRow row = (DataRow)lvi.Tag;

                DataRow newRow = dt.NewRow();
                newRow.ItemArray = (object[])row.ItemArray.Clone();
                dt.Rows.Add(newRow);
            }

            _insertMultiple = chkForce.Checked;
            _selectedOperationen = new DataView(dt);

            exit:
                ;
        }

        private void cmdDisplay_Click(object sender, EventArgs e)
        {
            DateTime? von;
            DateTime? bis;

            GetVonBisDatum(txtDatumVon, txtDatumBis, out von, out bis);

            lvOperationen.SelectedIndices.Clear();

            for (int i = 0; i < lvOperationen.Items.Count; i++)
            {
                DataRow row = (DataRow)lvOperationen.Items[i].Tag;
                DateTime datum = (DateTime)row["Datum"];

                // Hier kommt es nicht so auf die Uhrzeit an. Wenn dadurch etwas nicht stimmt,
                // muss man manuell auswählen oder einen anderen Wert eingeben.
                if (von <= datum && datum <= bis)
                {
                    lvOperationen.SelectedIndices.Add(i);
                }
            }
        }
    }
}

