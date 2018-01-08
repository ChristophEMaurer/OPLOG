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
    partial class OperationenEditView : OperationenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OperationenEditView));
            this.cmdCancel = new Windows.Forms.OplButton();
            this.grpOperationen = new System.Windows.Forms.GroupBox();
            this.lblHelp1 = new System.Windows.Forms.Label();
            this.lvOperationen = new Windows.Forms.OplListView();
            this.lblFilterOPS = new System.Windows.Forms.Label();
            this.txtOPS = new Windows.Forms.OplTextBox();
            this.cmdAnzeigenOperationen = new Windows.Forms.OplButton();
            this.lblChirurg = new System.Windows.Forms.Label();
            this.cbChirurgen = new System.Windows.Forms.ComboBox();
            this.grpChirurgenOperationen = new System.Windows.Forms.GroupBox();
            this.lblHelp2 = new System.Windows.Forms.Label();
            this.lblKlinischeErgebnisse = new System.Windows.Forms.Label();
            this.lblKlinischeErgebnisseTypen = new System.Windows.Forms.Label();
            this.cbKlinischeErgebnisseTypen = new System.Windows.Forms.ComboBox();
            this.txtKlinischeErgebnisse = new Windows.Forms.OplTextBox();
            this.txtAnzahl = new Windows.Forms.OplTextBox();
            this.lblAnzahl = new System.Windows.Forms.Label();
            this.txtOPSText = new Windows.Forms.OplTextBox();
            this.txtOPSKode = new Windows.Forms.OplTextBox();
            this.lblOPSText = new System.Windows.Forms.Label();
            this.lblOPSKode = new System.Windows.Forms.Label();
            this.radQuelleExtern = new Windows.Forms.OplRadioButton();
            this.radQuelleIntern = new Windows.Forms.OplRadioButton();
            this.txtFallzahl = new Windows.Forms.OplTextBox();
            this.lblFallzahl = new System.Windows.Forms.Label();
            this.txtZeitBis = new Windows.Forms.OplTextBox();
            this.lblZeitBis = new System.Windows.Forms.Label();
            this.txtDatum = new Windows.Forms.DateBoxPicker();
            this.lblOPFunktionen = new System.Windows.Forms.Label();
            this.cbOPFunktionen = new System.Windows.Forms.ComboBox();
            this.txtZeit = new Windows.Forms.OplTextBox();
            this.lblZeit = new System.Windows.Forms.Label();
            this.lblDatum = new System.Windows.Forms.Label();
            this.cmdAnzeigenChirurgenOperationen = new Windows.Forms.OplButton();
            this.txtFilterDatumBis = new Windows.Forms.DateBoxPicker();
            this.txtFilterDatumVon = new Windows.Forms.DateBoxPicker();
            this.lblFilterDatumVon = new System.Windows.Forms.Label();
            this.lblFilterDatumBis = new System.Windows.Forms.Label();
            this.cmdApply = new Windows.Forms.OplButton();
            this.cmdDelete = new Windows.Forms.OplButton();
            this.cmdInsert = new Windows.Forms.OplButton();
            this.lvChirurgenOperationen = new Windows.Forms.OplListView();
            this.lblInfo = new System.Windows.Forms.LinkLabel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grpOperationen.SuspendLayout();
            this.grpChirurgenOperationen.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // grpOperationen
            // 
            this.grpOperationen.Controls.Add(this.lblHelp1);
            this.grpOperationen.Controls.Add(this.lvOperationen);
            this.grpOperationen.Controls.Add(this.lblFilterOPS);
            this.grpOperationen.Controls.Add(this.txtOPS);
            this.grpOperationen.Controls.Add(this.cmdAnzeigenOperationen);
            resources.ApplyResources(this.grpOperationen, "grpOperationen");
            this.grpOperationen.Name = "grpOperationen";
            this.grpOperationen.TabStop = false;
            // 
            // lblHelp1
            // 
            resources.ApplyResources(this.lblHelp1, "lblHelp1");
            this.lblHelp1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblHelp1.Name = "lblHelp1";
            // 
            // lvOperationen
            // 
            resources.ApplyResources(this.lvOperationen, "lvOperationen");
            this.lvOperationen.BalkenGrafik = false;
            this.lvOperationen.FullRowSelect = true;
            this.lvOperationen.Name = "lvOperationen";
            this.lvOperationen.Sortable = false;
            this.lvOperationen.UseCompatibleStateImageBehavior = false;
            this.lvOperationen.SelectedIndexChanged += new System.EventHandler(this.lvOperationen_SelectedIndexChanged);
            // 
            // lblFilterOPS
            // 
            resources.ApplyResources(this.lblFilterOPS, "lblFilterOPS");
            this.lblFilterOPS.Name = "lblFilterOPS";
            // 
            // txtOPS
            // 
            resources.ApplyResources(this.txtOPS, "txtOPS");
            this.txtOPS.Name = "txtOPS";
            // 
            // cmdAnzeigenOperationen
            // 
            resources.ApplyResources(this.cmdAnzeigenOperationen, "cmdAnzeigenOperationen");
            this.cmdAnzeigenOperationen.Name = "cmdAnzeigenOperationen";
            this.cmdAnzeigenOperationen.UseVisualStyleBackColor = true;
            this.cmdAnzeigenOperationen.Click += new System.EventHandler(this.cmdAnzeigenOperationen_Click);
            // 
            // lblChirurg
            // 
            resources.ApplyResources(this.lblChirurg, "lblChirurg");
            this.lblChirurg.Name = "lblChirurg";
            // 
            // cbChirurgen
            // 
            this.cbChirurgen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbChirurgen, "cbChirurgen");
            this.cbChirurgen.Name = "cbChirurgen";
            this.cbChirurgen.SelectedIndexChanged += new System.EventHandler(this.cbChirurgen_SelectedIndexChanged);
            // 
            // grpChirurgenOperationen
            // 
            this.grpChirurgenOperationen.Controls.Add(this.lblHelp2);
            this.grpChirurgenOperationen.Controls.Add(this.lblKlinischeErgebnisse);
            this.grpChirurgenOperationen.Controls.Add(this.lblKlinischeErgebnisseTypen);
            this.grpChirurgenOperationen.Controls.Add(this.cbKlinischeErgebnisseTypen);
            this.grpChirurgenOperationen.Controls.Add(this.txtKlinischeErgebnisse);
            this.grpChirurgenOperationen.Controls.Add(this.txtAnzahl);
            this.grpChirurgenOperationen.Controls.Add(this.lblAnzahl);
            this.grpChirurgenOperationen.Controls.Add(this.txtOPSText);
            this.grpChirurgenOperationen.Controls.Add(this.txtOPSKode);
            this.grpChirurgenOperationen.Controls.Add(this.lblOPSText);
            this.grpChirurgenOperationen.Controls.Add(this.lblOPSKode);
            this.grpChirurgenOperationen.Controls.Add(this.radQuelleExtern);
            this.grpChirurgenOperationen.Controls.Add(this.radQuelleIntern);
            this.grpChirurgenOperationen.Controls.Add(this.txtFallzahl);
            this.grpChirurgenOperationen.Controls.Add(this.lblFallzahl);
            this.grpChirurgenOperationen.Controls.Add(this.txtZeitBis);
            this.grpChirurgenOperationen.Controls.Add(this.lblZeitBis);
            this.grpChirurgenOperationen.Controls.Add(this.txtDatum);
            this.grpChirurgenOperationen.Controls.Add(this.lblOPFunktionen);
            this.grpChirurgenOperationen.Controls.Add(this.cbOPFunktionen);
            this.grpChirurgenOperationen.Controls.Add(this.txtZeit);
            this.grpChirurgenOperationen.Controls.Add(this.lblZeit);
            this.grpChirurgenOperationen.Controls.Add(this.lblDatum);
            this.grpChirurgenOperationen.Controls.Add(this.cmdAnzeigenChirurgenOperationen);
            this.grpChirurgenOperationen.Controls.Add(this.txtFilterDatumBis);
            this.grpChirurgenOperationen.Controls.Add(this.txtFilterDatumVon);
            this.grpChirurgenOperationen.Controls.Add(this.lblFilterDatumVon);
            this.grpChirurgenOperationen.Controls.Add(this.lblFilterDatumBis);
            this.grpChirurgenOperationen.Controls.Add(this.lblChirurg);
            this.grpChirurgenOperationen.Controls.Add(this.cbChirurgen);
            this.grpChirurgenOperationen.Controls.Add(this.cmdApply);
            this.grpChirurgenOperationen.Controls.Add(this.cmdDelete);
            this.grpChirurgenOperationen.Controls.Add(this.cmdInsert);
            this.grpChirurgenOperationen.Controls.Add(this.lvChirurgenOperationen);
            resources.ApplyResources(this.grpChirurgenOperationen, "grpChirurgenOperationen");
            this.grpChirurgenOperationen.Name = "grpChirurgenOperationen";
            this.grpChirurgenOperationen.TabStop = false;
            // 
            // lblHelp2
            // 
            resources.ApplyResources(this.lblHelp2, "lblHelp2");
            this.lblHelp2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblHelp2.Name = "lblHelp2";
            // 
            // lblKlinischeErgebnisse
            // 
            resources.ApplyResources(this.lblKlinischeErgebnisse, "lblKlinischeErgebnisse");
            this.lblKlinischeErgebnisse.Name = "lblKlinischeErgebnisse";
            // 
            // lblKlinischeErgebnisseTypen
            // 
            resources.ApplyResources(this.lblKlinischeErgebnisseTypen, "lblKlinischeErgebnisseTypen");
            this.lblKlinischeErgebnisseTypen.Name = "lblKlinischeErgebnisseTypen";
            // 
            // cbKlinischeErgebnisseTypen
            // 
            resources.ApplyResources(this.cbKlinischeErgebnisseTypen, "cbKlinischeErgebnisseTypen");
            this.cbKlinischeErgebnisseTypen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKlinischeErgebnisseTypen.FormattingEnabled = true;
            this.cbKlinischeErgebnisseTypen.Name = "cbKlinischeErgebnisseTypen";
            // 
            // txtKlinischeErgebnisse
            // 
            resources.ApplyResources(this.txtKlinischeErgebnisse, "txtKlinischeErgebnisse");
            this.txtKlinischeErgebnisse.Name = "txtKlinischeErgebnisse";
            // 
            // txtAnzahl
            // 
            resources.ApplyResources(this.txtAnzahl, "txtAnzahl");
            this.txtAnzahl.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtAnzahl.Name = "txtAnzahl";
            this.txtAnzahl.TextChanged += new System.EventHandler(this.txtAnzahl_TextChanged);
            // 
            // lblAnzahl
            // 
            resources.ApplyResources(this.lblAnzahl, "lblAnzahl");
            this.lblAnzahl.Name = "lblAnzahl";
            // 
            // txtOPSText
            // 
            resources.ApplyResources(this.txtOPSText, "txtOPSText");
            this.txtOPSText.Name = "txtOPSText";
            this.txtOPSText.ReadOnly = true;
            // 
            // txtOPSKode
            // 
            resources.ApplyResources(this.txtOPSKode, "txtOPSKode");
            this.txtOPSKode.Name = "txtOPSKode";
            this.txtOPSKode.TextChanged += new System.EventHandler(this.txtOPSKode_TextChanged);
            // 
            // lblOPSText
            // 
            resources.ApplyResources(this.lblOPSText, "lblOPSText");
            this.lblOPSText.Name = "lblOPSText";
            // 
            // lblOPSKode
            // 
            resources.ApplyResources(this.lblOPSKode, "lblOPSKode");
            this.lblOPSKode.Name = "lblOPSKode";
            // 
            // radQuelleExtern
            // 
            resources.ApplyResources(this.radQuelleExtern, "radQuelleExtern");
            this.radQuelleExtern.Name = "radQuelleExtern";
            this.radQuelleExtern.TabStop = true;
            this.radQuelleExtern.UseVisualStyleBackColor = true;
            // 
            // radQuelleIntern
            // 
            resources.ApplyResources(this.radQuelleIntern, "radQuelleIntern");
            this.radQuelleIntern.Name = "radQuelleIntern";
            this.radQuelleIntern.TabStop = true;
            this.radQuelleIntern.UseVisualStyleBackColor = true;
            // 
            // txtFallzahl
            // 
            resources.ApplyResources(this.txtFallzahl, "txtFallzahl");
            this.txtFallzahl.Name = "txtFallzahl";
            // 
            // lblFallzahl
            // 
            resources.ApplyResources(this.lblFallzahl, "lblFallzahl");
            this.lblFallzahl.Name = "lblFallzahl";
            // 
            // txtZeitBis
            // 
            resources.ApplyResources(this.txtZeitBis, "txtZeitBis");
            this.txtZeitBis.Name = "txtZeitBis";
            // 
            // lblZeitBis
            // 
            resources.ApplyResources(this.lblZeitBis, "lblZeitBis");
            this.lblZeitBis.Name = "lblZeitBis";
            // 
            // txtDatum
            // 
            resources.ApplyResources(this.txtDatum, "txtDatum");
            this.txtDatum.Name = "txtDatum";
            // 
            // lblOPFunktionen
            // 
            resources.ApplyResources(this.lblOPFunktionen, "lblOPFunktionen");
            this.lblOPFunktionen.Name = "lblOPFunktionen";
            // 
            // cbOPFunktionen
            // 
            resources.ApplyResources(this.cbOPFunktionen, "cbOPFunktionen");
            this.cbOPFunktionen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOPFunktionen.FormattingEnabled = true;
            this.cbOPFunktionen.Name = "cbOPFunktionen";
            // 
            // txtZeit
            // 
            resources.ApplyResources(this.txtZeit, "txtZeit");
            this.txtZeit.Name = "txtZeit";
            // 
            // lblZeit
            // 
            resources.ApplyResources(this.lblZeit, "lblZeit");
            this.lblZeit.Name = "lblZeit";
            // 
            // lblDatum
            // 
            resources.ApplyResources(this.lblDatum, "lblDatum");
            this.lblDatum.Name = "lblDatum";
            // 
            // cmdAnzeigenChirurgenOperationen
            // 
            resources.ApplyResources(this.cmdAnzeigenChirurgenOperationen, "cmdAnzeigenChirurgenOperationen");
            this.cmdAnzeigenChirurgenOperationen.Name = "cmdAnzeigenChirurgenOperationen";
            this.cmdAnzeigenChirurgenOperationen.UseVisualStyleBackColor = true;
            this.cmdAnzeigenChirurgenOperationen.Click += new System.EventHandler(this.cmdAnzeigenChirurgenOperationen_Click);
            // 
            // txtFilterDatumBis
            // 
            resources.ApplyResources(this.txtFilterDatumBis, "txtFilterDatumBis");
            this.txtFilterDatumBis.Name = "txtFilterDatumBis";
            // 
            // txtFilterDatumVon
            // 
            resources.ApplyResources(this.txtFilterDatumVon, "txtFilterDatumVon");
            this.txtFilterDatumVon.Name = "txtFilterDatumVon";
            // 
            // lblFilterDatumVon
            // 
            resources.ApplyResources(this.lblFilterDatumVon, "lblFilterDatumVon");
            this.lblFilterDatumVon.Name = "lblFilterDatumVon";
            // 
            // lblFilterDatumBis
            // 
            resources.ApplyResources(this.lblFilterDatumBis, "lblFilterDatumBis");
            this.lblFilterDatumBis.Name = "lblFilterDatumBis";
            // 
            // cmdApply
            // 
            resources.ApplyResources(this.cmdApply, "cmdApply");
            this.cmdApply.Name = "cmdApply";
            this.cmdApply.UseVisualStyleBackColor = true;
            this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
            // 
            // cmdDelete
            // 
            resources.ApplyResources(this.cmdDelete, "cmdDelete");
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdInsert
            // 
            resources.ApplyResources(this.cmdInsert, "cmdInsert");
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.UseVisualStyleBackColor = true;
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // lvChirurgenOperationen
            // 
            resources.ApplyResources(this.lvChirurgenOperationen, "lvChirurgenOperationen");
            this.lvChirurgenOperationen.BalkenGrafik = false;
            this.lvChirurgenOperationen.FullRowSelect = true;
            this.lvChirurgenOperationen.Name = "lvChirurgenOperationen";
            this.lvChirurgenOperationen.Sortable = false;
            this.lvChirurgenOperationen.UseCompatibleStateImageBehavior = false;
            this.lvChirurgenOperationen.SelectedIndexChanged += new System.EventHandler(this.lvChirurgenOperationen_SelectedIndexChanged);
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.TabStop = true;
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grpOperationen);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpChirurgenOperationen);
            // 
            // OperationenEditView
            // 
            this.AcceptButton = this.cmdInsert;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.cmdCancel);
            this.Name = "OperationenEditView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.OperationenEditView_Load);
            this.Shown += new System.EventHandler(this.OperationenEditView_Shown);
            this.grpOperationen.ResumeLayout(false);
            this.grpOperationen.PerformLayout();
            this.grpChirurgenOperationen.ResumeLayout(false);
            this.grpChirurgenOperationen.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        void cmdCancel_Click(object sender, EventArgs e)
        {
            base.CancelClicked();
        }

        #endregion

        private Button cmdCancel;
        private GroupBox grpOperationen;
        private Label lblFilterOPS;
        private TextBox txtOPS;
        private Button cmdAnzeigenOperationen;
        private Label lblChirurg;
        private ComboBox cbChirurgen;
        private GroupBox grpChirurgenOperationen;
        private OplListView lvChirurgenOperationen;
        private Button cmdApply;
        private Button cmdDelete;
        private Button cmdInsert;
        private OplListView lvOperationen;
        private DateBoxPicker txtFilterDatumBis;
        private DateBoxPicker txtFilterDatumVon;
        private Label lblFilterDatumVon;
        private Label lblFilterDatumBis;
        private Button cmdAnzeigenChirurgenOperationen;
        private TextBox txtFallzahl;
        private Label lblFallzahl;
        private TextBox txtZeitBis;
        private Label lblZeitBis;
        private DateBoxPicker txtDatum;
        private Label lblOPFunktionen;
        private ComboBox cbOPFunktionen;
        private TextBox txtZeit;
        private Label lblZeit;
        private Label lblDatum;
        private RadioButton radQuelleExtern;
        private RadioButton radQuelleIntern;
        private TextBox txtOPSText;
        private TextBox txtOPSKode;
        private Label lblOPSText;
        private Label lblOPSKode;
        private LinkLabel lblInfo;
        private TextBox txtAnzahl;
        private Label lblAnzahl;
        private Label lblKlinischeErgebnisse;
        private Label lblKlinischeErgebnisseTypen;
        private ComboBox cbKlinischeErgebnisseTypen;
        private TextBox txtKlinischeErgebnisse;
        private Label lblHelp1;
        private Label lblHelp2;
        private SplitContainer splitContainer;
    }
}