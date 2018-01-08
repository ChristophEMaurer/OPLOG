using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Utility;

namespace Operationen
{
    public partial class RichtlinieSelectView : OperationenForm
    {
        private int _ID_Gebiete;
        private int _ID_Richtlinien = -1;

        public RichtlinieSelectView(BusinessLayer businessLayer)
            : this(businessLayer, -1)
        {
        }

        public RichtlinieSelectView(BusinessLayer businessLayer, int ID_Gebiete)
            : base(businessLayer)
        {
            _ID_Gebiete = ID_Gebiete;

            InitializeComponent();
        }


        public int ID_Richtlinien
        {
            get { return _ID_Richtlinien; }
        }

        private void RichtlinieSelectView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));

            InitRichtlinien();
            PopulateGebiete();

            GebietChanged();
        }

        private void PopulateGebiete()
        {
            DataView dv = BusinessLayer.GetGebiete();

            cbGebiete.ValueMember = "ID_Gebiete";
            cbGebiete.DisplayMember = "Gebiet";
            cbGebiete.DataSource = dv;

            if (_ID_Gebiete != -1)
            {
                cbGebiete.SelectedValue = _ID_Gebiete;
            }
        }

        private void InitRichtlinien()
        {
            DefaultListViewProperties(lvRichtlinien);

            lvRichtlinien.Clear();
            lvRichtlinien.Columns.Add(GetText("nr"), 50, HorizontalAlignment.Left);
            lvRichtlinien.Columns.Add(GetText("richtzahl"), 80, HorizontalAlignment.Left);
            lvRichtlinien.Columns.Add(GetText("methode"), -2, HorizontalAlignment.Left);
        }

        protected override bool ValidateInput()
        {
            bool bSuccess = true;
            string strMessage = EINGABEFEHLER;

            if (-1 == GetFirstSelectedTag(lvRichtlinien, false))
            {
                bSuccess = false;
                strMessage += GetTextControlNeedsSelection(grpRichtlinien);
            }

            if (!bSuccess)
            {
                MessageBox(strMessage);
            }

            return bSuccess;
        }

        private void PopulateRichtlinien(int nID_Gebiete)
        {
            DataView dv = BusinessLayer.GetRichtlinien(nID_Gebiete);

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
            SetGroupBoxText(lvRichtlinien, grpRichtlinien, GetText("richtlinien"));
        }

        protected override void OKClicked()
        {
            if (ValidateInput())
            {
                _ID_Richtlinien = GetFirstSelectedTag(lvRichtlinien, true);
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            OKClicked();
        }

        private void GebietChanged()
        {
            int nID_Gebiete = ConvertToInt32(cbGebiete.SelectedValue);

            Cursor = Cursors.WaitCursor;

            PopulateRichtlinien(nID_Gebiete);

            Cursor = Cursors.Default;
        }

        private void cbGebiete_SelectedIndexChanged(object sender, EventArgs e)
        {
            GebietChanged();
        }

        private void lvRichtlinien_DoubleClick(object sender, EventArgs e)
        {
            OKClicked();
        }
    }
}