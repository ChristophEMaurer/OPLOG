namespace Operationen
{
    partial class RichtlinienView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RichtlinienView));
            this.lvRichtlinien = new Windows.Forms.OplListView();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.lvGebiete = new Windows.Forms.OplListView();
            this.grpRichtlinien = new System.Windows.Forms.GroupBox();
            this.txtLfdNummer = new Windows.Forms.OplTextBox();
            this.lblLfdNummer = new System.Windows.Forms.Label();
            this.cmdDown = new Windows.Forms.OplButton();
            this.cmdUp = new Windows.Forms.OplButton();
            this.lblInfoRichtzahl = new System.Windows.Forms.Label();
            this.lblRichtzahl = new System.Windows.Forms.Label();
            this.lblUntBehMethode = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdDelete = new Windows.Forms.OplButton();
            this.txtRichtzahl = new Windows.Forms.OplTextBox();
            this.cmdClear = new Windows.Forms.OplButton();
            this.cmdApply = new Windows.Forms.OplButton();
            this.cmdInsert = new Windows.Forms.OplButton();
            this.txtUntBehMethode = new Windows.Forms.OplTextBox();
            this.grpGebiete = new System.Windows.Forms.GroupBox();
            this.lblGebiete = new System.Windows.Forms.Label();
            this.lblLegende = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grpRichtlinien.SuspendLayout();
            this.grpGebiete.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvRichtlinien
            // 
            resources.ApplyResources(this.lvRichtlinien, "lvRichtlinien");
            this.lvRichtlinien.DoubleClickActivation = false;
            this.lvRichtlinien.MultiSelect = false;
            this.lvRichtlinien.Name = "lvRichtlinien";
            this.lvRichtlinien.UseCompatibleStateImageBehavior = false;
            this.lvRichtlinien.View = System.Windows.Forms.View.Details;
            this.lvRichtlinien.SelectedIndexChanged += new System.EventHandler(this.lvRichtlinien_SelectedIndexChanged);
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
            // lvGebiete
            // 
            resources.ApplyResources(this.lvGebiete, "lvGebiete");
            this.lvGebiete.DoubleClickActivation = false;
            this.lvGebiete.FullRowSelect = true;
            this.lvGebiete.HideSelection = false;
            this.lvGebiete.MultiSelect = false;
            this.lvGebiete.Name = "lvGebiete";
            this.lvGebiete.UseCompatibleStateImageBehavior = false;
            this.lvGebiete.View = System.Windows.Forms.View.Details;
            this.lvGebiete.SelectedIndexChanged += new System.EventHandler(this.lvGebiete_SelectedIndexChanged);
            // 
            // grpRichtlinien
            // 
            this.grpRichtlinien.Controls.Add(this.txtLfdNummer);
            this.grpRichtlinien.Controls.Add(this.lblLfdNummer);
            this.grpRichtlinien.Controls.Add(this.cmdDown);
            this.grpRichtlinien.Controls.Add(this.cmdUp);
            this.grpRichtlinien.Controls.Add(this.lblInfoRichtzahl);
            this.grpRichtlinien.Controls.Add(this.lblRichtzahl);
            this.grpRichtlinien.Controls.Add(this.lblUntBehMethode);
            this.grpRichtlinien.Controls.Add(this.label1);
            this.grpRichtlinien.Controls.Add(this.cmdDelete);
            this.grpRichtlinien.Controls.Add(this.txtRichtzahl);
            this.grpRichtlinien.Controls.Add(this.cmdClear);
            this.grpRichtlinien.Controls.Add(this.cmdApply);
            this.grpRichtlinien.Controls.Add(this.cmdInsert);
            this.grpRichtlinien.Controls.Add(this.txtUntBehMethode);
            this.grpRichtlinien.Controls.Add(this.lvRichtlinien);
            resources.ApplyResources(this.grpRichtlinien, "grpRichtlinien");
            this.grpRichtlinien.Name = "grpRichtlinien";
            this.grpRichtlinien.TabStop = false;
            // 
            // txtLfdNummer
            // 
            resources.ApplyResources(this.txtLfdNummer, "txtLfdNummer");
            this.txtLfdNummer.Name = "txtLfdNummer";
            this.txtLfdNummer.ProtectContents = false;
            // 
            // lblLfdNummer
            // 
            resources.ApplyResources(this.lblLfdNummer, "lblLfdNummer");
            this.lblLfdNummer.Name = "lblLfdNummer";
            // 
            // cmdDown
            // 
            resources.ApplyResources(this.cmdDown, "cmdDown");
            this.cmdDown.Name = "cmdDown";
            this.cmdDown.SecurityManager = null;
            this.cmdDown.UserRight = null;
            this.cmdDown.UseVisualStyleBackColor = true;
            this.cmdDown.Click += new System.EventHandler(this.cmdDown_Click);
            // 
            // cmdUp
            // 
            resources.ApplyResources(this.cmdUp, "cmdUp");
            this.cmdUp.Name = "cmdUp";
            this.cmdUp.SecurityManager = null;
            this.cmdUp.UserRight = null;
            this.cmdUp.UseVisualStyleBackColor = true;
            this.cmdUp.Click += new System.EventHandler(this.cmdUp_Click);
            // 
            // lblInfoRichtzahl
            // 
            resources.ApplyResources(this.lblInfoRichtzahl, "lblInfoRichtzahl");
            this.lblInfoRichtzahl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfoRichtzahl.Name = "lblInfoRichtzahl";
            // 
            // lblRichtzahl
            // 
            resources.ApplyResources(this.lblRichtzahl, "lblRichtzahl");
            this.lblRichtzahl.Name = "lblRichtzahl";
            // 
            // lblUntBehMethode
            // 
            resources.ApplyResources(this.lblUntBehMethode, "lblUntBehMethode");
            this.lblUntBehMethode.Name = "lblUntBehMethode";
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
            // txtRichtzahl
            // 
            resources.ApplyResources(this.txtRichtzahl, "txtRichtzahl");
            this.txtRichtzahl.Name = "txtRichtzahl";
            this.txtRichtzahl.ProtectContents = false;
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
            // cmdInsert
            // 
            resources.ApplyResources(this.cmdInsert, "cmdInsert");
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.SecurityManager = null;
            this.cmdInsert.UserRight = null;
            this.cmdInsert.UseVisualStyleBackColor = true;
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // txtUntBehMethode
            // 
            resources.ApplyResources(this.txtUntBehMethode, "txtUntBehMethode");
            this.txtUntBehMethode.Name = "txtUntBehMethode";
            this.txtUntBehMethode.ProtectContents = false;
            // 
            // grpGebiete
            // 
            this.grpGebiete.Controls.Add(this.lblGebiete);
            this.grpGebiete.Controls.Add(this.lvGebiete);
            resources.ApplyResources(this.grpGebiete, "grpGebiete");
            this.grpGebiete.Name = "grpGebiete";
            this.grpGebiete.TabStop = false;
            // 
            // lblGebiete
            // 
            resources.ApplyResources(this.lblGebiete, "lblGebiete");
            this.lblGebiete.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGebiete.Name = "lblGebiete";
            // 
            // lblLegende
            // 
            resources.ApplyResources(this.lblLegende, "lblLegende");
            this.lblLegende.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLegende.Name = "lblLegende";
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grpGebiete);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpRichtlinien);
            // 
            // RichtlinienView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.lblLegende);
            this.Controls.Add(this.cmdCancel);
            this.DoubleBuffered = true;
            this.Name = "RichtlinienView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.RichtlinienView_Load);
            this.Shown += new System.EventHandler(this.RichtlinienView_Shown);
            this.grpRichtlinien.ResumeLayout(false);
            this.grpRichtlinien.PerformLayout();
            this.grpGebiete.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.OplListView lvRichtlinien;
        private Windows.Forms.OplButton cmdCancel;
        private Windows.Forms.OplListView lvGebiete;
        private System.Windows.Forms.GroupBox grpRichtlinien;
        private System.Windows.Forms.GroupBox grpGebiete;
        private System.Windows.Forms.Label lblLegende;
        private Windows.Forms.OplButton cmdClear;
        private Windows.Forms.OplButton cmdApply;
        private Windows.Forms.OplButton cmdInsert;
        private System.Windows.Forms.Label label1;
        private Windows.Forms.OplButton cmdDelete;
        private System.Windows.Forms.Label lblRichtzahl;
        private System.Windows.Forms.Label lblUntBehMethode;
        private Windows.Forms.OplTextBox txtRichtzahl;
        private Windows.Forms.OplTextBox txtUntBehMethode;
        private System.Windows.Forms.Label lblInfoRichtzahl;
        private Windows.Forms.OplButton cmdDown;
        private Windows.Forms.OplButton cmdUp;
        private Windows.Forms.OplTextBox txtLfdNummer;
        private System.Windows.Forms.Label lblLfdNummer;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label lblGebiete;
    }
}