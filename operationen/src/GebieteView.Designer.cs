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
    partial class GebieteView : OperationenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GebieteView));
            this.grpGebiete = new System.Windows.Forms.GroupBox();
            this.cmdClear = new Windows.Forms.OplButton();
            this.cmdApply = new Windows.Forms.OplButton();
            this.txtBemerkung = new Windows.Forms.OplTextBox();
            this.txtHerkunft = new Windows.Forms.OplTextBox();
            this.lblBemerkung = new System.Windows.Forms.Label();
            this.lblHerkunft = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdDelete = new Windows.Forms.OplButton();
            this.cmdInsert = new Windows.Forms.OplButton();
            this.lblGebiet = new System.Windows.Forms.Label();
            this.lvGebiete = new Windows.Forms.OplListView();
            this.txtGebiet = new Windows.Forms.OplTextBox();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.lblInfo = new System.Windows.Forms.Label();
            this.grpGebiete.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpGebiete
            // 
            resources.ApplyResources(this.grpGebiete, "grpGebiete");
            this.grpGebiete.Controls.Add(this.cmdClear);
            this.grpGebiete.Controls.Add(this.cmdApply);
            this.grpGebiete.Controls.Add(this.txtBemerkung);
            this.grpGebiete.Controls.Add(this.txtHerkunft);
            this.grpGebiete.Controls.Add(this.lblBemerkung);
            this.grpGebiete.Controls.Add(this.lblHerkunft);
            this.grpGebiete.Controls.Add(this.label1);
            this.grpGebiete.Controls.Add(this.cmdDelete);
            this.grpGebiete.Controls.Add(this.cmdInsert);
            this.grpGebiete.Controls.Add(this.lblGebiet);
            this.grpGebiete.Controls.Add(this.lvGebiete);
            this.grpGebiete.Controls.Add(this.txtGebiet);
            this.grpGebiete.Name = "grpGebiete";
            this.grpGebiete.TabStop = false;
            // 
            // cmdClear
            // 
            resources.ApplyResources(this.cmdClear, "cmdClear");
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.SecurityManager = null;
            this.cmdClear.UserRight = null;
            this.cmdClear.UseVisualStyleBackColor = true;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
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
            // txtBemerkung
            // 
            resources.ApplyResources(this.txtBemerkung, "txtBemerkung");
            this.txtBemerkung.Name = "txtBemerkung";
            this.txtBemerkung.ProtectContents = false;
            // 
            // txtHerkunft
            // 
            resources.ApplyResources(this.txtHerkunft, "txtHerkunft");
            this.txtHerkunft.Name = "txtHerkunft";
            this.txtHerkunft.ProtectContents = false;
            // 
            // lblBemerkung
            // 
            resources.ApplyResources(this.lblBemerkung, "lblBemerkung");
            this.lblBemerkung.Name = "lblBemerkung";
            // 
            // lblHerkunft
            // 
            resources.ApplyResources(this.lblHerkunft, "lblHerkunft");
            this.lblHerkunft.Name = "lblHerkunft";
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
            // lblGebiet
            // 
            resources.ApplyResources(this.lblGebiet, "lblGebiet");
            this.lblGebiet.Name = "lblGebiet";
            // 
            // lvGebiete
            // 
            resources.ApplyResources(this.lvGebiete, "lvGebiete");
            this.lvGebiete.DoubleClickActivation = false;
            this.lvGebiete.FullRowSelect = true;
            this.lvGebiete.Name = "lvGebiete";
            this.lvGebiete.UseCompatibleStateImageBehavior = false;
            this.lvGebiete.SelectedIndexChanged += new System.EventHandler(this.lvGebiete_SelectedIndexChanged);
            // 
            // txtGebiet
            // 
            resources.ApplyResources(this.txtGebiet, "txtGebiet");
            this.txtGebiet.Name = "txtGebiet";
            this.txtGebiet.ProtectContents = false;
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.SecurityManager = null;
            this.cmdCancel.UserRight = null;
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            // 
            // GebieteView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.grpGebiete);
            this.Name = "GebieteView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.GebieteView_Load);
            this.Resize += new System.EventHandler(this.GebieteView_Resize);
            this.grpGebiete.ResumeLayout(false);
            this.grpGebiete.PerformLayout();
            this.ResumeLayout(false);

        }

        void cmdCancel_Click(object sender, EventArgs e)
        {
            base.CancelClicked();
        }

        #endregion

        private System.Windows.Forms.GroupBox grpGebiete;
        private OplListView lvGebiete;
        private OplTextBox txtGebiet;

        private OplButton cmdDelete;
        private OplButton cmdInsert;
        private Label lblGebiet;
        private Label label1;
        private OplTextBox txtBemerkung;
        private OplTextBox txtHerkunft;
        private Label lblBemerkung;
        private Label lblHerkunft;
        private OplButton cmdApply;
        private OplButton cmdClear;
        private Label lblInfo;
        private OplButton cmdCancel;
    }
}