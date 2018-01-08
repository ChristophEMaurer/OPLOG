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
    partial class LogView : OperationenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogView));
            this.cmdSearch = new Windows.Forms.OplButton();
            this.grpData = new System.Windows.Forms.GroupBox();
            this.cmdDeleteList = new Windows.Forms.OplButton();
            this.txtBis = new Windows.Forms.DateBoxPicker();
            this.txtVon = new Windows.Forms.DateBoxPicker();
            this.cmdClearFields = new Windows.Forms.OplButton();
            this.txtAktion = new Windows.Forms.OplTextBox();
            this.lblAktion = new System.Windows.Forms.Label();
            this.txtNumRecords = new Windows.Forms.OplTextBox();
            this.lblNumRecords = new System.Windows.Forms.Label();
            this.lblBis = new System.Windows.Forms.Label();
            this.txtUser = new Windows.Forms.OplTextBox();
            this.txtMessage = new Windows.Forms.OplTextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblVon = new System.Windows.Forms.Label();
            this.lvLogTable = new Windows.Forms.OplListView();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.cmdDelete = new Windows.Forms.OplButton();
            this.grpData.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdSearch
            // 
            resources.ApplyResources(this.cmdSearch, "cmdSearch");
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // grpData
            // 
            resources.ApplyResources(this.grpData, "grpData");
            this.grpData.Controls.Add(this.cmdDeleteList);
            this.grpData.Controls.Add(this.txtBis);
            this.grpData.Controls.Add(this.txtVon);
            this.grpData.Controls.Add(this.cmdClearFields);
            this.grpData.Controls.Add(this.txtAktion);
            this.grpData.Controls.Add(this.cmdSearch);
            this.grpData.Controls.Add(this.lblAktion);
            this.grpData.Controls.Add(this.txtNumRecords);
            this.grpData.Controls.Add(this.lblNumRecords);
            this.grpData.Controls.Add(this.lblBis);
            this.grpData.Controls.Add(this.txtUser);
            this.grpData.Controls.Add(this.txtMessage);
            this.grpData.Controls.Add(this.lblMessage);
            this.grpData.Controls.Add(this.lblUser);
            this.grpData.Controls.Add(this.lblVon);
            this.grpData.Controls.Add(this.lvLogTable);
            this.grpData.Name = "grpData";
            this.grpData.TabStop = false;
            // 
            // cmdDeleteList
            // 
            resources.ApplyResources(this.cmdDeleteList, "cmdDeleteList");
            this.cmdDeleteList.Name = "cmdDeleteList";
            this.cmdDeleteList.SecurityManager = null;
            this.cmdDeleteList.UserRight = null;
            this.cmdDeleteList.UseVisualStyleBackColor = true;
            this.cmdDeleteList.Click += new System.EventHandler(this.cmdDeleteList_Click);
            // 
            // txtBis
            // 
            resources.ApplyResources(this.txtBis, "txtBis");
            this.txtBis.Name = "txtBis";
            // 
            // txtVon
            // 
            resources.ApplyResources(this.txtVon, "txtVon");
            this.txtVon.Name = "txtVon";
            // 
            // cmdClearFields
            // 
            resources.ApplyResources(this.cmdClearFields, "cmdClearFields");
            this.cmdClearFields.Name = "cmdClearFields";
            this.cmdClearFields.UseVisualStyleBackColor = true;
            this.cmdClearFields.Click += new System.EventHandler(this.cmdClearFields_Click);
            // 
            // txtAktion
            // 
            resources.ApplyResources(this.txtAktion, "txtAktion");
            this.txtAktion.Name = "txtAktion";
            // 
            // lblAktion
            // 
            resources.ApplyResources(this.lblAktion, "lblAktion");
            this.lblAktion.Name = "lblAktion";
            // 
            // txtNumRecords
            // 
            resources.ApplyResources(this.txtNumRecords, "txtNumRecords");
            this.txtNumRecords.Name = "txtNumRecords";
            // 
            // lblNumRecords
            // 
            resources.ApplyResources(this.lblNumRecords, "lblNumRecords");
            this.lblNumRecords.Name = "lblNumRecords";
            // 
            // lblBis
            // 
            resources.ApplyResources(this.lblBis, "lblBis");
            this.lblBis.Name = "lblBis";
            // 
            // txtUser
            // 
            resources.ApplyResources(this.txtUser, "txtUser");
            this.txtUser.Name = "txtUser";
            // 
            // txtMessage
            // 
            resources.ApplyResources(this.txtMessage, "txtMessage");
            this.txtMessage.Name = "txtMessage";
            // 
            // lblMessage
            // 
            resources.ApplyResources(this.lblMessage, "lblMessage");
            this.lblMessage.Name = "lblMessage";
            // 
            // lblUser
            // 
            resources.ApplyResources(this.lblUser, "lblUser");
            this.lblUser.Name = "lblUser";
            // 
            // lblVon
            // 
            resources.ApplyResources(this.lblVon, "lblVon");
            this.lblVon.Name = "lblVon";
            // 
            // lvLogTable
            // 
            resources.ApplyResources(this.lvLogTable, "lvLogTable");
            this.lvLogTable.HideSelection = false;
            this.lvLogTable.Name = "lvLogTable";
            this.lvLogTable.UseCompatibleStateImageBehavior = false;
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.CancelClicked);
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
            // LogView
            // 
            this.AcceptButton = this.cmdSearch;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.cmdDelete);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.grpData);
            this.Name = "LogView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.LogView_Load);
            this.grpData.ResumeLayout(false);
            this.grpData.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.OplButton cmdSearch;
        private System.Windows.Forms.GroupBox grpData;
        private OplListView lvLogTable;
        private Button cmdCancel;
        private Label lblVon;
        private TextBox txtUser;
        private TextBox txtMessage;
        private Label lblMessage;
        private Label lblUser;
        private Label lblBis;
        private TextBox txtNumRecords;
        private Label lblNumRecords;
        private TextBox txtAktion;
        private Label lblAktion;
        private Button cmdClearFields;
        private DateBoxPicker txtBis;
        private DateBoxPicker txtVon;
        private OplButton cmdDelete;
        private OplButton cmdDeleteList;
    }
}