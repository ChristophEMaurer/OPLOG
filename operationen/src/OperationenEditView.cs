using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Utility;
using Windows.Forms;

namespace Operationen
{
    public partial class OperationenEditView : OperationenForm
    {
        private DataRow _chirurgenOperation;
        private bool _editMode;
        private bool _dontDisplayNextTime = false;

        public OperationenEditView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();

            this.Text = AppTitle(GetText("title"));

            txtDatum.Text = Utility.Tools.NullableDateTime2DateString(DateTime.Today);
            txtZeit.Text = "10:00";
            txtZeitBis.Text = "12:00";
            txtKlinischeErgebnisse.Text = "";

            cmdApply.Enabled = false;
            cmdDelete.Enabled = false;
            radQuelleExtern.Checked = false;
            radQuelleIntern.Checked = true;

            PopulateChirurgen(cbChirurgen);
            PopulateOPFunktionen();
            PopulateKlinischeErgebnisseTypen(cbKlinischeErgebnisseTypen);

            InitListViewChirurgenOperationen();

            SetInfoText();
        }

        private void SetInfoText()
        {
            SetInfoText(lblInfo, string.Format(GetText("info"),
                grpOperationen.Text, cmdInsert.Text,
                grpChirurgenOperationen.Text, cmdApply.Text
                ));

            lblHelp1.BorderStyle = BorderStyle.Fixed3D;
            lblHelp1.ForeColor = BusinessLayer.InfoColor;
            lblHelp2.BorderStyle = BorderStyle.Fixed3D;
            lblHelp2.ForeColor = BusinessLayer.InfoColor;
        }

        private void PopulateOperationen()
        {
            base.PopulateOperationen(grpOperationen, lvOperationen, txtOPS.Text);
            cmdApply.Enabled = false;

            SetInfoText(lblInfo, string.Format(GetText("info"),
                grpOperationen.Text, cmdInsert.Text,
                grpChirurgenOperationen.Text, cmdApply.Text
                ));
        }

        private void PopulateOPFunktionen()
        {
            DataView dv = BusinessLayer.GetOPFunktionen(false);

            cbOPFunktionen.ValueMember = "ID_OPFunktionen";
            cbOPFunktionen.DisplayMember = "Beschreibung";
            cbOPFunktionen.DataSource = dv;
            cbOPFunktionen.SelectedValue = (int)OP_FUNCTION.OP_FUNCTION_OP;
        }

        protected override bool ValidateInput()
        {
            bool bSuccess = true;
            string strMessage = EINGABEFEHLER;

            if (_editMode)
            {
                //UPDATE: man muss eine vorhandene Operation ausgewählt haben

                //
                // OPSKode must never change
                //
                if (-1 == GetFirstSelectedTag(lvChirurgenOperationen))
                {
                    strMessage += string.Format(GetText("msg_no_surgeonop_selected"), grpChirurgenOperationen.Text);
                    bSuccess = false;
                }
                if (txtOPSKode.Text.Length == 0)
                {
                    strMessage += GetTextControlMissingText(lblOPSKode);
                    bSuccess = false;
                }
                if (txtOPSText.Text.Length == 0)
                {
                    strMessage += GetTextControlMissingText(lblOPSText);
                    bSuccess = false;
                }
            }
            else
            {
                //
                // 12/2008 new: can enter OPSKode directly
                //

                //INSERT: man muss eine OP aus dem Katalog ausgewählt haben
                //if (-1 == GetFirstSelectedTag(lvOperationen))
                //{
                //    strMessage += string.Format(GetText("msg_no_op_selected"), grpOperationen.Text);
                //    bSuccess = false;
                //}
                if (txtOPSKode.Text.Length == 0)
                {
                    strMessage += GetTextControlMissingText(lblOPSKode);
                    bSuccess = false;
                }

                //
                // Check for an existing OPS-Code and get the first matching one
                //
                DataRow row = BusinessLayer.GetOperationen(txtOPSKode.Text);
                if (row == null)
                {
                    strMessage += GetText("msg_op_nonexists");
                    bSuccess = false;
                }
                else
                {
                    txtOPSKode.Text = (string)row["OPS-Kode"];
                    txtOPSText.Text = (string)row["OPS-Text"];

                    //
                    // Make sure these two fields have been filled
                    //
                    if (txtOPSKode.Text.Length == 0)
                    {
                        strMessage += GetTextControlMissingText(lblOPSKode);
                        bSuccess = false;
                    }
                    if (txtOPSText.Text.Length == 0)
                    {
                        strMessage += GetTextControlMissingText(lblOPSText);
                        bSuccess = false;
                    }

                }

                if (txtAnzahl.Text.Length == 0)
                {
                    strMessage += GetTextControlMissingText(lblAnzahl);
                    bSuccess = false;
                }
            }

            if (txtFallzahl.Text.Length == 0)
            {
                strMessage += GetTextControlMissingText(lblFallzahl);
                bSuccess = false;
            }
            if (!Tools.DateIsValidGermanDate(txtDatum.Text))
            {
                strMessage += GetTextControlInvalidDate(lblDatum);
                bSuccess = false;
            }
            if (!Tools.TimeIsValidGermanTime(txtZeit.Text))
            {
                strMessage += GetTextControlInvalidTime(lblZeit);
                bSuccess = false;
            }
            if (!Tools.TimeIsValidGermanTime(txtZeitBis.Text))
            {
                strMessage += GetTextControlInvalidTime(lblZeitBis);
                bSuccess = false;
            }
            if (cbKlinischeErgebnisseTypen.SelectedValue == null)
            {
                bSuccess = false;
                strMessage += GetTextControlNeedsSelection(lblKlinischeErgebnisseTypen);
            }

            int ID_KlinischeErgebnisseTypenUnauffaellig = BusinessLayer.DatabaseLayer.GetIdKlinischeErgebnisseTypenUnauffaellig();

            if (ConvertToInt32(cbKlinischeErgebnisseTypen.SelectedValue) != ID_KlinischeErgebnisseTypenUnauffaellig
                && txtKlinischeErgebnisse.Text.Length == 0)
            {
                bSuccess = false;
                strMessage += GetTextControlNeedsContents(lblKlinischeErgebnisse);
            }

            if (!bSuccess)
            {
                MessageBox(strMessage);
            }

            return bSuccess;
        }

        protected override void Control2Object()
        {
            _chirurgenOperation["OPS-Kode"] = txtOPSKode.Text;
            _chirurgenOperation["OPS-Text"] = txtOPSText.Text;
            _chirurgenOperation["ID_OPFunktionen"] = ConvertToInt32(cbOPFunktionen.SelectedValue);
            _chirurgenOperation["ID_Richtlinien"] = DBNull.Value;
            _chirurgenOperation["ID_Chirurgen"] = ConvertToInt32(cbChirurgen.SelectedValue);
            _chirurgenOperation["ID_KlinischeErgebnisseTypen"] = ConvertToInt32(cbKlinischeErgebnisseTypen.SelectedValue);

            // Achtung: Datum enthält das Datum und den Zeit-Anteil vom Feld Zeit
            DateTime datum = Tools.InputTextDate2DateTime(txtDatum.Text);
            DateTime dtZeit = Tools.InputTextTime2DateTime(txtZeit.Text);
            DateTime dtDatum = new DateTime(datum.Year, datum.Month, datum.Day, dtZeit.Hour, dtZeit.Minute, dtZeit.Second);
            _chirurgenOperation["Datum"] = dtDatum;
            _chirurgenOperation["Zeit"] = dtZeit;
            _chirurgenOperation["ZeitBis"] = Tools.InputTextTime2DateTime(txtZeitBis.Text);

            _chirurgenOperation["Fallzahl"] = txtFallzahl.Text;
            _chirurgenOperation["Quelle"] = radQuelleIntern.Checked ? BusinessLayer.OperationQuelleIntern : BusinessLayer.OperationQuelleExtern;
            _chirurgenOperation["KlinischeErgebnisse"] = txtKlinischeErgebnisse.Text;
        }

        protected override void SaveObject()
        {
            if (_editMode)
            {
                BusinessLayer.UpdateChirurgenOperationen(_chirurgenOperation);
            }
            else
            {
                bool success = false;
                int anzahl;

                if (Int32.TryParse(txtAnzahl.Text, out anzahl))
                {
                    if (anzahl == 1)
                    {
                        // Ein Datensatz wird immer eingefügt
                        success = true;
                    }
                    else if (anzahl > 1)
                    {
                        // Mehrere Operationen auf einmal einfügen
                        if (_dontDisplayNextTime)
                        {
                            // Meldung wurde schon mal gezeigt, jetzt nicht mehr
                            success = true;
                        }
                        else
                        {
                            // Fragen, ob wirklich
                            MultipleOperationInsertNotificationView dlg =
                                new MultipleOperationInsertNotificationView(BusinessLayer, anzahl);

                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                success = true;
                                _dontDisplayNextTime = dlg.dontDisplayNextTime;
                            }
                        }
                    }
                }
                if (success)
                {
                    Cursor = Cursors.WaitCursor;
                    for (int i = 0; i < anzahl; i++)
                    {
                        int ID_ChirurgenOperationen = BusinessLayer.InsertChirurgenOperationen(_chirurgenOperation);
                        if (ID_ChirurgenOperationen <= 0)
                        {
                            break;
                        }
                    }
                    Cursor = Cursors.Default;
                }
            }
        }

        private void OperationenEditView_Shown(object sender, EventArgs e)
        {
            txtOPSKode.Focus();
        }

        private void cmdAnzeigenOperationen_Click(object sender, EventArgs e)
        {
            PopulateOperationen(grpOperationen, lvOperationen, txtOPS.Text);
            SetInfoText();
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            _editMode = false;
            if (ValidateInput())
            {
                _chirurgenOperation = BusinessLayer.CreateDataRowChirurgenOperationen(ConvertToInt32(cbChirurgen.SelectedValue));
                Control2Object();
                SaveObject();
                PopulateListViewChirurgenOperationen();
            }
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            _editMode = true;
            if (ValidateInput())
            {
                Control2Object();
                SaveObject();
                PopulateListViewChirurgenOperationen();
            }
        }

        private void InitListViewChirurgenOperationen()
        {
            DefaultListViewProperties(lvChirurgenOperationen);

            lvChirurgenOperationen.Columns.Add(GetText("quelle"), 50, HorizontalAlignment.Left);
            lvChirurgenOperationen.Columns.Add(GetText("fallzahl"), 80, HorizontalAlignment.Left);
            lvChirurgenOperationen.Columns.Add(GetText("datum"), 80, HorizontalAlignment.Left);
            lvChirurgenOperationen.Columns.Add(GetText("von"), 50, HorizontalAlignment.Left);
            lvChirurgenOperationen.Columns.Add(GetText("bis"), 50, HorizontalAlignment.Left);
            lvChirurgenOperationen.Columns.Add(GetText("funktion"), 80, HorizontalAlignment.Left);
            lvChirurgenOperationen.Columns.Add(GetText("klinische"), 120, HorizontalAlignment.Left);
            lvChirurgenOperationen.Columns.Add(GetText("ergebnisse"), 100, HorizontalAlignment.Left);
            lvChirurgenOperationen.Columns.Add(GetText("opsCode"), 80, HorizontalAlignment.Left);
            lvChirurgenOperationen.Columns.Add(GetText("bezeichnung"), -2, HorizontalAlignment.Left);
        }

        private void PopulateListViewChirurgenOperationen()
        {
            int nChirugen = ConvertToInt32(cbChirurgen.SelectedValue);

            DateTime? von;
            DateTime? bis;

            GetVonBisDatum(txtFilterDatumVon, txtFilterDatumBis, out von, out bis);

            DataView dataview = BusinessLayer.GetChirurgenOperationen(nChirugen, von, bis);

            int index = GetFirstSelectedIndex(lvChirurgenOperationen);

            lvChirurgenOperationen.Items.Clear();
            lvChirurgenOperationen.BeginUpdate();

            string strIntern = GetText("intern");
            string strExtern = GetText("extern");

            foreach (DataRow dataRow in dataview.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem(ConvertToInt32(dataRow["Quelle"]) == BusinessLayer.OperationQuelleExtern ? strExtern : strIntern);
                lvi.Tag = ConvertToInt32(dataRow["ID_ChirurgenOperationen"]);
                lvi.ToolTipText = "[" + (string)dataRow["OPS-Kode"] + "] " + (string)dataRow["OPS-Text"];
                lvi.SubItems.Add((string)dataRow["Fallzahl"]);
                lvi.SubItems.Add(Tools.DBNullableDateTime2DateString(dataRow["Datum"]));
                lvi.SubItems.Add(Tools.DBNullableDateTime2TimeString(dataRow["Zeit"]));
                lvi.SubItems.Add(Tools.DBNullableDateTime2TimeString(dataRow["ZeitBis"]));
                lvi.SubItems.Add((string)dataRow["Beschreibung"]);
                lvi.SubItems.Add((string)dataRow["KlinischeErgebnisseTyp"]);
                lvi.SubItems.Add((string)dataRow["KlinischeErgebnisse"]);
                lvi.SubItems.Add((string)dataRow["OPS-Kode"]);
                lvi.SubItems.Add((string)dataRow["OPS-Text"]);
                lvChirurgenOperationen.Items.Add(lvi);
            }
            if (index != -1 && lvChirurgenOperationen.Items.Count > 0)
            {
                //
                // if we deleted the last item of a list with items, the index would be invalid
                // if we deleted the last entry and there is no entry left, check this too
                //
                if (index >= lvChirurgenOperationen.Items.Count)
                {
                    index = lvChirurgenOperationen.Items.Count - 1;
                }
                if (index < lvChirurgenOperationen.Items.Count)
                {
                    lvChirurgenOperationen.SelectedIndices.Add(index);
                }
            }
            lvChirurgenOperationen.EndUpdate();

            SetGroupBoxText(lvChirurgenOperationen, grpChirurgenOperationen, GetText("prozeduren"));

            cmdApply.Enabled = false;
            cmdDelete.Enabled = false;

            SetInfoText();
        }

        private void PopulateChirurgenOperationen()
        {
            if (ValidateDatumVonBis(lblFilterDatumVon, txtFilterDatumVon.Text, lblFilterDatumBis, txtFilterDatumBis.Text))
            {
                Cursor = Cursors.WaitCursor;

                PopulateListViewChirurgenOperationen();

                Cursor = Cursors.Default;
            }
        }

        private void cmdAnzeigenChirurgenOperationen_Click(object sender, EventArgs e)
        {
            if (!_bIgnoreControlEvents)
            {
                PopulateChirurgenOperationen();
            }
        }

        protected override void Object2Control()
        {
            txtOPSKode.Text = (string)_chirurgenOperation["OPS-Kode"];
            txtOPSText.Text = (string)_chirurgenOperation["OPS-Text"];
            txtFallzahl.Text = (string)_chirurgenOperation["Fallzahl"];
            txtDatum.Text = Tools.DBNullableDateTime2DateString(_chirurgenOperation["Datum"]);
            txtZeit.Text = Tools.DBNullableDateTime2TimeString(_chirurgenOperation["Zeit"]);
            txtZeitBis.Text = Tools.DBNullableDateTime2TimeString(_chirurgenOperation["ZeitBis"]);
            cbOPFunktionen.SelectedValue = ConvertToInt32(_chirurgenOperation["ID_OPFunktionen"]);
            cbKlinischeErgebnisseTypen.SelectedValue = ConvertToInt32(_chirurgenOperation["ID_KlinischeErgebnisseTypen"]);
            cbChirurgen.SelectedValue = ConvertToInt32(_chirurgenOperation["ID_Chirurgen"]);
            if (ConvertToInt32(_chirurgenOperation["Quelle"]) == 0)
            {
                radQuelleExtern.Checked = false;
                radQuelleIntern.Checked = true;
            }
            else
            {
                radQuelleIntern.Checked = false;
                radQuelleExtern.Checked = true;
            }
            txtKlinischeErgebnisse.Text = (string)_chirurgenOperation["KlinischeErgebnisse"];
        }

        private void lvChirurgenOperationen_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID_ChirurgenOperationen = GetFirstSelectedTag(lvChirurgenOperationen);

            if (ID_ChirurgenOperationen != -1)
            {
                _chirurgenOperation = BusinessLayer.GetChirurgenOperationenRecord(ID_ChirurgenOperationen);
                Object2Control();
                cmdApply.Enabled = true;
                cmdDelete.Enabled = true;
                txtAnzahl.Text = "1";
            }
        }

        private void lvOperationen_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmdApply.Enabled = false;

            ListViewItem lvi = GetFirstSelectedLVI(lvOperationen);

            if (lvi != null)
            {
                txtOPSKode.Text = lvi.Text;
                txtOPSText.Text = lvi.SubItems[1].Text;
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            int nCount = lvChirurgenOperationen.SelectedItems.Count;

            if (nCount > 0)
            {
                if (Confirm(GetTextConfirmDeleteSimple(nCount)))
                {
                    Cursor = Cursors.WaitCursor;

                    foreach (ListViewItem lvi in lvChirurgenOperationen.SelectedItems)
                    {
                        int nID = (int)lvi.Tag;

                        if (nID != -1)
                        {
                            if (!BusinessLayer.DeleteChirurgenOperationen(nID))
                            {
                                break;
                            }
                        }
                    }
                    PopulateChirurgenOperationen();

                    Cursor = Cursors.Default;
                }
            }
            else
            {
                MessageBox(GetTextSelectionNone());
            }
        }

        private void txtAnzahl_TextChanged(object sender, EventArgs e)
        {
            int nAnzahl;

            if (Int32.TryParse(txtAnzahl.Text, out nAnzahl))
            {
                if (nAnzahl > 1)
                {
                    txtAnzahl.ForeColor = Color.Red;
                }
                else
                {
                    txtAnzahl.ForeColor = System.Drawing.SystemColors.WindowText;
                }
            }
        }

        private void cbChirurgen_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvChirurgenOperationen.Items.Clear();
            cmdApply.Enabled = false;
            cmdDelete.Enabled = false;
        }

        private void OperationenEditView_Load(object sender, EventArgs e)
        {

        }

        private void txtOPSKode_TextChanged(object sender, EventArgs e)
        {
            txtOPSText.Text = "";
        }
    }
}