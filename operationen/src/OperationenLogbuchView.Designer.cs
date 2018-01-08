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
    partial class OperationenLogbuchView : OperationenForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>s
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OperationenLogbuchView));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.rtpOffizielleDokumente = new Elegant.Ui.RibbonTabPage();
            this.rgrOffizielleDokumente1 = new Elegant.Ui.RibbonGroup();
            this.cmdOperationenKatalogView = new Elegant.Ui.Button();
            this.cmdGebieteView = new Elegant.Ui.Button();
            this.cmdRichtlinienView = new Elegant.Ui.Button();
            this.rgrOffizielleDokumente2 = new Elegant.Ui.RibbonGroup();
            this.cmdBaekWbOrdnung = new Elegant.Ui.Button();
            this.cmdBaekWbRichtlinien = new Elegant.Ui.Button();
            this.rtpVerwaltung = new Elegant.Ui.RibbonTabPage();
            this.rgrVerwaltung1 = new Elegant.Ui.RibbonGroup();
            this.cmdSecUserOverviewView = new Elegant.Ui.Button();
            this.cmdSecGroupsView = new Elegant.Ui.Button();
            this.cmdSecGroupsChirurgenView = new Elegant.Ui.Button();
            this.cmdSecGroupsSecRightsView = new Elegant.Ui.Button();
            this.rgrVerwaltung2 = new Elegant.Ui.RibbonGroup();
            this.cmdAbteilungenView = new Elegant.Ui.Button();
            this.cmdAbteilungenChirurgenView = new Elegant.Ui.Button();
            this.cmdWeiterbilderChirurgenView = new Elegant.Ui.Button();
            this.rgrVerwaltung3 = new Elegant.Ui.RibbonGroup();
            this.cmdOperationenSummaryView = new Elegant.Ui.Button();
            this.rtpBearbeiten = new Elegant.Ui.RibbonTabPage();
            this.rgrBearbeiten1 = new Elegant.Ui.RibbonGroup();
            this.cmdOperationenEditView = new Elegant.Ui.Button();
            this.cmdChirurgenRichtlinienView = new Elegant.Ui.Button();
            this.rgrBearbeiten2 = new Elegant.Ui.RibbonGroup();
            this.cmdRichtlinienOpsKodeUnassignedView = new Elegant.Ui.Button();
            this.rtpAuswertungen = new Elegant.Ui.RibbonTabPage();
            this.rgrAuswertungen1 = new Elegant.Ui.RibbonGroup();
            this.cmdOperationenView = new Elegant.Ui.Button();
            this.cmdOperationenZeitenVergleichView = new Elegant.Ui.Button();
            this.cmdOPDauerFortschrittView = new Elegant.Ui.Button();
            this.cmdKlinischeErgebnisseView = new Elegant.Ui.Button();
            this.rgrAuswertungen2 = new Elegant.Ui.RibbonGroup();
            this.cmdChirurgOperationenView = new Elegant.Ui.Button();
            this.cmdGesamtOperationenView = new Elegant.Ui.Button();
            this.cmdPlanOperationVergleichIstView = new Elegant.Ui.Button();
            this.cmdOperationenVergleichView = new Elegant.Ui.Button();
            this.rgrAuswertungen3 = new Elegant.Ui.RibbonGroup();
            this.cmdRichtlinienVergleichView = new Elegant.Ui.Button();
            this.cmdRichtlinienVergleichOverviewView = new Elegant.Ui.Button();
            this.rtpImport = new Elegant.Ui.RibbonTabPage();
            this.rgrImport1 = new Elegant.Ui.RibbonGroup();
            this.cmdOperationenImportView = new Elegant.Ui.Button();
            this.cmdImportChirurgenExcludeView = new Elegant.Ui.Button();
            this.cmdExecAutoImport = new Elegant.Ui.Button();
            this.rgrImport2 = new Elegant.Ui.RibbonGroup();
            this.cmdImportRichtlinienWizard = new Elegant.Ui.Button();
            this.cmdImportRichtlinienZuordnungWizard = new Elegant.Ui.Button();
            this.rgrImport3 = new Elegant.Ui.RibbonGroup();
            this.cmdImportChirurgWizard = new Elegant.Ui.Button();
            this.cmdImportOPSWizard = new Elegant.Ui.Button();
            this.cmdImportOperationenMobileWizard = new Elegant.Ui.Button();
            this.cmdClientServerViewImport = new Elegant.Ui.Button();
            this.rtpExport = new Elegant.Ui.RibbonTabPage();
            this.rgrExport1 = new Elegant.Ui.RibbonGroup();
            this.cmdExportRichtlinienWizard = new Elegant.Ui.Button();
            this.cmdExportRichtlinienZuordnungWizard = new Elegant.Ui.Button();
            this.rgrExport2 = new Elegant.Ui.RibbonGroup();
            this.cmdExportChirurgWizard = new Elegant.Ui.Button();
            this.cmdExportOperationenKatalogView = new Elegant.Ui.Button();
            this.cmdClientServerViewExport = new Elegant.Ui.Button();
            this.rtpHilfe = new Elegant.Ui.RibbonTabPage();
            this.rgrHilfe1 = new Elegant.Ui.RibbonGroup();
            this.cmdHelpChm = new Elegant.Ui.Button();
            this.cmdWwwHelp = new Elegant.Ui.Button();
            this.cmdWwwHome = new Elegant.Ui.Button();
            this.cmdAboutBox = new Elegant.Ui.Button();
            this.rgrHilfe2 = new Elegant.Ui.RibbonGroup();
            this.ddVideoTutorials = new Elegant.Ui.DropDown();
            this.rtpDebug = new Elegant.Ui.RibbonTabPage();
            this.ribbonGroup1 = new Elegant.Ui.RibbonGroup();
            this.cmdDebugSql = new Elegant.Ui.Button();
            this.cmdDebugExportOperationen = new Elegant.Ui.Button();
            this.cmdUpdateSerialnumbersView = new Elegant.Ui.Button();
            this.cmdDebugReloadUserRights = new Elegant.Ui.Button();
            this.cmdZeitraeumeView = new Elegant.Ui.Button();
            this.rtpExtras = new Elegant.Ui.RibbonTabPage();
            this.rgrExtras1 = new Elegant.Ui.RibbonGroup();
            this.cmdUserChangePasswordView = new Elegant.Ui.Button();
            this.cmdUserSetPasswordView = new Elegant.Ui.Button();
            this.rgrExtras2 = new Elegant.Ui.RibbonGroup();
            this.cmdSerialNumbersView = new Elegant.Ui.Button();
            this.cmdSerialBuy = new Elegant.Ui.Button();
            this.cmdInstallLicenseWizard = new Elegant.Ui.Button();
            this.rgrExtras3 = new Elegant.Ui.RibbonGroup();
            this.cmdUpdateFromFolder = new Elegant.Ui.Button();
            this.cmdUpdateFromWww = new Elegant.Ui.Button();
            this.cmdCopyWWWProgramUpdateFilesView = new Elegant.Ui.Button();
            this.rgrExtras4 = new Elegant.Ui.RibbonGroup();
            this.cmdLogView = new Elegant.Ui.Button();
            this.cmdOptionsView = new Elegant.Ui.Button();
            this.cmdLangDeDe = new Elegant.Ui.Button();
            this.cmdLangEnUs = new Elegant.Ui.Button();
            this.ribbon = new Elegant.Ui.Ribbon();
            this.applicationMenu = new Elegant.Ui.ApplicationMenu();
            this.cmdPrintIstOperationen = new Elegant.Ui.Button();
            this.rtpEigeneDateien = new Elegant.Ui.RibbonTabPage();
            this.rgrEigeneDateien1 = new Elegant.Ui.RibbonGroup();
            this.cmdDateiTypenView = new Elegant.Ui.Button();
            this.cmdDateienView = new Elegant.Ui.Button();
            this.rgrEigeneDateien2 = new Elegant.Ui.RibbonGroup();
            this.ddEigeneDateien = new Elegant.Ui.DropDown();
            this.rtpStammdaten = new Elegant.Ui.RibbonTabPage();
            this.rgrChirurg = new Elegant.Ui.RibbonGroup();
            this.cmdChirurgenFunktionenView = new Elegant.Ui.Button();
            this.cmdChirurgNew = new Elegant.Ui.Button();
            this.cmdChirurgEdit = new Elegant.Ui.Button();
            this.cmdChirurgDelete = new Elegant.Ui.Button();
            this.rgrStammdaten2 = new Elegant.Ui.RibbonGroup();
            this.cmdKommentareView = new Elegant.Ui.Button();
            this.rgrStammdaten3 = new Elegant.Ui.RibbonGroup();
            this.cmdDokumenteView = new Elegant.Ui.Button();
            this.cmdChirurgDokumenteView = new Elegant.Ui.Button();
            this.rgrStammdaten4 = new Elegant.Ui.RibbonGroup();
            this.cmdAkademischeAusbildungTypenView = new Elegant.Ui.Button();
            this.cmdAkademischeAusbildungView = new Elegant.Ui.Button();
            this.rgrStammdaten5 = new Elegant.Ui.RibbonGroup();
            this.cmdNotizTypenView = new Elegant.Ui.Button();
            this.cmdNotizenView = new Elegant.Ui.Button();
            this.rgrStammdaten6 = new Elegant.Ui.RibbonGroup();
            this.cmdPlanOperationenView = new Elegant.Ui.Button();
            this.cmdPlanOperationVergleichView = new Elegant.Ui.Button();
            this.cmdRichtlinienSollIstView = new Elegant.Ui.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pbMain = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.formFrameSkinner = new Elegant.Ui.FormFrameSkinner();
            ((System.ComponentModel.ISupportInitialize)(this.rtpOffizielleDokumente)).BeginInit();
            this.rtpOffizielleDokumente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrOffizielleDokumente1)).BeginInit();
            this.rgrOffizielleDokumente1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrOffizielleDokumente2)).BeginInit();
            this.rgrOffizielleDokumente2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtpVerwaltung)).BeginInit();
            this.rtpVerwaltung.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrVerwaltung1)).BeginInit();
            this.rgrVerwaltung1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrVerwaltung2)).BeginInit();
            this.rgrVerwaltung2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrVerwaltung3)).BeginInit();
            this.rgrVerwaltung3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtpBearbeiten)).BeginInit();
            this.rtpBearbeiten.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrBearbeiten1)).BeginInit();
            this.rgrBearbeiten1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrBearbeiten2)).BeginInit();
            this.rgrBearbeiten2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtpAuswertungen)).BeginInit();
            this.rtpAuswertungen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrAuswertungen1)).BeginInit();
            this.rgrAuswertungen1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrAuswertungen2)).BeginInit();
            this.rgrAuswertungen2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrAuswertungen3)).BeginInit();
            this.rgrAuswertungen3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtpImport)).BeginInit();
            this.rtpImport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrImport1)).BeginInit();
            this.rgrImport1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrImport2)).BeginInit();
            this.rgrImport2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrImport3)).BeginInit();
            this.rgrImport3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtpExport)).BeginInit();
            this.rtpExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrExport1)).BeginInit();
            this.rgrExport1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrExport2)).BeginInit();
            this.rgrExport2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtpHilfe)).BeginInit();
            this.rtpHilfe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrHilfe1)).BeginInit();
            this.rgrHilfe1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrHilfe2)).BeginInit();
            this.rgrHilfe2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtpDebug)).BeginInit();
            this.rtpDebug.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonGroup1)).BeginInit();
            this.ribbonGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtpExtras)).BeginInit();
            this.rtpExtras.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrExtras1)).BeginInit();
            this.rgrExtras1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrExtras2)).BeginInit();
            this.rgrExtras2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrExtras3)).BeginInit();
            this.rgrExtras3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrExtras4)).BeginInit();
            this.rgrExtras4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtpEigeneDateien)).BeginInit();
            this.rtpEigeneDateien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrEigeneDateien1)).BeginInit();
            this.rgrEigeneDateien1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrEigeneDateien2)).BeginInit();
            this.rgrEigeneDateien2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtpStammdaten)).BeginInit();
            this.rtpStammdaten.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrChirurg)).BeginInit();
            this.rgrChirurg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrStammdaten2)).BeginInit();
            this.rgrStammdaten2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrStammdaten3)).BeginInit();
            this.rgrStammdaten3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrStammdaten4)).BeginInit();
            this.rgrStammdaten4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrStammdaten5)).BeginInit();
            this.rgrStammdaten5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrStammdaten6)).BeginInit();
            this.rgrStammdaten6.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
            this.SuspendLayout();
            // 
            // rtpOffizielleDokumente
            // 
            this.rtpOffizielleDokumente.Controls.Add(this.rgrOffizielleDokumente1);
            this.rtpOffizielleDokumente.Controls.Add(this.rgrOffizielleDokumente2);
            resources.ApplyResources(this.rtpOffizielleDokumente, "rtpOffizielleDokumente");
            this.rtpOffizielleDokumente.Id = "75e8c33a-77ea-47bd-9c54-6743c731d27d";
            this.rtpOffizielleDokumente.KeyTip = "O";
            this.rtpOffizielleDokumente.Name = "rtpOffizielleDokumente";
            // 
            // rgrOffizielleDokumente1
            // 
            this.rgrOffizielleDokumente1.Controls.Add(this.cmdOperationenKatalogView);
            this.rgrOffizielleDokumente1.Controls.Add(this.cmdGebieteView);
            this.rgrOffizielleDokumente1.Controls.Add(this.cmdRichtlinienView);
            this.rgrOffizielleDokumente1.DialogLauncherButtonEnabled = false;
            this.rgrOffizielleDokumente1.DialogLauncherButtonVisible = false;
            this.rgrOffizielleDokumente1.Id = "9959bc93-d00b-4650-b12e-e4e168fcec4c";
            resources.ApplyResources(this.rgrOffizielleDokumente1, "rgrOffizielleDokumente1");
            this.rgrOffizielleDokumente1.Name = "rgrOffizielleDokumente1";
            // 
            // cmdOperationenKatalogView
            // 
            this.cmdOperationenKatalogView.CommandName = "OperationenKatalogView";
            this.cmdOperationenKatalogView.Id = "f47ce5bc-a846-4ab0-abad-41c7db59bbba";
            this.cmdOperationenKatalogView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdOperationenKatalogView.KeyTip = "1";
            this.cmdOperationenKatalogView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Book32)});
            resources.ApplyResources(this.cmdOperationenKatalogView, "cmdOperationenKatalogView");
            this.cmdOperationenKatalogView.Name = "cmdOperationenKatalogView";
            this.cmdOperationenKatalogView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Book16)});
            // 
            // cmdGebieteView
            // 
            this.cmdGebieteView.CommandName = "GebieteView";
            this.cmdGebieteView.Id = "48a9d7e8-2be8-46ef-9bb0-56fccc872a83";
            this.cmdGebieteView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdGebieteView.KeyTip = "2";
            this.cmdGebieteView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Cardiologist_Oculist_Surgeon32)});
            resources.ApplyResources(this.cmdGebieteView, "cmdGebieteView");
            this.cmdGebieteView.Name = "cmdGebieteView";
            this.cmdGebieteView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Cardiologist_Oculist_Surgeon16)});
            // 
            // cmdRichtlinienView
            // 
            this.cmdRichtlinienView.CommandName = "RichtlinienView";
            this.cmdRichtlinienView.Id = "aac4ccba-1ec7-444c-afde-05f711f70422";
            this.cmdRichtlinienView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdRichtlinienView.KeyTip = "3";
            this.cmdRichtlinienView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Certification32)});
            resources.ApplyResources(this.cmdRichtlinienView, "cmdRichtlinienView");
            this.cmdRichtlinienView.Name = "cmdRichtlinienView";
            this.cmdRichtlinienView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Certification16)});
            // 
            // rgrOffizielleDokumente2
            // 
            this.rgrOffizielleDokumente2.Controls.Add(this.cmdBaekWbOrdnung);
            this.rgrOffizielleDokumente2.Controls.Add(this.cmdBaekWbRichtlinien);
            this.rgrOffizielleDokumente2.DialogLauncherButtonEnabled = false;
            this.rgrOffizielleDokumente2.DialogLauncherButtonVisible = false;
            this.rgrOffizielleDokumente2.Id = "ea92bcf7-b52d-4d3c-be4b-f426badf5e3f";
            resources.ApplyResources(this.rgrOffizielleDokumente2, "rgrOffizielleDokumente2");
            this.rgrOffizielleDokumente2.Name = "rgrOffizielleDokumente2";
            // 
            // cmdBaekWbOrdnung
            // 
            this.cmdBaekWbOrdnung.CommandName = "BaekWbOrdnung";
            this.cmdBaekWbOrdnung.Id = "61ed476e-c5b8-4f65-8b45-78f3949ff3c9";
            this.cmdBaekWbOrdnung.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdBaekWbOrdnung.KeyTip = "4";
            this.cmdBaekWbOrdnung.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Earth_Certification_Problem32)});
            resources.ApplyResources(this.cmdBaekWbOrdnung, "cmdBaekWbOrdnung");
            this.cmdBaekWbOrdnung.Name = "cmdBaekWbOrdnung";
            this.cmdBaekWbOrdnung.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Earth_Certification_Problem16)});
            // 
            // cmdBaekWbRichtlinien
            // 
            this.cmdBaekWbRichtlinien.CommandName = "BaekWbRichtlinien";
            this.cmdBaekWbRichtlinien.Id = "817eeba6-1cf2-4749-8674-1e74b2a389c1";
            this.cmdBaekWbRichtlinien.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdBaekWbRichtlinien.KeyTip = "5";
            this.cmdBaekWbRichtlinien.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Earth_Certification32)});
            resources.ApplyResources(this.cmdBaekWbRichtlinien, "cmdBaekWbRichtlinien");
            this.cmdBaekWbRichtlinien.Name = "cmdBaekWbRichtlinien";
            this.cmdBaekWbRichtlinien.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Earth_Certification16)});
            // 
            // rtpVerwaltung
            // 
            this.rtpVerwaltung.Controls.Add(this.rgrVerwaltung1);
            this.rtpVerwaltung.Controls.Add(this.rgrVerwaltung2);
            this.rtpVerwaltung.Controls.Add(this.rgrVerwaltung3);
            resources.ApplyResources(this.rtpVerwaltung, "rtpVerwaltung");
            this.rtpVerwaltung.Id = "ec4a5cee-e77f-43cd-88c7-72c66e7a33ac";
            this.rtpVerwaltung.KeyTip = "V";
            this.rtpVerwaltung.Name = "rtpVerwaltung";
            // 
            // rgrVerwaltung1
            // 
            this.rgrVerwaltung1.Controls.Add(this.cmdSecUserOverviewView);
            this.rgrVerwaltung1.Controls.Add(this.cmdSecGroupsView);
            this.rgrVerwaltung1.Controls.Add(this.cmdSecGroupsChirurgenView);
            this.rgrVerwaltung1.Controls.Add(this.cmdSecGroupsSecRightsView);
            this.rgrVerwaltung1.DialogLauncherButtonEnabled = false;
            this.rgrVerwaltung1.DialogLauncherButtonVisible = false;
            this.rgrVerwaltung1.Id = "d3013f17-a38a-4a55-91ac-3d6e9cfcaddf";
            resources.ApplyResources(this.rgrVerwaltung1, "rgrVerwaltung1");
            this.rgrVerwaltung1.Name = "rgrVerwaltung1";
            // 
            // cmdSecUserOverviewView
            // 
            this.cmdSecUserOverviewView.CommandName = "SecUserOverviewView";
            this.cmdSecUserOverviewView.Id = "814c1ea9-d63a-4a74-a476-d816f9f75f57";
            this.cmdSecUserOverviewView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdSecUserOverviewView.KeyTip = "1";
            this.cmdSecUserOverviewView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.AccountCard_Many32)});
            resources.ApplyResources(this.cmdSecUserOverviewView, "cmdSecUserOverviewView");
            this.cmdSecUserOverviewView.Name = "cmdSecUserOverviewView";
            this.cmdSecUserOverviewView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.AccountCard_Many16)});
            // 
            // cmdSecGroupsView
            // 
            this.cmdSecGroupsView.CommandName = "SecGroupsView";
            this.cmdSecGroupsView.Id = "feee2248-7362-4f43-bc45-a2be1ae0956f";
            this.cmdSecGroupsView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdSecGroupsView.KeyTip = "2";
            this.cmdSecGroupsView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.AccountCard32)});
            resources.ApplyResources(this.cmdSecGroupsView, "cmdSecGroupsView");
            this.cmdSecGroupsView.Name = "cmdSecGroupsView";
            this.cmdSecGroupsView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.AccountCard16)});
            // 
            // cmdSecGroupsChirurgenView
            // 
            this.cmdSecGroupsChirurgenView.CommandName = "SecGroupsChirurgenView";
            this.cmdSecGroupsChirurgenView.Id = "da4b84c8-ce88-46b7-a670-8f8c3718e871";
            this.cmdSecGroupsChirurgenView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdSecGroupsChirurgenView.KeyTip = "3";
            this.cmdSecGroupsChirurgenView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.AccountCard_UserGroup32)});
            resources.ApplyResources(this.cmdSecGroupsChirurgenView, "cmdSecGroupsChirurgenView");
            this.cmdSecGroupsChirurgenView.Name = "cmdSecGroupsChirurgenView";
            this.cmdSecGroupsChirurgenView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.AccountCard_UserGroup16)});
            // 
            // cmdSecGroupsSecRightsView
            // 
            this.cmdSecGroupsSecRightsView.CommandName = "SecGroupsSecRightsView";
            this.cmdSecGroupsSecRightsView.Id = "76654594-5c29-487b-b9fc-51682a4c92d0";
            this.cmdSecGroupsSecRightsView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdSecGroupsSecRightsView.KeyTip = "4";
            this.cmdSecGroupsSecRightsView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.AccountCard_Key32)});
            resources.ApplyResources(this.cmdSecGroupsSecRightsView, "cmdSecGroupsSecRightsView");
            this.cmdSecGroupsSecRightsView.Name = "cmdSecGroupsSecRightsView";
            this.cmdSecGroupsSecRightsView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.AccountCard_Key16)});
            // 
            // rgrVerwaltung2
            // 
            this.rgrVerwaltung2.Controls.Add(this.cmdAbteilungenView);
            this.rgrVerwaltung2.Controls.Add(this.cmdAbteilungenChirurgenView);
            this.rgrVerwaltung2.Controls.Add(this.cmdWeiterbilderChirurgenView);
            this.rgrVerwaltung2.DialogLauncherButtonEnabled = false;
            this.rgrVerwaltung2.DialogLauncherButtonVisible = false;
            this.rgrVerwaltung2.Id = "7a22efe1-a5dc-42f4-9217-3ebd4012fd31";
            resources.ApplyResources(this.rgrVerwaltung2, "rgrVerwaltung2");
            this.rgrVerwaltung2.Name = "rgrVerwaltung2";
            // 
            // cmdAbteilungenView
            // 
            this.cmdAbteilungenView.CommandName = "AbteilungenView";
            this.cmdAbteilungenView.Id = "e99a6024-adfd-45e5-b4c4-c2e637d5ff3c";
            this.cmdAbteilungenView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdAbteilungenView.KeyTip = "5";
            this.cmdAbteilungenView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.MultistoreyBuilding32)});
            resources.ApplyResources(this.cmdAbteilungenView, "cmdAbteilungenView");
            this.cmdAbteilungenView.Name = "cmdAbteilungenView";
            this.cmdAbteilungenView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.MultistoreyBuilding16)});
            // 
            // cmdAbteilungenChirurgenView
            // 
            this.cmdAbteilungenChirurgenView.CommandName = "AbteilungenChirurgenView";
            this.cmdAbteilungenChirurgenView.Id = "f12ed07a-6d85-43a8-9445-c52bbf5bc901";
            this.cmdAbteilungenChirurgenView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdAbteilungenChirurgenView.KeyTip = "6";
            this.cmdAbteilungenChirurgenView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.MultistoreyBuilding_UserGroup32)});
            resources.ApplyResources(this.cmdAbteilungenChirurgenView, "cmdAbteilungenChirurgenView");
            this.cmdAbteilungenChirurgenView.Name = "cmdAbteilungenChirurgenView";
            this.cmdAbteilungenChirurgenView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.MultistoreyBuilding_UserGroup16)});
            // 
            // cmdWeiterbilderChirurgenView
            // 
            this.cmdWeiterbilderChirurgenView.CommandName = "WeiterbilderChirurgenView";
            this.cmdWeiterbilderChirurgenView.Id = "731482c3-35ce-4390-84f9-997c6b1facc1";
            this.cmdWeiterbilderChirurgenView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdWeiterbilderChirurgenView.KeyTip = "7";
            this.cmdWeiterbilderChirurgenView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Boss_UserGroup32)});
            resources.ApplyResources(this.cmdWeiterbilderChirurgenView, "cmdWeiterbilderChirurgenView");
            this.cmdWeiterbilderChirurgenView.Name = "cmdWeiterbilderChirurgenView";
            this.cmdWeiterbilderChirurgenView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Boss_UserGroup16)});
            // 
            // rgrVerwaltung3
            // 
            this.rgrVerwaltung3.Controls.Add(this.cmdOperationenSummaryView);
            this.rgrVerwaltung3.DialogLauncherButtonEnabled = false;
            this.rgrVerwaltung3.DialogLauncherButtonVisible = false;
            this.rgrVerwaltung3.Id = "21026e42-2a62-40c5-8c6f-d49f3789a74e";
            resources.ApplyResources(this.rgrVerwaltung3, "rgrVerwaltung3");
            this.rgrVerwaltung3.Name = "rgrVerwaltung3";
            // 
            // cmdOperationenSummaryView
            // 
            this.cmdOperationenSummaryView.CommandName = "OperationenSummaryView";
            this.cmdOperationenSummaryView.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdOperationenSummaryView.Id = "ae0247cc-e12a-47b8-89c4-ba582b2d4a5e";
            this.cmdOperationenSummaryView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdOperationenSummaryView.KeyTip = "8";
            this.cmdOperationenSummaryView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources._3Dchart_PieChart_Report32)});
            resources.ApplyResources(this.cmdOperationenSummaryView, "cmdOperationenSummaryView");
            this.cmdOperationenSummaryView.Name = "cmdOperationenSummaryView";
            this.cmdOperationenSummaryView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources._3Dchart_PieChart_Report16)});
            // 
            // rtpBearbeiten
            // 
            this.rtpBearbeiten.Controls.Add(this.rgrBearbeiten1);
            this.rtpBearbeiten.Controls.Add(this.rgrBearbeiten2);
            resources.ApplyResources(this.rtpBearbeiten, "rtpBearbeiten");
            this.rtpBearbeiten.Id = "70ba7da1-c219-47da-b436-088ffe9376fe";
            this.rtpBearbeiten.KeyTip = "B";
            this.rtpBearbeiten.Name = "rtpBearbeiten";
            // 
            // rgrBearbeiten1
            // 
            this.rgrBearbeiten1.Controls.Add(this.cmdOperationenEditView);
            this.rgrBearbeiten1.Controls.Add(this.cmdChirurgenRichtlinienView);
            this.rgrBearbeiten1.DialogLauncherButtonEnabled = false;
            this.rgrBearbeiten1.DialogLauncherButtonVisible = false;
            this.rgrBearbeiten1.Id = "6ecf49cd-688f-477b-944a-27cd5218fefa";
            resources.ApplyResources(this.rgrBearbeiten1, "rgrBearbeiten1");
            this.rgrBearbeiten1.Name = "rgrBearbeiten1";
            // 
            // cmdOperationenEditView
            // 
            this.cmdOperationenEditView.CommandName = "OperationenEditView";
            this.cmdOperationenEditView.Id = "f3ecdae7-2e76-423b-9c47-576e06eb7788";
            this.cmdOperationenEditView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdOperationenEditView.KeyTip = "1";
            this.cmdOperationenEditView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Book_BlackPencil32)});
            resources.ApplyResources(this.cmdOperationenEditView, "cmdOperationenEditView");
            this.cmdOperationenEditView.Name = "cmdOperationenEditView";
            this.cmdOperationenEditView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Book_BlackPencil16)});
            // 
            // cmdChirurgenRichtlinienView
            // 
            this.cmdChirurgenRichtlinienView.CommandName = "ChirurgenRichtlinienView";
            this.cmdChirurgenRichtlinienView.Id = "dddcf1f8-098b-4336-9b05-c4158671005f";
            this.cmdChirurgenRichtlinienView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdChirurgenRichtlinienView.KeyTip = "2";
            this.cmdChirurgenRichtlinienView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Certification_BlackPencil32)});
            resources.ApplyResources(this.cmdChirurgenRichtlinienView, "cmdChirurgenRichtlinienView");
            this.cmdChirurgenRichtlinienView.Name = "cmdChirurgenRichtlinienView";
            this.cmdChirurgenRichtlinienView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Certification_BlackPencil16)});
            // 
            // rgrBearbeiten2
            // 
            this.rgrBearbeiten2.Controls.Add(this.cmdRichtlinienOpsKodeUnassignedView);
            this.rgrBearbeiten2.DialogLauncherButtonEnabled = false;
            this.rgrBearbeiten2.DialogLauncherButtonVisible = false;
            this.rgrBearbeiten2.Id = "ac718559-4481-4b67-894c-fda0a8b9c9b0";
            resources.ApplyResources(this.rgrBearbeiten2, "rgrBearbeiten2");
            this.rgrBearbeiten2.Name = "rgrBearbeiten2";
            // 
            // cmdRichtlinienOpsKodeUnassignedView
            // 
            this.cmdRichtlinienOpsKodeUnassignedView.CommandName = "RichtlinienOpsKodeUnassignedView";
            this.cmdRichtlinienOpsKodeUnassignedView.Id = "b5995d3e-5853-4c9f-b5ee-4fed883ae398";
            this.cmdRichtlinienOpsKodeUnassignedView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdRichtlinienOpsKodeUnassignedView.KeyTip = "3";
            this.cmdRichtlinienOpsKodeUnassignedView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.FlipHorizontally_PatienID_Certification32)});
            resources.ApplyResources(this.cmdRichtlinienOpsKodeUnassignedView, "cmdRichtlinienOpsKodeUnassignedView");
            this.cmdRichtlinienOpsKodeUnassignedView.Name = "cmdRichtlinienOpsKodeUnassignedView";
            this.cmdRichtlinienOpsKodeUnassignedView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.FlipHorizontally_PatientID_Certification16)});
            // 
            // rtpAuswertungen
            // 
            this.rtpAuswertungen.Controls.Add(this.rgrAuswertungen1);
            this.rtpAuswertungen.Controls.Add(this.rgrAuswertungen2);
            this.rtpAuswertungen.Controls.Add(this.rgrAuswertungen3);
            resources.ApplyResources(this.rtpAuswertungen, "rtpAuswertungen");
            this.rtpAuswertungen.Id = "222ef866-713e-41e7-9e85-ebb85c706767";
            this.rtpAuswertungen.KeyTip = "A";
            this.rtpAuswertungen.Name = "rtpAuswertungen";
            // 
            // rgrAuswertungen1
            // 
            this.rgrAuswertungen1.Controls.Add(this.cmdOperationenView);
            this.rgrAuswertungen1.Controls.Add(this.cmdOperationenZeitenVergleichView);
            this.rgrAuswertungen1.Controls.Add(this.cmdOPDauerFortschrittView);
            this.rgrAuswertungen1.Controls.Add(this.cmdKlinischeErgebnisseView);
            this.rgrAuswertungen1.DialogLauncherButtonEnabled = false;
            this.rgrAuswertungen1.DialogLauncherButtonVisible = false;
            this.rgrAuswertungen1.Id = "6921344b-7bbe-45a8-a20d-9d2bbe45dd5c";
            resources.ApplyResources(this.rgrAuswertungen1, "rgrAuswertungen1");
            this.rgrAuswertungen1.Name = "rgrAuswertungen1";
            // 
            // cmdOperationenView
            // 
            this.cmdOperationenView.CommandName = "OperationenView";
            this.cmdOperationenView.Id = "73a189ac-371b-4aba-b4e2-8e0dcb154ef4";
            this.cmdOperationenView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdOperationenView.KeyTip = "1";
            this.cmdOperationenView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Table32)});
            resources.ApplyResources(this.cmdOperationenView, "cmdOperationenView");
            this.cmdOperationenView.Name = "cmdOperationenView";
            this.cmdOperationenView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Table16)});
            // 
            // cmdOperationenZeitenVergleichView
            // 
            this.cmdOperationenZeitenVergleichView.CommandName = "OperationenZeitenVergleichView";
            this.cmdOperationenZeitenVergleichView.Id = "6ecd1d6b-dbcf-4ad2-a688-c77ce26f556a";
            this.cmdOperationenZeitenVergleichView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdOperationenZeitenVergleichView.KeyTip = "2";
            this.cmdOperationenZeitenVergleichView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Table_Clock32)});
            resources.ApplyResources(this.cmdOperationenZeitenVergleichView, "cmdOperationenZeitenVergleichView");
            this.cmdOperationenZeitenVergleichView.Name = "cmdOperationenZeitenVergleichView";
            this.cmdOperationenZeitenVergleichView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Table_Clock16)});
            // 
            // cmdOPDauerFortschrittView
            // 
            this.cmdOPDauerFortschrittView.CommandName = "OPDauerFortschrittView";
            this.cmdOPDauerFortschrittView.Id = "b065f45e-33dd-4550-9e7a-12b335136d5d";
            this.cmdOPDauerFortschrittView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdOPDauerFortschrittView.KeyTip = "3";
            this.cmdOPDauerFortschrittView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Table_Timer32)});
            resources.ApplyResources(this.cmdOPDauerFortschrittView, "cmdOPDauerFortschrittView");
            this.cmdOPDauerFortschrittView.Name = "cmdOPDauerFortschrittView";
            this.cmdOPDauerFortschrittView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Table_Timer16)});
            // 
            // cmdKlinischeErgebnisseView
            // 
            this.cmdKlinischeErgebnisseView.CommandName = "KlinischeErgebnisseView";
            this.cmdKlinischeErgebnisseView.Id = "7b625f73-5960-4a4a-ad24-67ad4a975343";
            this.cmdKlinischeErgebnisseView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdKlinischeErgebnisseView.KeyTip = "4";
            this.cmdKlinischeErgebnisseView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Table_Death32)});
            resources.ApplyResources(this.cmdKlinischeErgebnisseView, "cmdKlinischeErgebnisseView");
            this.cmdKlinischeErgebnisseView.Name = "cmdKlinischeErgebnisseView";
            this.cmdKlinischeErgebnisseView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Table_Death16)});
            // 
            // rgrAuswertungen2
            // 
            this.rgrAuswertungen2.Controls.Add(this.cmdChirurgOperationenView);
            this.rgrAuswertungen2.Controls.Add(this.cmdGesamtOperationenView);
            this.rgrAuswertungen2.Controls.Add(this.cmdPlanOperationVergleichIstView);
            this.rgrAuswertungen2.Controls.Add(this.cmdOperationenVergleichView);
            this.rgrAuswertungen2.DialogLauncherButtonEnabled = false;
            this.rgrAuswertungen2.DialogLauncherButtonVisible = false;
            this.rgrAuswertungen2.Id = "23f04844-ac23-4fb2-9aec-6ae6fbfaedc9";
            resources.ApplyResources(this.rgrAuswertungen2, "rgrAuswertungen2");
            this.rgrAuswertungen2.Name = "rgrAuswertungen2";
            // 
            // cmdChirurgOperationenView
            // 
            this.cmdChirurgOperationenView.CommandName = "ChirurgOperationenView";
            this.cmdChirurgOperationenView.Id = "1de2a1ac-a8b6-4ca5-ba62-a93c652b2d26";
            this.cmdChirurgOperationenView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdChirurgOperationenView.KeyTip = "5";
            this.cmdChirurgOperationenView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources._3Dchart_PeopleContact32)});
            resources.ApplyResources(this.cmdChirurgOperationenView, "cmdChirurgOperationenView");
            this.cmdChirurgOperationenView.Name = "cmdChirurgOperationenView";
            this.cmdChirurgOperationenView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources._3Dchart_PeopleContact16)});
            // 
            // cmdGesamtOperationenView
            // 
            this.cmdGesamtOperationenView.CommandName = "GesamtOperationenView";
            this.cmdGesamtOperationenView.Id = "359a7b8f-a82d-4c4f-b4df-440605b32255";
            this.cmdGesamtOperationenView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdGesamtOperationenView.KeyTip = "6";
            this.cmdGesamtOperationenView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources._3Dchart_PeopleContacts32)});
            resources.ApplyResources(this.cmdGesamtOperationenView, "cmdGesamtOperationenView");
            this.cmdGesamtOperationenView.Name = "cmdGesamtOperationenView";
            this.cmdGesamtOperationenView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources._3Dchart_PeopleContacts16)});
            // 
            // cmdPlanOperationVergleichIstView
            // 
            this.cmdPlanOperationVergleichIstView.CommandName = "PlanOperationVergleichIstView";
            this.cmdPlanOperationVergleichIstView.Id = "fbae2795-b4f7-43c5-9b98-9c8ae7930d9f";
            this.cmdPlanOperationVergleichIstView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdPlanOperationVergleichIstView.KeyTip = "7";
            this.cmdPlanOperationVergleichIstView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.PieChart_PeopleContact32)});
            resources.ApplyResources(this.cmdPlanOperationVergleichIstView, "cmdPlanOperationVergleichIstView");
            this.cmdPlanOperationVergleichIstView.Name = "cmdPlanOperationVergleichIstView";
            this.cmdPlanOperationVergleichIstView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.PieChart_PeopleContact16)});
            // 
            // cmdOperationenVergleichView
            // 
            this.cmdOperationenVergleichView.CommandName = "OperationenVergleichView";
            this.cmdOperationenVergleichView.Id = "63b01544-0abf-4371-b537-3fcdac54dc6d";
            this.cmdOperationenVergleichView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdOperationenVergleichView.KeyTip = "8";
            this.cmdOperationenVergleichView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.PieChart_PeopleContacts32)});
            resources.ApplyResources(this.cmdOperationenVergleichView, "cmdOperationenVergleichView");
            this.cmdOperationenVergleichView.Name = "cmdOperationenVergleichView";
            this.cmdOperationenVergleichView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.PieChart_PeopleContacts16)});
            // 
            // rgrAuswertungen3
            // 
            this.rgrAuswertungen3.CommandName = "";
            this.rgrAuswertungen3.Controls.Add(this.cmdRichtlinienVergleichView);
            this.rgrAuswertungen3.Controls.Add(this.cmdRichtlinienVergleichOverviewView);
            this.rgrAuswertungen3.DialogLauncherButtonEnabled = false;
            this.rgrAuswertungen3.DialogLauncherButtonVisible = false;
            this.rgrAuswertungen3.Id = "db48f0dc-850b-4cd4-86c0-53911a389f93";
            resources.ApplyResources(this.rgrAuswertungen3, "rgrAuswertungen3");
            this.rgrAuswertungen3.Name = "rgrAuswertungen3";
            // 
            // cmdRichtlinienVergleichView
            // 
            this.cmdRichtlinienVergleichView.CommandName = "RichtlinienVergleichView";
            this.cmdRichtlinienVergleichView.Id = "23eda642-3c42-4520-8235-01792422dd86";
            this.cmdRichtlinienVergleichView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdRichtlinienVergleichView.KeyTip = "9";
            this.cmdRichtlinienVergleichView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Certification_PeopleContact32)});
            resources.ApplyResources(this.cmdRichtlinienVergleichView, "cmdRichtlinienVergleichView");
            this.cmdRichtlinienVergleichView.Name = "cmdRichtlinienVergleichView";
            this.cmdRichtlinienVergleichView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Certification_PeopleContact16)});
            // 
            // cmdRichtlinienVergleichOverviewView
            // 
            this.cmdRichtlinienVergleichOverviewView.CommandName = "RichtlinienVergleichOverviewView";
            this.cmdRichtlinienVergleichOverviewView.Id = "828f98a8-121c-44cc-b610-89659741e866";
            this.cmdRichtlinienVergleichOverviewView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdRichtlinienVergleichOverviewView.KeyTip = "A";
            this.cmdRichtlinienVergleichOverviewView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Certification_PeopleContacts32)});
            resources.ApplyResources(this.cmdRichtlinienVergleichOverviewView, "cmdRichtlinienVergleichOverviewView");
            this.cmdRichtlinienVergleichOverviewView.Name = "cmdRichtlinienVergleichOverviewView";
            this.cmdRichtlinienVergleichOverviewView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Certification_PeopleContacts16)});
            // 
            // rtpImport
            // 
            this.rtpImport.Controls.Add(this.rgrImport1);
            this.rtpImport.Controls.Add(this.rgrImport2);
            this.rtpImport.Controls.Add(this.rgrImport3);
            resources.ApplyResources(this.rtpImport, "rtpImport");
            this.rtpImport.Id = "9f4b9e92-d04c-4729-bf04-891a3924eb50";
            this.rtpImport.KeyTip = "I";
            this.rtpImport.Name = "rtpImport";
            // 
            // rgrImport1
            // 
            this.rgrImport1.Controls.Add(this.cmdOperationenImportView);
            this.rgrImport1.Controls.Add(this.cmdImportChirurgenExcludeView);
            this.rgrImport1.Controls.Add(this.cmdExecAutoImport);
            this.rgrImport1.DialogLauncherButtonEnabled = false;
            this.rgrImport1.DialogLauncherButtonVisible = false;
            this.rgrImport1.Id = "d1478002-71ff-4cd8-aee9-94d896e18783";
            resources.ApplyResources(this.rgrImport1, "rgrImport1");
            this.rgrImport1.Name = "rgrImport1";
            // 
            // cmdOperationenImportView
            // 
            this.cmdOperationenImportView.CommandName = "OperationenImportView";
            this.cmdOperationenImportView.Id = "bddb42e4-cd84-420a-adc1-8a4daae546a7";
            this.cmdOperationenImportView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdOperationenImportView.KeyTip = "1";
            this.cmdOperationenImportView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Database_Back32)});
            resources.ApplyResources(this.cmdOperationenImportView, "cmdOperationenImportView");
            this.cmdOperationenImportView.Name = "cmdOperationenImportView";
            this.cmdOperationenImportView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Database_Back16)});
            // 
            // cmdImportChirurgenExcludeView
            // 
            this.cmdImportChirurgenExcludeView.CommandName = "ImportChirurgenExcludeView";
            this.cmdImportChirurgenExcludeView.Id = "fe1e5711-7cc7-467c-af19-0e3afd5a0855";
            this.cmdImportChirurgenExcludeView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdImportChirurgenExcludeView.KeyTip = "2";
            this.cmdImportChirurgenExcludeView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Surgeon_Back_Erase32)});
            resources.ApplyResources(this.cmdImportChirurgenExcludeView, "cmdImportChirurgenExcludeView");
            this.cmdImportChirurgenExcludeView.Name = "cmdImportChirurgenExcludeView";
            this.cmdImportChirurgenExcludeView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Surgeon_Back_Erase16)});
            // 
            // cmdExecAutoImport
            // 
            this.cmdExecAutoImport.CommandName = "ExecAutoImport";
            this.cmdExecAutoImport.Id = "467a444f-c838-40dd-b229-9a6b5006f17f";
            this.cmdExecAutoImport.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdExecAutoImport.KeyTip = "3";
            this.cmdExecAutoImport.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Database_Clock_Back32)});
            resources.ApplyResources(this.cmdExecAutoImport, "cmdExecAutoImport");
            this.cmdExecAutoImport.Name = "cmdExecAutoImport";
            this.cmdExecAutoImport.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Database_Clock_Back16)});
            // 
            // rgrImport2
            // 
            this.rgrImport2.Controls.Add(this.cmdImportRichtlinienWizard);
            this.rgrImport2.Controls.Add(this.cmdImportRichtlinienZuordnungWizard);
            this.rgrImport2.DialogLauncherButtonEnabled = false;
            this.rgrImport2.DialogLauncherButtonVisible = false;
            this.rgrImport2.Id = "38d5d119-ce5c-4c50-adf0-8b919ff89dc4";
            resources.ApplyResources(this.rgrImport2, "rgrImport2");
            this.rgrImport2.Name = "rgrImport2";
            // 
            // cmdImportRichtlinienWizard
            // 
            this.cmdImportRichtlinienWizard.CommandName = "ImportRichtlinienWizard";
            this.cmdImportRichtlinienWizard.Id = "70d19139-c53b-44f5-8a87-65f58b1dcfd9";
            this.cmdImportRichtlinienWizard.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdImportRichtlinienWizard.KeyTip = "4";
            this.cmdImportRichtlinienWizard.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Certification_Back32)});
            resources.ApplyResources(this.cmdImportRichtlinienWizard, "cmdImportRichtlinienWizard");
            this.cmdImportRichtlinienWizard.Name = "cmdImportRichtlinienWizard";
            this.cmdImportRichtlinienWizard.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Certification_Back16)});
            // 
            // cmdImportRichtlinienZuordnungWizard
            // 
            this.cmdImportRichtlinienZuordnungWizard.CommandName = "ImportRichtlinienZuordnungWizard";
            this.cmdImportRichtlinienZuordnungWizard.Id = "7d51f300-3c2b-47ce-9faa-f341d4410f61";
            this.cmdImportRichtlinienZuordnungWizard.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdImportRichtlinienZuordnungWizard.KeyTip = "5";
            this.cmdImportRichtlinienZuordnungWizard.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.FlipHorizontally_Back32)});
            resources.ApplyResources(this.cmdImportRichtlinienZuordnungWizard, "cmdImportRichtlinienZuordnungWizard");
            this.cmdImportRichtlinienZuordnungWizard.Name = "cmdImportRichtlinienZuordnungWizard";
            this.cmdImportRichtlinienZuordnungWizard.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.FlipHorizontally_Back16)});
            // 
            // rgrImport3
            // 
            this.rgrImport3.Controls.Add(this.cmdImportChirurgWizard);
            this.rgrImport3.Controls.Add(this.cmdImportOPSWizard);
            this.rgrImport3.Controls.Add(this.cmdImportOperationenMobileWizard);
            this.rgrImport3.Controls.Add(this.cmdClientServerViewImport);
            this.rgrImport3.DialogLauncherButtonEnabled = false;
            this.rgrImport3.DialogLauncherButtonVisible = false;
            this.rgrImport3.Id = "7e08a5bb-13e5-4b2c-bfdc-b52998774fb9";
            resources.ApplyResources(this.rgrImport3, "rgrImport3");
            this.rgrImport3.Name = "rgrImport3";
            // 
            // cmdImportChirurgWizard
            // 
            this.cmdImportChirurgWizard.CommandName = "ImportChirurgWizard";
            this.cmdImportChirurgWizard.Id = "2cec8547-7cb7-4473-8540-babcb995edfd";
            this.cmdImportChirurgWizard.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdImportChirurgWizard.KeyTip = "6";
            this.cmdImportChirurgWizard.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Surgeon_Back32)});
            resources.ApplyResources(this.cmdImportChirurgWizard, "cmdImportChirurgWizard");
            this.cmdImportChirurgWizard.Name = "cmdImportChirurgWizard";
            this.cmdImportChirurgWizard.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Surgeon_Back16)});
            // 
            // cmdImportOPSWizard
            // 
            this.cmdImportOPSWizard.CommandName = "ImportOPSWizard";
            this.cmdImportOPSWizard.Id = "244947f1-c756-434e-b7d7-8912e8e5f65e";
            this.cmdImportOPSWizard.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdImportOPSWizard.KeyTip = "7";
            this.cmdImportOPSWizard.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Book_Back32)});
            resources.ApplyResources(this.cmdImportOPSWizard, "cmdImportOPSWizard");
            this.cmdImportOPSWizard.Name = "cmdImportOPSWizard";
            this.cmdImportOPSWizard.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Book_Back16)});
            // 
            // cmdImportOperationenMobileWizard
            // 
            this.cmdImportOperationenMobileWizard.CommandName = "ImportOperationenMobileWizard";
            this.cmdImportOperationenMobileWizard.Id = "254688b5-e81a-47c0-9883-d20d94a83897";
            this.cmdImportOperationenMobileWizard.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdImportOperationenMobileWizard.KeyTip = "8";
            this.cmdImportOperationenMobileWizard.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.MobilePhone_Back32)});
            resources.ApplyResources(this.cmdImportOperationenMobileWizard, "cmdImportOperationenMobileWizard");
            this.cmdImportOperationenMobileWizard.Name = "cmdImportOperationenMobileWizard";
            this.cmdImportOperationenMobileWizard.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.MobilePhone_Back16)});
            // 
            // cmdClientServerViewImport
            // 
            this.cmdClientServerViewImport.CommandName = "ClientServerView";
            this.cmdClientServerViewImport.Id = "394c316d-1020-47f8-81f4-51328ebd0b9a";
            this.cmdClientServerViewImport.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdClientServerViewImport.KeyTip = "9";
            this.cmdClientServerViewImport.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.PcPdaSynchronization32)});
            resources.ApplyResources(this.cmdClientServerViewImport, "cmdClientServerViewImport");
            this.cmdClientServerViewImport.Name = "cmdClientServerViewImport";
            this.cmdClientServerViewImport.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.PcPdaSynchronization16)});
            this.cmdClientServerViewImport.Click += new System.EventHandler(this.cmdClientServerViewImport_Click);
            // 
            // rtpExport
            // 
            this.rtpExport.Controls.Add(this.rgrExport1);
            this.rtpExport.Controls.Add(this.rgrExport2);
            resources.ApplyResources(this.rtpExport, "rtpExport");
            this.rtpExport.Id = "7453c21d-d38c-46fe-9d2b-eef704571bb0";
            this.rtpExport.KeyTip = "E";
            this.rtpExport.Name = "rtpExport";
            // 
            // rgrExport1
            // 
            this.rgrExport1.Controls.Add(this.cmdExportRichtlinienWizard);
            this.rgrExport1.Controls.Add(this.cmdExportRichtlinienZuordnungWizard);
            this.rgrExport1.DialogLauncherButtonEnabled = false;
            this.rgrExport1.DialogLauncherButtonVisible = false;
            this.rgrExport1.Id = "67fe5206-7fa4-4ac6-8530-83e00c11d4ba";
            resources.ApplyResources(this.rgrExport1, "rgrExport1");
            this.rgrExport1.Name = "rgrExport1";
            // 
            // cmdExportRichtlinienWizard
            // 
            this.cmdExportRichtlinienWizard.CommandName = "ExportRichtlinienWizard";
            this.cmdExportRichtlinienWizard.Id = "076900cc-c5cf-407b-9059-e906041c77ec";
            this.cmdExportRichtlinienWizard.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdExportRichtlinienWizard.KeyTip = "1";
            this.cmdExportRichtlinienWizard.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Certification_RightArrow32)});
            resources.ApplyResources(this.cmdExportRichtlinienWizard, "cmdExportRichtlinienWizard");
            this.cmdExportRichtlinienWizard.Name = "cmdExportRichtlinienWizard";
            this.cmdExportRichtlinienWizard.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Certification_RightArrow16)});
            // 
            // cmdExportRichtlinienZuordnungWizard
            // 
            this.cmdExportRichtlinienZuordnungWizard.CommandName = "ExportRichtlinienZuordnungWizard";
            this.cmdExportRichtlinienZuordnungWizard.Id = "b60ee2ff-14e4-4130-a8ce-b5dc8db760d6";
            this.cmdExportRichtlinienZuordnungWizard.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdExportRichtlinienZuordnungWizard.KeyTip = "2";
            this.cmdExportRichtlinienZuordnungWizard.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.FlipHorizontally_RightArrow32)});
            resources.ApplyResources(this.cmdExportRichtlinienZuordnungWizard, "cmdExportRichtlinienZuordnungWizard");
            this.cmdExportRichtlinienZuordnungWizard.Name = "cmdExportRichtlinienZuordnungWizard";
            this.cmdExportRichtlinienZuordnungWizard.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.FlipHorizontally_RightArrow16)});
            // 
            // rgrExport2
            // 
            this.rgrExport2.Controls.Add(this.cmdExportChirurgWizard);
            this.rgrExport2.Controls.Add(this.cmdExportOperationenKatalogView);
            this.rgrExport2.Controls.Add(this.cmdClientServerViewExport);
            this.rgrExport2.DialogLauncherButtonEnabled = false;
            this.rgrExport2.DialogLauncherButtonVisible = false;
            this.rgrExport2.Id = "7ed2c100-cc89-4cc9-bda1-e4c7da64c277";
            resources.ApplyResources(this.rgrExport2, "rgrExport2");
            this.rgrExport2.Name = "rgrExport2";
            // 
            // cmdExportChirurgWizard
            // 
            this.cmdExportChirurgWizard.CommandName = "ExportChirurgWizard";
            this.cmdExportChirurgWizard.Id = "c948c6b6-fd69-4ed5-8eb0-01dd96d907e9";
            this.cmdExportChirurgWizard.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdExportChirurgWizard.KeyTip = "3";
            this.cmdExportChirurgWizard.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Surgeon_RightArrow32)});
            resources.ApplyResources(this.cmdExportChirurgWizard, "cmdExportChirurgWizard");
            this.cmdExportChirurgWizard.Name = "cmdExportChirurgWizard";
            this.cmdExportChirurgWizard.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Surgeon_RightArrow16)});
            // 
            // cmdExportOperationenKatalogView
            // 
            this.cmdExportOperationenKatalogView.CommandName = "ExportOperationenKatalogView";
            this.cmdExportOperationenKatalogView.Id = "8bb1e0a9-4f52-4434-92c0-6eeda92de7af";
            this.cmdExportOperationenKatalogView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdExportOperationenKatalogView.KeyTip = "4";
            this.cmdExportOperationenKatalogView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.MobilePhone_RightArrow32)});
            resources.ApplyResources(this.cmdExportOperationenKatalogView, "cmdExportOperationenKatalogView");
            this.cmdExportOperationenKatalogView.Name = "cmdExportOperationenKatalogView";
            this.cmdExportOperationenKatalogView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.MobilePhone_RightArrow16)});
            // 
            // cmdClientServerViewExport
            // 
            this.cmdClientServerViewExport.CommandName = "ClientServerView";
            this.cmdClientServerViewExport.Id = "e97b3abf-344a-4646-83cf-1be56535cb40";
            this.cmdClientServerViewExport.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdClientServerViewExport.KeyTip = "5";
            this.cmdClientServerViewExport.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.PcPdaSynchronization32)});
            resources.ApplyResources(this.cmdClientServerViewExport, "cmdClientServerViewExport");
            this.cmdClientServerViewExport.Name = "cmdClientServerViewExport";
            this.cmdClientServerViewExport.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.PcPdaSynchronization16)});
            // 
            // rtpHilfe
            // 
            this.rtpHilfe.Controls.Add(this.rgrHilfe1);
            this.rtpHilfe.Controls.Add(this.rgrHilfe2);
            resources.ApplyResources(this.rtpHilfe, "rtpHilfe");
            this.rtpHilfe.Id = "dcf10895-5f0b-4a60-a859-5d75eba4dc6d";
            this.rtpHilfe.KeyTip = "H";
            this.rtpHilfe.Name = "rtpHilfe";
            // 
            // rgrHilfe1
            // 
            this.rgrHilfe1.Controls.Add(this.cmdHelpChm);
            this.rgrHilfe1.Controls.Add(this.cmdWwwHelp);
            this.rgrHilfe1.Controls.Add(this.cmdWwwHome);
            this.rgrHilfe1.Controls.Add(this.cmdAboutBox);
            this.rgrHilfe1.DialogLauncherButtonEnabled = false;
            this.rgrHilfe1.DialogLauncherButtonVisible = false;
            this.rgrHilfe1.Id = "74bc18ad-aa3c-42ca-8df7-20e4ad969909";
            resources.ApplyResources(this.rgrHilfe1, "rgrHilfe1");
            this.rgrHilfe1.Name = "rgrHilfe1";
            // 
            // cmdHelpChm
            // 
            this.cmdHelpChm.CommandName = "HelpChm";
            this.cmdHelpChm.Id = "3ff9dc3c-c96a-4c0f-85d1-ac39ac4a1d6e";
            this.cmdHelpChm.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdHelpChm.KeyTip = "1";
            this.cmdHelpChm.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Help32)});
            resources.ApplyResources(this.cmdHelpChm, "cmdHelpChm");
            this.cmdHelpChm.Name = "cmdHelpChm";
            this.cmdHelpChm.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Help16)});
            // 
            // cmdWwwHelp
            // 
            this.cmdWwwHelp.CommandName = "WwwHelp";
            this.cmdWwwHelp.Id = "ed88d3b7-ce51-4305-929f-31ef98cbdd40";
            this.cmdWwwHelp.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdWwwHelp.KeyTip = "2";
            this.cmdWwwHelp.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Earth_Help32)});
            resources.ApplyResources(this.cmdWwwHelp, "cmdWwwHelp");
            this.cmdWwwHelp.Name = "cmdWwwHelp";
            this.cmdWwwHelp.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Earth_Help16)});
            // 
            // cmdWwwHome
            // 
            this.cmdWwwHome.CommandName = "WwwHome";
            this.cmdWwwHome.Id = "e69324a2-3a1e-42e1-a225-f54c22c78be4";
            this.cmdWwwHome.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdWwwHome.KeyTip = "3";
            this.cmdWwwHome.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Earth_Surgeon32)});
            resources.ApplyResources(this.cmdWwwHome, "cmdWwwHome");
            this.cmdWwwHome.Name = "cmdWwwHome";
            this.cmdWwwHome.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Earth_Surgeon16)});
            // 
            // cmdAboutBox
            // 
            this.cmdAboutBox.CommandName = "AboutBox";
            this.cmdAboutBox.Id = "e0f9ca95-aeb5-429f-a04d-523eaff166a9";
            this.cmdAboutBox.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdAboutBox.KeyTip = "4";
            this.cmdAboutBox.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Question32)});
            resources.ApplyResources(this.cmdAboutBox, "cmdAboutBox");
            this.cmdAboutBox.Name = "cmdAboutBox";
            this.cmdAboutBox.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Question16)});
            // 
            // rgrHilfe2
            // 
            this.rgrHilfe2.Controls.Add(this.ddVideoTutorials);
            this.rgrHilfe2.DialogLauncherButtonEnabled = false;
            this.rgrHilfe2.DialogLauncherButtonVisible = false;
            this.rgrHilfe2.Id = "63ff2865-40cf-491a-abfe-fa8c5f8ab23a";
            resources.ApplyResources(this.rgrHilfe2, "rgrHilfe2");
            this.rgrHilfe2.Name = "rgrHilfe2";
            // 
            // ddVideoTutorials
            // 
            this.ddVideoTutorials.Id = "9c96d769-ed45-4589-8d81-be55d87fab25";
            this.ddVideoTutorials.InformativenessFixedLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.ddVideoTutorials.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.ddVideoTutorials.KeyTip = "5";
            this.ddVideoTutorials.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Camcorder32)});
            resources.ApplyResources(this.ddVideoTutorials, "ddVideoTutorials");
            this.ddVideoTutorials.Name = "ddVideoTutorials";
            this.ddVideoTutorials.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Camcorder16)});
            // 
            // rtpDebug
            // 
            this.rtpDebug.Controls.Add(this.ribbonGroup1);
            resources.ApplyResources(this.rtpDebug, "rtpDebug");
            this.rtpDebug.Id = "59266420-482c-4430-819b-604bc4923c82";
            this.rtpDebug.Name = "rtpDebug";
            // 
            // ribbonGroup1
            // 
            this.ribbonGroup1.Controls.Add(this.cmdDebugSql);
            this.ribbonGroup1.Controls.Add(this.cmdDebugExportOperationen);
            this.ribbonGroup1.Controls.Add(this.cmdUpdateSerialnumbersView);
            this.ribbonGroup1.Controls.Add(this.cmdDebugReloadUserRights);
            this.ribbonGroup1.Controls.Add(this.cmdZeitraeumeView);
            this.ribbonGroup1.DialogLauncherButtonEnabled = false;
            this.ribbonGroup1.DialogLauncherButtonVisible = false;
            this.ribbonGroup1.Id = "866afbbb-df3b-49a2-92f4-479adec56ab4";
            resources.ApplyResources(this.ribbonGroup1, "ribbonGroup1");
            this.ribbonGroup1.Name = "ribbonGroup1";
            // 
            // cmdDebugSql
            // 
            this.cmdDebugSql.CommandName = "DebugSql";
            this.cmdDebugSql.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdDebugSql.Id = "9bc92a4b-bd02-41eb-9810-ec20ec189e70";
            resources.ApplyResources(this.cmdDebugSql, "cmdDebugSql");
            this.cmdDebugSql.Name = "cmdDebugSql";
            // 
            // cmdDebugExportOperationen
            // 
            this.cmdDebugExportOperationen.CommandName = "DebugExportOperationen";
            this.cmdDebugExportOperationen.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdDebugExportOperationen.Id = "08406dc0-ca6f-496a-b9bf-1cb7617f91fc";
            resources.ApplyResources(this.cmdDebugExportOperationen, "cmdDebugExportOperationen");
            this.cmdDebugExportOperationen.Name = "cmdDebugExportOperationen";
            // 
            // cmdUpdateSerialnumbersView
            // 
            this.cmdUpdateSerialnumbersView.CommandName = "UpdateSerialnumbersView";
            this.cmdUpdateSerialnumbersView.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdUpdateSerialnumbersView.Id = "de77bda6-ce23-443b-bb2c-d7ba882a605e";
            resources.ApplyResources(this.cmdUpdateSerialnumbersView, "cmdUpdateSerialnumbersView");
            this.cmdUpdateSerialnumbersView.Name = "cmdUpdateSerialnumbersView";
            // 
            // cmdDebugReloadUserRights
            // 
            this.cmdDebugReloadUserRights.CommandName = "DebugReloadUserRights";
            this.cmdDebugReloadUserRights.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdDebugReloadUserRights.Id = "de7f81c8-d7a0-455f-a302-5ce5bfbe7771";
            resources.ApplyResources(this.cmdDebugReloadUserRights, "cmdDebugReloadUserRights");
            this.cmdDebugReloadUserRights.Name = "cmdDebugReloadUserRights";
            // 
            // cmdZeitraeumeView
            // 
            this.cmdZeitraeumeView.CommandName = "ZeitraeumeView";
            this.cmdZeitraeumeView.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdZeitraeumeView.Id = "ae2e7153-fe65-456f-add8-1e4258ec6b61";
            resources.ApplyResources(this.cmdZeitraeumeView, "cmdZeitraeumeView");
            this.cmdZeitraeumeView.Name = "cmdZeitraeumeView";
            // 
            // rtpExtras
            // 
            this.rtpExtras.Controls.Add(this.rgrExtras1);
            this.rtpExtras.Controls.Add(this.rgrExtras2);
            this.rtpExtras.Controls.Add(this.rgrExtras3);
            this.rtpExtras.Controls.Add(this.rgrExtras4);
            resources.ApplyResources(this.rtpExtras, "rtpExtras");
            this.rtpExtras.Id = "2bc59ea2-b923-4ad9-a42b-68836238deac";
            this.rtpExtras.KeyTip = "X";
            this.rtpExtras.Name = "rtpExtras";
            // 
            // rgrExtras1
            // 
            this.rgrExtras1.Controls.Add(this.cmdUserChangePasswordView);
            this.rgrExtras1.Controls.Add(this.cmdUserSetPasswordView);
            this.rgrExtras1.DialogLauncherButtonEnabled = false;
            this.rgrExtras1.DialogLauncherButtonVisible = false;
            this.rgrExtras1.Id = "bffed70a-4877-4c56-b3ae-f2f9c472539f";
            resources.ApplyResources(this.rgrExtras1, "rgrExtras1");
            this.rgrExtras1.Name = "rgrExtras1";
            // 
            // cmdUserChangePasswordView
            // 
            this.cmdUserChangePasswordView.CommandName = "UserChangePasswordView";
            this.cmdUserChangePasswordView.Id = "9e86fb45-fde8-4d66-9bbd-3531733fd3b2";
            this.cmdUserChangePasswordView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdUserChangePasswordView.KeyTip = "1";
            this.cmdUserChangePasswordView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Registration32)});
            resources.ApplyResources(this.cmdUserChangePasswordView, "cmdUserChangePasswordView");
            this.cmdUserChangePasswordView.Name = "cmdUserChangePasswordView";
            this.cmdUserChangePasswordView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Registration16)});
            // 
            // cmdUserSetPasswordView
            // 
            this.cmdUserSetPasswordView.CommandName = "UserSetPasswordView";
            this.cmdUserSetPasswordView.Id = "bec8424b-feb0-4041-a0cc-f589e5434461";
            this.cmdUserSetPasswordView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdUserSetPasswordView.KeyTip = "2";
            this.cmdUserSetPasswordView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.User_Registration32)});
            resources.ApplyResources(this.cmdUserSetPasswordView, "cmdUserSetPasswordView");
            this.cmdUserSetPasswordView.Name = "cmdUserSetPasswordView";
            this.cmdUserSetPasswordView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.User_Registration16)});
            // 
            // rgrExtras2
            // 
            this.rgrExtras2.Controls.Add(this.cmdSerialNumbersView);
            this.rgrExtras2.Controls.Add(this.cmdSerialBuy);
            this.rgrExtras2.Controls.Add(this.cmdInstallLicenseWizard);
            this.rgrExtras2.DialogLauncherButtonEnabled = false;
            this.rgrExtras2.DialogLauncherButtonVisible = false;
            this.rgrExtras2.Id = "30dfc3b1-8f8f-4542-adfe-0db92ad1c7ad";
            resources.ApplyResources(this.rgrExtras2, "rgrExtras2");
            this.rgrExtras2.Name = "rgrExtras2";
            // 
            // cmdSerialNumbersView
            // 
            this.cmdSerialNumbersView.CommandName = "SerialNumbersView";
            this.cmdSerialNumbersView.Id = "0db92fa0-837f-4172-94f5-a18135b6ba80";
            this.cmdSerialNumbersView.InformativenessMaximumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:SmallImageWithText";
            this.cmdSerialNumbersView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:SmallImageWithText";
            this.cmdSerialNumbersView.KeyTip = "3";
            resources.ApplyResources(this.cmdSerialNumbersView, "cmdSerialNumbersView");
            this.cmdSerialNumbersView.Name = "cmdSerialNumbersView";
            this.cmdSerialNumbersView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.BarCode_BlackPencil16)});
            // 
            // cmdSerialBuy
            // 
            this.cmdSerialBuy.CommandName = "SerialBuy";
            this.cmdSerialBuy.Id = "4c4056d5-c0ee-45e7-8e58-d4e22e943c4b";
            this.cmdSerialBuy.InformativenessMaximumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:SmallImageWithText";
            this.cmdSerialBuy.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:SmallImageWithText";
            this.cmdSerialBuy.KeyTip = "4";
            resources.ApplyResources(this.cmdSerialBuy, "cmdSerialBuy");
            this.cmdSerialBuy.Name = "cmdSerialBuy";
            this.cmdSerialBuy.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Earth_BarCode16)});
            // 
            // cmdInstallLicenseWizard
            // 
            this.cmdInstallLicenseWizard.CommandName = "InstallLicenseWizard";
            this.cmdInstallLicenseWizard.Id = "0a5eb8b4-f58c-44db-b7a9-8693c178cfa3";
            this.cmdInstallLicenseWizard.InformativenessMaximumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:SmallImageWithText";
            this.cmdInstallLicenseWizard.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:SmallImageWithText";
            this.cmdInstallLicenseWizard.KeyTip = "5";
            resources.ApplyResources(this.cmdInstallLicenseWizard, "cmdInstallLicenseWizard");
            this.cmdInstallLicenseWizard.Name = "cmdInstallLicenseWizard";
            this.cmdInstallLicenseWizard.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.BarCode_Back16)});
            // 
            // rgrExtras3
            // 
            this.rgrExtras3.Controls.Add(this.cmdUpdateFromFolder);
            this.rgrExtras3.Controls.Add(this.cmdUpdateFromWww);
            this.rgrExtras3.Controls.Add(this.cmdCopyWWWProgramUpdateFilesView);
            this.rgrExtras3.DialogLauncherButtonEnabled = false;
            this.rgrExtras3.DialogLauncherButtonVisible = false;
            this.rgrExtras3.Id = "bed72846-5a30-481f-9143-b19115384931";
            resources.ApplyResources(this.rgrExtras3, "rgrExtras3");
            this.rgrExtras3.Name = "rgrExtras3";
            // 
            // cmdUpdateFromFolder
            // 
            this.cmdUpdateFromFolder.CommandName = "UpdateFromFolder";
            this.cmdUpdateFromFolder.Id = "feb9c505-f9bb-4737-8813-cf722faade74";
            this.cmdUpdateFromFolder.InformativenessMaximumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:SmallImageWithText";
            this.cmdUpdateFromFolder.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:SmallImageWithText";
            this.cmdUpdateFromFolder.KeyTip = "6";
            resources.ApplyResources(this.cmdUpdateFromFolder, "cmdUpdateFromFolder");
            this.cmdUpdateFromFolder.Name = "cmdUpdateFromFolder";
            this.cmdUpdateFromFolder.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.OpenFile_Question16)});
            // 
            // cmdUpdateFromWww
            // 
            this.cmdUpdateFromWww.CommandName = "UpdateFromWww";
            this.cmdUpdateFromWww.Id = "9c748023-eb74-42bf-a5d8-5cc1042d0905";
            this.cmdUpdateFromWww.InformativenessMaximumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:SmallImageWithText";
            this.cmdUpdateFromWww.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:SmallImageWithText";
            this.cmdUpdateFromWww.KeyTip = "7";
            resources.ApplyResources(this.cmdUpdateFromWww, "cmdUpdateFromWww");
            this.cmdUpdateFromWww.Name = "cmdUpdateFromWww";
            this.cmdUpdateFromWww.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Earth_Question16)});
            // 
            // cmdCopyWWWProgramUpdateFilesView
            // 
            this.cmdCopyWWWProgramUpdateFilesView.CommandName = "CopyWWWProgramUpdateFilesView";
            this.cmdCopyWWWProgramUpdateFilesView.Id = "1bf4ba66-f99d-44b1-a9d6-a5b73527451c";
            this.cmdCopyWWWProgramUpdateFilesView.InformativenessMaximumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:SmallImageWithText";
            this.cmdCopyWWWProgramUpdateFilesView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:SmallImageWithText";
            this.cmdCopyWWWProgramUpdateFilesView.KeyTip = "8";
            resources.ApplyResources(this.cmdCopyWWWProgramUpdateFilesView, "cmdCopyWWWProgramUpdateFilesView");
            this.cmdCopyWWWProgramUpdateFilesView.Name = "cmdCopyWWWProgramUpdateFilesView";
            this.cmdCopyWWWProgramUpdateFilesView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Open16)});
            // 
            // rgrExtras4
            // 
            this.rgrExtras4.Controls.Add(this.cmdLogView);
            this.rgrExtras4.Controls.Add(this.cmdOptionsView);
            this.rgrExtras4.Controls.Add(this.cmdLangDeDe);
            this.rgrExtras4.Controls.Add(this.cmdLangEnUs);
            this.rgrExtras4.DialogLauncherButtonEnabled = false;
            this.rgrExtras4.DialogLauncherButtonVisible = false;
            this.rgrExtras4.Id = "9e691719-28d9-4122-a1e3-276919660346";
            resources.ApplyResources(this.rgrExtras4, "rgrExtras4");
            this.rgrExtras4.Name = "rgrExtras4";
            // 
            // cmdLogView
            // 
            this.cmdLogView.CommandName = "LogView";
            this.cmdLogView.Id = "3f20fb7a-9a48-46c2-9386-55e57c616c64";
            this.cmdLogView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdLogView.KeyTip = "9";
            this.cmdLogView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.History32)});
            resources.ApplyResources(this.cmdLogView, "cmdLogView");
            this.cmdLogView.Name = "cmdLogView";
            this.cmdLogView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.History16)});
            // 
            // cmdOptionsView
            // 
            this.cmdOptionsView.CommandName = "OptionsView";
            this.cmdOptionsView.Id = "fd6c097f-484b-4b57-82f8-9f6c0123e793";
            this.cmdOptionsView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdOptionsView.KeyTip = "A";
            this.cmdOptionsView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Wrench32)});
            resources.ApplyResources(this.cmdOptionsView, "cmdOptionsView");
            this.cmdOptionsView.Name = "cmdOptionsView";
            this.cmdOptionsView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Wrench16)});
            // 
            // cmdLangDeDe
            // 
            this.cmdLangDeDe.CommandName = "LangDeDe";
            this.cmdLangDeDe.Id = "b3e1bbed-800a-4d01-be79-b7aeb2d004d0";
            this.cmdLangDeDe.InformativenessFixedLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:SmallImageWithText";
            this.cmdLangDeDe.InformativenessMaximumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:SmallImageWithText";
            this.cmdLangDeDe.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:SmallImageWithText";
            this.cmdLangDeDe.KeyTip = "B";
            resources.ApplyResources(this.cmdLangDeDe, "cmdLangDeDe");
            this.cmdLangDeDe.Name = "cmdLangDeDe";
            this.cmdLangDeDe.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.lang_de16)});
            // 
            // cmdLangEnUs
            // 
            this.cmdLangEnUs.CommandName = "LangEnUs";
            this.cmdLangEnUs.Id = "033cc21b-f28b-4539-bc93-0c17551d1aec";
            this.cmdLangEnUs.InformativenessFixedLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:SmallImageWithText";
            this.cmdLangEnUs.InformativenessMaximumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:SmallImageWithText";
            this.cmdLangEnUs.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:SmallImageWithText";
            this.cmdLangEnUs.KeyTip = "C";
            resources.ApplyResources(this.cmdLangEnUs, "cmdLangEnUs");
            this.cmdLangEnUs.Name = "cmdLangEnUs";
            this.cmdLangEnUs.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.lang_en16)});
            // 
            // ribbon
            // 
            this.ribbon.AllowMinimizing = false;
            this.ribbon.AllowRibbonContextMenu = false;
            this.ribbon.ApplicationButtonAccessibleDescription = null;
            this.ribbon.ApplicationButtonAccessibleName = null;
            this.ribbon.ApplicationButtonAnimationEnabled = false;
            this.ribbon.ApplicationButtonImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", ((System.Drawing.Image)(resources.GetObject("ribbon.ApplicationButtonImages.Images"))))});
            this.ribbon.ApplicationButtonPopup = this.applicationMenu;
            this.ribbon.AutoHide = false;
            this.ribbon.CurrentTabPage = this.rtpAuswertungen;
            resources.ApplyResources(this.ribbon, "ribbon");
            this.ribbon.Id = "003e845a-9ea4-41ad-b052-a403e8ba4481";
            this.ribbon.MinimizeButtonVisible = false;
            this.ribbon.Name = "ribbon";
            this.ribbon.TabPages.AddRange(new Elegant.Ui.RibbonTabPage[] {
            this.rtpEigeneDateien,
            this.rtpOffizielleDokumente,
            this.rtpVerwaltung,
            this.rtpStammdaten,
            this.rtpBearbeiten,
            this.rtpAuswertungen,
            this.rtpExtras,
            this.rtpImport,
            this.rtpExport,
            this.rtpHilfe,
            this.rtpDebug});
            this.ribbon.CustomizeQuickAccessToolbar += new System.EventHandler<Elegant.Ui.QuickAccessToolbarCustomizeEventArgs>(this.ribbon_CustomizeQuickAccessToolbar);
            // 
            // applicationMenu
            // 
            this.applicationMenu.ContentMinimumHeight = 0;
            this.applicationMenu.ExitButtonAccessibleDescription = null;
            this.applicationMenu.ExitButtonAccessibleName = null;
            this.applicationMenu.ExitButtonCommandName = "Exit";
            this.applicationMenu.ExitButtonText = "Beenden";
            this.applicationMenu.Items.AddRange(new System.Windows.Forms.Control[] {
            this.cmdPrintIstOperationen});
            this.applicationMenu.OptionsButtonAccessibleDescription = null;
            this.applicationMenu.OptionsButtonAccessibleName = null;
            this.applicationMenu.OptionsButtonCommandName = "OptionsView";
            this.applicationMenu.OptionsButtonText = "Optionen...";
            this.applicationMenu.PlacementMode = Elegant.Ui.PopupPlacementMode.Bottom;
            this.applicationMenu.RightPaneWidth = 0;
            this.applicationMenu.Size = new System.Drawing.Size(100, 100);
            // 
            // cmdPrintIstOperationen
            // 
            this.cmdPrintIstOperationen.CommandName = "PrintIstOperationen";
            this.cmdPrintIstOperationen.Id = "f58e5d90-8d4f-485a-a6c4-3bba49ceef26";
            this.cmdPrintIstOperationen.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", ((System.Drawing.Image)(resources.GetObject("cmdPrintIstOperationen.LargeImages.Images"))))});
            resources.ApplyResources(this.cmdPrintIstOperationen, "cmdPrintIstOperationen");
            this.cmdPrintIstOperationen.Name = "cmdPrintIstOperationen";
            // 
            // rtpEigeneDateien
            // 
            this.rtpEigeneDateien.Controls.Add(this.rgrEigeneDateien1);
            this.rtpEigeneDateien.Controls.Add(this.rgrEigeneDateien2);
            resources.ApplyResources(this.rtpEigeneDateien, "rtpEigeneDateien");
            this.rtpEigeneDateien.Id = "2681a17d-68d7-4228-b933-885a159913cc";
            this.rtpEigeneDateien.KeyTip = "D";
            this.rtpEigeneDateien.Name = "rtpEigeneDateien";
            // 
            // rgrEigeneDateien1
            // 
            this.rgrEigeneDateien1.Controls.Add(this.cmdDateiTypenView);
            this.rgrEigeneDateien1.Controls.Add(this.cmdDateienView);
            this.rgrEigeneDateien1.DialogLauncherButtonEnabled = false;
            this.rgrEigeneDateien1.DialogLauncherButtonVisible = false;
            this.rgrEigeneDateien1.Id = "bd288e0a-f965-4134-b5ce-98f63aa396a1";
            resources.ApplyResources(this.rgrEigeneDateien1, "rgrEigeneDateien1");
            this.rgrEigeneDateien1.Name = "rgrEigeneDateien1";
            // 
            // cmdDateiTypenView
            // 
            this.cmdDateiTypenView.CommandName = "DateiTypenView";
            this.cmdDateiTypenView.Id = "6d1e3bf6-2b6c-4f8d-a8fa-f48509a3b132";
            this.cmdDateiTypenView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdDateiTypenView.KeyTip = "1";
            this.cmdDateiTypenView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Document_Problem32)});
            resources.ApplyResources(this.cmdDateiTypenView, "cmdDateiTypenView");
            this.cmdDateiTypenView.Name = "cmdDateiTypenView";
            this.cmdDateiTypenView.ScreenTip.Caption = resources.GetString("cmdDateiTypenView.ScreenTip.Caption");
            this.cmdDateiTypenView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Document_Problem16)});
            // 
            // cmdDateienView
            // 
            this.cmdDateienView.CommandName = "DateienView";
            this.cmdDateienView.Id = "7f67e32b-a132-4bc5-98d8-dcaf6524e911";
            this.cmdDateienView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdDateienView.KeyTip = "2";
            this.cmdDateienView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Document_BlackPencil32)});
            resources.ApplyResources(this.cmdDateienView, "cmdDateienView");
            this.cmdDateienView.Name = "cmdDateienView";
            this.cmdDateienView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Document_BlackPencil16)});
            // 
            // rgrEigeneDateien2
            // 
            this.rgrEigeneDateien2.Controls.Add(this.ddEigeneDateien);
            this.rgrEigeneDateien2.DialogLauncherButtonEnabled = false;
            this.rgrEigeneDateien2.DialogLauncherButtonVisible = false;
            this.rgrEigeneDateien2.Id = "06636af2-2f6c-41ea-a169-dbb3ca3f9007";
            resources.ApplyResources(this.rgrEigeneDateien2, "rgrEigeneDateien2");
            this.rgrEigeneDateien2.Name = "rgrEigeneDateien2";
            // 
            // ddEigeneDateien
            // 
            this.ddEigeneDateien.CommandName = "EigeneDateien";
            this.ddEigeneDateien.Id = "1005b11c-b018-43a6-85f8-edadb8535c68";
            this.ddEigeneDateien.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.ddEigeneDateien.KeyTip = "3";
            this.ddEigeneDateien.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Document32)});
            resources.ApplyResources(this.ddEigeneDateien, "ddEigeneDateien");
            this.ddEigeneDateien.Name = "ddEigeneDateien";
            this.ddEigeneDateien.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Document16)});
            // 
            // rtpStammdaten
            // 
            this.rtpStammdaten.Controls.Add(this.rgrChirurg);
            this.rtpStammdaten.Controls.Add(this.rgrStammdaten2);
            this.rtpStammdaten.Controls.Add(this.rgrStammdaten3);
            this.rtpStammdaten.Controls.Add(this.rgrStammdaten4);
            this.rtpStammdaten.Controls.Add(this.rgrStammdaten5);
            this.rtpStammdaten.Controls.Add(this.rgrStammdaten6);
            resources.ApplyResources(this.rtpStammdaten, "rtpStammdaten");
            this.rtpStammdaten.Id = "9561fb83-3dd5-4f99-aba7-cec3327fb628";
            this.rtpStammdaten.KeyTip = "S";
            this.rtpStammdaten.Name = "rtpStammdaten";
            // 
            // rgrChirurg
            // 
            this.rgrChirurg.Controls.Add(this.cmdChirurgenFunktionenView);
            this.rgrChirurg.Controls.Add(this.cmdChirurgNew);
            this.rgrChirurg.Controls.Add(this.cmdChirurgEdit);
            this.rgrChirurg.Controls.Add(this.cmdChirurgDelete);
            this.rgrChirurg.DialogLauncherButtonEnabled = false;
            this.rgrChirurg.DialogLauncherButtonVisible = false;
            this.rgrChirurg.Id = "ba7138fe-9a30-4f85-bef0-506b9c212ada";
            resources.ApplyResources(this.rgrChirurg, "rgrChirurg");
            this.rgrChirurg.Name = "rgrChirurg";
            // 
            // cmdChirurgenFunktionenView
            // 
            this.cmdChirurgenFunktionenView.CommandName = "ChirurgenFunktionenView";
            this.cmdChirurgenFunktionenView.Id = "8a60e957-64cf-499e-b6be-f712f9bf6bec";
            this.cmdChirurgenFunktionenView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdChirurgenFunktionenView.KeyTip = "1";
            this.cmdChirurgenFunktionenView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Surgeon_Problem32)});
            resources.ApplyResources(this.cmdChirurgenFunktionenView, "cmdChirurgenFunktionenView");
            this.cmdChirurgenFunktionenView.Name = "cmdChirurgenFunktionenView";
            this.cmdChirurgenFunktionenView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Surgeon_Problem16)});
            // 
            // cmdChirurgNew
            // 
            this.cmdChirurgNew.CommandName = "ChirurgNew";
            this.cmdChirurgNew.Id = "e72b53bf-8479-42d9-a5ee-bff06fe71d12";
            this.cmdChirurgNew.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdChirurgNew.KeyTip = "2";
            this.cmdChirurgNew.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Surgeon_Add32)});
            resources.ApplyResources(this.cmdChirurgNew, "cmdChirurgNew");
            this.cmdChirurgNew.Name = "cmdChirurgNew";
            this.cmdChirurgNew.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Surgeon_Add16)});
            // 
            // cmdChirurgEdit
            // 
            this.cmdChirurgEdit.CommandName = "ChirurgEdit";
            this.cmdChirurgEdit.Id = "7a4063ac-2da1-4bb3-aaa0-54751f8c2a5e";
            this.cmdChirurgEdit.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdChirurgEdit.KeyTip = "3";
            this.cmdChirurgEdit.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Surgeon_BlackPencil32)});
            resources.ApplyResources(this.cmdChirurgEdit, "cmdChirurgEdit");
            this.cmdChirurgEdit.Name = "cmdChirurgEdit";
            this.cmdChirurgEdit.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Surgeon_BlackPencil16)});
            // 
            // cmdChirurgDelete
            // 
            this.cmdChirurgDelete.CommandName = "ChirurgDelete";
            this.cmdChirurgDelete.Id = "82b3cd07-7983-4e31-a3a3-9f3f2480dbef";
            this.cmdChirurgDelete.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdChirurgDelete.KeyTip = "4";
            this.cmdChirurgDelete.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Surgeon_Erase32)});
            resources.ApplyResources(this.cmdChirurgDelete, "cmdChirurgDelete");
            this.cmdChirurgDelete.Name = "cmdChirurgDelete";
            this.cmdChirurgDelete.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Surgeon_Erase16)});
            // 
            // rgrStammdaten2
            // 
            this.rgrStammdaten2.Controls.Add(this.cmdKommentareView);
            this.rgrStammdaten2.DialogLauncherButtonEnabled = false;
            this.rgrStammdaten2.DialogLauncherButtonVisible = false;
            this.rgrStammdaten2.Id = "bebbb2c0-677c-4ab8-acc7-88c185e39e3f";
            resources.ApplyResources(this.rgrStammdaten2, "rgrStammdaten2");
            this.rgrStammdaten2.Name = "rgrStammdaten2";
            // 
            // cmdKommentareView
            // 
            this.cmdKommentareView.CommandName = "KommentareView";
            this.cmdKommentareView.Id = "7eae7ca1-80c9-461c-aa6d-5659412a4675";
            this.cmdKommentareView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdKommentareView.KeyTip = "5";
            this.cmdKommentareView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Messages32)});
            resources.ApplyResources(this.cmdKommentareView, "cmdKommentareView");
            this.cmdKommentareView.Name = "cmdKommentareView";
            this.cmdKommentareView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Messages16)});
            // 
            // rgrStammdaten3
            // 
            this.rgrStammdaten3.Controls.Add(this.cmdDokumenteView);
            this.rgrStammdaten3.Controls.Add(this.cmdChirurgDokumenteView);
            this.rgrStammdaten3.DialogLauncherButtonEnabled = false;
            this.rgrStammdaten3.DialogLauncherButtonVisible = false;
            this.rgrStammdaten3.Id = "ef368849-04b9-4b21-9f53-00d5d6a7b88e";
            resources.ApplyResources(this.rgrStammdaten3, "rgrStammdaten3");
            this.rgrStammdaten3.Name = "rgrStammdaten3";
            // 
            // cmdDokumenteView
            // 
            this.cmdDokumenteView.CommandName = "DokumenteView";
            this.cmdDokumenteView.Id = "353e8b2c-75ed-4e14-ae2d-7161afaaf93f";
            this.cmdDokumenteView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdDokumenteView.KeyTip = "6";
            this.cmdDokumenteView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.CaseHistory_Problem32)});
            resources.ApplyResources(this.cmdDokumenteView, "cmdDokumenteView");
            this.cmdDokumenteView.Name = "cmdDokumenteView";
            this.cmdDokumenteView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.CaseHistory_Problem16)});
            // 
            // cmdChirurgDokumenteView
            // 
            this.cmdChirurgDokumenteView.CommandName = "ChirurgDokumenteView";
            this.cmdChirurgDokumenteView.Id = "4371e158-b134-4115-9698-aa104eed964b";
            this.cmdChirurgDokumenteView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdChirurgDokumenteView.KeyTip = "7";
            this.cmdChirurgDokumenteView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.CaseHistory32)});
            resources.ApplyResources(this.cmdChirurgDokumenteView, "cmdChirurgDokumenteView");
            this.cmdChirurgDokumenteView.Name = "cmdChirurgDokumenteView";
            this.cmdChirurgDokumenteView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.CaseHistory16)});
            // 
            // rgrStammdaten4
            // 
            this.rgrStammdaten4.Controls.Add(this.cmdAkademischeAusbildungTypenView);
            this.rgrStammdaten4.Controls.Add(this.cmdAkademischeAusbildungView);
            this.rgrStammdaten4.DialogLauncherButtonEnabled = false;
            this.rgrStammdaten4.DialogLauncherButtonVisible = false;
            this.rgrStammdaten4.Id = "d8d2f3db-2e75-4f03-83c5-f2ad95808492";
            resources.ApplyResources(this.rgrStammdaten4, "rgrStammdaten4");
            this.rgrStammdaten4.Name = "rgrStammdaten4";
            // 
            // cmdAkademischeAusbildungTypenView
            // 
            this.cmdAkademischeAusbildungTypenView.CommandName = "AkademischeAusbildungTypenView";
            this.cmdAkademischeAusbildungTypenView.Id = "ddef47ad-3b60-45e9-9400-1730eebd45c5";
            this.cmdAkademischeAusbildungTypenView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdAkademischeAusbildungTypenView.KeyTip = "8";
            this.cmdAkademischeAusbildungTypenView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Student_Problem32)});
            resources.ApplyResources(this.cmdAkademischeAusbildungTypenView, "cmdAkademischeAusbildungTypenView");
            this.cmdAkademischeAusbildungTypenView.Name = "cmdAkademischeAusbildungTypenView";
            this.cmdAkademischeAusbildungTypenView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Student_Problem16)});
            // 
            // cmdAkademischeAusbildungView
            // 
            this.cmdAkademischeAusbildungView.CommandName = "AkademischeAusbildungView";
            this.cmdAkademischeAusbildungView.Id = "c0414e17-187a-426c-bc46-b8419f14cb0d";
            this.cmdAkademischeAusbildungView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdAkademischeAusbildungView.KeyTip = "9";
            this.cmdAkademischeAusbildungView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Student32)});
            resources.ApplyResources(this.cmdAkademischeAusbildungView, "cmdAkademischeAusbildungView");
            this.cmdAkademischeAusbildungView.Name = "cmdAkademischeAusbildungView";
            this.cmdAkademischeAusbildungView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Student16)});
            // 
            // rgrStammdaten5
            // 
            this.rgrStammdaten5.Controls.Add(this.cmdNotizTypenView);
            this.rgrStammdaten5.Controls.Add(this.cmdNotizenView);
            this.rgrStammdaten5.DialogLauncherButtonEnabled = false;
            this.rgrStammdaten5.DialogLauncherButtonVisible = false;
            this.rgrStammdaten5.Id = "40605efd-0fc5-4728-a63d-3369eeb98e17";
            resources.ApplyResources(this.rgrStammdaten5, "rgrStammdaten5");
            this.rgrStammdaten5.Name = "rgrStammdaten5";
            // 
            // cmdNotizTypenView
            // 
            this.cmdNotizTypenView.CommandName = "NotizTypenView";
            this.cmdNotizTypenView.Id = "86f37522-a8fc-4d2e-8ba8-509e53e4c606";
            this.cmdNotizTypenView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdNotizTypenView.KeyTip = "A";
            this.cmdNotizTypenView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Reports_Problem32)});
            resources.ApplyResources(this.cmdNotizTypenView, "cmdNotizTypenView");
            this.cmdNotizTypenView.Name = "cmdNotizTypenView";
            this.cmdNotizTypenView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Reports_Problem16)});
            // 
            // cmdNotizenView
            // 
            this.cmdNotizenView.CommandName = "NotizenView";
            this.cmdNotizenView.Id = "7f570c09-15c2-454f-9ad7-1a5bc2bd6c27";
            this.cmdNotizenView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdNotizenView.KeyTip = "B";
            this.cmdNotizenView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Reports32)});
            resources.ApplyResources(this.cmdNotizenView, "cmdNotizenView");
            this.cmdNotizenView.Name = "cmdNotizenView";
            this.cmdNotizenView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Reports16)});
            // 
            // rgrStammdaten6
            // 
            this.rgrStammdaten6.Controls.Add(this.cmdPlanOperationenView);
            this.rgrStammdaten6.Controls.Add(this.cmdPlanOperationVergleichView);
            this.rgrStammdaten6.Controls.Add(this.cmdRichtlinienSollIstView);
            this.rgrStammdaten6.DialogLauncherButtonEnabled = false;
            this.rgrStammdaten6.DialogLauncherButtonVisible = false;
            this.rgrStammdaten6.Id = "0949e162-a028-49c1-bd31-32cdf16d23fc";
            resources.ApplyResources(this.rgrStammdaten6, "rgrStammdaten6");
            this.rgrStammdaten6.Name = "rgrStammdaten6";
            // 
            // cmdPlanOperationenView
            // 
            this.cmdPlanOperationenView.CommandName = "PlanOperationenView";
            this.cmdPlanOperationenView.Id = "f0f1f519-0f62-4ae6-94a8-6ff29189050d";
            this.cmdPlanOperationenView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdPlanOperationenView.KeyTip = "C";
            this.cmdPlanOperationenView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Report_PeopleContact32)});
            resources.ApplyResources(this.cmdPlanOperationenView, "cmdPlanOperationenView");
            this.cmdPlanOperationenView.Name = "cmdPlanOperationenView";
            this.cmdPlanOperationenView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Report_PeopleContact16)});
            // 
            // cmdPlanOperationVergleichView
            // 
            this.cmdPlanOperationVergleichView.CommandName = "PlanOperationVergleichView";
            this.cmdPlanOperationVergleichView.Id = "8ac18e23-071c-48e2-adc1-5c0ed59d58c1";
            this.cmdPlanOperationVergleichView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdPlanOperationVergleichView.KeyTip = "D";
            this.cmdPlanOperationVergleichView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Report_PeopleContacts32)});
            resources.ApplyResources(this.cmdPlanOperationVergleichView, "cmdPlanOperationVergleichView");
            this.cmdPlanOperationVergleichView.Name = "cmdPlanOperationVergleichView";
            this.cmdPlanOperationVergleichView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Report_PeopleContacts16)});
            // 
            // cmdRichtlinienSollIstView
            // 
            this.cmdRichtlinienSollIstView.CommandName = "RichtlinienSollIstView";
            this.cmdRichtlinienSollIstView.Id = "3ddb546e-9339-44cc-8e29-ed08a9bdd576";
            this.cmdRichtlinienSollIstView.InformativenessMinimumLevel = "Elegant.Ui.RibbonGroupButtonInformativenessLevel:LargeImageWithText";
            this.cmdRichtlinienSollIstView.KeyTip = "E";
            this.cmdRichtlinienSollIstView.LargeImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Certification_Report32)});
            resources.ApplyResources(this.cmdRichtlinienSollIstView, "cmdRichtlinienSollIstView");
            this.cmdRichtlinienSollIstView.Name = "cmdRichtlinienSollIstView";
            this.cmdRichtlinienSollIstView.SmallImages.Images.AddRange(new Elegant.Ui.ControlImage[] {
            new Elegant.Ui.ControlImage("Normal", global::Operationen.Properties.Resources.Certification_Report16)});
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pbMain);
            resources.ApplyResources(this.pnlMain, "pnlMain");
            this.pnlMain.Name = "pnlMain";
            // 
            // pbMain
            // 
            this.pbMain.Image = global::Operationen.Properties.Resources.Hauptfenster_unten;
            resources.ApplyResources(this.pbMain, "pbMain");
            this.pbMain.Name = "pbMain";
            this.pbMain.TabStop = false;
            // 
            // formFrameSkinner
            // 
            this.formFrameSkinner.Form = this;
            // 
            // OperationenLogbuchView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.ribbon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "OperationenLogbuchView";
            this.Load += new System.EventHandler(this.OperationenLogbuchView_Load);
            this.Shown += new System.EventHandler(this.OperationenLogbuchView_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.rtpOffizielleDokumente)).EndInit();
            this.rtpOffizielleDokumente.ResumeLayout(false);
            this.rtpOffizielleDokumente.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrOffizielleDokumente1)).EndInit();
            this.rgrOffizielleDokumente1.ResumeLayout(false);
            this.rgrOffizielleDokumente1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrOffizielleDokumente2)).EndInit();
            this.rgrOffizielleDokumente2.ResumeLayout(false);
            this.rgrOffizielleDokumente2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtpVerwaltung)).EndInit();
            this.rtpVerwaltung.ResumeLayout(false);
            this.rtpVerwaltung.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrVerwaltung1)).EndInit();
            this.rgrVerwaltung1.ResumeLayout(false);
            this.rgrVerwaltung1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrVerwaltung2)).EndInit();
            this.rgrVerwaltung2.ResumeLayout(false);
            this.rgrVerwaltung2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrVerwaltung3)).EndInit();
            this.rgrVerwaltung3.ResumeLayout(false);
            this.rgrVerwaltung3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtpBearbeiten)).EndInit();
            this.rtpBearbeiten.ResumeLayout(false);
            this.rtpBearbeiten.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrBearbeiten1)).EndInit();
            this.rgrBearbeiten1.ResumeLayout(false);
            this.rgrBearbeiten1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrBearbeiten2)).EndInit();
            this.rgrBearbeiten2.ResumeLayout(false);
            this.rgrBearbeiten2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtpAuswertungen)).EndInit();
            this.rtpAuswertungen.ResumeLayout(false);
            this.rtpAuswertungen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrAuswertungen1)).EndInit();
            this.rgrAuswertungen1.ResumeLayout(false);
            this.rgrAuswertungen1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrAuswertungen2)).EndInit();
            this.rgrAuswertungen2.ResumeLayout(false);
            this.rgrAuswertungen2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrAuswertungen3)).EndInit();
            this.rgrAuswertungen3.ResumeLayout(false);
            this.rgrAuswertungen3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtpImport)).EndInit();
            this.rtpImport.ResumeLayout(false);
            this.rtpImport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrImport1)).EndInit();
            this.rgrImport1.ResumeLayout(false);
            this.rgrImport1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrImport2)).EndInit();
            this.rgrImport2.ResumeLayout(false);
            this.rgrImport2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrImport3)).EndInit();
            this.rgrImport3.ResumeLayout(false);
            this.rgrImport3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtpExport)).EndInit();
            this.rtpExport.ResumeLayout(false);
            this.rtpExport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrExport1)).EndInit();
            this.rgrExport1.ResumeLayout(false);
            this.rgrExport1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrExport2)).EndInit();
            this.rgrExport2.ResumeLayout(false);
            this.rgrExport2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtpHilfe)).EndInit();
            this.rtpHilfe.ResumeLayout(false);
            this.rtpHilfe.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrHilfe1)).EndInit();
            this.rgrHilfe1.ResumeLayout(false);
            this.rgrHilfe1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrHilfe2)).EndInit();
            this.rgrHilfe2.ResumeLayout(false);
            this.rgrHilfe2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtpDebug)).EndInit();
            this.rtpDebug.ResumeLayout(false);
            this.rtpDebug.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonGroup1)).EndInit();
            this.ribbonGroup1.ResumeLayout(false);
            this.ribbonGroup1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtpExtras)).EndInit();
            this.rtpExtras.ResumeLayout(false);
            this.rtpExtras.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrExtras1)).EndInit();
            this.rgrExtras1.ResumeLayout(false);
            this.rgrExtras1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrExtras2)).EndInit();
            this.rgrExtras2.ResumeLayout(false);
            this.rgrExtras2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrExtras3)).EndInit();
            this.rgrExtras3.ResumeLayout(false);
            this.rgrExtras3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrExtras4)).EndInit();
            this.rgrExtras4.ResumeLayout(false);
            this.rgrExtras4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtpEigeneDateien)).EndInit();
            this.rtpEigeneDateien.ResumeLayout(false);
            this.rtpEigeneDateien.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrEigeneDateien1)).EndInit();
            this.rgrEigeneDateien1.ResumeLayout(false);
            this.rgrEigeneDateien1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrEigeneDateien2)).EndInit();
            this.rgrEigeneDateien2.ResumeLayout(false);
            this.rgrEigeneDateien2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtpStammdaten)).EndInit();
            this.rtpStammdaten.ResumeLayout(false);
            this.rtpStammdaten.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrChirurg)).EndInit();
            this.rgrChirurg.ResumeLayout(false);
            this.rgrChirurg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrStammdaten2)).EndInit();
            this.rgrStammdaten2.ResumeLayout(false);
            this.rgrStammdaten2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrStammdaten3)).EndInit();
            this.rgrStammdaten3.ResumeLayout(false);
            this.rgrStammdaten3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrStammdaten4)).EndInit();
            this.rgrStammdaten4.ResumeLayout(false);
            this.rgrStammdaten4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrStammdaten5)).EndInit();
            this.rgrStammdaten5.ResumeLayout(false);
            this.rgrStammdaten5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgrStammdaten6)).EndInit();
            this.rgrStammdaten6.ResumeLayout(false);
            this.rgrStammdaten6.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ToolTip toolTip1;
        private Timer timer;
        private Elegant.Ui.Ribbon ribbon;
        private Elegant.Ui.RibbonTabPage rtpEigeneDateien;
        private Elegant.Ui.RibbonGroup rgrEigeneDateien1;
        public Elegant.Ui.Button cmdDateiTypenView;
        public Elegant.Ui.Button cmdDateienView;
        public Elegant.Ui.DropDown ddEigeneDateien;
        private Elegant.Ui.RibbonTabPage rtpOffizielleDokumente;
        private Elegant.Ui.FormFrameSkinner formFrameSkinner;
        private Elegant.Ui.RibbonTabPage rtpVerwaltung;
        private Elegant.Ui.ApplicationMenu applicationMenu;
        private Elegant.Ui.RibbonTabPage rtpAuswertungen;
        private Elegant.Ui.RibbonTabPage rtpExtras;
        private Elegant.Ui.RibbonTabPage rtpHilfe;
        private Elegant.Ui.RibbonTabPage rtpDebug;
        private Elegant.Ui.RibbonGroup rgrOffizielleDokumente1;
        public Elegant.Ui.Button cmdRichtlinienView;
        public Elegant.Ui.Button cmdOperationenKatalogView;
        public Elegant.Ui.Button cmdGebieteView;
        public Elegant.Ui.Button cmdPrintIstOperationen;
        private Elegant.Ui.RibbonGroup rgrVerwaltung2;
        public Elegant.Ui.Button cmdAbteilungenView;
        public Elegant.Ui.Button cmdAbteilungenChirurgenView;
        public Elegant.Ui.Button cmdWeiterbilderChirurgenView;
        private Elegant.Ui.RibbonGroup rgrOffizielleDokumente2;
        public Elegant.Ui.Button cmdBaekWbOrdnung;
        public Elegant.Ui.Button cmdBaekWbRichtlinien;
        private Elegant.Ui.RibbonTabPage rtpBearbeiten;
        private Elegant.Ui.RibbonGroup rgrEigeneDateien2;
        private Elegant.Ui.RibbonGroup rgrBearbeiten1;
        public Elegant.Ui.Button cmdOperationenEditView;
        private Elegant.Ui.RibbonGroup rgrBearbeiten2;
        public Elegant.Ui.Button cmdRichtlinienOpsKodeUnassignedView;
        public Elegant.Ui.Button cmdChirurgenRichtlinienView;
        private Elegant.Ui.RibbonGroup rgrAuswertungen1;
        public Elegant.Ui.Button cmdOperationenView;
        public Elegant.Ui.Button cmdOperationenZeitenVergleichView;
        public Elegant.Ui.Button cmdOPDauerFortschrittView;
        private Elegant.Ui.RibbonGroup rgrAuswertungen2;
        public Elegant.Ui.Button cmdChirurgOperationenView;
        public Elegant.Ui.Button cmdGesamtOperationenView;
        public Elegant.Ui.Button cmdOperationenVergleichView;
        public Elegant.Ui.Button cmdPlanOperationVergleichIstView;
        private Elegant.Ui.RibbonGroup rgrAuswertungen3;
        public Elegant.Ui.Button cmdRichtlinienVergleichView;
        public Elegant.Ui.Button cmdRichtlinienVergleichOverviewView;
        public Elegant.Ui.Button cmdKlinischeErgebnisseView;
        private Elegant.Ui.RibbonGroup rgrExtras1;
        public Elegant.Ui.Button cmdUserChangePasswordView;
        public Elegant.Ui.Button cmdUserSetPasswordView;
        private Elegant.Ui.RibbonGroup rgrExtras4;
        public Elegant.Ui.Button cmdLogView;
        private Elegant.Ui.RibbonGroup rgrExtras2;
        public Elegant.Ui.Button cmdSerialNumbersView;
        public Elegant.Ui.Button cmdUpdateFromFolder;
        public Elegant.Ui.Button cmdUpdateFromWww;
        public Elegant.Ui.Button cmdSerialBuy;
        public Elegant.Ui.RibbonGroup rgrExtras3;
        public Elegant.Ui.Button cmdInstallLicenseWizard;
        public Elegant.Ui.Button cmdCopyWWWProgramUpdateFilesView;
        public Elegant.Ui.Button cmdOptionsView;
        public Elegant.Ui.RibbonGroup rgrVerwaltung1;
        public Elegant.Ui.Button cmdSecUserOverviewView;
        public Elegant.Ui.Button cmdSecGroupsView;
        public Elegant.Ui.Button cmdSecGroupsChirurgenView;
        public Elegant.Ui.Button cmdSecGroupsSecRightsView;
        public Elegant.Ui.RibbonTabPage rtpImport;
        public Elegant.Ui.RibbonGroup rgrImport1;
        public Elegant.Ui.Button cmdImportChirurgenExcludeView;
        public Elegant.Ui.Button cmdOperationenImportView;
        public Elegant.Ui.Button cmdExecAutoImport;
        public Elegant.Ui.RibbonGroup rgrImport2;
        public Elegant.Ui.Button cmdImportRichtlinienWizard;
        public Elegant.Ui.Button cmdImportRichtlinienZuordnungWizard;
        public Elegant.Ui.RibbonGroup rgrImport3;
        public Elegant.Ui.Button cmdImportChirurgWizard;
        public Elegant.Ui.Button cmdImportOperationenMobileWizard;
        public Elegant.Ui.Button cmdImportOPSWizard;
        public Elegant.Ui.RibbonTabPage rtpExport;
        public Elegant.Ui.RibbonGroup rgrExport2;
        public Elegant.Ui.Button cmdExportChirurgWizard;
        public Elegant.Ui.Button cmdExportOperationenKatalogView;
        public Elegant.Ui.RibbonGroup rgrExport1;
        public Elegant.Ui.Button cmdExportRichtlinienWizard;
        public Elegant.Ui.Button cmdExportRichtlinienZuordnungWizard;
        private Elegant.Ui.RibbonGroup ribbonGroup1;
        public Elegant.Ui.Button cmdDebugSql;
        public Elegant.Ui.Button cmdDebugExportOperationen;
        public Elegant.Ui.Button cmdUpdateSerialnumbersView;
        public Elegant.Ui.Button cmdDebugReloadUserRights;
        private Elegant.Ui.RibbonTabPage rtpStammdaten;
        private Elegant.Ui.RibbonGroup rgrStammdaten4;
        public Elegant.Ui.Button cmdNotizTypenView;
        public Elegant.Ui.Button cmdNotizenView;
        public Elegant.Ui.Button cmdAkademischeAusbildungTypenView;
        public Elegant.Ui.Button cmdAkademischeAusbildungView;
        public Elegant.Ui.RibbonGroup rgrStammdaten3;
        public Elegant.Ui.Button cmdChirurgDokumenteView;
        public Elegant.Ui.Button cmdDokumenteView;
        public Elegant.Ui.RibbonGroup rgrStammdaten2;
        public Elegant.Ui.Button cmdKommentareView;
        public Elegant.Ui.RibbonGroup rgrStammdaten5;
        public Elegant.Ui.RibbonGroup rgrChirurg;
        public Elegant.Ui.Button cmdChirurgenFunktionenView;
        public Elegant.Ui.Button cmdChirurgNew;
        public Elegant.Ui.Button cmdChirurgEdit;
        public Elegant.Ui.Button cmdChirurgDelete;
        public Elegant.Ui.RibbonGroup rgrStammdaten6;
        public Elegant.Ui.Button cmdPlanOperationenView;
        public Elegant.Ui.Button cmdPlanOperationVergleichView;
        private Elegant.Ui.RibbonGroup rgrVerwaltung3;
        public Elegant.Ui.Button cmdOperationenSummaryView;
        private Panel pnlMain;
        public Elegant.Ui.Button cmdLangDeDe;
        public Elegant.Ui.Button cmdLangEnUs;
        private PictureBox pbMain;
        public Elegant.Ui.Button cmdRichtlinienSollIstView;
        private Elegant.Ui.RibbonGroup rgrHilfe1;
        public Elegant.Ui.Button cmdHelpChm;
        public Elegant.Ui.Button cmdWwwHelp;
        public Elegant.Ui.Button cmdWwwHome;
        public Elegant.Ui.Button cmdAboutBox;
        private Elegant.Ui.RibbonGroup rgrHilfe2;
        private Elegant.Ui.DropDown ddVideoTutorials;
        public Elegant.Ui.Button cmdClientServerViewImport;
        public Elegant.Ui.Button cmdClientServerViewExport;
        public Elegant.Ui.Button cmdZeitraeumeView;

    }
}