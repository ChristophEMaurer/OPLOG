using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices; 

using Windows.Forms;
using Utility;
using CMaurer.Operationen.AppFramework;
using Operationen.Visitors;

namespace Operationen
{
    /// <summary>
    /// Basisklassen von allen Views. 
    /// </summary>
    public class OperationenForm : DatabaseForm
    {
        public delegate void ProgressCallbackPercentTextDelegate(int percent, string text);

        /// <summary>
        /// Use this form identifier for common texts
        /// </summary>
        public const string FormName = "OperationenForm";

        protected System.Windows.Forms.HelpProvider helpProvider;

        protected const int ControlYOffset = 6;
        protected const int HelpButtonYOffset = 60;

        public const int ModePrintForm = 0x01;
        public const int ModePrintListViewBrowser = 0x02;
        public const int ModePrintListViewBrowserSelectedColumns = 0x04;
        public const int ModePrintListViewBrowserBdc = 0x08;

        //
        // TextKonstanten
        //
        // Diese waren vorher nicht static, also in jedem Fenster!
        // jetzt werden sie einmal erzeugt nachdem das Menu erzeugt wird.
        //
        public static string Command_AboutBox;
        public static string Command_AbteilungenView;
        public static string Command_AbteilungenChirurgenView;
        public static string Command_AkademischeAusbildungView;
        public static string Command_AkademischeAusbildungTypenView;
        public static string Command_BaekWbOrdnung;
        public static string Command_BaekWbRichtlinien;
        public static string Command_ChirurgDokumenteView;
        public static string Command_ChirurgDelete;
        public static string Command_ChirurgEdit;
        public static string Command_ChirurgNew;
        public static string Command_ChirurgOperationenView;
        public static string Command_CopyWWWProgramUpdateFilesView;
        public static string Command_ChirurgenFunktionenView;
        public static string Command_ChirurgenRichtlinienView;
        public static string Command_DateienView;
        public static string Command_DateiTypenView;
        public static string Command_DokumenteView;
        public static string Command_ImportChirurgenExcludeView;
        public static string Command_InstallLicenseWizard;
        public static string Command_ImportChirurgWizard;
        public static string Command_ImportOPSWizard;
        public static string Command_ImportOperationenMobileWizard;
        public static string Command_ImportRichtlinienWizard;
        public static string Command_ImportRichtlinienZuordnungWizard;
        public static string Command_KommentareView;
        public static string Command_EigeneDateien;
        public static string Command_ExecAutoImport;
        public static string Command_ExportChirurgWizard;
        public static string Command_ExportOperationenKatalogView;
        public static string Command_ExportRichtlinienWizard;
        public static string Command_ExportRichtlinienZuordnungWizard;
        public static string Command_GebieteView;
        public static string Command_GesamtOperationenView;
        public static string Command_HelpChm;
        public static string Command_KlinischeErgebnisseView;
        public static string Command_LogView;
        public static string Command_NotizenView;
        public static string Command_NotizTypenView;
        public static string Command_OPDauerFortschrittView;
        public static string Command_OperationenKatalogView;
        public static string Command_OperationenSummaryView;
        public static string Command_OperationenVergleichView;
        public static string Command_OperationenView;
        public static string Command_OperationenEditView;
        public static string Command_OperationenImportView;
        public static string Command_OperationenZeitenVergleichView;
        public static string Command_OptionsView;
        public static string Command_OptionsView_tabUpdate;
        public static string Command_OptionsView_tabImport;
        public static string Command_PlanOperationenView;
        public static string Command_PlanOperationVergleichView;
        public static string Command_PrintIstOperationen;
        public static string Command_Ribbon;
        public static string Command_RichtlinienOpsKodeUnassignedView;
        public static string Command_RichtlinienVergleichView;
        public static string Command_PlanOperationVergleichIstView;
        public static string Command_RichtlinienView;
        public static string Command_RichtlinienSollIstView;
        public static string Command_RichtlinienVergleichOverviewView;
        public static string Command_SerialNumbersView;
        public static string Command_SerialBuy;
        public static string Command_SecGroupsChirurgenView;
        public static string Command_SecGroupsSecRightsView;
        public static string Command_SecGroupsView;
        public static string Command_SecUserOverviewView;
        public static string Command_UpdateFromFolder;
        public static string Command_UpdateFromWww;
        public static string Command_UserChangePasswordView;
        public static string Command_UserSetPasswordView;
        public static string Command_WeiterbilderChirurgenView;
        public static string Command_WwwHelp;
        public static string Command_WwwHome;

        protected static string EINGABEFEHLER;

        protected PrintDocument _pd;
        protected Font _printFont;
        protected int _dataRowIndex;
        protected int _currentPageIndex;

        protected bool _bIgnoreControlEvents;
        protected bool _showWaitCursorDuringPopulate;

        /// <summary>
        /// Derived windows can use this to abort a lenghty action
        /// </summary>
        private bool _abort = false;

        private bool _mayFormClose = true;

        // Balkengrafik
        protected Brush _balkenHintergrund = Brushes.LightBlue;
        protected Brush _balkenIst = Brushes.Blue;
        protected PictureBox pbHelp;
        protected Brush _balkenText = Brushes.White;

        protected OperationenForm()
        {

        }

        protected OperationenForm(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(OperationenForm_KeyDown);
        }

        protected void CreateCommandTextVariables()
        {
            Command_AboutBox = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdAboutBox);
            Command_AbteilungenView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdAbteilungenView);
            Command_AbteilungenChirurgenView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdAbteilungenChirurgenView);
            Command_AkademischeAusbildungView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdAkademischeAusbildungView);
            Command_AkademischeAusbildungTypenView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdAkademischeAusbildungTypenView);
            Command_BaekWbOrdnung = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdBaekWbOrdnung);
            Command_BaekWbRichtlinien = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdBaekWbRichtlinien);

            Command_ChirurgenFunktionenView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdChirurgenFunktionenView);
            Command_ChirurgenRichtlinienView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdChirurgenRichtlinienView);
            Command_ChirurgDokumenteView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdChirurgDokumenteView);
            Command_ChirurgDelete = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdChirurgDelete);
            Command_ChirurgEdit = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdChirurgEdit);
            Command_ChirurgNew = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdChirurgNew);
            Command_ChirurgOperationenView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdChirurgOperationenView);
            Command_CopyWWWProgramUpdateFilesView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdCopyWWWProgramUpdateFilesView);

            Command_DateiTypenView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdDateiTypenView);
            Command_DateienView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdDateienView);
            Command_DokumenteView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdDokumenteView);

            Command_ImportChirurgenExcludeView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdImportChirurgenExcludeView);
            Command_InstallLicenseWizard = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdInstallLicenseWizard);
            Command_ImportChirurgWizard = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdImportChirurgWizard);
            Command_ImportOPSWizard = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdImportOPSWizard);

            Command_EigeneDateien = MakeMenuPath(OperationenLogbuchView.TheMainWindow.ddEigeneDateien);
            Command_ExecAutoImport = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdExecAutoImport);
            Command_ExportChirurgWizard = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdExportChirurgWizard);
            Command_ExportOperationenKatalogView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdExportOperationenKatalogView);
            Command_ExportRichtlinienWizard = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdExportRichtlinienWizard);
            Command_ExportRichtlinienZuordnungWizard = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdExportRichtlinienZuordnungWizard);

            Command_GebieteView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdGebieteView);
            Command_GesamtOperationenView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdGesamtOperationenView);

            Command_HelpChm = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdHelpChm);

            Command_ImportChirurgWizard = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdImportChirurgWizard);
            Command_ImportOPSWizard = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdImportOPSWizard);
            Command_ImportOperationenMobileWizard = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdImportOperationenMobileWizard);
            Command_ImportRichtlinienWizard = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdImportRichtlinienWizard);
            Command_ImportRichtlinienZuordnungWizard = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdImportRichtlinienZuordnungWizard);

            Command_KommentareView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdKommentareView);
            Command_KlinischeErgebnisseView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdKlinischeErgebnisseView);

            Command_LogView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdLogView);

            Command_NotizenView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdNotizenView);
            Command_NotizTypenView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdNotizTypenView);

            Command_OPDauerFortschrittView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOPDauerFortschrittView);
            Command_OperationenView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOperationenView);
            Command_OperationenEditView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOperationenEditView);
            Command_OperationenImportView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOperationenImportView);
            Command_OperationenKatalogView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOperationenKatalogView);
            Command_OperationenSummaryView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOperationenSummaryView);
            Command_OperationenVergleichView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOperationenVergleichView);
            Command_OperationenView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOperationenView);
            Command_OperationenZeitenVergleichView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOperationenZeitenVergleichView);

            Command_OptionsView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOptionsView);
            Command_OptionsView_tabUpdate = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOptionsView, GetText("OptionsView", "tab_program"));
            Command_OptionsView_tabImport = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOptionsView, GetText("OptionsView", "tab_auto_import"));

            Command_PlanOperationenView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdPlanOperationenView);
            Command_PlanOperationVergleichIstView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdPlanOperationVergleichIstView);
            Command_PlanOperationVergleichView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdPlanOperationVergleichView);

            Command_Ribbon = GetText("ribbonApplicationButton");
            Command_RichtlinienOpsKodeUnassignedView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdRichtlinienOpsKodeUnassignedView);
            Command_RichtlinienView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdRichtlinienView);
            Command_RichtlinienSollIstView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdRichtlinienSollIstView);
            Command_RichtlinienVergleichOverviewView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdRichtlinienVergleichOverviewView);
            Command_RichtlinienVergleichView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdRichtlinienVergleichView);

            Command_SerialNumbersView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdSerialNumbersView);
            Command_SerialBuy = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdSerialBuy);
            Command_SecGroupsChirurgenView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdSecGroupsChirurgenView);
            Command_SecGroupsSecRightsView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdSecGroupsSecRightsView);
            Command_SecGroupsView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdSecGroupsView);
            Command_SecUserOverviewView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdSecUserOverviewView);

            Command_UpdateFromFolder = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdUpdateFromFolder);
            Command_UpdateFromWww = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdUpdateFromWww);
            Command_UserChangePasswordView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdUserChangePasswordView);
            Command_UserSetPasswordView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdUserSetPasswordView);

            Command_WeiterbilderChirurgenView = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdWeiterbilderChirurgenView);
            Command_WwwHelp = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdWwwHelp);
            Command_WwwHome = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdWwwHome);

            EINGABEFEHLER = GetText(FormName, "hinweis");
        }

        public static string MakeMenuPath(System.Windows.Forms.Control item)
        {
            StringBuilder sb = new StringBuilder(item.Text);
            Control parent = item.Parent;

            while (parent != null && !string.IsNullOrEmpty(parent.Text))
            {
                sb.Insert(0, " > ");
                sb.Insert(0, parent.Text);
                parent = parent.Parent;
            }

            sb.Replace("...", "");

            return sb.ToString();
        }

        public static string MakeMenuPath(Elegant.Ui.Control item, string text)
        {
            string path = MakeMenuPath(item) + " > " + text;

            return path;
        }

        public static string MakeMenuPath(Elegant.Ui.Control item)
        {
            StringBuilder sb = new StringBuilder(item.Text);
            Control parent = item.Parent;

            while (parent != null && !string.IsNullOrEmpty(parent.Text))
            {
                sb.Insert(0, " > ");
                sb.Insert(0, parent.Text);
                parent = parent.Parent;
            }

            sb.Replace("...", "");

            return sb.ToString();
        }

        public static string MakeMenuPath(Elegant.Ui.RibbonGroup item)
        {
            StringBuilder sb = new StringBuilder(item.Text);
            Control parent = item.Parent;

            while (parent != null && !string.IsNullOrEmpty(parent.Text))
            {
                sb.Insert(0, " > ");
                sb.Insert(0, parent.Text);
                parent = parent.Parent;
            }

            sb.Replace("...", "");

            return sb.ToString();
        }

        protected bool Abort
        {
            get { return _abort; }
            set { _abort = value; }
        }

        protected override string GetFormNameForResourceTexts()
        {
            return this.Name;
        }

        protected string GetTextControlMissingFile(string fileName)
        {
            return string.Format(CultureInfo.InvariantCulture, GetText(FormName, "err_missing_file"), fileName);
        }
        protected string GetTextControlMissingText(GroupBox c)
        {
            return string.Format(CultureInfo.InvariantCulture, GetText(FormName, "missingText"), c.Text);
        }
        protected string GetTextControlMissingText(Label c)
        {
            return string.Format(CultureInfo.InvariantCulture, GetText(FormName, "missingText"), c.Text);
        }
        protected string GetTextControlNeedsSelection(Label c)
        {
            return string.Format(CultureInfo.InvariantCulture, GetText(FormName, "needsSelection"), c.Text);
        }
        protected string GetTextControlNeedsContents(Label c)
        {
            return string.Format(CultureInfo.InvariantCulture, GetText(FormName, "needsContents"), c.Text);
        }
        protected string GetTextControlNeedsSelection(GroupBox grp)
        {
            return string.Format(CultureInfo.InvariantCulture, GetText(FormName, "needsSelection_grp"), grp.Text);
        }
        protected string GetTextItemNeedsSelection(string text)
        {
            return string.Format(CultureInfo.InvariantCulture, GetText(FormName, "needsSelectionItem"), text);
        }
        protected string GetTextControlInvalidDate(Label c)
        {
            return string.Format(CultureInfo.InvariantCulture, GetText(FormName, "invalidDate"), c.Text);
        }
        protected string GetTextControlInvalidTime(Label c)
        {
            return string.Format(CultureInfo.InvariantCulture, GetText(FormName, "invalidTime"), c.Text);
        }
        protected string GetTextControlInvalid(Control control)
        {
            return string.Format(CultureInfo.InvariantCulture, GetText(FormName, "invalid"), control.Text);
        }
        protected string GetTextControlInvalid(string controlText)
        {
            return string.Format(CultureInfo.InvariantCulture, GetText(FormName, "invalid"), controlText);
        }
        protected string GetTextControlInvalidContents(string labelText, string content)
        {
            return string.Format(CultureInfo.InvariantCulture, GetText(FormName, "invalidContent"), labelText, content);
        }
        protected string GetTextFileCouldNotBeDeleted(string fileName)
        {
            return string.Format(CultureInfo.InvariantCulture, GetText(FormName, "errDeleteFile"), fileName);
        }
        protected string GetTextFileCouldNotBeFound(string fileName)
        {
            return string.Format(CultureInfo.InvariantCulture, GetText(FormName, "errFileNotFound"), fileName);
        }
        protected string GetTextFileAlreadyExists(string fileName)
        {
            return string.Format(CultureInfo.InvariantCulture, GetText(FormName, "errFileExists"), fileName);
        }
        protected string GetTextFileCouldNotBeCopied(string src, string dst)
        {
            return string.Format(CultureInfo.InvariantCulture, GetText(FormName, "errFileCopySrcDst"), src, dst);
        }

        protected string GetTextSelectionNone()
        {
            return GetText(FormName, "noSelection");
        }

        protected string GetTextConfirmDelete(int count)
        {
            string msg;

            if (count == 1)
            {
                msg = GetText(FormName, "confirmDeleteSingle");
            }
            else
            {
                msg = string.Format(GetText(FormName, "confirmDeleteMultiple"), count);
            }

            return msg;
        }
        protected string GetTextConfirmDeleteSimple(int count)
        {
            string msg;

            if (count == 1)
            {
                msg = GetText(FormName, "confirmDeleteSimpleSingle");
            }
            else
            {
                msg = string.Format(GetText(FormName, "confirmDeleteSimpleMultiple"), count);
            }

            return msg;
        }

        protected override void OnLoad(EventArgs e)
        {
            this.helpProvider = new System.Windows.Forms.HelpProvider();
            this.helpProvider.HelpNamespace = "operationen.chm";
            this.helpProvider.SetHelpKeyword(this, "html\\" + GetFormNameForResourceTexts() + ".html");
            this.helpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
            this.helpProvider.SetShowHelp(this, true);


            if (_showWaitCursorDuringPopulate)
            {
                Cursor = Cursors.WaitCursor;
                Application.DoEvents();
            }

            // base.OnLoad() calls the formName_Load(object sender, System.EventArgs e) delegate
            base.OnLoad(e);

            SetupGuiDebugPrint();

            if (_showWaitCursorDuringPopulate)
            {
                Cursor = Cursors.Default;
                Application.DoEvents();
            }
        }

        protected virtual int GetHelpButtonPositionY()
        {
            return this.Height - HelpButtonYOffset;
        }

        protected virtual bool DisplayHelpButton()
        {
            return true;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (DisplayHelpButton())
            {
                if (pbHelp != null)
                {
                    this.helpProvider.SetHelpKeyword(pbHelp, "html\\" + GetFormNameForResourceTexts() + ".html");
                    this.helpProvider.SetHelpNavigator(pbHelp, System.Windows.Forms.HelpNavigator.Topic);
                    this.helpProvider.SetShowHelp(pbHelp, true);

                    pbHelp.Location = new Point(10, GetHelpButtonPositionY());
                }
            }
            else
            {
                pbHelp.Visible = false;
            }
        }

        protected BusinessLayer BusinessLayer
        {
            get { return (BusinessLayer)_businessLayerBase; }
        }

        protected bool ShowWaitCursorDuringPopulate
        {
            set { _showWaitCursorDuringPopulate = value; }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OperationenForm));
            this.pbHelp = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbHelp)).BeginInit();
            this.SuspendLayout();
            // 
            // pbHelp
            // 
            this.pbHelp.Cursor = System.Windows.Forms.Cursors.Help;
            this.pbHelp.Image = ((System.Drawing.Image)(resources.GetObject("pbHelp.Image")));
            this.pbHelp.Location = new System.Drawing.Point(10, 234);
            this.pbHelp.Name = "pbHelp";
            this.pbHelp.Size = new System.Drawing.Size(16, 16);
            this.pbHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbHelp.TabIndex = 0;
            this.pbHelp.TabStop = false;
            this.pbHelp.Click += new System.EventHandler(this.pbHelp_Click);
            // 
            // OperationenForm
            // 
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(373, 261);
            this.Controls.Add(this.pbHelp);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OperationenForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.OperationenForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OperationenForm_FormClosing);
            this.Resize += new System.EventHandler(this.OperationenForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbHelp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void pbHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "operationen.chm", "html\\" + GetFormNameForResourceTexts() + ".html");
        }

        protected void AddRichtzahl(ListViewItem lvi, string strRichtzahl)
        {
            lvi.SubItems.Add(BusinessLayer.FormatRichtzahl(strRichtzahl));
        }

        protected void AddRichtlinie(ListViewItem lvi, string s, bool bIsTooltip)
        {
            if (bIsTooltip)
            {
                lvi.ToolTipText = s.Replace("\r\n", "\n");
            }
            s = s.Replace("\r\n", " ");
            lvi.SubItems.Add(s);
        }

        protected void launchFileDirect(string strFilename)
        {
            launchFileDirect(strFilename, true);
        }

        /// <summary>
        /// Launch a file displaying an error if it is missing or an error occurs when launching the file.
        /// </summary>
        /// <param name="strFilename"></param>
        protected void launchFileDirect(string strFilename, bool showError)
        {
            Process oProcess = null;
            try
            {
                if (!File.Exists(strFilename))
                {
                    MessageBox(string.Format(GetText(FormName, "missingFile"), strFilename));
                }
                else
                {
                    oProcess = Process.Start(strFilename);
                }
            }
            catch
            {
                if (showError)
                {
                    MessageBox(string.Format(GetText(FormName, "launchFileError"), strFilename));
                }
            }
        }

        /// <summary>
        /// Launch a file which is in the BusinessLayer.PathDokumente folder
        /// </summary>
        /// <param name="strFilename"></param>
        protected void launchFile(string strFilename)
        {
            string strPath = strFilename;

            strPath = BusinessLayer.PathDokumente + System.IO.Path.DirectorySeparatorChar + strFilename;
            launchFileDirect(strPath);
        }

        /// <summary>
        /// Pop up a window to select one person and return that person as a datarow
        /// </summary>
        /// <returns></returns>
        protected DataRow SelectChirurgDataRow()
        {
            DataRow dataRow = null;

            int nID_Chirurg = SelectChirurg(true);

            if (nID_Chirurg != -1)
            {
                dataRow = this.BusinessLayer.GetChirurg(nID_Chirurg);
            }

            return dataRow;
        }

        protected void PopulateChirurgen(OplListView lv)
        {
            PopulateChirurgen(lv, null, false, false, true);
        }

        protected void PopulateChirurgen(OplListView lv, bool includeUserId)
        {
            PopulateChirurgen(lv, null, false, includeUserId, true);
        }

        protected void PopulateChirurgen(OplListView lv, bool includeUserId, bool active)
        {
            PopulateChirurgen(lv, null, false, includeUserId, active);
        }

        protected void PopulateChirurgen(OplListView lv, DataView dv, bool multiSelect, bool includeUserId, bool active)
        {
            DataView dataview = dv;

            if (dataview == null)
            {
                if (active)
                {
                    dataview = BusinessLayer.GetAktiveChirurgen();
                }
                else
                {
                    dataview = BusinessLayer.GetInaktiveChirurgen();
                }
            }

            lv.Clear();

            DefaultListViewProperties(lv);
            lv.MultiSelect = multiSelect;

            lv.Columns.Add(GetText(FormName, "anrede"), 60, HorizontalAlignment.Left);
            lv.Columns.Add(GetText(FormName, "nachname"), 120, HorizontalAlignment.Left);
            if (includeUserId)
            {
                lv.Columns.Add(GetText(FormName, "vorname"), 120, HorizontalAlignment.Left);
                lv.Columns.Add(GetText(FormName, "anmeldename"), -2, HorizontalAlignment.Left);
            }
            else
            {
                lv.Columns.Add(GetText(FormName, "vorname"), -2, HorizontalAlignment.Left);
            }

            foreach (DataRow dataRow in dataview.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem((string)dataRow["Anrede"]);
                lvi.Tag = ConvertToInt32(dataRow["ID_Chirurgen"]);
                lvi.SubItems.Add((string)dataRow["Nachname"]);
                lvi.SubItems.Add((string)dataRow["Vorname"]);

                if (includeUserId)
                {
                    lvi.SubItems.Add((string)dataRow["UserID"]);
                }

                lv.Items.Add(lvi);
            }
        }

        /// <summary>
        /// Fill the combobox and preselect the currently logged-on user
        /// </summary>
        /// <param name="cb"></param>
        protected void PopulateChirurgen(ComboBox cb)
        {
            DataView dv = BusinessLayer.GetChirurgen();

            cb.ValueMember = "ID_Chirurgen";
            cb.DisplayMember = "DisplayText";
            cb.DataSource = dv;

            int index = -1;

            // Der angemeldete Chirurg muss genau bestimmt werden!
            // wegen aufruf aus Fenster Einzelübersicht.
            for (int i = 0; i < cb.Items.Count; i++)
            {
                DataRow row = ((DataRowView)cb.Items[i]).Row;
                if (ConvertToInt32(row["ID_Chirurgen"]) == BusinessLayer.CurrentUser_ID_Chirurgen)
                {
                    index = i;
                    break;
                }
            }
            if (index != -1)
            {
                cb.SelectedIndex = index;
            }
        }
        /// <summary>
        /// Fill the combobox and preselect the specified user
        /// </summary>
        /// <param name="cb"></param>
        protected void PopulateChirurgen(ComboBox cb, int ID_Chirurgen)
        {
            DataView dv = BusinessLayer.GetChirurgen();

            cb.ValueMember = "ID_Chirurgen";
            cb.DisplayMember = "DisplayText";
            cb.DataSource = dv;

            int index = -1;

            // Der angemeldete Chirurg muss genau bestimmt werden!
            // wegen aufruf aus Fenster Einzelübersicht.
            for (int i = 0; i < cb.Items.Count; i++)
            {
                DataRow row = ((DataRowView)cb.Items[i]).Row;
                if (ConvertToInt32(row["ID_Chirurgen"]) == ID_Chirurgen)
                {
                    index = i;
                    break;
                }
            }
            if (index != -1)
            {
                cb.SelectedIndex = index;
            }
        }

        protected void PopulateKlinischeErgebnisseTypen(ComboBox cb)
        {
            PopulateKlinischeErgebnisseTypen(cb, false);
        }

        protected void PopulateKlinischeErgebnisseTypen(ComboBox cb, bool includeAll)
        {
            DataView dv = BusinessLayer.GetKlinischeErgebnisseTypen(includeAll);

            cb.ValueMember = "ID_KlinischeErgebnisseTypen";
            cb.DisplayMember = "Text";
            cb.DataSource = dv;
            cb.SelectedValue = BusinessLayer.KlinischeErgebnisseTypenUnauffaellig;
        }

        protected virtual void ProgressBegin()
        {
        }
        protected virtual void ProgressEnd()
        {
        }

        internal bool MayFormClose
        {
            get {return _mayFormClose; }
            set {_mayFormClose = value; }
        }

        /// <summary>
        /// Pop up a window that allows to select one person
        /// </summary>
        /// <param name="activeOnly">Specifiy active only or all</param>
        /// <returns></returns>
        protected int SelectChirurg(bool activeOnly)
        {
            return SelectChirurg(null, activeOnly, null);
        }

        /// <summary>
        /// Default is single selection
        /// </summary>
        /// <param name="dv"></param>
        /// <param name="activeOnly"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        protected int SelectChirurg(DataView dv, bool activeOnly, string info)
        {
            int ID_Chirurgen = -1;
            List<int> ID_ChirurgenList;

            ChirurgenView dlg = new ChirurgenView(BusinessLayer, dv, false, info);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                ID_ChirurgenList = dlg.ID_ChirurgenList;
                if (ID_ChirurgenList != null && ID_ChirurgenList.Count > 0)
                {
                    ID_Chirurgen = ID_ChirurgenList[0];
                }
            }

            return ID_Chirurgen;
        }

        protected List<int> SelectChirurgMulti(DataView dv, bool activeOnly, string info)
        {
            List<int> ID_ChirurgenList = null;

            ChirurgenView dlg = new ChirurgenView(BusinessLayer, dv, true, info);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                ID_ChirurgenList = dlg.ID_ChirurgenList;
            }

            //
            // Always return a list which may be empty to avoid
            // having to check for null at each call to this function
            //
            if (ID_ChirurgenList == null)
            {
                ID_ChirurgenList = new List<int>();
            }
                
            return ID_ChirurgenList;
        }

        protected void PopulateOperationen(GroupBox groupBox, OplListView lvOperationen, 
            string filterKode, string filterText)
        {
            // TODO: Watermark wird hier entfernt

            bool readData = true;

            long count = BusinessLayer.GetOperationenCount(filterKode, filterText);

            if (count > 5000)
            {
                if (!Confirm(string.Format(GetText(FormName, "confirmData"), count)))
                {
                    readData = false;
                }
            }
            if (readData)
            {
                _mayFormClose = false;

                Cursor = Cursors.WaitCursor;
                ProgressBegin();

                DefaultListViewProperties(lvOperationen);
                Application.DoEvents();
                BusinessLayer.GetOperationen(lvOperationen, filterKode, filterText);
                SetGroupBoxText(lvOperationen, groupBox, GetText(FormName, "OPSKatalog"));

                ProgressEnd();
                Cursor = Cursors.Default;

                Application.DoEvents();

                _mayFormClose = true;
            }
        }

        protected void PopulateOperationen(GroupBox groupBox, OplListView lvOperationen, string filterKodeText)
        {
            bool readData = true;

            long count = BusinessLayer.GetOperationenCount(filterKodeText);

            if (count > 5000)
            {
                if (!Confirm(string.Format(GetText(FormName, "confirmData"), count)))
                {
                    readData = false;
                }
            }
            if (readData)
            {
                _mayFormClose = false;

                Cursor = Cursors.WaitCursor;
                ProgressBegin();

                DefaultListViewProperties(lvOperationen);
                Application.DoEvents();
                BusinessLayer.GetOperationen(lvOperationen, filterKodeText);
                SetGroupBoxText(lvOperationen, groupBox, GetText(FormName, "OPSKatalog"));

                ProgressEnd();
                Cursor = Cursors.Default;

                Application.DoEvents();

                _mayFormClose = true;
            }
        }

        public static void SetGroupBoxText(OplListView listView, GroupBox groupBox, string prefix, string entries)
        {
            groupBox.Text = string.Format(CultureInfo.InvariantCulture, "{0} ({1} {2})", prefix, listView.Items.Count, entries);
        }

        /// <summary>
        /// Set the group box text as specified and append the number of items like this:
        /// "MyGroup (1 Eintrag)"
        /// or
        /// "MyGroup (2 Einträge")
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="grpBox"></param>
        /// <param name="prefix"></param>
        protected void SetGroupBoxText(OplListView lv, GroupBox grpBox, string prefix)
        {
            if (lv.Items.Count == 1)
            {
                SetGroupBoxText(lv, grpBox, prefix, GetText(FormName, "entry"));
            }
            else
            {
                //
                // 0 oder > 1
                //
                SetGroupBoxText(lv, grpBox, prefix, GetText(FormName, "entries"));
            }
        }

        protected void ClearItemsAndSetGroupBoxText(OplListView lv, GroupBox grpBox, string prefix)
        {
            lv.Items.Clear();
            SetGroupBoxText(lv, grpBox, prefix, GetText(FormName, "entries"));
        }

        protected bool DownloadFile(string windowTitle, int fileSizeKb, string url, string localFilename)
        {
            bool fileDownloaded = false;

            DownloadFileView dlg = new DownloadFileView(BusinessLayer,
                windowTitle,
                fileSizeKb,
                url,
                localFilename);

            if (DialogResult.OK == dlg.ShowDialog())
            {
                fileDownloaded = true;
            }

            return fileDownloaded;
        }

        /// <summary>
        /// This file often has a newline at the end which causes a hard to find error.
        /// 
        /// NOTE: It must NOT have a newlien at the end
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="showErrorMessage"></param>
        /// <param name="outFileName"></param>
        /// <param name="fileContents"></param>
        /// <returns></returns>
        protected bool DownloadAsciiInfoFile(string url, bool showErrorMessage, string outFileName, out string fileContents)
        {
            bool success = false;

            StringBuilder sb = new StringBuilder("");
            System.IO.Stream streamReader = null;

            try
            {
                // version.txt must be saved with Notepad in ANSI Format and must NOT have a newline at the end
                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);

                //
                // This call takes several seconds
                //
                BusinessLayer.SetWebProxy(webRequest);

                System.Net.HttpWebResponse webResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();

                streamReader = webResponse.GetResponseStream();

                int BUFFER_SIZE = 4096;

                byte[] buf = new byte[BUFFER_SIZE];
                int n = streamReader.Read(buf, 0, BUFFER_SIZE);
                while (n > 0)
                {
                    sb.Append(Encoding.ASCII.GetString(buf, 0, n));

                    n = streamReader.Read(buf, 0, BUFFER_SIZE);
                }
                success = true;
            }
            catch 
            {
                if (showErrorMessage)
                {
                    MessageBox(GetText(FormName, "err_nowwwconnection"));
                }
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                }
            }

            //
            // Soll wohl immer gesetzt werden
            //
            fileContents = sb.ToString();
            fileContents = fileContents.Trim();

            if (success && outFileName != null && outFileName.Length > 0)
            {
                StreamWriter writer = null;
                try
                {
                    writer = new StreamWriter(outFileName);
                    writer.Write(fileContents);
                }
                catch
                {
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                        writer = null;
                    }
                }
            }

            return success;
        }

        protected void SetInfoText(Label label)
        {
            SetInfoText(label, GetText("info"));
        }

        protected void SetInfoText(Label label, string text)
        {
            label.BorderStyle = BorderStyle.Fixed3D;
            label.ForeColor = BusinessLayer.InfoColor;
            label.Text = text;
        }

        protected void SetInfoText(TextBox txt, string text)
        {
            txt.BorderStyle = BorderStyle.Fixed3D;
            txt.ForeColor = BusinessLayer.InfoColor;
            txt.Text = text;
        }

        protected void SetInfoText(LinkLabel label, string text)
        {
            label.Links.Clear();
            label.BorderStyle = BorderStyle.Fixed3D;
            label.ForeColor = BusinessLayer.InfoColor;
            label.Text = text;
        }
        protected void AddLinkLabelLink(LinkLabel label, string text, string command)
        {
            int start = label.Text.IndexOf(text);
            if (start != -1)
            {
                label.Links.Add(start, text.Length, command);
            }
        }

        protected void SearchOPSKodeOrOPSText(OplListView listView, int columnOPSKode, string opsKode, int columnOPSText, string opsText)
        {
            bool found = false;
            int index = -1;

            if (opsKode.Length > 0 || opsText.Length > 0)
            {
                int beginIndex = 0;

                if (listView.SelectedIndices.Count > 0)
                {
                    if (listView.SelectedIndices[0] + 1 < listView.Items.Count)
                    {
                        beginIndex = listView.SelectedIndices[0] + 1;
                    }
                }
                for (int i = beginIndex; i < listView.Items.Count; i++)
                {
                    // OPSKode oder OPSBezeichnung suchen
                    if ((opsKode.Length > 0 && listView.Items[i].SubItems[columnOPSKode].Text.ToLower().IndexOf(opsKode.ToLower()) != -1)
                        ||
                        (opsText.Length > 0 && listView.Items[i].SubItems[columnOPSText].Text.ToLower().IndexOf(opsText.ToLower()) != -1))
                    {
                        found = true;
                        index = i;
                        break;
                    }
                }
                if (!found)
                {
                    for (int i = 0; i < beginIndex; i++)
                    {
                        // OPSKode oder OPSBezeichnung suchen
                        if ((opsKode.Length > 0 && listView.Items[i].SubItems[columnOPSKode].Text.ToLower().IndexOf(opsKode.ToLower()) != -1)
                            ||
                            (opsText.Length > 0 && listView.Items[i].SubItems[columnOPSText].Text.ToLower().IndexOf(opsText.ToLower()) != -1))
                        {
                            found = true;
                            index = i;
                            break;
                        }
                    }
                }
                if (found)
                {
                    ListViewItem lvi = listView.Items[index];
                    listView.SelectedIndices.Clear();
                    listView.SelectedIndices.Add(lvi.Index);
                    listView.EnsureVisible(lvi.Index);
                }
            }
        }

        protected bool ValidateDatumVonBis(Label lblVon, string von, Label lblBis, string bis)
        {
            return ValidateDatumVonBis(lblVon, von, lblBis, bis, true);
        }

        /// <summary>
        /// Check that both date fields have a valid input.
        /// </summary>
        /// <param name="lblVon"></param>
        /// <param name="von"></param>
        /// <param name="lblBis"></param>
        /// <param name="bis"></param>
        /// <param name="emptyAllowed">Specify whether an empty string is allowed</param>
        /// <returns></returns>
        protected bool ValidateDatumVonBis(Label lblVon, string von, Label lblBis, string bis, bool emptyAllowed)
        {
            bool bSuccess = true;
            string strMessage = EINGABEFEHLER;

            if (!emptyAllowed && von.Length == 0)
            {
                strMessage += GetTextControlMissingText(lblVon);
                bSuccess = false;
            }
            if (!emptyAllowed && bis.Length == 0)
            {
                strMessage += GetTextControlMissingText(lblBis);
                bSuccess = false;
            }
            if (von.Length > 0 && !Tools.DateIsValidGermanDate(von))
            {
                strMessage += GetTextControlInvalidDate(lblVon);
                bSuccess = false;
            }
            if (bis.Length > 0 && !Tools.DateIsValidGermanDate(bis))
            {
                strMessage += GetTextControlInvalidDate(lblBis);
                bSuccess = false;
            }

            if (!bSuccess)
            {
                MessageBox(strMessage);
            }

            return bSuccess;
        }

        protected bool ValidateDatumVonBis(Label lblVon, string von, Label lblBis, string bis, StringBuilder sb)
        {
            bool bSuccess = true;
            bool emptyAllowed = true;

            if (!emptyAllowed && von.Length == 0)
            {
                sb.Append(GetTextControlMissingText(lblVon));
                bSuccess = false;
            }
            if (!emptyAllowed && bis.Length == 0)
            {
                sb.Append(GetTextControlMissingText(lblBis));
                bSuccess = false;
            }
            if (von.Length > 0 && !Tools.DateIsValidGermanDate(von))
            {
                sb.Append(GetTextControlInvalidDate(lblVon));
                bSuccess = false;
            }
            if (bis.Length > 0 && !Tools.DateIsValidGermanDate(bis))
            {
                sb.Append(GetTextControlInvalidDate(lblBis));
                bSuccess = false;
            }

            return bSuccess;
        }

        protected bool ValidateIntRange(Label label, TextBox textBox, int from, int to, StringBuilder message)
        {
            bool success = true;

            if (string.IsNullOrEmpty(textBox.Text))
            {
                success = false;
                message.Append(GetTextControlMissingText(label));
            }
            else
            {
                int value;

                // Überprüfen, ob dieser eingegebene OPS-Kode überhaupt vorkommt: txtOpsKode
                if (Int32.TryParse(textBox.Text, out value))
                {
                    if (value < from || value > to)
                    {
                        success = false;
                        message.Append(string.Format(GetText("msg_range"), label.Text, from, to));
                    }
                }
            }

            return success;
        }

#region Printing
        virtual protected void PrintLine(PrintPageEventArgs ev, int nLine, int x, string strLine)
        {
            float yPos = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;

            yPos = topMargin + (nLine * _printFont.GetHeight(ev.Graphics));

            ev.Graphics.DrawString(strLine, _printFont, Brushes.Black,
                   leftMargin + x, yPos, new StringFormat());
        }
        virtual protected void PrintDocument_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {
            e.PageSettings.Landscape = true;
        }
        virtual protected void PrintDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            _currentPageIndex = 1;
            _dataRowIndex = 0;
        }
        virtual public void PrintForm()
        {
            _printFont = new Font("Courier New", 8);

            // Setup a PrintDocument
            _pd = new PrintDocument();
            _pd.BeginPrint += new PrintEventHandler(this.PrintDocument_BeginPrint);
            _pd.PrintPage += new PrintPageEventHandler(this.PrintDocument_PrintPage);
            _pd.QueryPageSettings += new QueryPageSettingsEventHandler(PrintDocument_QueryPageSettings);
            _pd.EndPrint += new PrintEventHandler(PrintDocument_EndPrint);

            try
            {
                PrintPreviewDialog ppd = new PrintPreviewDialog();
                ppd.Left = 0;
                ppd.Top = 0;
                ppd.Width = Screen.PrimaryScreen.WorkingArea.Width;
                ppd.Height = Screen.PrimaryScreen.WorkingArea.Height;
                ppd.Document = _pd;
                ppd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox(GetText(FormName, "err_noprinter") + "\n\n" + ex.ToString());
            }
        }
        virtual protected void PrintDocument_EndPrint(object sender, PrintEventArgs e)
        {
            _currentPageIndex = 1;
        }

        virtual protected void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Set the page margins
            Rectangle rPageMargins = new Rectangle(e.MarginBounds.Location, e.MarginBounds.Size);

            // Make sure nothing gets printed in the margins
            e.Graphics.SetClip(rPageMargins);

            PrintAll(e);
        }

        virtual protected void PrintAll(PrintPageEventArgs ev)
        {
        }

        protected void PrintLine(PrintPageEventArgs ev, int nLine, string strLine)
        {
            PrintLine(ev, nLine, 0, strLine);
        }


        #endregion

        protected int GetInternExternFlag(bool intern, bool ext)
        {
            return BusinessLayer.GetInternExternFlag(intern, ext);
        }

        protected void CheckQuelleDefaultSettings(CheckBox chkIntern, CheckBox chkExtern)
        {
            chkIntern.Checked = true;
            chkExtern.Checked = false;
        }


        protected void CheckQuelleCorrectDumbSettings(CheckBox chkIntern, CheckBox chkExtern)
        {
            if (!chkExtern.Checked && !chkIntern.Checked)
            {
                chkExtern.Checked = chkIntern.Checked = true;
            }
        }

        #region PrintListViewHTML
        protected string MakeSafeHTML(string s)
        {
            StringBuilder sb = new StringBuilder(s);

            // Das & muss zuerst ersetzt werden, weil es ja danach vielfach wieder eingefügt wird.
            sb.Replace("&", "&amp;");

            sb.Replace("ä", "&auml;");
            sb.Replace("Ä", "&Auml;");
            sb.Replace("ö", "&ouml;");
            sb.Replace("Ö", "&Ouml;");
            sb.Replace("ü", "&uuml;");
            sb.Replace("Ü", "&Uuml;");
            sb.Replace("ß", "&szlig;");
            sb.Replace(">", "&gt;");
            sb.Replace("<", "&lt;");
            sb.Replace("\"", "&quot;");

            return sb.ToString();
        }

        /// <summary>
        /// Ensure that there is a non breakable space if the data is empty.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected string HtmlEnsureNonBreakableSpace(string data)
        {
            string html;

            if (data.Trim().Length > 0)
            {
                html = data;
            }
            else
            {
                html = "&nbsp;";
            }

            return html;
        }


        protected string MakeHTML(string s)
        {
            return MakeHTML(s, true);
        }

        /// <summary>
        /// Create an HTML string: remove HTML-specific characters '&amp;', '&gt;' and '&lt;', and leading spaces are turned into non-breakable-spaces
        /// All other spaces remain because the text must be wrapped.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        protected string MakeHTML(string s, bool spaceToAmp)
        {
            StringBuilder sb = new StringBuilder(MakeSafeHTML(s));

            int i = 0;
            // pos is highest index of the last leading space
            int pos = -1;
            while (i < sb.Length)
            {
                if (sb[i] == ' ')
                {
                    pos = i;
                }
                else
                {
                    break;
                }

                i++;
            }

            if (pos != -1)
            {
                StringBuilder repl = new StringBuilder();
                for (i = 0; i <= pos; i++)
                {
                    repl.Append("&nbsp;");
                }

                sb.Remove(0, pos + 1);
                sb.Insert(0, repl);
            }

            if (spaceToAmp && sb.Length == 0)
            {
                sb.Append("&nbsp;");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Print the filter settings for this query.
        /// This function cannot be abstract, because not all classes have something to print.
        /// </summary>
        /// <returns></returns>
        protected virtual string PrintHTMLFilter()
        {
            return "";
        }

        protected string PrintHTMLFilter(string dateFrom, string dateTo, bool chkIntern, bool chkExtern, string funktion)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(GetDateVonBisHTMLFilterText(dateFrom, dateTo));
            sb.Append(GetQuelleInternExternText(chkIntern, chkExtern));
            sb.Append("<br/>");
            sb.Append(GetText(FormName, "funktion") + ": " + MakeSafeHTML(funktion));
            sb.Append("<br/>");

            return sb.ToString();
        }

        protected string PrintHTMLFilter(string dateFrom, string dateTo, bool chkIntern, bool chkExtern)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(GetDateVonBisHTMLFilterText(dateFrom, dateTo));
            sb.Append(GetQuelleInternExternText(chkIntern, chkExtern));

            return sb.ToString();
        }

        /// <summary>
        /// Return what this page prints.
        /// This function cannot be abstract, becuase not all classes have something to print.
        /// </summary>
        /// <returns></returns>
        protected virtual string PrintHTMLHeaderLine1()
        {
            return MakeSafeHTML(GetText("title"));
        }

        protected virtual string PrintHTMLSummary()
        {
            return "";
        }

        /// <summary>
        /// Return true if you don't want to print this column.
        /// Neither the column header not the contents will be printed.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        protected virtual bool PrintHTMLSkipColumn(int i)
        {
            return false;
        }

        protected void PrintHTMLLaunchFile(string fileName)
        {
            System.PlatformID platform;
            int major;
            int minor;

            Utility.Tools.GetOperationSystem(out platform, out major, out minor);

            if (platform == System.PlatformID.Win32NT && major == 6 && minor == 0)
            {
                // on Vista, firefox is launched and displays the file but an exception occurs all the same.
                launchFileDirect(fileName, false);
            }
            else
            {
                launchFileDirect(fileName);
            }
        }
        protected void PrintListView(OplListView lv)
        {
            PrintListView(lv, GlobalConstants.KeyPrintLinesDefaultString);
        }

        protected void PrintListViewSelectedColumns(OplListView lv, string key)
        {
            SelectListViewColumnsView dlg = new SelectListViewColumnsView(BusinessLayer, lv);
            DialogResult result = dlg.ShowDialog();

            if (result == DialogResult.OK)
            {
                List<int> columnList = dlg.ColumnList;
                PrintListView(lv, key, columnList);
            }
        }

        protected void PrintListView(OplListView lv, string key)
        {
            PrintListView(lv, key, null, -1, -1);
        }

        protected void PrintListView(OplListView lv, string key, List<int> columnList)
        {
            PrintListView(lv, key, columnList, -1, -1);
        }

        /// <summary>
        /// Create an HTML file to display  in an Internet Browser
        /// </summary>
        /// <param name="lv">The ListView to print</param>
        /// <param name="key">Number of lines after which to break a page</param>
        /// <param name="columnList">Columns to print</param>
        /// <param name="repeatRowIndexBegin">Repeat all rows with every header beginning with and including this 0-based row</param>
        /// <param name="repeatRowIndexEnd">Repeat all rows with every header endgin with and including this 0-based row</param>
        protected void PrintListView(OplListView lv, string key, List<int> columnList, int repeatRowIndexBegin, int repeatRowIndexEnd)
        {
            string temp;
            int i;
            StringBuilder sbHeader = new StringBuilder();
            StringBuilder sbRepeatRows = new StringBuilder();

            //
            // Kopftext mit Programmversion usw. und Filtereinstellungen
            //
            sbHeader.Append("<p><b>" + BusinessLayer.AppAndVersionStringForPrinting + ", " + GetTextPrintHeader(DateTime.Now));
            sbHeader.Append(Environment.NewLine);
            sbHeader.Append("<br/>" + PrintHTMLHeaderLine1());
            string s = PrintHTMLFilter();
            if (s.Length > 0)
            {
                sbHeader.Append(Environment.NewLine);
                sbHeader.Append("<br/>");
                sbHeader.Append(GetText(FormName, "filter"));
                sbHeader.Append(": </b>");
                sbHeader.Append(Environment.NewLine);
                sbHeader.Append("<br/>" + s);
            }
            s = PrintHTMLSummary();
            if (s.Length > 0)
            {
                sbHeader.Append(Environment.NewLine);
                sbHeader.Append("<br/>" + s);
            }
            sbHeader.Append(Environment.NewLine);
            sbHeader.Append("</p>");
            sbHeader.Append(Environment.NewLine);

            //
            // Anfang der Tabelle mit Spaltenüberschriften
            //
            sbHeader.Append("<div>");
            sbHeader.Append(Environment.NewLine);
            sbHeader.Append("<table width=\"100%\" border=\"1\">");
            sbHeader.Append(Environment.NewLine);
            sbHeader.Append(Environment.NewLine);
            sbHeader.Append("<tr>");
            for (i = 0; i < lv.Columns.Count; i++)
            {
                ColumnHeader col = (ColumnHeader)lv.Columns[i];

                if (PrintHTMLSkipColumn(i))
                {
                    continue;
                }

                if ((columnList != null) && !columnList.Contains(i))
                {
                    continue;
                }

                sbHeader.Append(Environment.NewLine);
                sbHeader.Append("<td><b>");
                sbHeader.Append(MakeHTML(col.Text));
                sbHeader.Append("</b></td>");
            }
            sbHeader.Append(Environment.NewLine);
            sbHeader.Append("</tr>");

            //
            // Diese Zeilen sollen auch immer nach den Spaltenüberschriften wiederholt werden
            //
            if ((repeatRowIndexBegin >= 0) && (repeatRowIndexBegin < lv.Items.Count)
                && (repeatRowIndexEnd >= 0) && (repeatRowIndexEnd < lv.Items.Count))
            {
                for (int row = repeatRowIndexBegin; row <= repeatRowIndexEnd; row++)
                {
                    ListViewItem lvi = lv.Items[row];

                    sbRepeatRows.Append(Environment.NewLine);
                    sbRepeatRows.Append("<tr>");
                    for (i = 0; i < lvi.SubItems.Count; i++)
                    {
                        if (PrintHTMLSkipColumn(i))
                        {
                            continue;
                        }
                        if ((columnList != null) && !columnList.Contains(i))
                        {
                            continue;
                        }

                        sbRepeatRows.Append(Environment.NewLine);
                        sbRepeatRows.Append("<td>" + PrintHTMLGetText(lvi, i) + "</td>");
                    }
                    sbRepeatRows.Append(Environment.NewLine);
                    sbRepeatRows.Append("</tr>");
                }
            }

            string folder = Tools.GetTempDirectoryName();
            string fileName = folder + Path.DirectorySeparatorChar + Path.GetRandomFileName() + ".html";

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("<html>");
                writer.WriteLine("<head>");
                writer.WriteLine(string.Format("<title>{0}</title>", AppTitle()));
                writer.WriteLine("<meta http-equiv=\"content-type\" content=\"text/html; charset=ISO-8859-1\">");
                writer.WriteLine("</head>");
                writer.WriteLine("<body style=\"font-family:arial; font-size:0.9em;\">");
                writer.Write("<p>");
                temp = MakeSafeHTML(string.Format(CultureInfo.InvariantCulture, GetText(FormName, "print_instructions"), Command_OptionsView));
                writer.Write(temp);
                writer.Write("</p>");
                writer.WriteLine(sbHeader.ToString());

                int lines = 0;
                int linesPerPage = GetPrintHTMLLinesPerPage(key);

                foreach (ListViewItem lvi in lv.Items)
                {
                    lines++;
                    if (lines > linesPerPage)
                    {
                        // Neue Seite anfangen
                        writer.WriteLine("");
                        writer.WriteLine("</table>");
                        writer.WriteLine("</div>");
                        writer.WriteLine("<p style=\"page-break-before:always\"></p>");
                        writer.WriteLine(sbHeader);
                        writer.WriteLine(sbRepeatRows);
                        lines = 1;
                    }

                    writer.WriteLine("");
                    writer.WriteLine("<tr>");
                    for (i = 0; i < lvi.SubItems.Count; i++)
                    {
                        if (PrintHTMLSkipColumn(i))
                        {
                            continue;
                        }
                        if ((columnList != null) && !columnList.Contains(i))
                        {
                            continue;
                        }

                        writer.WriteLine("<td>" + PrintHTMLGetText(lvi, i) + "</td>");
                    }
                    writer.WriteLine("</tr>");
                }
                writer.WriteLine("</table>");
                writer.WriteLine("</div>");
                writer.WriteLine("</body>");
                writer.WriteLine("</html>");
            }

            CopyBalkenImageToFolder(folder);
            PrintHTMLLaunchFile(fileName);
        }

        protected string MakeHtmlBalkenGrafik(string data)
        {
            double ist;
            double total;

            int prozent = ListViewBalken.ProzentFromBalkenGrafikData(data, out ist, out total);

            string s = string.Format("<IMG src=\"blue.jpg\" width=\"{0}\" height=\"10\" border=\"0\">", prozent * 2);

            return s;
        }

        /// <summary>
        /// Get the text of the column with index index of list view item lvi.
        /// Override this function only if you wan tto return something different from the text displayed, 
        /// such as when the column displays a graphical bar.
        /// </summary>
        /// <param name="lvi"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected virtual string PrintHTMLGetText(ListViewItem lvi, int index)
        {
            string s = lvi.SubItems[index].Text;
            if (s.Length == 0)
            {
                s = " ";
            }
            return MakeHTML(s);
        }
        private void CopyBalkenImageToFolder(string folder)
        {
            Stream inStream = null;
            BinaryWriter b = null;

            try
            {
                byte[] buffer = new byte[1000];
                int bytes = 0;

                // Achtung: Die Datei OperationenPublicKey.xml muss als embedded resource
                // dem Projekt hinzugefuegt werden, damit das hier klappt.

                Assembly assembly = Assembly.GetExecutingAssembly();
                inStream = assembly.GetManifestResourceStream("Operationen.Images.blue.jpg");
                bytes = inStream.Read(buffer, 0, 1000);
                b = new BinaryWriter(File.Open(folder + Path.DirectorySeparatorChar + "blue.jpg", FileMode.Create));
                b.Write(buffer, 0, bytes);
            }
            catch
            {
                // do nothing if this fails
            }
            finally
            {
                if (inStream != null)
                {
                    inStream.Close();
                    inStream.Dispose();
                    inStream = null;
                }
                if (b != null)
                {
                    b.Close();
                    b = null;
                }
            }
        }

        protected string GetDateVonBisHTMLFilterText(string von, string bis)
        {
            StringBuilder sb = new StringBuilder();

            if (von.Length > 0)
            {
                sb.Append(GetText(FormName, "von"));
                sb.Append(" ");
                sb.Append(von);
                sb.Append("<br/>");
            }
            if (bis.Length > 0)
            {
                sb.Append(GetText(FormName, "bis"));
                sb.Append(" ");
                sb.Append(bis);
                sb.Append("<br/>");
            }

            return sb.ToString();
        }

        protected string GetQuelleInternExternText(bool intern, bool xtern)
        {
            string s = "";

            if (intern && xtern)
            {
                s = GetText(FormName, "klinikInternUndExtern");
            }
            else if (intern)
            {
                s = GetText(FormName, "klinikIntern");
            }
            else if (xtern)
            {
                s = GetText(FormName, "klinikExtern");
            }
            
            return s;
        }
        private int GetPrintHTMLLinesPerPage(string key)
        {
            string strLinesPerPage = BusinessLayer.GetUserSettingsString(GlobalConstants.SectionPrint, key, GlobalConstants.KeyPrintLinesDefaultString);
            int linesPerPage = GlobalConstants.KeyPrintLinesDefault;
            Int32.TryParse(strLinesPerPage, out linesPerPage);

            return linesPerPage;
        }

        protected void LaunchInternetBrowser(string url)
        {
            System.Diagnostics.ProcessStartInfo si = new ProcessStartInfo();

            si.FileName = url;
            try
            {
                System.Diagnostics.Process.Start(si);
            }
            catch (Exception exception)
            {
                AppFramework.Debugging.DebugLogging.Write(AppFramework.Debugging.DebugLogging.DebugFlagInfo, exception);
            }
        }

    #endregion

        private void OperationenForm_Load(object sender, EventArgs e)
        {
        }

        protected void DrawPercentage(DrawListViewSubItemEventArgs e)
        {
            Rectangle rect = e.Bounds;
            Graphics g = e.Graphics;

            rect.Inflate(-1, -1);

            string[] arValues = e.SubItem.Text.Split('|');

            double ist = double.Parse(arValues[0]);
            double max = double.Parse(arValues[1]);

            if (max > 0 && ist <= max)
            {
                int totalWidth = rect.Width;
                e.Graphics.FillRectangle(_balkenHintergrund, rect);
                rect.Width = (int)((double)rect.Width / max * ist);
                e.Graphics.FillRectangle(_balkenIst, rect);
                int prozent = 0;

                prozent = Convert.ToInt32(ist * 100 / max);

                //if (prozent > 0)
                {
                    string s;
                    if (prozent >= 10)
                    {
                        s = string.Format("{0}%", prozent);
                    }
                    else
                    {
                        s = string.Format(" {0}%", prozent);
                    }
                    e.Graphics.DrawString(s, new Font("Courier New", 8), _balkenText,
                        rect.Left + totalWidth / 2, rect.Top - 2);
                }
            }
        }

        protected int ProzentFromBalkenGrafikData(string s)
        {
            return OplListView.ProzentFromBalkenGrafikData(s);
        }


        #region Richtlinien
        protected void InitRichtlinien(OplListView lv)
        {
            DefaultListViewProperties(lv);
            lv.MultiSelect = true;

            lv.Clear();
            lv.Columns.Add(GetText(FormName, "nr"), 30, HorizontalAlignment.Left);
            lv.Columns.Add(GetText(FormName, "richtzahl"), 80, HorizontalAlignment.Left);
            lv.Columns.Add(GetText(FormName, "richttext"), -2, HorizontalAlignment.Left);
        }

        protected bool RichtlinienGetRichtzahl(ListViewItem lvi, out int richtzahl)
        {
            richtzahl = 0;
            return Int32.TryParse(lvi.SubItems[1].Text, out richtzahl);
        }

        protected void PopulateRichtlinien(OplListView lv, int nID_Gebiete, GroupBox grpBox, string grpBoxText)
        {
            DataView dv = BusinessLayer.GetRichtlinien(nID_Gebiete);

            lv.Items.Clear();

            foreach (DataRow dataRow in dv.Table.Rows)
            {
                ListViewItem lvi = new ListViewItem(dataRow["LfdNummer"].ToString());
                lvi.Tag = ConvertToInt32(dataRow["ID_Richtlinien"]);

                AddRichtzahl(lvi, dataRow["Richtzahl"].ToString());
                AddRichtlinie(lvi, (string)dataRow["UntBehMethode"], false);

                lv.Items.Add(lvi);
            }

            SetGroupBoxText(lv, grpBox, grpBoxText);
        }
        #endregion

        protected DateTime? GetDateTimeFromTextBox(DateBoxPicker dbp)
        {
            return GetDateTimeFromTextBox(dbp.DateBox);
        }

        protected DateTime? GetDateTimeFromTextBox(TextBox txt)
        {
            DateTime? dt = null;

            if (txt.Text.Length > 0)
            {
                dt = Tools.InputTextDate2DateTime(txt.Text);
            }

            return dt;
        }
        #region Security
        protected bool UserHasRight(string right)
        {
            return BusinessLayer.UserHasRight(right);
        }
        #endregion

        protected void GetVonBisDatum(DateBoxPicker txtDatumVon, DateBoxPicker txtDatumBis, out DateTime? dtVon, out DateTime? dtBis)
        {
            GetVonBisDatum(txtDatumVon, txtDatumBis, out dtVon, out dtBis, false);
        }

        protected void GetVonBisDatum(DateBoxPicker txtDatumVon, DateBoxPicker txtDatumBis, out DateTime dtVon, out DateTime dtBis)
        {
            DateTime? von, bis;

            GetVonBisDatum(txtDatumVon, txtDatumBis, out von, out bis, false);

            dtVon = von.Value;
            dtBis = bis.Value;
        }

        protected bool TryGetVonBisDatum(DateBoxPicker txtDatumVon, DateBoxPicker txtDatumBis,
            out DateTime dtVon, out DateTime dtBis)
        {
            bool success = false;

            dtVon = DateTime.MinValue;
            dtBis = DateTime.MaxValue;

            try
            {
                GetVonBisDatum(txtDatumVon, txtDatumBis, out dtVon, out dtBis);
                success = true;
            }
            catch
            {
                success = false;
            }

            return success;
        }

        protected bool TryGetDatum(DateBoxPicker txtDatum, out DateTime dt)
        {
            bool success = false;
            dt = DateTime.MinValue;

            try
            {
                dt = Tools.InputTextDate2DateTime(txtDatum.Text);
                success = true;
            }
            catch
            {
                success = false;
            }

            return success;
        }

        protected void GetVonBisDatum(DateBoxPicker txtDatumVon, DateBoxPicker txtDatumBis, 
            out DateTime? dtVon, out DateTime? dtBis, bool setDateIfNull)
        {
            dtVon = null;
            dtBis = null;

            if (txtDatumVon.Text.Length > 0)
            {
                dtVon = Tools.InputTextDate2DateTime(txtDatumVon.Text);
            }
            else
            {
                if (setDateIfNull)
                {
                    dtVon = Tools.DatabaseDateTimeMinValue;
                }
            }
            if (txtDatumBis.Text.Length > 0)
            {
                dtBis = Tools.InputTextDate2DateTimeEnd(txtDatumBis.Text);
            }
            else
            {
                if (setDateIfNull)
                {
                    dtBis = Tools.DatabaseDateTimeMaxValue;
                }
            }
        }

        private void OperationenForm_Resize(object sender, EventArgs e)
        {
            if (DisplayHelpButton())
            {
                pbHelp.Location = new Point(10, GetHelpButtonPositionY());
            }

#if DEBUG
            string user = "*** not logged in ***";

            if (BusinessLayer.CurrentUser != null)
            {
                user = BusinessLayer.CurrentUser_Nachname;
            }

            string text = this.Width.ToString() + " " + this.Height.ToString() + " " + AppTitle() + " - [" + user + "]";
            this.Text = text;
#endif

        }

        protected void PopulateOPFunktionen(ComboBox comboBox, bool includeAll)
        {
            DataView dv = BusinessLayer.GetOPFunktionen(includeAll);

            comboBox.ValueMember = "ID_OPFunktionen";
            comboBox.DisplayMember = "Beschreibung";
            comboBox.DataSource = dv;
            comboBox.SelectedValue = (int)OP_FUNCTION.OP_FUNCTION_OP;
        }

        /// <summary>
        /// Remove the &amp; used with ALT-key to set the focus to a control,
        /// so that the &amp; is not displayed in a message box.
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        protected string ExtractControlText(Control control)
        {
            string text = control.Text.TrimStart(new char[] { '&' });

            return text;
        }

        protected void OperationenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !MayFormClose;
        }

        #region XableAllButtonsForLongOperation

        protected void XableAllButtonsForLongOperation(bool enable)
        {
            XableAllButtonsForLongOperation(null, null, enable);
        }

        /// <summary>
        /// Disable all Buttons and enable the abortButton, or enable all and disable the abortButton.
        /// </summary>
        /// <param name="abortButton">This button is set to !enable</param>
        /// <param name="enable">All other buttons are enabled to this value</param>
        protected void XableAllButtonsForLongOperation(Button abortButton, bool enable)
        {
            XableAllButtonsForLongOperation(abortButton, null, enable);
        }

        /// <summary>
        /// </summary>
        /// <param name="abortButton"></param>
        /// <param name="enable"></param>
        protected void XableAllButtonsForLongOperation(Button abortButton1, Button abortButton2, bool enable)
        {
            XAbleAllCommandsVisitor visitor = new XAbleAllCommandsVisitor(this, abortButton1, abortButton2, enable);
        }

        #endregion

        protected void SetWatermark(OplListView lv)
        {
            SetWatermark(lv, false, false);
        }

        protected void SetWatermark(OplListView lv, bool useBuiltin, bool forceWaterMark)
        {
            if (forceWaterMark || (BusinessLayer.GetUserSettingsString(GlobalConstants.SectionOther, GlobalConstants.KeyOtherWatermark, "1") == "1"))
            {
                Bitmap bitmap = null;
                Bitmap bitmap2 = null;

                if (!useBuiltin)
                {
                    try
                    {
                        string secretWatermarkFileName = BusinessLayer.AppPath + Path.DirectorySeparatorChar + "Watermark.png";
                        if (File.Exists(secretWatermarkFileName))
                        {
                            bitmap = new Bitmap(secretWatermarkFileName);
                            bitmap2 = new Bitmap(secretWatermarkFileName);
                        }
                    }
                    catch
                    {
                        bitmap = null;
                        bitmap2 = null;
                    }
                }

                if (bitmap == null || bitmap2 == null)
                {
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    Stream stream = assembly.GetManifestResourceStream("Operationen.Images.Watermark.png");
                    bitmap = new Bitmap(stream);
                    stream.Seek(0, SeekOrigin.Begin);
                    bitmap2 = new Bitmap(stream);
                }

                Graphics g = Graphics.FromImage(bitmap);
                g.Clear(lv.BackColor);
                g.DrawImage(bitmap2, 0, 0, bitmap2.Width, bitmap2.Height);
                g.Dispose();

                lv.SetWatermark(bitmap);
            }
        }
        protected string GetTextPrintForm()
        {
            return GetText(FormName, "print_form");
        }
        protected string GetTextPrintListViewBrowser()
        {
            return GetText(FormName, "print_listviewBrowser");
        }
        protected string GetTextPrintListViewBrowserSelectedColumns()
        {
            return GetText(FormName, "print_listviewBrowserSelectedColumns");
        }
        protected string GetTextPrintListViewBrowserBdc()
        {
            return GetText(FormName, "print_listviewBrowserBdc");
        }

        protected string GetTextPrintHeader(DateTime dateTime, int page)
        {
            string line = string.Format(CultureInfo.InvariantCulture, GetText(FormName, "print_header"),
                Tools.DBNullableDateTime2DateString(dateTime), page);

            return line;
        }

        protected string GetTextPrintHeader(DateTime dateTime)
        {
            string line = string.Format(CultureInfo.InvariantCulture, GetText(FormName, "print_header2"),
                Tools.DBNullableDateTime2DateString(dateTime));

            return line;
        }

        protected ContextMenuStrip CreateContextMenu(int bitMask)
        {
            ContextMenuStrip cmsListViewPrint;
            cmsListViewPrint = new System.Windows.Forms.ContextMenuStrip();

            if ((bitMask & ModePrintForm) != 0)
            {
                ToolStripMenuItem mi;
                mi = new System.Windows.Forms.ToolStripMenuItem();
                mi.Name = "miListViewBrowserPrint";
                mi.Size = new System.Drawing.Size(344, 22);
                mi.Text = GetTextPrintForm();
                mi.Click += new EventHandler(PrintForm_Click);

                cmsListViewPrint.Items.Add(mi);
            }

            if ((bitMask & ModePrintListViewBrowser) != 0)
            {
                ToolStripMenuItem miListViewBrowserPrint;
                miListViewBrowserPrint = new System.Windows.Forms.ToolStripMenuItem();
                miListViewBrowserPrint.Name = "miListViewBrowserPrint";
                miListViewBrowserPrint.Size = new System.Drawing.Size(344, 22);
                miListViewBrowserPrint.Text = GetTextPrintListViewBrowser();
                miListViewBrowserPrint.Click += new EventHandler(PrintListViewBrowser_Click);

                cmsListViewPrint.Items.Add(miListViewBrowserPrint);
            }

            if ((bitMask & ModePrintListViewBrowserSelectedColumns) != 0)
            {
                ToolStripMenuItem miListViewBrowserPrintSelectedColumns;
                miListViewBrowserPrintSelectedColumns = new System.Windows.Forms.ToolStripMenuItem();
                miListViewBrowserPrintSelectedColumns.Name = "miListViewBrowserPrintSelectedColumns";
                miListViewBrowserPrintSelectedColumns.Size = new System.Drawing.Size(344, 22);
                miListViewBrowserPrintSelectedColumns.Text = GetTextPrintListViewBrowserSelectedColumns();
                miListViewBrowserPrintSelectedColumns.Click += new EventHandler(PrintListViewBrowserSelectedColumns_Click);
                cmsListViewPrint.Items.Add(miListViewBrowserPrintSelectedColumns);
            }
            if ((bitMask & ModePrintListViewBrowserBdc) != 0)
            {
                ToolStripMenuItem mi;
                mi = new System.Windows.Forms.ToolStripMenuItem();
                mi.Name = "miListViewBrowserPrint";
                mi.Size = new System.Drawing.Size(344, 22);
                mi.Text = GetTextPrintListViewBrowserBdc();
                mi.Click += new EventHandler(PrintListViewBrowserBdc_Click);

                cmsListViewPrint.Items.Add(mi);
            }


            return cmsListViewPrint;
        }

        virtual protected void PrintForm_Click(object sender, EventArgs e)
        {
        }
        virtual protected void PrintListViewBrowser_Click(object sender, EventArgs e)
        {
        }
        virtual protected void PrintListViewBrowserSelectedColumns_Click(object sender, EventArgs e)
        {
        }
        virtual protected void PrintListViewBrowserBdc_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Set the selection to the line below the passed index, if there is one, otherwise 
        /// select the last line.
        /// </summary>
        /// <param name="listView"></param>
        /// <param name="index"></param>
        protected void EnsureVisibleNearIndex(ListView listView, int index)
        {
            if (index < listView.Items.Count - 1)
            {
                //
                // count = 4:
                // 0  index
                // 1  index 
                // 2  index
                // 3  
                //
                // Focus auf die Zeile darunter
                //
                index++;
            }
            else
            {
                //
                // count = 4:
                // 0
                // 1
                // 2 
                // 3  index in letzter Zeile
                //
                // falls index >= Count ist
                //
                index = listView.Items.Count - 1;
            }

            if ((0 <= index) && (index < listView.Items.Count))
            {
                listView.EnsureVisible(index);
            }
        }

        protected string GetTextPrintFromTo(string from, string to)
        {
            string line = "";

            if (from.Length > 0)
            {
                line += GetText(FormName, "von") + " " + from;
            }
            if (to.Length > 0)
            {
                if (line.Length > 0)
                {
                    line += " " + GetText(FormName, "lcase_bis") + " " ;
                }
                else
                {
                    line = GetText(FormName, "ucase_bis") +  " ";
                }
                line += to;
            }

            return line;
        }

        protected string GetMatchingOpsText(string opsKode)
        {
            string text;

            DataRow oOperation = BusinessLayer.GetIstOperationForPlanOperation(opsKode);
            if (oOperation == null)
            {
                text = GetText(FormName, "noneFound");
            }
            else
            {
                text = "[" + (string)oOperation["Kode"] + "] " + (string)oOperation["Name"];
            }

            return text;
        }

        protected string GetTextPrintQuelle(int quelle)
        {
            string text = "";

            if (quelle == BusinessLayer.OperationQuelleAlle)
            {
                text = GetText(FormName, "printQuelle1");
            }
            else if (quelle == BusinessLayer.OperationQuelleIntern)
            {
                text = GetText(FormName, "printQuelle2");
            }
            else if (quelle == BusinessLayer.OperationQuelleExtern)
            {
                text = GetText(FormName, "printQuelle3");
            }

            return text;
        }

        #region Visitor Design pattern
        public void Accept(IControlsVisitor visitor)
        {
            foreach (Control c in this.Controls)
            {
                Accept(visitor, c);
            }
        }

        public void Accept(IControlsVisitor visitor, Control control)
        {
            visitor.VisitControl(control);

            if (control is MenuStrip)
            {
                MenuStrip menuStrip = (MenuStrip)control;
                visitor.VisitMenuStrip(menuStrip);
            }
            else if (control is Elegant.Ui.Ribbon)
            {
                Elegant.Ui.Ribbon ribbon = (Elegant.Ui.Ribbon)control;
                foreach (Elegant.Ui.RibbonTabPage tabPage in ribbon.TabPages)
                {
                    Accept(visitor, tabPage);
                }
            }
            else if (control is System.Windows.Forms.Button)
            {
                System.Windows.Forms.Button button = (System.Windows.Forms.Button)control;
                visitor.VisitButton(button);
            }
            else if (control is Elegant.Ui.Button)
            {
                Elegant.Ui.Button button = (Elegant.Ui.Button)control;
                visitor.VisitElegantUiButton(button);
            }

            foreach (Control child in control.Controls)
            {
                Accept(visitor, child);
            }
        }

        public void Accept(IControlsVisitor visitor, MenuStrip item)
        {
            visitor.VisitMenuStrip(item);

            foreach (ToolStripMenuItem subitem in item.Items)
            {
                Accept(visitor, subitem);
            }
        }

        public void Accept(IControlsVisitor visitor, ToolStripItem item)
        {
            visitor.VisitToolStripItem(item);
        }

        public void Accept(IControlsVisitor visitor, ToolStripMenuItem item)
        {
            visitor.VisitToolStripMenuItem(item);

            for (int i = 0; i < item.DropDownItems.Count; i++)
            {
                if (item.DropDownItems[i] is ToolStripSeparator)
                {
                }
                else
                {
                    Accept(visitor, item.DropDownItems[i]);
                }
            }
        }

        public void Accept(IControlsVisitor visitor, Elegant.Ui.RibbonGroup item)
        {
            visitor.VisitRibbonGroup(item);
         
            foreach (Control control in item.Controls)
            {
                Accept(visitor, control);
            }
        }

        public void Accept(IControlsVisitor visitor, Elegant.Ui.RibbonTabPage item)
        {
            visitor.VisitRibbonTabPage(item);

            foreach (Elegant.Ui.RibbonGroup ribbonGroup in item.Controls)
            {
                Accept(visitor, ribbonGroup);
            }
        }

        #endregion

        protected void ChangeLanguage(string uiCulture)
        {
            CultureInfo ci = new CultureInfo(uiCulture);
            Thread.CurrentThread.CurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(uiCulture);

            Type type = GetType();
            ComponentResourceManager resources = new ComponentResourceManager(type);

            ApplyResourcesVisitor visitor = new ApplyResourcesVisitor(resources, ci);

            Accept(visitor);
        }

        protected bool DisplayPluginLoadExceptionAndQueryForBreak(Exception e, string text)
        {
            bool bBreak = false;

            StringBuilder sb = new StringBuilder(text);
            sb.Append("\r\r");
            sb.Append(GetText(FormName, "errLoadingPluginError"));
            sb.Append("\r");
            sb.Append(e.Message);

            if (e is ReflectionTypeLoadException)
            {
                ReflectionTypeLoadException reflectionTypeLoadException = e as ReflectionTypeLoadException;
                sb.Append("\r\rLoaderExceptions:");
                if (reflectionTypeLoadException.LoaderExceptions.Length > 0)
                {
                    for (int i = 0; i < reflectionTypeLoadException.LoaderExceptions.Length; i++)
                    {
                        sb.Append("\r - ");
                        sb.Append(reflectionTypeLoadException.LoaderExceptions[i].Message);
                    }
                }
            }

            //
            // Continue loading ? OK - continue, Cancel - abort
            //
            sb.Append("\r\r");
            text = string.Format(CultureInfo.InvariantCulture, GetText(FormName, "errLoadingPluginContinueYesNo"), DialogResult.OK.ToString(), DialogResult.Cancel.ToString());
            sb.Append(text);

            DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(sb.ToString(), _businessLayerBase.AppTitle(), MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
            if (dialogResult != DialogResult.OK)
            {
                bBreak = true;
            }

            return bBreak;
        }

        protected void DisplayErrorToUser(Exception exception, string text)
        {
            BusinessLayer.DisplayError(exception, text);
        }

        protected Assembly curDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            AssemblyName name = new AssemblyName(args.Name);
            string pluginPath = BusinessLayer.PathPlugins;

            String asmToCheck = pluginPath + System.IO.Path.DirectorySeparatorChar + name.Name + ".dll";

            if (File.Exists(asmToCheck))
            {
                return Assembly.ReflectionOnlyLoadFrom(asmToCheck);
            }
            return Assembly.ReflectionOnlyLoad(args.Name);
        }

        void OperationenForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.D)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                BusinessLayer.ShowDebugWindow();
            }
        }

        /// <summary>
        /// This function must be called BEFORE setting another callback on the command
        /// because we want to debug print that this command was clicked BEFORE
        /// the command execution opens a window.
        /// </summary>
        protected virtual void SetupGuiDebugPrint()
        {
            ApplyGuiDebugVisitor visitor = new ApplyGuiDebugVisitor(BusinessLayer, true);

            Accept(visitor);
        }

        protected void XableAllCommands(bool enable)
        {
            XAbleAllCommandsVisitor visitor = new XAbleAllCommandsVisitor(this, enable);

            Accept(visitor);
        }

        public override void DebugPrintControlContents(Control control)
        {
            if (control is TextBox)
            {
                string text = control.Text;

                AppFramework.Debugging.DebugLogging.WriteLine(AppFramework.Debugging.DebugLogging.DebugFlagGuiContents,
                    "Contents of '" + control.Name + "': " + text);
            }
        }

        public override void DebugPrintControlClicked(Control control)
        {
            string text = BuildFullControlName(control);

            AppFramework.Debugging.DebugLogging.WriteLine(AppFramework.Debugging.DebugLogging.DebugFlagGuiCommand,
                "Clicked: " + text);
        }

        public void DebugPrint(long flag, string text)
        {
            AppFramework.Debugging.DebugLogging.WriteLine(flag, text);
        }
    }
}

