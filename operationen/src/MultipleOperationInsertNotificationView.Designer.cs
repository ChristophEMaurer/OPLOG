namespace Operationen
{
    partial class MultipleOperationInsertNotificationView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultipleOperationInsertNotificationView));
            this.cmdOK = new Windows.Forms.OplButton();
            this.chkNoDisplay = new Windows.Forms.OplCheckBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.SecurityManager = null;
            this.cmdOK.UserRight = null;
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // chkNoDisplay
            // 
            resources.ApplyResources(this.chkNoDisplay, "chkNoDisplay");
            this.chkNoDisplay.Name = "chkNoDisplay";
            this.chkNoDisplay.UseVisualStyleBackColor = true;
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
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.SecurityManager = null;
            this.cmdCancel.UserRight = null;
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // MultipleOperationInsertNotificationView
            // 
            this.AcceptButton = this.cmdOK;
            this.CancelButton = this.cmdCancel;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.chkNoDisplay);
            this.Controls.Add(this.cmdOK);
            this.MaximizeBox = false;
            this.Name = "MultipleOperationInsertNotificationView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Windows.Forms.OplButton cmdOK;
        private Windows.Forms.OplCheckBox chkNoDisplay;
        private System.Windows.Forms.Label lblInfo;
        private Windows.Forms.OplButton cmdCancel;

    }
}
