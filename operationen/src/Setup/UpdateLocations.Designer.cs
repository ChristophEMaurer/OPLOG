namespace Operationen.Setup
{
    partial class UpdateLocations
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
            this.lblInfo = new System.Windows.Forms.Label();
            this.txtProgramDirectory = new System.Windows.Forms.TextBox();
            this.cmdProgramDirectory = new System.Windows.Forms.Button();
            this.grpProgram = new System.Windows.Forms.GroupBox();
            this.grpProgram.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(8, 9);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(61, 13);
            this.lblInfo.TabIndex = 7;
            this.lblInfo.Text = "Verzeichnis";
            // 
            // txtProgramDirectory
            // 
            this.txtProgramDirectory.Location = new System.Drawing.Point(6, 19);
            this.txtProgramDirectory.Multiline = true;
            this.txtProgramDirectory.Name = "txtProgramDirectory";
            this.txtProgramDirectory.ReadOnly = true;
            this.txtProgramDirectory.Size = new System.Drawing.Size(313, 50);
            this.txtProgramDirectory.TabIndex = 8;
            // 
            // cmdProgramDirectory
            // 
            this.cmdProgramDirectory.Location = new System.Drawing.Point(6, 75);
            this.cmdProgramDirectory.Name = "cmdProgramDirectory";
            this.cmdProgramDirectory.Size = new System.Drawing.Size(313, 29);
            this.cmdProgramDirectory.TabIndex = 9;
            this.cmdProgramDirectory.Text = "Verzeichnis auswählen...";
            this.cmdProgramDirectory.UseVisualStyleBackColor = true;
            this.cmdProgramDirectory.Click += new System.EventHandler(this.cmdDirectory_Click);
            // 
            // grpProgram
            // 
            this.grpProgram.Controls.Add(this.txtProgramDirectory);
            this.grpProgram.Controls.Add(this.cmdProgramDirectory);
            this.grpProgram.Location = new System.Drawing.Point(12, 25);
            this.grpProgram.Name = "grpProgram";
            this.grpProgram.Size = new System.Drawing.Size(326, 120);
            this.grpProgram.TabIndex = 15;
            this.grpProgram.TabStop = false;
            this.grpProgram.Text = "Programm";
            // 
            // UpdateLocations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpProgram);
            this.Controls.Add(this.lblInfo);
            this.Name = "UpdateLocations";
            this.Size = new System.Drawing.Size(350, 320);
            this.grpProgram.ResumeLayout(false);
            this.grpProgram.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.TextBox txtProgramDirectory;
        private System.Windows.Forms.Button cmdProgramDirectory;
        private System.Windows.Forms.GroupBox grpProgram;
    }
}
