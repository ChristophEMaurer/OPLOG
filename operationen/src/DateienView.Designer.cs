namespace Operationen
{
    partial class DateienView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DateienView));
            this.lvDateien = new Windows.Forms.OplListView();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.grpDateien = new System.Windows.Forms.GroupBox();
            this.cmdApply = new Windows.Forms.OplButton();
            this.cmdDisplay = new Windows.Forms.OplButton();
            this.cbDateiTypen = new System.Windows.Forms.ComboBox();
            this.cmdDateiname = new Windows.Forms.OplButton();
            this.txtDateiname = new Windows.Forms.OplTextBox();
            this.lblDateiname = new System.Windows.Forms.Label();
            this.txtBeschreibung = new Windows.Forms.OplTextBox();
            this.lblBeschreibung = new System.Windows.Forms.Label();
            this.lblDateiTypen = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdDelete = new Windows.Forms.OplButton();
            this.cmdClear = new Windows.Forms.OplButton();
            this.cmdInsert = new Windows.Forms.OplButton();
            this.lblInfo = new System.Windows.Forms.Label();
            this.grpDateien.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvDateien
            // 
            resources.ApplyResources(this.lvDateien, "lvDateien");
            this.lvDateien.DoubleClickActivation = false;
            this.lvDateien.MultiSelect = false;
            this.lvDateien.Name = "lvDateien";
            this.lvDateien.UseCompatibleStateImageBehavior = false;
            this.lvDateien.View = System.Windows.Forms.View.Details;
            this.lvDateien.SelectedIndexChanged += new System.EventHandler(this.lvDateien_SelectedIndexChanged);
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.SecurityManager = null;
            this.cmdCancel.UserRight = null;
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // grpDateien
            // 
            resources.ApplyResources(this.grpDateien, "grpDateien");
            this.grpDateien.Controls.Add(this.cmdApply);
            this.grpDateien.Controls.Add(this.cmdDisplay);
            this.grpDateien.Controls.Add(this.cbDateiTypen);
            this.grpDateien.Controls.Add(this.cmdDateiname);
            this.grpDateien.Controls.Add(this.txtDateiname);
            this.grpDateien.Controls.Add(this.lblDateiname);
            this.grpDateien.Controls.Add(this.txtBeschreibung);
            this.grpDateien.Controls.Add(this.lblBeschreibung);
            this.grpDateien.Controls.Add(this.lblDateiTypen);
            this.grpDateien.Controls.Add(this.label1);
            this.grpDateien.Controls.Add(this.cmdDelete);
            this.grpDateien.Controls.Add(this.cmdClear);
            this.grpDateien.Controls.Add(this.cmdInsert);
            this.grpDateien.Controls.Add(this.lvDateien);
            this.grpDateien.Name = "grpDateien";
            this.grpDateien.TabStop = false;
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
            // cmdDisplay
            // 
            resources.ApplyResources(this.cmdDisplay, "cmdDisplay");
            this.cmdDisplay.Name = "cmdDisplay";
            this.cmdDisplay.SecurityManager = null;
            this.cmdDisplay.UserRight = null;
            this.cmdDisplay.UseVisualStyleBackColor = true;
            this.cmdDisplay.Click += new System.EventHandler(this.cmdDisplay_Click);
            // 
            // cbDateiTypen
            // 
            resources.ApplyResources(this.cbDateiTypen, "cbDateiTypen");
            this.cbDateiTypen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDateiTypen.Name = "cbDateiTypen";
            // 
            // cmdDateiname
            // 
            resources.ApplyResources(this.cmdDateiname, "cmdDateiname");
            this.cmdDateiname.Name = "cmdDateiname";
            this.cmdDateiname.SecurityManager = null;
            this.cmdDateiname.UserRight = null;
            this.cmdDateiname.UseVisualStyleBackColor = true;
            this.cmdDateiname.Click += new System.EventHandler(this.cmdDateiname_Click);
            // 
            // txtDateiname
            // 
            resources.ApplyResources(this.txtDateiname, "txtDateiname");
            this.txtDateiname.Name = "txtDateiname";
            this.txtDateiname.ProtectContents = false;
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
            this.txtBeschreibung.ProtectContents = false;
            // 
            // lblBeschreibung
            // 
            resources.ApplyResources(this.lblBeschreibung, "lblBeschreibung");
            this.lblBeschreibung.Name = "lblBeschreibung";
            // 
            // lblDateiTypen
            // 
            resources.ApplyResources(this.lblDateiTypen, "lblDateiTypen");
            this.lblDateiTypen.Name = "lblDateiTypen";
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
            // cmdClear
            // 
            resources.ApplyResources(this.cmdClear, "cmdClear");
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.SecurityManager = null;
            this.cmdClear.UserRight = null;
            this.cmdClear.UseVisualStyleBackColor = true;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
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
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            // 
            // DateienView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.grpDateien);
            this.Controls.Add(this.cmdCancel);
            this.Name = "DateienView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.DateienView_Load);
            this.Resize += new System.EventHandler(this.DateienView_Resize);
            this.grpDateien.ResumeLayout(false);
            this.grpDateien.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.OplListView lvDateien;
        private Windows.Forms.OplButton cmdCancel;
        private System.Windows.Forms.GroupBox grpDateien;
        private System.Windows.Forms.Label lblInfo;
        private Windows.Forms.OplButton cmdClear;
        private Windows.Forms.OplButton cmdInsert;
        private System.Windows.Forms.Label label1;
        private Windows.Forms.OplButton cmdDelete;
        private System.Windows.Forms.Label lblDateiTypen;
        private System.Windows.Forms.Label lblBeschreibung;
        private Windows.Forms.OplTextBox txtBeschreibung;
        private Windows.Forms.OplTextBox txtDateiname;
        private System.Windows.Forms.Label lblDateiname;
        private Windows.Forms.OplButton cmdDateiname;
        private System.Windows.Forms.ComboBox cbDateiTypen;
        private Windows.Forms.OplButton cmdDisplay;
        private Windows.Forms.OplButton cmdApply;
    }
}