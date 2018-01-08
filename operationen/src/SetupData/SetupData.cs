using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Operationen.Setup
{
    public class SetupData
    {
        public const string SetupFileName = "OP-LOG";
        public const string WebSite = "http://www.op-log.de";
        public const string ProgramExeFileName = "operationen.exe";
        public const string ProgramConfigFileName = "operationen.exe.config";

#if urologie
        public const string ProgramName = "OP-LOG-Urologie";
        public const string LinkName = "OP-LOG Urologie";
        public const string REG_KEY_LOGBUCH = "OP-LOG-Urologie";
#elif gynaekologie
        public const string ProgramName = "OP-LOG-Gynaekologie";
        public const string LinkName = "OP-LOG Gynaekologie";
        public const string REG_KEY_LOGBUCH = "OP-LOG-Gynaekologie";
#else
        public const string ProgramName = "OP-LOG";
        public const string LinkName = "OP-LOG";
        public const string REG_KEY_LOGBUCH = "Logbuch-Weiterbildung";
#endif

        public const string REG_ENTRY_PROGRAM_FOLDER = "ProgramFolder";
        public const string REG_ENTRY_DATABASE_FOLDER = "DatabaseFolder";
        public const string REG_ENTRY_SECURITY = "Security";

        private static string[] _databaseFolderSubdirectories = 
        {
            "Dokumente",
            "Edit",
            "Logfiles"
        };

        private static string[] _programFolderSubdirectories = 
        {
            "Plugins",
            "en-US",
            "de-DE"
        };

        private static string[] _programFiles = 
        { 
            "en-US\\Operationen.resources.dll",
            "de-DE\\Elegant.Ui.Common.resources.dll",
            "de-DE\\Elegant.Ui.Ribbon.resources.dll",
            "Interop.IWshRuntimeLibrary.dll", 
            "MySql.Data.dll",
            "Plugins\\Operationen.OperationenImportCSV.dll",
            "Plugins\\Operationen.OperationenImportCSVUnicode.dll",
            "Plugins\\Operationen.OperationenImportIcpm3Op3CSV.dll",
            "Plugins\\Operationen.OperationenImportIcpm3Op3CSVUnicode.dll",
            "Plugins\\Operationen.OperationenImportIcpm5Op3CSV.dll",
            "Plugins\\Operationen.OperationenImportIcpm5Op3CSVUnicode.dll",
            "Plugins\\Operationen.OperationenImportIKPM10.dll",
            "Plugins\\Operationen.OperationenImportIKPM10Unicode.dll",
            "Plugins\\Operationen.OperationenImportMccIsop.dll",
            "Plugins\\Operationen.OperationenImportMccIsopUnicode.dll",
            "Plugins\\Operationen.OperationenImportOrbis.dll",
            "Plugins\\Operationen.OperationenImportOrbisUnicode.dll",
            "Plugins\\Operationen.OperationenImportOrbisText.dll",
            "Plugins\\Operationen.OperationenImportOrbisTextUnicode.dll",
            "Plugins\\Operationen.OperationenImportSapCsv.dll",
            "Plugins\\Operationen.OperationenImportSapCsvUnicode.dll",
            "Elegant.Ui.Common.dll",
            "Elegant.Ui.Common.Theme.Office2007Blue.dll",
            "Elegant.Ui.Ribbon.dll",
            "Elegant.Ui.Ribbon.Theme.Office2007Blue.dll",
            "AppFramework.dll",
            "Windows.Forms.dll",
            "operationen.exe",
            "operationen.chm",
            "CMaurer.Operationen.AppFramework.dll",
            "Operationen.OperationenImport.dll",
            "Sekurity.dll",
            "Utility.dll",
            "License.txt",
            "changelog.txt",
            "chirurg.mdb",
            "SetupData.dll",
            "Uninstall.exe",
            "2014.oplog-V2.oplog",
        };
        private static string[] _programFilesForUninstall = 
        { 
            "addTrust.bat",
            "operationen.exe.config"
        };

        private static string[] _documents = 
        {
        };

        public static string[] Documents
        {
            get { return _documents; }
        }
        public static string[] ProgramFolderSubdirectories
        {
            get { return _programFolderSubdirectories; }
        }
        public static string[] ProgramFiles
        {
            get { return _programFiles; }
        }

        public static string[] ProgramFilesForUninstall
        {
            get { return _programFilesForUninstall; }
        }

        public static string[] DatabaseFolderSubdirectories
        {
            get { return _databaseFolderSubdirectories; }
        }

        /// <summary>
        /// Copy a file only if it does not already exit.
        /// Never overwrite any existing files.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        /// <returns>true if the file was copied or if a file with this name already exists</returns>
        public static bool CopyFileIfNotExists(string src, string dst)
        {
            bool success = true;

            try
            {
                if (!System.IO.File.Exists(dst))
                {
                    success = CopyFile(src, dst);
                }
            }
            catch
            {
                success = false;
            }

            return success;
        }

        /// <summary>
        /// Copy a file trying as hard as you can with user interaction.
        /// If copying doesn't work, pop up a AbortRetryIgnore message box.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        /// <returns></returns>
        public static bool CopyFile(string src, string dst)
        {
            bool success = true;

            bool queryUserOnError = true;

        Retry:

            if (!System.IO.File.Exists(src))
            {
                success = false;
                MessageBox.Show(string.Format("Die Datei {0} fehlt.", src), ProgramName);
                goto _exit;
            }

            try
            {
                System.IO.File.Copy(src, dst, true);
            }
            catch
            {
                try
                {
                    // Kopieren hat nicht geklappt, evtl. ist die Datei schreibgeschützt, 
                    // also den Schreibschutz entfernen.
                    System.IO.File.SetAttributes(dst, FileAttributes.Normal);
                    System.IO.File.Copy(src, dst, true);
                }
                catch
                {
                    DialogResult result = DialogResult.Abort;

                    if (queryUserOnError)
                    {
                        result = MessageBox.Show(string.Format("Die Datei \r\r'{0}'\r\rkann nicht nach \r\r'{1}'\r\rkopiert werden."
                            + "\r\rFalls das Programm '" + ProgramName + "' gestartet ist, müssen Sie es beenden und das Kopieren der"
                            + " Datei noch einmal versuchen."
                            , src, dst),
                            ProgramName, MessageBoxButtons.AbortRetryIgnore);
                    }

                    if (result == DialogResult.Retry)
                    {
                        goto Retry;
                    }
                    else if (result == DialogResult.Abort)
                    {
                        success = false;
                        goto _exit;
                    }
                    else
                    {
                        // Ignore: Datei konnte nicht kopiert werden, ist aber egal.
                        // Wenn mehrere Benutzer dieselbe Datenbank benutzen, und jemand den 
                        // Datenimport durchführt, dann ist die Datei operationen.operationenimportcsv.dll gelockt,
                        // aber schon auf dem neuesten Stand
                    }
                }
            }

        _exit:

            return success;
        }
    }
}
