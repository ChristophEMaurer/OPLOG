using System;
using System.Collections.Generic;
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
    partial class KlinischeErgebnisseView : OperationenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KlinischeErgebnisseView));
            this.cmdOK = new Windows.Forms.OplButton();
            this.lblVon = new System.Windows.Forms.Label();
            this.lblBis = new System.Windows.Forms.Label();
            this.txtDatumBis = new Windows.Forms.DateBoxPicker();
            this.txtDatumVon = new Windows.Forms.DateBoxPicker();
            this.lblOPFunktionen = new System.Windows.Forms.Label();
            this.cbChirurgenOPFunktionen = new System.Windows.Forms.ComboBox();
            this.cmdVergleich = new Windows.Forms.OplButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtGesamt = new Windows.Forms.OplTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblGesamt = new System.Windows.Forms.Label();
            this.lvTest = new SortableListViewBalken();
            this.grpZeitraum = new System.Windows.Forms.GroupBox();
            this.txtOPSKode = new Windows.Forms.OplTextBox();
            this.lblOPSKode = new System.Windows.Forms.Label();
            this.cbKlinischeErgebnisseTypen = new System.Windows.Forms.ComboBox();
            this.lblKlinischeErgebnisseTypen = new System.Windows.Forms.Label();
            this.chkExtern = new Windows.Forms.OplCheckBox();
            this.chkIntern = new Windows.Forms.OplCheckBox();
            this.cmdPrint = new Windows.Forms.OplButton();
            this.groupBox2.SuspendLayout();
            this.grpZeitraum.SuspendLayout();
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
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.txtGesamt);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblGesamt);
            this.groupBox2.Controls.Add(this.lvTest);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // txtGesamt
            // 
            resources.ApplyResources(this.txtGesamt, "txtGesamt");
            this.txtGesamt.BackColor = System.Drawing.SystemColors.Control;
            this.txtGesamt.Name = "txtGesamt";
            this.txtGesamt.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lblGesamt
            // 
            resources.ApplyResources(this.lblGesamt, "lblGesamt");
            this.lblGesamt.Name = "lblGesamt";
            // 
            // lvTest
            // 
            resources.ApplyResources(this.lvTest, "lvTest");
            this.lvTest.FullRowSelect = true;
            this.lvTest.MultiSelect = false;
            this.lvTest.Name = "lvTest";
            this.lvTest.OwnerDraw = true;
            this.lvTest.UseCompatibleStateImageBehavior = false;
            this.lvTest.View = System.Windows.Forms.View.Details;
            // 
            // grpZeitraum
            // 
            resources.ApplyResources(this.grpZeitraum, "grpZeitraum");
            this.grpZeitraum.Controls.Add(this.txtOPSKode);
            this.grpZeitraum.Controls.Add(this.lblOPSKode);
            this.grpZeitraum.Controls.Add(this.cbKlinischeErgebnisseTypen);
            this.grpZeitraum.Controls.Add(this.lblKlinischeErgebnisseTypen);
            this.grpZeitraum.Controls.Add(this.chkExtern);
            this.grpZeitraum.Controls.Add(this.chkIntern);
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
            // txtOPSKode
            // 
            this.txtOPSKode.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.txtOPSKode, "txtOPSKode");
            this.txtOPSKode.Name = "txtOPSKode";
            // 
            // lblOPSKode
            // 
            resources.ApplyResources(this.lblOPSKode, "lblOPSKode");
            this.lblOPSKode.Name = "lblOPSKode";
            // 
            // cbKlinischeErgebnisseTypen
            // 
            this.cbKlinischeErgebnisseTypen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKlinischeErgebnisseTypen.FormattingEnabled = true;
            resources.ApplyResources(this.cbKlinischeErgebnisseTypen, "cbKlinischeErgebnisseTypen");
            this.cbKlinischeErgebnisseTypen.Name = "cbKlinischeErgebnisseTypen";
            // 
            // lblKlinischeErgebnisseTypen
            // 
            resources.ApplyResources(this.lblKlinischeErgebnisseTypen, "lblKlinischeErgebnisseTypen");
            this.lblKlinischeErgebnisseTypen.Name = "lblKlinischeErgebnisseTypen";
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
            // cmdPrint
            // 
            resources.ApplyResources(this.cmdPrint, "cmdPrint");
            this.cmdPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // KlinischeErgebnisseView
            // 
            this.AcceptButton = this.cmdVergleich;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdOK;
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.grpZeitraum);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmdOK);
            this.Name = "KlinischeErgebnisseView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.KlinischeErgebnisseView_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpZeitraum.ResumeLayout(false);
            this.grpZeitraum.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.OplButton cmdOK;
        private Label lblVon;
        private Label lblBis;
        private GroupBox groupBox2;
        private SortableListViewBalken lvTest;
        private Button cmdVergleich;
        private Label lblOPFunktionen;
        private ComboBox cbChirurgenOPFunktionen;
        private DateBoxPicker txtDatumVon;
        private DateBoxPicker txtDatumBis;
        private GroupBox grpZeitraum;
        private CheckBox chkExtern;
        private CheckBox chkIntern;
        private Button cmdPrint;
        private ComboBox cbKlinischeErgebnisseTypen;
        private Label lblKlinischeErgebnisseTypen;
        private Label lblGesamt;
        private Label label1;
        private TextBox txtOPSKode;
        private Label lblOPSKode;
        private TextBox txtGesamt;
    }
}