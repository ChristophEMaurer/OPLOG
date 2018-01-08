using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using Utility;

namespace Operationen
{
    public partial class ChirurgDokumenteView : OperationenForm
    {
        public ChirurgDokumenteView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();
            this.Text = AppTitle(GetText("title"));

            cmdEdit.Enabled = cmdUpdate.Enabled = cmdDelete.Enabled = cmdView.Enabled = false;
        }

        private void ChirurgDokumenteView_Load(object sender, EventArgs e)
        {
            SetInfoText(lblInfo, string.Format(GetText("info"), cmdEdit.Text, cmdUpdate.Text, cmdView.Text, Command_DokumenteView));

            PopulateChirurgen(lvChirurgen);
        }

        private void PopulateDokumente(int nID_Chirurgen)
        {
            bool mayEdit = (nID_Chirurgen == BusinessLayer.CurrentUser_ID_Chirurgen);

            cmdEdit.Enabled = cmdUpdate.Enabled = cmdDelete.Enabled = cmdView.Enabled = mayEdit;

            DataView dataview = BusinessLayer.GetChirurgenDokumente(nID_Chirurgen);

            lvDokumente.Clear();

            DefaultListViewProperties(lvDokumente);

            lvDokumente.Columns.Add(GetText("Beschreibung"), 200, HorizontalAlignment.Left);
            lvDokumente.Columns.Add(GetText("Status"), -2, HorizontalAlignment.Left);

            foreach (DataRow dataRow in dataview.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem(dataRow["Beschreibung"].ToString());
                lvi.Tag = ConvertToInt32(dataRow["ID_ChirurgenDokumente"]);
                if (ConvertToInt32(dataRow["InBearbeitung"]) != 0)
                {
                    lvi.SubItems.Add(GetText("BearbeitetAm") + Tools.DBNullableDateTime2DateString(dataRow["Bearbeitungsdatum"]));
                }
                else
                {
                    lvi.SubItems.Add(GetText(FormName, "OK"));
                }
                lvDokumente.Items.Add(lvi);
            }
        }

        private void PopulateDokumente()
        {
            int nID_Chirurgen = GetFirstSelectedTag(lvChirurgen);

            if (nID_Chirurgen != -1)
            {
                PopulateDokumente(nID_Chirurgen);
            }
        }

        private void lvChirurgen_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateDokumente();
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            ExtractDocument();
        }
        private void ExtractDocument()
        {
            int nID_ChirurgenDokumente = GetFirstSelectedTag(lvDokumente, true, grpBooks.Text);

            if (nID_ChirurgenDokumente != -1)
            {
                ExtractDocument(nID_ChirurgenDokumente);
            }
        }

        private string CreateDokumentName(DataRow oChirurgenDokument, DataRow oChirurg, DataRow oDokument)
        {
            string strFilename = BusinessLayer.PathEdit + Path.DirectorySeparatorChar
                + (string)oChirurg["UserID"]
                + "_" + oChirurgenDokument["ID_ChirurgenDokumente"].ToString()
                + "_" + (string)oDokument["Dateiname"];

            return strFilename;
        }

        private void ExtractDocument(int nID_ChirurgenDokumente)
        {
            string strPath = BusinessLayer.PathEdit;

            DataRow oChirurgenDokument = BusinessLayer.GetChirurgenDokument(nID_ChirurgenDokumente);
            DataRow oDokument = BusinessLayer.GetDokument(ConvertToInt32(oChirurgenDokument["ID_Dokumente"]));
            DataRow oChirurg = BusinessLayer.GetChirurg(ConvertToInt32(oChirurgenDokument["ID_Chirurgen"]));

            string strFilename = CreateDokumentName(oChirurgenDokument, oChirurg, oDokument);

            bool bExtract = true;

            if (File.Exists(strFilename))
            {
                // Die Datei gibts schon
                // Cancel:     die vorhandene editieren
                // OK:         überschreiben und editieren
                PendingDokumentOverwriteView dlg = new PendingDokumentOverwriteView(BusinessLayer);
                DialogResult result = dlg.ShowDialog();
                if (result == DialogResult.Abort)
                {
                    goto Exit;
                }
                if (result != DialogResult.OK)
                {
                    bExtract = false;
                }
            }

            if (bExtract)
            {
                // überschreiben
                Byte[] b = (byte[])oChirurgenDokument["Blob"];

                System.IO.FileStream fs = new System.IO.FileStream(strFilename, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                fs.Write(b, 0, b.Length);
                fs.Close();
            }

            BusinessLayer.UpdateChirurgenDokumenteBearbeitung(nID_ChirurgenDokumente);

            launchFileDirect(strFilename);

            PopulateDokumente();

        Exit: ;
        }

        /// <summary>
        /// Format einer Datei im Bearbeitungsverzeichnis ist
        /// [UserID]_[ID_ChirurgenDokumente]_[Dateiname]
        /// Die Datei aus dem Verzeichnis Edit in die Datenbank bewegen
        /// und anschließend löschen.
        /// </summary>
        private void UpdateDokument()
        {
            int nID_Chirurgen = GetFirstSelectedTag(lvChirurgen, true, grpUser.Text);
            if (nID_Chirurgen == -1)
            {
                goto Exit;
            }
            int nID_ChirurgenDokumente = GetFirstSelectedTag(lvDokumente, true, grpBooks.Text);
            if (nID_ChirurgenDokumente == -1)
            {
                goto Exit;
            }
            PendingDokumenteView dlg = new PendingDokumenteView(BusinessLayer, nID_Chirurgen);
            if (DialogResult.OK != dlg.ShowDialog())
            {
                goto Exit;
            }

            string strFullName = dlg.FullName;
            if (strFullName.Length == 0)
            {
                goto Exit;
            }

            if (!File.Exists(strFullName))
            {
                MessageBox(string.Format(GetText("missingFile"), strFullName));
                goto Exit;
            }

            // Wenn im Pfad vor dem Edit-Verzeichnis Unterstriche vorkommen,
            // funktionierte das nicht mehr.
            int nPosFrom = strFullName.IndexOf('_', BusinessLayer.PathEdit.Length);
            int nPosFromTo = -1;
            if (nPosFrom != -1)
            {
                nPosFromTo = strFullName.IndexOf('_', nPosFrom + 1);
            }
            if (nPosFrom == -1 || nPosFromTo == -1)
            {
                MessageBox(string.Format(GetText("err_format"), strFullName));
                goto Exit;
            }

            string strID_ChirurgenDokumente = strFullName.Substring(nPosFrom + 1, nPosFromTo - nPosFrom - 1);
            if (!Int32.TryParse(strID_ChirurgenDokumente, out nID_ChirurgenDokumente))
            {
                MessageBox(string.Format(GetText("err_format2"), strFullName, strID_ChirurgenDokumente));
                goto Exit;
            }

            System.IO.FileStream fs = null;

            try
            {
                fs = new System.IO.FileStream(strFullName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                Byte[] b = new Byte[fs.Length];
                fs.Read(b, 0, b.Length);

                //
                // must close file or it cannot be deleted!
                //
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                    fs = null;
                }

                DataRow row = BusinessLayer.CreateDataRowChirurgenDokumente(-1, -1);
                row["ID_ChirurgenDokumente"] = nID_ChirurgenDokumente;
                row["Blob"] = b;
                if (BusinessLayer.UpdateChirurgenDokumente(row))
                {
                    Utility.Tools.DeleteFile(strFullName);
                }
                else
                {
                    MessageBox(string.Format(GetText("err_insert"), strFullName));
                    goto Exit;
                }
            }
            catch (Exception e)
            {
                MessageBox(e.Message);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                    fs = null;
                }
            }

            PopulateDokumente();

            Exit: ;
        }

        private void InsertNewDokument()
        {
            int nID_Chirurgen = GetFirstSelectedTag(lvChirurgen, true, grpUser.Text);
            if (nID_Chirurgen != -1)
            {
                SelectDokumentView dlg = new SelectDokumentView(BusinessLayer);

                if (DialogResult.OK == dlg.ShowDialog())
                {
                    DataRow oDokument = dlg.Dokument;

                    if (oDokument != null)
                    {
                        string strFilename = BusinessLayer.PathDokumente + System.IO.Path.DirectorySeparatorChar + (string)oDokument["Dateiname"];

                        try
                        {
                            System.IO.FileStream fs = new System.IO.FileStream(strFilename, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                            Byte[] b = new Byte[fs.Length];
                            fs.Read(b, 0, b.Length);
                            fs.Close();

                            DataRow row = BusinessLayer.CreateDataRowChirurgenDokumente(nID_Chirurgen, ConvertToInt32(oDokument["ID_Dokumente"]));
                            row["Blob"] = b;

                            BusinessLayer.InsertChirurgenDokumente(row);
                            PopulateDokumente();
                        }
                        catch (Exception ex)
                        {
                            MessageBox(GetText("err_read")
                                + "\r\r'" + strFilename + "'"
                                + "\r\r" + ex.Message);
                        }
                    }
                }
            }
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            InsertNewDokument();
        }

        private void DeleteDokument()
        {
            int nID_ChirurgenDokumente = GetFirstSelectedTag(lvDokumente, true, grpBooks.Text);

            if (nID_ChirurgenDokumente != -1)
            {
                if (Confirm(GetText("confirm_delete")))
                {
                    // Erst die Datei löschen, dann den Datenbankeintrag, denn den braucht man,
                    // um den Dateinamen zu erzeugen
                    string strPath = BusinessLayer.PathEdit;

                    DataRow oChirurgenDokument = BusinessLayer.GetChirurgenDokument(nID_ChirurgenDokumente);
                    DataRow oDokument = BusinessLayer.GetDokument(ConvertToInt32(oChirurgenDokument["ID_Dokumente"]));
                    DataRow oChirurg = BusinessLayer.GetChirurg(ConvertToInt32(oChirurgenDokument["ID_Chirurgen"]));

                    string strFilename = CreateDokumentName(oChirurgenDokument, oChirurg, oDokument);

                    if (Tools.DeleteFile(strFilename))
                    {
                        BusinessLayer.DeleteChirurgenDokumente(nID_ChirurgenDokumente);
                    }
                    else
                    {
                        MessageBox(string.Format(GetText("err_delete"), strFilename));
                    }

                    PopulateDokumente();
                }
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            DeleteDokument();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            UpdateDokument();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Wenn das Dokument schon auf dem PC ist, dann dieses nehmen, ansonsten aus der Datenbank.
        /// In jedem Fall wird es kopiert und eine Änderunge nicht wirksam.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdView_Click(object sender, EventArgs e)
        {
            int nID_ChirurgenDokumente = GetFirstSelectedTag(lvDokumente, true, grpBooks.Text);

            if (nID_ChirurgenDokumente != -1)
            {
                DataRow oChirurgenDokument = BusinessLayer.GetChirurgenDokument(nID_ChirurgenDokumente);
                DataRow oDokument = BusinessLayer.GetDokument(ConvertToInt32(oChirurgenDokument["ID_Dokumente"]));
                DataRow oChirurg = BusinessLayer.GetChirurg(ConvertToInt32(oChirurgenDokument["ID_Chirurgen"]));

                string dateiName = (string)oDokument["Dateiname"];
                string strTempFilename = System.IO.Path.GetTempFileName() + "." + dateiName;

                string strLocalFilename = CreateDokumentName(oChirurgenDokument, oChirurg, oDokument);
                if (File.Exists(strLocalFilename))
                {
                    if (BusinessLayer.CopyFile(strLocalFilename, strTempFilename, true, false))
                    {
                        launchFileDirect(strTempFilename);
                    }
                }
                else
                {
                    Byte[] b = (byte[])oChirurgenDokument["Blob"];

                    System.IO.FileStream fs = new System.IO.FileStream(strTempFilename, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                    fs.Write(b, 0, b.Length);
                    fs.Close();

                    launchFileDirect(strTempFilename);
                }
            }
        }
    }
}


