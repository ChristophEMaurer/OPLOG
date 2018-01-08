using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Operationen
{
    public partial class ChirurgenView : OperationenForm
    {
        private List<int> _ID_ChirurgenList = new List<int>();
        private DataView _dataView = null;
        private bool _multiSelect = false;

        public ChirurgenView(BusinessLayer businessLayer, DataView dv, bool multiSelect, string info)
            : base(businessLayer)
        {
            _dataView = dv;
            _multiSelect = multiSelect;

            InitializeComponent();

            _bIgnoreControlEvents = true;

            radAktiv.Checked = true;
            radInaktiv.Checked = false;

            radInaktiv.SetSecurity(businessLayer, "ChirurgenView.radInaktiv");
            llExclude.SetSecurity(businessLayer, "ChirurgenView.llExclude");

            if (string.IsNullOrEmpty(info))
            {
                SetInfoText(lblInfo, GetText("info"));
            }
            else
            {
                SetInfoText(lblInfo, info);
            }

            _bIgnoreControlEvents = false;
        }

        public List<int> ID_ChirurgenList
        {
            get { return _ID_ChirurgenList;  }
        }

        private void ChirurgenView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));

            llExclude.Text = GetText("llExclude");
            radInaktiv.Text = GetText("radInaktiv");

            PopulateChirurgen(lvChirurgen, _dataView, _multiSelect, true, radAktiv.Checked);
        }

        private void ChirurgSelected()
        {
            _ID_ChirurgenList.Clear();

            foreach (ListViewItem lvi in lvChirurgen.SelectedItems)
            {
                int ID_Chirurgen = (int)lvi.Tag;

                if (ID_Chirurgen != -1)
                {
                    _ID_ChirurgenList.Add(ID_Chirurgen);
                }
            }
            if (_ID_ChirurgenList.Count > 0)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            ChirurgSelected();
        }

        private void lvChirurgen_DoubleClick(object sender, EventArgs e)
        {
            ChirurgSelected();
        }

        private void radAktiv_CheckedChanged(object sender, EventArgs e)
        {
            if (!_bIgnoreControlEvents)
            {
                if (radAktiv.Checked)
                {
                    PopulateChirurgen(lvChirurgen, null, _multiSelect, true, true);
                }
            }
        }

        private void radInaktiv_CheckedChanged(object sender, EventArgs e)
        {
            if (!_bIgnoreControlEvents)
            {
                if (radInaktiv.Checked)
                {
                    PopulateChirurgen(lvChirurgen, null, _multiSelect, true, false);
                }
            }
        }

        private void llExclude_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Confirm(GetText("confirmExclude")))
            {
                Cursor = Cursors.WaitCursor;

                foreach (ListViewItem lvi in lvChirurgen.SelectedItems)
                {
                    string nachname = lvi.SubItems[1].Text;
                    string vorname = lvi.SubItems[2].Text;

                    DataRow row = BusinessLayer.CreateDataRowImportChirurgenExclude();
                    row["Nachname"] = nachname;
                    row["Vorname"] = vorname;
                    BusinessLayer.InsertImportChirurgenExclude(row);
                }

                Cursor = Cursors.Default;

                ImportChirurgenExcludeView dlg = new ImportChirurgenExcludeView(BusinessLayer);
                dlg.ShowDialog();
            }
        }
    }
}