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
    partial class OPDauerFortschrittView : OperationenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OPDauerFortschrittView));
            this.cmdOK = new Windows.Forms.OplButton();
            this.lblVon = new System.Windows.Forms.Label();
            this.lblBis = new System.Windows.Forms.Label();
            this.txtDatumBis = new Windows.Forms.DateBoxPicker();
            this.txtDatumVon = new Windows.Forms.DateBoxPicker();
            this.lblOPFunktionen = new System.Windows.Forms.Label();
            this.cbChirurgenOPFunktionen = new System.Windows.Forms.ComboBox();
            this.cmdVergleich = new Windows.Forms.OplButton();
            this.grpTest = new System.Windows.Forms.GroupBox();
            this.lvTest = new Windows.Forms.SortableListView();
            this.radJahr = new Windows.Forms.OplRadioButton();
            this.radAlle = new Windows.Forms.OplRadioButton();
            this.grpZeitraum = new System.Windows.Forms.GroupBox();
            this.cmdAbort = new Windows.Forms.OplButton();
            this.chkExtern = new Windows.Forms.OplCheckBox();
            this.chkIntern = new Windows.Forms.OplCheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMonate = new System.Windows.Forms.Label();
            this.txtMonate = new Windows.Forms.OplTextBox();
            this.radMonate = new Windows.Forms.OplRadioButton();
            this.lblOPSKode = new System.Windows.Forms.Label();
            this.txtOPSKode = new Windows.Forms.OplTextBox();
            this.lblChirurg = new System.Windows.Forms.Label();
            this.cbChirurgen = new System.Windows.Forms.ComboBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.txtGesamt = new Windows.Forms.OplTextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.cmdPrint = new Windows.Forms.OplButton();
            this.grpTest.SuspendLayout();
            this.grpZeitraum.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // lblVon
            // 
            resources.ApplyResources(this.lblVon, "lblVon");
            this.lblVon.Name = "lblVon";
            // 
            // lblBis
            // 
            resources.ApplyResources(this.lblBis, "lblBis");
            this.lblBis.Name = "lblBis";
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
            // lblOPFunktionen
            // 
            resources.ApplyResources(this.lblOPFunktionen, "lblOPFunktionen");
            this.lblOPFunktionen.Name = "lblOPFunktionen";
            // 
            // cbChirurgenOPFunktionen
            // 
            this.cbChirurgenOPFunktionen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChirurgenOPFunktionen.FormattingEnabled = true;
            resources.ApplyResources(this.cbChirurgenOPFunktionen, "cbChirurgenOPFunktionen");
            this.cbChirurgenOPFunktionen.Name = "cbChirurgenOPFunktionen";
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
            resources.ApplyResources(this.grpTest, "grpTest");
            this.grpTest.Controls.Add(this.lvTest);
            this.grpTest.Name = "grpTest";
            this.grpTest.TabStop = false;
            // 
            // lvTest
            // 
            resources.ApplyResources(this.lvTest, "lvTest");
            this.lvTest.BalkenGrafik = false;
            this.lvTest.Name = "lvTest";
            this.lvTest.Sortable = true;
            this.lvTest.UseCompatibleStateImageBehavior = false;
            this.lvTest.View = System.Windows.Forms.View.Details;
            this.lvTest.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvTest_MouseDoubleClick);
            // 
            // radJahr
            // 
            resources.ApplyResources(this.radJahr, "radJahr");
            this.radJahr.Name = "radJahr";
            this.radJahr.TabStop = true;
            this.radJahr.UseVisualStyleBackColor = true;
            this.radJahr.CheckedChanged += new System.EventHandler(this.radJahr_CheckedChanged);
            // 
            // radAlle
            // 
            resources.ApplyResources(this.radAlle, "radAlle");
            this.radAlle.Name = "radAlle";
            this.radAlle.TabStop = true;
            this.radAlle.UseVisualStyleBackColor = true;
            this.radAlle.CheckedChanged += new System.EventHandler(this.radAlle_CheckedChanged);
            // 
            // grpZeitraum
            // 
            resources.ApplyResources(this.grpZeitraum, "grpZeitraum");
            this.grpZeitraum.Controls.Add(this.cmdAbort);
            this.grpZeitraum.Controls.Add(this.chkExtern);
            this.grpZeitraum.Controls.Add(this.chkIntern);
            this.grpZeitraum.Controls.Add(this.groupBox1);
            this.grpZeitraum.Controls.Add(this.lblOPSKode);
            this.grpZeitraum.Controls.Add(this.txtOPSKode);
            this.grpZeitraum.Controls.Add(this.lblChirurg);
            this.grpZeitraum.Controls.Add(this.cbChirurgen);
            this.grpZeitraum.Controls.Add(this.txtDatumBis);
            this.grpZeitraum.Controls.Add(this.lblVon);
            this.grpZeitraum.Controls.Add(this.lblBis);
            this.grpZeitraum.Controls.Add(this.cbChirurgenOPFunktionen);
            this.grpZeitraum.Controls.Add(this.txtDatumVon);
            this.grpZeitraum.Controls.Add(this.lblOPFunktionen);
            this.grpZeitraum.Controls.Add(this.cmdVergleich);
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
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.lblMonate);
            this.groupBox1.Controls.Add(this.txtMonate);
            this.groupBox1.Controls.Add(this.radMonate);
            this.groupBox1.Controls.Add(this.radJahr);
            this.groupBox1.Controls.Add(this.radAlle);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // lblMonate
            // 
            resources.ApplyResources(this.lblMonate, "lblMonate");
            this.lblMonate.Name = "lblMonate";
            // 
            // txtMonate
            // 
            resources.ApplyResources(this.txtMonate, "txtMonate");
            this.txtMonate.Name = "txtMonate";
            // 
            // radMonate
            // 
            resources.ApplyResources(this.radMonate, "radMonate");
            this.radMonate.Name = "radMonate";
            this.radMonate.TabStop = true;
            this.radMonate.UseVisualStyleBackColor = true;
            this.radMonate.CheckedChanged += new System.EventHandler(this.radMonate_CheckedChanged);
            // 
            // lblOPSKode
            // 
            resources.ApplyResources(this.lblOPSKode, "lblOPSKode");
            this.lblOPSKode.Name = "lblOPSKode";
            // 
            // txtOPSKode
            // 
            resources.ApplyResources(this.txtOPSKode, "txtOPSKode");
            this.txtOPSKode.Name = "txtOPSKode";
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
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            // 
            // txtGesamt
            // 
            resources.ApplyResources(this.txtGesamt, "txtGesamt");
            this.txtGesamt.BackColor = System.Drawing.SystemColors.Control;
            this.txtGesamt.Name = "txtGesamt";
            this.txtGesamt.ReadOnly = true;
            // 
            // lblTotal
            // 
            resources.ApplyResources(this.lblTotal, "lblTotal");
            this.lblTotal.Name = "lblTotal";
            // 
            // cmdPrint
            // 
            resources.ApplyResources(this.cmdPrint, "cmdPrint");
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // OPDauerFortschrittView
            // 
            this.AcceptButton = this.cmdVergleich;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdOK;
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.txtGesamt);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.grpZeitraum);
            this.Controls.Add(this.grpTest);
            this.Controls.Add(this.cmdOK);
            this.Name = "OPDauerFortschrittView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.OPDauerFortschrittView_Load);
            this.grpTest.ResumeLayout(false);
            this.grpZeitraum.ResumeLayout(false);
            this.grpZeitraum.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Windows.Forms.OplButton cmdOK;
        private Label lblVon;
        private Label lblBis;
        private GroupBox grpTest;
        private SortableListView lvTest;
        private Button cmdVergleich;
        private Label lblOPFunktionen;
        private ComboBox cbChirurgenOPFunktionen;
        private Windows.Forms.DateBoxPicker txtDatumVon;
        private Windows.Forms.DateBoxPicker txtDatumBis;
        private GroupBox grpZeitraum;
        private Label lblChirurg;
        private ComboBox cbChirurgen;
        private RadioButton radAlle;
        private RadioButton radJahr;
        private Label lblOPSKode;
        private TextBox txtOPSKode;
        private GroupBox groupBox1;
        private RadioButton radMonate;
        private Label lblMonate;
        private TextBox txtMonate;
        private Label lblInfo;
        private TextBox txtGesamt;
        private Label lblTotal;
        private CheckBox chkExtern;
        private CheckBox chkIntern;
        private Button cmdAbort;
        private Button cmdPrint;
    }
}