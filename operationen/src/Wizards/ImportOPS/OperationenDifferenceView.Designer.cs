namespace Operationen.Wizards.ImportOPS
{
    partial class OperationenDifferenceView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OperationenDifferenceView));
            this.cmdCancel = new Windows.Forms.OplButton();
            this.grpDatabase = new System.Windows.Forms.GroupBox();
            this.lvDatabase = new Windows.Forms.OplListView();
            this.grpImport = new System.Windows.Forms.GroupBox();
            this.lvImport = new Windows.Forms.OplListView();
            this.cmdUpdateAll = new Windows.Forms.OplButton();
            this.cmdUpdateNew = new Windows.Forms.OplButton();
            this.cmdUpdateDifferent = new Windows.Forms.OplButton();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblDifferent = new System.Windows.Forms.Label();
            this.lblMissingInDatabase = new System.Windows.Forms.Label();
            this.lblMissingInImport = new System.Windows.Forms.Label();
            this.lblExists = new System.Windows.Forms.Label();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.lblInfo2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grpDatabase.SuspendLayout();
            this.grpImport.SuspendLayout();
            this.grpInfo.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // grpDatabase
            // 
            this.grpDatabase.Controls.Add(this.lvDatabase);
            resources.ApplyResources(this.grpDatabase, "grpDatabase");
            this.grpDatabase.Name = "grpDatabase";
            this.grpDatabase.TabStop = false;
            // 
            // lvDatabase
            // 
            resources.ApplyResources(this.lvDatabase, "lvDatabase");
            this.lvDatabase.FullRowSelect = true;
            this.lvDatabase.Name = "lvDatabase";
            this.lvDatabase.UseCompatibleStateImageBehavior = false;
            this.lvDatabase.DoubleClick += new System.EventHandler(this.lvDatabase_DoubleClick);
            // 
            // grpImport
            // 
            this.grpImport.Controls.Add(this.lvImport);
            resources.ApplyResources(this.grpImport, "grpImport");
            this.grpImport.Name = "grpImport";
            this.grpImport.TabStop = false;
            // 
            // lvImport
            // 
            resources.ApplyResources(this.lvImport, "lvImport");
            this.lvImport.FullRowSelect = true;
            this.lvImport.Name = "lvImport";
            this.lvImport.UseCompatibleStateImageBehavior = false;
            this.lvImport.DoubleClick += new System.EventHandler(this.lvImport_DoubleClick);
            // 
            // cmdUpdateAll
            // 
            resources.ApplyResources(this.cmdUpdateAll, "cmdUpdateAll");
            this.cmdUpdateAll.Name = "cmdUpdateAll";
            this.cmdUpdateAll.UseVisualStyleBackColor = true;
            this.cmdUpdateAll.Click += new System.EventHandler(this.cmdUpdateAll_Click);
            // 
            // cmdUpdateNew
            // 
            resources.ApplyResources(this.cmdUpdateNew, "cmdUpdateNew");
            this.cmdUpdateNew.Name = "cmdUpdateNew";
            this.cmdUpdateNew.UseVisualStyleBackColor = true;
            this.cmdUpdateNew.Click += new System.EventHandler(this.cmdUpdateNew_Click);
            // 
            // cmdUpdateDifferent
            // 
            resources.ApplyResources(this.cmdUpdateDifferent, "cmdUpdateDifferent");
            this.cmdUpdateDifferent.Name = "cmdUpdateDifferent";
            this.cmdUpdateDifferent.UseVisualStyleBackColor = true;
            this.cmdUpdateDifferent.Click += new System.EventHandler(this.cmdUpdateDifferent_Click);
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            // 
            // lblProgress
            // 
            resources.ApplyResources(this.lblProgress, "lblProgress");
            this.lblProgress.Name = "lblProgress";
            // 
            // lblInfo
            // 
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.Name = "lblInfo";
            // 
            // lblDifferent
            // 
            resources.ApplyResources(this.lblDifferent, "lblDifferent");
            this.lblDifferent.Name = "lblDifferent";
            // 
            // lblMissingInDatabase
            // 
            resources.ApplyResources(this.lblMissingInDatabase, "lblMissingInDatabase");
            this.lblMissingInDatabase.Name = "lblMissingInDatabase";
            // 
            // lblMissingInImport
            // 
            resources.ApplyResources(this.lblMissingInImport, "lblMissingInImport");
            this.lblMissingInImport.Name = "lblMissingInImport";
            // 
            // lblExists
            // 
            resources.ApplyResources(this.lblExists, "lblExists");
            this.lblExists.Name = "lblExists";
            // 
            // grpInfo
            // 
            resources.ApplyResources(this.grpInfo, "grpInfo");
            this.grpInfo.Controls.Add(this.lblMissingInDatabase);
            this.grpInfo.Controls.Add(this.lblInfo);
            this.grpInfo.Controls.Add(this.lblExists);
            this.grpInfo.Controls.Add(this.lblDifferent);
            this.grpInfo.Controls.Add(this.lblMissingInImport);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.TabStop = false;
            // 
            // lblInfo2
            // 
            resources.ApplyResources(this.lblInfo2, "lblInfo2");
            this.lblInfo2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInfo2.Name = "lblInfo2";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.progressBar);
            this.groupBox1.Controls.Add(this.lblInfo2);
            this.groupBox1.Controls.Add(this.lblProgress);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grpDatabase);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpImport);
            // 
            // OperationenDifferenceView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.cmdUpdateAll);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.cmdUpdateNew);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdUpdateDifferent);
            this.Controls.Add(this.grpInfo);
            this.Controls.Add(this.cmdCancel);
            this.MaximizeBox = false;
            this.Name = "OperationenDifferenceView";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.OperationenDifferenceView_Load);
            this.Shown += new System.EventHandler(this.OperationenDifferenceView_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OperationenDifferenceView_FormClosing);
            this.grpDatabase.ResumeLayout(false);
            this.grpImport.ResumeLayout(false);
            this.grpInfo.ResumeLayout(false);
            this.grpInfo.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.OplButton cmdCancel;
        private System.Windows.Forms.GroupBox grpDatabase;
        private Windows.Forms.OplListView lvDatabase;
        private System.Windows.Forms.GroupBox grpImport;
        private Windows.Forms.OplListView lvImport;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblDifferent;
        private System.Windows.Forms.Label lblMissingInDatabase;
        private System.Windows.Forms.Label lblMissingInImport;
        private System.Windows.Forms.Label lblExists;
        private System.Windows.Forms.GroupBox grpInfo;
        private Windows.Forms.OplButton cmdUpdateDifferent;
        private Windows.Forms.OplButton cmdUpdateNew;
        private Windows.Forms.OplButton cmdUpdateAll;
        private System.Windows.Forms.Label lblInfo2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer;
    }
}