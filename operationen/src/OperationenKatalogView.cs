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
    public partial class OperationenKatalogView : OperationenForm
    {
        private DataRow _oOperation;

        public OperationenKatalogView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            _oOperation = this.BusinessLayer.CreateDataRowOperation();

            InitializeComponent();

            // SetWatermark(lvOperationen);

            cmdInsert.SetSecurity(BusinessLayer, "OperationenKatalogView.cmdInsert");
            cmdDelete.SetSecurity(BusinessLayer, "OperationenKatalogView.cmdDelete");
            cmdDeleteAll.SetSecurity(BusinessLayer, "OperationenKatalogView.cmdDeleteAll");
            txtKode.SetSecurity(BusinessLayer, "OperationenKatalogView.edit");
            txtText.SetSecurity(BusinessLayer, "OperationenKatalogView.edit");
        }

        protected override void ProgressBegin()
        {
            cmdInsert.Enabled = false;
            cmdDelete.Enabled = false;
            cmdCancel.Enabled = false;
            cmdPopulate.Enabled = false;
            cmdDeleteAll.Enabled = false;
        }
        protected override void ProgressEnd()
        {
            cmdInsert.Enabled = true;       
            cmdDelete.Enabled = true;       
            cmdDeleteAll.Enabled = true;    
            cmdCancel.Enabled = true;
            cmdPopulate.Enabled = true;
        }

        private void PopulateOperationen()
        {
            base.PopulateOperationen(grpOperationen, lvOperationen, txtFilterOPSCode.Text, txtFilterOPSText.Text);
        }

        private void OperationenKatalogView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));

            cmdDelete.Text = GetText("cmdDelete");
            cmdDeleteAll.Text = GetText("cmdDeleteAll");
            cmdInsert.Text = GetText("cmdInsert");
            
            cmdDeleteAll.Enabled = true;
            cmdInsert.Enabled = true;
            cmdDelete.Enabled = true;
            txtKode.Enabled = true;
            txtText.Enabled = true;

            SetInfoText(lblInfoSearch, string.Format(GetText("info_search"), cmdDeleteAll.Text));
            SetInfoText(lblInfo, string.Format(GetText("info"), cmdDeleteAll.Text,Command_ImportOPSWizard));

            if (UserHasRight("ImportOPSWizard.edit"))
            {
                AddLinkLabelLink(lblInfo, Command_ImportOPSWizard, Command_ImportOPSWizard);
            }
        }

        protected override bool ValidateInput()
        {
            bool bSuccess = true;
            string strMessage = EINGABEFEHLER;

            if (txtKode.Text.Length <= 0)
            {
                strMessage += GetTextControlMissingText(lblKode);
                bSuccess = false;
            }
            if (txtText.Text.Length <= 0)
            {
                strMessage += GetTextControlMissingText(lblText);
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
            _oOperation["OPS-Kode"] = txtKode.Text;
            _oOperation["OPS-Text"] = txtText.Text;
        }

        protected override void SaveObject()
        {
            BusinessLayer.InsertOperation(_oOperation);
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Control2Object();
                SaveObject();
                PopulateOperationen();
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            ListViewItem lviTest = GetFirstSelectedLVI(lvOperationen, true);

            if (lviTest != null)
            {
                int nCount = lvOperationen.SelectedItems.Count;

                if (Confirm(GetTextConfirmDelete(nCount)))
                {
                    foreach (ListViewItem lvi in lvOperationen.SelectedItems)
                    {
                        int nID_Operationen = (int)lvi.Tag;

                        if (nID_Operationen != -1)
                        {
                            if (!BusinessLayer.DeleteOperation(nID_Operationen))
                            {
                                break;
                            }
                        }
                    }
                    PopulateOperationen();
                }
            }
        }

        private void cmdPopulate_Click(object sender, EventArgs e)
        {
            PopulateOperationen();
        }

        private void OperationenKatalogView_Shown(object sender, EventArgs e)
        {
            txtFilterOPSCode.Focus();
        }

        private void lvOperationen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvOperationen.SelectedItems.Count > 0)
            {
                int nIndex = lvOperationen.SelectedItems[0].Index;
                txtKode.Text = lvOperationen.Items[nIndex].SubItems[0].Text;
                txtText.Text = lvOperationen.Items[nIndex].SubItems[1].Text;
            }
        }

        private void cmdDeleteAll_Click(object sender, EventArgs e)
        {
            if (Confirm(GetText("confirm_delete1")))
            {
                if (Confirm(GetText("confirm_delete2")))
                {
                    lvOperationen.Items.Clear();
                    BusinessLayer.DeleteOPSKatalog();
                    MessageBox(GetText("delete2_success"));
                }
            }
        }

        private void lblInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OperationenLogbuchView.TheMainWindow.StartMenuItem(e.Link.LinkData);
        }

        private void OperationenKatalogView_Resize(object sender, EventArgs e)
        {
            grpOperationen.Height = cmdDeleteAll.Top - grpOperationen.Top - ControlYOffset;
        }
    }
}