namespace Windows.Forms
{
    partial class DateBoxPicker
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
            this.txtDate = new Windows.Forms.OplTextBox();
            this.cmdDate = new Windows.Forms.OplButton();
            this.SuspendLayout();
            // 
            // txtDate
            // 
            this.txtDate.Location = new System.Drawing.Point(0, 0);
            this.txtDate.MaxLength = 10;
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(77, 20);
            this.txtDate.TabIndex = 0;
            // 
            // cmdDate
            // 
            this.cmdDate.Location = new System.Drawing.Point(83, -1);
            this.cmdDate.Name = "cmdDate";
            this.cmdDate.Size = new System.Drawing.Size(25, 21);
            this.cmdDate.TabIndex = 1;
            this.cmdDate.Text = "...";
            this.cmdDate.UseVisualStyleBackColor = true;
            this.cmdDate.Click += new System.EventHandler(this.cmdDate_Click);
            // 
            // DateBoxPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmdDate);
            this.Controls.Add(this.txtDate);
            this.Name = "DateBoxPicker";
            this.Size = new System.Drawing.Size(110, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Windows.Forms.OplTextBox txtDate;
        private Windows.Forms.OplButton cmdDate;
    }
}
