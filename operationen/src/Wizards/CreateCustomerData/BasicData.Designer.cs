namespace Operationen.Wizards.CreateCustomerData
{
    partial class BasicData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BasicData));
            this.label1 = new System.Windows.Forms.Label();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.txtStrasse = new System.Windows.Forms.TextBox();
            this.lblStrasse = new System.Windows.Forms.Label();
            this.txtKrankenhaus = new System.Windows.Forms.TextBox();
            this.lblKrankenhaus = new System.Windows.Forms.Label();
            this.lblPLZ = new System.Windows.Forms.Label();
            this.txtPLZ = new System.Windows.Forms.TextBox();
            this.grpInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.lblInfo);
            this.grpInfo.Controls.Add(this.txtStrasse);
            this.grpInfo.Controls.Add(this.lblStrasse);
            this.grpInfo.Controls.Add(this.txtKrankenhaus);
            this.grpInfo.Controls.Add(this.lblKrankenhaus);
            this.grpInfo.Controls.Add(this.lblPLZ);
            this.grpInfo.Controls.Add(this.txtPLZ);
            resources.ApplyResources(this.grpInfo, "grpInfo");
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.TabStop = false;
            // 
            // lblInfo
            // 
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.Name = "lblInfo";
            // 
            // txtStrasse
            // 
            this.txtStrasse.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.txtStrasse, "txtStrasse");
            this.txtStrasse.Name = "txtStrasse";
            // 
            // lblStrasse
            // 
            resources.ApplyResources(this.lblStrasse, "lblStrasse");
            this.lblStrasse.Name = "lblStrasse";
            // 
            // txtKrankenhaus
            // 
            this.txtKrankenhaus.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.txtKrankenhaus, "txtKrankenhaus");
            this.txtKrankenhaus.Name = "txtKrankenhaus";
            // 
            // lblKrankenhaus
            // 
            resources.ApplyResources(this.lblKrankenhaus, "lblKrankenhaus");
            this.lblKrankenhaus.Name = "lblKrankenhaus";
            // 
            // lblPLZ
            // 
            resources.ApplyResources(this.lblPLZ, "lblPLZ");
            this.lblPLZ.Name = "lblPLZ";
            // 
            // txtPLZ
            // 
            this.txtPLZ.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.txtPLZ, "txtPLZ");
            this.txtPLZ.Name = "txtPLZ";
            // 
            // BasicData
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpInfo);
            this.Controls.Add(this.label1);
            this.Name = "BasicData";
            this.grpInfo.ResumeLayout(false);
            this.grpInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.TextBox txtKrankenhaus;
        private System.Windows.Forms.Label lblKrankenhaus;
        private System.Windows.Forms.Label lblPLZ;
        private System.Windows.Forms.TextBox txtPLZ;
        private System.Windows.Forms.TextBox txtStrasse;
        private System.Windows.Forms.Label lblStrasse;
        private System.Windows.Forms.Label lblInfo;
    }
}
