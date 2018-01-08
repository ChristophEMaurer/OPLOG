using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

using Utility;
using Windows.Forms;

namespace Operationen
{
    public partial class ExportOperationenKatalogView : OperationenForm
    {
        /// <summary>
        /// Contains the data: key = group, List/Dataitem are all entries for this group
        /// </summary>
        SortedDictionary<string, List<ExportOperationenDataItem>> _data = new SortedDictionary<string, List<ExportOperationenDataItem>>();

        public ExportOperationenKatalogView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();

            lblSelection.AutoSize = true;
            ListView_ItemSelectionChanged(null, null);

            DefaultListViewProperties(lvExport);
            lvExport.Clear();
            lvExport.Sortable = false;
            lvExport.Columns.Add(GetText("opscode"), 80, HorizontalAlignment.Left);
            lvExport.Columns.Add(GetText("opstext"), -2, HorizontalAlignment.Left);

            DefaultListViewProperties(lvGroups);
            lvGroups.Clear();
            lvGroups.Columns.Add(GetText("group"), 400, HorizontalAlignment.Left);
            lvGroups.Columns.Add("", -2, HorizontalAlignment.Left);
            lvGroups.Sortable = false;
        }

        protected override void ProgressBegin()
        {
            XableAllButtonsForLongOperation(null, false);
        }

        protected override void ProgressEnd()
        {
            XableAllButtonsForLongOperation(null, true);
        }

        private void PopulateOperationen()
        {
            base.PopulateOperationen(grpOperationen, lvOperationen, txtFilterOPSCode.Text, txtFilterOPSText.Text);
        }

        private void OperationenKatalogView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));

            SetInfoText(lblInfo, string.Format(CultureInfo.InvariantCulture, GetText("info"), cmdImport.Text, cmdAllUsed.Text));
        }

        private void InsertSelectedItems()
        {
            if (lvGroups.SelectedItems.Count > 0)
            {
                ListViewItem lviGroup = GetSelectedGroup();

                if (lviGroup != null)
                {
                    string group = lviGroup.SubItems[0].Text;
                    List<ExportOperationenDataItem> list;
                    if (_data.TryGetValue(group, out list))
                    {
                        foreach (ListViewItem lvi in lvOperationen.SelectedItems)
                        {
                            ExportOperationenDataItem dataItem = new ExportOperationenDataItem(lvi.SubItems[0].Text, lvi.SubItems[1].Text);
                            ListViewItem lviCopy = new ListViewItem(dataItem.opsCode);
                            lviCopy.SubItems.Add(dataItem.opsText);
                            lvExport.Items.Add(lviCopy);
                            list.Add(dataItem);
                        }
                        list.Sort();
                        base.SetGroupBoxText(lvExport, grpExportDaten, GetText("grpExportDaten"));
                    }
                }
            }
        }

        private ListViewItem GetSelectedGroup()
        {
            return GetFirstSelectedLVI(lvGroups, true, GetText("group"));
        }

        private void DeleteSelectedItems()
        {
            if (lvGroups.SelectedItems.Count > 0)
            {
                ListViewItem lviGroup = GetSelectedGroup();
                if (lviGroup != null)
                {
                    string group = lviGroup.SubItems[0].Text;
                    List<ExportOperationenDataItem> list;
                    if (_data.TryGetValue(group, out list))
                    {
                        bool beginUpdate = false;

                        XableAllButtonsForLongOperation(null, false);

                        Application.DoEvents();

                        if (lvExport.SelectedItems.Count > 100)
                        {
                            lvExport.BeginUpdate();
                            beginUpdate = true;
                        }

                        Application.DoEvents();

                        foreach (ListViewItem lvi in lvExport.SelectedItems)
                        {
                            ExportOperationenDataItem dataItem = (ExportOperationenDataItem)lvi.Tag;
                            lvExport.Items.Remove(lvi);
                            list.Remove(dataItem);
                        }

                        if (beginUpdate)
                        {
                            lvExport.EndUpdate();
                        }

                        base.SetGroupBoxText(lvExport, grpExportDaten, GetText("grpExportDaten"));

                        XableAllButtonsForLongOperation(null, true);
                    }
                }
            }
        }
        private void DeleteAllItems()
        {
            if (lvGroups.SelectedItems.Count > 0)
            {
                ListViewItem lviGroup = GetSelectedGroup();
                if (lviGroup != null)
                {
                    string group = lviGroup.SubItems[0].Text;
                    List<ExportOperationenDataItem> list;
                    if (_data.TryGetValue(group, out list))
                    {
                        bool beginUpdate = false;

                        XableAllButtonsForLongOperation(null, false);

                        Application.DoEvents();

                        if (lvExport.SelectedItems.Count > 100)
                        {
                            lvExport.BeginUpdate();
                            beginUpdate = true;
                        }

                        Application.DoEvents();

                        foreach (ListViewItem lvi in lvExport.Items)
                        {
                            ExportOperationenDataItem dataItem = (ExportOperationenDataItem)lvi.Tag;
                            list.Remove(dataItem);
                        }
                        lvExport.Items.Clear();

                        if (beginUpdate)
                        {
                            lvExport.EndUpdate();
                        }

                        base.SetGroupBoxText(lvExport, grpExportDaten, GetText("grpExportDaten"));

                        XableAllButtonsForLongOperation(null, true);
                    }
                }
            }
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            InsertSelectedItems();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (Confirm(GetTextConfirmDeleteSimple(lvExport.SelectedItems.Count)))
            {
                DeleteSelectedItems();
            }
        }

        private void cmdPopulate_Click(object sender, EventArgs e)
        {
            PopulateOperationen();
        }

        private void ExportOperationenKatalogView_Shown(object sender, EventArgs e)
        {
            txtFilterOPSCode.Focus();
        }

        private void ImportData(string fileName)
        {
            BinaryReader reader = null;

            try
            {
                XableAllButtonsForLongOperation(null, false);

                reader = new BinaryReader(File.Open(fileName, FileMode.Open));

                //
                // File signature
                //
                string line = Tools.ReadJavaModifiedUtf8(reader);
                string[] arLine = line.Split('|');

                //
                // "_P_|1"
                // "_P_|2"
                //
                string fileSignatures = GlobalConstantsMobile.FileIdOPSKatalogMobile + "|1, " + GlobalConstantsMobile.FileIdOPSKatalogMobile + "|2";
                if (arLine.Length != 2)
                {
                    MessageBox(string.Format(CultureInfo.InvariantCulture, GetText("err_read_signature"), fileName, fileSignatures));
                    goto _exit;
                }

                //
                // "_P_"
                //
                if (arLine[0] != GlobalConstantsMobile.FileIdOPSKatalogMobile)
                {
                    MessageBox(string.Format(CultureInfo.InvariantCulture, GetText("err_read_signature"), fileName, fileSignatures));
                    goto _exit;
                }

                //
                // 1 or 2
                //
                int version = Convert.ToInt32(arLine[1]);
                if ((version < 1) || (version > GlobalConstantsMobile.FileVersionOPSKatalogMobile))
                {
                    MessageBox(string.Format(CultureInfo.InvariantCulture, GetText("err_read_signature"), fileName, fileSignatures));
                    goto _exit;
                }

                //
                // Clear only if version was ok
                //
                _data.Clear();
                lvExport.Items.Clear();
                lvGroups.Items.Clear();

                if (version == 1)
                {
                    ImportDataVersion1(reader);
                }
                else if (version == GlobalConstantsMobile.FileVersionOPSKatalogMobile)
                {
                    ImportDataVersion2(reader);
                }

                PopulateGroups();
            }
            catch (Exception exc)
            {
                MessageBox(string.Format(GetText("err_read_file"), fileName, exc.ToString()));
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                XableAllButtonsForLongOperation(null, true);
            }

        _exit: ;
        }

        /// <summary>
        /// Version 1: no groups only entries
        /// </summary>
        /// <param name="reader">the reader from which to import</param>
        private void ImportDataVersion1(BinaryReader reader)
        {
            // "_P_|1"
            List<ExportOperationenDataItem> list = new List<ExportOperationenDataItem>();

            //
            // Skip number of lines
            //
            string line = Tools.ReadJavaModifiedUtf8(reader);

            //
            // Entries
            //
            while ((line = Tools.ReadJavaModifiedUtf8(reader)) != null)
            {
                string[] arLine = line.Split('|');
                if (arLine.Length == 2)
                {
                    ExportOperationenDataItem item = new ExportOperationenDataItem(arLine[0], arLine[1]);
                    list.Add(item);
                }
            }

            string group = GetText("defaultGroupName");
            list.Sort();
            _data.Add(group, list);
        }

        /// <summary>
        /// Version 2: groups and entries
        /// </summary>
        /// <param name="reader"></param>
        private void ImportDataVersion2(BinaryReader reader)
        {
            // "_P_|2"
            //
            // Number of groups
            //
            string line = Tools.ReadJavaModifiedUtf8(reader);
            int numGroups = Convert.ToInt32(line);

            for (int i = 0; i < numGroups; i++)
            {
                //
                // Group name
                //
                string group = Tools.ReadJavaModifiedUtf8(reader);

                List<ExportOperationenDataItem> list = new List<ExportOperationenDataItem>();

                //
                // Number of entries
                //
                line = Tools.ReadJavaModifiedUtf8(reader);
                int numEntries = Convert.ToInt32(line);
                for (int j = 0; j < numEntries; j++)
                {
                    line = Tools.ReadJavaModifiedUtf8(reader);
                    string[] arLine = line.Split('|');
                    if (arLine.Length == 2)
                    {
                        ExportOperationenDataItem item = new ExportOperationenDataItem(arLine[0], arLine[1]);
                        list.Add(item);
                    }
                }
                list.Sort();
                _data.Add(group, list);
            }
        }

        private void WriteJavaModifiedUtf8(BinaryWriter writer, string line)
        {
            Tools.WriteJavaModifiedUtf8(writer, line);
        }

        private string RemoveSeparator(string text)
        {
            string ret = text.Replace('|', ' ');
            return ret;
        }

        private void ExportDataVersion1(string fileName)
        {
            BinaryWriter writer = null;

            try
            {
                XableAllButtonsForLongOperation(null, false);

                writer = new BinaryWriter(File.Open(fileName, FileMode.Create));

                //
                // File version
                //
                string line = GlobalConstantsMobile.FileIdOPSKatalogMobile + "|1";
                WriteJavaModifiedUtf8(writer, line);

                //
                // Count all entries
                //
                int total = 0;

                foreach (string key in _data.Keys)
                {
                    List<ExportOperationenDataItem> list;
                    if (_data.TryGetValue(key, out list))
                    {
                        total += list.Count;
                    }
                }

                //
                // Number of entries
                //
                line = total.ToString();
                WriteJavaModifiedUtf8(writer, line);

                //
                // All entries
                //
                foreach (string group in _data.Keys)
                {
                    List<ExportOperationenDataItem> list;

                    if (_data.TryGetValue(group, out list))
                    {
                        //
                        // Items
                        //
                        foreach (ExportOperationenDataItem item in list)
                        {
                            line = RemoveSeparator(item.opsCode) + "|" + RemoveSeparator(item.opsText);
                            WriteJavaModifiedUtf8(writer, line);
                        }
                    }
                }
            }
            catch
            {
                MessageBox(string.Format(GetText("err_write_file"), fileName));
                goto _exit;
            }
            finally
            {
                if (writer != null)
                {
                    writer.Flush();
                    writer.Close();
                }

                XableAllButtonsForLongOperation(null, true);
            }

        _exit: ;
        }

        private void ExportDataVersion2(string fileName)
        {
            BinaryWriter writer = null;

            try
            {
                XableAllButtonsForLongOperation(null, false);

                writer = new BinaryWriter(File.Open(fileName, FileMode.Create));

                //
                // File version
                //
                string line = GlobalConstantsMobile.FileIdOPSKatalogMobile + "|2";
                WriteJavaModifiedUtf8(writer, line);

                //
                // Number of groups
                //
                int count = _data.Count;
                line = _data.Count.ToString();
                WriteJavaModifiedUtf8(writer, line);

                //
                // Groups
                //
                foreach (string group in _data.Keys)
                {
                    //
                    // Group name
                    //
                    WriteJavaModifiedUtf8(writer, group);

                    List<ExportOperationenDataItem> list;

                    if (_data.TryGetValue(group, out list))
                    {
                        //
                        // Number of items in group
                        // n items, n >= 0
                        //
                        line = list.Count.ToString();
                        WriteJavaModifiedUtf8(writer, line);

                        //
                        // Items
                        //
                        foreach (ExportOperationenDataItem item in list)
                        {
                            line = RemoveSeparator(item.opsCode) + "|" + RemoveSeparator(item.opsText);
                            WriteJavaModifiedUtf8(writer, line);
                        }
                    }
                    else
                    {
                        //
                        // Number of items in group
                        // 0 items
                        //
                        WriteJavaModifiedUtf8(writer, "0");
                    }
                }
            }
            catch
            {
                MessageBox(string.Format(GetText("err_write_file"), fileName));
                goto _exit;
            }
            finally
            {
                if (writer != null)
                {
                    writer.Flush();
                    writer.Close();
                }

                XableAllButtonsForLongOperation(null, true);
            }

        _exit:;
        }

#if DEBUG
        private void CheckEntries()
        {
            const string FileName = "d:\\temp\\oplog.txt";
            bool foundSomething = false;

            // opscode, group
            Dictionary<string, string> unique = new Dictionary<string, string>();

            StreamWriter writer = new StreamWriter(File.Open(FileName, FileMode.Create));

            writer.WriteLine("Duplicate entries:");

            foreach (string group in _data.Keys)
            {
                List<ExportOperationenDataItem> list;

                _data.TryGetValue(group, out list);
                foreach (ExportOperationenDataItem item in list)
                {
                    string opscode = item.opsCode;
                    try
                    {
                        unique.Add(opscode, group);
                    }
                    catch
                    {
                        foundSomething = true;
                        string existingGroup;
                        unique.TryGetValue(opscode, out existingGroup);
                        writer.WriteLine(existingGroup + " <- " + opscode + ", " + group);
                    }
                }
            }

            //
            // find all missing entries: entries which are in not in the current OPS-catalog but which exist for any user.
            // (manually entered or from a previous ops-catalog)
            // unique now contains all entries from all groups
            //
            writer.WriteLine("Missing entries:");
            foreach (ListViewItem lvi in lvOperationen.Items)
            {
                if (!unique.ContainsKey(lvi.Text))
                {
                    foundSomething = true;
                    writer.WriteLine(lvi.Text + ", " + lvi.SubItems[1].Text);
                }
            }

            writer.Close();
            writer = null;

            if (foundSomething)
            {
                System.Diagnostics.Process.Start("notepad.exe", FileName);
            }
        }
#endif

        private void cmdExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();

            //
            // *.oplog
            //
            dlg.Filter = "*." + GlobalConstantsMobile.FILE_EXTENSION + "|*." + GlobalConstantsMobile.FILE_EXTENSION;
            dlg.FileName = "opscatalog." + GlobalConstantsMobile.FILE_EXTENSION;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                string fileName = dlg.FileName;
                string fileNameVersion2 = fileName + "-V2." + GlobalConstantsMobile.FILE_EXTENSION;
                ProgressBegin();
                ExportDataVersion1(fileName);
                ExportDataVersion2(fileNameVersion2);
                ProgressEnd();
            }
        }

        private void cmdImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            //
            // *.oplog
            //
            dlg.Filter = "*." + GlobalConstantsMobile.FILE_EXTENSION + "|*." + GlobalConstantsMobile.FILE_EXTENSION;
            dlg.FileName = "opscatalog." + GlobalConstantsMobile.FILE_EXTENSION;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                ProgressBegin();
                ImportData(dlg.FileName);
                ProgressEnd();
            }

#if DEBUG
            CheckEntries();
#endif

        }

        private void cmdDeleteAll_Click(object sender, EventArgs e)
        {
            if (Confirm(GetText("confirm_remove_all")))
            {
                DeleteAllItems();
            }
        }

        private void lvOperationen_DoubleClick(object sender, EventArgs e)
        {
            InsertSelectedItems();
        }

        private void lvExport_DoubleClick(object sender, EventArgs e)
        {
            DeleteSelectedItems();
        }

        private void ListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            int count = 0;

            if ((e != null) && (e.Item != null) && (e.Item.ListView != null))
            {
                count = e.Item.ListView.SelectedItems.Count;
            }

            if (count == 1)
            {
                //
                // 1 Eintrag
                //
                lblSelection.Text = string.Format(CultureInfo.InvariantCulture, GetText("lblSelection_one"), count);
            }
            else
            {
                //
                // 0 Einträge
                // 2 Einträge
                //
                lblSelection.Text = string.Format(CultureInfo.InvariantCulture, GetText("lblSelection_many"), count);
            }
        }

        private void lvOperationen_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ListView_ItemSelectionChanged(sender, e);
        }

        private void lvExport_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ListView_ItemSelectionChanged(sender, e);
        }

        private void cmdAllUsed_Click(object sender, EventArgs e)
        {
            ImportExecuted();
        }

        private void ImportExecuted()
        {
            try
            {
                XableAllButtonsForLongOperation(null, false);

                DefaultListViewProperties(lvOperationen);
                lvOperationen.Clear();
                lvOperationen.Columns.Add(GetText("opscode"), 80, HorizontalAlignment.Left);
                lvOperationen.Columns.Add(GetText("opstext"), -2, HorizontalAlignment.Left);

                DataView dv = BusinessLayer.GetOperationenExecuted();

                foreach (DataRow row in dv.Table.Rows)
                {
                    ListViewItem lvi = new ListViewItem((string)row["OPSCode"]);
                    lvi.SubItems.Add((string)row["OPSText"]);
                    lvOperationen.Items.Add(lvi);
                }

                SetGroupBoxText(lvOperationen, grpOperationen, GetText(FormName, "OPSKatalog"));
            }
            finally
            {
                XableAllButtonsForLongOperation(null, true);
            }
        }

        private void lvOperationen_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lvGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvGroups.SelectedItems.Count > 0)
            {
                ListViewItem lvi = GetSelectedGroup();

                txtGroup.Clear();
                if (lvi != null)
                {
                    txtGroup.Text = lvi.SubItems[0].Text;
                    PopulateData();
                }
            }
        }

        protected override bool ValidateInput()
        {
            bool success = true;

            string strMessage = EINGABEFEHLER;

            if (txtGroup.Text.Length <= 0)
            {
                strMessage += GetTextControlMissingText(lblGroup);
                success = false;
            }

            if (!success)
            {
                MessageBox(strMessage);
            }

            return success;
        }

        private void PopulateGroups()
        {
            int total = 0;
            ListViewItem lvi;
            lvGroups.Items.Clear();
            lvExport.Items.Clear();

            foreach (string key in _data.Keys)
            {
                lvi = new ListViewItem(key);

                List<ExportOperationenDataItem> list;

                int count = 0;
                if (_data.TryGetValue(key, out list))
                {
                    count = list.Count;
                    total += count;
                }
                lvi.SubItems.Add(count.ToString());
                lvGroups.Items.Add(lvi);
            }
            SetGroupBoxText(lvGroups, grpGroups, GetText("groups"));

            txtTotal.Text = total.ToString();


#if false
            if (lvGroups.Items.Count > 0)
            {
                lvGroups.Items[0].Selected = true;
            }
#endif
        }

        /// <summary>
        /// Populate Listview with all entries for the selected group
        /// </summary>
        private void PopulateData()
        {
            if (lvGroups.SelectedItems.Count > 0)
            {
                lvExport.Items.Clear();

                ListViewItem lviGroup = GetSelectedGroup();
                if (lviGroup == null)
                {
                    // Do nothing, if no group is selected (deleted), clear list.
                }
                else
                {
                    string group = lviGroup.SubItems[0].Text;
                    lvExport.Items.Clear();
                    List<ExportOperationenDataItem> list;

                    if (_data.TryGetValue(group, out list))
                    {
                        foreach (ExportOperationenDataItem item in list)
                        {
                            string opsCode = item.opsCode;
                            string opsText = item.opsText;

                            ListViewItem lvi = new ListViewItem(opsCode);
                            lvi.Tag = item;
                            lvi.SubItems.Add(opsText);
                            lvExport.Items.Add(lvi);
                        }
                        SetGroupBoxText(lvExport, grpExportDaten, GetText("exported_ops"));
                    }
                    else
                    {
                        // No message, because this can never happen... The ListView will simply be empty.
                    }
                }
            }
        }

        private void SelectGroup(string group)
        {
            foreach (ListViewItem lvi in lvGroups.Items)
            {
                if (lvi.SubItems[0].Text.Equals(group))
                {
                    lvi.Selected = true;
                    lvi.EnsureVisible();
                    break;
                }
            }
        }

        private void InsertGroup()
        {
            if (ValidateInput())
            {
                string group = txtGroup.Text;

                List<ExportOperationenDataItem> list;
                if (_data.TryGetValue(group, out list))
                {
                    string msg = string.Format(CultureInfo.InvariantCulture, GetText("err_groupExists"), group);
                    MessageBox(msg);
                }
                else
                {
                    list = new List<ExportOperationenDataItem>();
                    _data.Add(group, list);

                    ListViewItem lvi = new ListViewItem(group);
                    lvGroups.Items.Add(lvi);

                    PopulateGroups();

                    lvExport.Items.Clear();
                    SelectGroup(group);
                }
            }
        }

        private void UpdateGroup()
        {
            if (lvGroups.SelectedItems.Count > 0)
            {
                ListViewItem lviGroup = GetSelectedGroup();

                if (lviGroup != null)
                {
                    string oldGroup = lviGroup.SubItems[0].Text;
                    string newGroup = txtGroup.Text;

                    if (_data.ContainsKey(newGroup))
                    {
                        string msg = string.Format(CultureInfo.InvariantCulture, GetText("err_groupExists"), newGroup);
                        MessageBox(msg);
                    }
                    else
                    {
                        List<ExportOperationenDataItem> list;
                        if (_data.TryGetValue(oldGroup, out list))
                        {
                            lviGroup.SubItems[0].Text = newGroup;
                            _data.Remove(oldGroup);
                            _data.Add(newGroup, list);
                            list.Sort();
                            //SetGroupBoxText(lvGroups, grpGroups, GetText("groups"));

                            //PopulateGroups();
                        }
                    }
                }
            }
        }

        private void DeleteGroup()
        {
            if (lvGroups.SelectedItems.Count > 0)
            {
                if (Confirm(GetTextConfirmDeleteSimple(1)))
                {
                    ListViewItem lviGroup = GetSelectedGroup();
                    if (lviGroup != null)
                    {
                        string group = lviGroup.SubItems[0].Text;
                        _data.Remove(group);
                        lvGroups.Items.Remove(lviGroup);
                        //PopulateGroups();
                        lvExport.Items.Clear();
                        //PopulateData();
                        SetGroupBoxText(lvGroups, grpGroups, GetText("groups"));
                    }
                }
            }
        }

        private void cmdGroupInsert_Click(object sender, EventArgs e)
        {
            InsertGroup();
            txtGroup.Focus();
        }

        private void cmdGroupUpdate_Click(object sender, EventArgs e)
        {
            UpdateGroup();
        }

        private void cmdGroupDelete_Click(object sender, EventArgs e)
        {
            DeleteGroup();
        }

        private void txtGroup_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmdCopyName_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvOperationen.SelectedItems)
            {
                txtGroup.Text = lvi.Text + " " + lvi.SubItems[1].Text;
            }
        }


        private void CreateAll()
        {
            if (Confirm(GetText("deleteAllForAutoCreation")))
            {
                ExportOperationenAutomatic worker = new ExportOperationenAutomatic(BusinessLayer, this);

                XableAllButtonsForLongOperation(false);

                worker.CreateAll();
                _data = worker.GetData();
                PopulateGroups();

                XableAllButtonsForLongOperation(true);
            }
        }

        private void cmdAutoCreate_Click(object sender, EventArgs e)
        {
            CreateAll();
        }
    }
}

