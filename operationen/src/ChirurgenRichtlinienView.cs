using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Globalization;

using Utility;

namespace Operationen
{
    public partial class ChirurgenRichtlinienView : OperationenForm
    {
        private DataView _printDataView = null;
        private bool _editMode;

        public ChirurgenRichtlinienView() 
        {
        }

        public ChirurgenRichtlinienView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();

            this.Text = AppTitle(GetText("title"));

            base._bIgnoreControlEvents = true;

            InitRichtlinien();
            InitChirurgenRichtlinien();
            PopulateGebiete();
            PopulateChirurgen(cbChirurgen);

            base._bIgnoreControlEvents = false;

            GebietChanged();
        }

        private void SetInfoText()
        {
            SetInfoText(lblInfo, string.Format(CultureInfo.InvariantCulture, GetText("info"),
                grpRichtlinien.Text, cmdInsert.Text,
                grpChirurgenRichtlinien.Text, cmdApply.Text,
                Command_RichtlinienVergleichView));
            AddLinkLabelLink(lblInfo, Command_RichtlinienVergleichView, Command_RichtlinienVergleichView);
        }

        private void PopulateGebiete()
        {
            DataView dv = BusinessLayer.GetGebiete();

            cbGebiete.ValueMember = "ID_Gebiete";
            cbGebiete.DisplayMember = "Gebiet";
            cbGebiete.DataSource = dv;
        }

        private void InitRichtlinien()
        {
            DefaultListViewProperties(lvRichtlinien);

            lvRichtlinien.Clear();
            lvRichtlinien.Columns.Add(GetText("nr"), 50, HorizontalAlignment.Left);
            lvRichtlinien.Columns.Add(GetText("richtzahl"), 80, HorizontalAlignment.Left);
            lvRichtlinien.Columns.Add(GetText("methode"), -2, HorizontalAlignment.Left);
        }
        private void InitChirurgenRichtlinien()
        {
            lvChirurgenRichtlinien.Clear();

            DefaultListViewProperties(lvChirurgenRichtlinien);

            lvChirurgenRichtlinien.Columns.Add(GetText("datum"), 70, HorizontalAlignment.Left);
            lvChirurgenRichtlinien.Columns.Add(GetText("anzahl"), 50, HorizontalAlignment.Left);
            lvChirurgenRichtlinien.Columns.Add(GetText("ort"), 150, HorizontalAlignment.Left);
            lvChirurgenRichtlinien.Columns.Add(GetText("nr"), 30, HorizontalAlignment.Left);
            lvChirurgenRichtlinien.Columns.Add(GetText("richtzahl"), 60, HorizontalAlignment.Left);
            lvChirurgenRichtlinien.Columns.Add(GetText("methode"), -2, HorizontalAlignment.Left);
        }

        private void PopulateChirurgenRichtlinien()
        {
            int ID_Chirurgen = ConvertToInt32(cbChirurgen.SelectedValue);
            int ID_Gebiete = ConvertToInt32(cbGebiete.SelectedValue);

            _printDataView = BusinessLayer.GetChirurgenRichtlinien(ID_Chirurgen, ID_Gebiete);

            lvChirurgenRichtlinien.Items.Clear();
            lvChirurgenRichtlinien.BeginUpdate();

            foreach (DataRow dataRow in _printDataView.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem(Tools.DBNullableDateTime2DateString(dataRow["Datum"]));
                lvi.Tag = ConvertToInt32(dataRow["ID_ChirurgenRichtlinien"]);
                lvi.SubItems.Add(dataRow["Anzahl"].ToString());
                lvi.SubItems.Add((string)dataRow["Ort"]);

                // LfdNummer
                lvi.SubItems.Add(dataRow["LfdNummer"].ToString());

                // Richtzahl: "", "50 oder "BK" (Basiskenntnisse)
                AddRichtzahl(lvi, dataRow["Richtzahl"].ToString());

                // Methode
                string s = (string)dataRow["UntBehMethode"];
                AddRichtlinie(lvi, s, true);

                lvChirurgenRichtlinien.Items.Add(lvi);
            }
            lvChirurgenRichtlinien.EndUpdate();

            cmdApply.Enabled = false;
            cmdDelete.Enabled = false;
            txtAnzahl.Clear();
            txtDatum.Clear();
            txtOrt.Clear();

            SetGroupBoxText(lvChirurgenRichtlinien, grpChirurgenRichtlinien, GetText("grp_chirurgenrichtlinien"));
            SetInfoText();
        }


        protected override bool ValidateInput()
        {
            bool bSuccess = true;
            string strMessage = EINGABEFEHLER;

            if (!_editMode && (-1 == GetFirstSelectedTag(lvRichtlinien, false)))
            {
                bSuccess = false;
                strMessage += GetTextControlNeedsSelection(grpRichtlinien);
            }
            
            if (_editMode && (-1 == GetFirstSelectedTag(lvChirurgenRichtlinien, false)))
            {
                bSuccess = false;
                strMessage += GetTextControlNeedsSelection(grpChirurgenRichtlinien);
            }


            if (txtAnzahl.Text.Length == 0)
            {
                bSuccess = false;
                strMessage += GetTextControlMissingText(lblAnzahl);
            }
            else
            {
                int anzahl;

                // Überprüfen, ob dieser eingegebene OPS-Kode überhaupt vorkommt: txtOpsKode
                if (Int32.TryParse(txtAnzahl.Text, out anzahl))
                {
                    if (anzahl < 1 || anzahl > 1000)
                    {
                        bSuccess = false;
                        strMessage += string.Format(GetText("msg_anzahl_range"), lblAnzahl.Text, 1, 1000);
                    }
                }
                else
                {
                    bSuccess = false;
                    strMessage += string.Format(GetText("msg_anzahl_number"), lblAnzahl.Text);
                }
            }
            if (txtOrt.Text.Length == 0)
            {
                bSuccess = false;
                strMessage += GetTextControlMissingText(lblOrt);
            }
            if (!Tools.DateIsValidGermanDate(txtDatum.Text))
            {
                bSuccess = false;
                strMessage += GetTextControlInvalidDate(lblDatum);
            }

            if (!bSuccess)
            {
                MessageBox(strMessage);
            }

            return bSuccess;
        }

        private void PopulateRichtlinien()
        {
            int ID_Gebiete = ConvertToInt32(cbGebiete.SelectedValue);

            DataView dv = BusinessLayer.GetRichtlinien(ID_Gebiete);

            lvRichtlinien.Items.Clear();
            lvRichtlinien.BeginUpdate();

            foreach (DataRow dataRow in dv.Table.Rows)
            {
                // LfdNummer
                ListViewItem lvi = new ListViewItem(dataRow["LfdNummer"].ToString());
                lvi.Tag = ConvertToInt32(dataRow["ID_Richtlinien"]);

                // Richtzahl: "", "50 oder "BK" (Basiskenntnisse)
                AddRichtzahl(lvi, dataRow["Richtzahl"].ToString());

                // Methode
                string s = (string)dataRow["UntBehMethode"];
                AddRichtlinie(lvi, s, true);

                lvRichtlinien.Items.Add(lvi);
            }
            lvRichtlinien.EndUpdate();
            SetGroupBoxText(lvRichtlinien, grpRichtlinien, GetText("grp_richtlinien"));
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (lvChirurgenRichtlinien.SelectedItems.Count > 0)
            {
                if (Confirm(GetTextConfirmDeleteSimple(lvChirurgenRichtlinien.SelectedItems.Count)))
                {
                    Cursor = Cursors.WaitCursor;

                    try
                    {
                        BusinessLayer.OpenDatabaseForImport();
                        foreach (ListViewItem lvi in lvChirurgenRichtlinien.SelectedItems)
                        {
                            int nID = (int)lvi.Tag;
                            if (nID != -1)
                            {
                                if (!BusinessLayer.DeleteChirurgenRichtlinien(nID))
                                {
                                    break;
                                }
                            }
                        }
                    }
                    finally
                    {
                        BusinessLayer.CloseDatabaseForImport();
                    }

                    PopulateChirurgenRichtlinien();

                    Cursor = Cursors.Default;
                }
            }
        }


        private void cmdOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GebietChanged()
        {
            if (!_bIgnoreControlEvents)
            {
                Cursor = Cursors.WaitCursor;

                PopulateChirurgenRichtlinien();
                PopulateRichtlinien();

                Cursor = Cursors.Default;
            }
        }

        private void cbGebiete_SelectedIndexChanged(object sender, EventArgs e)
        {
            GebietChanged();
        }

        private void lblInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OperationenLogbuchView.TheMainWindow.StartMenuItem(e.Link.LinkData);
        }

        private void cbChirurgen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_bIgnoreControlEvents)
            {
                PopulateChirurgenRichtlinien();
            }
        }

        private void lvChirurgenRichtlinien_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem lvi = GetFirstSelectedLVI(lvChirurgenRichtlinien);

            if (lvi != null)
            {
                txtDatum.Text = lvi.SubItems[0].Text;
                txtAnzahl.Text = lvi.SubItems[1].Text;
                txtOrt.Text = lvi.SubItems[2].Text;

                cmdApply.Enabled = true;
                cmdDelete.Enabled = true;
            }
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            _editMode = true;

            if (ValidateInput())
            {
                int ID_ChirurgenRichtlinien = GetFirstSelectedTag(lvChirurgenRichtlinien, true);
                if (ID_ChirurgenRichtlinien != -1)
                {
                    DataRow dataRow = BusinessLayer.GetChirurgenRichtlinien(ID_ChirurgenRichtlinien);

                    dataRow["Datum"] = Tools.InputTextDate2DateTime(txtDatum.Text);
                    dataRow["Anzahl"] = Convert.ToInt32(txtAnzahl.Text);
                    dataRow["Ort"] = txtOrt.Text;

                    Cursor = Cursors.WaitCursor;
                    BusinessLayer.UpdateChirurgenRichtlinien(dataRow);
                    PopulateChirurgenRichtlinien();
                    Cursor = Cursors.Default;

                    txtOrt.Focus();

                }
            }
        }
        private void cmdInsert_Click(object sender, EventArgs e)
        {
            _editMode = false;

            if (ValidateInput())
            {
                int ID_Chirurgen = ConvertToInt32(cbChirurgen.SelectedValue);
                int ID_Richtlinien = GetFirstSelectedTag(lvRichtlinien, true);

                if (ID_Richtlinien != -1)
                {
                    DataRow dataRow = BusinessLayer.CreateDataRowChirurgenRichtlinien(ID_Chirurgen, ID_Richtlinien);
                    dataRow["ID_Richtlinien"] = ID_Richtlinien;
                    dataRow["Datum"] = Tools.InputTextDate2DateTime(txtDatum.Text);
                    dataRow["Anzahl"] = Convert.ToInt32(txtAnzahl.Text);
                    dataRow["Ort"] = txtOrt.Text;

                    Cursor = Cursors.WaitCursor;
                    BusinessLayer.InsertChirurgenRichtlinien(dataRow);
                    PopulateChirurgenRichtlinien();
                    Cursor = Cursors.Default;

                    txtOrt.Focus();
                }
            }
        }

        private void ChirurgenRichtlinienView_Load(object sender, EventArgs e)
        {
        }

        private void ChirurgenRichtlinienView_Resize(object sender, EventArgs e)
        {
            grpRichtlinien.Height = (int)(0.32 * this.Height);
            grpChirurgenRichtlinien.Top = grpRichtlinien.Bottom + ControlYOffset;
            grpChirurgenRichtlinien.Height = lblInfo.Top - grpChirurgenRichtlinien.Top - ControlYOffset;

        }
    }
}