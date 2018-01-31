using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using Utility;
using CMaurer.Operationen.AppFramework;

namespace Operationen
{
    /// <summary>
    /// Verwaltet die Tabelle Dokumente.
    /// Dokumente enthält Dateien, die für einen Chirurgen als Logbuch angelegt werden können.
    /// Wen man eine neue Datei anlegt, wird die nach 
    /// Server\Dokumente kopiert und nur der Dateiname ohne Pfad
    /// in die Datenbank eingetragen.
    /// 
    /// Der Dateiname kann auch nicht geändert werden, denn dann
    /// muesste man die Datei umbenennen, das ist zu kompliziert.
    /// </summary>
    public partial class CopyWWWProgramUpdateFilesView : OperationenForm
    {
        public CopyWWWProgramUpdateFilesView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();

            cmdVerzeichnis.SetSecurity(BusinessLayer, "CopyWWWProgramUpdateFilesView.cmdVerzeichnis");
            cmdCopy.SetSecurity(BusinessLayer, "CopyWWWProgramUpdateFilesView.cmdCopy");
            cmdCopy.Enabled = false;
        }

        private void CopyWWWProgramUpdateFilesView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));

            cmdVerzeichnis.Text = GetText("cmdVerzeichnis");
            cmdCopy.Text = GetText("cmdCopy");

            SetInfoText(lblInfo1Text, GetText("info1"));
            SetInfoText(lblInfo2Text, string.Format(GetText("info2"), cmdCopy.Text));
            lblInfo3Text.Text = "";

            string localFolder = BusinessLayer.GetUserSettingsString(GlobalConstants.SectionProgram, GlobalConstants.KeyProgramAutoUpdateLocalFolder);
            if (!string.IsNullOrEmpty(localFolder))
            {
                if (Directory.Exists(localFolder))
                {
                    txtVerzeichnis.Text = localFolder;
                    cmdCopy.Enabled = true;
                }
            }
        }

        private void cmdVerzeichnis_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            dlg.RootFolder = Environment.SpecialFolder.MyComputer;
            dlg.SelectedPath = txtVerzeichnis.Text;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                txtVerzeichnis.Text = dlg.SelectedPath;

                cmdCopy.Enabled = Directory.Exists(txtVerzeichnis.Text);
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdCopy_Click(object sender, EventArgs e)
        {
            if (CopyFiles())
            {
                string tempFolder = txtVerzeichnis.Text;
                string msg = string.Format(GetText("info3_ok"), tempFolder, Command_OptionsView_tabUpdate);
                SetInfoText(lblInfo3Text, msg);
                MessageBox(msg);
            }
            else
            {
                string msg = GetText("info3_err");
                SetInfoText(lblInfo3Text, msg);
                MessageBox(msg);
            }
        }

        private bool CopyFiles()
        {
            bool success = false;

            string versionInfo;
            string tempFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string localFolder = txtVerzeichnis.Text;

            string tempVersionFile = tempFolder + System.IO.Path.DirectorySeparatorChar + BusinessLayer.VERSION_DOWNLOAD_FILENAME;
            string localVersionFile = localFolder + System.IO.Path.DirectorySeparatorChar + BusinessLayer.VERSION_DOWNLOAD_FILENAME;

            // beide Verzeichnisse überprüfen
            if (!Directory.Exists(localFolder))
            {
                MessageBox(string.Format(GetText("missing_dir"), localFolder));
                goto exit;
            }
            if (!Directory.Exists(tempFolder))
            {
                MessageBox(string.Format(GetText("missing_dir"), tempFolder));
                goto exit;
            }

            // delete version.txt in temp folder
            if (!Utility.Tools.DeleteFile(tempVersionFile))
            {
                MessageBox(string.Format(GetText("err_delete_file"), tempVersionFile));
                goto exit;
            }

            // download version.txt to temp folder
            if (!DownloadAsciiInfoFile(
                    BusinessLayer.UrlHomepageForDownload + "/download/" + BusinessLayer.VERSION_DOWNLOAD_FILENAME,
                    true,
                    tempVersionFile,
                    out versionInfo))
            {
                // delete file in any case if download failed
                Utility.Tools.DeleteFile(tempVersionFile);
                goto exit;
            }

            string tempSetupFile;
            string localSetupFile;

            if (versionInfo == null)
            {
                    MessageBox(string.Format(GetText("bad_version_file"), tempVersionFile));
                    goto exit;
            }

            string[] arVersionInfo = versionInfo.Split('|');
            if (arVersionInfo.Length != 3)
            {
                MessageBox(string.Format(GetText("bad_version_file"), tempVersionFile));
                goto exit;
            }

            string setupFilename;

            // version.txt enthaelt: "1.7.3|1013|operationen-logbuch-V1.7.3.exe"
            // version.txt enthaelt: "1.16.0|6123|operationen-logbuch-update.exe"
            // version-urologie.txt enthaelt: "1.17.1|6060|operationen-update-urologie.exe"
            // version-gynaekologie.txt enthaelt: "1.17.1|6060|operationen-update-gynaekologie.exe"
            setupFilename = arVersionInfo[2];
            tempSetupFile = tempFolder + System.IO.Path.DirectorySeparatorChar + setupFilename;
            localSetupFile = localFolder + System.IO.Path.DirectorySeparatorChar + setupFilename;

            // default Wert
            int fileSizeKb = 15868;
            
            Int32.TryParse(arVersionInfo[1], out fileSizeKb);

            // delete setup.exe in temp folder
            if (!Utility.Tools.DeleteFile(tempSetupFile))
            {
                MessageBox(string.Format(GetText("err_delete_file"), tempSetupFile));
                goto exit;
            }

            if (!DownloadFile(
                GetText("download_file"),
                fileSizeKb,
                BusinessLayer.UrlHomepageForDownload + "/download/" + setupFilename,
                tempSetupFile))
            {
                // delete file in any case if download failed
                Utility.Tools.DeleteFile(tempSetupFile);
                goto exit;
            }

            // both file downloaded to temp folder. Delete existing files...
            if (!Utility.Tools.DeleteFile(localVersionFile))
            {
                MessageBox(string.Format(GetText("err_delete_file"), localVersionFile));
                goto exit;
            }
            if (!Utility.Tools.DeleteFile(localSetupFile))
            {
                MessageBox(string.Format(GetText("err_delete_file"), localSetupFile));
                goto exit;
            }
            // ...and copy from temp to update folder
            if (!BusinessLayer.CopyFile(tempVersionFile, localVersionFile, BusinessLayer.ProgramTitle))
            {
                MessageBox(string.Format(GetText("err_copy_file"), tempSetupFile, localVersionFile));
                // delete temp and local version.txt
                Utility.Tools.DeleteFile(tempVersionFile);
                Utility.Tools.DeleteFile(localVersionFile);
                goto exit;
            }
            if (!BusinessLayer.CopyFile(tempSetupFile, localSetupFile, BusinessLayer.ProgramTitle))
            {
                MessageBox(string.Format(GetText("err_copy_file"), tempSetupFile, localSetupFile));
                // version.txt has been copied. Delete that and setup.exe in both temp and local folder.
                Utility.Tools.DeleteFile(tempVersionFile);
                Utility.Tools.DeleteFile(localVersionFile);
                Utility.Tools.DeleteFile(tempSetupFile);
                Utility.Tools.DeleteFile(localSetupFile);
                goto exit;
            }
            success = true;

        exit:
            return success;
        }
    }
}
