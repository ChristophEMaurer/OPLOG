// Translation: done

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
using System.Reflection;

using Utility;
using CMaurer.Operationen.AppFramework;

namespace Operationen
{
    public partial class AutoImportView : OperationenForm
    {
        private delegate void ProgressCallback();

        private string _path;
        private int _totalFiles;
        private int _indexFile;

        private int _countTotal;
        private int _countInserted;

        // Ist true wenn alle Dateien ohne Fehler und ohne Benutzerabbruch eingelesen werden konnten.
        private bool _success;

        public AutoImportView(BusinessLayer businessLayer, string path)
            : base(businessLayer)
        {
            _path = path;

            InitializeComponent();

            lblFilename.BorderStyle = BorderStyle.None;
            lblInfo.BorderStyle = BorderStyle.None;

            Text = GetText("title");
        }

        private bool PerformAutoImport(OperationenImport plugin, string filename, bool identFirstName, bool insertSurgeon, bool insertOperation, bool identifyByImportID, bool idenfifyOpIdentifier)
        {
            bool success = true;

            lblFilename.Text = string.Format(GetText("progress"), _indexFile, _totalFiles, filename);
            lblInfo.Text = "";

            OperationenImporter importer = new OperationenImporter(BusinessLayer);
            importer.ImportProgress += new OperationenImporter.ImportProgressHandler(ImportProgress);

            OperationenImportPluginCustomData customData = new OperationenImportPluginCustomData();
            customData.DataSource = filename;

            success = importer.Import(
                plugin,
                customData,
                identFirstName,
                insertSurgeon,
                insertOperation,
                identifyByImportID,
                idenfifyOpIdentifier,
                null,
                null,
                false
                );

            return success;
        }

        /// <summary>
        /// Create the plugin that can be used for an unattended data import.
        /// </summary>
        /// <param name="plugin">The name of the DLL with no path.</param>
        private OperationenImport CreatePlugin(string pluginName)
        {
            OperationenImport plugin = null;

            try
            {
                string strAssemblyFilename = BusinessLayer.PathPlugins + Path.DirectorySeparatorChar + pluginName;

                Assembly assembly = Assembly.LoadFile(strAssemblyFilename);

                Type[] types = assembly.GetTypes();

                foreach (Type t in types)
                {
                    if (BusinessLayer.IsValidPlugin(t))
                    {
                        plugin = (OperationenImport)Activator.CreateInstance(t);

                        //
                        // PluginIdSqlCcopm pops up a filter dialog
                        // and cannot be used for auto import.
                        // It should not be selectable in the first place, but you never know...
                        //
                        if (plugin.PluginId != OperationenImport.OpLogPluginId.PluginIdSqlCcopm)
                        {
                            // Use the first type that fits
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox(string.Format("{0}:\n\n{1}", GetText("err_load_plugin"), ex.Message));
            }

            return plugin;
        }

        private void AutoImportView_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();

            OperationenImport plugin = null;

            if (Directory.Exists(_path))
            {
                try
                {
                    Directory.CreateDirectory(_path + Path.DirectorySeparatorChar + BusinessLayer.AutoImportProcessedDirectory);
                }
                catch
                {
                    // Wenn es das Verzeichniss schon gibt, macht das nichts
                }

                string[] files = Directory.GetFiles(_path, "*.csv");

                _totalFiles = files.Length;

                if (_totalFiles > 0)
                {
                    _indexFile = 0;
                    Abort = false;

                    bool identifyByImportID = "1" == BusinessLayer.GetUserSettingsString(GlobalConstants.SectionOpImport, GlobalConstants.KeyOpImportIdentifyByImportID);
                    bool identFirstName = "1" == BusinessLayer.GetUserSettingsString(GlobalConstants.SectionOpImport, GlobalConstants.KeyOpImportIdentFirstName);
                    bool insertSurgeon = "1" == BusinessLayer.GetUserSettingsString(GlobalConstants.SectionOpImport, GlobalConstants.KeyOpImportInsertSurgeon);
                    bool insertOperation = "1" == BusinessLayer.GetUserSettingsString(GlobalConstants.SectionOpImport, GlobalConstants.KeyOpImportInsertOperation);
                    string pluginName = BusinessLayer.GetUserSettingsString(GlobalConstants.SectionOpImport, GlobalConstants.KeyOpImportPlugin);
                    bool identifyOpByIdentifier = "1" == BusinessLayer.GetUserSettingsString(GlobalConstants.SectionOpImport, GlobalConstants.KeyOpImportIdentifyOpByIdentifier);
                    
                    try
                    {
                        plugin = CreatePlugin(pluginName);
                        if (plugin == null)
                        {
                            goto exit;
                        }

                        _success = true;


                        foreach (string file in files)
                        {
                            Application.DoEvents();

                            _indexFile++;
                            if (PerformAutoImport(plugin, file, identFirstName, insertSurgeon, insertOperation, identifyByImportID, identifyOpByIdentifier))
                            {
                                try
                                {
                                    FileInfo fi = new FileInfo(file);

                                    DateTime dtNow = DateTime.Now;
                                    string timeStamp = string.Format("{0:0000}-{1:00}-{2:00}", dtNow.Year, dtNow.Month, dtNow.Day)
                                        + "-" + string.Format("{0:00}-{1:00}-{2:00}-{3:000}", dtNow.Hour, dtNow.Minute, dtNow.Second, dtNow.Millisecond);

                                    string movedFileName = _path + Path.DirectorySeparatorChar + BusinessLayer.AutoImportProcessedDirectory + Path.DirectorySeparatorChar + fi.Name + "." + timeStamp;

                                    File.Move(file, movedFileName);
                                }
                                catch
                                {
                                }
                            }
                            else
                            {
                                _success = false;
                                break;
                            }
                        }
                    }
                    finally
                    {
                        if (plugin != null)
                        {
                            plugin = null;
                        }
                    }
                }
            }

        exit:

            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Abort = true;
        }

        protected override void ProgressBegin()
        {
            progressBar.Visible = true;
            Tools.SetProgressBarStyleMarqueeOnSupportedPlatforms(progressBar);
        }

        protected override void ProgressEnd()
        {
            progressBar.Value = 0;
            progressBar.Visible = false;
        }

        private void ProgressInc()
        {
            if (this.progressBar.InvokeRequired)
            {
                AutoImportView.ProgressCallback d = new AutoImportView.ProgressCallback(ProgressInc);
                this.Invoke(d);
            }
            else
            {
                int value = progressBar.Value + 1;

                if (value >= progressBar.Maximum)
                {
                    value = 0;
                }
                progressBar.Value = value;
            }
        }

        void ImportProgress(object sender, OperationenImporter.ImportProgressEvent e)
        {
            switch (e.State)
            {
                case OperationenImporter.ImportProgressState.ProgressBegin:
                    ProgressBegin();
                    break;

                case OperationenImporter.ImportProgressState.ProgressEnd:
                    ProgressEnd();
                    break;

                case OperationenImporter.ImportProgressState.Progress:
                    ProgressInc();
                    break;

                case OperationenImporter.ImportProgressState.CountTotal:
                    _countTotal = e.IntData;
                    lblInfo.Text = string.Format(GetText("progress2"), _countTotal, _countInserted);
                    ProgressInc();
                    break;

                case OperationenImporter.ImportProgressState.CountNew:
                    _countInserted = e.IntData;
                    lblInfo.Text = string.Format(GetText("progress2"), _countTotal, _countInserted);
                    break;
            }

            if (Abort)
            {
                e.Abort = true;
            }

            Application.DoEvents();
        }
        public bool Success
        {
            get { return _success; }
        }

        private void AutoImportView_Load(object sender, EventArgs e)
        {

        }
    }
}

