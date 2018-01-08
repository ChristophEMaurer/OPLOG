using System;
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
    public partial class WeiterbilderChirurgenView : OperationenForm
    {
        public WeiterbilderChirurgenView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();

            SetInfoText(lblInfo, string.Format(GetText("info"), Command_ChirurgEdit));
        }

        private void WeiterbilderChirurgenView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));

            PopulateWeiterbilder();

            if (lvWeiterbilder.Items.Count > 0)
            {
                lvWeiterbilder.SelectedIndices.Add(0);
            }
        }

        private void PopulateWeiterbilder()
        {
            DataView dv = BusinessLayer.GetWeiterbilder();

            lvWeiterbilder.Clear();

            DefaultListViewProperties(lvWeiterbilder);

            lvWeiterbilder.Columns.Add(GetText(FormName, "anrede"), 60, HorizontalAlignment.Left);
            lvWeiterbilder.Columns.Add(GetText(FormName, "nachname"), 120, HorizontalAlignment.Left);
            lvWeiterbilder.Columns.Add(GetText(FormName, "vorname"), 120, HorizontalAlignment.Left);
            lvWeiterbilder.Columns.Add(GetText(FormName, "anmeldename"), -2, HorizontalAlignment.Left);

            foreach (DataRow dataRow in dv.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem((string)dataRow["Anrede"]);
                lvi.Tag = ConvertToInt32(dataRow["ID_Chirurgen"]);
                lvi.SubItems.Add((string)dataRow["Nachname"]);
                lvi.SubItems.Add((string)dataRow["Vorname"]);
                lvi.SubItems.Add((string)dataRow["UserID"]);

                lvWeiterbilder.Items.Add(lvi);
            }
        }

        private void PopulateChirurgen()
        {
            int ID_Weiterbilder = GetFirstSelectedTag(lvWeiterbilder, false);

            if (ID_Weiterbilder != -1)
            {
                DataView dv = BusinessLayer.GetChirurgenOfWeiterbilder(ID_Weiterbilder);
                lvChirurgen.Clear();

                DefaultListViewProperties(lvChirurgen);
                lvChirurgen.MultiSelect = true;

                lvChirurgen.Columns.Add(GetText(FormName, "anrede"), 60, HorizontalAlignment.Left);
                lvChirurgen.Columns.Add(GetText(FormName, "nachname"), 120, HorizontalAlignment.Left);
                lvChirurgen.Columns.Add(GetText(FormName, "vorname"), 120, HorizontalAlignment.Left);
                lvChirurgen.Columns.Add(GetText(FormName, "anmeldename"), -2, HorizontalAlignment.Left);

                foreach (DataRow dataRow in dv.Table.Rows)
                {
                    ListViewItem lvi = new ListViewItem((string)dataRow["Anrede"]);
                    lvi.Tag = ConvertToInt32(dataRow["ID_WeiterbilderChirurgen"]);
                    lvi.SubItems.Add((string)dataRow["Nachname"]);
                    lvi.SubItems.Add((string)dataRow["Vorname"]);
                    lvi.SubItems.Add((string)dataRow["UserID"]);

                    lvChirurgen.Items.Add(lvi);
                }
            }
        }

        private void lvAbteilungen_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateChirurgen();
        }

        private void RemoveChirurg()
        {
            foreach (ListViewItem lvi in lvChirurgen.SelectedItems)
            {
                int ID_WeiterbilderChirurgen = (int)lvi.Tag;
                if (ID_WeiterbilderChirurgen != -1)
                {
                    BusinessLayer.DeleteWeiterbilderChirurgen(ID_WeiterbilderChirurgen);
                }
            }
            PopulateChirurgen();
        }

        private void cmdRemove_Click(object sender, EventArgs e)
        {
            RemoveChirurg();
        }

        private void AddChirurg()
        {
            int ID_Weiterbilder = GetFirstSelectedTag(lvWeiterbilder, true, GetText(FormName, "chirurg"));

            if (ID_Weiterbilder != -1)
            {
                DataView dv = BusinessLayer.GetChirurgenNotInWeiterbilder(ID_Weiterbilder);

                List<int> ID_ChirurgenList = SelectChirurgMulti(dv, true, GetText("select_surgeon_info"));

                foreach (int ID_Chirurgen in ID_ChirurgenList)
                {
                    BusinessLayer.InsertWeiterbilderChirurgen(ID_Weiterbilder, ID_Chirurgen);
                }
                PopulateChirurgen();
            }
        }        

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            AddChirurg();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}