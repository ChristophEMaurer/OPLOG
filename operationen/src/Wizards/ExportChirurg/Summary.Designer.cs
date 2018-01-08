namespace Operationen.Wizards.ExportChirurg
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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblSummary = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(14, 236);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(320, 22);
            this.progressBar.TabIndex = 10;
            // 
            // lblProgress
            // 
            this.lblProgress.Location = new System.Drawing.Point(11, 271);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(323, 13);
            this.lblProgress.TabIndex = 13;
            this.lblProgress.Text = "info";
            // 
            // lblSummary
            // 
            this.lblSummary.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSummary.Location = new System.Drawing.Point(11, 12);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(323, 188);
            this.lblSummary.TabIndex = 14;
            this.lblSummary.Text = "info";
            // 
            // Summary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblSummary);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.progressBar);
            this.Name = "Summary";
            this.Size = new System.Drawing.Size(350, 320);
            this.Load += new System.EventHandler(this.Summary_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblSummary;
    }
}
