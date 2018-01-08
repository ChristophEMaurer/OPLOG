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
    public partial class SecGroupsSecRightsView : OperationenForm
    {
         internal class tagLVSecGroupsSecRights
        {
            internal int _ID_SecGroupsSecRights;
            internal int _ID_SecRights;

            internal tagLVSecGroupsSecRights(int ID_SecGroupsSecRights, int ID_SecRights)
            {
                _ID_SecGroupsSecRights = ID_SecGroupsSecRights;
                _ID_SecRights = ID_SecRights;
            }
        }

         public SecGroupsSecRightsView(BusinessLayer businessLayer)
             : base(businessLayer)
        {
            InitializeComponent();

            llSecGroups.Enabled = UserHasRight("SecGroupsView.edit");
        }

        private void SecGroupsSecRightsView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));
            
            SetInfoText(lblInfo, GetText("info"));

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

        private void PopulateSecGroupsSecRights()
        {
            int ID_SecGroups = GetFirstSelectedTag(lvSecGroups, false);

            if (ID_SecGroups != -1)
            {
                DataView dv = BusinessLayer.GetSecRightsOfSecGroup(ID_SecGroups);
                lvSecGroupsSecRights.Clear();

                DefaultListViewProperties(lvSecGroupsSecRights);
                lvSecGroupsSecRights.MultiSelect = true;

                lvSecGroupsSecRights.Columns.Add(GetText("description"), 1000, HorizontalAlignment.Left);

                foreach (DataRow dataRow in dv.Table.Rows)
                {
#if DEBUG
                    //
                    // Den Datenbanknamen des Rechtes anzeigen, sonst findet man es nie
                    // 
                    ListViewItem lvi = new ListViewItem((string)dataRow["Description"] + " (" + (string)dataRow["Name"] + ")");
#else
                    ListViewItem lvi = new ListViewItem((string)dataRow["Description"]);
#endif
                    lvi.Tag = new tagLVSecGroupsSecRights(ConvertToInt32(dataRow["ID_SecGroupsSecRights"]), ConvertToInt32(dataRow["ID_SecRights"]));

                    lvSecGroupsSecRights.Items.Add(lvi);
                }
            }
        }

        private void PopulateSecRights()
        {
            int ID_SecGroups = GetFirstSelectedTag(lvSecGroups, false);

            if (ID_SecGroups != -1)
            {
                DataView dv = BusinessLayer.GetSecRightsNotInSecGroup(ID_SecGroups);
                lvSecRights.Clear();

                DefaultListViewProperties(lvSecRights);
                lvSecRights.MultiSelect = true;

                lvSecRights.Columns.Add(GetText("description"), 1000, HorizontalAlignment.Left);

                foreach (DataRow dataRow in dv.Table.Rows)
                {
#if DEBUG
                    //
                    // Den Datenbanknamen des Rechtes anzeigen, sonst findet man es nie
                    // 
                    ListViewItem lvi = new ListViewItem((string)dataRow["Description"] + " (" + (string)dataRow["Name"] + ")");
#else
                    ListViewItem lvi = new ListViewItem((string)dataRow["Description"]);
#endif
                    lvi.Tag = ConvertToInt32(dataRow["ID_SecRights"]);

                    lvSecRights.Items.Add(lvi);
                }
            }
        }

        private void lvSecGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateSecGroupsSecRights();
            PopulateSecRights();
        }

        private void EnableControls(bool enable)
        {
            cmdAdd.Enabled = enable;
            cmdCancel.Enabled = enable;
            cmdRemove.Enabled = enable;
        }

        private void RemoveSecRight()
        {
            bool changed = false;
            int lastIndex = 0;
            int lastOtherIndex = GetFirstSelectedIndex(lvSecRights);

            EnableControls(false);
            Cursor = Cursors.WaitCursor;

            foreach (ListViewItem lvi in lvSecGroupsSecRights.SelectedItems)
            {
                tagLVSecGroupsSecRights tag = (tagLVSecGroupsSecRights)lvi.Tag;
                int ID_SecGroupsSecRights = tag._ID_SecGroupsSecRights;
                int ID_SecRights = tag._ID_SecRights;

                if (ID_SecRights != -1)
                {
                    long users = BusinessLayer.CountSecRightAssignmentsWithout(ID_SecGroupsSecRights, ID_SecRights);
                    if (users < 1)
                    {
                        DataRow right = BusinessLayer.GetSecRight(ID_SecRights);

                        string description = OperationenLogbuchView.TheMainWindow.GetSecurityRightDescription((string)right["Name"]);
                        string msg = GetText("err_last_right") + "\r\r- '" + description + "'";
                        MessageBox(msg);
                        break;
                    }
                    BusinessLayer.DeleteSecGroupsSecRights(ID_SecGroupsSecRights);
                    changed = true;
                    lastIndex = lvi.Index;
                }
            }
            if (changed)
            {
                PopulateSecGroupsSecRights();
                PopulateSecRights();
                EnsureVisibleNearIndex(lvSecGroupsSecRights, lastIndex);
                if (lastOtherIndex != -1)
                {
                    EnsureVisibleNearIndex(lvSecRights, lastOtherIndex);
                }
            }

            Cursor = Cursors.Default;
            EnableControls(true);
        }

        private void cmdRemove_Click(object sender, EventArgs e)
        {
            RemoveSecRight();
        }

        private void AddSecRight()
        {
            bool changed = false;
            int lastIndex = 0;
            int lastOtherIndex = GetFirstSelectedIndex(lvSecGroupsSecRights);

            EnableControls(false);
            Cursor = Cursors.WaitCursor;

            int ID_SecGroups = GetFirstSelectedTag(lvSecGroups, true, GetText("usergroups"));

            foreach (ListViewItem lvi in lvSecRights.SelectedItems)
            {
                int ID_SecRights = (int) lvi.Tag;

                if (ID_SecGroups != -1 && ID_SecRights != -1)
                {
                    BusinessLayer.InsertSecGroupsSecRights(ID_SecGroups, ID_SecRights);
                    changed = true;
                    lastIndex = lvi.Index;
                }
            }

            if (changed)
            {
                PopulateSecGroupsSecRights();
                PopulateSecRights();
                EnsureVisibleNearIndex(lvSecRights, lastIndex);
                if (lastOtherIndex != -1)
                {
                    EnsureVisibleNearIndex(lvSecGroupsSecRights, lastOtherIndex);
                }
            }

            Cursor = Cursors.Default;
            EnableControls(true);
        }        
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            AddSecRight();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void llSecGroups_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OperationenLogbuchView.TheMainWindow.StartMenuItem(Command_SecGroupsView);
        }

        private void lvSecRights_DoubleClick(object sender, EventArgs e)
        {
            AddSecRight();
        }

        private void lvSecGroupsSecRights_DoubleClick(object sender, EventArgs e)
        {
            RemoveSecRight();
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            PopulateSecGroups();
        }
    }
}