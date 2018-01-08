using Windows.Forms;

namespace Operationen
{
    partial class ChirurgOperationenView : OperationenForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChirurgOperationenView));
            this.grpOperationen = new System.Windows.Forms.GroupBox();
            this.cmdPrintOperationen = new Windows.Forms.OplButton();
            this.cmdRemoveRichtlinie = new Windows.Forms.OplButton();
            this.cmdAssignRichtlinie = new Windows.Forms.OplButton();
            this.cmdSearch = new Windows.Forms.OplButton();
            this.txtOPSSearch = new Windows.Forms.OplTextBox();
            this.lblFilterOPSText = new System.Windows.Forms.Label();
            this.cmdDelete = new Windows.Forms.OplButton();
            this.cmdNew = new Windows.Forms.OplButton();
            this.lvOperationen = new Windows.Forms.SortableListView();
            this.cmdPrintAll = new Windows.Forms.OplButton();
            this.txtDatumBis = new Windows.Forms.DateBoxPicker();
            this.txtDatumVon = new Windows.Forms.DateBoxPicker();
            this.lblChirurg = new System.Windows.Forms.Label();
            this.cmdAnzeigen = new Windows.Forms.OplButton();
            this.cbGebiete = new System.Windows.Forms.ComboBox();
            this.lblDatumBis = new System.Windows.Forms.Label();
            this.lblDatumVon = new System.Windows.Forms.Label();
            this.cbChirurgen = new System.Windows.Forms.ComboBox();
            this.lblOPFunktionen = new System.Windows.Forms.Label();
            this.cbOPFunktionen = new System.Windows.Forms.ComboBox();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grpOPsProJahr = new System.Windows.Forms.GroupBox();
            this.cmdPrintOverview = new Windows.Forms.OplButton();
            this.lblInfoSummary = new System.Windows.Forms.Label();
            this.txtSumme = new Windows.Forms.OplTextBox();
            this.lblSumme = new System.Windows.Forms.Label();
            this.lvOverview = new Windows.Forms.OplListView();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.chkExtern = new Windows.Forms.OplCheckBox();
            this.chkIntern = new Windows.Forms.OplCheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOPS = new Windows.Forms.OplTextBox();
            this.lblGebiet = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grpOperationen.SuspendLayout();
            this.grpOPsProJahr.SuspendLayout();
            this.grpFilter.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpOperationen
            // 
            this.grpOperationen.Controls.Add(this.cmdPrintOperationen);
            this.grpOperationen.Controls.Add(this.cmdRemoveRichtlinie);
            this.grpOperationen.Controls.Add(this.cmdAssignRichtlinie);
            this.grpOperationen.Controls.Add(this.cmdSearch);
            this.grpOperationen.Controls.Add(this.txtOPSSearch);
            this.grpOperationen.Controls.Add(this.lblFilterOPSText);
            this.grpOperationen.Controls.Add(this.cmdDelete);
            this.grpOperationen.Controls.Add(this.cmdNew);
            this.grpOperationen.Controls.Add(this.lvOperationen);
            resources.ApplyResources(this.grpOperationen, "grpOperationen");
            this.grpOperationen.Name = "grpOperationen";
            this.grpOperationen.TabStop = false;
            // 
            // cmdPrintOperationen
            // 
            resources.ApplyResources(this.cmdPrintOperationen, "cmdPrintOperationen");
            this.cmdPrintOperationen.Name = "cmdPrintOperationen";
            this.cmdPrintOperationen.SecurityManager = null;
            this.cmdPrintOperationen.UserRight = null;
            this.cmdPrintOperationen.UseVisualStyleBackColor = true;
            this.cmdPrintOperationen.Click += new System.EventHandler(this.cmdPrintOperationen_Click);
            // 
            // cmdRemoveRichtlinie
            // 
            resources.ApplyResources(this.cmdRemoveRichtlinie, "cmdRemoveRichtlinie");
            this.cmdRemoveRichtlinie.Name = "cmdRemoveRichtlinie";
            this.cmdRemoveRichtlinie.SecurityManager = null;
            this.toolTip1.SetToolTip(this.cmdRemoveRichtlinie, resources.GetString("cmdRemoveRichtlinie.ToolTip"));
            this.cmdRemoveRichtlinie.UserRight = null;
            this.cmdRemoveRichtlinie.UseVisualStyleBackColor = true;
            this.cmdRemoveRichtlinie.Click += new System.EventHandler(this.cmdRemoveRichtlinie_Click);
            // 
            // cmdAssignRichtlinie
            // 
            resources.ApplyResources(this.cmdAssignRichtlinie, "cmdAssignRichtlinie");
            this.cmdAssignRichtlinie.Name = "cmdAssignRichtlinie";
            this.cmdAssignRichtlinie.SecurityManager = null;
            this.toolTip1.SetToolTip(this.cmdAssignRichtlinie, resources.GetString("cmdAssignRichtlinie.ToolTip"));
            this.cmdAssignRichtlinie.UserRight = null;
            this.cmdAssignRichtlinie.UseVisualStyleBackColor = true;
            this.cmdAssignRichtlinie.Click += new System.EventHandler(this.cmdAssignRichtlinie_Click);
            // 
            // cmdSearch
            // 
            resources.ApplyResources(this.cmdSearch, "cmdSearch");
            this.cmdSearch.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.SecurityManager = null;
            this.cmdSearch.UserRight = null;
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // txtOPSSearch
            // 
            resources.ApplyResources(this.txtOPSSearch, "txtOPSSearch");
            this.txtOPSSearch.Name = "txtOPSSearch";
            // 
            // lblFilterOPSText
            // 
            resources.ApplyResources(this.lblFilterOPSText, "lblFilterOPSText");
            this.lblFilterOPSText.Name = "lblFilterOPSText";
            // 
            // cmdDelete
            // 
            resources.ApplyResources(this.cmdDelete, "cmdDelete");
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.SecurityManager = null;
            this.toolTip1.SetToolTip(this.cmdDelete, resources.GetString("cmdDelete.ToolTip"));
            this.cmdDelete.UserRight = null;
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdNew
            // 
            resources.ApplyResources(this.cmdNew, "cmdNew");
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.SecurityManager = null;
            this.toolTip1.SetToolTip(this.cmdNew, resources.GetString("cmdNew.ToolTip"));
            this.cmdNew.UserRight = null;
            this.cmdNew.UseVisualStyleBackColor = true;
            this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
            // 
            // lvOperationen
            // 
            resources.ApplyResources(this.lvOperationen, "lvOperationen");
            this.lvOperationen.FullRowSelect = true;
            this.lvOperationen.Name = "lvOperationen";
            this.lvOperationen.Sortable = true;
            this.lvOperationen.UseCompatibleStateImageBehavior = false;
            this.lvOperationen.View = System.Windows.Forms.View.Details;
            // 
            // cmdPrintAll
            // 
            resources.ApplyResources(this.cmdPrintAll, "cmdPrintAll");
            this.cmdPrintAll.Name = "cmdPrintAll";
            this.cmdPrintAll.SecurityManager = null;
            this.cmdPrintAll.UserRight = null;
            this.cmdPrintAll.UseVisualStyleBackColor = true;
            this.cmdPrintAll.Click += new System.EventHandler(this.cmdPrintAll_Click);
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
            // lblChirurg
            // 
            resources.ApplyResources(this.lblChirurg, "lblChirurg");
            this.lblChirurg.Name = "lblChirurg";
            // 
            // cmdAnzeigen
            // 
            resources.ApplyResources(this.cmdAnzeigen, "cmdAnzeigen");
            this.cmdAnzeigen.Name = "cmdAnzeigen";
            this.cmdAnzeigen.SecurityManager = null;
            this.cmdAnzeigen.UserRight = null;
            this.cmdAnzeigen.UseVisualStyleBackColor = true;
            this.cmdAnzeigen.Click += new System.EventHandler(this.cmdAnzeigen_Click);
            // 
            // cbGebiete
            // 
            this.cbGebiete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGebiete.FormattingEnabled = true;
            resources.ApplyResources(this.cbGebiete, "cbGebiete");
            this.cbGebiete.Name = "cbGebiete";
            this.cbGebiete.SelectedIndexChanged += new System.EventHandler(this.cbGebiete_SelectedIndexChanged);
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
            // cbChirurgen
            // 
            this.cbChirurgen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbChirurgen, "cbChirurgen");
            this.cbChirurgen.Name = "cbChirurgen";
            this.cbChirurgen.SelectedIndexChanged += new System.EventHandler(this.cbChirurgen_SelectedIndexChanged);
            // 
            // lblOPFunktionen
            // 
            resources.ApplyResources(this.lblOPFunktionen, "lblOPFunktionen");
            this.lblOPFunktionen.Name = "lblOPFunktionen";
            // 
            // cbOPFunktionen
            // 
            this.cbOPFunktionen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOPFunktionen.FormattingEnabled = true;
            resources.ApplyResources(this.cbOPFunktionen, "cbOPFunktionen");
            this.cbOPFunktionen.Name = "cbOPFunktionen";
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
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 200;
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 200;
            this.toolTip1.ReshowDelay = 40;
            // 
            // grpOPsProJahr
            // 
            this.grpOPsProJahr.Controls.Add(this.cmdPrintOverview);
            this.grpOPsProJahr.Controls.Add(this.lblInfoSummary);
            this.grpOPsProJahr.Controls.Add(this.txtSumme);
            this.grpOPsProJahr.Controls.Add(this.lblSumme);
            this.grpOPsProJahr.Controls.Add(this.lvOverview);
            resources.ApplyResources(this.grpOPsProJahr, "grpOPsProJahr");
            this.grpOPsProJahr.Name = "grpOPsProJahr";
            this.grpOPsProJahr.TabStop = false;
            // 
            // cmdPrintOverview
            // 
            resources.ApplyResources(this.cmdPrintOverview, "cmdPrintOverview");
            this.cmdPrintOverview.Name = "cmdPrintOverview";
            this.cmdPrintOverview.SecurityManager = null;
            this.cmdPrintOverview.UserRight = null;
            this.cmdPrintOverview.UseVisualStyleBackColor = true;
            this.cmdPrintOverview.Click += new System.EventHandler(this.cmdPrintOverview_Click);
            // 
            // lblInfoSummary
            // 
            resources.ApplyResources(this.lblInfoSummary, "lblInfoSummary");
            this.lblInfoSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInfoSummary.Name = "lblInfoSummary";
            // 
            // txtSumme
            // 
            resources.ApplyResources(this.txtSumme, "txtSumme");
            this.txtSumme.Name = "txtSumme";
            this.txtSumme.ReadOnly = true;
            // 
            // lblSumme
            // 
            resources.ApplyResources(this.lblSumme, "lblSumme");
            this.lblSumme.Name = "lblSumme";
            // 
            // lvOverview
            // 
            resources.ApplyResources(this.lvOverview, "lvOverview");
            this.lvOverview.MultiSelect = false;
            this.lvOverview.Name = "lvOverview";
            this.lvOverview.UseCompatibleStateImageBehavior = false;
            // 
            // grpFilter
            // 
            resources.ApplyResources(this.grpFilter, "grpFilter");
            this.grpFilter.Controls.Add(this.chkExtern);
            this.grpFilter.Controls.Add(this.chkIntern);
            this.grpFilter.Controls.Add(this.label1);
            this.grpFilter.Controls.Add(this.txtOPS);
            this.grpFilter.Controls.Add(this.cmdAnzeigen);
            this.grpFilter.Controls.Add(this.cbOPFunktionen);
            this.grpFilter.Controls.Add(this.lblGebiet);
            this.grpFilter.Controls.Add(this.cbGebiete);
            this.grpFilter.Controls.Add(this.lblOPFunktionen);
            this.grpFilter.Controls.Add(this.lblChirurg);
            this.grpFilter.Controls.Add(this.cbChirurgen);
            this.grpFilter.Controls.Add(this.txtDatumBis);
            this.grpFilter.Controls.Add(this.txtDatumVon);
            this.grpFilter.Controls.Add(this.lblDatumVon);
            this.grpFilter.Controls.Add(this.lblDatumBis);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.TabStop = false;
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtOPS
            // 
            resources.ApplyResources(this.txtOPS, "txtOPS");
            this.txtOPS.Name = "txtOPS";
            // 
            // lblGebiet
            // 
            resources.ApplyResources(this.lblGebiet, "lblGebiet");
            this.lblGebiet.Name = "lblGebiet";
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grpOperationen);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpOPsProJahr);
            // 
            // ChirurgOperationenView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.grpFilter);
            this.Controls.Add(this.cmdPrintAll);
            this.Controls.Add(this.cmdCancel);
            this.Name = "ChirurgOperationenView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.ChirurgOperationenViewNeu_Load);
            this.grpOperationen.ResumeLayout(false);
            this.grpOperationen.PerformLayout();
            this.grpOPsProJahr.ResumeLayout(false);
            this.grpOPsProJahr.PerformLayout();
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpOperationen;
        private Windows.Forms.OplButton cmdCancel;
        private System.Windows.Forms.ToolTip toolTip1;
        private Windows.Forms.SortableListView lvOperationen;
        private System.Windows.Forms.GroupBox grpOPsProJahr;
        private Windows.Forms.OplListView lvOverview;
        private Windows.Forms.OplButton cmdDelete;
        private Windows.Forms.OplButton cmdNew;
        private Windows.Forms.OplButton cmdPrintAll;
        private System.Windows.Forms.Label lblOPFunktionen;
        private System.Windows.Forms.ComboBox cbOPFunktionen;
        private System.Windows.Forms.ComboBox cbChirurgen;
        private Windows.Forms.OplButton cmdAnzeigen;
        private System.Windows.Forms.ComboBox cbGebiete;
        private System.Windows.Forms.Label lblDatumBis;
        private System.Windows.Forms.Label lblDatumVon;
        private System.Windows.Forms.Label lblChirurg;
        private DateBoxPicker txtDatumBis;
        private DateBoxPicker txtDatumVon;
        private Windows.Forms.OplTextBox txtSumme;
        private System.Windows.Forms.Label lblSumme;
        private Windows.Forms.OplButton cmdSearch;
        private Windows.Forms.OplTextBox txtOPSSearch;
        private System.Windows.Forms.Label lblFilterOPSText;
        private Windows.Forms.OplButton cmdAssignRichtlinie;
        private Windows.Forms.OplButton cmdRemoveRichtlinie;
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.Label lblGebiet;
        private System.Windows.Forms.Label label1;
        private Windows.Forms.OplTextBox txtOPS;
        private Windows.Forms.OplCheckBox chkExtern;
        private Windows.Forms.OplCheckBox chkIntern;
        private System.Windows.Forms.Label lblInfoSummary;
        private OplButton cmdPrintOperationen;
        private OplButton cmdPrintOverview;
        private System.Windows.Forms.SplitContainer splitContainer;
    }
}