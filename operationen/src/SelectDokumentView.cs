using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Operationen
{
    public partial class SelectDokumentView : OperationenForm
    {
        private DataRow _oDokument = null;

        public SelectDokumentView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();
        }

        private void DokumenteView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));

            FillListViewOverview();
        }
        private void FillListViewOverview()
        {
            DataView dataview = BusinessLayer.GetDokumente();

            lvDokumente.Clear();

            DefaultListViewProperties(lvDokumente);

            lvDokumente.Columns.Add(GetText("gruppe"), 100, HorizontalAlignment.Left);
            lvDokumente.Columns.Add(GetText("lfdNr"), 50, HorizontalAlignment.Left);
            lvDokumente.Columns.Add(GetText("beschreibung"), 150, HorizontalAlignment.Left);
            lvDokumente.Columns.Add(GetText("dateiname"), 250, HorizontalAlignment.Left);
            lvDokumente.Columns.Add(GetText("dokumentenstatus"), -2, HorizontalAlignment.Left);

            DataTable dataTable = dataview.Table;

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow dataRow = dataTable.Rows[i];
                ListViewItem lvi = new ListViewItem((string)dataRow["Gruppe"]);
                lvi.Tag = ConvertToInt32(dataRow["ID_Dokumente"]);
                lvi.SubItems.Add(dataRow["LfdNummer"].ToString());
                lvi.SubItems.Add((string)dataRow["Beschreibung"]);
                lvi.SubItems.Add((string)dataRow["Dateiname"]);

                if (FileExists((string)dataRow["Dateiname"]))
                {
                    lvi.SubItems.Add(GetText("vorhanden"));
                }
                else
                {
                    lvi.SubItems.Add(GetText("dateiFehlt"));
                }

                lvDokumente.Items.Add(lvi);
            }
        }

        private bool FileExists(string strFilename)
        {
            bool bSuccess = false;

            string strFullPath = BusinessLayer.PathDokumente + System.IO.Path.DirectorySeparatorChar + strFilename;
            if (File.Exists(strFullPath))
            {
                bSuccess = true;
            }

            return bSuccess;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            int nID_Dokumente = GetFirstSelectedTag(lvDokumente);
            if (nID_Dokumente != -1)
            {
                _oDokument = BusinessLayer.GetDokument(nID_Dokumente);
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        public DataRow Dokument
        {
            get { return _oDokument; }
        }
    }
}

