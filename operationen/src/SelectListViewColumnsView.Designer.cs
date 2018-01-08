namespace Operationen
{
    partial class SelectListViewColumnsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectListViewColumnsView));
            this.cmdCancel = new Windows.Forms.OplButton();
            this.grpListView = new System.Windows.Forms.GroupBox();
            this.lvListView = new Windows.Forms.OplListView();
            this.cmdOK = new Windows.Forms.OplButton();
            this.lblInfo = new System.Windows.Forms.Label();
            this.grpListView.SuspendLayout();
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
            // grpListView
            // 
            resources.ApplyResources(this.grpListView, "grpListView");
            this.grpListView.Controls.Add(this.lvListView);
            this.grpListView.Name = "grpListView";
            this.grpListView.TabStop = false;
            // 
            // lvListView
            // 
            resources.ApplyResources(this.lvListView, "lvListView");
            this.lvListView.DoubleClickActivation = false;
            this.lvListView.FullRowSelect = true;
            this.lvListView.Name = "lvListView";
            this.lvListView.UseCompatibleStateImageBehavior = false;
            this.lvListView.View = System.Windows.Forms.View.Details;
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
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            // 
            // SelectListViewColumnsView
            // 
            this.AcceptButton = this.cmdOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.grpListView);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Name = "SelectListViewColumnsView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.ChirurgenView_Load);
            this.grpListView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.OplButton cmdCancel;
        private System.Windows.Forms.GroupBox grpListView;
        private Windows.Forms.OplButton cmdOK;
        private System.Windows.Forms.Label lblInfo;
        private Windows.Forms.OplListView lvListView;
    }
}