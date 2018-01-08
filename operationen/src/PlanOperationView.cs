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
    public partial class PlanOperationView : OperationenForm
    {
        private int _nID_Chirurgen = -1;
        private bool _bEdit;
        private DataRow _oPlanOperation;

        public PlanOperationView(BusinessLayer businessLayer, int nID_Chirurgen, DataRow oPlanOperation)
            : base(businessLayer)
        {
            _nID_Chirurgen = nID_Chirurgen;

            if (oPlanOperation == null)
            {
                _oPlanOperation = this.BusinessLayer.CreateDataRowPlanOperation(nID_Chirurgen);
                _bEdit = false;
            }
            else
            {
                _oPlanOperation = oPlanOperation;
                _bEdit = true;
            }

            InitializeComponent();
        }

        protected override void Control2Object()
        {
            _oPlanOperation["Operation"] = this.txtOperation.Text;
            _oPlanOperation["Anzahl"] = Convert.ToInt32(txtAnzahl.Text);
            _oPlanOperation["DatumVon"] = Tools.InputTextDate2DateTime(txtDatumVon.Text);
            _oPlanOperation["DatumBis"] = Tools.InputTextDate2DateTime(txtDatumBis.Text);
        }

        protected override void Object2Control()
        {
            txtAnzahl.Text = _oPlanOperation["Anzahl"].ToString();
            txtOperation.Text = _oPlanOperation["Operation"].ToString();
            txtDatumVon.Text = Tools.DBDateTime2DateString(_oPlanOperation["DatumVon"]);
            txtDatumBis.Text = Tools.DBDateTime2DateString(_oPlanOperation["DatumBis"]);
        }
        protected override void SaveObject()
        {
            if (this._bEdit)
            {
                BusinessLayer.UpdatePlanOperation(_oPlanOperation);
            }
            else
            {
                BusinessLayer.InsertPlanOperation(_oPlanOperation);
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Control2Object();
                SaveObject();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ProgressEnableButtons(bool enable)
        {
            txtDatumBis.Enabled = enable;
            cmdCancel.Enabled = enable;
            cmdOK.Enabled = enable;
            cmdPopulate.Enabled = enable;
            txtDatumVon.Enabled = enable;
        }
        protected override void ProgressBegin()
        {
            ProgressEnableButtons(false);
        }

        protected override void ProgressEnd()
        {
            ProgressEnableButtons(true);
        }

        private void PopulateOperationen()
        {
            PopulateOperationen(grpOperationen, lvOperationen, txtFilterKode.Text, txtFilterText.Text);
        }

        private void PlanOperationView_Load(object sender, EventArgs e)
        {
            if (_bEdit)
            {
                this.Text = AppTitle(GetText("titleEdit"));
            }
            else
            {
                this.Text = AppTitle(GetText("titleNew"));
            }

            Object2Control();
            
            SetInfoText(lblInfo, string.Format(CultureInfo.InvariantCulture, GetText("info"), this.lblOperation.Text));
        }

        private void cmdPlanOperationen_Click(object sender, EventArgs e)
        {
            OperationenLogbuchView dlg = new OperationenLogbuchView(BusinessLayer);
            dlg.ShowDialog();
        }

        private void txtOperation_TextChanged(object sender, EventArgs e)
        {
            if (lvOperationen.Items.Count > 0)
            {
                ListViewItem lvi = lvOperationen.FindItemWithText(txtOperation.Text, true, 0, true);

                if (lvi != null)
                {
                    lvOperationen.EnsureVisible(lvi.Index);
                }
            }
        }

        private void lvOperationen_DoubleClick(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvOperationen.SelectedItems)
            {
                string strText = lvi.Text;
                int nIndex = strText.LastIndexOf('.');
                if (nIndex == -1)
                {
                    this.txtOperation.Text = lvi.Text;
                }
                else
                {
                    this.txtOperation.Text = lvi.Text.Substring(0, nIndex);
                }
                break;
            }
        }
        protected override bool ValidateInput()
        {
            bool success = true;
            StringBuilder sb = new StringBuilder(EINGABEFEHLER);

            if (!ValidateDatumVonBis(lblVon, txtDatumVon.Text, lblBis, txtDatumBis.Text, sb))
            {
                success = false;
            }

            if (txtOperation.Text.Length == 0)
            {
                sb.Append(GetTextControlMissingText(lblOperation));
                success = false;
            }
            if (txtAnzahl.Text.Length == 0)
            {
                sb.Append(GetTextControlMissingText(lblAnzahl));
                success = false;
            }
            int anzahl;
            if (!int.TryParse(txtAnzahl.Text, out anzahl))
            {
                success = false;
                sb.Append(GetTextControlInvalid(lblAnzahl));
            }

            if (!success)
            {
                MessageBox(sb.ToString());
            }

            return success;
        }


        private void cmdVon_Click(object sender, EventArgs e)
        {
            Windows.Forms.CalendarPickerView dlg = new Windows.Forms.CalendarPickerView();
            if (DialogResult.OK == dlg.ShowDialog())
            {
                this.txtDatumVon.Text = Tools.DBNullableDateTime2DateString(dlg.SelectedDate);
            }
        }

        private void cmdBis_Click(object sender, EventArgs e)
        {
            Windows.Forms.CalendarPickerView dlg = new Windows.Forms.CalendarPickerView();
            if (DialogResult.OK == dlg.ShowDialog())
            {
                this.txtDatumBis.Text = Tools.DBNullableDateTime2DateString(dlg.SelectedDate);
            }
        }

        private void cmdPopulate_Click(object sender, EventArgs e)
        {
            PopulateOperationen();
        }
    }
}