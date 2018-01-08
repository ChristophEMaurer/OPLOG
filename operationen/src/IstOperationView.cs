using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Utility;
using Windows.Forms;
using Operationen;

namespace Operationen
{

    public partial class IstOperationView : OperationenForm
    {
        private const int AnzahlMin = 1;
        private const int AnzahlMax = 50;

        private bool dontDisplayNextTime = false;

        private bool _bDataAdded = false;
        int _nID_Chirurgen = -1;
        DataRow _chirurgenOperationen;

        public IstOperationView(BusinessLayer b, ArrayList args) 
            : base(b)
        {
            _nID_Chirurgen = (int)args[0];
            _chirurgenOperationen = this.BusinessLayer.CreateDataRowChirurgenOperationen(_nID_Chirurgen);

            InitializeComponent();

            radQuelleExtern.Checked = false;
            radQuelleIntern.Checked = true;
        }

        public bool DataAdded { get { return _bDataAdded; } }

        private string GetSelectedOpsKode()
        {
            string opsKode = "";

            foreach (ListViewItem lvi in this.lvOperationen.SelectedItems)
            {
                opsKode = lvi.Text;
                break;
            }

            return opsKode;
        }

        protected override void Control2Object()
        {
            ListViewItem lvi = GetFirstSelectedLVI(lvOperationen);

            string opsKode = lvi.Text;
            string opsText = lvi.SubItems[1].Text;

            _chirurgenOperationen["OPS-Kode"] = opsKode;
            _chirurgenOperationen["OPS-Text"] = opsText;
            _chirurgenOperationen["Fallzahl"] = txtFallzahl.Text;
            _chirurgenOperationen["Datum"] = Tools.InputTextDateTime2DateTime(txtDatum.Text, txtZeit.Text);
            _chirurgenOperationen["Zeit"] = Tools.InputTextDateTime2DateTime(txtDatum.Text, txtZeit.Text);
            _chirurgenOperationen["ZeitBis"] = Tools.InputTextDateTime2DateTime(txtDatum.Text, txtZeitBis.Text);
            _chirurgenOperationen["ID_OPFunktionen"] = cbOPFunktionen.SelectedValue;
            _chirurgenOperationen["ID_KlinischeErgebnisseTypen"] = cbKlinischeErgebnisseTypen.SelectedValue;
            _chirurgenOperationen["Quelle"] = radQuelleIntern.Checked ? BusinessLayer.OperationQuelleIntern : BusinessLayer.OperationQuelleExtern;
            _chirurgenOperationen["KlinischeErgebnisse"] = txtKlinischeErgebnisse.Text;
        }

        protected override void SaveObject()
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
                    if (dontDisplayNextTime)
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
                            dontDisplayNextTime = dlg.dontDisplayNextTime;
                        }
                    }
                }
            }

            if (success)
            {
                Cursor = Cursors.WaitCursor;
                for (int i = 0; i < anzahl; i++)
                {
                    int ID_ChirurgenOperationen = BusinessLayer.InsertChirurgenOperationen(_chirurgenOperationen);
                    if (ID_ChirurgenOperationen <= 0)
                    {
                        break;
                    }
                }
                Cursor = Cursors.Default;
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Control2Object();
                SaveObject();
                _bDataAdded = true;
            }
        }

        private void IstOperationView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle("Prozedur manuell eingeben");

            txtDatum.Text = Utility.Tools.NullableDateTime2DateString(DateTime.Today);
            txtZeit.Text = "10:00";
            txtZeitBis.Text = "12:00";
            txtKlinischeErgebnisse.Text = "";

            PopulateOPFunktionen(cbOPFunktionen);
            PopulateKlinischeErgebnisseTypen(cbKlinischeErgebnisseTypen);
        }

        private void PopulateOPFunktionen(ComboBox cb)
        {
            DataView dv = BusinessLayer.GetOPFunktionen(false);

            cb.ValueMember = "ID_OPFunktionen";
            cb.DisplayMember = "Beschreibung";
            cb.DataSource = dv;
            cb.SelectedValue = (int)OP_FUNCTION.OP_FUNCTION_OP;
        }

        protected override bool ValidateInput()
        {
            bool bSuccess = true;
            string strMessage = EINGABEFEHLER;

            if (GetFirstSelectedTag(lvOperationen) == -1)
            {
                bSuccess = false;
                strMessage += "\n- es muss eine Operation ausgewählt werden";
            }

            if (!Tools.DateIsValidGermanDate(txtDatum.Text))
            {
                bSuccess = false;
                strMessage += "\n- '" + lblDatum.Text + "' ist ein ungültiges Datum";
            }
            if (!Tools.TimeIsValidGermanTime(txtZeit.Text))
            {
                bSuccess = false;
                strMessage += "\n- '" + lblZeit.Text + "' ist eine ungültige Zeit";
            }
            if (!Tools.TimeIsValidGermanTime(txtZeitBis.Text))
            {
                bSuccess = false;
                strMessage += "\n- '" + lblZeitBis.Text + "' ist eine ungültige Zeit";
            }
            if (cbOPFunktionen.SelectedValue == null)
            {
                bSuccess = false;
                strMessage += "\n- '" + lblOPFunktionen.Text + "' muss ausgewählt werden";
            }
            if (cbKlinischeErgebnisseTypen.SelectedValue == null)
            {
                bSuccess = false;
                strMessage += "\n- '" + grpKlinischeErgebnisse.Text + "' muss ausgewählt werden";
            }

            int ID_KlinischeErgebnisseTypenUnauffaellig = BusinessLayer.DatabaseLayer.GetIdKlinischeErgebnisseTypenUnauffaellig();

            if ((int)cbKlinischeErgebnisseTypen.SelectedValue != ID_KlinischeErgebnisseTypenUnauffaellig
                && txtKlinischeErgebnisse.Text.Length == 0)
            {
                bSuccess = false;
                strMessage += "\n- '" + grpKlinischeErgebnisse.Text + "' darf nicht leer sein";
            }

            if (txtFallzahl.Text.Length == 0)
            {
                bSuccess = false;
                strMessage += "\n- '" + lblFallzahl.Text + "' darf nicht leer sein";
            }
            if (txtAnzahl.Text.Length == 0)
            {
                bSuccess = false;
                strMessage += "\n- '" + lblAnzahl.Text + "' darf nicht leer sein";
            }
            else
            {
                int anzahl;
                if (Int32.TryParse(txtAnzahl.Text, out anzahl))
                {
                    if (anzahl < AnzahlMin || anzahl > AnzahlMax)
                    {
                        bSuccess = false;
                        strMessage += string.Format("\n- '" + lblAnzahl.Text + "' muss eine Zahl zwischen {0} und {1} sein", AnzahlMin, AnzahlMax);
                    }
                }
                else
                {
                    bSuccess = false;
                    strMessage += string.Format("\n- '" + lblAnzahl.Text + "' ist keine gültige Zahl zwischen {0} und {1}", AnzahlMin, AnzahlMax);
                }
            }

            if (!bSuccess)
            {
                MessageBox(strMessage);
            }

            return bSuccess;
        }

        protected override void ProgressBegin()
        {
            txtFallzahl.Enabled = false;
            txtDatum.Enabled = false;
            txtZeit.Enabled = false;
            txtZeitBis.Enabled = false;
            cmdOK.Enabled = false;
            cmdCancel.Enabled = false;
            cmdPopulate.Enabled = false;
        }
        protected override void ProgressEnd()
        {
            txtFallzahl.Enabled = true;
            txtDatum.Enabled = true;
            txtZeit.Enabled = true;
            txtZeitBis.Enabled = true;
            cmdOK.Enabled = true;
            cmdCancel.Enabled = true;
            cmdPopulate.Enabled = true;
        }

        private void cmdPopulate_Click(object sender, EventArgs e)
        {
            PopulateOperationen(grpOperationen, lvOperationen, txtFilterKode.Text, txtFilterText.Text);
        }

        private void IstOperationView_Shown(object sender, EventArgs e)
        {
            txtFilterKode.Focus();
        }

        private void IstOperationView_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !MayFormClose();
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
    }
}