namespace Operationen.Setup
{
    partial class License
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
            this.txtText = new System.Windows.Forms.TextBox();
            this.chkAcceptLicense = new System.Windows.Forms.CheckBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtText
            // 
            this.txtText.Location = new System.Drawing.Point(3, 44);
            this.txtText.Multiline = true;
            this.txtText.Name = "txtText";
            this.txtText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtText.Size = new System.Drawing.Size(344, 226);
            this.txtText.TabIndex = 8;
            // 
            // chkAcceptLicense
            // 
            this.chkAcceptLicense.Location = new System.Drawing.Point(15, 276);
            this.chkAcceptLicense.Name = "chkAcceptLicense";
            this.chkAcceptLicense.Size = new System.Drawing.Size(321, 41);
            this.chkAcceptLicense.TabIndex = 9;
            this.chkAcceptLicense.Text = "Lizenz";
            this.chkAcceptLicense.UseVisualStyleBackColor = true;
            // 
            // lblInfo
            // 
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Location = new System.Drawing.Point(3, 5);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(344, 36);
            this.lblInfo.TabIndex = 10;
            // 
            // License
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.chkAcceptLicense);
            this.Controls.Add(this.txtText);
            this.Name = "License";
            this.Size = new System.Drawing.Size(350, 320);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.CheckBox chkAcceptLicense;
        private System.Windows.Forms.Label lblInfo;
    }
}
