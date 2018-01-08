using Windows.Forms;

namespace Operationen
{
    partial class ChirurgView : OperationenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChirurgView));
            this.grpGeschlecht = new System.Windows.Forms.GroupBox();
            this.lblImportIDInfo = new System.Windows.Forms.Label();
            this.grpAusbildung = new System.Windows.Forms.GroupBox();
            this.txtGebietBis = new Windows.Forms.DateBoxPicker();
            this.txtGebietVon = new Windows.Forms.DateBoxPicker();
            this.lblGebietBis = new System.Windows.Forms.Label();
            this.lblGebietVon = new System.Windows.Forms.Label();
            this.cbGebiete = new System.Windows.Forms.ComboBox();
            this.txtImportID = new Windows.Forms.OplTextBox();
            this.lblImportID = new System.Windows.Forms.Label();
            this.lblTitel = new System.Windows.Forms.Label();
            this.cbTitel = new System.Windows.Forms.ComboBox();
            this.cbAnrede = new System.Windows.Forms.ComboBox();
            this.lblAnrede = new System.Windows.Forms.Label();
            this.txtDatum = new Windows.Forms.OplTextBox();
            this.lblAnfangsdatum = new System.Windows.Forms.Label();
            this.txtNachname = new Windows.Forms.OplTextBox();
            this.lblNachname = new System.Windows.Forms.Label();
            this.lblVorname = new System.Windows.Forms.Label();
            this.txtVorname = new Windows.Forms.OplTextBox();
            this.cmdOK = new Windows.Forms.OplButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdCancel = new Windows.Forms.OplButton();
            this.lblAnmeldename = new System.Windows.Forms.Label();
            this.txtUserID = new Windows.Forms.OplTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkAktiv = new Windows.Forms.OplCheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chkWeiterbilder = new Windows.Forms.OplCheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblInfo = new Windows.Forms.OplLinkLabel();
            this.grpGeschlecht.SuspendLayout();
            this.grpAusbildung.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpGeschlecht
            // 
            resources.ApplyResources(this.grpGeschlecht, "grpGeschlecht");
            this.grpGeschlecht.Controls.Add(this.lblImportIDInfo);
            this.grpGeschlecht.Controls.Add(this.grpAusbildung);
            this.grpGeschlecht.Controls.Add(this.txtImportID);
            this.grpGeschlecht.Controls.Add(this.lblImportID);
            this.grpGeschlecht.Controls.Add(this.lblTitel);
            this.grpGeschlecht.Controls.Add(this.cbTitel);
            this.grpGeschlecht.Controls.Add(this.cbAnrede);
            this.grpGeschlecht.Controls.Add(this.lblAnrede);
            this.grpGeschlecht.Controls.Add(this.txtDatum);
            this.grpGeschlecht.Controls.Add(this.lblAnfangsdatum);
            this.grpGeschlecht.Controls.Add(this.txtNachname);
            this.grpGeschlecht.Controls.Add(this.lblNachname);
            this.grpGeschlecht.Controls.Add(this.lblVorname);
            this.grpGeschlecht.Controls.Add(this.txtVorname);
            this.grpGeschlecht.Name = "grpGeschlecht";
            this.grpGeschlecht.TabStop = false;
            // 
            // lblImportIDInfo
            // 
            resources.ApplyResources(this.lblImportIDInfo, "lblImportIDInfo");
            this.lblImportIDInfo.Name = "lblImportIDInfo";
            // 
            // grpAusbildung
            // 
            resources.ApplyResources(this.grpAusbildung, "grpAusbildung");
            this.grpAusbildung.Controls.Add(this.txtGebietBis);
            this.grpAusbildung.Controls.Add(this.txtGebietVon);
            this.grpAusbildung.Controls.Add(this.lblGebietBis);
            this.grpAusbildung.Controls.Add(this.lblGebietVon);
            this.grpAusbildung.Controls.Add(this.cbGebiete);
            this.grpAusbildung.Name = "grpAusbildung";
            this.grpAusbildung.TabStop = false;
            // 
            // txtGebietBis
            // 
            resources.ApplyResources(this.txtGebietBis, "txtGebietBis");
            this.txtGebietBis.Name = "txtGebietBis";
            // 
            // txtGebietVon
            // 
            resources.ApplyResources(this.txtGebietVon, "txtGebietVon");
            this.txtGebietVon.Name = "txtGebietVon";
            // 
            // lblGebietBis
            // 
            resources.ApplyResources(this.lblGebietBis, "lblGebietBis");
            this.lblGebietBis.Name = "lblGebietBis";
            // 
            // lblGebietVon
            // 
            resources.ApplyResources(this.lblGebietVon, "lblGebietVon");
            this.lblGebietVon.Name = "lblGebietVon";
            // 
            // cbGebiete
            // 
            resources.ApplyResources(this.cbGebiete, "cbGebiete");
            this.cbGebiete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGebiete.Name = "cbGebiete";
            this.cbGebiete.SelectedIndexChanged += new System.EventHandler(this.cbGebiete_SelectedIndexChanged);
            // 
            // txtImportID
            // 
            resources.ApplyResources(this.txtImportID, "txtImportID");
            this.txtImportID.BackColor = System.Drawing.SystemColors.Window;
            this.txtImportID.Name = "txtImportID";
            // 
            // lblImportID
            // 
            resources.ApplyResources(this.lblImportID, "lblImportID");
            this.lblImportID.Name = "lblImportID";
            // 
            // lblTitel
            // 
            resources.ApplyResources(this.lblTitel, "lblTitel");
            this.lblTitel.Name = "lblTitel";
            // 
            // cbTitel
            // 
            resources.ApplyResources(this.cbTitel, "cbTitel");
            this.cbTitel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTitel.FormattingEnabled = true;
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
            // txtDatum
            // 
            this.txtDatum.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.txtDatum, "txtDatum");
            this.txtDatum.Name = "txtDatum";
            // 
            // lblAnfangsdatum
            // 
            resources.ApplyResources(this.lblAnfangsdatum, "lblAnfangsdatum");
            this.lblAnfangsdatum.Name = "lblAnfangsdatum";
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
            resources.ApplyResources(this.txtVorname, "txtVorname");
            this.txtVorname.BackColor = System.Drawing.SystemColors.Window;
            this.txtVorname.Name = "txtVorname";
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.OKClicked);
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
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.CancelClicked);
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
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.chkAktiv);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.chkWeiterbilder);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblAnmeldename);
            this.groupBox1.Controls.Add(this.txtUserID);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // chkAktiv
            // 
            resources.ApplyResources(this.chkAktiv, "chkAktiv");
            this.chkAktiv.Name = "chkAktiv";
            this.chkAktiv.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Name = "label1";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // chkWeiterbilder
            // 
            resources.ApplyResources(this.chkWeiterbilder, "chkWeiterbilder");
            this.chkWeiterbilder.Name = "chkWeiterbilder";
            this.chkWeiterbilder.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.TabStop = true;
            this.lblInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblInfo_LinkClicked);
            // 
            // ChirurgView
            // 
            this.AcceptButton = this.cmdOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.grpGeschlecht);
            this.Controls.Add(this.cmdOK);
            this.Name = "ChirurgView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.ChirurgView_Load);
            this.Shown += new System.EventHandler(this.ChirurgView_Shown);
            this.grpGeschlecht.ResumeLayout(false);
            this.grpGeschlecht.PerformLayout();
            this.grpAusbildung.ResumeLayout(false);
            this.grpAusbildung.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.Label lblNachname;
        private Windows.Forms.OplTextBox txtVorname;
        private System.Windows.Forms.Label lblVorname;
        private Windows.Forms.OplTextBox txtNachname;
        private Windows.Forms.OplButton cmdOK;
        private System.Windows.Forms.GroupBox grpGeschlecht;
        private System.Windows.Forms.ToolTip toolTip1;
        private Windows.Forms.OplTextBox txtDatum;
        private System.Windows.Forms.Label lblAnfangsdatum;
        private System.Windows.Forms.Label lblAnrede;
        private Windows.Forms.OplButton cmdCancel;
        private System.Windows.Forms.Label lblAnmeldename;
        private Windows.Forms.OplTextBox txtUserID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private OplCheckBox chkWeiterbilder;
        private System.Windows.Forms.ComboBox cbAnrede;
        private System.Windows.Forms.Label lblTitel;
        private System.Windows.Forms.ComboBox cbTitel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpAusbildung;
        private System.Windows.Forms.ComboBox cbGebiete;
        private System.Windows.Forms.Label lblGebietBis;
        private System.Windows.Forms.Label lblGebietVon;
        private DateBoxPicker txtGebietBis;
        private DateBoxPicker txtGebietVon;
        private System.Windows.Forms.Label label2;
        private Windows.Forms.OplCheckBox chkAktiv;
        private OplLinkLabel lblInfo;
        private System.Windows.Forms.Label lblImportID;
        private Windows.Forms.OplTextBox txtImportID;
        private System.Windows.Forms.Label lblImportIDInfo;
    }
}