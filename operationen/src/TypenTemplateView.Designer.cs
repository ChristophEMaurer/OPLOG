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
    partial class TypenTemplateView : OperationenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TypenTemplateView));
            this.grpTypen = new System.Windows.Forms.GroupBox();
            this.cmdApply = new Windows.Forms.OplButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdDelete = new Windows.Forms.OplButton();
            this.cmdInsert = new Windows.Forms.OplButton();
            this.lblTypen = new System.Windows.Forms.Label();
            this.lvTypen = new Windows.Forms.OplListView();
            this.txtTypen = new Windows.Forms.OplTextBox();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.lblInfo = new System.Windows.Forms.Label();
            this.grpDemo = new System.Windows.Forms.GroupBox();
            this.lblDemoTitle = new System.Windows.Forms.Label();
            this.cbDemo = new System.Windows.Forms.ComboBox();
            this.lblDemo = new System.Windows.Forms.Label();
            this.grpTypen.SuspendLayout();
            this.grpDemo.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpTypen
            // 
            resources.ApplyResources(this.grpTypen, "grpTypen");
            this.grpTypen.Controls.Add(this.cmdApply);
            this.grpTypen.Controls.Add(this.label1);
            this.grpTypen.Controls.Add(this.cmdDelete);
            this.grpTypen.Controls.Add(this.cmdInsert);
            this.grpTypen.Controls.Add(this.lblTypen);
            this.grpTypen.Controls.Add(this.lvTypen);
            this.grpTypen.Controls.Add(this.txtTypen);
            this.grpTypen.Name = "grpTypen";
            this.grpTypen.TabStop = false;
            // 
            // cmdApply
            // 
            resources.ApplyResources(this.cmdApply, "cmdApply");
            this.cmdApply.Name = "cmdApply";
            this.cmdApply.SecurityManager = null;
            this.cmdApply.UserRight = null;
            this.cmdApply.UseVisualStyleBackColor = true;
            this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Name = "label1";
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
            // cmdInsert
            // 
            resources.ApplyResources(this.cmdInsert, "cmdInsert");
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.SecurityManager = null;
            this.cmdInsert.UserRight = null;
            this.cmdInsert.UseVisualStyleBackColor = true;
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // lblTypen
            // 
            resources.ApplyResources(this.lblTypen, "lblTypen");
            this.lblTypen.Name = "lblTypen";
            // 
            // lvTypen
            // 
            resources.ApplyResources(this.lvTypen, "lvTypen");
            this.lvTypen.BalkenGrafik = false;
            this.lvTypen.FullRowSelect = true;
            this.lvTypen.Name = "lvTypen";
            this.lvTypen.Sortable = false;
            this.lvTypen.UseCompatibleStateImageBehavior = false;
            this.lvTypen.SelectedIndexChanged += new System.EventHandler(this.lvTypen_SelectedIndexChanged);
            // 
            // txtTypen
            // 
            resources.ApplyResources(this.txtTypen, "txtTypen");
            this.txtTypen.Name = "txtTypen";
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            // 
            // grpDemo
            // 
            resources.ApplyResources(this.grpDemo, "grpDemo");
            this.grpDemo.Controls.Add(this.lblDemoTitle);
            this.grpDemo.Controls.Add(this.cbDemo);
            this.grpDemo.Controls.Add(this.lblDemo);
            this.grpDemo.Name = "grpDemo";
            this.grpDemo.TabStop = false;
            // 
            // lblDemoTitle
            // 
            resources.ApplyResources(this.lblDemoTitle, "lblDemoTitle");
            this.lblDemoTitle.Name = "lblDemoTitle";
            // 
            // cbDemo
            // 
            resources.ApplyResources(this.cbDemo, "cbDemo");
            this.cbDemo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDemo.FormattingEnabled = true;
            this.cbDemo.Name = "cbDemo";
            // 
            // lblDemo
            // 
            resources.ApplyResources(this.lblDemo, "lblDemo");
            this.lblDemo.Name = "lblDemo";
            // 
            // TypenTemplateView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.grpDemo);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.grpTypen);
            this.Name = "TypenTemplateView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.TypenTemplateView_Load);
            this.grpTypen.ResumeLayout(false);
            this.grpTypen.PerformLayout();
            this.grpDemo.ResumeLayout(false);
            this.grpDemo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpTypen;
        private OplListView lvTypen;
        private Button cmdCancel;

        protected OplButton cmdDelete;
        protected OplButton cmdInsert;
        private Label label1;
        protected OplButton cmdApply;
        private GroupBox grpDemo;
        private ComboBox cbDemo;
        private Label lblDemo;
        private Label lblDemoTitle;
        protected OplTextBox txtTypen;
        protected Label lblTypen;
        protected Label lblInfo;
    }
}