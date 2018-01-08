namespace Operationen
{
    partial class SelectFunktionView
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
            this.chkOp = new Windows.Forms.OplCheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAss3 = new Windows.Forms.OplCheckBox();
            this.chkAss2 = new Windows.Forms.OplCheckBox();
            this.chkAss1 = new Windows.Forms.OplCheckBox();
            this.cmdOK = new Windows.Forms.OplButton();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkOp
            // 
            this.chkOp.AutoSize = true;
            this.chkOp.Location = new System.Drawing.Point(19, 33);
            this.chkOp.Name = "chkOp";
            this.chkOp.Size = new System.Drawing.Size(73, 17);
            this.chkOp.TabIndex = 0;
            this.chkOp.Text = "Operateur";
            this.chkOp.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chkAss3);
            this.groupBox1.Controls.Add(this.chkAss2);
            this.groupBox1.Controls.Add(this.chkAss1);
            this.groupBox1.Controls.Add(this.chkOp);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 214);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Funktionen";
            // 
            // chkAss3
            // 
            this.chkAss3.AutoSize = true;
            this.chkAss3.Location = new System.Drawing.Point(19, 102);
            this.chkAss3.Name = "chkAss3";
            this.chkAss3.Size = new System.Drawing.Size(80, 17);
            this.chkAss3.TabIndex = 3;
            this.chkAss3.Text = "3. Assistent";
            this.chkAss3.UseVisualStyleBackColor = true;
            // 
            // chkAss2
            // 
            this.chkAss2.AutoSize = true;
            this.chkAss2.Location = new System.Drawing.Point(19, 79);
            this.chkAss2.Name = "chkAss2";
            this.chkAss2.Size = new System.Drawing.Size(80, 17);
            this.chkAss2.TabIndex = 2;
            this.chkAss2.Text = "2. Assistent";
            this.chkAss2.UseVisualStyleBackColor = true;
            // 
            // chkAss1
            // 
            this.chkAss1.AutoSize = true;
            this.chkAss1.Location = new System.Drawing.Point(19, 56);
            this.chkAss1.Name = "chkAss1";
            this.chkAss1.Size = new System.Drawing.Size(80, 17);
            this.chkAss1.TabIndex = 1;
            this.chkAss1.Text = "1. Assistent";
            this.chkAss1.UseVisualStyleBackColor = true;
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Location = new System.Drawing.Point(99, 232);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(82, 23);
            this.cmdOK.TabIndex = 2;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(187, 232);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(82, 23);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Abbrechen";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // SelectFunktionView
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(281, 267);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.groupBox1);
            this.Name = "SelectFunktionView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "SelectFunktionView";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.OplCheckBox chkOp;
        private System.Windows.Forms.GroupBox groupBox1;
        private Windows.Forms.OplButton cmdOK;
        private Windows.Forms.OplButton cmdCancel;
        private Windows.Forms.OplCheckBox chkAss3;
        private Windows.Forms.OplCheckBox chkAss2;
        private Windows.Forms.OplCheckBox chkAss1;
    }
}