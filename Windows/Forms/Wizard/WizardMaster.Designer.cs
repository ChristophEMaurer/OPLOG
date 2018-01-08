namespace Windows.Forms.Wizard
{
    partial class WizardMaster
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
            this.picPlaceholder = new System.Windows.Forms.PictureBox();
            this.cmdNext = new System.Windows.Forms.Button();
            this.cmdBack = new System.Windows.Forms.Button();
            this.cmdFinish = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.lblHeader1 = new System.Windows.Forms.Label();
            this.lblHeader2 = new System.Windows.Forms.Label();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblLine = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picPlaceholder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // picPlaceholder
            // 
            this.picPlaceholder.Location = new System.Drawing.Point(189, 70);
            this.picPlaceholder.Name = "picPlaceholder";
            this.picPlaceholder.Size = new System.Drawing.Size(350, 320);
            this.picPlaceholder.TabIndex = 0;
            this.picPlaceholder.TabStop = false;
            // 
            // cmdNext
            // 
            this.cmdNext.Location = new System.Drawing.Point(367, 419);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Size = new System.Drawing.Size(81, 23);
            this.cmdNext.TabIndex = 1001;
            this.cmdNext.Text = "&Weiter >>";
            this.cmdNext.UseVisualStyleBackColor = true;
            this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
            // 
            // cmdBack
            // 
            this.cmdBack.Location = new System.Drawing.Point(280, 419);
            this.cmdBack.Name = "cmdBack";
            this.cmdBack.Size = new System.Drawing.Size(81, 23);
            this.cmdBack.TabIndex = 1000;
            this.cmdBack.Text = "<< &Zurück";
            this.cmdBack.UseVisualStyleBackColor = true;
            this.cmdBack.Click += new System.EventHandler(this.cmdBack_Click);
            // 
            // cmdFinish
            // 
            this.cmdFinish.Location = new System.Drawing.Point(189, 419);
            this.cmdFinish.Name = "cmdFinish";
            this.cmdFinish.Size = new System.Drawing.Size(81, 23);
            this.cmdFinish.TabIndex = 1002;
            this.cmdFinish.Text = "&Fertigstellen";
            this.cmdFinish.UseVisualStyleBackColor = true;
            this.cmdFinish.Click += new System.EventHandler(this.cmdFinish_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(454, 419);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(81, 23);
            this.cmdCancel.TabIndex = 1003;
            this.cmdCancel.Text = "&Abbrechen";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // lblHeader1
            // 
            this.lblHeader1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader1.Location = new System.Drawing.Point(20, 3);
            this.lblHeader1.Name = "lblHeader1";
            this.lblHeader1.Size = new System.Drawing.Size(399, 21);
            this.lblHeader1.TabIndex = 5;
            this.lblHeader1.Text = "label1";
            // 
            // lblHeader2
            // 
            this.lblHeader2.Location = new System.Drawing.Point(44, 24);
            this.lblHeader2.Name = "lblHeader2";
            this.lblHeader2.Size = new System.Drawing.Size(375, 30);
            this.lblHeader2.TabIndex = 6;
            this.lblHeader2.Text = "label1";
            // 
            // picImage
            // 
            this.picImage.Dock = System.Windows.Forms.DockStyle.Right;
            this.picImage.InitialImage = null;
            this.picImage.Location = new System.Drawing.Point(430, 0);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(122, 59);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picImage.TabIndex = 7;
            this.picImage.TabStop = false;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Controls.Add(this.picImage);
            this.pnlTop.Controls.Add(this.lblHeader1);
            this.pnlTop.Controls.Add(this.lblHeader2);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(552, 59);
            this.pnlTop.TabIndex = 8;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.SystemColors.Control;
            this.pnlLeft.Location = new System.Drawing.Point(8, 70);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(170, 320);
            this.pnlLeft.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(0, 403);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(552, 3);
            this.label1.TabIndex = 10;
            this.label1.Text = "label1";
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(0, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(552, 3);
            this.label3.TabIndex = 1004;
            this.label3.Text = "label3";
            // 
            // lblLine
            // 
            this.lblLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLine.Location = new System.Drawing.Point(182, 73);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(1, 320);
            this.lblLine.TabIndex = 1005;
            this.lblLine.Text = "label2";
            // 
            // WizardMaster
            // 
            this.AcceptButton = this.cmdNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(552, 454);
            this.Controls.Add(this.lblLine);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdFinish);
            this.Controls.Add(this.cmdBack);
            this.Controls.Add(this.cmdNext);
            this.Controls.Add(this.picPlaceholder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.Name = "WizardMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PageTitle";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WizardMaster_FormClosing);
            this.Load += new System.EventHandler(this.WizardMaster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picPlaceholder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picPlaceholder;
        private System.Windows.Forms.Button cmdNext;
        private System.Windows.Forms.Button cmdBack;
        private System.Windows.Forms.Button cmdFinish;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label lblHeader1;
        private System.Windows.Forms.Label lblHeader2;
        private System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblLine;
    }
}

