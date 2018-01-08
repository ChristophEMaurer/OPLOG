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
    public abstract partial class TypenTemplateView : OperationenForm
    {
        private new const string FormName = "TypenTemplateView";

        private DataRow _row;

        /// <summary>
        /// Subclass must specify the right that allows editing
        /// </summary>
        /// <returns>The right that allows editing</returns>
        protected abstract string GetEditRight();

        /// <summary>
        /// Override this function to specify your table name
        /// </summary>
        /// <returns></returns>
        protected abstract string GetTableName();

        public TypenTemplateView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();

            cmdInsert.SetSecurity(businessLayer, GetEditRight());
            cmdApply.SetSecurity(businessLayer, GetEditRight());
            cmdDelete.SetSecurity(businessLayer, GetEditRight());
            txtTypen.SetSecurity(businessLayer, GetEditRight());
        }

        protected bool EditAllowed()
        {
            return UserHasRight(GetEditRight());
        }

        /// <summary>
        /// Override this function if the name of the text column is different from 'Text'
        /// </summary>
        /// <returns></returns>
        protected virtual string GetTextColumnName()
        {
            return GetText(FormName, "Text");
        }

        private void InitTypen()
        {
            DefaultListViewProperties(lvTypen);

            lvTypen.Columns.Add(GetTitleSingular(), -2, HorizontalAlignment.Left);
        }

        private void PopulateTypen()
        {
            cmdApply.Enabled = false;

            DataView dataview = GetDataView();

            lvTypen.Items.Clear();
            lvTypen.BeginUpdate();
            foreach (DataRow dataRow in dataview.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem((string)dataRow[GetTextColumnName()]);
                lvi.Tag = ConvertToInt32(dataRow["ID"]);

                lvTypen.Items.Add(lvi);
            }
            lvTypen.EndUpdate();
            txtTypen.Clear();

            PopulateDemo(dataview);
        }

        private void PopulateDemo(DataView dv)
        {
            cbDemo.DisplayMember = GetTextColumnName();
            cbDemo.DataSource = dv;
        }

        private string GetTitleSingular()
        {
            return GetText("titleSingular");
        }

        private string GetTitlePlural()
        {
            return GetText("titlePlural");
        }

        virtual protected string GetInfoText()
        {
            return GetText("info");
        }

        private void TypenTemplateView_Load(object sender, EventArgs e)
        {
            SetInfoText(lblInfo, GetInfoText());
            this.Text = AppTitle(GetText("title"));

            InitTypen();
            PopulateTypen();

            lblDemoTitle.Text = GetTitleSingular();
            grpTypen.Text = GetTitlePlural();
        }

        protected override bool ValidateInput()
        {
            bool bSuccess = true;
            string strMessage = EINGABEFEHLER;

            if (txtTypen.Text.Length <= 0)
            {
                strMessage += GetTextControlMissingText(lblTypen);
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
            _row[GetTextColumnName()] = txtTypen.Text;
        }

        protected override void Object2Control()
        {
            txtTypen.Text = (string)_row[GetTextColumnName()];
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            if (EditAllowed())
            {
                if (ValidateInput())
                {
                    _row = CreateDataRow();

                    Control2Object();
                    InsertObject(_row);
                    PopulateTypen();
                }
            }
        }

        void cmdCancel_Click(object sender, EventArgs e)
        {
            base.CancelClicked();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (EditAllowed())
            {
                int nCount = lvTypen.SelectedItems.Count;

                if (nCount > 0)
                {
                    if (Confirm(GetTextConfirmDelete(nCount)))
                    {
                        Cursor = Cursors.WaitCursor;
                        foreach (ListViewItem lvi in lvTypen.SelectedItems)
                        {
                            int nID = (int)lvi.Tag;

                            if (nID != -1)
                            {
                                DeleteObject(nID);
                            }
                        }
                        PopulateTypen();
                        Cursor = Cursors.Default;
                    }
                }
                else
                {
                    MessageBox(GetTextSelectionNone());
                }
            }
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            if (EditAllowed())
            {
                if (ValidateInput())
                {
                    Control2Object();
                    UpdateObject(_row);
                    PopulateTypen();
                }
            }
        }

        private void lvTypen_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID = (int)GetFirstSelectedTag(lvTypen);
            if (ID != -1)
            {
                _row = GetObject(ID);
                Object2Control();
                if (EditAllowed())
                {
                    cmdApply.Enabled = true;
                }
            }
        }

        protected virtual DataView GetDataView()
        {
            return BusinessLayer.GetTypenTemplate(GetTableName(),
                GetTextColumnName(), false);
        }
        protected virtual void InsertObject(DataRow row)
        {
            BusinessLayer.InsertTypenTemplate(GetTableName(),
                GetTextColumnName(), row);
        }
        protected virtual void UpdateObject(DataRow row)
        {
            BusinessLayer.UpdateTypenTemplate(GetTableName(),
                GetTextColumnName(), row);
        }
        protected virtual void DeleteObject(int id)
        {
            BusinessLayer.DeleteTypenTemplate(GetTableName(), id);
        }
        protected virtual DataRow GetObject(int id)
        {
            return BusinessLayer.GetTypenTemplate(GetTableName(),
                GetTextColumnName(), id);
        }
        protected virtual DataRow CreateDataRow()
        {
            return BusinessLayer.CreateDataRowTypenTemplate(GetTableName(),
                GetTextColumnName());
        }
    }
}


