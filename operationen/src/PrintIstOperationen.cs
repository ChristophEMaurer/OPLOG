using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

using Utility;
using Windows.Forms;
using Operationen;

namespace Operationen
{
    public partial class PrintIstOperationen : OperationenForm
    {
        private bool _printAll;
        private DataView _dataView;
        private int _quelle;

        DataRow _chirurg;

        public PrintIstOperationen(BusinessLayer businessLayer, DataRow oChirurg,
            string datumVon, string datumBis)
            : base(businessLayer)
        {
            _chirurg = oChirurg;
            InitializeComponent();

            Text = AppTitle(GetText("title"));

            PopulateOPFunktionen();

            txtVon.Text = datumVon;
            txtBis.Text = datumBis;
        }

        private void PrintIstOperationen_Load(object sender, EventArgs e)
        {
            radAll.Checked = true;

            CheckQuelleDefaultSettings(chkIntern, chkExtern);

            Object2Control();
        }

        private void PopulateOPFunktionen()
        {
            DataView dv = BusinessLayer.GetOPFunktionen(true);

            cbOPFunktionen.ValueMember = "ID_OPFunktionen";
            cbOPFunktionen.DisplayMember = "Beschreibung";
            cbOPFunktionen.DataSource = dv;
            cbOPFunktionen.SelectedValue = (int)OP_FUNCTION.OP_FUNCTION_OP;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override void Object2Control()
        {
            string strText = (string)_chirurg["Anrede"] + " "
                + (string)_chirurg["Nachname"];

            if (_chirurg["Vorname"].ToString().Length > 0)
            {
                strText += ", " + (string)_chirurg["Vorname"];
            }
            this.txtNachname.Text = strText;
            this.txtDatum.Text = Tools.DBNullableDateTime2DateString(_chirurg["Anfangsdatum"]);
        }

        override protected void PrintDocument_BeginPrint(object sender, PrintEventArgs e)
		{
			_currentPageIndex = 1;

            int nChirurgen = ConvertToInt32(_chirurg["ID_Chirurgen"]);
            int ID_OPFunktionen = ConvertToInt32(cbOPFunktionen.SelectedValue);

            DateTime? von = null;
            DateTime? bis = null;

            if (chkZeitraum.Checked)
            {
                if (txtVon.Text.Length > 0)
                {
                    von = Tools.InputTextDate2DateTime(txtVon.Text);
                }
                if (txtBis.Text.Length > 0)
                {
                    bis = Tools.InputTextDate2DateTimeEnd(txtBis.Text);
                }
            }

            if (_printAll)
            {
                _dataView = BusinessLayer.GetIstOperationenChirurg(nChirurgen, ID_OPFunktionen, _quelle, von, bis, true);
            }
            else
            {
                _dataView = BusinessLayer.GetChirurgOperationenOverview(nChirurgen, ID_OPFunktionen, _quelle, von, bis);
            }

            _dataRowIndex = 0;
        }

        override protected void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
		{
			// Set the page margins
			Rectangle rPageMargins = new Rectangle(e.MarginBounds.Location, e.MarginBounds.Size);

			// Make sure nothing gets printed in the margins
			e.Graphics.SetClip(rPageMargins);

            if (this._printAll)
            {
                PrintAll(e);
            }
            else
            {
                PrintOverview(e);
            }
        }

        override protected void PrintDocument_EndPrint(object sender, PrintEventArgs e)
        {
            _currentPageIndex = 1;
            _dataView = null;
        }

        protected override bool ValidateInput()
        {
            bool bSuccess = true;
            string strMessage = EINGABEFEHLER;

            if (chkZeitraum.Checked)
            {
                if (txtVon.Text.Length > 0 && !Utility.Tools.DateIsValidGermanDate(txtVon.Text))
                {
                    bSuccess = false;
                    strMessage += GetTextControlInvalidDate(lblVon);
                }
                if (txtBis.Text.Length > 0 && !Utility.Tools.DateIsValidGermanDate(txtBis.Text))
                {
                    bSuccess = false;
                    strMessage += GetTextControlInvalidDate(lblBis);
                }

                if (!bSuccess)
                {
                    MessageBox(strMessage);
                }
            }

            return bSuccess;
        }

        private void cmdPrintAll_Click(object sender, EventArgs e)
        {
            if (this.ValidateInput())
            {
                if (radAll.Checked)
                {
                    _printAll = true;
                }
                else
                {
                    _printAll = false;
                }

                CheckQuelleCorrectDumbSettings(chkIntern, chkExtern);
                _quelle = GetInternExternFlag(chkIntern.Checked, chkExtern.Checked);

                PrintForm();
            }
        }

        void PrintOverview(PrintPageEventArgs ev)
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

            line = GetText("printFilter1") + " " + (string)_chirurg["Nachname"] + ", " + (string)_chirurg["Vorname"];
            PrintLine(ev, nLine++, line);

            if (chkZeitraum.Checked)
            {
                line = GetText("printFilter2") + ": " + txtVon.Text + " - " + txtBis.Text;
                PrintLine(ev, nLine++, line);
            }

            line = GetTextPrintQuelle(_quelle);
            PrintLine(ev, nLine++, line);

            long summeAnzahl = 0;
            foreach (DataRow row in _dataView.Table.Rows)
            {
                summeAnzahl += Convert.ToInt64(row["Anzahl"]);
            }

            line = GetText("gesamtanzahl") + ": " + summeAnzahl;
            PrintLine(ev, nLine++, line);

            nLine++;
            nLine++;

            // -1 weil irgendwie die unterste Zeile abgeschnitten war
            int nMaxIndex = _dataRowIndex + nLinesPerPage - nLine - 1;
            if (nMaxIndex >= _dataView.Table.Rows.Count)
            {
                nMaxIndex = _dataView.Table.Rows.Count;
            }

            for (; _dataRowIndex < nMaxIndex; _dataRowIndex++)
            {
                DataRow dataRow = _dataView.Table.Rows[_dataRowIndex];

                line = String.Format("{0,4:G}: {1,3} {2}{3}",
                    _dataRowIndex + 1,
                    dataRow["Anzahl"], 
                    (string)Utility.Tools.CutString((string)dataRow["OPSKode"] + "            ", 10, false),
                    (string)dataRow["OPSText"]);
                PrintLine(ev, nLine++, line);
            }

            _currentPageIndex++;
            ev.HasMorePages = _dataRowIndex < _dataView.Table.Rows.Count;
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

            line = GetText("printFilter3") + " " + _chirurg["Nachname"].ToString() + ", " + _chirurg["Vorname"].ToString();
            PrintLine(ev, nLine++, line);

            if (chkZeitraum.Checked)
            {
                line = GetText("von") + " " + txtVon.Text + " " + GetText("bis") + " " + txtBis.Text;
                PrintLine(ev, nLine++, line);
            }

            line = GetTextPrintQuelle(_quelle);
            PrintLine(ev, nLine++, line);

            nLine++;
            nLine++;

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
                    + Tools.DBNullableDateTime2DateString((DateTime)dataRow["Datum"]) + " "
                    + Utility.Tools.CutString((string)dataRow["Kode"] + "            ", 10, false) 
                    + (string)dataRow["Name"];
                PrintLine(ev, nLine++, line); 
            }

            _currentPageIndex++;
            ev.HasMorePages = _dataRowIndex < _dataView.Table.Rows.Count;
        }

        private void XAbleControls()
        {
            //bool bEnable = radAll.Checked && chkZeitraum.Checked;
            bool bEnable = chkZeitraum.Checked;
 
            txtVon.Enabled = bEnable;
            txtBis.Enabled = bEnable;
        }

        private void radAll_CheckedChanged(object sender, EventArgs e)
        {
            XAbleControls();
        }

        private void radOverview_CheckedChanged(object sender, EventArgs e)
        {
            XAbleControls();
        }

        private void chkZeitraum_CheckedChanged(object sender, EventArgs e)
        {
            XAbleControls();
        }
    }
}
