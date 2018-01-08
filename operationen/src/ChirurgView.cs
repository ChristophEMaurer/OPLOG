using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using Utility;
using Windows.Forms;

namespace Operationen
{
    public partial class ChirurgView : OperationenForm
    {
        DataRow _chirurg;
        bool _bEdit;
        int _lastIDGebiete;
        Dictionary<int, DateTime?> _gebieteVon = new Dictionary<int, DateTime?>();
        Dictionary<int, DateTime?> _gebieteBis = new Dictionary<int, DateTime?>();

        public ChirurgView(BusinessLayer businessLayer, DataRow chirurg)
            : base(businessLayer)
        {
            if (chirurg == null)
            {
                // neuer Chirurg/User
                _chirurg = this.BusinessLayer.CreateDataRowChirurg();
                _bEdit = false;
            }
            else
            {
                _chirurg = chirurg;
                _bEdit =true;
            }
            
            InitializeComponent();

            chkWeiterbilder.SetSecurity(businessLayer, "ChirurgView.chkWeiterbilder");
            lblInfo.SetSecurity(businessLayer, "ChirurgenFunktionenView.view");
            SetInfoText(lblInfo, string.Format(CultureInfo.InvariantCulture, GetText("info"), Command_ChirurgenFunktionenView));
            if (UserHasRight("ChirurgenFunktionenView.view"))
            {
                AddLinkLabelLink(lblInfo, Command_ChirurgenFunktionenView, Command_ChirurgenFunktionenView);
            }

        }

        private void PopulateTitel()
        {
            DataView dv = BusinessLayer.GetChirurgenFunktionen();

            cbTitel.ValueMember = "ID_ChirurgenFunktionen";
            cbTitel.DisplayMember = "Funktion";
            cbTitel.DataSource = dv;
        }

        private void PopulateGebiete()
        {
            DataView dv = BusinessLayer.GetGebiete();

            cbGebiete.ValueMember = "ID_Gebiete";
            cbGebiete.DisplayMember = "Gebiet";
            cbGebiete.DataSource = dv;

            _lastIDGebiete = ConvertToInt32(cbGebiete.SelectedValue);
        }

        private void ChirurgView_Load(object sender, EventArgs e)
        {
            _bIgnoreControlEvents = true;

            chkWeiterbilder.Text = GetText("chkWeiterbilder");

            cbAnrede.Items.Add(GetText("herr"));
            cbAnrede.Items.Add(GetText("frau"));

            GetChirurgenGebiete(ConvertToInt32(_chirurg["ID_Chirurgen"]));
            PopulateTitel();
            PopulateGebiete(); // löst schon einen changedEvent aus.

            if (_bEdit)
            {
                this.Text = AppTitle(GetText("title_edit"));
            }
            else
            {
                this.Text = AppTitle(GetText("title_new"));
            }

            Object2Control();

            _bIgnoreControlEvents = false;
        }

        protected override void SaveObject()
        {
            if (this._bEdit)
            {
                BusinessLayer.UpdateChirurg(_chirurg, _gebieteVon, _gebieteBis);
            }
            else
            {
                if (-1 != BusinessLayer.InsertChirurg(_chirurg, _gebieteVon, _gebieteBis))
                {
                    MessageBox(GetText("msg_done"));
                }
            }
        }

        private bool ValidateGebietVonBis()
        {
            bool bSuccess = true;
            string strMessage = EINGABEFEHLER;

            if (txtGebietVon.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtGebietVon.Text))
            {
                strMessage += GetTextControlInvalidDate(lblGebietVon);
                bSuccess = false;
            }
            if (txtGebietBis.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtGebietBis.Text))
            {
                strMessage += GetTextControlInvalidDate(lblGebietBis);
                bSuccess = false;
            }

            if (!bSuccess)
            {
                MessageBox(strMessage);
            }

            return bSuccess;
        }

        protected override bool ValidateInput()
        {
            bool bSuccess = true;
            string strMessage = EINGABEFEHLER;

            if (this.txtNachname.Text.Length == 0)
            {
                strMessage += GetTextControlMissingText(lblNachname);
                bSuccess = false;
            }
            if (this.txtUserID.Text.Length == 0)
            {
                strMessage += GetTextControlMissingText(lblAnmeldename);
                bSuccess = false;
            }
            if (this.txtImportID.Text.Length == 0)
            {
                strMessage += GetTextControlMissingText(lblImportID);
                bSuccess = false;
            }
            if (cbTitel.SelectedValue == null)
            {
                strMessage += GetTextControlNeedsSelection(lblTitel);
                bSuccess = false;
            }
            if (!Tools.DateIsValidGermanDate(txtDatum.Text))
            {
                strMessage += GetTextControlInvalidDate(lblAnfangsdatum);
                bSuccess = false;
            }
            if (txtGebietVon.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtGebietVon.Text))
            {
                strMessage += GetTextControlInvalidDate(lblGebietVon);
                bSuccess = false;
            }
            if (txtGebietBis.Text.Length > 0 && !Tools.DateIsValidGermanDate(txtGebietBis.Text))
            {
                strMessage += GetTextControlInvalidDate(lblGebietBis);
                bSuccess = false;
            }

            // UserID muss eindeutig sein. Diese Abfrage soll zuletzt kommen
            // da hier die Datenbank benutzt wird.
            // Bei INSERT ist _oChirurg["ID_Chirurgen"] -1 und es darf die UserID gar nicht vorkommen
            // bei UPDATE ist _oChirurg["ID_Chirurgen"] gesetzt und die UserID darf nur bei diesem Chirurgen vorkommen
            int ID_Chirurgen = ConvertToInt32(_chirurg["ID_Chirurgen"]);
            if (BusinessLayer.GetCountUserID(ID_Chirurgen, txtUserID.Text) > 0)
            {
                string text = string.Format(CultureInfo.InvariantCulture, GetText("err_username1"),  txtUserID.Text);
                strMessage += "\n- " + text;
                bSuccess = false;
            }

            if (BusinessLayer.GetCountImportID(ID_Chirurgen, txtImportID.Text) > 0)
            {
                string text = string.Format(CultureInfo.InvariantCulture, GetText("err_importname1"), txtImportID.Text);
                strMessage += "\n- " + text;
                bSuccess = false;
            }

            if (bSuccess)
            {
                // Wenn alles in Ordnung war, melden, wenn sich der Anmeldename geändert hat!
                if (_bEdit && txtUserID.Text != (string)_chirurg["UserID"])
                {
                    MessageBox(GetText("err_username2"));
                }
            }
            else
            {
                MessageBox(strMessage);
            }

            return bSuccess;
        }

        protected override void Object2Control()
        {
            cbAnrede.Text = (string)_chirurg["Anrede"];
            txtNachname.Text = (string)_chirurg["Nachname"];
            txtImportID.Text = (string)_chirurg["ImportID"];
            txtVorname.Text = (string)_chirurg["Vorname"];
            txtUserID.Text = (string)_chirurg["UserID"];
            txtDatum.Text = Tools.DBDateTime2DateString(_chirurg["Anfangsdatum"]);
            chkWeiterbilder.Checked = 1 == ConvertToInt32(_chirurg["IstWeiterbilder"]);
            
            chkAktiv.Checked = 1 == ConvertToInt32(_chirurg["Aktiv"]);
            cbTitel.SelectedValue = ConvertToInt32(_chirurg["ID_ChirurgenFunktionen"]);

            // gemerkte Werte zu diesem Gebiet holen und in die Textboxen stellen
            int ID_Gebiete = ConvertToInt32(cbGebiete.SelectedValue);

            DateTime? von = _gebieteVon.ContainsKey(ID_Gebiete) ? _gebieteVon[ID_Gebiete] : null;
            DateTime? bis = _gebieteBis.ContainsKey(ID_Gebiete) ? _gebieteBis[ID_Gebiete] : null;

            txtGebietVon.Text = Tools.NullableDateTime2DateString(von);
            txtGebietBis.Text = Tools.NullableDateTime2DateString(bis);
        }

        protected override void Control2Object()
        {
            _chirurg["Anrede"] = this.cbAnrede.Text;
            _chirurg["Nachname"] = this.txtNachname.Text;
            _chirurg["ImportID"] = this.txtImportID.Text;
            _chirurg["Vorname"] = this.txtVorname.Text;
            _chirurg["UserID"] = this.txtUserID.Text;
            _chirurg["Anfangsdatum"] = Tools.InputTextDate2DateTime(txtDatum.Text);
            _chirurg["IstWeiterbilder"] = chkWeiterbilder.Checked ? 1 : 0;
            _chirurg["ID_ChirurgenFunktionen"] = cbTitel.SelectedValue;
            _chirurg["Aktiv"] = chkAktiv.Checked ? 1 : 0;

            DateTime? von = Tools.InputTextDate2NullableDateTime(txtGebietVon.Text);
            DateTime? bis = Tools.InputTextDate2NullableDateTime(txtGebietBis.Text);

            _gebieteVon[_lastIDGebiete] = von;
            _gebieteBis[_lastIDGebiete] = bis;
        }

        private void ChirurgView_Shown(object sender, EventArgs e)
        {
            cbAnrede.Focus();
        }
        protected virtual void OKClicked(object sender, EventArgs e)
        {
            cmdCancel.Enabled = cmdOK.Enabled = false;
            base.OKClicked();
            cmdCancel.Enabled = cmdOK.Enabled = true;
        }
        protected virtual void CancelClicked(object sender, EventArgs e)
        {
            base.CancelClicked();
        }

        private void txtNachname_TextChanged(object sender, EventArgs e)
        {
            if (!_bEdit)
            {
                //
                // wenn noch keine userid eingetragen wurde,
                // dann generieren wir eine aus dem Nachnamen.
                //
                string strOld = txtNachname.Text;
                string strNew = Tools.LastName2LogOnName(strOld);
                txtUserID.Text = strNew;
                txtImportID.Text = strNew;
            }
        }

        private void GetChirurgenGebiete(int ID_Chirurgen)
        {
            DataView dv = BusinessLayer.GetChirurgenGebiete(ID_Chirurgen);

            foreach (DataRow row in dv.Table.Rows)
            {
                DateTime? von = (row["GebietVon"] == DBNull.Value) ? null : (DateTime?)row["GebietVon"];
                DateTime? bis = (row["GebietBis"] == DBNull.Value) ? null : (DateTime?)row["GebietBis"];

                _gebieteVon[ConvertToInt32(row["ID_Gebiete"])] = von;
                _gebieteBis[ConvertToInt32(row["ID_Gebiete"])] = bis;
            }
        }

        private void GebietChanged()
        {
            if (!_bIgnoreControlEvents)
            {
                if (ValidateGebietVonBis())
                {
                    // aktuelle Werte aus Controls auslesen und merken
                    DateTime? von = Tools.InputTextDate2NullableDateTime(txtGebietVon.Text);
                    DateTime? bis = Tools.InputTextDate2NullableDateTime(txtGebietBis.Text);

                    _gebieteVon[_lastIDGebiete] = von;
                    _gebieteBis[_lastIDGebiete] = bis;

                    // gemerkte Werte zu diesem Gebiet holen und in die Textboxen stellen
                    int ID_Gebiete = ConvertToInt32(cbGebiete.SelectedValue);

                    von = _gebieteVon.ContainsKey(ID_Gebiete) ? _gebieteVon[ID_Gebiete] : null;
                    bis = _gebieteBis.ContainsKey(ID_Gebiete) ? _gebieteBis[ID_Gebiete] : null;

                    txtGebietVon.Text = Tools.NullableDateTime2DateString(von);
                    txtGebietBis.Text = Tools.NullableDateTime2DateString(bis);

                    _lastIDGebiete = ID_Gebiete;
                }
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
    }
}

