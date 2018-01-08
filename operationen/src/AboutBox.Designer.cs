namespace Operationen
{
    partial class AboutBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
            this.cmdOK = new Windows.Forms.OplButton();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.lvInfos = new Windows.Forms.OplListView();
            this.pbIcon = new System.Windows.Forms.PictureBox();
            this.llChanges = new System.Windows.Forms.LinkLabel();
            this.llLicence = new System.Windows.Forms.LinkLabel();
            this.pnlCredits = new System.Windows.Forms.Panel();
            this.cmdDetails = new Windows.Forms.OplButton();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.SecurityManager = null;
            this.cmdOK.UserRight = null;
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // lblCopyright
            // 
            resources.ApplyResources(this.lblCopyright, "lblCopyright");
            this.lblCopyright.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCopyright.Name = "lblCopyright";
            // 
            // lblProduct
            // 
            resources.ApplyResources(this.lblProduct, "lblProduct");
            this.lblProduct.Name = "lblProduct";
            // 
            // lvInfos
            // 
            resources.ApplyResources(this.lvInfos, "lvInfos");
            this.lvInfos.DoubleClickActivation = false;
            this.lvInfos.Name = "lvInfos";
            this.lvInfos.UseCompatibleStateImageBehavior = false;
            // 
            // pbIcon
            // 
            resources.ApplyResources(this.pbIcon, "pbIcon");
            this.pbIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.TabStop = false;
            this.pbIcon.Click += new System.EventHandler(this.pbIcon_Click);
            // 
            // llChanges
            // 
            resources.ApplyResources(this.llChanges, "llChanges");
            this.llChanges.Name = "llChanges";
            this.llChanges.TabStop = true;
            this.llChanges.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llChanges_LinkClicked);
            // 
            // llLicence
            // 
            resources.ApplyResources(this.llLicence, "llLicence");
            this.llLicence.Name = "llLicence";
            this.llLicence.TabStop = true;
            this.llLicence.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llLicence_LinkClicked);
            // 
            // pnlCredits
            // 
            resources.ApplyResources(this.pnlCredits, "pnlCredits");
            this.pnlCredits.Name = "pnlCredits";
            // 
            // cmdDetails
            // 
            resources.ApplyResources(this.cmdDetails, "cmdDetails");
            this.cmdDetails.Name = "cmdDetails";
            this.cmdDetails.SecurityManager = null;
            this.cmdDetails.UserRight = null;
            this.cmdDetails.UseVisualStyleBackColor = true;
            this.cmdDetails.Click += new System.EventHandler(this.cmdDetails_Click);
            // 
            // AboutBox
            // 
            this.CancelButton = this.cmdOK;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.cmdDetails);
            this.Controls.Add(this.pnlCredits);
            this.Controls.Add(this.llLicence);
            this.Controls.Add(this.llChanges);
            this.Controls.Add(this.pbIcon);
            this.Controls.Add(this.lvInfos);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.lblProduct);
            this.Controls.Add(this.lblCopyright);
            this.Name = "AboutBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.AboutBox_Load);
            this.DoubleClick += new System.EventHandler(this.AboutBox_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Windows.Forms.OplButton cmdOK;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Label lblProduct;
        private Windows.Forms.OplListView lvInfos;
        private System.Windows.Forms.PictureBox pbIcon;
        private System.Windows.Forms.LinkLabel llChanges;
        private System.Windows.Forms.LinkLabel llLicence;
        private System.Windows.Forms.Panel pnlCredits;
        private Windows.Forms.OplButton cmdDetails;

    }
}
