using System;
using System.Windows.Forms;
using System.Globalization;

using Utility;

namespace Operationen
{
    partial class MultipleOperationInsertNotificationView : OperationenForm
    {
        public bool dontDisplayNextTime = false;

        public MultipleOperationInsertNotificationView(BusinessLayer businessLayer, int anzahl)
            : base(businessLayer)
        {
            InitializeComponent();

            Text = AppTitle(GetText("title"));

            string text = string.Format(CultureInfo.InvariantCulture, GetText("info"), anzahl);
            lblInfo.Text = text;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (chkNoDisplay.Checked)
            {
                dontDisplayNextTime = true;
            }

            Close();
        }
    }
}
