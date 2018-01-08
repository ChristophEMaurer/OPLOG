using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using Utility;
using CMaurer.Operationen.AppFramework;

namespace Operationen
{
    public partial class SecGroupsChirurgenView : OperationenForm
    {
        public SecGroupsChirurgenView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();

            cmdAbort.Enabled = false;
            llSecGroups.Enabled = UserHasRight("SecGroupsView.edit");

            // lblProgress hat im Designer einen border, damit man sieht, wo die Grenzen sind
            lblProgress.Visible = false;
            lblProgress.BorderStyle = BorderStyle.None;
        }

        private void SecGroupsChirurgenView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));

            PopulateSecGroups();
        }

        private void PopulateSecGroups()
        {
            DataView dv = BusinessLayer.GetTypenTemplate(BusinessLayer.TableSecGroups, false);

            DefaultListViewProperties(lvSecGroups);

            lvSecGroups.Clear();
            lvSecGroups.Columns.Add(GetText("usergroup"), -2, HorizontalAlignment.Left);

            foreach (DataRow dataRow in dv.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem((string)dataRow["Text"]);
                lvi.Tag = ConvertToInt32(dataRow["ID"]);
                lvSecGroups.Items.Add(lvi);
            }
            if (lvSecGroups.Items.Count > 0)
            {
                lvSecGroups.SelectedIndices.Add(0);
            }
        }

        private void PopulateChirurgen()
        {
            int ID_SecGroups = GetFirstSelectedTag(lvSecGroups, false);

            if (ID_SecGroups != -1)
            {
                DataView dv = BusinessLayer.GetChirurgenOfSecGroup(ID_SecGroups);
                lvChirurgen.Clear();

                DefaultListViewProperties(lvChirurgen);

                lvChirurgen.Columns.Add(GetText(FormName, "anrede"), 60, HorizontalAlignment.Left);
                lvChirurgen.Columns.Add(GetText(FormName, "nachname"), 120, HorizontalAlignment.Left);
                lvChirurgen.Columns.Add(GetText(FormName, "vorname"), 120, HorizontalAlignment.Left);
                lvChirurgen.Columns.Add(GetText(FormName, "anmeldename"), -2, HorizontalAlignment.Left);

                foreach (DataRow dataRow in dv.Table.Rows)
                {
                    ListViewItem lvi = new ListViewItem((string)dataRow["Anrede"]);
                    lvi.Tag = ConvertToInt32(dataRow["ID_SecGroupsChirurgen"]);
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
            if (lvChirurgen.SelectedItems.Count > 0)
            {
                List<string> abandonedRights = new List<string>();

                lblProgress.Visible = true;
                XableAllButtonsForLongOperation(cmdAbort, false);
                Application.DoEvents();

                BusinessLayer.DatabaseLayer.Progress += new ProgressCallback(DatabaseLayer_Progress);
                foreach (ListViewItem lvi in lvChirurgen.SelectedItems)
                {
                    Application.DoEvents();
                    if (Abort)
                    {
                        break;
                    }
                    int ID_SecGroupsChirurgen = (int)lvi.Tag;

                    if (ID_SecGroupsChirurgen != -1)
                    {
                        if (Abort)
                        {
                            break;
                        }
                        if (BusinessLayer.ExistsAbandonedSecRightWithoutSecGroupsChirurgen(ID_SecGroupsChirurgen, abandonedRights))
                        {
                            // Wenn man abgebrochen hatte, kam auch true zurück, dann aber keine Meldung!
                            if (!Abort)
                            {
                                StringBuilder sb = new StringBuilder(GetText("err_del_user_abandoned_rights"));

                                BusinessLayer.AddListToMessageBox(sb, abandonedRights);

                                MessageBox(sb.ToString());
                            }
                            break;
                        }
                        BusinessLayer.DeleteSecGroupsChirurgen(ID_SecGroupsChirurgen);

                        // Das Überprüfen dauert irre lange, also etwas anzeigen, sicherheitshalber am Ende aber ganz neu laden
                        lvChirurgen.Items.Remove(lvi);
                    }
                }
                BusinessLayer.DatabaseLayer.Progress -= new ProgressCallback(DatabaseLayer_Progress);
                PopulateChirurgen();

                lblProgress.Visible = false;
                Application.DoEvents();
                XableAllButtonsForLongOperation(cmdAbort, true);
            }
            else
            {
                MessageBox(GetTextSelectionNone());
            }
        }

        void DatabaseLayer_Progress(ProgressEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                lblProgress.Text = GetText("ueberpruefe") + " '" + e.Data + "'";
                Application.DoEvents();
            }
            if (Abort)
            {
                e.Cancel = true;
            }
        }

        private void cmdRemove_Click(object sender, EventArgs e)
        {
            RemoveChirurg();
        }

        private void AddChirurg()
        {
            int ID_SecGroups = GetFirstSelectedTag(lvSecGroups, true, GetText(FormName, "chirurg"));

            if (ID_SecGroups != -1)
            {
                DataView dv = BusinessLayer.GetChirurgenNotInSecGroup(ID_SecGroups);

                List<int> ID_ChirurgenList = SelectChirurgMulti(dv, true, GetText("select_surgeon_info"));
                foreach (int ID_Chirurgen in ID_ChirurgenList)
                {
                    BusinessLayer.InsertSecGroupsChirurgen(ID_SecGroups, ID_Chirurgen);
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

        private void llSecGroups_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OperationenLogbuchView.TheMainWindow.StartMenuItem(Command_SecGroupsView);
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            PopulateSecGroups();
        }

        private void lvChirurgen_DoubleClick(object sender, EventArgs e)
        {
            RemoveChirurg();
        }

        private void cmdAbort_Click(object sender, EventArgs e)
        {
            Abort = true;
        }
    }
}