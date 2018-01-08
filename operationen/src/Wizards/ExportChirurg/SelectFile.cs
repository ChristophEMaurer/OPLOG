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

namespace Operationen.Wizards.ExportChirurg
{
    public partial class SelectFile : ExportChirurgWizardPage
    {
        private const string FormName = "Wizards_ExportChirurg_SelectFile";
        DataRow _row;

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
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "*.mdb|*.mdb";
            dlg.FileName = "operationen." + ((string)_row["Nachname"]).ToLower() + ".mdb";
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
                _businessLayer.MessageBox(GetText(FormName, "error_select_file"));
                success = false;
                goto _exit;
            }
            if (File.Exists(fileName))
            {
                _businessLayer.MessageBox(string.Format(CultureInfo.InvariantCulture, GetText(FormName, "error_file_exists"), fileName));
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

            _row = _businessLayer.GetChirurg((int)Data[ExportChirurgWizardPage.ID_Chirurgen]);

            txtFileName.Text = (string)data[FileName];
        }
    }
}
