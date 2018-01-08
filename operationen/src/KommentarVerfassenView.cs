using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Globalization;

using Utility;
using Windows.Forms;

namespace Operationen
{
	/// <summary>
    /// Derjenige, der den Kommentar angelegt hat, darf links editieren.
    /// Derjenige, ueber den der Kommentar ist, darf rechts editieren.
	/// </summary>
    public class KommentarVerfassenView : OperationenForm
	{
        private System.Windows.Forms.Label lblChirurgen;
        private Windows.Forms.OplTextBox txtKommentarVon;
        private System.Windows.Forms.ComboBox cbChirurgen;
        private Windows.Forms.OplButton cmdOK;
        private Windows.Forms.OplButton cmdCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpComment;
        private System.Windows.Forms.Label lblDatumVon;
        private System.Windows.Forms.Label lblDatumBis;
        private Windows.Forms.OplTextBox txtCommentDate;
        private System.Windows.Forms.Label lblVon;

		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        private DataRow _comment;
        private bool _bIsEditMode;
        private OplTextBox txtVerfasstVon;
        private Label label6;
        private Label lblFunktion;
        private ComboBox cbFunktionen;
        private OplTextBox txtKommentarFuer;
        private Label lblInfo;
        private DateBoxPicker txtCommentRangeTo;
        private DateBoxPicker txtCommentRangeFrom;
        private Label lblFuer;

        public KommentarVerfassenView(BusinessLayer businessLayer, int nID_Kommentare, int nID_ChirurgenFuer)
            : base(businessLayer)
        {
            InitializeComponent();

            if (nID_Kommentare == -1)
            {
                // Neue: aus Menu alles leer: nIDChirurgenFuer == -1
                // Zu einem bestimmten Chirurg: nIDChirurgenFuer != -1
                _bIsEditMode  = false;
                _comment = BusinessLayer.CreateDataRowKommentar(nID_ChirurgenFuer);
            }
            else
            {
                _bIsEditMode  = true;
                _comment = BusinessLayer.GetKommentar(nID_Kommentare);
            }
        }

        private void PopulateFunktionen()
        {
            BusinessLayer.PopulateFunktionen(cbFunktionen);
        }
 
        protected override void Object2Control()
        {
            this.txtCommentDate.Text = Tools.DBNullableDateTime2DateTimeString(_comment["Datum"]);
            txtVerfasstVon.Text = (string) _comment["NachnameVon"];
            this.txtCommentRangeFrom.Text = Tools.DBDateTime2DateString(_comment["AbschnittVon"]);
            this.txtCommentRangeTo.Text = Tools.DBDateTime2DateString(_comment["AbschnittBis"]);

            cbChirurgen.SelectedValue = _comment["ID_Chirurgen_Fuer"];
            cbFunktionen.SelectedValue = _comment["ID_ChirurgenFunktionen"];

            this.txtKommentarVon.Text = (string)_comment["KommentarVon"];
            this.txtKommentarFuer.Text = (string)_comment["KommentarFuer"];

            SetLabelTexts();
        }

        protected override void Control2Object()
        {
            _comment["KommentarVon"] = this.txtKommentarVon.Text;
            _comment["KommentarFuer"] = this.txtKommentarFuer.Text;
            _comment["AbschnittVon"] = Tools.InputTextDate2DateTime(txtCommentRangeFrom.Text);
            _comment["AbschnittBis"] = Tools.InputTextDate2DateTime(txtCommentRangeTo.Text);
            _comment["ID_Chirurgen_Fuer"] = cbChirurgen.SelectedValue;
            _comment["ID_ChirurgenFunktionen"] = cbFunktionen.SelectedValue;
        }

        protected override void SaveObject()
        {
            if (_bIsEditMode)
            {
                BusinessLayer.UpdateKommentar(_comment);
            }
            else
            {
                BusinessLayer.InsertKommentar(_comment);
            }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KommentarVerfassenView));
            this.lblChirurgen = new System.Windows.Forms.Label();
            this.txtKommentarVon = new Windows.Forms.OplTextBox();
            this.cbChirurgen = new System.Windows.Forms.ComboBox();
            this.cmdOK = new Windows.Forms.OplButton();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCommentDate = new Windows.Forms.OplTextBox();
            this.grpComment = new System.Windows.Forms.GroupBox();
            this.txtCommentRangeTo = new Windows.Forms.DateBoxPicker();
            this.txtCommentRangeFrom = new Windows.Forms.DateBoxPicker();
            this.lblFuer = new System.Windows.Forms.Label();
            this.txtKommentarFuer = new Windows.Forms.OplTextBox();
            this.lblVon = new System.Windows.Forms.Label();
            this.cbFunktionen = new System.Windows.Forms.ComboBox();
            this.lblFunktion = new System.Windows.Forms.Label();
            this.txtVerfasstVon = new Windows.Forms.OplTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblDatumBis = new System.Windows.Forms.Label();
            this.lblDatumVon = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.grpComment.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblChirurgen
            // 
            resources.ApplyResources(this.lblChirurgen, "lblChirurgen");
            this.lblChirurgen.Name = "lblChirurgen";
            // 
            // txtKommentarVon
            // 
            resources.ApplyResources(this.txtKommentarVon, "txtKommentarVon");
            this.txtKommentarVon.Name = "txtKommentarVon";
            this.txtKommentarVon.ProtectContents = false;
            // 
            // cbChirurgen
            // 
            this.cbChirurgen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbChirurgen, "cbChirurgen");
            this.cbChirurgen.Name = "cbChirurgen";
            this.cbChirurgen.SelectedIndexChanged += new System.EventHandler(this.cbChirurgen_SelectedIndexChanged);
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.SecurityManager = null;
            this.cmdOK.UserRight = null;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.SecurityManager = null;
            this.cmdCancel.UserRight = null;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtCommentDate
            // 
            resources.ApplyResources(this.txtCommentDate, "txtCommentDate");
            this.txtCommentDate.Name = "txtCommentDate";
            this.txtCommentDate.ProtectContents = false;
            this.txtCommentDate.ReadOnly = true;
            // 
            // grpComment
            // 
            resources.ApplyResources(this.grpComment, "grpComment");
            this.grpComment.Controls.Add(this.txtCommentRangeTo);
            this.grpComment.Controls.Add(this.txtCommentRangeFrom);
            this.grpComment.Controls.Add(this.lblFuer);
            this.grpComment.Controls.Add(this.txtKommentarFuer);
            this.grpComment.Controls.Add(this.txtKommentarVon);
            this.grpComment.Controls.Add(this.lblVon);
            this.grpComment.Controls.Add(this.cbFunktionen);
            this.grpComment.Controls.Add(this.lblFunktion);
            this.grpComment.Controls.Add(this.lblChirurgen);
            this.grpComment.Controls.Add(this.txtVerfasstVon);
            this.grpComment.Controls.Add(this.label6);
            this.grpComment.Controls.Add(this.cbChirurgen);
            this.grpComment.Controls.Add(this.lblDatumBis);
            this.grpComment.Controls.Add(this.lblDatumVon);
            this.grpComment.Controls.Add(this.label1);
            this.grpComment.Controls.Add(this.txtCommentDate);
            this.grpComment.Name = "grpComment";
            this.grpComment.TabStop = false;
            // 
            // txtCommentRangeTo
            // 
            resources.ApplyResources(this.txtCommentRangeTo, "txtCommentRangeTo");
            this.txtCommentRangeTo.Name = "txtCommentRangeTo";
            // 
            // txtCommentRangeFrom
            // 
            resources.ApplyResources(this.txtCommentRangeFrom, "txtCommentRangeFrom");
            this.txtCommentRangeFrom.Name = "txtCommentRangeFrom";
            // 
            // lblFuer
            // 
            resources.ApplyResources(this.lblFuer, "lblFuer");
            this.lblFuer.Name = "lblFuer";
            // 
            // txtKommentarFuer
            // 
            resources.ApplyResources(this.txtKommentarFuer, "txtKommentarFuer");
            this.txtKommentarFuer.Name = "txtKommentarFuer";
            this.txtKommentarFuer.ProtectContents = false;
            // 
            // lblVon
            // 
            resources.ApplyResources(this.lblVon, "lblVon");
            this.lblVon.Name = "lblVon";
            // 
            // cbFunktionen
            // 
            resources.ApplyResources(this.cbFunktionen, "cbFunktionen");
            this.cbFunktionen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFunktionen.Name = "cbFunktionen";
            // 
            // lblFunktion
            // 
            resources.ApplyResources(this.lblFunktion, "lblFunktion");
            this.lblFunktion.Name = "lblFunktion";
            // 
            // txtVerfasstVon
            // 
            resources.ApplyResources(this.txtVerfasstVon, "txtVerfasstVon");
            this.txtVerfasstVon.Name = "txtVerfasstVon";
            this.txtVerfasstVon.ProtectContents = false;
            this.txtVerfasstVon.ReadOnly = true;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // lblDatumBis
            // 
            resources.ApplyResources(this.lblDatumBis, "lblDatumBis");
            this.lblDatumBis.Name = "lblDatumBis";
            // 
            // lblDatumVon
            // 
            resources.ApplyResources(this.lblDatumVon, "lblDatumVon");
            this.lblDatumVon.Name = "lblDatumVon";
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblInfo.Name = "lblInfo";
            // 
            // KommentarVerfassenView
            // 
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.grpComment);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Name = "KommentarVerfassenView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.KommentarVerfassenView_Load);
            this.Shown += new System.EventHandler(this.KommentarVerfassenView_Shown);
            this.grpComment.ResumeLayout(false);
            this.grpComment.PerformLayout();
            this.ResumeLayout(false);

        }

        void cmdOK_Click(object sender, EventArgs e)
        {
            base.OKClicked();
        }
		#endregion

        private void XableControls()
        {
            // Nur einer von beiden kann true sein, denn man kann
            // nur einen der beiden Seiten editieren.
            bool bEnableBy = false;
            bool bEnableOn = false;
            int status = ConvertToInt32(_comment["Status"]);

            if (status == KommentareView.KommentarStatusBearbeitung)
            {
                if (_bIsEditMode)
                {
                    // 31.01.2008 man darf solange editieren, bis der Status "Fertig" ist.
                    if (ConvertToInt32(_comment["ID_Chirurgen_Von"]) == BusinessLayer.CurrentUser_ID_Chirurgen)
                    {
                        // Kommentar
                        bEnableBy = true;
                    }

                    if (ConvertToInt32(_comment["ID_Chirurgen_Fuer"]) == BusinessLayer.CurrentUser_ID_Chirurgen)
                    {
                        // Stellungnahme
                        // Der angemeldete Benutzer ist derjenige, über den der Kommentar verfasst wurde,
                        // dann darf dieser rechts "antworten"
                        bEnableOn = true;
                    }
                }
                else
                {
                    // Neuer Kommentar: es darf nur links der Kommentar editiert werden, 
                    // und das von jedem Administrator
                    if (ConvertToInt32(_comment["ID_Chirurgen_Von"]) == BusinessLayer.CurrentUser_ID_Chirurgen)
                    {
                        bEnableBy = true;
                    }
                }
            }

            // Beim Editieren darf man solagen ändern, bis Status="Fertig"

            // Nur der Weiterbilder, der den Kommentar angelegt hat, darf ihn auch ändern.
            txtCommentRangeTo.Enabled =
            txtCommentRangeFrom.Enabled =
            cbFunktionen.Enabled =
            cbChirurgen.Enabled =
            txtKommentarVon.Enabled =
                bEnableBy;
            
            txtKommentarFuer.Enabled = bEnableOn;

            cbChirurgen.Enabled = (status == KommentareView.KommentarStatusBearbeitung) ? false : true;
        }

        private void SetInfoText()
        {
            string info = string.Format(CultureInfo.InvariantCulture, GetText("info"), Command_ChirurgenFunktionenView);
            SetInfoText(lblInfo, info);
        }

        private void KommentarVerfassenView_Load(object sender, System.EventArgs e)
        {
            _bIgnoreControlEvents = true;

            lblInfo.ForeColor = BusinessLayer.InfoColor;

            if (this._bIsEditMode)
            {
                this.Text = AppTitle(GetText("titleEdit"));
            }
            else
            {
                this.Text = AppTitle(GetText("titleNew"));
                txtCommentDate.Text = Tools.DBNullableDateTime2DateTimeString(DateTime.Now);
            }

            PopulateChirurgen(cbChirurgen);
            PopulateFunktionen();

            Object2Control();

            SetInfoText();

            XableControls();

            _bIgnoreControlEvents = false;

            SetLabelTexts();
        }

        private void cmdCancel_Click(object sender, System.EventArgs e)
        {
            base.CancelClicked();
            Close();
        }

        protected override bool ValidateInput()
        {
            bool bSuccess = true;
            string strMessage = EINGABEFEHLER;

            if (cbChirurgen.SelectedValue == null)
            {
                bSuccess = false;
                strMessage += GetTextControlNeedsSelection(lblChirurgen);
            }
            if (cbFunktionen.SelectedValue == null)
            {
                bSuccess = false;
                strMessage += GetTextControlNeedsSelection(lblFunktion);
            }
            if (!Tools.DateIsValidGermanDate(txtCommentRangeFrom.Text))
            {
                bSuccess = false;
                strMessage += GetTextControlInvalidDate(lblDatumVon);
            }
            if (!Tools.DateIsValidGermanDate(txtCommentRangeTo.Text))
            {
                bSuccess = false;
                strMessage += GetTextControlInvalidDate(lblDatumBis);
            }

            if (txtKommentarVon.Text.Length == 0)
            {
                //Mindestens der erste Kommentar muss etwas enthalten
                bSuccess = false;
                strMessage += GetTextControlMissingText(lblVon);
            }

            if (!bSuccess)
            {
                MessageBox(strMessage);
            }

            return bSuccess;
        }

        private void KommentarVerfassenView_Shown(object sender, EventArgs e)
        {
            txtCommentRangeFrom.Focus();
        }

        private void SetLabelTexts()
        {
            if (!_bIgnoreControlEvents)
            {
                lblVon.Text = GetText("einschaetzungVon") + " " + txtVerfasstVon.Text + " " + GetText("ueber") + " " + cbChirurgen.Text + ":";
                lblFuer.Text = GetText("stellungnahmeVon") + " " + cbChirurgen.Text + ":";
            }
        }

        private void cbChirurgen_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetLabelTexts();
        }

        private void cmdVon_Click(object sender, EventArgs e)
        {
            Windows.Forms.CalendarPickerView dlg = new Windows.Forms.CalendarPickerView(txtCommentRangeFrom.Text);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                this.txtCommentRangeFrom.Text = Tools.DBNullableDateTime2DateString(dlg.SelectedDate);
            }
        }

        private void cmdBis_Click(object sender, EventArgs e)
        {
            Windows.Forms.CalendarPickerView dlg = new Windows.Forms.CalendarPickerView(txtCommentRangeTo.Text);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                this.txtCommentRangeTo.Text = Tools.DBNullableDateTime2DateString(dlg.SelectedDate);
            }
        }
	}
}
