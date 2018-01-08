using Windows.Forms;

namespace Operationen.Wizards.ImportOperationenMobile
{
    partial class NewOperationenMobileView : OperationenForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewOperationenMobileView));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdCancel = new Windows.Forms.OplButton();
            this.cmdOK = new Windows.Forms.OplButton();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.txtDateLatest = new Windows.Forms.OplTextBox();
            this.cmdDisplay = new Windows.Forms.OplButton();
            this.label1 = new System.Windows.Forms.Label();
            this.radDateRange = new Windows.Forms.OplRadioButton();
            this.radAll = new Windows.Forms.OplRadioButton();
            this.txtDatumBis = new Windows.Forms.DateBoxPicker();
            this.txtDatumVon = new Windows.Forms.DateBoxPicker();
            this.lblDatumBis = new System.Windows.Forms.Label();
            this.lblDatumVon = new System.Windows.Forms.Label();
            this.grpOperationen = new System.Windows.Forms.GroupBox();
            this.lvOperationen = new Windows.Forms.OplListView();
            this.lblInfo = new System.Windows.Forms.Label();
            this.chkForce = new Windows.Forms.OplCheckBox();
            this.grpFilter.SuspendLayout();
            this.grpOperationen.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 200;
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 200;
            this.toolTip1.ReshowDelay = 40;
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // grpFilter
            // 
            this.grpFilter.Controls.Add(this.txtDateLatest);
            this.grpFilter.Controls.Add(this.cmdDisplay);
            this.grpFilter.Controls.Add(this.label1);
            this.grpFilter.Controls.Add(this.radDateRange);
            this.grpFilter.Controls.Add(this.radAll);
            this.grpFilter.Controls.Add(this.txtDatumBis);
            this.grpFilter.Controls.Add(this.txtDatumVon);
            this.grpFilter.Controls.Add(this.lblDatumBis);
            this.grpFilter.Controls.Add(this.lblDatumVon);
            resources.ApplyResources(this.grpFilter, "grpFilter");
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.TabStop = false;
            // 
            // txtDateLatest
            // 
            resources.ApplyResources(this.txtDateLatest, "txtDateLatest");
            this.txtDateLatest.Name = "txtDateLatest";
            this.txtDateLatest.ReadOnly = true;
            // 
            // cmdDisplay
            // 
            resources.ApplyResources(this.cmdDisplay, "cmdDisplay");
            this.cmdDisplay.Name = "cmdDisplay";
            this.cmdDisplay.UseVisualStyleBackColor = true;
            this.cmdDisplay.Click += new System.EventHandler(this.cmdDisplay_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // radDateRange
            // 
            resources.ApplyResources(this.radDateRange, "radDateRange");
            this.radDateRange.Name = "radDateRange";
            this.radDateRange.TabStop = true;
            this.radDateRange.UseVisualStyleBackColor = true;
            this.radDateRange.CheckedChanged += new System.EventHandler(this.radDateRange_CheckedChanged);
            // 
            // radAll
            // 
            resources.ApplyResources(this.radAll, "radAll");
            this.radAll.Name = "radAll";
            this.radAll.TabStop = true;
            this.radAll.UseVisualStyleBackColor = true;
            this.radAll.CheckedChanged += new System.EventHandler(this.radAll_CheckedChanged);
            // 
            // txtDatumBis
            // 
            resources.ApplyResources(this.txtDatumBis, "txtDatumBis");
            this.txtDatumBis.Name = "txtDatumBis";
            // 
            // txtDatumVon
            // 
            resources.ApplyResources(this.txtDatumVon, "txtDatumVon");
            this.txtDatumVon.Name = "txtDatumVon";
            // 
            // lblDatumBis
            // 
            resources.ApplyResources(this.lblDatumBis, "lblDatumBis");
            this.lblDatumBis.Name = "lblDatumBis";
            // 
            // lblDatumVon
            // 
            resources.ApplyResources(this.lblDatumVon, "lblDatumVon");
            this.lblDatumVon.Name = "lblDatumVon";
            // 
            // grpOperationen
            // 
            this.grpOperationen.Controls.Add(this.lvOperationen);
            resources.ApplyResources(this.grpOperationen, "grpOperationen");
            this.grpOperationen.Name = "grpOperationen";
            this.grpOperationen.TabStop = false;
            // 
            // lvOperationen
            // 
            this.lvOperationen.FullRowSelect = true;
            resources.ApplyResources(this.lvOperationen, "lvOperationen");
            this.lvOperationen.Name = "lvOperationen";
            this.lvOperationen.UseCompatibleStateImageBehavior = false;
            // 
            // lblInfo
            // 
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.Name = "lblInfo";
            // 
            // chkForce
            // 
            resources.ApplyResources(this.chkForce, "chkForce");
            this.chkForce.Name = "chkForce";
            this.chkForce.UseVisualStyleBackColor = true;
            // 
            // NewOperationenView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.chkForce);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.grpFilter);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.grpOperationen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "NewOperationenMobileView";
            this.Load += new System.EventHandler(this.NewOperationenMobileView_Load);
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            this.grpOperationen.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpOperationen;
        private Windows.Forms.OplButton cmdCancel;
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.ToolTip toolTip1;
        private Windows.Forms.OplListView lvOperationen;
        private System.Windows.Forms.Label lblDatumBis;
        private System.Windows.Forms.Label lblDatumVon;
        private DateBoxPicker txtDatumBis;
        private DateBoxPicker txtDatumVon;
        private Windows.Forms.OplRadioButton radDateRange;
        private Windows.Forms.OplRadioButton radAll;
        private Windows.Forms.OplButton cmdDisplay;
        private Windows.Forms.OplTextBox txtDateLatest;
        private System.Windows.Forms.Label label1;
        private Windows.Forms.OplButton cmdOK;
        private System.Windows.Forms.Label lblInfo;
        private Windows.Forms.OplCheckBox chkForce;
    }
}