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
    partial class RichtlinienVergleichView : OperationenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RichtlinienVergleichView));
            this.cmdCancel = new Windows.Forms.OplButton();
            this.lblDatumVon = new System.Windows.Forms.Label();
            this.lvRichtlinien = new Windows.Forms.OplListView();
            this.lblDatumBis = new System.Windows.Forms.Label();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.cmdStop = new Windows.Forms.OplButton();
            this.chkZeitraum = new Windows.Forms.OplCheckBox();
            this.chkExtern = new Windows.Forms.OplCheckBox();
            this.cmdZeitraum = new Windows.Forms.OplButton();
            this.chkIntern = new Windows.Forms.OplCheckBox();
            this.cmdVergleich = new Windows.Forms.OplButton();
            this.txtDatumBis = new Windows.Forms.DateBoxPicker();
            this.txtDatumVon = new Windows.Forms.DateBoxPicker();
            this.cbChirurgen = new System.Windows.Forms.ComboBox();
            this.chkDate = new Windows.Forms.OplCheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbGebiete = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grpTest = new System.Windows.Forms.GroupBox();
            this.chkAlleRichtlinien = new Windows.Forms.OplCheckBox();
            this.cmdPrintBDC = new Windows.Forms.OplButton();
            this.cmdPrintTest = new Windows.Forms.OplButton();
            this.lvTest = new Windows.Forms.OplListView();
            this.grpRichtlinien = new System.Windows.Forms.GroupBox();
            this.lblProzedur = new System.Windows.Forms.Label();
            this.lblRichtlinie = new System.Windows.Forms.Label();
            this.grpMissing = new System.Windows.Forms.GroupBox();
            this.cmdPrintMissing = new Windows.Forms.OplButton();
            this.cmdStop2 = new Windows.Forms.OplButton();
            this.cmdMissing = new Windows.Forms.OplButton();
            this.cmdAssignOPSRichtlinie = new Windows.Forms.OplButton();
            this.cmdAssignRichtlinie = new Windows.Forms.OplButton();
            this.lvMissing = new Windows.Forms.OplListView();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainerInner = new System.Windows.Forms.SplitContainer();
            this.splitContainerOuter = new System.Windows.Forms.SplitContainer();
            this.grpFilter.SuspendLayout();
            this.grpTest.SuspendLayout();
            this.grpRichtlinien.SuspendLayout();
            this.grpMissing.SuspendLayout();
            this.splitContainerInner.Panel1.SuspendLayout();
            this.splitContainerInner.Panel2.SuspendLayout();
            this.splitContainerInner.SuspendLayout();
            this.splitContainerOuter.Panel1.SuspendLayout();
            this.splitContainerOuter.Panel2.SuspendLayout();
            this.splitContainerOuter.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // lblDatumVon
            // 
            resources.ApplyResources(this.lblDatumVon, "lblDatumVon");
            this.lblDatumVon.Name = "lblDatumVon";
            // 
            // lvRichtlinien
            // 
            resources.ApplyResources(this.lvRichtlinien, "lvRichtlinien");
            this.lvRichtlinien.DoubleClickActivation = false;
            this.lvRichtlinien.Name = "lvRichtlinien";
            this.lvRichtlinien.UseCompatibleStateImageBehavior = false;
            this.lvRichtlinien.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.lvRichtlinien_ColumnWidthChanged);
            this.lvRichtlinien.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lvRichtlinien_ColumnWidthChanging);
            // 
            // lblDatumBis
            // 
            resources.ApplyResources(this.lblDatumBis, "lblDatumBis");
            this.lblDatumBis.Name = "lblDatumBis";
            // 
            // grpFilter
            // 
            resources.ApplyResources(this.grpFilter, "grpFilter");
            this.grpFilter.Controls.Add(this.cmdStop);
            this.grpFilter.Controls.Add(this.chkZeitraum);
            this.grpFilter.Controls.Add(this.chkExtern);
            this.grpFilter.Controls.Add(this.cmdZeitraum);
            this.grpFilter.Controls.Add(this.chkIntern);
            this.grpFilter.Controls.Add(this.cmdVergleich);
            this.grpFilter.Controls.Add(this.txtDatumBis);
            this.grpFilter.Controls.Add(this.txtDatumVon);
            this.grpFilter.Controls.Add(this.cbChirurgen);
            this.grpFilter.Controls.Add(this.chkDate);
            this.grpFilter.Controls.Add(this.label2);
            this.grpFilter.Controls.Add(this.cbGebiete);
            this.grpFilter.Controls.Add(this.label3);
            this.grpFilter.Controls.Add(this.lblDatumBis);
            this.grpFilter.Controls.Add(this.lblDatumVon);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.TabStop = false;
            // 
            // cmdStop
            // 
            resources.ApplyResources(this.cmdStop, "cmdStop");
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.UseVisualStyleBackColor = true;
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // chkZeitraum
            // 
            resources.ApplyResources(this.chkZeitraum, "chkZeitraum");
            this.chkZeitraum.Name = "chkZeitraum";
            this.chkZeitraum.UseVisualStyleBackColor = true;
            this.chkZeitraum.CheckedChanged += new System.EventHandler(this.chkZeitraum_CheckedChanged);
            // 
            // chkExtern
            // 
            resources.ApplyResources(this.chkExtern, "chkExtern");
            this.chkExtern.Name = "chkExtern";
            this.chkExtern.UseVisualStyleBackColor = true;
            // 
            // cmdZeitraum
            // 
            resources.ApplyResources(this.cmdZeitraum, "cmdZeitraum");
            this.cmdZeitraum.Name = "cmdZeitraum";
            this.cmdZeitraum.UseVisualStyleBackColor = true;
            this.cmdZeitraum.Click += new System.EventHandler(this.cmdZeitraum_Click);
            // 
            // chkIntern
            // 
            resources.ApplyResources(this.chkIntern, "chkIntern");
            this.chkIntern.Name = "chkIntern";
            this.chkIntern.UseVisualStyleBackColor = true;
            // 
            // cmdVergleich
            // 
            resources.ApplyResources(this.cmdVergleich, "cmdVergleich");
            this.cmdVergleich.Name = "cmdVergleich";
            this.cmdVergleich.UseVisualStyleBackColor = true;
            this.cmdVergleich.Click += new System.EventHandler(this.cmdVergleich_Click);
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
            // cbChirurgen
            // 
            this.cbChirurgen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbChirurgen, "cbChirurgen");
            this.cbChirurgen.Name = "cbChirurgen";
            this.cbChirurgen.SelectedIndexChanged += new System.EventHandler(this.cbChirurgen_SelectedIndexChanged);
            // 
            // chkDate
            // 
            resources.ApplyResources(this.chkDate, "chkDate");
            this.chkDate.Name = "chkDate";
            this.chkDate.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cbGebiete
            // 
            this.cbGebiete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGebiete.FormattingEnabled = true;
            resources.ApplyResources(this.cbGebiete, "cbGebiete");
            this.cbGebiete.Name = "cbGebiete";
            this.cbGebiete.SelectedIndexChanged += new System.EventHandler(this.cbGebiete_SelectedIndexChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // grpTest
            // 
            this.grpTest.Controls.Add(this.chkAlleRichtlinien);
            this.grpTest.Controls.Add(this.cmdPrintBDC);
            this.grpTest.Controls.Add(this.cmdPrintTest);
            this.grpTest.Controls.Add(this.lvTest);
            resources.ApplyResources(this.grpTest, "grpTest");
            this.grpTest.Name = "grpTest";
            this.grpTest.TabStop = false;
            // 
            // chkAlleRichtlinien
            // 
            resources.ApplyResources(this.chkAlleRichtlinien, "chkAlleRichtlinien");
            this.chkAlleRichtlinien.Name = "chkAlleRichtlinien";
            this.chkAlleRichtlinien.UseVisualStyleBackColor = true;
            // 
            // cmdPrintBDC
            // 
            resources.ApplyResources(this.cmdPrintBDC, "cmdPrintBDC");
            this.cmdPrintBDC.Name = "cmdPrintBDC";
            this.cmdPrintBDC.UseVisualStyleBackColor = true;
            this.cmdPrintBDC.Click += new System.EventHandler(this.cmdPrintBDC_Click);
            // 
            // cmdPrintTest
            // 
            resources.ApplyResources(this.cmdPrintTest, "cmdPrintTest");
            this.cmdPrintTest.Name = "cmdPrintTest";
            this.cmdPrintTest.UseVisualStyleBackColor = true;
            this.cmdPrintTest.Click += new System.EventHandler(this.cmdPrintTest_Click);
            // 
            // lvTest
            // 
            resources.ApplyResources(this.lvTest, "lvTest");
            this.lvTest.DoubleClickActivation = false;
            this.lvTest.Name = "lvTest";
            this.lvTest.UseCompatibleStateImageBehavior = false;
            // 
            // grpRichtlinien
            // 
            this.grpRichtlinien.Controls.Add(this.lblProzedur);
            this.grpRichtlinien.Controls.Add(this.lblRichtlinie);
            this.grpRichtlinien.Controls.Add(this.lvRichtlinien);
            resources.ApplyResources(this.grpRichtlinien, "grpRichtlinien");
            this.grpRichtlinien.Name = "grpRichtlinien";
            this.grpRichtlinien.TabStop = false;
            // 
            // lblProzedur
            // 
            this.lblProzedur.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.lblProzedur, "lblProzedur");
            this.lblProzedur.Name = "lblProzedur";
            // 
            // lblRichtlinie
            // 
            this.lblRichtlinie.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.lblRichtlinie, "lblRichtlinie");
            this.lblRichtlinie.Name = "lblRichtlinie";
            // 
            // grpMissing
            // 
            this.grpMissing.Controls.Add(this.cmdPrintMissing);
            this.grpMissing.Controls.Add(this.cmdStop2);
            this.grpMissing.Controls.Add(this.cmdMissing);
            this.grpMissing.Controls.Add(this.cmdAssignOPSRichtlinie);
            this.grpMissing.Controls.Add(this.cmdAssignRichtlinie);
            this.grpMissing.Controls.Add(this.lvMissing);
            resources.ApplyResources(this.grpMissing, "grpMissing");
            this.grpMissing.Name = "grpMissing";
            this.grpMissing.TabStop = false;
            // 
            // cmdPrintMissing
            // 
            resources.ApplyResources(this.cmdPrintMissing, "cmdPrintMissing");
            this.cmdPrintMissing.Name = "cmdPrintMissing";
            this.cmdPrintMissing.UseVisualStyleBackColor = true;
            this.cmdPrintMissing.Click += new System.EventHandler(this.cmdPrintMissing_Click);
            // 
            // cmdStop2
            // 
            resources.ApplyResources(this.cmdStop2, "cmdStop2");
            this.cmdStop2.Name = "cmdStop2";
            this.cmdStop2.UseVisualStyleBackColor = true;
            // 
            // cmdMissing
            // 
            resources.ApplyResources(this.cmdMissing, "cmdMissing");
            this.cmdMissing.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdMissing.Name = "cmdMissing";
            this.cmdMissing.UseVisualStyleBackColor = true;
            this.cmdMissing.Click += new System.EventHandler(this.cmdMissing_Click);
            // 
            // cmdAssignOPSRichtlinie
            // 
            resources.ApplyResources(this.cmdAssignOPSRichtlinie, "cmdAssignOPSRichtlinie");
            this.cmdAssignOPSRichtlinie.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdAssignOPSRichtlinie.Name = "cmdAssignOPSRichtlinie";
            this.cmdAssignOPSRichtlinie.SecurityManager = null;
            this.cmdAssignOPSRichtlinie.UserRight = null;
            this.cmdAssignOPSRichtlinie.UseVisualStyleBackColor = true;
            this.cmdAssignOPSRichtlinie.Click += new System.EventHandler(this.cmdAssignOPSRichtlinie_Click);
            // 
            // cmdAssignRichtlinie
            // 
            resources.ApplyResources(this.cmdAssignRichtlinie, "cmdAssignRichtlinie");
            this.cmdAssignRichtlinie.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdAssignRichtlinie.Name = "cmdAssignRichtlinie";
            this.cmdAssignRichtlinie.SecurityManager = null;
            this.cmdAssignRichtlinie.UserRight = null;
            this.cmdAssignRichtlinie.UseVisualStyleBackColor = true;
            this.cmdAssignRichtlinie.Click += new System.EventHandler(this.cmdAssignRichtlinie_Click);
            // 
            // lvMissing
            // 
            resources.ApplyResources(this.lvMissing, "lvMissing");
            this.lvMissing.DoubleClickActivation = false;
            this.lvMissing.Name = "lvMissing";
            this.lvMissing.UseCompatibleStateImageBehavior = false;
            // 
            // splitContainerInner
            // 
            this.splitContainerInner.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.splitContainerInner, "splitContainerInner");
            this.splitContainerInner.Name = "splitContainerInner";
            // 
            // splitContainerInner.Panel1
            // 
            this.splitContainerInner.Panel1.Controls.Add(this.grpRichtlinien);
            // 
            // splitContainerInner.Panel2
            // 
            this.splitContainerInner.Panel2.Controls.Add(this.grpTest);
            // 
            // splitContainerOuter
            // 
            resources.ApplyResources(this.splitContainerOuter, "splitContainerOuter");
            this.splitContainerOuter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainerOuter.Name = "splitContainerOuter";
            // 
            // splitContainerOuter.Panel1
            // 
            this.splitContainerOuter.Panel1.Controls.Add(this.splitContainerInner);
            // 
            // splitContainerOuter.Panel2
            // 
            this.splitContainerOuter.Panel2.Controls.Add(this.grpMissing);
            // 
            // RichtlinienVergleichView
            // 
            this.AcceptButton = this.cmdVergleich;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.splitContainerOuter);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.grpFilter);
            this.Name = "RichtlinienVergleichView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.RichtlinienVergleichView_Load);
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            this.grpTest.ResumeLayout(false);
            this.grpTest.PerformLayout();
            this.grpRichtlinien.ResumeLayout(false);
            this.grpMissing.ResumeLayout(false);
            this.splitContainerInner.Panel1.ResumeLayout(false);
            this.splitContainerInner.Panel2.ResumeLayout(false);
            this.splitContainerInner.ResumeLayout(false);
            this.splitContainerOuter.Panel1.ResumeLayout(false);
            this.splitContainerOuter.Panel2.ResumeLayout(false);
            this.splitContainerOuter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.OplButton cmdCancel;
        private Label lblDatumVon;
        private OplListView lvRichtlinien;
        private Label lblDatumBis;
        private GroupBox grpFilter;
        private GroupBox grpTest;
        private OplListView lvTest;
        private Button cmdVergleich;
        private GroupBox grpRichtlinien;
        private Label label3;
        private ComboBox cbGebiete;
        private Label label2;
        private CheckBox chkDate;
        private ComboBox cbChirurgen;
        private GroupBox grpMissing;
        private OplListView lvMissing;
        private DateBoxPicker txtDatumBis;
        private DateBoxPicker txtDatumVon;
        private OplButton cmdAssignRichtlinie;
        private OplButton cmdAssignOPSRichtlinie;
        private Button cmdPrintTest;
        private Button cmdPrintBDC;
        private CheckBox chkExtern;
        private CheckBox chkIntern;
        private Button cmdMissing;
        private Button cmdZeitraum;
        private CheckBox chkZeitraum;
        private Button cmdStop;
        private Button cmdStop2;
        private CheckBox chkAlleRichtlinien;
        private ToolTip toolTip;
        private Label lblProzedur;
        private Label lblRichtlinie;
        private SplitContainer splitContainerInner;
        private SplitContainer splitContainerOuter;
        private Button cmdPrintMissing;
    }
}