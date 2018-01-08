using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;
using System.Data;
using System.Timers; 
using System.Drawing.Drawing2D; 

using Utility;
using Operationen.Wizards.CreateCustomerData;

namespace Operationen
{
    partial class TextBoxView : OperationenForm
    {
        public TextBoxView(BusinessLayer businessLayer, string header, Dictionary<string, string> dict)
            : base(businessLayer)
        {
            InitializeComponent();

            Text = AppTitle();

            lblHeader.Text = header;

            lvInfos.Clear();
            DefaultListViewProperties(lvInfos);

            lvInfos.Clear();
            lvInfos.Columns.Add(GetText("name"), 150, HorizontalAlignment.Left);
            lvInfos.Columns.Add(GetText("value"), -2, HorizontalAlignment.Left);

            foreach (string key in dict.Keys)
            {
                ListViewItem lvi = new ListViewItem(key);

                lvi.SubItems.Add(dict[key]);
                lvInfos.Items.Add(lvi);
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

