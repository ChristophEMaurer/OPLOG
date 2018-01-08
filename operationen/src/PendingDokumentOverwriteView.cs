using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace Operationen
{
    public partial class PendingDokumentOverwriteView : OperationenForm
    {
        public PendingDokumentOverwriteView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();
            Text = AppTitle(GetText("title"));
        }

        private void PendingDokumentOverwriteView_Load(object sender, EventArgs e)
        {
            SetInfoText(lblInfo, GetText("info1"));

            string text = string.Format(CultureInfo.InvariantCulture, GetText("info2"),
                cmdEdit.Text, cmdOverwrite.Text, cmdCancel.Text);

            SetInfoText(lblInfo2, text);
        }

        private void cmdOverwrite_Click(object sender, EventArgs e)
        {
            string text = string.Format(CultureInfo.InvariantCulture, GetText("confirm1"));
            if (Confirm(text))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }
    }
}
