namespace Operationen.Wizards.ImportRichtlinienZuordnung
{
    partial class RichtlinienValidateView
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
            this.grpVorhandeneRichtlinien = new System.Windows.Forms.GroupBox();
            this.lblVorhandenesGebiet2 = new System.Windows.Forms.Label();
            this.lblVorhandenesGebiet = new System.Windows.Forms.Label();
            this.lvVorhandeneRichtlinien = new Windows.Forms.OplListView();
            this.cmdOK = new Windows.Forms.OplButton();
            this.grpNeueRichtlinien = new System.Windows.Forms.GroupBox();
            this.lblNeuesGebiet2 = new System.Windows.Forms.Label();
            this.lblNeuesGebiet = new System.Windows.Forms.Label();
            this.lvNeueRichtlinien = new Windows.Forms.OplListView();
            this.chkAccept = new Windows.Forms.OplCheckBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblInfo2 = new System.Windows.Forms.Label();
            this.grpVorhandeneRichtlinien.SuspendLayout();
            this.grpNeueRichtlinien.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(903, 698);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(82, 23);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Abbrechen";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // grpVorhandeneRichtlinien
            // 
            this.grpVorhandeneRichtlinien.Controls.Add(this.lblVorhandenesGebiet2);
            this.grpVorhandeneRichtlinien.Controls.Add(this.lblVorhandenesGebiet);
            this.grpVorhandeneRichtlinien.Controls.Add(this.lvVorhandeneRichtlinien);
            this.grpVorhandeneRichtlinien.Location = new System.Drawing.Point(12, 12);
            this.grpVorhandeneRichtlinien.Name = "grpVorhandeneRichtlinien";
            this.grpVorhandeneRichtlinien.Size = new System.Drawing.Size(979, 253);
            this.grpVorhandeneRichtlinien.TabIndex = 2;
            this.grpVorhandeneRichtlinien.TabStop = false;
            this.grpVorhandeneRichtlinien.Text = "Vorhandende Richtlinien";
            // 
            // lblVorhandenesGebiet2
            // 
            this.lblVorhandenesGebiet2.AutoSize = true;
            this.lblVorhandenesGebiet2.Location = new System.Drawing.Point(53, 16);
            this.lblVorhandenesGebiet2.Name = "lblVorhandenesGebiet2";
            this.lblVorhandenesGebiet2.Size = new System.Drawing.Size(41, 13);
            this.lblVorhandenesGebiet2.TabIndex = 4;
            this.lblVorhandenesGebiet2.Text = "Gebiet:";
            // 
            // lblVorhandenesGebiet
            // 
            this.lblVorhandenesGebiet.AutoSize = true;
            this.lblVorhandenesGebiet.Location = new System.Drawing.Point(6, 16);
            this.lblVorhandenesGebiet.Name = "lblVorhandenesGebiet";
            this.lblVorhandenesGebiet.Size = new System.Drawing.Size(41, 13);
            this.lblVorhandenesGebiet.TabIndex = 3;
            this.lblVorhandenesGebiet.Text = "Gebiet:";
            // 
            // lvVorhandeneRichtlinien
            // 
            this.lvVorhandeneRichtlinien.FullRowSelect = true;
            this.lvVorhandeneRichtlinien.Location = new System.Drawing.Point(6, 32);
            this.lvVorhandeneRichtlinien.Name = "lvVorhandeneRichtlinien";
            this.lvVorhandeneRichtlinien.Size = new System.Drawing.Size(967, 213);
            this.lvVorhandeneRichtlinien.TabIndex = 0;
            this.lvVorhandeneRichtlinien.UseCompatibleStateImageBehavior = false;
            this.lvVorhandeneRichtlinien.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvVorhandeneRichtlinien_MouseDoubleClick);
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(815, 698);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(82, 23);
            this.cmdOK.TabIndex = 4;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // grpNeueRichtlinien
            // 
            this.grpNeueRichtlinien.Controls.Add(this.lblNeuesGebiet2);
            this.grpNeueRichtlinien.Controls.Add(this.lblNeuesGebiet);
            this.grpNeueRichtlinien.Controls.Add(this.lvNeueRichtlinien);
            this.grpNeueRichtlinien.Location = new System.Drawing.Point(12, 271);
            this.grpNeueRichtlinien.Name = "grpNeueRichtlinien";
            this.grpNeueRichtlinien.Size = new System.Drawing.Size(979, 281);
            this.grpNeueRichtlinien.TabIndex = 5;
            this.grpNeueRichtlinien.TabStop = false;
            this.grpNeueRichtlinien.Text = "Neue Richtlinien";
            // 
            // lblNeuesGebiet2
            // 
            this.lblNeuesGebiet2.AutoSize = true;
            this.lblNeuesGebiet2.Location = new System.Drawing.Point(53, 16);
            this.lblNeuesGebiet2.Name = "lblNeuesGebiet2";
            this.lblNeuesGebiet2.Size = new System.Drawing.Size(41, 13);
            this.lblNeuesGebiet2.TabIndex = 2;
            this.lblNeuesGebiet2.Text = "Gebiet:";
            // 
            // lblNeuesGebiet
            // 
            this.lblNeuesGebiet.AutoSize = true;
            this.lblNeuesGebiet.Location = new System.Drawing.Point(6, 16);
            this.lblNeuesGebiet.Name = "lblNeuesGebiet";
            this.lblNeuesGebiet.Size = new System.Drawing.Size(41, 13);
            this.lblNeuesGebiet.TabIndex = 1;
            this.lblNeuesGebiet.Text = "Gebiet:";
            // 
            // lvNeueRichtlinien
            // 
            this.lvNeueRichtlinien.FullRowSelect = true;
            this.lvNeueRichtlinien.Location = new System.Drawing.Point(6, 32);
            this.lvNeueRichtlinien.Name = "lvNeueRichtlinien";
            this.lvNeueRichtlinien.Size = new System.Drawing.Size(967, 239);
            this.lvNeueRichtlinien.TabIndex = 0;
            this.lvNeueRichtlinien.UseCompatibleStateImageBehavior = false;
            this.lvNeueRichtlinien.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvNeueRichtlinien_MouseDoubleClick);
            // 
            // chkAccept
            // 
            this.chkAccept.Location = new System.Drawing.Point(750, 588);
            this.chkAccept.Name = "chkAccept";
            this.chkAccept.Size = new System.Drawing.Size(235, 44);
            this.chkAccept.TabIndex = 6;
            this.chkAccept.Text = "Accept";
            this.chkAccept.UseVisualStyleBackColor = true;
            // 
            // lblInfo
            // 
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Location = new System.Drawing.Point(12, 566);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(718, 119);
            this.lblInfo.TabIndex = 7;
            this.lblInfo.Text = "Info";
            // 
            // lblInfo2
            // 
            this.lblInfo2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo2.Location = new System.Drawing.Point(12, 698);
            this.lblInfo2.Name = "lblInfo2";
            this.lblInfo2.Size = new System.Drawing.Size(718, 19);
            this.lblInfo2.TabIndex = 8;
            this.lblInfo2.Text = "Info";
            // 
            // RichtlinienValidateView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(1014, 757);
            this.Controls.Add(this.lblInfo2);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.chkAccept);
            this.Controls.Add(this.grpNeueRichtlinien);
            this.Controls.Add(this.grpVorhandeneRichtlinien);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "RichtlinienValidateView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RichtlinienValidateView";
            this.grpVorhandeneRichtlinien.ResumeLayout(false);
            this.grpVorhandeneRichtlinien.PerformLayout();
            this.grpNeueRichtlinien.ResumeLayout(false);
            this.grpNeueRichtlinien.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.OplButton cmdCancel;
        private System.Windows.Forms.GroupBox grpVorhandeneRichtlinien;
        private Windows.Forms.OplListView lvVorhandeneRichtlinien;
        private Windows.Forms.OplButton cmdOK;
        private System.Windows.Forms.GroupBox grpNeueRichtlinien;
        private Windows.Forms.OplListView lvNeueRichtlinien;
        private Windows.Forms.OplCheckBox chkAccept;
        private System.Windows.Forms.Label lblNeuesGebiet;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblVorhandenesGebiet;
        private System.Windows.Forms.Label lblInfo2;
        private System.Windows.Forms.Label lblVorhandenesGebiet2;
        private System.Windows.Forms.Label lblNeuesGebiet2;
    }
}