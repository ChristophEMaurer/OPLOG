namespace Operationen
{
    partial class RichtlinieSelectView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RichtlinieSelectView));
            this.grpRichtlinien = new System.Windows.Forms.GroupBox();
            this.lvRichtlinien = new Windows.Forms.OplListView();
            this.cmdOK = new Windows.Forms.OplButton();
            this.lblGebiete = new System.Windows.Forms.Label();
            this.cbGebiete = new System.Windows.Forms.ComboBox();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.grpRichtlinien.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpRichtlinien
            // 
            resources.ApplyResources(this.grpRichtlinien, "grpRichtlinien");
            this.grpRichtlinien.Controls.Add(this.lvRichtlinien);
            this.grpRichtlinien.Name = "grpRichtlinien";
            this.grpRichtlinien.TabStop = false;
            // 
            // lvRichtlinien
            // 
            resources.ApplyResources(this.lvRichtlinien, "lvRichtlinien");
            this.lvRichtlinien.DoubleClickActivation = false;
            this.lvRichtlinien.MultiSelect = false;
            this.lvRichtlinien.Name = "lvRichtlinien";
            this.lvRichtlinien.UseCompatibleStateImageBehavior = false;
            this.lvRichtlinien.View = System.Windows.Forms.View.Details;
            this.lvRichtlinien.DoubleClick += new System.EventHandler(this.lvRichtlinien_DoubleClick);
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.SecurityManager = null;
            this.cmdOK.UserRight = null;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // lblGebiete
            // 
            resources.ApplyResources(this.lblGebiete, "lblGebiete");
            this.lblGebiete.Name = "lblGebiete";
            // 
            // cbGebiete
            // 
            resources.ApplyResources(this.cbGebiete, "cbGebiete");
            this.cbGebiete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGebiete.FormattingEnabled = true;
            this.cbGebiete.Name = "cbGebiete";
            this.cbGebiete.SelectedIndexChanged += new System.EventHandler(this.cbGebiete_SelectedIndexChanged);
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.SecurityManager = null;
            this.cmdCancel.UserRight = null;
            // 
            // RichtlinieSelectView
            // 
            this.AcceptButton = this.cmdOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.lblGebiete);
            this.Controls.Add(this.cbGebiete);
            this.Controls.Add(this.grpRichtlinien);
            this.Controls.Add(this.cmdCancel);
            this.MaximizeBox = false;
            this.Name = "RichtlinieSelectView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.RichtlinieSelectView_Load);
            this.grpRichtlinien.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Windows.Forms.OplButton cmdCancel;
        private System.Windows.Forms.GroupBox grpRichtlinien;
        private Windows.Forms.OplListView lvRichtlinien;
        private Windows.Forms.OplButton cmdOK;
        private System.Windows.Forms.Label lblGebiete;
        private System.Windows.Forms.ComboBox cbGebiete;
    }
}