using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;


namespace Operationen.Setup
{
    public class Installer
    {
        private ProgressBar _progressBar;

        private string _startupPath;

        private bool _isUpdate;

        private int _installationMode;
        private string _programFolder;
        private string _databaseFolder;

        private string _programTitle = SetupData.ProgramName;


        public Installer(ProgressBar progressBar, bool IsUpdate, int installationMode, string programFolder, string databaseFolder)
        {
            _progressBar = progressBar;

            _progressBar.Minimum = 0;
            _progressBar.Maximum = SetupData.ProgramFiles.Length;

            _startupPath = Application.StartupPath;

            _isUpdate = IsUpdate;
            _installationMode = installationMode;
            _programFolder = programFolder;
            _databaseFolder = databaseFolder;
        }

        /// <summary>
        /// Setup
        /// </summary>
        /// <param name="progressBar"></param>
        /// <param name="isStandalone"></param>
        /// <param name="programFolder"></param>
        /// <param name="databaseFolder"></param>
        /// <param name="useExistingDatabase"></param>
        public Installer(ProgressBar progressBar, int installationMode, string programFolder, string databaseFolder) :
             this(progressBar, false, installationMode, programFolder, databaseFolder)
        {
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="progressBar"></param>
        /// <param name="programFolder"></param>
        public Installer(ProgressBar progressBar, string programFolder)
            : this(progressBar, true, SetupWizardPage.ModeSingleUser, programFolder, null)
        {
        }

        private void Progress()
        {
            if (_progressBar.Value < _progressBar.Maximum - 1)
            {
                _progressBar.Value++;
            }
            if (_progressBar.Value == _progressBar.Maximum)
            {
                _progressBar.Value = 0;
            }

            Application.DoEvents();
        }

        
        private bool CreateDesktopLinks()
        {
            try
            {
                string specialFolder;

                // Desktop
                specialFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                if (Link.Exists(specialFolder, SetupData.LinkName))
                {
                    // delete link
                    Link.Update(specialFolder, _programFolder + "\\" + SetupData.ProgramExeFileName, _programFolder, SetupData.LinkName, false);
                }
                // create link
                Link.Update(specialFolder, _programFolder + "\\" + SetupData.ProgramExeFileName, _programFolder, SetupData.LinkName, true);

                // Start | All Programs
                specialFolder = Environment.GetFolderPath(Environment.SpecialFolder.Programs);
                if (Link.Exists(specialFolder, SetupData.LinkName))
                {
                    // delete link
                    Link.Update(specialFolder, _programFolder + "\\" + SetupData.ProgramExeFileName, _programFolder, SetupData.LinkName, false);
                }

                //create link
                Link.Update(specialFolder, _programFolder + "\\" + SetupData.ProgramExeFileName, _programFolder, SetupData.LinkName, true);
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message);
                goto Exit;
            }


            Exit:

            // Wenn hier etwas nicht klappt, darf deswegen die Installation nicht abbrechen.
            return true;
        }

        public bool Install()
        {
            bool success = true;

            _progressBar.Visible = true;

            success = CreateRegistryEntriesHTMLHelp() && success;

            //
            // After lots of trying: do everything and display only a failure message,
            // don't skip anything!
            //

            if (_isUpdate)
            {
                success = CreateDirectories() && success;
                Progress();
                success = CopyProgram() && success;
                Progress();
                success = CopyDokumente() && success;
                Progress();
                success = CreateDesktopLinks() && success;
                Progress();
                success = CreateRegistryEntries() && success;
                Progress();
            }
            else
            {
                if (_installationMode == SetupWizardPage.ModeMultiOneShortcut)
                {
                    //
                    // Shortcut mode: create shortcut only and run caspol.exe, don't copy any files.
                    // addTrust.bat has been created when SetupWizardPage.ModeMultiOneProgram was executed
                    // Don't add registry entries, as one could delete the common installation afterwards!
                    //
                    success = CheckDirectories() && success;
                    Progress();
                    success = CreateDesktopLinks() && success;
                    Progress();

                    //
                    // addTrust.bat has been created when SetupWizardPage.ModeMultiOneProgram was executed
                    //
                    success = RunAddTrust() && success;
                    Progress();
                }
                else
                {
                    success = CheckDirectories() && success;
                    Progress();
                    success = CreateDirectories() && success;
                    Progress();
                    success = CopyProgram() && success;
                    Progress();
                    success = CopyDatabase() && success;
                    Progress();
                    success = CopyDokumente() && success;
                    Progress();
                    success = CreateConfigFile() && success;
                    Progress();
                    success = CreateDesktopLinks() && success;
                    Progress();
                    success = CreateAddTrust() && success;
                    Progress();
                    success = RunAddTrust() && success;
                    Progress();
                    success = CreateRegistryEntries() && success;
                    Progress();
                }
            }
            _progressBar.Value = _progressBar.Maximum;

            return success;
        }

        private bool CreateDirectory(string folder)
        {
            bool success = true;

            try
            {
                Directory.CreateDirectory(folder);
            }
            catch
            {
                ShowMessageBox(string.Format("Das Verzeichnis {0} kann nicht erzeugt werden.", folder)); // TODO
                success = false;
            }

            return success;
        }


        private bool ReplaceTextAndCopyTextFile(string src, string dst, Dictionary<string,string> replacementTexts)
        {
            bool success = false;

            if (!System.IO.File.Exists(src))
            {
                ShowMessageBox(string.Format("Die Datei {0} fehlt.", src));
                goto _exit;
            }

            StreamReader reader = null;
            string line = null;

            try
            {
                // Die Batch Dateien und die Config Dateien sind NICHT Unicode!
                reader = new StreamReader(src, Encoding.ASCII);
                line = reader.ReadToEnd();
            }
            catch
            {
                ShowMessageBox(string.Format("Die Datei {0} konnte nicht gelesen werden.", src));
                goto _exit;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            StringBuilder sb = new StringBuilder(line);

            if (replacementTexts != null)
            {
                foreach (string key in replacementTexts.Keys)
                {
                    string value = replacementTexts[key];
                    if (string.IsNullOrEmpty(value))
                    {
                        value = "";
                    }
                    sb.Replace(key, value);
                }
            }

            StreamWriter writer = null;
            try
            {
                // Die Batch Dateie und die Config Dateien sind NICHT Unicode!
                writer = new StreamWriter(dst, false, Encoding.ASCII);
                writer.Write(sb.ToString());
            }
            catch
            {
                ShowMessageBox(string.Format("Die Datei {0} konnte nicht geschrieben werden.", dst));
                goto _exit;
            }
            finally
            {
                if (writer != null)
                {
                    writer.Flush();
                    writer.Close();
                }
            }

            success = true;

        _exit:
            Progress();

            return success;
        }

        private bool CreateAddTrust()
        {
            string src = _startupPath + Path.DirectorySeparatorChar + "files" + Path.DirectorySeparatorChar  + "addTrust.bat";
            string dst = _programFolder + Path.DirectorySeparatorChar  + "addTrust.bat";

            //
            // create and copy so that it can be viewed and modified.
            // If caspol.exe is missing, never mind. We are only creating the .bat file
            //
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("$__PLACEHOLDER__PROGRAM__$", _programFolder);
            d.Add("$__PLACEHOLDER__DATA__$", _databaseFolder);

            return ReplaceTextAndCopyTextFile(src, dst, d);
        }

        private bool RunAddTrust()
        {
            bool success = false;
            string dst = _programFolder + Path.DirectorySeparatorChar  + "addTrust.bat";

            //
            // if caspol.exe as specified in addTrust.bat is missing, don't execute addTrust.bat
            //
            string fullPathCaspol = Environment.SystemDirectory;

            int index = fullPathCaspol.LastIndexOf(Path.DirectorySeparatorChar);
            if (index != -1)
            {
                fullPathCaspol = fullPathCaspol.Substring(0, index);
            }
            fullPathCaspol += Path.DirectorySeparatorChar  + "Microsoft.NET" + Path.DirectorySeparatorChar + "Framework" + Path.DirectorySeparatorChar  
                + "v2.0.50727" + Path.DirectorySeparatorChar  + "caspol.exe";

            if (!System.IO.File.Exists(fullPathCaspol))
            {
                // kein false zurückgeben, denn das hier soll nichts abbrechen!
                // Auch keine Messagebox, denn das ist zuviel für die Benutzer
                ShowMessageBox(string.Format("Die Datei {0} fehlt.", fullPathCaspol));
                goto _exit;
            }

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = dst;

            try
            {
                success = proc.Start();
            }
            catch (Exception e)
            {
                ShowMessageBox(string.Format("Die Datei {0} konnte nicht ausgeführt werden.\r\r{1}", dst, e.Message));
            }

            _exit:
            return success;
        }

        private bool CreateConfigFile()
        {
            string src = _startupPath + Path.DirectorySeparatorChar + "files" + Path.DirectorySeparatorChar + SetupData.ProgramConfigFileName;
            string dst = _programFolder + Path.DirectorySeparatorChar + SetupData.ProgramConfigFileName;

            string databasePath;
            databasePath = _databaseFolder;

            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("$__PLACEHOLDER__$", databasePath);
            bool success = ReplaceTextAndCopyTextFile(src, dst, d);

            return success;
        }

        /// <summary>
        /// Kopiert die Dokumente. Kopiert nur Dateien, die es noch nicht gibt.
        /// Bricht nicht ab, wenn ein Dokument nicht kopiert werden kann.
        /// </summary>
        /// <returns>true if all files were copied, false if not.</returns>
        public bool CopyDokumente()
        {
            bool success = true;

            foreach (string file in SetupData.Documents)
            {
                string src = _startupPath + Path.DirectorySeparatorChar + "files" + Path.DirectorySeparatorChar + file;
                string dst = _programFolder + Path.DirectorySeparatorChar + file;

                success = success && SetupData.CopyFileIfNotExists(src, dst);
            }

            // return the overall copy success, if one failed, return false
            return success;
        }

        public bool CopyProgram()
        {
            bool success = true;

            foreach (string file in SetupData.ProgramFiles)
            {
                string src = _startupPath + Path.DirectorySeparatorChar  + "files" + Path.DirectorySeparatorChar + file;
                string dst = _programFolder + Path.DirectorySeparatorChar + file;

                success = success && SetupData.CopyFile(src, dst);

                if (!success)
                {
                    break;
                }
            }

            return success;
        }

        /// <summary>
        /// Alle Überprüfungen am Anfang, damit man nicht mittendrin
        /// merkt, dass etwas nicht stimmt.
        /// </summary>
        /// <returns></returns>
        private bool CheckDirectories()
        {
            bool success = true;

            if (_installationMode == SetupWizardPage.ModeSingleUser)
            {
                if (_databaseFolder != _programFolder)
                {
                    ShowMessageBox(
                        "Bei der Single-User Installation müssen das Programmverzeichnis"
                        + " und das Datenbankverzeichnis identisch sein!."); // TODO

                    success = false;
                    goto _exit;
                }
            }
            else
            {
                if (_databaseFolder == _programFolder)
                {
                    MessageBox.Show(
                        "Bei der Multi-User Installation dürfen das Programmverzeichnis"
                        + " und das Datenbankverzeichnis nicht identisch sein!.", 
                       _programTitle);

                    success = false;
                    goto _exit;
                }
            }

            _exit:
            return success;
        }

        private bool CreateDirectories()
        {
            bool success = true;

            success = success && CreateDirectory(_programFolder);

            foreach (string subdir in SetupData.ProgramFolderSubdirectories)
            {
                success = success && CreateDirectory(_programFolder + Path.DirectorySeparatorChar + subdir);
            }

            if (!_isUpdate)
            {
                success = success && CreateDirectory(_databaseFolder);
                foreach (string subdir in SetupData.DatabaseFolderSubdirectories)
                {
                    success = success && CreateDirectory(_databaseFolder + Path.DirectorySeparatorChar + subdir);
                }
            }

            return success;
        }

        private bool CopyDatabase()
        {
            bool success = true;

            string src;
            string dst;

            // Die Datenbank
            src = _startupPath + Path.DirectorySeparatorChar  + "files" + Path.DirectorySeparatorChar + "operationen.mdb";
            dst = _databaseFolder + Path.DirectorySeparatorChar  + "operationen.mdb";

            if (System.IO.File.Exists(dst))
            {
                // Keine Meldung, wenn es die Datenbank schon gibt.
                // Message boxes during a setup suck.
            }

            // Nur kopieren, wenn nicht schon vorhanden!
            if (!System.IO.File.Exists(dst))
            {
                success = success && SetupData.CopyFile(src, dst);
            }

            return success;
        }

        private bool CreateRegistryEntries()
        {
            bool success = false;
            string s;

            try
            {
                RegistryKey hklm = Registry.LocalMachine;

                s = "SOFTWARE\\" + SetupData.REG_KEY_LOGBUCH;
                try
                {
                    hklm.DeleteSubKeyTree(s);
                }
                catch
                {
                }

                RegistryKey logbuch = hklm.CreateSubKey(s);
                logbuch.SetValue(SetupData.REG_ENTRY_PROGRAM_FOLDER, _programFolder);
                if (!_isUpdate)
                {
                    // Beim Update ist _databaseFolder nicht gesetzt
                    // Dieser key wird nie benutzt, steht aber als Info in der registry
                    logbuch.SetValue(SetupData.REG_ENTRY_DATABASE_FOLDER, _databaseFolder);
                }


                // Create entries for uninstaller
                s = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\" + SetupData.REG_KEY_LOGBUCH;
                try
                {
                    hklm.DeleteSubKeyTree(s);
                }
                catch
                {
                }

                RegistryKey uninstallLogbuch = hklm.CreateSubKey(s);
                uninstallLogbuch.SetValue("DisplayName", SetupData.ProgramName);
                uninstallLogbuch.SetValue("InstallLocation", _programFolder);
                uninstallLogbuch.SetValue("Publisher", SetupData.ProgramName);
                uninstallLogbuch.SetValue("UninstallString", _programFolder + Path.DirectorySeparatorChar  + "uninstall.exe");
                uninstallLogbuch.SetValue("URLInfoAbout", SetupData.WebSite);

                success = true;
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message);
            }

            return success;
        }

        private void ShowMessageBox(string text)
        {
            MessageBox.Show(text, _programTitle);
        }

        /// <summary>
        /// .CHM file opened via a UNC share does not display topics unless the registry is tweaked.
        /// See http://support.microsoft.com/kb/896054/
        /// Also copied to D:\Daten\Develop\DOT.NET\Operationen\Dokumentation\Dokumentation\Hilfe\chm Files
        /// </summary>
        /// <returns></returns>
        private bool CreateRegistryEntriesHTMLHelp()
        {
            bool success = false;

            try
            {
                RegistryKey hklm = Registry.LocalMachine;

                RegistryKey htmlHelp = hklm.CreateSubKey("SOFTWARE\\Microsoft\\HTMLHelp");
                htmlHelp = hklm.CreateSubKey("SOFTWARE\\Microsoft\\HTMLHelp\\1.x");
                htmlHelp = hklm.CreateSubKey("SOFTWARE\\Microsoft\\HTMLHelp\\1.x\\ItssRestrictions");

                // "\\cmaurer\Logbuch\;file://\\cmaurer\Logbuch;"
                htmlHelp.SetValue("UrlAllowList", string.Format("{0}\\;file://{0}", _programFolder));

                success = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return success;
        }
    }
}
