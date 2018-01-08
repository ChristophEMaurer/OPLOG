namespace Operationen.Wizards.ImportOPS
{
    partial class SelectVersion
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectVersion));
            this.radVersion2009 = new System.Windows.Forms.RadioButton();
            this.lblInfo = new System.Windows.Forms.Label();
            this.radVersion2010 = new System.Windows.Forms.RadioButton();
            this.radVersion2013Xml = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // radVersion2009
            // 
            resources.ApplyResources(this.radVersion2009, "radVersion2009");
            this.radVersion2009.Name = "radVersion2009";
            this.radVersion2009.TabStop = true;
            this.radVersion2009.UseVisualStyleBackColor = true;
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.Name = "lblInfo";
            // 
            // radVersion2010
            // 
            resources.ApplyResources(this.radVersion2010, "radVersion2010");
            this.radVersion2010.Name = "radVersion2010";
            this.radVersion2010.TabStop = true;
            this.radVersion2010.UseVisualStyleBackColor = true;
            // 
            // radVersion2013Xml
            // 
            resources.ApplyResources(this.radVersion2013Xml, "radVersion2013Xml");
            this.radVersion2013Xml.Name = "radVersion2013Xml";
            this.radVersion2013Xml.TabStop = true;
            this.radVersion2013Xml.UseVisualStyleBackColor = true;
            // 
            // SelectVersion
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radVersion2013Xml);
            this.Controls.Add(this.radVersion2010);
            this.Controls.Add(this.radVersion2009);
            this.Controls.Add(this.lblInfo);
            this.Name = "SelectVersion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radVersion2009;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.RadioButton radVersion2010;
        private System.Windows.Forms.RadioButton radVersion2013Xml;

    }
}
