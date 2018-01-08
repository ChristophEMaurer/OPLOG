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
    public partial class AbteilungenChirurgenView : OperationenForm
    {
        public AbteilungenChirurgenView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();

            llAbteilungen.SetSecurity(businessLayer, "AbteilungenView.view");
        }

        private void AbteilungenChirurgenView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));

            PopulateAbteilungen();
        }

        private void PopulateAbteilungen()
        {
            DataView dv = BusinessLayer.GetTypenTemplate(BusinessLayer.TableAbteilungen, false);

            DefaultListViewProperties(lvAbteilungen);

            lvAbteilungen.Clear();
            lvAbteilungen.Columns.Add(GetText("abteilung"), -2, HorizontalAlignment.Left);

            foreach (DataRow dataRow in dv.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem((string)dataRow["Text"]);
                lvi.Tag = ConvertToInt32(dataRow["ID"]);
                lvAbteilungen.Items.Add(lvi);
            }
            if (lvAbteilungen.Items.Count > 0)
            {
                lvAbteilungen.SelectedIndices.Add(0);
            }
        }

        private void PopulateChirurgen()
        {
            int ID_Abteilungen = GetFirstSelectedTag(lvAbteilungen, false);

            if (ID_Abteilungen != -1)
            {
                DataView dv = BusinessLayer.GetChirurgenVonAbteilung(ID_Abteilungen);
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
                    lvi.Tag = ConvertToInt32(dataRow["ID_AbteilungenChirurgen"]);
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
                int ID_AbteilungenChirurgen = (int)lvi.Tag;
                if (ID_AbteilungenChirurgen != -1)
                {
                    BusinessLayer.DeleteAbteilungenChirurgen(ID_AbteilungenChirurgen);
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
            int ID_Abteilungen = GetFirstSelectedTag(lvAbteilungen, true, GetText(FormName, "chirurg"));

            if (ID_Abteilungen != -1)
            {
                DataView dv = BusinessLayer.GetChirurgenNichtInAbteilung(ID_Abteilungen);

                List<int> ID_ChirurgenList = SelectChirurgMulti(dv, true, GetText("select_surgeon_info"));

                foreach (int ID_Chirurgen in ID_ChirurgenList)
                {
                    BusinessLayer.InsertAbteilungenChirurgen(ID_Abteilungen, ID_Chirurgen);
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

        private void llAbteilungen_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OperationenLogbuchView.TheMainWindow.StartMenuItem(Command_AbteilungenView);
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            PopulateAbteilungen();
        }

        private void lvChirurgen_DoubleClick(object sender, EventArgs e)
        {
            RemoveChirurg();
        }
    }
}