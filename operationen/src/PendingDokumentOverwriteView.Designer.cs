namespace Operationen
{
    partial class PendingDokumentOverwriteView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PendingDokumentOverwriteView));
            this.lblInfo = new System.Windows.Forms.Label();
            this.cmdOverwrite = new Windows.Forms.OplButton();
            this.cmdEdit = new Windows.Forms.OplButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lblInfo2 = new System.Windows.Forms.Label();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInfo.Name = "lblInfo";
            // 
            // cmdOverwrite
            // 
            resources.ApplyResources(this.cmdOverwrite, "cmdOverwrite");
            this.cmdOverwrite.Name = "cmdOverwrite";
            this.cmdOverwrite.UseVisualStyleBackColor = true;
            this.cmdOverwrite.Click += new System.EventHandler(this.cmdOverwrite_Click);
            // 
            // cmdEdit
            // 
            resources.ApplyResources(this.cmdEdit, "cmdEdit");
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.UseVisualStyleBackColor = true;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Name = "label1";
            // 
            // lblInfo2
            // 
            resources.ApplyResources(this.lblInfo2, "lblInfo2");
            this.lblInfo2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInfo2.Name = "lblInfo2";
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // PendingDokumentOverwriteView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.lblInfo2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdEdit);
            this.Controls.Add(this.cmdOverwrite);
            this.Controls.Add(this.lblInfo);
            this.MaximizeBox = false;
            this.Name = "PendingDokumentOverwriteView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.PendingDokumentOverwriteView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblInfo;
        private Windows.Forms.OplButton cmdOverwrite;
        private Windows.Forms.OplButton cmdEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblInfo2;
        private Windows.Forms.OplButton cmdCancel;
    }
}