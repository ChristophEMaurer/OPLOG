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
    partial class RichtlinienVergleichOverviewView : OperationenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RichtlinienVergleichOverviewView));
            this.cmdCancel = new Windows.Forms.OplButton();
            this.lblDatumVon = new System.Windows.Forms.Label();
            this.lblDatumBis = new System.Windows.Forms.Label();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.chkExtern = new Windows.Forms.OplCheckBox();
            this.chkIntern = new Windows.Forms.OplCheckBox();
            this.txtDatumBis = new Windows.Forms.DateBoxPicker();
            this.txtDatumVon = new Windows.Forms.DateBoxPicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cbGebiete = new System.Windows.Forms.ComboBox();
            this.grpRichlinien = new System.Windows.Forms.GroupBox();
            this.lvRichtlinien = new Windows.Forms.OplListView();
            this.cmdVergleich = new Windows.Forms.OplButton();
            this.grpTest = new System.Windows.Forms.GroupBox();
            this.txtSumme = new Windows.Forms.OplTextBox();
            this.cmdAbort = new Windows.Forms.OplButton();
            this.lblSumme = new System.Windows.Forms.Label();
            this.cmdPrint = new Windows.Forms.OplButton();
            this.lvTest = new Windows.Forms.ListViewBalken();
            this.lblInfo = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grpFilter.SuspendLayout();
            this.grpRichlinien.SuspendLayout();
            this.grpTest.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
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
            // lblDatumBis
            // 
            resources.ApplyResources(this.lblDatumBis, "lblDatumBis");
            this.lblDatumBis.Name = "lblDatumBis";
            // 
            // grpFilter
            // 
            resources.ApplyResources(this.grpFilter, "grpFilter");
            this.grpFilter.Controls.Add(this.chkExtern);
            this.grpFilter.Controls.Add(this.chkIntern);
            this.grpFilter.Controls.Add(this.txtDatumBis);
            this.grpFilter.Controls.Add(this.txtDatumVon);
            this.grpFilter.Controls.Add(this.label2);
            this.grpFilter.Controls.Add(this.cbGebiete);
            this.grpFilter.Controls.Add(this.lblDatumBis);
            this.grpFilter.Controls.Add(this.lblDatumVon);
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
            // grpRichlinien
            // 
            this.grpRichlinien.Controls.Add(this.lvRichtlinien);
            resources.ApplyResources(this.grpRichlinien, "grpRichlinien");
            this.grpRichlinien.Name = "grpRichlinien";
            this.grpRichlinien.TabStop = false;
            // 
            // lvRichtlinien
            // 
            resources.ApplyResources(this.lvRichtlinien, "lvRichtlinien");
            this.lvRichtlinien.BalkenGrafik = false;
            this.lvRichtlinien.MultiSelect = false;
            this.lvRichtlinien.Name = "lvRichtlinien";
            this.lvRichtlinien.Sortable = false;
            this.lvRichtlinien.UseCompatibleStateImageBehavior = false;
            this.lvRichtlinien.View = System.Windows.Forms.View.Details;
            this.lvRichtlinien.Click += new System.EventHandler(this.lvRichtlinien_Click);
            // 
            // cmdVergleich
            // 
            resources.ApplyResources(this.cmdVergleich, "cmdVergleich");
            this.cmdVergleich.Name = "cmdVergleich";
            this.cmdVergleich.UseVisualStyleBackColor = true;
            this.cmdVergleich.Click += new System.EventHandler(this.cmdVergleich_Click);
            // 
            // grpTest
            // 
            this.grpTest.Controls.Add(this.txtSumme);
            this.grpTest.Controls.Add(this.cmdAbort);
            this.grpTest.Controls.Add(this.lblSumme);
            this.grpTest.Controls.Add(this.cmdPrint);
            this.grpTest.Controls.Add(this.lvTest);
            this.grpTest.Controls.Add(this.cmdVergleich);
            resources.ApplyResources(this.grpTest, "grpTest");
            this.grpTest.Name = "grpTest";
            this.grpTest.TabStop = false;
            // 
            // txtSumme
            // 
            resources.ApplyResources(this.txtSumme, "txtSumme");
            this.txtSumme.Name = "txtSumme";
            this.txtSumme.ReadOnly = true;
            // 
            // cmdAbort
            // 
            resources.ApplyResources(this.cmdAbort, "cmdAbort");
            this.cmdAbort.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdAbort.Name = "cmdAbort";
            this.cmdAbort.UseVisualStyleBackColor = true;
            this.cmdAbort.Click += new System.EventHandler(this.cmdAbort_Click);
            // 
            // lblSumme
            // 
            resources.ApplyResources(this.lblSumme, "lblSumme");
            this.lblSumme.Name = "lblSumme";
            // 
            // cmdPrint
            // 
            resources.ApplyResources(this.cmdPrint, "cmdPrint");
            this.cmdPrint.BackColor = System.Drawing.SystemColors.Control;
            this.cmdPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.UseVisualStyleBackColor = false;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // lvTest
            // 
            resources.ApplyResources(this.lvTest, "lvTest");
            this.lvTest.BalkenGrafik = false;
            this.lvTest.FullRowSelect = true;
            this.lvTest.MultiSelect = false;
            this.lvTest.Name = "lvTest";
            this.lvTest.OwnerDraw = true;
            this.lvTest.Sortable = false;
            this.lvTest.UseCompatibleStateImageBehavior = false;
            this.lvTest.View = System.Windows.Forms.View.Details;
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.TabStop = true;
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grpRichlinien);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpTest);
            // 
            // RichtlinienVergleichOverviewView
            // 
            this.AcceptButton = this.cmdVergleich;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.grpFilter);
            this.Controls.Add(this.cmdCancel);
            this.Name = "RichtlinienVergleichOverviewView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.RichtlinienVergleichOverviewView_Load);
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            this.grpRichlinien.ResumeLayout(false);
            this.grpTest.ResumeLayout(false);
            this.grpTest.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.OplButton cmdCancel;
        private Label lblDatumVon;
        private Label lblDatumBis;
        private GroupBox grpFilter;
        private GroupBox grpTest;
        private Button cmdVergleich;
        private ComboBox cbGebiete;
        private Label label2;
        private Windows.Forms.DateBoxPicker txtDatumBis;
        private Windows.Forms.DateBoxPicker txtDatumVon;
        private CheckBox chkExtern;
        private CheckBox chkIntern;
        private OplListView lvRichtlinien;
        private GroupBox grpRichlinien;
        private Windows.Forms.ListViewBalken lvTest;
        private Button cmdPrint;
        private Label lblInfo;
        private Label lblSumme;
        private Button cmdAbort;
        private OplTextBox txtSumme;
        private SplitContainer splitContainer;
    }
}