using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Utility;
using Windows.Forms;


namespace Operationen
{
    public partial class DatabaseUpdateView : OperationenForm
    {
        public DatabaseUpdateView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();
        }

        private void DatabaseUpdateView_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            this.Text = AppTitle(GetText("title"));

            sb.Append(string.Format(GetText("info"), cmdOK.Text, cmdCancel.Text));
            sb.Append(GetText("change"));
            sb.Replace("\r", Environment.NewLine);

            BusinessLayer.GetDatabaseChanges(sb);
            sb.Replace("$r$", Environment.NewLine);
            
            txtInfo.Text = sb.ToString();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            bool bSuccess;
            string strError = "";

            cmdOK.Enabled = false;
            cmdCancel.Enabled = false;

            Cursor = Cursors.WaitCursor;
            bSuccess = BusinessLayer.TryUpdate(ref strError);
            Cursor = Cursors.Default;

            cmdCancel.Enabled = true;
            if (bSuccess)
            {
                MessageBox(GetText("update_ok"));
                Close();
            }
            else
            {
                MessageBox(string.Format(GetText("update_error"), strError));
            }
        }

        private void DatabaseUpdateView_Shown(object sender, EventArgs e)
        {
            txtInfo.Focus();
            txtInfo.Select(0, 0);
        }
    }
}

