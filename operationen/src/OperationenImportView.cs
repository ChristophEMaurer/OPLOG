using System;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Collections;

using Utility;
using Operationen;
using Windows.Forms;
using CMaurer.Operationen.AppFramework;

namespace Operationen
{
	/// <summary>
    /// Zusammenfassung für OperationenImportView
	/// </summary>
    public class OperationenImportView : OperationenForm
	{
        private OperationenImporter _importer;

        private DataRow _importSingle;

        private Windows.Forms.OplButton cmdImport;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblInfo;
        private OplButton cmdOK;
        private Label lblInserted;
        private OplButton cmdAbort;
        private OplCheckBox chkInsertUnknownSurgeon;
        private Label lblProgressRead;
        private OplListView lvPlugins;
        private OplButton cmdInfo;
        private Label lblProgressInserted;
        private Label lblTotal;
        private OplCheckBox chkInsertUnknownOperation;
        private GroupBox grpIdentityChirurg;
        private OplCheckBox chkIdentLastName;
        private OplCheckBox chkIdentFirstName;
        private Label lblInfoOperateur;
        private OplRadioButton radIdentifyName;
        private OplRadioButton radIdentifyImportID;
        private Timer timerProgress;
        private GroupBox grpPlugins;
        private GroupBox grpSettings;
        private GroupBox grpProgress;
        private GroupBox grpInsert;
        private OplButton cmdImportSingle;
        private OplCheckBox chkImportSingle;
        private Label lblImportSingle;
        private Label lblImportSingleInfo;
        private GroupBox grpIdentityOperation;
        private OplCheckBox chkIdentifyOpFallzahl;
        private OplCheckBox chkIdentifyOpDateCode;
        private Label lblIdentifyImportIDInfo;
        private GroupBox grpFunktionen;
        private OplCheckBox chkFunctionAss3;
        private OplCheckBox chkFunctionAss2;
        private OplCheckBox chkFunctionAss1;
        private OplCheckBox chkFunctionOp;
        private OplCheckBox chkLogAll;
        private OplButton cmdSave;
        private Label lblImportSingleInfo2;
        private System.ComponentModel.IContainer components;

        public OperationenImportView(BusinessLayer businessLayer)
            : base(businessLayer)
		{
            _importer = new OperationenImporter(businessLayer);
            _importer.ImportProgress += new OperationenImporter.ImportProgressHandler(ImportProgress);

			//
			// Erforderlich für die Windows Form-Designerunterstützung
			//
			InitializeComponent();

            cmdSave.SetSecurity(businessLayer, "OperationenImportView.cmdSave");

            _bIgnoreControlEvents = true;

            // Nachname muss immer übereinstimmen
            chkIdentLastName.Checked = true;
            chkIdentLastName.Enabled = false;
            chkImportSingle.Checked = false;

            ReadSettings();

            progressBar.Visible = false;
            timerProgress.Tick += new EventHandler(timerProgress_Tick);
            timerProgress.Interval = 1000;
            timerProgress.Stop();

            Text = AppTitle(GetText("title"));

            _bIgnoreControlEvents = false;

            IdentificationChanged();
        }

        void timerProgress_Tick(object sender, EventArgs e)
        {
            ProgressInc();
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
                    //ProgressInc();
                    break;

                case OperationenImporter.ImportProgressState.CountTotal:
                    lblTotal.Text = e.IntData.ToString();
                    //ProgressInc();
                    break;

                case OperationenImporter.ImportProgressState.CountNew:
                    lblInserted.Text = e.IntData.ToString();
                    break;
            }

            if (Abort)
            {
                e.Abort = true;
            }

            Application.DoEvents();
        }

		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OperationenImportView));
            this.cmdImport = new Windows.Forms.OplButton();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblInfo = new System.Windows.Forms.Label();
            this.cmdOK = new Windows.Forms.OplButton();
            this.chkInsertUnknownOperation = new Windows.Forms.OplCheckBox();
            this.chkInsertUnknownSurgeon = new Windows.Forms.OplCheckBox();
            this.lblInserted = new System.Windows.Forms.Label();
            this.cmdAbort = new Windows.Forms.OplButton();
            this.lblProgressRead = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblProgressInserted = new System.Windows.Forms.Label();
            this.cmdInfo = new Windows.Forms.OplButton();
            this.lvPlugins = new Windows.Forms.OplListView();
            this.chkIdentLastName = new Windows.Forms.OplCheckBox();
            this.grpIdentityChirurg = new System.Windows.Forms.GroupBox();
            this.lblImportSingleInfo2 = new System.Windows.Forms.Label();
            this.lblIdentifyImportIDInfo = new System.Windows.Forms.Label();
            this.lblImportSingleInfo = new System.Windows.Forms.Label();
            this.lblImportSingle = new System.Windows.Forms.Label();
            this.cmdImportSingle = new Windows.Forms.OplButton();
            this.chkImportSingle = new Windows.Forms.OplCheckBox();
            this.radIdentifyName = new Windows.Forms.OplRadioButton();
            this.radIdentifyImportID = new Windows.Forms.OplRadioButton();
            this.chkIdentFirstName = new Windows.Forms.OplCheckBox();
            this.lblInfoOperateur = new System.Windows.Forms.Label();
            this.timerProgress = new System.Windows.Forms.Timer(this.components);
            this.grpPlugins = new System.Windows.Forms.GroupBox();
            this.grpSettings = new System.Windows.Forms.GroupBox();
            this.chkLogAll = new Windows.Forms.OplCheckBox();
            this.grpFunktionen = new System.Windows.Forms.GroupBox();
            this.chkFunctionAss3 = new Windows.Forms.OplCheckBox();
            this.chkFunctionAss2 = new Windows.Forms.OplCheckBox();
            this.chkFunctionAss1 = new Windows.Forms.OplCheckBox();
            this.chkFunctionOp = new Windows.Forms.OplCheckBox();
            this.grpIdentityOperation = new System.Windows.Forms.GroupBox();
            this.chkIdentifyOpFallzahl = new Windows.Forms.OplCheckBox();
            this.chkIdentifyOpDateCode = new Windows.Forms.OplCheckBox();
            this.grpInsert = new System.Windows.Forms.GroupBox();
            this.grpProgress = new System.Windows.Forms.GroupBox();
            this.cmdSave = new Windows.Forms.OplButton();
            this.grpIdentityChirurg.SuspendLayout();
            this.grpPlugins.SuspendLayout();
            this.grpSettings.SuspendLayout();
            this.grpFunktionen.SuspendLayout();
            this.grpIdentityOperation.SuspendLayout();
            this.grpInsert.SuspendLayout();
            this.grpProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdImport
            // 
            resources.ApplyResources(this.cmdImport, "cmdImport");
            this.cmdImport.Name = "cmdImport";
            this.cmdImport.SecurityManager = null;
            this.cmdImport.UserRight = null;
            this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.MarqueeAnimationSpeed = 50;
            this.progressBar.Maximum = 500;
            this.progressBar.Name = "progressBar";
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BackColor = System.Drawing.SystemColors.Control;
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.SecurityManager = null;
            this.cmdOK.UserRight = null;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // chkInsertUnknownOperation
            // 
            resources.ApplyResources(this.chkInsertUnknownOperation, "chkInsertUnknownOperation");
            this.chkInsertUnknownOperation.Name = "chkInsertUnknownOperation";
            this.chkInsertUnknownOperation.UseVisualStyleBackColor = true;
            // 
            // chkInsertUnknownSurgeon
            // 
            resources.ApplyResources(this.chkInsertUnknownSurgeon, "chkInsertUnknownSurgeon");
            this.chkInsertUnknownSurgeon.Checked = true;
            this.chkInsertUnknownSurgeon.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInsertUnknownSurgeon.Name = "chkInsertUnknownSurgeon";
            this.chkInsertUnknownSurgeon.UseVisualStyleBackColor = true;
            // 
            // lblInserted
            // 
            this.lblInserted.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.lblInserted, "lblInserted");
            this.lblInserted.Name = "lblInserted";
            // 
            // cmdAbort
            // 
            resources.ApplyResources(this.cmdAbort, "cmdAbort");
            this.cmdAbort.Name = "cmdAbort";
            this.cmdAbort.SecurityManager = null;
            this.cmdAbort.UserRight = null;
            this.cmdAbort.Click += new System.EventHandler(this.cmdAbort_Click);
            // 
            // lblProgressRead
            // 
            resources.ApplyResources(this.lblProgressRead, "lblProgressRead");
            this.lblProgressRead.Name = "lblProgressRead";
            // 
            // lblTotal
            // 
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.lblTotal, "lblTotal");
            this.lblTotal.Name = "lblTotal";
            // 
            // lblProgressInserted
            // 
            resources.ApplyResources(this.lblProgressInserted, "lblProgressInserted");
            this.lblProgressInserted.Name = "lblProgressInserted";
            // 
            // cmdInfo
            // 
            resources.ApplyResources(this.cmdInfo, "cmdInfo");
            this.cmdInfo.Name = "cmdInfo";
            this.cmdInfo.SecurityManager = null;
            this.cmdInfo.UserRight = null;
            this.cmdInfo.Click += new System.EventHandler(this.cmdInfo_Click);
            // 
            // lvPlugins
            // 
            resources.ApplyResources(this.lvPlugins, "lvPlugins");
            this.lvPlugins.DoubleClickActivation = false;
            this.lvPlugins.Name = "lvPlugins";
            this.lvPlugins.UseCompatibleStateImageBehavior = false;
            this.lvPlugins.SelectedIndexChanged += new System.EventHandler(this.lvPlugins_SelectedIndexChanged);
            this.lvPlugins.DoubleClick += new System.EventHandler(this.lvPlugins_DoubleClick);
            // 
            // chkIdentLastName
            // 
            resources.ApplyResources(this.chkIdentLastName, "chkIdentLastName");
            this.chkIdentLastName.Name = "chkIdentLastName";
            this.chkIdentLastName.UseVisualStyleBackColor = true;
            // 
            // grpIdentityChirurg
            // 
            this.grpIdentityChirurg.Controls.Add(this.lblImportSingleInfo2);
            this.grpIdentityChirurg.Controls.Add(this.lblIdentifyImportIDInfo);
            this.grpIdentityChirurg.Controls.Add(this.lblImportSingleInfo);
            this.grpIdentityChirurg.Controls.Add(this.lblImportSingle);
            this.grpIdentityChirurg.Controls.Add(this.cmdImportSingle);
            this.grpIdentityChirurg.Controls.Add(this.chkImportSingle);
            this.grpIdentityChirurg.Controls.Add(this.radIdentifyName);
            this.grpIdentityChirurg.Controls.Add(this.radIdentifyImportID);
            this.grpIdentityChirurg.Controls.Add(this.chkIdentFirstName);
            this.grpIdentityChirurg.Controls.Add(this.chkIdentLastName);
            resources.ApplyResources(this.grpIdentityChirurg, "grpIdentityChirurg");
            this.grpIdentityChirurg.Name = "grpIdentityChirurg";
            this.grpIdentityChirurg.TabStop = false;
            // 
            // lblImportSingleInfo2
            // 
            resources.ApplyResources(this.lblImportSingleInfo2, "lblImportSingleInfo2");
            this.lblImportSingleInfo2.Name = "lblImportSingleInfo2";
            // 
            // lblIdentifyImportIDInfo
            // 
            resources.ApplyResources(this.lblIdentifyImportIDInfo, "lblIdentifyImportIDInfo");
            this.lblIdentifyImportIDInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIdentifyImportIDInfo.Name = "lblIdentifyImportIDInfo";
            // 
            // lblImportSingleInfo
            // 
            resources.ApplyResources(this.lblImportSingleInfo, "lblImportSingleInfo");
            this.lblImportSingleInfo.Name = "lblImportSingleInfo";
            // 
            // lblImportSingle
            // 
            this.lblImportSingle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.lblImportSingle, "lblImportSingle");
            this.lblImportSingle.Name = "lblImportSingle";
            // 
            // cmdImportSingle
            // 
            resources.ApplyResources(this.cmdImportSingle, "cmdImportSingle");
            this.cmdImportSingle.Name = "cmdImportSingle";
            this.cmdImportSingle.SecurityManager = null;
            this.cmdImportSingle.UserRight = null;
            this.cmdImportSingle.UseVisualStyleBackColor = true;
            this.cmdImportSingle.Click += new System.EventHandler(this.cmdImportSingle_Click);
            // 
            // chkImportSingle
            // 
            resources.ApplyResources(this.chkImportSingle, "chkImportSingle");
            this.chkImportSingle.Name = "chkImportSingle";
            this.chkImportSingle.UseVisualStyleBackColor = true;
            this.chkImportSingle.CheckedChanged += new System.EventHandler(this.chkImportSingle_CheckedChanged);
            // 
            // radIdentifyName
            // 
            resources.ApplyResources(this.radIdentifyName, "radIdentifyName");
            this.radIdentifyName.Name = "radIdentifyName";
            this.radIdentifyName.TabStop = true;
            this.radIdentifyName.UseVisualStyleBackColor = true;
            this.radIdentifyName.CheckedChanged += new System.EventHandler(this.radIdentifyName_CheckedChanged);
            // 
            // radIdentifyImportID
            // 
            resources.ApplyResources(this.radIdentifyImportID, "radIdentifyImportID");
            this.radIdentifyImportID.Name = "radIdentifyImportID";
            this.radIdentifyImportID.TabStop = true;
            this.radIdentifyImportID.UseVisualStyleBackColor = true;
            this.radIdentifyImportID.CheckedChanged += new System.EventHandler(this.radIdentifyImportID_CheckedChanged);
            // 
            // chkIdentFirstName
            // 
            resources.ApplyResources(this.chkIdentFirstName, "chkIdentFirstName");
            this.chkIdentFirstName.Name = "chkIdentFirstName";
            this.chkIdentFirstName.UseVisualStyleBackColor = true;
            this.chkIdentFirstName.CheckedChanged += new System.EventHandler(this.chkIdentFirstName_CheckedChanged);
            // 
            // lblInfoOperateur
            // 
            resources.ApplyResources(this.lblInfoOperateur, "lblInfoOperateur");
            this.lblInfoOperateur.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfoOperateur.Name = "lblInfoOperateur";
            // 
            // grpPlugins
            // 
            resources.ApplyResources(this.grpPlugins, "grpPlugins");
            this.grpPlugins.Controls.Add(this.lvPlugins);
            this.grpPlugins.Controls.Add(this.cmdInfo);
            this.grpPlugins.Name = "grpPlugins";
            this.grpPlugins.TabStop = false;
            // 
            // grpSettings
            // 
            resources.ApplyResources(this.grpSettings, "grpSettings");
            this.grpSettings.Controls.Add(this.chkLogAll);
            this.grpSettings.Controls.Add(this.grpFunktionen);
            this.grpSettings.Controls.Add(this.grpIdentityOperation);
            this.grpSettings.Controls.Add(this.grpInsert);
            this.grpSettings.Controls.Add(this.lblInfo);
            this.grpSettings.Controls.Add(this.grpIdentityChirurg);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.TabStop = false;
            // 
            // chkLogAll
            // 
            resources.ApplyResources(this.chkLogAll, "chkLogAll");
            this.chkLogAll.Name = "chkLogAll";
            this.chkLogAll.UseVisualStyleBackColor = true;
            // 
            // grpFunktionen
            // 
            this.grpFunktionen.Controls.Add(this.chkFunctionAss3);
            this.grpFunktionen.Controls.Add(this.chkFunctionAss2);
            this.grpFunktionen.Controls.Add(this.chkFunctionAss1);
            this.grpFunktionen.Controls.Add(this.chkFunctionOp);
            resources.ApplyResources(this.grpFunktionen, "grpFunktionen");
            this.grpFunktionen.Name = "grpFunktionen";
            this.grpFunktionen.TabStop = false;
            // 
            // chkFunctionAss3
            // 
            resources.ApplyResources(this.chkFunctionAss3, "chkFunctionAss3");
            this.chkFunctionAss3.Checked = true;
            this.chkFunctionAss3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFunctionAss3.Name = "chkFunctionAss3";
            this.chkFunctionAss3.UseVisualStyleBackColor = true;
            // 
            // chkFunctionAss2
            // 
            resources.ApplyResources(this.chkFunctionAss2, "chkFunctionAss2");
            this.chkFunctionAss2.Checked = true;
            this.chkFunctionAss2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFunctionAss2.Name = "chkFunctionAss2";
            this.chkFunctionAss2.UseVisualStyleBackColor = true;
            // 
            // chkFunctionAss1
            // 
            resources.ApplyResources(this.chkFunctionAss1, "chkFunctionAss1");
            this.chkFunctionAss1.Checked = true;
            this.chkFunctionAss1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFunctionAss1.Name = "chkFunctionAss1";
            this.chkFunctionAss1.UseVisualStyleBackColor = true;
            // 
            // chkFunctionOp
            // 
            resources.ApplyResources(this.chkFunctionOp, "chkFunctionOp");
            this.chkFunctionOp.Checked = true;
            this.chkFunctionOp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFunctionOp.Name = "chkFunctionOp";
            this.chkFunctionOp.UseVisualStyleBackColor = true;
            // 
            // grpIdentityOperation
            // 
            this.grpIdentityOperation.Controls.Add(this.chkIdentifyOpFallzahl);
            this.grpIdentityOperation.Controls.Add(this.chkIdentifyOpDateCode);
            resources.ApplyResources(this.grpIdentityOperation, "grpIdentityOperation");
            this.grpIdentityOperation.Name = "grpIdentityOperation";
            this.grpIdentityOperation.TabStop = false;
            // 
            // chkIdentifyOpFallzahl
            // 
            resources.ApplyResources(this.chkIdentifyOpFallzahl, "chkIdentifyOpFallzahl");
            this.chkIdentifyOpFallzahl.Name = "chkIdentifyOpFallzahl";
            this.chkIdentifyOpFallzahl.UseVisualStyleBackColor = true;
            // 
            // chkIdentifyOpDateCode
            // 
            this.chkIdentifyOpDateCode.Checked = true;
            this.chkIdentifyOpDateCode.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.chkIdentifyOpDateCode, "chkIdentifyOpDateCode");
            this.chkIdentifyOpDateCode.Name = "chkIdentifyOpDateCode";
            this.chkIdentifyOpDateCode.UseVisualStyleBackColor = true;
            // 
            // grpInsert
            // 
            resources.ApplyResources(this.grpInsert, "grpInsert");
            this.grpInsert.Controls.Add(this.chkInsertUnknownOperation);
            this.grpInsert.Controls.Add(this.chkInsertUnknownSurgeon);
            this.grpInsert.Controls.Add(this.lblInfoOperateur);
            this.grpInsert.Name = "grpInsert";
            this.grpInsert.TabStop = false;
            // 
            // grpProgress
            // 
            resources.ApplyResources(this.grpProgress, "grpProgress");
            this.grpProgress.Controls.Add(this.progressBar);
            this.grpProgress.Controls.Add(this.lblInserted);
            this.grpProgress.Controls.Add(this.lblProgressRead);
            this.grpProgress.Controls.Add(this.lblProgressInserted);
            this.grpProgress.Controls.Add(this.lblTotal);
            this.grpProgress.Name = "grpProgress";
            this.grpProgress.TabStop = false;
            // 
            // cmdSave
            // 
            resources.ApplyResources(this.cmdSave, "cmdSave");
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.SecurityManager = null;
            this.cmdSave.UserRight = null;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // OperationenImportView
            // 
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.cmdOK;
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.grpProgress);
            this.Controls.Add(this.grpSettings);
            this.Controls.Add(this.grpPlugins);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdAbort);
            this.Controls.Add(this.cmdImport);
            this.Name = "OperationenImportView";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.OperationenImportView_Load);
            this.grpIdentityChirurg.ResumeLayout(false);
            this.grpIdentityChirurg.PerformLayout();
            this.grpPlugins.ResumeLayout(false);
            this.grpSettings.ResumeLayout(false);
            this.grpSettings.PerformLayout();
            this.grpFunktionen.ResumeLayout(false);
            this.grpFunktionen.PerformLayout();
            this.grpIdentityOperation.ResumeLayout(false);
            this.grpIdentityOperation.PerformLayout();
            this.grpInsert.ResumeLayout(false);
            this.grpProgress.ResumeLayout(false);
            this.grpProgress.PerformLayout();
            this.ResumeLayout(false);

        }
		#endregion


        private List<OP_FUNCTION> GetFunktionen()
        {
            List<OP_FUNCTION> funktionen = new List<OP_FUNCTION>();

            if (chkFunctionOp.Checked)
            {
                funktionen.Add(OP_FUNCTION.OP_FUNCTION_OP);
            }
            if (chkFunctionAss1.Checked)
            {
                funktionen.Add(OP_FUNCTION.OP_FUNCTION_ASS);
            }
            if (chkFunctionAss2.Checked)
            {
                funktionen.Add(OP_FUNCTION.OP_FUNCTION_ASS2);
            }
            if (chkFunctionAss3.Checked)
            {
                funktionen.Add(OP_FUNCTION.OP_FUNCTION_ASS3);
            }

            //
            // If all is selected, then this means we do not filter, so return null
            //
            if (funktionen.Count > 3)
            {
                funktionen = null;
            }

            return funktionen;
        }

        private void DoOperationenImport(string strAssemblyFilename)
        {
            try
            {
                _importer.Protokoll(GetText("ladePlugin") + "'" + strAssemblyFilename + "'...");
                _importer.DumpSettings(this);

                Assembly assembly = Assembly.LoadFile(strAssemblyFilename);

                Type[] types = assembly.GetTypes();

                foreach (Type t in types)
                {
                    if (BusinessLayer.IsValidPlugin(t))
                    {
                        List<OP_FUNCTION> funktionen = GetFunktionen();

                        OperationenImport plugin = (OperationenImport)Activator.CreateInstance(t);
                        _importer.Protokoll("..." + GetText("pluginGeladen") + " '" + strAssemblyFilename + "'.");

                        if (plugin.PluginId == OperationenImport.OpLogPluginId.PluginIdSqlCcopm)
                        {
                            //
                            // PluginIdSqlCcopm displays the last OP in the system to the user.
                            // This information is passed in Op erationenImportEx.Data
                            //

                            DataRow rowDatum = BusinessLayer.GetChirurgenOperationenLast();
                            string value = Tools.DBNullableDateTime2DateString(rowDatum["Datum"]);

                            Hashtable hashtable = new Hashtable();
                            hashtable["opLast"] = value;
                            plugin.Data = hashtable;
                        }

                        _importer.Import(
                            plugin,
                            null,
                            chkIdentFirstName.Checked,
                            chkInsertUnknownSurgeon.Checked,
                            chkInsertUnknownOperation.Checked,
                            radIdentifyImportID.Checked,
                            chkIdentifyOpFallzahl.Checked,
                            _importSingle,
                            funktionen,
                            chkLogAll.Checked
                            );

                        // Nur die erste Klasse verwenden
                        break;
                    }
                }
            }
            catch (TargetInvocationException)
            {
                //
                // Wenn man von Plugins versucht von '\\cmaurer\oplog\plugins' zu laden und hatte addTrust.bat nicht ausgeführt
                //
                MessageBox(GetText("PluginInfoView", "TargetInvocationException"));
            }
            catch (Exception ex)
            {
                MessageBox(GetText("errImport") + ex.Message);
            }
        }

        protected override void ProgressBegin()
        {
            progressBar.Visible = true;
            Tools.SetProgressBarStyleMarqueeOnSupportedPlatforms(progressBar);

            timerProgress.Enabled = true;
            timerProgress.Start();
        }

        protected override void ProgressEnd()
        {
            progressBar.Value = 0;
            progressBar.Visible = false;

            timerProgress.Stop();
            timerProgress.Enabled = false;
        }

        private void ProgressInc()
        {
            progressBar.Value = progressBar.Value + 1;
            if (progressBar.Value >= progressBar.Maximum)
            {
                progressBar.Value = 0;
            }
        }

        private bool CheckImportSingle()
        {
            bool success = true;

            if (chkImportSingle.Checked && (_importSingle == null))
            {
                success = false;
                MessageBox(string.Format(CultureInfo.InvariantCulture, GetText("importSingleMissing"), chkImportSingle.Text));
            }

            return success;
        }

        private void cmdImport_Click(object sender, System.EventArgs e)
        {
            string strPlugin = this.GetFirstSelectedTagString(lvPlugins, true, GetText("plugin"));

            if (CheckImportSingle() && (strPlugin != null) && (strPlugin.Length > 0))
            {
                Abort = false;
                cmdImport.Enabled = false;
                cmdOK.Enabled = false;
                cmdInfo.Enabled = false;
                cmdImportSingle.Enabled = false;
                cmdSave.Enabled = false;
                chkImportSingle.Enabled = false;
                lvPlugins.Enabled = false;
                chkFunctionOp.Enabled = chkFunctionAss1.Enabled = chkFunctionAss2.Enabled = chkFunctionAss3.Enabled = false;

                lblTotal.Text = "0";
                lblInserted.Text = "0";

                cmdAbort.Enabled = true;

                DoOperationenImport(strPlugin);

                cmdAbort.Enabled = false;

                chkFunctionOp.Enabled = chkFunctionAss1.Enabled = chkFunctionAss2.Enabled = chkFunctionAss3.Enabled = true;
                lvPlugins.Enabled = true;
                chkImportSingle.Enabled = true;
                cmdImportSingle.Enabled = chkImportSingle.Checked;
                cmdSave.Enabled = true;
                cmdImport.Enabled = true;
                cmdOK.Enabled = true;
                cmdInfo.Enabled = true;
            }
        }

        private void InitPlugins()
        {
            lvPlugins.Clear();

            DefaultListViewProperties(lvPlugins);

            lvPlugins.Columns.Add(GetText("dateiname"), 300, HorizontalAlignment.Left);
            lvPlugins.Columns.Add(GetText("beschreibung"), -2, HorizontalAlignment.Left);
        }

        private void PopulatePlugins()
        {
            AppDomain curDomain = null;

            lvPlugins.BeginUpdate();
            lvPlugins.Items.Clear();

            string strPluginPath = BusinessLayer.PathPlugins;

            if (Directory.Exists(strPluginPath))
            {
                DirectoryInfo dir = new DirectoryInfo(strPluginPath);
                FileInfo[] files = dir.GetFiles("*.dll");

                try
                {
                    curDomain = AppDomain.CurrentDomain;
                    curDomain.ReflectionOnlyAssemblyResolve += new ResolveEventHandler(curDomain_ReflectionOnlyAssemblyResolve);

                    foreach (FileInfo file in files)
                    {
                        Assembly asm = null;
                        Type[] types = null;

                        try
                        {
                            asm = Assembly.ReflectionOnlyLoadFrom(file.FullName);
                            types = asm.GetTypes();

                            foreach (Type t in types)
                            {
                                if (BusinessLayer.IsValidPlugin(t))
                                {
                                    string strAssemblyDescription = "???";

                                    IList<CustomAttributeData> attributes = CustomAttributeData.GetCustomAttributes(asm);

                                    foreach (CustomAttributeData cad in attributes)
                                    {
                                        if (cad.Constructor.ReflectedType == typeof(AssemblyDescriptionAttribute))
                                        {
                                            strAssemblyDescription = cad.ConstructorArguments[0].Value.ToString();
                                            break;
                                        }
                                    }

                                    ListViewItem lvi = new ListViewItem(file.Name);

                                    //
                                    // if liv.Tag is null, then not valid entry
                                    //
                                    lvi.Tag = file.FullName;

                                    lvi.SubItems.Add(strAssemblyDescription);

                                    lvPlugins.Items.Add(lvi);
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
                }
                finally
                {
                    if (curDomain != null)
                    {
                        curDomain.ReflectionOnlyAssemblyResolve -= new ResolveEventHandler(curDomain_ReflectionOnlyAssemblyResolve);
                    }
                }
            }
            if (lvPlugins.Items.Count > 0)
            {
                lvPlugins.SelectedIndices.Add(0);
            }
            else
            {
                ListViewItem lvi = new ListViewItem(GetText("lvNoData"));

                //
                // if liv.Tag is null, then not valid entry
                //
                lvi.Tag = null;
                lvi.SubItems.Add("");
                lvPlugins.Items.Add(lvi);
            }
            lvPlugins.EndUpdate();
            }

        private void OperationenImportView_Load(object sender, EventArgs e)
        {
            Text = AppTitle(GetText("title"));

            string text = string.Format(CultureInfo.InvariantCulture, GetText("info1"), cmdImport.Text);
            SetInfoText(lblInfo, text);

            text = string.Format(CultureInfo.InvariantCulture, GetText("info2"), Command_ImportChirurgenExcludeView);
            SetInfoText(lblInfoOperateur, text);

            SetInfoText(lblIdentifyImportIDInfo, string.Format(CultureInfo.InvariantCulture, GetText("info_importid"), chkInsertUnknownSurgeon.Text));

            InitPlugins();

            PopulatePlugins();
        }

        private void cmdAbort_Click(object sender, EventArgs e)
        {
            Abort = true;
        }

        private void ShowPluginDescription()
        {
            string strPlugin = this.GetFirstSelectedTagString(lvPlugins, true, GetText("plugin"));

            if (strPlugin != null && strPlugin.Length > 0)
            {
                PluginInfoView dlg = new PluginInfoView(BusinessLayer, strPlugin);
                dlg.ShowDialog();
            }
        }

        private void cmdInfo_Click(object sender, EventArgs e)
        {
            ShowPluginDescription();
        }

        private void radIdentifyImportID_CheckedChanged(object sender, EventArgs e)
        {
            IdentificationChanged();
        }

        private void radIdentifyName_CheckedChanged(object sender, EventArgs e)
        {
            IdentificationChanged();
        }

        private void cmdImportSingle_Click(object sender, EventArgs e)
        {
            _importSingle = SelectChirurgDataRow();
            if (_importSingle != null)
            {
                string displayName = string.Format(CultureInfo.InvariantCulture, "{0}, {1} [{2}]",
                    (string)_importSingle["Nachname"],
                    (string)_importSingle["Vorname"],
                    (string)_importSingle["ImportId"]);

                lblImportSingle.Text = displayName;
            }
        }

        private void IdentificationChanged()
        {
            if (!_bIgnoreControlEvents)
            {

                // Wenn man einen Chirurgen nach ImportId identifizert, kann man ihn nicht anhand es Vornamens identifizieren und umgekehrt
                chkIdentFirstName.Enabled = !radIdentifyImportID.Checked;

                if (radIdentifyImportID.Checked)
                {
                    //
                    // Wenn man einen Chirurgen nach ImportId identifizert, kann man ihn nicht einfügen, 
                    // weil es ihn schon geben muss und man keinen Namen hat, unter dem man ihn anlegen könnte.
                    //
                    chkInsertUnknownSurgeon.Checked = false;
                    chkIdentFirstName.Checked = false;

                    lblImportSingleInfo.Text = string.Format(CultureInfo.InvariantCulture, "{0} {1}",
                            GetText("importpk_info"),
                            GetText("importpk_importid"));
                }
                else
                {
                    // radIdentifyName.Checked
                    if (chkIdentFirstName.Checked)
                    {
                        lblImportSingleInfo.Text = string.Format(CultureInfo.InvariantCulture, "{0} {1}",
                                GetText("importpk_info"),
                                GetText("importpk_lastnameFirstname"));
                    }
                    else
                    {
                        lblImportSingleInfo.Text = string.Format(CultureInfo.InvariantCulture, "{0} {1}",
                                GetText("importpk_info"),
                                GetText("importpk_lastname"));
                    }
                }
            }
        }

        private void chkImportSingleCheckedChanged()
        {
            if (!_bIgnoreControlEvents)
            {
                // Der button zur Auswahl ist enabled genau dann wenn die Checkbox enabled ist.
                cmdImportSingle.Enabled = chkImportSingle.Checked;

                if (chkImportSingle.Checked)
                {
                    // Wenn man die Daten eines bestimmten Chirurgen importiert, kann man ihn nicht einfügen, denn es muss ihn schon geben.
                    chkInsertUnknownSurgeon.Enabled = false;
                    chkInsertUnknownSurgeon.Checked = false;
                }
                else
                {
                    // Wenn man einen Bestimmten Chirurgen wieder ausschaltet, wird der Name zurückgesetzt und das dazugehörige Objekt
                    chkInsertUnknownSurgeon.Enabled = true;
                    // Ausgewählten Namen löschen
                    lblImportSingle.Text = "";
                    _importSingle = null;
                }
            }
        }

        private void chkImportSingle_CheckedChanged(object sender, EventArgs e)
        {
            chkImportSingleCheckedChanged();
        }

        private void lvPlugins_DoubleClick(object sender, EventArgs e)
        {
            ShowPluginDescription();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (UserHasRight("OperationenImportView.cmdSave"))
                {
                    XableAllButtonsForLongOperation(null, false);
                    SaveSettings();
                }
            }
            finally
            {
                XableAllButtonsForLongOperation(cmdAbort, true);
            }
        }

        private void SaveImportSetting(string key, string value)
        {
            BusinessLayer.SaveConfig(GlobalConstants.SectionOperationenImportView + "." + key, value);
        }

        private void SaveSettings()
        {
            SaveImportSetting(GlobalConstants.KeyOpImportIdentifyByImportID, radIdentifyImportID.Checked ? "1" : "0");
            SaveImportSetting(GlobalConstants.KeyOpImportIdentFirstName, chkIdentFirstName.Checked ? "1" : "0");
            SaveImportSetting(GlobalConstants.KeyOpImportIdentifyOpByIdentifier, chkIdentifyOpFallzahl.Checked ? "1" : "0");

            SaveImportSetting(GlobalConstants.KeyOpImportFunctionOp, chkFunctionOp.Checked ? "1" : "0");
            SaveImportSetting(GlobalConstants.KeyOpImportFunctionAss1, chkFunctionAss1.Checked ? "1" : "0");
            SaveImportSetting(GlobalConstants.KeyOpImportFunctionAss2, chkFunctionAss2.Checked ? "1" : "0");
            SaveImportSetting(GlobalConstants.KeyOpImportFunctionAss3, chkFunctionAss3.Checked ? "1" : "0");

            SaveImportSetting(GlobalConstants.KeyOpImportLogAll, chkLogAll.Checked ? "1" : "0");

            SaveImportSetting(GlobalConstants.KeyOpImportInsertOperation, chkInsertUnknownOperation.Checked ? "1" : "0");
            SaveImportSetting(GlobalConstants.KeyOpImportInsertSurgeon, chkInsertUnknownSurgeon.Checked ? "1" : "0");
        }

        private string GetImportSettingValue(string key)
        {
            return BusinessLayer.GetConfigValue(GlobalConstants.SectionOperationenImportView + "." + key);
        }

        private void ReadSettings()
        {
            if ("1" == GetImportSettingValue(GlobalConstants.KeyOpImportIdentifyByImportID))
            {
                radIdentifyImportID.Checked = true;
            }
            else
            {
                radIdentifyName.Checked = true;
            }

            chkIdentFirstName.Checked = "1" == GetImportSettingValue(GlobalConstants.KeyOpImportIdentFirstName);
            chkIdentifyOpFallzahl.Checked = "1" == GetImportSettingValue(GlobalConstants.KeyOpImportIdentifyOpByIdentifier);

            chkFunctionOp.Checked = "1" == GetImportSettingValue(GlobalConstants.KeyOpImportFunctionOp);
            chkFunctionAss1.Checked = "1" == GetImportSettingValue(GlobalConstants.KeyOpImportFunctionAss1);
            chkFunctionAss2.Checked = "1" == GetImportSettingValue(GlobalConstants.KeyOpImportFunctionAss2);
            chkFunctionAss3.Checked = "1" == GetImportSettingValue(GlobalConstants.KeyOpImportFunctionAss3);

            chkLogAll.Checked = "1" == GetImportSettingValue(GlobalConstants.KeyOpImportLogAll);

            chkInsertUnknownOperation.Checked = "1" == GetImportSettingValue(GlobalConstants.KeyOpImportInsertOperation);
            chkInsertUnknownSurgeon.Checked = "1" == GetImportSettingValue(GlobalConstants.KeyOpImportInsertSurgeon);
        }

        private void chkIdentFirstName_CheckedChanged(object sender, EventArgs e)
        {
            IdentificationChanged();
        }

        private void lvPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}



