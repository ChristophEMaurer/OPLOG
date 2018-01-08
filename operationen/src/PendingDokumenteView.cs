using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace Operationen
{
    public partial class PendingDokumenteView : OperationenForm
    {
        private string _strFullName = "";
        private DataRow _oChirurg;

        public PendingDokumenteView(BusinessLayer businessLayer, int nID_Chirurgen)
            : base(businessLayer)
        {
            _oChirurg = businessLayer.GetChirurg(nID_Chirurgen);

            InitializeComponent();

            this.Text = AppTitle(GetText("title"));

            string text = string.Format(CultureInfo.InvariantCulture, GetText("info"), cmdOK.Text);
            SetInfoText(lblInfo, text);
        }

        private void PendingDokumenteView_Load(object sender, EventArgs e)
        {
            PopulateDokumente();
        }

        public string FullName
        {
            get { return _strFullName; }
        }
        private void PopulateDokumente()
        {
            lvDokumente.Clear();

            DefaultListViewProperties(lvDokumente);

            lvDokumente.Columns.Add(GetText("dateiname"), -2, HorizontalAlignment.Left);

            string strDirectory = BusinessLayer.PathEdit;
            DirectoryInfo dir = new DirectoryInfo(strDirectory);

            // Admin sieht alle, normale user nur die eigenen.
            string strFilter;

            if (UserHasRight("cmd.viewAllDocs"))
            {
                strFilter = "*.*";
            }
            else
            {
                strFilter = "*" + (string)_oChirurg["UserID"] + "*.*";
            }
            foreach (FileInfo fi in dir.GetFiles(strFilter))
            {
                ListViewItem lvi = new ListViewItem(fi.Name);
                lvi.Tag = fi.FullName;

                lvDokumente.Items.Add(lvi);
            }
            if (lvDokumente.Items.Count == 0)
            {
                ListViewItem lvi = new ListViewItem(GetText("keineBearbeitung"));
                lvi.Tag = "";

                lvDokumente.Items.Add(lvi);
            }
              
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            _strFullName = GetFirstSelectedTagString(lvDokumente, true, GetText("dokument"));
            if (_strFullName == null)
            {
                _strFullName = "";
            }
            if (_strFullName.Length > 0)
            {
                // Der Dummy-Eintrag hat Laenge 0
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}

