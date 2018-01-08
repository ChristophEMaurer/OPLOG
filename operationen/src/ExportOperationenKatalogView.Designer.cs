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
    partial class ExportOperationenKatalogView : OperationenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportOperationenKatalogView));
            this.splitExport = new System.Windows.Forms.SplitContainer();
            this.grpGroups = new System.Windows.Forms.GroupBox();
            this.splitGroups = new System.Windows.Forms.SplitContainer();
            this.lvGroups = new Windows.Forms.OplListView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtGroup = new Windows.Forms.OplTextBox();
            this.txtTotal = new Windows.Forms.OplTextBox();
            this.cmdGroupDelete = new Windows.Forms.OplButton();
            this.cmdCopyName = new Windows.Forms.OplButton();
            this.cmdGroupInsert = new Windows.Forms.OplButton();
            this.lblGroup = new System.Windows.Forms.Label();
            this.cmdGroupUpdate = new Windows.Forms.OplButton();
            this.grpExportDaten = new System.Windows.Forms.GroupBox();
            this.cmdDelete = new Windows.Forms.OplButton();
            this.cmdDeleteAll = new Windows.Forms.OplButton();
            this.lvExport = new Windows.Forms.OplListView();
            this.splitData = new System.Windows.Forms.SplitContainer();
            this.grpOperationen = new System.Windows.Forms.GroupBox();
            this.cmdAllUsed = new Windows.Forms.OplButton();
            this.txtFilterOPSCode = new Windows.Forms.OplTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdPopulate = new Windows.Forms.OplButton();
            this.txtFilterOPSText = new Windows.Forms.OplTextBox();
            this.cmdInsert = new Windows.Forms.OplButton();
            this.lblFilter = new System.Windows.Forms.Label();
            this.lvOperationen = new Windows.Forms.OplListView();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.lblInfo = new System.Windows.Forms.LinkLabel();
            this.cmdExport = new Windows.Forms.OplButton();
            this.cmdImport = new Windows.Forms.OplButton();
            this.lblSelection = new System.Windows.Forms.Label();
            this.cmdAutoCreate = new Windows.Forms.OplButton();
            this.splitExport.Panel1.SuspendLayout();
            this.splitExport.Panel2.SuspendLayout();
            this.splitExport.SuspendLayout();
            this.grpGroups.SuspendLayout();
            this.splitGroups.Panel1.SuspendLayout();
            this.splitGroups.Panel2.SuspendLayout();
            this.splitGroups.SuspendLayout();
            this.grpExportDaten.SuspendLayout();
            this.splitData.Panel1.SuspendLayout();
            this.splitData.Panel2.SuspendLayout();
            this.splitData.SuspendLayout();
            this.grpOperationen.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitExport
            // 
            this.splitExport.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.splitExport, "splitExport");
            this.splitExport.Name = "splitExport";
            // 
            // splitExport.Panel1
            // 
            this.splitExport.Panel1.Controls.Add(this.grpGroups);
            // 
            // splitExport.Panel2
            // 
            this.splitExport.Panel2.Controls.Add(this.grpExportDaten);
            // 
            // grpGroups
            // 
            this.grpGroups.Controls.Add(this.splitGroups);
            resources.ApplyResources(this.grpGroups, "grpGroups");
            this.grpGroups.Name = "grpGroups";
            this.grpGroups.TabStop = false;
            // 
            // splitGroups
            // 
            this.splitGroups.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.splitGroups, "splitGroups");
            this.splitGroups.Name = "splitGroups";
            // 
            // splitGroups.Panel1
            // 
            this.splitGroups.Panel1.Controls.Add(this.lvGroups);
            // 
            // splitGroups.Panel2
            // 
            this.splitGroups.Panel2.Controls.Add(this.lblTotal);
            this.splitGroups.Panel2.Controls.Add(this.txtGroup);
            this.splitGroups.Panel2.Controls.Add(this.txtTotal);
            this.splitGroups.Panel2.Controls.Add(this.cmdGroupDelete);
            this.splitGroups.Panel2.Controls.Add(this.cmdCopyName);
            this.splitGroups.Panel2.Controls.Add(this.cmdGroupInsert);
            this.splitGroups.Panel2.Controls.Add(this.lblGroup);
            this.splitGroups.Panel2.Controls.Add(this.cmdGroupUpdate);
            // 
            // lvGroups
            // 
            resources.ApplyResources(this.lvGroups, "lvGroups");
            this.lvGroups.DoubleClickActivation = false;
            this.lvGroups.FullRowSelect = true;
            this.lvGroups.MultiSelect = false;
            this.lvGroups.Name = "lvGroups";
            this.lvGroups.UseCompatibleStateImageBehavior = false;
            this.lvGroups.SelectedIndexChanged += new System.EventHandler(this.lvGroups_SelectedIndexChanged);
            // 
            // lblTotal
            // 
            resources.ApplyResources(this.lblTotal, "lblTotal");
            this.lblTotal.Name = "lblTotal";
            // 
            // txtGroup
            // 
            resources.ApplyResources(this.txtGroup, "txtGroup");
            this.txtGroup.Name = "txtGroup";
            this.txtGroup.ProtectContents = false;
            this.txtGroup.TextChanged += new System.EventHandler(this.txtGroup_TextChanged);
            // 
            // txtTotal
            // 
            resources.ApplyResources(this.txtTotal, "txtTotal");
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ProtectContents = false;
            this.txtTotal.ReadOnly = true;
            // 
            // cmdGroupDelete
            // 
            resources.ApplyResources(this.cmdGroupDelete, "cmdGroupDelete");
            this.cmdGroupDelete.Name = "cmdGroupDelete";
            this.cmdGroupDelete.SecurityManager = null;
            this.cmdGroupDelete.UserRight = null;
            this.cmdGroupDelete.UseVisualStyleBackColor = true;
            this.cmdGroupDelete.Click += new System.EventHandler(this.cmdGroupDelete_Click);
            // 
            // cmdCopyName
            // 
            resources.ApplyResources(this.cmdCopyName, "cmdCopyName");
            this.cmdCopyName.Name = "cmdCopyName";
            this.cmdCopyName.SecurityManager = null;
            this.cmdCopyName.UserRight = null;
            this.cmdCopyName.UseVisualStyleBackColor = true;
            this.cmdCopyName.Click += new System.EventHandler(this.cmdCopyName_Click);
            // 
            // cmdGroupInsert
            // 
            resources.ApplyResources(this.cmdGroupInsert, "cmdGroupInsert");
            this.cmdGroupInsert.Name = "cmdGroupInsert";
            this.cmdGroupInsert.SecurityManager = null;
            this.cmdGroupInsert.UserRight = null;
            this.cmdGroupInsert.UseVisualStyleBackColor = true;
            this.cmdGroupInsert.Click += new System.EventHandler(this.cmdGroupInsert_Click);
            // 
            // lblGroup
            // 
            resources.ApplyResources(this.lblGroup, "lblGroup");
            this.lblGroup.Name = "lblGroup";
            // 
            // cmdGroupUpdate
            // 
            resources.ApplyResources(this.cmdGroupUpdate, "cmdGroupUpdate");
            this.cmdGroupUpdate.Name = "cmdGroupUpdate";
            this.cmdGroupUpdate.SecurityManager = null;
            this.cmdGroupUpdate.UserRight = null;
            this.cmdGroupUpdate.UseVisualStyleBackColor = true;
            this.cmdGroupUpdate.Click += new System.EventHandler(this.cmdGroupUpdate_Click);
            // 
            // grpExportDaten
            // 
            this.grpExportDaten.Controls.Add(this.cmdDelete);
            this.grpExportDaten.Controls.Add(this.cmdDeleteAll);
            this.grpExportDaten.Controls.Add(this.lvExport);
            resources.ApplyResources(this.grpExportDaten, "grpExportDaten");
            this.grpExportDaten.Name = "grpExportDaten";
            this.grpExportDaten.TabStop = false;
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
            // cmdDeleteAll
            // 
            resources.ApplyResources(this.cmdDeleteAll, "cmdDeleteAll");
            this.cmdDeleteAll.Name = "cmdDeleteAll";
            this.cmdDeleteAll.SecurityManager = null;
            this.cmdDeleteAll.UserRight = null;
            this.cmdDeleteAll.UseVisualStyleBackColor = true;
            this.cmdDeleteAll.Click += new System.EventHandler(this.cmdDeleteAll_Click);
            // 
            // lvExport
            // 
            resources.ApplyResources(this.lvExport, "lvExport");
            this.lvExport.DoubleClickActivation = false;
            this.lvExport.FullRowSelect = true;
            this.lvExport.MultiSelect = false;
            this.lvExport.Name = "lvExport";
            this.lvExport.UseCompatibleStateImageBehavior = false;
            this.lvExport.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvExport_ItemSelectionChanged);
            this.lvExport.DoubleClick += new System.EventHandler(this.lvExport_DoubleClick);
            // 
            // splitData
            // 
            resources.ApplyResources(this.splitData, "splitData");
            this.splitData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitData.Name = "splitData";
            // 
            // splitData.Panel1
            // 
            this.splitData.Panel1.Controls.Add(this.grpOperationen);
            // 
            // splitData.Panel2
            // 
            this.splitData.Panel2.Controls.Add(this.splitExport);
            // 
            // grpOperationen
            // 
            this.grpOperationen.Controls.Add(this.cmdAllUsed);
            this.grpOperationen.Controls.Add(this.txtFilterOPSCode);
            this.grpOperationen.Controls.Add(this.label1);
            this.grpOperationen.Controls.Add(this.cmdPopulate);
            this.grpOperationen.Controls.Add(this.txtFilterOPSText);
            this.grpOperationen.Controls.Add(this.cmdInsert);
            this.grpOperationen.Controls.Add(this.lblFilter);
            this.grpOperationen.Controls.Add(this.lvOperationen);
            resources.ApplyResources(this.grpOperationen, "grpOperationen");
            this.grpOperationen.Name = "grpOperationen";
            this.grpOperationen.TabStop = false;
            // 
            // cmdAllUsed
            // 
            resources.ApplyResources(this.cmdAllUsed, "cmdAllUsed");
            this.cmdAllUsed.Name = "cmdAllUsed";
            this.cmdAllUsed.SecurityManager = null;
            this.cmdAllUsed.UserRight = null;
            this.cmdAllUsed.UseVisualStyleBackColor = true;
            this.cmdAllUsed.Click += new System.EventHandler(this.cmdAllUsed_Click);
            // 
            // txtFilterOPSCode
            // 
            resources.ApplyResources(this.txtFilterOPSCode, "txtFilterOPSCode");
            this.txtFilterOPSCode.Name = "txtFilterOPSCode";
            this.txtFilterOPSCode.ProtectContents = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cmdPopulate
            // 
            resources.ApplyResources(this.cmdPopulate, "cmdPopulate");
            this.cmdPopulate.Name = "cmdPopulate";
            this.cmdPopulate.SecurityManager = null;
            this.cmdPopulate.UserRight = null;
            this.cmdPopulate.UseVisualStyleBackColor = true;
            this.cmdPopulate.Click += new System.EventHandler(this.cmdPopulate_Click);
            // 
            // txtFilterOPSText
            // 
            resources.ApplyResources(this.txtFilterOPSText, "txtFilterOPSText");
            this.txtFilterOPSText.Name = "txtFilterOPSText";
            this.txtFilterOPSText.ProtectContents = false;
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
            // lblFilter
            // 
            resources.ApplyResources(this.lblFilter, "lblFilter");
            this.lblFilter.Name = "lblFilter";
            // 
            // lvOperationen
            // 
            resources.ApplyResources(this.lvOperationen, "lvOperationen");
            this.lvOperationen.DoubleClickActivation = false;
            this.lvOperationen.FullRowSelect = true;
            this.lvOperationen.MultiSelect = false;
            this.lvOperationen.Name = "lvOperationen";
            this.lvOperationen.UseCompatibleStateImageBehavior = false;
            this.lvOperationen.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvOperationen_ItemSelectionChanged);
            this.lvOperationen.SelectedIndexChanged += new System.EventHandler(this.lvOperationen_SelectedIndexChanged);
            this.lvOperationen.DoubleClick += new System.EventHandler(this.lvOperationen_DoubleClick);
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
            this.lblInfo.TabStop = true;
            // 
            // cmdExport
            // 
            resources.ApplyResources(this.cmdExport, "cmdExport");
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.SecurityManager = null;
            this.cmdExport.UserRight = null;
            this.cmdExport.UseVisualStyleBackColor = true;
            this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
            // 
            // cmdImport
            // 
            resources.ApplyResources(this.cmdImport, "cmdImport");
            this.cmdImport.Name = "cmdImport";
            this.cmdImport.SecurityManager = null;
            this.cmdImport.UserRight = null;
            this.cmdImport.UseVisualStyleBackColor = true;
            this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
            // 
            // lblSelection
            // 
            resources.ApplyResources(this.lblSelection, "lblSelection");
            this.lblSelection.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSelection.Name = "lblSelection";
            // 
            // cmdAutoCreate
            // 
            resources.ApplyResources(this.cmdAutoCreate, "cmdAutoCreate");
            this.cmdAutoCreate.Name = "cmdAutoCreate";
            this.cmdAutoCreate.SecurityManager = null;
            this.cmdAutoCreate.UserRight = null;
            this.cmdAutoCreate.UseVisualStyleBackColor = true;
            this.cmdAutoCreate.Click += new System.EventHandler(this.cmdAutoCreate_Click);
            // 
            // ExportOperationenKatalogView
            // 
            this.AcceptButton = this.cmdPopulate;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.cmdAutoCreate);
            this.Controls.Add(this.splitData);
            this.Controls.Add(this.lblSelection);
            this.Controls.Add(this.cmdImport);
            this.Controls.Add(this.cmdExport);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.cmdCancel);
            this.Name = "ExportOperationenKatalogView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.OperationenKatalogView_Load);
            this.Shown += new System.EventHandler(this.ExportOperationenKatalogView_Shown);
            this.splitExport.Panel1.ResumeLayout(false);
            this.splitExport.Panel2.ResumeLayout(false);
            this.splitExport.ResumeLayout(false);
            this.grpGroups.ResumeLayout(false);
            this.splitGroups.Panel1.ResumeLayout(false);
            this.splitGroups.Panel2.ResumeLayout(false);
            this.splitGroups.Panel2.PerformLayout();
            this.splitGroups.ResumeLayout(false);
            this.grpExportDaten.ResumeLayout(false);
            this.splitData.Panel1.ResumeLayout(false);
            this.splitData.Panel2.ResumeLayout(false);
            this.splitData.ResumeLayout(false);
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
        protected override void Object2Control() { }
        private Label lblFilter;
        private LinkLabel lblInfo;
        private Label label1;
        private OplListView lvExport;
        private GroupBox grpExportDaten;
        private Label lblSelection;
        private OplButton cmdCancel;
        private OplButton cmdDelete;
        private OplButton cmdInsert;
        private OplButton cmdPopulate;
        private OplTextBox txtFilterOPSText;
        private OplTextBox txtFilterOPSCode;
        private OplButton cmdExport;
        private OplButton cmdImport;
        private OplButton cmdDeleteAll;
        private OplButton cmdAllUsed;
        private GroupBox grpGroups;
        private OplButton cmdGroupDelete;
        private OplTextBox txtGroup;
        private OplListView lvGroups;
        private OplButton cmdGroupUpdate;
        private OplButton cmdGroupInsert;
        private Label lblGroup;
        private SplitContainer splitData;
        private SplitContainer splitExport;
        private OplButton cmdCopyName;
        private Label lblTotal;
        private OplTextBox txtTotal;
        private SplitContainer splitGroups;
        private OplButton cmdAutoCreate;
    }
}