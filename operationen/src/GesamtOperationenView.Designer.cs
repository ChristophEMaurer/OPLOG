using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Windows.Forms;


namespace Operationen
{
    partial class GesamtOperationenView : OperationenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GesamtOperationenView));
            this.cmdOK = new Windows.Forms.OplButton();
            this.lblVon = new System.Windows.Forms.Label();
            this.lvOperationen = new Windows.Forms.OplListView();
            this.lblBis = new System.Windows.Forms.Label();
            this.grpOperationen = new System.Windows.Forms.GroupBox();
            this.txtSummeSelektion = new Windows.Forms.OplTextBox();
            this.lblSummeSelektion = new System.Windows.Forms.Label();
            this.cmdSearch = new Windows.Forms.OplButton();
            this.txtGesamtanzahl = new Windows.Forms.OplTextBox();
            this.lblGesamtanzahl = new System.Windows.Forms.Label();
            this.txtFilterOPS = new Windows.Forms.OplTextBox();
            this.lblFilterOPSText = new System.Windows.Forms.Label();
            this.cmdAnzeigenOperationen = new Windows.Forms.OplButton();
            this.cbOperationenOPFunktionen = new System.Windows.Forms.ComboBox();
            this.lblGebiete = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDatumBis = new Windows.Forms.DateBoxPicker();
            this.txtDatumVon = new Windows.Forms.DateBoxPicker();
            this.grpZeitraum = new System.Windows.Forms.GroupBox();
            this.cmdAbort = new Windows.Forms.OplButton();
            this.chkExtern = new Windows.Forms.OplCheckBox();
            this.chkIntern = new Windows.Forms.OplCheckBox();
            this.chkGroupRichtlinie = new Windows.Forms.OplCheckBox();
            this.cmdPrint = new Windows.Forms.OplButton();
            this.radSortOPSKode = new Windows.Forms.OplRadioButton();
            this.radSortRichtlinie = new Windows.Forms.OplRadioButton();
            this.lvGebiete = new Windows.Forms.OplListView();
            this.lblInfo = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grpOperationen.SuspendLayout();
            this.grpZeitraum.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // lblVon
            // 
            resources.ApplyResources(this.lblVon, "lblVon");
            this.lblVon.Name = "lblVon";
            // 
            // lvOperationen
            // 
            resources.ApplyResources(this.lvOperationen, "lvOperationen");
            this.lvOperationen.DoubleClickActivation = false;
            this.lvOperationen.Name = "lvOperationen";
            this.lvOperationen.UseCompatibleStateImageBehavior = false;
            this.lvOperationen.Click += new System.EventHandler(this.lvOperationen_Click);
            // 
            // lblBis
            // 
            resources.ApplyResources(this.lblBis, "lblBis");
            this.lblBis.Name = "lblBis";
            // 
            // grpOperationen
            // 
            this.grpOperationen.Controls.Add(this.txtSummeSelektion);
            this.grpOperationen.Controls.Add(this.lblSummeSelektion);
            this.grpOperationen.Controls.Add(this.cmdSearch);
            this.grpOperationen.Controls.Add(this.txtGesamtanzahl);
            this.grpOperationen.Controls.Add(this.lblGesamtanzahl);
            this.grpOperationen.Controls.Add(this.txtFilterOPS);
            this.grpOperationen.Controls.Add(this.lblFilterOPSText);
            this.grpOperationen.Controls.Add(this.lvOperationen);
            resources.ApplyResources(this.grpOperationen, "grpOperationen");
            this.grpOperationen.Name = "grpOperationen";
            this.grpOperationen.TabStop = false;
            // 
            // txtSummeSelektion
            // 
            resources.ApplyResources(this.txtSummeSelektion, "txtSummeSelektion");
            this.txtSummeSelektion.Name = "txtSummeSelektion";
            this.txtSummeSelektion.ReadOnly = true;
            // 
            // lblSummeSelektion
            // 
            resources.ApplyResources(this.lblSummeSelektion, "lblSummeSelektion");
            this.lblSummeSelektion.Name = "lblSummeSelektion";
            // 
            // cmdSearch
            // 
            resources.ApplyResources(this.cmdSearch, "cmdSearch");
            this.cmdSearch.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // txtGesamtanzahl
            // 
            resources.ApplyResources(this.txtGesamtanzahl, "txtGesamtanzahl");
            this.txtGesamtanzahl.Name = "txtGesamtanzahl";
            this.txtGesamtanzahl.ReadOnly = true;
            // 
            // lblGesamtanzahl
            // 
            resources.ApplyResources(this.lblGesamtanzahl, "lblGesamtanzahl");
            this.lblGesamtanzahl.Name = "lblGesamtanzahl";
            // 
            // txtFilterOPS
            // 
            resources.ApplyResources(this.txtFilterOPS, "txtFilterOPS");
            this.txtFilterOPS.Name = "txtFilterOPS";
            // 
            // lblFilterOPSText
            // 
            resources.ApplyResources(this.lblFilterOPSText, "lblFilterOPSText");
            this.lblFilterOPSText.Name = "lblFilterOPSText";
            // 
            // cmdAnzeigenOperationen
            // 
            resources.ApplyResources(this.cmdAnzeigenOperationen, "cmdAnzeigenOperationen");
            this.cmdAnzeigenOperationen.Name = "cmdAnzeigenOperationen";
            this.cmdAnzeigenOperationen.UseVisualStyleBackColor = true;
            this.cmdAnzeigenOperationen.Click += new System.EventHandler(this.cmdAnzeigenOperationen_Click);
            // 
            // cbOperationenOPFunktionen
            // 
            resources.ApplyResources(this.cbOperationenOPFunktionen, "cbOperationenOPFunktionen");
            this.cbOperationenOPFunktionen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperationenOPFunktionen.FormattingEnabled = true;
            this.cbOperationenOPFunktionen.Name = "cbOperationenOPFunktionen";
            // 
            // lblGebiete
            // 
            resources.ApplyResources(this.lblGebiete, "lblGebiete");
            this.lblGebiete.Name = "lblGebiete";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
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
            // grpZeitraum
            // 
            this.grpZeitraum.Controls.Add(this.cmdAbort);
            this.grpZeitraum.Controls.Add(this.chkExtern);
            this.grpZeitraum.Controls.Add(this.chkIntern);
            this.grpZeitraum.Controls.Add(this.chkGroupRichtlinie);
            this.grpZeitraum.Controls.Add(this.cmdPrint);
            this.grpZeitraum.Controls.Add(this.radSortOPSKode);
            this.grpZeitraum.Controls.Add(this.radSortRichtlinie);
            this.grpZeitraum.Controls.Add(this.cmdAnzeigenOperationen);
            this.grpZeitraum.Controls.Add(this.lvGebiete);
            this.grpZeitraum.Controls.Add(this.txtDatumBis);
            this.grpZeitraum.Controls.Add(this.lblVon);
            this.grpZeitraum.Controls.Add(this.lblBis);
            this.grpZeitraum.Controls.Add(this.cbOperationenOPFunktionen);
            this.grpZeitraum.Controls.Add(this.txtDatumVon);
            this.grpZeitraum.Controls.Add(this.label1);
            this.grpZeitraum.Controls.Add(this.lblGebiete);
            resources.ApplyResources(this.grpZeitraum, "grpZeitraum");
            this.grpZeitraum.MinimumSize = new System.Drawing.Size(0, 196);
            this.grpZeitraum.Name = "grpZeitraum";
            this.grpZeitraum.TabStop = false;
            // 
            // cmdAbort
            // 
            resources.ApplyResources(this.cmdAbort, "cmdAbort");
            this.cmdAbort.Name = "cmdAbort";
            this.cmdAbort.UseVisualStyleBackColor = true;
            this.cmdAbort.Click += new System.EventHandler(this.cmdAbort_Click);
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
            // chkGroupRichtlinie
            // 
            resources.ApplyResources(this.chkGroupRichtlinie, "chkGroupRichtlinie");
            this.chkGroupRichtlinie.Name = "chkGroupRichtlinie";
            this.chkGroupRichtlinie.UseVisualStyleBackColor = true;
            // 
            // cmdPrint
            // 
            resources.ApplyResources(this.cmdPrint, "cmdPrint");
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // radSortOPSKode
            // 
            resources.ApplyResources(this.radSortOPSKode, "radSortOPSKode");
            this.radSortOPSKode.Name = "radSortOPSKode";
            this.radSortOPSKode.TabStop = true;
            this.radSortOPSKode.UseVisualStyleBackColor = true;
            this.radSortOPSKode.CheckedChanged += new System.EventHandler(this.radSortOPSKode_CheckedChanged);
            // 
            // radSortRichtlinie
            // 
            resources.ApplyResources(this.radSortRichtlinie, "radSortRichtlinie");
            this.radSortRichtlinie.Name = "radSortRichtlinie";
            this.radSortRichtlinie.TabStop = true;
            this.radSortRichtlinie.UseVisualStyleBackColor = true;
            this.radSortRichtlinie.CheckedChanged += new System.EventHandler(this.radSortNr_CheckedChanged);
            // 
            // lvGebiete
            // 
            resources.ApplyResources(this.lvGebiete, "lvGebiete");
            this.lvGebiete.DoubleClickActivation = false;
            this.lvGebiete.Name = "lvGebiete";
            this.lvGebiete.UseCompatibleStateImageBehavior = false;
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grpZeitraum);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpOperationen);
            // 
            // GesamtOperationenView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdOK;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.cmdOK);
            this.Name = "GesamtOperationenView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.GesamtOperationenView_Load);
            this.grpOperationen.ResumeLayout(false);
            this.grpOperationen.PerformLayout();
            this.grpZeitraum.ResumeLayout(false);
            this.grpZeitraum.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.OplButton cmdOK;
        private Label lblVon;
        private OplListView lvOperationen;
        private Label lblBis;
        private GroupBox grpOperationen;
        private DateBoxPicker txtDatumVon;
        private DateBoxPicker txtDatumBis;
        private Label lblGebiete;
        private GroupBox grpZeitraum;
        private TextBox txtFilterOPS;
        private Label lblFilterOPSText;
        private ComboBox cbOperationenOPFunktionen;
        private Label label1;
        private TextBox txtGesamtanzahl;
        private Label lblGesamtanzahl;
        private Button cmdAnzeigenOperationen;
        private OplListView lvGebiete;
        private Label lblInfo;
        private Button cmdSearch;
        private RadioButton radSortRichtlinie;
        private RadioButton radSortOPSKode;
        private Button cmdPrint;
        private CheckBox chkGroupRichtlinie;
        private TextBox txtSummeSelektion;
        private Label lblSummeSelektion;
        private CheckBox chkExtern;
        private CheckBox chkIntern;
        private Button cmdAbort;
        private SplitContainer splitContainer;
    }
}