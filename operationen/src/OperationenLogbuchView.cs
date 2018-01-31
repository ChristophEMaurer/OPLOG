
//#define USE_SPLASHSCREEN


using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Globalization;
using System.Reflection;
using System.IO.IsolatedStorage;

using Utility;
using Windows.Forms;
using CMaurer.Operationen.AppFramework;

using Operationen.Wizards.ImportRichtlinien;
using Operationen.Wizards.ImportRichtlinienZuordnung;
using Operationen.Wizards.ExportRichtlinien;
using Operationen.Wizards.ExportRichtlinienZuordnung;
using Operationen.Wizards.ExportChirurg;
using Operationen.Wizards.ImportOPS;
using Operationen.Wizards.ImportChirurg;
using Operationen.Wizards.ImportOperationenMobile;
using Operationen.Wizards.InstallLicense;


namespace Operationen
{
    public partial class OperationenLogbuchView : OperationenForm
    {
        static int NumTabPages = 0;
        static OperationenLogbuchView _theMainForm = null;

        private Dictionary<string, OperationenForm> openWindows = new Dictionary<string, OperationenForm>();

#if USE_SPLASHSCREEN
        private SplashScreenView _splashScreen;
#endif

        public OperationenLogbuchView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            _theMainForm = this;

#if USE_SPLASHSCREEN
            _splashScreen = new SplashScreenView(businessLayer);
#endif
            CreateCommands();

            InitializeComponent();

            NumTabPages = ribbon.TabPages.Count;

#if USE_SPLASHSCREEN
            _splashScreen.Show();
#endif

        }

        protected override void OnLoad(EventArgs e)
        {
            LoadRibbonStateForCurrentUser();

            this.Width = 1138;

            ribbon.QuickAccessToolbarPlacementMode = Elegant.Ui.QuickAccessToolbarPlacementMode.AboveRibbon;
            ribbon.QuickAccessToolbarPlacementModeChanged += new EventHandler(ribbon_QuickAccessToolbarPlacementModeChanged);

            LoadMainPicture();

            pbMain.Top = 0;
            pbMain.Left = 0;
            pbMain.SizeMode = PictureBoxSizeMode.AutoSize;
            pbMain.BringToFront();
            pbMain.Focus();

            SetDetailsAndHeight();

            //
            // Erst wenn die Menüs erzeugt wurden, kann man auf sie zugreifen.
            //
            CreateCommandTextVariables();

            SetRibbonScreentips();

#if urologie
           // Grafik auf Hauptseite ändern
#elif gynaekologie
           // Grafik auf Hauptseite ändern
#else
#endif

            CreateEigeneDateienCustomMenu();
            CreateVideoTutorialGui();

            timer.Stop();
            timer.Tick += new EventHandler(timer_Tick);

            base.OnLoad(e);
        }
    
        void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            UpdateAfterShownStuff();

            //
            // Achtung: hiermit werden die Buttons enabled, auch wenn man kein Recht dazu hat!
            // Wenn man aber XableAll ButtonsForLongOperation(true);
            // nicht aufruft, bleiben auch die Buttons ohne Rechte disabled...
            //
            XableAllButtonsForLongOperation(true);

            //
            // Alle disablen, für die man kein Recht hat.
            //
            EnableRibbonMenu();

#if USE_SPLASHSCREEN
            if (_splashScreen != null)
            {
                _splashScreen.Close();
            }
#endif

            if ("1" == BusinessLayer.GetUserSettingsString(GlobalConstants.SectionOther, GlobalConstants.KeyOtherDisplayOpenCommentMessage, "1"))
            {
                if (BusinessLayer.CurrentUserHasOpenComments())
                {
                    MessageBox(string.Format(CultureInfo.InvariantCulture, GetText("open_comments"), Command_OptionsView));
                }
            }

            // Automatischer Operationen-Import aus einem vordefiniertem Verzeichnis
            if ("1" == BusinessLayer.GetUserSettingsString(GlobalConstants.SectionOpImport, GlobalConstants.KeyOpImportAutoImport))
            {
                if (UserHasRight("OperationenImportView.edit"))
                {
                    PerformAutoImport();
                }
            }
        }

        protected override bool DisplayHelpButton()
        {
            return false;
        }

        private void SetRibbonScreentips()
        {
            //
            // Start Button
            //
            applicationMenu.OptionsButtonScreenTip.Caption = GetText("mnItem_OptionsView_caption");
            applicationMenu.OptionsButtonScreenTip.Text = GetText("mnItem_OptionsView");

            applicationMenu.ExitButtonScreenTip.Caption = GetText("mnItem_Exit_caption");
            applicationMenu.ExitButtonScreenTip.Text = GetText("mnItem_Exit");

            SetScreenTip(cmdPrintIstOperationen, "mnItem_PrintIstOperationen");

            //
            // Eigene Dateien
            //
            SetScreenTip(rtpEigeneDateien, "mnEigeneDateien");
            SetScreenTip(cmdDateiTypenView, "mnItem_DateiTypenView",
                string.Format(CultureInfo.InvariantCulture,
                GetText("mnItem_DateiTypenView"),
                Command_EigeneDateien));
            SetScreenTip(cmdDateienView, "mnItem_DateienView",
                string.Format(CultureInfo.InvariantCulture,
                GetText("mnItem_DateienView"),
                Command_EigeneDateien));
            SetScreenTip(ddEigeneDateien, "mnItem_EigeneDateien",
                string.Format(CultureInfo.InvariantCulture, GetText("mnItem_EigeneDateien"),
                Command_DateienView));

            //
            // Offizielle Dokumente
            //
            SetScreenTip(rtpOffizielleDokumente, "mnOffizielleDokumente");
            SetScreenTip(cmdOperationenKatalogView, "mnItem_OperationenKatalogView");
            SetScreenTip(cmdGebieteView, "mnItem_GebieteView");
            SetScreenTip(cmdRichtlinienView, "mnItem_RichtlinienView");
            SetScreenTip(cmdBaekWbOrdnung, "mnItem_BAEK_WBOrdnung");
            SetScreenTip(cmdBaekWbRichtlinien, "mnItem_BAEK_WBRichtlinien");

            //
            // Verwaltung
            //
            SetScreenTip(rtpVerwaltung, "mnVerwaltung");
            SetScreenTip(cmdSecUserOverviewView, "mnItem_SecUserOverviewView");
            SetScreenTip(cmdSecGroupsView, "mnItem_SecGroupsView");
            SetScreenTip(cmdSecGroupsChirurgenView, "mnItem_SecGroupsChirurgenView");
            SetScreenTip(cmdSecGroupsSecRightsView, "mnItem_SecGroupsSecRightsView");
            SetScreenTip(cmdAbteilungenView, "mnItem_AbteilungenView");
            SetScreenTip(cmdAbteilungenChirurgenView, "mnItem_AbteilungenChirurgenView");
            SetScreenTip(cmdWeiterbilderChirurgenView, "mnItem_WeiterbilderChirurgenView");
            SetScreenTip(cmdOperationenSummaryView, "mnItem_OperationenSummaryView");

            //
            // Stammdaten
            //
            SetScreenTip(rtpStammdaten, "mnStammdaten");
            SetScreenTip(cmdChirurgenFunktionenView, "mnItem_ChirurgenFunktionenView");
            SetScreenTip(cmdChirurgNew, "mnItem_ChirurgView_new");
            SetScreenTip(cmdChirurgEdit, "mnItem_ChirurgView_edit");
            SetScreenTip(cmdChirurgDelete, "mnItem_ChirurgView_delete");
            SetScreenTip(cmdKommentareView, "mnItem_KommentareView");
            SetScreenTip(cmdDokumenteView, "mnItem_DokumenteView");
            SetScreenTip(cmdChirurgDokumenteView, "mnItem_ChirurgDokumenteView",
                string.Format(CultureInfo.InvariantCulture,
                GetText("mnItem_ChirurgDokumenteView"),
                Command_DokumenteView));
            SetScreenTip(cmdAkademischeAusbildungTypenView, "mnItem_AkademischeAusbildungTypenView");
            SetScreenTip(cmdAkademischeAusbildungView, "mnItem_AkademischeAusbildungView");
            SetScreenTip(cmdNotizTypenView, "mnItem_NotizTypenView");
            SetScreenTip(cmdNotizenView, "mnItem_NotizenView");
            SetScreenTip(cmdPlanOperationenView, "mnItem_PlanOperationenView");
            SetScreenTip(cmdPlanOperationVergleichView, "mnItem_PlanOperationVergleichView",
                string.Format(CultureInfo.InvariantCulture,
                GetText("mnItem_PlanOperationVergleichView"),
                Command_PlanOperationenView));
            SetScreenTip(cmdRichtlinienSollIstView, "mnItem_RichtlinienSollIstView");

            //
            // Bearbeiten
            //
            SetScreenTip(rtpBearbeiten, "mnBearbeiten");
            SetScreenTip(cmdOperationenEditView, "mnItem_OperationenEditView");
            SetScreenTip(cmdChirurgenRichtlinienView, "mnItem_ChirurgenRichtlinienView");
            SetScreenTip(cmdRichtlinienOpsKodeUnassignedView, "mnItem_RichtlinienOpsKodeUnassignedView");

            //
            // Auswertungen
            //
            SetScreenTip(rtpAuswertungen, "mnAuswertungen");
            SetScreenTip(cmdOperationenView, "mnItem_OperationenView");
            SetScreenTip(cmdOperationenZeitenVergleichView, "mnItem_OperationenZeitenVergleichView");
            SetScreenTip(cmdOPDauerFortschrittView, "mnItem_OPDauerFortschrittView");
            SetScreenTip(cmdKlinischeErgebnisseView, "mnItem_KlinischeErgebnisseView");
            SetScreenTip(cmdChirurgOperationenView, "mnItem_ChirurgOperationenView");
            SetScreenTip(cmdGesamtOperationenView, "mnItem_GesamtOperationenView");
            SetScreenTip(cmdPlanOperationVergleichIstView, "mnItem_PlanOperationVergleichIstView",
                string.Format(CultureInfo.InvariantCulture, 
                GetText("mnItem_PlanOperationVergleichIstView"), Command_PlanOperationenView));
            SetScreenTip(cmdOperationenVergleichView, "mnItem_OperationenVergleichView");
            SetScreenTip(cmdRichtlinienVergleichView, "mnItem_RichtlinienVergleichView");
            SetScreenTip(cmdRichtlinienVergleichOverviewView, "mnItem_RichtlinienVergleichOverviewView");

            //
            // Extras
            //
            SetScreenTip(rtpExtras, "mnExtras");
            SetScreenTip(cmdUserChangePasswordView, "mnItem_UserChangePasswordView");
            SetScreenTip(cmdUserSetPasswordView, "mnItem_UserSetPasswordView");
            SetScreenTip(cmdSerialNumbersView, "mnItem_SerialNumbersView");
            SetScreenTip(cmdSerialBuy, "mnItem_www_serialnumbers");
            SetScreenTip(cmdInstallLicenseWizard, "mnItem_InstallLicenseWizard");
            SetScreenTip(cmdUpdateFromFolder, "mnItem_updatecheck_folder");
            SetScreenTip(cmdUpdateFromWww, "mnItem_updatecheck_internet");
            SetScreenTip(cmdCopyWWWProgramUpdateFilesView, "mnItem_CopyWWWProgramUpdateFilesView");
            SetScreenTip(cmdLogView, "mnItem_LogView");
            SetScreenTip(cmdOptionsView, "mnItem_OptionsView");

            //
            // Datenimport
            //
            SetScreenTip(rtpImport, "mnItem_Import");
            SetScreenTip(cmdOperationenImportView, "mnItem_OperationenImportView");
            SetScreenTip(cmdImportChirurgenExcludeView, "mnItem_ImportChirurgenExcludeView");
            SetScreenTip(cmdExecAutoImport, "mnItem_AutoImportView");
            SetScreenTip(cmdImportRichtlinienWizard, "mnItem_ImportRichtlinienWizard",
                string.Format(CultureInfo.InvariantCulture,
                GetText("mnItem_ImportRichtlinienWizard"),
                Command_ExportRichtlinienWizard));
            SetScreenTip(cmdImportRichtlinienZuordnungWizard, "mnItem_ImportRichtlinienZuordnungWizard",
                string.Format(CultureInfo.InvariantCulture,
                GetText("mnItem_ImportRichtlinienZuordnungWizard"),
                Command_ExportRichtlinienZuordnungWizard));
            SetScreenTip(cmdImportChirurgWizard, "mnItem_ImportChirurgWizard",
                string.Format(CultureInfo.InvariantCulture,
                GetText("mnItem_ImportChirurgWizard"),
                Command_ExportChirurgWizard));
            SetScreenTip(cmdImportOPSWizard, "mnItem_ImportOPSWizard");
            SetScreenTip(cmdImportOperationenMobileWizard, "mnItem_ImportOperationenMobileWizard");
            SetScreenTip(cmdClientServerViewImport, "mnItem_ClientServerView");
            
            //
            // Datenexport
            //
            SetScreenTip(rtpExport, "mnItem_Export");
            SetScreenTip(cmdExportRichtlinienWizard, "mnItem_ExportRichtlinienWizard");
            SetScreenTip(cmdExportRichtlinienZuordnungWizard, "mnItem_ExportRichtlinienZuordnungWizard");
            SetScreenTip(cmdExportChirurgWizard, "mnItem_ExportChirurgWizard");
            SetScreenTip(cmdExportOperationenKatalogView, "mnItem_ExportOperationenKatalogView");
            SetScreenTip(cmdClientServerViewExport, "mnItem_ClientServerView");

            //
            // Hilfe
            //
            SetScreenTip(rtpHilfe, "mnHilfe");
            SetScreenTip(cmdHelpChm, "mnItem_help_chm");
            SetScreenTip(cmdWwwHelp, "mnItem_www_help");
            cmdWwwHome.ScreenTip.Caption = BusinessLayer.ProgramTitle;
            cmdWwwHome.ScreenTip.Text = string.Format(CultureInfo.InvariantCulture,
                GetText("mnItem_www_main"),
                BusinessLayer.ProgramTitle);
            cmdAboutBox.ScreenTip.Caption = string.Format(CultureInfo.InvariantCulture,
                GetText("mnItem_AboutBox_caption"),
                BusinessLayer.ProgramTitle);
            cmdAboutBox.ScreenTip.Text = GetText("mnItem_AboutBox");
            SetScreenTip(ddVideoTutorials, "mnItem_ddVideoTutorials");

            //
            // Debug
            //
            SetScreenTip(cmdLangEnUs, "mnItem_sprache_en_us");
            SetScreenTip(cmdLangDeDe, "mnItem_sprache_de_de");
        }

        static public OperationenLogbuchView TheMainWindow
        {
            get { return _theMainForm; }
        }

        /// <summary>
        /// Öffnet ein Fenster falls es noch nicht geöffnet ist.
        /// Gilt für Dialoge, die von OperationenForm abgleitet sind und folgenden Konstruktor haben:
        /// <code>
        /// public NotizTypenView(BusinessLayer b);
        /// </code>
        /// Wird immer mit einer Instanz von BusinessLayer geöffnet:
        /// <code>
        /// Aufruf mit: OpenWindow(typeof(NotizTypenView));
        /// <br/>
        /// statt 
        /// <br/>
        /// NotizTypenView dlg = new NotizTypenView(BusinessLayer);
        /// dlg.ShowDialog();
        /// </code>
        /// </summary>
        /// <param name="t">Der Typ des Forms</param>
        /// 
        public void OpenWindow(string right, Type t)
        {
            if (string.IsNullOrEmpty(right) || UserHasRight(right))
            {
                string typeName = t.Name;

                if (openWindows.ContainsKey(typeName))
                {
                    OperationenForm form = openWindows[typeName];
                    form.BringToFront();
                }
                else
                {
                    object[] args = { BusinessLayer };

                    OperationenForm form = (OperationenForm)Activator.CreateInstance(t, args);
                    form.FormClosed += new FormClosedEventHandler(form_FormClosed);

                    openWindows.Add(typeName, form);
                    form.Show();
                }
            }
        }

        public void OpenWindow(Type t)
        {
            OpenWindow(null, t);
        }

        public void OpenWindowWithAdditionalParameters(Type t, ArrayList arguments)
        {
            {
                OpenWindowWithAdditionalParameters(null, t, arguments);
            }
        }

        /// <summary>
        /// Öffnet ein Fenster falls es noch nicht geöffnet ist.
        /// Gilt für Dialoge, die von OperationenForm abgleitet sind und außer dem BusinessLayer noch weitere Parameter haben
        /// <code>
        /// public ChirurgOperationenView(BusinessLayer b, ArrayList args)
        /// public SerialNumbersView(BusinessLayer b, bool ignoreRights)
        /// </code>
        /// Wird immer mit einer Instanz von BusinessLayer geöffnet:
        /// <code>
        /// ArrayList args = new ArrayList();
        /// args.Add(this);
        /// 
        /// OpenWindowWithAdditionalParameters(typeof(ChirurgOperationenView), args);
        /// <br/>
        /// statt 
        /// <br/>
        /// ChirurgOperationenView dlg = new ChirurgOperationenView(BusinessLayer, ...);
        /// dlg.ShowDialog();
        /// </code>
        /// </summary>
        /// <param name="t">Der Typ des Forms</param>
        public void OpenWindowWithAdditionalParameters(string right, Type t, ArrayList arguments)
        {
            if (string.IsNullOrEmpty(right) || UserHasRight(right))
            {
                string typeName = t.Name;

                if (openWindows.ContainsKey(typeName))
                {
                    OperationenForm form = openWindows[typeName];
                    form.BringToFront();
                }
                else
                {
                    object[] args = { BusinessLayer, arguments };

                    OperationenForm form = (OperationenForm)Activator.CreateInstance(t, args);
                    form.FormClosed += new FormClosedEventHandler(form_FormClosed);

                    openWindows.Add(typeName, form);
                    form.Show();
                }
            }
        }

        void form_FormClosed(object sender, FormClosedEventArgs e)
        {
            string typeName = sender.GetType().Name;

            if (openWindows.ContainsKey(typeName))
            {
                openWindows.Remove(typeName);
            }
        }

        private void ChirurgEdit()
        {
            int nID_Chirurg = SelectChirurg(false);

            if (nID_Chirurg != -1)
            {
                DataRow oChirurg = this.BusinessLayer.GetChirurg(nID_Chirurg);
                ChirurgView dlg = new ChirurgView(BusinessLayer, oChirurg);
                dlg.ShowDialog();
            }
        }

        private void ChirurgNew()
        {
            ChirurgView dlg = new ChirurgView(BusinessLayer, null);
            dlg.ShowDialog();
        }

        private void ChirurgDelete()
        {
            DataRow dataRow = SelectChirurgDataRow();

            if (dataRow != null)
            {
                if (ConvertToInt32(dataRow["ID_Chirurgen"]) == BusinessLayer.CurrentUser_ID_Chirurgen)
                {
                    MessageBox(GetText("NoSelfDelete"));
                }
                else
                {
                    string strVorname = (string)dataRow["Vorname"];
                    string strNachname = (string)dataRow["Nachname"];
                    string msg = string.Format(CultureInfo.InvariantCulture, GetText("del_surgeon_msg"), strVorname, strNachname, Command_ChirurgEdit);

                    if (Confirm(msg))
                    {
                        msg = string.Format(CultureInfo.InvariantCulture, GetText("del_surgeon_msg2"), strVorname, strNachname);
                        if (Confirm(msg))
                        {
                            Cursor = Cursors.WaitCursor;
                            Application.DoEvents();
                            bool success = BusinessLayer.DeleteChirurg(ConvertToInt32(dataRow["ID_Chirurgen"]));
                            Cursor = Cursors.Default;
                            Application.DoEvents();

                            if (success)
                            {
                                msg = string.Format(CultureInfo.InvariantCulture, GetText("del_surgeon_success"), strVorname, strNachname);
                            }
                            else
                            {
                                msg = string.Format(CultureInfo.InvariantCulture, GetText("del_surgeon_failure"), strVorname, strNachname);
                            }
                            MessageBox(msg);
                        }
                    }
                }
            }
        }


        private void ChirurgOverview()
        {
            ArrayList args = new ArrayList();
            args.Add(this);

            OpenWindowWithAdditionalParameters("ChirurgOperationenView.edit", typeof(ChirurgOperationenView), args);
        }

        private bool ConfirmAndCloseAllOpenWindows(string text)
        {
            bool success = true;

            if (openWindows.Count > 0)
            {
                success = false;
                
                MessageBox(string.Format(GetText("CloseWindows"), text));
            }

            return success;
        }



#if false
        private void CompactAndRepairAccessDatabase()
        {
            string s = "Sind Sie sicher, dass Sie die Datenbank komprimieren und reparieren wollen?"
                + "\n\nHierzu benötigen Sie ausreichend Platz auf c:\\ sowie Schreibberechtigung hierauf."
                + "\nDie Datenbank darf nicht in Benutzung sein, d.h. kein Benutzer darf auf sie zugreifen."
                + "\nSichern Sie die Datenbank, bevor Sie diesen Befehl ausführen."
                + "\n\nFortfahren?";

            if (Confirm(s))
            {
                if (Confirm("Sind Sie ganz sicher, dass Sie die Datenbank komprimieren wollen? Dieses darf nur vom Systemadministrator ausgeführt werden."))
                {
                    Cursor = Cursors.WaitCursor;

                    string strFilename = "operationen-" + Tools.DBNullableDateTime2DateString(DateTime.Now) + "." + Tools.DBNullableDateTime2TimeString(DateTime.Now) + ".mdb";
                    // Mit : wird die DB in einem Stream geschrieben!
                    strFilename = "c:\\" + strFilename.Replace(':', '.');
                    bool success = BusinessLayer.CompactAccessDB(strFilename);

                    Cursor = Cursors.Default;

                    if (success)
                    {
                        MessageBox("Die Datenbank Komprimierung wurde erfolgreich durchgeführt.");
                    }
                }
            }
        }
#endif

        private void IfMissingQueryUserAndDownload(string localFile, string downloadInfoFile, string description, string remoteName)
        {
            bool fileExists = false;

            if (File.Exists(localFile))
            {
                fileExists = true;
            }
            else
            {
                string text = string.Format(CultureInfo.InvariantCulture, GetText("downloadInfo1"), localFile, BusinessLayer.UrlHomepageForDownload);
                MessageBox(text);
                

                // Größe der Datei holen, damit der Progress richtig angezeigt werden kann.
                // Hierbei darf keine Fehlermeldung erscheinen.
                int fileSizeKb = 8000;

                if (!string.IsNullOrEmpty(downloadInfoFile))
                {
                    string fileContents;

                    if (DownloadAsciiInfoFile(
                        downloadInfoFile,
                        false, null, out fileContents))
                    {
                        // bedienungsanleitung.txt enthaelt die Größe in KB: "4700"
                        Int32.TryParse(fileContents, out fileSizeKb);
                    }

                    text = string.Format(CultureInfo.InvariantCulture, GetText("downloadInfo2"), description);
                    bool fileDownloaded = DownloadFile(
                        text,
                        fileSizeKb, BusinessLayer.UrlHomepageForDownload + "/download/" + remoteName,
                        localFile);

                    if (fileDownloaded)
                    {
                        fileExists = true;
                    }
                }
            }

            if (fileExists)
            {
                launchFileDirect(localFile);
            }
        }

        private void bedienungsanleitungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IfMissingQueryUserAndDownload(BusinessLayer.PathDokumente + System.IO.Path.DirectorySeparatorChar + BusinessLayer.BedienungsanleitungPdf,
                BusinessLayer.UrlHomepageForDownload + "/download/bedienungsanleitung.txt",
                GetText("bedienungsanleitung"),
                BusinessLayer.BedienungsanleitungPdf
                );
        }

        /// <summary>
        /// Je nach vorhandenen Rechten werden alle commands und damit auch die dazugehörigen
        /// Menüpunkte enabled oder auch nicht.
        /// </summary>
        internal void EnableRibbonMenu()
        {
            //
            // Eigene Dateien
            //
            ApplicationCommands.PrintIstOperationen.Enabled = UserHasRight("PrintIstOperationen.edit");
            ApplicationCommands.DateiTypenView.Enabled = UserHasRight("DateiTypenView.view");
            ApplicationCommands.DateienView.Enabled = UserHasRight("DateienView.edit");

            //
            // Offizielle Dokumente
            //
            ApplicationCommands.OperationenKatalogView.Enabled = UserHasRight("OperationenKatalogView.edit");
            ApplicationCommands.GebieteView.Enabled = UserHasRight("GebieteView.view");
            ApplicationCommands.RichtlinienView.Enabled = UserHasRight("RichtlinienView.view");

            //
            // Verwaltung
            //
            ApplicationCommands.SecUserOverviewView.Enabled = UserHasRight("SecUserOverviewView.edit");
            ApplicationCommands.SecGroupsView.Enabled = UserHasRight("SecGroupsView.view");
            ApplicationCommands.SecGroupsChirurgenView.Enabled = UserHasRight("SecGroupsChirurgenView.edit");
            ApplicationCommands.SecGroupsSecRightsView.Enabled = UserHasRight("SecGroupsSecRightsView.edit");
            ApplicationCommands.AbteilungenView.Enabled = UserHasRight("AbteilungenView.view");
            ApplicationCommands.AbteilungenChirurgenView.Enabled = UserHasRight("AbteilungenChirurgenView.edit");
            ApplicationCommands.WeiterbilderChirurgenView.Enabled = UserHasRight("WeiterbilderChirurgenView.edit");
            ApplicationCommands.OperationenSummaryView.Enabled = UserHasRight("OperationenSummaryView.view");

            //
            // Stammdaten
            //
            rgrChirurg.Enabled = UserHasRight("mn.Chirurg");
            ApplicationCommands.ChirurgenFunktionenView.Enabled = UserHasRight("ChirurgenFunktionenView.view");
            ApplicationCommands.ChirurgNew.Enabled = UserHasRight("mn.ChirurgNew");
            ApplicationCommands.ChirurgEdit.Enabled = UserHasRight("mn.ChirurgEdit");
            ApplicationCommands.ChirurgDelete.Enabled = UserHasRight("mn.ChirurgDelete");
            ApplicationCommands.KommentareView.Enabled = UserHasRight("KommentareView.edit");
            ApplicationCommands.DokumenteView.Enabled = UserHasRight("DokumenteView.edit");
            ApplicationCommands.ChirurgDokumenteView.Enabled = UserHasRight("ChirurgDokumenteView.edit");
            ApplicationCommands.AkademischeAusbildungTypenView.Enabled = UserHasRight("AkademischeAusbildungTypenView.view");
            ApplicationCommands.AkademischeAusbildungView.Enabled = UserHasRight("AkademischeAusbildungView.edit");
            ApplicationCommands.NotizTypenView.Enabled = UserHasRight("NotizTypenView.view");
            ApplicationCommands.NotizenView.Enabled = UserHasRight("NotizenView.edit");
            ApplicationCommands.PlanOperationenView.Enabled = UserHasRight("PlanOperationenView.view");
            ApplicationCommands.PlanOperationVergleichIstView.Enabled = UserHasRight("PlanOperationVergleichIstView.edit");
            ApplicationCommands.RichtlinienSollIstView.Enabled = UserHasRight("RichtlinienSollIstView.view");

            //
            // Bearbeiten
            //
            ApplicationCommands.OperationenEditView.Enabled = UserHasRight("OperationenEditView.edit");
            ApplicationCommands.ChirurgenRichtlinienView.Enabled = UserHasRight("ChirurgenRichtlinienView.edit");
            ApplicationCommands.RichtlinienOpsKodeUnassignedView.Enabled = UserHasRight("RichtlinienOpsKodeUnassignedView.edit");

            //
            // Auswertungen
            //
            ApplicationCommands.OperationenView.Enabled = UserHasRight("OperationenView.edit");
            ApplicationCommands.OperationenZeitenVergleichView.Enabled = UserHasRight("OperationenZeitenVergleichView.edit");
            ApplicationCommands.OPDauerFortschrittView.Enabled = UserHasRight("OPDauerFortschrittView.edit");
            ApplicationCommands.ChirurgOperationenView.Enabled = UserHasRight("ChirurgOperationenView.edit");
            ApplicationCommands.GesamtOperationenView.Enabled = UserHasRight("GesamtOperationenView.edit");
            ApplicationCommands.OperationenVergleichView.Enabled = UserHasRight("OperationenVergleichView.edit");
            ApplicationCommands.PlanOperationVergleichView.Enabled = UserHasRight("PlanOperationVergleichView.edit");

            ApplicationCommands.RichtlinienVergleichView.Enabled = UserHasRight("RichtlinienVergleichView.edit");
            
            ApplicationCommands.RichtlinienVergleichOverviewView.Enabled
                = UserHasRight("RichtlinienVergleichOverviewView.edit");

            ApplicationCommands.KlinischeErgebnisseView.Enabled = UserHasRight("KlinischeErgebnisseView.edit");

            //
            // Extras
            // Extras > Datenimport
            //
            rtpImport.Enabled = UserHasRight("mn.Import");
            ApplicationCommands.ImportChirurgenExcludeView.Enabled = UserHasRight("ImportChirurgenExcludeView.edit");
            ApplicationCommands.OperationenImportView.Enabled = UserHasRight("OperationenImportView.edit");
            ApplicationCommands.ExecAutoImport.Enabled = UserHasRight("mn.ImportAutoImport");
            ApplicationCommands.ImportRichtlinienWizard.Enabled = UserHasRight("cmd.ImportRichtlinien");
            ApplicationCommands.ImportRichtlinienZuordnungWizard.Enabled = UserHasRight("cmd.ImportZuordnungen");
            ApplicationCommands.ImportChirurgWizard.Enabled = UserHasRight("cmd.ImportChirurg");
            ApplicationCommands.ImportOPSWizard.Enabled = UserHasRight("ImportOPSWizard.edit");
            ApplicationCommands.ImportOperationenMobileWizard.Enabled = UserHasRight("ImportOperationenMobileWizard.edit");
            ApplicationCommands.ClientServerView.Enabled = UserHasRight("ClientServerView.view");
            
            //
            // Extras > Datenexport
            //
            rtpExport.Enabled = UserHasRight("mn.Export");
            ApplicationCommands.ExportRichtlinienWizard.Enabled = UserHasRight("cmd.ExportRichtlinien");
            ApplicationCommands.ExportRichtlinienZuordnungWizard.Enabled = UserHasRight("cmd.ExportZuordnungen");
            ApplicationCommands.ExportChirurgWizard.Enabled = UserHasRight("cmd.ExportChirurg");
            ApplicationCommands.ExportOperationenKatalogView.Enabled = UserHasRight("ExportOperationenKatalogView.edit");
            //
            // This button appears twice, this is handled above
            //
            // ApplicationCommands.ClientServerView.Enabled = UserHasRight("ClientServerView.view");

            //
            // Für UserChangePasswordView gibt es kein Recht weil bisher jeder Benutzer
            // unabhängig von irgendwelchen Rechten sein password ändern können muss.
            // Nur bei Windows Authentifizierung gibt es kein eigenes Password in der Anwendung
            // Die ganze Ribbon group muss ganz invisible sein.
            //
            if (BusinessLayer.AuthenticationMode == System.Web.Configuration.AuthenticationMode.Windows)
            {
                ApplicationCommands.UserChangePasswordView.Enabled = false;
                ApplicationCommands.UserSetPasswordView.Enabled = false;
                rgrExtras1.Enabled = false;
                rgrExtras1.Visible = false;
            }
            else
            {
                ApplicationCommands.UserChangePasswordView.Enabled = true;
                ApplicationCommands.UserSetPasswordView.Enabled = UserHasRight("UserSetPasswordView.edit");
            }

            ApplicationCommands.LogView.Enabled = UserHasRight("LogView.edit");
            ApplicationCommands.SerialNumbersView.Enabled = UserHasRight("SerialNumbersView.edit");
            ApplicationCommands.InstallLicenseWizard.Enabled = UserHasRight("mn.InstallLicense");
            ApplicationCommands.SerialBuy.Enabled = UserHasRight("mn.SerialNumbersWebshop");

            ApplicationCommands.UpdateFromWww.Enabled = UserHasRight("mn.AutoUpdateInternet");
            ApplicationCommands.UpdateFromFolder.Enabled = UserHasRight("mn.AutoUpdateFolder");
            ApplicationCommands.CopyWWWProgramUpdateFilesView.Enabled = UserHasRight("CopyWWWProgramUpdateFilesView.edit");
            ApplicationCommands.OptionsView.Enabled = UserHasRight("OptionsView.edit");

            //
            // Hilfe
            //
#if !DEBUG
            //
            // Debug
            //

            if (ribbon.TabPages.Count == NumTabPages)
            {
                //
                // Wenn man diese Funktion mehrmals aufruft, darf das Debug Menu trotzdem nur einmal entfernt werden!
                // daher merken wir uns die Anzahl am Anfang und entfernen nur einen, wenn es soviele sind.
                // Beim zweiten Aufrtuf wurde das Hilfe Menü entfernt!!!
                //
                // Wenn man nur .Enabled auf false setzt, bleibt irgendwie Platz im Menü erhalten und es scrollt plötzlich so komisch!
                ribbon.TabPages.RemoveAt(ribbon.TabPages.Count - 1);
                // cmdClientServerViewImport.Visible = cmdClientServerViewExport.Visible = false;
            }
#endif

        }

        private void OperationenLogbuchView_Load(object sender, EventArgs e)
        {
            EnableRibbonMenu();

            string title = AppTitle() + " - [";
            //if (...)
            //{
            //    title = title + "Admin: ";
            //}
            title = title + BusinessLayer.CurrentUser_Nachname + "]";
            this.Text = title;
        }

        private void llblWWW_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LaunchInternetBrowser(BusinessLayer.UrlHomepageForDisplay);
        }

        internal void CreateEigeneDateienCustomMenu()
        {
            _CreateEigeneDateienGui();
        }

        private bool HasUpdate(bool fromInternet, bool showErrorMessage, out int fileSizeKb, out string newProgramName, out bool hasUpdate)
        {
            bool success = false;

            hasUpdate = false;
            fileSizeKb = 0;
            newProgramName = "";
            string versionInfo = "";
            string localVersionFile = null;
            string versionFileName = null;

            if (fromInternet && ("1" == BusinessLayer.GetUserSettingsString(GlobalConstants.SectionProgram, GlobalConstants.KeyProgramCopyUpdateFiles)))
            {
                localVersionFile = BusinessLayer.GetUserSettingsString(GlobalConstants.SectionProgram, GlobalConstants.KeyProgramAutoUpdateLocalFolder)
                    + System.IO.Path.DirectorySeparatorChar + BusinessLayer.VERSION_DOWNLOAD_FILENAME;
            }
            else
            {
                // no file needed!
                localVersionFile = null;
            }

            if (fromInternet)
            {
                success = DownloadAsciiInfoFile(
                    BusinessLayer.UrlHomepageForDownload + "/download/" + BusinessLayer.VERSION_DOWNLOAD_FILENAME,
                    showErrorMessage,
                    localVersionFile,
                    out versionInfo);
            }
            else
            {
                StreamReader reader = null;
                try
                {
                    versionFileName = BusinessLayer.GetUserSettingsString(GlobalConstants.SectionProgram, GlobalConstants.KeyProgramAutoUpdateLocalFolder)
                        + System.IO.Path.DirectorySeparatorChar + BusinessLayer.VERSION_DOWNLOAD_FILENAME;
                    if (File.Exists(versionFileName))
                    {
                        reader = new StreamReader(versionFileName);
                        versionInfo = reader.ReadLine();
                        reader.Close();
                        reader = null;
                        success = true;
                    }
                    else
                    {
                        if (showErrorMessage)
                        {
                            string msg = string.Format(GetText("err_missing_file"), versionFileName);
                            MessageBox(msg);
                        }
                    }
                }
                catch
                {
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Close();
                        reader = null;
                    }
                }
            }

            if (!success)
            {
                goto _exit;
            }

            if (versionInfo == null)
            {
                if (showErrorMessage)
                {
                    MessageBox(string.Format(GetText("err_version_file"), versionFileName));
                }
                goto _exit;
            }

            versionInfo = versionInfo.Trim();
            string[] arVersionInfo = versionInfo.Split('|');
            if (arVersionInfo.Length != 3)
            {
                if (showErrorMessage)
                {
                    MessageBox(string.Format(GetText("err_version_file"), versionFileName));
                }
                goto _exit;

            }

            // version.txt enthaelt: "1.7.3|1013|operationen-logbuch-V1.7.3.exe"
            string version = arVersionInfo[0];
            string[] arVersion = version.Split('.');
            if ((arVersion.Length != 3) || !Int32.TryParse(arVersionInfo[1], out fileSizeKb))
            {
                if (showErrorMessage)
                {
                    MessageBox(string.Format(GetText("err_version_file"), versionFileName));
                }
                goto _exit;
            }

            int majorVersion;
            int minorVersion;
            int release;

            if (!(Int32.TryParse(arVersion[0], out majorVersion)
                && Int32.TryParse(arVersion[1], out minorVersion)
                && Int32.TryParse(arVersion[2], out release)))
            {
                if (showErrorMessage)
                {
                    MessageBox(string.Format(GetText("err_version_file"), versionFileName));
                }
                goto _exit;
            }
         
            if (majorVersion == BusinessLayer.VersionMajor)
            {
                if ((minorVersion > BusinessLayer.VersionMinor)
                    || ((minorVersion == BusinessLayer.VersionMinor) && (release > BusinessLayer.ReleaseMajor)))
                {
                    newProgramName = arVersionInfo[2];
                    hasUpdate = true;
                }
            }

        _exit:
            return success;
        }

        private void AutoUpdateInternet(bool showUptodateMessage)
        {
            if (UserHasRight("mn.AutoUpdateInternet"))
            {
                AutoUpdateCommon(true, showUptodateMessage);
            }
        }

        private void AutoUpdateFolder(bool showUptodateMessage)
        {
            if (UserHasRight("mn.AutoUpdateFolder"))
            {
                AutoUpdateCommon(false, showUptodateMessage);
            }
        }

        private bool CopySetupToLocalFolderIfRequired(bool fromInternet, string folder, string fileName)
        {
            bool success = false;

            if (fromInternet)
            {
                if ("1" == BusinessLayer.GetUserSettingsString(GlobalConstants.SectionProgram, GlobalConstants.KeyProgramCopyUpdateFiles))
                {
                    string src = folder + System.IO.Path.DirectorySeparatorChar + fileName;
                    string dst = BusinessLayer.GetUserSettingsString(GlobalConstants.SectionProgram, GlobalConstants.KeyProgramAutoUpdateLocalFolder)
                        + System.IO.Path.DirectorySeparatorChar
                        + fileName;
                    try
                    {
                        BusinessLayer.CopyFile(src, dst, true, false);
                        success = true;
                    }
                    catch
                    {
                    }
                }
            }
            else
            {
                success = true;
            }

            return success;
        }

        private void AutoUpdateCommon(bool fromInternet, bool showUptodateMessage)
        {
            int fileSizeKb;
            string newProgramName;
            bool fileDownloaded = false;

            bool hasUpdate;

            if (!HasUpdate(fromInternet, showUptodateMessage, out fileSizeKb, out newProgramName, out hasUpdate))
            {
                if (showUptodateMessage)
                {
                    MessageBox(GetText("updateErr1"));
                }
                goto _exit;
            }

            if (!hasUpdate)
            {
                if (showUptodateMessage)
                {
                    MessageBox(GetText("updateOk"));
                }
                goto _exit;
            }

            string text = string.Format(CultureInfo.InvariantCulture, GetText("confirmUpgrade2"), AppTitle());
            if (!Confirm(text))
            {
                goto _exit;
            }

            string setupFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string setupFilename = setupFolder + System.IO.Path.DirectorySeparatorChar + newProgramName;

            if (fromInternet)
            {
                text = GetText("title3");
                fileDownloaded = DownloadFile(
                    text,
                    fileSizeKb,
                    BusinessLayer.UrlHomepageForDownload + "/download/" + newProgramName,
                    setupFilename);
            }
            else
            {
                try
                {
                    string updateFilename = BusinessLayer.GetUserSettingsString(GlobalConstants.SectionProgram, GlobalConstants.KeyProgramAutoUpdateLocalFolder)
                        + System.IO.Path.DirectorySeparatorChar + newProgramName;
                    if (BusinessLayer.CopyFile(updateFilename, setupFilename, false, false))
                    {
                        fileDownloaded = true;
                    }
                }
                catch
                {
                }
            }

            if (fileDownloaded)
            {
                CopySetupToLocalFolderIfRequired(fromInternet, setupFolder, newProgramName);

                try
                {
                    Process.Start(setupFilename);
                    Application.Exit();
                }
                catch (Exception exception)
                {
                    AppFramework.Debugging.DebugLogging.Write(AppFramework.Debugging.DebugLogging.DebugFlagInfo, exception);
                }
            }

        _exit: ;
        }

        private void UpdateAfterShownStuff()
        {
            if ("1" == BusinessLayer.GetUserSettingsString(GlobalConstants.SectionProgram, GlobalConstants.KeyProgramAutoUpdate))
            {
                if (UserHasRight("mn.AutoUpdateInternet"))
                {
#if USE_SPLASHSCREEN
                    _splashScreen.SetText(GetText("splashScreen_autoupdateInternet"));
#endif
                    AutoUpdateInternet(false);
                }
            }
            else if ("1" == BusinessLayer.GetUserSettingsString(GlobalConstants.SectionProgram, GlobalConstants.KeyProgramAutoUpdateLocal))
            {
                if (UserHasRight("mn.AutoUpdateFolder"))
                {
#if USE_SPLASHSCREEN
                    _splashScreen.SetText(GetText("splashScreen_autoupdateFolder"));
#endif
                    AutoUpdateFolder(false);
                }
            }
        }

        private void OperationenLogbuchView_Shown(object sender, EventArgs e)
        {
            XableAllButtonsForLongOperation(false);

            timer.Interval = 1000;
            timer.Start();
        }

        private bool PerformAutoImport()
        {
            bool success = false;

            string path = BusinessLayer.GetUserSettingsString(GlobalConstants.SectionOpImport, GlobalConstants.KeyOpImportPath);

            if (Directory.Exists(path))
            {
                AutoImportView dlg = new AutoImportView(BusinessLayer, path);
                dlg.ShowDialog();
                success = dlg.Success;
            }

            return success;
        }

        private void LoadRibbonStateForCurrentUser()
        {
            try
            {
                byte[] blob = null;

                DataRow dataRow = BusinessLayer.GetUserSettings(BusinessLayer.CurrentUser_ID_Chirurgen, GlobalConstants.SectionRibbon, GlobalConstants.KeyRibbonQat);
                if ((dataRow != null) && (dataRow["Blob"] != DBNull.Value))
                {
                    blob = (byte[])dataRow["Blob"];
#if DEBUG
                    string s = System.Text.Encoding.ASCII.GetString(blob);
#endif
                    if (blob != null)
                    {
                        using (MemoryStream stream = new MemoryStream(blob))
                        {
                            Elegant.Ui.PersistentStateManager.Load(stream);
                        }
                    }
                }

                if (blob == null)
                {
                    //
                    // Default
                    //
                    Elegant.Ui.PersistentStateManager.LoadFromIsolatedStorageForDomain();
                }
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message);
            }
        }

        private void SaveRibbonStateForCurrentUser()
        {
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    Elegant.Ui.PersistentStateManager.Save(stream);
                    stream.Flush();
                    byte[] blob = stream.GetBuffer();
                    BusinessLayer.SetUserSettings(BusinessLayer.CurrentUser_ID_Chirurgen, GlobalConstants.SectionRibbon, GlobalConstants.KeyRibbonQat, "dummy", blob);
                }
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message);
            }
        }


        /// <summary>
        /// Invoke a menu which is passed along as a text
        /// </summary>
        /// <param name="command">The menu to click.</param>
        public void StartMenuItem(object linkData)
        {
            string command = linkData as string;

            if (command != null)
            {
                if (false)
                {
                    // damit man immer schön einsortieren kann
                }

                else if (command == Command_OperationenKatalogView)
                {
                    OpenWindow("OperationenKatalogView.edit", typeof(OperationenKatalogView));
                }

                else if (command == Command_PlanOperationenView)
                {
                    OpenWindow("PlanOperationenView.view", typeof(PlanOperationenView));
                }

                else if (command == Command_RichtlinienVergleichView)
                {
                    OpenWindow("RichtlinienVergleichView.edit", typeof(RichtlinienVergleichView));
                }
                else if (command == Command_PlanOperationVergleichView)
                {
                    OpenWindow("PlanOperationVergleichView.edit", typeof(PlanOperationVergleichView));
                }
                else if (command == Command_RichtlinienSollIstView)
                {
                    OpenWindow("RichtlinienSollIstView.view", typeof(RichtlinienSollIstView));
                }
                else if (command == Command_RichtlinienVergleichView)
                {
                    OpenWindow("RichtlinienVergleichView.edit", typeof(RichtlinienVergleichView));
                }
                else if (command == Command_NotizTypenView)
                {
                    OpenWindow("NotizTypenView.view", typeof(NotizTypenView));
                }
                else if (command == Command_ChirurgenFunktionenView)
                {
                    OpenWindow("ChirurgenFunktionenView.view", typeof(ChirurgenFunktionenView));
                }
                else if (command == Command_AbteilungenView)
                {
                    OpenWindow("AbteilungenView.view", typeof(AbteilungenView));
                }
                else if (command == Command_ImportOPSWizard)
                {
                    if (UserHasRight("ImportOPSWizard.edit"))
                    {
                        new ImportOPSWizard(BusinessLayer).ShowDialog();
                    }
                }
                else if (command == Command_SerialNumbersView)
                {
                    OpenWindow("SerialNumbersView.edit", typeof(SerialNumbersView));
                }
                else if (command == Command_SecGroupsView)
                {
                    OpenWindow("SecGroupsView.view", typeof(SecGroupsView));
                }
            }
        }

        private void aufNeueProgramversionAusEinemVerzeichnisÜberprüfenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoUpdateFolder(true);
        }

        private void ChangeCultureTo(string culture)
        {
            if (BusinessLayer.SetCurrentUICulture(culture))
            {
                ChangeLanguage(culture);
                CreateCommandTextVariables();
                SetRibbonScreentips();
                SetDetailsAndHeight();
            }

            //
            // Application.Restart(); darf man nicht verwenden!
            // Wenn man den aktuellen Pfad verstellt, indem man einen FileOpen Dialog öffnet,
            // findet das Programm die Datenbank nicht mehr.
            //
            //Application.Restart();
        }

        private void lblLogo_Click(object sender, EventArgs e)
        {
            LaunchInternetBrowser(BusinessLayer.UrlHomepageForDisplay);
        }

        private void pbLogo_Click(object sender, EventArgs e)
        {
            LaunchInternetBrowser(BusinessLayer.UrlHomepageForDisplay);
        }

        private void llOperationenKatalogView_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenWindow("OperationenKatalogView.edit", typeof(OperationenKatalogView));
        }

        private void llRichtlinienOpsKodeUnassignedView_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenWindow("RichtlinienOpsKodeUnassignedView.edit", typeof(RichtlinienOpsKodeUnassignedView));
        }

        private void llRichtlinienView_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenWindow("RichtlinienView.view", typeof(RichtlinienView));
        }

        private void llOperationenImportView_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenWindow("OperationenImportView.edit", typeof(OperationenImportView));
        }

        private void llOperationenEditView_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenWindow("OperationenEditView.edit", typeof(OperationenEditView));
        }

        private void llOperationenView_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenWindow("OperationenView.edit", typeof(OperationenView));
        }

        private void llRichtlinienVergleichView_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenWindow("RichtlinienVergleichView.edit", typeof(RichtlinienVergleichView));
        }

        private void llRichtlinienVergleichOverviewView_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenWindow("RichtlinienVergleichOverviewView.edit", typeof(RichtlinienVergleichOverviewView));
        }

        void RichtlinienView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("RichtlinienView.view", typeof(RichtlinienView));
        }

        /// <summary>
        /// If a file with the name main_window.jpg is found in the root folder, then this is used as the graphics for the main window.
        /// </summary>
        private void LoadMainPicture()
        {
            Image image = null;

            try
            {
                string secretFileName = BusinessLayer.AppPath + Path.DirectorySeparatorChar + "main_window.jpg";
                if (File.Exists(secretFileName))
                {
                    image = Image.FromFile(secretFileName);
                }
            }
            catch
            {
                image = null;
            }

            /*
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream("Operationen.Resources.Hauptfenster_unten.jpg");

            image = Image.FromStream(stream);
             */

            if (image != null)
            {
                pbMain.Image = image;
            }
        }


        // Das Menaue Dokumente wird aus der Datenbank gefuellt
        // Jeder Eintrag aus Tabelle DateiTypen ist ein Untermenuepunkt
        // und enthaelt alle Eintraege aus Tabelle Dateien von diesem Typ.
        private void _CreateEigeneDateienGui()
        {
            Elegant.Ui.PopupMenu popupMain = new Elegant.Ui.PopupMenu();
            ddEigeneDateien.Popup = popupMain;
            
            DataView dateiTypen = BusinessLayer.GetDateiTypen();

            foreach (DataRow dateiTyp in dateiTypen.Table.Rows)
            {
                Elegant.Ui.DropDown submenu = new Elegant.Ui.DropDown();
                submenu.Text = (string)dateiTyp["DateiTyp"];
                Elegant.Ui.PopupMenu popupSubmenu = new Elegant.Ui.PopupMenu();
                submenu.Popup = popupSubmenu;

                DataView dateien = BusinessLayer.GetDateien(ConvertToInt32(dateiTyp["ID_DateiTypen"]));


                foreach (DataRow datei in dateien.Table.Rows)
                {
                    Elegant.Ui.Button button = new Elegant.Ui.Button();
                    button.Text = (string)datei["Beschreibung"] + "...";
                    button.Tag = ConvertToInt32(datei["ID_Dateien"]);
                    button.Click += new EventHandler(EigeneDateien_Click);

                    popupSubmenu.Items.Add(button);
                }

                if (dateien.Table.Rows.Count == 0)
                {
                    // Es gab keine Dateien, also einen erklärenden Eintrag erzeugen
                    Elegant.Ui.Button item = new Elegant.Ui.Button();
                    item.Text = GetText("KeineEigenen");
                    SetScreenTip(item, "mnItem_EigeneDateien",
                        string.Format(CultureInfo.InvariantCulture, GetText("mnItem_EigeneDateien"),
                        Command_DateienView));
                    popupSubmenu.Items.Add(item);
                }

                popupMain.Items.Add(submenu);
            }

            if (dateiTypen.Table.Rows.Count == 0)
            {
                // Es gab keine Dateien, also einen erklärenden Eintrag erzeugen
                Elegant.Ui.Button item = new Elegant.Ui.Button();
                item.Text = GetText("KeineEigenen");
                SetScreenTip(item, "mnItem_EigeneDateien",
                    string.Format(CultureInfo.InvariantCulture, GetText("mnItem_EigeneDateien"),
                    Command_DateienView));
                popupMain.Items.Add(item);
            }
        }

        private void CreateVideoTutorialGui()
        {
            Elegant.Ui.PopupMenu popupMain = new Elegant.Ui.PopupMenu();
            ddVideoTutorials.Popup = popupMain;

            for (int i = 1; i <= 5; i++)
            {
                string text = GetText("videoTutorial" + i);
                string url = GetText("videoTutorial" + i + "Url");

                Elegant.Ui.Button button = new Elegant.Ui.Button();
                button.Text = text;
                button.Tag = url;
                button.Click += new EventHandler(VideoTutorial_Click);

                popupMain.Items.Add(button);
            }
        }

        void VideoTutorial_Click(object sender, EventArgs e)
        {
            Elegant.Ui.Button button = (Elegant.Ui.Button)sender;
            string url = (string)button.Tag;
            LaunchInternetBrowser(url);
        }

        private void SetScreenTip(Elegant.Ui.Control control, string id, string text)
        {
            string idCaption = id + "_caption";

            string caption = GetText(idCaption);
            if (string.IsNullOrEmpty(caption) || caption.Equals(idCaption))
            {
                control.ScreenTip.Caption = control.Text;
            }
            else
            {
                control.ScreenTip.Caption = caption;
            }

            control.ScreenTip.Text = text;
        }

        private void SetScreenTip(Elegant.Ui.Control control, string id)
        {
            string idCaption = id + "_caption";

            string caption = GetText(idCaption);
            if (string.IsNullOrEmpty(caption) || caption.Equals(idCaption))
            {
                control.ScreenTip.Caption = control.Text;
            }
            else
            {
                control.ScreenTip.Caption = caption;
            }

            control.ScreenTip.Text = GetText(id);
        }

        void EigeneDateien_Click(object sender, EventArgs e)
        {
            Elegant.Ui.Button button = (Elegant.Ui.Button)sender;

            int ID_Dateien = (int)button.Tag;

            DataRow datei = BusinessLayer.GetDatei(ID_Dateien);

            string fileName = (string)datei["DateiName"];

            launchFile(fileName);
        }

        void DateiTypenView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("DateiTypenView.view", typeof(DateiTypenView));
        }

        void DateienView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            if (UserHasRight("DateienView.edit"))
            {
                new DateienView(BusinessLayer).ShowDialog();
                CreateEigeneDateienCustomMenu();
            }
        }


#if DEBUG
        private void dEBUGReloadRightsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BusinessLayer.ReloadUserRights();
            EnableRibbonMenu();
        }
#endif

        void AbteilungenView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("AbteilungenView.view", typeof(AbteilungenView));
        }

        void AkademischeAusbildungTypenView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("AkademischeAusbildungTypenView.view", typeof(AkademischeAusbildungTypenView));
        }

        void NotizTypenView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("NotizTypenView.view", typeof(NotizTypenView));
        }

        void ChirurgenFunktionenView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("ChirurgenFunktionenView.view", typeof(ChirurgenFunktionenView));
        }

        void PrintIstOperationen_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            if (UserHasRight("PrintIstOperationen.edit"))
            {
                int nID_Chirurgen = SelectChirurg(true);

                if (nID_Chirurgen != -1)
                {
                    DataRow oChirurg = BusinessLayer.GetChirurg(nID_Chirurgen);

                    PrintIstOperationen dlg = new PrintIstOperationen(BusinessLayer, oChirurg, "", "");
                    dlg.ShowDialog();
                }
            }
        }

        void OptionsView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            if (UserHasRight("OptionsView.edit"))
            {
                OptionsView dlg = new OptionsView(BusinessLayer);
                dlg.ShowDialog();
            }
        }

        void Exit_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            this.Close();
        }

        void GebieteView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("GebieteView.view", typeof(GebieteView));
        }

        void DokumenteView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("DokumenteView.edit", typeof(DokumenteView));
        }

        void OperationenKatalogView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("OperationenKatalogView.edit", typeof(OperationenKatalogView));
        }

        private void CreateCommands()
        {
            //
            // Start
            //
            ApplicationCommands.PrintIstOperationen.Executed += new Elegant.Ui.CommandExecutedEventHandler(PrintIstOperationen_Executed);
            ApplicationCommands.Exit.Executed += new Elegant.Ui.CommandExecutedEventHandler(Exit_Executed);
            ApplicationCommands.OptionsView.Executed += new Elegant.Ui.CommandExecutedEventHandler(OptionsView_Executed);

            //
            // Eigene Dateien
            //
            ApplicationCommands.DateiTypenView.Executed += new Elegant.Ui.CommandExecutedEventHandler(DateiTypenView_Executed);
            ApplicationCommands.DateienView.Executed += new Elegant.Ui.CommandExecutedEventHandler(DateienView_Executed);

            //
            // Offizielle Dokumente
            //
            ApplicationCommands.RichtlinienView.Executed += new Elegant.Ui.CommandExecutedEventHandler(RichtlinienView_Executed);
            ApplicationCommands.OperationenKatalogView.Executed += new Elegant.Ui.CommandExecutedEventHandler(OperationenKatalogView_Executed);
            ApplicationCommands.DokumenteView.Executed += new Elegant.Ui.CommandExecutedEventHandler(DokumenteView_Executed);
            ApplicationCommands.GebieteView.Executed += new Elegant.Ui.CommandExecutedEventHandler(GebieteView_Executed);
            ApplicationCommands.BaekWbOrdnung.Executed += new Elegant.Ui.CommandExecutedEventHandler(BaekWbOrdnung_Executed);
            ApplicationCommands.BaekWbRichtlinien.Executed += new Elegant.Ui.CommandExecutedEventHandler(BaekWbRichtlinien_Executed);

            //
            // Verwaltung
            //
            ApplicationCommands.ChirurgenFunktionenView.Executed += new Elegant.Ui.CommandExecutedEventHandler(ChirurgenFunktionenView_Executed);
            ApplicationCommands.NotizTypenView.Executed += new Elegant.Ui.CommandExecutedEventHandler(NotizTypenView_Executed);
            ApplicationCommands.AkademischeAusbildungTypenView.Executed += new Elegant.Ui.CommandExecutedEventHandler(AkademischeAusbildungTypenView_Executed);
            ApplicationCommands.AbteilungenView.Executed += new Elegant.Ui.CommandExecutedEventHandler(AbteilungenView_Executed);
            ApplicationCommands.AbteilungenChirurgenView.Executed += new Elegant.Ui.CommandExecutedEventHandler(AbteilungenChirurgenView_Executed);
            ApplicationCommands.WeiterbilderChirurgenView.Executed += new Elegant.Ui.CommandExecutedEventHandler(WeiterbilderChirurgenView_Executed);
            ApplicationCommands.ChirurgNew.Executed += new Elegant.Ui.CommandExecutedEventHandler(ChirurgNew_Executed);
            ApplicationCommands.ChirurgEdit.Executed += new Elegant.Ui.CommandExecutedEventHandler(ChirurgEdit_Executed);
            ApplicationCommands.ChirurgDelete.Executed += new Elegant.Ui.CommandExecutedEventHandler(ChirurgDelete_Executed);
            ApplicationCommands.OperationenSummaryView.Executed += new Elegant.Ui.CommandExecutedEventHandler(OperationenSummaryView_Executed);

            //
            // Stammdaten
            //
            ApplicationCommands.RichtlinienSollIstView.Executed += new Elegant.Ui.CommandExecutedEventHandler(RichtlinienSollIstView_Executed);

            //
            // Bearbeiten
            //
            ApplicationCommands.OperationenEditView.Executed += new Elegant.Ui.CommandExecutedEventHandler(OperationenEditView_Executed);
            ApplicationCommands.KommentareView.Executed += new Elegant.Ui.CommandExecutedEventHandler(KommentareView_Executed);
            ApplicationCommands.NotizenView.Executed += new Elegant.Ui.CommandExecutedEventHandler(NotizenView_Executed);
            ApplicationCommands.AkademischeAusbildungView.Executed += new Elegant.Ui.CommandExecutedEventHandler(AkademischeAusbildungView_Executed);
            ApplicationCommands.PlanOperationenView.Executed += new Elegant.Ui.CommandExecutedEventHandler(PlanOperationenView_Executed);
            ApplicationCommands.ChirurgDokumenteView.Executed += new Elegant.Ui.CommandExecutedEventHandler(ChirurgDokumenteView_Executed);
            ApplicationCommands.RichtlinienOpsKodeUnassignedView.Executed += new Elegant.Ui.CommandExecutedEventHandler(RichtlinienOpsKodeUnassignedView_Executed);
            ApplicationCommands.ChirurgenRichtlinienView.Executed += new Elegant.Ui.CommandExecutedEventHandler(ChirurgenRichtlinienView_Executed);

            //
            // Auswertungen
            //
            ApplicationCommands.OperationenView.Executed += new Elegant.Ui.CommandExecutedEventHandler(OperationenView_Executed);
            ApplicationCommands.OperationenZeitenVergleichView.Executed += new Elegant.Ui.CommandExecutedEventHandler(OperationenZeitenVergleichView_Executed);
            ApplicationCommands.OPDauerFortschrittView.Executed += new Elegant.Ui.CommandExecutedEventHandler(OPDauerFortschrittView_Executed);
            ApplicationCommands.ChirurgOperationenView.Executed += new Elegant.Ui.CommandExecutedEventHandler(ChirurgOperationenView_Executed);
            ApplicationCommands.GesamtOperationenView.Executed += new Elegant.Ui.CommandExecutedEventHandler(GesamtOperationenView_Executed);
            ApplicationCommands.OperationenVergleichView.Executed += new Elegant.Ui.CommandExecutedEventHandler(OperationenVergleichView_Executed);
            ApplicationCommands.PlanOperationVergleichIstView.Executed += new Elegant.Ui.CommandExecutedEventHandler(PlanOperationVergleichIstView_Executed);
            ApplicationCommands.PlanOperationVergleichView.Executed += new Elegant.Ui.CommandExecutedEventHandler(PlanOperationVergleichView_Executed);
            ApplicationCommands.RichtlinienVergleichView.Executed += new Elegant.Ui.CommandExecutedEventHandler(RichtlinienVergleichView_Executed);
            ApplicationCommands.RichtlinienVergleichOverviewView.Executed += new Elegant.Ui.CommandExecutedEventHandler(RichtlinienVergleichOverviewView_Executed);
            ApplicationCommands.KlinischeErgebnisseView.Executed += new Elegant.Ui.CommandExecutedEventHandler(KlinischeErgebnisseView_Executed);

            //
            // Extras
            //
            ApplicationCommands.UserChangePasswordView.Executed += new Elegant.Ui.CommandExecutedEventHandler(UserChangePasswordView_Executed);
            ApplicationCommands.UserSetPasswordView.Executed += new Elegant.Ui.CommandExecutedEventHandler(UserSetPasswordView_Executed);
            ApplicationCommands.LogView.Executed += new Elegant.Ui.CommandExecutedEventHandler(LogView_Executed);
            ApplicationCommands.SerialNumbersView.Executed += new Elegant.Ui.CommandExecutedEventHandler(SerialNumbersView_Executed);
            ApplicationCommands.UpdateFromFolder.Executed += new Elegant.Ui.CommandExecutedEventHandler(UpdateFromFolder_Executed);
            ApplicationCommands.SerialBuy.Executed += new Elegant.Ui.CommandExecutedEventHandler(SerialBuy_Executed);
            ApplicationCommands.InstallLicenseWizard.Executed += new Elegant.Ui.CommandExecutedEventHandler(InstallLicenseWizard_Executed);
            ApplicationCommands.UpdateFromWww.Executed += new Elegant.Ui.CommandExecutedEventHandler(UpdateFromWww_Executed);
            ApplicationCommands.CopyWWWProgramUpdateFilesView.Executed += new Elegant.Ui.CommandExecutedEventHandler(CopyWWWProgramUpdateFilesView_Executed);
            ApplicationCommands.SecGroupsView.Executed += new Elegant.Ui.CommandExecutedEventHandler(SecGroupsView_Executed);
            ApplicationCommands.SecGroupsChirurgenView.Executed += new Elegant.Ui.CommandExecutedEventHandler(SecGroupsChirurgenView_Executed);
            ApplicationCommands.SecGroupsSecRightsView.Executed += new Elegant.Ui.CommandExecutedEventHandler(SecGroupsSecRightsView_Executed);
            ApplicationCommands.SecUserOverviewView.Executed += new Elegant.Ui.CommandExecutedEventHandler(SecUserOverviewView_Executed);
            // Gibt es schon einmal oben ApplicationCommands.OptionsView.Executed

            //
            // Import
            //
            ApplicationCommands.ImportChirurgenExcludeView.Executed += new Elegant.Ui.CommandExecutedEventHandler(ImportChirurgenExcludeView_Executed);
            ApplicationCommands.OperationenImportView.Executed += new Elegant.Ui.CommandExecutedEventHandler(OperationenImportView_Executed);
            ApplicationCommands.ImportRichtlinienWizard.Executed += new Elegant.Ui.CommandExecutedEventHandler(ImportRichtlinienWizard_Executed);
            ApplicationCommands.ImportRichtlinienZuordnungWizard.Executed += new Elegant.Ui.CommandExecutedEventHandler(ImportRichtlinienZuordnungWizard_Executed);
            ApplicationCommands.ImportChirurgWizard.Executed += new Elegant.Ui.CommandExecutedEventHandler(ImportChirurgWizard_Executed);
            ApplicationCommands.ImportOperationenMobileWizard.Executed += new Elegant.Ui.CommandExecutedEventHandler(ImportOperationenMobileWizard_Executed);
            ApplicationCommands.ImportOPSWizard.Executed += new Elegant.Ui.CommandExecutedEventHandler(ImportOPSWizard_Executed);
            ApplicationCommands.ExecAutoImport.Executed += new Elegant.Ui.CommandExecutedEventHandler(ExecAutoImport_Executed);

            // 62 Menüs ohne export

            //
            // Export
            //
            ApplicationCommands.ExportRichtlinienWizard.Executed += new Elegant.Ui.CommandExecutedEventHandler(ExportRichtlinienWizard_Executed);
            ApplicationCommands.ExportRichtlinienZuordnungWizard.Executed += new Elegant.Ui.CommandExecutedEventHandler(ExportRichtlinienZuordnungWizard_Executed);
            ApplicationCommands.ExportChirurgWizard.Executed += new Elegant.Ui.CommandExecutedEventHandler(ExportChirurgWizard_Executed);
            ApplicationCommands.ExportOperationenKatalogView.Executed += new Elegant.Ui.CommandExecutedEventHandler(ExportOperationenKatalogView_Executed);

            //
            // Hilfe
            //
            ApplicationCommands.HelpChm.Executed += new Elegant.Ui.CommandExecutedEventHandler(HelpChm_Executed);
            ApplicationCommands.WwwHelp.Executed += new Elegant.Ui.CommandExecutedEventHandler(WwwHelp_Executed);
            ApplicationCommands.WwwHome.Executed += new Elegant.Ui.CommandExecutedEventHandler(WwwHome_Executed);
            ApplicationCommands.AboutBox.Executed += new Elegant.Ui.CommandExecutedEventHandler(AboutBox_Executed);
        
            //
            // Debug
            //
            ApplicationCommands.LangDeDe.Executed += new Elegant.Ui.CommandExecutedEventHandler(LangDeDe_Executed);
            ApplicationCommands.LangEnUs.Executed += new Elegant.Ui.CommandExecutedEventHandler(LangEnUs_Executed);

            ApplicationCommands.DebugSql.Executed += new Elegant.Ui.CommandExecutedEventHandler(DebugSql_Executed);
            ApplicationCommands.DebugExportOperationen.Executed += new Elegant.Ui.CommandExecutedEventHandler(DebugExportOperationen_Executed);
            ApplicationCommands.UpdateSerialnumbersView.Executed += new Elegant.Ui.CommandExecutedEventHandler(UpdateSerialnumbersView_Executed);
            ApplicationCommands.DebugReloadUserRights.Executed += new Elegant.Ui.CommandExecutedEventHandler(DebugReloadUserRights_Executed);
            ApplicationCommands.ClientServerView.Executed += new Elegant.Ui.CommandExecutedEventHandler(ClientServerView_Executed);
            ApplicationCommands.ZeitraeumeView.Executed += new Elegant.Ui.CommandExecutedEventHandler(ZeitraeumeView_Executed);
            

        }

        void OperationenSummaryView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("OperationenSummaryView.view", typeof(OperationenSummaryView));
        }

        void RichtlinienSollIstView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow(typeof(RichtlinienSollIstView));
        }
        

        void DebugReloadUserRights_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
#if DEBUG
            BusinessLayer.ReloadUserRights();
            EnableRibbonMenu();
#endif
        }

        void ClientServerView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow(typeof(ClientServerView));
        }

        void ZeitraeumeView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow(typeof(ZeitraeumeView));
        }

        void UpdateSerialnumbersView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
#if DEBUG
            UpdateSerialnumbersView dlg = new UpdateSerialnumbersView(BusinessLayer);
            dlg.ShowDialog();
#endif
        }

        void DebugExportOperationen_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
#if DEBUG
            string fileName = "c:\\operationen.txt";

            BusinessLayer.ExportOperationenToTextFileOrbis(fileName);
            launchFileDirect(fileName);
#endif
        }

        void DebugSql_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
#if DEBUG
            OpenWindow(typeof(SQLBox));
#endif
        }

        void AboutBox_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            new AboutBox(BusinessLayer).ShowDialog();
        }

        void WwwHome_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            LaunchInternetBrowser(BusinessLayer.UrlHomepageForDisplay);
        }

        void HelpChm_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            try
            {
                string fileName = BusinessLayer.PathApplication + System.IO.Path.DirectorySeparatorChar + "operationen.chm";
                if (File.Exists(fileName))
                {
                    System.Diagnostics.Process.Start("operationen.chm");
                }
                else
                {
                    MessageBox(GetTextControlMissingFile(fileName));
                }
            }
            catch
            {
            }
        }

        void WwwHelp_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            LaunchInternetBrowser(BusinessLayer.UrlHomepageForDownload + "/help/operationen_help.html");
        }

        void ExportOperationenKatalogView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("ExportOperationenKatalogView.edit", typeof(ExportOperationenKatalogView));
        }

        void ExportChirurgWizard_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            if (UserHasRight("cmd.ExportChirurg"))
            {
                if (ConfirmAndCloseAllOpenWindows(Command_ExportChirurgWizard))
                {
                    ExportChirurgWizard w = new ExportChirurgWizard(BusinessLayer);
                    w.ShowDialog();
                }
            }
        }

        void ExportRichtlinienZuordnungWizard_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            if (UserHasRight("cmd.ExportZuordnungen"))
            {
                if (ConfirmAndCloseAllOpenWindows(Command_ExportRichtlinienZuordnungWizard))
                {
                    ExportRichtlinienZuordnungWizard w = new ExportRichtlinienZuordnungWizard(BusinessLayer);
                    w.ShowDialog();
                }
            }
        }

        void ExportRichtlinienWizard_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            if (UserHasRight("cmd.ExportRichtlinien"))
            {
                if (ConfirmAndCloseAllOpenWindows(Command_ExportRichtlinienWizard))
                {
                    ExportRichtlinienWizard w = new ExportRichtlinienWizard(BusinessLayer);
                    w.ShowDialog();
                }
            }
        }

        void ExecAutoImport_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            if (UserHasRight("mn.ImportAutoImport"))
            {
                string text = string.Format(CultureInfo.InvariantCulture, GetText("confirm_autoImport1"), Command_OptionsView_tabImport);

                if (Confirm(text))
                {
                    bool success = PerformAutoImport();

                    string path = BusinessLayer.GetUserSettingsString(GlobalConstants.SectionOpImport, GlobalConstants.KeyOpImportPath);

                    if (success)
                    {
                        text = string.Format(CultureInfo.InvariantCulture, GetText("msg_importSuccess1"), 
                            path + System.IO.Path.DirectorySeparatorChar + BusinessLayer.AutoImportProcessedDirectory);
                        MessageBox(text);
                    }
                    else
                    {
                        text = string.Format(CultureInfo.InvariantCulture, GetText("msg_importError1"), 
                            path + System.IO.Path.DirectorySeparatorChar + BusinessLayer.AutoImportProcessedDirectory);
                        MessageBox(text);
                    }
                }
            }
        }

        void ImportOPSWizard_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            if (UserHasRight("ImportOPSWizard.edit"))
            {
                if (ConfirmAndCloseAllOpenWindows(Command_OperationenImportView))
                {
                    ImportOPSWizard w = new ImportOPSWizard(BusinessLayer);
                    w.ShowDialog();
                }
            }
        }

        void ImportOperationenMobileWizard_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            if (UserHasRight("ImportOperationenMobileWizard.edit"))
            {
                ImportOperationenMobileWizard dlg = new ImportOperationenMobileWizard(BusinessLayer);
                dlg.ShowDialog();
            }
        }

        void ImportChirurgWizard_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            if (UserHasRight("cmd.ImportChirurg"))
            {
                new ImportChirurgWizard(BusinessLayer).ShowDialog();
            }
        }

        void ImportRichtlinienZuordnungWizard_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            if (UserHasRight("cmd.ImportZuordnungen"))
            {
                if (ConfirmAndCloseAllOpenWindows(Command_ImportRichtlinienZuordnungWizard))
                {
                    ImportRichtlinienZuordnungWizard w = new ImportRichtlinienZuordnungWizard(BusinessLayer);
                    w.ShowDialog();
                }
            }
        }

        void ImportRichtlinienWizard_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            if (UserHasRight("cmd.ImportRichtlinien"))
            {
                if (ConfirmAndCloseAllOpenWindows(Command_ImportRichtlinienWizard))
                {
                    ImportRichtlinienWizard w = new ImportRichtlinienWizard(BusinessLayer);
                    w.ShowDialog();
                }
            }
        }

        void OperationenImportView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            if (UserHasRight("OperationenImportView.edit"))
            {
                if (ConfirmAndCloseAllOpenWindows(Command_OperationenImportView))
                {
                    OperationenImportView dlg = new OperationenImportView(BusinessLayer);
                    dlg.ShowDialog();
                }
            }
        }

        void ImportChirurgenExcludeView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            if (UserHasRight("ImportChirurgenExcludeView.edit"))
            {
                ImportChirurgenExcludeView dlg = new ImportChirurgenExcludeView(BusinessLayer);
                dlg.ShowDialog();
            }
        }

        void LangEnUs_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            ChangeCultureTo("en-US");
        }

        void LangDeDe_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            ChangeCultureTo("de-DE");
        }

        void SecUserOverviewView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("SecUserOverviewView.edit", typeof(SecUserOverviewView));
        }

        void SecGroupsSecRightsView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("SecGroupsSecRightsView.edit", typeof(SecGroupsSecRightsView));
        }

        void SecGroupsChirurgenView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("SecGroupsChirurgenView.edit", typeof(SecGroupsChirurgenView));
        }

        void SecGroupsView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("SecGroupsView.view", typeof(SecGroupsView));
        }

        void CopyWWWProgramUpdateFilesView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("CopyWWWProgramUpdateFilesView.edit", typeof(CopyWWWProgramUpdateFilesView));
        }

        void UpdateFromWww_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            if (UserHasRight("mn.AutoUpdateInternet"))
            {
                AutoUpdateInternet(true);
            }
        }

        void InstallLicenseWizard_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            if (UserHasRight("mn.InstallLicense"))
            {
                InstallLicenseWizard dlg = new InstallLicenseWizard(BusinessLayer);
                dlg.ShowDialog();
            }
        }

        void SerialBuy_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            if (UserHasRight("mn.SerialNumbersWebshop"))
            {
                LaunchInternetBrowser(BusinessLayer.UrlWebshop);
            }
        }

        void UpdateFromFolder_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            if (UserHasRight("mn.AutoUpdateFolder"))
            {
                AutoUpdateFolder(true);
            }
        }

        void SerialNumbersView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("SerialNumbersView.edit", typeof(SerialNumbersView));
        }

        void LogView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("LogView.edit", typeof(LogView));
        }

        void UserSetPasswordView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            if (UserHasRight("UserSetPasswordView.edit"))
            {
                UserSetPasswordView dlg = new UserSetPasswordView(BusinessLayer);
                dlg.ShowDialog();
            }
        }

        void UserChangePasswordView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            UserChangePasswordView dlg = new UserChangePasswordView(BusinessLayer);
            dlg.ShowDialog();
        }

        void KlinischeErgebnisseView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("KlinischeErgebnisseView.edit", typeof(KlinischeErgebnisseView));
        }

        void RichtlinienVergleichOverviewView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("RichtlinienVergleichOverviewView.edit", typeof(RichtlinienVergleichOverviewView));
        }

        void RichtlinienVergleichView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("RichtlinienVergleichView.edit", typeof(RichtlinienVergleichView));
        }

        void PlanOperationVergleichView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("PlanOperationVergleichView.edit", typeof(PlanOperationVergleichView));
        }

        void PlanOperationVergleichIstView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("PlanOperationVergleichIstView.edit", typeof(PlanOperationVergleichIstView));
        }

        void OperationenVergleichView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("OperationenVergleichView.edit", typeof(OperationenVergleichView));
        }

        void GesamtOperationenView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("GesamtOperationenView.edit", typeof(GesamtOperationenView));
        }

        void ChirurgOperationenView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            if (UserHasRight("ChirurgOperationenView.edit"))
            {
                ChirurgOverview();
            }
        }

        void OPDauerFortschrittView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("OPDauerFortschrittView.edit", typeof(OPDauerFortschrittView));
        }

        void OperationenZeitenVergleichView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("OperationenZeitenVergleichView.edit", typeof(OperationenZeitenVergleichView));
        }

        void OperationenView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("OperationenView.edit", typeof(OperationenView));
        }

        void ChirurgenRichtlinienView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("ChirurgenRichtlinienView.edit", typeof(ChirurgenRichtlinienView));
        }

        void RichtlinienOpsKodeUnassignedView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("RichtlinienOpsKodeUnassignedView.edit", typeof(RichtlinienOpsKodeUnassignedView));
        }

        void ChirurgDokumenteView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("ChirurgDokumenteView.edit", typeof(ChirurgDokumenteView));
        }

        void PlanOperationenView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("PlanOperationenView.view", typeof(PlanOperationenView));
        }

        void AkademischeAusbildungView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("AkademischeAusbildungView.edit", typeof(AkademischeAusbildungView));
        }

        void NotizenView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("NotizenView.edit", typeof(NotizenView));
        }

        void KommentareView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("KommentareView.edit", typeof(KommentareView));
        }

        void OperationenEditView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("OperationenEditView.edit", typeof(OperationenEditView));
        }

        void BaekWbRichtlinien_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            LaunchInternetBrowser("http://www.baek.de/page.asp?his=1.128.129");
        }

        void BaekWbOrdnung_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            LaunchInternetBrowser("http://www.baek.de/page.asp?his=1.128.129");
        }

        void ChirurgNew_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            if (UserHasRight("mn.ChirurgNew"))
            {
                ChirurgNew();
            }
        }
        void ChirurgEdit_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            if (UserHasRight("mn.ChirurgEdit"))
            {
                ChirurgEdit();
            }
        }
        void ChirurgDelete_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            if (UserHasRight("mn.ChirurgDelete"))
            {
                ChirurgDelete();
            }
        }

        void WeiterbilderChirurgenView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("WeiterbilderChirurgenView.edit", typeof(WeiterbilderChirurgenView));
        }

        void AbteilungenChirurgenView_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            OpenWindow("AbteilungenChirurgenView.edit", typeof(AbteilungenChirurgenView));
        }

        private void ribbon_CustomizeQuickAccessToolbar(object sender, Elegant.Ui.QuickAccessToolbarCustomizeEventArgs e)
        {
            QATCustomizationDialog d = new QATCustomizationDialog(BusinessLayer, ribbon);
            d.ShowDialog();
            e.Handled = true;
            SaveRibbonStateForCurrentUser();

            SetDetailsAndHeight();
        }

        private void SetDetailsAndHeight()
        {
            int height = 490;

            if (ribbon.QuickAccessToolbarPlacementMode == Elegant.Ui.QuickAccessToolbarPlacementMode.BelowRibbon)
            {
            }
            else
            {
            }

            this.Height = height;
        }

        protected override int GetHelpButtonPositionY()
        {
            // Das ribbon ändert irgendwie die this.Height

            //return base.GetHelpButtonPositionY();
            return this.Height - 32;
        }

        public string GetSecurityRightDescription(string name)
        {
            string description;

            name = name.ToLower();

            switch (name)
            {
                case "abteilungenchirurgenview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdAbteilungenChirurgenView);
                    break;

                case "abteilungenview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdAbteilungenView) + GetText("rightEdit");
                    break;

                case "abteilungenview.view":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdAbteilungenView);
                    break;

                case "akademischeausbildungtypenview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdAkademischeAusbildungTypenView) + GetText("rightEdit");
                    break;

                case "akademischeausbildungtypenview.view":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdAkademischeAusbildungTypenView);
                    break;

                case "akademischeausbildungview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdAkademischeAusbildungView);
                    break;

                case "chirurgdokumenteview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdChirurgDokumenteView);
                    break;

                case "chirurgenfunktionenview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdChirurgenFunktionenView) + GetText("rightEdit");
                    break;

                case "chirurgenfunktionenview.view":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdChirurgenFunktionenView);
                    break;

                case "chirurgenrichtlinienview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdChirurgenRichtlinienView);
                    break;

                case "chirurgenview.llexclude":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdChirurgEdit) 
                        + " > '" + GetText("ChirurgenView", "llExclude") + "'";
                    break;

                case "chirurgenview.radinaktiv":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdChirurgEdit) 
                        + " > '" + GetText("ChirurgenView", "radInaktiv") + "'";
                    break;

                case "chirurgoperationenview.cmdassignrichtlinie":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdChirurgOperationenView) + " > '" 
                        + GetText("ChirurgOperationenView", "cmdAssignRichtlinie") + "'";
                    break;

                case "chirurgoperationenview.cmddelete":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdChirurgOperationenView) + " > '" 
                        + GetText("ChirurgOperationenView", "cmdDelete") + "'";
                    break;

                case "chirurgoperationenview.cmdnew":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdChirurgOperationenView)+ " > '" 
                        + GetText("ChirurgOperationenView", "cmdNew") + "'";
                    break;

                case "chirurgoperationenview.cmdremoverichtlinie":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdChirurgOperationenView)+ " > '" 
                        + GetText("ChirurgOperationenView", "cmdRemoveRichtlinie") + "'";
                    break;

                case "chirurgoperationenview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdChirurgOperationenView);
                    break;

                case "chirurgview.chkweiterbilder":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdChirurgEdit)
                        + " > '" + GetText("ChirurgView", "chkWeiterbilder") + "'";
                    break;

                case "clientserverview.view":
                    //
                    // Es gibt zwei buttons: cmdClientServerViewImportund cmdClientServerViewExport.
                    // daher können sie nicht cmdClientServerView heißen
                    //
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdClientServerViewImport);
                    break;

                case "clientserverview.cmdsend":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdClientServerViewImport)
                        + " > '" + GetText("ClientServerView", "cmdSend") + "'";
                    break;

                case "clientserverview.cmdreceive":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdClientServerViewImport)
                        + " > '" + GetText("ClientServerView", "cmdReceive") + "'";
                    break;

                case "cmd.exportchirurg":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdExportChirurgWizard);
                    break;

                case "cmd.exportrichtlinien":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdExportRichtlinienWizard);
                    break;

                case "cmd.exportzuordnungen":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdExportRichtlinienZuordnungWizard);
                    break;

                case "cmd.importchirurg":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdImportChirurgWizard);
                    break;

                case "cmd.importrichtlinien":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdImportRichtlinienWizard);
                    break;

                case "cmd.importzuordnungen":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdImportRichtlinienZuordnungWizard);
                    break;

                case "cmd.viewalldocs":
                    description = GetText("befehl_alleDokumenteSehen");
                    break;

                case "copywwwprogramupdatefilesview.cmdcopy":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdCopyWWWProgramUpdateFilesView) + " > '" 
                        + GetText("CopyWWWProgramUpdateFilesView", "cmdCopy") + "'";
                    break;

                case "copywwwprogramupdatefilesview.cmdverzeichnis":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdCopyWWWProgramUpdateFilesView) + " > '" 
                        + GetText("CopyWWWProgramUpdateFilesView", "cmdVerzeichnis") + "'";
                    break;

                case "copywwwprogramupdatefilesview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdCopyWWWProgramUpdateFilesView);
                    break;

                case "dateienview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdDateienView);
                    break;

                case "dateitypenview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdDateiTypenView) + GetText("rightEdit");
                    break;

                case "dateitypenview.view":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdDateiTypenView);
                    break;

                case "dokumenteview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdDokumenteView);
                    break;

                case "exportoperationenkatalogview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdExportOperationenKatalogView);
                    break;

                case "gebieteview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdGebieteView) + GetText("rightEdit");
                    break;

                case "gebieteview.view":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdGebieteView);
                    break;

                case "gesamtoperationenview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdGesamtOperationenView);
                    break;

                case "importchirurgenexcludeview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdImportChirurgenExcludeView);
                    break;

                case "importoperationenmobilewizard.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdImportOperationenMobileWizard);
                    break;

                case "importopswizard.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdImportOPSWizard);
                    break;

                case "klinischeergebnisseview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdKlinischeErgebnisseView);
                    break;

                case "kommentareview.cmdallcomments":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdKommentareView) + " > '"
                        + GetText("KommentareView", "cmdAllComments") + "'";
                    break;

                case "kommentareview.cmdcommentnew":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdKommentareView) + " > '"
                        + GetText("KommentareView", "cmdCommentNew") + "'";
                    break;

                case "kommentareview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdKommentareView);
                    break;

                case "logview.cmddelete":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdLogView) + " > '"
                        + GetText("LogView", "cmdDelete") + "'/'" + GetText("LogView", "cmdDeleteList") + "'";
                    break;

                case "logview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdLogView);
                    break;

                case "mn.autoupdatefolder":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdUpdateFromFolder);
                    break;

                case "mn.autoupdateinternet":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdUpdateFromWww);
                    break;

                case "mn.chirurg":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.rgrChirurg);
                    break;

                case "mn.chirurgdelete":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdChirurgDelete);
                    break;

                case "mn.chirurgedit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdChirurgEdit);
                    break;

                case "mn.chirurgnew":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdChirurgNew);
                    break;

                case "mn.export":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.rtpExport);
                    break;

                case "mn.import":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.rtpImport);
                    break;

                case "mn.importautoimport":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdExecAutoImport);
                    break;

                case "mn.installlicense":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdInstallLicenseWizard);
                    break;

                case "mn.serialnumberswebshop":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdSerialBuy);
                    break;

                case "notizenview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdNotizenView);
                    break;

                case "notiztypenview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdNotizTypenView) + GetText("rightEdit");
                    break;

                case "notiztypenview.view":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdNotizTypenView);
                    break;

                case "opdauerfortschrittview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOPDauerFortschrittView);
                    break;

                case "operationeneditview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOperationenEditView);
                    break;

                case "operationenimportview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOperationenImportView);
                    break;

                case "operationenimportview.cmdsave":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOperationenImportView) + " > '"
                    + GetText("OperationenImportView", "cmdSave") + "'";
                    break;

                case "operationenkatalogview.cmddelete":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOperationenKatalogView) + " > '"
                        + GetText("OperationenKatalogView", "cmdDelete") + "'";
                    break;

                case "operationenkatalogview.cmddeleteall":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOperationenKatalogView) + " > '"
                    + GetText("OperationenKatalogView", "cmdDeleteAll") + "'";
                    break;

                case "operationenkatalogview.cmdinsert":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOperationenKatalogView) + " > '"
                    + GetText("OperationenKatalogView", "cmdInsert") + "'";
                    break;

                case "operationenkatalogview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOperationenKatalogView);
                    break;

                case "operationensummaryview.view":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOperationenSummaryView);
                    break;

                case "operationenvergleichview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOperationenVergleichView);
                    break;

                case "operationenview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOperationenView);
                    break;

                case "operationenzeitenvergleichview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOperationenZeitenVergleichView);
                    break;

                case "optionsview.chkopencommentmsg":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOptionsView) + " > "
                        + GetText("OptionsView", "tab_other") + " > '" + GetText("OptionsView", "chkOpenCommentMsg") + "'";
                    break;

                case "optionsview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOptionsView);
                    break;

                case "optionsview.tabimport":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOptionsView) + " > "
                    + GetText("OptionsView", "tab_auto_import");
                    break;

                case "optionsview.tabprint":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOptionsView) + " > "
                     + GetText("OptionsView", "tab_print");
                    break;

                case "optionsview.tabproxy":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOptionsView) + " > "
                     + GetText("OptionsView", "tab_proxy");
                    break;

                case "optionsview.tabserialnumbers":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOptionsView) + " > "
                        + GetText("OptionsView", "tab_serial_numbers");
                    break;

                case "optionsview.tabsonstiges":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOptionsView) + " > "
                        + GetText("OptionsView", "tab_other");
                    break;

                case "optionsview.tabupdate":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOptionsView) + " > "
                        + GetText("OptionsView", "tab_program");
                    break;

                case "optionsview.txtstellenopskode":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdOptionsView) + " > "
                        + GetText("OptionsView", "tab_other") + " > '"
                        + GetText("OptionsView", "lblOpscodeStellen") + "'";
                    break;

                case "planoperationenview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdPlanOperationenView) + GetText("rightEdit");
                    break;

                case "planoperationenview.view":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdPlanOperationenView);
                    break;

                case "planoperationvergleichistview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdPlanOperationVergleichIstView);
                    break;

                case "planoperationvergleichview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdPlanOperationVergleichView);
                    break;

                case "printistoperationen.edit":
                    description = "Start > " + MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdPrintIstOperationen);
                    break;

                case "richtliniensollistview.view":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdRichtlinienSollIstView);
                    break;

                case "richtliniensollistview.cmdadd":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdRichtlinienSollIstView) + " > '"
                        + GetText("RichtlinienSollIstView", "cmdAdd") + "'";
                    break;

                case "richtliniensollistview.cmddelete":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdRichtlinienSollIstView) + " > '"
                        + GetText("RichtlinienSollIstView", "cmdDelete") + "'";
                    break;

                case "richtliniensollistview.cmdupdate":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdRichtlinienSollIstView) + " > '"
                        + GetText("RichtlinienSollIstView", "cmdupdate") + "'";
                    break;

                case "richtlinienopskodeunassignedview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdRichtlinienOpsKodeUnassignedView);
                    break;

                case "richtlinienvergleichoverviewview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdRichtlinienVergleichOverviewView);
                    break;

                case "richtlinienvergleichview.cmdassignopsrichtlinie":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdRichtlinienVergleichView) + " > '"
                        + GetText("RichtlinienVergleichview", "cmdAssignOpsRichtlinie") + "'";
                    break;

                case "richtlinienvergleichview.cmdassignrichtlinie":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdRichtlinienVergleichView) + " > '"
                        + GetText("RichtlinienVergleichview", "cmdAssignRichtlinie") + "'";
                    break;

                case "richtlinienvergleichview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdRichtlinienVergleichView);
                    break;

                case "richtlinienview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdRichtlinienView) + GetText("rightEdit");
                    break;

                case "richtlinienview.view":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdRichtlinienView);
                    break;

                case "secgroupschirurgenview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdSecGroupsChirurgenView);
                    break;

                case "secgroupssecrightsview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdSecGroupsSecRightsView);
                    break;

                case "secgroupsview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdSecGroupsView) + GetText("rightEdit");
                    break;

                case "secgroupsview.view":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdSecGroupsView);
                    break;

                case "secuseroverviewview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdSecUserOverviewView);
                    break;

                case "select.surgeons.abteilung":
                    description = GetText("rightDataAbteilung");
                    break;

                case "select.surgeons.all":
                    description = GetText("rightDataAll");
                    break;

                case "serialnumbersview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdSerialNumbersView);
                    break;

                case "usersetpasswordview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdUserSetPasswordView);
                    break;

                case "weiterbilderchirurgenview.edit":
                    description = MakeMenuPath(OperationenLogbuchView.TheMainWindow.cmdWeiterbilderChirurgenView);
                    break;

                default:
                    description = name;
                    break;
            }

            return description;
        }

        private void ribbon_QuickAccessToolbarPlacementModeChanged(object sender, EventArgs e)
        {
            SetDetailsAndHeight();
        }

        private void cmdClientServerViewImport_Click(object sender, EventArgs e)
        {

        }

        private void cmdSerialBuy_Click(object sender, EventArgs e)
        {

        }
    }
}


