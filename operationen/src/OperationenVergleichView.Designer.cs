using Windows.Forms;

namespace Operationen
{
    partial class OperationenVergleichView : OperationenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OperationenVergleichView));
            this.grpData = new System.Windows.Forms.GroupBox();
            this.lvData = new Windows.Forms.OplListView();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.cmdAbort = new Windows.Forms.OplButton();
            this.cbChirurgenOPFunktionen = new System.Windows.Forms.ComboBox();
            this.lblOPFunktionen = new System.Windows.Forms.Label();
            this.chkExtern = new Windows.Forms.OplCheckBox();
            this.chkIntern = new Windows.Forms.OplCheckBox();
            this.txtDatumBis = new Windows.Forms.DateBoxPicker();
            this.txtDatumVon = new Windows.Forms.DateBoxPicker();
            this.cmdAnzeigen = new Windows.Forms.OplButton();
            this.lblDatumBis = new System.Windows.Forms.Label();
            this.lblDatumVon = new System.Windows.Forms.Label();
            this.txtOperation = new Windows.Forms.OplTextBox();
            this.lblOPS = new System.Windows.Forms.Label();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.cmdPrint = new Windows.Forms.OplButton();
            this.cmdOPSCode = new Windows.Forms.OplButton();
            this.cmdAddOPSCode = new Windows.Forms.OplButton();
            this.lvOperationen = new Windows.Forms.OplListView();
            this.contextMenuColumn = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.entfernenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abbrechenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpOperationen = new System.Windows.Forms.GroupBox();
            this.cmdSaveOpsCodes = new Windows.Forms.OplButton();
            this.lblInfo = new System.Windows.Forms.LinkLabel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grpData.SuspendLayout();
            this.grpFilter.SuspendLayout();
            this.contextMenuColumn.SuspendLayout();
            this.grpOperationen.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpData
            // 
            this.grpData.Controls.Add(this.lvData);
            resources.ApplyResources(this.grpData, "grpData");
            this.grpData.Name = "grpData";
            this.grpData.TabStop = false;
            // 
            // lvData
            // 
            resources.ApplyResources(this.lvData, "lvData");
            this.lvData.FullRowSelect = true;
            this.lvData.Name = "lvData";
            this.lvData.UseCompatibleStateImageBehavior = false;
            this.lvData.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvData_ColumnClick);
            this.lvData.Click += new System.EventHandler(this.lvData_Click);
            // 
            // grpFilter
            // 
            resources.ApplyResources(this.grpFilter, "grpFilter");
            this.grpFilter.Controls.Add(this.cmdAbort);
            this.grpFilter.Controls.Add(this.cbChirurgenOPFunktionen);
            this.grpFilter.Controls.Add(this.lblOPFunktionen);
            this.grpFilter.Controls.Add(this.chkExtern);
            this.grpFilter.Controls.Add(this.chkIntern);
            this.grpFilter.Controls.Add(this.txtDatumBis);
            this.grpFilter.Controls.Add(this.txtDatumVon);
            this.grpFilter.Controls.Add(this.cmdAnzeigen);
            this.grpFilter.Controls.Add(this.lblDatumBis);
            this.grpFilter.Controls.Add(this.lblDatumVon);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.TabStop = false;
            // 
            // cmdAbort
            // 
            resources.ApplyResources(this.cmdAbort, "cmdAbort");
            this.cmdAbort.Name = "cmdAbort";
            this.cmdAbort.UseVisualStyleBackColor = true;
            this.cmdAbort.Click += new System.EventHandler(this.cmdAbort_Click);
            // 
            // cbChirurgenOPFunktionen
            // 
            this.cbChirurgenOPFunktionen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChirurgenOPFunktionen.FormattingEnabled = true;
            resources.ApplyResources(this.cbChirurgenOPFunktionen, "cbChirurgenOPFunktionen");
            this.cbChirurgenOPFunktionen.Name = "cbChirurgenOPFunktionen";
            // 
            // lblOPFunktionen
            // 
            resources.ApplyResources(this.lblOPFunktionen, "lblOPFunktionen");
            this.lblOPFunktionen.Name = "lblOPFunktionen";
            // 
            // chkExtern
            // 
            resources.ApplyResources(this.chkExtern, "chkExtern");
            this.chkExtern.Name = "chkExtern";
            this.chkExtern.UseVisualStyleBackColor = true;
            // 
            // chkIntern
            // 
            resources.ApplyResources(this.chkIntern, "chkIntern");
            this.chkIntern.Name = "chkIntern";
            this.chkIntern.UseVisualStyleBackColor = true;
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
            // cmdAnzeigen
            // 
            resources.ApplyResources(this.cmdAnzeigen, "cmdAnzeigen");
            this.cmdAnzeigen.Name = "cmdAnzeigen";
            this.cmdAnzeigen.UseVisualStyleBackColor = true;
            this.cmdAnzeigen.Click += new System.EventHandler(this.cmdAnzeigen_Click);
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
            // txtOperation
            // 
            resources.ApplyResources(this.txtOperation, "txtOperation");
            this.txtOperation.Name = "txtOperation";
            // 
            // lblOPS
            // 
            resources.ApplyResources(this.lblOPS, "lblOPS");
            this.lblOPS.Name = "lblOPS";
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdPrint
            // 
            resources.ApplyResources(this.cmdPrint, "cmdPrint");
            this.cmdPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // cmdOPSCode
            // 
            resources.ApplyResources(this.cmdOPSCode, "cmdOPSCode");
            this.cmdOPSCode.Name = "cmdOPSCode";
            this.cmdOPSCode.UseVisualStyleBackColor = true;
            this.cmdOPSCode.Click += new System.EventHandler(this.cmdOPSCode_Click);
            // 
            // cmdAddOPSCode
            // 
            resources.ApplyResources(this.cmdAddOPSCode, "cmdAddOPSCode");
            this.cmdAddOPSCode.Name = "cmdAddOPSCode";
            this.cmdAddOPSCode.UseVisualStyleBackColor = true;
            this.cmdAddOPSCode.Click += new System.EventHandler(this.cmdAddOPSCode_Click);
            // 
            // lvOperationen
            // 
            resources.ApplyResources(this.lvOperationen, "lvOperationen");
            this.lvOperationen.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvOperationen.Name = "lvOperationen";
            this.lvOperationen.UseCompatibleStateImageBehavior = false;
            this.lvOperationen.DoubleClick += new System.EventHandler(this.lvOperationen_DoubleClick);
            // 
            // contextMenuColumn
            // 
            this.contextMenuColumn.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.entfernenToolStripMenuItem,
            this.abbrechenToolStripMenuItem});
            this.contextMenuColumn.Name = "contextMenuColumn";
            this.contextMenuColumn.ShowImageMargin = false;
            resources.ApplyResources(this.contextMenuColumn, "contextMenuColumn");
            // 
            // entfernenToolStripMenuItem
            // 
            this.entfernenToolStripMenuItem.Name = "entfernenToolStripMenuItem";
            resources.ApplyResources(this.entfernenToolStripMenuItem, "entfernenToolStripMenuItem");
            this.entfernenToolStripMenuItem.Click += new System.EventHandler(this.entfernenToolStripMenuItem_Click);
            // 
            // abbrechenToolStripMenuItem
            // 
            this.abbrechenToolStripMenuItem.Name = "abbrechenToolStripMenuItem";
            resources.ApplyResources(this.abbrechenToolStripMenuItem, "abbrechenToolStripMenuItem");
            this.abbrechenToolStripMenuItem.Click += new System.EventHandler(this.abbrechenToolStripMenuItem_Click);
            // 
            // grpOperationen
            // 
            this.grpOperationen.Controls.Add(this.cmdAddOPSCode);
            this.grpOperationen.Controls.Add(this.lvOperationen);
            this.grpOperationen.Controls.Add(this.lblOPS);
            this.grpOperationen.Controls.Add(this.cmdOPSCode);
            this.grpOperationen.Controls.Add(this.txtOperation);
            resources.ApplyResources(this.grpOperationen, "grpOperationen");
            this.grpOperationen.Name = "grpOperationen";
            this.grpOperationen.TabStop = false;
            // 
            // cmdSaveOpsCodes
            // 
            resources.ApplyResources(this.cmdSaveOpsCodes, "cmdSaveOpsCodes");
            this.cmdSaveOpsCodes.Name = "cmdSaveOpsCodes";
            this.cmdSaveOpsCodes.UseVisualStyleBackColor = true;
            this.cmdSaveOpsCodes.Click += new System.EventHandler(this.cmdSaveOpsCodes_Click);
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.TabStop = true;
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grpOperationen);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpData);
            // 
            // OperationenVergleichView
            // 
            this.AcceptButton = this.cmdAnzeigen;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.grpFilter);
            this.Controls.Add(this.cmdSaveOpsCodes);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdPrint);
            this.Name = "OperationenVergleichView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.OperationenVergleichView_Load);
            this.grpData.ResumeLayout(false);
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            this.contextMenuColumn.ResumeLayout(false);
            this.grpOperationen.ResumeLayout(false);
            this.grpOperationen.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpData;
        private Windows.Forms.OplButton cmdCancel;
        private System.Windows.Forms.GroupBox grpFilter;
        private Windows.Forms.OplListView lvData;
        private Windows.Forms.OplButton cmdAnzeigen;
        private System.Windows.Forms.Label lblDatumBis;
        private System.Windows.Forms.Label lblDatumVon;
        private DateBoxPicker txtDatumBis;
        private DateBoxPicker txtDatumVon;
        private Windows.Forms.OplTextBox txtOperation;
        private System.Windows.Forms.Label lblOPS;
        private Windows.Forms.OplCheckBox chkExtern;
        private Windows.Forms.OplCheckBox chkIntern;
        private Windows.Forms.OplButton cmdPrint;
        private System.Windows.Forms.ComboBox cbChirurgenOPFunktionen;
        private System.Windows.Forms.Label lblOPFunktionen;
        private Windows.Forms.OplButton cmdOPSCode;
        private Windows.Forms.OplButton cmdAddOPSCode;
        private Windows.Forms.OplListView lvOperationen;
        private System.Windows.Forms.ContextMenuStrip contextMenuColumn;
        private System.Windows.Forms.ToolStripMenuItem entfernenToolStripMenuItem;
        private System.Windows.Forms.GroupBox grpOperationen;
        private System.Windows.Forms.ToolStripMenuItem abbrechenToolStripMenuItem;
        private Windows.Forms.OplButton cmdSaveOpsCodes;
        private System.Windows.Forms.LinkLabel lblInfo;
        private Windows.Forms.OplButton cmdAbort;
        private System.Windows.Forms.SplitContainer splitContainer;
    }
}