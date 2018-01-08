namespace Operationen.Setup
{
    partial class Summary
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
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // txtSummary
            // 
            this.txtSummary.Location = new System.Drawing.Point(15, 16);
            this.txtSummary.Multiline = true;
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.Size = new System.Drawing.Size(321, 229);
            this.txtSummary.TabIndex = 8;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(15, 266);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(321, 22);
            this.progressBar.TabIndex = 9;
            // 
            // Summary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.txtSummary);
            this.Name = "Summary";
            this.Size = new System.Drawing.Size(350, 320);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSummary;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}
