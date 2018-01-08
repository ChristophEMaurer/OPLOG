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
    public partial class AkademischeAusbildungView : OperationenForm
    {
        private DataRow _datarow;

        public AkademischeAusbildungView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();
        }

        private void InitAA()
        {
            DefaultListViewProperties(lvAA);

            lvAA.Columns.Add(GetText("Beginn"), 80, HorizontalAlignment.Left);
            lvAA.Columns.Add(GetText("Ende"), 80, HorizontalAlignment.Left);
            lvAA.Columns.Add(GetText("Bezeichnung"), 140, HorizontalAlignment.Left);
            lvAA.Columns.Add(GetText("Organisation"), -2, HorizontalAlignment.Left);
        }

        private void PopulateAA()
        {
            cmdApply.Enabled = false;
            ClearTextBoxes();

            int ID_Chirurgen = -1;

            if (cbFilterChirurgen.Items.Count > 0)
            {
                ID_Chirurgen = ConvertToInt32(cbFilterChirurgen.SelectedValue);
            }

            lvAA.Items.Clear();
            if (ID_Chirurgen != -1)
            {
                DataView dataview = null;

                dataview = BusinessLayer.GetAkademischeAusbildungen(ID_Chirurgen);

                if (dataview != null)
                {
                    lvAA.Items.Clear();
                    lvAA.BeginUpdate();
                    foreach (DataRow dataRow in dataview.Table.Rows)
                    {
                        ListViewItem lvi = new ListViewItem(Tools.DBNullableDateTime2DateString(dataRow["Beginn"]));
                        lvi.Tag = ConvertToInt32(dataRow["ID_AkademischeAusbildungen"]);
                        lvi.SubItems.Add(Tools.DBNullableDateTime2DateString(dataRow["Ende"]));
                        lvi.SubItems.Add((string)dataRow["Text"]);
                        lvi.SubItems.Add((string)dataRow["Organisation"]);

                        lvAA.Items.Add(lvi);
                    }
                    lvAA.EndUpdate();
                }
            }
        }

        private void PopulateAkademischeAusbildungTypen()
        {
            // Zum Eingeben: ohne "alle"
            DataView dv = BusinessLayer.GetTypenTemplate(BusinessLayer.TableAkademischeAusbildungTypen, false);
            cbTypen.ValueMember = "ID";
            cbTypen.DisplayMember = "Text";
            cbTypen.DataSource = dv;
        }

        private void AkademischeAusbildungView_Load(object sender, EventArgs e)
        {
            Text = AppTitle(GetText("title"));

            SetInfoText(lblInfo, string.Format(CultureInfo.InvariantCulture, GetText("info"), Command_AkademischeAusbildungTypenView));

            _bIgnoreControlEvents = true;

            PopulateChirurgen(cbFilterChirurgen);
            PopulateAkademischeAusbildungTypen();
            InitAA();

            _bIgnoreControlEvents = false;

            PopulateAA();
        }

        protected override bool ValidateInput()
        {
            bool bSuccess = true;
            string strMessage = EINGABEFEHLER;

            if (txtOrganisation.Text.Length <= 0)
            {
                strMessage += GetTextControlMissingText(lblOrganisation);
                bSuccess = false;
            }
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
            if (txtEnde.Text.Length > 0)
            {
                if (!Tools.DateIsValidGermanDate(txtEnde.Text))
                {
                    strMessage += GetTextControlInvalidDate(lblEnde);
                    bSuccess = false;
                }
            }
            if (cbTypen.SelectedIndex == -1)
            {
                strMessage += GetText("notype");
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
            _datarow["Beginn"] = Tools.InputTextDate2NullableDatabaseDateTime(txtBeginn.Text);
            _datarow["Ende"] = Tools.InputTextDate2NullableDatabaseDateTime(txtEnde.Text);
            _datarow["Organisation"] = txtOrganisation.Text;
            _datarow["ID_AkademischeAusbildungTypen"] = ConvertToInt32(cbTypen.SelectedValue);
            _datarow["ID_Chirurgen"] = ConvertToInt32(cbFilterChirurgen.SelectedValue);
        }

        protected override void Object2Control()
        {
            txtBeginn.Text = Tools.DBNullableDateTime2DateString(_datarow["Beginn"]);
            txtEnde.Text = Tools.DBNullableDateTime2DateString(_datarow["Ende"]);
            txtOrganisation.Text = (string)_datarow["Organisation"];
            cbTypen.SelectedValue = ConvertToInt32(_datarow["ID_AkademischeAusbildungTypen"]);
        }

        private void ClearTextBoxes()
        {
            txtBeginn.Text = "";
            txtEnde.Text = "";
            txtOrganisation.Text = "";
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                _datarow = BusinessLayer.CreateDataRowAkademischeAusbildungen();

                Control2Object();
                BusinessLayer.InsertAkademischeAusbildung(_datarow);
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
                            if (!BusinessLayer.DeleteAkademischeAusbildung(nID))
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
            int ID_AkademischeAusbildungen = (int)GetFirstSelectedTag(lvAA);

            if (ID_AkademischeAusbildungen != -1)
            {
                _datarow = BusinessLayer.GetAkademischeAusbildung(ID_AkademischeAusbildungen);
                Object2Control();
                cmdApply.Enabled = true;
            }
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Control2Object();
                if (BusinessLayer.UpdateAkademischeAusbildung(_datarow))
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
            return GetText("operateur") + ": " + MakeSafeHTML(cbFilterChirurgen.Text);
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
            PrintListViewSelectedColumns(lvAA, GlobalConstants.KeyPrintLinesAkademischeAusbildungView);
        }
    }
}

