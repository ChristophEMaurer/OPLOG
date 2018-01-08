using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Diagnostics;

using AppFramework;
using AppFramework.Debugging;
using Utility;

namespace Operationen
{
    /*
    Files:
     Setup/Images/Surgeon32x32.ico: used by CreateVersionFull.cmd
    */

    static class Program
    {
        /// <summary>
        /// For this application to run from a network share, run the following command
        /// caspol -q -machine -addgroup "All_Code" -url \\server\share\* FullTrust
        /// caspol -q -machine -addgroup "All_Code" -url [mapped drive letter:\* FullTrust
        /// caspol -q -machine -addgroup 1. -url h:\* FullTrust -levelfinal on
        /// </summary>

        static ResourceManager _resourceManager = null;

        static private ResourceManager ResourceMgr
        {
            get
            {
                if (_resourceManager == null)
                {
                    _resourceManager = new ResourceManager("Operationen.OperationenStrings", typeof(OperationenStrings).Assembly);
                    _resourceManager.IgnoreCase = true;
                }
                return _resourceManager;
            }
        }

        public static string GetText(string id, string defaultText)
        {
            StringBuilder sb = new StringBuilder(_resourceManager.GetString("Program_" + id));

            if (sb.Length == 0)
            {
                sb.Append(defaultText);
            }

            sb.Replace("$r$", "\r");
            sb.Replace("$nl$", Environment.NewLine);

            return sb.ToString();
        }

        public static bool Login(BusinessLayer businessLayer)
        {
            bool success = false;


            if (businessLayer.AuthenticationMode == System.Web.Configuration.AuthenticationMode.Windows)
            {
                //
                // Es wid nur der user name verglichen. Wenn es den gibt, ist menfertig.
                // Wenn es den nicht gibt, dann wird das Programm beendet.
                // Man wird NICHT nach dem Kennwort gefragt.
                //
                if (businessLayer.LoginWindowsAuthentication())
                {
                    success = true;
                }
            }
            else
            {
                //
                // Hier muss man sein Kennwort eingeben.
                // Evtl. muss man das Kennwort ändern
                //
                LogonView dlgLogon = new LogonView(businessLayer);
                if (dlgLogon.ShowDialog() == DialogResult.OK)
                {
                    success = true;
                }
            }

            return success;
        }

        static void ProcessCommandlineArguments(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "/showDebugWindow")
                {
                    DebugLogging.ShowDebugWindow(BusinessLayer.ProgramTitle, null, true);
                }
                else if (i + 1 < args.Length && args[i] == "/debugMask")
                {
                    long mask = Convert.ToInt64(args[i + 1], 16);
                    DebugLogging.DebugMask = mask;
                    i++;
                }
                else if (args[i] == "/debugWindowPassword")
                {
                    DateTime date = DateTime.Now;

                    for (int j = 0; j < 10; j++)
                    {
                        string plainText = string.Format(CultureInfo.InvariantCulture, "{0:00}.{1:00}.{2:0000}", date.Day, date.Month, date.Year);
                        string cipherText = BusinessLayerBase.Encrypt(plainText);
                        DebugLogging.WriteLine(DebugLogging.DebugFlagError, string.Format(CultureInfo.InvariantCulture, "{0}: {1}", plainText, cipherText));
                        date = date.AddDays(1);
                    }
                    try
                    {
                        Process oProcess = null;
                        if (File.Exists(DebugLogging.FileName))
                        {
                            oProcess = Process.Start(DebugLogging.FileName);
                        }
                    }
                    catch { }
                }
            }
        }

        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                bool bSuccess = false;

                // Wegen watermark in ListView
                Windows.Win32Interop.CoInitialize(0);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                DebugLogging.SetLogFile("oplog");
                DebugLogging.AutoFlush = true;
                DebugLogging.DebugMask = DebugLogging.DebugFlagError | DebugLogging.DebugFlagWarning;
                DebugLogging.IncludeDebugFlags = false;
                DebugLogging.IncludeDebugMask = false;
                DebugLogging.IncludeThreadId = false;
                DebugLogging.IncludeTimestamp = true;

                ProcessCommandlineArguments(args);

                ResourceManager resMgr = ResourceMgr;

                string applicationStartupPath = Application.StartupPath;

                //
                // Bevor man angemeldet ist, kann man keine benutzerspezifischen Einstellungen aus der
                // Datenbank holen, daher nehmen wir, was in .exe.config steht
                //
                string uiCulture = Operationen.Default.UICulture;

                Thread.CurrentThread.CurrentUICulture = new CultureInfo(uiCulture);

                BusinessLayer businessLayer = new BusinessLayer(resMgr);

                string strServerPath = Operationen.Default.DatabasePath;
                string strConnectionString = Operationen.Default.ConnectionString;

                // Neu ab Version 11.3: Man kann in der .config die Datenbank umstellen!
                string strDatabaseType = Operationen.Default.DatabaseType;

#if DEBUG
#if SERVERINSTALLATION
                //
                // To test server installation enable this code
                //
                applicationStartupPath = @"\\cmaurer\oplog";
                strServerPath = @"\\cmaurer\oplogdaten";
#endif
#endif

                if (strDatabaseType.Length == 0)
                {
                    strDatabaseType = "MSAccess";
                }

                if (strDatabaseType.ToLower() == "msaccess")
                {
                    bSuccess = businessLayer.InitializeMSAccessDb(applicationStartupPath, strServerPath, "operationen.mdb");
                }
                else if (strDatabaseType.ToLower() == "msaccessacc")
                {
                    bSuccess = businessLayer.InitializeMSAccessDbAcc(applicationStartupPath, strServerPath, "operationen.accdb");
                }
                else if (strDatabaseType.ToLower() == "sqlserver")
                {
                    bSuccess = businessLayer.InitializeSQLServer(applicationStartupPath, strServerPath, strConnectionString);
                }
                else if (strDatabaseType.ToLower() == "mysql")
                {
                    bSuccess = businessLayer.InitializeMySql(applicationStartupPath, strServerPath, strConnectionString);
                }
                else if (strDatabaseType.ToLower() == "oraclexe")
                {
                    bSuccess = businessLayer.InitializeOracle(applicationStartupPath, strServerPath, strConnectionString);
                }
                else
                {
                    bSuccess = false;
                    string defaultMsg = "The only supported database types are MSAccess, SQLServer and MySQL."
                        + "\nThe following type is unkonwn:"
                        + "\n\n'" + strDatabaseType + "'"
                        + "\n\nThe program will terminate.";
                    string msg = string.Format(CultureInfo.InvariantCulture, GetText("error1", defaultMsg), strDatabaseType);

                    DisplayError(msg);
                }

                if (!bSuccess)
                {
                    goto exit;
                }

                if (!businessLayer.TestDatabaseConnection())
                {
                    string defaultMsg = "Could not connect to database. Program will terminate.";
                    string msg = GetText("error2", defaultMsg);
                    DisplayError(msg);
                    goto exit;
                }

                if (!businessLayer.ReadDatabaseVersion())
                {
                    string defaultMsg = "Could not retrieve database version. Program will terminate.";
                    string msg = GetText("error3", defaultMsg);
                    DisplayError(msg);
                    goto exit;
                }

                if (businessLayer.ProgramVersionLowerThanDatabase())
                {
                    //
                    // Vorher ging hier das Fenster auf und es wurde auf www-update überprüft, das hatte aber viele Fehler weil 
                    // noch keine Benutzer eangemeldet ist und man daher ständig abstürzte
                    //
                    string msg = "The program version '" + BusinessLayer.VersionString + "' is lower then the database version '" + businessLayer.DatabaseVersion
                        + "'.\nYou must update the program to match the database version.\nThe program will terminate.";
                    DisplayError(msg);
                    goto exit;
                }

                if (businessLayer.DatabaseUpdateNeeded())
                {
                    DatabaseUpdateView dlgUpdate = new DatabaseUpdateView(businessLayer);
                    dlgUpdate.ShowDialog();
                }

                if (businessLayer.DatabaseUpdateNeeded())
                {
                    businessLayer.BadVersionMessage();
                    goto exit;
                }

                if (!businessLayer.InitialDirCheck())
                {
                    goto exit;
                }

                if (!businessLayer.InitialSurgeonCheck())
                {
                    goto exit;
                }

#if CHECK_RESTRICTIONS
                if (!businessLayer.VerifyDuplicateSerialNumbers())
                {
                    goto exit;
                }

                if (!businessLayer.CheckTrialVersion())
                {
                    UpdateSerialnumbersView dlg = new UpdateSerialnumbersView(businessLayer);
                    dlg.ShowDialog();
                    goto exit;
                }

                if (!businessLayer.CheckDate())
                {
                    goto exit;
                }
#endif

                if (!Login(businessLayer))
                {
                    goto exit;
                }


                if (!businessLayer.InitialDocumentCheck())
                {
                    goto exit;
                }

                Application.Run(new OperationenLogbuchView(businessLayer));
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                Windows.Win32Interop.CoUninitialize();
            }

            exit: ;
        }

        public static void DisplayError(string text)
        {
            MessageBox.Show(text, 
                CMaurer.Operationen.AppFramework.BusinessLayerCommon.ProgramTitle);
        }
    }
}

