namespace Operationen.Wizards.ImportRichtlinien
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.cmdFileName = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(324, 34);
            this.label1.TabIndex = 3;
            this.label1.Text = "Wählen Sie die Datei, aus der die Richtlinien importiert werden sollen:";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(16, 51);
            this.txtFileName.Multiline = true;
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(321, 91);
            this.txtFileName.TabIndex = 5;
            // 
            // cmdFileName
            // 
            this.cmdFileName.Location = new System.Drawing.Point(16, 148);
            this.cmdFileName.Name = "cmdFileName";
            this.cmdFileName.Size = new System.Drawing.Size(321, 23);
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
            this.Controls.Add(this.label1);
            this.Name = "SelectFile";
            this.Size = new System.Drawing.Size(350, 320);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button cmdFileName;
    }
}
