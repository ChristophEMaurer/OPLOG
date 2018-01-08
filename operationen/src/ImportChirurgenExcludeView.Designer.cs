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
    partial class ImportChirurgenExcludeView : OperationenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportChirurgenExcludeView));
            this.grpData = new System.Windows.Forms.GroupBox();
            this.lblVorname = new System.Windows.Forms.Label();
            this.txtVorname = new Windows.Forms.OplTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdDelete = new Windows.Forms.OplButton();
            this.cmdInsert = new Windows.Forms.OplButton();
            this.lblNachname = new System.Windows.Forms.Label();
            this.lvChirurgen = new Windows.Forms.OplListView();
            this.txtNachname = new Windows.Forms.OplTextBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.grpData.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpData
            // 
            resources.ApplyResources(this.grpData, "grpData");
            this.grpData.Controls.Add(this.lblVorname);
            this.grpData.Controls.Add(this.txtVorname);
            this.grpData.Controls.Add(this.label1);
            this.grpData.Controls.Add(this.cmdDelete);
            this.grpData.Controls.Add(this.cmdInsert);
            this.grpData.Controls.Add(this.lblNachname);
            this.grpData.Controls.Add(this.lvChirurgen);
            this.grpData.Controls.Add(this.txtNachname);
            this.grpData.Name = "grpData";
            this.grpData.TabStop = false;
            // 
            // lblVorname
            // 
            resources.ApplyResources(this.lblVorname, "lblVorname");
            this.lblVorname.Name = "lblVorname";
            // 
            // txtVorname
            // 
            resources.ApplyResources(this.txtVorname, "txtVorname");
            this.txtVorname.Name = "txtVorname";
            this.txtVorname.ProtectContents = false;
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
            // lblNachname
            // 
            resources.ApplyResources(this.lblNachname, "lblNachname");
            this.lblNachname.Name = "lblNachname";
            // 
            // lvChirurgen
            // 
            resources.ApplyResources(this.lvChirurgen, "lvChirurgen");
            this.lvChirurgen.DoubleClickActivation = false;
            this.lvChirurgen.FullRowSelect = true;
            this.lvChirurgen.Name = "lvChirurgen";
            this.lvChirurgen.UseCompatibleStateImageBehavior = false;
            // 
            // txtNachname
            // 
            resources.ApplyResources(this.txtNachname, "txtNachname");
            this.txtNachname.Name = "txtNachname";
            this.txtNachname.ProtectContents = false;
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblInfo.Name = "lblInfo";
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
            // ImportChirurgenExcludeView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.grpData);
            this.Controls.Add(this.lblInfo);
            this.Name = "ImportChirurgenExcludeView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.ImportChirurgenExcludeView_Load);
            this.grpData.ResumeLayout(false);
            this.grpData.PerformLayout();
            this.ResumeLayout(false);

        }

        void cmdCancel_Click(object sender, EventArgs e)
        {
            base.CancelClicked();
        }

        #endregion

        private System.Windows.Forms.GroupBox grpData;
        private OplListView lvChirurgen;
        protected override void Object2Control() { }
        private Label lblNachname;
        private Label label1;
        private Label lblInfo;
        private Label lblVorname;
        private OplTextBox txtNachname;
        private OplButton cmdCancel;
        private OplButton cmdDelete;
        private OplButton cmdInsert;
        private OplTextBox txtVorname;
    }
}