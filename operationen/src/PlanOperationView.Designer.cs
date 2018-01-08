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
    partial class PlanOperationView : OperationenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlanOperationView));
            this.cmdOK = new Windows.Forms.OplButton();
            this.grpOperationen = new System.Windows.Forms.GroupBox();
            this.txtFilterText = new Windows.Forms.OplTextBox();
            this.lblFilterText = new System.Windows.Forms.Label();
            this.txtDatumBis = new Windows.Forms.DateBoxPicker();
            this.txtDatumVon = new Windows.Forms.DateBoxPicker();
            this.cmdPopulate = new Windows.Forms.OplButton();
            this.txtFilterKode = new Windows.Forms.OplTextBox();
            this.lblFilterKode = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.txtOperation = new Windows.Forms.OplTextBox();
            this.lblOperation = new System.Windows.Forms.Label();
            this.lblBis = new System.Windows.Forms.Label();
            this.lvOperationen = new Windows.Forms.OplListView();
            this.txtAnzahl = new Windows.Forms.OplTextBox();
            this.lblAnzahl = new System.Windows.Forms.Label();
            this.lblVon = new System.Windows.Forms.Label();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.grpOperationen.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // grpOperationen
            // 
            resources.ApplyResources(this.grpOperationen, "grpOperationen");
            this.grpOperationen.Controls.Add(this.txtFilterText);
            this.grpOperationen.Controls.Add(this.lblFilterText);
            this.grpOperationen.Controls.Add(this.txtDatumBis);
            this.grpOperationen.Controls.Add(this.txtDatumVon);
            this.grpOperationen.Controls.Add(this.cmdPopulate);
            this.grpOperationen.Controls.Add(this.txtFilterKode);
            this.grpOperationen.Controls.Add(this.lblFilterKode);
            this.grpOperationen.Controls.Add(this.lblInfo);
            this.grpOperationen.Controls.Add(this.txtOperation);
            this.grpOperationen.Controls.Add(this.lblOperation);
            this.grpOperationen.Controls.Add(this.lblBis);
            this.grpOperationen.Controls.Add(this.lvOperationen);
            this.grpOperationen.Controls.Add(this.txtAnzahl);
            this.grpOperationen.Controls.Add(this.lblAnzahl);
            this.grpOperationen.Controls.Add(this.lblVon);
            this.grpOperationen.Name = "grpOperationen";
            this.grpOperationen.TabStop = false;
            // 
            // txtFilterText
            // 
            resources.ApplyResources(this.txtFilterText, "txtFilterText");
            this.txtFilterText.Name = "txtFilterText";
            // 
            // lblFilterText
            // 
            resources.ApplyResources(this.lblFilterText, "lblFilterText");
            this.lblFilterText.Name = "lblFilterText";
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
            // cmdPopulate
            // 
            resources.ApplyResources(this.cmdPopulate, "cmdPopulate");
            this.cmdPopulate.Name = "cmdPopulate";
            this.cmdPopulate.UseVisualStyleBackColor = true;
            this.cmdPopulate.Click += new System.EventHandler(this.cmdPopulate_Click);
            // 
            // txtFilterKode
            // 
            resources.ApplyResources(this.txtFilterKode, "txtFilterKode");
            this.txtFilterKode.Name = "txtFilterKode";
            // 
            // lblFilterKode
            // 
            resources.ApplyResources(this.lblFilterKode, "lblFilterKode");
            this.lblFilterKode.Name = "lblFilterKode";
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            // 
            // txtOperation
            // 
            resources.ApplyResources(this.txtOperation, "txtOperation");
            this.txtOperation.Name = "txtOperation";
            this.txtOperation.TextChanged += new System.EventHandler(this.txtOperation_TextChanged);
            // 
            // lblOperation
            // 
            resources.ApplyResources(this.lblOperation, "lblOperation");
            this.lblOperation.Name = "lblOperation";
            // 
            // lblBis
            // 
            resources.ApplyResources(this.lblBis, "lblBis");
            this.lblBis.Name = "lblBis";
            // 
            // lvOperationen
            // 
            resources.ApplyResources(this.lvOperationen, "lvOperationen");
            this.lvOperationen.BalkenGrafik = false;
            this.lvOperationen.Name = "lvOperationen";
            this.lvOperationen.Sortable = false;
            this.lvOperationen.UseCompatibleStateImageBehavior = false;
            this.lvOperationen.DoubleClick += new System.EventHandler(this.lvOperationen_DoubleClick);
            // 
            // txtAnzahl
            // 
            resources.ApplyResources(this.txtAnzahl, "txtAnzahl");
            this.txtAnzahl.Name = "txtAnzahl";
            // 
            // lblAnzahl
            // 
            resources.ApplyResources(this.lblAnzahl, "lblAnzahl");
            this.lblAnzahl.Name = "lblAnzahl";
            // 
            // lblVon
            // 
            resources.ApplyResources(this.lblVon, "lblVon");
            this.lblVon.Name = "lblVon";
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // PlanOperationView
            // 
            this.AcceptButton = this.cmdPopulate;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.grpOperationen);
            this.MaximizeBox = false;
            this.Name = "PlanOperationView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.PlanOperationView_Load);
            this.grpOperationen.ResumeLayout(false);
            this.grpOperationen.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.OplButton cmdOK;
        private System.Windows.Forms.GroupBox grpOperationen;
        private Windows.Forms.OplListView lvOperationen;
        private TextBox txtAnzahl;
        private Label lblAnzahl;
        private Label lblVon;
        private Button cmdCancel;
        private Label lblBis;
        private TextBox txtOperation;
        private Label lblOperation;
        private Label lblInfo;
        private Button cmdPopulate;
        private TextBox txtFilterKode;
        private Label lblFilterKode;
        private DateBoxPicker txtDatumBis;
        private DateBoxPicker txtDatumVon;
        private TextBox txtFilterText;
        private Label lblFilterText;
    }
}