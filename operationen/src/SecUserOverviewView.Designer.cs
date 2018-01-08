namespace Operationen
{
    partial class SecUserOverviewView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SecUserOverviewView));
            this.cmdCancel = new Windows.Forms.OplButton();
            this.lvChirurgen = new Windows.Forms.OplListView();
            this.grpChirurgen = new System.Windows.Forms.GroupBox();
            this.grpAbteilungen = new System.Windows.Forms.GroupBox();
            this.lvAbteilungen = new Windows.Forms.OplListView();
            this.grpRollen = new System.Windows.Forms.GroupBox();
            this.lvSecGroups = new Windows.Forms.OplListView();
            this.grpWeiterbilder = new System.Windows.Forms.GroupBox();
            this.lvWeiterbilder = new Windows.Forms.OplListView();
            this.chkWeiterbilder = new Windows.Forms.OplCheckBox();
            this.grpRechte = new System.Windows.Forms.GroupBox();
            this.lvSecRights = new Windows.Forms.OplListView();
            this.grpAssigned = new System.Windows.Forms.GroupBox();
            this.lvWeiterzubildende = new Windows.Forms.OplListView();
            this.grpDaten = new System.Windows.Forms.GroupBox();
            this.lvDaten = new Windows.Forms.OplListView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.grpChirurgen.SuspendLayout();
            this.grpAbteilungen.SuspendLayout();
            this.grpRollen.SuspendLayout();
            this.grpWeiterbilder.SuspendLayout();
            this.grpRechte.SuspendLayout();
            this.grpAssigned.SuspendLayout();
            this.grpDaten.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
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
            // lvChirurgen
            // 
            resources.ApplyResources(this.lvChirurgen, "lvChirurgen");
            this.lvChirurgen.DoubleClickActivation = false;
            this.lvChirurgen.FullRowSelect = true;
            this.lvChirurgen.HideSelection = false;
            this.lvChirurgen.MultiSelect = false;
            this.lvChirurgen.Name = "lvChirurgen";
            this.lvChirurgen.UseCompatibleStateImageBehavior = false;
            this.lvChirurgen.View = System.Windows.Forms.View.Details;
            this.lvChirurgen.SelectedIndexChanged += new System.EventHandler(this.lvChirurgen_SelectedIndexChanged);
            // 
            // grpChirurgen
            // 
            this.grpChirurgen.Controls.Add(this.lvChirurgen);
            resources.ApplyResources(this.grpChirurgen, "grpChirurgen");
            this.grpChirurgen.Name = "grpChirurgen";
            this.grpChirurgen.TabStop = false;
            // 
            // grpAbteilungen
            // 
            this.grpAbteilungen.Controls.Add(this.lvAbteilungen);
            resources.ApplyResources(this.grpAbteilungen, "grpAbteilungen");
            this.grpAbteilungen.Name = "grpAbteilungen";
            this.grpAbteilungen.TabStop = false;
            // 
            // lvAbteilungen
            // 
            resources.ApplyResources(this.lvAbteilungen, "lvAbteilungen");
            this.lvAbteilungen.DoubleClickActivation = false;
            this.lvAbteilungen.FullRowSelect = true;
            this.lvAbteilungen.HideSelection = false;
            this.lvAbteilungen.MultiSelect = false;
            this.lvAbteilungen.Name = "lvAbteilungen";
            this.lvAbteilungen.UseCompatibleStateImageBehavior = false;
            this.lvAbteilungen.View = System.Windows.Forms.View.Details;
            // 
            // grpRollen
            // 
            this.grpRollen.Controls.Add(this.lvSecGroups);
            resources.ApplyResources(this.grpRollen, "grpRollen");
            this.grpRollen.Name = "grpRollen";
            this.grpRollen.TabStop = false;
            // 
            // lvSecGroups
            // 
            resources.ApplyResources(this.lvSecGroups, "lvSecGroups");
            this.lvSecGroups.DoubleClickActivation = false;
            this.lvSecGroups.FullRowSelect = true;
            this.lvSecGroups.HideSelection = false;
            this.lvSecGroups.MultiSelect = false;
            this.lvSecGroups.Name = "lvSecGroups";
            this.lvSecGroups.UseCompatibleStateImageBehavior = false;
            this.lvSecGroups.View = System.Windows.Forms.View.Details;
            // 
            // grpWeiterbilder
            // 
            this.grpWeiterbilder.Controls.Add(this.lvWeiterbilder);
            resources.ApplyResources(this.grpWeiterbilder, "grpWeiterbilder");
            this.grpWeiterbilder.Name = "grpWeiterbilder";
            this.grpWeiterbilder.TabStop = false;
            // 
            // lvWeiterbilder
            // 
            resources.ApplyResources(this.lvWeiterbilder, "lvWeiterbilder");
            this.lvWeiterbilder.DoubleClickActivation = false;
            this.lvWeiterbilder.FullRowSelect = true;
            this.lvWeiterbilder.HideSelection = false;
            this.lvWeiterbilder.MultiSelect = false;
            this.lvWeiterbilder.Name = "lvWeiterbilder";
            this.lvWeiterbilder.UseCompatibleStateImageBehavior = false;
            this.lvWeiterbilder.View = System.Windows.Forms.View.Details;
            // 
            // chkWeiterbilder
            // 
            resources.ApplyResources(this.chkWeiterbilder, "chkWeiterbilder");
            this.chkWeiterbilder.Name = "chkWeiterbilder";
            this.chkWeiterbilder.UseVisualStyleBackColor = true;
            // 
            // grpRechte
            // 
            this.grpRechte.Controls.Add(this.lvSecRights);
            resources.ApplyResources(this.grpRechte, "grpRechte");
            this.grpRechte.Name = "grpRechte";
            this.grpRechte.TabStop = false;
            // 
            // lvSecRights
            // 
            resources.ApplyResources(this.lvSecRights, "lvSecRights");
            this.lvSecRights.DoubleClickActivation = false;
            this.lvSecRights.FullRowSelect = true;
            this.lvSecRights.HideSelection = false;
            this.lvSecRights.MultiSelect = false;
            this.lvSecRights.Name = "lvSecRights";
            this.lvSecRights.UseCompatibleStateImageBehavior = false;
            this.lvSecRights.View = System.Windows.Forms.View.Details;
            // 
            // grpAssigned
            // 
            this.grpAssigned.Controls.Add(this.lvWeiterzubildende);
            this.grpAssigned.Controls.Add(this.chkWeiterbilder);
            resources.ApplyResources(this.grpAssigned, "grpAssigned");
            this.grpAssigned.Name = "grpAssigned";
            this.grpAssigned.TabStop = false;
            // 
            // lvWeiterzubildende
            // 
            resources.ApplyResources(this.lvWeiterzubildende, "lvWeiterzubildende");
            this.lvWeiterzubildende.DoubleClickActivation = false;
            this.lvWeiterzubildende.FullRowSelect = true;
            this.lvWeiterzubildende.HideSelection = false;
            this.lvWeiterzubildende.MultiSelect = false;
            this.lvWeiterzubildende.Name = "lvWeiterzubildende";
            this.lvWeiterzubildende.UseCompatibleStateImageBehavior = false;
            this.lvWeiterzubildende.View = System.Windows.Forms.View.Details;
            // 
            // grpDaten
            // 
            this.grpDaten.Controls.Add(this.lvDaten);
            resources.ApplyResources(this.grpDaten, "grpDaten");
            this.grpDaten.Name = "grpDaten";
            this.grpDaten.TabStop = false;
            // 
            // lvDaten
            // 
            resources.ApplyResources(this.lvDaten, "lvDaten");
            this.lvDaten.DoubleClickActivation = false;
            this.lvDaten.FullRowSelect = true;
            this.lvDaten.HideSelection = false;
            this.lvDaten.MultiSelect = false;
            this.lvDaten.Name = "lvDaten";
            this.lvDaten.UseCompatibleStateImageBehavior = false;
            this.lvDaten.View = System.Windows.Forms.View.Details;
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer6);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            // 
            // splitContainer6
            // 
            this.splitContainer6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.splitContainer6, "splitContainer6");
            this.splitContainer6.Name = "splitContainer6";
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.splitContainer5);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.splitContainer4);
            // 
            // splitContainer5
            // 
            this.splitContainer5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.splitContainer5, "splitContainer5");
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.grpChirurgen);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.grpWeiterbilder);
            // 
            // splitContainer4
            // 
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.splitContainer4, "splitContainer4");
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.grpAssigned);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.grpDaten);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.splitContainer2, "splitContainer2");
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.grpRechte);
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.splitContainer3, "splitContainer3");
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.grpAbteilungen);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.grpRollen);
            // 
            // SecUserOverviewView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.cmdCancel);
            this.Name = "SecUserOverviewView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.SecUserOverviewView_Load);
            this.grpChirurgen.ResumeLayout(false);
            this.grpAbteilungen.ResumeLayout(false);
            this.grpRollen.ResumeLayout(false);
            this.grpWeiterbilder.ResumeLayout(false);
            this.grpRechte.ResumeLayout(false);
            this.grpAssigned.ResumeLayout(false);
            this.grpAssigned.PerformLayout();
            this.grpDaten.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel2.ResumeLayout(false);
            this.splitContainer6.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            this.splitContainer5.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.OplButton cmdCancel;
        private Windows.Forms.OplListView lvChirurgen;
        private System.Windows.Forms.GroupBox grpChirurgen;
        private System.Windows.Forms.GroupBox grpAbteilungen;
        private Windows.Forms.OplListView lvAbteilungen;
        private System.Windows.Forms.GroupBox grpRollen;
        private Windows.Forms.OplListView lvSecGroups;
        private System.Windows.Forms.GroupBox grpWeiterbilder;
        private Windows.Forms.OplListView lvWeiterbilder;
        private Windows.Forms.OplCheckBox chkWeiterbilder;
        private System.Windows.Forms.GroupBox grpRechte;
        private Windows.Forms.OplListView lvSecRights;
        private System.Windows.Forms.GroupBox grpAssigned;
        private Windows.Forms.OplListView lvWeiterzubildende;
        private System.Windows.Forms.GroupBox grpDaten;
        private Windows.Forms.OplListView lvDaten;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer6;
        private System.Windows.Forms.SplitContainer splitContainer5;
    }
}