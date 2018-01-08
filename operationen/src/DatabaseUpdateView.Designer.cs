namespace Operationen
{
    partial class DatabaseUpdateView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseUpdateView));
            this.cmdCancel = new Windows.Forms.OplButton();
            this.cmdOK = new Windows.Forms.OplButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInfo = new Windows.Forms.OplTextBox();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtInfo
            // 
            resources.ApplyResources(this.txtInfo, "txtInfo");
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ReadOnly = true;
            // 
            // DatabaseUpdateView
            // 
            this.AcceptButton = this.cmdCancel;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.MaximizeBox = false;
            this.Name = "DatabaseUpdateView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.DatabaseUpdateView_Load);
            this.Shown += new System.EventHandler(this.DatabaseUpdateView_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Windows.Forms.OplButton cmdCancel;
        private Windows.Forms.OplButton cmdOK;
        private System.Windows.Forms.Label label1;
        private Windows.Forms.OplTextBox txtInfo;
    }
}