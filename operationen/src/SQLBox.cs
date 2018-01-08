// Translation: done

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
    partial class SQLBox : OperationenForm
    {
        public SQLBox(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataView dv = BusinessLayer.DatabaseLayer.GetDataView(txtSQL.Text, null, "Test");
                txtCount.Text = "count=" + dv.Table.Rows.Count;
            }
            catch (Exception ex)
            {
                MessageBox(ex.ToString());
            }
        }
    }
}
