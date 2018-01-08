using System;
using System.Collections;
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
    public partial class SecUserOverviewView : OperationenForm
    {
        public SecUserOverviewView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();
        }

        private void SecUserOverviewView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));

            InitAbteilungen();
            InitRollen();
            InitSecRights();
            InitWeiterbilder();
            InitWeiterzubildende();
            InitDaten();

            PopulateChirurgen();
        }

        private void PopulateChirurgen()
        {
            lvChirurgen.Clear();
            DefaultListViewProperties(lvChirurgen);

            lvChirurgen.Columns.Add(GetText(FormName, "anrede"), 60, HorizontalAlignment.Left);
            lvChirurgen.Columns.Add(GetText(FormName, "nachname"), 120, HorizontalAlignment.Left);
            lvChirurgen.Columns.Add(GetText(FormName, "vorname"), 120, HorizontalAlignment.Left);
            lvChirurgen.Columns.Add(GetText(FormName, "anmeldename"), -2, HorizontalAlignment.Left);

            DataView dv = BusinessLayer.GetChirurgenAlle();

            foreach (DataRow dataRow in dv.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem((string)dataRow["Anrede"]);
                lvi.Tag = ConvertToInt32(dataRow["ID_Chirurgen"]);
                lvi.SubItems.Add((string)dataRow["Nachname"]);
                lvi.SubItems.Add((string)dataRow["Vorname"]);
                lvi.SubItems.Add((string)dataRow["UserID"]);

                lvChirurgen.Items.Add(lvi);
            }
        }

        private void InitDaten()
        {
            lvDaten.Clear();
            DefaultListViewProperties(lvDaten);

            lvDaten.Columns.Add(GetText(FormName, "anrede"), 60, HorizontalAlignment.Left);
            lvDaten.Columns.Add(GetText(FormName, "nachname"), 120, HorizontalAlignment.Left);
            lvDaten.Columns.Add(GetText(FormName, "vorname"), 120, HorizontalAlignment.Left);
            lvDaten.Columns.Add(GetText(FormName, "anmeldename"), -2, HorizontalAlignment.Left);
        }

        private void PopulateDaten(int ID_Chirurgen)
        {
            DataView dv = BusinessLayer.GetChirurgen(ID_Chirurgen);

            lvDaten.Items.Clear();
            foreach (DataRow dataRow in dv.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem((string)dataRow["Anrede"]);
                lvi.SubItems.Add((string)dataRow["Nachname"]);
                lvi.SubItems.Add((string)dataRow["Vorname"]);
                lvi.SubItems.Add((string)dataRow["UserID"]);

                lvDaten.Items.Add(lvi);
            }
        }

        private void InitWeiterbilder()
        {
            lvWeiterbilder.Clear();
            DefaultListViewProperties(lvWeiterbilder);

            lvWeiterbilder.Columns.Add(GetText(FormName, "anrede"), 60, HorizontalAlignment.Left);
            lvWeiterbilder.Columns.Add(GetText(FormName, "nachname"), 120, HorizontalAlignment.Left);
            lvWeiterbilder.Columns.Add(GetText(FormName, "vorname"), 120, HorizontalAlignment.Left);
            lvWeiterbilder.Columns.Add(GetText(FormName, "anmeldename"), -2, HorizontalAlignment.Left);
        }

        private void PopulateWeiterbilder(int ID_Chirurgen)
        {
            DataView dv = BusinessLayer.GetWeiterbilderOfUser(ID_Chirurgen);

            lvWeiterbilder.Items.Clear();
            foreach (DataRow dataRow in dv.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem((string)dataRow["Anrede"]);
                lvi.SubItems.Add((string)dataRow["Nachname"]);
                lvi.SubItems.Add((string)dataRow["Vorname"]);
                lvi.SubItems.Add((string)dataRow["UserID"]);

                lvWeiterbilder.Items.Add(lvi);
            }
        }

        private void InitWeiterzubildende()
        {
            lvWeiterzubildende.Clear();
            DefaultListViewProperties(lvWeiterzubildende);

            lvWeiterzubildende.Columns.Add(GetText(FormName, "anrede"), 60, HorizontalAlignment.Left);
            lvWeiterzubildende.Columns.Add(GetText(FormName, "nachname"), 120, HorizontalAlignment.Left);
            lvWeiterzubildende.Columns.Add(GetText(FormName, "vorname"), 100, HorizontalAlignment.Left);
            lvWeiterzubildende.Columns.Add(GetText(FormName, "anmeldename"), -2, HorizontalAlignment.Left);
        }
        private void PopulateWeiterzubildende(int ID_Chirurgen)
        {
            DataView dv = BusinessLayer.GetWeiterzubildendeOfUser(ID_Chirurgen);

            lvWeiterzubildende.Items.Clear();
            foreach (DataRow dataRow in dv.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem((string)dataRow["Anrede"]);
                lvi.SubItems.Add((string)dataRow["Nachname"]);
                lvi.SubItems.Add((string)dataRow["Vorname"]);
                lvi.SubItems.Add((string)dataRow["UserID"]);

                lvWeiterzubildende.Items.Add(lvi);
            }
        }

        private void InitAbteilungen()
        {
            DefaultListViewProperties(lvAbteilungen);

            lvAbteilungen.Clear();
            lvAbteilungen.Columns.Add(GetText("abteilung"), -2, HorizontalAlignment.Left);
        }
        
        private void PopulateAbteilungen(int ID_Chirurgen)
        {
            DataView dv = BusinessLayer.GetAbteilungenOfUser(ID_Chirurgen);

            lvAbteilungen.Items.Clear();
            foreach (DataRow dataRow in dv.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem((string)dataRow["Text"]);
                lvAbteilungen.Items.Add(lvi);
            }
        }

        private void InitRollen()
        {
            DefaultListViewProperties(lvSecGroups);

            lvSecGroups.Clear();
            lvSecGroups.Columns.Add(GetText("usergroup"), -2, HorizontalAlignment.Left);
        }
        
        private void PopulateRollen(int ID_Chirurgen)
        {
            DataView dv = BusinessLayer.GetRollenOfUser(ID_Chirurgen);

            lvSecGroups.Items.Clear();
            foreach (DataRow dataRow in dv.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem((string)dataRow["Text"]);
                lvSecGroups.Items.Add(lvi);
            }
        }
        private void InitSecRights()
        {
            lvSecRights.Clear();
            DefaultListViewProperties(lvSecRights);

            lvSecRights.Columns.Add(GetText("right_description"), 1000, HorizontalAlignment.Left);
        }
        
        private void PopulateSecRights(int ID_Chirurgen)
        {
            DataView rights = BusinessLayer.GetRightsOfUser(ID_Chirurgen);

            lvSecRights.Items.Clear();
            foreach (DataRow row in rights.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem((string)row["Description"]);

                lvSecRights.Items.Add(lvi);
            }
        }

        private void UserChanged()
        {
            int ID_Chirurgen = GetFirstSelectedTag(lvChirurgen);

            if (ID_Chirurgen != -1)
            {
                DataRow row = BusinessLayer.GetChirurg(ID_Chirurgen);

                chkWeiterbilder.Checked = 1 == ConvertToInt32(row["IstWeiterbilder"]);

                PopulateAbteilungen(ID_Chirurgen);
                PopulateRollen(ID_Chirurgen);
                PopulateSecRights(ID_Chirurgen);
                PopulateWeiterbilder(ID_Chirurgen);
                PopulateWeiterzubildende(ID_Chirurgen);
                PopulateDaten(ID_Chirurgen);
            }
        }

        private void lvChirurgen_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserChanged();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

