namespace Operationen
{
    partial class SelectDokumentView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectDokumentView));
            this.lvDokumente = new Windows.Forms.OplListView();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.cmdOK = new Windows.Forms.OplButton();
            this.SuspendLayout();
            // 
            // lvDokumente
            // 
            resources.ApplyResources(this.lvDokumente, "lvDokumente");
            this.lvDokumente.Name = "lvDokumente";
            this.lvDokumente.UseCompatibleStateImageBehavior = false;
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
            // SelectDokumentView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.lvDokumente);
            this.MaximizeBox = false;
            this.Name = "SelectDokumentView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.DokumenteView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.OplListView lvDokumente;
        private Windows.Forms.OplButton cmdCancel;
        private Windows.Forms.OplButton cmdOK;
    }
}