namespace Operationen
{
    partial class ChirurgenView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChirurgenView));
            this.cmdCancel = new Windows.Forms.OplButton();
            this.grpChirurgen = new System.Windows.Forms.GroupBox();
            this.lvChirurgen = new Windows.Forms.SortableListView();
            this.cmdOK = new Windows.Forms.OplButton();
            this.radAktiv = new Windows.Forms.OplRadioButton();
            this.radInaktiv = new Windows.Forms.OplRadioButton();
            this.llExclude = new Windows.Forms.OplLinkLabel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.grpChirurgen.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // grpChirurgen
            // 
            resources.ApplyResources(this.grpChirurgen, "grpChirurgen");
            this.grpChirurgen.Controls.Add(this.lvChirurgen);
            this.grpChirurgen.Name = "grpChirurgen";
            this.grpChirurgen.TabStop = false;
            // 
            // lvChirurgen
            // 
            resources.ApplyResources(this.lvChirurgen, "lvChirurgen");
            this.lvChirurgen.FullRowSelect = true;
            this.lvChirurgen.Name = "lvChirurgen";
            this.lvChirurgen.Sortable = true;
            this.lvChirurgen.UseCompatibleStateImageBehavior = false;
            this.lvChirurgen.View = System.Windows.Forms.View.Details;
            this.lvChirurgen.DoubleClick += new System.EventHandler(this.lvChirurgen_DoubleClick);
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // radAktiv
            // 
            resources.ApplyResources(this.radAktiv, "radAktiv");
            this.radAktiv.Name = "radAktiv";
            this.radAktiv.TabStop = true;
            this.radAktiv.UseVisualStyleBackColor = true;
            this.radAktiv.CheckedChanged += new System.EventHandler(this.radAktiv_CheckedChanged);
            // 
            // radInaktiv
            // 
            resources.ApplyResources(this.radInaktiv, "radInaktiv");
            this.radInaktiv.Name = "radInaktiv";
            this.radInaktiv.TabStop = true;
            this.radInaktiv.UseVisualStyleBackColor = true;
            this.radInaktiv.CheckedChanged += new System.EventHandler(this.radInaktiv_CheckedChanged);
            // 
            // llExclude
            // 
            resources.ApplyResources(this.llExclude, "llExclude");
            this.llExclude.Name = "llExclude";
            this.llExclude.TabStop = true;
            this.llExclude.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llExclude_LinkClicked);
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            // 
            // ChirurgenView
            // 
            this.AcceptButton = this.cmdOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.llExclude);
            this.Controls.Add(this.radInaktiv);
            this.Controls.Add(this.radAktiv);
            this.Controls.Add(this.grpChirurgen);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Name = "ChirurgenView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.ChirurgenView_Load);
            this.grpChirurgen.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Windows.Forms.OplButton cmdCancel;
        private System.Windows.Forms.GroupBox grpChirurgen;
        private Windows.Forms.SortableListView lvChirurgen;
        private Windows.Forms.OplButton cmdOK;
        private Windows.Forms.OplRadioButton radAktiv;
        private Windows.Forms.OplRadioButton radInaktiv;
        private Windows.Forms.OplLinkLabel llExclude;
        private System.Windows.Forms.Label lblInfo;
    }
}