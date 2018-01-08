using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using System.Text;
using System.Globalization;

using Utility;
using AppFramework;
using CMaurer.Operationen.AppFramework;

namespace Operationen
{
    partial class OptionsView : OperationenForm
    {
        public OptionsView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();

            lblInfo.Text = "";

            Text = AppTitle(GetText("title"));

            chkOpenCommentMsg.Text = GetText("chkOpenCommentMsg");
            lblOpscodeStellen.Text = GetText("lblOpscodeStellen");

            SetInfoText(
                lblInfoAutoSerial,
                string.Format(GetText("info_auto_serial"), Command_SerialNumbersView));

            lvWatermark.View = View.Details;
            lvWatermark.Columns.Add("Lorem", 160);
            lvWatermark.Columns.Add("ipsum", -2);
            string []placeHolders = {"Lorem ipsum dolor sit amet", "consectetuer sadipscing elitr", "sed diam nonumy eirmod tempor", "invidunt ut labore et dolore magna", "aliquyam erat", "sed diam voluptua." };
            for (int i = 0; i < placeHolders.Length; i++)
            {
                lvWatermark.Items.Add(placeHolders[i]);
            }

            if (UserHasRight("SerialNumbersView.edit"))
            {
                AddLinkLabelLink(lblInfoAutoSerial, Command_SerialNumbersView, Command_SerialNumbersView);
            }

            tabProxy.Text = GetText("tab_proxy");
            tabImport.Text = GetText("tab_auto_import");
            tabUpdate.Text = GetText("tab_program");
            tabSerialnumbers.Text = GetText("tab_serial_numbers");
            tabPrint.Text = GetText("tab_print");
            tabSonstiges.Text = GetText("tab_other");

            lblOpscodeStellen.Text = GetText("lblOpscodeStellen");

            lblPrintBDCWeiterbildung.Text = string.Format(GetText("print_lines_print"), Command_RichtlinienVergleichView)
                + ": " + GetText("print_bdc");
            lblPrintOperationenView.Text = string.Format(GetText("print_lines_print"), Command_OperationenView);
            lblPrintOperationenZeitenVergleichView.Text = string.Format(GetText("print_lines_print"), Command_OperationenZeitenVergleichView);
            lblPrintPlanOperationVergleichIstView.Text = string.Format(GetText("print_lines_print"), Command_PlanOperationVergleichIstView);
            lblPrintAkademischeAusbildungView.Text = string.Format(GetText("print_lines_print"), Command_AkademischeAusbildungView);
            lblPrintKlinischeErgebnisseView.Text = string.Format(GetText("print_lines_print"), Command_KlinischeErgebnisseView);
            lblRichtlinienVergleichOverviewView.Text = string.Format(GetText("print_lines_print"), Command_RichtlinienVergleichOverviewView);
            lblOperationenVergleichView.Text = string.Format(GetText("print_lines_print"), Command_OperationenVergleichView);
            lblPrintDefault.Text = GetText("print_lines_default");

            string secretWatermarkFileName = BusinessLayer.AppPath + Path.DirectorySeparatorChar + "Watermark.png";
            lblWatermarkInfo.Text = string.Format(GetText("watermarkSecretInfo"), secretWatermarkFileName);

            tabProxy.Enabled = UserHasRight("OptionsView.tabProxy");
            tabImport.Enabled = UserHasRight("OptionsView.tabImport");
            tabUpdate.Enabled = UserHasRight("OptionsView.tabUpdate");
            tabSerialnumbers.Enabled = UserHasRight("OptionsView.tabSerialnumbers");
            tabPrint.Enabled = UserHasRight("OptionsView.tabPrint");
            tabSonstiges.Enabled = UserHasRight("OptionsView.tabSonstiges");

            chkOpenCommentMsg.Enabled = UserHasRight("OptionsView.chkOpenCommentMsg");
            txtStellenOPSKode.Enabled = UserHasRight("OptionsView.txtStellenOPSKode");
        }

        private void EnableControls(bool enabled)
        {
            cmdOK.Enabled = cmdCancel.Enabled = enabled;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            EnableControls(false);
            Application.DoEvents();

            base.OKClicked();

            EnableControls(true);
            Cursor = Cursors.Default;
            Application.DoEvents();
        }

        private void OptionsView_Load(object sender, EventArgs e)
        {
            PopulatePlugins();
            Object2Control();
        }

        private void OptionsView_Shown(object sender, EventArgs e)
        {
            _bIgnoreControlEvents = true;

            EnableControlsAutoImport();
            EnableControlsProxy();
            EnableControlsAutoUpdate();

            _bIgnoreControlEvents = false;
        }

        protected override void SaveObject()
        {
            //
            // When removing a section, the entry is still in the database and will not be deleted!
            //

            // Proxy
            if (radProxyNone.Checked)
            {
                BusinessLayer.SaveUserSettings(GlobalConstants.SectionProxy, GlobalConstants.KeyProxyMode, GlobalConstants.ValueProxyModeNone);
            }
            else if (radProxyIE.Checked)
            {
                BusinessLayer.SaveUserSettings(GlobalConstants.SectionProxy, GlobalConstants.KeyProxyMode, GlobalConstants.ValueProxyModeIE);
            }
            else if (radProxyUser.Checked)
            {
                BusinessLayer.SaveUserSettings(GlobalConstants.SectionProxy, GlobalConstants.KeyProxyMode, GlobalConstants.ValueProxyModeUser);
                BusinessLayer.SaveUserSettings(GlobalConstants.SectionProxy, GlobalConstants.KeyProxyUserAddress, txtProxyAddress.Text);
                BusinessLayer.ConfigProxyUser = txtProxyUser.Text;
                BusinessLayer.ConfigProxyPassword = txtProxyPassword.Text;
                BusinessLayer.ConfigProxyDomain = txtProxyDomain.Text;
            }

            // Autoupdate
            BusinessLayer.SaveUserSettings(GlobalConstants.SectionProgram, GlobalConstants.KeyProgramAutoUpdate, chkAutoUpdate.Checked ? "1" : "0");
            BusinessLayer.SaveUserSettings(GlobalConstants.SectionProgram, GlobalConstants.KeyProgramAutoUpdateLocal, chkAutoUpdateLocal.Checked ? "1" : "0");
            BusinessLayer.SaveUserSettings(GlobalConstants.SectionProgram, GlobalConstants.KeyProgramCopyUpdateFiles, chkCopyUpdate.Checked ? "1" : "0");
            if (chkAutoUpdateLocal.Checked || chkCopyUpdate.Checked)
            {
                BusinessLayer.SaveUserSettings(GlobalConstants.SectionProgram, GlobalConstants.KeyProgramAutoUpdateLocalFolder, txtAutoUpdateLocalFolder.Text);
            }

            // AutoImport
            // Erstmal alles löschen, damit nur das drinsteht, was auch eingestellt ist.
            BusinessLayer.SaveUserSettings(GlobalConstants.SectionOpImport, GlobalConstants.KeyOpImportPath, txtPath.Text);
            BusinessLayer.SaveUserSettings(GlobalConstants.SectionOpImport, GlobalConstants.KeyOpImportPlugin, cbPlugins.Text);
            BusinessLayer.SaveUserSettings(GlobalConstants.SectionOpImport, GlobalConstants.KeyOpImportAutoImport, chkAutoImport.Checked ? "1" : "0");
            BusinessLayer.SaveUserSettings(GlobalConstants.SectionOpImport, GlobalConstants.KeyOpImportInsertOperation, chkInsertUnknownOperation.Checked ? "1" : "0");
            BusinessLayer.SaveUserSettings(GlobalConstants.SectionOpImport, GlobalConstants.KeyOpImportInsertSurgeon, chkInsertUnknownSurgeon.Checked ? "1" : "0");
            BusinessLayer.SaveUserSettings(GlobalConstants.SectionOpImport, GlobalConstants.KeyOpImportIdentFirstName, chkIdentFirstName.Checked ? "1" : "0");
            BusinessLayer.SaveUserSettings(GlobalConstants.SectionOpImport, GlobalConstants.KeyOpImportIdentifyOpByIdentifier, chkIdentifyOpFallzahl.Checked ? "1" : "0");
            BusinessLayer.SaveUserSettings(GlobalConstants.SectionOpImport, GlobalConstants.KeyOpImportIdentifyByImportID, radIdentifyByImportID.Checked ? "1" : "0");

            // Serial numbers
            BusinessLayer.SaveUserSettings(GlobalConstants.SectionSerialNumbers, GlobalConstants.KeySerialNumbersAutomatic, chkSerialNumbersAutomatic.Checked ? "1" : "0");

            // Print
            PrintLinesControlToConfig(GlobalConstants.KeyPrintLinesBDCWeiterbildung, txtPrintBDCWeiterbildung);
            PrintLinesControlToConfig(GlobalConstants.KeyPrintLinesOperationenView, txtPrintOperationenView);
            PrintLinesControlToConfig(GlobalConstants.KeyPrintLinesOperationenZeitenVergleichView, txtPrintOperationenZeitenVergleichView);
            PrintLinesControlToConfig(GlobalConstants.KeyPrintLinesPlanOperationVergleichIstView, txtPrintPlanOperationVergleichIstView);
            PrintLinesControlToConfig(GlobalConstants.KeyPrintLinesAkademischeAusbildungView, txtPrintAkademischeAusbildungView);
            PrintLinesControlToConfig(GlobalConstants.KeyPrintLinesKlinischeErgebnisseView, txtPrintKlinischeErgebnisseView);
            PrintLinesControlToConfig(GlobalConstants.KeyPrintLinesRichtlinienVergleichOverviewView, txtPrintRichtlinienVergleichOverviewView);
            PrintLinesControlToConfig(GlobalConstants.KeyPrintLinesOperationenVergleichView, txtPrintOperationenVergleichView);
            PrintLinesControlToConfig(GlobalConstants.KeyPrintLinesDefaultView, txtPrintDefault);

            //
            // Other
            //
            BusinessLayer.SaveUserSettings(GlobalConstants.SectionOther, GlobalConstants.KeyOtherDisplayOpenCommentMessage, chkOpenCommentMsg.Checked ? "1" : "0");
            IntegerControlToDBConfig(GlobalConstants.SectionOther, GlobalConstants.KeyOtherRelevantPositionsOPSCode, txtStellenOPSKode.Text, GlobalConstants.OtherRelevantPositionsMin, GlobalConstants.OtherRelevantPositionsMax);
            BusinessLayer.SaveUserSettings(GlobalConstants.SectionOther, GlobalConstants.KeyOtherWatermark, chkWatermark.Checked ? "1" : "0");
        }

        private void IntegerControlToDBConfig(string section, string key, string txt, int min, int max)
        {
            int n;
            if (Int32.TryParse(txt, out n))
            {
                if (min <= n && n <= max)
                {
                    BusinessLayer.SaveConfig(section + "." + key, txt);
                }
            }
        }
        private void IntegerControlToConfig(string section, string key, TextBox txt, int min, int max)
        {
            int n;
            if (Int32.TryParse(txt.Text, out n))
            {
                if (min <= n && n <= max)
                {
                    BusinessLayer.SaveUserSettings(section, key, txt.Text);
                }
            }
        }

        private void PrintLinesControlToConfig(string key, TextBox txt)
        {
            IntegerControlToConfig(GlobalConstants.SectionPrint, key, txt, GlobalConstants.PrintLinesPerPageMin, GlobalConstants.PrintLinesPerPageMax);
        }

        protected override bool ValidateInput()
        {
            bool fSuccess = true;

            string strMessage = EINGABEFEHLER;

            // Auto-Import
            if (chkAutoImport.Checked)
            {
                if (txtPath.Text.Length == 0)
                {
                    strMessage += GetTextControlMissingText(lblPath);
                    fSuccess = false;
                }
                else
                {
                    if (!Directory.Exists(txtPath.Text))
                    {
                        strMessage += string.Format(GetText("missing_dir"), txtPath.Text);
                        fSuccess = false;
                    }
                }
            }

            // Auto-Update
            // Wenn man beim www-Update die Dateien lokal kopieren möchte
            // oder lokal auf ein Update testen möchte
            if (chkAutoUpdateLocal.Checked || chkCopyUpdate.Checked)
            {
                if (txtAutoUpdateLocalFolder.Text.Length == 0)
                {
                    strMessage += GetTextControlMissingText(lblAutoUpdateLocalFolder);
                    fSuccess = false;
                }
                else
                {
                    if (!Directory.Exists(txtAutoUpdateLocalFolder.Text))
                    {
                        strMessage += string.Format(GetText("missing_dir"), txtAutoUpdateLocalFolder.Text);
                        fSuccess = false;
                    }
                }
            }

            // Print
            CorrectLinesPerPage(txtPrintBDCWeiterbildung);
            CorrectLinesPerPage(txtPrintOperationenView);
            CorrectLinesPerPage(txtPrintOperationenZeitenVergleichView);
            CorrectLinesPerPage(txtPrintPlanOperationVergleichIstView);
            CorrectLinesPerPage(txtPrintKlinischeErgebnisseView);
            CorrectLinesPerPage(txtPrintRichtlinienVergleichOverviewView);

            if (!fSuccess)
            {
                MessageBox(strMessage);
            }

            return fSuccess;
        }

        private void CorrectLinesPerPage(TextBox txt)
        {
            int n;
            if (txt.Text.Length > 0 && Int32.TryParse(txt.Text, out n))
            {
                if (n < GlobalConstants.PrintLinesPerPageMin || n > GlobalConstants.PrintLinesPerPageMax)
                {
                    txt.Text = GlobalConstants.KeyPrintLinesDefaultString;
                }
            }
            else
            {
                txt.Text = GlobalConstants.KeyPrintLinesDefaultString;
            }
        }

        protected override void Object2Control()
        {
            //
            // Proxy
            //
            switch (BusinessLayer.GetUserSettingsString(GlobalConstants.SectionProxy, GlobalConstants.KeyProxyMode))
            {
                case GlobalConstants.ValueProxyModeNone:
                    radProxyNone.Checked = true;
                    radProxyIE.Checked = false;
                    radProxyUser.Checked = false;
                    break;

                case GlobalConstants.ValueProxyModeIE:
                default:
                    radProxyNone.Checked = false;
                    radProxyIE.Checked = true;
                    radProxyUser.Checked = false;
                    break;

                case GlobalConstants.ValueProxyModeUser:
                    radProxyNone.Checked = false;
                    radProxyIE.Checked = false;
                    radProxyUser.Checked = true;
                    break;
            }

            txtProxyAddress.Text = BusinessLayer.GetUserSettingsString(GlobalConstants.SectionProxy, GlobalConstants.KeyProxyUserAddress);
            txtProxyUser.Text = BusinessLayer.ConfigProxyUser;
            txtProxyPassword.Text = BusinessLayer.ConfigProxyPassword;
            txtProxyDomain.Text = BusinessLayer.ConfigProxyDomain;
            SetInfoText(lblInfoProxy, GetText("info_proxy"));

            //
            // Program - AutoUpdate
            //
            if ("1" == BusinessLayer.GetUserSettingsString(GlobalConstants.SectionProgram, GlobalConstants.KeyProgramAutoUpdate))
            {
                chkAutoUpdate.Checked = true;
                chkAutoUpdateLocal.Checked = false;
            }
            else if ("1" == BusinessLayer.GetUserSettingsString(GlobalConstants.SectionProgram, GlobalConstants.KeyProgramAutoUpdateLocal))
            {
                chkAutoUpdate.Checked = false;
                chkAutoUpdateLocal.Checked = true;
            }
            chkCopyUpdate.Checked = ("1" == BusinessLayer.GetUserSettingsString(GlobalConstants.SectionProgram, GlobalConstants.KeyProgramCopyUpdateFiles));
            if (chkAutoUpdateLocal.Checked || chkCopyUpdate.Checked)
            {
                txtAutoUpdateLocalFolder.Text = BusinessLayer.GetUserSettingsString(GlobalConstants.SectionProgram, GlobalConstants.KeyProgramAutoUpdateLocalFolder);
            }

            //
            // AutoImport
            //
            chkAutoImport.Checked = "1" == BusinessLayer.GetUserSettingsString(GlobalConstants.SectionOpImport, GlobalConstants.KeyOpImportAutoImport);

            txtPath.Text = BusinessLayer.GetUserSettingsString(GlobalConstants.SectionOpImport, GlobalConstants.KeyOpImportPath);
            chkInsertUnknownOperation.Checked = "1" == BusinessLayer.GetUserSettingsString(GlobalConstants.SectionOpImport, GlobalConstants.KeyOpImportInsertOperation);
            chkInsertUnknownSurgeon.Checked = "1" == BusinessLayer.GetUserSettingsString(GlobalConstants.SectionOpImport, GlobalConstants.KeyOpImportInsertSurgeon);
            chkIdentFirstName.Checked = "1" == BusinessLayer.GetUserSettingsString(GlobalConstants.SectionOpImport, GlobalConstants.KeyOpImportIdentFirstName);
            chkIdentifyOpFallzahl.Checked = "1" == BusinessLayer.GetUserSettingsString(GlobalConstants.SectionOpImport, GlobalConstants.KeyOpImportIdentifyOpByIdentifier);

            string opImportIdentifyByImportID = BusinessLayer.GetUserSettingsString(GlobalConstants.SectionOpImport, GlobalConstants.KeyOpImportIdentifyByImportID);
            radIdentifyByImportID.Checked = "1" == opImportIdentifyByImportID;
            radIdentifyByName.Checked = "0" == opImportIdentifyByImportID;

            string plugin = BusinessLayer.GetUserSettingsString(GlobalConstants.SectionOpImport, GlobalConstants.KeyOpImportPlugin);
            if (string.IsNullOrEmpty(plugin))
            {
                if (cbPlugins.Items.Count > 0)
                {
                    cbPlugins.SelectedIndex = 0;
                }
            }
            else
            {
                if (cbPlugins.Items.Count > 0)
                {
                    cbPlugins.Text = plugin;
                }
            }

            //
            // Serial numbers
            //
            chkSerialNumbersAutomatic.Checked = "1" == BusinessLayer.GetUserSettingsString(GlobalConstants.SectionSerialNumbers, GlobalConstants.KeySerialNumbersAutomatic);

            //
            // Print
            //
            PrintLinesConfigToControl(GlobalConstants.KeyPrintLinesBDCWeiterbildung, txtPrintBDCWeiterbildung);
            PrintLinesConfigToControl(GlobalConstants.KeyPrintLinesOperationenView, txtPrintOperationenView);
            PrintLinesConfigToControl(GlobalConstants.KeyPrintLinesOperationenZeitenVergleichView, txtPrintOperationenZeitenVergleichView);
            PrintLinesConfigToControl(GlobalConstants.KeyPrintLinesPlanOperationVergleichIstView, txtPrintPlanOperationVergleichIstView);
            PrintLinesConfigToControl(GlobalConstants.KeyPrintLinesAkademischeAusbildungView, txtPrintAkademischeAusbildungView);
            PrintLinesConfigToControl(GlobalConstants.KeyPrintLinesKlinischeErgebnisseView, txtPrintKlinischeErgebnisseView);
            PrintLinesConfigToControl(GlobalConstants.KeyPrintLinesRichtlinienVergleichOverviewView, txtPrintRichtlinienVergleichOverviewView);
            PrintLinesConfigToControl(GlobalConstants.KeyPrintLinesOperationenVergleichView, txtPrintOperationenVergleichView);
            PrintLinesConfigToControl(GlobalConstants.KeyPrintLinesDefaultView, txtPrintDefault);
            
            //
            // Sonstiges
            //
            chkOpenCommentMsg.Checked = "1" == BusinessLayer.GetUserSettingsString(GlobalConstants.SectionOther, GlobalConstants.KeyOtherDisplayOpenCommentMessage, "1");
            IntegerDBConfigToControl(
                GlobalConstants.SectionOther, 
                GlobalConstants.KeyOtherRelevantPositionsOPSCode, 
                txtStellenOPSKode, 
                GlobalConstants.OtherRelevantPositionsMin, 
                GlobalConstants.OtherRelevantPositionsMax,
                GlobalConstants.OtherRelevantPositionsDefault);

            _bIgnoreControlEvents = true;
            chkWatermark.Checked = "1" == BusinessLayer.GetUserSettingsString(GlobalConstants.SectionOther, GlobalConstants.KeyOtherWatermark, "1");
            UpdateListViewWatermark();
            _bIgnoreControlEvents = false;

        }

        private void UpdateListViewWatermark()
        {
            if (chkWatermark.Checked)
            {
                SetWatermark(lvWatermark, true, true);
            }
            else
            {
                lvWatermark.RemoveWatermark();
            }
        }

        private void IntegerDBConfigToControl(string section, string key, TextBox txt, int min, int max, int defaultValue)
        {
            int n;
            string lines = BusinessLayer.GetConfigValue(section  + "." + key);
            if (lines.Length > 0 && Int32.TryParse(lines, out n) && (n >= min && n <= max))
            {
                txt.Text = lines;
            }
            else
            {
                txt.Text = defaultValue.ToString();
            }
        }

        private void PrintLinesConfigToControl(string key, TextBox txt)
        {
            int n;
            string lines = BusinessLayer.GetUserSettingsString(GlobalConstants.SectionPrint, key);
            if (lines.Length > 0 && Int32.TryParse(lines, out n) 
                && (n > GlobalConstants.PrintLinesPerPageMin && n < GlobalConstants.PrintLinesPerPageMax))
            {
                txt.Text = lines;
            }
            else
            {
                txt.Text = GlobalConstants.KeyPrintLinesDefaultString;
            }
        }

        private void PopulatePlugins()
        {
            cbPlugins.Items.Clear();

            string strPluginPath = BusinessLayer.PathPlugins;

            if (Directory.Exists(strPluginPath))
            {
                DirectoryInfo dir = new DirectoryInfo(strPluginPath);
                FileInfo[] files = dir.GetFiles("*.dll");

                AppDomain curDomain = AppDomain.CurrentDomain;
                curDomain.ReflectionOnlyAssemblyResolve += new ResolveEventHandler(curDomain_ReflectionOnlyAssemblyResolve);

                foreach (FileInfo file in files)
                {
                    Type[] types = null;
                    Assembly asm = null;

                    try
                    {
                        asm = Assembly.ReflectionOnlyLoadFrom(file.FullName);
                        types = asm.GetTypes();

                        foreach (Type t in types)
                        {
                            if (BusinessLayer.IsValidPlugin(t))
                            {
                                //
                                // Plugin Operationen.OperationenImportSqlCcopm
                                // may not be selected, because it requires user interaction
                                // We cannot call Plugin Id because we have no instance!
                                // So we must make this very bad file name comparison.
                                //
                                if (file.Name.ToLower().IndexOf("sqlccopm") == -1)
                                {
                                    cbPlugins.Items.Add(file.Name);
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        string text = string.Format(CultureInfo.InvariantCulture, GetText(FormName, "errLoadingPlugin"), file.FullName);

                        if (DisplayPluginLoadExceptionAndQueryForBreak(e, text))
                        {
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                curDomain.ReflectionOnlyAssemblyResolve -= new ResolveEventHandler(curDomain_ReflectionOnlyAssemblyResolve);
            }
        }

        private void EnableControlsProxy()
        {
            txtProxyAddress.Enabled =
            txtProxyUser.Enabled =
            txtProxyPassword.Enabled =
            txtProxyDomain.Enabled =
                radProxyUser.Checked;

            if (!radProxyUser.Checked)
            {
                txtProxyAddress.Text = "";
                txtProxyUser.Text = "";
                txtProxyPassword.Text = "";
                txtProxyDomain.Text = "";
            }
        }
        private void EnableControlsAutoImport()
        {
            radIdentifyByName.Enabled =
            radIdentifyByImportID.Enabled =
            cmdPath.Enabled =
            chkInsertUnknownOperation.Enabled =
            chkInsertUnknownSurgeon.Enabled =
            chkIdentFirstName.Enabled =
            chkIdentifyOpFallzahl.Enabled = 
                chkAutoImport.Checked;
        }

        private void EnableControlsAutoUpdate()
        {
            cmdAutoUpdateFolder.Enabled =
            txtAutoUpdateLocalFolder.Enabled =
                chkAutoUpdateLocal.Checked || chkCopyUpdate.Checked;
        }

        private void ProxyCheckedChanged(object sender, EventArgs e)
        {
            EnableControlsProxy();
        }

        private void cmdPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            dlg.RootFolder = Environment.SpecialFolder.MyComputer;
            dlg.SelectedPath = txtPath.Text;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                txtPath.Text = dlg.SelectedPath;
            }
        }

        private void chkAutoImport_CheckedChanged(object sender, EventArgs e)
        {
            EnableControlsAutoImport();
        }

        private void cmdAutoUpdateFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            dlg.RootFolder = Environment.SpecialFolder.MyComputer;
            dlg.SelectedPath = txtAutoUpdateLocalFolder.Text;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                txtAutoUpdateLocalFolder.Text = dlg.SelectedPath;
            }
        }

        private void chkCopyUpdate_CheckedChanged(object sender, EventArgs e)
        {
            EnableControlsAutoUpdate();
        }

        private void lblInfoAutoSerial_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OperationenLogbuchView.TheMainWindow.StartMenuItem(e.Link.LinkData);
        }

        private void chkAutoUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (!_bIgnoreControlEvents)
            {
                _bIgnoreControlEvents = true;
                if (chkAutoUpdate.Checked)
                {
                    chkAutoUpdateLocal.Checked = false;
                }
                EnableControlsAutoUpdate();
                _bIgnoreControlEvents = false;
            }
        }

        private void chkAutoUpdateLocal_CheckedChanged(object sender, EventArgs e)
        {
            if (!_bIgnoreControlEvents)
            {
                _bIgnoreControlEvents = true;
                if (chkAutoUpdateLocal.Checked)
                {
                    chkAutoUpdate.Checked = false;
                }
                EnableControlsAutoUpdate();
                _bIgnoreControlEvents = false;
            }
        }


        private void IdentificationChanged()
        {
            chkIdentFirstName.Enabled = !radIdentifyByImportID.Checked;
        }

        private void radIdentifyByImportID_CheckedChanged(object sender, EventArgs e)
        {
            IdentificationChanged();
        }

        private void radIdentifyByName_CheckedChanged(object sender, EventArgs e)
        {
            IdentificationChanged();
        }

        private void chkWatermark_CheckedChanged(object sender, EventArgs e)
        {
            if (!_bIgnoreControlEvents)
            {
                UpdateListViewWatermark();
            }
        }

        private void tabOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is TabControl)
            {
                TabControl tabControl = (TabControl)sender;

                int index = tabControl.SelectedIndex;

                lblInfo.Text = GetText("tab_info" + index);
            }
        }
    }
}

