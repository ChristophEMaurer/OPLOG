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
    partial class AkademischeAusbildungView : OperationenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AkademischeAusbildungView));
            this.grpNotizen = new System.Windows.Forms.GroupBox();
            this.cmdPrint = new Windows.Forms.OplButton();
            this.txtEnde = new Windows.Forms.DateBoxPicker();
            this.lblEnde = new System.Windows.Forms.Label();
            this.cbTypen = new System.Windows.Forms.ComboBox();
            this.lbTyp = new System.Windows.Forms.Label();
            this.grpAuswahl = new System.Windows.Forms.GroupBox();
            this.cbFilterChirurgen = new System.Windows.Forms.ComboBox();
            this.lblFilterChirurgen = new System.Windows.Forms.Label();
            this.txtBeginn = new Windows.Forms.DateBoxPicker();
            this.cmdClear = new Windows.Forms.OplButton();
            this.cmdApply = new Windows.Forms.OplButton();
            this.txtOrganisation = new Windows.Forms.OplTextBox();
            this.lblOrganisation = new System.Windows.Forms.Label();
            this.cmdDelete = new Windows.Forms.OplButton();
            this.cmdInsert = new Windows.Forms.OplButton();
            this.lblBeginn = new System.Windows.Forms.Label();
            this.lvAA = new Windows.Forms.OplListView();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.lblInfo = new System.Windows.Forms.Label();
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
            this.grpNotizen.Controls.Add(this.cbTypen);
            this.grpNotizen.Controls.Add(this.lbTyp);
            this.grpNotizen.Controls.Add(this.grpAuswahl);
            this.grpNotizen.Controls.Add(this.txtBeginn);
            this.grpNotizen.Controls.Add(this.cmdClear);
            this.grpNotizen.Controls.Add(this.cmdApply);
            this.grpNotizen.Controls.Add(this.txtOrganisation);
            this.grpNotizen.Controls.Add(this.lblOrganisation);
            this.grpNotizen.Controls.Add(this.cmdDelete);
            this.grpNotizen.Controls.Add(this.cmdInsert);
            this.grpNotizen.Controls.Add(this.lblBeginn);
            this.grpNotizen.Controls.Add(this.lvAA);
            this.grpNotizen.Name = "grpNotizen";
            this.grpNotizen.TabStop = false;
            // 
            // cmdPrint
            // 
            resources.ApplyResources(this.cmdPrint, "cmdPrint");
            this.cmdPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
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
            // cbTypen
            // 
            resources.ApplyResources(this.cbTypen, "cbTypen");
            this.cbTypen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypen.Name = "cbTypen";
            // 
            // lbTyp
            // 
            resources.ApplyResources(this.lbTyp, "lbTyp");
            this.lbTyp.Name = "lbTyp";
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
            // txtOrganisation
            // 
            resources.ApplyResources(this.txtOrganisation, "txtOrganisation");
            this.txtOrganisation.Name = "txtOrganisation";
            this.txtOrganisation.ProtectContents = false;
            // 
            // lblOrganisation
            // 
            resources.ApplyResources(this.lblOrganisation, "lblOrganisation");
            this.lblOrganisation.Name = "lblOrganisation";
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
            // lvAA
            // 
            resources.ApplyResources(this.lvAA, "lvAA");
            this.lvAA.DoubleClickActivation = false;
            this.lvAA.FullRowSelect = true;
            this.lvAA.Name = "lvAA";
            this.lvAA.UseCompatibleStateImageBehavior = false;
            this.lvAA.SelectedIndexChanged += new System.EventHandler(this.lvAA_SelectedIndexChanged);
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
            // AkademischeAusbildungView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.grpNotizen);
            this.Name = "AkademischeAusbildungView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.AkademischeAusbildungView_Load);
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
        private OplListView lvAA;
        private Label lblBeginn;
        private Label lblOrganisation;
        private DateBoxPicker txtBeginn;
        private Label lblFilterChirurgen;
        private ComboBox cbFilterChirurgen;
        private GroupBox grpAuswahl;
        private ComboBox cbTypen;
        private Label lbTyp;
        private Label lblInfo;
        private Label lblEnde;
        private DateBoxPicker txtEnde;
        private OplButton cmdCancel;
        private OplButton cmdDelete;
        private OplButton cmdInsert;
        private OplTextBox txtOrganisation;
        private OplButton cmdApply;
        private OplButton cmdClear;
        private OplButton cmdPrint;
    }
}