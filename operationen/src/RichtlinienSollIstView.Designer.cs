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
    partial class RichtlinienSollIstView : OperationenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RichtlinienSollIstView));
            this.cmdCancel = new Windows.Forms.OplButton();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.cmdStop = new Windows.Forms.OplButton();
            this.chkExtern = new Windows.Forms.OplCheckBox();
            this.chkIntern = new Windows.Forms.OplCheckBox();
            this.cmdVergleich = new Windows.Forms.OplButton();
            this.cbChirurgen = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbGebiete = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdAdd = new Windows.Forms.OplButton();
            this.txtDatumBis = new Windows.Forms.DateBoxPicker();
            this.txtDatumVon = new Windows.Forms.DateBoxPicker();
            this.lblDatumBis = new System.Windows.Forms.Label();
            this.lblDatumVon = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.grpTest = new System.Windows.Forms.GroupBox();
            this.cmdPrintTest = new Windows.Forms.OplButton();
            this.lvTest = new Windows.Forms.OplListView();
            this.txtTextBox = new Windows.Forms.OplTextBox();
            this.grpRichtlinien = new System.Windows.Forms.GroupBox();
            this.cmdDelete = new Windows.Forms.OplButton();
            this.cmdPrint = new Windows.Forms.OplButton();
            this.cmdUpdate = new Windows.Forms.OplButton();
            this.lvRichtlinien = new Windows.Forms.OplListView();
            this.splitContainerInner = new System.Windows.Forms.SplitContainer();
            this.contextMenuColumn = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.entfernenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abbrechenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblInfo = new System.Windows.Forms.Label();
            this.grpFilter.SuspendLayout();
            this.grpTest.SuspendLayout();
            this.grpRichtlinien.SuspendLayout();
            this.splitContainerInner.Panel1.SuspendLayout();
            this.splitContainerInner.Panel2.SuspendLayout();
            this.splitContainerInner.SuspendLayout();
            this.contextMenuColumn.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.SecurityManager = null;
            this.cmdCancel.UserRight = null;
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // grpFilter
            // 
            resources.ApplyResources(this.grpFilter, "grpFilter");
            this.grpFilter.Controls.Add(this.cmdStop);
            this.grpFilter.Controls.Add(this.chkExtern);
            this.grpFilter.Controls.Add(this.chkIntern);
            this.grpFilter.Controls.Add(this.cmdVergleich);
            this.grpFilter.Controls.Add(this.cbChirurgen);
            this.grpFilter.Controls.Add(this.label2);
            this.grpFilter.Controls.Add(this.cbGebiete);
            this.grpFilter.Controls.Add(this.label3);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.TabStop = false;
            this.grpFilter.Enter += new System.EventHandler(this.grpFilter_Enter);
            // 
            // cmdStop
            // 
            resources.ApplyResources(this.cmdStop, "cmdStop");
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.SecurityManager = null;
            this.cmdStop.UserRight = null;
            this.cmdStop.UseVisualStyleBackColor = true;
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
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
            // cmdVergleich
            // 
            resources.ApplyResources(this.cmdVergleich, "cmdVergleich");
            this.cmdVergleich.Name = "cmdVergleich";
            this.cmdVergleich.SecurityManager = null;
            this.cmdVergleich.UserRight = null;
            this.cmdVergleich.UseVisualStyleBackColor = true;
            this.cmdVergleich.Click += new System.EventHandler(this.cmdVergleich_Click);
            // 
            // cbChirurgen
            // 
            this.cbChirurgen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbChirurgen, "cbChirurgen");
            this.cbChirurgen.Name = "cbChirurgen";
            this.cbChirurgen.SelectedIndexChanged += new System.EventHandler(this.cbChirurgen_SelectedIndexChanged);
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
            // cmdAdd
            // 
            resources.ApplyResources(this.cmdAdd, "cmdAdd");
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.SecurityManager = null;
            this.cmdAdd.UserRight = null;
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
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
            // grpTest
            // 
            this.grpTest.Controls.Add(this.cmdPrintTest);
            this.grpTest.Controls.Add(this.lvTest);
            resources.ApplyResources(this.grpTest, "grpTest");
            this.grpTest.Name = "grpTest";
            this.grpTest.TabStop = false;
            // 
            // cmdPrintTest
            // 
            resources.ApplyResources(this.cmdPrintTest, "cmdPrintTest");
            this.cmdPrintTest.Name = "cmdPrintTest";
            this.cmdPrintTest.SecurityManager = null;
            this.cmdPrintTest.UserRight = null;
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
            // txtTextBox
            // 
            resources.ApplyResources(this.txtTextBox, "txtTextBox");
            this.txtTextBox.Name = "txtTextBox";
            this.txtTextBox.ProtectContents = false;
            // 
            // grpRichtlinien
            // 
            this.grpRichtlinien.Controls.Add(this.cmdDelete);
            this.grpRichtlinien.Controls.Add(this.cmdPrint);
            this.grpRichtlinien.Controls.Add(this.cmdUpdate);
            this.grpRichtlinien.Controls.Add(this.cmdAdd);
            this.grpRichtlinien.Controls.Add(this.lvRichtlinien);
            this.grpRichtlinien.Controls.Add(this.txtDatumVon);
            this.grpRichtlinien.Controls.Add(this.lblDatumVon);
            this.grpRichtlinien.Controls.Add(this.lblDatumBis);
            this.grpRichtlinien.Controls.Add(this.txtDatumBis);
            resources.ApplyResources(this.grpRichtlinien, "grpRichtlinien");
            this.grpRichtlinien.Name = "grpRichtlinien";
            this.grpRichtlinien.TabStop = false;
            // 
            // cmdDelete
            // 
            resources.ApplyResources(this.cmdDelete, "cmdDelete");
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.SecurityManager = null;
            this.cmdDelete.UserRight = null;
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdPrint
            // 
            resources.ApplyResources(this.cmdPrint, "cmdPrint");
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.SecurityManager = null;
            this.cmdPrint.UserRight = null;
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // cmdUpdate
            // 
            resources.ApplyResources(this.cmdUpdate, "cmdUpdate");
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.SecurityManager = null;
            this.cmdUpdate.UserRight = null;
            this.cmdUpdate.UseVisualStyleBackColor = true;
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // lvRichtlinien
            // 
            resources.ApplyResources(this.lvRichtlinien, "lvRichtlinien");
            this.lvRichtlinien.DoubleClickActivation = false;
            this.lvRichtlinien.Name = "lvRichtlinien";
            this.lvRichtlinien.UseCompatibleStateImageBehavior = false;
            this.lvRichtlinien.SelectedIndexChanged += new System.EventHandler(this.lvRichtlinien_SelectedIndexChanged);
            this.lvRichtlinien.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvRichtlinien_ColumnClick);
            // 
            // splitContainerInner
            // 
            resources.ApplyResources(this.splitContainerInner, "splitContainerInner");
            this.splitContainerInner.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
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
            // contextMenuColumn
            // 
            this.contextMenuColumn.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.entfernenToolStripMenuItem,
            this.abbrechenToolStripMenuItem});
            this.contextMenuColumn.Name = "contextMenuColumn";
            this.contextMenuColumn.ShowImageMargin = false;
            resources.ApplyResources(this.contextMenuColumn, "contextMenuColumn");
            // 
            // entfernenToolStripMenuItem
            // 
            this.entfernenToolStripMenuItem.Name = "entfernenToolStripMenuItem";
            resources.ApplyResources(this.entfernenToolStripMenuItem, "entfernenToolStripMenuItem");
            this.entfernenToolStripMenuItem.Click += new System.EventHandler(this.entfernenToolStripMenuItem_Click);
            // 
            // abbrechenToolStripMenuItem
            // 
            this.abbrechenToolStripMenuItem.Name = "abbrechenToolStripMenuItem";
            resources.ApplyResources(this.abbrechenToolStripMenuItem, "abbrechenToolStripMenuItem");
            this.abbrechenToolStripMenuItem.Click += new System.EventHandler(this.abbrechenToolStripMenuItem_Click);
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.TabStop = true;
            // 
            // RichtlinienSollIstView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.splitContainerInner);
            this.Controls.Add(this.grpFilter);
            this.Controls.Add(this.txtTextBox);
            this.Controls.Add(this.cmdCancel);
            this.Name = "RichtlinienSollIstView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.RichtlinienVergleichView_Load);
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            this.grpTest.ResumeLayout(false);
            this.grpRichtlinien.ResumeLayout(false);
            this.grpRichtlinien.PerformLayout();
            this.splitContainerInner.Panel1.ResumeLayout(false);
            this.splitContainerInner.Panel2.ResumeLayout(false);
            this.splitContainerInner.ResumeLayout(false);
            this.contextMenuColumn.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Windows.Forms.OplButton cmdCancel;
        private GroupBox grpFilter;
        private Label label3;
        private ComboBox cbGebiete;
        private Label label2;
        private ComboBox cbChirurgen;
        private ToolTip toolTip;
        private OplButton cmdAdd;
        private DateBoxPicker txtDatumBis;
        private DateBoxPicker txtDatumVon;
        private Label lblDatumBis;
        private Label lblDatumVon;
        private GroupBox grpTest;
        private OplListView lvTest;
        private GroupBox grpRichtlinien;
        private OplListView lvRichtlinien;
        private SplitContainer splitContainerInner;
        private ContextMenuStrip contextMenuColumn;
        private ToolStripMenuItem entfernenToolStripMenuItem;
        private ToolStripMenuItem abbrechenToolStripMenuItem;
        private OplButton cmdUpdate;
        private Label lblInfo;
        private OplButton cmdDelete;
        private OplButton cmdVergleich;
        private OplCheckBox chkExtern;
        private OplCheckBox chkIntern;
        private OplButton cmdStop;
        private OplButton cmdPrintTest;
        private OplTextBox txtTextBox;
        private OplButton cmdPrint;
    }
}