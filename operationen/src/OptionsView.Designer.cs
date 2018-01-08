namespace Operationen
{
    partial class OptionsView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsView));
            this.cmdOK = new Windows.Forms.OplButton();
            this.cmdCancel = new Windows.Forms.OplButton();
            this.radProxyNone = new Windows.Forms.OplRadioButton();
            this.radProxyIE = new Windows.Forms.OplRadioButton();
            this.lblInfoAutoSerial = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.chkSerialNumbersAutomatic = new Windows.Forms.OplCheckBox();
            this.txtProxyDomain = new Windows.Forms.OplTextBox();
            this.lblProxyDomain = new System.Windows.Forms.Label();
            this.txtProxyPassword = new Windows.Forms.OplProtectedTextBox();
            this.txtProxyUser = new Windows.Forms.OplTextBox();
            this.lblProxyPassword = new System.Windows.Forms.Label();
            this.lblProxyUser = new System.Windows.Forms.Label();
            this.lblProxyAddress = new System.Windows.Forms.Label();
            this.txtProxyAddress = new Windows.Forms.OplTextBox();
            this.radProxyUser = new Windows.Forms.OplRadioButton();
            this.chkCopyUpdate = new Windows.Forms.OplCheckBox();
            this.lblAutoUpdateLocalFolder = new System.Windows.Forms.Label();
            this.cmdAutoUpdateFolder = new Windows.Forms.OplButton();
            this.txtAutoUpdateLocalFolder = new Windows.Forms.OplTextBox();
            this.lblPlugins = new System.Windows.Forms.Label();
            this.cbPlugins = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radIdentifyByName = new Windows.Forms.OplRadioButton();
            this.radIdentifyByImportID = new Windows.Forms.OplRadioButton();
            this.chkIdentFirstName = new Windows.Forms.OplCheckBox();
            this.chkIdentLastName = new Windows.Forms.OplCheckBox();
            this.chkInsertUnknownSurgeon = new Windows.Forms.OplCheckBox();
            this.chkAutoImport = new Windows.Forms.OplCheckBox();
            this.chkInsertUnknownOperation = new Windows.Forms.OplCheckBox();
            this.lblPath = new System.Windows.Forms.Label();
            this.txtPath = new Windows.Forms.OplTextBox();
            this.cmdPath = new Windows.Forms.OplButton();
            this.tabOptions = new System.Windows.Forms.TabControl();
            this.tabProxy = new System.Windows.Forms.TabPage();
            this.lblInfoProxy = new System.Windows.Forms.Label();
            this.tabUpdate = new System.Windows.Forms.TabPage();
            this.chkAutoUpdate = new Windows.Forms.OplCheckBox();
            this.chkAutoUpdateLocal = new Windows.Forms.OplCheckBox();
            this.tabImport = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkIdentifyOpFallzahl = new Windows.Forms.OplCheckBox();
            this.tabSerialnumbers = new System.Windows.Forms.TabPage();
            this.tabPrint = new System.Windows.Forms.TabPage();
            this.txtPrintDefault = new Windows.Forms.OplTextBox();
            this.lblPrintDefault = new System.Windows.Forms.Label();
            this.lblOperationenVergleichView = new System.Windows.Forms.Label();
            this.txtPrintOperationenVergleichView = new Windows.Forms.OplTextBox();
            this.lblRichtlinienVergleichOverviewView = new System.Windows.Forms.Label();
            this.txtPrintRichtlinienVergleichOverviewView = new Windows.Forms.OplTextBox();
            this.lblPrintKlinischeErgebnisseView = new System.Windows.Forms.Label();
            this.txtPrintKlinischeErgebnisseView = new Windows.Forms.OplTextBox();
            this.txtPrintAkademischeAusbildungView = new Windows.Forms.OplTextBox();
            this.lblPrintAkademischeAusbildungView = new System.Windows.Forms.Label();
            this.txtPrintPlanOperationVergleichIstView = new Windows.Forms.OplTextBox();
            this.lblPrintPlanOperationVergleichIstView = new System.Windows.Forms.Label();
            this.lblPrintOperationenZeitenVergleichView = new System.Windows.Forms.Label();
            this.txtPrintOperationenZeitenVergleichView = new Windows.Forms.OplTextBox();
            this.txtPrintOperationenView = new Windows.Forms.OplTextBox();
            this.lblPrintOperationenView = new System.Windows.Forms.Label();
            this.txtPrintBDCWeiterbildung = new Windows.Forms.OplTextBox();
            this.lblPrintBDCWeiterbildung = new System.Windows.Forms.Label();
            this.tabSonstiges = new System.Windows.Forms.TabPage();
            this.grpWatermark = new System.Windows.Forms.GroupBox();
            this.lblWatermarkInfo = new System.Windows.Forms.Label();
            this.lvWatermark = new Windows.Forms.OplListView();
            this.chkWatermark = new Windows.Forms.OplCheckBox();
            this.grpAuswertungenEinzel = new System.Windows.Forms.GroupBox();
            this.txtStellenOPSKode = new Windows.Forms.OplTextBox();
            this.lblOpscodeStellen = new System.Windows.Forms.Label();
            this.grpComment = new System.Windows.Forms.GroupBox();
            this.chkOpenCommentMsg = new Windows.Forms.OplCheckBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.groupBox4.SuspendLayout();
            this.tabOptions.SuspendLayout();
            this.tabProxy.SuspendLayout();
            this.tabUpdate.SuspendLayout();
            this.tabImport.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabSerialnumbers.SuspendLayout();
            this.tabPrint.SuspendLayout();
            this.tabSonstiges.SuspendLayout();
            this.grpWatermark.SuspendLayout();
            this.grpAuswertungenEinzel.SuspendLayout();
            this.grpComment.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.SecurityManager = null;
            this.cmdOK.UserRight = null;
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.SecurityManager = null;
            this.cmdCancel.UserRight = null;
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // radProxyNone
            // 
            resources.ApplyResources(this.radProxyNone, "radProxyNone");
            this.radProxyNone.Name = "radProxyNone";
            this.radProxyNone.TabStop = true;
            this.radProxyNone.UseVisualStyleBackColor = true;
            this.radProxyNone.CheckedChanged += new System.EventHandler(this.ProxyCheckedChanged);
            // 
            // radProxyIE
            // 
            resources.ApplyResources(this.radProxyIE, "radProxyIE");
            this.radProxyIE.Name = "radProxyIE";
            this.radProxyIE.TabStop = true;
            this.radProxyIE.UseVisualStyleBackColor = true;
            this.radProxyIE.CheckedChanged += new System.EventHandler(this.ProxyCheckedChanged);
            // 
            // lblInfoAutoSerial
            // 
            resources.ApplyResources(this.lblInfoAutoSerial, "lblInfoAutoSerial");
            this.lblInfoAutoSerial.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfoAutoSerial.Name = "lblInfoAutoSerial";
            this.lblInfoAutoSerial.TabStop = true;
            this.lblInfoAutoSerial.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblInfoAutoSerial_LinkClicked);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // chkSerialNumbersAutomatic
            // 
            resources.ApplyResources(this.chkSerialNumbersAutomatic, "chkSerialNumbersAutomatic");
            this.chkSerialNumbersAutomatic.Checked = true;
            this.chkSerialNumbersAutomatic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSerialNumbersAutomatic.Name = "chkSerialNumbersAutomatic";
            this.chkSerialNumbersAutomatic.UseVisualStyleBackColor = true;
            // 
            // txtProxyDomain
            // 
            resources.ApplyResources(this.txtProxyDomain, "txtProxyDomain");
            this.txtProxyDomain.Name = "txtProxyDomain";
            this.txtProxyDomain.ProtectContents = false;
            // 
            // lblProxyDomain
            // 
            resources.ApplyResources(this.lblProxyDomain, "lblProxyDomain");
            this.lblProxyDomain.Name = "lblProxyDomain";
            // 
            // txtProxyPassword
            // 
            resources.ApplyResources(this.txtProxyPassword, "txtProxyPassword");
            this.txtProxyPassword.Name = "txtProxyPassword";
            this.txtProxyPassword.ProtectContents = true;
            // 
            // txtProxyUser
            // 
            resources.ApplyResources(this.txtProxyUser, "txtProxyUser");
            this.txtProxyUser.Name = "txtProxyUser";
            this.txtProxyUser.ProtectContents = false;
            // 
            // lblProxyPassword
            // 
            resources.ApplyResources(this.lblProxyPassword, "lblProxyPassword");
            this.lblProxyPassword.Name = "lblProxyPassword";
            // 
            // lblProxyUser
            // 
            resources.ApplyResources(this.lblProxyUser, "lblProxyUser");
            this.lblProxyUser.Name = "lblProxyUser";
            // 
            // lblProxyAddress
            // 
            resources.ApplyResources(this.lblProxyAddress, "lblProxyAddress");
            this.lblProxyAddress.Name = "lblProxyAddress";
            // 
            // txtProxyAddress
            // 
            resources.ApplyResources(this.txtProxyAddress, "txtProxyAddress");
            this.txtProxyAddress.Name = "txtProxyAddress";
            this.txtProxyAddress.ProtectContents = false;
            // 
            // radProxyUser
            // 
            resources.ApplyResources(this.radProxyUser, "radProxyUser");
            this.radProxyUser.Name = "radProxyUser";
            this.radProxyUser.TabStop = true;
            this.radProxyUser.UseVisualStyleBackColor = true;
            this.radProxyUser.CheckedChanged += new System.EventHandler(this.ProxyCheckedChanged);
            // 
            // chkCopyUpdate
            // 
            resources.ApplyResources(this.chkCopyUpdate, "chkCopyUpdate");
            this.chkCopyUpdate.Name = "chkCopyUpdate";
            this.chkCopyUpdate.UseVisualStyleBackColor = true;
            this.chkCopyUpdate.CheckedChanged += new System.EventHandler(this.chkCopyUpdate_CheckedChanged);
            // 
            // lblAutoUpdateLocalFolder
            // 
            resources.ApplyResources(this.lblAutoUpdateLocalFolder, "lblAutoUpdateLocalFolder");
            this.lblAutoUpdateLocalFolder.Name = "lblAutoUpdateLocalFolder";
            // 
            // cmdAutoUpdateFolder
            // 
            resources.ApplyResources(this.cmdAutoUpdateFolder, "cmdAutoUpdateFolder");
            this.cmdAutoUpdateFolder.Name = "cmdAutoUpdateFolder";
            this.cmdAutoUpdateFolder.SecurityManager = null;
            this.cmdAutoUpdateFolder.UserRight = null;
            this.cmdAutoUpdateFolder.UseVisualStyleBackColor = true;
            this.cmdAutoUpdateFolder.Click += new System.EventHandler(this.cmdAutoUpdateFolder_Click);
            // 
            // txtAutoUpdateLocalFolder
            // 
            resources.ApplyResources(this.txtAutoUpdateLocalFolder, "txtAutoUpdateLocalFolder");
            this.txtAutoUpdateLocalFolder.Name = "txtAutoUpdateLocalFolder";
            this.txtAutoUpdateLocalFolder.ProtectContents = false;
            // 
            // lblPlugins
            // 
            resources.ApplyResources(this.lblPlugins, "lblPlugins");
            this.lblPlugins.Name = "lblPlugins";
            // 
            // cbPlugins
            // 
            resources.ApplyResources(this.cbPlugins, "cbPlugins");
            this.cbPlugins.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlugins.FormattingEnabled = true;
            this.cbPlugins.Name = "cbPlugins";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radIdentifyByName);
            this.groupBox4.Controls.Add(this.radIdentifyByImportID);
            this.groupBox4.Controls.Add(this.chkIdentFirstName);
            this.groupBox4.Controls.Add(this.chkIdentLastName);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // radIdentifyByName
            // 
            resources.ApplyResources(this.radIdentifyByName, "radIdentifyByName");
            this.radIdentifyByName.Name = "radIdentifyByName";
            this.radIdentifyByName.TabStop = true;
            this.radIdentifyByName.UseVisualStyleBackColor = true;
            this.radIdentifyByName.CheckedChanged += new System.EventHandler(this.radIdentifyByName_CheckedChanged);
            // 
            // radIdentifyByImportID
            // 
            resources.ApplyResources(this.radIdentifyByImportID, "radIdentifyByImportID");
            this.radIdentifyByImportID.Name = "radIdentifyByImportID";
            this.radIdentifyByImportID.TabStop = true;
            this.radIdentifyByImportID.UseVisualStyleBackColor = true;
            this.radIdentifyByImportID.CheckedChanged += new System.EventHandler(this.radIdentifyByImportID_CheckedChanged);
            // 
            // chkIdentFirstName
            // 
            resources.ApplyResources(this.chkIdentFirstName, "chkIdentFirstName");
            this.chkIdentFirstName.Name = "chkIdentFirstName";
            this.chkIdentFirstName.UseVisualStyleBackColor = true;
            // 
            // chkIdentLastName
            // 
            resources.ApplyResources(this.chkIdentLastName, "chkIdentLastName");
            this.chkIdentLastName.Checked = true;
            this.chkIdentLastName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIdentLastName.Name = "chkIdentLastName";
            this.chkIdentLastName.UseVisualStyleBackColor = true;
            // 
            // chkInsertUnknownSurgeon
            // 
            resources.ApplyResources(this.chkInsertUnknownSurgeon, "chkInsertUnknownSurgeon");
            this.chkInsertUnknownSurgeon.Checked = true;
            this.chkInsertUnknownSurgeon.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInsertUnknownSurgeon.Name = "chkInsertUnknownSurgeon";
            this.chkInsertUnknownSurgeon.UseVisualStyleBackColor = true;
            // 
            // chkAutoImport
            // 
            resources.ApplyResources(this.chkAutoImport, "chkAutoImport");
            this.chkAutoImport.Name = "chkAutoImport";
            this.chkAutoImport.UseVisualStyleBackColor = true;
            this.chkAutoImport.CheckedChanged += new System.EventHandler(this.chkAutoImport_CheckedChanged);
            // 
            // chkInsertUnknownOperation
            // 
            resources.ApplyResources(this.chkInsertUnknownOperation, "chkInsertUnknownOperation");
            this.chkInsertUnknownOperation.Checked = true;
            this.chkInsertUnknownOperation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInsertUnknownOperation.Name = "chkInsertUnknownOperation";
            this.chkInsertUnknownOperation.UseVisualStyleBackColor = true;
            // 
            // lblPath
            // 
            resources.ApplyResources(this.lblPath, "lblPath");
            this.lblPath.Name = "lblPath";
            // 
            // txtPath
            // 
            resources.ApplyResources(this.txtPath, "txtPath");
            this.txtPath.Name = "txtPath";
            this.txtPath.ProtectContents = false;
            this.txtPath.ReadOnly = true;
            // 
            // cmdPath
            // 
            resources.ApplyResources(this.cmdPath, "cmdPath");
            this.cmdPath.Name = "cmdPath";
            this.cmdPath.SecurityManager = null;
            this.cmdPath.UserRight = null;
            this.cmdPath.UseVisualStyleBackColor = true;
            this.cmdPath.Click += new System.EventHandler(this.cmdPath_Click);
            // 
            // tabOptions
            // 
            resources.ApplyResources(this.tabOptions, "tabOptions");
            this.tabOptions.Controls.Add(this.tabProxy);
            this.tabOptions.Controls.Add(this.tabUpdate);
            this.tabOptions.Controls.Add(this.tabImport);
            this.tabOptions.Controls.Add(this.tabSerialnumbers);
            this.tabOptions.Controls.Add(this.tabPrint);
            this.tabOptions.Controls.Add(this.tabSonstiges);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.SelectedIndex = 0;
            this.tabOptions.SelectedIndexChanged += new System.EventHandler(this.tabOptions_SelectedIndexChanged);
            // 
            // tabProxy
            // 
            this.tabProxy.Controls.Add(this.lblInfoProxy);
            this.tabProxy.Controls.Add(this.radProxyNone);
            this.tabProxy.Controls.Add(this.txtProxyDomain);
            this.tabProxy.Controls.Add(this.radProxyIE);
            this.tabProxy.Controls.Add(this.lblProxyDomain);
            this.tabProxy.Controls.Add(this.radProxyUser);
            this.tabProxy.Controls.Add(this.txtProxyPassword);
            this.tabProxy.Controls.Add(this.txtProxyAddress);
            this.tabProxy.Controls.Add(this.txtProxyUser);
            this.tabProxy.Controls.Add(this.lblProxyAddress);
            this.tabProxy.Controls.Add(this.lblProxyPassword);
            this.tabProxy.Controls.Add(this.lblProxyUser);
            resources.ApplyResources(this.tabProxy, "tabProxy");
            this.tabProxy.Name = "tabProxy";
            this.tabProxy.UseVisualStyleBackColor = true;
            // 
            // lblInfoProxy
            // 
            resources.ApplyResources(this.lblInfoProxy, "lblInfoProxy");
            this.lblInfoProxy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfoProxy.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblInfoProxy.Name = "lblInfoProxy";
            // 
            // tabUpdate
            // 
            this.tabUpdate.Controls.Add(this.chkAutoUpdate);
            this.tabUpdate.Controls.Add(this.chkAutoUpdateLocal);
            this.tabUpdate.Controls.Add(this.chkCopyUpdate);
            this.tabUpdate.Controls.Add(this.txtAutoUpdateLocalFolder);
            this.tabUpdate.Controls.Add(this.cmdAutoUpdateFolder);
            this.tabUpdate.Controls.Add(this.lblAutoUpdateLocalFolder);
            resources.ApplyResources(this.tabUpdate, "tabUpdate");
            this.tabUpdate.Name = "tabUpdate";
            this.tabUpdate.UseVisualStyleBackColor = true;
            // 
            // chkAutoUpdate
            // 
            resources.ApplyResources(this.chkAutoUpdate, "chkAutoUpdate");
            this.chkAutoUpdate.Name = "chkAutoUpdate";
            this.chkAutoUpdate.UseVisualStyleBackColor = true;
            this.chkAutoUpdate.CheckedChanged += new System.EventHandler(this.chkAutoUpdate_CheckedChanged);
            // 
            // chkAutoUpdateLocal
            // 
            resources.ApplyResources(this.chkAutoUpdateLocal, "chkAutoUpdateLocal");
            this.chkAutoUpdateLocal.Name = "chkAutoUpdateLocal";
            this.chkAutoUpdateLocal.UseVisualStyleBackColor = true;
            this.chkAutoUpdateLocal.CheckedChanged += new System.EventHandler(this.chkAutoUpdateLocal_CheckedChanged);
            // 
            // tabImport
            // 
            this.tabImport.Controls.Add(this.groupBox1);
            this.tabImport.Controls.Add(this.lblPlugins);
            this.tabImport.Controls.Add(this.chkAutoImport);
            this.tabImport.Controls.Add(this.cbPlugins);
            this.tabImport.Controls.Add(this.cmdPath);
            this.tabImport.Controls.Add(this.groupBox4);
            this.tabImport.Controls.Add(this.txtPath);
            this.tabImport.Controls.Add(this.chkInsertUnknownSurgeon);
            this.tabImport.Controls.Add(this.lblPath);
            this.tabImport.Controls.Add(this.chkInsertUnknownOperation);
            resources.ApplyResources(this.tabImport, "tabImport");
            this.tabImport.Name = "tabImport";
            this.tabImport.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkIdentifyOpFallzahl);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // chkIdentifyOpFallzahl
            // 
            resources.ApplyResources(this.chkIdentifyOpFallzahl, "chkIdentifyOpFallzahl");
            this.chkIdentifyOpFallzahl.Checked = true;
            this.chkIdentifyOpFallzahl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIdentifyOpFallzahl.Name = "chkIdentifyOpFallzahl";
            this.chkIdentifyOpFallzahl.UseVisualStyleBackColor = true;
            // 
            // tabSerialnumbers
            // 
            this.tabSerialnumbers.Controls.Add(this.lblInfoAutoSerial);
            this.tabSerialnumbers.Controls.Add(this.chkSerialNumbersAutomatic);
            this.tabSerialnumbers.Controls.Add(this.label1);
            resources.ApplyResources(this.tabSerialnumbers, "tabSerialnumbers");
            this.tabSerialnumbers.Name = "tabSerialnumbers";
            this.tabSerialnumbers.UseVisualStyleBackColor = true;
            // 
            // tabPrint
            // 
            this.tabPrint.Controls.Add(this.txtPrintDefault);
            this.tabPrint.Controls.Add(this.lblPrintDefault);
            this.tabPrint.Controls.Add(this.lblOperationenVergleichView);
            this.tabPrint.Controls.Add(this.txtPrintOperationenVergleichView);
            this.tabPrint.Controls.Add(this.lblRichtlinienVergleichOverviewView);
            this.tabPrint.Controls.Add(this.txtPrintRichtlinienVergleichOverviewView);
            this.tabPrint.Controls.Add(this.lblPrintKlinischeErgebnisseView);
            this.tabPrint.Controls.Add(this.txtPrintKlinischeErgebnisseView);
            this.tabPrint.Controls.Add(this.txtPrintAkademischeAusbildungView);
            this.tabPrint.Controls.Add(this.lblPrintAkademischeAusbildungView);
            this.tabPrint.Controls.Add(this.txtPrintPlanOperationVergleichIstView);
            this.tabPrint.Controls.Add(this.lblPrintPlanOperationVergleichIstView);
            this.tabPrint.Controls.Add(this.lblPrintOperationenZeitenVergleichView);
            this.tabPrint.Controls.Add(this.txtPrintOperationenZeitenVergleichView);
            this.tabPrint.Controls.Add(this.txtPrintOperationenView);
            this.tabPrint.Controls.Add(this.lblPrintOperationenView);
            this.tabPrint.Controls.Add(this.txtPrintBDCWeiterbildung);
            this.tabPrint.Controls.Add(this.lblPrintBDCWeiterbildung);
            resources.ApplyResources(this.tabPrint, "tabPrint");
            this.tabPrint.Name = "tabPrint";
            this.tabPrint.UseVisualStyleBackColor = true;
            // 
            // txtPrintDefault
            // 
            resources.ApplyResources(this.txtPrintDefault, "txtPrintDefault");
            this.txtPrintDefault.Name = "txtPrintDefault";
            this.txtPrintDefault.ProtectContents = false;
            // 
            // lblPrintDefault
            // 
            resources.ApplyResources(this.lblPrintDefault, "lblPrintDefault");
            this.lblPrintDefault.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPrintDefault.Name = "lblPrintDefault";
            // 
            // lblOperationenVergleichView
            // 
            resources.ApplyResources(this.lblOperationenVergleichView, "lblOperationenVergleichView");
            this.lblOperationenVergleichView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOperationenVergleichView.Name = "lblOperationenVergleichView";
            // 
            // txtPrintOperationenVergleichView
            // 
            resources.ApplyResources(this.txtPrintOperationenVergleichView, "txtPrintOperationenVergleichView");
            this.txtPrintOperationenVergleichView.Name = "txtPrintOperationenVergleichView";
            this.txtPrintOperationenVergleichView.ProtectContents = false;
            // 
            // lblRichtlinienVergleichOverviewView
            // 
            resources.ApplyResources(this.lblRichtlinienVergleichOverviewView, "lblRichtlinienVergleichOverviewView");
            this.lblRichtlinienVergleichOverviewView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRichtlinienVergleichOverviewView.Name = "lblRichtlinienVergleichOverviewView";
            // 
            // txtPrintRichtlinienVergleichOverviewView
            // 
            resources.ApplyResources(this.txtPrintRichtlinienVergleichOverviewView, "txtPrintRichtlinienVergleichOverviewView");
            this.txtPrintRichtlinienVergleichOverviewView.Name = "txtPrintRichtlinienVergleichOverviewView";
            this.txtPrintRichtlinienVergleichOverviewView.ProtectContents = false;
            // 
            // lblPrintKlinischeErgebnisseView
            // 
            resources.ApplyResources(this.lblPrintKlinischeErgebnisseView, "lblPrintKlinischeErgebnisseView");
            this.lblPrintKlinischeErgebnisseView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPrintKlinischeErgebnisseView.Name = "lblPrintKlinischeErgebnisseView";
            // 
            // txtPrintKlinischeErgebnisseView
            // 
            resources.ApplyResources(this.txtPrintKlinischeErgebnisseView, "txtPrintKlinischeErgebnisseView");
            this.txtPrintKlinischeErgebnisseView.Name = "txtPrintKlinischeErgebnisseView";
            this.txtPrintKlinischeErgebnisseView.ProtectContents = false;
            // 
            // txtPrintAkademischeAusbildungView
            // 
            resources.ApplyResources(this.txtPrintAkademischeAusbildungView, "txtPrintAkademischeAusbildungView");
            this.txtPrintAkademischeAusbildungView.Name = "txtPrintAkademischeAusbildungView";
            this.txtPrintAkademischeAusbildungView.ProtectContents = false;
            // 
            // lblPrintAkademischeAusbildungView
            // 
            resources.ApplyResources(this.lblPrintAkademischeAusbildungView, "lblPrintAkademischeAusbildungView");
            this.lblPrintAkademischeAusbildungView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPrintAkademischeAusbildungView.Name = "lblPrintAkademischeAusbildungView";
            // 
            // txtPrintPlanOperationVergleichIstView
            // 
            resources.ApplyResources(this.txtPrintPlanOperationVergleichIstView, "txtPrintPlanOperationVergleichIstView");
            this.txtPrintPlanOperationVergleichIstView.Name = "txtPrintPlanOperationVergleichIstView";
            this.txtPrintPlanOperationVergleichIstView.ProtectContents = false;
            // 
            // lblPrintPlanOperationVergleichIstView
            // 
            resources.ApplyResources(this.lblPrintPlanOperationVergleichIstView, "lblPrintPlanOperationVergleichIstView");
            this.lblPrintPlanOperationVergleichIstView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPrintPlanOperationVergleichIstView.Name = "lblPrintPlanOperationVergleichIstView";
            // 
            // lblPrintOperationenZeitenVergleichView
            // 
            resources.ApplyResources(this.lblPrintOperationenZeitenVergleichView, "lblPrintOperationenZeitenVergleichView");
            this.lblPrintOperationenZeitenVergleichView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPrintOperationenZeitenVergleichView.Name = "lblPrintOperationenZeitenVergleichView";
            // 
            // txtPrintOperationenZeitenVergleichView
            // 
            resources.ApplyResources(this.txtPrintOperationenZeitenVergleichView, "txtPrintOperationenZeitenVergleichView");
            this.txtPrintOperationenZeitenVergleichView.Name = "txtPrintOperationenZeitenVergleichView";
            this.txtPrintOperationenZeitenVergleichView.ProtectContents = false;
            // 
            // txtPrintOperationenView
            // 
            resources.ApplyResources(this.txtPrintOperationenView, "txtPrintOperationenView");
            this.txtPrintOperationenView.Name = "txtPrintOperationenView";
            this.txtPrintOperationenView.ProtectContents = false;
            // 
            // lblPrintOperationenView
            // 
            resources.ApplyResources(this.lblPrintOperationenView, "lblPrintOperationenView");
            this.lblPrintOperationenView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPrintOperationenView.Name = "lblPrintOperationenView";
            // 
            // txtPrintBDCWeiterbildung
            // 
            resources.ApplyResources(this.txtPrintBDCWeiterbildung, "txtPrintBDCWeiterbildung");
            this.txtPrintBDCWeiterbildung.Name = "txtPrintBDCWeiterbildung";
            this.txtPrintBDCWeiterbildung.ProtectContents = false;
            // 
            // lblPrintBDCWeiterbildung
            // 
            resources.ApplyResources(this.lblPrintBDCWeiterbildung, "lblPrintBDCWeiterbildung");
            this.lblPrintBDCWeiterbildung.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPrintBDCWeiterbildung.Name = "lblPrintBDCWeiterbildung";
            // 
            // tabSonstiges
            // 
            this.tabSonstiges.Controls.Add(this.grpWatermark);
            this.tabSonstiges.Controls.Add(this.grpAuswertungenEinzel);
            this.tabSonstiges.Controls.Add(this.grpComment);
            resources.ApplyResources(this.tabSonstiges, "tabSonstiges");
            this.tabSonstiges.Name = "tabSonstiges";
            this.tabSonstiges.UseVisualStyleBackColor = true;
            // 
            // grpWatermark
            // 
            resources.ApplyResources(this.grpWatermark, "grpWatermark");
            this.grpWatermark.Controls.Add(this.lblWatermarkInfo);
            this.grpWatermark.Controls.Add(this.lvWatermark);
            this.grpWatermark.Controls.Add(this.chkWatermark);
            this.grpWatermark.Name = "grpWatermark";
            this.grpWatermark.TabStop = false;
            // 
            // lblWatermarkInfo
            // 
            resources.ApplyResources(this.lblWatermarkInfo, "lblWatermarkInfo");
            this.lblWatermarkInfo.Name = "lblWatermarkInfo";
            // 
            // lvWatermark
            // 
            resources.ApplyResources(this.lvWatermark, "lvWatermark");
            this.lvWatermark.DoubleClickActivation = false;
            this.lvWatermark.Name = "lvWatermark";
            this.lvWatermark.UseCompatibleStateImageBehavior = false;
            // 
            // chkWatermark
            // 
            resources.ApplyResources(this.chkWatermark, "chkWatermark");
            this.chkWatermark.Name = "chkWatermark";
            this.chkWatermark.UseVisualStyleBackColor = true;
            this.chkWatermark.CheckedChanged += new System.EventHandler(this.chkWatermark_CheckedChanged);
            // 
            // grpAuswertungenEinzel
            // 
            resources.ApplyResources(this.grpAuswertungenEinzel, "grpAuswertungenEinzel");
            this.grpAuswertungenEinzel.Controls.Add(this.txtStellenOPSKode);
            this.grpAuswertungenEinzel.Controls.Add(this.lblOpscodeStellen);
            this.grpAuswertungenEinzel.Name = "grpAuswertungenEinzel";
            this.grpAuswertungenEinzel.TabStop = false;
            // 
            // txtStellenOPSKode
            // 
            resources.ApplyResources(this.txtStellenOPSKode, "txtStellenOPSKode");
            this.txtStellenOPSKode.Name = "txtStellenOPSKode";
            this.txtStellenOPSKode.ProtectContents = false;
            // 
            // lblOpscodeStellen
            // 
            resources.ApplyResources(this.lblOpscodeStellen, "lblOpscodeStellen");
            this.lblOpscodeStellen.Name = "lblOpscodeStellen";
            // 
            // grpComment
            // 
            resources.ApplyResources(this.grpComment, "grpComment");
            this.grpComment.Controls.Add(this.chkOpenCommentMsg);
            this.grpComment.Name = "grpComment";
            this.grpComment.TabStop = false;
            // 
            // chkOpenCommentMsg
            // 
            resources.ApplyResources(this.chkOpenCommentMsg, "chkOpenCommentMsg");
            this.chkOpenCommentMsg.Name = "chkOpenCommentMsg";
            this.chkOpenCommentMsg.UseVisualStyleBackColor = true;
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.TabStop = true;
            // 
            // OptionsView
            // 
            this.CancelButton = this.cmdCancel;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.tabOptions);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Name = "OptionsView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.OptionsView_Load);
            this.Shown += new System.EventHandler(this.OptionsView_Shown);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabOptions.ResumeLayout(false);
            this.tabProxy.ResumeLayout(false);
            this.tabProxy.PerformLayout();
            this.tabUpdate.ResumeLayout(false);
            this.tabUpdate.PerformLayout();
            this.tabImport.ResumeLayout(false);
            this.tabImport.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabSerialnumbers.ResumeLayout(false);
            this.tabSerialnumbers.PerformLayout();
            this.tabPrint.ResumeLayout(false);
            this.tabPrint.PerformLayout();
            this.tabSonstiges.ResumeLayout(false);
            this.grpWatermark.ResumeLayout(false);
            this.grpWatermark.PerformLayout();
            this.grpAuswertungenEinzel.ResumeLayout(false);
            this.grpAuswertungenEinzel.PerformLayout();
            this.grpComment.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Windows.Forms.OplButton cmdOK;
        private Windows.Forms.OplButton cmdCancel;
        private Windows.Forms.OplRadioButton radProxyNone;
        private Windows.Forms.OplRadioButton radProxyIE;
        private System.Windows.Forms.Label lblProxyAddress;
        private Windows.Forms.OplTextBox txtProxyAddress;
        private Windows.Forms.OplRadioButton radProxyUser;
        private Windows.Forms.OplProtectedTextBox txtProxyPassword;
        private Windows.Forms.OplTextBox txtProxyUser;
        private System.Windows.Forms.Label lblProxyPassword;
        private System.Windows.Forms.Label lblProxyUser;
        private Windows.Forms.OplTextBox txtProxyDomain;
        private System.Windows.Forms.Label lblProxyDomain;
        private System.Windows.Forms.GroupBox groupBox4;
        private Windows.Forms.OplCheckBox chkIdentFirstName;
        private Windows.Forms.OplCheckBox chkIdentLastName;
        private Windows.Forms.OplCheckBox chkAutoImport;
        private Windows.Forms.OplCheckBox chkInsertUnknownSurgeon;
        private Windows.Forms.OplCheckBox chkInsertUnknownOperation;
        private Windows.Forms.OplButton cmdPath;
        private Windows.Forms.OplTextBox txtPath;
        private System.Windows.Forms.Label lblPath;
        private Windows.Forms.OplButton cmdAutoUpdateFolder;
        private Windows.Forms.OplTextBox txtAutoUpdateLocalFolder;
        private System.Windows.Forms.Label lblAutoUpdateLocalFolder;
        private Windows.Forms.OplCheckBox chkCopyUpdate;
        private System.Windows.Forms.Label lblPlugins;
        private System.Windows.Forms.ComboBox cbPlugins;
        private Windows.Forms.OplCheckBox chkSerialNumbersAutomatic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lblInfoAutoSerial;
        private System.Windows.Forms.TabControl tabOptions;
        private System.Windows.Forms.TabPage tabProxy;
        private System.Windows.Forms.TabPage tabUpdate;
        private System.Windows.Forms.TabPage tabImport;
        private System.Windows.Forms.TabPage tabSerialnumbers;
        private Windows.Forms.OplCheckBox chkAutoUpdateLocal;
        private Windows.Forms.OplCheckBox chkAutoUpdate;
        private System.Windows.Forms.TabPage tabPrint;
        private Windows.Forms.OplTextBox txtPrintBDCWeiterbildung;
        private System.Windows.Forms.Label lblPrintBDCWeiterbildung;
        private System.Windows.Forms.TabPage tabSonstiges;
        private Windows.Forms.OplCheckBox chkOpenCommentMsg;
        private Windows.Forms.OplTextBox txtPrintOperationenView;
        private System.Windows.Forms.Label lblPrintOperationenView;
        private System.Windows.Forms.Label lblPrintOperationenZeitenVergleichView;
        private Windows.Forms.OplTextBox txtPrintOperationenZeitenVergleichView;
        private Windows.Forms.OplTextBox txtPrintPlanOperationVergleichIstView;
        private System.Windows.Forms.Label lblPrintPlanOperationVergleichIstView;
        private Windows.Forms.OplTextBox txtPrintAkademischeAusbildungView;
        private System.Windows.Forms.Label lblPrintAkademischeAusbildungView;
        private System.Windows.Forms.Label lblPrintKlinischeErgebnisseView;
        private Windows.Forms.OplTextBox txtPrintKlinischeErgebnisseView;
        private Windows.Forms.OplTextBox txtStellenOPSKode;
        private System.Windows.Forms.Label lblOpscodeStellen;
        private System.Windows.Forms.Label lblRichtlinienVergleichOverviewView;
        private Windows.Forms.OplTextBox txtPrintRichtlinienVergleichOverviewView;
        private Windows.Forms.OplRadioButton radIdentifyByName;
        private Windows.Forms.OplRadioButton radIdentifyByImportID;
        private System.Windows.Forms.Label lblOperationenVergleichView;
        private Windows.Forms.OplTextBox txtPrintOperationenVergleichView;
        private System.Windows.Forms.Label lblInfoProxy;
        private System.Windows.Forms.GroupBox grpAuswertungenEinzel;
        private System.Windows.Forms.GroupBox grpComment;
        private System.Windows.Forms.GroupBox grpWatermark;
        private Windows.Forms.OplListView lvWatermark;
        private Windows.Forms.OplCheckBox chkWatermark;
        private Windows.Forms.OplTextBox txtPrintDefault;
        private System.Windows.Forms.Label lblPrintDefault;
        private System.Windows.Forms.Label lblWatermarkInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private Windows.Forms.OplCheckBox chkIdentifyOpFallzahl;
        private System.Windows.Forms.Label lblInfo;

    }
}
