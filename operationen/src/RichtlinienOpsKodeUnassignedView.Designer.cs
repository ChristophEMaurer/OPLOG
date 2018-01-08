namespace Operationen
{
    partial class RichtlinienOpsKodeUnassignedView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RichtlinienOpsKodeUnassignedView));
            this.cmdDelete = new Windows.Forms.OplButton();
            this.grpRichtlinien = new System.Windows.Forms.GroupBox();
            this.lvRichtlinien = new Windows.Forms.OplListView();
            this.cmdInsert = new Windows.Forms.OplButton();
            this.txtOpsKode = new Windows.Forms.OplTextBox();
            this.cmdPrintRichtlinien = new Windows.Forms.OplButton();
            this.lblOpsKode = new System.Windows.Forms.Label();
            this.chkRichtlinie = new Windows.Forms.OplCheckBox();
            this.lblGebiete = new System.Windows.Forms.Label();
            this.cbGebiete = new System.Windows.Forms.ComboBox();
            this.lvRichtlinienOpsCodes = new Windows.Forms.SortableListView();
            this.cmdOK = new Windows.Forms.OplButton();
            this.grpZuordnungen = new System.Windows.Forms.GroupBox();
            this.cmdPrintZuordnungen = new Windows.Forms.OplButton();
            this.lblInfoListe = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.LinkLabel();
            this.radSortKode = new Windows.Forms.OplRadioButton();
            this.radSortRichtlinie = new Windows.Forms.OplRadioButton();
            this.grpMissing = new System.Windows.Forms.GroupBox();
            this.cmdPrintMissing = new Windows.Forms.OplButton();
            this.cmdMissing = new Windows.Forms.OplButton();
            this.txtDatumBis = new Windows.Forms.DateBoxPicker();
            this.txtDatumVon = new Windows.Forms.DateBoxPicker();
            this.lblBis = new System.Windows.Forms.Label();
            this.lblVon = new System.Windows.Forms.Label();
            this.lvMissing = new Windows.Forms.OplListView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.grpRichtlinien.SuspendLayout();
            this.grpZuordnungen.SuspendLayout();
            this.grpMissing.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdDelete
            // 
            resources.ApplyResources(this.cmdDelete, "cmdDelete");
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.SecurityManager = null;
            this.cmdDelete.UserRight = null;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // grpRichtlinien
            // 
            this.grpRichtlinien.Controls.Add(this.lvRichtlinien);
            this.grpRichtlinien.Controls.Add(this.cmdInsert);
            this.grpRichtlinien.Controls.Add(this.txtOpsKode);
            this.grpRichtlinien.Controls.Add(this.cmdPrintRichtlinien);
            this.grpRichtlinien.Controls.Add(this.lblOpsKode);
            this.grpRichtlinien.Controls.Add(this.chkRichtlinie);
            resources.ApplyResources(this.grpRichtlinien, "grpRichtlinien");
            this.grpRichtlinien.Name = "grpRichtlinien";
            this.grpRichtlinien.TabStop = false;
            // 
            // lvRichtlinien
            // 
            resources.ApplyResources(this.lvRichtlinien, "lvRichtlinien");
            this.lvRichtlinien.DoubleClickActivation = false;
            this.lvRichtlinien.MultiSelect = false;
            this.lvRichtlinien.Name = "lvRichtlinien";
            this.lvRichtlinien.UseCompatibleStateImageBehavior = false;
            this.lvRichtlinien.View = System.Windows.Forms.View.Details;
            this.lvRichtlinien.SelectedIndexChanged += new System.EventHandler(this.lvRichtlinien_SelectedIndexChanged);
            // 
            // cmdInsert
            // 
            resources.ApplyResources(this.cmdInsert, "cmdInsert");
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.SecurityManager = null;
            this.cmdInsert.UserRight = null;
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // txtOpsKode
            // 
            resources.ApplyResources(this.txtOpsKode, "txtOpsKode");
            this.txtOpsKode.Name = "txtOpsKode";
            this.txtOpsKode.ProtectContents = false;
            // 
            // cmdPrintRichtlinien
            // 
            resources.ApplyResources(this.cmdPrintRichtlinien, "cmdPrintRichtlinien");
            this.cmdPrintRichtlinien.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdPrintRichtlinien.Name = "cmdPrintRichtlinien";
            this.cmdPrintRichtlinien.SecurityManager = null;
            this.cmdPrintRichtlinien.UserRight = null;
            this.cmdPrintRichtlinien.Click += new System.EventHandler(this.cmdPrintRichtlinien_Click);
            // 
            // lblOpsKode
            // 
            resources.ApplyResources(this.lblOpsKode, "lblOpsKode");
            this.lblOpsKode.Name = "lblOpsKode";
            // 
            // chkRichtlinie
            // 
            resources.ApplyResources(this.chkRichtlinie, "chkRichtlinie");
            this.chkRichtlinie.Name = "chkRichtlinie";
            this.chkRichtlinie.UseVisualStyleBackColor = true;
            this.chkRichtlinie.CheckedChanged += new System.EventHandler(this.chkRichtlinie_CheckedChanged);
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
            // lvRichtlinienOpsCodes
            // 
            resources.ApplyResources(this.lvRichtlinienOpsCodes, "lvRichtlinienOpsCodes");
            this.lvRichtlinienOpsCodes.DoubleClickActivation = false;
            this.lvRichtlinienOpsCodes.Name = "lvRichtlinienOpsCodes";
            this.lvRichtlinienOpsCodes.Sortable = true;
            this.lvRichtlinienOpsCodes.UseCompatibleStateImageBehavior = false;
            this.lvRichtlinienOpsCodes.View = System.Windows.Forms.View.Details;
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.SecurityManager = null;
            this.cmdOK.UserRight = null;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // grpZuordnungen
            // 
            this.grpZuordnungen.Controls.Add(this.cmdPrintZuordnungen);
            this.grpZuordnungen.Controls.Add(this.lblInfoListe);
            this.grpZuordnungen.Controls.Add(this.cmdDelete);
            this.grpZuordnungen.Controls.Add(this.lvRichtlinienOpsCodes);
            resources.ApplyResources(this.grpZuordnungen, "grpZuordnungen");
            this.grpZuordnungen.Name = "grpZuordnungen";
            this.grpZuordnungen.TabStop = false;
            // 
            // cmdPrintZuordnungen
            // 
            resources.ApplyResources(this.cmdPrintZuordnungen, "cmdPrintZuordnungen");
            this.cmdPrintZuordnungen.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdPrintZuordnungen.Name = "cmdPrintZuordnungen";
            this.cmdPrintZuordnungen.SecurityManager = null;
            this.cmdPrintZuordnungen.UserRight = null;
            this.cmdPrintZuordnungen.Click += new System.EventHandler(this.cmdPrintZuordnungen_Click);
            // 
            // lblInfoListe
            // 
            resources.ApplyResources(this.lblInfoListe, "lblInfoListe");
            this.lblInfoListe.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfoListe.Name = "lblInfoListe";
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.TabStop = true;
            this.lblInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblInfo_LinkClicked);
            // 
            // radSortKode
            // 
            resources.ApplyResources(this.radSortKode, "radSortKode");
            this.radSortKode.Name = "radSortKode";
            this.radSortKode.TabStop = true;
            this.radSortKode.UseVisualStyleBackColor = true;
            this.radSortKode.CheckedChanged += new System.EventHandler(this.radSortKode_CheckedChanged);
            // 
            // radSortRichtlinie
            // 
            resources.ApplyResources(this.radSortRichtlinie, "radSortRichtlinie");
            this.radSortRichtlinie.Name = "radSortRichtlinie";
            this.radSortRichtlinie.TabStop = true;
            this.radSortRichtlinie.UseVisualStyleBackColor = true;
            this.radSortRichtlinie.CheckedChanged += new System.EventHandler(this.radSortRichtlinie_CheckedChanged);
            // 
            // grpMissing
            // 
            this.grpMissing.Controls.Add(this.cmdPrintMissing);
            this.grpMissing.Controls.Add(this.cmdMissing);
            this.grpMissing.Controls.Add(this.txtDatumBis);
            this.grpMissing.Controls.Add(this.txtDatumVon);
            this.grpMissing.Controls.Add(this.lblBis);
            this.grpMissing.Controls.Add(this.lblVon);
            this.grpMissing.Controls.Add(this.lvMissing);
            resources.ApplyResources(this.grpMissing, "grpMissing");
            this.grpMissing.Name = "grpMissing";
            this.grpMissing.TabStop = false;
            // 
            // cmdPrintMissing
            // 
            resources.ApplyResources(this.cmdPrintMissing, "cmdPrintMissing");
            this.cmdPrintMissing.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdPrintMissing.Name = "cmdPrintMissing";
            this.cmdPrintMissing.SecurityManager = null;
            this.cmdPrintMissing.UserRight = null;
            this.cmdPrintMissing.Click += new System.EventHandler(this.cmdPrintMissing_Click);
            // 
            // cmdMissing
            // 
            resources.ApplyResources(this.cmdMissing, "cmdMissing");
            this.cmdMissing.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdMissing.Name = "cmdMissing";
            this.cmdMissing.SecurityManager = null;
            this.cmdMissing.UserRight = null;
            this.cmdMissing.Click += new System.EventHandler(this.cmdMissing_Click);
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
            // lblBis
            // 
            resources.ApplyResources(this.lblBis, "lblBis");
            this.lblBis.Name = "lblBis";
            // 
            // lblVon
            // 
            resources.ApplyResources(this.lblVon, "lblVon");
            this.lblVon.Name = "lblVon";
            // 
            // lvMissing
            // 
            resources.ApplyResources(this.lvMissing, "lvMissing");
            this.lvMissing.DoubleClickActivation = false;
            this.lvMissing.Name = "lvMissing";
            this.lvMissing.UseCompatibleStateImageBehavior = false;
            this.lvMissing.DoubleClick += new System.EventHandler(this.lvMissing_DoubleClick);
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grpMissing);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.splitContainer2, "splitContainer2");
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.grpZuordnungen);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.grpRichtlinien);
            // 
            // RichtlinienOpsKodeUnassignedView
            // 
            this.AcceptButton = this.cmdInsert;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdOK;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.radSortRichtlinie);
            this.Controls.Add(this.radSortKode);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblGebiete);
            this.Controls.Add(this.cbGebiete);
            this.Controls.Add(this.cmdOK);
            this.Name = "RichtlinienOpsKodeUnassignedView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.RichtlinienOpsKodeUnassignedView_Load);
            this.grpRichtlinien.ResumeLayout(false);
            this.grpRichtlinien.PerformLayout();
            this.grpZuordnungen.ResumeLayout(false);
            this.grpMissing.ResumeLayout(false);
            this.grpMissing.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Windows.Forms.OplButton cmdOK;
        private Windows.Forms.SortableListView lvRichtlinienOpsCodes;
        private System.Windows.Forms.GroupBox grpRichtlinien;
        private System.Windows.Forms.Label lblOpsKode;
        private Windows.Forms.OplTextBox txtOpsKode;
        private Windows.Forms.OplListView lvRichtlinien;
        private Windows.Forms.OplButton cmdDelete;
        private Windows.Forms.OplButton cmdInsert;
        private System.Windows.Forms.GroupBox grpZuordnungen;
        private System.Windows.Forms.Label lblGebiete;
        private System.Windows.Forms.ComboBox cbGebiete;
        private System.Windows.Forms.LinkLabel lblInfo;
        private System.Windows.Forms.Label lblInfoListe;
        private Windows.Forms.OplButton cmdPrintRichtlinien;
        private Windows.Forms.OplRadioButton radSortKode;
        private Windows.Forms.OplRadioButton radSortRichtlinie;
        private System.Windows.Forms.GroupBox grpMissing;
        private Windows.Forms.OplListView lvMissing;
        private Windows.Forms.DateBoxPicker txtDatumBis;
        private Windows.Forms.DateBoxPicker txtDatumVon;
        private System.Windows.Forms.Label lblBis;
        private System.Windows.Forms.Label lblVon;
        private Windows.Forms.OplButton cmdMissing;
        private Windows.Forms.OplCheckBox chkRichtlinie;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Windows.Forms.OplButton cmdPrintMissing;
        private Windows.Forms.OplButton cmdPrintZuordnungen;
    }
}