using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Operationen.Setup
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string []args)
        {
            // 1. Aus dem Control panel wird uninstall.exe wie in der Registry eingetragen 
            // ohne parameter gestartet, 
            // eben so, wie es in der registry steht.
            if (args.Length == 0)
            {
                // 2. Jetzt dieses Programm selber in ein temp-Verzeichnis kopiert,
                // 3. dort MIT Parameter gestartet und dieses hier beendet.
                string tmpFolder = "";
                string tmpFilename = "";

                if (CopySelf(ref tmpFolder, ref tmpFilename))
                {
                    ProcessStartInfo si = new ProcessStartInfo(tmpFilename, "dummy");
                    si.WorkingDirectory = tmpFolder;
                    System.Diagnostics.Process.Start(si);

                    Application.Exit();
                }
            }
            else
            {
                // 4. beim zweiten Start gibt es einen (dummy) Parameter, und jetzt werden die operat ionen.exe
                // Dateien gelöscht.
                // Bis der Benutzer hier was geklickt hat, ist das StartProgramm längst beendet.
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainView());
            }
        }
        private static bool CopySelf(ref string tmpFolder, ref string tmpFilename)
        {
            bool success = false;

            try
            {
                string src = Application.StartupPath;
                string dst = Environment.GetEnvironmentVariable("temp");

                if (dst == null || dst.Length == 0)
                {
                    dst = Environment.GetEnvironmentVariable("tmp");
                }
                if (dst == null || dst.Length == 0)
                {
                    goto exit;
                }
                if (!System.IO.Directory.Exists(dst))
                {
                    goto exit;
                }
                if (!System.IO.Directory.Exists(src))
                {
                    goto exit;
                }
                if (!CopySelf(src, dst, ref tmpFolder, ref tmpFilename))
                {
                    goto exit;
                }
                success = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        exit:
            return success;
        }

        public static bool CopySelf(string srcPath, string dstPath, ref string tmpFolder, ref string tmpFilename)
        {
            bool success = false;
            string[] fileNames = 
                {
                    "Uninstall.exe",
                    "Interop.IWshRuntimeLibrary.dll",
                    "SetupData.dll"
                };

            try
            {
                foreach (string fileName in fileNames)
                {
                    string src = srcPath + System.IO.Path.DirectorySeparatorChar + fileName;
                    string dst = dstPath + System.IO.Path.DirectorySeparatorChar + fileName;
                    if (!SetupData.CopyFile(src, dst))
                    {
                        goto exit;
                    }
                }

                tmpFolder = dstPath;
                tmpFilename = dstPath + System.IO.Path.DirectorySeparatorChar + "Uninstall.exe";

                success = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            exit:
            return success;
        }
    }
}