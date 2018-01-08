using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;

using Utility;

namespace Operationen
{
    /// <summary>
    /// Verwaltet die Tabelle Dokumente.
    /// Dokumente enthält Dateien, die für einen Chirurgen als Logbuch angelegt werden können.
    /// Wen man eine neue Datei anlegt, wird die nach 
    /// Server\Dokumente kopiert und nur der Dateiname ohne Pfad
    /// in die Datenbank eingetragen.
    /// 
    /// Der Dateiname kann auch nicht geändert werden, denn dann
    /// muesste man die Datei umbenennen, das ist zu kompliziert.
    /// </summary>
    public partial class DokumenteView : OperationenForm
    {
        private int MinLfdNummer = 1;
        private int MaxLfdNummer = 99;

        public DokumenteView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();
            cmdApply.Enabled = false;
        }

        private void DokumenteView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));
            
            SetInfoText(lblInfo, string.Format(CultureInfo.InvariantCulture, GetText("info"), Command_ChirurgDokumenteView));

            lblMinMax.Text = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", MinLfdNummer, MaxLfdNummer);

            InitDokumente();
            PopulateDokumente();

            //
            // Erst wenn es das Recht "DateienView.view" gibt, braucht man OplButton zu nehmen.
            //

            cmdUp.Enabled =
            cmdDown.Enabled =
            cmdDelete.Enabled =
            cmdInsert.Enabled =
            cmdApply.Enabled =
            cmdClear.Enabled =
            cmdDateiname.Enabled =
            txtGruppe.Enabled =
            txtLfdNummer.Enabled =
            txtBeschreibung.Enabled = UserHasRight("DokumenteView.edit");
        }

        private void InitDokumente()
        {
            DefaultListViewProperties(lvDokumente);
            lvDokumente.MultiSelect = false;

            lvDokumente.Clear();
            lvDokumente.Columns.Add(GetText("gruppe"), 140, HorizontalAlignment.Left);
            lvDokumente.Columns.Add(GetText("nr"), 30, HorizontalAlignment.Left);
            lvDokumente.Columns.Add(GetText("beschreibung"), 200, HorizontalAlignment.Left);
            lvDokumente.Columns.Add(GetText("dateiname"), -2, HorizontalAlignment.Left);
        }

        private void ClearTextBoxes()
        {
            txtBeschreibung.Text = "";
            txtGruppe.Text = "";
            txtDateiname.Text = "";
            txtLfdNummer.Text = "";
        }

        private void PopulateDokumente()
        {
            cmdApply.Enabled = false;

            DataView dv = BusinessLayer.GetDokumente();

            ClearTextBoxes();
            lvDokumente.Items.Clear();

            foreach (DataRow dataRow in dv.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem((string)dataRow["Gruppe"]);
                lvi.Tag = ConvertToInt32(dataRow["ID_Dokumente"]);

                lvi.SubItems.Add(dataRow["LfdNummer"].ToString());
                lvi.SubItems.Add((string)dataRow["Beschreibung"]);
                lvi.SubItems.Add((string)dataRow["Dateiname"]);

                lvDokumente.Items.Add(lvi);
            }

            SetGroupBoxText(lvDokumente, grpDokumente, "Dokumente");
        }

        private void lvDokumente_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvDokumente.SelectedItems)
            {
                if (UserHasRight("DokumenteView.edit"))
                {
                    cmdApply.Enabled = true;
                }

                txtGruppe.Text = lvi.SubItems[0].Text;
                txtLfdNummer.Text = lvi.SubItems[1].Text;
                txtBeschreibung.Text = lvi.SubItems[2].Text;
                txtDateiname.Text = lvi.SubItems[3].Text;
                break;
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            int ID_Dokumente = GetFirstSelectedTag(lvDokumente, true);

            if (ID_Dokumente != -1)
            {
                string msg = GetTextConfirmDelete(1);

                if (Confirm(msg))
                {
                    DataRow row = BusinessLayer.GetDokument(ID_Dokumente);
                    string fileName = BusinessLayer.PathDokumente + System.IO.Path.DirectorySeparatorChar + (string)row["Dateiname"];

                    BusinessLayer.DeleteDokument(ID_Dokumente);

                    if (!Tools.DeleteFile(fileName))
                    {
                        msg = GetTextFileCouldNotBeDeleted(fileName);
                        MessageBox(msg);
                    }
                    PopulateDokumente();
                }
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }

        protected override bool ValidateInput()
        {
            // Hier darf man nicht überprüfen, ob es die Datei
            // txtDateinamen.Text gibt, weil beim Neuanlegen der
            // Ganze Pfad drin steht
            // und bei Update nur der Dateiname ohne Pfad.
            bool bSuccess = true;
            string strMessage = EINGABEFEHLER;

            if (txtGruppe.Text.Length <= 0)
            {
                strMessage += GetTextControlMissingText(lblGruppe);
                bSuccess = false;
            }
            if (txtBeschreibung.Text.Length <= 0)
            {
                strMessage += GetTextControlMissingText(lblBeschreibung);
                bSuccess = false;
            }
            if (txtDateiname.Text.Length <= 0)
            {
                strMessage += GetTextControlMissingText(lblDateiname);
                bSuccess = false;
            }
            if (txtLfdNummer.Text.Length <= 0)
            {
                strMessage += GetTextControlMissingText(lblLfdNummer);
                bSuccess = false;
            }

            if (!Tools.IsIntBetween(txtLfdNummer.Text, MinLfdNummer, MaxLfdNummer))
            {
                strMessage += string.Format(CultureInfo.InvariantCulture, GetText("errMinMax"), 
                    lblLfdNummer.Text, MinLfdNummer, MaxLfdNummer);
                bSuccess = false;
            }

            if (!bSuccess)
            {
                MessageBox(strMessage);
            }

            return bSuccess;
        }

        /// <summary>
        /// Aktualisiert ein Dokument.
        /// Der Dateiname darf NICHT geändert werden, da
        /// das Dokument auch nach Server\Dokumente kopiert wurde.
        /// </summary>
        private void UpdateDokument()
        {
            if (ValidateInput())
            {
                int ID_Dokumente = (int)GetFirstSelectedTag(lvDokumente);

                DataRow dokument = BusinessLayer.GetDokument(ID_Dokumente);

                if (dokument != null)
                {
                    dokument["Gruppe"] = txtGruppe.Text;
                    dokument["Beschreibung"] = txtBeschreibung.Text;
                    dokument["LfdNummer"] = txtLfdNummer.Text;

                    if (BusinessLayer.UpdateDokument(dokument))
                    {
                        PopulateDokumente();
                    }
                }
            }
        }

        private void InsertDokument()
        {
            if (ValidateInput())
            {
                DataRow row = BusinessLayer.CreateDataRowDokument();

                string src = txtDateiname.Text;

                if (!File.Exists(src))
                {
                    MessageBox(GetTextFileCouldNotBeFound(src));
                    goto Exit;
                }

                int index = src.LastIndexOf(System.IO.Path.DirectorySeparatorChar);
                if ((index != -1) && (src.Length >= index + 1))
                {
                    string fileName = src.Substring(index + 1);

                    string dst = BusinessLayer.PathDokumente + System.IO.Path.DirectorySeparatorChar + fileName;

                    if (File.Exists(dst))
                    {
                        // Zieldatei darf niemals überschrieben werden
                        MessageBox(GetTextFileAlreadyExists(dst));
                        goto Exit;
                    }

                    try
                    {
                        File.Copy(src, dst, false);
                    }
                    catch
                    {
                        MessageBox(GetTextFileCouldNotBeCopied(src, dst));
                        goto Exit;
                    }

                    row["Gruppe"] = txtGruppe.Text;
                    row["Beschreibung"] = txtBeschreibung.Text;
                    row["Dateiname"] = fileName;

                    if (BusinessLayer.InsertDokument(row) != -1)
                    {
                        PopulateDokumente();
                    }
                }
            }
            Exit: ;
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            InsertDokument();
        }

        /// <summary>
        /// Die laufende Nummer eines Dokumentes ändern: dieses Dokument eins nach oben verschieben
        /// innerhalb der Dokumente mit demselben Gruppennamen.
        /// </summary>
        private void MoveDokumentUp()
        {
            foreach (int index in lvDokumente.SelectedIndices)
            {
                ListViewItem lvi = lvDokumente.Items[index];
                int indexPrev = index - 1;

                if (indexPrev >= 0)
                {
                    ListViewItem lviPrev = lvDokumente.Items[indexPrev];

                    if (lviPrev.SubItems[0].ToString() == lvi.SubItems[0].ToString())
                    {
                        int ID_Dokumente = (int)lvi.Tag;
                        int ID_DokumentePrev = (int)lviPrev.Tag;

                        DataRow row = BusinessLayer.GetDokument(ID_Dokumente);
                        DataRow rowPrev = BusinessLayer.GetDokument(ID_DokumentePrev);

                        int temp = ConvertToInt32(row["LfdNummer"]);
                        row["LfdNummer"] = ConvertToInt32(rowPrev["LfdNummer"]);
                        rowPrev["LfdNummer"] = temp;

                        BusinessLayer.UpdateDokument(row);
                        BusinessLayer.UpdateDokument(rowPrev);

                        PopulateDokumente();

                        lvDokumente.SelectedIndices.Add(indexPrev);
                    }
                }
                break;
            }
        }
        private void MoveDokumentDown()
        {
            foreach (int index in lvDokumente.SelectedIndices)
            {
                ListViewItem lvi = lvDokumente.Items[index];
                int indexNext = index + 1;

                if (indexNext < lvDokumente.Items.Count)
                {
                    ListViewItem lviNext = lvDokumente.Items[indexNext];

                    if (lviNext.SubItems[0].ToString() == lvi.SubItems[0].ToString())
                    {
                        int ID_Dokumente = (int)lvi.Tag;
                        int ID_DokumenteNext = (int)lviNext.Tag;

                        DataRow row = BusinessLayer.GetDokument(ID_Dokumente);
                        DataRow rowNext = BusinessLayer.GetDokument(ID_DokumenteNext);

                        int temp = ConvertToInt32(row["LfdNummer"]);
                        row["LfdNummer"] = ConvertToInt32(rowNext["LfdNummer"]);
                        rowNext["LfdNummer"] = temp;

                        BusinessLayer.UpdateDokument(row);
                        BusinessLayer.UpdateDokument(rowNext);

                        PopulateDokumente();

                        lvDokumente.SelectedIndices.Add(indexNext);
                    }
                }
                break;
            }
        }
        private void cmdUp_Click(object sender, EventArgs e)
        {
            MoveDokumentUp();
        }

        private void cmdDown_Click(object sender, EventArgs e)
        {
            MoveDokumentDown();
        }

        private void cmdDateiname_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "*.doc|*.*";
            if (DialogResult.OK == dlg.ShowDialog())
            {
                string fileName = dlg.FileName;
                
                txtDateiname.Text = fileName;
            }
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            UpdateDokument();
        }

        private void cmdDisplay_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = GetFirstSelectedLVI(lvDokumente, true);

            if (lvi != null)
            {
                string fileName = lvi.SubItems[3].Text;
                launchFile(fileName);
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
