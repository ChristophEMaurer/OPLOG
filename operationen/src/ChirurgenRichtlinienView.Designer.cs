namespace Operationen
{
    partial class ChirurgenRichtlinienView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChirurgenRichtlinienView));
            this.cmdDelete = new Windows.Forms.OplButton();
            this.grpChirurgenRichtlinien = new System.Windows.Forms.GroupBox();
            this.txtDatum = new Windows.Forms.DateBoxPicker();
            this.txtOrt = new Windows.Forms.OplTextBox();
            this.lblOrt = new System.Windows.Forms.Label();
            this.lblDatum = new System.Windows.Forms.Label();
            this.cmdApply = new Windows.Forms.OplButton();
            this.cmdInsert = new Windows.Forms.OplButton();
            this.lvChirurgenRichtlinien = new Windows.Forms.OplListView();
            this.lblAnzahl = new System.Windows.Forms.Label();
            this.txtAnzahl = new Windows.Forms.OplTextBox();
            this.lvRichtlinien = new Windows.Forms.OplListView();
            this.lblGebiete = new System.Windows.Forms.Label();
            this.cbGebiete = new System.Windows.Forms.ComboBox();
            this.cmdOK = new Windows.Forms.OplButton();
            this.grpRichtlinien = new System.Windows.Forms.GroupBox();
            this.lblInfo = new System.Windows.Forms.LinkLabel();
            this.lblChirurg = new System.Windows.Forms.Label();
            this.cbChirurgen = new System.Windows.Forms.ComboBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grpChirurgenRichtlinien.SuspendLayout();
            this.grpRichtlinien.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdDelete
            // 
            resources.ApplyResources(this.cmdDelete, "cmdDelete");
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // grpChirurgenRichtlinien
            // 
            this.grpChirurgenRichtlinien.Controls.Add(this.txtDatum);
            this.grpChirurgenRichtlinien.Controls.Add(this.txtOrt);
            this.grpChirurgenRichtlinien.Controls.Add(this.lblOrt);
            this.grpChirurgenRichtlinien.Controls.Add(this.lblDatum);
            this.grpChirurgenRichtlinien.Controls.Add(this.cmdApply);
            this.grpChirurgenRichtlinien.Controls.Add(this.cmdDelete);
            this.grpChirurgenRichtlinien.Controls.Add(this.cmdInsert);
            this.grpChirurgenRichtlinien.Controls.Add(this.lvChirurgenRichtlinien);
            this.grpChirurgenRichtlinien.Controls.Add(this.lblAnzahl);
            this.grpChirurgenRichtlinien.Controls.Add(this.txtAnzahl);
            resources.ApplyResources(this.grpChirurgenRichtlinien, "grpChirurgenRichtlinien");
            this.grpChirurgenRichtlinien.Name = "grpChirurgenRichtlinien";
            this.grpChirurgenRichtlinien.TabStop = false;
            // 
            // txtDatum
            // 
            resources.ApplyResources(this.txtDatum, "txtDatum");
            this.txtDatum.Name = "txtDatum";
            // 
            // txtOrt
            // 
            resources.ApplyResources(this.txtOrt, "txtOrt");
            this.txtOrt.Name = "txtOrt";
            // 
            // lblOrt
            // 
            resources.ApplyResources(this.lblOrt, "lblOrt");
            this.lblOrt.Name = "lblOrt";
            // 
            // lblDatum
            // 
            resources.ApplyResources(this.lblDatum, "lblDatum");
            this.lblDatum.Name = "lblDatum";
            // 
            // cmdApply
            // 
            resources.ApplyResources(this.cmdApply, "cmdApply");
            this.cmdApply.Name = "cmdApply";
            this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
            // 
            // cmdInsert
            // 
            resources.ApplyResources(this.cmdInsert, "cmdInsert");
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // lvChirurgenRichtlinien
            // 
            resources.ApplyResources(this.lvChirurgenRichtlinien, "lvChirurgenRichtlinien");
            this.lvChirurgenRichtlinien.BalkenGrafik = false;
            this.lvChirurgenRichtlinien.Name = "lvChirurgenRichtlinien";
            this.lvChirurgenRichtlinien.Sortable = false;
            this.lvChirurgenRichtlinien.UseCompatibleStateImageBehavior = false;
            this.lvChirurgenRichtlinien.SelectedIndexChanged += new System.EventHandler(this.lvChirurgenRichtlinien_SelectedIndexChanged);
            // 
            // lblAnzahl
            // 
            resources.ApplyResources(this.lblAnzahl, "lblAnzahl");
            this.lblAnzahl.Name = "lblAnzahl";
            // 
            // txtAnzahl
            // 
            resources.ApplyResources(this.txtAnzahl, "txtAnzahl");
            this.txtAnzahl.Name = "txtAnzahl";
            // 
            // lvRichtlinien
            // 
            this.lvRichtlinien.BalkenGrafik = false;
            resources.ApplyResources(this.lvRichtlinien, "lvRichtlinien");
            this.lvRichtlinien.MultiSelect = false;
            this.lvRichtlinien.Name = "lvRichtlinien";
            this.lvRichtlinien.Sortable = false;
            this.lvRichtlinien.UseCompatibleStateImageBehavior = false;
            this.lvRichtlinien.View = System.Windows.Forms.View.Details;
            // 
            // lblGebiete
            // 
            resources.ApplyResources(this.lblGebiete, "lblGebiete");
            this.lblGebiete.Name = "lblGebiete";
            // 
            // cbGebiete
            // 
            this.cbGebiete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGebiete.FormattingEnabled = true;
            resources.ApplyResources(this.cbGebiete, "cbGebiete");
            this.cbGebiete.Name = "cbGebiete";
            this.cbGebiete.SelectedIndexChanged += new System.EventHandler(this.cbGebiete_SelectedIndexChanged);
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // grpRichtlinien
            // 
            this.grpRichtlinien.Controls.Add(this.lvRichtlinien);
            resources.ApplyResources(this.grpRichtlinien, "grpRichtlinien");
            this.grpRichtlinien.Name = "grpRichtlinien";
            this.grpRichtlinien.TabStop = false;
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.TabStop = true;
            this.lblInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblInfo_LinkClicked);
            // 
            // lblChirurg
            // 
            resources.ApplyResources(this.lblChirurg, "lblChirurg");
            this.lblChirurg.Name = "lblChirurg";
            // 
            // cbChirurgen
            // 
            this.cbChirurgen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbChirurgen, "cbChirurgen");
            this.cbChirurgen.Name = "cbChirurgen";
            this.cbChirurgen.SelectedIndexChanged += new System.EventHandler(this.cbChirurgen_SelectedIndexChanged);
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grpRichtlinien);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpChirurgenRichtlinien);
            // 
            // ChirurgenRichtlinienView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdOK;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.lblChirurg);
            this.Controls.Add(this.cbChirurgen);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblGebiete);
            this.Controls.Add(this.cbGebiete);
            this.Controls.Add(this.cmdOK);
            this.Name = "ChirurgenRichtlinienView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.ChirurgenRichtlinienView_Load);
            this.Resize += new System.EventHandler(this.ChirurgenRichtlinienView_Resize);
            this.grpChirurgenRichtlinien.ResumeLayout(false);
            this.grpChirurgenRichtlinien.PerformLayout();
            this.grpRichtlinien.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Windows.Forms.OplButton cmdOK;
        private Windows.Forms.OplListView lvChirurgenRichtlinien;
        private System.Windows.Forms.GroupBox grpChirurgenRichtlinien;
        private System.Windows.Forms.Label lblAnzahl;
        private Windows.Forms.OplTextBox txtAnzahl;
        private Windows.Forms.OplListView lvRichtlinien;
        private Windows.Forms.OplButton cmdDelete;
        private Windows.Forms.OplButton cmdInsert;
        private System.Windows.Forms.GroupBox grpRichtlinien;
        private System.Windows.Forms.Label lblGebiete;
        private System.Windows.Forms.ComboBox cbGebiete;
        private System.Windows.Forms.LinkLabel lblInfo;
        private System.Windows.Forms.Label lblChirurg;
        private System.Windows.Forms.ComboBox cbChirurgen;
        private Windows.Forms.OplButton cmdApply;
        private Windows.Forms.OplTextBox txtOrt;
        private System.Windows.Forms.Label lblOrt;
        private System.Windows.Forms.Label lblDatum;
        private Windows.Forms.DateBoxPicker txtDatum;
        private System.Windows.Forms.SplitContainer splitContainer;
    }
}