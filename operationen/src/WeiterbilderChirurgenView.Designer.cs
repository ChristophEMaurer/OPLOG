namespace Operationen
{
    partial class WeiterbilderChirurgenView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WeiterbilderChirurgenView));
            this.lvChirurgen = new Windows.Forms.SortableListView();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.lvWeiterbilder = new Windows.Forms.SortableListView();
            this.cmdRemove = new Windows.Forms.OplButton();
            this.cmdAdd = new Windows.Forms.OplButton();
            this.grpWeiterbilder = new System.Windows.Forms.GroupBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.grpChirurgen = new System.Windows.Forms.GroupBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grpWeiterbilder.SuspendLayout();
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
            this.lvChirurgen.HideSelection = false;
            this.lvChirurgen.MultiSelect = false;
            this.lvChirurgen.Name = "lvChirurgen";
            this.lvChirurgen.Sortable = true;
            this.lvChirurgen.UseCompatibleStateImageBehavior = false;
            this.lvChirurgen.View = System.Windows.Forms.View.Details;
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
            // lvWeiterbilder
            // 
            resources.ApplyResources(this.lvWeiterbilder, "lvWeiterbilder");
            this.lvWeiterbilder.DoubleClickActivation = false;
            this.lvWeiterbilder.FullRowSelect = true;
            this.lvWeiterbilder.HideSelection = false;
            this.lvWeiterbilder.MultiSelect = false;
            this.lvWeiterbilder.Name = "lvWeiterbilder";
            this.lvWeiterbilder.Sortable = true;
            this.lvWeiterbilder.UseCompatibleStateImageBehavior = false;
            this.lvWeiterbilder.View = System.Windows.Forms.View.Details;
            this.lvWeiterbilder.SelectedIndexChanged += new System.EventHandler(this.lvAbteilungen_SelectedIndexChanged);
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
            // grpWeiterbilder
            // 
            this.grpWeiterbilder.Controls.Add(this.lblInfo);
            this.grpWeiterbilder.Controls.Add(this.lvWeiterbilder);
            resources.ApplyResources(this.grpWeiterbilder, "grpWeiterbilder");
            this.grpWeiterbilder.Name = "grpWeiterbilder";
            this.grpWeiterbilder.TabStop = false;
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
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
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grpWeiterbilder);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpChirurgen);
            // 
            // WeiterbilderChirurgenView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.cmdCancel);
            this.Name = "WeiterbilderChirurgenView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.WeiterbilderChirurgenView_Load);
            this.grpWeiterbilder.ResumeLayout(false);
            this.grpChirurgen.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.SortableListView lvChirurgen;
        private Windows.Forms.OplButton cmdCancel;
        private Windows.Forms.SortableListView lvWeiterbilder;
        private System.Windows.Forms.GroupBox grpWeiterbilder;
        private Windows.Forms.OplButton cmdRemove;
        private Windows.Forms.OplButton cmdAdd;
        private System.Windows.Forms.GroupBox grpChirurgen;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.SplitContainer splitContainer;
    }
}