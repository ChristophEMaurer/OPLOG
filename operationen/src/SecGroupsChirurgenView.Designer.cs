namespace Operationen
{
    partial class SecGroupsChirurgenView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SecGroupsChirurgenView));
            this.lvChirurgen = new Windows.Forms.SortableListView();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.lvSecGroups = new Windows.Forms.OplListView();
            this.cmdRemove = new Windows.Forms.OplButton();
            this.cmdAdd = new Windows.Forms.OplButton();
            this.grpBenutzergruppen = new System.Windows.Forms.GroupBox();
            this.llSecGroups = new System.Windows.Forms.LinkLabel();
            this.grpBenutzer = new System.Windows.Forms.GroupBox();
            this.cmdAbort = new Windows.Forms.OplButton();
            this.cmdRefresh = new Windows.Forms.OplButton();
            this.lblProgress = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grpBenutzergruppen.SuspendLayout();
            this.grpBenutzer.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvChirurgen
            // 
            resources.ApplyResources(this.lvChirurgen, "lvChirurgen");
            this.lvChirurgen.Name = "lvChirurgen";
            this.lvChirurgen.Sortable = true;
            this.lvChirurgen.UseCompatibleStateImageBehavior = false;
            this.lvChirurgen.View = System.Windows.Forms.View.Details;
            this.lvChirurgen.DoubleClick += new System.EventHandler(this.lvChirurgen_DoubleClick);
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
            resources.ApplyResources(this.lvSecGroups, "lvSecGroups");
            this.lvSecGroups.FullRowSelect = true;
            this.lvSecGroups.HideSelection = false;
            this.lvSecGroups.MultiSelect = false;
            this.lvSecGroups.Name = "lvSecGroups";
            this.lvSecGroups.UseCompatibleStateImageBehavior = false;
            this.lvSecGroups.View = System.Windows.Forms.View.Details;
            this.lvSecGroups.SelectedIndexChanged += new System.EventHandler(this.lvAbteilungen_SelectedIndexChanged);
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
            this.grpBenutzergruppen.Controls.Add(this.llSecGroups);
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
            // grpBenutzer
            // 
            this.grpBenutzer.Controls.Add(this.cmdAbort);
            this.grpBenutzer.Controls.Add(this.lvChirurgen);
            this.grpBenutzer.Controls.Add(this.cmdRemove);
            this.grpBenutzer.Controls.Add(this.cmdAdd);
            resources.ApplyResources(this.grpBenutzer, "grpBenutzer");
            this.grpBenutzer.Name = "grpBenutzer";
            this.grpBenutzer.TabStop = false;
            // 
            // cmdAbort
            // 
            resources.ApplyResources(this.cmdAbort, "cmdAbort");
            this.cmdAbort.Name = "cmdAbort";
            this.cmdAbort.UseVisualStyleBackColor = true;
            this.cmdAbort.Click += new System.EventHandler(this.cmdAbort_Click);
            // 
            // cmdRefresh
            // 
            resources.ApplyResources(this.cmdRefresh, "cmdRefresh");
            this.cmdRefresh.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // lblProgress
            // 
            resources.ApplyResources(this.lblProgress, "lblProgress");
            this.lblProgress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblProgress.ForeColor = System.Drawing.Color.Red;
            this.lblProgress.Name = "lblProgress";
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grpBenutzergruppen);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpBenutzer);
            // 
            // SecGroupsChirurgenView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.cmdCancel);
            this.Name = "SecGroupsChirurgenView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.SecGroupsChirurgenView_Load);
            this.grpBenutzergruppen.ResumeLayout(false);
            this.grpBenutzergruppen.PerformLayout();
            this.grpBenutzer.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.SortableListView lvChirurgen;
        private Windows.Forms.OplButton cmdCancel;
        private Windows.Forms.OplListView lvSecGroups;
        private System.Windows.Forms.GroupBox grpBenutzergruppen;
        private Windows.Forms.OplButton cmdRemove;
        private Windows.Forms.OplButton cmdAdd;
        private System.Windows.Forms.GroupBox grpBenutzer;
        private System.Windows.Forms.LinkLabel llSecGroups;
        private Windows.Forms.OplButton cmdRefresh;
        private System.Windows.Forms.Label lblProgress;
        private Windows.Forms.OplButton cmdAbort;
        private System.Windows.Forms.SplitContainer splitContainer;
    }
}