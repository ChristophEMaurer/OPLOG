using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Windows.Forms;

namespace Operationen
{
    public partial class SelectListViewColumnsView : OperationenForm
    {
        private List<int> _columnList = new List<int>();
        private ListView _listView;

        public SelectListViewColumnsView(BusinessLayer businessLayer, ListView listView)
            : base(businessLayer)
        {
            _listView = listView;

            InitializeComponent();

            this.Text = AppTitle(GetText("title"));
            SetInfoText(lblInfo, GetText("info"));
        }

        public List<int> ColumnList
        {
            get { return _columnList; }
        }

        private void ChirurgenView_Load(object sender, EventArgs e)
        {
            PopulateListView(lvListView);
        }

        private void PopulateListView(OplListView lv)
        {
            lv.Clear();

            DefaultListViewProperties(lv);
            lv.CheckBoxes = true;

            lv.Columns.Add(GetText("spalte"), -2, HorizontalAlignment.Left);

            foreach (ColumnHeader column in _listView.Columns)
            {
                ListViewItem lvi = new ListViewItem((column.DisplayIndex + 1).ToString() + " - " + column.Text);
                lvi.Checked = true;
                lv.Items.Add(lvi);
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            _columnList.Clear();

            for (int i = 0; i < lvListView.Items.Count; i++)
            {
                if (lvListView.Items[i].Checked)
                {
                    _columnList.Add(i);
                }
            }
            if (_columnList.Count == 0)
            {
                MessageBox(GetText("noColumnSelected"));
            }
            else
            {
                Close();
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            _columnList.Clear();
            Close();
        }
    }
}
