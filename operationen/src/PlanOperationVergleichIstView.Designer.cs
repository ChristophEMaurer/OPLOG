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
    partial class PlanOperationVergleichIstView : OperationenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlanOperationVergleichIstView));
            this.cmdOK = new Windows.Forms.OplButton();
            this.lblVon = new System.Windows.Forms.Label();
            this.lblBis = new System.Windows.Forms.Label();
            this.txtDatumBis = new Windows.Forms.DateBoxPicker();
            this.txtDatumVon = new Windows.Forms.DateBoxPicker();
            this.lblOPFunktionen = new System.Windows.Forms.Label();
            this.cbChirurgenOPFunktionen = new System.Windows.Forms.ComboBox();
            this.cmdVergleich = new Windows.Forms.OplButton();
            this.txtOperation = new Windows.Forms.OplTextBox();
            this.lblFilterOPS = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.txtGesamtIst = new Windows.Forms.OplTextBox();
            this.lblGesamtIst = new System.Windows.Forms.Label();
            this.lvTest = new Windows.Forms.SortableListViewBalken();
            this.grpZeitraum = new System.Windows.Forms.GroupBox();
            this.chkExtern = new Windows.Forms.OplCheckBox();
            this.lvOperationen = new Windows.Forms.OplListView();
            this.chkIntern = new Windows.Forms.OplCheckBox();
            this.cmdPrint = new Windows.Forms.OplButton();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.groupBox2.SuspendLayout();
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
            // txtOperation
            // 
            resources.ApplyResources(this.txtOperation, "txtOperation");
            this.txtOperation.Name = "txtOperation";
            this.txtOperation.TextChanged += new System.EventHandler(this.txtOperation_TextChanged);
            // 
            // lblFilterOPS
            // 
            resources.ApplyResources(this.lblFilterOPS, "lblFilterOPS");
            this.lblFilterOPS.Name = "lblFilterOPS";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblInfo);
            this.groupBox2.Controls.Add(this.txtGesamtIst);
            this.groupBox2.Controls.Add(this.lblGesamtIst);
            this.groupBox2.Controls.Add(this.lvTest);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInfo.Name = "lblInfo";
            // 
            // txtGesamtIst
            // 
            resources.ApplyResources(this.txtGesamtIst, "txtGesamtIst");
            this.txtGesamtIst.Name = "txtGesamtIst";
            this.txtGesamtIst.ReadOnly = true;
            // 
            // lblGesamtIst
            // 
            resources.ApplyResources(this.lblGesamtIst, "lblGesamtIst");
            this.lblGesamtIst.Name = "lblGesamtIst";
            // 
            // lvTest
            // 
            resources.ApplyResources(this.lvTest, "lvTest");
            this.lvTest.DoubleClickActivation = false;
            this.lvTest.FullRowSelect = true;
            this.lvTest.MultiSelect = false;
            this.lvTest.Name = "lvTest";
            this.lvTest.OwnerDraw = true;
            this.lvTest.Sortable = true;
            this.lvTest.UseCompatibleStateImageBehavior = false;
            this.lvTest.View = System.Windows.Forms.View.Details;
            // 
            // grpZeitraum
            // 
            this.grpZeitraum.Controls.Add(this.chkExtern);
            this.grpZeitraum.Controls.Add(this.lvOperationen);
            this.grpZeitraum.Controls.Add(this.chkIntern);
            this.grpZeitraum.Controls.Add(this.txtDatumBis);
            this.grpZeitraum.Controls.Add(this.lblVon);
            this.grpZeitraum.Controls.Add(this.lblBis);
            this.grpZeitraum.Controls.Add(this.cbChirurgenOPFunktionen);
            this.grpZeitraum.Controls.Add(this.txtDatumVon);
            this.grpZeitraum.Controls.Add(this.lblOPFunktionen);
            this.grpZeitraum.Controls.Add(this.cmdVergleich);
            this.grpZeitraum.Controls.Add(this.txtOperation);
            this.grpZeitraum.Controls.Add(this.lblFilterOPS);
            resources.ApplyResources(this.grpZeitraum, "grpZeitraum");
            this.grpZeitraum.Name = "grpZeitraum";
            this.grpZeitraum.TabStop = false;
            // 
            // chkExtern
            // 
            resources.ApplyResources(this.chkExtern, "chkExtern");
            this.chkExtern.Name = "chkExtern";
            this.chkExtern.UseVisualStyleBackColor = true;
            // 
            // lvOperationen
            // 
            resources.ApplyResources(this.lvOperationen, "lvOperationen");
            this.lvOperationen.DoubleClickActivation = false;
            this.lvOperationen.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvOperationen.Name = "lvOperationen";
            this.lvOperationen.UseCompatibleStateImageBehavior = false;
            this.lvOperationen.SelectedIndexChanged += new System.EventHandler(this.lvOperationen_SelectedIndexChanged);
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
            this.splitContainer.Panel2.Controls.Add(this.groupBox2);
            // 
            // PlanOperationVergleichIstView
            // 
            this.AcceptButton = this.cmdVergleich;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdOK;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.cmdOK);
            this.Name = "PlanOperationVergleichIstView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.PlanOperationVergleichViewIst_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private Label lblBis;
        private GroupBox groupBox2;
        private Windows.Forms.SortableListViewBalken lvTest;
        private Button cmdVergleich;
        private TextBox txtOperation;
        private Label lblFilterOPS;
        private Label lblOPFunktionen;
        private ComboBox cbChirurgenOPFunktionen;
        private Windows.Forms.DateBoxPicker txtDatumVon;
        private Windows.Forms.DateBoxPicker txtDatumBis;
        private GroupBox grpZeitraum;
        private TextBox txtGesamtIst;
        private Label lblGesamtIst;
        private Label lblInfo;
        private OplListView lvOperationen;
        private CheckBox chkExtern;
        private CheckBox chkIntern;
        private Button cmdPrint;
        private SplitContainer splitContainer;
    }
}