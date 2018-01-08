namespace Operationen.Wizards.ImportChirurg
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
            this.lblText = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.cmdFileName = new System.Windows.Forms.Button();
            this.lblFileName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblText
            // 
            this.lblText.Location = new System.Drawing.Point(11, 10);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(329, 78);
            this.lblText.TabIndex = 3;
            this.lblText.Text = "Info";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(14, 123);
            this.txtFileName.Multiline = true;
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(326, 91);
            this.txtFileName.TabIndex = 5;
            // 
            // cmdFileName
            // 
            this.cmdFileName.Location = new System.Drawing.Point(14, 220);
            this.cmdFileName.Name = "cmdFileName";
            this.cmdFileName.Size = new System.Drawing.Size(326, 23);
            this.cmdFileName.TabIndex = 6;
            this.cmdFileName.Text = "Datei auswählen...";
            this.cmdFileName.UseVisualStyleBackColor = true;
            this.cmdFileName.Click += new System.EventHandler(this.cmdFileName_Click);
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(11, 107);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(61, 13);
            this.lblFileName.TabIndex = 7;
            this.lblFileName.Text = "Dateiname:";
            // 
            // SelectFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.cmdFileName);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.lblText);
            this.Name = "SelectFile";
            this.Size = new System.Drawing.Size(350, 320);
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
