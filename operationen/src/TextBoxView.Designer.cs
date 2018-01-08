namespace Operationen
{
    partial class TextBoxView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextBoxView));
            this.cmdOK = new Windows.Forms.OplButton();
            this.lvInfos = new Windows.Forms.OplListView();
            this.lblHeader = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // lvInfos
            // 
            resources.ApplyResources(this.lvInfos, "lvInfos");
            this.lvInfos.Name = "lvInfos";
            this.lvInfos.UseCompatibleStateImageBehavior = false;
            // 
            // lblHeader
            // 
            resources.ApplyResources(this.lblHeader, "lblHeader");
            this.lblHeader.Name = "lblHeader";
            // 
            // TextBoxView
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lvInfos);
            this.Controls.Add(this.cmdOK);
            this.MaximizeBox = false;
            this.Name = "TextBoxView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Windows.Forms.OplButton cmdOK;
        private Windows.Forms.OplListView lvInfos;
        private System.Windows.Forms.Label lblHeader;

    }
}
