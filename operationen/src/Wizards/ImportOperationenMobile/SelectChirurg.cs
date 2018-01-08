using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ImportOperationenMobile
{
    public partial class SelectChirurg : ImportOperationenMobileWizardPage
    {
        private const string FormName = "Wizards_ImportOperationenMobile_SelectChirurg";

        public SelectChirurg(BusinessLayer b)
            : base(b)
        {
            InitializeComponent();
            PopulateChirurgen(lvChirurgen);
        }
        protected void PopulateChirurgen(ListView lv)
        {
            DataView dataview = _businessLayer.GetChirurgen();

            lv.Clear();

            DefaultListViewProperties(lv);

            lv.Columns.Add(GetText(FormName, "Anrede"), 60, HorizontalAlignment.Left);
            lv.Columns.Add(GetText(FormName, "Nachname"), 120, HorizontalAlignment.Left);
            lv.Columns.Add(GetText(FormName, "Vorname"), 70, HorizontalAlignment.Left);
            lv.Columns.Add(GetText(FormName, "UserID"), -2, HorizontalAlignment.Left);

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

        protected override string PageName
        {
            get { return GetText(FormName, "pagename"); }
        }

        protected override string Header1
        {
            get { return GetText(FormName, "header1"); }
        }

        protected override string Header2
        {
            get { return GetText(FormName, "header2"); }
        }
        private bool LeavePage(bool validateInput)
        {
            bool success = true;

            if (validateInput)
            {
                success = ValidateInput();
            }

            if (success)
            {
                Hashtable data = Data;

                data[SelectedIndex] = lvChirurgen.SelectedIndices[0];
                data[ID_Chirurgen] = (int)lvChirurgen.SelectedItems[0].Tag;
            }

            return success;
        }

        private bool ValidateInput()
        {
            bool success = true;

            if (lvChirurgen.SelectedIndices.Count == 0)
            {
                _businessLayer.MessageBox(GetText(FormName, "error_select_user"));
                success = false;
            }

            return success;
        }

        protected override bool OnPreNext()
        {
            return LeavePage(true);
        }
        protected override bool OnPreBack()
        {
            return LeavePage(false);
        }

        protected override void OnActivate()
        {
            Hashtable data = Data;

            lvChirurgen.SelectedIndices.Clear();
            lvChirurgen.SelectedIndices.Add((int)data[SelectedIndex]);
        }
    }
}
