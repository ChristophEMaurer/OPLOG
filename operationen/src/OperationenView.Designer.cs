using Windows.Forms;

namespace Operationen
{
    partial class OperationenView : OperationenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OperationenView));
            this.grpOperationen = new System.Windows.Forms.GroupBox();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.cmdSearch = new Windows.Forms.OplButton();
            this.txtSearchOPS = new Windows.Forms.OplTextBox();
            this.lvOperationen = new Windows.Forms.SortableListView();
            this.lblFilterOPSText = new System.Windows.Forms.Label();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.cbChirurgenOPFunktionen = new System.Windows.Forms.ComboBox();
            this.lblOPFunktionen = new System.Windows.Forms.Label();
            this.chkExtern = new Windows.Forms.OplCheckBox();
            this.chkIntern = new Windows.Forms.OplCheckBox();
            this.txtFilterOPS = new Windows.Forms.OplTextBox();
            this.lblOPS = new System.Windows.Forms.Label();
            this.txtDatumBis = new Windows.Forms.DateBoxPicker();
            this.txtDatumVon = new Windows.Forms.DateBoxPicker();
            this.cmdAnzeigen = new Windows.Forms.OplButton();
            this.lblDatumBis = new System.Windows.Forms.Label();
            this.lblDatumVon = new System.Windows.Forms.Label();
            this.cmdOK = new Windows.Forms.OplButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdPrint = new Windows.Forms.OplButton();
            this.grpOperationen.SuspendLayout();
            this.grpFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpOperationen
            // 
            resources.ApplyResources(this.grpOperationen, "grpOperationen");
            this.grpOperationen.Controls.Add(this.cmdCancel);
            this.grpOperationen.Controls.Add(this.cmdSearch);
            this.grpOperationen.Controls.Add(this.txtSearchOPS);
            this.grpOperationen.Controls.Add(this.lvOperationen);
            this.grpOperationen.Controls.Add(this.lblFilterOPSText);
            this.grpOperationen.Name = "grpOperationen";
            this.grpOperationen.TabStop = false;
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.SecurityManager = null;
            this.cmdCancel.UserRight = null;
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdSearch
            // 
            this.cmdSearch.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cmdSearch, "cmdSearch");
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.SecurityManager = null;
            this.cmdSearch.UserRight = null;
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // txtSearchOPS
            // 
            resources.ApplyResources(this.txtSearchOPS, "txtSearchOPS");
            this.txtSearchOPS.Name = "txtSearchOPS";
            this.txtSearchOPS.ProtectContents = false;
            // 
            // lvOperationen
            // 
            resources.ApplyResources(this.lvOperationen, "lvOperationen");
            this.lvOperationen.DoubleClickActivation = false;
            this.lvOperationen.FullRowSelect = true;
            this.lvOperationen.Name = "lvOperationen";
            this.lvOperationen.Sortable = true;
            this.lvOperationen.UseCompatibleStateImageBehavior = false;
            this.lvOperationen.View = System.Windows.Forms.View.Details;
            // 
            // lblFilterOPSText
            // 
            resources.ApplyResources(this.lblFilterOPSText, "lblFilterOPSText");
            this.lblFilterOPSText.Name = "lblFilterOPSText";
            // 
            // grpFilter
            // 
            resources.ApplyResources(this.grpFilter, "grpFilter");
            this.grpFilter.Controls.Add(this.cbChirurgenOPFunktionen);
            this.grpFilter.Controls.Add(this.lblOPFunktionen);
            this.grpFilter.Controls.Add(this.chkExtern);
            this.grpFilter.Controls.Add(this.chkIntern);
            this.grpFilter.Controls.Add(this.txtFilterOPS);
            this.grpFilter.Controls.Add(this.lblOPS);
            this.grpFilter.Controls.Add(this.txtDatumBis);
            this.grpFilter.Controls.Add(this.txtDatumVon);
            this.grpFilter.Controls.Add(this.cmdAnzeigen);
            this.grpFilter.Controls.Add(this.lblDatumBis);
            this.grpFilter.Controls.Add(this.lblDatumVon);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.TabStop = false;
            // 
            // cbChirurgenOPFunktionen
            // 
            this.cbChirurgenOPFunktionen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChirurgenOPFunktionen.FormattingEnabled = true;
            resources.ApplyResources(this.cbChirurgenOPFunktionen, "cbChirurgenOPFunktionen");
            this.cbChirurgenOPFunktionen.Name = "cbChirurgenOPFunktionen";
            // 
            // lblOPFunktionen
            // 
            resources.ApplyResources(this.lblOPFunktionen, "lblOPFunktionen");
            this.lblOPFunktionen.Name = "lblOPFunktionen";
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
            // txtFilterOPS
            // 
            resources.ApplyResources(this.txtFilterOPS, "txtFilterOPS");
            this.txtFilterOPS.Name = "txtFilterOPS";
            this.txtFilterOPS.ProtectContents = false;
            // 
            // lblOPS
            // 
            resources.ApplyResources(this.lblOPS, "lblOPS");
            this.lblOPS.Name = "lblOPS";
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
            // cmdAnzeigen
            // 
            resources.ApplyResources(this.cmdAnzeigen, "cmdAnzeigen");
            this.cmdAnzeigen.Name = "cmdAnzeigen";
            this.cmdAnzeigen.SecurityManager = null;
            this.cmdAnzeigen.UserRight = null;
            this.cmdAnzeigen.UseVisualStyleBackColor = true;
            this.cmdAnzeigen.Click += new System.EventHandler(this.cmdAnzeigen_Click);
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
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.SecurityManager = null;
            this.cmdOK.UserRight = null;
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 200;
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 200;
            this.toolTip1.ReshowDelay = 40;
            // 
            // cmdPrint
            // 
            resources.ApplyResources(this.cmdPrint, "cmdPrint");
            this.cmdPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.SecurityManager = null;
            this.cmdPrint.UserRight = null;
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // OperationenView
            // 
            this.AcceptButton = this.cmdAnzeigen;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdOK;
            this.Controls.Add(this.grpFilter);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.grpOperationen);
            this.Name = "OperationenView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.OperationenView_Load);
            this.grpOperationen.ResumeLayout(false);
            this.grpOperationen.PerformLayout();
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpOperationen;
        private Windows.Forms.OplButton cmdOK;
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.ToolTip toolTip1;
        private Windows.Forms.SortableListView lvOperationen;
        private Windows.Forms.OplButton cmdAnzeigen;
        private System.Windows.Forms.Label lblDatumBis;
        private System.Windows.Forms.Label lblDatumVon;
        private DateBoxPicker txtDatumBis;
        private DateBoxPicker txtDatumVon;
        private Windows.Forms.OplButton cmdSearch;
        private Windows.Forms.OplTextBox txtSearchOPS;
        private System.Windows.Forms.Label lblFilterOPSText;
        private Windows.Forms.OplTextBox txtFilterOPS;
        private System.Windows.Forms.Label lblOPS;
        private Windows.Forms.OplCheckBox chkExtern;
        private Windows.Forms.OplCheckBox chkIntern;
        private Windows.Forms.OplButton cmdPrint;
        private System.Windows.Forms.ComboBox cbChirurgenOPFunktionen;
        private System.Windows.Forms.Label lblOPFunktionen;
        private Windows.Forms.OplButton cmdCancel;
    }
}