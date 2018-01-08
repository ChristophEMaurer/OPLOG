namespace Operationen.Weiterbildungzeitraum
{
    partial class WeiterbildungszeitraumView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WeiterbildungszeitraumView));
            this.cmdAddSeparator = new Windows.Forms.OplButton();
            this.chkSnap = new Windows.Forms.OplCheckBox();
            this.cmdDeleteAll = new Windows.Forms.OplButton();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.grpGrid = new System.Windows.Forms.GroupBox();
            this.txtDateTo = new Windows.Forms.DateBoxPicker();
            this.txtDateFrom = new Windows.Forms.DateBoxPicker();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.lvGrid = new Windows.Forms.OplListView();
            this.grpDates = new System.Windows.Forms.GroupBox();
            this.cmdSave = new Windows.Forms.OplButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblChirurg = new System.Windows.Forms.Label();
            this.cbChirurgen = new System.Windows.Forms.ComboBox();
            this.lblInfo = new System.Windows.Forms.LinkLabel();
            this.grpGrid.SuspendLayout();
            this.grpDates.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdAddSeparator
            // 
            this.cmdAddSeparator.AccessibleDescription = null;
            this.cmdAddSeparator.AccessibleName = null;
            resources.ApplyResources(this.cmdAddSeparator, "cmdAddSeparator");
            this.cmdAddSeparator.BackgroundImage = null;
            this.cmdAddSeparator.Font = null;
            this.cmdAddSeparator.Name = "cmdAddSeparator";
            this.cmdAddSeparator.SecurityManager = null;
            this.cmdAddSeparator.UserRight = null;
            this.cmdAddSeparator.UseVisualStyleBackColor = true;
            this.cmdAddSeparator.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // chkSnap
            // 
            this.chkSnap.AccessibleDescription = null;
            this.chkSnap.AccessibleName = null;
            resources.ApplyResources(this.chkSnap, "chkSnap");
            this.chkSnap.BackgroundImage = null;
            this.chkSnap.Checked = true;
            this.chkSnap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSnap.Font = null;
            this.chkSnap.Name = "chkSnap";
            this.chkSnap.UseVisualStyleBackColor = true;
            // 
            // cmdDeleteAll
            // 
            this.cmdDeleteAll.AccessibleDescription = null;
            this.cmdDeleteAll.AccessibleName = null;
            resources.ApplyResources(this.cmdDeleteAll, "cmdDeleteAll");
            this.cmdDeleteAll.BackgroundImage = null;
            this.cmdDeleteAll.Font = null;
            this.cmdDeleteAll.Name = "cmdDeleteAll";
            this.cmdDeleteAll.SecurityManager = null;
            this.cmdDeleteAll.UserRight = null;
            this.cmdDeleteAll.UseVisualStyleBackColor = true;
            this.cmdDeleteAll.Click += new System.EventHandler(this.cmdDeleteAll_Click);
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.AccessibleDescription = null;
            this.lblDateFrom.AccessibleName = null;
            resources.ApplyResources(this.lblDateFrom, "lblDateFrom");
            this.lblDateFrom.Font = null;
            this.lblDateFrom.Name = "lblDateFrom";
            // 
            // lblDateTo
            // 
            this.lblDateTo.AccessibleDescription = null;
            this.lblDateTo.AccessibleName = null;
            resources.ApplyResources(this.lblDateTo, "lblDateTo");
            this.lblDateTo.Font = null;
            this.lblDateTo.Name = "lblDateTo";
            // 
            // grpGrid
            // 
            this.grpGrid.AccessibleDescription = null;
            this.grpGrid.AccessibleName = null;
            resources.ApplyResources(this.grpGrid, "grpGrid");
            this.grpGrid.BackgroundImage = null;
            this.grpGrid.Controls.Add(this.chkSnap);
            this.grpGrid.Controls.Add(this.txtDateTo);
            this.grpGrid.Controls.Add(this.cmdDeleteAll);
            this.grpGrid.Controls.Add(this.txtDateFrom);
            this.grpGrid.Controls.Add(this.cmdAddSeparator);
            this.grpGrid.Controls.Add(this.lblDateFrom);
            this.grpGrid.Controls.Add(this.pnlGrid);
            this.grpGrid.Controls.Add(this.lblDateTo);
            this.grpGrid.Font = null;
            this.grpGrid.Name = "grpGrid";
            this.grpGrid.TabStop = false;
            // 
            // txtDateTo
            // 
            this.txtDateTo.AccessibleDescription = null;
            this.txtDateTo.AccessibleName = null;
            resources.ApplyResources(this.txtDateTo, "txtDateTo");
            this.txtDateTo.BackgroundImage = null;
            this.txtDateTo.Font = null;
            this.txtDateTo.Name = "txtDateTo";
            this.txtDateTo.Leave += new System.EventHandler(this.txtDateTo_Leave);
            this.txtDateTo.DateChanged += new Windows.Forms.DateBoxPicker.DateChangedCallback(this.txtDateTo_DateChanged);
            // 
            // txtDateFrom
            // 
            this.txtDateFrom.AccessibleDescription = null;
            this.txtDateFrom.AccessibleName = null;
            resources.ApplyResources(this.txtDateFrom, "txtDateFrom");
            this.txtDateFrom.BackgroundImage = null;
            this.txtDateFrom.Font = null;
            this.txtDateFrom.Name = "txtDateFrom";
            this.txtDateFrom.Leave += new System.EventHandler(this.txtDateFrom_Leave);
            this.txtDateFrom.DateChanged += new Windows.Forms.DateBoxPicker.DateChangedCallback(this.txtDateFrom_DateChanged);
            // 
            // pnlGrid
            // 
            this.pnlGrid.AccessibleDescription = null;
            this.pnlGrid.AccessibleName = null;
            resources.ApplyResources(this.pnlGrid, "pnlGrid");
            this.pnlGrid.BackgroundImage = null;
            this.pnlGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlGrid.Font = null;
            this.pnlGrid.Name = "pnlGrid";
            // 
            // cmdCancel
            // 
            this.cmdCancel.AccessibleDescription = null;
            this.cmdCancel.AccessibleName = null;
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.BackgroundImage = null;
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Font = null;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.SecurityManager = null;
            this.cmdCancel.UserRight = null;
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // lvGrid
            // 
            this.lvGrid.AccessibleDescription = null;
            this.lvGrid.AccessibleName = null;
            resources.ApplyResources(this.lvGrid, "lvGrid");
            this.lvGrid.BackgroundImage = null;
            this.lvGrid.DoubleClickActivation = false;
            this.lvGrid.Font = null;
            this.lvGrid.Name = "lvGrid";
            this.lvGrid.UseCompatibleStateImageBehavior = false;
            // 
            // grpDates
            // 
            this.grpDates.AccessibleDescription = null;
            this.grpDates.AccessibleName = null;
            resources.ApplyResources(this.grpDates, "grpDates");
            this.grpDates.BackgroundImage = null;
            this.grpDates.Controls.Add(this.lvGrid);
            this.grpDates.Font = null;
            this.grpDates.Name = "grpDates";
            this.grpDates.TabStop = false;
            // 
            // cmdSave
            // 
            this.cmdSave.AccessibleDescription = null;
            this.cmdSave.AccessibleName = null;
            resources.ApplyResources(this.cmdSave, "cmdSave");
            this.cmdSave.BackgroundImage = null;
            this.cmdSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdSave.Font = null;
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.SecurityManager = null;
            this.cmdSave.UserRight = null;
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = null;
            this.groupBox1.AccessibleName = null;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.BackgroundImage = null;
            this.groupBox1.Controls.Add(this.lblChirurg);
            this.groupBox1.Controls.Add(this.cbChirurgen);
            this.groupBox1.Font = null;
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // lblChirurg
            // 
            this.lblChirurg.AccessibleDescription = null;
            this.lblChirurg.AccessibleName = null;
            resources.ApplyResources(this.lblChirurg, "lblChirurg");
            this.lblChirurg.Font = null;
            this.lblChirurg.Name = "lblChirurg";
            // 
            // cbChirurgen
            // 
            this.cbChirurgen.AccessibleDescription = null;
            this.cbChirurgen.AccessibleName = null;
            resources.ApplyResources(this.cbChirurgen, "cbChirurgen");
            this.cbChirurgen.BackgroundImage = null;
            this.cbChirurgen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChirurgen.Font = null;
            this.cbChirurgen.Name = "cbChirurgen";
            this.cbChirurgen.SelectedIndexChanged += new System.EventHandler(this.cbChirurgen_SelectedIndexChanged);
            // 
            // lblInfo
            // 
            this.lblInfo.AccessibleDescription = null;
            this.lblInfo.AccessibleName = null;
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Font = null;
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.TabStop = true;
            this.lblInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblInfo_LinkClicked);
            // 
            // WeiterbildungszeitraumView
            // 
            this.AcceptButton = this.cmdAddSeparator;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.grpDates);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.grpGrid);
            this.Font = null;
            this.Icon = null;
            this.Name = "WeiterbildungszeitraumView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.WeiterbildungszeitraumView_Load);
            this.Resize += new System.EventHandler(this.WeiterbildungszeitraumView_Resize);
            this.ResizeEnd += new System.EventHandler(this.WeiterbildungszeitraumView_ResizeEnd);
            this.grpGrid.ResumeLayout(false);
            this.grpGrid.PerformLayout();
            this.grpDates.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.OplButton cmdAddSeparator;
        private Windows.Forms.OplCheckBox chkSnap;
        private System.Windows.Forms.Panel pnlGrid;
        private Windows.Forms.OplButton cmdDeleteAll;
        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.GroupBox grpGrid;
        private Windows.Forms.OplButton cmdCancel;
        private Windows.Forms.OplListView lvGrid;
        private System.Windows.Forms.GroupBox grpDates;
        private Windows.Forms.OplButton cmdSave;
        private Windows.Forms.DateBoxPicker txtDateTo;
        private Windows.Forms.DateBoxPicker txtDateFrom;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblChirurg;
        private System.Windows.Forms.ComboBox cbChirurgen;
        private System.Windows.Forms.LinkLabel lblInfo;
    }
}