using Windows.Forms;

namespace Operationen.Wizards.ImportChirurg
{
    partial class NewOperationenView : OperationenForm
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdCancel = new Windows.Forms.OplButton();
            this.cmdOK = new Windows.Forms.OplButton();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.txtDateLatest = new Windows.Forms.OplTextBox();
            this.cmdDisplay = new Windows.Forms.OplButton();
            this.label1 = new System.Windows.Forms.Label();
            this.radDateRange = new Windows.Forms.OplRadioButton();
            this.radAll = new Windows.Forms.OplRadioButton();
            this.txtDatumBis = new Windows.Forms.DateBoxPicker();
            this.txtDatumVon = new Windows.Forms.DateBoxPicker();
            this.lblDatumBis = new System.Windows.Forms.Label();
            this.lblDatumVon = new System.Windows.Forms.Label();
            this.grpOperationen = new System.Windows.Forms.GroupBox();
            this.lvOperationen = new Windows.Forms.OplListView();
            this.lblInfo = new System.Windows.Forms.Label();
            this.chkForce = new Windows.Forms.OplCheckBox();
            this.grpFilter.SuspendLayout();
            this.grpOperationen.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 200;
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 200;
            this.toolTip1.ReshowDelay = 40;
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(768, 651);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(105, 23);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "Abbrechen";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdOK.Location = new System.Drawing.Point(550, 651);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(212, 23);
            this.cmdOK.TabIndex = 3;
            this.cmdOK.Text = "Ausgewählte Operationen importieren";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // grpFilter
            // 
            this.grpFilter.Controls.Add(this.txtDateLatest);
            this.grpFilter.Controls.Add(this.cmdDisplay);
            this.grpFilter.Controls.Add(this.label1);
            this.grpFilter.Controls.Add(this.radDateRange);
            this.grpFilter.Controls.Add(this.radAll);
            this.grpFilter.Controls.Add(this.txtDatumBis);
            this.grpFilter.Controls.Add(this.txtDatumVon);
            this.grpFilter.Controls.Add(this.lblDatumBis);
            this.grpFilter.Controls.Add(this.lblDatumVon);
            this.grpFilter.Location = new System.Drawing.Point(12, 12);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Size = new System.Drawing.Size(855, 90);
            this.grpFilter.TabIndex = 0;
            this.grpFilter.TabStop = false;
            this.grpFilter.Text = "Importieren";
            // 
            // txtDateLatest
            // 
            this.txtDateLatest.Location = new System.Drawing.Point(756, 16);
            this.txtDateLatest.MaxLength = 20;
            this.txtDateLatest.Name = "txtDateLatest";
            this.txtDateLatest.ReadOnly = true;
            this.txtDateLatest.Size = new System.Drawing.Size(79, 20);
            this.txtDateLatest.TabIndex = 5;
            // 
            // cmdDisplay
            // 
            this.cmdDisplay.Location = new System.Drawing.Point(468, 48);
            this.cmdDisplay.Name = "cmdDisplay";
            this.cmdDisplay.Size = new System.Drawing.Size(194, 23);
            this.cmdDisplay.TabIndex = 11;
            this.cmdDisplay.Text = "Alle im Zeitraum auswählen";
            this.cmdDisplay.UseVisualStyleBackColor = true;
            this.cmdDisplay.Click += new System.EventHandler(this.cmdDisplay_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(529, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Datum der neuesten vorhandenen Prozedur:";
            // 
            // radDateRange
            // 
            this.radDateRange.AutoSize = true;
            this.radDateRange.Location = new System.Drawing.Point(9, 51);
            this.radDateRange.Name = "radDateRange";
            this.radDateRange.Size = new System.Drawing.Size(136, 17);
            this.radDateRange.TabIndex = 8;
            this.radDateRange.TabStop = true;
            this.radDateRange.Text = "Alle in diesem Zeitraum:";
            this.radDateRange.UseVisualStyleBackColor = true;
            this.radDateRange.CheckedChanged += new System.EventHandler(this.radDateRange_CheckedChanged);
            // 
            // radAll
            // 
            this.radAll.AutoSize = true;
            this.radAll.Location = new System.Drawing.Point(9, 22);
            this.radAll.Name = "radAll";
            this.radAll.Size = new System.Drawing.Size(96, 17);
            this.radAll.TabIndex = 6;
            this.radAll.TabStop = true;
            this.radAll.Text = "Alle auswählen";
            this.radAll.UseVisualStyleBackColor = true;
            this.radAll.CheckedChanged += new System.EventHandler(this.radAll_CheckedChanged);
            // 
            // txtDatumBis
            // 
            this.txtDatumBis.Location = new System.Drawing.Point(341, 49);
            this.txtDatumBis.Name = "txtDatumBis";
            this.txtDatumBis.Size = new System.Drawing.Size(112, 22);
            this.txtDatumBis.TabIndex = 3;
            // 
            // txtDatumVon
            // 
            this.txtDatumVon.Location = new System.Drawing.Point(185, 51);
            this.txtDatumVon.Name = "txtDatumVon";
            this.txtDatumVon.Size = new System.Drawing.Size(112, 22);
            this.txtDatumVon.TabIndex = 1;
            // 
            // lblDatumBis
            // 
            this.lblDatumBis.AutoSize = true;
            this.lblDatumBis.Location = new System.Drawing.Point(312, 53);
            this.lblDatumBis.Name = "lblDatumBis";
            this.lblDatumBis.Size = new System.Drawing.Size(23, 13);
            this.lblDatumBis.TabIndex = 2;
            this.lblDatumBis.Text = "bis:";
            // 
            // lblDatumVon
            // 
            this.lblDatumVon.AutoSize = true;
            this.lblDatumVon.Location = new System.Drawing.Point(151, 54);
            this.lblDatumVon.Name = "lblDatumVon";
            this.lblDatumVon.Size = new System.Drawing.Size(28, 13);
            this.lblDatumVon.TabIndex = 0;
            this.lblDatumVon.Text = "von:";
            // 
            // grpOperationen
            // 
            this.grpOperationen.Controls.Add(this.lvOperationen);
            this.grpOperationen.Location = new System.Drawing.Point(12, 108);
            this.grpOperationen.Name = "grpOperationen";
            this.grpOperationen.Size = new System.Drawing.Size(855, 424);
            this.grpOperationen.TabIndex = 1;
            this.grpOperationen.TabStop = false;
            this.grpOperationen.Text = "Neue Operationen/Prozeduren";
            // 
            // lvOperationen
            // 
            this.lvOperationen.FullRowSelect = true;
            this.lvOperationen.Location = new System.Drawing.Point(6, 19);
            this.lvOperationen.Name = "lvOperationen";
            this.lvOperationen.Size = new System.Drawing.Size(843, 399);
            this.lvOperationen.TabIndex = 3;
            this.lvOperationen.UseCompatibleStateImageBehavior = false;
            // 
            // lblInfo
            // 
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblInfo.Location = new System.Drawing.Point(47, 535);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(491, 142);
            this.lblInfo.TabIndex = 4;
            this.lblInfo.Text = "Info";
            // 
            // chkForce
            // 
            this.chkForce.AutoSize = true;
            this.chkForce.Location = new System.Drawing.Point(550, 608);
            this.chkForce.Name = "chkForce";
            this.chkForce.Size = new System.Drawing.Size(259, 17);
            this.chkForce.TabIndex = 12;
            this.chkForce.Text = "Operationen mehrfach einfügen (nicht empfohlen)";
            this.chkForce.UseVisualStyleBackColor = true;
            // 
            // NewOperationenView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(879, 686);
            this.Controls.Add(this.chkForce);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.grpFilter);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.grpOperationen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.Name = "NewOperationenView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NewOperationenView";
            this.Load += new System.EventHandler(this.NewOperationenView_Load);
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            this.grpOperationen.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpOperationen;
        private Windows.Forms.OplButton cmdCancel;
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.ToolTip toolTip1;
        private Windows.Forms.OplListView lvOperationen;
        private System.Windows.Forms.Label lblDatumBis;
        private System.Windows.Forms.Label lblDatumVon;
        private DateBoxPicker txtDatumBis;
        private DateBoxPicker txtDatumVon;
        private Windows.Forms.OplRadioButton radDateRange;
        private Windows.Forms.OplRadioButton radAll;
        private Windows.Forms.OplButton cmdDisplay;
        private Windows.Forms.OplTextBox txtDateLatest;
        private System.Windows.Forms.Label label1;
        private Windows.Forms.OplButton cmdOK;
        private System.Windows.Forms.Label lblInfo;
        private Windows.Forms.OplCheckBox chkForce;
    }
}