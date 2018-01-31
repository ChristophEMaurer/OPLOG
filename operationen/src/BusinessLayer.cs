using System;
using System.Net;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;
using System.Resources;
using System.Globalization;
using Microsoft.Win32;

using AppFramework;
using AppFramework.Debugging;
using Utility;
using Operationen.Wizards.Chirurg;
using Operationen.Wizards.CreateCustomerData;
using CMaurer.Operationen.AppFramework;
using Security;
using Security.Cryptography;

namespace Operationen
{
	/// <summary>
	/// Zusammenfassung für BusinessLayer.
	/// </summary>
    public partial class BusinessLayer : BusinessLayerCommon
	{
        public const int NumberOfFreeUsers = 2;

#if CHECK_RESTRICTIONS
        public const bool NumberOfFreeUsersIsValid = true;
#else
        public const bool NumberOfFreeUsersIsValid = false;
#endif

        public const string BedienungsanleitungPdf = "Bedienungsanleitung.pdf";
        public const string HelpfileChm = "operationen.chm";

        public const string LicenseFileName = "operationen.license.xml";

        // 04.02.2018 www.op-log.de changed to github
        public const string UrlWebshop = "https://github.com/ChristophEMaurer/OPLOG";
        public const string UrlHomepageForDisplay = "https://github.com/ChristophEMaurer/OPLOG/wiki";

        /// <summary>
        /// UrlHomepageForDownload is always used like this: 
        /// BusinessLayer.UrlHomepageForDownload +  "/download/[fileName]"
        ///                                         "/help/operationen_help.html");
        /// </summary>
        public const string UrlHomepageForDownload = "https://github.com/ChristophEMaurer/OPLOG/raw/master/operationen/www";
        public const string UrlHomepageNoHttpForDisplay = "github.com/ChristophEMaurer/OPLOG";

#if urologie
#if targetplatform_x86
        public const string VERSION_DOWNLOAD_FILENAME = "version-urologie-x86.txt";
#else
        public const string VERSION_DOWNLOAD_FILENAME = "version-urologie.txt";
#endif
#elif gynaekologie
#if targetplatform_x86
        public const string VERSION_DOWNLOAD_FILENAME = "version-gynaekologie-x86.txt";
#else
        public const string VERSION_DOWNLOAD_FILENAME = "version-gynaekologie.txt";
#endif
#else
#if targetplatform_x86
        public const string VERSION_DOWNLOAD_FILENAME = "version-x86.txt";
#else
        public const string VERSION_DOWNLOAD_FILENAME = "version.txt";
#endif
#endif

        public const string AutoImportProcessedDirectory = "Done";

        public const string TableAkademischeAusbildungTypen = "AkademischeAusbildungTypen";
        public const string TableKlinischeErgebnisseTypen = "KlinischeErgebnisseTypen";
        public const string TableAbteilungen = "Abteilungen";
        public const string TableSecGroups = "SecGroups";
        public const string TableNotizTypen = "NotizTypen";
        public const string TableChirurgenFunktionen = "ChirurgenFunktionen";
        public const string TextColumnChirurgenFunktionen = "Funktion";
        public const string TableDateiTypen = "DateiTypen";
        public const string TextColumnDateiTypen = "DateiTyp";

		protected DatabaseLayer _databaseLayer;
        
        DataView _importChirurgenExclude;

        public const string FileSignatureRichtlinien = "__$$OPL-Richtlinien$$__";
        public const string FileSignatureOPSKodesRichtlinien = "__$$OPL-OPSCodesRichtlinien$$__";

        public const string KlinischeErgebnisseTypenUnauffaellig = "01";

        public BusinessLayer(ResourceManager resMgr)
            : base(resMgr)
		{
            ActivateEleganzUiLicense();
            Elegant.Ui.PersistentStateManager.LoadStateAutomaticallyFromIsolatedStorageForDomain = false;

            string authenticationMode = Operationen.Default.AuthenticationMode;

            //
            // Es gibt nur diese beiden modes, und es MUSS einer gesetzt werden.
            //
            if (authenticationMode.ToLowerInvariant().Equals("windows"))
            {
                AuthenticationMode = System.Web.Configuration.AuthenticationMode.Windows;
            }
            else
            {
                AuthenticationMode = System.Web.Configuration.AuthenticationMode.Forms;
            }
        }

#region License File

        public long GetCountChirurgenWithInvalidSerialNumbers()
        {
            return DatabaseLayer.GetCountChirurgenWithInvalidSerialNumbers();
        }

        public bool UpdateChirurgenSerialNumber(DataRow chirurg)
        {
            return DatabaseLayer.UpdateChirurgenSerialNumber(chirurg);
        }

        public int ReduceSerial(string serial)
        {
            return DatabaseLayer.ReduceSerial(serial);
        }

        /// <summary>
        /// Returns true if there is no serial number which is used more than once. this excludes "Demo" and "NoneRequired"
        /// </summary>
        /// <returns>true iff each serial number is used only once</returns>
        public bool VerifyDuplicateSerialNumbers()
        {
            bool success = true;
            StringBuilder sb = new StringBuilder();
            DataView dv = DatabaseLayer.GetDuplicateSerialNumbers();

            foreach (DataRow row in dv.Table.Rows)
            {
                long number = Convert.ToInt32(row["dups"]);
                string serial = (string)row["LizenzDaten"];

                if (number > 1)
                {
                    success = false;

                    if (sb.Length > 0)
                    {
                        sb.Append(", ");
                    }
                    sb.Append(serial);

                    int recordsUpdated = ReduceSerial(serial);

                    //
                    // number must be recordsUpdated + 1
                    //
                }
            }

            if (!success)
            {
                sb.Insert(0, GetText("err_serial_multiple"));
                MessageBox(sb.ToString());
            }

            return success;
        }

        public bool CheckTrialVersion()
        {
            bool success = false;
            long countSerialsMissing = DatabaseLayer.GetCountChirurgenWithInvalidSerialNumbers();

            if (BusinessLayer.NumberOfFreeUsersIsValid && (countSerialsMissing > BusinessLayer.NumberOfFreeUsers))
            {
                Dictionary<string, string> licenseData = new Dictionary<string, string>();
                if (!VerifyLicense(licenseData, false))
                {
                    string msg = string.Format(CultureInfo.InvariantCulture, GetText("err_trial_failed"));
                    MessageBox(msg);
                    goto exit;
                }
            }

            success = true;

            exit:
            return success;
        }

        private bool GetNodeValue(XmlDocument xmldoc, string fileName, Dictionary<string, string> ht, string key, ref string errorText)
        {
            bool success = false;

            XmlNode node = xmldoc.SelectSingleNode("license/customer/" + key);
            if (node != null)
            {
                ht.Add(key, node.InnerText);
                success = true;
            }
            else
            {
                errorText = string.Format(CultureInfo.InvariantCulture, GetText("err_license_missingnode"), key, fileName);
            }
            return success;
        }

        /// <summary>
        /// Check that both dates are valid, that the expireDate is later than the create Date
        /// and that the expireDate is today or after today.
        /// </summary>
        /// <param name="date">dd.mm.yyyy</param>
        /// <returns></returns>
        private bool CheckDates(string createDate, string expireDate, bool silent)
        {
            bool success = false;

            if (!Tools.DateIsValidGermanDate(createDate))
            {
                if (!silent)
                {
                    string msg = string.Format(CultureInfo.InvariantCulture, GetText("err_license_badcreatedateformat"), createDate);
                    MessageBox(msg);
                }
                goto _exit;
            }
            if (!Tools.DateIsValidGermanDate(expireDate))
            {
                if (!silent)
                {
                    string msg = string.Format(CultureInfo.InvariantCulture, GetText("err_license_badexpiredateformat"), expireDate);
                    MessageBox(msg);
                }
                goto _exit;
            }

            DateTime dtCreateDate = Tools.InputTextDate2DateTime(createDate);
            DateTime dtExpireDate = Tools.InputTextDate2DateTime(expireDate);
            DateTime today = DateTime.Today;

            if (dtCreateDate.CompareTo(today) > 0)
            {
                if (!silent)
                {
                    string msg = string.Format(CultureInfo.InvariantCulture, GetText("err_license_createdateinfuture"), createDate);
                    MessageBox(msg);
                }
                goto _exit;
            }
            if (dtExpireDate.CompareTo(today) < 0)
            {
                if (!silent)
                {
                    string msg = string.Format(CultureInfo.InvariantCulture, GetText("err_license_dateexpired"), expireDate);
                    MessageBox(msg);
                }
                goto _exit;
            }

            success = true;

            _exit:
            return success;
        }

        internal bool VerifyLicense(Dictionary<string, string> ht, bool silent, StringBuilder displayError)
        {
            string fileName = PathApplication + System.IO.Path.DirectorySeparatorChar + LicenseFileName;
            return VerifyLicense(fileName, ht, silent, displayError);
        }

        internal bool VerifyLicense(Dictionary<string, string> ht, bool silent)
        {
            StringBuilder displayError = new StringBuilder();

            string fileName = PathApplication + System.IO.Path.DirectorySeparatorChar + LicenseFileName;

            bool success = VerifyLicenseAndContent(fileName, ht, silent, displayError);

            return success;
        }

        private bool VerifyLicenseAndContent(
            string fileName,
            Dictionary<string, string> ht,
            bool silent,
            StringBuilder displayError)
        {
            bool success = false;

            if (VerifyLicense(fileName, ht, silent, displayError))
            {
                //
                // License file has not been tampered with.
                // Now we can check the dates.
                //
                string createDate = ht["licenseCreateDate"];
                string expireDate = ht["licenseExpireDate"];
                if (CheckDates(createDate, expireDate, silent))
                {
                    success = true;
                }
            }

            return success;
        }

        /// <summary>
        /// Checks the existence and format of the license file and that certain required nodes are present.
        /// </summary>
        /// <param name="fileName">License file</param>
        /// <param name="ht">Hashtable that will be populated with the required key-values pairs</param>
        /// <param name="silent">Do or do not display a message box on error </param>
        /// <param name="displayError">The error text if any occured, so that the cause is
        /// not lost if silent=true</param>
        /// <returns></returns>
        private  bool VerifyLicense(
            string fileName, 
            Dictionary<string, string> ht, 
            bool silent, 
            StringBuilder displayError)
        {
            bool success = false;
            string errMsg = "";

            try
            {
                // Verify that an XML document path is provided.
                if (!File.Exists(fileName))
                {
                    errMsg = string.Format(CultureInfo.InvariantCulture, GetText("err_license_notfound"), fileName);
                    displayError.Append(GetText("err_license_nonexists"));
                    goto exit;
                }

                // Get the XML content from the embedded XML public key.
                Stream inStream = null;
                string xmlkey = string.Empty;
                // Achtung: Die Datei OperationenPublicKey.xml muss als embedded resource
                // dem Projekt hinzugefuegt werden, damit das hier klappt.
                inStream = typeof(BusinessLayer).Assembly.GetManifestResourceStream(
                    "Operationen.OperationenLicensePublicKey.xml");

                // Read-in the XML content.
                StreamReader reader = new StreamReader(inStream);
                xmlkey = reader.ReadToEnd();
                reader.Close();
                inStream.Close();

                // Create an RSA crypto service provider from the embedded
                // XML document resource (the public key).
                RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
                csp.FromXmlString(xmlkey);

                // Load the signed XML license file.
                XmlDocument xmldoc = new XmlDocument();
                try
                {
                    xmldoc.Load(fileName);
                }
                catch
                {
                    errMsg = string.Format(CultureInfo.InvariantCulture, GetText("err_license_badxml"), fileName);
                    displayError.Append(GetText("err_license_badformat"));
                    goto exit;
                }

                // Create the signed XML object.
                SignedXml sxml = new SignedXml(xmldoc);

                try
                {
                    //
                    // Get the XML Signature node and load it into the signed XML object.
                    //
                    XmlNode dsig = xmldoc.GetElementsByTagName("Signature", SignedXml.XmlDsigNamespaceUrl)[0];

                    //
                    // If someone tried to be smart and removed the entire Signature element, dsig is null and we get an exception
                    //
                    sxml.LoadXml((XmlElement)dsig);
                }
                catch
                {
                    errMsg = string.Format(CultureInfo.InvariantCulture, GetText("err_license_missingsignature"), fileName);
                    displayError.Append(GetText("err_license_badformat"));
                    goto exit;
                }

                // Verify the signature.
                if (!sxml.CheckSignature(csp))
                {
                    errMsg = string.Format(CultureInfo.InvariantCulture, GetText("err_license_badsignature"), fileName);
                    displayError.Append(GetText("err_license_badformat"));
                    goto exit;
                }

                string []requiredNodes = {
                    //
                    // suchen nach __$xmllicense$__
                    //
                    "email",
                    "lastName",
                    "firstName",
                    "company",
                    "street",
                    "city",
                    "licenseCreateDate",
                    "licenseExpireDate"
                };

                ht.Clear();
                foreach (string key in requiredNodes)
                {
                    string errorText = null;

                    // Werte aus der XML-Datei einlesen
                    if (!GetNodeValue(xmldoc, fileName, ht, key, ref errorText))
                    {
                        errMsg = errorText;
                        displayError.Append(GetText("err_license_badformat"));
                        goto exit;
                    }
                }

                success = true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                success = false;
            }

        exit:
            if (!success && !silent && !string.IsNullOrEmpty(errMsg))
            {
                MessageBox(errMsg);
            }
            return success;
        }

#endregion

        public void MessageBox(string msg)
        {
            System.Windows.Forms.MessageBox.Show(msg, ProgramTitle, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        public bool Confirm(string strText)
        {
            return DialogResult.Yes == System.Windows.Forms.MessageBox.Show(strText, ProgramTitle,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }


        public string PathEdit
        {
            get { return ServerDir + Path.DirectorySeparatorChar + "Edit"; }
        }

        public string PathDokumente
        {
            get { return ServerDir + Path.DirectorySeparatorChar + "Dokumente"; }
        }

        public string PathLogfiles
        {
            get { return ServerDir + Path.DirectorySeparatorChar + "logfiles"; }
        }

        public string PathPlugins
        {
            get { return AppPath + Path.DirectorySeparatorChar + "plugins"; }
        }

        public string PathApplication
        {
            get
            {
                string path = Application.StartupPath;
                return path;
            }
        }

        public string PathData
        {
            get { return ServerDir; }
        }

        /// <summary>
        /// Get the number of files and the total size of a directory. Do not recurse.
        /// </summary>
        /// <param name="path">The directory</param>
        /// <param name="infoFiles">Text expressing 'number of files' in 'path'</param>
        /// <param name="infoSize">Text expressing 'total size' in 'path'</param>
        internal void GetFilesinDirectoryInfo(string path, out long numFiles, out string totalSize)
        {
            //
            // On error, this is the default text
            //
            numFiles = -1;
            totalSize = null;

            try
            {
                if (Directory.Exists(path))
                {
                    long totalBytes = 0;
                    DirectoryInfo dir = new DirectoryInfo(path);
                    FileInfo[] files = dir.GetFiles();

                    foreach (FileInfo file in files)
                    {
                        totalBytes += file.Length;
                    }

                    numFiles = files.Length;
                    totalSize = BytesToUserReadableUnit(totalBytes);
                }
            }
            catch
            {
            }
        }

        public static string BytesToUserReadableUnit(long bytes)
        {
            string text = "";
            const long _1024 = 1024;

            if (bytes < _1024)
            {
                text = "" + bytes + " Bytes";
            }
            else if (bytes < _1024 * _1024)
            {
                long kb = bytes / _1024;
                text = "" + kb + " KB";
            }
            else if (bytes < _1024 * _1024 * _1024)
            {
                long mb = bytes / _1024 / _1024;
                text = "" + mb + " MB";
            }
            else if (bytes < _1024 * _1024 * _1024 * _1024)
            {
                long gb = bytes / _1024 / _1024 / _1024;
                text = "" + gb + " GB";
            }
            else if (bytes < _1024 * _1024 * _1024 * _1024 * 1024)
            {
                long tb = bytes / _1024 / _1024 / _1024 / 1024;
                text = "" + tb + " TB";
            }

            return text;
        }

        public DatabaseLayer DatabaseLayer
        {
            get { return _databaseLayer; }
        }

        public override void SetDatabaseLayer(DatabaseLayerBase databaseLayer)
        {
            base.SetDatabaseLayer(databaseLayer);
            _databaseLayer = (DatabaseLayer)databaseLayer;
        }

        public string AppAndVersionStringForPrinting
        {
            get { return AppTitle() + " " + VersionString + " " + UrlHomepageForDisplay; }
        }

        public string AppAndVersionString
        {
            get { return AppTitle() + " " + VersionString; }
        }

        public string BareVersionString
        {
            get { return VersionMajor + "." + VersionMinor + "." + ReleaseMajor; }
        }

#region DatabaseLayer 
        public void OpenDatabaseForImport()
        {
            _databaseLayer.OpenForImport();
        }
        public void CloseDatabaseForImport()
        {
            _databaseLayer.CloseForImport();
        }
/*        public DatabaseLayer oDatabaseLayer
		{
			get {return _databaseLayer;}
		}*/
#endregion

#region CreateDataRow
        public DataRow CreateDataRowChirurg()
        {
            return _databaseLayer.CreateDataRowChirurg();
        }
        public DataRow CreateDataRowChirurgenRichtlinien(int ID_Chirurgen, int ID_Richtlinien)
        {
            return _databaseLayer.CreateDataRowChirurgenRichtlinien(ID_Chirurgen, ID_Richtlinien);
        }
        public DataRow CreateDataRowChirurgenOperationen(int nID_Chirurgen)
        {
            return _databaseLayer.CreateDataRowChirurgenOperationen(nID_Chirurgen);
        }

        public DataRow CreateDataRowKommentar(int nID_ChirurgenFuer)
        {
            return _databaseLayer.CreateDataRowKommentar(CurrentUser_ID_Chirurgen, CurrentUser_Nachname, nID_ChirurgenFuer);
        }

        public DataRow CreateDataRowPlanOperation(int nID_Chirurgen)
        {
            return _databaseLayer.CreateDataRowPlanOperation(nID_Chirurgen);
        }
        public int GetID_Chirurgen(string strNachname)
        {
            return _databaseLayer.GetID_Chirurgen(strNachname);
        }
#endregion

#region Chirurgen
        public int GetID_Chirurgen(string nachname, string vorname)
        {
            return _databaseLayer.GetID_Chirurgen(nachname, vorname);
        }
        public int GetID_ChirurgenByImportID(string importID)
        {
            return _databaseLayer.GetID_ChirurgenByImportID(importID);
        }

        public DataRow GetChirurgenOperationenRecord(int ID_ChirurgenOperationen)
        {
            return _databaseLayer.GetChirurgenOperationenRecord(ID_ChirurgenOperationen);
        }

        public bool UpdateChirurgenOperationenRichtlinie(DataRow dataRow)
        {
            return DatabaseLayer.UpdateChirurgenOperationenRichtlinie(dataRow);
        }

        public DataRow CheckChirurgOperationen(int nID_Chirurgen, string opsKode, DateTime dt)
        {
            return _databaseLayer.CheckChirurgOperationen(nID_Chirurgen, opsKode, dt);
        }
        public DataRow CheckChirurgOperationen(int nID_Chirurgen, string opsKode, DateTime dt, string identifier)
        {
            return _databaseLayer.CheckChirurgOperationen(nID_Chirurgen, opsKode, dt, identifier);
        }
        public DataView GetChirurgenOperationen(int nID_Chirurgen)
        {
            return _databaseLayer.GetChirurgenOperationen(nID_Chirurgen);
        }

        public DataRow GetChirurgenOperationenFirst()
        {
            return _databaseLayer.GetChirurgenOperationenFirst();
        }
        public DataRow GetChirurgenOperationenFirst(int nID_Chirurgen)
        {
            return _databaseLayer.GetChirurgenOperationenFirst(nID_Chirurgen);
        }
        public DataRow GetChirurgenOperationenLast()
        {
            return _databaseLayer.GetChirurgenOperationenLast();
        }
        public DataRow GetChirurgenOperationenLast(int nID_Chirurgen)
        {
            return _databaseLayer.GetChirurgenOperationenLast(nID_Chirurgen);
        }
        public long GetChirurgenOperationenCount()
        {
            return _databaseLayer.GetChirurgenOperationenCount();
        }
        public long GetChirurgenOperationenCount(int nID_Chirurgen)
        {
            return _databaseLayer.GetChirurgenOperationenCount(nID_Chirurgen);
        }

        public bool UpdateChirurg(
            DataRow oDataRow,
            Dictionary<int, DateTime?> gebieteVon,
            Dictionary<int, DateTime?> gebieteBis)
        {
            Write2Log(Update, "Chirurgen " + (string)oDataRow["Nachname"]);
            return _databaseLayer.UpdateChirurg(oDataRow, gebieteVon, gebieteBis);
        }


        public long GetCountImportID(int ID_Chirurgen, string userID)
        {
            return DatabaseLayer.GetCountImportID(ID_Chirurgen, userID);
        }
        public long GetCountUserID(int ID_Chirurgen, string userID)
        {
            return DatabaseLayer.GetCountUserID(ID_Chirurgen, userID);
        }
        public long GetCountUserID(string userID)
        {
            return DatabaseLayer.GetCountUserID(userID);
        }

        /// <summary>
        /// Versucht, aus dem Nachnamen eine eindeutige UserID zu generieren.
        /// 
        /// </summary>
        /// <param name="nachname"></param>
        /// <returns>Die erzeugte userID, diese muss nicht eindeutig sein</returns>
        public string AutoCreateUniqueUserID(string nachname)
        {
            // Solange eine 1 an die UserID dranhängen, bis sie eindeutig ist.
            // Das muss ja automatisch geschehen.
            // Ein Administrator kann dann im Chirurgen - Bearbeiten Fenster
            // sehen, welche UserID erzeugt wurde.
            // Sicherheitshalber probieren wir das aber nur so oft aus, wie das Datenbankfeld groß ist, 
            // damit man nicht in eine Endlosschleife gerät.

            string userID = Tools.LastName2LogOnName(nachname);

            // Chirurgen.UserID hat Länge 20 in der DB.
            while (userID.Length < 20)
            {
                if (GetCountUserID(-1, userID) == 0)
                {
                    break;
                }
                userID = userID + "1";
            }

            return userID;
        }

        public long GetIstOperationenCountForChirurg(int ID_Chirurgen, DateTime? von, DateTime? bis, string ops, int quelle, int opFunktionen)
        {
            return _databaseLayer.GetIstOperationenCountForChirurg(ID_Chirurgen, von, bis, ops, quelle, opFunktionen);
        }
        public DataView GetChirurgenOperationen(int nID_Chirurgen, DateTime? dtVon, DateTime? dtBis)
        {
            return _databaseLayer.GetChirurgenOperationen(nID_Chirurgen, dtVon, dtBis);
        }

        public static void AddListToMessageBox(StringBuilder sb, List<string> items)
        {
            sb.Append("\r");
            sb.Append("\r");

            for (int i = 0; i < items.Count; i++)
            {
                sb.Append("\r- '");
                sb.Append(items[i]);
                sb.Append("'");

                if (i > 15)
                {
                    //
                    // Trying to delete the last user would produce a list with ALL rights
                    //
                    sb.Append("\r- ...'");
                    break;
                }
            }
        }

        /// <summary>
        /// May not delete if this causes a right to be abandoned
        /// Chirurgen - SecGroupsChirurgen - SecGroupsSecRights - SecRights
        /// </summary>
        /// <param name="ID_Chirurgen"></param>
        /// <returns></returns>
        private bool ChirurgMayBeDeleted(int ID_Chirurgen)
        {
            List<string> abandonedRights = new List<string>();

            bool success = true;

            long n = CountAbandonedSecRightsWithoutUser(ID_Chirurgen, abandonedRights);

            ReplaceSecRightNameByDescription(abandonedRights);

            if (n > 0)
            {
                success = false;

                StringBuilder sb = new StringBuilder(GetText("err_del_user_abandoned_rights"));
                AddListToMessageBox(sb, abandonedRights);
                MessageBox(sb.ToString());
            }

            return success;
        }

        /// <summary>
        /// Chirurg und alle abhaengigen Daten loeschen
        /// </summary>
        /// <param name="nID_Chirurgen"></param>
        /// <returns>true wenn alles geloescht wurde, false sonst</returns>
        public bool DeleteChirurg(int ID_Chirurgen)
        {
            bool success = false;

            DataRow row = GetChirurg(ID_Chirurgen);

            this.Write2Log(Delete, "Chirurgen " + (string)row["Nachname"]);

            if (ChirurgMayBeDeleted(ID_Chirurgen))
            {
                success = _databaseLayer.DeleteChirurg(ID_Chirurgen);
            }

            return success;
        }

        public long GetChirurgenCount()
        {
            return _databaseLayer.GetChirurgenCount();
        }

        public long GetChirurgenCountUserId(string userId)
        {
            return _databaseLayer.GetChirurgenCountUserId(userId);
        }
        public long GetCustomerDataCount()
        {
            return _databaseLayer.GetCustomerDataCount();
        }

        public bool ValidateSerialFormat(string serialNumber)
        {
            OperationenSerial.SerialLogic sl = new OperationenSerial.SerialLogic();

            return sl.ValidateSerialNumber(serialNumber);
        }

        public bool CheckSerial(DataRow chirurg, string serialNumber)
        {
            // 1. Format der Seriennummer überprüfen
            // 2. Wenn es diese Seriennummer schon gibt, dann Fehler!
            bool success = false;

            if (!ValidateSerialFormat(serialNumber))
            {
                goto exit;
            }

            long count = DatabaseLayer.GetChirurgenSerialCount(serialNumber);
            if (count > 0)
            {
                MessageBox(GetText("err_serial_used"));
                goto exit;
            }

            success = true;
            exit:
            return success;
        }

        /// <summary>
        /// Holt alle Chirurgen, auf die ID_Chirurgen Zugriff hat.
        /// Ist wie GetChirurgen(), nur dass nicht der angemeldete Benutzer, sondern der übergebene genommen wird
        /// </summary>
        /// <returns></returns>
        public DataView GetChirurgen(int ID_Chirurgen)
        {
            return _databaseLayer.GetChirurgen(ID_Chirurgen, "Aktiv <> 0");
        }

        public int InsertChirurg(
            DataRow oDataRow,
            Dictionary<int, DateTime?> gebieteVon,
            Dictionary<int, DateTime?> gebieteBis)
		{
            int nID_Chirurgen = _databaseLayer.InsertChirurg(oDataRow, gebieteVon, gebieteBis);
            if (nID_Chirurgen != -1)
            {
                this.Write2Log(Insert, "Chirurgen " + (string)oDataRow["Nachname"]);
            }
            return nID_Chirurgen;
        }
#endregion

#region Kommentare
        public DataView GetKommentare()
        {
            return _databaseLayer.GetKommentare();
        }
        public DataView GetKommentare(int nID_ChirurgenFuer)
        {
            return _databaseLayer.GetKommentare(nID_ChirurgenFuer);
        }
        public DataRow GetKommentar(int nID_Kommentare)
        {
            return _databaseLayer.GetKommentar(nID_Kommentare);
        }
        public bool UpdateKommentar(DataRow oKommentar)
        {
            this.Write2Log(Update, "Kommentare", ConvertToInt32(oKommentar["ID_Kommentare"]));
            return _databaseLayer.UpdateKommentar(oKommentar);
        }
        public int InsertKommentar(DataRow oKommentar)
        {
            int nID_Kommentare = _databaseLayer.InsertKommentar(oKommentar);
            this.Write2Log(Insert, "Kommentare", nID_Kommentare);
            return nID_Kommentare;
        }
        public bool DeleteKommentar(int nID_Kommentare)
        {
            this.Write2Log(Delete, "Kommentare", nID_Kommentare);
            return _databaseLayer.DeleteKommentar(nID_Kommentare);
        }

#endregion

#region Operationen
        public DataView GetAusgefuehrteOperationenFuerGebiete(
            List<int> arGebiete, 
            int quelle,
            DateTime? von, 
            DateTime? bis, 
            int ID_OPFunktionen)
        {
            if (arGebiete.Contains(ID_Alle))
            {
                arGebiete = null;
            }

            int ID_ChirurgenLogin = ConvertToInt32(_currentUser["ID_Chirurgen"]);
            return _databaseLayer.GetAusgefuehrteOperationenFuerGebiete(ID_ChirurgenLogin, arGebiete, quelle, von, bis, ID_OPFunktionen);
        }

        public int GetID_OperationenByOpsKode(string strOPSKode)
        {
            return _databaseLayer.GetID_OperationenByOpsKode(strOPSKode);
        }

        public int GetMaxID_Operationen()
        {
            return _databaseLayer.GetMaxID_Operationen();
        }
        
        public long OPSKodePatternExists(string strOPSKode)
        {
            return _databaseLayer.OPSKodePatternExists(strOPSKode);
        }

        public DataRow CreateDataRowOperation()
        {
            return _databaseLayer.CreateDataRowOperation();
        }

        public int InsertOperation(DataRow oDataRow)
        {
            int nID_Operationen = _databaseLayer.InsertOperation(oDataRow);
            this.Write2Log(Insert, "Operationen " + (string)oDataRow["OPS-Kode"]);
            return nID_Operationen;
        }
        public bool DeleteOperation(int nID_Operationen)
        {
            DataRow row = _databaseLayer.GetOperation(nID_Operationen);
            this.Write2Log(Delete, "Operationen " + (string)row["Kode"]);
            return _databaseLayer.DeleteOperation(nID_Operationen);
        }

        public DataRow GetOperation(int nID_Operationen)
        {
            return _databaseLayer.GetOperation(nID_Operationen);
        }

        public DataRow GetOperationen(string filterOpsKodeText)
        {
            return _databaseLayer.GetOperationen(filterOpsKodeText);
        }

        public void GetOperationen(ListView lv)
        {
            _databaseLayer.GetOperationen(lv);
        }
        public void GetOperationen(ListView lv, string filterOpsKode, string filterOpsText)
        {
            _databaseLayer.GetOperationen(lv, filterOpsKode, filterOpsText);
        }
        public void GetOperationen(ListView lv, string filterOpsKodeText)
        {
            _databaseLayer.GetOperationen(lv, filterOpsKodeText);
        }
        public DataRow GetPlanOperation(int nID_PlanOperationen)
        {
            return _databaseLayer.GetPlanOperation(nID_PlanOperationen);
        }
        public DataView GetPlanOperationen(int nID_Chirurgen)
        {
            return _databaseLayer.GetPlanOperationen(nID_Chirurgen);
        }
        public DataView GetPlanOperationenArten(int nID_Chirurgen, DateTime? dtFrom, DateTime? dtTo)
        {
            return _databaseLayer.GetPlanOperationenArten(nID_Chirurgen, dtFrom, dtTo);
        }
        public bool UpdatePlanOperation(DataRow oDataRow)
        {
            this.Write2Log(Update, "PlanOperationen " + (string)oDataRow["Operation"]);
            return _databaseLayer.UpdatePlanOperation(oDataRow);
        }
        public int InsertPlanOperation(DataRow oDataRow)
        {
            int nID = _databaseLayer.InsertPlanOperation(oDataRow);
            this.Write2Log(Insert, "PlanOperationen", nID);
            return nID;
        }
        public bool DeletePlanOperation(int nID_PlanOperationen)
        {
            this.Write2Log(Delete, "PlanOperationen", nID_PlanOperationen);
            return _databaseLayer.DeletePlanOperation(nID_PlanOperationen);
        }
        public int GetPlanOperationenSumme(int nID_Chirurgen, string sOperation)
        {
            return _databaseLayer.GetPlanOperationenSumme(nID_Chirurgen, sOperation);
        }
        public int GetPlanOperationenSumme(int nID_Chirurgen, string sOperation, DateTime? dtFrom, DateTime? dtTo)
        {
            return _databaseLayer.GetPlanOperationenSumme(nID_Chirurgen, sOperation, dtFrom, dtTo);
        }
        protected internal bool UpdateChirurgenOperationen(DataRow dataRow)
        {
            return _databaseLayer.UpdateChirurgenOperationen(dataRow);
        }
        protected internal int InsertChirurgenOperationen(DataRow dataRow)
        {
            return InsertChirurgenOperationen(dataRow, true);
        }
        protected internal int InsertChirurgenOperationen(DataRow dataRow, bool bLogAction)
        {
            int nID = _databaseLayer.InsertChirurgenOperationen(dataRow);
            if (bLogAction)
            {
                //
                // Bei Datenimport nicht loggen!
                // Tabellenname - nicht übersetzen!
                //
                this.Write2Log(Insert, "ChirurgenOperationen", nID);
            }
            return nID;
        }
        public bool DeleteChirurgenOperationen(int _nID_ChirurgenOperationen)
        {
            //
            // Tabellenname - nicht übersetzen!
            //
            this.Write2Log(Delete, "ChirurgenOperationen", _nID_ChirurgenOperationen);
            return _databaseLayer.DeleteChirurgenOperationen(_nID_ChirurgenOperationen);
        }
      
        public long GetChirurgenOperationenAnzahl(int nID_Chirurgen, int nID_OPFunktionen, int quelle, string sOperation, DateTime? dtFrom, DateTime? dtTo)
        {
            return _databaseLayer.GetChirurgenOperationenAnzahl(nID_Chirurgen, nID_OPFunktionen, quelle, sOperation, dtFrom, dtTo);
        }
        public DataView GetOperationenExecuted()
        {
            return _databaseLayer.GetOperationenExecuted();
        }
        public DataRow GetIstOperationForPlanOperation(string sOperation)
        {
            return _databaseLayer.GetIstOperationForPlanOperation(sOperation);
        }

        public DataView GetOPZeiten(int nID_Chirurgen, int nID_OPFunktionen, DateTime? dtVon, DateTime? dtBis, int quelle, string opsKode, string orderBy)
        {
            return _databaseLayer.GetOPZeiten(nID_Chirurgen, nID_OPFunktionen, dtVon, dtBis, quelle, opsKode, orderBy);
        }
        public DataView GetOPZeiten(int nID_Chirurgen, int nID_OPFunktionen, DateTime? dtVon, DateTime? dtBis, int quelle)
        {
            return _databaseLayer.GetOPZeiten(nID_Chirurgen, nID_OPFunktionen, dtVon, dtBis, quelle);
        }

#endregion

#region KlinischeErgebnisseTypen
        public DataView GetKlinischeErgebnisseTypen()
        {
            return GetKlinischeErgebnisseTypen(false);
        }
        public DataView GetKlinischeErgebnisseTypen(bool includeAll)
        {
            return _databaseLayer.GetKlinischeErgebnisseTypen(includeAll);
        }
#endregion

#region Richtlinien
        public DataRow CreateDataRowRichtlinie()
        {
            return _databaseLayer.CreateDataRowRichtlinie();
        }

        public DataView GetRichtlinien()
        {
            return _databaseLayer.GetRichtlinien();
        }
        public DataRow GetRichtlinie(int ID_Richtlinien)
        {
            return _databaseLayer.GetRichtlinie(ID_Richtlinien);
        }
        public DataRow GetRichtlinie(int ID_Gebiete, int lfdNummer, int richtzahl, string untBehMethode, bool returnFirst)
        {
            return _databaseLayer.GetRichtlinie(ID_Gebiete, lfdNummer, richtzahl, untBehMethode, returnFirst);
        }
        public bool UpdateRichtlinie(DataRow row)
        {
            return _databaseLayer.UpdateRichtlinie(row);
        }
        public int InsertRichtlinie(DataRow row)
        {
            return _databaseLayer.InsertRichtlinie(row, false);
        }
        public int InsertRichtlinie(DataRow row, bool useLfdNummer)
        {
            return _databaseLayer.InsertRichtlinie(row, useLfdNummer);
        }
        public bool DeleteRichtlinie(int ID_Richtlinien)
        {
            DataRow row = GetRichtlinie(ID_Richtlinien);

            this.Write2Log(Delete, "Richtlinie " + ConvertToInt32(row["LfdNummer"]) + ", " + (string)row["UntBehMethode"]);
            return _databaseLayer.DeleteRichtlinie(ID_Richtlinien);
        }
        public DataRow GetRichtlinieForLfdNummerGebiet(int ID_Gebiete, int nLfdNummer, bool returnFirst)
        {
            return _databaseLayer.GetRichtlinieForLfdNummerGebiet(ID_Gebiete, nLfdNummer, returnFirst);
        }
#endregion
        
#region Login
        public bool LoginWindowsAuthentication()
        {
            string userName = System.Environment.UserName;
            _currentUser = _databaseLayer.Login(userName);
            if (_currentUser == null)
            {
                this.Write2Log("LOGIN", "Login failed: " + userName);
                this.DisplayError("Windows user authentication login failed for user '" + userName + "'");
            }
            else
            {
                int ID_Chirurgen = ConvertToInt32(_currentUser["ID_Chirurgen"]);
                _currentUserRights = GetUserRights(ID_Chirurgen);

                string uiCulture = GetUserSettingsString(GlobalConstants.SectionProgram, GlobalConstants.KeyProgramUICulture, Operationen.Default.UICulture);
                SetCurrentUICulture(uiCulture);
            }

            return (_currentUser != null);
        }

        public bool Login(string strUser, string strPassword)
        {
            _currentUser = _databaseLayer.Login(strUser, strPassword);
            if (_currentUser == null)
            {
                this.Write2Log("LOGIN", "Login failed: " + strUser);
            }
            else
            {
                int ID_Chirurgen = ConvertToInt32(_currentUser["ID_Chirurgen"]);
                _currentUserRights = GetUserRights(ID_Chirurgen);

                string uiCulture = GetUserSettingsString(GlobalConstants.SectionProgram, GlobalConstants.KeyProgramUICulture, Operationen.Default.UICulture);
                SetCurrentUICulture(uiCulture);
            }

            return (_currentUser != null);
        }

        public bool SetCurrentUICulture(string culture)
        {
            bool changed = false;

            if (IsValidUICulture(culture) && (culture != Thread.CurrentThread.CurrentUICulture.Name))
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
                SaveUserSettings(ConvertToInt32(_currentUser["ID_Chirurgen"]), GlobalConstants.SectionProgram, GlobalConstants.KeyProgramUICulture, culture);
                changed = true;
            }

            return changed;
        }

        internal void ReloadUserRights()
        {
            _currentUserRights = GetUserRights(ConvertToInt32(_currentUser["ID_Chirurgen"]));
        }

        public bool CheckUserAndPassword(string strUserID, string strPassword)
        {
            return _databaseLayer.CheckUserAndPassword(strUserID, strPassword);
        }
        public bool UpdatePassword(string strUserID, string strNewPassword)
        {
            this.Write2Log(Update, "Password: " + strUserID);
            return _databaseLayer.UpdatePassword(strUserID, strNewPassword);
        }
        public bool UpdatePassword(int nID_Chirurgen, string strNewPassword)
        {
            DataRow row = GetChirurg(nID_Chirurgen);
            this.Write2Log(Update, "Password: " + (string)row["Nachname"]);
            return _databaseLayer.UpdatePassword(nID_Chirurgen, strNewPassword);
        }

#endregion

        public override string AppTitle()
        {
            string windowText = base.AppTitle();

            return windowText;
        }

        public override string AppTitle(string text)
        {
            string windowText = base.AppTitle(text);

            return windowText;
        }
        public bool CurrentUserMustChangePassword
        {
            get { return 0 != ConvertToInt32(_currentUser["MustChangePassword"]); }
        }

#region RichtlinienOpsKodes
        public DataView GetRichtlinienOpsKodesSubset(int ID_Richtlinien, string opsKode)
        {
            return _databaseLayer.GetRichtlinienOpsKodesSubset(ID_Richtlinien, opsKode);
        }
        public DataView GetRichtlinienOpsKodesSuperset(int ID_Richtlinien, string opsKode)
        {
            return _databaseLayer.GetRichtlinienOpsKodesSuperset(ID_Richtlinien, opsKode);
        }
        public DataView GetRichtlinienOpsKodes(int ID_Gebiete, bool orderByOPSKode)
        {
            return _databaseLayer.GetRichtlinienOpsKodes(ID_Gebiete, orderByOPSKode);
        }
        public DataView GetRichtlinienOpsKodesRichtlinie(int ID_Richtlinien, bool orderByOPSKode)
        {
            return _databaseLayer.GetRichtlinienOpsKodesRichtlinie(ID_Richtlinien, orderByOPSKode);
        }
        public DataView GetRichtlinienOpsKodes()
        {
            return _databaseLayer.GetRichtlinienOpsKodes();
        }
        public int GetRichtlinienOpsKodesCount(int ID_Richtlinien, string opsKode)
        {
            return _databaseLayer.GetRichtlinienOpsKodesCount(ID_Richtlinien, opsKode);
        }
        public bool DeleteRichtlinienOpsKodes(int nID_RichtlinienOpsKodes)
        {
            //
            // Tabellenname - nicht übersetzen!
            //
            this.Write2Log(Delete, "RichtlinienOpsKodes", nID_RichtlinienOpsKodes);
            return _databaseLayer.DeleteRichtlinienOpsKodes(nID_RichtlinienOpsKodes);
        }
        public int InsertRichtlinienOpsKodes(DataRow oDataRow)
        {
            //
            // Tabellenname - nicht übersetzen!
            //
            int nID = _databaseLayer.InsertRichtlinienOpsKodes(oDataRow);
            this.Write2Log(Insert, "RichtlinienOpsKodes", nID);
            return nID;
        }
        public DataRow CreateDataRowRichtlinienOpsKodes()
        {
            return _databaseLayer.CreateDataRowRichtlinienOpsKodes();
        }

        public DataView GetMissingRichtlinienOPs(int nID_Gebiete, int nID_Chirurgen, int quelle, DateTime? dtFrom, DateTime? dtTo)
        {
            return _databaseLayer.GetMissingRichtlinienOPs(nID_Gebiete, nID_Chirurgen, quelle, dtFrom, dtTo);
        }

        public DataView GetMissingRichtlinienOPsAlle(int nID_Gebiete, int quelle, DateTime? dtFrom, DateTime? dtTo)
        {
            int ID_ChirurgenLogin = ConvertToInt32(_currentUser["ID_Chirurgen"]);

            return _databaseLayer.GetMissingRichtlinienOPsAlle(ID_ChirurgenLogin, nID_Gebiete, quelle, dtFrom, dtTo);
        }


        public DataView GetRichtlinienOPSummenChirurgRichtlinie(int nID_Chirurgen, int ID_Richtlinien, int quelle, DateTime? dtFrom, DateTime? dtTo)
        {
            return _databaseLayer.GetRichtlinienOPSummenChirurgRichtlinie(nID_Chirurgen, ID_Richtlinien, quelle, dtFrom, dtTo);
        }

#endregion

#region ChirurgenDokumente
        public DataRow CreateDataRowChirurgenDokumente(int nID_Chirurgen, int nID_Dokumente)
        {
            return _databaseLayer.CreateDataRowChirurgenDokumente(nID_Chirurgen, nID_Dokumente);
        }
        public int InsertChirurgenDokumente(DataRow oDataRow)
        {
            DataRow oChirurg = GetChirurg(ConvertToInt32(oDataRow["ID_Chirurgen"]));
            DataRow oDokument = GetDokument(ConvertToInt32(oDataRow["ID_Dokumente"]));

            //
            // Tabellenname - nicht übersetzen!
            //
            this.Write2Log(Insert, "ChirurgenDokumente: " 
                + (string)oChirurg["Nachname"]
                + "/" + (string)oDokument["Beschreibung"]);

            return _databaseLayer.InsertChirurgenDokumente(oDataRow);
        }
        public bool DeleteChirurgenDokumente(int nID_ChirurgenDokumente)
        {
            //
            // Tabellenname - nicht übersetzen!
            //
            this.Write2Log(Delete, "ChirurgenDokumente: " + nID_ChirurgenDokumente);
            return _databaseLayer.DeleteChirurgenDokumente(nID_ChirurgenDokumente);
        }
        public bool UpdateChirurgenDokumente(DataRow oDataRow)
        {
            //
            // Tabellenname - nicht übersetzen!
            //
            this.Write2Log(Insert, "ChirurgenDokumente: " + oDataRow["ID_ChirurgenDokumente"].ToString());
            return _databaseLayer.UpdateChirurgenDokumente(oDataRow);
        }
        public DataView GetChirurgenDokumente(int nID_Chirurgen)
        {
            return _databaseLayer.GetChirurgenDokumente(nID_Chirurgen);
        }
        public DataView GetChirurgenDokumenteTable(int nID_Chirurgen)
        {
            return _databaseLayer.GetChirurgenDokumenteTable(nID_Chirurgen);
        }
        public DataRow GetChirurgenDokument(int nID_ChirurgenDokumente)
        {
            return _databaseLayer.GetChirurgenDokument(nID_ChirurgenDokumente);
        }
        public bool UpdateChirurgenDokumenteBearbeitung(int nID_ChirurgenDokumente)
        {
            return _databaseLayer.UpdateChirurgenDokumenteBearbeitung(nID_ChirurgenDokumente);
        }

#endregion

#region Dokumente
        public DataView GetDokumente()
        {
            return _databaseLayer.GetDokumente();
        }
        public DataRow GetDokument(int nID_Dokumente)
        {
            return _databaseLayer.GetDokument(nID_Dokumente);
        }
        public int InsertDokument(DataRow row)
        {
            return _databaseLayer.InsertDokument(row);
        }
        public DataRow CreateDataRowDokument()
        {
            return _databaseLayer.CreateDataRowDokument();
        }
        public bool UpdateDokument(DataRow row)
        {
            return _databaseLayer.UpdateDokument(row);
        }
        public bool DeleteDokument(int ID_Dokumente)
        {
            DataRow row = GetDokument(ID_Dokumente);

            //
            // Tabellenname - nicht übersetzen!
            //
            this.Write2Log(Delete, "Dokumente: " + ConvertToInt32(row["LfdNummer"]) + ", " + (string)row["Dateiname"]);
            return _databaseLayer.DeleteDokument(ID_Dokumente);
        }

        /// <summary>
        /// We must have Write-Rights in the data path
        /// </summary>
        /// <param name="dataPath">The folder containing the common data</param>
        /// <returns></returns>
        public bool CheckInstallationRights()
        {
            bool success = true;

            string fileName = Path.Combine(ServerDir, "installation-test.txt");
            try
            {
                FileStream fs = File.Create(fileName);
                fs.Close();
                fs = null;

                File.Delete(fileName);
            }
            catch
            {
                MessageBox(string.Format(CultureInfo.InvariantCulture, GetText("error1"), ServerDir));
                success = false;
            }

            return success;
        }

        public bool InitialCheckDir(string strPath)
        {
            bool bSuccess = false;

            try
            {
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
            }
            catch
            {
                //
                // Seit 1.19.0 sind alle Verzeichnisse im server-Verzeichnis.
                // Nach einem Update gibt es die aber nicht, daher werden sie jetzt einfach angelegt.
                //
            }

            if (Directory.Exists(strPath))
            {
                bSuccess = true;
            }
            else
            {
                DisplayError(string.Format(GetText("err_missing_folder"), strPath));
            }

            return bSuccess;
        }

        public override bool InitialDirCheck()
        {
            bool bSuccess = false;

            if (!CheckInstallationRights())
            {
                goto Exit;
            }

            if (!InitialCheckDir(PathEdit))
            {
                goto Exit;
            }
            if (!InitialCheckDir(PathDokumente))
            {
                goto Exit;
            }
            if (!InitialCheckDir(PathLogfiles))
            {
                goto Exit;
            }
            if (!InitialCheckDir(PathPlugins))
            {
                goto Exit;
            }

            bSuccess = true;

        Exit:

            return bSuccess;
        }

        public override bool InitialDocumentCheck()
        {
            bool bSuccess = false;

            try
            {
                DirectoryInfo dir = new DirectoryInfo(PathEdit);

                // Admin sieht alle, normale user nur die eigenen.
                string strFilter;

                if (UserHasRight("cmd.viewAllDocs"))
                {
                    strFilter = "*.*";
                }
                else
                {
                    strFilter = CurrentUser_UserID + "*.*";
                }

                FileInfo[] files = dir.GetFiles(strFilter);

                if (files.Length > 0)
                {
                    MessageBox(GetText("attn_files_to_edit"));
                }
                bSuccess = true;
            }
            catch (Exception e)
            {
                DisplayError(e, string.Format(GetText("err_missing_folder"), PathEdit));
            }

            return bSuccess;
        }
#endregion

#region ImportChirurgenExclude
        public DataRow CreateDataRowImportChirurgenExclude()
        {
            return _databaseLayer.CreateDataRowImportChirurgenExclude();
        }
        public DataView GetImportChirurgenExclude()
        {
            if (_importChirurgenExclude == null)
            {
                _importChirurgenExclude = _databaseLayer.GetImportChirurgenExclude();
            }

            return _importChirurgenExclude;
        }

        public bool DeleteImportChirurgenExclude(int nID)
        {
            _importChirurgenExclude = null;

            //
            // Tabellenname - nicht übersetzen
            //
            this.Write2Log(Delete, "ImportChirurgenExclude", nID);
            return _databaseLayer.DeleteImportChirurgenExclude(nID);
        }
        public int InsertImportChirurgenExclude(DataRow oDataRow)
		{
            _importChirurgenExclude = null;

            //
            // Tabellenname - nicht übersetzen
            //
            this.Write2Log(Insert, "ImportChirurgenExclude: " + (string)oDataRow["Nachname"]);

            return _databaseLayer.InsertImportChirurgenExclude(oDataRow);
		}
        public int GetID_ImportChirurgenExclude(string strNachname)
        {
            int nID = -1;
            DataView dv = GetImportChirurgenExclude();

            foreach (DataRow row in dv.Table.Rows)
            {
                if (((string)row["Nachname"]).ToLower() == strNachname.ToLower())
                {
                    nID = ConvertToInt32(row["ID_ImportChirurgenExclude"]);
                    break;
                }
            }

            return nID;
        }
        public int GetID_ImportChirurgenExclude(string strNachname, string strVorname)
        {
            int nID = -1;
            DataView dv = GetImportChirurgenExclude();

            foreach (DataRow row in dv.Table.Rows)
            {
                if (((string)row["Nachname"]).ToLower() == strNachname.ToLower() &&
                    (((string)row["Vorname"]).ToLower() == strVorname.ToLower()))
                {
                    nID = ConvertToInt32(row["ID_ImportChirurgenExclude"]);
                    break;
                }
            }

            return nID;
        }

#endregion

#region ChirurgenFunktionen

        public int GetAnyOneID_ChirurgenFunktionen()
        {
            int ID_ChirurgenFunktionen = -1;

            // Der Chirurg braucht eine Funktion, es wird die erstbeste genommen.
            // Sollte die Tabelle leer sein, gibt es einen SQL-Fehler und das ist gut so.
            DataView dv = GetChirurgenFunktionen();
            if (dv.Table.Rows.Count > 0)
            {
                ID_ChirurgenFunktionen = ConvertToInt32(dv.Table.Rows[0]["ID_ChirurgenFunktionen"]);
            }

            return ID_ChirurgenFunktionen;
        }

        public DataRow CreateDataRowChirurgenFunktionen()
        {
            return _databaseLayer.CreateDataRowChirurgenFunktionen();
        }
        public DataRow GetChirurgenFunktion(int ID_ChirurgenFunktionen)
        {
            DataRow row = null;

            foreach (DataRow r in _chirurgenFunktionen.Table.Rows)
            {
                if (ConvertToInt32(r["ID_ChirurgenFunktionen"]) == ID_ChirurgenFunktionen)
                {
                    row = r;
                    break;
                }
            }

            return row;
        }

        public bool DeleteChirurgenFunktionen(int nID)
        {
            _chirurgenFunktionen = null;

            //
            // Tabellenname - nicht übersetzen
            //
            this.Write2Log(Delete, "ChirurgenFunktionen", nID);
            return _databaseLayer.DeleteChirurgenFunktionen(nID);
        }
        public int InsertChirurgenFunktionen(DataRow oDataRow)
        {
            _chirurgenFunktionen = null;

            //
            // Tabellenname - nicht übersetzen
            //
            this.Write2Log(Insert, "ChirurgenFunktionen: " + (string)oDataRow["Funktion"]);

            return _databaseLayer.InsertChirurgenFunktionen(oDataRow);
        }
        public int UpdateChirurgenFunktionen(DataRow oDataRow)
        {
            _chirurgenFunktionen = null;

            //
            // Tabellenname - nicht übersetzen
            //
            this.Write2Log(Update, "ChirurgenFunktionen: " + (string)oDataRow["Funktion"]);

            return _databaseLayer.UpdateChirurgenFunktionen(oDataRow);
        }

#endregion

#region Dateien
        public DataRow CreateDataRowDatei()
        {
            return _databaseLayer.CreateDataRowDatei();
        }
        public DataView GetDateien()
        {
            return _databaseLayer.GetDateien();
        }
        public DataView GetDateien(int ID_DateiTypen)
        {
            return _databaseLayer.GetDateien(ID_DateiTypen);
        }
        public DataRow GetDatei(int ID_Datei)
        {
            return _databaseLayer.GetDatei(ID_Datei);
        }
        public bool DeleteDatei(int ID_Dateien)
        {
            DataRow row = GetDatei(ID_Dateien);

            //
            // Tabellenname - nicht übersetzen
            //
            this.Write2Log(Delete, "Dateien " + (string)row["Dateiname"]);
            return _databaseLayer.DeleteDatei(ID_Dateien);
        }
        public bool UpdateDatei(DataRow row)
        {
            //
            // Tabellenname - nicht übersetzen
            //
            this.Write2Log(Update, "Dateien " + (string)row["Dateiname"]);
            return _databaseLayer.UpdateDatei(row);
        }
        public int InsertDatei(DataRow oDataRow)
        {
            //
            // Tabellenname - nicht übersetzen
            //
            this.Write2Log(Insert, "Dateien " + (string)oDataRow["Dateiname"]);

            return _databaseLayer.InsertDatei(oDataRow);
        }

#endregion

#region DateiTypen
        public DataRow CreateDataRowDateiTypen()
        {
            return _databaseLayer.CreateDataRowDateiTypen();
        }
        public DataView GetDateiTypen()
        {
            return _databaseLayer.GetDateiTypen();
        }
        public DataRow GetDateiTyp(int ID_DateiTypen)
        {
            return _databaseLayer.GetDateiTyp(ID_DateiTypen);
        }

        public bool DeleteDateiTyp(int ID_DateiTypen)
        {
            //
            // Tabellenname - nicht übersetzen
            //
            this.Write2Log(Delete, "DateiTypen", ID_DateiTypen);
            return _databaseLayer.DeleteDateiTyp(ID_DateiTypen);
        }
        public int InsertDateiTyp(DataRow oDataRow)
        {
            //
            // Tabellenname - nicht übersetzen
            //
            this.Write2Log(Insert, "DateiTypen: " + (string)oDataRow["DateiTyp"]);

            return _databaseLayer.InsertDateiTyp(oDataRow);
        }
        public bool UpdateDateiTyp(DataRow oDataRow)
        {
            //
            // Tabellenname - nicht übersetzen
            //
            this.Write2Log(Update, "DateiTypen: " + (string)oDataRow["DateiTyp"]);

            return _databaseLayer.UpdateDateiTyp(oDataRow);
        }

#endregion

#region NotizTypen
        public DataRow CreateDataRowNotizTypen()
        {
            return _databaseLayer.CreateDataRowNotizTypen();
        }
        public DataView GetNotizTypen(bool includeAll)
        {
            return _databaseLayer.GetNotizTypen(includeAll);
        }
        public DataRow GetNotizTypen(int ID)
        {
            return _databaseLayer.GetNotizTypen(ID);
        }

        public bool DeleteNotizTyp(int nID)
        {
            this.Write2Log(Delete, "NotizTypen", nID);
            return _databaseLayer.DeleteNotizTyp(nID);
        }
        public int InsertNotizTyp(DataRow oDataRow)
        {
            this.Write2Log(Insert, "NotizTypen: " + (string)oDataRow["Text"]);

            return _databaseLayer.InsertNotizTyp(oDataRow);
        }
        public int UpdateNotizTyp(DataRow oDataRow)
        {
            this.Write2Log(Update, "NotizTypen: " + (string)oDataRow["Text"]);

            return _databaseLayer.UpdateNotizTyp(oDataRow);
        }

#endregion

        public int GetCountUnusedSerialNumbers()
        {
            return _databaseLayer.GetCountUnusedSerialNumbers();
        }

        public void GetCountSerialNumber(string serialNumber, out int countUsed, out int countUnused)
        {
            _databaseLayer.GetCountSerialNumber(serialNumber, out countUsed, out countUnused);
        }

        public string GetFirstUnusedSerialNumber()
        {
            return _databaseLayer.GetFirstUnusedSerialNumber();
        }

        public DataView GetChirurgenSerialNumbers()
        {
            return _databaseLayer.GetChirurgenSerialNumbers();
        }
        public DataRow CreateDataRowSerialNumbers()
        {
            return _databaseLayer.CreateDataRowSerialNumbers();
        }
        public DataView GetSerialNumbers()
        {
            return _databaseLayer.GetSerialNumbers();
        }
        public bool DeleteSerialNumber(string serial)
        {
            this.Write2Log(Delete, "SerialNumber");
            return _databaseLayer.DeleteSerialNumber(serial);
        }
        public int InsertSerialNumber(DataRow row)
        {
            int n = _databaseLayer.InsertSerialNumber(row);

            if (n > 0)
            {
                this.Write2Log(Insert, "SerialNumber");
            }

            return n;
        }

#region Notizen
        public DataRow CreateDataRowNotiz()
        {
            return _databaseLayer.CreateDataRowNotiz();
        }
        public DataView GetNotizen(int ID_Chirurgen, int ID_NotizTypen)
        {
            return _databaseLayer.GetNotizen(ID_Chirurgen, ID_NotizTypen);
        }
        public DataView GetNotizen(int ID_Chirurgen)
        {
            return _databaseLayer.GetNotizen(ID_Chirurgen);
        }
        public DataRow GetNotiz(int ID_Notiz)
        {
            return _databaseLayer.GetNotiz(ID_Notiz);
        }
        public bool UpdateNotiz(DataRow _notiz)
        {
            return _databaseLayer.UpdateNotiz(_notiz);
        }

        public bool DeleteNotiz(int nID)
        {
            this.Write2Log(Delete, "Notizen", nID);
            return _databaseLayer.DeleteNotiz(nID);
        }
        public int InsertNotiz(DataRow dataRow)
        {
            this.Write2Log(Insert, "Notizen " + dataRow["Datum"].ToString());

            return _databaseLayer.InsertNotiz(dataRow);
        }
#endregion

#region AkademischeAusbildungen
        public DataRow CreateDataRowAkademischeAusbildungen()
        {
            return _databaseLayer.CreateDataRowAkademischeAusbildungen();
        }
        public DataView GetAkademischeAusbildungen(int ID_Chirurgen, int ID_AkademischeAusbildungTypen)
        {
            return _databaseLayer.GetAkademischeAusbildungen(ID_Chirurgen, ID_AkademischeAusbildungTypen);
        }
        public DataView GetAkademischeAusbildungen(int ID_Chirurgen)
        {
            return _databaseLayer.GetAkademischeAusbildungen(ID_Chirurgen);
        }
        public DataView GetAkademischeAusbildungTypen()
        {
            return _databaseLayer.GetAkademischeAusbildungTypen();
        }
        public DataRow GetAkademischeAusbildung(int ID_AkademischeAusbildung)
        {
            return _databaseLayer.GetAkademischeAusbildung(ID_AkademischeAusbildung);
        }
        public bool UpdateAkademischeAusbildung(DataRow row)
        {
            return _databaseLayer.UpdateAkademischeAusbildung(row);
        }

        public bool DeleteAkademischeAusbildung(int nID)
        {
            this.Write2Log(Delete, "AkademischeAusbildungen", nID);
            return _databaseLayer.DeleteAkademischeAusbildung(nID);
        }
        public int InsertAkademischeAusbildung(DataRow dataRow)
        {
            this.Write2Log(Insert, "AkademischeAusbildungen " + dataRow["Beginn"].ToString());

            return _databaseLayer.InsertAkademischeAusbildung(dataRow);
        }
#endregion

#region TypenTemplate
        public DataRow CreateDataRowTypenTemplate(string table)
        {
            return _databaseLayer.CreateDataRowTypenTemplate(table, "Text");
        }
        public DataRow CreateDataRowTypenTemplate(string table, string text)
        {
            return _databaseLayer.CreateDataRowTypenTemplate(table, text);
        }
        public DataView GetTypenTemplate(string table, bool includeAll)
        {
            return _databaseLayer.GetTypenTemplate(table, "Text", includeAll);
        }
        public DataView GetTypenTemplate(string table, string text, bool includeAll)
        {
            return _databaseLayer.GetTypenTemplate(table, text, includeAll);
        }
        public DataRow GetTypenTemplate(string table, int ID)
        {
            return _databaseLayer.GetTypenTemplate(table, "Text", ID);
        }
        public DataRow GetTypenTemplate(string table, string text, int ID)
        {
            return _databaseLayer.GetTypenTemplate(table, text, ID);
        }

        public bool DeleteTypenTemplate(string table, int nID)
        {
            this.Write2Log(Delete, table, nID);
            return _databaseLayer.DeleteTypenTemplate(table, nID);
        }
        public int InsertTypenTemplate(string table, DataRow oDataRow)
        {
            this.Write2Log(Insert, table + ": " + (string)oDataRow["Text"]);

            return _databaseLayer.InsertTypenTemplate(table, "Text", oDataRow);
        }
        public int InsertTypenTemplate(string table, string text, DataRow oDataRow)
        {
            this.Write2Log(Insert, table + ": " + (string)oDataRow[text]);

            return _databaseLayer.InsertTypenTemplate(table, text, oDataRow);
        }
        public int UpdateTypenTemplate(string table, DataRow oDataRow)
        {
            this.Write2Log(Update, table + ": " + (string)oDataRow["Text"]);

            return _databaseLayer.UpdateTypenTemplate(table, "Text", oDataRow);
        }
        public int UpdateTypenTemplate(string table, string text, DataRow oDataRow)
        {
            this.Write2Log(Update, table + ": " + (string)oDataRow[text]);

            return _databaseLayer.UpdateTypenTemplate(table, text, oDataRow);
        }

#endregion

#region Gebiete
        public DataRow CreateDataRowGebiet()
        {
            return _databaseLayer.CreateDataRowGebiet();
        }
        public DataRow GetGebiet(int ID_Gebiete)
        {
            return _databaseLayer.GetGebiet(ID_Gebiete);
        }
        public bool UpdateGebiet(DataRow _gebiet)
        {
            return _databaseLayer.UpdateGebiet(_gebiet);
        }

        public bool DeleteGebiet(int nID)
        {
            this.Write2Log(Delete, "Gebiete", nID);
            return _databaseLayer.DeleteGebiet(nID);
        }
        public int InsertGebiet(DataRow dataRow)
        {
            this.Write2Log(Insert, "Gebiete " + dataRow["Gebiet"].ToString());

            return _databaseLayer.InsertGebiet(dataRow);
        }
#endregion


#region Database Updates

        protected internal void GetDatabaseChanges(StringBuilder sb)
        {
            DatabaseLayer.GetDatabaseChanges(DatabaseMajor, DatabaseMinor, sb);
        }

        protected internal bool TryUpdate(ref string strError)
        {
            bool bSuccess = true;

            // If bSuccess is false then the version will not have been updated

            //if (_nDatabaseMajor == 1 && _nDatabaseMinor == 6)
            //{
            //    bSuccess = DatabaseLayer.UpdateV1_6ToV1_7(ref strError);
            //    ReadDatabaseVersion();
            //}
            //if (_nDatabaseMajor == 1 && _nDatabaseMinor == 7)
            //{
            //    bSuccess = DatabaseLayer.UpdateV1_7ToV1_8(ref strError);
            //    ReadDatabaseVersion();
            //}
            if (DatabaseMajor == 1 && DatabaseMinor == 8)
            {
                bSuccess = bSuccess && DatabaseLayer.UpdateV1_8ToV1_9(ref strError);
                ReadDatabaseVersion();
            }
            if (DatabaseMajor == 1 && DatabaseMinor == 9)
            {
                bSuccess = bSuccess && DatabaseLayer.UpdateV1_9ToV1_10(ref strError);
                ReadDatabaseVersion();
            }
            if (DatabaseMajor == 1 && DatabaseMinor == 10)
            {
                bSuccess = bSuccess && DatabaseLayer.UpdateV1_10ToV1_11(ref strError);
                ReadDatabaseVersion();
            }
            if (DatabaseMajor == 1 && DatabaseMinor == 11)
            {
                bSuccess = bSuccess && DatabaseLayer.UpdateV1_11ToV1_12(ref strError);
                ReadDatabaseVersion();
            }
            if (DatabaseMajor == 1 && DatabaseMinor == 12)
            {
                bSuccess = bSuccess && DatabaseLayer.UpdateV1_12ToV1_13(ref strError);
                ReadDatabaseVersion();
            }
            if (DatabaseMajor == 1 && DatabaseMinor == 13)
            {
                bSuccess = bSuccess && DatabaseLayer.UpdateV1_13ToV1_14(ref strError);
                ReadDatabaseVersion();
            }
            if (DatabaseMajor == 1 && DatabaseMinor == 14)
            {
                bSuccess = bSuccess && DatabaseLayer.UpdateV1_14ToV1_15(ref strError);
                ReadDatabaseVersion();
            }
            if (DatabaseMajor == 1 && DatabaseMinor == 15)
            {
                bSuccess = bSuccess && DatabaseLayer.UpdateV1_15ToV1_16(ref strError);
                ReadDatabaseVersion();
            }
            if (DatabaseMajor == 1 && DatabaseMinor == 16)
            {
                bSuccess = bSuccess && DatabaseLayer.UpdateV1_16ToV1_17(ref strError);
                ReadDatabaseVersion();
            }
            if (DatabaseMajor == 1 && DatabaseMinor == 17)
            {
                bSuccess = bSuccess && DatabaseLayer.UpdateV1_17ToV1_18(ref strError);
                bSuccess = bSuccess && UpdateV1_17ToV1_18();
                ReadDatabaseVersion();
            }

            if (DatabaseMajor == 1 && DatabaseMinor == 18)
            {
                bSuccess = bSuccess && DatabaseLayer.UpdateV1_18ToV1_19(ref strError);
                bSuccess = bSuccess && UpdateV1_18ToV1_19();
                ReadDatabaseVersion();
            }
            if (DatabaseMajor == 1 && DatabaseMinor == 19)
            {
                bSuccess = bSuccess && DatabaseLayer.UpdateV1_19ToV1_20(ref strError);
                ReadDatabaseVersion();
            }
            if (DatabaseMajor == 1 && DatabaseMinor == 20)
            {
                bSuccess = bSuccess && DatabaseLayer.UpdateV1_20ToV1_21(ref strError);
                ReadDatabaseVersion();
            }
            if (DatabaseMajor == 1 && DatabaseMinor == 21)
            {
                bSuccess = bSuccess && DatabaseLayer.UpdateV1_21ToV1_22(ref strError);
                ReadDatabaseVersion();
            }
            if (DatabaseMajor == 1 && DatabaseMinor == 22)
            {
                bSuccess = bSuccess && DatabaseLayer.UpdateV1_22ToV1_23(ref strError);
                ReadDatabaseVersion();
            }
            if (DatabaseMajor == 1 && DatabaseMinor == 23)
            {
                bSuccess = bSuccess && DatabaseLayer.UpdateV1_23ToV1_24(ref strError);
                ReadDatabaseVersion();
            }
            if (DatabaseMajor == 1 && DatabaseMinor == 24)
            {
                bSuccess = bSuccess && DatabaseLayer.UpdateV1_24ToV1_25(ref strError);
                ReadDatabaseVersion();
            }
            if (DatabaseMajor == 1 && DatabaseMinor == 25)
            {
                bSuccess = bSuccess && DatabaseLayer.UpdateV1_25ToV1_26(ref strError);
                ReadDatabaseVersion();
            }
            if (DatabaseMajor == 1 && DatabaseMinor == 26)
            {
                bSuccess = bSuccess && DatabaseLayer.UpdateV1_26ToV1_27(ref strError);
                ReadDatabaseVersion();
            }
            if (DatabaseMajor == 1 && DatabaseMinor == 27)
            {
                bSuccess = bSuccess && DatabaseLayer.UpdateV1_27ToV1_28(ref strError);
                ReadDatabaseVersion();
            }
            if (DatabaseMajor == 1 && DatabaseMinor == 28)
            {
                bSuccess = bSuccess && DatabaseLayer.UpdateV1_28ToV1_29(ref strError);
                ReadDatabaseVersion();
            }

            if (bSuccess)
            {
                //ShowChangeLog();
            }

            return bSuccess;
        }

        private bool UpdateV1_17ToV1_18()
        {
            //
            // Delete Bedienungsanleitung.pdf. There is a new version. It will be downloaded again when the file is missing.
            // Force this download
            //
            string filename = PathDokumente + System.IO.Path.DirectorySeparatorChar + BedienungsanleitungPdf;
            try
            {
                File.Delete(filename);
            }
            catch 
            { 
                // do nothing if this fails
            }

            return true;
        }
        private bool UpdateV1_18ToV1_19()
        {
            //
            // Delete Bedienungsanleitung.pdf. There is a new version. It will be downloaded again when the file is missing.
            // Force this download
            //
            string filename = PathDokumente + System.IO.Path.DirectorySeparatorChar + BedienungsanleitungPdf;
            try
            {
                File.Delete(filename);
            }
            catch
            {
                // do nothing if this fails
            }

            return true;
        }

#endregion

#region ChirurgenGebiete
        public DataView GetChirurgenGebiete(int ID_Chirurgen)
        {
            return DatabaseLayer.GetChirurgenGebiete(ID_Chirurgen);
        }

#endregion

#region Access Database specific
#if false
        public bool CompactAccessDB(string strTempFileName)
        {
            bool bSuccess = false;

            bSuccess = base.CompactAccessDB(strTempFileName, Password);

            return bSuccess;
        }
#endif
#endregion

        /// <summary>
        /// Damit mann sich anmelden kann, muss ein Chirurg vorhanden sein!
        /// Das ist in allen Versionen so.
        /// Wenn es also noch keinen gibt, dann kommt jetzt der
        /// "Chirurg anlegen-Dialog" hoch
        /// </summary>
        /// <returns></returns>
        public bool InitialSurgeonCheck()
        {
            bool success = true;
            long count;

            count = GetCustomerDataCount();
            if (count == 0)
            {
                CreateCustomerDataWizard dlg = new CreateCustomerDataWizard(this);
                dlg.ShowDialog();
                if (!dlg.GetSuccess())
                {
                    success = false;
                }
            }

            if (success)
            {
                // Früher war das System anfangs leer. Jetzt ist der Demobenutzer 
                // Benutzer=mustermann, Kennwort=start bereits angelegt.
                // Trotzdem soll man beim ersten Starten einen eigenen Benutzer anlegen.
                //
                // 13.11.2007: Wenn man den Mustermann löscht, und nur einen Benutzer anlegt, dann
                // darf man nicht gezwungen werden, einen weiteren anzulegen!
                count = GetChirurgenCount();
                if (count == 0)
                {
                    ChirurgWizard dlg = new ChirurgWizard(this);
                    dlg.ShowDialog();
                    if (!dlg.GetSuccess())
                    {
                        success = false;
                    }
                }
            }

            return success;
        }

#region Options
        public void SetWebProxy(System.Net.HttpWebRequest webRequest)
        {
            switch (GetUserSettingsString(GlobalConstants.SectionProxy, GlobalConstants.KeyProxyMode))
            {
                case GlobalConstants.ValueProxyModeIE:
                default:
                    IWebProxy iwebProxy = (IWebProxy)WebRequest.DefaultWebProxy;
                    iwebProxy.Credentials = CredentialCache.DefaultNetworkCredentials;
                    webRequest.Proxy = iwebProxy;
                    break;

                case GlobalConstants.ValueProxyModeUser:
                    string address = GetUserSettingsString(GlobalConstants.SectionProxy, GlobalConstants.KeyProxyUserAddress);
                    string user = ConfigProxyUser;
                    string password = ConfigProxyPassword;
                    string domain = ConfigProxyDomain;

                    WebProxy webProxy = new WebProxy(address);
                    webProxy.Credentials = new System.Net.NetworkCredential(user, password, domain);
                    webRequest.Proxy = webProxy;
                    break;

                case GlobalConstants.ValueProxyModeNone:
                    webRequest.Proxy = null;
                    break;
            }
        }

        public static bool IsValidUICulture(string culture)
        {
            return culture == "de-DE"
                || culture == "en-US";
        }

        public string ConfigProxyUser
        {
            get { return Decrypt(GetUserSettingsString(GlobalConstants.SectionProxy, GlobalConstants.KeyProxyUserUser)); }
            set { SaveUserSettings(GlobalConstants.SectionProxy, GlobalConstants.KeyProxyUserUser, Encrypt(value)); }
        }
        public string ConfigProxyPassword
        {
            get { return Decrypt(GetUserSettingsString(GlobalConstants.SectionProxy, GlobalConstants.KeyProxyUserPassword)); }
            set { SaveUserSettings(GlobalConstants.SectionProxy, GlobalConstants.KeyProxyUserPassword, Encrypt(value)); }
        }
        public string ConfigProxyDomain
        {
            get { return Decrypt(GetUserSettingsString(GlobalConstants.SectionProxy, GlobalConstants.KeyProxyUserDomain)); }
            set { SaveUserSettings(GlobalConstants.SectionProxy, GlobalConstants.KeyProxyUserDomain, Encrypt(value)); }
        }

#endregion

        public string GetEmbeddedStringResource(string embeddedResourceName)
        {
            string xmlkey = string.Empty;
            Stream inStream = null;
            StreamReader reader = null;

            try
            {
                // Get the XML content from the embedded XML public key.
                inStream = typeof(BusinessLayer).Assembly.GetManifestResourceStream(embeddedResourceName);

                // Read-in the XML content.
                reader = new StreamReader(inStream);
                xmlkey = reader.ReadToEnd();
            }
            catch
            {
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (inStream != null)
                {
                    inStream.Close();
                }
            }

            return xmlkey;
        }

        /// <summary>
        /// Löscht die Tabelle Operationen komplett. Danach kann man dsen OPS-Katalog neu importieren
        /// </summary>
        public void DeleteOPSKatalog()
        {
            Write2Log(Delete, "Operationen");
            _databaseLayer.DeleteOPSKatalog();
        }

        public bool CurrentUserHasOpenComments()
        {
            long count = DatabaseLayer.CheckForOpenComments(CurrentUser_ID_Chirurgen);

            return count > 0;
        }

#region Suggest
        public DataView SuggestGetOPSKodeTextForKode(string opsKode, int top)
        {
            return DatabaseLayer.SuggestGetOPSKodeTextForKode(opsKode, top);
        }
        public DataView SuggestGetOPSKodeTextForText(string opsKode, int top)
        {
            return DatabaseLayer.SuggestGetOPSKodeTextForText(opsKode, top);
        }
        public DataView SuggestGetOPSKodeTextForKodeOrText(string opsKode, int top)
        {
            return DatabaseLayer.SuggestGetOPSKodeTextForKodeOrText(opsKode, top);
        }
#endregion


#region KlinischeErgebnisse
        public long GetKlinischeErgebnisseAnzahl(int ID_Chirurgen, int ID_OPFunktionen, int ID_KlinischeErgebnisseTypen, int quelle, string opsKode, DateTime? from, DateTime? to)
        {
            return _databaseLayer.GetKlinischeErgebnisseAnzahl(ID_Chirurgen, ID_OPFunktionen, ID_KlinischeErgebnisseTypen, quelle, opsKode, from, to);
        }
#endregion

        private string GetTextControlMissingFile(string filename)
        {
            return string.Format(CultureInfo.InvariantCulture, GetText("err_missing_file"), filename);
        }

        internal void ShowChangeLog()
        {
            try
            {
                string filename = Application.StartupPath + System.IO.Path.DirectorySeparatorChar + "changelog.txt";
                if (File.Exists(filename))
                {
                    System.Diagnostics.Process.Start("notepad.exe", filename);
                }
                else
                {
                    MessageBox(GetTextControlMissingFile(filename));
                }
            }
            catch
            {
            }
        }
#region Abteilungen

        public DataView GetChirurgenNichtInAbteilung(int ID_Abteilungen)
        {
            return _databaseLayer.GetChirurgenNichtInAbteilung(ID_Abteilungen);
        }
        public DataView GetChirurgenVonAbteilung(int ID_Abteilungen)
        {
            return _databaseLayer.GetChirurgenVonAbteilung(ID_Abteilungen);
        }
        public bool DeleteAbteilungenChirurgen(int ID_AbteilungenChirurgen)
        {
            return _databaseLayer.DeleteAbteilungenChirurgen(ID_AbteilungenChirurgen);
        }
        public bool InsertAbteilungenChirurgen(int ID_Abteilungen, int ID_Chirurgen)
        {
            return _databaseLayer.InsertAbteilungenChirurgen(ID_Abteilungen, ID_Chirurgen);
        }
#endregion

#region Weiterbilder
        public DataView GetWeiterbilder()
        {
            return _databaseLayer.GetWeiterbilder();
        }
        public DataView GetChirurgenNotInWeiterbilder(int ID_Weiterbilder)
        {
            return _databaseLayer.GetChirurgenNotInWeiterbilder(ID_Weiterbilder);
        }
        public DataView GetChirurgenOfWeiterbilder(int ID_Weiterbilder)
        {
            return _databaseLayer.GetChirurgenOfWeiterbilder(ID_Weiterbilder);
        }
        public bool DeleteWeiterbilderChirurgen(int ID_WeiterbilderChirurgen)
        {
            return _databaseLayer.DeleteWeiterbilderChirurgen(ID_WeiterbilderChirurgen);
        }
        public bool InsertWeiterbilderChirurgen(int ID_Weiterbilder, int ID_Chirurgen)
        {
            return _databaseLayer.InsertWeiterbilderChirurgen(ID_Weiterbilder, ID_Chirurgen);
        }
#endregion

#region SecGroupsChirurgen
        public DataView GetChirurgenNotInSecGroup(int ID_SecGroup)
        {
            return _databaseLayer.GetChirurgenNotInSecGroup(ID_SecGroup);
        }
        public DataView GetChirurgenOfSecGroup(int ID_SecGroup)
        {
            return _databaseLayer.GetChirurgenOfSecGroup(ID_SecGroup);
        }
        public DataView GetChirurgenAlle()
        {
            return _databaseLayer.GetChirurgenAlle();
        }
        
        public bool DeleteSecGroupsChirurgen(int ID_SecGroupsChirurgen)
        {
            return _databaseLayer.DeleteSecGroupsChirurgen(ID_SecGroupsChirurgen);
        }
        public bool InsertSecGroupsChirurgen(int ID_SecGroup, int ID_Chirurgen)
        {
            return _databaseLayer.InsertSecGroupsChirurgen(ID_SecGroup, ID_Chirurgen);
        }
#endregion

#region SecRights
        public DataRow GetSecRight(int ID_SecRights)
        {
            return _databaseLayer.GetSecRight(ID_SecRights);
        }
#endregion

#region SecGroupsSecRights
        private DataView AddDescriptionToSecRights(DataView dataView)
        {
            //
            // SecRights contains no desription, must add from string resource
            //
            foreach (DataRow row in dataView.Table.Rows)
            {
                string name = (string)row["Name"];
                row["Description"] = OperationenLogbuchView.TheMainWindow.GetSecurityRightDescription(name);
            }

            dataView.Table.DefaultView.Sort = "[Description] asc";
            DataTable table = dataView.Table.DefaultView.ToTable();
            DataView dataView2 = new DataView(table);

            return dataView2;
        }

        public DataView GetSecRightsNotInSecGroup(int ID_SecGroups)
        {
            DataView dataView = _databaseLayer.GetSecRightsNotInSecGroup(ID_SecGroups);
            DataView dataView2 = AddDescriptionToSecRights(dataView);

            return dataView2;
        }

        public DataView GetSecRightsOfSecGroup(int ID_SecGroups)
        {
            DataView dataView = _databaseLayer.GetSecRightsOfSecGroup(ID_SecGroups);
            DataView dataView2 = AddDescriptionToSecRights(dataView);

            return dataView2;
        }

        public bool DeleteSecGroupsSecRights(int ID_SecGroupsChirurgen)
        {
            return _databaseLayer.DeleteSecGroupsSecRights(ID_SecGroupsChirurgen);
        }
        public bool InsertSecGroupsSecRights(int ID_SecGroups, int ID_SecRights)
        {
            return _databaseLayer.InsertSecGroupsSecRights(ID_SecGroups, ID_SecRights);
        }
        public DataView GetRightsOfUser(int ID_Chirurgen)
        {
            DataView dataView = _databaseLayer.GetRightsOfUser(ID_Chirurgen);
            DataView dataView2 = AddDescriptionToSecRights(dataView);

            return dataView2;
        }

        public long CountAbandonedSecRightsWithoutUser(int ID_Chirurgen, List<string> abandonedRights)
        {
            return _databaseLayer.CountAbandonedSecRightsWithoutUser(ID_Chirurgen, abandonedRights);
        }

        public long CountSecRightAssignmentsWithout(int ID_SecGroupsSecRights, int ID_SecRights)
        {
            return _databaseLayer.CountSecRightAssignmentsWithout(ID_SecGroupsSecRights, ID_SecRights);
        }

        public void ReplaceSecRightNameByDescription(List<string> rights)
        {
            for (int i = 0; i < rights.Count; i++)
            {
                string description = OperationenLogbuchView.TheMainWindow.GetSecurityRightDescription(rights[i]);
                rights[i] = description;
            }
        }

        public bool ExistsAbandonedSecRightWithoutSecGroupsChirurgen(int ID_SecGroupsChirurgen, List<string> abandonedRights)
        {
            bool abandoned = _databaseLayer.ExistsAbandonedSecRightWithoutSecGroupsChirurgen(ID_SecGroupsChirurgen, abandonedRights);

            //
            // abandoned can be true due to user abort, so check the list only
            //
            ReplaceSecRightNameByDescription(abandonedRights);

            return abandoned;
        }

        public DataView GetRollenOfUser(int ID_Chirurgen)
        {
            return _databaseLayer.GetRollenOfUser(ID_Chirurgen);
        }
        public DataView GetAbteilungenOfUser(int ID_Chirurgen)
        {
            return _databaseLayer.GetAbteilungenOfUser(ID_Chirurgen);
        }
        public DataView GetWeiterbilderOfUser(int ID_Chirurgen)
        {
            return _databaseLayer.GetWeiterbilderOfUser(ID_Chirurgen);
        }
        public DataView GetWeiterzubildendeOfUser(int ID_Chirurgen)
        {
            return _databaseLayer.GetWeiterzubildendeOfUser(ID_Chirurgen);
        }
#endregion

        public void ExportOperationenToTextFileOrbis(string fileName)
        {
            DatabaseLayer.ExportOperationenToTextFileOrbis(fileName);
        }

        public bool InitializeMSAccessDb(string strAppPath, string strServerPath, string strAccessDbName)
        {
            string strProvider = "Provider=Microsoft.Jet.OLEDB.4.0";

            return InitializeMSAccessDb(strAppPath, strServerPath, strAccessDbName, strProvider);
        }

        public bool InitializeMSAccessDbAcc(string strAppPath, string strServerPath, string strAccessDbName)
        {
            //
            // "Provider=Microsoft.ACE.OLEDB.12.0; Jet OLEDB:Database Password=" + Password + "; Data Source=" + DatabasePath;
            //
            string strProvider = "Provider=Microsoft.ACE.OLEDB.12.0";

            return InitializeMSAccessDb(strAppPath, strServerPath, strAccessDbName, strProvider);
        }

        private bool InitializeMSAccessDb(string strAppPath, string strServerPath, string strAccessDbName, string strProvider)
        {
            bool bSuccess = false;

            try
            {
                AppPath = System.IO.Path.GetFullPath(strAppPath);
                ServerDir = System.IO.Path.GetFullPath(strServerPath);

                DatabasePath = strServerPath + Path.DirectorySeparatorChar + strAccessDbName;
                DatabasePath = System.IO.Path.GetFullPath(DatabasePath);

                if (!File.Exists(DatabasePath))
                {
                    DisplayError(string.Format(GetText("err_missing_database"), DatabasePath));
                    goto Exit;
                }
                else
                {
                    string strDataSource = strProvider + "; Jet OLEDB:Database Password=" + Password + "; Data Source=" + DatabasePath;
                    SetDatabaseLayer(new DatabaseAccess(this, DatabaseType.MSAccess, strDataSource));
                    bSuccess = true;
                }
            }
            catch (Exception e)
            {
                DisplayError(e, string.Format(GetText("err_missing_database2"), strAccessDbName));
            }

        Exit:
            return bSuccess;
        }

        public bool InitializeSQLServer(string strAppPath, string strServerPath, string connectionString)
        {
            bool bSuccess = false;

            try
            {
                AppPath = System.IO.Path.GetFullPath(strAppPath);
                ServerDir = System.IO.Path.GetFullPath(strServerPath);

                //strDataSource = "Trusted_Connection=Yes;Data Source=" + strServername + "\\SQLExpress;Initial catalog=Operationen;User ID=Operationen;Password=Operationen";
                string strDataSource = connectionString;
                DatabasePath = "SQLServer";
                SetDatabaseLayer(new DatabaseSqlServer(this, DatabaseType.MSSqlServer, strDataSource));
                bSuccess = true;
            }
            catch (Exception e)
            {
                DisplayError(e, string.Format(GetText("err_missing_db_operationen")));
            }
            return bSuccess;
        }

        public bool InitializeMySql(string strAppPath, string strServerPath, string connectionString)
        {
            bool bSuccess = false;

            try
            {
                AppPath = System.IO.Path.GetFullPath(strAppPath);
                ServerDir = System.IO.Path.GetFullPath(strServerPath);

                string strDataSource = connectionString;
                DatabasePath = "MySql";
                SetDatabaseLayer(new DatabaseMySql(this, DatabaseType.MySql, strDataSource));
                bSuccess = true;
            }
            catch (Exception e)
            {
                DisplayError(e, string.Format(GetText("err_missing_db_operationen")));
            }
            return bSuccess;
        }

        public bool InitializeOracle(string strAppPath, string strServerPath, string connectionString)
        {
            bool bSuccess = false;

            try
            {
                AppPath = System.IO.Path.GetFullPath(strAppPath);
                ServerDir = System.IO.Path.GetFullPath(strServerPath);

                string strDataSource = connectionString;
                DatabasePath = "OracleXE";
                SetDatabaseLayer(new DatabaseLayer(this, DatabaseType.OracleXE, strDataSource));
                bSuccess = true;
            }
            catch (Exception e)
            {
                DisplayError(e, string.Format(GetText("err_missing_db_operationen")));
            }
            return bSuccess;
        }

        //
        // In der Registry steht das Datum, an dem das Programm zuletzt gestartet wurde. Dieses wird aktualisiert.
        // Wenn das Rechnerdatum zurückgesetzt wurde, dann gibt es einen Fehler.
        //
        public bool CheckDate()
        {
            //
            // Zahl ist die Anzahl der Tage
            // Monat: 12*40 = 480
            // Tag:            31
            // gibt höchstens 511
            //
            bool success = true;
            RegistryKey logbuch = null;

            try
            {
                RegistryKey root = Microsoft.Win32.Registry.CurrentUser;

                string pathLogbuch = @"SOFTWARE\\" + Setup.SetupData.REG_KEY_LOGBUCH;

                logbuch = root.CreateSubKey(pathLogbuch);

                if (logbuch != null)
                {
                    DateTime now = DateTime.Now;
                    int date = now.Year * 550 + now.Month * 40 + now.Day;
                    int regDate = (int)logbuch.GetValue(Setup.SetupData.REG_ENTRY_SECURITY, 0);

                    if (date > regDate)
                    {
                        logbuch.SetValue(Setup.SetupData.REG_ENTRY_SECURITY, date, RegistryValueKind.DWord);
                    }

                    if (date < regDate)
                    {
                        success = false;
                        DisplayError(GetText("err_date_manipulated"));
                    }
                }
            }
            catch
            {
                //
                // Das Schreiben in die Registry habe ich nicht verstanden. Nach HKLM kann man nicht schreiben.
                // Falls dabei etwas schief geht, soll bloss keine Meldung kommen, das wäre peinlich!
                //
                success = true;
            }
            finally
            {
                if (logbuch != null)
                {
                    logbuch.Close();
                }
            }

            return success;
        }

#region RichtlinienSoll
        public DataRow CreateDataRowGebieteSoll()
        {
            return _databaseLayer.CreateDataRowGebieteSoll();
        }

        public DataRow CreateDataRowRichtlinienSoll()
        {
            return _databaseLayer.CreateDataRowRichtlinienSoll();
        }

        public DataView GetGebieteSoll(int ID_Chirurgen, int ID_Gebiete)
        {
            return _databaseLayer.GetGebieteSoll(ID_Chirurgen, ID_Gebiete);
        }

        public DataRow GetGebieteSoll(int ID_GebieteSoll)
        {
            return _databaseLayer.GetGebieteSoll(ID_GebieteSoll);
        }

        public DataRow GetRichtlinienSoll(int ID_GebieteSoll, int ID_Richtlinie)
        {
            return _databaseLayer.GetRichtlinienSoll(ID_GebieteSoll, ID_Richtlinie);
        }

        public int InsertGebieteSoll(DataRow row)
        {
            int result = _databaseLayer.InsertGebieteSoll(row);
            if (result > 0)
            {
                this.Write2Log(Insert, "GebieteSoll " + row["ID_GebieteSoll"]);
            }
            return result;
        }

        public int InsertRichtlinienSoll(DataRow row)
        {
            int result = _databaseLayer.InsertRichtlinienSoll(row);
            this.Write2Log(Insert, "RichtlinienSoll " + row["ID_RichtlinienSoll"]);
            return result;
        }

        public bool DeleteGebieteSoll(int ID_GebietSoll)
        {
            bool result = _databaseLayer.DeleteGebieteSoll(ID_GebietSoll);
            this.Write2Log(Delete, "GebieteSoll " + ID_GebietSoll);
            return result;
        }

        public bool UpdateRichtlinienSollSoll(DataRow row)
        {
            return _databaseLayer.UpdateRichtlinienSollSoll(row);
        }

        public bool UpdateGebieteSoll(DataRow row)
        {
            return _databaseLayer.UpdateGebieteSoll(row);
        }
        public bool SetRichtlinienSoll(int ID_GebieteSoll, int ID_Richtlinie, int soll)
        {
            return _databaseLayer.SetRichtlinienSoll(ID_GebieteSoll, ID_Richtlinie, soll);
        }

#endregion

        /// <summary>
        /// Check if a plugin is a valid type.
        /// Plugins loaded with ReflectionOnlyLoadFrom() are of type System.ReflectionOnlyType
        /// Normal runtime types are of type System.RuntimeType
        /// Even if the type seems the same, it is not!
        /// </summary>
        /// <param name="type">A type. This must be a subclass of O perationenImport or O perationenImportEx</param>
        /// <returns></returns>
        internal bool IsValidPlugin(Type type)
        {
            bool success = false;

            while (type != null && type.BaseType != null)
            {
                if (type.GUID == typeof(OperationenImport).GUID)
                {
                    //
                    // If type is OperationenImport or O perationenImportEx, then we have the O perationen.O perationenImport.dll
                    // This only contains the interface but no plugins
                    //
                    break;
                }

                if ((type.BaseType.GUID == typeof(OperationenImport).GUID) && (type.BaseType.FullName == typeof(OperationenImport).FullName))
                {
                    success = true;
                    break;
                }
                type = type.BaseType;
            }

            return success;
        }

        internal static void ShowDebugWindow()
        {
            DebugLogging.ShowDebugWindow(BusinessLayer.ProgramTitle, null, false);
        }

        /// <summary>
        /// Copy a file trying as hard as you can with user interaction.
        /// If copying doesn't work, pop up a AbortRetryIgnore message box.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public bool CopyFile(string fromFileName, string toFileName, string title)
        {
            bool success = true;
            bool queryUserOnError = true;

        Retry:

            if (!System.IO.File.Exists(fromFileName))
            {
                success = false;
                DisplayError(string.Format(CultureInfo.InvariantCulture, GetText("err_missing_file"), fromFileName));
                goto _exit;
            }

            try
            {
                System.IO.File.Copy(fromFileName, toFileName, true);
            }
            catch
            {
                try
                {
                    // Kopieren hat nicht geklappt, evtl. ist die Datei schreibgeschützt, 
                    // also den Schreibschutz entfernen.
                    System.IO.File.SetAttributes(toFileName, FileAttributes.Normal);
                    System.IO.File.Copy(fromFileName, toFileName, true);
                }
                catch
                {
                    DialogResult result = DialogResult.Abort;

                    if (queryUserOnError)
                    {
                        result = System.Windows.Forms.MessageBox.Show(
                            string.Format(CultureInfo.InvariantCulture, GetText("err_copy_file"), fromFileName, toFileName), title,
                            MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, (MessageBoxOptions)0);
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

        /// <summary>
        /// Try hard to copy a file
        /// </summary>
        /// <param name="src">The source file to copy</param>
        /// <param name="dst">The target file name</param>
        /// <param name="silent">Display Messaboxes or not</param>
        /// <param name="queryUserOnError">Pop up a confirm box on error and query user for retry</param>
        /// <returns></returns>
        public bool CopyFile(string fromFileName, string toFileName, bool silent, bool queryUserOnError)
        {
            bool success = true;

        Retry:

            if (!System.IO.File.Exists(fromFileName))
            {
                success = false;
                if (!silent)
                {
                    DisplayError(string.Format(CultureInfo.InvariantCulture, GetText("err_missing_file"), fromFileName));
                }
                goto _exit;
            }

            try
            {
                System.IO.File.Copy(fromFileName, toFileName, true);
            }
            catch
            {
                try
                {
                    // Kopieren hat nicht geklappt, evtl. ist die Datei schreibgeschützt, 
                    // also den Schreibschutz entfernen.
                    System.IO.File.SetAttributes(toFileName, FileAttributes.Normal);
                    System.IO.File.Copy(fromFileName, toFileName, true);
                }
                catch
                {
                    DialogResult result = DialogResult.Abort;

                    if (queryUserOnError)
                    {
                        result = System.Windows.Forms.MessageBox.Show(
                            string.Format(CultureInfo.InvariantCulture, GetText("err_copy_file"), fromFileName, toFileName), ProgramTitle,
                            MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, (MessageBoxOptions)0);
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

        /// <summary>
        /// Verify whether the first line of a file contains a specified magic text and version.
        /// The first line of the file must have format "magic|version".
        /// </summary>
        /// <param name="fileName">Full path and file name</param>
        /// <param name="magicText">The magic text expected in the first line</param>
        /// <param name="version"></param>
        /// <returns></returns>
        public bool CheckTextFileSignature(string fileName, string magicText, string version)
        {
            bool success = false;

            StreamReader reader = null;

            try
            {
                reader = new StreamReader(fileName);
                success = CheckTextFileSignature(reader, magicText, version);
            }
            catch
            {
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return success;
        }

        /// <summary>
        /// Verify whether the first line of a file contains a specified magic text and version.
        /// The first line of the file must have format "magic|version".
        /// </summary>
        /// <param name="reader">The reader for the file</param>
        /// <param name="magicText">The magic text expected in the first line</param>
        /// <param name="version">The expected version</param>
        /// <returns></returns>
        public bool CheckTextFileSignature(TextReader reader, string magicText, string version)
        {
            bool success = false;

            try
            {
                string line = reader.ReadLine();
                if (line != null)
                {
                    string[] arLine = line.Split('|');

                    if (arLine.Length == 2)
                    {
                        if (arLine[0] == magicText && arLine[1] == version)
                        {
                            success = true;
                        }
                    }
                }
            }
            catch
            {
                success = false;
            }

            if (!success)
            {
                DisplayError(GetText("err_fileNoReadOrBadFormat"));
            }

            return success;
        }

        public override void DebugPrint(long flag, string msg)
        {
            AppFramework.Debugging.DebugLogging.WriteLine(flag, msg);
        }
    }
}

