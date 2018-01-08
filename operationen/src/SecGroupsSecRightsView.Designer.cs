namespace Operationen
{
    partial class SecGroupsSecRightsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SecGroupsSecRightsView));
            this.lvSecGroupsSecRights = new Windows.Forms.OplListView();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.lvSecGroups = new Windows.Forms.OplListView();
            this.cmdRemove = new Windows.Forms.OplButton();
            this.cmdAdd = new Windows.Forms.OplButton();
            this.grpBenutzergruppen = new System.Windows.Forms.GroupBox();
            this.llSecGroups = new System.Windows.Forms.LinkLabel();
            this.grpRechte = new System.Windows.Forms.GroupBox();
            this.lvSecRights = new Windows.Forms.OplListView();
            this.lblInfo = new System.Windows.Forms.Label();
            this.cmdRefresh = new Windows.Forms.OplButton();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grpBenutzergruppen.SuspendLayout();
            this.grpRechte.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvSecGroupsSecRights
            // 
            this.lvSecGroupsSecRights.BalkenGrafik = false;
            resources.ApplyResources(this.lvSecGroupsSecRights, "lvSecGroupsSecRights");
            this.lvSecGroupsSecRights.MultiSelect = false;
            this.lvSecGroupsSecRights.Name = "lvSecGroupsSecRights";
            this.lvSecGroupsSecRights.Sortable = false;
            this.lvSecGroupsSecRights.UseCompatibleStateImageBehavior = false;
            this.lvSecGroupsSecRights.View = System.Windows.Forms.View.Details;
            this.lvSecGroupsSecRights.DoubleClick += new System.EventHandler(this.lvSecGroupsSecRights_DoubleClick);
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // lvSecGroups
            // 
            this.lvSecGroups.BalkenGrafik = false;
            resources.ApplyResources(this.lvSecGroups, "lvSecGroups");
            this.lvSecGroups.FullRowSelect = true;
            this.lvSecGroups.HideSelection = false;
            this.lvSecGroups.MultiSelect = false;
            this.lvSecGroups.Name = "lvSecGroups";
            this.lvSecGroups.Sortable = false;
            this.lvSecGroups.UseCompatibleStateImageBehavior = false;
            this.lvSecGroups.View = System.Windows.Forms.View.Details;
            this.lvSecGroups.SelectedIndexChanged += new System.EventHandler(this.lvSecGroups_SelectedIndexChanged);
            // 
            // cmdRemove
            // 
            resources.ApplyResources(this.cmdRemove, "cmdRemove");
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.UseVisualStyleBackColor = true;
            this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
            // 
            // cmdAdd
            // 
            resources.ApplyResources(this.cmdAdd, "cmdAdd");
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // grpBenutzergruppen
            // 
            this.grpBenutzergruppen.Controls.Add(this.lvSecGroups);
            resources.ApplyResources(this.grpBenutzergruppen, "grpBenutzergruppen");
            this.grpBenutzergruppen.Name = "grpBenutzergruppen";
            this.grpBenutzergruppen.TabStop = false;
            // 
            // llSecGroups
            // 
            resources.ApplyResources(this.llSecGroups, "llSecGroups");
            this.llSecGroups.Name = "llSecGroups";
            this.llSecGroups.TabStop = true;
            this.llSecGroups.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSecGroups_LinkClicked);
            // 
            // grpRechte
            // 
            this.grpRechte.Controls.Add(this.lvSecRights);
            this.grpRechte.Controls.Add(this.cmdAdd);
            resources.ApplyResources(this.grpRechte, "grpRechte");
            this.grpRechte.Name = "grpRechte";
            this.grpRechte.TabStop = false;
            // 
            // lvSecRights
            // 
            resources.ApplyResources(this.lvSecRights, "lvSecRights");
            this.lvSecRights.BalkenGrafik = false;
            this.lvSecRights.MultiSelect = false;
            this.lvSecRights.Name = "lvSecRights";
            this.lvSecRights.Sortable = false;
            this.lvSecRights.UseCompatibleStateImageBehavior = false;
            this.lvSecRights.View = System.Windows.Forms.View.Details;
            this.lvSecRights.DoubleClick += new System.EventHandler(this.lvSecRights_DoubleClick);
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            // 
            // cmdRefresh
            // 
            resources.ApplyResources(this.cmdRefresh, "cmdRefresh");
            this.cmdRefresh.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpRechte);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.lvSecGroupsSecRights);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grpBenutzergruppen);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.cmdRemove);
            this.splitContainer1.Panel2.Controls.Add(this.llSecGroups);
            // 
            // SecGroupsSecRightsView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.cmdCancel);
            this.Name = "SecGroupsSecRightsView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.SecGroupsSecRightsView_Load);
            this.grpBenutzergruppen.ResumeLayout(false);
            this.grpRechte.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.OplListView lvSecGroupsSecRights;
        private Windows.Forms.OplButton cmdCancel;
        private Windows.Forms.OplListView lvSecGroups;
        private System.Windows.Forms.GroupBox grpBenutzergruppen;
        private Windows.Forms.OplButton cmdRemove;
        private Windows.Forms.OplButton cmdAdd;
        private System.Windows.Forms.GroupBox grpRechte;
        private System.Windows.Forms.LinkLabel llSecGroups;
        private Windows.Forms.OplListView lvSecRights;
        private System.Windows.Forms.Label lblInfo;
        private Windows.Forms.OplButton cmdRefresh;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}