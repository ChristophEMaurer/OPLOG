namespace Operationen
{
    partial class AbteilungenChirurgenView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AbteilungenChirurgenView));
            this.lvChirurgen = new Windows.Forms.SortableListView();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.lvAbteilungen = new Windows.Forms.OplListView();
            this.cmdRemove = new Windows.Forms.OplButton();
            this.cmdAdd = new Windows.Forms.OplButton();
            this.grpAbteilungen = new System.Windows.Forms.GroupBox();
            this.llAbteilungen = new Windows.Forms.OplLinkLabel();
            this.grpChirurgen = new System.Windows.Forms.GroupBox();
            this.cmdRefresh = new Windows.Forms.OplButton();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grpAbteilungen.SuspendLayout();
            this.grpChirurgen.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvChirurgen
            // 
            resources.ApplyResources(this.lvChirurgen, "lvChirurgen");
            this.lvChirurgen.DoubleClickActivation = false;
            this.lvChirurgen.MultiSelect = false;
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
            this.cmdCancel.SecurityManager = null;
            this.cmdCancel.UserRight = null;
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // lvAbteilungen
            // 
            resources.ApplyResources(this.lvAbteilungen, "lvAbteilungen");
            this.lvAbteilungen.DoubleClickActivation = false;
            this.lvAbteilungen.FullRowSelect = true;
            this.lvAbteilungen.HideSelection = false;
            this.lvAbteilungen.MultiSelect = false;
            this.lvAbteilungen.Name = "lvAbteilungen";
            this.lvAbteilungen.UseCompatibleStateImageBehavior = false;
            this.lvAbteilungen.View = System.Windows.Forms.View.Details;
            this.lvAbteilungen.SelectedIndexChanged += new System.EventHandler(this.lvAbteilungen_SelectedIndexChanged);
            // 
            // cmdRemove
            // 
            resources.ApplyResources(this.cmdRemove, "cmdRemove");
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.SecurityManager = null;
            this.cmdRemove.UserRight = null;
            this.cmdRemove.UseVisualStyleBackColor = true;
            this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
            // 
            // cmdAdd
            // 
            resources.ApplyResources(this.cmdAdd, "cmdAdd");
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.SecurityManager = null;
            this.cmdAdd.UserRight = null;
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // grpAbteilungen
            // 
            this.grpAbteilungen.Controls.Add(this.llAbteilungen);
            this.grpAbteilungen.Controls.Add(this.lvAbteilungen);
            resources.ApplyResources(this.grpAbteilungen, "grpAbteilungen");
            this.grpAbteilungen.Name = "grpAbteilungen";
            this.grpAbteilungen.TabStop = false;
            // 
            // llAbteilungen
            // 
            resources.ApplyResources(this.llAbteilungen, "llAbteilungen");
            this.llAbteilungen.Name = "llAbteilungen";
            this.llAbteilungen.TabStop = true;
            this.llAbteilungen.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llAbteilungen_LinkClicked);
            // 
            // grpChirurgen
            // 
            this.grpChirurgen.Controls.Add(this.lvChirurgen);
            this.grpChirurgen.Controls.Add(this.cmdRemove);
            this.grpChirurgen.Controls.Add(this.cmdAdd);
            resources.ApplyResources(this.grpChirurgen, "grpChirurgen");
            this.grpChirurgen.Name = "grpChirurgen";
            this.grpChirurgen.TabStop = false;
            // 
            // cmdRefresh
            // 
            resources.ApplyResources(this.cmdRefresh, "cmdRefresh");
            this.cmdRefresh.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.SecurityManager = null;
            this.cmdRefresh.UserRight = null;
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
            this.splitContainer.Panel1.Controls.Add(this.grpAbteilungen);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpChirurgen);
            // 
            // AbteilungenChirurgenView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.cmdCancel);
            this.Name = "AbteilungenChirurgenView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.AbteilungenChirurgenView_Load);
            this.grpAbteilungen.ResumeLayout(false);
            this.grpAbteilungen.PerformLayout();
            this.grpChirurgen.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.SortableListView lvChirurgen;
        private Windows.Forms.OplButton cmdCancel;
        private Windows.Forms.OplListView lvAbteilungen;
        private System.Windows.Forms.GroupBox grpAbteilungen;
        private Windows.Forms.OplButton cmdRemove;
        private Windows.Forms.OplButton cmdAdd;
        private System.Windows.Forms.GroupBox grpChirurgen;
        private Windows.Forms.OplLinkLabel llAbteilungen;
        private Windows.Forms.OplButton cmdRefresh;
        private System.Windows.Forms.SplitContainer splitContainer;
    }
}