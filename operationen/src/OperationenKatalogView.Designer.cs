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
    partial class OperationenKatalogView : OperationenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OperationenKatalogView));
            this.grpOperationen = new System.Windows.Forms.GroupBox();
            this.txtFilterOPSCode = new Windows.Forms.OplTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblInfoSearch = new System.Windows.Forms.Label();
            this.cmdPopulate = new Windows.Forms.OplButton();
            this.txtFilterOPSText = new Windows.Forms.OplTextBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.cmdDelete = new Windows.Forms.OplButton();
            this.cmdInsert = new Windows.Forms.OplButton();
            this.lblText = new System.Windows.Forms.Label();
            this.lblKode = new System.Windows.Forms.Label();
            this.txtText = new Windows.Forms.OplTextBox();
            this.lvOperationen = new Windows.Forms.OplListView();
            this.txtKode = new Windows.Forms.OplTextBox();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.cmdDeleteAll = new Windows.Forms.OplButton();
            this.lblInfo = new System.Windows.Forms.LinkLabel();
            this.grpOperationen.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpOperationen
            // 
            resources.ApplyResources(this.grpOperationen, "grpOperationen");
            this.grpOperationen.Controls.Add(this.txtFilterOPSCode);
            this.grpOperationen.Controls.Add(this.label1);
            this.grpOperationen.Controls.Add(this.lblInfoSearch);
            this.grpOperationen.Controls.Add(this.cmdPopulate);
            this.grpOperationen.Controls.Add(this.txtFilterOPSText);
            this.grpOperationen.Controls.Add(this.lblFilter);
            this.grpOperationen.Controls.Add(this.cmdDelete);
            this.grpOperationen.Controls.Add(this.cmdInsert);
            this.grpOperationen.Controls.Add(this.lblText);
            this.grpOperationen.Controls.Add(this.lblKode);
            this.grpOperationen.Controls.Add(this.txtText);
            this.grpOperationen.Controls.Add(this.lvOperationen);
            this.grpOperationen.Controls.Add(this.txtKode);
            this.grpOperationen.Name = "grpOperationen";
            this.grpOperationen.TabStop = false;
            // 
            // txtFilterOPSCode
            // 
            resources.ApplyResources(this.txtFilterOPSCode, "txtFilterOPSCode");
            this.txtFilterOPSCode.Name = "txtFilterOPSCode";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lblInfoSearch
            // 
            this.lblInfoSearch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.lblInfoSearch, "lblInfoSearch");
            this.lblInfoSearch.Name = "lblInfoSearch";
            // 
            // cmdPopulate
            // 
            resources.ApplyResources(this.cmdPopulate, "cmdPopulate");
            this.cmdPopulate.Name = "cmdPopulate";
            this.cmdPopulate.UseVisualStyleBackColor = true;
            this.cmdPopulate.Click += new System.EventHandler(this.cmdPopulate_Click);
            // 
            // txtFilterOPSText
            // 
            resources.ApplyResources(this.txtFilterOPSText, "txtFilterOPSText");
            this.txtFilterOPSText.Name = "txtFilterOPSText";
            // 
            // lblFilter
            // 
            resources.ApplyResources(this.lblFilter, "lblFilter");
            this.lblFilter.Name = "lblFilter";
            // 
            // cmdDelete
            // 
            resources.ApplyResources(this.cmdDelete, "cmdDelete");
            this.cmdDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.SecurityManager = null;
            this.cmdDelete.UserRight = null;
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdInsert
            // 
            resources.ApplyResources(this.cmdInsert, "cmdInsert");
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.SecurityManager = null;
            this.cmdInsert.UserRight = null;
            this.cmdInsert.UseVisualStyleBackColor = true;
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // lblText
            // 
            resources.ApplyResources(this.lblText, "lblText");
            this.lblText.Name = "lblText";
            // 
            // lblKode
            // 
            resources.ApplyResources(this.lblKode, "lblKode");
            this.lblKode.Name = "lblKode";
            // 
            // txtText
            // 
            resources.ApplyResources(this.txtText, "txtText");
            this.txtText.Name = "txtText";
            // 
            // lvOperationen
            // 
            resources.ApplyResources(this.lvOperationen, "lvOperationen");
            this.lvOperationen.FullRowSelect = true;
            this.lvOperationen.Name = "lvOperationen";
            this.lvOperationen.UseCompatibleStateImageBehavior = false;
            this.lvOperationen.SelectedIndexChanged += new System.EventHandler(this.lvOperationen_SelectedIndexChanged);
            // 
            // txtKode
            // 
            resources.ApplyResources(this.txtKode, "txtKode");
            this.txtKode.Name = "txtKode";
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdDeleteAll
            // 
            resources.ApplyResources(this.cmdDeleteAll, "cmdDeleteAll");
            this.cmdDeleteAll.Name = "cmdDeleteAll";
            this.cmdDeleteAll.SecurityManager = null;
            this.cmdDeleteAll.UserRight = null;
            this.cmdDeleteAll.UseVisualStyleBackColor = true;
            this.cmdDeleteAll.Click += new System.EventHandler(this.cmdDeleteAll_Click);
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.TabStop = true;
            this.lblInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblInfo_LinkClicked);
            // 
            // OperationenKatalogView
            // 
            this.AcceptButton = this.cmdPopulate;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.cmdDeleteAll);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.grpOperationen);
            this.Name = "OperationenKatalogView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.OperationenKatalogView_Load);
            this.Shown += new System.EventHandler(this.OperationenKatalogView_Shown);
            this.Resize += new System.EventHandler(this.OperationenKatalogView_Resize);
            this.grpOperationen.ResumeLayout(false);
            this.grpOperationen.PerformLayout();
            this.ResumeLayout(false);

        }

        void cmdCancel_Click(object sender, EventArgs e)
        {
            base.CancelClicked();
        }

        #endregion

        private System.Windows.Forms.GroupBox grpOperationen;
        private Windows.Forms.OplListView lvOperationen;
        private OplTextBox txtKode;
        private Button cmdCancel;
        private OplTextBox txtText;
        protected override void Object2Control() { }

        private OplButton cmdDelete;
        private OplButton cmdInsert;
        private Label lblText;
        private Label lblKode;
        private Button cmdPopulate;
        private TextBox txtFilterOPSText;
        private Label lblFilter;
        private OplButton cmdDeleteAll;
        private LinkLabel lblInfo;
        private Label lblInfoSearch;
        private Label label1;
        private TextBox txtFilterOPSCode;
    }
}