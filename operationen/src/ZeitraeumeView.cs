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

using CMaurer.Operationen.AppFramework;

namespace Operationen
{
    public partial class ZeitraeumeView : OperationenForm
    {
        private DataRow _datarow;

        public ZeitraeumeView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();
        }

        private void InitAA()
        {
            DefaultListViewProperties(lvAA);

            lvAA.Columns.Add(GetText("von"), 100, HorizontalAlignment.Left);
            lvAA.Columns.Add(GetText("bis"), -2, HorizontalAlignment.Left);
        }

        private void PopulateAA()
        {
            cmdApply.Enabled = false;
            ClearTextBoxes();

            lvAA.Items.Clear();
            DataView dataview = null;

            dataview = BusinessLayer.GetZeitraeume();

            if (dataview != null)
            {
                lvAA.Items.Clear();
                lvAA.BeginUpdate();
                foreach (DataRow dataRow in dataview.Table.Rows)
                {
                    ListViewItem lvi = new ListViewItem(Tools.DBNullableDateTime2DateString(dataRow["Von"]));
                    lvi.Tag = ConvertToInt32(dataRow["ID_Zeitraeume"]);
                    lvi.SubItems.Add(Tools.DBNullableDateTime2DateString(dataRow["Bis"]));

                    lvAA.Items.Add(lvi);
                }
                lvAA.EndUpdate();
            }
        }

        private void ZeitraeumeView_Load(object sender, EventArgs e)
        {
            Text = AppTitle(GetText("title"));

            SetInfoText(lblInfo, GetText("info"));

            _bIgnoreControlEvents = true;

            InitAA();

            _bIgnoreControlEvents = false;

            PopulateAA();
        }

        protected override bool ValidateInput()
        {
            bool bSuccess = true;
            string strMessage = EINGABEFEHLER;

            if (txtBeginn.Text.Length <= 0)
            {
                strMessage += GetTextControlMissingText(lblBeginn);
                bSuccess = false;
            }
            else
            {
                if (!Tools.DateIsValidGermanDate(txtBeginn.Text))
                {
                    strMessage += GetTextControlInvalidDate(lblBeginn);
                    bSuccess = false;
                }
            }
            if (txtEnde.Text.Length <= 0)
            {
                strMessage += GetTextControlMissingText(lblEnde);
                bSuccess = false;
            }
            else
            {
                if (txtEnde.Text.Length > 0)
                {
                    if (!Tools.DateIsValidGermanDate(txtEnde.Text))
                    {
                        strMessage += GetTextControlInvalidDate(lblEnde);
                        bSuccess = false;
                    }
                }
            }

            if (!bSuccess)
            {
                MessageBox(strMessage);
            }

            return bSuccess;
        }

        protected override void Control2Object()
        {
            _datarow["Von"] = Tools.InputTextDate2NullableDatabaseDateTime(txtBeginn.Text);
            _datarow["Bis"] = Tools.InputTextDate2NullableDatabaseDateTime(txtEnde.Text);
        }

        protected override void Object2Control()
        {
            txtBeginn.Text = Tools.DBNullableDateTime2DateString(_datarow["Von"]);
            txtEnde.Text = Tools.DBNullableDateTime2DateString(_datarow["Bis"]);
        }

        private void ClearTextBoxes()
        {
            txtBeginn.Text = "";
            txtEnde.Text = "";
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                _datarow = BusinessLayer.CreateDataRowZeitraeume();

                Control2Object();
                BusinessLayer.InsertZeitraum(_datarow);
                PopulateAA();
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            int nCount = lvAA.SelectedItems.Count;

            if (nCount > 0)
            {
                if (Confirm(GetTextConfirmDelete(nCount)))
                {
                    Cursor = Cursors.WaitCursor;

                    foreach (ListViewItem lvi in lvAA.SelectedItems)
                    {
                        int nID = (int)lvi.Tag;

                        if (nID != -1)
                        {
                            if (!BusinessLayer.DeleteZeitraum(nID))
                            {
                                break;
                            }
                        }
                    }
                    PopulateAA();

                    Cursor = Cursors.Default;
                }
            }
            else
            {
                MessageBox(GetTextSelectionNone());
            }
        }

        private void lvAA_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID_Zeitraeume = (int)GetFirstSelectedTag(lvAA);

            if (ID_Zeitraeume != -1)
            {
                _datarow = BusinessLayer.GetZeitraum(ID_Zeitraeume);
                Object2Control();
                cmdApply.Enabled = true;
            }
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Control2Object();
                if (BusinessLayer.UpdateZeitraum(_datarow))
                {
                    PopulateAA();
                }
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }

        private void cbChirurgen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_bIgnoreControlEvents)
            {
                PopulateAA();
            }
        }

        private void cbFilterTypen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_bIgnoreControlEvents)
            {
                PopulateAA();
            }
        }
        protected override string PrintHTMLSummary()
        {
            return GetText("operateur");
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            ContextMenuStrip cms = CreateContextMenu(ModePrintListViewBrowser | ModePrintListViewBrowserSelectedColumns);
            cms.Show(cmdPrint, new Point(10, 10));
        }

        protected override void PrintListViewBrowser_Click(object sender, EventArgs e)
        {
            PrintListView(lvAA, GlobalConstants.KeyPrintLinesAkademischeAusbildungView);
        }
        protected override void PrintListViewBrowserSelectedColumns_Click(object sender, EventArgs e)
        {
            PrintListViewSelectedColumns(lvAA, GlobalConstants.KeyPrintLinesDefaultString);
        }
    }
}

