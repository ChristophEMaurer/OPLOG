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
    public partial class GebieteView : OperationenForm
    {
        private DataRow _gebiet;

        public GebieteView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();

            cmdApply.SetSecurity(BusinessLayer, "GebieteView.edit");
            cmdClear.SetSecurity(BusinessLayer, "GebieteView.edit");
            cmdDelete.SetSecurity(BusinessLayer, "GebieteView.edit");
            cmdInsert.SetSecurity(BusinessLayer, "GebieteView.edit");
            txtBemerkung.SetSecurity(BusinessLayer, "GebieteView.edit");
            txtGebiet.SetSecurity(BusinessLayer, "GebieteView.edit");
            txtHerkunft.SetSecurity(BusinessLayer, "GebieteView.edit");
        }

        private void InitGebiete()
        {
            DefaultListViewProperties(lvGebiete);

            lvGebiete.Columns.Add(GetText("gebiet"), 200, HorizontalAlignment.Left);
            lvGebiete.Columns.Add(GetText("bemerkung"), 240, HorizontalAlignment.Left);
            lvGebiete.Columns.Add(GetText("herkunft"), -2, HorizontalAlignment.Left);
        }

        private void PopulateGebiete()
        {
            cmdApply.Enabled = false;

            DataView dataview = BusinessLayer.GetGebiete();

            if (dataview != null)
            {
                lvGebiete.Items.Clear();
                lvGebiete.BeginUpdate();
                foreach (DataRow dataRow in dataview.Table.Rows)
                {
                    ListViewItem lvi = new ListViewItem((string)dataRow["Gebiet"]);
                    lvi.Tag = ConvertToInt32(dataRow["ID_Gebiete"]);
                    lvi.SubItems.Add((string)dataRow["Bemerkung"]);
                    lvi.SubItems.Add((string)dataRow["Herkunft"]);

                    lvGebiete.Items.Add(lvi);
                }
                lvGebiete.EndUpdate();
                ClearTextBoxes();
            }
        }

        private void GebieteView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));

            SetInfoText(lblInfo, GetText("info"));
 
            InitGebiete();
            PopulateGebiete();
        }

        protected override bool ValidateInput()
        {
            bool bSuccess = true;
            string strMessage = EINGABEFEHLER;

            if (txtGebiet.Text.Length <= 0)
            {
                strMessage += GetTextControlMissingText(lblGebiet);
                bSuccess = false;
            }
            if (txtBemerkung.Text.Length <= 0)
            {
                strMessage += GetTextControlMissingText(lblBemerkung);
                bSuccess = false;
            }
            if (txtHerkunft.Text.Length <= 0)
            {
                strMessage += GetTextControlMissingText(lblHerkunft);
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
            _gebiet["Gebiet"] = txtGebiet.Text;
            _gebiet["Bemerkung"] = txtBemerkung.Text;
            _gebiet["Herkunft"] = txtHerkunft.Text;
        }
        protected override void Object2Control()
        {
            txtGebiet.Text = (string)_gebiet["Gebiet"];
            txtBemerkung.Text = (string)_gebiet["Bemerkung"];
            txtHerkunft.Text = (string)_gebiet["Herkunft"];
        }

        private void ClearTextBoxes()
        {
            txtGebiet.Text = "";
            txtBemerkung.Text = "";
            txtHerkunft.Text = "";
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            if (UserHasRight("GebieteView.edit"))
            {
                if (ValidateInput())
                {
                    _gebiet = BusinessLayer.CreateDataRowGebiet();

                    Control2Object();
                    BusinessLayer.InsertGebiet(_gebiet);
                    PopulateGebiete();
                }
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (UserHasRight("GebieteView.edit"))
            {
                int nCount = lvGebiete.SelectedItems.Count;

                if (nCount > 0)
                {
                    string msg = string.Format(CultureInfo.InvariantCulture, GetText("confirm"), nCount);

                    if (Confirm(msg))
                    {
                        Cursor = Cursors.WaitCursor;
                        foreach (ListViewItem lvi in lvGebiete.SelectedItems)
                        {
                            int nID = (int)lvi.Tag;

                            if (nID != -1)
                            {
                                if (!BusinessLayer.DeleteGebiet(nID))
                                {
                                    break;
                                }
                            }
                        }
                        PopulateGebiete();
                        Cursor = Cursors.Default;
                    }
                }
                else
                {
                    MessageBox(GetTextSelectionNone());
                }
            }
        }

        private void lvGebiete_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID_Gebiete = (int)GetFirstSelectedTag(lvGebiete);

            if (ID_Gebiete != -1)
            {
                _gebiet = BusinessLayer.GetGebiet(ID_Gebiete);
                Object2Control();

                cmdApply.Enabled = true;
            }
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            if (UserHasRight("GebieteView.edit"))
            {
                if (ValidateInput())
                {
                    Control2Object();
                    if (BusinessLayer.UpdateGebiet(_gebiet))
                    {
                        PopulateGebiete();
                    }
                }
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            if (UserHasRight("GebieteView.edit"))
            {
                ClearTextBoxes();
            }
        }

        private void GebieteView_Resize(object sender, EventArgs e)
        {
            grpGebiete.Height = lblInfo.Top - grpGebiete.Top - ControlYOffset;
        }
    }
}

