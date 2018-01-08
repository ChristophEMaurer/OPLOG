using Windows.Forms;

namespace Operationen
{
    partial class PrintIstOperationen : OperationenForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintIstOperationen));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbOPFunktionen = new System.Windows.Forms.ComboBox();
            this.lblOPFunktionen = new System.Windows.Forms.Label();
            this.chkExtern = new System.Windows.Forms.CheckBox();
            this.chkIntern = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtBis = new Windows.Forms.DateBoxPicker();
            this.txtVon = new Windows.Forms.DateBoxPicker();
            this.chkZeitraum = new System.Windows.Forms.CheckBox();
            this.lblVon = new System.Windows.Forms.Label();
            this.lblBis = new System.Windows.Forms.Label();
            this.radOverview = new System.Windows.Forms.RadioButton();
            this.radAll = new System.Windows.Forms.RadioButton();
            this.cmdPrintAll = new System.Windows.Forms.Button();
            this.grpGeschlecht = new System.Windows.Forms.GroupBox();
            this.txtDatum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNachname = new System.Windows.Forms.TextBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpGeschlecht.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.cbOPFunktionen);
            this.groupBox1.Controls.Add(this.lblOPFunktionen);
            this.groupBox1.Controls.Add(this.chkExtern);
            this.groupBox1.Controls.Add(this.chkIntern);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.radOverview);
            this.groupBox1.Controls.Add(this.radAll);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // cbOPFunktionen
            // 
            resources.ApplyResources(this.cbOPFunktionen, "cbOPFunktionen");
            this.cbOPFunktionen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOPFunktionen.FormattingEnabled = true;
            this.cbOPFunktionen.Name = "cbOPFunktionen";
            // 
            // lblOPFunktionen
            // 
            resources.ApplyResources(this.lblOPFunktionen, "lblOPFunktionen");
            this.lblOPFunktionen.Name = "lblOPFunktionen";
            // 
            // chkExtern
            // 
            resources.ApplyResources(this.chkExtern, "chkExtern");
            this.chkExtern.Name = "chkExtern";
            this.chkExtern.UseVisualStyleBackColor = true;
            // 
            // chkIntern
            // 
            resources.ApplyResources(this.chkIntern, "chkIntern");
            this.chkIntern.Name = "chkIntern";
            this.chkIntern.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.txtBis);
            this.groupBox2.Controls.Add(this.txtVon);
            this.groupBox2.Controls.Add(this.chkZeitraum);
            this.groupBox2.Controls.Add(this.lblVon);
            this.groupBox2.Controls.Add(this.lblBis);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // txtBis
            // 
            resources.ApplyResources(this.txtBis, "txtBis");
            this.txtBis.Name = "txtBis";
            // 
            // txtVon
            // 
            resources.ApplyResources(this.txtVon, "txtVon");
            this.txtVon.Name = "txtVon";
            // 
            // chkZeitraum
            // 
            resources.ApplyResources(this.chkZeitraum, "chkZeitraum");
            this.chkZeitraum.Name = "chkZeitraum";
            this.chkZeitraum.UseVisualStyleBackColor = true;
            this.chkZeitraum.CheckedChanged += new System.EventHandler(this.chkZeitraum_CheckedChanged);
            // 
            // lblVon
            // 
            resources.ApplyResources(this.lblVon, "lblVon");
            this.lblVon.Name = "lblVon";
            // 
            // lblBis
            // 
            resources.ApplyResources(this.lblBis, "lblBis");
            this.lblBis.Name = "lblBis";
            // 
            // radOverview
            // 
            resources.ApplyResources(this.radOverview, "radOverview");
            this.radOverview.Name = "radOverview";
            this.radOverview.UseVisualStyleBackColor = true;
            this.radOverview.CheckedChanged += new System.EventHandler(this.radOverview_CheckedChanged);
            // 
            // radAll
            // 
            resources.ApplyResources(this.radAll, "radAll");
            this.radAll.Name = "radAll";
            this.radAll.UseVisualStyleBackColor = true;
            this.radAll.CheckedChanged += new System.EventHandler(this.radAll_CheckedChanged);
            // 
            // cmdPrintAll
            // 
            resources.ApplyResources(this.cmdPrintAll, "cmdPrintAll");
            this.cmdPrintAll.Name = "cmdPrintAll";
            this.cmdPrintAll.UseVisualStyleBackColor = true;
            this.cmdPrintAll.Click += new System.EventHandler(this.cmdPrintAll_Click);
            // 
            // grpGeschlecht
            // 
            resources.ApplyResources(this.grpGeschlecht, "grpGeschlecht");
            this.grpGeschlecht.Controls.Add(this.txtDatum);
            this.grpGeschlecht.Controls.Add(this.label3);
            this.grpGeschlecht.Controls.Add(this.txtNachname);
            this.grpGeschlecht.Name = "grpGeschlecht";
            this.grpGeschlecht.TabStop = false;
            // 
            // txtDatum
            // 
            resources.ApplyResources(this.txtDatum, "txtDatum");
            this.txtDatum.Name = "txtDatum";
            this.txtDatum.ReadOnly = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtNachname
            // 
            resources.ApplyResources(this.txtNachname, "txtNachname");
            this.txtNachname.Name = "txtNachname";
            this.txtNachname.ReadOnly = true;
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 200;
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 200;
            this.toolTip1.ReshowDelay = 40;
            // 
            // PrintIstOperationen
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.grpGeschlecht);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdPrintAll);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PrintIstOperationen";
            this.Load += new System.EventHandler(this.PrintIstOperationen_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpGeschlecht.ResumeLayout(false);
            this.grpGeschlecht.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNachname;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.GroupBox grpGeschlecht;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox txtDatum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdPrintAll;
        private System.Windows.Forms.Label lblVon;
        private System.Windows.Forms.Label lblBis;
        private System.Windows.Forms.RadioButton radAll;
        private System.Windows.Forms.RadioButton radOverview;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkZeitraum;
        private DateBoxPicker txtBis;
        private DateBoxPicker txtVon;
        private System.Windows.Forms.CheckBox chkExtern;
        private System.Windows.Forms.CheckBox chkIntern;
        private System.Windows.Forms.Label lblOPFunktionen;
        private System.Windows.Forms.ComboBox cbOPFunktionen;
    }
}