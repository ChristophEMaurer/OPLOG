using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Utility;
using Windows.Forms;

namespace Operationen
{
	/// <summary>
	/// Zusammenfassung für KommentareView.
	/// </summary>
    public class KommentareView : OperationenForm
	{
        public const int KommentarStatusBearbeitung = 0;
        public const int KommentarStatusFertig = 1;

        private Windows.Forms.OplListView lvChirurgen;
        private Windows.Forms.OplButton cmdOK;
        private System.Windows.Forms.GroupBox grpKommentare;
        private System.Windows.Forms.GroupBox grpChirurgen;
        private Windows.Forms.OplButton cmdCommentEdit;
        private OplButton cmdCommentNew;

        private OplButton cmdAllComments;
        private Windows.Forms.OplTextBox txtKommentarVon;
        private OplListView lvComments;
        private Label lblVon;
        private bool _bAllComments;
        private TextBox txtKommentarFuer;
        private Label lblFuer;
        private Button cmdDone;
        private Label lblInfo;
        private SplitContainer splitContainer;

		private System.ComponentModel.Container components = null;

        public KommentareView(BusinessLayer businessLayer)
            : base(businessLayer)
		{
			InitializeComponent();

            cmdCommentNew.SetSecurity(BusinessLayer, "KommentareView.cmdCommentNew");
            cmdAllComments.SetSecurity(BusinessLayer, "KommentareView.cmdAllComments");

            InititalizeComments();

		}

		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KommentareView));
            this.lvChirurgen = new Windows.Forms.OplListView();
            this.cmdOK = new Windows.Forms.OplButton();
            this.grpKommentare = new System.Windows.Forms.GroupBox();
            this.cmdDone = new Windows.Forms.OplButton();
            this.txtKommentarFuer = new Windows.Forms.OplTextBox();
            this.lblFuer = new System.Windows.Forms.Label();
            this.lblVon = new System.Windows.Forms.Label();
            this.lvComments = new Windows.Forms.OplListView();
            this.txtKommentarVon = new Windows.Forms.OplTextBox();
            this.cmdCommentEdit = new Windows.Forms.OplButton();
            this.cmdCommentNew = new Windows.Forms.OplButton();
            this.grpChirurgen = new System.Windows.Forms.GroupBox();
            this.cmdAllComments = new Windows.Forms.OplButton();
            this.lblInfo = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grpKommentare.SuspendLayout();
            this.grpChirurgen.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvChirurgen
            // 
            resources.ApplyResources(this.lvChirurgen, "lvChirurgen");
            this.lvChirurgen.Name = "lvChirurgen";
            this.lvChirurgen.UseCompatibleStateImageBehavior = false;
            this.lvChirurgen.SelectedIndexChanged += new System.EventHandler(this.lvChirurgen_SelectedIndexChanged);
            this.lvChirurgen.DoubleClick += new System.EventHandler(this.ChirurgenView_DoubleClick);
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // grpKommentare
            // 
            this.grpKommentare.Controls.Add(this.cmdDone);
            this.grpKommentare.Controls.Add(this.txtKommentarFuer);
            this.grpKommentare.Controls.Add(this.lblFuer);
            this.grpKommentare.Controls.Add(this.lblVon);
            this.grpKommentare.Controls.Add(this.lvComments);
            this.grpKommentare.Controls.Add(this.txtKommentarVon);
            this.grpKommentare.Controls.Add(this.cmdCommentEdit);
            resources.ApplyResources(this.grpKommentare, "grpKommentare");
            this.grpKommentare.Name = "grpKommentare";
            this.grpKommentare.TabStop = false;
            // 
            // cmdDone
            // 
            resources.ApplyResources(this.cmdDone, "cmdDone");
            this.cmdDone.Name = "cmdDone";
            this.cmdDone.Click += new System.EventHandler(this.cmdDone_Click);
            // 
            // txtKommentarFuer
            // 
            resources.ApplyResources(this.txtKommentarFuer, "txtKommentarFuer");
            this.txtKommentarFuer.Name = "txtKommentarFuer";
            this.txtKommentarFuer.ReadOnly = true;
            // 
            // lblFuer
            // 
            resources.ApplyResources(this.lblFuer, "lblFuer");
            this.lblFuer.Name = "lblFuer";
            // 
            // lblVon
            // 
            resources.ApplyResources(this.lblVon, "lblVon");
            this.lblVon.Name = "lblVon";
            // 
            // lvComments
            // 
            resources.ApplyResources(this.lvComments, "lvComments");
            this.lvComments.Name = "lvComments";
            this.lvComments.UseCompatibleStateImageBehavior = false;
            this.lvComments.SelectedIndexChanged += new System.EventHandler(this.lvComments_SelectedIndexChanged);
            // 
            // txtKommentarVon
            // 
            resources.ApplyResources(this.txtKommentarVon, "txtKommentarVon");
            this.txtKommentarVon.Name = "txtKommentarVon";
            this.txtKommentarVon.ReadOnly = true;
            // 
            // cmdCommentEdit
            // 
            resources.ApplyResources(this.cmdCommentEdit, "cmdCommentEdit");
            this.cmdCommentEdit.Name = "cmdCommentEdit";
            this.cmdCommentEdit.Click += new System.EventHandler(this.cmdCommentEdit_Click);
            // 
            // cmdCommentNew
            // 
            resources.ApplyResources(this.cmdCommentNew, "cmdCommentNew");
            this.cmdCommentNew.Name = "cmdCommentNew";
            this.cmdCommentNew.SecurityManager = null;
            this.cmdCommentNew.UserRight = null;
            this.cmdCommentNew.Click += new System.EventHandler(this.cmdCommentNew_Click);
            // 
            // grpChirurgen
            // 
            this.grpChirurgen.Controls.Add(this.cmdAllComments);
            this.grpChirurgen.Controls.Add(this.lvChirurgen);
            this.grpChirurgen.Controls.Add(this.cmdCommentNew);
            resources.ApplyResources(this.grpChirurgen, "grpChirurgen");
            this.grpChirurgen.Name = "grpChirurgen";
            this.grpChirurgen.TabStop = false;
            // 
            // cmdAllComments
            // 
            resources.ApplyResources(this.cmdAllComments, "cmdAllComments");
            this.cmdAllComments.Name = "cmdAllComments";
            this.cmdAllComments.SecurityManager = null;
            this.cmdAllComments.UserRight = null;
            this.cmdAllComments.Click += new System.EventHandler(this.cmdAllComments_Click);
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.splitContainer.Panel1.Controls.Add(this.grpChirurgen);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpKommentare);
            // 
            // KommentareView
            // 
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.cmdOK;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.cmdOK);
            this.Name = "KommentareView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.KommentareView_Load);
            this.grpKommentare.ResumeLayout(false);
            this.grpKommentare.PerformLayout();
            this.grpChirurgen.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }
		#endregion

        private void InititalizeComments()
        {
            lvComments.Clear();

            DefaultListViewProperties(lvComments);
            SetWatermark(lvComments);

            lvComments.Columns.Add(GetText("angelegt"), 100, HorizontalAlignment.Left);
            lvComments.Columns.Add(GetText("status"), 70, HorizontalAlignment.Left);
            lvComments.Columns.Add(GetText("zeitraum"), 140, HorizontalAlignment.Left);
            lvComments.Columns.Add(GetText("von"), 80, HorizontalAlignment.Left);
            lvComments.Columns.Add(GetText("ueber"), 80, HorizontalAlignment.Left);
            lvComments.Columns.Add(GetText("dienststellung"), -2, HorizontalAlignment.Left);
        }

        private void PopulateComments(int nID_Chirurgen)
        {
            DataView dv = null;

            lvComments.BeginUpdate();
            lvComments.Items.Clear();

            if (nID_Chirurgen == -1)
            {
                _bAllComments = true;
                dv = BusinessLayer.GetKommentare();
            }
            else
            {
                _bAllComments = false;
                dv = this.BusinessLayer.GetKommentare(nID_Chirurgen);
            }

            foreach(DataRow row in dv.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem(Tools.DBNullableDateTime2DateTimeString(row["Datum"]));
                lvi.Tag = row["ID_Kommentare"];

                if (ConvertToInt32(row["Status"]) == KommentarStatusBearbeitung)
                {
                    lvi.SubItems.Add(GetText("status_edit"));
                }
                else
                {
                    lvi.SubItems.Add(GetText("status_done"));
                }

                lvi.SubItems.Add(
                    Tools.DBNullableDateTime2DateString(row["AbschnittVon"])
                    + " - "
                    + Tools.DBNullableDateTime2DateString(row["AbschnittBis"])
                    );
                lvi.SubItems.Add((string)row["NachnameVon"]);
                lvi.SubItems.Add((string)row["NachnameFuer"]);
                lvi.SubItems.Add((string)row["Funktion"]);

                lvComments.Items.Add(lvi);
            }
            lvComments.EndUpdate();

            txtKommentarVon.Clear();
            txtKommentarFuer.Clear();

            cmdCommentEdit.Enabled = false;
            cmdDone.Enabled = false;

            cmdCommentNew.Enabled = true;
            cmdAllComments.Enabled = true;
        }

        private void KommentareView_Load(object sender, System.EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));

            cmdAllComments.Text = GetText("cmdAllComments");
            cmdCommentNew.Text = GetText("cmdCommentNew");
            
            SetInfoText(lblInfo, string.Format(GetText("info"), cmdDone.Text));

            PopulateChirurgen(lvChirurgen);

            if (lvChirurgen.Items.Count == 1)
            {
                lvChirurgen.SelectedIndices.Clear();
                _bIgnoreControlEvents = true;
                lvChirurgen.SelectedIndices.Add(0);
                _bIgnoreControlEvents = false;
                ShowCommentsFor1();
                if (lvComments.Items.Count > 0)
                {
                    lvComments.SelectedIndices.Clear();
                    lvComments.SelectedIndices.Add(0);
                }
            }
        }

        private int GetSelectedComment()
        {
            int nID_Kommentare = GetFirstSelectedTag(lvComments, true);

            return nID_Kommentare;
        }

        private void cmdCommentNew_Click(object sender, System.EventArgs e)
        {
            int nID_ChirurgenFuer = GetFirstSelectedTag(lvChirurgen, true, GetText("chirurg"));
            if (nID_ChirurgenFuer != -1)
            {
                if (DialogResult.OK == new KommentarVerfassenView(BusinessLayer, -1, nID_ChirurgenFuer).ShowDialog())
                {
                    this.RePopulateComments();
                }
            }
        }

        private void cmdOK_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Wenn eine Bemerkung bearbeitet oder hinzugefuegt wurde, dann wieder alle oder nur die 
        /// Bemerkung fuer einen Chirurg anzeigen
        /// </summary>
        private void RePopulateComments()
        {
            if (_bAllComments)
            {
                PopulateComments(-1);
            }
            else
            {
                int nID_Chirurgen = GetFirstSelectedTag(lvChirurgen, false);
                if (nID_Chirurgen != -1)
                {
                    PopulateComments(nID_Chirurgen);
                }
            }
        }

        private void cmdCommentEdit_Click(object sender, System.EventArgs e)
        {
            int nID_Kommentare = GetSelectedComment();

            if (nID_Kommentare != -1)
            {
                if (DialogResult.OK == new KommentarVerfassenView(BusinessLayer, nID_Kommentare, -1).ShowDialog())
                {
                    this.RePopulateComments();
                }
            }
        }

        private void ShowCommentsFor1()
        {
            int nID_Chirurgen = GetFirstSelectedTag(lvChirurgen, false);
            if (nID_Chirurgen != -1)
            {
                PopulateComments(nID_Chirurgen);
            }
        }
        private void ChirurgenView_DoubleClick(object sender, System.EventArgs e)
        {
            ShowCommentsFor1();
        }

        private void cmdAllComments_Click(object sender, System.EventArgs e)
        {
            PopulateComments(-1);
        }

        private void PopulateCommentDetails()
        {
            ListViewItem lvi = GetFirstSelectedLVI(lvComments);

            if (lvi != null)
            {
                int ID_Kommentare = (int)lvi.Tag;

                if (ID_Kommentare != -1)
                {
                    DataRow row = BusinessLayer.DatabaseLayer.GetKommentar(ID_Kommentare);

                    txtKommentarVon.Text = (string)row["KommentarVon"];
                    txtKommentarFuer.Text = (string)row["KommentarFuer"];
                    lblVon.Text = GetText("einschaetzungVon") + " " + lvi.SubItems[3].Text;
                    lblFuer.Text = GetText("stellungnahmeVon") + " " + lvi.SubItems[4].Text;

                    // Wenn der Kommentar ueber den angemeldeten Benutzer ist
                    // und die Stellungnahme keinen Text enthaelt, dann darf dieser
                    // seine Stellungnahme hinzufuegen.
                    int status = ConvertToInt32(row["Status"]);

                    // Nur einer der beiden darf Bearbeiten klicken
                    cmdCommentEdit.Enabled = (status == KommentarStatusBearbeitung) &&
                        (
                            ConvertToInt32(row["ID_Chirurgen_Von"]) == BusinessLayer.CurrentUser_ID_Chirurgen
                         ||
                            ConvertToInt32(row["ID_Chirurgen_Fuer"]) == BusinessLayer.CurrentUser_ID_Chirurgen
                         );

                    // Nur derjenige, der den Kommentar angelegt hat, darf "Fertig" klicken
                    cmdDone.Enabled =
                        (ConvertToInt32(row["ID_Chirurgen_Von"]) == BusinessLayer.CurrentUser_ID_Chirurgen)
                        && (status == KommentarStatusBearbeitung);
                }
            }
        }

        private void lvComments_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            PopulateCommentDetails();
        }

        private void cmdShowComment_Click(object sender, EventArgs e)
        {
            ShowCommentsFor1();
        }

        private void lvChirurgen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_bIgnoreControlEvents)
            {
                ShowCommentsFor1();
            }
        }

        private void cmdDone_Click(object sender, EventArgs e)
        {
            int ID_Kommentare = GetFirstSelectedTag(lvComments, true);

            if (ID_Kommentare != -1)
            {
                if (txtKommentarFuer.Text.Length == 0 || txtKommentarVon.Text.Length == 0)
                {
                    MessageBox(GetText("status_update_text"));
                }
                else
                {
                    if (Confirm(GetText("status_update_confirm")))
                    {
                        if (Confirm(GetText("status_update_confirm2")))
                        {
                            DataRow row = BusinessLayer.DatabaseLayer.GetKommentar(ID_Kommentare);

                            row["Status"] = KommentarStatusFertig;
                            BusinessLayer.UpdateKommentar(row);
                            RePopulateComments();
                        }
                    }
                }
            }
        }
    }
}

