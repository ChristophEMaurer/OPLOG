namespace Operationen.Setup
{
    partial class Locations
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtProgramDirectory = new System.Windows.Forms.TextBox();
            this.cmdProgramDirectory = new System.Windows.Forms.Button();
            this.cmdDatabaseDirectory = new System.Windows.Forms.Button();
            this.txtDatabaseDirectory = new System.Windows.Forms.TextBox();
            this.grpProgram = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.grpProgram.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(8, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(222, 13);
            this.lblTitle.TabIndex = 7;
            this.lblTitle.Text = "Wählen Sie nun die Installationsverzeichnisse";
            // 
            // txtProgramDirectory
            // 
            this.txtProgramDirectory.Location = new System.Drawing.Point(6, 19);
            this.txtProgramDirectory.Multiline = true;
            this.txtProgramDirectory.Name = "txtProgramDirectory";
            this.txtProgramDirectory.Size = new System.Drawing.Size(313, 52);
            this.txtProgramDirectory.TabIndex = 8;
            this.txtProgramDirectory.TextChanged += new System.EventHandler(this.txtProgramDirectory_TextChanged);
            // 
            // cmdProgramDirectory
            // 
            this.cmdProgramDirectory.Location = new System.Drawing.Point(6, 77);
            this.cmdProgramDirectory.Name = "cmdProgramDirectory";
            this.cmdProgramDirectory.Size = new System.Drawing.Size(313, 33);
            this.cmdProgramDirectory.TabIndex = 9;
            this.cmdProgramDirectory.Text = "Programmverzeichnis auswählen...";
            this.cmdProgramDirectory.UseVisualStyleBackColor = true;
            this.cmdProgramDirectory.Click += new System.EventHandler(this.cmdDirectory_Click);
            // 
            // cmdDatabaseDirectory
            // 
            this.cmdDatabaseDirectory.Location = new System.Drawing.Point(7, 77);
            this.cmdDatabaseDirectory.Name = "cmdDatabaseDirectory";
            this.cmdDatabaseDirectory.Size = new System.Drawing.Size(313, 33);
            this.cmdDatabaseDirectory.TabIndex = 13;
            this.cmdDatabaseDirectory.Text = "Verzeichnis der gemeinsamen Daten auswählen...";
            this.cmdDatabaseDirectory.UseVisualStyleBackColor = true;
            this.cmdDatabaseDirectory.Click += new System.EventHandler(this.cmdDatabaseDirectory_Click);
            // 
            // txtDatabaseDirectory
            // 
            this.txtDatabaseDirectory.Location = new System.Drawing.Point(7, 19);
            this.txtDatabaseDirectory.Multiline = true;
            this.txtDatabaseDirectory.Name = "txtDatabaseDirectory";
            this.txtDatabaseDirectory.Size = new System.Drawing.Size(313, 52);
            this.txtDatabaseDirectory.TabIndex = 12;
            // 
            // grpProgram
            // 
            this.grpProgram.Controls.Add(this.txtProgramDirectory);
            this.grpProgram.Controls.Add(this.cmdProgramDirectory);
            this.grpProgram.Location = new System.Drawing.Point(11, 25);
            this.grpProgram.Name = "grpProgram";
            this.grpProgram.Size = new System.Drawing.Size(326, 117);
            this.grpProgram.TabIndex = 15;
            this.grpProgram.TabStop = false;
            this.grpProgram.Text = "Programmverzeichnis";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDatabaseDirectory);
            this.groupBox2.Controls.Add(this.cmdDatabaseDirectory);
            this.groupBox2.Location = new System.Drawing.Point(11, 148);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(326, 117);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Verzeichnis der gemeinsamen Daten";
            // 
            // lblInfo
            // 
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Location = new System.Drawing.Point(11, 268);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(320, 43);
            this.lblInfo.TabIndex = 17;
            this.lblInfo.Text = "Info";
            // 
            // Locations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpProgram);
            this.Controls.Add(this.lblTitle);
            this.Name = "Locations";
            this.Size = new System.Drawing.Size(350, 320);
            this.Load += new System.EventHandler(this.Locations_Load);
            this.grpProgram.ResumeLayout(false);
            this.grpProgram.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtProgramDirectory;
        private System.Windows.Forms.Button cmdProgramDirectory;
        private System.Windows.Forms.Button cmdDatabaseDirectory;
        private System.Windows.Forms.TextBox txtDatabaseDirectory;
        private System.Windows.Forms.GroupBox grpProgram;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblInfo;
    }
}
