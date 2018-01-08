namespace Operationen
{
    partial class PluginInfoView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginInfoView));
            this.label1 = new System.Windows.Forms.Label();
            this.txtInfo = new Windows.Forms.OplTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAsmDescription = new Windows.Forms.OplTextBox();
            this.cmdOK = new Windows.Forms.OplButton();
            this.txtAsmFilename = new Windows.Forms.OplTextBox();
            this.grpPlugin = new System.Windows.Forms.GroupBox();
            this.txtPluginId = new Windows.Forms.OplTextBox();
            this.lblPluginId = new System.Windows.Forms.Label();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.grpPlugin.SuspendLayout();
            this.grpInfo.SuspendLayout();
            this.SuspendLayout();
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
            this.txtInfo.ProtectContents = false;
            this.txtInfo.ReadOnly = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtAsmDescription
            // 
            resources.ApplyResources(this.txtAsmDescription, "txtAsmDescription");
            this.txtAsmDescription.Name = "txtAsmDescription";
            this.txtAsmDescription.ProtectContents = false;
            this.txtAsmDescription.ReadOnly = true;
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.SecurityManager = null;
            this.cmdOK.UserRight = null;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // txtAsmFilename
            // 
            resources.ApplyResources(this.txtAsmFilename, "txtAsmFilename");
            this.txtAsmFilename.Name = "txtAsmFilename";
            this.txtAsmFilename.ProtectContents = false;
            this.txtAsmFilename.ReadOnly = true;
            // 
            // grpPlugin
            // 
            resources.ApplyResources(this.grpPlugin, "grpPlugin");
            this.grpPlugin.Controls.Add(this.txtPluginId);
            this.grpPlugin.Controls.Add(this.lblPluginId);
            this.grpPlugin.Controls.Add(this.label1);
            this.grpPlugin.Controls.Add(this.txtAsmFilename);
            this.grpPlugin.Controls.Add(this.txtAsmDescription);
            this.grpPlugin.Controls.Add(this.label3);
            this.grpPlugin.Name = "grpPlugin";
            this.grpPlugin.TabStop = false;
            // 
            // txtPluginId
            // 
            resources.ApplyResources(this.txtPluginId, "txtPluginId");
            this.txtPluginId.Name = "txtPluginId";
            this.txtPluginId.ProtectContents = false;
            this.txtPluginId.ReadOnly = true;
            // 
            // lblPluginId
            // 
            resources.ApplyResources(this.lblPluginId, "lblPluginId");
            this.lblPluginId.Name = "lblPluginId";
            // 
            // grpInfo
            // 
            resources.ApplyResources(this.grpInfo, "grpInfo");
            this.grpInfo.Controls.Add(this.txtInfo);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.TabStop = false;
            // 
            // PluginInfoView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdOK;
            this.Controls.Add(this.grpInfo);
            this.Controls.Add(this.grpPlugin);
            this.Controls.Add(this.cmdOK);
            this.Name = "PluginInfoView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.PluginInfoView_Load);
            this.grpPlugin.ResumeLayout(false);
            this.grpPlugin.PerformLayout();
            this.grpInfo.ResumeLayout(false);
            this.grpInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Windows.Forms.OplTextBox txtInfo;
        private System.Windows.Forms.Label label3;
        private Windows.Forms.OplTextBox txtAsmDescription;
        private Windows.Forms.OplButton cmdOK;
        private Windows.Forms.OplTextBox txtAsmFilename;
        private System.Windows.Forms.GroupBox grpPlugin;
        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.Label lblPluginId;
        private Windows.Forms.OplTextBox txtPluginId;
    }
}