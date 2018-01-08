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

namespace Operationen.Wizards.ImportChirurg
{
    public partial class SelectFile : ImportChirurgWizardPage
    {
        public SelectFile(BusinessLayer b)
            : base(b, "Wizards_ImportChirurg_SelectFile")
        {
            InitializeComponent();

            lblText.Text = GetText("info");
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
                return GetText("header2");
            }
        }
        private void cmdFileName_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "*.mdb|*.mdb";
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
                _businessLayer.MessageBox(GetText("msg1"));
                success = false;
                goto _exit;
            }
            if (!File.Exists(fileName))
            {
                string text = string.Format(CultureInfo.InvariantCulture, GetText("msg2"), fileName);
                _businessLayer.MessageBox(text);
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
