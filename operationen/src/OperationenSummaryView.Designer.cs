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
    partial class OperationenSummaryView : OperationenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OperationenSummaryView));
            this.grpChirurgen = new System.Windows.Forms.GroupBox();
            this.lvChirurgen = new Windows.Forms.OplListView();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvData = new Windows.Forms.OplListView();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.lblInfo = new System.Windows.Forms.Label();
            this.grpChirurgen.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpChirurgen
            // 
            this.grpChirurgen.Controls.Add(this.lvChirurgen);
            resources.ApplyResources(this.grpChirurgen, "grpChirurgen");
            this.grpChirurgen.Name = "grpChirurgen";
            this.grpChirurgen.TabStop = false;
            // 
            // lvChirurgen
            // 
            resources.ApplyResources(this.lvChirurgen, "lvChirurgen");
            this.lvChirurgen.DoubleClickActivation = false;
            this.lvChirurgen.HideSelection = false;
            this.lvChirurgen.Name = "lvChirurgen";
            this.lvChirurgen.UseCompatibleStateImageBehavior = false;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvData);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // lvData
            // 
            resources.ApplyResources(this.lvData, "lvData");
            this.lvData.DoubleClickActivation = false;
            this.lvData.HideSelection = false;
            this.lvData.Name = "lvData";
            this.lvData.UseCompatibleStateImageBehavior = false;
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpChirurgen);
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInfo.Name = "lblInfo";
            // 
            // OperationenSummaryView
            // 
            this.AcceptButton = this.cmdCancel;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.cmdCancel);
            this.Name = "OperationenSummaryView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.OperationenSummaryView_Load);
            this.Shown += new System.EventHandler(this.OperationenSummaryView_Shown);
            this.grpChirurgen.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpChirurgen;
        private OplListView lvChirurgen;
        private GroupBox groupBox1;
        private OplListView lvData;
        private SplitContainer splitContainer;
        private Label lblInfo;
        private OplButton cmdCancel;
    }
}