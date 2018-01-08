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
    /// Verwaltet die Tabelle Dateien.
    /// Dateien enthält Dateien, die angezeigt werden können.
    /// Für jeden Dateitypen gibt es einen Menüpunkt unter Dokumente.
    /// Für jede Datei gibt es einen entsprechenden Untermenüpunkt.
    /// </summary>
    public partial class DateienView : OperationenForm
    {
        public DateienView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();
        }

        private void DateienView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));

            SetInfoText(lblInfo, GetText("info"));

            //
            // Erst wenn es das Recht "DateienView.view" gibt, braucht man OplButton zu nehmen.
            //
            cmdDisplay.Enabled =
            cmdDelete.Enabled =
            cmdInsert.Enabled =
            cmdApply.Enabled =
            cmdDateiname.Enabled =
            cmdClear.Enabled =
            cbDateiTypen.Enabled =
            txtBeschreibung.Enabled = UserHasRight("DateienView.edit");

            PopulateDateiTypen();
            InitDateien();
            PopulateDateien();
        }

        private void InitDateien()
        {
            DefaultListViewProperties(lvDateien);
            lvDateien.MultiSelect = false;

            lvDateien.Clear();
            lvDateien.Columns.Add(GetText("dateiart"), 100, HorizontalAlignment.Left);
            lvDateien.Columns.Add(GetText("menuetext"), 200, HorizontalAlignment.Left);
            lvDateien.Columns.Add(GetText("dateiname"), -2, HorizontalAlignment.Left);
        }

        private void PopulateDateiTypen()
        {
            DataView dv = BusinessLayer.GetDateiTypen();

            cbDateiTypen.ValueMember = "ID_DateiTypen";
            cbDateiTypen.DisplayMember = "DateiTyp";
            cbDateiTypen.DataSource = dv;
        }

        private void ClearTextBoxes()
        {
            txtBeschreibung.Text = "";
            txtDateiname.Text = "";
        }

        private void PopulateDateien()
        {
            cmdApply.Enabled = false;

            DataView dv = BusinessLayer.GetDateien();

            ClearTextBoxes();
            lvDateien.Items.Clear();

            foreach (DataRow dataRow in dv.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem((string)dataRow["DateiTyp"]);
                lvi.Tag = ConvertToInt32(dataRow["ID_Dateien"]);

                lvi.SubItems.Add((string)dataRow["Beschreibung"]);
                lvi.SubItems.Add((string)dataRow["Dateiname"]);

                lvDateien.Items.Add(lvi);
            }

            SetGroupBoxText(lvDateien, grpDateien, GetText("eigenedateien"));
        }

        private void lvDateien_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvDateien.SelectedItems)
            {
                txtBeschreibung.Text = lvi.SubItems[1].Text;
                txtDateiname.Text = lvi.SubItems[2].Text;

                // In der Combobox den Eintrag aus der Liste selektieren.
                // In der Liste ist der Anzeigetext, aber nicht die ID_DateiTypen,
                // daher wird der Anzeigetext gesucht und darüber selektiert.
                int index = cbDateiTypen.FindStringExact(lvi.SubItems[0].Text);

                if (index != -1)
                {
                    cbDateiTypen.SelectedIndex = index;
                }

                if (UserHasRight("DateienView.edit"))
                {
                    cmdApply.Enabled = true;
                }

                break;
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            int ID_Dateien = GetFirstSelectedTag(lvDateien, true);

            if (ID_Dateien != -1)
            {
                string msg = GetTextConfirmDeleteSimple(1);

                if (Confirm(msg))
                {
                    DataRow row = BusinessLayer.GetDatei(ID_Dateien);
                    string fileName = BusinessLayer.PathDokumente + System.IO.Path.DirectorySeparatorChar + (string)row["Dateiname"];

                    BusinessLayer.DeleteDatei(ID_Dateien);

                    if (!Tools.DeleteFile(fileName))
                    {
                        msg = string.Format(CultureInfo.InvariantCulture, GetText("errDeleteFile"), fileName);
                        MessageBox(msg);
                    }
                    PopulateDateien();
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

            if (cbDateiTypen.SelectedIndex == -1)
            {
                strMessage += GetText("errNoDateiart");
                bSuccess = false;
            }

            if (!bSuccess)
            {
                MessageBox(strMessage);
            }

            return bSuccess;
        }

        private void InsertDatei()
        {
            if (ValidateInput())
            {
                DataRow row = BusinessLayer.CreateDataRowDatei();

                string src = txtDateiname.Text;

                if (!File.Exists(src))
                {
                    string msg = string.Format(CultureInfo.InvariantCulture, GetText("errFileNotFound"), src);
                    MessageBox(msg);
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
                        string msg = string.Format(CultureInfo.InvariantCulture, GetText("errFileExists"), dst);
                        MessageBox(msg);
                        goto Exit;
                    }

                    try
                    {
                        File.Copy(src, dst, false);
                    }
                    catch
                    {
                        string msg = string.Format(CultureInfo.InvariantCulture, GetText("errFileNoCopy"), src, dst);
                        MessageBox(msg);
                        goto Exit;
                    }

                        row["ID_DateiTypen"] = cbDateiTypen.SelectedValue;
                        row["Beschreibung"] = txtBeschreibung.Text;
                        row["Dateiname"] = fileName;

                        if (BusinessLayer.InsertDatei(row) != -1)
                        {
                            PopulateDateien();
                        }
                }
            }
            Exit: ;
        }

        private void UpdateDatei()
        {
            if (ValidateInput())
            {
                int ID_Dateien = GetFirstSelectedTag(lvDateien, true);

                DataRow row = BusinessLayer.GetDatei(ID_Dateien);

                row["ID_DateiTypen"] = cbDateiTypen.SelectedValue;
                row["Beschreibung"] = txtBeschreibung.Text;

                BusinessLayer.UpdateDatei(row);
                PopulateDateien();
            }
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            InsertDatei();
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

        private void cmdDisplay_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = GetFirstSelectedLVI(lvDateien, true);

            if (lvi != null)
            {
                string fileName = lvi.SubItems[2].Text;
                launchFile(fileName);
            }
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            UpdateDatei();
        }

        private void DateienView_Resize(object sender, EventArgs e)
        {
            grpDateien.Height = lblInfo.Top - grpDateien.Top - ControlYOffset;
        }
    }
}

