namespace Operationen
{
    partial class UpdateSerialnumbersView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateSerialnumbersView));
            this.grpChirurgen = new System.Windows.Forms.GroupBox();
            this.txtSerialNeeded = new Windows.Forms.OplTextBox();
            this.cmdAuto = new Windows.Forms.OplButton();
            this.lblSerialNeeded = new System.Windows.Forms.Label();
            this.lblSerialExisting = new System.Windows.Forms.Label();
            this.txtSerialExisting = new Windows.Forms.OplTextBox();
            this.cmdSerialNumberView = new Windows.Forms.OplButton();
            this.cmdOK = new Windows.Forms.OplButton();
            this.lblInfo = new System.Windows.Forms.Label();
            this.cmdInstallLicense = new Windows.Forms.OplButton();
            this.grpChirurgen.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpChirurgen
            // 
            resources.ApplyResources(this.grpChirurgen, "grpChirurgen");
            this.grpChirurgen.Controls.Add(this.txtSerialNeeded);
            this.grpChirurgen.Controls.Add(this.cmdAuto);
            this.grpChirurgen.Controls.Add(this.lblSerialNeeded);
            this.grpChirurgen.Controls.Add(this.lblSerialExisting);
            this.grpChirurgen.Controls.Add(this.txtSerialExisting);
            this.grpChirurgen.Name = "grpChirurgen";
            this.grpChirurgen.TabStop = false;
            // 
            // txtSerialNeeded
            // 
            resources.ApplyResources(this.txtSerialNeeded, "txtSerialNeeded");
            this.txtSerialNeeded.Name = "txtSerialNeeded";
            this.txtSerialNeeded.ReadOnly = true;
            // 
            // cmdAuto
            // 
            resources.ApplyResources(this.cmdAuto, "cmdAuto");
            this.cmdAuto.Name = "cmdAuto";
            this.cmdAuto.UseVisualStyleBackColor = true;
            this.cmdAuto.Click += new System.EventHandler(this.cmdSerialExisting_Click);
            // 
            // lblSerialNeeded
            // 
            resources.ApplyResources(this.lblSerialNeeded, "lblSerialNeeded");
            this.lblSerialNeeded.Name = "lblSerialNeeded";
            // 
            // lblSerialExisting
            // 
            resources.ApplyResources(this.lblSerialExisting, "lblSerialExisting");
            this.lblSerialExisting.Name = "lblSerialExisting";
            // 
            // txtSerialExisting
            // 
            resources.ApplyResources(this.txtSerialExisting, "txtSerialExisting");
            this.txtSerialExisting.Name = "txtSerialExisting";
            this.txtSerialExisting.ReadOnly = true;
            // 
            // cmdSerialNumberView
            // 
            resources.ApplyResources(this.cmdSerialNumberView, "cmdSerialNumberView");
            this.cmdSerialNumberView.Name = "cmdSerialNumberView";
            this.cmdSerialNumberView.UseVisualStyleBackColor = true;
            this.cmdSerialNumberView.Click += new System.EventHandler(this.cmdSerialNumberView_Click);
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.UseVisualStyleBackColor = true;
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            // 
            // cmdInstallLicense
            // 
            resources.ApplyResources(this.cmdInstallLicense, "cmdInstallLicense");
            this.cmdInstallLicense.Name = "cmdInstallLicense";
            this.cmdInstallLicense.UseVisualStyleBackColor = true;
            this.cmdInstallLicense.Click += new System.EventHandler(this.cmdInstallLicense_Click);
            // 
            // UpdateSerialnumbersView
            // 
            this.AcceptButton = this.cmdOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdOK;
            this.Controls.Add(this.cmdSerialNumberView);
            this.Controls.Add(this.cmdInstallLicense);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.grpChirurgen);
            this.Controls.Add(this.cmdOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateSerialnumbersView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.UpdateSerialnumbersView_Load);
            this.grpChirurgen.ResumeLayout(false);
            this.grpChirurgen.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpChirurgen;
        private Windows.Forms.OplButton cmdOK;
        private System.Windows.Forms.Label lblInfo;
        private Windows.Forms.OplTextBox txtSerialExisting;
        private Windows.Forms.OplButton cmdAuto;
        private Windows.Forms.OplButton cmdSerialNumberView;
        private System.Windows.Forms.Label lblSerialNeeded;
        private System.Windows.Forms.Label lblSerialExisting;
        private Windows.Forms.OplTextBox txtSerialNeeded;
        private Windows.Forms.OplButton cmdInstallLicense;
    }
}