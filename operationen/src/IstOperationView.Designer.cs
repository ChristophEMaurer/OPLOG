using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Windows.Forms;

namespace Operationen
{
    partial class IstOperationView : OperationenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IstOperationView));
            this.cmdOK = new System.Windows.Forms.Button();
            this.grpOperationen = new System.Windows.Forms.GroupBox();
            this.grpKlinischeErgebnisse = new System.Windows.Forms.GroupBox();
            this.cbKlinischeErgebnisseTypen = new System.Windows.Forms.ComboBox();
            this.txtKlinischeErgebnisse = new System.Windows.Forms.TextBox();
            this.radQuelleExtern = new System.Windows.Forms.RadioButton();
            this.radQuelleIntern = new System.Windows.Forms.RadioButton();
            this.txtFallzahl = new System.Windows.Forms.TextBox();
            this.lblFallzahl = new System.Windows.Forms.Label();
            this.txtZeitBis = new System.Windows.Forms.TextBox();
            this.lblZeitBis = new System.Windows.Forms.Label();
            this.txtAnzahl = new System.Windows.Forms.TextBox();
            this.lblAnzahl = new System.Windows.Forms.Label();
            this.txtFilterText = new System.Windows.Forms.TextBox();
            this.lblFilterText = new System.Windows.Forms.Label();
            this.txtDatum = new Windows.Forms.DateBoxPicker();
            this.cmdPopulate = new System.Windows.Forms.Button();
            this.txtFilterKode = new System.Windows.Forms.TextBox();
            this.lblFilterKode = new System.Windows.Forms.Label();
            this.lblOPFunktionen = new System.Windows.Forms.Label();
            this.cbOPFunktionen = new System.Windows.Forms.ComboBox();
            this.txtZeit = new System.Windows.Forms.TextBox();
            this.lblZeit = new System.Windows.Forms.Label();
            this.lvOperationen = new System.Windows.Forms.ListView();
            this.lblDatum = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.grpOperationen.SuspendLayout();
            this.grpKlinischeErgebnisse.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.AccessibleDescription = null;
            this.cmdOK.AccessibleName = null;
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.BackgroundImage = null;
            this.cmdOK.Font = null;
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // grpOperationen
            // 
            this.grpOperationen.AccessibleDescription = null;
            this.grpOperationen.AccessibleName = null;
            resources.ApplyResources(this.grpOperationen, "grpOperationen");
            this.grpOperationen.BackgroundImage = null;
            this.grpOperationen.Controls.Add(this.grpKlinischeErgebnisse);
            this.grpOperationen.Controls.Add(this.radQuelleExtern);
            this.grpOperationen.Controls.Add(this.radQuelleIntern);
            this.grpOperationen.Controls.Add(this.txtFallzahl);
            this.grpOperationen.Controls.Add(this.lblFallzahl);
            this.grpOperationen.Controls.Add(this.txtZeitBis);
            this.grpOperationen.Controls.Add(this.lblZeitBis);
            this.grpOperationen.Controls.Add(this.txtAnzahl);
            this.grpOperationen.Controls.Add(this.lblAnzahl);
            this.grpOperationen.Controls.Add(this.txtFilterText);
            this.grpOperationen.Controls.Add(this.lblFilterText);
            this.grpOperationen.Controls.Add(this.txtDatum);
            this.grpOperationen.Controls.Add(this.cmdPopulate);
            this.grpOperationen.Controls.Add(this.txtFilterKode);
            this.grpOperationen.Controls.Add(this.lblFilterKode);
            this.grpOperationen.Controls.Add(this.lblOPFunktionen);
            this.grpOperationen.Controls.Add(this.cbOPFunktionen);
            this.grpOperationen.Controls.Add(this.txtZeit);
            this.grpOperationen.Controls.Add(this.lblZeit);
            this.grpOperationen.Controls.Add(this.lvOperationen);
            this.grpOperationen.Controls.Add(this.lblDatum);
            this.grpOperationen.Font = null;
            this.grpOperationen.Name = "grpOperationen";
            this.grpOperationen.TabStop = false;
            // 
            // grpKlinischeErgebnisse
            // 
            this.grpKlinischeErgebnisse.AccessibleDescription = null;
            this.grpKlinischeErgebnisse.AccessibleName = null;
            resources.ApplyResources(this.grpKlinischeErgebnisse, "grpKlinischeErgebnisse");
            this.grpKlinischeErgebnisse.BackgroundImage = null;
            this.grpKlinischeErgebnisse.Controls.Add(this.cbKlinischeErgebnisseTypen);
            this.grpKlinischeErgebnisse.Controls.Add(this.txtKlinischeErgebnisse);
            this.grpKlinischeErgebnisse.Font = null;
            this.grpKlinischeErgebnisse.Name = "grpKlinischeErgebnisse";
            this.grpKlinischeErgebnisse.TabStop = false;
            // 
            // cbKlinischeErgebnisseTypen
            // 
            this.cbKlinischeErgebnisseTypen.AccessibleDescription = null;
            this.cbKlinischeErgebnisseTypen.AccessibleName = null;
            resources.ApplyResources(this.cbKlinischeErgebnisseTypen, "cbKlinischeErgebnisseTypen");
            this.cbKlinischeErgebnisseTypen.BackgroundImage = null;
            this.cbKlinischeErgebnisseTypen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKlinischeErgebnisseTypen.Font = null;
            this.cbKlinischeErgebnisseTypen.FormattingEnabled = true;
            this.cbKlinischeErgebnisseTypen.Name = "cbKlinischeErgebnisseTypen";
            // 
            // txtKlinischeErgebnisse
            // 
            this.txtKlinischeErgebnisse.AccessibleDescription = null;
            this.txtKlinischeErgebnisse.AccessibleName = null;
            resources.ApplyResources(this.txtKlinischeErgebnisse, "txtKlinischeErgebnisse");
            this.txtKlinischeErgebnisse.BackgroundImage = null;
            this.txtKlinischeErgebnisse.Font = null;
            this.txtKlinischeErgebnisse.Name = "txtKlinischeErgebnisse";
            // 
            // radQuelleExtern
            // 
            this.radQuelleExtern.AccessibleDescription = null;
            this.radQuelleExtern.AccessibleName = null;
            resources.ApplyResources(this.radQuelleExtern, "radQuelleExtern");
            this.radQuelleExtern.BackgroundImage = null;
            this.radQuelleExtern.Font = null;
            this.radQuelleExtern.Name = "radQuelleExtern";
            this.radQuelleExtern.TabStop = true;
            this.radQuelleExtern.UseVisualStyleBackColor = true;
            // 
            // radQuelleIntern
            // 
            this.radQuelleIntern.AccessibleDescription = null;
            this.radQuelleIntern.AccessibleName = null;
            resources.ApplyResources(this.radQuelleIntern, "radQuelleIntern");
            this.radQuelleIntern.BackgroundImage = null;
            this.radQuelleIntern.Font = null;
            this.radQuelleIntern.Name = "radQuelleIntern";
            this.radQuelleIntern.TabStop = true;
            this.radQuelleIntern.UseVisualStyleBackColor = true;
            // 
            // txtFallzahl
            // 
            this.txtFallzahl.AccessibleDescription = null;
            this.txtFallzahl.AccessibleName = null;
            resources.ApplyResources(this.txtFallzahl, "txtFallzahl");
            this.txtFallzahl.BackgroundImage = null;
            this.txtFallzahl.Font = null;
            this.txtFallzahl.Name = "txtFallzahl";
            // 
            // lblFallzahl
            // 
            this.lblFallzahl.AccessibleDescription = null;
            this.lblFallzahl.AccessibleName = null;
            resources.ApplyResources(this.lblFallzahl, "lblFallzahl");
            this.lblFallzahl.Font = null;
            this.lblFallzahl.Name = "lblFallzahl";
            // 
            // txtZeitBis
            // 
            this.txtZeitBis.AccessibleDescription = null;
            this.txtZeitBis.AccessibleName = null;
            resources.ApplyResources(this.txtZeitBis, "txtZeitBis");
            this.txtZeitBis.BackgroundImage = null;
            this.txtZeitBis.Font = null;
            this.txtZeitBis.Name = "txtZeitBis";
            // 
            // lblZeitBis
            // 
            this.lblZeitBis.AccessibleDescription = null;
            this.lblZeitBis.AccessibleName = null;
            resources.ApplyResources(this.lblZeitBis, "lblZeitBis");
            this.lblZeitBis.Font = null;
            this.lblZeitBis.Name = "lblZeitBis";
            // 
            // txtAnzahl
            // 
            this.txtAnzahl.AccessibleDescription = null;
            this.txtAnzahl.AccessibleName = null;
            resources.ApplyResources(this.txtAnzahl, "txtAnzahl");
            this.txtAnzahl.BackgroundImage = null;
            this.txtAnzahl.Font = null;
            this.txtAnzahl.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtAnzahl.Name = "txtAnzahl";
            this.txtAnzahl.TextChanged += new System.EventHandler(this.txtAnzahl_TextChanged);
            // 
            // lblAnzahl
            // 
            this.lblAnzahl.AccessibleDescription = null;
            this.lblAnzahl.AccessibleName = null;
            resources.ApplyResources(this.lblAnzahl, "lblAnzahl");
            this.lblAnzahl.Font = null;
            this.lblAnzahl.Name = "lblAnzahl";
            // 
            // txtFilterText
            // 
            this.txtFilterText.AccessibleDescription = null;
            this.txtFilterText.AccessibleName = null;
            resources.ApplyResources(this.txtFilterText, "txtFilterText");
            this.txtFilterText.BackgroundImage = null;
            this.txtFilterText.Font = null;
            this.txtFilterText.Name = "txtFilterText";
            // 
            // lblFilterText
            // 
            this.lblFilterText.AccessibleDescription = null;
            this.lblFilterText.AccessibleName = null;
            resources.ApplyResources(this.lblFilterText, "lblFilterText");
            this.lblFilterText.Font = null;
            this.lblFilterText.Name = "lblFilterText";
            // 
            // txtDatum
            // 
            this.txtDatum.AccessibleDescription = null;
            this.txtDatum.AccessibleName = null;
            resources.ApplyResources(this.txtDatum, "txtDatum");
            this.txtDatum.BackgroundImage = null;
            this.txtDatum.Font = null;
            this.txtDatum.Name = "txtDatum";
            // 
            // cmdPopulate
            // 
            this.cmdPopulate.AccessibleDescription = null;
            this.cmdPopulate.AccessibleName = null;
            resources.ApplyResources(this.cmdPopulate, "cmdPopulate");
            this.cmdPopulate.BackgroundImage = null;
            this.cmdPopulate.Font = null;
            this.cmdPopulate.Name = "cmdPopulate";
            this.cmdPopulate.UseVisualStyleBackColor = true;
            this.cmdPopulate.Click += new System.EventHandler(this.cmdPopulate_Click);
            // 
            // txtFilterKode
            // 
            this.txtFilterKode.AccessibleDescription = null;
            this.txtFilterKode.AccessibleName = null;
            resources.ApplyResources(this.txtFilterKode, "txtFilterKode");
            this.txtFilterKode.BackgroundImage = null;
            this.txtFilterKode.Font = null;
            this.txtFilterKode.Name = "txtFilterKode";
            // 
            // lblFilterKode
            // 
            this.lblFilterKode.AccessibleDescription = null;
            this.lblFilterKode.AccessibleName = null;
            resources.ApplyResources(this.lblFilterKode, "lblFilterKode");
            this.lblFilterKode.Font = null;
            this.lblFilterKode.Name = "lblFilterKode";
            // 
            // lblOPFunktionen
            // 
            this.lblOPFunktionen.AccessibleDescription = null;
            this.lblOPFunktionen.AccessibleName = null;
            resources.ApplyResources(this.lblOPFunktionen, "lblOPFunktionen");
            this.lblOPFunktionen.Font = null;
            this.lblOPFunktionen.Name = "lblOPFunktionen";
            // 
            // cbOPFunktionen
            // 
            this.cbOPFunktionen.AccessibleDescription = null;
            this.cbOPFunktionen.AccessibleName = null;
            resources.ApplyResources(this.cbOPFunktionen, "cbOPFunktionen");
            this.cbOPFunktionen.BackgroundImage = null;
            this.cbOPFunktionen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOPFunktionen.Font = null;
            this.cbOPFunktionen.FormattingEnabled = true;
            this.cbOPFunktionen.Name = "cbOPFunktionen";
            // 
            // txtZeit
            // 
            this.txtZeit.AccessibleDescription = null;
            this.txtZeit.AccessibleName = null;
            resources.ApplyResources(this.txtZeit, "txtZeit");
            this.txtZeit.BackgroundImage = null;
            this.txtZeit.Font = null;
            this.txtZeit.Name = "txtZeit";
            // 
            // lblZeit
            // 
            this.lblZeit.AccessibleDescription = null;
            this.lblZeit.AccessibleName = null;
            resources.ApplyResources(this.lblZeit, "lblZeit");
            this.lblZeit.Font = null;
            this.lblZeit.Name = "lblZeit";
            // 
            // lvOperationen
            // 
            this.lvOperationen.AccessibleDescription = null;
            this.lvOperationen.AccessibleName = null;
            resources.ApplyResources(this.lvOperationen, "lvOperationen");
            this.lvOperationen.BackgroundImage = null;
            this.lvOperationen.Font = null;
            this.lvOperationen.FullRowSelect = true;
            this.lvOperationen.Name = "lvOperationen";
            this.lvOperationen.UseCompatibleStateImageBehavior = false;
            // 
            // lblDatum
            // 
            this.lblDatum.AccessibleDescription = null;
            this.lblDatum.AccessibleName = null;
            resources.ApplyResources(this.lblDatum, "lblDatum");
            this.lblDatum.Font = null;
            this.lblDatum.Name = "lblDatum";
            // 
            // cmdCancel
            // 
            this.cmdCancel.AccessibleDescription = null;
            this.cmdCancel.AccessibleName = null;
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.BackgroundImage = null;
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Font = null;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // IstOperationView
            // 
            this.AcceptButton = this.cmdPopulate;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.grpOperationen);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IstOperationView";
            this.Load += new System.EventHandler(this.IstOperationView_Load);
            this.Shown += new System.EventHandler(this.IstOperationView_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IstOperationView_FormClosing);
            this.grpOperationen.ResumeLayout(false);
            this.grpOperationen.PerformLayout();
            this.grpKlinischeErgebnisse.ResumeLayout(false);
            this.grpKlinischeErgebnisse.PerformLayout();
            this.ResumeLayout(false);

        }

        void cmdCancel_Click(object sender, EventArgs e)
        {
            base.CancelClicked();
        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.GroupBox grpOperationen;
        private ListView lvOperationen;
        private Label lblDatum;
        private Button cmdCancel;
        private TextBox txtZeit;
        private Label lblZeit;
        protected override void Object2Control() { }
        private Label lblOPFunktionen;
        private ComboBox cbOPFunktionen;
        private Button cmdPopulate;
        private TextBox txtFilterKode;
        private Label lblFilterKode;
        private DateBoxPicker txtDatum;
        private TextBox txtFilterText;
        private Label lblFilterText;
        private TextBox txtAnzahl;
        private Label lblAnzahl;
        private TextBox txtZeitBis;
        private Label lblZeitBis;
        private TextBox txtFallzahl;
        private Label lblFallzahl;
        private RadioButton radQuelleExtern;
        private RadioButton radQuelleIntern;
        private GroupBox grpKlinischeErgebnisse;
        private ComboBox cbKlinischeErgebnisseTypen;
        private TextBox txtKlinischeErgebnisse;
    }
}