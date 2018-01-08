namespace Operationen
{
    partial class CopyWWWProgramUpdateFilesView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CopyWWWProgramUpdateFilesView));
            this.cmdCancel = new Windows.Forms.OplButton();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblInfo3Text = new System.Windows.Forms.Label();
            this.lblInfo1Text = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblInfo2Text = new System.Windows.Forms.Label();
            this.lblInfo1 = new System.Windows.Forms.Label();
            this.cmdCopy = new Windows.Forms.OplButton();
            this.cmdVerzeichnis = new Windows.Forms.OplButton();
            this.txtVerzeichnis = new Windows.Forms.OplTextBox();
            this.lblVerzeichnis = new System.Windows.Forms.Label();
            this.grpInfo.SuspendLayout();
            this.SuspendLayout();
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
            // grpInfo
            // 
            resources.ApplyResources(this.grpInfo, "grpInfo");
            this.grpInfo.Controls.Add(this.label3);
            this.grpInfo.Controls.Add(this.lblInfo3Text);
            this.grpInfo.Controls.Add(this.lblInfo1Text);
            this.grpInfo.Controls.Add(this.label4);
            this.grpInfo.Controls.Add(this.lblInfo2Text);
            this.grpInfo.Controls.Add(this.lblInfo1);
            this.grpInfo.Controls.Add(this.cmdCopy);
            this.grpInfo.Controls.Add(this.cmdVerzeichnis);
            this.grpInfo.Controls.Add(this.txtVerzeichnis);
            this.grpInfo.Controls.Add(this.lblVerzeichnis);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.TabStop = false;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // lblInfo3Text
            // 
            resources.ApplyResources(this.lblInfo3Text, "lblInfo3Text");
            this.lblInfo3Text.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo3Text.Name = "lblInfo3Text";
            // 
            // lblInfo1Text
            // 
            resources.ApplyResources(this.lblInfo1Text, "lblInfo1Text");
            this.lblInfo1Text.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo1Text.Name = "lblInfo1Text";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // lblInfo2Text
            // 
            resources.ApplyResources(this.lblInfo2Text, "lblInfo2Text");
            this.lblInfo2Text.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo2Text.Name = "lblInfo2Text";
            // 
            // lblInfo1
            // 
            resources.ApplyResources(this.lblInfo1, "lblInfo1");
            this.lblInfo1.Name = "lblInfo1";
            // 
            // cmdCopy
            // 
            resources.ApplyResources(this.cmdCopy, "cmdCopy");
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.SecurityManager = null;
            this.cmdCopy.UserRight = null;
            this.cmdCopy.UseVisualStyleBackColor = true;
            this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
            // 
            // cmdVerzeichnis
            // 
            resources.ApplyResources(this.cmdVerzeichnis, "cmdVerzeichnis");
            this.cmdVerzeichnis.Name = "cmdVerzeichnis";
            this.cmdVerzeichnis.SecurityManager = null;
            this.cmdVerzeichnis.UserRight = null;
            this.cmdVerzeichnis.UseVisualStyleBackColor = true;
            this.cmdVerzeichnis.Click += new System.EventHandler(this.cmdVerzeichnis_Click);
            // 
            // txtVerzeichnis
            // 
            resources.ApplyResources(this.txtVerzeichnis, "txtVerzeichnis");
            this.txtVerzeichnis.Name = "txtVerzeichnis";
            this.txtVerzeichnis.ProtectContents = false;
            this.txtVerzeichnis.ReadOnly = true;
            // 
            // lblVerzeichnis
            // 
            resources.ApplyResources(this.lblVerzeichnis, "lblVerzeichnis");
            this.lblVerzeichnis.Name = "lblVerzeichnis";
            // 
            // CopyWWWProgramUpdateFilesView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.grpInfo);
            this.Controls.Add(this.cmdCancel);
            this.Name = "CopyWWWProgramUpdateFilesView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.CopyWWWProgramUpdateFilesView_Load);
            this.grpInfo.ResumeLayout(false);
            this.grpInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.OplButton cmdCancel;
        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.Label lblInfo1;
        private Windows.Forms.OplTextBox txtVerzeichnis;
        private System.Windows.Forms.Label lblVerzeichnis;
        private Windows.Forms.OplButton cmdVerzeichnis;
        private Windows.Forms.OplButton cmdCopy;
        private System.Windows.Forms.Label lblInfo1Text;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblInfo2Text;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblInfo3Text;
    }
}