namespace Operationen.Wizards.ImportChirurg
{
    partial class ChirurgenView
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdCancel = new Windows.Forms.OplButton();
            this.grpChirurgen = new System.Windows.Forms.GroupBox();
            this.lvChirurgen = new Windows.Forms.OplListView();
            this.cmdOK = new Windows.Forms.OplButton();
            this.lblInfo = new System.Windows.Forms.Label();
            this.grpChirurgen.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(369, 490);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(82, 23);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Abbrechen";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // grpChirurgen
            // 
            this.grpChirurgen.Controls.Add(this.lvChirurgen);
            this.grpChirurgen.Location = new System.Drawing.Point(12, 106);
            this.grpChirurgen.Name = "grpChirurgen";
            this.grpChirurgen.Size = new System.Drawing.Size(439, 373);
            this.grpChirurgen.TabIndex = 2;
            this.grpChirurgen.TabStop = false;
            this.grpChirurgen.Text = "Operateure";
            // 
            // lvChirurgen
            // 
            this.lvChirurgen.FullRowSelect = true;
            this.lvChirurgen.Location = new System.Drawing.Point(6, 19);
            this.lvChirurgen.Name = "lvChirurgen";
            this.lvChirurgen.Size = new System.Drawing.Size(427, 348);
            this.lvChirurgen.TabIndex = 0;
            this.lvChirurgen.UseCompatibleStateImageBehavior = false;
            this.lvChirurgen.DoubleClick += new System.EventHandler(this.lvChirurgen_DoubleClick);
            // 
            // cmdOK
            // 
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Location = new System.Drawing.Point(281, 490);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(82, 23);
            this.cmdOK.TabIndex = 4;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Location = new System.Drawing.Point(12, 9);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(439, 81);
            this.lblInfo.TabIndex = 5;
            this.lblInfo.Text = "Info";
            // 
            // ChirurgenView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 525);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.grpChirurgen);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.Name = "ChirurgenView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChirurgenView";
            this.Load += new System.EventHandler(this.ChirurgenView_Load);
            this.grpChirurgen.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.OplButton cmdCancel;
        private System.Windows.Forms.GroupBox grpChirurgen;
        private Windows.Forms.OplListView lvChirurgen;
        private Windows.Forms.OplButton cmdOK;
        private System.Windows.Forms.Label lblInfo;
    }
}