using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ImportOperationenMobile
{
    public partial class SelectFile : ImportOperationenMobileWizardPage
    {
        private const string FormName = "Wizards_ImportOperationenMobile_SelectFile";

        public SelectFile(BusinessLayer b)
            : base(b)
        {
            InitializeComponent();

            lblText.Text = GetText(FormName, "text");
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

        private void cmdFileName_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            //
            // *.oplog  *.*
            //
            dlg.Filter = "*." + GlobalConstantsMobile.FILE_EXTENSION + "|*." + GlobalConstantsMobile.FILE_EXTENSION + "|" + GetText("filterAll") + "|*.*";
            if (DialogResult.OK == dlg.ShowDialog())
            {
                txtFileName.Text = dlg.FileName;
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

                data[FileName] = txtFileName.Text;
            }

            return success;
        }

        private bool ValidateInput()
        {
            bool success = true;

            string fileName = txtFileName.Text;

            if (fileName.Length == 0)
            {
                _businessLayer.MessageBox(GetText(FormName, "err_no_file"));
                success = false;
                goto _exit;
            }
            if (!File.Exists(fileName))
            {
                _businessLayer.MessageBox(string.Format(CultureInfo.InvariantCulture, GetText("err_missing_file"), fileName));
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

            txtFileName.Text = (string)data[FileName];
        }
    }
}
