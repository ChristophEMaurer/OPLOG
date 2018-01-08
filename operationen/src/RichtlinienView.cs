using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;

using Utility;
using CMaurer.Operationen.AppFramework;

namespace Operationen
{
    public partial class RichtlinienView : OperationenForm
    {
        public RichtlinienView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();

            cmdUp.SetSecurity(BusinessLayer, "RichtlinienView.edit");
            cmdDown.SetSecurity(BusinessLayer, "RichtlinienView.edit");
            cmdInsert.SetSecurity(BusinessLayer, "RichtlinienView.edit");
            cmdApply.SetSecurity(BusinessLayer, "RichtlinienView.edit");
            cmdDelete.SetSecurity(BusinessLayer, "RichtlinienView.edit");
            cmdClear.SetSecurity(BusinessLayer, "RichtlinienView.edit");

            txtLfdNummer.SetSecurity(BusinessLayer, "RichtlinienView.edit");
            txtRichtzahl.SetSecurity(BusinessLayer, "RichtlinienView.edit");
            txtUntBehMethode.SetSecurity(BusinessLayer, "RichtlinienView.edit");

            cmdDown.Enabled =
            cmdUp.Enabled = 
            cmdDelete.Enabled = 
            cmdApply.Enabled = false;
        }

        private void RichtlinienView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));

            lblLegende.ForeColor = BusinessLayer.InfoColor;
            lblLegende.Text = GetText("info1");

            lblInfoRichtzahl.ForeColor = BusinessLayer.InfoColor;
            lblInfoRichtzahl.Text = GetText("info2");

            lblGebiete.ForeColor = BusinessLayer.InfoColor;
            lblGebiete.Text = string.Format(CultureInfo.InvariantCulture, GetText("infoGebiete"), Command_GebieteView);

            PopulateGebiete();
            InitRichtlinien(lvRichtlinien);

            if (lvGebiete.Items.Count > 0)
            {
                lvGebiete.SelectedIndices.Add(0);
            }
        }

        private void PopulateGebiete()
        {
            DataView dv = BusinessLayer.GetGebiete();

            DefaultListViewProperties(lvGebiete);

            lvGebiete.Clear();
            lvGebiete.Columns.Add(GetText("gebiet"), 150, HorizontalAlignment.Left);
            lvGebiete.Columns.Add(GetText("bemerkung"), 150, HorizontalAlignment.Left);
            lvGebiete.Columns.Add(GetText("herkunft"), -2, HorizontalAlignment.Left);

            foreach (DataRow dataRow in dv.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem((string)dataRow["Gebiet"]);

                lvi.Tag = ConvertToInt32(dataRow["ID_Gebiete"]);
                lvi.SubItems.Add((string)dataRow["Bemerkung"]);
                lvi.SubItems.Add((string)dataRow["Herkunft"]);

                lvGebiete.Items.Add(lvi);
            }
        }

        private void ClearTextBoxes()
        {
            if (UserHasRight("RichtlinienView.edit"))
            {
                txtUntBehMethode.Text = "";
                txtRichtzahl.Text = "";
                txtLfdNummer.Text = "";
            }
        }

        private void PopulateRichtlinien()
        {
            //
            // Wenn man alle markierte und löschte, verschwanden die aus der Datenbank,
            // aber die letzte blieb angezeigt???
            //
            ClearTextBoxes();
            lvRichtlinien.Items.Clear();
            Application.DoEvents();

            int ID_Gebiete = GetFirstSelectedTag(lvGebiete);

            if (ID_Gebiete != -1)
            {
                PopulateRichtlinien(ID_Gebiete);
            }
        }

        private void PopulateRichtlinien(int nID_Gebiete)
        {
            ClearTextBoxes();
            lvRichtlinien.Items.Clear();

            PopulateRichtlinien(lvRichtlinien, nID_Gebiete, grpRichtlinien, GetText("richtlinien"));
        }

        private void lvGebiete_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID_Gebiete = GetFirstSelectedTag(lvGebiete);

            if (ID_Gebiete != -1)
            {
                PopulateRichtlinien(ID_Gebiete);

                cmdDown.Enabled =
                cmdUp.Enabled =
                cmdDelete.Enabled =
                cmdApply.Enabled = false;
            }
        }

        private void lvRichtlinien_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvRichtlinien.SelectedItems)
            {
                string strRichtzahl = lvi.SubItems[1].Text.Trim();
                if (strRichtzahl.Length == 0)
                {
                    strRichtzahl = "0";
                }

                txtUntBehMethode.Text = lvi.SubItems[2].Text;
                txtRichtzahl.Text = strRichtzahl;
                txtLfdNummer.Text = lvi.SubItems[0].Text;

                cmdDown.Enabled =
                cmdUp.Enabled =
                cmdDelete.Enabled =
                cmdApply.Enabled = true;

                break;
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (UserHasRight("RichtlinienView.edit"))
            {
                int nCount = lvRichtlinien.SelectedItems.Count;

                if (nCount > 0)
                {
                    if (Confirm(GetTextConfirmDelete(nCount)))
                    {
                        Cursor = Cursors.WaitCursor;

                        try
                        {
                            BusinessLayer.OpenDatabaseForImport();
                            foreach (ListViewItem lvi in lvRichtlinien.SelectedItems)
                            {
                                int nID = (int)lvi.Tag;

                                if (nID != -1)
                                {
                                    if (!BusinessLayer.DeleteRichtlinie(nID))
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
                        PopulateRichtlinien();

                        Cursor = Cursors.Default;
                    }
                }
                else
                {
                    MessageBox(GetTextSelectionNone());
                }
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }

        protected override bool ValidateInput()
        {
            bool bSuccess = true;
            string strMessage = EINGABEFEHLER;

            if (txtUntBehMethode.Text.Length <= 0)
            {
                strMessage += GetTextControlMissingText(lblUntBehMethode);
                bSuccess = false;
            }
            if (txtRichtzahl.Text.Length <= 0)
            {
                strMessage += GetTextControlMissingText(lblRichtzahl);
                bSuccess = false;
            }

            if (txtRichtzahl.Text != BusinessLayerCommon.BasisKenntnis)
            {
                int richtzahl;
                if (!Int32.TryParse(txtRichtzahl.Text, out richtzahl))
                {
                    strMessage += GetTextControlInvalidContents(lblRichtzahl.Text, txtRichtzahl.Text);
                    bSuccess = false;
                }
            }
            if (txtLfdNummer.Text.Length <= 0)
            {
                strMessage += GetTextControlMissingText(lblLfdNummer);
                bSuccess = false;
            }
            int nummer;
            if (!Int32.TryParse(txtLfdNummer.Text, out nummer))
            {
                strMessage += GetTextControlInvalid(lblLfdNummer);
                bSuccess = false;
            }

            if (!bSuccess)
            {
                MessageBox(strMessage);
            }

            return bSuccess;
        }

        private void UpdateRichtlinie()
        {
            if (UserHasRight("RichtlinienView.edit"))
            {
                if (ValidateInput())
                {
                    ListViewItem lvi = GetFirstSelectedLVI(lvRichtlinien, true);
                    if (lvi != null)
                    {
                        int ID_Richtlinie = (int)lvi.Tag;
                        int lviIndex = lvi.Index;
                        DataRow richtlinie = BusinessLayer.GetRichtlinie(ID_Richtlinie);

                        richtlinie["UntBehMethode"] = txtUntBehMethode.Text;
                        if (txtRichtzahl.Text == BusinessLayerCommon.BasisKenntnis)
                        {
                            richtlinie["Richtzahl"] = -1;
                        }
                        else
                        {
                            richtlinie["Richtzahl"] = txtRichtzahl.Text;
                        }
                        richtlinie["LfdNummer"] = txtLfdNummer.Text;

                        if (BusinessLayer.UpdateRichtlinie(richtlinie))
                        {
                            PopulateRichtlinien();
                            EnsureVisibleNearIndex(lvRichtlinien, lviIndex);
                        }
                    }
                }
            }
        }

        private void InsertRichtlinie()
        {
            if (ValidateInput())
            {
                int ID_Gebiete = GetFirstSelectedTag(lvGebiete, true, GetText("facharztgebiet"));

                if (ID_Gebiete > 0)
                {
                    DataRow richtlinie = BusinessLayer.CreateDataRowRichtlinie();

                    richtlinie["ID_Gebiete"] = ID_Gebiete;
                    richtlinie["UntBehMethode"] = txtUntBehMethode.Text;
                    if (txtRichtzahl.Text == BusinessLayerCommon.BasisKenntnis)
                    {
                        richtlinie["Richtzahl"] = -1;
                    }
                    else
                    {
                        richtlinie["Richtzahl"] = txtRichtzahl.Text;
                    }
                    richtlinie["LfdNummer"] = txtLfdNummer.Text;

                    if (BusinessLayer.InsertRichtlinie(richtlinie) != -1)
                    {
                        string strLfdNummer = txtLfdNummer.Text;
                        PopulateRichtlinien();

                        try
                        {
                            int nLfdNummer;

                            if (Int32.TryParse(strLfdNummer, out nLfdNummer))
                            {
                                txtLfdNummer.Text = (nLfdNummer + 1).ToString();
                            }
                        }
                        catch
                        {
                        }

                        //
                        // Wenn man eine Zeile eingefügt hatte, will man die sehen, sonst ist das echt ätzend
                        // Die Richtzahl ist verändert worden! Es wurde immer ganz unten eingefügt, also immer unterste Zeile
                        // sichtbar machen!
                        //
                        EnsureVisibleNearIndex(lvRichtlinien, lvRichtlinien.Items.Count - 1);
                    }
                }
            }
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            if (UserHasRight("RichtlinienView.edit"))
            {
                InsertRichtlinie();
                txtRichtzahl.Focus();
            }
        }

        /// <summary>
        /// Die laufende Nummer einer Richtlinie ändern: diese Richtlinie eins nach oben verschieben.
        /// </summary>
        private void MoveRichtlinieUp()
        {
            if (UserHasRight("RichtlinienView.edit"))
            {
                foreach (int index in lvRichtlinien.SelectedIndices)
                {
                    ListViewItem lvi = lvRichtlinien.Items[index];
                    int indexPrev = index - 1;

                    if (indexPrev >= 0)
                    {
                        ListViewItem lviPrev = lvRichtlinien.Items[indexPrev];

                        int ID_Richtlinie = (int)lvi.Tag;
                        int ID_RichtliniePrev = (int)lviPrev.Tag;

                        DataRow row = BusinessLayer.GetRichtlinie(ID_Richtlinie);
                        DataRow rowPrev = BusinessLayer.GetRichtlinie(ID_RichtliniePrev);

                        int temp = ConvertToInt32(row["LfdNummer"]);
                        row["LfdNummer"] = ConvertToInt32(rowPrev["LfdNummer"]);
                        rowPrev["LfdNummer"] = temp;

                        BusinessLayer.UpdateRichtlinie(row);
                        BusinessLayer.UpdateRichtlinie(rowPrev);
                        PopulateRichtlinien();

                        lvRichtlinien.SelectedIndices.Add(indexPrev);
                        lvRichtlinien.EnsureVisible(indexPrev);
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Bewegt eine Richtlinie nach unten, das heißt, ihre laufende Nummer wird mit der laufenden Nummer der Zeile darunter vertauscht.
        /// Wenn die durcheinander sind und dieselbe Nummer haben, passiert nichts. Man muss dann die Nummer ändern und "Ändern" klicken
        /// </summary>
        
        private void MoveRichtlinieDown()
        {
            if (UserHasRight("RichtlinienView.edit"))
            {
                foreach (int index in lvRichtlinien.SelectedIndices)
                {
                    ListViewItem lvi = lvRichtlinien.Items[index];
                    int indexNext = index + 1;

                    if (indexNext < lvRichtlinien.Items.Count)
                    {
                        ListViewItem lviNext = lvRichtlinien.Items[indexNext];

                        int ID_Richtlinie = (int)lvi.Tag;
                        int ID_RichtlinieNext = (int)lviNext.Tag;

                        DataRow row = BusinessLayer.GetRichtlinie(ID_Richtlinie);
                        DataRow rowNext = BusinessLayer.GetRichtlinie(ID_RichtlinieNext);

                        int temp = ConvertToInt32(row["LfdNummer"]);
                        row["LfdNummer"] = ConvertToInt32(rowNext["LfdNummer"]);
                        rowNext["LfdNummer"] = temp;

                        BusinessLayer.UpdateRichtlinie(row);
                        BusinessLayer.UpdateRichtlinie(rowNext);

                        PopulateRichtlinien();

                        lvRichtlinien.SelectedIndices.Add(indexNext);
                        lvRichtlinien.EnsureVisible(indexNext);
                    }
                    break;
                }
            }
        }
        private void cmdUp_Click(object sender, EventArgs e)
        {
            MoveRichtlinieUp();
        }

        private void cmdDown_Click(object sender, EventArgs e)
        {
            MoveRichtlinieDown();
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            UpdateRichtlinie();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RichtlinienView_Shown(object sender, EventArgs e)
        {
            if (UserHasRight("RichtlinienView.edit"))
            {
                txtRichtzahl.Focus();
            }
        }
    }
}