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
    partial class SerialNumbersView : OperationenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SerialNumbersView));
            this.grpSerial = new System.Windows.Forms.GroupBox();
            this.lblInfoData = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.cmdDelete = new Windows.Forms.OplButton();
            this.cmdInsert = new Windows.Forms.OplButton();
            this.lvData = new Windows.Forms.OplListView();
            this.txtData = new Windows.Forms.OplProtectedTextBox();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.grpNew = new System.Windows.Forms.GroupBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grpSerial.SuspendLayout();
            this.grpNew.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSerial
            // 
            this.grpSerial.Controls.Add(this.lblInfoData);
            this.grpSerial.Controls.Add(this.cmdDelete);
            this.grpSerial.Controls.Add(this.lvData);
            resources.ApplyResources(this.grpSerial, "grpSerial");
            this.grpSerial.Name = "grpSerial";
            this.grpSerial.TabStop = false;
            // 
            // lblInfoData
            // 
            resources.ApplyResources(this.lblInfoData, "lblInfoData");
            this.lblInfoData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfoData.Name = "lblInfoData";
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            // 
            // cmdDelete
            // 
            resources.ApplyResources(this.cmdDelete, "cmdDelete");
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdInsert
            // 
            resources.ApplyResources(this.cmdInsert, "cmdInsert");
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.UseVisualStyleBackColor = true;
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // lvData
            // 
            resources.ApplyResources(this.lvData, "lvData");
            this.lvData.BalkenGrafik = false;
            this.lvData.FullRowSelect = true;
            this.lvData.Name = "lvData";
            this.lvData.Sortable = false;
            this.lvData.UseCompatibleStateImageBehavior = false;
            // 
            // txtData
            // 
            resources.ApplyResources(this.txtData, "txtData");
            this.txtData.Name = "txtData";
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // grpNew
            // 
            this.grpNew.Controls.Add(this.txtData);
            this.grpNew.Controls.Add(this.lblInfo);
            this.grpNew.Controls.Add(this.cmdInsert);
            resources.ApplyResources(this.grpNew, "grpNew");
            this.grpNew.Name = "grpNew";
            this.grpNew.TabStop = false;
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grpSerial);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpNew);
            // 
            // SerialNumbersView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.cmdCancel);
            this.Name = "SerialNumbersView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.SerialNumbersView_Load);
            this.grpSerial.ResumeLayout(false);
            this.grpNew.ResumeLayout(false);
            this.grpNew.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        void cmdCancel_Click(object sender, EventArgs e)
        {
            base.CancelClicked();
        }

        #endregion

        private System.Windows.Forms.GroupBox grpSerial;
        private OplListView lvData;
        private OplProtectedTextBox txtData;
        private Button cmdCancel;

        private Button cmdDelete;
        private Button cmdInsert;
        private Label lblInfo;
        private Label lblInfoData;
        private GroupBox grpNew;
        private SplitContainer splitContainer;
    }
}