using System;
using System.Collections.Generic;
using System.Text;

namespace CMaurer.Operationen.AppFramework
{
    /// <summary>
    /// Global constants
    /// </summary>
    public sealed class GlobalConstants
    {
        //
        // All min and max are inclusive
        //
        /// <summary>
        /// Minimum of lines per page for printing
        /// </summary>
        public const int PrintLinesPerPageMin = 1;

        /// <summary>
        /// Max of lines per page
        /// </summary>
        public const int PrintLinesPerPageMax = 99;

        /// <summary>
        /// When searching, this is the minimum of characters from the left of the ops-code is searched for 
        /// </summary>
        public const int OtherRelevantPositionsMin = 0;
        /// <summary>
        /// When searching, this is the maximum in the text of ops-code up to which is searched
        /// </summary>
        public const int OtherRelevantPositionsMax = 7;
        /// <summary>
        /// Default for searching in the ops-code is 5 characters
        /// </summary>
        public const int OtherRelevantPositionsDefault = 5;

        /// <summary>
        /// Program section
        /// </summary>
        public const string SectionProgram = "Program";
        /// <summary>
        /// Is auto update active or not?
        /// </summary>
        public const string KeyProgramAutoUpdate = "AutoUpdate";
        /// <summary>
        /// The master program can be set up do copy a version from the internet to a local folder
        /// </summary>
        public const string KeyProgramCopyUpdateFiles = "CopyUpdateFiles";
        /// <summary>
        /// Check for updates from a local folder
        /// </summary>
        public const string KeyProgramAutoUpdateLocal = "AutoUpdateLocal";
        /// <summary>
        /// Local updates are kept in this folder
        /// </summary>
        public const string KeyProgramAutoUpdateLocalFolder = "AutoUpdateLocalFolder";
        /// <summary>
        /// The current language
        /// </summary>
        public const string KeyProgramUICulture = "UICulture";

        /// <summary>
        /// Section Proxy
        /// </summary>
        public const string SectionProxy = "Proxy";
        /// <summary>
        /// proxy modes: ValueProxyModeNone, ValueProxyModeIE, ValueProxyModeUser
        /// </summary>
        public const string KeyProxyMode = "Mode";
        /// <summary>
        /// key for proxy mode ValueProxyModeNone
        /// </summary>
        public const string ValueProxyModeNone = "none";
        /// <summary>
        /// key for proxy mode ValueProxyModeIE
        /// </summary>
        public const string ValueProxyModeIE = "ie";
        /// <summary>
        /// key for proxy mode ValueProxyModeUser
        /// </summary>
        public const string ValueProxyModeUser = "user";
        /// <summary>
        /// in proxy mode user: address
        /// </summary>
        public const string KeyProxyUserAddress = "userAddress";
        /// <summary>
        /// in proxy mode user: domain
        /// </summary>
        public const string KeyProxyUserDomain = "userDomain";
        /// <summary>
        /// in proxy mode user: user
        /// </summary>
        public const string KeyProxyUserUser = "userUser";
        /// <summary>
        /// in proxy mode user: password
        /// </summary>
        public const string KeyProxyUserPassword = "userPassword";

        //
        // SectionOpImport and SectionOperationenImportView both use all of KeyOpImport*
        //
        public const string SectionOpImport = "OPImport";
        public const string SectionOperationenImportView = "OperationenImportView";

        //
        // Perform automatic data import upon program start?
        //
        public const string KeyOpImportAutoImport = "AutoImport";

        //
        // Full path of files to read during automatic data import
        //
        public const string KeyOpImportPath = "Path";

        //
        // In addition to ID_Chirurgen, OPSCode and time, use the 'Fallzahl' to identify an operation
        //
        public const string KeyOpImportIdentifyOpByIdentifier = "IdentifyOpIdentifier";

        public const string KeyOpImportIdentifyByImportID = "IdentifyByImportID";

        public const string KeyOpImportIdentFirstName = "IdentFirstName";
        public const string KeyOpImportInsertOperation = "InsertOperation";
        public const string KeyOpImportInsertSurgeon = "InsertSurgeon";
        public const string KeyOpImportPlugin = "Plugin";

        public const string KeyOpImportFunctionOp = "KeyOpImportFunctionOp";
        public const string KeyOpImportFunctionAss1 = "KeyOpImportFunctionAss1";
        public const string KeyOpImportFunctionAss2 = "KeyOpImportFunctionAss2";
        public const string KeyOpImportFunctionAss3 = "KeyOpImportFunctionAss3";
        public const string KeyOpImportLogAll = "KeyOpImportLogAll";

        //
        //
        //
        public const string SectionSerialNumbers = "SerialNumbers";
        public const string KeySerialNumbersAutomatic = "Automatic";

        public const string SectionPrint = "Print";
        public const int KeyPrintLinesDefault = 15;
        public const string KeyPrintLinesDefaultString = "15";
        public const string KeyPrintLinesBDCWeiterbildung = "BDCWeiterbildung";
        public const string KeyPrintLinesOperationenView = "OperationenView";
        public const string KeyPrintLinesOperationenZeitenVergleichView = "OperationenZeitenVergleichView";
        public const string KeyPrintLinesPlanOperationVergleichIstView = "PlanOperationVergleichIstView";
        public const string KeyPrintLinesAkademischeAusbildungView = "AkademischeAusbildungView";
        public const string KeyPrintLinesKlinischeErgebnisseView = "KlinischeErgebnisseView";
        public const string KeyPrintLinesRichtlinienVergleichOverviewView = "RichtlinienVergleichOverviewView";
        public const string KeyPrintLinesOperationenVergleichView = "OperationenVergleichView";
        public const string KeyPrintLinesDefaultView = "DefaultView";

        public const string SectionOther = "Other";
        public const string KeyOtherDisplayOpenCommentMessage = "commentMessage";
        public const string KeyOtherRelevantPositionsOPSCode = "relevantPositionsOpsCode";
        public const string KeyOtherWatermark = "watermark";

        public const string SectionAuswertungenOpsCode = "AuswertungenOPSCode";

        public const string SectionRibbon = "Ribbon";
        public const string KeyRibbonQat = "qat";

        //
        // Weiterbildungszeitraum
        //
        public const string SectionWbzr = "Wbzr";
        public const string KeyWbzrCount = "count";

    }
}
