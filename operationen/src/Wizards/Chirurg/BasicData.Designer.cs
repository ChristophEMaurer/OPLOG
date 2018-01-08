namespace Operationen.Wizards.Chirurg
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
            this.grpGeschlecht = new System.Windows.Forms.GroupBox();
            this.lblTitel = new System.Windows.Forms.Label();
            this.cbTitel = new System.Windows.Forms.ComboBox();
            this.cbAnrede = new System.Windows.Forms.ComboBox();
            this.lblAnrede = new System.Windows.Forms.Label();
            this.txtNachname = new System.Windows.Forms.TextBox();
            this.lblNachname = new System.Windows.Forms.Label();
            this.lblVorname = new System.Windows.Forms.Label();
            this.txtVorname = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblAnmeldename = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.grpGeschlecht.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // grpGeschlecht
            // 
            this.grpGeschlecht.Controls.Add(this.lblTitel);
            this.grpGeschlecht.Controls.Add(this.cbTitel);
            this.grpGeschlecht.Controls.Add(this.cbAnrede);
            this.grpGeschlecht.Controls.Add(this.lblAnrede);
            this.grpGeschlecht.Controls.Add(this.txtNachname);
            this.grpGeschlecht.Controls.Add(this.lblNachname);
            this.grpGeschlecht.Controls.Add(this.lblVorname);
            this.grpGeschlecht.Controls.Add(this.txtVorname);
            resources.ApplyResources(this.grpGeschlecht, "grpGeschlecht");
            this.grpGeschlecht.Name = "grpGeschlecht";
            this.grpGeschlecht.TabStop = false;
            // 
            // lblTitel
            // 
            resources.ApplyResources(this.lblTitel, "lblTitel");
            this.lblTitel.Name = "lblTitel";
            // 
            // cbTitel
            // 
            this.cbTitel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTitel.FormattingEnabled = true;
            resources.ApplyResources(this.cbTitel, "cbTitel");
            this.cbTitel.Name = "cbTitel";
            // 
            // cbAnrede
            // 
            this.cbAnrede.FormattingEnabled = true;
            resources.ApplyResources(this.cbAnrede, "cbAnrede");
            this.cbAnrede.Name = "cbAnrede";
            // 
            // lblAnrede
            // 
            resources.ApplyResources(this.lblAnrede, "lblAnrede");
            this.lblAnrede.Name = "lblAnrede";
            // 
            // txtNachname
            // 
            this.txtNachname.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.txtNachname, "txtNachname");
            this.txtNachname.Name = "txtNachname";
            this.txtNachname.TextChanged += new System.EventHandler(this.txtNachname_TextChanged);
            // 
            // lblNachname
            // 
            resources.ApplyResources(this.lblNachname, "lblNachname");
            this.lblNachname.Name = "lblNachname";
            // 
            // lblVorname
            // 
            resources.ApplyResources(this.lblVorname, "lblVorname");
            this.lblVorname.Name = "lblVorname";
            // 
            // txtVorname
            // 
            this.txtVorname.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.txtVorname, "txtVorname");
            this.txtVorname.Name = "txtVorname";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblInfo);
            this.groupBox1.Controls.Add(this.lblAnmeldename);
            this.groupBox1.Controls.Add(this.txtUserID);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // lblInfo
            // 
            this.lblInfo.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.Name = "lblInfo";
            // 
            // lblAnmeldename
            // 
            resources.ApplyResources(this.lblAnmeldename, "lblAnmeldename");
            this.lblAnmeldename.Name = "lblAnmeldename";
            // 
            // txtUserID
            // 
            this.txtUserID.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.txtUserID, "txtUserID");
            this.txtUserID.Name = "txtUserID";
            // 
            // BasicData
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpGeschlecht);
            this.Controls.Add(this.label1);
            this.Name = "BasicData";
            this.Load += new System.EventHandler(this.BasicData_Load);
            this.grpGeschlecht.ResumeLayout(false);
            this.grpGeschlecht.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpGeschlecht;
        private System.Windows.Forms.Label lblTitel;
        private System.Windows.Forms.ComboBox cbTitel;
        private System.Windows.Forms.ComboBox cbAnrede;
        private System.Windows.Forms.Label lblAnrede;
        private System.Windows.Forms.TextBox txtNachname;
        private System.Windows.Forms.Label lblNachname;
        private System.Windows.Forms.Label lblVorname;
        private System.Windows.Forms.TextBox txtVorname;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblAnmeldename;
        private System.Windows.Forms.TextBox txtUserID;
    }
}
