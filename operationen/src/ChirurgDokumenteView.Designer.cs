namespace Operationen
{
    partial class ChirurgDokumenteView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChirurgDokumenteView));
            this.lvChirurgen = new Windows.Forms.OplListView();
            this.lvDokumente = new Windows.Forms.OplListView();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.cmdEdit = new Windows.Forms.OplButton();
            this.cmdInsert = new Windows.Forms.OplButton();
            this.cmdDelete = new Windows.Forms.OplButton();
            this.cmdUpdate = new Windows.Forms.OplButton();
            this.grpUser = new System.Windows.Forms.GroupBox();
            this.grpBooks = new System.Windows.Forms.GroupBox();
            this.cmdView = new Windows.Forms.OplButton();
            this.lblInfo = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grpUser.SuspendLayout();
            this.grpBooks.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvChirurgen
            // 
            resources.ApplyResources(this.lvChirurgen, "lvChirurgen");
            this.lvChirurgen.DoubleClickActivation = false;
            this.lvChirurgen.Name = "lvChirurgen";
            this.lvChirurgen.UseCompatibleStateImageBehavior = false;
            this.lvChirurgen.SelectedIndexChanged += new System.EventHandler(this.lvChirurgen_SelectedIndexChanged);
            // 
            // lvDokumente
            // 
            resources.ApplyResources(this.lvDokumente, "lvDokumente");
            this.lvDokumente.DoubleClickActivation = false;
            this.lvDokumente.Name = "lvDokumente";
            this.lvDokumente.UseCompatibleStateImageBehavior = false;
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdEdit
            // 
            resources.ApplyResources(this.cmdEdit, "cmdEdit");
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.UseVisualStyleBackColor = true;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // cmdInsert
            // 
            resources.ApplyResources(this.cmdInsert, "cmdInsert");
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.UseVisualStyleBackColor = true;
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // cmdDelete
            // 
            resources.ApplyResources(this.cmdDelete, "cmdDelete");
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdUpdate
            // 
            resources.ApplyResources(this.cmdUpdate, "cmdUpdate");
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.UseVisualStyleBackColor = true;
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // grpUser
            // 
            this.grpUser.Controls.Add(this.lvChirurgen);
            this.grpUser.Controls.Add(this.cmdInsert);
            resources.ApplyResources(this.grpUser, "grpUser");
            this.grpUser.Name = "grpUser";
            this.grpUser.TabStop = false;
            // 
            // grpBooks
            // 
            this.grpBooks.Controls.Add(this.cmdView);
            this.grpBooks.Controls.Add(this.lvDokumente);
            this.grpBooks.Controls.Add(this.cmdEdit);
            this.grpBooks.Controls.Add(this.cmdUpdate);
            this.grpBooks.Controls.Add(this.cmdDelete);
            resources.ApplyResources(this.grpBooks, "grpBooks");
            this.grpBooks.Name = "grpBooks";
            this.grpBooks.TabStop = false;
            // 
            // cmdView
            // 
            resources.ApplyResources(this.cmdView, "cmdView");
            this.cmdView.Name = "cmdView";
            this.cmdView.UseVisualStyleBackColor = true;
            this.cmdView.Click += new System.EventHandler(this.cmdView_Click);
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grpUser);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpBooks);
            // 
            // ChirurgDokumenteView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.cmdCancel);
            this.MaximizeBox = false;
            this.Name = "ChirurgDokumenteView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.ChirurgDokumenteView_Load);
            this.grpUser.ResumeLayout(false);
            this.grpBooks.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.OplListView lvChirurgen;
        private Windows.Forms.OplListView lvDokumente;
        private Windows.Forms.OplButton cmdCancel;
        private Windows.Forms.OplButton cmdEdit;
        private Windows.Forms.OplButton cmdInsert;
        private Windows.Forms.OplButton cmdDelete;
        private Windows.Forms.OplButton cmdUpdate;
        private System.Windows.Forms.GroupBox grpUser;
        private System.Windows.Forms.GroupBox grpBooks;
        private System.Windows.Forms.Label lblInfo;
        private Windows.Forms.OplButton cmdView;
        private System.Windows.Forms.SplitContainer splitContainer;
    }
}