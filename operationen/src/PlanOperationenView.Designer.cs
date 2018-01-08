namespace Operationen
{
    partial class PlanOperationenView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlanOperationenView));
            this.grpPlanOperationen = new System.Windows.Forms.GroupBox();
            this.cmdEditPlanOperation = new Windows.Forms.OplButton();
            this.cmdPrintPlanOperationen = new Windows.Forms.OplButton();
            this.cmdNewPlanOperation = new Windows.Forms.OplButton();
            this.cmdDeletePlanOperation = new Windows.Forms.OplButton();
            this.lvPlanOperationen = new Windows.Forms.OplListView();
            this.grpTest = new System.Windows.Forms.GroupBox();
            this.cbChirurgenOPFunktionen = new System.Windows.Forms.ComboBox();
            this.lblOPFunktionen = new System.Windows.Forms.Label();
            this.chkExtern = new Windows.Forms.OplCheckBox();
            this.chkIntern = new Windows.Forms.OplCheckBox();
            this.txtDatumBis = new Windows.Forms.DateBoxPicker();
            this.txtDatumVon = new Windows.Forms.DateBoxPicker();
            this.cmdIstSoll = new Windows.Forms.OplButton();
            this.lblDatumBis = new System.Windows.Forms.Label();
            this.lblDatumVon = new System.Windows.Forms.Label();
            this.lvTest = new Windows.Forms.ListViewBalken();
            this.label2 = new System.Windows.Forms.Label();
            this.cbChirurgen = new System.Windows.Forms.ComboBox();
            this.cmdClose = new Windows.Forms.OplButton();
            this.lblInfo = new Windows.Forms.OplLinkLabel();
            this.cmdPrintTest = new Windows.Forms.OplButton();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grpPlanOperationen.SuspendLayout();
            this.grpTest.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpPlanOperationen
            // 
            this.grpPlanOperationen.Controls.Add(this.cmdEditPlanOperation);
            this.grpPlanOperationen.Controls.Add(this.cmdPrintPlanOperationen);
            this.grpPlanOperationen.Controls.Add(this.cmdNewPlanOperation);
            this.grpPlanOperationen.Controls.Add(this.cmdDeletePlanOperation);
            this.grpPlanOperationen.Controls.Add(this.lvPlanOperationen);
            resources.ApplyResources(this.grpPlanOperationen, "grpPlanOperationen");
            this.grpPlanOperationen.Name = "grpPlanOperationen";
            this.grpPlanOperationen.TabStop = false;
            // 
            // cmdEditPlanOperation
            // 
            resources.ApplyResources(this.cmdEditPlanOperation, "cmdEditPlanOperation");
            this.cmdEditPlanOperation.Name = "cmdEditPlanOperation";
            this.cmdEditPlanOperation.SecurityManager = null;
            this.cmdEditPlanOperation.UserRight = null;
            this.cmdEditPlanOperation.UseVisualStyleBackColor = true;
            this.cmdEditPlanOperation.Click += new System.EventHandler(this.cmdEditPlanOperation_Click);
            // 
            // cmdPrintPlanOperationen
            // 
            resources.ApplyResources(this.cmdPrintPlanOperationen, "cmdPrintPlanOperationen");
            this.cmdPrintPlanOperationen.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdPrintPlanOperationen.Name = "cmdPrintPlanOperationen";
            this.cmdPrintPlanOperationen.SecurityManager = null;
            this.cmdPrintPlanOperationen.UserRight = null;
            this.cmdPrintPlanOperationen.UseVisualStyleBackColor = true;
            this.cmdPrintPlanOperationen.Click += new System.EventHandler(this.cmdPrintPlanOperationen_Click);
            // 
            // cmdNewPlanOperation
            // 
            resources.ApplyResources(this.cmdNewPlanOperation, "cmdNewPlanOperation");
            this.cmdNewPlanOperation.Name = "cmdNewPlanOperation";
            this.cmdNewPlanOperation.SecurityManager = null;
            this.cmdNewPlanOperation.UserRight = null;
            this.cmdNewPlanOperation.UseVisualStyleBackColor = true;
            this.cmdNewPlanOperation.Click += new System.EventHandler(this.cmdNewPlanOperation_Click);
            // 
            // cmdDeletePlanOperation
            // 
            resources.ApplyResources(this.cmdDeletePlanOperation, "cmdDeletePlanOperation");
            this.cmdDeletePlanOperation.Name = "cmdDeletePlanOperation";
            this.cmdDeletePlanOperation.SecurityManager = null;
            this.cmdDeletePlanOperation.UserRight = null;
            this.cmdDeletePlanOperation.UseVisualStyleBackColor = true;
            this.cmdDeletePlanOperation.Click += new System.EventHandler(this.cmdDeletePlanOperation_Click);
            // 
            // lvPlanOperationen
            // 
            resources.ApplyResources(this.lvPlanOperationen, "lvPlanOperationen");
            this.lvPlanOperationen.DoubleClickActivation = false;
            this.lvPlanOperationen.Name = "lvPlanOperationen";
            this.lvPlanOperationen.UseCompatibleStateImageBehavior = false;
            this.lvPlanOperationen.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvPlanOperationen_MouseDoubleClick);
            // 
            // grpTest
            // 
            this.grpTest.Controls.Add(this.cbChirurgenOPFunktionen);
            this.grpTest.Controls.Add(this.lblOPFunktionen);
            this.grpTest.Controls.Add(this.chkExtern);
            this.grpTest.Controls.Add(this.chkIntern);
            this.grpTest.Controls.Add(this.txtDatumBis);
            this.grpTest.Controls.Add(this.txtDatumVon);
            this.grpTest.Controls.Add(this.cmdIstSoll);
            this.grpTest.Controls.Add(this.lblDatumBis);
            this.grpTest.Controls.Add(this.lblDatumVon);
            this.grpTest.Controls.Add(this.lvTest);
            resources.ApplyResources(this.grpTest, "grpTest");
            this.grpTest.Name = "grpTest";
            this.grpTest.TabStop = false;
            // 
            // cbChirurgenOPFunktionen
            // 
            resources.ApplyResources(this.cbChirurgenOPFunktionen, "cbChirurgenOPFunktionen");
            this.cbChirurgenOPFunktionen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChirurgenOPFunktionen.FormattingEnabled = true;
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
            // cmdIstSoll
            // 
            resources.ApplyResources(this.cmdIstSoll, "cmdIstSoll");
            this.cmdIstSoll.Name = "cmdIstSoll";
            this.cmdIstSoll.SecurityManager = null;
            this.cmdIstSoll.UserRight = null;
            this.cmdIstSoll.UseVisualStyleBackColor = true;
            this.cmdIstSoll.Click += new System.EventHandler(this.cmdIstSoll_Click);
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
            // lvTest
            // 
            resources.ApplyResources(this.lvTest, "lvTest");
            this.lvTest.DoubleClickActivation = false;
            this.lvTest.FullRowSelect = true;
            this.lvTest.MultiSelect = false;
            this.lvTest.Name = "lvTest";
            this.lvTest.OwnerDraw = true;
            this.lvTest.UseCompatibleStateImageBehavior = false;
            this.lvTest.View = System.Windows.Forms.View.Details;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cbChirurgen
            // 
            this.cbChirurgen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbChirurgen, "cbChirurgen");
            this.cbChirurgen.Name = "cbChirurgen";
            this.cbChirurgen.SelectedIndexChanged += new System.EventHandler(this.cbChirurgen_SelectedIndexChanged);
            // 
            // cmdClose
            // 
            resources.ApplyResources(this.cmdClose, "cmdClose");
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.SecurityManager = null;
            this.cmdClose.UserRight = null;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.TabStop = true;
            this.lblInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblInfo_LinkClicked);
            // 
            // cmdPrintTest
            // 
            resources.ApplyResources(this.cmdPrintTest, "cmdPrintTest");
            this.cmdPrintTest.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdPrintTest.Name = "cmdPrintTest";
            this.cmdPrintTest.SecurityManager = null;
            this.cmdPrintTest.UserRight = null;
            this.cmdPrintTest.UseVisualStyleBackColor = true;
            this.cmdPrintTest.Click += new System.EventHandler(this.cmdPrintTest_Click);
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grpPlanOperationen);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpTest);
            // 
            // PlanOperationenView
            // 
            this.AcceptButton = this.cmdIstSoll;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.Controls.Add(this.cbChirurgen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.cmdPrintTest);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.cmdClose);
            this.Name = "PlanOperationenView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.PlanOperationen_Load);
            this.grpPlanOperationen.ResumeLayout(false);
            this.grpTest.ResumeLayout(false);
            this.grpTest.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPlanOperationen;
        private Windows.Forms.OplButton cmdNewPlanOperation;
        private Windows.Forms.OplButton cmdDeletePlanOperation;
        private Windows.Forms.OplListView lvPlanOperationen;
        private System.Windows.Forms.GroupBox grpTest;
        private Windows.Forms.OplButton cmdIstSoll;
        private System.Windows.Forms.Label lblDatumBis;
        private System.Windows.Forms.Label lblDatumVon;
        private Windows.Forms.ListViewBalken lvTest;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbChirurgen;
        private Windows.Forms.OplButton cmdClose;
        private Windows.Forms.DateBoxPicker txtDatumBis;
        private Windows.Forms.DateBoxPicker txtDatumVon;
        private Windows.Forms.OplLinkLabel lblInfo;
        private Windows.Forms.OplCheckBox chkExtern;
        private Windows.Forms.OplCheckBox chkIntern;
        private Windows.Forms.OplButton cmdPrintTest;
        private Windows.Forms.OplButton cmdPrintPlanOperationen;
        private Windows.Forms.OplButton cmdEditPlanOperation;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ComboBox cbChirurgenOPFunktionen;
        private System.Windows.Forms.Label lblOPFunktionen;
    }
}