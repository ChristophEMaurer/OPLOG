namespace Operationen.Wizards.ImportOperationenMobile
{
    partial class SelectFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectFile));
            this.lblText = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.cmdFileName = new System.Windows.Forms.Button();
            this.lblFileName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblText
            // 
            resources.ApplyResources(this.lblText, "lblText");
            this.lblText.Name = "lblText";
            // 
            // txtFileName
            // 
            resources.ApplyResources(this.txtFileName, "txtFileName");
            this.txtFileName.Name = "txtFileName";
            // 
            // cmdFileName
            // 
            resources.ApplyResources(this.cmdFileName, "cmdFileName");
            this.cmdFileName.Name = "cmdFileName";
            this.cmdFileName.UseVisualStyleBackColor = true;
            this.cmdFileName.Click += new System.EventHandler(this.cmdFileName_Click);
            // 
            // lblFileName
            // 
            resources.ApplyResources(this.lblFileName, "lblFileName");
            this.lblFileName.Name = "lblFileName";
            // 
            // SelectFile
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.cmdFileName);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.lblText);
            this.Name = "SelectFile";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button cmdFileName;
        private System.Windows.Forms.Label lblFileName;
    }
}
