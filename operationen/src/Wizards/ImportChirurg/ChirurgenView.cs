using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Windows.Forms;

namespace Operationen.Wizards.ImportChirurg
{
    public partial class ChirurgenView : OperationenForm
    {
        private int _ID_Chirurgen = -1;
        private DataView _dataview;

        public ChirurgenView(BusinessLayer businessLayer, DataView dataview, string text)
            : base(businessLayer)
        {
            _dataview = dataview;

            InitializeComponent();
            SetInfoText(lblInfo, text);
        }

        protected override string GetFormNameForResourceTexts()
        {
            return "Wizards_ImportChirurg_ChirurgenView";
        }
        public int ID_Chirurgen
        {
            get { return _ID_Chirurgen;  }
        }

        protected void PopulateChirurgen(DataView dataview)
        {
            OplListView lv = lvChirurgen;

            lv.Clear();

            DefaultListViewProperties(lv);

            lv.Columns.Add(GetText("anrede"), 60, HorizontalAlignment.Left);
            lv.Columns.Add(GetText("nachname"), 120, HorizontalAlignment.Left);
            lv.Columns.Add(GetText("vorname"), 120, HorizontalAlignment.Left);
            lv.Columns.Add(GetText("anmeldename"), -2, HorizontalAlignment.Left);

            foreach (DataRow dataRow in dataview.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem((string)dataRow["Anrede"]);
                lvi.Tag = ConvertToInt32(dataRow["ID_Chirurgen"]);
                lvi.SubItems.Add((string)dataRow["Nachname"]);
                lvi.SubItems.Add((string)dataRow["Vorname"]);
                lvi.SubItems.Add((string)dataRow["UserID"]);

                lv.Items.Add(lvi);
            }
        }

        private void ChirurgenView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));

            PopulateChirurgen(_dataview);
        }

        private void ChirurgSelected()
        {
            _ID_Chirurgen = GetFirstSelectedTag(lvChirurgen, true);
            if (_ID_Chirurgen != -1)
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
    }
}