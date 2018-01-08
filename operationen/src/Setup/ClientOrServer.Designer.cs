namespace Operationen.Setup
{
    partial class ClientOrServer
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
            this.radStandalone = new System.Windows.Forms.RadioButton();
            this.radMultiMany = new System.Windows.Forms.RadioButton();
            this.radMultiOneProgram = new System.Windows.Forms.RadioButton();
            this.radMultiOneShortcut = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Location = new System.Drawing.Point(15, 18);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(320, 43);
            this.lblInfo.TabIndex = 7;
            this.lblInfo.Text = "Info";
            // 
            // radStandalone
            // 
            this.radStandalone.Location = new System.Drawing.Point(18, 64);
            this.radStandalone.Name = "radStandalone";
            this.radStandalone.Size = new System.Drawing.Size(317, 46);
            this.radStandalone.TabIndex = 10;
            this.radStandalone.Text = "1. Ein Benutzer: Ich möchte das Programm alleine benutzen und greife als einziger" +
                " auf die Daten zu.";
            this.radStandalone.UseVisualStyleBackColor = true;
            // 
            // radMultiMany
            // 
            this.radMultiMany.Checked = true;
            this.radMultiMany.Location = new System.Drawing.Point(18, 116);
            this.radMultiMany.Name = "radMultiMany";
            this.radMultiMany.Size = new System.Drawing.Size(317, 71);
            this.radMultiMany.TabIndex = 11;
            this.radMultiMany.TabStop = true;
            this.radMultiMany.Text = "2. Mehrere Benutzer: Jeder installiert das Programm auf seinem PC und alle greife" +
                "n auf dieselbe Daten zu. Alle Benutzer führen diese Installation aus.";
            this.radMultiMany.UseVisualStyleBackColor = true;
            // 
            // radMultiOneProgram
            // 
            this.radMultiOneProgram.Checked = true;
            this.radMultiOneProgram.Location = new System.Drawing.Point(18, 193);
            this.radMultiOneProgram.Name = "radMultiOneProgram";
            this.radMultiOneProgram.Size = new System.Drawing.Size(317, 52);
            this.radMultiOneProgram.TabIndex = 12;
            this.radMultiOneProgram.TabStop = true;
            this.radMultiOneProgram.Text = "3a. Mehrere Benutzer, eine zentrale Installation: Ich installiere das Programm an" +
                " einer zentralen Stelle, alle anderen führen 3b. aus.";
            this.radMultiOneProgram.UseVisualStyleBackColor = true;
            // 
            // radMultiOneShortcut
            // 
            this.radMultiOneShortcut.Checked = true;
            this.radMultiOneShortcut.Location = new System.Drawing.Point(18, 251);
            this.radMultiOneShortcut.Name = "radMultiOneShortcut";
            this.radMultiOneShortcut.Size = new System.Drawing.Size(317, 45);
            this.radMultiOneShortcut.TabIndex = 13;
            this.radMultiOneShortcut.TabStop = true;
            this.radMultiOneShortcut.Text = "3b. Mehrere Benutzer, eine zentrale Installation: Das Programm ist bereits am zen" +
                "traler Stelle installiert, ich möchte nur eine Verknüpfung anlegen.";
            this.radMultiOneShortcut.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(15, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 3);
            this.label1.TabIndex = 14;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(15, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(320, 3);
            this.label2.TabIndex = 15;
            this.label2.Text = "label2";
            // 
            // ClientOrServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radMultiOneShortcut);
            this.Controls.Add(this.radMultiOneProgram);
            this.Controls.Add(this.radStandalone);
            this.Controls.Add(this.radMultiMany);
            this.Controls.Add(this.lblInfo);
            this.Name = "ClientOrServer";
            this.Size = new System.Drawing.Size(350, 320);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.RadioButton radStandalone;
        private System.Windows.Forms.RadioButton radMultiMany;
        private System.Windows.Forms.RadioButton radMultiOneProgram;
        private System.Windows.Forms.RadioButton radMultiOneShortcut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
