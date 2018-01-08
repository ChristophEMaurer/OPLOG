namespace Operationen.Wizards.ImportOPS
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
            this.lblFile = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.cmdFileName = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblFile
            // 
            this.lblFile.Location = new System.Drawing.Point(19, 21);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(315, 80);
            this.lblFile.TabIndex = 3;
            this.lblFile.Text = "File:";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(19, 104);
            this.txtFileName.Multiline = true;
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(315, 58);
            this.txtFileName.TabIndex = 5;
            // 
            // cmdFileName
            // 
            this.cmdFileName.Location = new System.Drawing.Point(19, 168);
            this.cmdFileName.Name = "cmdFileName";
            this.cmdFileName.Size = new System.Drawing.Size(315, 23);
            this.cmdFileName.TabIndex = 6;
            this.cmdFileName.Text = "Datei auswählen...";
            this.cmdFileName.UseVisualStyleBackColor = true;
            this.cmdFileName.Click += new System.EventHandler(this.cmdFileName_Click);
            // 
            // SelectFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmdFileName);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.lblFile);
            this.Name = "SelectFile";
            this.Size = new System.Drawing.Size(350, 320);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button cmdFileName;
    }
}
