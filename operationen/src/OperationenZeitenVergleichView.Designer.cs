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
    partial class OperationenZeitenVergleichView : OperationenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OperationenZeitenVergleichView));
            this.cmdCancel = new Windows.Forms.OplButton();
            this.lblVon = new System.Windows.Forms.Label();
            this.lblBis = new System.Windows.Forms.Label();
            this.txtDatumBis = new Windows.Forms.DateBoxPicker();
            this.txtDatumVon = new Windows.Forms.DateBoxPicker();
            this.lblOPFunktionen = new System.Windows.Forms.Label();
            this.cbChirurgenOPFunktionen = new System.Windows.Forms.ComboBox();
            this.cmdVergleich = new Windows.Forms.OplButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtGesamtIst = new Windows.Forms.OplTextBox();
            this.lblGesamt = new System.Windows.Forms.Label();
            this.lvTest = new Windows.Forms.ListViewBalken();
            this.grpZeitraum = new System.Windows.Forms.GroupBox();
            this.chkExtern = new Windows.Forms.OplCheckBox();
            this.chkIntern = new Windows.Forms.OplCheckBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.cmdPrint = new Windows.Forms.OplButton();
            this.groupBox2.SuspendLayout();
            this.grpZeitraum.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
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
            this.groupBox2.Controls.Add(this.txtGesamtIst);
            this.groupBox2.Controls.Add(this.lblGesamt);
            this.groupBox2.Controls.Add(this.lvTest);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // txtGesamtIst
            // 
            resources.ApplyResources(this.txtGesamtIst, "txtGesamtIst");
            this.txtGesamtIst.Name = "txtGesamtIst";
            this.txtGesamtIst.ReadOnly = true;
            // 
            // lblGesamt
            // 
            resources.ApplyResources(this.lblGesamt, "lblGesamt");
            this.lblGesamt.Name = "lblGesamt";
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
            // grpZeitraum
            // 
            resources.ApplyResources(this.grpZeitraum, "grpZeitraum");
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
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            // 
            // cmdPrint
            // 
            resources.ApplyResources(this.cmdPrint, "cmdPrint");
            this.cmdPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // OperationenZeitenVergleichView
            // 
            this.AcceptButton = this.cmdVergleich;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.grpZeitraum);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmdCancel);
            this.Name = "OperationenZeitenVergleichView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.OperationenZeitenVergleichView_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpZeitraum.ResumeLayout(false);
            this.grpZeitraum.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.OplButton cmdCancel;
        private Label lblVon;
        private Label lblBis;
        private GroupBox groupBox2;
        private ListViewBalken lvTest;
        private Button cmdVergleich;
        private Label lblOPFunktionen;
        private ComboBox cbChirurgenOPFunktionen;
        private DateBoxPicker txtDatumVon;
        private DateBoxPicker txtDatumBis;
        private GroupBox grpZeitraum;
        private TextBox txtGesamtIst;
        private Label lblGesamt;
        private Label lblInfo;
        private CheckBox chkExtern;
        private CheckBox chkIntern;
        private Button cmdPrint;
    }
}