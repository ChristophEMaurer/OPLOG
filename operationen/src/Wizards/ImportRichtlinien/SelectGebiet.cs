using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ImportRichtlinien
{
    public partial class SelectGebiet : ImportRichtlinienWizardPage
    {
        public SelectGebiet(BusinessLayer b)
            : base(b, "Wizards_ImportRichtlinien_SelectGebiet")
        {
            InitializeComponent();
            PopulateGebiete();
        }

        protected override string PageName
        {
            get { return GetText("pageName"); } 
        }

        protected override string Header1
        {
            get
            {
                return GetText("header1");
            }
        }
        protected override string Header2
        {
            get
            {
                return GetText("header1");
            }
        }

        private void PopulateGebiete()
        {
            DataView dv = _businessLayer.GetGebiete();

            DefaultListViewProperties(lvGebiete);

            lvGebiete.Clear();
            lvGebiete.Columns.Add(GetText("gebiet"), 150, HorizontalAlignment.Left);
            lvGebiete.Columns.Add(GetText("bemerkung"), 150, HorizontalAlignment.Left);
            lvGebiete.Columns.Add(GetText("herkunft"), -2, HorizontalAlignment.Left);

            foreach (DataRow dataRow in dv.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem((string)dataRow["Gebiet"]);

                lvi.Tag = ConvertToInt32(dataRow["ID_Gebiete"]);
                lvi.SubItems.Add((string)dataRow["Bemerkung"]);
                lvi.SubItems.Add((string)dataRow["Herkunft"]);

                lvGebiete.Items.Add(lvi);
            }
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

                data[SelectedIndex] = lvGebiete.SelectedIndices[0];
                data[ID_Gebiete] = (int)lvGebiete.SelectedItems[0].Tag;
            }

            return success;
        }

        private bool ValidateInput()
        {
            bool success = true;

            if (lvGebiete.SelectedIndices.Count == 0)
            {
                _businessLayer.MessageBox(GetText("msg1"));
                success = false;
                goto _exit;
            }

        _exit:
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

            lvGebiete.SelectedIndices.Clear();
            lvGebiete.SelectedIndices.Add((int)data[SelectedIndex]);
        }
    }
}
