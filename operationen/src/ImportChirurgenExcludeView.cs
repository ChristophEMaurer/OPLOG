using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using Utility;
using Windows.Forms;

namespace Operationen
{
    public partial class ImportChirurgenExcludeView : OperationenForm
    {
        DataRow _oRow;

        public ImportChirurgenExcludeView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();
        }

        private void InitChirurgen()
        {
            DefaultListViewProperties(lvChirurgen);

            lvChirurgen.Columns.Add(GetText(FormName, "nachname"), 200, HorizontalAlignment.Left);
            lvChirurgen.Columns.Add(GetText(FormName, "vorname"), -2, HorizontalAlignment.Left);
        }


        private void PopulateChirurgen()
        {
            DataView dataview = BusinessLayer.GetImportChirurgenExclude();

            lvChirurgen.Items.Clear();
            lvChirurgen.BeginUpdate();
            foreach (DataRow dataRow in dataview.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem((string)dataRow["Nachname"]);
                lvi.Tag = ConvertToInt32(dataRow["ID_ImportChirurgenExclude"]);
                lvi.SubItems.Add((string)dataRow["Vorname"]);
                lvChirurgen.Items.Add(lvi);
            }
            lvChirurgen.EndUpdate();

            ClearFields();
        }

        private void ImportChirurgenExcludeView_Load(object sender, EventArgs e)
        {
            SetInfoText(lblInfo, GetText("info"));
            
            this.Text = AppTitle(GetText("title"));

            txtNachname.Enabled =
            txtVorname.Enabled = 
            cmdInsert.Enabled =
            cmdDelete.Enabled = UserHasRight("ImportChirurgenExcludeView.edit");

            InitChirurgen();
            PopulateChirurgen();
        }

        protected override bool ValidateInput()
        {
            bool bSuccess = true;
            string strMessage = EINGABEFEHLER;

            if (txtNachname.Text.Length <= 0)
            {
                strMessage += GetTextControlMissingText(lblNachname);
                bSuccess = false;
            }

            if (!bSuccess)
            {
                MessageBox(strMessage);
            }

            return bSuccess;
        }

        protected override void Control2Object()
        {
            _oRow["Nachname"] = txtNachname.Text.ToLower();
            _oRow["Vorname"] = txtVorname.Text.ToLower();
        }

        protected override void SaveObject()
        {
            BusinessLayer.InsertImportChirurgenExclude(_oRow);
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                _oRow = BusinessLayer.CreateDataRowImportChirurgenExclude();

                Control2Object();
                SaveObject();
                PopulateChirurgen();
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            int count = lvChirurgen.SelectedItems.Count;

            if (count > 0)
            {
                if (Confirm(string.Format(CultureInfo.InvariantCulture, GetText("confirm_delete"), count)))
                {
                    Cursor = Cursors.WaitCursor;
                    foreach (ListViewItem lvi in lvChirurgen.SelectedItems)
                    {
                        int nImportChirurgenExclude = (int)lvi.Tag;

                        if (nImportChirurgenExclude != -1)
                        {
                            if (!BusinessLayer.DeleteImportChirurgenExclude(nImportChirurgenExclude))
                            {
                                break;
                            }
                        }
                    }
                    PopulateChirurgen();
                    Cursor = Cursors.Default;
                }
            }
            else
            {
                MessageBox(GetTextSelectionNone());
            }
        }

        private void ClearFields()
        {
            txtNachname.Text = "";
            txtVorname.Text = "";
        }
    }
}