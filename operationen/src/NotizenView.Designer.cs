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
    partial class NotizenView : OperationenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotizenView));
            this.grpNotizen = new System.Windows.Forms.GroupBox();
            this.cmdPrint = new Windows.Forms.OplButton();
            this.txtEnde = new Windows.Forms.DateBoxPicker();
            this.lblEnde = new System.Windows.Forms.Label();
            this.cbNotizTypen = new System.Windows.Forms.ComboBox();
            this.lblNotiztyp = new System.Windows.Forms.Label();
            this.grpAuswahl = new System.Windows.Forms.GroupBox();
            this.cbFilterChirurgen = new System.Windows.Forms.ComboBox();
            this.lblFilterChirurgen = new System.Windows.Forms.Label();
            this.txtBeginn = new Windows.Forms.DateBoxPicker();
            this.cmdClear = new Windows.Forms.OplButton();
            this.cmdApply = new Windows.Forms.OplButton();
            this.txtNotiz = new Windows.Forms.OplTextBox();
            this.lblNotiz = new System.Windows.Forms.Label();
            this.cmdDelete = new Windows.Forms.OplButton();
            this.cmdInsert = new Windows.Forms.OplButton();
            this.lblBeginn = new System.Windows.Forms.Label();
            this.lvNotizen = new Windows.Forms.OplListView();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.lblInfo = new System.Windows.Forms.LinkLabel();
            this.grpNotizen.SuspendLayout();
            this.grpAuswahl.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpNotizen
            // 
            resources.ApplyResources(this.grpNotizen, "grpNotizen");
            this.grpNotizen.Controls.Add(this.cmdPrint);
            this.grpNotizen.Controls.Add(this.txtEnde);
            this.grpNotizen.Controls.Add(this.lblEnde);
            this.grpNotizen.Controls.Add(this.cbNotizTypen);
            this.grpNotizen.Controls.Add(this.lblNotiztyp);
            this.grpNotizen.Controls.Add(this.grpAuswahl);
            this.grpNotizen.Controls.Add(this.txtBeginn);
            this.grpNotizen.Controls.Add(this.cmdClear);
            this.grpNotizen.Controls.Add(this.cmdApply);
            this.grpNotizen.Controls.Add(this.txtNotiz);
            this.grpNotizen.Controls.Add(this.lblNotiz);
            this.grpNotizen.Controls.Add(this.cmdDelete);
            this.grpNotizen.Controls.Add(this.cmdInsert);
            this.grpNotizen.Controls.Add(this.lblBeginn);
            this.grpNotizen.Controls.Add(this.lvNotizen);
            this.grpNotizen.Name = "grpNotizen";
            this.grpNotizen.TabStop = false;
            // 
            // cmdPrint
            // 
            resources.ApplyResources(this.cmdPrint, "cmdPrint");
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.SecurityManager = null;
            this.cmdPrint.UserRight = null;
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // txtEnde
            // 
            resources.ApplyResources(this.txtEnde, "txtEnde");
            this.txtEnde.Name = "txtEnde";
            // 
            // lblEnde
            // 
            resources.ApplyResources(this.lblEnde, "lblEnde");
            this.lblEnde.Name = "lblEnde";
            // 
            // cbNotizTypen
            // 
            resources.ApplyResources(this.cbNotizTypen, "cbNotizTypen");
            this.cbNotizTypen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNotizTypen.Name = "cbNotizTypen";
            // 
            // lblNotiztyp
            // 
            resources.ApplyResources(this.lblNotiztyp, "lblNotiztyp");
            this.lblNotiztyp.Name = "lblNotiztyp";
            // 
            // grpAuswahl
            // 
            resources.ApplyResources(this.grpAuswahl, "grpAuswahl");
            this.grpAuswahl.Controls.Add(this.cbFilterChirurgen);
            this.grpAuswahl.Controls.Add(this.lblFilterChirurgen);
            this.grpAuswahl.Name = "grpAuswahl";
            this.grpAuswahl.TabStop = false;
            // 
            // cbFilterChirurgen
            // 
            resources.ApplyResources(this.cbFilterChirurgen, "cbFilterChirurgen");
            this.cbFilterChirurgen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterChirurgen.Name = "cbFilterChirurgen";
            this.cbFilterChirurgen.SelectedIndexChanged += new System.EventHandler(this.cbChirurgen_SelectedIndexChanged);
            // 
            // lblFilterChirurgen
            // 
            resources.ApplyResources(this.lblFilterChirurgen, "lblFilterChirurgen");
            this.lblFilterChirurgen.Name = "lblFilterChirurgen";
            // 
            // txtBeginn
            // 
            resources.ApplyResources(this.txtBeginn, "txtBeginn");
            this.txtBeginn.Name = "txtBeginn";
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
            // txtNotiz
            // 
            resources.ApplyResources(this.txtNotiz, "txtNotiz");
            this.txtNotiz.Name = "txtNotiz";
            this.txtNotiz.ProtectContents = false;
            // 
            // lblNotiz
            // 
            resources.ApplyResources(this.lblNotiz, "lblNotiz");
            this.lblNotiz.Name = "lblNotiz";
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
            // lblBeginn
            // 
            resources.ApplyResources(this.lblBeginn, "lblBeginn");
            this.lblBeginn.Name = "lblBeginn";
            // 
            // lvNotizen
            // 
            resources.ApplyResources(this.lvNotizen, "lvNotizen");
            this.lvNotizen.DoubleClickActivation = false;
            this.lvNotizen.FullRowSelect = true;
            this.lvNotizen.Name = "lvNotizen";
            this.lvNotizen.UseCompatibleStateImageBehavior = false;
            this.lvNotizen.SelectedIndexChanged += new System.EventHandler(this.lvNotizen_SelectedIndexChanged);
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
            this.lblInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblInfo_LinkClicked);
            // 
            // NotizenView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.grpNotizen);
            this.MaximizeBox = false;
            this.Name = "NotizenView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.NotizenView_Load);
            this.grpNotizen.ResumeLayout(false);
            this.grpNotizen.PerformLayout();
            this.grpAuswahl.ResumeLayout(false);
            this.grpAuswahl.PerformLayout();
            this.ResumeLayout(false);

        }

        void cmdCancel_Click(object sender, EventArgs e)
        {
            base.CancelClicked();
        }

        #endregion

        private System.Windows.Forms.GroupBox grpNotizen;
        private OplListView lvNotizen;
        private Label lblBeginn;
        private Label lblNotiz;
        private DateBoxPicker txtBeginn;
        private Label lblFilterChirurgen;
        private ComboBox cbFilterChirurgen;
        private GroupBox grpAuswahl;
        private ComboBox cbNotizTypen;
        private Label lblNotiztyp;
        private DateBoxPicker txtEnde;
        private Label lblEnde;
        private LinkLabel lblInfo;
        private OplButton cmdCancel;
        private OplButton cmdDelete;
        private OplButton cmdInsert;
        private OplTextBox txtNotiz;
        private OplButton cmdApply;
        private OplButton cmdClear;
        private OplButton cmdPrint;
    }
}