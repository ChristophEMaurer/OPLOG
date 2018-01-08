namespace Operationen
{
    partial class AutoImportView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoImportView));
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblFilename = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.MarqueeAnimationSpeed = 10;
            this.progressBar.Name = "progressBar";
            // 
            // lblFilename
            // 
            resources.ApplyResources(this.lblFilename, "lblFilename");
            this.lblFilename.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFilename.Name = "lblFilename";
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.SecurityManager = null;
            this.cmdCancel.UserRight = null;
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // AutoImportView
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblFilename);
            this.Controls.Add(this.progressBar);
            this.MaximizeBox = false;
            this.Name = "AutoImportView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.AutoImportView_Load);
            this.Shown += new System.EventHandler(this.AutoImportView_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblFilename;
        private System.Windows.Forms.Label lblInfo;
        private Windows.Forms.OplButton cmdCancel;

    }
}
