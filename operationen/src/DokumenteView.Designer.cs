namespace Operationen
{
    partial class DokumenteView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DokumenteView));
            this.lvDokumente = new Windows.Forms.OplListView();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.grpDokumente = new System.Windows.Forms.GroupBox();
            this.lblMinMax = new System.Windows.Forms.Label();
            this.cmdDisplay = new Windows.Forms.OplButton();
            this.txtLfdNummer = new Windows.Forms.OplTextBox();
            this.lblLfdNummer = new System.Windows.Forms.Label();
            this.cmdApply = new Windows.Forms.OplButton();
            this.cmdDateiname = new Windows.Forms.OplButton();
            this.txtDateiname = new Windows.Forms.OplTextBox();
            this.lblDateiname = new System.Windows.Forms.Label();
            this.txtBeschreibung = new Windows.Forms.OplTextBox();
            this.cmdDown = new Windows.Forms.OplButton();
            this.lblBeschreibung = new System.Windows.Forms.Label();
            this.cmdUp = new Windows.Forms.OplButton();
            this.lblGruppe = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdDelete = new Windows.Forms.OplButton();
            this.txtGruppe = new Windows.Forms.OplTextBox();
            this.cmdClear = new Windows.Forms.OplButton();
            this.cmdInsert = new Windows.Forms.OplButton();
            this.lblInfo = new System.Windows.Forms.Label();
            this.grpDokumente.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvDokumente
            // 
            resources.ApplyResources(this.lvDokumente, "lvDokumente");
            this.lvDokumente.MultiSelect = false;
            this.lvDokumente.Name = "lvDokumente";
            this.lvDokumente.UseCompatibleStateImageBehavior = false;
            this.lvDokumente.View = System.Windows.Forms.View.Details;
            this.lvDokumente.SelectedIndexChanged += new System.EventHandler(this.lvDokumente_SelectedIndexChanged);
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // grpDokumente
            // 
            resources.ApplyResources(this.grpDokumente, "grpDokumente");
            this.grpDokumente.Controls.Add(this.lblMinMax);
            this.grpDokumente.Controls.Add(this.cmdDisplay);
            this.grpDokumente.Controls.Add(this.txtLfdNummer);
            this.grpDokumente.Controls.Add(this.lblLfdNummer);
            this.grpDokumente.Controls.Add(this.cmdApply);
            this.grpDokumente.Controls.Add(this.cmdDateiname);
            this.grpDokumente.Controls.Add(this.txtDateiname);
            this.grpDokumente.Controls.Add(this.lblDateiname);
            this.grpDokumente.Controls.Add(this.txtBeschreibung);
            this.grpDokumente.Controls.Add(this.cmdDown);
            this.grpDokumente.Controls.Add(this.lblBeschreibung);
            this.grpDokumente.Controls.Add(this.cmdUp);
            this.grpDokumente.Controls.Add(this.lblGruppe);
            this.grpDokumente.Controls.Add(this.label1);
            this.grpDokumente.Controls.Add(this.cmdDelete);
            this.grpDokumente.Controls.Add(this.txtGruppe);
            this.grpDokumente.Controls.Add(this.cmdClear);
            this.grpDokumente.Controls.Add(this.cmdInsert);
            this.grpDokumente.Controls.Add(this.lvDokumente);
            this.grpDokumente.Name = "grpDokumente";
            this.grpDokumente.TabStop = false;
            // 
            // lblMinMax
            // 
            resources.ApplyResources(this.lblMinMax, "lblMinMax");
            this.lblMinMax.Name = "lblMinMax";
            // 
            // cmdDisplay
            // 
            resources.ApplyResources(this.cmdDisplay, "cmdDisplay");
            this.cmdDisplay.Name = "cmdDisplay";
            this.cmdDisplay.UseVisualStyleBackColor = true;
            this.cmdDisplay.Click += new System.EventHandler(this.cmdDisplay_Click);
            // 
            // txtLfdNummer
            // 
            resources.ApplyResources(this.txtLfdNummer, "txtLfdNummer");
            this.txtLfdNummer.Name = "txtLfdNummer";
            // 
            // lblLfdNummer
            // 
            resources.ApplyResources(this.lblLfdNummer, "lblLfdNummer");
            this.lblLfdNummer.Name = "lblLfdNummer";
            // 
            // cmdApply
            // 
            resources.ApplyResources(this.cmdApply, "cmdApply");
            this.cmdApply.Name = "cmdApply";
            this.cmdApply.UseVisualStyleBackColor = true;
            this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
            // 
            // cmdDateiname
            // 
            resources.ApplyResources(this.cmdDateiname, "cmdDateiname");
            this.cmdDateiname.Name = "cmdDateiname";
            this.cmdDateiname.UseVisualStyleBackColor = true;
            this.cmdDateiname.Click += new System.EventHandler(this.cmdDateiname_Click);
            // 
            // txtDateiname
            // 
            resources.ApplyResources(this.txtDateiname, "txtDateiname");
            this.txtDateiname.Name = "txtDateiname";
            this.txtDateiname.ReadOnly = true;
            // 
            // lblDateiname
            // 
            resources.ApplyResources(this.lblDateiname, "lblDateiname");
            this.lblDateiname.Name = "lblDateiname";
            // 
            // txtBeschreibung
            // 
            resources.ApplyResources(this.txtBeschreibung, "txtBeschreibung");
            this.txtBeschreibung.Name = "txtBeschreibung";
            // 
            // cmdDown
            // 
            resources.ApplyResources(this.cmdDown, "cmdDown");
            this.cmdDown.Name = "cmdDown";
            this.cmdDown.UseVisualStyleBackColor = true;
            this.cmdDown.Click += new System.EventHandler(this.cmdDown_Click);
            // 
            // lblBeschreibung
            // 
            resources.ApplyResources(this.lblBeschreibung, "lblBeschreibung");
            this.lblBeschreibung.Name = "lblBeschreibung";
            // 
            // cmdUp
            // 
            resources.ApplyResources(this.cmdUp, "cmdUp");
            this.cmdUp.Name = "cmdUp";
            this.cmdUp.UseVisualStyleBackColor = true;
            this.cmdUp.Click += new System.EventHandler(this.cmdUp_Click);
            // 
            // lblGruppe
            // 
            resources.ApplyResources(this.lblGruppe, "lblGruppe");
            this.lblGruppe.Name = "lblGruppe";
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
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // txtGruppe
            // 
            resources.ApplyResources(this.txtGruppe, "txtGruppe");
            this.txtGruppe.Name = "txtGruppe";
            // 
            // cmdClear
            // 
            resources.ApplyResources(this.cmdClear, "cmdClear");
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.UseVisualStyleBackColor = true;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // cmdInsert
            // 
            resources.ApplyResources(this.cmdInsert, "cmdInsert");
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.UseVisualStyleBackColor = true;
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            // 
            // DokumenteView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.grpDokumente);
            this.Controls.Add(this.cmdCancel);
            this.Name = "DokumenteView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.DokumenteView_Load);
            this.grpDokumente.ResumeLayout(false);
            this.grpDokumente.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.OplListView lvDokumente;
        private Windows.Forms.OplButton cmdCancel;
        private System.Windows.Forms.GroupBox grpDokumente;
        private System.Windows.Forms.Label lblInfo;
        private Windows.Forms.OplButton cmdClear;
        private Windows.Forms.OplButton cmdInsert;
        private System.Windows.Forms.Label label1;
        private Windows.Forms.OplButton cmdDelete;
        private System.Windows.Forms.Label lblGruppe;
        private System.Windows.Forms.Label lblBeschreibung;
        private Windows.Forms.OplTextBox txtGruppe;
        private Windows.Forms.OplTextBox txtBeschreibung;
        private Windows.Forms.OplButton cmdDown;
        private Windows.Forms.OplButton cmdUp;
        private Windows.Forms.OplTextBox txtDateiname;
        private System.Windows.Forms.Label lblDateiname;
        private Windows.Forms.OplButton cmdDateiname;
        private Windows.Forms.OplButton cmdApply;
        private Windows.Forms.OplTextBox txtLfdNummer;
        private System.Windows.Forms.Label lblLfdNummer;
        private Windows.Forms.OplButton cmdDisplay;
        private System.Windows.Forms.Label lblMinMax;
    }
}