using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Runtime;

using Microsoft.Win32;

namespace Operationen.Setup
{
    public delegate void ProgressCallback(int percent, string text);

    public partial class MainView : Form
    {
        private string _programFolder = "";

        public MainView()
        {
            InitializeComponent();
        }

        public static bool CopyFile(string src, string dst)
        {
            bool success = false;

            try
            {
                success = success && SetupData.CopyFile(src, dst);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return success;
        }

        private void DeleteFile(string fileName)
        {
            bool queryUserOnError = true;

            Retry:

            if (!System.IO.File.Exists(fileName))
            {
                // Datei gibts nicht, braucht man sie auch nicht zu löschen
                goto _exit;
            }

            try
            {
                System.IO.File.Delete(fileName);
            }
            catch
            {
                try
                {
                    // Kopieren hat nicht geklappt, evtl. ist die Datei schreibgeschützt, 
                    // also den Schreibschutz entfernen.
                    System.IO.File.SetAttributes(fileName, FileAttributes.Normal);
                    System.IO.File.Delete(fileName);
                }
                catch
                {
                    DialogResult result = DialogResult.Abort;

                    if (queryUserOnError)
                    {
                        result = MessageBox.Show(string.Format("Die Datei \r\r'{0}'\r\rkann nicht gelöscht werden."
                            + "\r\rFalls das Programm '{1}' gestartet ist, beenden Sie es bitte und versuchen es noch einmal."
                            , fileName, SetupData.ProgramName),
                            SetupData.ProgramName, MessageBoxButtons.AbortRetryIgnore);
                    }

                    if (result == DialogResult.Retry)
                    {
                        goto Retry;
                    }
                    else if (result == DialogResult.Abort)
                    {
                        goto _exit;
                    }
                }
            }

        _exit: ;
        }

        private void DeleteFolder(string subdir)
        {
            bool queryUserOnError = true;

            Retry:

            if (!System.IO.Directory.Exists(subdir))
            {
                // Datei gibts nicht, braucht man sie auch nicht zu löschen
                goto _exit;
            }

            try
            {
                System.IO.Directory.Delete(subdir, true);
            }
            catch
            {
                DialogResult result = DialogResult.Abort;

                if (queryUserOnError)
                {
                    result = MessageBox.Show(string.Format("Das Verzeichnis \r\r'{0}'\r\rkann nicht gelöscht werden."
                        + "\r\rFalls das Programm '{1}' gestartet ist, beenden Sie es bitte und versuchen es noch einmal."
                        , subdir, SetupData.ProgramName),
                        SetupData.ProgramName, MessageBoxButtons.AbortRetryIgnore);
                }

                if (result == DialogResult.Retry)
                {
                    goto Retry;
                }
                else if (result == DialogResult.Abort)
                {
                    goto _exit;
                }
            }

        _exit: ;
        }

        private void DeleteProgramFiles()
        {
            foreach (string fileName in SetupData.ProgramFiles)
            {
                string fullPath = string.Format("{0}\\{1}", _programFolder, fileName);
                DeleteFile(fullPath);
            }
            foreach (string fileName in SetupData.ProgramFilesForUninstall)
            {
                string fullPath = string.Format("{0}\\{1}", _programFolder, fileName);
                DeleteFile(fullPath);
            }

            foreach (string subdir in SetupData.ProgramFolderSubdirectories)
            {
                string fullPath = string.Format("{0}\\{1}", _programFolder, subdir);
                DeleteFolder(fullPath);
            }

            DeleteFolder(_programFolder);
        }

        private void RemoveFromRegistry()
        {
            try
            {
                RegistryKey hklm = Registry.LocalMachine;

                string s = "SOFTWARE\\" + SetupData.REG_KEY_LOGBUCH;
                hklm.DeleteSubKeyTree(s);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            try
            {
                RegistryKey hklm = Registry.LocalMachine;

                string s = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\" + SetupData.REG_KEY_LOGBUCH;
                hklm.DeleteSubKeyTree(s);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void RemoveShortcuts()
        {
            try
            {
                string specialFolder;

                specialFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                if (Link.Exists(specialFolder, SetupData.LinkName))
                {
                    // delete link
                    Link.Update(specialFolder, _programFolder + "\\" + SetupData.ProgramExeFileName, _programFolder, SetupData.LinkName, false);
                }

                specialFolder = Environment.GetFolderPath(Environment.SpecialFolder.Programs);
                if (Link.Exists(specialFolder, SetupData.LinkName))
                {
                    // delete link
                    Link.Update(specialFolder, _programFolder + "\\" + SetupData.ProgramExeFileName, _programFolder, SetupData.LinkName, false);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// This program is started via the control panel - add or remove programs.
        /// It is copied into a separate temp folder and started from there, otherwise
        /// it could not delete the folder it was started from.
        /// </summary>
        private void Uninstall()
        {
            string s;

            try
            {
                RegistryKey hklm = Registry.LocalMachine;

                s = "SOFTWARE\\" + SetupData.REG_KEY_LOGBUCH;
                RegistryKey logbuch = hklm.OpenSubKey(s);

                if (logbuch != null)
                {
                    _programFolder = (string)logbuch.GetValue(SetupData.REG_ENTRY_PROGRAM_FOLDER);
                }

                // program folder very basic check. Muss irgendeinen Wert haben. c:\tmp reicht schon!
                if (_programFolder.Length > 5)
                {
                    DeleteProgramFiles();
                    RemoveShortcuts();
                }
                RemoveFromRegistry();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void cmdRemove_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(string.Format("Sind Sie sicher, dass Sie {0} entfernen wollen?"
                + "\nHiermit werden alle Ihre Daten gelöscht.",
                SetupData.ProgramName), SetupData.ProgramName,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;
                Uninstall();
                Cursor = Cursors.Default;

                lblStatus.Text = SetupData.ProgramName + " wurde von Ihrem Computer entfernt.";
                cmdClose.Visible = true;
                cmdRemove.Visible = false;
                
                Application.DoEvents();
            }
        }

        private void MainView_Load(object sender, EventArgs e)
        {
            lblStatus.BorderStyle = BorderStyle.None;

            Text = SetupData.ProgramName + " entfernen";
            cmdClose.Visible = false;
            cmdRemove.Visible = true;
            cmdRemove.Text = SetupData.ProgramName + " entfernen";
            lblStatus.Text = "Hiermit wird " + SetupData.ProgramName + " von Ihrem Computer entfernt.";
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}