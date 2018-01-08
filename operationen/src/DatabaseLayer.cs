using System;
using System.Windows.Forms;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Globalization;

using Utility;
using AppFramework;
using Operationen;
using CMaurer.Operationen.AppFramework;
using Operationen.Wizards.CreateCustomerData;
using Security.Cryptography;

namespace Operationen
{
    public partial class DatabaseLayer : DatabaseLayerCommon
    {
        private BusinessLayer _businessLayer;


        public DatabaseLayer(BusinessLayer businessLayer, DatabaseType databaseType, string strConnectionString)
            : base(businessLayer, databaseType, strConnectionString)
        {
            _businessLayer = businessLayer;
        }

        private BusinessLayer BusinessLayer
        {
            get {return _businessLayer; }
        }

        protected DialogResult MessageBox(string strText)
        {
            string strTitle = BusinessLayer.ProgramTitle;

            return System.Windows.Forms.MessageBox.Show(strText, strTitle);
        }

        #region ChirurgenGebiete
        public bool InsertUpdateChirurgenGebiete(int ID_Chirurgen,
            Dictionary<int, DateTime?> gebieteVon,
            Dictionary<int, DateTime?> gebieteBis)
        {
            string sql = @"
                DELETE FROM ChirurgenGebiete
                WHERE
                    ID_Chirurgen=@ID_Chirurgen
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
            ExecuteNonQuery(sql, sqlParameters);

            foreach (int ID_Gebiete in gebieteVon.Keys)
            {
                if (gebieteVon[ID_Gebiete] != null || gebieteBis[ID_Gebiete] != null)
                {
                    StringBuilder sb = new StringBuilder(@"
                    INSERT INTO ChirurgenGebiete
                    (
                    ID_Chirurgen,
                    ID_Gebiete,
                    GebietVon,
                    GebietBis
                    )
                    VALUES
                    (
                     @ID_Chirurgen,
                     @ID_Gebiete,
                     @GebietVon,
                     @GebietBis
                    )
                ");

                    sb.Replace("@GebietVon", this.NullableDateTime2DBDateString(gebieteVon[ID_Gebiete]));
                    sb.Replace("@GebietBis", this.NullableDateTime2DBDateString(gebieteBis[ID_Gebiete]));

                    sqlParameters = new ArrayList();
                    sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                    sqlParameters.Add(this.SqlParameter("@ID_Gebiete", ID_Gebiete));

                    InsertRecord(sb.ToString(), sqlParameters, "ChirurgenGebiete");
                }
            }

            return true;
        }

        #endregion

        #region Chirurgen

        /// <summary>
        /// This serial has been used multiple times. Set each one but the first to "Demo".
        /// When restarting the program, a dialog will appear that allows the user to enter new valid serials.
        /// </summary>
        /// <param name="serial">The multi serial</param>
        /// <returns>The number of Records updated</returns>
        public int ReduceSerial(string serial)
        {
            StringBuilder sb = new StringBuilder(
                    @"select @@TOP@@
                        ID_Chirurgen
                    from 
                        Chirurgen 
                    where 
                        LizenzDaten=@LizenzDaten
                    ");

            HandleTopLimitStuff(sb, "1");

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(SqlParameter("@LizenzDaten", serial));

            DataRow row = GetRecord(sb.ToString(), sqlParameters, "Chirurgen");

            int ID_Chirurgen = (int)row["ID_Chirurgen"];

            string sql = @"
                    update 
                        Chirurgen
                    set
                        LizenzDaten='Demo'
                    where
                        ID_Chirurgen <> @ID_Chirurgen
                        and LizenzDaten=@LizenzDaten
                    ";

            sqlParameters = new ArrayList();
            sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));
            sqlParameters.Add(SqlParameter("@LizenzDaten", serial));

            int noUpdated = ExecuteNonQuery(sql, sqlParameters);

            _businessLayer.DebugPrint(AppFramework.Debugging.DebugLogging.DebugFlagSql, "Updated " + noUpdated + " records Chirurgen.LizenzDaten->'Demo'");

            return noUpdated;
        }

        /// <summary>
        /// Testen, dass niemand die Datenbank geändert hat, so dass man eine Seriennummer mehrfach eingeben kann.
        /// Jemand könnte das Feld auf nicht-unique ändern und mehrfach dieselbe Seriennummer kopieren.
        /// </summary>
        /// <returns></returns>
        public DataView GetDuplicateSerialNumbers()
        {
            string sql =
                    @"select 
                        count(ID_Chirurgen) as dups,
                        LizenzDaten
                    from 
                        Chirurgen 
                    where 
                        ((LizenzDaten <> 'Demo') and (LizenzDaten <> 'NoneRequired'))
                    group by
                        LizenzDaten";

            DataView dv = GetDataView(sql, null, "Chirurgen");

            return dv;
        }

        /// <summary>
        /// Anzahl der Chirurgen, die keine gültige Seriennummer haben.
        /// Zwei brauchen keine.
        /// #user   #invalid-SN     return
        /// 0       0               0
        /// 2       2               0
        /// 3       2               1
        /// </summary>
        /// <returns></returns>
        public long GetCountChirurgenWithInvalidSerialNumbers()
        {
            long invalidCount = 0;

            string sql = 
                @"
                SELECT 
                    a.Lizenzdaten
                FROM
                    Chirurgen a
                 ";

            DataView dv = GetDataView(sql, null, "Chirurgen");
            
            if (dv.Table.Rows.Count > 0)
            {
                OperationenSerial.SerialLogic sl = new OperationenSerial.SerialLogic();

                foreach (DataRow row in dv.Table.Rows)
                {
                    string serial = (string)row["Lizenzdaten"];;
                    if (!sl.ValidateSerialNumber(serial))
                    {
                        invalidCount++;
                    }
                }
            }

            return invalidCount;
        }

        public long GetChirurgenSerialCount(string serialNumber)
        {
            string sql =
                @"
                SELECT 
                    count(ID_Chirurgen)
                FROM
                    Chirurgen
                WHERE
                    Lizenzdaten=@Lizenzdaten
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@Lizenzdaten", serialNumber));

            return ExecuteScalar(sql, sqlParameters);
        }

        public bool UpdateChirurg(
            DataRow oDataRow,
            Dictionary<int, DateTime?> gebieteVon,
            Dictionary<int, DateTime?> gebieteBis)
        {

            StringBuilder sb = new StringBuilder(
                @"
                UPDATE Chirurgen
                SET
                    Anrede=@Anrede,
                    Nachname=@Nachname,
                    Vorname=@Vorname,
                    Anfangsdatum=@Anfangsdatum,
                    ID_ChirurgenFunktionen=@ID_ChirurgenFunktionen,
                    UserID=@UserID,
                    ImportID=@ImportID,
                    Aktiv=@Aktiv,
                    IstWeiterbilder=@IstWeiterbilder
                WHERE
                    ID_Chirurgen=@ID_Chirurgen
                ");

            sb.Replace("@Anfangsdatum", this.DateTime2DBDateTimeString(oDataRow["Anfangsdatum"]));

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@Anrede", (string)oDataRow["Anrede"]));
            aSQLParameters.Add(this.SqlParameter("@Nachname", (string)oDataRow["Nachname"]));
            aSQLParameters.Add(this.SqlParameter("@Vorname", (string)oDataRow["Vorname"]));
            aSQLParameters.Add(this.SqlParameter("@ID_ChirurgenFunktionen", oDataRow["ID_ChirurgenFunktionen"]));
            aSQLParameters.Add(this.SqlParameter("@UserID", (string)oDataRow["UserID"]));
            aSQLParameters.Add(this.SqlParameter("@ImportID", (string)oDataRow["ImportID"]));
            aSQLParameters.Add(this.SqlParameter("@Aktiv", oDataRow["Aktiv"]));
            aSQLParameters.Add(this.SqlParameter("@IstWeiterbilder", oDataRow["IstWeiterbilder"]));
            aSQLParameters.Add(this.SqlParameter("@ID_Chirurgen", oDataRow["ID_Chirurgen"]));

            ExecuteNonQuery(sb.ToString(), aSQLParameters);

            InsertUpdateChirurgenGebiete(ConvertToInt32(oDataRow["ID_Chirurgen"]), gebieteVon, gebieteBis);

            return true;
        }


        /// <summary>
        /// Zählen, wie oft eine ImportID schon vorkommt außer der von übergebenen Chirurgen
        /// </summary>
        /// <param name="ID_Chirurgen"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public long GetCountImportID(int ID_Chirurgen, string importID)
        {
            string sql = @"
                select
                    count(ID_Chirurgen)
                from
                    Chirurgen
                where
                    ID_Chirurgen <> @ID_Chirurgen and 
                    [ImportID]=@ImportID
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
            sqlParameters.Add(this.SqlParameter("@ImportID", importID));

            return ExecuteScalar(sql, sqlParameters);
        }

        /// <summary>
        /// Zählen, wie oft eine UserID schon vorkommt außer der von übergebenen Chirurgen
        /// </summary>
        /// <param name="ID_Chirurgen"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public long GetCountUserID(int ID_Chirurgen, string userID)
        {
            string sql = @"
                select
                    count(ID_Chirurgen)
                from
                    Chirurgen
                where
                    ID_Chirurgen <> @ID_Chirurgen and 
                    [UserID]=@UserID
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
            sqlParameters.Add(this.SqlParameter("@UserID", userID));

            return ExecuteScalar(sql, sqlParameters);
        }

        public long GetCountUserID(string userID)
        {
            string sql = @"
                select
                    count(ID_Chirurgen)
                from
                    Chirurgen
                where
                    [UserID]=@UserID
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@UserID", userID));

            return ExecuteScalar(sql, sqlParameters);
        }

        /// <summary>
        /// If you entered serial numbers and they should be used automatically, then
        /// don't query the user but take the first one.
        /// Otherwise, pop up a window for the user to enter a serial number.
        /// </summary>
        /// <returns></returns>
        private string GetSerialNumber(DataRow chirurg, ref bool insertAllowed)
        {
            string serialNumber = "";

            bool automatic = "1" == BusinessLayer.GetUserSettingsString(GlobalConstants.SectionSerialNumbers, GlobalConstants.KeySerialNumbersAutomatic);

            if (automatic)
            {
                serialNumber = BusinessLayer.GetFirstUnusedSerialNumber();
            }

            if (string.IsNullOrEmpty(serialNumber))
            {
                // Seriennummer manuell eingeben
                EnterSerialNumberView dlg = new EnterSerialNumberView(BusinessLayer, chirurg);
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    serialNumber = dlg.SerialNumber;
                    insertAllowed = true;
                }
                else
                {
                    MessageBox(GetText("error1"));
                }
            }
            else
            {
                insertAllowed = true;
            }

            return serialNumber;
        }

        ///<summary>
         ///Fügt einen neuen Chirurgen ein.
         ///Da dieses nur an genau dieser Stelle passiert, wird hier die Seriennummer überprüft
         ///und bei Bedarf abgefragt.
         ///TODO: Problem: die UserID muss eindeutig sein.
         ///Das wird vom Aufrufenden vor dem Aufruf dieser Funktion sichergestellt, 
         ///außer beim automatischen Import.
         ///</summary>
         ///<param name="oDataRow"></param>
         ///<param name="gebieteVon"></param>
         ///<param name="gebieteBis"></param>
        ///<returns></returns>
        public int InsertChirurg(
            DataRow oDataRow,
            Dictionary<int, DateTime?> gebieteVon,
            Dictionary<int, DateTime?> gebieteBis)
        {
            Dictionary<string, string> licenseData = new Dictionary<string, string>();
            bool insertAllowed = false;
            bool serialRequired = true;
            int ID_Chirurgen = -1;
            string serialNumber = "";

#if CHECK_RESTRICTIONS
            // Es können bis zu zwei Chirurgen angelegt sein ohne Lizenz und Seriennummer.
            // In der installierten Datenbank ist ein DemoChirurg vorhanden, der aber nicht zählen darf.
            // ist aber zu kompliziert, zu unterscheiden, ob der der Demodatenchirurg ist oder ein echter.
            // Daher lassen wir ohne Lizenz zwei Chirurgen zu.

            long countSerialsMissing = GetCountChirurgenWithInvalidSerialNumbers();

            //
            // Genau jetzt gibt es BusinessLayer.NumberOfFreeUsers oder mehr Benutzer, und einer soll jetzt dazu kommen
            // silent=true muss sein, denn die Lizenzdatei kann alt sein, aber es kann trotzdem noch freie Seriennummern 
            // geben, die automatisch verwendet werden können.
            //

            if (BusinessLayer.NumberOfFreeUsersIsValid && (countSerialsMissing >= BusinessLayer.NumberOfFreeUsers) && !BusinessLayer.VerifyLicense(licenseData, true))
            {
                serialRequired = true;
                serialNumber = GetSerialNumber(oDataRow, ref insertAllowed);
            }
            else
            {
                // Keiner oder erst ein Chirurg vorhanden.
                // Bis zu zwei Chirurgen kann man gratis anlegen.
                serialRequired = false;
                insertAllowed = true;
            }
#else
            insertAllowed = true;
            serialRequired = false;
#endif

            if (insertAllowed)
            {
                if (serialRequired)
                {
                    //
                    // Sicherheitshalber noch mal direkt vor dem Einfügen überprüfen,
                    // ob die Seriennummer wirklich gültig ist.
                    // Wer weiß, was in dem Dialog mit der Seriennummer Eingabe so getan
                    // oder nicht getan wurde.
                    //
                    if (!BusinessLayer.CheckSerial(oDataRow, serialNumber))
                    {
                        string text = string.Format(CultureInfo.InvariantCulture, GetText("error2"), serialNumber);
                        MessageBox(text);
                        goto exit;
                    }
                }
                else
                {
                    serialNumber = "NoneRequired";
                }

                // 'start' -> '2B020927D3C6EB407223A1BAA3D6CE3597A3F88D'
                StringBuilder sb = new StringBuilder(
                        @"
                INSERT INTO Chirurgen
                    (
                    Anrede,
                    Nachname,
                    Vorname,
                    Anfangsdatum,
                    [UserID],
                    ImportID,
                    [Password],
                    MustChangePassword,
                    ID_ChirurgenFunktionen,
                    Aktiv,
                    Lizenzdaten,
                    IstWeiterbilder
                    )
                    VALUES
                    (
                     @Anrede,
                     @Nachname,
                     @Vorname,
                     @Anfangsdatum,
                     @UserID,
                     @ImportID,
                     @Password,
                     1,
                     @ID_ChirurgenFunktionen,
                     @Aktiv,
                     @Lizenzdaten,
                     @IstWeiterbilder
                    )
                ");

                sb.Replace("@Anfangsdatum", this.DateTime2DBDateTimeString(oDataRow["Anfangsdatum"]));

                ArrayList aSQLParameters = new ArrayList();
                aSQLParameters.Add(this.SqlParameter("@Anrede", (string)oDataRow["Anrede"]));
                aSQLParameters.Add(this.SqlParameter("@Nachname", (string)oDataRow["Nachname"]));
                aSQLParameters.Add(this.SqlParameter("@Vorname", (string)oDataRow["Vorname"]));
                aSQLParameters.Add(this.SqlParameter("@UserID", (string)oDataRow["UserID"]));
                aSQLParameters.Add(this.SqlParameter("@ImportID", (string)oDataRow["ImportID"]));

                // 'start' -> '2B020927D3C6EB407223A1BAA3D6CE3597A3F88D'
                aSQLParameters.Add(this.SqlParameter("@Password", "2B020927D3C6EB407223A1BAA3D6CE3597A3F88D"));
                aSQLParameters.Add(this.SqlParameter("@ID_ChirurgenFunktionen", oDataRow["ID_ChirurgenFunktionen"]));
                aSQLParameters.Add(this.SqlParameter("@Aktiv", oDataRow["Aktiv"]));
                aSQLParameters.Add(this.SqlParameter("@Lizenzdaten", serialNumber));
                aSQLParameters.Add(this.SqlParameter("@IstWeiterbilder", oDataRow["IstWeiterbilder"]));

                ID_Chirurgen = InsertRecord(sb.ToString(), aSQLParameters, "Chirurgen");
                if (ID_Chirurgen != -1)
                {
                    if (serialRequired)
                    {
                        // Serial number serialNumber was used. Delete it and tell the web service who used it.
                        DeleteSerialNumber(serialNumber);
                    }
                    if (gebieteVon != null && gebieteBis != null)
                    {
                        InsertUpdateChirurgenGebiete(ID_Chirurgen, gebieteVon, gebieteBis);
                    }
                    // 2009-01-30 Webservice wird immer aufgerufen, nicht erst ab 3. Datensatz
                    QueryWebServiceSerialInsert(serialNumber, oDataRow);
                }
            }

            exit:
            return ID_Chirurgen;
        }

        private void QueryWebServiceSerialInsert(string serialNumber, DataRow chirurg)
        {
            QueryWebService("serial_insert", serialNumber, chirurg);
        }

        private void QueryWebServiceSerialDelete(string serialNumber, DataRow chirurg)
        {
            QueryWebService("serial_delete", serialNumber, chirurg);
        }

        /// <summary>
        /// The character used to separate parameters must not appear in any parameter,
        /// so we replace it with a space
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string RemoveColumnSeparators(string data)
        {
            string tmp = data.Replace('|', ' ');

            return tmp;
        }

        /// <summary>
        /// Seriennummer und anderes an den Web Service schicken
        /// "<operationen><data>1|005JW56UF741JLXK12U4OH05OU1IZ23Y0606|LogbuchWeiterbildung|1.14.0|CMAURER|g|g|g|Maurer|Christoph</data></operationen>"
        /// "<operationen><data>2|serial_insert|005JW56UF741JLXK12U4OH05OU1IZ23Y0606|LogbuchWeiterbildung|1.14.0|CMAURER|g|g|g|Maurer|Christoph</data></operationen>"
        /// "<operationen><data>2|serial_delete|005JW56UF741JLXK12U4OH05OU1IZ23Y0606|LogbuchWeiterbildung|1.14.0|CMAURER|g|g|g|Maurer|Christoph</data></operationen>"
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        private void QueryWebService(string command, string serialNumber, DataRow chirurg)
        {
            try
            {
                // Erster Parameter ist immer die Version
                StringBuilder sb = new StringBuilder("2|");
                sb.Append(RemoveColumnSeparators(command));
                sb.Append("|");
                sb.Append(RemoveColumnSeparators(serialNumber));
                sb.Append("|LogbuchWeiterbildung|");
                sb.Append(RemoveColumnSeparators(BusinessLayer.BareVersionString));
                sb.Append("|");
                sb.Append(RemoveColumnSeparators(System.Environment.MachineName));
                sb.Append("|");
                sb.Append(RemoveColumnSeparators(BusinessLayer.GetConfigValue(CreateCustomerDataWizardPage.KEY_CD_KRANKENHAUS)));
                sb.Append("|");
                sb.Append(RemoveColumnSeparators(BusinessLayer.GetConfigValue(CreateCustomerDataWizardPage.KEY_CD_PLZ)));
                sb.Append("|");
                sb.Append(RemoveColumnSeparators(BusinessLayer.GetConfigValue(CreateCustomerDataWizardPage.KEY_CD_STRASSE)));
                sb.Append("|");
                sb.Append(RemoveColumnSeparators((string)chirurg["Nachname"]));
                sb.Append("|");
                sb.Append(RemoveColumnSeparators((string)chirurg["Vorname"]));

                string plainText = string.Format("<operationen><data>{0}</data></operationen>", sb.ToString());
                string keyfile = BusinessLayer.GetEmbeddedStringResource("Operationen.OperationenSerialPublicKey.xml");
                string cipherText = Security.XmlCryptography.AsymmetricEncryptXmlDocument(keyfile, true, plainText, "data");

                de.op_log.www.OperationenWebService service = new de.op_log.www.OperationenWebService();

#if DEBUG
                {
                    // Der Debug-Aufruf soll zuerst kommen, damit bei einer exception nicht der www-webservice aufgerufen wird.
                    // Damit der lokale Aufruf klappt, muss der webservice im WebStudio gestartet sein.
                    string debugErrorText = string.Empty;
                    localhost.OperationenWebService localservice = new localhost.OperationenWebService();
                    // Wenn es hier eine Exception gibt, hat sich evtl. der Port geändert! Dann muss man 
                    // den Web reference refreshen
                    bool debugSuccess = localservice.RegisterLicense(cipherText, out debugErrorText);
                    localservice.RegisterLicenseOneWay(cipherText);
                }
#endif

                string errorText = string.Empty;
                // Wenn man hier eine Exception "... must use updatable query" bekommt,
                // dann hat man keine Schreibrechte auf das App_Data Verzeichnis.
                //Bei 1und1 stellt mam das unter Homepage/Anwendungen - Webfiles ein:
                // u45...: alle Rechte
                // NETWORK_SERVICE muss Schreiben-Rechte haben
                // IUSR_45... : nur Lesen
                //bool success = service.RegisterLicense(cipherText, out errorText);
                service.RegisterLicenseOneWay(cipherText);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }
        }

        /// <summary>
        /// Löscht einen Chirurgen mitsamt allen seinen Daten.
        /// Seine Seriennummer wird wieder in die Seriennummern Tabelle eingefügt.
        /// </summary>
        /// <param name="ID_Chirurgen"></param>
        /// <returns></returns>
        public bool DeleteChirurg(int ID_Chirurgen)
        {
            bool success = true;

            try
            {
                string sql;
                ArrayList sqlParameters;

                DataRow chirurg = GetChirurg(ID_Chirurgen);

                // delete and then copy the serial number: this is faster and easier than to check whether it exists...
                sql = "delete from SerialNumbers where SerialNumber=(select LizenzDaten from Chirurgen where ID_Chirurgen=@ID_Chirurgen)";
                sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                ExecuteNonQuery(sql, sqlParameters);
                Application.DoEvents();

                sql = "insert into SerialNumbers (SerialNumber) select LizenzDaten from Chirurgen "
                     + " where ID_Chirurgen=@ID_Chirurgen and (LizenzDaten <> 'Demo' and LizenzDaten <> 'NoneRequired')";
                sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                ExecuteNonQuery(sql, sqlParameters);
                Application.DoEvents();

                //
                // 1.26.0 GebieteSoll und RichtlinienSoll
                //
                sql = "DELETE FROM RichtlinienSoll WHERE ID_GebieteSoll in (select ID_GebieteSoll from GebieteSoll WHERE ID_Chirurgen=@ID_Chirurgen)";
                sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                ExecuteNonQuery(sql, sqlParameters);
                Application.DoEvents();

                sql = "DELETE FROM GebieteSoll WHERE ID_Chirurgen=@ID_Chirurgen";
                sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                ExecuteNonQuery(sql, sqlParameters);
                Application.DoEvents();

                sql = "DELETE FROM AbteilungenChirurgen WHERE ID_Chirurgen=@ID_Chirurgen";
                sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                ExecuteNonQuery(sql, sqlParameters);
                Application.DoEvents();

                sql = "DELETE FROM AkademischeAusbildungen WHERE ID_Chirurgen=@ID_Chirurgen";
                sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                ExecuteNonQuery(sql, sqlParameters);
                Application.DoEvents();

                sql = "DELETE FROM ChirurgenDokumente WHERE ID_Chirurgen=@ID_Chirurgen";
                sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                ExecuteNonQuery(sql, sqlParameters);
                Application.DoEvents();

                sql = "DELETE FROM ChirurgenGebiete WHERE ID_Chirurgen=@ID_Chirurgen";
                sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                ExecuteNonQuery(sql, sqlParameters);
                Application.DoEvents();

                sql = "DELETE FROM ChirurgenOperationen WHERE ID_Chirurgen=@ID_Chirurgen";
                sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                ExecuteNonQuery(sql, sqlParameters);
                Application.DoEvents();
                
                sql = "DELETE FROM ChirurgenRichtlinien WHERE ID_Chirurgen=@ID_Chirurgen";
                sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                ExecuteNonQuery(sql, sqlParameters);
                Application.DoEvents();

                sql = "DELETE FROM Kommentare WHERE ID_Chirurgen_Von=@ID_Chirurgen OR ID_Chirurgen_Fuer=@ID_Chirurgen";
                sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                ExecuteNonQuery(sql, sqlParameters);
                Application.DoEvents();

                sql = "DELETE FROM Notizen WHERE ID_Chirurgen=@ID_Chirurgen";
                sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                ExecuteNonQuery(sql, sqlParameters);
                Application.DoEvents();

                sql = "DELETE FROM PlanOperationen WHERE ID_Chirurgen=@ID_Chirurgen";
                sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                ExecuteNonQuery(sql, sqlParameters);
                Application.DoEvents();

                sql = "DELETE FROM SecGroupsChirurgen WHERE ID_Chirurgen=@ID_Chirurgen";
                sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                ExecuteNonQuery(sql, sqlParameters);
                Application.DoEvents();

                sql = "DELETE FROM UserSettings WHERE ID_Chirurgen=@ID_Chirurgen";
                sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                ExecuteNonQuery(sql, sqlParameters);
                Application.DoEvents();

                sql = "DELETE FROM WeiterbilderChirurgen WHERE ID_Chirurgen=@ID_Chirurgen";
                sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                ExecuteNonQuery(sql, sqlParameters);
                Application.DoEvents();

                sql = "DELETE FROM WeiterbilderChirurgen WHERE ID_Weiterbilder=@ID_Chirurgen";
                sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                ExecuteNonQuery(sql, sqlParameters);
                Application.DoEvents();

                sql = "DELETE FROM Chirurgen WHERE ID_Chirurgen=@ID_Chirurgen";
                sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                ExecuteNonQuery(sql, sqlParameters);
                Application.DoEvents();

                if (chirurg != null)
                {
                    QueryWebServiceSerialDelete((string)chirurg["Lizenzdaten"], chirurg);
                }
            }
            catch
            {
                success = false;
            }
            return success;
        }

        public DataRow GetChirurg(string strNachname, string strVorname)
        {
            string sb = 
                @"
                SELECT 
                    ID_Chirurgen
                FROM 
                    Chirurgen
                WHERE
                    Nachname=@Nachname And
                    Vorname=@Vorname
                ";

            ArrayList arSqlParameter = new ArrayList();
            arSqlParameter.Add(this.SqlParameter("@Nachname", strNachname));
            arSqlParameter.Add(this.SqlParameter("@Vorname", strVorname));

            return GetRecord(sb, arSqlParameter, "Chirurgen");
        }

        public int GetID_Chirurgen(string nachname)
        {
            return GetID_Chirurgen(nachname, null);
        }

        public int GetID_Chirurgen(string nachname, string vorname)
        {
            int nID_Chirurgen = -1;

            StringBuilder sb = new StringBuilder(
                @"
                SELECT 
                    ID_Chirurgen
                FROM 
                    Chirurgen
                WHERE
                    Nachname=@Nachname
                    @vorname
                ");

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(SqlParameter("@Nachname", nachname));

            if (vorname == null)
            {
                sb.Replace("@vorname", "");
            }
            else
            {
                sb.Replace("@vorname", "AND Vorname=@Vorname");
                sqlParameters.Add(SqlParameter("@Vorname", vorname));
            }

            DataRow oRow = GetRecord(sb.ToString(), sqlParameters, "Chirurgen");

            if (oRow != null)
            {
                nID_Chirurgen = ConvertToInt32(oRow["ID_Chirurgen"]);
            }

            return nID_Chirurgen;
        }

        public int GetID_ChirurgenByImportID(string importId)
        {
            int nID_Chirurgen = -1;

            string sql = 
                @"
                SELECT 
                    ID_Chirurgen
                FROM 
                    Chirurgen
                WHERE
                    ImportId=@ImportId
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(SqlParameter("@ImportId", importId));

            DataRow row = GetRecord(sql, sqlParameters, "Chirurgen");

            if (row != null)
            {
                nID_Chirurgen = ConvertToInt32(row["ID_Chirurgen"]);
            }

            return nID_Chirurgen;
        }


        public long GetChirurgenCountUserId(string userId)
        {
            string sql =
                @"
                SELECT 
                    count(ID_Chirurgen) 
                FROM 
                    Chirurgen
                WHERE
                    UserID=@UserID
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@UserID", userId));

            return ExecuteScalar(sql, sqlParameters);
        }

        public DataRow CreateDataRowKommentar(int nIDChirurgenVon, string strNachnameVon, int nID_ChirurgenFuer)
        {
            DataTable dt = new DataTable("Kommentare");

            dt.Columns.Add("ID_Kommentare", typeof(int));
            dt.Columns.Add("Datum", typeof(DateTime));
            dt.Columns.Add("AbschnittVon", typeof(DateTime));
            dt.Columns.Add("AbschnittBis", typeof(DateTime));
            dt.Columns.Add("ID_Chirurgen_Von", typeof(int));
            dt.Columns.Add("ID_Chirurgen_Fuer", typeof(int));
            dt.Columns.Add("KommentarVon", typeof(string));
            dt.Columns.Add("KommentarFuer", typeof(string));
            dt.Columns.Add("NachnameVon", typeof(string));
            dt.Columns.Add("NachnameFuer", typeof(string));
            dt.Columns.Add("ID_ChirurgenFunktionen", typeof(int));
            dt.Columns.Add("Funktion", typeof(string));
            dt.Columns.Add("Status", typeof(int));

            DataRow row = dt.NewRow();
            row["Datum"] = DateTime.Now;
            row["AbschnittVon"] = DateTime.Now;
            row["AbschnittBis"] = DateTime.Now;
            row["ID_Chirurgen_Von"] = nIDChirurgenVon;
            row["ID_Chirurgen_Fuer"] = nID_ChirurgenFuer;
            row["NachnameVon"] = strNachnameVon;
            row["NachnameFuer"] = "";
            row["KommentarVon"] = "";
            row["KommentarFuer"] = "";
            row["ID_ChirurgenFunktionen"] = -1;
            row["Funktion"] = "";
            row["Status"] = KommentareView.KommentarStatusBearbeitung;

            return row;
        }

        public DataRow CreateDataRowChirurg()
        {
            DataTable dt = new DataTable("Chirurgen");
            DataRow dataRow;

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("ID_Chirurgen", typeof(int)));
            dt.Columns.Add(new DataColumn("Nachname", typeof(string)));
            dt.Columns.Add(new DataColumn("Vorname", typeof(string)));
            dt.Columns.Add(new DataColumn("Anfangsdatum", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Anrede", typeof(string)));
            dt.Columns.Add(new DataColumn("UserID", typeof(string)));
            dt.Columns.Add(new DataColumn("ImportID", typeof(string)));
            dt.Columns.Add(new DataColumn("ID_ChirurgenFunktionen", typeof(int)));
            dt.Columns.Add(new DataColumn("Aktiv", typeof(int)));
            dt.Columns.Add(new DataColumn("IstWeiterbilder", typeof(int)));

            dataRow = dt.NewRow();

            dataRow["ID_Chirurgen"] = -1;
            dataRow["ID_ChirurgenFunktionen"] = BusinessLayer.GetAnyOneID_ChirurgenFunktionen();
            dataRow["Nachname"] = "";
            dataRow["Vorname"] = "";
            dataRow["Anrede"] = "";
            dataRow["UserID"] = "";
            dataRow["ImportID"] = "";
            dataRow["Anfangsdatum"] = DateTime.Today;
            dataRow["Aktiv"] = 1;
            dataRow["IstWeiterbilder"] = 0;

            return dataRow;
        }

        public long GetChirurgenCount()
        {
            string sql =
                @"
                SELECT 
                    count(ID_Chirurgen)
                FROM
                    Chirurgen
                ";

            return ExecuteScalar(sql);
        }

        public DataView GetChirurgenAlle()
        {
            StringBuilder sb = new StringBuilder(
                @"
                SELECT 
                    a.ID_Chirurgen,
                    a.Nachname,
                    a.Vorname,
                    a.UserID,
                    a.ImportID,
                    a.Anfangsdatum,
                    a.Anrede, 
                    a.Aktiv,
                    a.Lizenzdaten
                FROM
                    Chirurgen a
                order by 
                    a.Nachname,
                    a.Vorname
                 ");

            return GetDataView(sb.ToString(), null, "Chirurgen");
        }

        public DataView GetAusgefuehrteOperationenFuerGebiete(
            int ID_ChirurgenLogin,
            List<int> gebiete,
            int quelle,
            DateTime? dtFrom,
            DateTime? dtTo,
            int ID_OPFunktionen)
        {
            ProgressEventArgs progressEventArgs = new ProgressEventArgs();

            StringBuilder sbGebiete = new StringBuilder();
            if (gebiete != null && gebiete.Count > 0)
            {
                sbGebiete.Append(" and Richtlinien.ID_Gebiete in (");
                for (int i = 0; i < gebiete.Count; i++)
                {
                    if (i > 0)
                    {
                        sbGebiete.Append(",");
                    }
                    sbGebiete.Append(gebiete[i].ToString());
                }
                sbGebiete.Append(")");
            }

            //
            // Alle holen, die fest einer Richtlinie zugeordnet sind. Damit wird automatisch die dv.Table erzeugt und enthält alle Spalten.
            //
            StringBuilder sb = new StringBuilder(@"
                Select
                    1 as Anzahl,
                    ChirurgenOperationen.ID_ChirurgenOperationen,
                    ChirurgenOperationen.[OPS-Kode] as OPSKode,
                    ChirurgenOperationen.[OPS-Text] as OPSText,
                    Richtlinien.ID_Richtlinien,
                    Richtlinien.LfdNummer,
                    Richtlinien.Richtzahl,
                    Richtlinien.UntBehMethode,
                    Gebiete.ID_Gebiete,
                    Gebiete.Gebiet
                FROM 
                    ChirurgenOperationen inner join (Richtlinien inner join Gebiete on Richtlinien.ID_Gebiete = Gebiete.ID_Gebiete)
                    on ChirurgenOperationen.ID_Richtlinien=Richtlinien.ID_Richtlinien
                where
                    1=1
                    $datefrom$ $dateto$
                    $gebiete$
                    $opfunktionen$
                    $quelle$
                    $chirurgen$
                ");

            HandleDatum(dtFrom, dtTo, sb, "ChirurgenOperationen");
            sb.Replace("$gebiete$", sbGebiete.ToString());
            ArrayList sqlParameters = new ArrayList();
            HandleOPFunktionen(sqlParameters, "ChirurgenOperationen", ID_OPFunktionen, sb);
            HandleQuelleSingle(sqlParameters, "ChirurgenOperationen", quelle, sb);
            HandleSubselectID_Chirurgen(sqlParameters, "ChirurgenOperationen", ID_ChirurgenLogin, sb);

            DataView dv = GetDataView(sb.ToString(), sqlParameters, "ChirurgenOperationen");

            //
            // Jetzt durch alle Operationen laufen mit Filter...
            //
            sb = new StringBuilder(@"
                Select
                    1 as Anzahl,
                    ID_ChirurgenOperationen,
                    [OPS-Kode] as OPSKode,
                    [OPS-Text] as OPSText
                FROM 
                    ChirurgenOperationen 
                            where
                                ID_Richtlinien is null
                                $datefrom$ $dateto$
                                $opfunktionen$
                                $quelle$
                                $chirurgen$
                                ");

            sqlParameters = new ArrayList();
            HandleDatum(dtFrom, dtTo, sb);
            HandleOPFunktionen(sqlParameters, null, ID_OPFunktionen, sb);
            HandleQuelleSingle(sqlParameters, null, quelle, sb);
            HandleSubselectID_Chirurgen(sqlParameters, "ChirurgenOperationen", ID_ChirurgenLogin, sb);

            //
            // ...und für jede operation nachsehen, ob sie zugeordnet ist.
            //
            StringBuilder sb2 = new StringBuilder(@"
                    select @@TOP@@
                        Richtlinien.ID_Richtlinien,
                        Richtlinien.LfdNummer,
                        Richtlinien.Richtzahl,
                        Richtlinien.UntBehMethode,
                        Gebiete.ID_Gebiete,
                        Gebiete.Gebiet
                    from RichtlinienOpsKodes 
                        inner join (Richtlinien inner join Gebiete on Richtlinien.ID_Gebiete = Gebiete.ID_Gebiete)
                        on RichtlinienOpsKodes.ID_Richtlinien = Richtlinien.ID_Richtlinien
                    where 
                        @opsKode like $like$
                        $gebiete$

                    @@LIMIT@@
                    ");

            HandleTopLimitStuff(sb2, "1");
            sb2.Replace("$like$", MakeConcat("RichtlinienOpsKodes.[OPS-Kode]", "'%'"));
            sb2.Replace("$gebiete$", sbGebiete.ToString());

            ArrayList sqlParameters2 = new ArrayList();
            IDbDataParameter paramOpsKode = this.SqlParameter("@opsKode", "prepare");
            paramOpsKode.Size = 20;
            sqlParameters2.Add(paramOpsKode);

            IDataReader reader1 = null;
            IDataReader reader2 = null;

            //
            // Man braucht zwei connections. MSAccess kann immer zwei gleichzeitig.
            // Bei MS SQLServer muss man im connection string 'MultipleResultSet' oder so angeben
            // MySql kann das aber gar nicht, also brauchen wir zwei connections.
            //
            if (this.Open())
            {
                if (Open2())
                {
                    try
                    {
                        IDbCommand command1 = Connection.CreateCommand();
                        command1.CommandText = CleanSqlStatement(sb.ToString());
                        MapSqlParameter2Command(command1, sqlParameters);

                        IDbCommand command2 = Connection2.CreateCommand();
                        command2.CommandText = CleanSqlStatement(sb2.ToString());
                        MapSqlParameter2Command(command2, sqlParameters2);
                        command2.Prepare();

                        reader1 = command1.ExecuteReader();

                        // Durch alle Operationen laufen
                        while (reader1.Read())
                        {
                            string opsKode = reader1.GetString(2);

                            paramOpsKode.Value = opsKode;

                            reader2 = command2.ExecuteReader();

                            if (reader2.Read())
                            {
                                // diese Operationen wird einem der ausgewählten Gebiete automatisch zugeordnet
                                DataRow dataRow = dv.Table.NewRow();

                                // ChirurgenOperationen
                                // 1 as Anzahl,
                                dataRow[0] = reader1.GetInt32(0);
                                // ChirurgenOperationen.ID_ChirurgenOperationen,
                                dataRow[1] = reader1.GetInt32(1);
                                // ChirurgenOperationen.[OPS-Kode] as OPSKode,
                                dataRow[2] = opsKode;
                                // ChirurgenOperationen.[OPS-Text] as OPSText,
                                dataRow[3] = reader1.GetString(3);

                                // Richtlinien, Gebiete
                                // Richtlinien.ID_Richtlinien,
                                dataRow[4] = reader2.GetInt32(0);
                                // Richtlinien.LfdNummer,
                                dataRow[5] = reader2.GetInt32(1);
                                // Richtlinien.Richtzahl,
                                dataRow[6] = reader2.GetInt32(2);
                                // Richtlinien.UntBehMethode,
                                dataRow[7] = reader2.GetString(3);
                                // Gebiete.ID_Gebiete,
                                dataRow[8] = reader2.GetInt32(4);
                                // Gebiete.Gebiet
                                dataRow[9] = reader2.GetString(5);

                                dv.Table.Rows.Add(dataRow);
                            }
                            reader2.Close();
                            reader2.Dispose();
                            reader2 = null;
                        }
                        reader1.Close();
                        reader1.Dispose();
                        reader1 = null;
                    }
                    catch (Exception e)
                    {
                        Write2ErrorLog(e.Message);
                    }
                    finally
                    {
                        if (reader1 != null)
                        {
                            reader1.Close();
                            reader1.Dispose();
                            reader1 = null;
                        }
                        if (reader2 != null)
                        {
                            reader2.Close();
                            reader2.Dispose();
                            reader2 = null;
                        }
                        try
                        {
                            Close2();
                        }
                        catch
                        {
                        }
                        Close();
                    }
                }
                else
                {
                    Write2ErrorLog("Could not open second connection.");
                }
            }
            else
            {
                Write2ErrorLog("Could not open connection.");
            }

            return dv;
        }

        /// <summary>
        /// Liefert alle Operationen, die bestimmten Gebieten zugeordnet sind,
        /// allerdings nur von den Chirurgen, die der angemeldete Chirurg sehen darf.
        /// </summary>
        /// <param name="ID_Chirurgen">Der angemeldete Chirurg</param>
        /// <param name="gebiete"></param>
        /// <param name="quelle"></param>
        /// <param name="dtFrom"></param>
        /// <param name="dtTo"></param>
        /// <param name="ID_OPFunktionen"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public DataView GetAusgefuehrteOperationenFuerGebiete_old(
            int ID_ChirurgenLogin,
            List<int> gebiete,
            int quelle,
            DateTime? dtFrom,
            DateTime? dtTo,
            int ID_OPFunktionen,
            string orderBy)
        {
            //TODO: schneller machen, außerdem die manuell zugeordneten berücksichtigen, also die mit ChirurgenOperationen.ID_Richtlinie != null
            StringBuilder sbGebiete = new StringBuilder();

            if (gebiete != null && gebiete.Count > 0)
            {
                sbGebiete.Append(" and Richtlinien.ID_Gebiete in (");
                for (int i = 0; i < gebiete.Count; i++)
                {
                    if (i > 0)
                    {
                        sbGebiete.Append(",");
                    }
                    sbGebiete.Append(gebiete[i].ToString());
                }
                sbGebiete.Append(")");
            }

            StringBuilder sb = new StringBuilder(@"
                SELECT 
                    1 as Anzahl,
                    Richtlinien.ID_Richtlinien,
                    Richtlinien.LfdNummer,
                    Richtlinien.Richtzahl,
                    Richtlinien.UntBehMethode,
                    ChirurgenOperationen.[OPS-Kode] as OPSKode,
                    ChirurgenOperationen.[OPS-Text] as OPSText,
                    ChirurgenOperationen.ID_ChirurgenOperationen,
                    Gebiete.ID_Gebiete,
                    Gebiete.Gebiet
                FROM 
                    (Richtlinien inner JOIN (RichtlinienOpsKodes inner JOIN ChirurgenOperationen ON ChirurgenOperationen.[OPS-Kode] like $$LIKE$$)
                                            ON RichtlinienOpsKodes.ID_Richtlinien=Richtlinien.ID_Richtlinien)
                    inner join Gebiete on Richtlinien.ID_Gebiete = Gebiete.ID_Gebiete
                where
                    1=1
                    $datefrom$ $dateto$
                    $opfunktionen$
                    $Gebiete$
                    $quelle$
                    $chirurgen$
                order by 
                    ");

            sb.Replace("$$LIKE$$", MakeConcat("RichtlinienOpsKodes.[OPS-Kode]", "'%'"));

            sb.Append(orderBy);

            HandleDatum(dtFrom, dtTo, sb, "ChirurgenOperationen");

            sb.Replace("$Gebiete$", sbGebiete.ToString());

            ArrayList sqlParameter = new ArrayList();

            HandleOPFunktionen(sqlParameter, "ChirurgenOperationen", ID_OPFunktionen, sb);
            HandleQuelleSingle(sqlParameter, "ChirurgenOperationen", quelle, sb);
            HandleSubselectID_Chirurgen(sqlParameter, "ChirurgenOperationen", ID_ChirurgenLogin, sb);

            return GetDataView(sb.ToString(), sqlParameter, "RichtlinienOPSCodes");
        }

        public void DeleteOPSKatalog()
        {
            ExecuteNonQuery("delete from Operationen");
        }
        
        public DataView GetOPZeiten(int nID_Chirurgen, int nID_OPFunktionen, DateTime? dtVon, DateTime? dtBis, int quelle)
        {
            return GetOPZeiten(nID_Chirurgen, nID_OPFunktionen, dtVon, dtBis, quelle, "", "Fallzahl");
        }
        public DataView GetOPZeiten(int nID_Chirurgen, int nID_OPFunktionen, DateTime? dtVon, DateTime? dtBis, int quelle, string opskode, string orderBy)
        {
            StringBuilder sb = new StringBuilder(@"
                SELECT 
                    Datum,
                    Fallzahl,
                    Zeit,
                    ZeitBis,
                    [OPS-Kode],
                    [OPS-Text]
                FROM 
                    ChirurgenOperationen
                where 
                    ID_Chirurgen=@ID_Chirurgen
                    $datefrom$ $dateto$
                    $opskode$
                    $opfunktionen$
                    $quelle$
                    $orderby$
                ");

            HandleDatum(dtVon, dtBis, sb);

            ArrayList arSqlParameter = new ArrayList();
            arSqlParameter.Add(this.SqlParameter("@ID_Chirurgen", nID_Chirurgen));

            if (opskode != null && opskode.Length > 0)
            {
                string opsKodeLike = CreateLikeExpression(opskode + "%");

                sb.Replace("$opskode$", "and [OPS-Kode] like @opskode");
                arSqlParameter.Add(this.SqlParameter("@opskode", opsKodeLike));
            }
            else
            {
                sb.Replace("$opskode$", "");
            }

            HandleOPFunktionen(arSqlParameter, "", nID_OPFunktionen, sb);

            HandleQuelleSingle(arSqlParameter, "", quelle, sb);

            if (!String.IsNullOrEmpty(orderBy))
            {
                sb.Replace("$orderby$", "order by " + orderBy);
            }
            else
            {
                sb.Replace("$orderby$", "");
            }

            return GetDataView(sb.ToString(), arSqlParameter, "ChirurgenOperationen");
        }

        public void GetOperationen(ListView lv)
        {
            GetOperationen(lv, null, null);
        }

        /// <summary>
        /// Get the first entry that matches the OPSCode
        /// </summary>
        /// <param name="opsKodeText"></param>
        /// <returns>null if none exists</returns>
        public DataRow GetOperationen(string filterOpsKodeText)
        {
            ArrayList parameters = null;

            string opsKode = CreateLikeExpression(filterOpsKodeText + "%");

            StringBuilder sb = new StringBuilder(
                @"
                SELECT @@TOP@@
                    ID_Operationen,
                    [OPS-Kode],
                    [OPS-Text]
                FROM
                    Operationen
                $where$
                @@LIMIT@@
                ");

            if (!string.IsNullOrEmpty(filterOpsKodeText))
            {
                sb.Replace("$where$", "where [OPS-Kode] like @OpsKode");
                parameters = new ArrayList();
                parameters.Add(this.SqlParameter("@OpsKode", opsKode));
            }
            else
            {
                sb.Replace("$where$", "");
            }

            HandleTopLimitStuff(sb, "1");

            return GetRecord(sb.ToString(), parameters, "Operationen", true);
        }

        public void GetOperationen(ListView lvOperationen, string filterOpsKode, string filterOpsText)
        {
            ArrayList parameters = null;

            string opsKode = CreateLikeExpression(filterOpsKode + "%");
            string opsText = CreateLikeExpression("%" + filterOpsText + "%");

            StringBuilder sb = new StringBuilder(
                @"
                SELECT 
                    ID_Operationen,
                    [OPS-Kode] as Kode,
                    [OPS-Text] as Name
                FROM
                    Operationen
                @@where
                ORDER BY
                    [OPS-Kode]
                ");

            string whereClause = "";

            if (filterOpsKode != null && filterOpsKode.Length > 0)
            {
                whereClause = "[OPS-Kode] like @OpsKode";
                parameters = new ArrayList();
                parameters.Add(this.SqlParameter("@OpsKode", opsKode));
            }
            if (filterOpsText != null && filterOpsText.Length > 0)
            {
                if (whereClause.Length > 0)
                {
                    whereClause = whereClause + " and ";
                }
                whereClause = whereClause + "[OPS-Text] like @OpsText";
                if (parameters == null)
                {
                    parameters = new ArrayList();
                }
                parameters.Add(this.SqlParameter("@OpsText", opsText));
            }

            if (whereClause.Length > 0)
            {
                sb.Replace("@@where", "WHERE " + whereClause);
            }
            else
            {
                sb.Replace("@@where", "");
            }

            if (Open())
            {
                IDataReader reader = null;

                try
                {
                    lvOperationen.Clear();
                    lvOperationen.Columns.Add(GetText("opscode"), 80, HorizontalAlignment.Left);
                    lvOperationen.Columns.Add(GetText("bezeichnung"), -2, HorizontalAlignment.Left);
                    lvOperationen.BeginUpdate();

                    string sql = CleanSqlStatement(sb.ToString());
                    IDbCommand command = Connection.CreateCommand();
                    command.CommandText = sql;
                    MapSqlParameter2Command(command, parameters);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ListViewItem lvi = new ListViewItem(reader.GetString(1));
                        lvi.Tag = reader.GetInt32(0);
                        lvi.ToolTipText = "[" + reader.GetString(1) + "] " + reader.GetString(2);
                        lvi.SubItems.Add(reader.GetString(2));
                        lvOperationen.Items.Add(lvi);
                    }
                    reader.Close();
                    reader.Dispose();
                    reader = null;

                    command.Dispose();
                    command = null;

                    lvOperationen.EndUpdate();
                }
                catch (Exception ex)
                {
                    Write2ErrorLog(ex.Message);
                }
                finally
                {
                    if (reader != null && !reader.IsClosed)
                    {
                        reader.Close();
                        reader.Dispose();
                        reader = null;
                    }
                    Close();
                }
            }
        }

        public void GetOperationen(ListView lvOperationen, string filterOpsKodeText)
        {
            ArrayList parameters = null;

            string opsKode = CreateLikeExpression(filterOpsKodeText + "%");
            string opsText = CreateLikeExpression("%" + filterOpsKodeText + "%");

            StringBuilder sb = new StringBuilder(
                @"
                SELECT 
                    ID_Operationen,
                    [OPS-Kode],
                    [OPS-Text]
                FROM
                    Operationen
                $where$
                ORDER BY
                    [OPS-Kode]
                ");

            if (filterOpsKodeText != null && filterOpsKodeText.Length > 0)
            {
                sb.Replace("$where$", "where [OPS-Kode] like @OpsKode or [OPS-Text] like @OpsText");
                parameters = new ArrayList();
                parameters.Add(this.SqlParameter("@OpsKode", opsKode));
                parameters.Add(this.SqlParameter("@OpsText", opsText));
            }
            else
            {
                sb.Replace("$where$", "");
            }

            if (Open())
            {
                IDataReader reader = null;

                try
                {
                    lvOperationen.Clear();
                    lvOperationen.Columns.Add(GetText("opscode"), 80, HorizontalAlignment.Left);
                    lvOperationen.Columns.Add(GetText("bezeichnung"), -2, HorizontalAlignment.Left);
                    lvOperationen.BeginUpdate();

                    string sql = CleanSqlStatement(sb.ToString());
                    IDbCommand command = Connection.CreateCommand();
                    command.CommandText = sql;
                    MapSqlParameter2Command(command, parameters);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ListViewItem lvi = new ListViewItem(reader.GetString(1));
                        lvi.Tag = reader.GetInt32(0);
                        lvi.ToolTipText = "[" + reader.GetString(1) + "] " + reader.GetString(2);
                        lvi.SubItems.Add(reader.GetString(2));
                        lvOperationen.Items.Add(lvi);
                    }

                    reader.Close();
                    reader.Dispose();
                    reader = null;

                    command.Dispose();
                    command = null;

                    lvOperationen.EndUpdate();
                }
                catch (Exception ex)
                {
                    Write2ErrorLog(ex.Message);
                }
                finally
                {
                    if (reader != null && !reader.IsClosed)
                    {
                        reader.Close();
                        reader.Dispose();
                        reader = null;
                    }
                    Close();
                }
            }
        }

        public DataView GetOperationenExecuted()
        {
              string sql = @"
                select 
                    [OPS-Kode] as OPSCode,
                    max([OPS-Text]) as OPSText
                from 
                    ChirurgenOperationen
                group by 
                    [OPS-Kode]
                order by 
                    [OPS-Kode]
                ";
            return GetDataView(sql, "Operationen");
        }

        public DataRow GetOperation(int nID_Operationen)
        {
            string sSQL =
                @"
                SELECT 
                    ID_Operationen,
                    [OPS-Kode] as Kode,
                    [OPS-Text] as Name
                FROM
                    Operationen
                WHERE
                    ID_Operationen=@ID_Operationen
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Operationen", nID_Operationen));

            return GetRecord(sSQL, aSQLParameters, "Operationen");
        }

        public int GetMaxID_Operationen()
        {
            string sSQL =
                @"
                SELECT 
                    max(ID_Operationen) as max_id
                FROM
                    Operationen
                ";

            DataRow dataRow =  GetRecord(sSQL, null, "Operationen");
            int id_operationen = (int)dataRow["max_id"];

            return id_operationen;
        }

        public int InsertOperation(DataRow oDataRow)
        {
            StringBuilder sb = new StringBuilder(
                @"
                INSERT INTO Operationen
                    (
                    [OPS-Kode],
                    [OPS-Text]
                    )
                    VALUES
                    (
                     @OPSKode,
                     @OPSText
                    )
                ");

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@OPSKode", (string)oDataRow["OPS-Kode"]));
            aSQLParameters.Add(this.SqlParameter("@OPSText", (string)oDataRow["OPS-Text"]));

            return this.InsertRecord(sb.ToString(), aSQLParameters, "Operationen");
        }
        public bool DeleteOperation(int nID_Operationen)
        {
            StringBuilder sb = new StringBuilder(
                @"
                DELETE FROM
                    Operationen
                WHERE
                    ID_Operationen=@ID_Operationen
                ");

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Operationen", nID_Operationen));

            int numRecords = this.ExecuteNonQuery(sb.ToString(), sqlParameters);

            return (numRecords == 1);

        }
        public bool DeleteOperationenForChirurg(int nID_Chirurgen)
        {
            string strSQL = @"
                DELETE FROM
                    ChirurgenOperationen
                WHERE
                    ID_Chirurgen=@ID_Chirurgen
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(SqlParameter("@ID_Chirurgen", nID_Chirurgen));

            int iEffectedRecords = this.ExecuteNonQuery(strSQL, sqlParameters);

            return true;
        }

        public bool DeleteChirurgenOperationen(int _nID_ChirurgenOperationen)
        {
            StringBuilder sb = new StringBuilder(
                @"
                DELETE FROM
                    ChirurgenOperationen
                WHERE
                    ID_ChirurgenOperationen=@ID_ChirurgenOperationen
                ");

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_ChirurgenOperationen", _nID_ChirurgenOperationen));

            int iEffectedRecords = this.ExecuteNonQuery(sb.ToString(), aSQLParameters);

            return (iEffectedRecords == 1);

        }

        /// <summary>
        /// Holt fuer einen Chirurgen und eine bestimmte OP-Art zu einem Datum und einer Zeit
        /// Wird benutzt um zu überprüfen, ob eine Operation schon existiert oder nicht, damit
        /// sie beim Import nicht doppelt eingefügt wird.
        /// </summary>
        /// <param name="nID_Chirurgen"></param>
        /// <param name="nID_Operationen"></param>
        /// <param name="dt"></param>
        /// <returns>Gibt die erste Operatin zurück, die gleich ist, wenn es eine oder mehrere gibt</returns>
        public DataRow CheckChirurgOperationen(int nID_Chirurgen, string opsCode, DateTime dt)
        {
            StringBuilder sb = new StringBuilder(
                @"
                select 
                    ID_ChirurgenOperationen
                from 
                    ChirurgenOperationen
                where 
                    ID_Chirurgen=@ID_Chirurgen AND
                    [OPS-Kode]=@OPSKode AND
                    Datum=@Datum AND
                    Zeit=@Zeit
                ");

            sb.Replace("@Datum", this.DateTime2DBDateTimeString(dt));
            sb.Replace("@Zeit", this.DateTime2DBTimeString(dt));

            ArrayList arSqlParameter = new ArrayList();
            arSqlParameter.Add(this.SqlParameter("@ID_Chirurgen", nID_Chirurgen));
            arSqlParameter.Add(this.SqlParameter("@OPSKode", opsCode));

            return GetRecord(sb.ToString(), arSqlParameter, "ChirurgenOperationen", true);
        }

        /// <summary>
        /// Holt fuer einen Chirurgen und eine bestimmte OP-Art zu einem Datum und einer Zeit und der Fallzahl
        /// Wird benutzt um zu überprüfen, ob eine Operation schon existiert oder nicht, damit
        /// sie beim Import nicht doppelt eingefügt wird.
        /// </summary>
        /// <param name="nID_Chirurgen"></param>
        /// <param name="nID_Operationen"></param>
        /// <param name="dt"></param>
        /// <returns>Gibt die erste Operatin zurück, die gleich ist, wenn es eine oder mehrere gibt</returns>
        public DataRow CheckChirurgOperationen(int nID_Chirurgen, string opsCode, DateTime dt, string fallzahl)
        {
            StringBuilder sb = new StringBuilder(
                @"
                select 
                    ID_ChirurgenOperationen
                from 
                    ChirurgenOperationen
                where 
                    ID_Chirurgen=@ID_Chirurgen AND
                    [OPS-Kode]=@OPSKode AND
                    Datum=@Datum AND
                    Zeit=@Zeit AND
                    Fallzahl=@Fallzahl
                ");

            sb.Replace("@Datum", this.DateTime2DBDateTimeString(dt));
            sb.Replace("@Zeit", this.DateTime2DBTimeString(dt));

            ArrayList arSqlParameter = new ArrayList();
            arSqlParameter.Add(this.SqlParameter("@ID_Chirurgen", nID_Chirurgen));
            arSqlParameter.Add(this.SqlParameter("@OPSKode", opsCode));
            arSqlParameter.Add(this.SqlParameter("@Fallzahl", fallzahl));

            return GetRecord(sb.ToString(), arSqlParameter, "ChirurgenOperationen", true);
        }

        public DataView GetChirurgenOperationen(int nID_Chirurgen)
        {
            StringBuilder sb = new StringBuilder(
                @"
                select 
                    ID_ChirurgenOperationen,
                    ID_Chirurgen,
                    ID_OPFunktionen,
                    ID_Richtlinien,
                    ID_KlinischeErgebnisseTypen,
                    Fallzahl,
                    [OPS-Kode],
                    [OPS-Text],
                    Datum,
                    Zeit,
                    ZeitBis,
                    Quelle,
                    KlinischeErgebnisse
                from 
                    ChirurgenOperationen
                where 
                    ID_Chirurgen = @ID_Chirurgen
                ");

            ArrayList arSqlParameter = new ArrayList();
            arSqlParameter.Add(this.SqlParameter("@ID_Chirurgen", nID_Chirurgen));

            return GetDataView(sb.ToString(), arSqlParameter, "ChirurgenOperationen");
        }
        public DataRow GetChirurgenOperationenRecord(int nID_ChirurgenOperationen)
        {
            string sql =
                @"
                select 
                    ID_ChirurgenOperationen,
                    ID_Chirurgen,
                    ID_Richtlinien,
                    ID_OPFunktionen,
                    ID_KlinischeErgebnisseTypen,
                    Fallzahl,
                    Datum,
                    Zeit,
                    ZeitBis,
                    [OPS-Kode],
                    [OPS-Text],
                    Quelle,
                    KlinischeErgebnisse
                from 
                    ChirurgenOperationen
                where 
                    ID_ChirurgenOperationen = @ID_ChirurgenOperationen
                ";

            ArrayList arSqlParameter = new ArrayList();
            arSqlParameter.Add(this.SqlParameter("@ID_ChirurgenOperationen", nID_ChirurgenOperationen));

            return GetRecord(sql, arSqlParameter, "ChirurgenOperationen");
        }


        public bool UpdateChirurgenOperationenRichtlinie(DataRow oDataRow)
        {
            StringBuilder sb = new StringBuilder(
                @"
                UPDATE ChirurgenOperationen
                SET
                    ID_Richtlinien=@ID_Richtlinien
                WHERE
                    ID_ChirurgenOperationen=@ID_ChirurgenOperationen
                ");

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameterInt("@ID_Richtlinien", oDataRow["ID_Richtlinien"]));
            aSQLParameters.Add(this.SqlParameter("@ID_ChirurgenOperationen", oDataRow["ID_ChirurgenOperationen"]));

            int iEffectedRecords = this.ExecuteNonQuery(sb.ToString(), aSQLParameters);

            return (iEffectedRecords == 1);
        }

        /// <summary>
        /// Hier kommt jetzt die erste heraus. Beim Datenimport wird ja nicht mehr ID_Operationen verwendet, sondern OPSKode und OPSText steht direkt
        /// in ChirurgenOperationen drin.
        /// </summary>
        /// <param name="strOPSKode"></param>
        /// <returns></returns>
        public int GetID_OperationenByOpsKode(string strOPSKode)
        {
            int nID_Operationen = -1;

            string sb =
                @"
                SELECT 
                    ID_Operationen
                FROM
                    Operationen
                WHERE 
                    [OPS-Kode] = @OPSKode
                ";

            ArrayList arSqlParameter = new ArrayList();
            arSqlParameter.Add(this.SqlParameter("@OPSKode", strOPSKode));

            DataRow oRow = GetRecord(sb, arSqlParameter, "Operationen", true);

            if (oRow != null)
            {
                nID_Operationen = ConvertToInt32(oRow["ID_Operationen"]);
            }

            return nID_Operationen;
        }

        /// <summary>
        /// Check if there is a code that matches strOPSKode
        /// </summary>
        /// <param name="strOPSKode"></param>
        /// <returns></returns>
        public long OPSKodePatternExists(string strOPSKode)
        {
            strOPSKode = CreateLikeExpression(strOPSKode + "%");
 
            string sql =
                @"
                SELECT 
                    count(ID_Operationen)
                FROM
                    Operationen
                WHERE 
                    [OPS-Kode] LIKE @OPSKode
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@OPSKode", strOPSKode));

            return ExecuteScalar(sql, sqlParameters);
        }

        private void HandleKlinischeErgebnisseTypen(ArrayList sqlParameters, string prefix, int ID_KlinischeErgebnisseTypen, StringBuilder sb)
        {
            string sql = "";

            if (ID_KlinischeErgebnisseTypen != BusinessLayerCommon.ID_Alle)
            {
                if (string.IsNullOrEmpty(prefix))
                {
                    sql = string.Format("AND ID_KlinischeErgebnisseTypen={0}", ID_KlinischeErgebnisseTypen);
                }
                else
                {
                    sql = string.Format(" and {0}.ID_KlinischeErgebnisseTypen={1}", prefix, ID_KlinischeErgebnisseTypen);
                }
            }
            sb.Replace("$KlinischeErgebnisse$", sql);
        }

        public long GetChirurgenOperationenCount(int ID_Chirurgen)
        {
            ArrayList sqlParameters = new ArrayList();

            string sql =
                @"
                SELECT 
                    count(ID_ChirurgenOperationen)
                FROM
                    ChirurgenOperationen
                WHERE
                    ID_Chirurgen=@ID_Chirurgen
                ";

            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));

            return this.ExecuteScalar(sql, sqlParameters);
        }

        public long GetChirurgenOperationenCount()
        {
            string sql =
                @"
                SELECT 
                    count(ID_ChirurgenOperationen)
                FROM
                    ChirurgenOperationen
                ";

            return this.ExecuteScalar(sql);
        }

        public long GetIstOperationenCountForChirurg(int ID_Chirurgen, DateTime? von, DateTime? bis, string ops, int quelle, int opFunktionen)
        {
            ArrayList sqlParameters = new ArrayList();

            StringBuilder sb = new StringBuilder(
                @"
                SELECT 
                    count(ID_ChirurgenOperationen)
                FROM
                    ChirurgenOperationen
                WHERE 
                    ID_Chirurgen=@ID_Chirurgen
                    $datefrom$ $dateto$
                    $ops$
                    $quelle$
                    $opfunktionen$
                ");

            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));

            HandleDatum(von, bis, sb);

            if (ops != null && ops.Length > 0)
            {
                string opsKodeLike = CreateLikeExpression(ops + "%");
                string opsTextLike = CreateLikeExpression("%" + ops + "%");

                sb.Replace("$ops$", "and ([OPS-Kode] like @opskode or [OPS-Text] like @opstext)");
                sqlParameters.Add(this.SqlParameter("@opskode", opsKodeLike));
                sqlParameters.Add(this.SqlParameter("@opstext", opsTextLike));
            }
            else
            {
                sb.Replace("$ops$", "");
            }

            HandleQuelleSingle(sqlParameters, "", quelle, sb);
            HandleOPFunktionen(sqlParameters, "", opFunktionen, sb);

            return this.ExecuteScalar(sb.ToString(), sqlParameters);
        }

        public DataView GetChirurgenOperationen(int nID_Chirurgen, DateTime? dtVon, DateTime? dtBis)
        {
            StringBuilder sb = new StringBuilder(
                @"
                SELECT 
                    a.ID_ChirurgenOperationen,
                    a.ID_OPFunktionen,
                    a.ID_KlinischeErgebnisseTypen,
                    a.Fallzahl,
                    a.Datum,
                    a.Zeit,
                    a.ZeitBis,
                    a.[OPS-Kode],
                    a.[OPS-Text],
                    a.Quelle,
                    a.KlinischeErgebnisse,
                    b.Beschreibung,
                    c.Text as KlinischeErgebnisseTyp
                FROM
                    (ChirurgenOperationen a inner join OPFunktionen b on a.ID_OPFunktionen = b.ID_OPFunktionen)
                        inner join KlinischeErgebnisseTypen c on a.ID_KlinischeErgebnisseTypen = c.ID_KlinischeErgebnisseTypen
                WHERE 
                    ID_Chirurgen=@ID_Chirurgen
                    $datefrom$ $dateto$
                ORDER BY
                    Datum desc, 
                    [OPS-Kode]
                ");

            HandleDatum(dtVon, dtBis, sb);

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", nID_Chirurgen));

            return GetDataView(sb.ToString(), sqlParameters, "ChirurgenOperationen");
        }

        public DataRow GetChirurgenOperationenFirst()
        {
            string sql =
                @"
                SELECT 
                    min(Datum) as Datum
                FROM
                    ChirurgenOperationen
                ";

            return GetRecord(sql, null, "ChirurgenOperationen");
        }
        public DataRow GetChirurgenOperationenLast()
        {
            string sql =
                @"
                SELECT 
                    max(Datum) as Datum
                FROM
                    ChirurgenOperationen
                ";

            return GetRecord(sql, null, "ChirurgenOperationen");
        }

        public DataRow GetChirurgenOperationenFirst(int ID_Chirurgen)
        {
            ArrayList sqlParameters = new ArrayList();

            string sql =
                @"
                SELECT 
                    min(Datum) as Datum
                FROM
                    ChirurgenOperationen
                WHERE
                    ID_Chirurgen=@ID_Chirurgen
                ";

            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));

            return GetRecord(sql, sqlParameters, "ChirurgenOperationen");
        }
        public DataRow GetChirurgenOperationenLast(int ID_Chirurgen)
        {
            ArrayList sqlParameters = new ArrayList();

            string sql =
                @"
                SELECT 
                    max(Datum) as Datum
                FROM
                    ChirurgenOperationen
                WHERE
                    ID_Chirurgen=@ID_Chirurgen
                ";

            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));

            return GetRecord(sql, sqlParameters, "ChirurgenOperationen");
        }


#endregion

#region Users 

        public bool CheckUserAndPassword(string userID, string password)
        {
            string hashedPassword = Tools.Password2HashedPassword(password);

            string sql =
                @"
                SELECT 
                    ID_Chirurgen
                FROM 
                    Chirurgen
                WHERE
                    UserID=@UserID
                    AND Password=@Password
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@UserID", userID));
            sqlParameters.Add(this.SqlParameter("@Password", hashedPassword));

            DataRow row = this.GetRecord(sql, sqlParameters, "Chirurgen");

            return (row != null);
        }

        /// <summary>
        /// Benutzer 'ndert sein eigenes Password. Dann muss er es nicht beim naechsten
        /// Anmelden aendern.
        /// </summary>
        /// <param name="strUserID"></param>
        /// <param name="strNewPassword"></param>
        /// <returns></returns>
        public bool UpdatePassword(string strUserID, string strNewPassword)
        {
            string strHashedPassword = Tools.Password2HashedPassword(strNewPassword);

            string strSQL =
                @"
                UPDATE 
                    Chirurgen
                SET
                    [Password]=@Password,
                    MustChangePassword=0
                WHERE
                    [UserID]=@UserID
                ";


            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@Password", strHashedPassword));
            aSQLParameters.Add(this.SqlParameter("@UserID", strUserID));

            int iEffectedRecords = this.ExecuteNonQuery(strSQL, aSQLParameters);

            return (iEffectedRecords == 1);
        }

        /// <summary>
        /// Admin setyt Kennwort zurueck. Benutzer muss es beim naechsten Mal aendern.
        /// </summary>
        /// <param name="nID_Chirurgen"></param>
        /// <param name="strNewPassword"></param>
        /// <returns></returns>
        public bool UpdatePassword(int nID_Chirurgen, string strNewPassword)
        {
            string strHashedPassword = Tools.Password2HashedPassword(strNewPassword);

            string strSQL =
                @"
                UPDATE 
                    Chirurgen
                SET
                    [Password]=@Password,
                    MustChangePassword=1
                WHERE
                    ID_Chirurgen=@ID_Chirurgen
                ";


            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@Password", strHashedPassword));
            aSQLParameters.Add(this.SqlParameter("@ID_Chirurgen", nID_Chirurgen));

            int iEffectedRecords = this.ExecuteNonQuery(strSQL, aSQLParameters);

            return (iEffectedRecords == 1);
        }
#endregion

#region Kommentare
        /// <summary>
        /// Holt einen betimmten Comment
        /// </summary>
        /// <param name="nID"></param>
        /// <returns></returns>
        public DataRow GetKommentar(int nID_Kommentar)
        {
            string sSQL =
                @"
                SELECT 
                    k.ID_Kommentare, 
                    k.Datum, 
                    k.AbschnittVon, 
                    k.AbschnittBis, 
                    k.ID_Chirurgen_Von, 
                    k.ID_Chirurgen_Fuer, 
                    k.KommentarVon, 
                    k.KommentarFuer, 
                    k.Status, 
                    f.ID_ChirurgenFunktionen,
                    f.Funktion,
                    cVon.Nachname AS NachnameVon, 
                    cFuer.Nachname AS NachnameFuer,
                    cFuer.UserID AS UserIDFuer
                FROM 
                    Kommentare AS k, 
                    Chirurgen AS cVon, 
                    Chirurgen AS cFuer,
                    ChirurgenFunktionen As f
                WHERE 
                    k.ID_Chirurgen_Von=cVon.ID_Chirurgen And 
                    k.ID_Chirurgen_Fuer=cFuer.ID_Chirurgen and 
                    k.ID_ChirurgenFunktionen=f.ID_ChirurgenFunktionen And
                    k.ID_Kommentare=@ID_Kommentar
                ORDER BY 
                    k.Datum
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Kommentar", nID_Kommentar));

            return GetRecord(sSQL, aSQLParameters, "Kommentare");
        }

        public DataView GetKommentare()
        {
            string sSQL =
                @"
                SELECT 
                    k.ID_Kommentare, 
                    k.Datum, 
                    k.AbschnittVon, 
                    k.AbschnittBis, 
                    k.ID_Chirurgen_Von, 
                    k.ID_Chirurgen_Fuer, 
                    k.KommentarVon, 
                    k.KommentarFuer, 
                    k.Status, 
                    f.ID_ChirurgenFunktionen,
                    f.Funktion,
                    cVon.Nachname AS NachnameVon, 
                    cFuer.Nachname AS NachnameFuer
                FROM 
                    Kommentare AS k, 
                    Chirurgen AS cVon, 
                    Chirurgen AS cFuer,
                    ChirurgenFunktionen As f
                WHERE 
                    k.ID_Chirurgen_Von=cVon.ID_Chirurgen And 
                    k.ID_Chirurgen_Fuer=cFuer.ID_Chirurgen And
                    k.ID_ChirurgenFunktionen=f.ID_ChirurgenFunktionen
                ORDER BY 
                    k.Datum,
                    k.AbschnittVon
                ";

            return GetDataView(sSQL, null, "Kommentare");
        }

        /// <summary>
        /// Alle Kommentare fuer einen bestimmten Chirurg (ID_Chirurgen_Fuer)
        /// </summary>
        /// <param name="nID_Chirurgen"></param>
        /// <returns></returns>
        public DataView GetKommentare(int nID_Chirurgen)
        {
            string sSQL =
                @"
                SELECT 
                    k.ID_Kommentare, 
                    k.Datum, 
                    k.AbschnittVon, 
                    k.AbschnittBis, 
                    k.ID_Chirurgen_Von, 
                    k.ID_Chirurgen_Fuer, 
                    k.KommentarVon, 
                    k.KommentarFuer, 
                    k.Status, 
                    f.ID_ChirurgenFunktionen,
                    f.Funktion,
                    cVon.Nachname AS NachnameVon, 
                    cFuer.Nachname AS NachnameFuer
                FROM 
                    Kommentare AS k, 
                    Chirurgen AS cVon, 
                    Chirurgen AS cFuer,
                    ChirurgenFunktionen As f
                WHERE 
                    k.ID_Chirurgen_Von=cVon.ID_Chirurgen And 
                    k.ID_Chirurgen_Fuer=cFuer.ID_Chirurgen And
                    k.ID_ChirurgenFunktionen=f.ID_ChirurgenFunktionen And
                    cFuer.ID_Chirurgen = @ID_Chirurgen
                ORDER BY 
                    k.Datum,
                    k.AbschnittVon
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Chirurgen", nID_Chirurgen));

            return GetDataView(sSQL, aSQLParameters, "Kommentare");
        }

        public int InsertKommentar(DataRow oDataRow)
        {
            StringBuilder sb = new StringBuilder(
                @"
                INSERT INTO Kommentare
                    (
                    Datum,
                    AbschnittVon,
                    AbschnittBis,
                    ID_Chirurgen_Von,
                    ID_Chirurgen_Fuer,
                    ID_ChirurgenFunktionen,
                    KommentarVon,
                    KommentarFuer,
                    Status
                    )
                    VALUES
                    (
                     @Datum,
                     @AbschnittVon,
                     @AbschnittBis,
                     @ID_Chirurgen_Von,
                     @ID_Chirurgen_Fuer,
                     @ID_ChirurgenFunktionen,
                     @KommentarVon,
                     @KommentarFuer,
                     @Status
                    )
                ");

            sb.Replace("@Datum", this.DateTime2DBDateTimeString(oDataRow["Datum"]));
            sb.Replace("@AbschnittVon", this.DateTime2DBDateTimeString(oDataRow["AbschnittVon"]));
            sb.Replace("@AbschnittBis", this.DateTime2DBDateTimeString(oDataRow["AbschnittBis"]));

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Chirurgen_Von", oDataRow["ID_Chirurgen_Von"]));
            aSQLParameters.Add(this.SqlParameter("@ID_Chirurgen_Fuer", oDataRow["ID_Chirurgen_Fuer"]));
            aSQLParameters.Add(this.SqlParameter("@ID_ChirurgenFunktionen", oDataRow["ID_ChirurgenFunktionen"]));
            aSQLParameters.Add(this.SqlParameter("@KommentarVon", (string)oDataRow["KommentarVon"]));
            aSQLParameters.Add(this.SqlParameter("@KommentarFuer", (string)oDataRow["KommentarFuer"]));
            aSQLParameters.Add(this.SqlParameter("@Status", oDataRow["Status"]));

            return this.InsertRecord(sb.ToString(), aSQLParameters, "Kommentare");
        }

        public bool UpdateKommentar(DataRow oDataRow)
        {
            StringBuilder sb = new StringBuilder(
                @"
                UPDATE Kommentare
                SET
                    KommentarVon=@KommentarVon,
                    KommentarFuer=@KommentarFuer,
                    AbschnittVon=@AbschnittVon,
                    AbschnittBis=@AbschnittBis,
                    ID_ChirurgenFunktionen=@ID_ChirurgenFunktionen,
                    Status=@Status
                WHERE
                    ID_Kommentare=@ID_Kommentare
                ");

            sb.Replace("@AbschnittVon", this.DateTime2DBDateTimeString(oDataRow["AbschnittVon"]));
            sb.Replace("@AbschnittBis", this.DateTime2DBDateTimeString(oDataRow["AbschnittBis"]));

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@KommentarVon", (string)oDataRow["KommentarVon"]));
            aSQLParameters.Add(this.SqlParameter("@KommentarFuer", (string)oDataRow["KommentarFuer"]));
            aSQLParameters.Add(this.SqlParameter("@ID_ChirurgenFunktionen", oDataRow["ID_ChirurgenFunktionen"]));
            aSQLParameters.Add(this.SqlParameter("@Status", oDataRow["Status"]));
            aSQLParameters.Add(this.SqlParameter("@ID_Kommentare", oDataRow["ID_Kommentare"]));

            int iEffectedRecords = this.ExecuteNonQuery(sb.ToString(), aSQLParameters);

            return (iEffectedRecords == 1);
        }

        public bool DeleteKommentar(int nID_Kommentare)
        {
            StringBuilder sb = new StringBuilder(
                @"
                DELETE FROM
                    Kommentare
                WHERE
                    ID_Kommentare=@ID_Kommentare
                ");

            sb.Replace("@ID_Kommentare", nID_Kommentare.ToString());

            int numRecords = this.ExecuteNonQuery(sb.ToString(), null);

            return (numRecords == 1);
        }

        public bool DeleteKommentareForChirurg(int nID_Chirurgen)
        {
            string strSQL = @"
                DELETE FROM
                    Kommentare
                WHERE
                    ID_Chirurgen_Von=@ID_Chirurgen OR
                    ID_Chirurgen_Fuer=@ID_Chirurgen
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(SqlParameter("@ID_Chirurgen", nID_Chirurgen));

            int iEffectedRecords = this.ExecuteNonQuery(strSQL, aSQLParameters);

            return true;
        }

        public long CheckForOpenComments(int ID_Chirurgen)
        {
            string sql = string.Format(@"
                select count(ID_Kommentare) FROM
                    Kommentare
                WHERE
                    Status <> {0} and
                    (ID_Chirurgen_Fuer=@ID_Chirurgen_Fuer or
                    ID_Chirurgen_Von=@ID_Chirurgen_Von)
                ", KommentareView.KommentarStatusFertig);

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(SqlParameter("@ID_Chirurgen_Fuer", ID_Chirurgen));
            sqlParameters.Add(SqlParameter("@ID_Chirurgen_Von", ID_Chirurgen));

            return this.ExecuteScalar(sql, sqlParameters);
        }

#endregion

#region Operationen

        public DataRow GetPlanOperation(int nID_PlanOperationen)
        {
           string sql = 
                @"
                SELECT 
                    ID_PlanOperationen,
                    ID_Chirurgen,
                    Operation,
                    Anzahl,
                    DatumVon, 
                    DatumBis
                FROM 
                    PlanOperationen 
                WHERE 
                    ID_PlanOperationen=@ID_PlanOperationen
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_PlanOperationen", nID_PlanOperationen));

            return GetRecord(sql, sqlParameters, "PlanOperationen");
        }

        public DataView GetPlanOperationen(int nID_Chirurgen)
        {
            StringBuilder sb = new StringBuilder(
                @"
                SELECT 
                    ID_PlanOperationen,
                    ID_Chirurgen,
                    Operation,
                    Anzahl,
                    DatumVon, 
                    DatumBis
                FROM 
                    PlanOperationen 
                WHERE 
                    ID_Chirurgen=@ID_Chirurgen
                ORDER BY
                    DatumVon
                ");


            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Chirurgen", nID_Chirurgen));

            return GetDataView(sb.ToString(), aSQLParameters, "PlanOperationen");
        }

        /// <summary>
        /// Alle DB-Einträge holen, die ganz in der angegebenen Zeitspanne liegen. Diese werden dann gezählt.
        /// </summary>
        /// <param name="nID_Chirurgen"></param>
        /// <param name="dtFrom"></param>
        /// <param name="dtTo"></param>
        /// <returns></returns>
        public DataView GetPlanOperationenArten(int nID_Chirurgen, DateTime? dtFrom, DateTime? dtTo)
        {
            StringBuilder sb = new StringBuilder(
                @"
                SELECT DISTINCT
                    Operation
                FROM 
                    PlanOperationen
                WHERE 
                    ID_Chirurgen=@ID_Chirurgen
                    and @DateFrom <= DatumVon and DatumBis <= @DateTo
                ORDER BY 
                    Operation
                ");

            sb.Replace("@DateFrom", this.DateTime2DBDateTimeString(dtFrom));
            sb.Replace("@DateTo", this.DateTime2DBDateTimeString(dtTo));

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", nID_Chirurgen));

            return GetDataView(sb.ToString(), sqlParameters, "PlanOperationen");
        }

        public DataRow CreateDataRowPlanOperation(int nID_Chirurgen)
        {
            DataTable dt = new DataTable("PlanOperationen");
            DataRow dataRow;

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("ID_Chirurgen", typeof(int)));
            dt.Columns.Add(new DataColumn("Operation", typeof(string)));
            dt.Columns.Add(new DataColumn("Anzahl", typeof(int)));
            dt.Columns.Add(new DataColumn("DatumVon", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("DatumBis", typeof(DateTime)));

            dataRow = dt.NewRow();

            dataRow["ID_Chirurgen"] = nID_Chirurgen;
            dataRow["Operation"] = "";
            dataRow["Anzahl"] = 1;
            dataRow["DatumVon"] = DateTime.Now;
            dataRow["DatumBis"] = DateTime.Now;

            return dataRow;
        }

        public DataRow CreateDataRowOperation()
        {
            DataTable dt = new DataTable("Operationen");
            DataRow dataRow;

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("OPS-Kode", typeof(string)));
            dt.Columns.Add(new DataColumn("OPS-Text", typeof(string)));

            dataRow = dt.NewRow();

            dataRow["OPS-Kode"] = "";
            dataRow["OPS-Text"] = "";

            return dataRow;
        }

        public int GetIdKlinischeErgebnisseTypenUnauffaellig()
        {
            int ID_KlinischeErgebnisseTypen = ExecuteScalarInteger(
                string.Format("select ID_KlinischeErgebnisseTypen from KlinischeErgebnisseTypen where"
                    + " [ID] = '{0}'", BusinessLayer.KlinischeErgebnisseTypenUnauffaellig), null);

            return ID_KlinischeErgebnisseTypen;
        }

        public DataRow CreateDataRowChirurgenOperationen(int nID_Chirurgen)
        {
            DataTable dt = new DataTable("ChirurgenOperationen");
            DataRow dataRow;

            int ID_KlinischeErgebnisseTypen = GetIdKlinischeErgebnisseTypenUnauffaellig();

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("ID_Chirurgen", typeof(int)));
            dt.Columns.Add(new DataColumn("ID_OPFunktionen", typeof(int)));
            dt.Columns.Add(new DataColumn("ID_Richtlinien", typeof(int)));
            dt.Columns.Add(new DataColumn("ID_KlinischeErgebnisseTypen", typeof(int)));
            dt.Columns.Add(new DataColumn("OPS-Kode", typeof(string)));
            dt.Columns.Add(new DataColumn("OPS-Text", typeof(string)));
            dt.Columns.Add(new DataColumn("Fallzahl", typeof(string)));
            dt.Columns.Add(new DataColumn("Datum", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Zeit", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("ZeitBis", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Quelle", typeof(int)));
            dt.Columns.Add(new DataColumn("KlinischeErgebnisse", typeof(string)));

            dataRow = dt.NewRow();

            dataRow["ID_Chirurgen"] = nID_Chirurgen;
            dataRow["ID_OPFunktionen"] = -1;
            dataRow["ID_Richtlinien"] = DBNull.Value;
            dataRow["ID_KlinischeErgebnisseTypen"] = ID_KlinischeErgebnisseTypen;
            dataRow["OPS-Kode"] = "";
            dataRow["OPS-Text"] = "";
            dataRow["Fallzahl"] = "";
            dataRow["Datum"] = DateTime.Now;
            dataRow["Zeit"] = DateTime.Now;
            dataRow["ZeitBis"] = DateTime.Now;
            dataRow["Quelle"] = BusinessLayer.OperationQuelleIntern;
            dataRow["KlinischeErgebnisse"] = "";

            return dataRow;
        }

        public bool DeletePlanOperation(int nID_PlanOperationen)
        {
            string sb = 
                @"
                DELETE FROM
                    PlanOperationen
                WHERE
                    ID_PlanOperationen=@ID_PlanOperationen
                ";

            ArrayList arSqlParameter = new ArrayList();
            arSqlParameter.Add(this.SqlParameter("@ID_PlanOperationen", nID_PlanOperationen));

            int iEffectedRecords = this.ExecuteNonQuery(sb, arSqlParameter);

            return (iEffectedRecords == 1);
        }

        public bool DeletePlanOperationenForChirurg(int nID_Chirurgen)
        {
            string strSQL = @"
                DELETE FROM
                    PlanOperationen
                WHERE
                    ID_Chirurgen=@ID_Chirurgen
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(SqlParameter("@ID_Chirurgen", nID_Chirurgen));

            int iEffectedRecords = this.ExecuteNonQuery(strSQL, aSQLParameters);

            return true;
        }

        public int InsertPlanOperation(DataRow oDataRow)
        {
            StringBuilder sb = new StringBuilder(
                @"
                INSERT INTO PlanOperationen
                    (
                    ID_Chirurgen,
                    Operation,
                    Anzahl,
                    DatumVon,
                    DatumBis
                    )
                    VALUES
                    (
                     @ID_Chirurgen,
                     @Operation,
                     @Anzahl,
                     @DatumVon,
                     @DatumBis
                    )
                ");


            sb.Replace("@DatumVon", this.DateTime2DBDateTimeString(oDataRow["DatumVon"]));
            sb.Replace("@DatumBis", this.DateTime2DBDateTimeString(oDataRow["DatumBis"]));

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Chirurgen", oDataRow["ID_Chirurgen"]));
            aSQLParameters.Add(this.SqlParameter("@Operation", oDataRow["Operation"]));
            aSQLParameters.Add(this.SqlParameter("@Anzahl", oDataRow["Anzahl"]));
            int iID_NewRecord = this.InsertRecord(sb.ToString(), aSQLParameters, "PlanOperationen");

            return iID_NewRecord;
        }

        public int InsertChirurgenOperationen(DataRow row)
        {
            StringBuilder sb = new StringBuilder(
                @"
                INSERT INTO ChirurgenOperationen
                    (
                    ID_Chirurgen,
                    ID_Richtlinien,
                    ID_OPFunktionen,
                    ID_KlinischeErgebnisseTypen,
                    [OPS-Kode],
                    [OPS-Text],
                    Fallzahl,
                    Datum,
                    Zeit, 
                    ZeitBis,
                    Quelle,
                    KlinischeErgebnisse
                    )
                    VALUES
                    (
                     @ID_Chirurgen,
                     @ID_Richtlinien,
                     @ID_OPFunktionen,
                     @ID_KlinischeErgebnisseTypen,
                     @OPSKode,
                     @OPSText,
                     @Fallzahl,
                     @Datum,
                     @ZeitVon,
                     @ZeitBis,
                     @Quelle,
                     @KlinischeErgebnisse
                    )
                ");

            sb.Replace("@Datum", this.DateTime2DBDateTimeString(row["Datum"]));
            sb.Replace("@ZeitVon", this.DateTime2DBTimeString(row["Zeit"]));
            sb.Replace("@ZeitBis", this.DateTime2DBTimeString(row["ZeitBis"]));

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", row["ID_Chirurgen"]));
            sqlParameters.Add(this.SqlParameterInt("@ID_Richtlinien", row["ID_Richtlinien"]));
            sqlParameters.Add(this.SqlParameter("@ID_OPFunktionen", row["ID_OPFunktionen"]));
            sqlParameters.Add(this.SqlParameter("@ID_KlinischeErgebnisseTypen", row["ID_KlinischeErgebnisseTypen"]));
            sqlParameters.Add(this.SqlParameter("@OPSKode", row["OPS-Kode"]));
            sqlParameters.Add(this.SqlParameter("@OPSText", row["OPS-Text"]));
            sqlParameters.Add(this.SqlParameter("@Fallzahl", row["Fallzahl"]));
            sqlParameters.Add(this.SqlParameter("@Quelle", row["Quelle"]));
            sqlParameters.Add(this.SqlParameter("@KlinischeErgebnisse", row["KlinischeErgebnisse"]));

            return InsertRecord(sb.ToString(), sqlParameters, "ChirurgenOperationen");
        }

        public bool UpdateChirurgenOperationen(DataRow dataRow)
        {
            StringBuilder sb = new StringBuilder(
                @"
                UPDATE ChirurgenOperationen
                    set ID_Richtlinien=@ID_Richtlinien,
                    ID_OPFunktionen=@ID_OPFunktionen,
                    ID_KlinischeErgebnisseTypen=@ID_KlinischeErgebnisseTypen,
                    [OPS-Kode]=@OPSKode,
                    [OPS-Text]=@OPSText,
                    Fallzahl=@Fallzahl,
                    Datum=@Datum,
                    Zeit=@ZeitVon,
                    ZeitBis=@ZeitBis,
                    Quelle=@Quelle,
                    KlinischeErgebnisse=@KlinischeErgebnisse
                WHERE 
                    ID_ChirurgenOperationen=@ID_ChirurgenOperationen
                ");

            sb.Replace("@Datum", this.DateTime2DBDateTimeString(dataRow["Datum"]));
            sb.Replace("@ZeitVon", this.DateTime2DBTimeString(dataRow["Zeit"]));
            sb.Replace("@ZeitBis", this.DateTime2DBTimeString(dataRow["ZeitBis"]));

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameterInt("@ID_Richtlinien", dataRow["ID_Richtlinien"]));
            sqlParameters.Add(this.SqlParameter("@ID_OPFunktionen", dataRow["ID_OPFunktionen"]));
            sqlParameters.Add(this.SqlParameter("@ID_KlinischeErgebnisseTypen", dataRow["ID_KlinischeErgebnisseTypen"]));
            sqlParameters.Add(this.SqlParameter("@OPSKode", dataRow["OPS-Kode"]));
            sqlParameters.Add(this.SqlParameter("@OPSText", dataRow["OPS-Text"]));
            sqlParameters.Add(this.SqlParameter("@Fallzahl", dataRow["Fallzahl"]));
            sqlParameters.Add(this.SqlParameter("@Quelle", dataRow["Quelle"]));
            sqlParameters.Add(this.SqlParameter("@KlinischeErgebnisse", dataRow["KlinischeErgebnisse"]));
            sqlParameters.Add(this.SqlParameter("@ID_ChirurgenOperationen", dataRow["ID_ChirurgenOperationen"]));

            int effectedRecords = ExecuteNonQuery(sb.ToString(), sqlParameters);
            return (effectedRecords == 1);
        }

        public bool UpdatePlanOperation(DataRow oDataRow)
        {
            StringBuilder sb = new StringBuilder(
                @"
                UPDATE PlanOperationen
                SET
                    Operation=@Operation,
                    Anzahl=@Anzahl,
                    DatumVon=@DatumVon,
                    DatumBis=@DatumBis
                WHERE
                    ID_PlanOperationen=@ID_PlanOperationen
                ");

            sb.Replace("@DatumVon", this.DateTime2DBDateTimeString(oDataRow["DatumVon"]));
            sb.Replace("@DatumBis", this.DateTime2DBDateTimeString(oDataRow["DatumBis"]));

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@Operation", (string)oDataRow["Operation"]));
            aSQLParameters.Add(this.SqlParameter("@Anzahl", oDataRow["Anzahl"]));
            aSQLParameters.Add(this.SqlParameter("@ID_PlanOperationen", oDataRow["ID_PlanOperationen"]));

            int iEffectedRecords = this.ExecuteNonQuery(sb.ToString(), aSQLParameters);

            return (iEffectedRecords == 1);
        }
#endregion

#region KlinischeErgebnisseTypen
        public DataView GetKlinischeErgebnisseTypen(bool includeAll)
        {
            string sql =
                @"
                SELECT 
                    ID_KlinischeErgebnisseTypen,
                    [ID],
                    [Text]
                FROM
                    KlinischeErgebnisseTypen
                ORDER BY
                    [ID]
                ";

            DataView dv = GetDataView(sql, null, "KlinischeErgebnisseTypen");

            if (includeAll)
            {
                DataRow row = dv.Table.NewRow();
                row["ID_KlinischeErgebnisseTypen"] = BusinessLayerCommon.ID_Alle;
                row["ID"] = "";
                row["Text"] = TextAlle;

                dv.Table.Rows.InsertAt(row, 0);
            }

            return dv;
        }
#endregion

#region Richtlinien
        public bool DeleteRichtlinie(int ID_Richtlinien)
        {
            string sql = 
                @"
                DELETE FROM
                    Richtlinien
                WHERE
                    ID_Richtlinien=@ID_Richtlinien
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Richtlinien", ID_Richtlinien));

            int effectedRecords = this.ExecuteNonQuery(sql, sqlParameters);

            return (effectedRecords == 1);
        }

        public DataRow CreateDataRowRichtlinie()
        {
            DataTable dt = new DataTable("Richtlinien");

            dt.Columns.Add("ID_Richtlinien", typeof(int));
            dt.Columns.Add("ID_Gebiete", typeof(int));
            dt.Columns.Add("LfdNummer", typeof(int));
            dt.Columns.Add("UntBehMethode", typeof(string));
            dt.Columns.Add("Richtzahl", typeof(int));

            DataRow row = dt.NewRow();

            return row;
        }

        public bool UpdateRichtlinie(DataRow dataRow)
        {
            string sb = @"
                UPDATE 
                    Richtlinien
                SET
                    ID_Gebiete=@ID_Gebiete,
                    LfdNummer=@LfdNummer,
                    UntBehMethode=@UntBehMethode,
                    Richtzahl=@Richtzahl
                WHERE
                    ID_Richtlinien=@ID_Richtlinien
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Gebiete", dataRow["ID_Gebiete"]));
            aSQLParameters.Add(this.SqlParameter("@LfdNummer", dataRow["LfdNummer"]));
            aSQLParameters.Add(this.SqlParameter("@UntBehMethode", (string)dataRow["UntBehMethode"]));
            aSQLParameters.Add(this.SqlParameter("@Richtzahl", dataRow["Richtzahl"]));
            aSQLParameters.Add(this.SqlParameter("@ID_Richtlinien", dataRow["ID_Richtlinien"]));

            int iEffectedRecords = this.ExecuteNonQuery(sb, aSQLParameters);

            return (iEffectedRecords == 1);
        }

        /// <summary>
        /// Wenn useLfdNummer = true, wird der übergebene Wert verwendet, bei Datenimport.
        /// Ansonsten wird MAX(LfdNummer) + 1 als neuer Wert genommen.
        /// </summary>
        /// <param name="oDataRow"></param>
        /// <param name="useLfdNummer"></param>
        /// <returns></returns>
        public int InsertRichtlinie(DataRow oDataRow, bool useLfdNummer)
        {
            string sql;
            ArrayList sqlParameters;
            int ID_Richtlinie = -1;
            int lfdNummer = -1;

            if (useLfdNummer)
            {
                lfdNummer = ConvertToInt32(oDataRow["LfdNummer"]);
            }
            else
            {
                sql = "select count(ID_Richtlinien) from Richtlinien where ID_Gebiete=@ID_Gebiete";
                sqlParameters = new ArrayList();
                sqlParameters.Add(this.SqlParameter("@ID_Gebiete", oDataRow["ID_Gebiete"]));
                long count = ExecuteScalar(sql, sqlParameters);

                if (count == 0)
                {
                    lfdNummer = 1;
                }
                else
                {
                    sql = "select max(LfdNummer)+1 from Richtlinien where ID_Gebiete=@ID_Gebiete";
                    sqlParameters = new ArrayList();
                    sqlParameters.Add(this.SqlParameter("@ID_Gebiete", oDataRow["ID_Gebiete"]));

                    lfdNummer = ExecuteScalarInteger(sql, sqlParameters);
                }
            }

            if (lfdNummer != -1)
            {
                sql =
                    @"
                INSERT INTO Richtlinien
                    (
                    ID_Gebiete,
                    LfdNummer,
                    UntBehMethode,
                    Richtzahl
                    )
                    VALUES
                    (
                     @ID_Gebiete,
                     @LfdNummer,
                     @UntBehMethode,
                     @Richtzahl
                    )
                ";

                sqlParameters = new ArrayList();
                sqlParameters.Add(this.SqlParameter("@ID_Gebiete", oDataRow["ID_Gebiete"]));
                sqlParameters.Add(this.SqlParameter("@LfdNummer", lfdNummer));
                sqlParameters.Add(this.SqlParameter("@UntBehMethode", (string)oDataRow["UntBehMethode"]));
                sqlParameters.Add(this.SqlParameter("@Richtzahl", oDataRow["Richtzahl"]));

                ID_Richtlinie = InsertRecord(sql, sqlParameters, "Richtlinien");
            }

            return ID_Richtlinie;
        }

        public DataView GetRichtlinien()
        {
            string sSQL =
                @"
                SELECT 
                    ID_Richtlinien,
                    ID_Gebiete,
                    LfdNummer,
                    UntBehMethode,
                    Richtzahl
                FROM
                    Richtlinien
                ";

            return GetDataView(sSQL, "Richtlinien");
        }

        public DataRow GetRichtlinie(int ID_Gebiete, int lfdNummer, int richtzahl, string untBehMethode, bool returnFirst)
        {
            string sql =
                @"
                SELECT 
                    ID_Richtlinien,
                    ID_Gebiete,
                    LfdNummer,
                    Richtzahl,
                    UntBehMethode
                FROM
                    Richtlinien
                WHERE
                    ID_Gebiete=@ID_Gebiete
                    and LfdNummer=@LfdNummer
                    and Richtzahl=@Richtzahl
                    and UntBehMethode=@UntBehMethode
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Gebiete", ID_Gebiete));
            sqlParameters.Add(this.SqlParameter("@LfdNummer", lfdNummer));
            sqlParameters.Add(this.SqlParameter("@Richtzahl", richtzahl));
            sqlParameters.Add(this.SqlParameter("@UntBehMethode", untBehMethode));

            return GetRecord(sql, sqlParameters, "Richtlinien", returnFirst);
        }

        public DataRow GetRichtlinie(int ID_Richtlinien)
        {
            string sql =
                @"
                SELECT 
                    ID_Richtlinien,
                    ID_Gebiete,
                    LfdNummer,
                    Richtzahl,
                    UntBehMethode
                FROM
                    Richtlinien
                WHERE
                    ID_Richtlinien=@ID_Richtlinien
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Richtlinien", ID_Richtlinien));

            return GetRecord(sql, sqlParameters, "Richtlinien");
        }

        public DataRow GetRichtlinieForLfdNummerGebiet(int ID_Gebiete, int lfdNummer, bool returnFirst)
        {
            string sql =
                @"
                SELECT 
                    ID_Richtlinien,
                    ID_Gebiete,
                    LfdNummer,
                    Richtzahl,
                    UntBehMethode
                FROM
                    Richtlinien
                WHERE
                    ID_Gebiete=@ID_Gebiete and
                    LfdNummer=@LfdNummer
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Gebiete", ID_Gebiete));
            sqlParameters.Add(this.SqlParameter("@LfdNummer", lfdNummer));

            return GetRecord(sql, sqlParameters, "Richtlinien", returnFirst);
        }

#endregion

#region Auswertungen

        public int GetPlanOperationenSumme(int nID_Chirurgen, string sOperation, DateTime? dtFrom, DateTime? dtTo)
        {
            int nSumme = 0;

            sOperation = CreateLikeExpression(sOperation);

            StringBuilder sb = new StringBuilder(
                @"
                    SELECT 
                        Sum(Anzahl) AS Summe
                    FROM 
                        PlanOperationen
                    WHERE 
                        ID_Chirurgen = @ID_Chirurgen
                        AND Operation LIKE @Operation
                ");

            sb.Append(" AND ");
            sb.Append(this.DateTime2DBDateTimeString(dtFrom));
            sb.Append(" <= DatumBis AND DatumVon <= ");
            sb.Append(this.DateTime2DBDateTimeString(dtTo));

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Chirurgen", nID_Chirurgen));
            aSQLParameters.Add(this.SqlParameter("@Operation", sOperation + "%"));

            DataRow oDataRow = this.GetRecord(sb.ToString(), aSQLParameters, "PlanOperationen");

            if (oDataRow["Summe"] != DBNull.Value)
            {
                nSumme = int.Parse(oDataRow["Summe"].ToString());
            }

            return nSumme;
        }

        public int GetPlanOperationenSumme(int nID_Chirurgen, string sOperation)
        {
            int nSumme = 0;

            sOperation = CreateLikeExpression(sOperation);

            StringBuilder sb = new StringBuilder(
                @"
                    SELECT 
                        Sum(Anzahl) AS Summe
                    FROM 
                        PlanOperationen
                    WHERE 
                        ID_Chirurgen = @ID_Chirurgen
                        AND Operation LIKE @Operation

                ");

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Chirurgen", nID_Chirurgen));
            aSQLParameters.Add(this.SqlParameter("@Operation", sOperation + "%"));

            DataRow oDataRow = this.GetRecord(sb.ToString(), aSQLParameters, "PlanOperationen");

            if (oDataRow["Summe"] != DBNull.Value)
            {
                nSumme = int.Parse(oDataRow["Summe"].ToString());
            }

            return nSumme;
        }

        public long GetChirurgenOperationenAnzahl(int nID_Chirurgen, int nID_OPFunktionen, int quelle, string sOperation, DateTime? dtFrom, DateTime? dtTo)
        {
            sOperation = CreateLikeExpression(sOperation);

            StringBuilder sb = new StringBuilder(
                @"
                    SELECT COUNT(ID_ChirurgenOperationen)
                    FROM ChirurgenOperationen
                    WHERE 
                        ID_Chirurgen = @ID_Chirurgen
                        $opfunktionen$
                        $datefrom$ $dateto$
                        AND [OPS-Kode] LIKE @Operation
                        $quelle$
                ");

            HandleDatum(dtFrom, dtTo, sb);

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", nID_Chirurgen));

            HandleOPFunktionen(sqlParameters, "", nID_OPFunktionen, sb);

            sqlParameters.Add(this.SqlParameter("@Operation", sOperation + "%"));

            HandleQuelleSingle(sqlParameters, "", quelle, sb);

            return ExecuteScalar(sb.ToString(), sqlParameters);
        }

        /// <summary>
        /// Holt den ersten Eintrag aus dem Operationen-Katalog, der zu dem OPS-Kode passt.
        /// </summary>
        /// <param name="sOperation"></param>
        /// <returns></returns>
        public DataRow GetIstOperationForPlanOperation(string sOperation)
        {
            sOperation = CreateLikeExpression(sOperation);

            string sql =
                @"
                    SELECT @@TOP@@
                        ID_Operationen,
                        [OPS-Kode] as Kode,
                        [OPS-Text] as Name
                    FROM Operationen
                    WHERE 
                        Operationen.[OPS-Kode] LIKE @Operation
                        ORDER BY [OPS-Kode]
                        @@LIMIT@@
                ";

            sql = HandleTopLimitStuff(sql, "1");

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@Operation", sOperation + "%"));

            return this.GetRecord(sql, aSQLParameters, "Operationen");
        }

#endregion

#region RichtlinienOpsKodes

        /// <summary>
        /// Get all entries for this guideline which are a subset of this entry.
        /// If this entry is 5-800, then any entry that starts with 5-800 is part of the subset.
        /// </summary>
        /// <param name="ID_Richtlinien"></param>
        /// <param name="opsKode"></param>
        /// <returns></returns>
        public DataView GetRichtlinienOpsKodesSubset(int ID_Richtlinien, string opsKode)
        {
            string sb = @"
                SELECT 
                    [OPS-Kode] as OPSKode
                FROM 
                    RichtlinienOpsKodes
                WHERE
                    ID_Richtlinien=@ID_Richtlinien
                    and [OPS-Kode] like @opsKode
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(SqlParameter("@ID_Richtlinien", ID_Richtlinien));
            sqlParameters.Add(SqlParameter("@opsKode", CreateLikeExpression(opsKode + "%")));

            return GetDataView(sb, sqlParameters, "RichtlinienOpsKodes");
        }

        /// <summary>
        /// Get all entries that are a prefix of this code.
        /// If this code is 5-800.1 then 5, 5-8, 5-80, 5-800 are part of the superset.
        /// If the superset is non empty, it means that this entry is redundant
        /// </summary>
        /// <param name="ID_Richtlinien"></param>
        /// <param name="opsKode"></param>
        /// <returns></returns>
        public DataView GetRichtlinienOpsKodesSuperset(int ID_Richtlinien, string opsKode)
        {
            StringBuilder sb = new StringBuilder(@"
                SELECT 
                    [OPS-Kode] as OPSKode
                FROM 
                    RichtlinienOpsKodes
                WHERE
                    ID_Richtlinien=@ID_Richtlinien
                    and @opsKode like $$LIKE$$
                ");

            sb.Replace("$$LIKE$$", MakeConcat("[OPS-Kode]", "'%'"));

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(SqlParameter("@ID_Richtlinien", ID_Richtlinien));
            sqlParameters.Add(SqlParameter("@opsKode", opsKode));

            return GetDataView(sb.ToString(), sqlParameters, "RichtlinienOpsKodes");
        }

        /// <summary>
        /// Gibt es genau diesen Eintrag schon als Zuordnung?
        /// </summary>
        /// <param name="ID_Richtlinien"></param>
        /// <param name="opsKode"></param>
        /// <returns></returns>
        public int GetRichtlinienOpsKodesCount(int ID_Richtlinien, string opsKode)
        {
            string sql = @"
                select 
                    count(ID_RichtlinienOpsKodes)
                from
                    RichtlinienOpsKodes
                where
                    ID_Richtlinien=@ID_Richtlinien
                    and [OPS-Kode]=@OPSKode
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Richtlinien", ID_Richtlinien));
            sqlParameters.Add(this.SqlParameter("@OPSKode", opsKode));

            return ExecuteScalarInteger(sql, sqlParameters);
        }

        public DataView GetRichtlinienOpsKodes(int nID_Gebiete, bool orderByOPSKode)
        {
            StringBuilder sb = new StringBuilder(@"
                SELECT 
                    ID_RichtlinienOpsKodes, 
                    Richtlinien.LfdNummer,
                    Richtlinien.Richtzahl,
                    Richtlinien.UntBehMethode,
                    RichtlinienOpsKodes.[OPS-Kode],
                    Operationen.[OPS-Text]
                FROM 
                    Richtlinien INNER JOIN (RichtlinienOpsKodes left JOIN Operationen
                                           ON Operationen.[OPS-Kode] = RichtlinienOpsKodes.[OPS-Kode])
                    ON RichtlinienOpsKodes.ID_Richtlinien=Richtlinien.ID_Richtlinien
                WHERE
                    Richtlinien.ID_Gebiete=@ID_Gebiete
                @orderby
                ");

            if (orderByOPSKode)
            {
                sb.Replace("@orderby", "order by 5, 2");
            }
            else
            {
                sb.Replace("@orderby", "order by 2, 5");
            }

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Gebiete", nID_Gebiete));

            return GetDataView(sb.ToString(), aSQLParameters, "RichtlinienOpsKodes");
        }

        public DataView GetRichtlinienOpsKodesRichtlinie(int ID_Richtlinien, bool orderByOPSKode)
        {
            StringBuilder sb = new StringBuilder(@"
                SELECT 
                    ID_RichtlinienOpsKodes, 
                    Richtlinien.LfdNummer,
                    Richtlinien.Richtzahl,
                    Richtlinien.UntBehMethode,
                    RichtlinienOpsKodes.[OPS-Kode],
                    Operationen.[OPS-Text]
                FROM 
                    Richtlinien INNER JOIN (RichtlinienOpsKodes left JOIN Operationen
                                           ON Operationen.[OPS-Kode] = RichtlinienOpsKodes.[OPS-Kode])
                    ON RichtlinienOpsKodes.ID_Richtlinien=Richtlinien.ID_Richtlinien
                WHERE
                    Richtlinien.ID_Richtlinien=@ID_Richtlinien
                @orderby
                ");

            if (orderByOPSKode)
            {
                sb.Replace("@orderby", "order by 5, 2");
            }
            else
            {
                sb.Replace("@orderby", "order by 2, 5");
            }

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Richtlinien", ID_Richtlinien));

            return GetDataView(sb.ToString(), sqlParameters, "RichtlinienOpsKodes");
        }

        public DataView GetRichtlinienOpsKodes()
        {
            string sb = @"
                SELECT 
                    ID_RichtlinienOpsKodes, 
                    ID_Richtlinien,
                    [OPS-Kode]
                FROM 
                    RichtlinienOpsKodes
                ";
            return GetDataView(sb, "RichtlinienOpsKodes");
        }

        public bool DeleteRichtlinienOpsKodes(int nID_RichtlinienOpsKodes)
        {
            StringBuilder sb = new StringBuilder(
                @"
                DELETE FROM
                    RichtlinienOpsKodes
                WHERE
                    ID_RichtlinienOpsKodes=@ID_RichtlinienOpsKodes
                ");

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_RichtlinienOpsKodes", nID_RichtlinienOpsKodes));

            int iEffectedRecords = this.ExecuteNonQuery(sb.ToString(), aSQLParameters);

            return (iEffectedRecords == 1);
        }
        public int InsertRichtlinienOpsKodes(DataRow oDataRow)
        {
            StringBuilder sb = new StringBuilder(
                @"
                INSERT INTO RichtlinienOpsKodes
                    (
                    ID_Richtlinien,
                    [OPS-Kode]
                    )
                    VALUES
                    (
                     @ID_Richtlinien,
                     @OPSKode
                    )
                ");

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Richtlinien", oDataRow["ID_Richtlinien"]));
            aSQLParameters.Add(this.SqlParameter("@OPSKode", (string)oDataRow["OPS-Kode"]));

            return InsertRecord(sb.ToString(), aSQLParameters, "RichtlinienOpsKodes");
        }

        public DataRow CreateDataRowRichtlinienOpsKodes()
        {
            DataTable dt = new DataTable("RichtlinienOpsKodes");

            dt.Columns.Add("ID_RichtlinienOpsKodes", typeof(int));
            dt.Columns.Add("ID_Richtlinien", typeof(int));
            dt.Columns.Add("OPS-Kode", typeof(string));

            DataRow row = dt.NewRow();
            row["ID_RichtlinienOpsKodes"] = -1;
            row["ID_Richtlinien"] = DBNull.Value;
            row["OPS-Kode"] = "";

            return row;
        }


#endregion

#region RichtlinienOPSCodes

        /// <summary>
        /// Alle OPSCodes anzeigen, die für ein Gebiet keine Zuordnung haben, jeden nur einmal
        /// </summary>
        /// <param name="ID_Gebiete"></param>
        /// <param name="quelle"></param>
        /// <param name="dtFrom"></param>
        /// <param name="dtTo"></param>
        /// <returns></returns>
       public DataView GetMissingRichtlinienOPsAlle(int ID_ChirurgenLogin, int ID_Gebiete, int quelle, DateTime? dtFrom, DateTime? dtTo)
        {
            DataTable dt = new DataTable("ChirurgenOperationen");

           // Define the columns of the table.
            dt.Columns.Add(new DataColumn("OPSKode", typeof(string)));
            dt.Columns.Add(new DataColumn("OPSText", typeof(string)));

            DataView dv;

            StringBuilder sql1 = new StringBuilder(@"
                SELECT 
                    ChirurgenOperationen.[OPS-Kode] as OPSKode,
                    max(ChirurgenOperationen.[OPS-Text]) as OPSText
                from
                    ChirurgenOperationen
                WHERE
                    ChirurgenOperationen.ID_Richtlinien is null 
                    and ChirurgenOperationen.ID_OPFunktionen = " + (int)OP_FUNCTION.OP_FUNCTION_OP + @"
                    $datefrom$ $dateto$
                    $quelle$
                    $chirurgen$
                group by 
                    ChirurgenOperationen.[OPS-Kode]
                order by 1
                ");


            HandleDatum(dtFrom, dtTo, sql1, "ChirurgenOperationen");
            ArrayList sqlParameters1 = new ArrayList();
            HandleQuelleMultiple(quelle, "", sql1);
            HandleSubselectID_Chirurgen(sqlParameters1, "ChirurgenOperationen", ID_ChirurgenLogin, sql1);

            if (this.Open())
            {
                try
                {
                    DataView dvOpsCodes = GetDataView(sql1.ToString(), sqlParameters1, "ChirurgenOperationen");

                    //Richtlinien inner JOIN (RichtlinienOpsKodes inner JOIN ChirurgenOperationen ON ChirurgenOperationen.[OPS-Kode] like $$LIKE$$)
                    //sql1.Replace("$$LIKE$$", MakeConcat("RichtlinienOpsKodes.[OPS-Kode]", "'%'"));

                    string sql2 = @"
                            select count(ID_RichtlinienOpsKodes) 
                            from RichtlinienOpsKodes 
                                inner join Richtlinien on RichtlinienOpsKodes.ID_Richtlinien = Richtlinien.ID_Richtlinien
                            where 
                                Richtlinien.ID_Gebiete = @ID_Gebiete
                                and @opsKode like $like$";

                    sql2 = sql2.Replace("$like$", MakeConcat("RichtlinienOpsKodes.[OPS-Kode]", "'%'"));

                    ArrayList sqlParameters2 = new ArrayList();
                    sqlParameters2.Add(this.SqlParameter("@ID_Gebiete", ID_Gebiete));
                    IDbDataParameter paramOpsKode = this.SqlParameter("@opsKode", "addlater");
                    paramOpsKode.Size = 20;
                    sqlParameters2.Add(paramOpsKode);

                    IDbCommand command2 = Connection.CreateCommand();
                    command2.CommandText = CleanSqlStatement(sql2);
                    MapSqlParameter2Command(command2, sqlParameters2);
                    command2.Prepare();

                    foreach (DataRow row in dvOpsCodes.Table.Rows)
                    {
                        string opsKode = (string)row[0];

                        paramOpsKode.Value = opsKode;
                        long count = Convert.ToInt32(command2.ExecuteScalar());
                        if (count == 0)
                        {
                            DataRow dataRow;
                            dataRow = dt.NewRow();

                            dataRow[0] = opsKode;
                            dataRow[1] = (string)row[1];

                            dt.Rows.Add(dataRow);
                        }
                    }
                }
                catch (Exception e)
                {
                    Write2ErrorLog(e.Message);
                }
                finally
                {
                    Close();
                }
            }
            else
            {
                Write2ErrorLog("Could not open connection!");
            }

            dv = new DataView(dt);

            return dv;
        }

        /// <summary>
        /// Holt alle Operationen mit PK, die für einen bestimmten Chirurgen
        /// nicht zu einem bestimmte Gebiet zugeordnet sind.
        /// Damit kann man jede einzelne Operation einzeln identifizieren.
        /// </summary>
        /// <param name="nID_Gebiete"></param>
        /// <param name="nID_Chirurgen"></param>
        /// <param name="dtFrom"></param>
        /// <param name="dtTo"></param>
        /// <returns></returns>
        public DataView GetMissingRichtlinienOPs(int ID_Gebiete, int ID_Chirurgen, int quelle, DateTime? dtFrom, DateTime? dtTo)
        {
            DataTable dt = new DataTable("ChirurgenOperationen");
            DataView dv;

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("ID_ChirurgenOperationen", typeof(int)));
            dt.Columns.Add(new DataColumn("Datum", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("OPSKode", typeof(string)));
            dt.Columns.Add(new DataColumn("OPSText", typeof(string)));

            StringBuilder sql1 = new StringBuilder(@"
                SELECT 
                    ChirurgenOperationen.ID_ChirurgenOperationen,
                    ChirurgenOperationen.Datum,
                    ChirurgenOperationen.[OPS-Kode] as OPSKode,
                    ChirurgenOperationen.[OPS-Text] as OPSText
                from
                    ChirurgenOperationen
                WHERE
                    ChirurgenOperationen.ID_Chirurgen=@ID_Chirurgen
                    and ChirurgenOperationen.ID_Richtlinien is null 
                    and ChirurgenOperationen.ID_OPFunktionen = " + (int)OP_FUNCTION.OP_FUNCTION_OP + @"
                    $datefrom$ $dateto$
                    $quelle$
                order by 2 desc, 3
                    ");

            HandleDatum(dtFrom, dtTo, sql1, "ChirurgenOperationen");
            ArrayList sqlParameters1 = new ArrayList();
            sqlParameters1.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
            HandleQuelleMultiple(quelle, "", sql1);

            if (Open())
            {
                if (Open2())
                {
                    IDataReader reader1 = null;
                    try
                    {
                        IDbCommand command1 = Connection.CreateCommand();
                        command1.CommandText = CleanSqlStatement(sql1.ToString());
                        MapSqlParameter2Command(command1, sqlParameters1);

                        string sql2 = @"
                                select count(ID_RichtlinienOpsKodes) 
                                from RichtlinienOpsKodes 
                                    inner join Richtlinien on RichtlinienOpsKodes.ID_Richtlinien = Richtlinien.ID_Richtlinien
                                where 
                                    Richtlinien.ID_Gebiete = @ID_Gebiete
                                    and @opsKode like $like$";

                        sql2 = sql2.Replace("$like$", MakeConcat("RichtlinienOpsKodes.[OPS-Kode]", "'%'"));

                        ArrayList sqlParameters2 = new ArrayList();
                        IDbDataParameter paramOpsKode = this.SqlParameter("@opsKode", "prepare");
                        paramOpsKode.Size = 20;
                        sqlParameters2.Add(this.SqlParameter("@ID_Gebiete", ID_Gebiete));
                        sqlParameters2.Add(paramOpsKode);

                        IDbCommand command2 = Connection2.CreateCommand();
                        command2.CommandText = CleanSqlStatement(sql2);
                        MapSqlParameter2Command(command2, sqlParameters2);
                        command2.Prepare();

                        reader1 = command1.ExecuteReader();

                        while (reader1.Read())
                        {
                            string opsKode = reader1.GetString(2);

                            paramOpsKode.Value = opsKode;

                            // MySQL cannot handle this!

                            // SQLServer: Must have "MultipleActiveResultSets=True;" in connection string or get error
                            // "There is already an open DataReader associated with this Command which must be closed first."
                            long count = Convert.ToInt32(command2.ExecuteScalar());
                            if (count == 0)
                            {
                                DataRow dataRow;
                                dataRow = dt.NewRow();

                                dataRow[0] = reader1.GetInt32(0);
                                dataRow[1] = reader1.GetDateTime(1);
                                dataRow[2] = opsKode;
                                dataRow[3] = reader1.GetString(3);

                                dt.Rows.Add(dataRow);
                            }
                        }
                        reader1.Close();
                        reader1.Dispose();
                        reader1 = null;
                    }
                    catch (Exception e)
                    {
                        Write2ErrorLog(e.Message);
                    }
                    finally
                    {
                        if (reader1 != null && !reader1.IsClosed)
                        {
                            reader1.Close();
                            reader1.Dispose();
                            reader1 = null;
                        }
                        Close();
                    }
                }
                else
                {
                    Write2ErrorLog("Could not open second connection.");
                }
            }
            else
            {
                Write2ErrorLog("Could not open first connection.");
            }

            dv = new DataView(dt);

            return dv;
        }

        public DataView GetRichtlinienOPSummenChirurgRichtlinie(int ID_Chirurgen, int ID_Richtlinien, int quelle, DateTime? von, DateTime? bis)
        {
            StringBuilder sb = new StringBuilder(@"
            SELECT 
                count(ChirurgenOperationen.ID_ChirurgenOperationen) as Anzahl
            FROM 
                RichtlinienOpsKodes left JOIN ChirurgenOperationen ON ChirurgenOperationen.[OPS-Kode] like $$LIKE$$
            where 
                ChirurgenOperationen.ID_Richtlinien is null and 
                RichtlinienOpsKodes.ID_Richtlinien = @ID_Richtlinien and 
                ChirurgenOperationen.ID_Chirurgen = @ID_Chirurgen and 
                ChirurgenOperationen.ID_OPFunktionen = " + (int)OP_FUNCTION.OP_FUNCTION_OP + @"   
                $datefrom$ $dateto$
                $quelle$

            UNION ALL

            SELECT 
                count(ChirurgenOperationen.ID_ChirurgenOperationen) as Anzahl
            FROM 
                ChirurgenOperationen
            where
                ChirurgenOperationen.ID_Richtlinien = @ID_Richtlinien and 
                ChirurgenOperationen.ID_Chirurgen = @ID_Chirurgen and 
                ChirurgenOperationen.ID_OPFunktionen = " + (int)OP_FUNCTION.OP_FUNCTION_OP + @"
                $datefrom$ $dateto$
                $quelle$
            ");

            HandleDatum(von, bis, sb, "ChirurgenOperationen");

            sb.Replace("$$LIKE$$", MakeConcat("RichtlinienOpsKodes.[OPS-Kode]", "'%'"));

            if (DatabaseType == DatabaseType.MSAccess)
            {
                //
                // group by auf ein Memo Feld geht nicht in Access
                //
                sb.Replace("$$UntBehMethode$$", "left(Richtlinien.UntBehMethode, 255)");
            }
            else
            {
                sb.Replace("$$UntBehMethode$$", "Richtlinien.UntBehMethode");
            }

            ArrayList sqlParameter = new ArrayList();
            sqlParameter.Add(this.SqlParameter("@ID_Richtlinien", ID_Richtlinien));
            sqlParameter.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));

            HandleQuelleMultiple(quelle, "", sb);

            return GetDataView(sb.ToString(), sqlParameter, "RichtlinienOPSCodes");
        }
#endregion

#region
        public DataRow CreateDataRowChirurgenDokumente(int nID_Chirurgen, int nID_Dokumente)
        {
            DataTable dt = new DataTable("ChirurgenDokumente");
            DataRow dataRow;

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("ID_ChirurgenDokumente", typeof(int)));
            dt.Columns.Add(new DataColumn("ID_Chirurgen", typeof(int)));
            dt.Columns.Add(new DataColumn("ID_Dokumente", typeof(int)));
            dt.Columns.Add(new DataColumn("Blob", typeof(byte[])));
            dt.Columns.Add(new DataColumn("InBearbeitung", typeof(int)));

            dataRow = dt.NewRow();

            dataRow["ID_ChirurgenDokumente"] = -1;
            dataRow["ID_Chirurgen"] = nID_Chirurgen;
            dataRow["ID_Dokumente"] = nID_Dokumente;
            dataRow["Blob"] = DBNull.Value;
            dataRow["InBearbeitung"] = 0;

            return dataRow;
        }
        public int InsertChirurgenDokumente(DataRow oDataRow)
        {
            string sb = @"
                INSERT INTO ChirurgenDokumente
                    (
                    ID_Chirurgen,
                    ID_Dokumente,
                    [Blob],
                    InBearbeitung,
                    BearbeitungsDatum
                    )
                    VALUES
                    (
                     @ID_Chirurgen,
                     @ID_Dokumente,
                     @Blob,
                    0,
                    @BearbeitungsDatum
                    )
                ";

            sb = sb.Replace("@BearbeitungsDatum", this.DateTime2DBDateTimeString(DateTime.Today));

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Chirurgen", oDataRow["ID_Chirurgen"]));
            aSQLParameters.Add(this.SqlParameter("@ID_Dokumente", oDataRow["ID_Dokumente"]));
            aSQLParameters.Add(this.SqlParameter("@Blob", (byte[])oDataRow["Blob"]));

            return InsertRecord(sb, aSQLParameters, "ChirurgenDokumente");
        }

        public bool DeleteChirurgenDokumente(int nID_ChirurgenDokumente)
        {
            string strSQL = @"
                DELETE FROM
                    ChirurgenDokumente
                WHERE
                    ID_ChirurgenDokumente=@ID_ChirurgenDokumente
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(SqlParameter("@ID_ChirurgenDokumente", nID_ChirurgenDokumente));

            int iEffectedRecords = this.ExecuteNonQuery(strSQL, aSQLParameters);

            return true;
        }

        public DataView GetChirurgenDokumenteTable(int nID_Chirurgen)
        {
            string sb = @"
                SELECT 
                    ID_ChirurgenDokumente,
                    ID_Chirurgen,
                    ID_Dokumente,
                    [Blob],
                    InBearbeitung,
                    Bearbeitungsdatum
                FROM 
                    ChirurgenDokumente
                where
                    ID_Chirurgen = @ID_Chirurgen
                    ";

            ArrayList arSqlParameter = new ArrayList();
            arSqlParameter.Add(this.SqlParameter("@ID_Chirurgen", nID_Chirurgen));

            return GetDataView(sb, arSqlParameter, "ChirurgenDokumente");
        }

        public DataView GetChirurgenDokumente(int nID_Chirurgen)
        {
            string sb = @"
                SELECT 
                    ID_ChirurgenDokumente,
                    ChirurgenDokumente.InBearbeitung,
                    ChirurgenDokumente.Bearbeitungsdatum,
                    Dokumente.Beschreibung
                FROM 
                    ChirurgenDokumente
                    inner join Dokumente on ChirurgenDokumente.ID_Dokumente = Dokumente.ID_Dokumente
                where
                    ChirurgenDokumente.ID_Chirurgen = @ID_Chirurgen
                    ";

            ArrayList arSqlParameter = new ArrayList();
            arSqlParameter.Add(this.SqlParameter("@ID_Chirurgen", nID_Chirurgen));

            return GetDataView(sb, arSqlParameter, "ChirurgenDokumente");
        }

        public DataRow GetChirurgenDokument(int nID_ChirurgenDokumente)
        {
            string sb = @"
                SELECT 
                    ID_ChirurgenDokumente,
                    ID_Chirurgen,
                    ID_Dokumente,
                    [Blob],
                    InBearbeitung,
                    Bearbeitungsdatum
                FROM 
                    ChirurgenDokumente
                where
                    ID_ChirurgenDokumente = @ID_ChirurgenDokumente
                    ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_ChirurgenDokumente", nID_ChirurgenDokumente));

            return GetRecord(sb, sqlParameters, "ChirurgenDokumente");
        }

        public bool UpdateChirurgenDokumente(DataRow oDataRow)
        {
            string sb = @"
                UPDATE ChirurgenDokumente
                SET
                    InBearbeitung = 0,
                    [Blob]=@Blob
                WHERE
                    ID_ChirurgenDokumente=@ID_ChirurgenDokumente
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@Blob", (byte[])oDataRow["Blob"]));
            aSQLParameters.Add(this.SqlParameter("@ID_ChirurgenDokumente", oDataRow["ID_ChirurgenDokumente"]));

            int iEffectedRecords = this.ExecuteNonQuery(sb, aSQLParameters);

            return (iEffectedRecords == 1);
        }

        public bool UpdateChirurgenDokumenteBearbeitung(int nID_ChirurgenDokumente)
        {
            string sb = @"
                UPDATE 
                    ChirurgenDokumente
                SET
                    InBearbeitung = 1,
                    Bearbeitungsdatum=@Bearbeitungsdatum
                WHERE
                    ID_ChirurgenDokumente=@ID_ChirurgenDokumente
                ";

            sb = sb.Replace("@Bearbeitungsdatum", this.DateTime2DBDateTimeString(DateTime.Today));

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_ChirurgenDokumente", nID_ChirurgenDokumente));

            int iEffectedRecords = this.ExecuteNonQuery(sb, aSQLParameters);

            return (iEffectedRecords == 1);
        }

#endregion

#region Dokumente
        public DataRow GetDokument(int nID_Dokumente)
        {
            string sb = @"
                SELECT 
                    ID_Dokumente,
                    Gruppe,
                    LfdNummer,
                    Beschreibung,
                    Dateiname
                FROM 
                    Dokumente
                WHERE
                    ID_Dokumente=@ID_Dokumente
                    ";

            ArrayList arSqlParameter = new ArrayList();
            arSqlParameter.Add(this.SqlParameter("@ID_Dokumente", nID_Dokumente));

            return GetRecord(sb, arSqlParameter, "Dokumente");
        }

        public DataView GetDokumente()
        {
            string sb = @"
                SELECT 
                    ID_Dokumente,
                    Gruppe,
                    LfdNummer,
                    Beschreibung,
                    Dateiname
                FROM 
                    Dokumente
                order by
                    Gruppe,
                    LfdNummer
                    ";

            return GetDataView(sb, null, "Dokumente");
        }

        public bool DeleteDokumenteForChirurg(int nID_Chirurgen)
        {
            string strSQL = @"
                DELETE FROM
                    ChirurgenDokumente
                WHERE
                    ID_Chirurgen=@ID_Chirurgen
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(SqlParameter("@ID_Chirurgen", nID_Chirurgen));

            int iEffectedRecords = this.ExecuteNonQuery(strSQL, aSQLParameters);

            return true;
        }

        public bool DeleteDokument(int ID_Dokumente)
        {
            string sql =
                @"
                DELETE FROM
                    Dokumente
                WHERE
                    ID_Dokumente=@ID_Dokumente
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Dokumente", ID_Dokumente));

            int effectedRecords = this.ExecuteNonQuery(sql, sqlParameters);

            return (effectedRecords == 1);
        }
        public DataRow CreateDataRowDokument()
        {
            DataTable dt = new DataTable("Dokumente");

            dt.Columns.Add("ID_Dokumente", typeof(int));
            dt.Columns.Add("LfdNummer", typeof(int));
            dt.Columns.Add("Gruppe", typeof(string));
            dt.Columns.Add("Beschreibung", typeof(string));
            dt.Columns.Add("Dateiname", typeof(string));

            return dt.NewRow();
        }
        public bool UpdateDokument(DataRow dataRow)
        {
            string sql = @"
                UPDATE 
                    Dokumente
                SET
                    Gruppe=@Gruppe,
                    Beschreibung=@Beschreibung,
                    LfdNummer=@LfdNummer
                WHERE
                    ID_Dokumente=@ID_Dokumente
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@Gruppe", (string)dataRow["Gruppe"]));
            sqlParameters.Add(this.SqlParameter("@Beschreibung", (string)dataRow["Beschreibung"]));
            sqlParameters.Add(this.SqlParameter("@LfdNummer", dataRow["LfdNummer"]));
            sqlParameters.Add(this.SqlParameter("@ID_Dokumente", dataRow["ID_Dokumente"]));

            int iEffectedRecords = ExecuteNonQuery(sql, sqlParameters);

            return (iEffectedRecords == 1);
        }
        public int InsertDokument(DataRow oDataRow)
        {
            int ID_Dokumente = -1;
            int lfdNummer = -1;

            string sql = "select count(ID_Dokumente) from Dokumente";
            long count = ExecuteScalar(sql, null);

            if (count == 0)
            {
                lfdNummer = 1;
            }
            else
            {
                // max() liefert einen Fehler wenn es keinen Datensatz gibt.
                sql = "select max(LfdNummer)+1 from Dokumente";

                lfdNummer = ExecuteScalarInteger(sql, null);
            }

            if (lfdNummer != -1)
            {
                sql =
                    @"
                INSERT INTO Dokumente
                    (
                    LfdNummer,
                    Gruppe,
                    Beschreibung,
                    Dateiname
                    )
                    VALUES
                    (
                     @LfdNummer,
                     @Gruppe,
                     @Beschreibung,
                     @Dateiname
                    )
                ";

                ArrayList sqlParameters = new ArrayList();
                sqlParameters.Add(this.SqlParameter("@LfdNummer", lfdNummer));
                sqlParameters.Add(this.SqlParameter("@Gruppe", (string)oDataRow["Gruppe"]));
                sqlParameters.Add(this.SqlParameter("@Beschreibung", (string)oDataRow["Beschreibung"]));
                sqlParameters.Add(this.SqlParameter("@Dateiname", (string)oDataRow["Dateiname"]));

                ID_Dokumente = InsertRecord(sql, sqlParameters, "Dokumente");
            }

            return ID_Dokumente;
        }

#endregion

#region ImportChirurgenExclude
        public DataRow CreateDataRowImportChirurgenExclude()
        {
            DataTable dt = new DataTable("ImportChirurgenExclude");
            DataRow dataRow;

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("Nachname", typeof(string)));
            dt.Columns.Add(new DataColumn("Vorname", typeof(string)));

            dataRow = dt.NewRow();

            dataRow["Nachname"] = "";
            dataRow["Vorname"] = "";

            return dataRow;
        }
        public DataView GetImportChirurgenExclude()
        {
            string sb = @"
                SELECT 
                    ID_ImportChirurgenExclude,
                    Nachname,
                    Vorname
                FROM 
                    ImportChirurgenExclude
                order by
                    Nachname,
                    Vorname
                    ";

            return GetDataView(sb, null, "ImportChirurgenExclude");
        }
        public int GetID_ImportChirurgenExclude(string strNachname)
        {
            int nID = -1;

            string sb = @"
                SELECT 
                    ID_ImportChirurgenExclude
                FROM 
                    ImportChirurgenExclude
                WHERE 
                    Nachname=@Nachname
                    ";

            ArrayList arSqlParameter = new ArrayList();
            arSqlParameter.Add(this.SqlParameter("@Nachname", strNachname));

            DataRow oRow = GetRecord(sb, arSqlParameter, "ImportChirurgenExclude");
            if (oRow != null)
            {
                nID = ConvertToInt32(oRow["ID_ImportChirurgenExclude"]);
            }

            return nID;
        }

        public int GetID_ImportChirurgenExclude(string strNachname, string strVorname)
        {
            int nID = -1;

            string sb = @"
                SELECT 
                    ID_ImportChirurgenExclude
                FROM 
                    ImportChirurgenExclude
                WHERE 
                    Nachname=@Nachname and
                    Vorname=@Vorname
                    ";

            ArrayList arSqlParameter = new ArrayList();
            arSqlParameter.Add(this.SqlParameter("@Nachname", strNachname));
            arSqlParameter.Add(this.SqlParameter("@Vorname", strVorname));

            DataRow oRow = GetRecord(sb, arSqlParameter, "ImportChirurgenExclude");
            if (oRow != null)
            {
                nID = ConvertToInt32(oRow["ID_ImportChirurgenExclude"]);
            }

            return nID;
        }

        public bool DeleteImportChirurgenExclude(int nID)
        {
            string sb =
                @"
                DELETE FROM
                    ImportChirurgenExclude
                WHERE
                    ID_ImportChirurgenExclude=@ID_ImportChirurgenExclude
                ";

            ArrayList arSqlParameter = new ArrayList();
            arSqlParameter.Add(this.SqlParameter("@ID_ImportChirurgenExclude", nID));

            int iEffectedRecords = this.ExecuteNonQuery(sb, arSqlParameter);

            return (iEffectedRecords == 1);
        }
        public int InsertImportChirurgenExclude(DataRow oDataRow)
        {
            string sb =
                @"
                INSERT INTO ImportChirurgenExclude
                    (
                    Nachname,
                    Vorname
                    )
                    VALUES
                    (
                     @Nachname,
                     @Vorname
                    )
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@Nachname", (string)oDataRow["Nachname"]));
            aSQLParameters.Add(this.SqlParameter("@Vorname", (string)oDataRow["Vorname"]));

            return this.InsertRecord(sb, aSQLParameters, "ImportChirurgenExclude");
        }
#endregion

#region Gebiete
        public bool UpdateGebiet(DataRow dataRow)
        {
            string sb = @"
                UPDATE 
                    Gebiete
                SET
                    Gebiet=@Gebiet,
                    Bemerkung=@Bemerkung,
                    Herkunft=@Herkunft
                WHERE
                    ID_Gebiete=@ID_Gebiete
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@Gebiet", (string)dataRow["Gebiet"]));
            aSQLParameters.Add(this.SqlParameter("@Bemerkung", (string)dataRow["Bemerkung"]));
            aSQLParameters.Add(this.SqlParameter("@Herkunft", (string)dataRow["Herkunft"]));
            aSQLParameters.Add(this.SqlParameter("@ID_Gebiete", dataRow["ID_Gebiete"]));

            int iEffectedRecords = this.ExecuteNonQuery(sb, aSQLParameters);

            return (iEffectedRecords == 1);
        }
        public DataRow CreateDataRowGebiet()
        {
            DataTable dt = new DataTable("Gebiete");
            DataRow dataRow;

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("Gebiet", typeof(string)));
            dt.Columns.Add(new DataColumn("Bemerkung", typeof(string)));
            dt.Columns.Add(new DataColumn("Herkunft", typeof(string)));

            dataRow = dt.NewRow();

            dataRow["Gebiet"] = "";
            dataRow["Bemerkung"] = "";
            dataRow["Herkunft"] = "";

            return dataRow;
        }
        public DataRow GetGebiet(int ID_Gebiete)
        {
            string sb = @"
                SELECT 
                    ID_Gebiete,
                    Gebiet,
                    Bemerkung,
                    Herkunft
                FROM 
                    Gebiete
                WHERE
                    ID_Gebiete=@ID_Gebiete
                order by
                    Gebiet
                    ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Gebiete", ID_Gebiete));

            return GetRecord(sb, sqlParameters, "Gebiete");
        }

        public bool DeleteGebiet(int nID)
        {
            string sb =
                @"
                DELETE FROM
                    Gebiete
                WHERE
                    ID_Gebiete=@ID_Gebiete
                ";

            ArrayList arSqlParameter = new ArrayList();
            arSqlParameter.Add(this.SqlParameter("@ID_Gebiete", nID));

            int iEffectedRecords = this.ExecuteNonQuery(sb, arSqlParameter);

            return (iEffectedRecords == 1);
        }
        public int InsertGebiet(DataRow oDataRow)
        {
            string sb =
                @"
                INSERT INTO Gebiete
                    (Gebiet, Bemerkung, Herkunft)
                    VALUES
                    (@Gebiet, @Bemerkung, @Herkunft)
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@Gebiet", (string)oDataRow["Gebiet"]));
            aSQLParameters.Add(this.SqlParameter("@Bemerkung", (string)oDataRow["Bemerkung"]));
            aSQLParameters.Add(this.SqlParameter("@Herkunft", (string)oDataRow["Herkunft"]));

            return this.InsertRecord(sb, aSQLParameters, "Gebiete");
        }
#endregion

#region Notizen
        public bool UpdateNotiz(DataRow row)
        {
            StringBuilder sb = new StringBuilder(@"
                UPDATE 
                    Notizen
                SET
                    Datum=@Datum,
                    Ende=@Ende,
                    Notiz=@Notiz,
                    ID_NotizTypen=@ID_NotizTypen
                WHERE
                    ID_Notizen=@ID_Notizen
                ");

            sb.Replace("@Datum", Object2DBDateString(row["Datum"]));
            sb.Replace("@Ende", Object2DBDateString(row["Ende"]));

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@Notiz", (string)row["Notiz"]));
            sqlParameters.Add(this.SqlParameter("@ID_NotizTypen", row["ID_NotizTypen"]));
            sqlParameters.Add(this.SqlParameter("@ID_Notizen", row["ID_Notizen"]));

            int iEffectedRecords = this.ExecuteNonQuery(sb.ToString(), sqlParameters);

            return (iEffectedRecords == 1);
        }

        public DataRow CreateDataRowNotiz()
        {
            DataTable dt = new DataTable("Notizen");
            DataRow dataRow;

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("ID_Notizen", typeof(int)));
            dt.Columns.Add(new DataColumn("ID_NotizTypen", typeof(int)));
            dt.Columns.Add(new DataColumn("ID_Chirurgen", typeof(int)));
            dt.Columns.Add(new DataColumn("Datum", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Ende", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Notiz", typeof(string)));

            dataRow = dt.NewRow();

            dataRow["Datum"] = DateTime.Now;
            dataRow["Ende"] = DBNull.Value;
            dataRow["Notiz"] = "";

            return dataRow;
        }
        public DataRow GetNotiz(int ID_Notizen)
        {
            string sb = @"
                SELECT 
                    ID_Notizen,
                    ID_NotizTypen,
                    ID_Chirurgen,
                    Datum,
                    Ende,
                    Notiz
                FROM 
                    Notizen
                WHERE
                    ID_Notizen=@ID_Notizen
                    ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Notizen", ID_Notizen));

            return GetRecord(sb, sqlParameters, "Notizen");
        }

        public DataView GetNotizen(int ID_Chirurgen, int ID_NotizTypen)
        {
            string sb = @"
                SELECT 
                    a.ID_Notizen,
                    a.ID_NotizTypen,
                    a.ID_Chirurgen,
                    a.Datum,
                    a.Ende,
                    a.Notiz,
                    b.[Text]
                FROM 
                    Notizen as a inner join NotizTypen b on (a.ID_NotizTypen = b.ID_NotizTypen)
                WHERE
                    a.ID_Chirurgen=@ID_Chirurgen
                    and a.ID_NotizTypen=@ID_NotizTypen
                order by
                    a.Datum desc
                    ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
            sqlParameters.Add(this.SqlParameter("@ID_NotizTypen", ID_NotizTypen));

            return GetDataView(sb, sqlParameters, "Notizen");
        }
        public DataView GetNotizen(int ID_Chirurgen)
        {
            string sb = @"
                SELECT 
                    a.ID_Notizen,
                    a.ID_NotizTypen,
                    a.ID_Chirurgen,
                    a.Datum,
                    a.Ende,
                    a.Notiz,
                    b.[Text]
                FROM 
                    Notizen as a inner join NotizTypen b on (a.ID_NotizTypen = b.ID_NotizTypen)
                WHERE
                    a.ID_Chirurgen=@ID_Chirurgen
                order by
                    a.Datum desc
                    ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));

            return GetDataView(sb, sqlParameters, "Notizen");
        }

        public bool DeleteNotiz(int nID)
        {
            string sb =
                @"
                DELETE FROM
                    Notizen
                WHERE
                    ID_Notizen=@ID_Notizen
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Notizen", nID));

            int iEffectedRecords = this.ExecuteNonQuery(sb, sqlParameters);

            return (iEffectedRecords == 1);
        }
        public int InsertNotiz(DataRow row)
        {
            StringBuilder sb = new StringBuilder(
                @"
                INSERT INTO Notizen
                    (
                    ID_Chirurgen, 
                    ID_NotizTypen, 
                    Datum, 
                    Ende, 
                    Notiz
                    )
                    VALUES
                    (
                    @ID_Chirurgen, 
                    @ID_NotizTypen, 
                    @Datum, 
                    @Ende, 
                    @Notiz
                    )
                ");

            sb.Replace("@Datum", Object2DBDateString(row["Datum"]));
            sb.Replace("@Ende", Object2DBDateString(row["Ende"]));

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", row["ID_Chirurgen"]));
            sqlParameters.Add(this.SqlParameter("@ID_NotizTypen", row["ID_NotizTypen"]));
            sqlParameters.Add(this.SqlParameter("@Notiz", (string)row["Notiz"]));

            return this.InsertRecord(sb.ToString(), sqlParameters, "Notizen");
        }
#endregion

#region NotizTypen
        public DataRow CreateDataRowNotizTypen()
        {
            DataTable dt = new DataTable("NotizTypen");
            DataRow dataRow;

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("Text", typeof(string)));

            dataRow = dt.NewRow();

            dataRow["Text"] = "";

            return dataRow;
        }
        public DataRow GetNotizTypen(int ID_NotizTypen)
        {
            string sql = @"
                SELECT 
                    ID_NotizTypen,
                    [Text]
                FROM 
                    NotizTypen
                WHERE
                    ID_NotizTypen=@ID_NotizTypen
                    ";

            ArrayList sqlParameter = new ArrayList();
            sqlParameter.Add(this.SqlParameter("@ID_NotizTypen", ID_NotizTypen));

            return GetRecord(sql, sqlParameter, "NotizTypen");
        }

        public DataView GetNotizTypen(bool includeAll)
        {
            string sql = @"
                SELECT 
                    ID_NotizTypen,
                    [Text]
                FROM 
                    NotizTypen
                order by
                    [Text]
                    ";

            DataView dv = GetDataView(sql, null, "NotizTypen");

            if (includeAll)
            {
                DataRow row = dv.Table.NewRow();
                row["ID_NotizTypen"] = BusinessLayer.ID_Alle;
                row["Text"] = TextAlle;
                dv.Table.Rows.InsertAt(row, 0);
            }

            return dv;
        }

        public bool DeleteNotizTyp(int nID)
        {
            string sb =
                @"
                DELETE FROM
                    NotizTypen
                WHERE
                    ID_NotizTypen=@ID_NotizTypen
                ";

            ArrayList arSqlParameter = new ArrayList();
            arSqlParameter.Add(this.SqlParameter("@ID_NotizTypen", nID));

            int iEffectedRecords = this.ExecuteNonQuery(sb, arSqlParameter);

            return (iEffectedRecords == 1);
        }
        public int InsertNotizTyp(DataRow oDataRow)
        {
            string sb =
                @"
                INSERT INTO NotizTypen
                    ([Text])
                    VALUES
                    (@Text)
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@Text", (string)oDataRow["Text"]));

            return this.InsertRecord(sb, aSQLParameters, "NotizTypen");
        }
        public int UpdateNotizTyp(DataRow oDataRow)
        {
            string sql =
                @"
                UPDATE NotizTypen set
                    [Text]=@Text
                WHERE
                    ID_NotizTypen=@ID_NotizTypen
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@Text", (string)oDataRow["Text"]));
            sqlParameters.Add(this.SqlParameter("@ID_NotizTypen", oDataRow["ID_NotizTypen"]));

            return this.ExecuteNonQuery(sql, sqlParameters);
        }

#endregion


#region TypenTemplate
        public DataRow CreateDataRowTypenTemplate(string table, string text)
        {
            DataTable dt = new DataTable(table);
            DataRow dataRow;

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn(text, typeof(string)));

            dataRow = dt.NewRow();

            dataRow[text] = "";

            return dataRow;
        }

        public DataRow GetTypenTemplate(string table, string text, int id)
        {
            StringBuilder sb = new StringBuilder(@"
                SELECT 
                    ID_" + table + @" as ID,
                    [@text]
                FROM 
                    " + table + @"
                WHERE
                    ID_" + table + @"=@ID
                    ");

            sb.Replace("@text", text);

            ArrayList sqlParameter = new ArrayList();
            sqlParameter.Add(this.SqlParameter("@ID", id));

            return GetRecord(sb.ToString(), sqlParameter, table);
        }

        public DataView GetTypenTemplate(string table, string text, bool includeAll)
        {
            StringBuilder sb = new StringBuilder(@"
                SELECT 
                    ID_" + table + @" as ID,
                    [@text]
                FROM 
                    " + table + @"
                order by
                    [@text]
                    ");

            sb.Replace("@text", text);

            DataView dv = GetDataView(sb.ToString(), null, table);

            if (includeAll)
            {
                DataRow row = dv.Table.NewRow();
                row["ID"] = BusinessLayer.ID_Alle;
                row[text] = TextAlle;
                dv.Table.Rows.InsertAt(row, 0);
            }

            return dv;
        }

        public bool DeleteTypenTemplate(string table, int id)
        {
            string sql = "DELETE FROM " + table + " WHERE ID_" + table + "=@ID";

            ArrayList arSqlParameter = new ArrayList();
            arSqlParameter.Add(this.SqlParameter("@ID", id));

            int iEffectedRecords = this.ExecuteNonQuery(sql, arSqlParameter);

            return iEffectedRecords == 1;
        }

        public int InsertTypenTemplate(string table, string text, DataRow oDataRow)
        {
            string sql = "INSERT INTO " + table + " ([" + text + "]) VALUES (@Text)";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@Text", (string)oDataRow[text]));

            return this.InsertRecord(sql, sqlParameters, table);
        }

        public int UpdateTypenTemplate(string table, string textColumn, DataRow oDataRow)
        {
            StringBuilder sb = new StringBuilder(
                @"
                UPDATE " + table + @" set
                    [@textColumn]=@Text
                WHERE
                    ID_" + table + @" = @ID
                ");

            sb.Replace("@textColumn", textColumn);

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@Text", (string)oDataRow[textColumn]));
            sqlParameters.Add(this.SqlParameter("@ID", oDataRow["ID"]));

            return this.ExecuteNonQuery(sb.ToString(), sqlParameters);
        }

#endregion


#region Dateien
        public DataRow CreateDataRowDatei()
        {
            DataTable dt = new DataTable("Datei");
            DataRow dataRow;

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("ID_Dateien", typeof(int)));
            dt.Columns.Add(new DataColumn("ID_DateiTypen", typeof(int)));
            dt.Columns.Add(new DataColumn("Dateiname", typeof(string)));
            dt.Columns.Add(new DataColumn("Beschreibung", typeof(string)));

            dataRow = dt.NewRow();

            dataRow["Dateiname"] = "";
            dataRow["Beschreibung"] = "";

            return dataRow;
        }
        public DataView GetDateien()
        {
            string sb = @"
                SELECT 
                    Dateien.ID_Dateien,
                    Dateien.Dateiname,
                    Dateien.Beschreibung,
                    DateiTypen.ID_DateiTypen,
                    DateiTypen.DateiTyp
                FROM 
                    Dateien
                    inner join DateiTypen on DateiTypen.ID_DateiTypen=Dateien.ID_DateiTypen
                ORDER by
                    Dateityp,  
                    Dateiname
                    ";

            return GetDataView(sb, null, "Dateien");
        }

        public DataView GetDateien(int ID_DateiTypen)
        {
            string sb = @"
                SELECT 
                    ID_Dateien,
                    Dateiname,
                    Beschreibung
                FROM 
                    Dateien
                WHERE
                    ID_DateiTypen=@ID_DateiTypen
                    ";

            ArrayList arSqlParameter = new ArrayList();
            arSqlParameter.Add(this.SqlParameter("@ID_DateiTypen", ID_DateiTypen));

            return GetDataView(sb, arSqlParameter, "Dateien");
        }

        public DataRow GetDatei(int ID_Dateien)
        {
            string sb = @"
                SELECT 
                    Dateien.ID_Dateien,
                    Dateien.Dateiname,
                    Dateien.Beschreibung,
                    DateiTypen.ID_DateiTypen,
                    DateiTypen.DateiTyp
                FROM 
                    Dateien
                    inner join DateiTypen on DateiTypen.ID_DateiTypen=Dateien.ID_DateiTypen
                WHERE 
                    ID_Dateien=@ID_Dateien
                    ";

            ArrayList arSqlParameter = new ArrayList();
            arSqlParameter.Add(this.SqlParameter("@ID_Dateien", ID_Dateien));

            return GetRecord(sb, arSqlParameter, "Dateien");
        }

        /// <summary>
        /// Der Dateiname kann niemals aktualisiert werden, da dabei eine
        /// Datei kopiert werden müsste.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool UpdateDatei(DataRow row)
        {
            string sb =
                @"
                UPDATE Dateien SET
                    ID_DateiTypen=@ID_DateiTypen,
                    Beschreibung=@Beschreibung
                WHERE
                    ID_Dateien=@ID_Dateien
                ";

            ArrayList arSqlParameter = new ArrayList();
            arSqlParameter.Add(this.SqlParameter("@ID_DateiTypen", row["ID_DateiTypen"]));
            arSqlParameter.Add(this.SqlParameter("@Beschreibung", (string)row["Beschreibung"]));
            arSqlParameter.Add(this.SqlParameter("@ID_Dateien", row["ID_Dateien"]));

            int iEffectedRecords = this.ExecuteNonQuery(sb, arSqlParameter);

            return (iEffectedRecords == 1);
        }

        public bool DeleteDatei(int ID_Dateien)
        {
            string sb =
                @"
                DELETE FROM
                    Dateien
                WHERE
                    ID_Dateien=@ID_Dateien
                ";

            ArrayList arSqlParameter = new ArrayList();
            arSqlParameter.Add(this.SqlParameter("@ID_Dateien", ID_Dateien));

            int iEffectedRecords = this.ExecuteNonQuery(sb, arSqlParameter);

            return (iEffectedRecords == 1);
        }
        public int InsertDatei(DataRow oDataRow)
        {
            string sb =
                @"
                INSERT INTO Dateien
                    (
                    ID_DateiTypen,
                    Dateiname,
                    Beschreibung
                    )
                    VALUES
                    (
                     @ID_DateiTypen,
                     @Dateiname,
                     @Beschreibung
                    )
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_DateiTypen", oDataRow["ID_DateiTypen"]));
            aSQLParameters.Add(this.SqlParameter("@Dateiname", (string)oDataRow["Dateiname"]));
            aSQLParameters.Add(this.SqlParameter("@Beschreibung", (string)oDataRow["Beschreibung"]));

            return this.InsertRecord(sb, aSQLParameters, "Dateien");
        }
#endregion


#region DateiTypen
        public DataRow CreateDataRowDateiTypen()
        {
            DataTable dt = new DataTable("DateiTypen");
            DataRow dataRow;

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("DateiTyp", typeof(string)));

            dataRow = dt.NewRow();

            dataRow["DateiTyp"] = "";

            return dataRow;
        }
        public DataRow GetDateiTyp(int ID_DateiTypen)
        {
            string sb = @"
                SELECT 
                    ID_DateiTypen,
                    DateiTyp
                FROM 
                    DateiTypen
                WHERE
                    ID_DateiTypen=@ID_DateiTypen
                    ";

            ArrayList arSqlParameter = new ArrayList();
            arSqlParameter.Add(this.SqlParameter("@ID_DateiTypen", ID_DateiTypen));

            return GetRecord(sb, arSqlParameter, "DateiTypen");
        }
        public DataView GetDateiTypen()
        {
            string sb = @"
                SELECT 
                    ID_DateiTypen,
                    DateiTyp
                FROM 
                    DateiTypen
                order by
                    DateiTyp
                    ";

            return GetDataView(sb, null, "DateiTypen");
        }
        public bool DeleteDateiTyp(int nID)
        {
            string sb =
                @"
                DELETE FROM
                    DateiTypen
                WHERE
                    ID_DateiTypen=@ID_DateiTypen
                ";

            ArrayList arSqlParameter = new ArrayList();
            arSqlParameter.Add(this.SqlParameter("@ID_DateiTypen", nID));

            int iEffectedRecords = this.ExecuteNonQuery(sb, arSqlParameter);

            return (iEffectedRecords == 1);
        }
        public int InsertDateiTyp(DataRow oDataRow)
        {
            string sb =
                @"
                INSERT INTO DateiTypen
                    (
                    DateiTyp
                    )
                    VALUES
                    (
                     @DateiTyp
                    )
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@DateiTyp", (string)oDataRow["DateiTyp"]));

            return this.InsertRecord(sb, aSQLParameters, "DateiTypen");
        }
        public bool UpdateDateiTyp(DataRow row)
        {
            string sb =
                @"
                UPDATE DateiTypen SET
                    DateiTyp=@DateiTyp
                WHERE
                    ID_DateiTypen=@ID_DateiTypen
                ";

            ArrayList arSqlParameter = new ArrayList();
            arSqlParameter.Add(this.SqlParameter("@DateiTyp", (string)row["DateiTyp"]));
            arSqlParameter.Add(this.SqlParameter("@ID_DateiTypen", row["ID_DateiTypen"]));

            int iEffectedRecords = this.ExecuteNonQuery(sb, arSqlParameter);

            return (iEffectedRecords == 1);
        }
#endregion

        public DataRow CreateDataRowOPFunktionen(string tableName)
        {
            DataTable dt = new DataTable(tableName);

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("ID_OPFunktionen", typeof(int)));
            dt.Columns.Add(new DataColumn("LfdNr", typeof(int)));
            dt.Columns.Add(new DataColumn("Beschreibung", typeof(string)));

            return dt.NewRow();
        }

#region ChirurgenFunktionen
        public DataRow CreateDataRowChirurgenFunktionen()
        {
            DataTable dt = new DataTable("ChirurgenFunktionen");
            DataRow dataRow;

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("Funktion", typeof(string)));

            dataRow = dt.NewRow();

            dataRow["Funktion"] = "";

            return dataRow;
        }

        public bool DeleteChirurgenFunktionen(int nID)
        {
            string sb =
                @"
                DELETE FROM
                    ChirurgenFunktionen
                WHERE
                    ID_ChirurgenFunktionen=@ID_ChirurgenFunktionen
                ";

            ArrayList arSqlParameter = new ArrayList();
            arSqlParameter.Add(this.SqlParameter("@ID_ChirurgenFunktionen", nID));

            int iEffectedRecords = this.ExecuteNonQuery(sb, arSqlParameter);

            return (iEffectedRecords == 1);
        }
        public int InsertChirurgenFunktionen(DataRow oDataRow)
        {
            string sb =
                @"
                INSERT INTO ChirurgenFunktionen
                    (
                    Funktion
                    )
                    VALUES
                    (
                     @Funktion
                    )
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@Funktion", (string)oDataRow["Funktion"]));

            return this.InsertRecord(sb, aSQLParameters, "ChirurgenFunktionen");
        }
        public int UpdateChirurgenFunktionen(DataRow row)
        {
            string sql =
                @"
                UPDATE ChirurgenFunktionen SET
                    Funktion=@Funktion
                WHERE
                    ID_ChirurgenFunktionen=@ID_ChirurgenFunktionen
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@Funktion", (string)row["Funktion"]));
            sqlParameters.Add(this.SqlParameter("@ID_ChirurgenFunktionen", row["ID_ChirurgenFunktionen"]));

            return this.ExecuteNonQuery(sql, sqlParameters);
        }
#endregion



#region ChirurgenGebiete
        public DataView GetChirurgenGebiete(int ID_Chirurgen)
        {
            string sql =
                @"
                SELECT 
                    ID_ChirurgenGebiete,
                    ID_Gebiete,
                    ID_Chirurgen,
                    GebietVon,
                    GebietBis
                FROM
                    ChirurgenGebiete
                WHERE
                    ID_Chirurgen=@ID_Chirurgen
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
            return GetDataView(sql, sqlParameters, "ChirurgenGebiete");
        }

        public bool DeleteGebieteForChirurg(int nID_Chirurgen)
        {
            string strSQL = @"
                DELETE FROM
                    ChirurgenGebiete
                WHERE
                    ID_Chirurgen=@ID_Chirurgen
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(SqlParameter("@ID_Chirurgen", nID_Chirurgen));

            int iEffectedRecords = this.ExecuteNonQuery(strSQL, aSQLParameters);

            return true;
        }
        public bool DeleteNotizenForChirurg(int nID_Chirurgen)
        {
            string strSQL = @"
                DELETE FROM
                    Notizen
                WHERE
                    ID_Chirurgen=@ID_Chirurgen
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(SqlParameter("@ID_Chirurgen", nID_Chirurgen));

            int iEffectedRecords = this.ExecuteNonQuery(strSQL, aSQLParameters);

            return true;
        }
#endregion


#region config
        public long GetCustomerDataCount()
        {
            string sql =
                @"
                SELECT 
                    count(ID_Config)
                FROM
                    Config
                WHERE
                    [Key] like 'cd_%'
                ";

            return ExecuteScalar(sql);
        }
#endregion

#region SerialNumbers

        public string GetFirstUnusedSerialNumber()
        {
            string serial = "";

            string sb = @"
                SELECT @@TOP@@
                    SerialNumber
                FROM 
                    SerialNumbers
                    @@LIMIT@@
                    ";

            sb = HandleTopLimitStuff(sb, "1");

            DataRow row = GetRecord(sb, null, "SerialNumbers");

            if (row != null)
            {
                serial = (string)row["SerialNumber"];
            }
            return serial;
        }

        public DataRow CreateDataRowSerialNumbers()
        {
            DataTable dt = new DataTable("SerialNumbers");

            dt.Columns.Add(new DataColumn("SerialNumber", typeof(string)));

            return dt.NewRow();
        }

        /// <summary>
        /// Don't sort by serial number, one might see a pattern
        /// </summary>
        /// <returns></returns>
        public DataView GetSerialNumbers()
        {
            string sb = @"
                SELECT 
                    SerialNumber
                FROM 
                    SerialNumbers
                    ";

            return GetDataView(sb, null, "SerialNumbers");
        }

        /// <summary>
        /// Don't sort by serial number, one might see a pattern
        /// Alle Chirurgen zurückgeben, egal welche Rechte, damit man alle Seriennummern sieht.
        /// </summary>
        /// <returns></returns>
        public DataView GetChirurgenSerialNumbers()
        {
            string sb = @"
                SELECT 
                    Lizenzdaten,
                    Nachname,
                    Vorname
                FROM 
                    Chirurgen
                order by
                    Nachname, Vorname
                    ";

            return GetDataView(sb, null, "Chirurgen");
        }

        public bool UpdateChirurgenSerialNumber(DataRow row)
        {
            string sql =
                @"
                UPDATE Chirurgen
                SET
                    Lizenzdaten=@Lizenzdaten
                WHERE
                    ID_Chirurgen = @ID_Chirurgen
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(SqlParameter("@Lizenzdaten", row["Lizenzdaten"]));
            sqlParameters.Add(SqlParameter("@ID_Chirurgen", row["ID_Chirurgen"]));

            int effectedRecords = this.ExecuteNonQuery(sql, sqlParameters);

            return (effectedRecords == 1);
        }

        public int GetCountUnusedSerialNumbers()
        {
            string sb = "SELECT count(*) FROM SerialNumbers";

            return ExecuteScalarInteger(sb, null);
        }

        /// <summary>
        /// For one specified serial number, count how often it is used by a surgeon and count
        /// whether it has already been added.
        /// </summary>
        /// <param name="serialNumber">The serial number to check</param>
        /// <param name="countUsed">The number of surgeons, who use this serial</param>
        /// <param name="countUnused">Number of accurrences in tabl SerialNumbers</param>
        public void GetCountSerialNumber(string serialNumber, out int countUsed, out int countUnused)
        {
            string sb = "SELECT count(*) FROM Chirurgen where Lizenzdaten=@Lizenzdaten";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@Lizenzdaten", serialNumber));

            countUsed = ExecuteScalarInteger(sb, sqlParameters);

            sb = "SELECT count(*) FROM SerialNumbers where SerialNumber=@SerialNumber";

            sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@SerialNumber", serialNumber));

            countUnused = ExecuteScalarInteger(sb, sqlParameters);
        }

        public bool DeleteSerialNumber(string serial)
        {
            string sb =
                @"
                DELETE FROM
                    SerialNumbers
                WHERE
                    SerialNumber=@SerialNumber
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@SerialNumber", serial));

            int iEffectedRecords = this.ExecuteNonQuery(sb, sqlParameters);

            return (iEffectedRecords == 1);
        }

        public int InsertSerialNumber(DataRow row)
        {
            StringBuilder sb = new StringBuilder(
                @"
                INSERT INTO SerialNumbers
                    (
                    SerialNumber
                    )
                    VALUES
                    (
                    @SerialNumber
                    )
                ");

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@SerialNumber", (string)row["SerialNumber"]));

            return this.InsertRecord(sb.ToString(), sqlParameters, "SerialNumbers");
        }
#endregion

#region Suggest
        public DataView SuggestGetOPSKodeTextForKode(string ops, int top)
        {
            string sql =
                @"
                SELECT @@TOP@@
                    [OPS-Kode],
                    [OPS-Text]
                FROM
                    Operationen
                WHERE
                    [OPS-Kode] like @ops
                ORDER BY
                    [OPS-Kode]
                    @@LIMIT@@
                ";

            sql = HandleTopLimitStuff(sql, top.ToString());

            string opsLike = CreateLikeExpression(ops + "%");

            ArrayList sqlParameter = new ArrayList();
            sqlParameter.Add(this.SqlParameter("@ops", opsLike));

            return GetDataView(sql, sqlParameter, "Operationen");
        }
        public DataView SuggestGetOPSKodeTextForText(string ops, int top)
        {
            string sql =
                @"
                SELECT @@TOP@@
                    [OPS-Kode],
                    [OPS-Text]
                FROM
                    Operationen
                WHERE
                    [OPS-Text] like @ops
                ORDER BY
                    [OPS-Kode]
                    @@LIMIT@@
                ";

            sql = HandleTopLimitStuff(sql, top.ToString());

            string opsLike = CreateLikeExpression("%" + ops + "%");

            ArrayList sqlParameter = new ArrayList();
            sqlParameter.Add(this.SqlParameter("@ops", opsLike));

            return GetDataView(sql, sqlParameter, "Operationen");
        }
        public DataView SuggestGetOPSKodeTextForKodeOrText(string ops, int top)
        {
            // TODO left und distinct + limit/top für mySQL und SQLServer!!!
            string sql =
                @"
                SELECT @@TOP@@
                    [OPS-Kode] as OPSKode,
                    [OPS-Text] as OPSText
                FROM
                    Operationen
                WHERE
                    [OPS-Kode] like @opsKode
                    or [OPS-Text] like @opsText
                ORDER BY
                    [OPS-Kode]
                    @@LIMIT@@
                ";

            sql = HandleTopLimitStuff(sql, top.ToString());

            string opsKodeLike = CreateLikeExpression(ops + "%");
            string opsTextLike = CreateLikeExpression("%" + ops + "%");

            ArrayList sqlParameter = new ArrayList();
            sqlParameter.Add(this.SqlParameter("@opsKode", opsKodeLike));
            sqlParameter.Add(this.SqlParameter("@opsText", opsTextLike));

            return GetDataView(sql, sqlParameter, "Operationen");
        }
#endregion


#region ChirurgenRichtlinien
        public DataRow CreateDataRowChirurgenRichtlinien(int ID_Chirurgen, int ID_Richtlinien)
        {
            DataTable dt = new DataTable("ChirurgenRichtlinien");
            DataRow dataRow;

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("ID_ChirurgenRichtlinien", typeof(int)));
            dt.Columns.Add(new DataColumn("ID_Chirurgen", typeof(int)));
            dt.Columns.Add(new DataColumn("ID_Richtlinien", typeof(int)));
            dt.Columns.Add(new DataColumn("Datum", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Anzahl", typeof(int)));
            dt.Columns.Add(new DataColumn("Ort", typeof(string)));

            dataRow = dt.NewRow();

            dataRow["ID_Chirurgen"] = ID_Chirurgen;
            dataRow["ID_Richtlinien"] = ID_Richtlinien;
            dataRow["Datum"] = DateTime.Now;
            dataRow["Anzahl"] = 0;
            dataRow["Ort"] = "";

            return dataRow;
        }
#endregion


#region KlinischeErgebnisse
        public long GetKlinischeErgebnisseAnzahl(int ID_Chirurgen, int ID_OPFunktionen, int ID_KlinischeErgebnisseTypen, 
            int quelle, string opsKode, DateTime? from, DateTime? to)
        {
            opsKode = CreateLikeExpression(opsKode);

            StringBuilder sb = new StringBuilder(
                @"
                    SELECT COUNT(ID_ChirurgenOperationen)
                    FROM ChirurgenOperationen
                    WHERE 
                        ID_Chirurgen = @ID_Chirurgen
                        $datefrom$ $dateto$
                        $KlinischeErgebnisse$
                        $opfunktionen$
                        $OPSKode$
                        $quelle$

                ");

            HandleDatum(from, to, sb);

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));

            HandleKlinischeErgebnisseTypen(sqlParameters, "", ID_KlinischeErgebnisseTypen, sb);

            HandleOPFunktionen(sqlParameters, "", ID_OPFunktionen, sb);

            if (string.IsNullOrEmpty(opsKode))
            {
                sb.Replace("$OPSKode$", "");
            }
            else
            {
                sb.Replace("$OPSKode$", "AND [OPS-Kode] LIKE @Operation");
                sqlParameters.Add(this.SqlParameter("@Operation", opsKode + "%"));
            }

            HandleQuelleSingle(sqlParameters, "", quelle, sb);

            return ExecuteScalar(sb.ToString(), sqlParameters);
        }
#endregion

#region GetText
        internal string GetText(string id)
        {
            return _businessLayer.GetText("DatabaseLayer", id);
        }
        internal string GetText(string formName, string id)
        {
            return _businessLayer.GetText(formName, id);
        }
#endregion

#region Abteilungen
        /// <summary>
        /// Alle Chirurgen holen, egal welche Rechte, weil man alle Chirurgen von allen Abteilungen sehen muss
        /// </summary>
        /// <param name="ID_Abteilungen"></param>
        /// <returns></returns>
        public DataView GetChirurgenNichtInAbteilung(int ID_Abteilungen)
        {
            String sql =
                @"
                    SELECT 
                        Chirurgen.ID_Chirurgen,
                        Chirurgen.Anrede,
                        Chirurgen.Nachname,
                        Chirurgen.Vorname,
                        Chirurgen.UserID
                    FROM 
                        Chirurgen
                    WHERE 
                        ID_Chirurgen not in (select ID_Chirurgen from AbteilungenChirurgen where AbteilungenChirurgen.ID_Abteilungen = @ID_Abteilungen)
                    order by
                        Chirurgen.Nachname,
                        Chirurgen.Vorname
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Abteilungen", ID_Abteilungen));

            return GetDataView(sql, sqlParameters, "AbteilungenChirurgen");
        }

        public DataView GetChirurgenVonAbteilung(int ID_Abteilungen)
        {
            String sql =
                @"
                    SELECT 
                        AbteilungenChirurgen.ID_AbteilungenChirurgen,
                        Chirurgen.Anrede,
                        Chirurgen.Nachname,
                        Chirurgen.Vorname,
                        Chirurgen.UserID
                    FROM 
                        AbteilungenChirurgen inner join Chirurgen on AbteilungenChirurgen.ID_Chirurgen=Chirurgen.ID_Chirurgen
                    WHERE 
                        ID_Abteilungen = @ID_Abteilungen
                    ORDER BY
                        Chirurgen.Nachname,
                        Chirurgen.Vorname
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Abteilungen", ID_Abteilungen));

            return GetDataView(sql, sqlParameters, "AbteilungenChirurgen");
        }

        public bool DeleteAbteilungenChirurgen(int ID_AbteilungenChirurgen)
        {
            String sql = 
                @"
                DELETE FROM
                    AbteilungenChirurgen
                WHERE
                    ID_AbteilungenChirurgen=@ID_AbteilungenChirurgen
                ";

            ArrayList sqlParameter = new ArrayList();
            sqlParameter.Add(SqlParameter("@ID_AbteilungenChirurgen", ID_AbteilungenChirurgen));

            int iEffectedRecords = this.ExecuteNonQuery(sql, sqlParameter);

            return (iEffectedRecords == 1);
        }

        public bool InsertAbteilungenChirurgen(int ID_Abteilungen, int ID_Chirurgen)
        {
            String sql = @"select count(ID_AbteilungenChirurgen) from AbteilungenChirurgen
                    where ID_Abteilungen = @ID_Abteilungen and ID_Chirurgen = @ID_Chirurgen";

            ArrayList sqlParameter = new ArrayList();
            sqlParameter.Add(SqlParameter("@ID_Abteilungen", ID_Abteilungen));
            sqlParameter.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));

            int records = ExecuteScalarInteger(sql, sqlParameter);

            if (records == 0)
            {
                sql = @"
                insert into
                    AbteilungenChirurgen (ID_Abteilungen, ID_Chirurgen)
                values 
                    (@ID_Abteilungen, @ID_Chirurgen)
                ";

                sqlParameter = new ArrayList();
                sqlParameter.Add(SqlParameter("@ID_Abteilungen", ID_Abteilungen));
                sqlParameter.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));

                records = ExecuteNonQuery(sql, sqlParameter);
            }

            return true;
        }

#endregion

#region Weiterbilder
        /// <summary>
        /// Alt: Hole alle Chirurgen aus den Abteilungen des Weiterbilder, die diesem Weiterbilder nicht zugeordnet sind.
        /// Neu: Hole alle Chirurgen, die diesem Weiterbilder nicht zugeordnet sind.
        /// </summary>
        /// <param name="ID_Weiterbilder"></param>
        /// <returns></returns>
        public DataView GetChirurgenNotInWeiterbilder(int ID_Weiterbilder)
        {
            String sql =
                @"
                    SELECT 
                        Chirurgen.ID_Chirurgen,
                        Chirurgen.Anrede,
                        Chirurgen.Nachname,
                        Chirurgen.Vorname,
                        Chirurgen.UserID
                    FROM 
                        Chirurgen
                    WHERE 
                        ID_Chirurgen not in (select ID_Chirurgen from WeiterbilderChirurgen where WeiterbilderChirurgen.ID_Weiterbilder = @ID_Weiterbilder)
                        and ID_Chirurgen <> @ID_Weiterbilder
                    ORDER BY
                        Chirurgen.Nachname,
                        Chirurgen.Vorname
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Weiterbilder", ID_Weiterbilder));

            return GetDataView(sql, sqlParameters, "WeiterbilderChirurgen");
        }

        /// <summary>
        /// Holt alle Weiterbilder aus allen Chirurgen
        /// </summary>
        /// <returns></returns>
        public DataView GetWeiterbilder()
        {
            String sql =
                @"
                    SELECT 
                        Chirurgen.ID_Chirurgen,
                        Chirurgen.Anrede,
                        Chirurgen.Nachname,
                        Chirurgen.Vorname,
                        Chirurgen.UserID
                    FROM 
                        Chirurgen 
                    WHERE
                        IstWeiterbilder=1
                    ORDER BY
                        Chirurgen.Nachname,
                        Chirurgen.Vorname
                ";

            return GetDataView(sql, null, "Chirurgen");
        }

        /// <summary>
        /// Hole alle Chirurgen, die diesem Weiterbilder zugeordnet sind.
        /// </summary>
        /// <param name="ID_Weiterbilder"></param>
        /// <returns></returns>
        public DataView GetChirurgenOfWeiterbilder(int ID_Weiterbilder)
        {
            String sql =
                @"
                    SELECT 
                        WeiterbilderChirurgen.ID_WeiterbilderChirurgen,
                        Chirurgen.Anrede,
                        Chirurgen.Nachname,
                        Chirurgen.Vorname,
                        Chirurgen.UserID
                    FROM 
                        WeiterbilderChirurgen inner join Chirurgen on WeiterbilderChirurgen.ID_Chirurgen=Chirurgen.ID_Chirurgen
                    WHERE 
                        ID_Weiterbilder = @ID_Weiterbilder
                    ORDER BY
                        Chirurgen.Nachname,
                        Chirurgen.Vorname
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Weiterbilder", ID_Weiterbilder));

            return GetDataView(sql, sqlParameters, "WeiterbilderChirurgen");
        }

        public bool DeleteWeiterbilderChirurgen(int ID_WeiterbilderChirurgen)
        {
            String sql =
                @"
                DELETE FROM
                    WeiterbilderChirurgen
                WHERE
                    ID_WeiterbilderChirurgen=@ID_WeiterbilderChirurgen
                ";

            ArrayList sqlParameter = new ArrayList();
            sqlParameter.Add(SqlParameter("@ID_WeiterbilderChirurgen", ID_WeiterbilderChirurgen));

            int iEffectedRecords = this.ExecuteNonQuery(sql, sqlParameter);

            return (iEffectedRecords == 1);
        }

        public bool InsertWeiterbilderChirurgen(int ID_Weiterbilder, int ID_Chirurgen)
        {
            String sql = @"select count(ID_WeiterbilderChirurgen) from WeiterbilderChirurgen
                    where ID_Weiterbilder = @ID_Weiterbilder and ID_Chirurgen = @ID_Chirurgen";

            ArrayList sqlParameter = new ArrayList();
            sqlParameter.Add(SqlParameter("@ID_Weiterbilder", ID_Weiterbilder));
            sqlParameter.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));

            int records = ExecuteScalarInteger(sql, sqlParameter);

            if (records == 0)
            {
                sql = @"
                insert into
                    WeiterbilderChirurgen (ID_Weiterbilder, ID_Chirurgen)
                values 
                    (@ID_Weiterbilder, @ID_Chirurgen)
                ";

                sqlParameter = new ArrayList();
                sqlParameter.Add(SqlParameter("@ID_Weiterbilder", ID_Weiterbilder));
                sqlParameter.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));

                records = ExecuteNonQuery(sql, sqlParameter);
            }

            return true;
        }

#endregion

#region SecGroupsChirurgen
        public DataView GetChirurgenNotInSecGroup(int ID_SecGroups)
        {
            String sql =
                @"
                    SELECT 
                        Chirurgen.ID_Chirurgen,
                        Chirurgen.Anrede,
                        Chirurgen.Nachname,
                        Chirurgen.Vorname,
                        Chirurgen.UserID
                    FROM 
                        Chirurgen
                    WHERE 
                        ID_Chirurgen not in (select ID_Chirurgen from SecGroupsChirurgen where SecGroupsChirurgen.ID_SecGroups = @ID_SecGroups)
                    order by 
                        Chirurgen.Nachname,
                        Chirurgen.Vorname
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_SecGroups", ID_SecGroups));

            return GetDataView(sql, sqlParameters, "SecGroupsChirurgen");
        }

        public DataView GetChirurgenOfSecGroup(int ID_SecGroups)
        {
            String sql =
                @"
                    SELECT 
                        SecGroupsChirurgen.ID_SecGroupsChirurgen,
                        Chirurgen.Anrede,
                        Chirurgen.Nachname,
                        Chirurgen.Vorname,
                        Chirurgen.UserID
                    FROM 
                        SecGroupsChirurgen inner join Chirurgen on SecGroupsChirurgen.ID_Chirurgen=Chirurgen.ID_Chirurgen
                    WHERE 
                        ID_SecGroups = @ID_SecGroups
                    order by 
                        Chirurgen.Nachname,
                        Chirurgen.Vorname
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_SecGroups", ID_SecGroups));

            return GetDataView(sql, sqlParameters, "SecGroupsChirurgen");
        }

        public bool DeleteSecGroupsChirurgen(int ID_SecGroupsChirurgen)
        {
            String sql =
                @"
                DELETE FROM
                    SecGroupsChirurgen
                WHERE
                    ID_SecGroupsChirurgen=@ID_SecGroupsChirurgen
                ";

            ArrayList sqlParameter = new ArrayList();
            sqlParameter.Add(SqlParameter("@ID_SecGroupsChirurgen", ID_SecGroupsChirurgen));

            int iEffectedRecords = this.ExecuteNonQuery(sql, sqlParameter);

            return (iEffectedRecords == 1);
        }

        public bool InsertSecGroupsChirurgen(int ID_SecGroups, int ID_Chirurgen)
        {
            String sql = @"select count(ID_SecGroupsChirurgen) from SecGroupsChirurgen
                    where ID_SecGroups = @ID_SecGroups and ID_Chirurgen = @ID_Chirurgen";

            ArrayList sqlParameter = new ArrayList();
            sqlParameter.Add(SqlParameter("@ID_SecGroups", ID_SecGroups));
            sqlParameter.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));

            int records = ExecuteScalarInteger(sql, sqlParameter);

            if (records == 0)
            {
                sql = @"
                insert into
                    SecGroupsChirurgen (ID_SecGroups, ID_Chirurgen)
                values 
                    (@ID_SecGroups, @ID_Chirurgen)
                ";

                sqlParameter = new ArrayList();
                sqlParameter.Add(SqlParameter("@ID_SecGroups", ID_SecGroups));
                sqlParameter.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));

                records = ExecuteNonQuery(sql, sqlParameter);
            }

            return true;
        }

#endregion

#region SecGroupsSecRights
        public DataView GetSecRightsNotInSecGroup(int ID_SecGroups)
        {
            String sql =
                @"
                    SELECT 
                        SecRights.ID_SecRights,
                        SecRights.Name,
                        SecRights.Description
                    FROM 
                        SecRights
                    WHERE 
                        SecRights.ID_SecRights not in (select ID_SecRights from SecGroupsSecRights where SecGroupsSecRights.ID_SecGroups = @ID_SecGroups)
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_SecGroups", ID_SecGroups));

            return GetDataView(sql, sqlParameters, "SecGroupsSecRights");
        }

        /// <summary>
        /// Get all rights that would be abandoned if this surgeon were deleted.
        /// </summary>
        /// <param name="ID_Chirurgen">The surgeon to delete</param>
        /// <returns>true if deleting ID_Chirurgen would produce an abandoned right</returns>
        public long CountAbandonedSecRightsWithoutUser(int ID_Chirurgen, List<string> abandonedRights)
        {
            abandonedRights.Clear();

            //
            // Count all existing rights
            //
            DataView dvAllRights = GetDataView("select ID_SecRights, Name from SecRights", "SecRights");
            long countAllRights = dvAllRights.Table.Rows.Count;

            //
            // Get all rights that would be assigned to a surgeon if ID_Chirurgen were deleted
            //
            string sql = 
                @"select distinct 
                        SecGroupsSecRights.ID_SecRights 
                    from 
                        SecGroupsChirurgen inner join SecGroupsSecRights on SecGroupsChirurgen.ID_SecGroups = SecGroupsSecRights.ID_SecGroups
                    where
                        SecGroupsChirurgen.ID_Chirurgen <> @ID_Chirurgen";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));

            DataView dvAssignedRights = GetDataView(sql, sqlParameters, "SecGroupsSecRights");

            long countAbandonedRights = countAllRights - dvAssignedRights.Table.Rows.Count;

            for (int i = 0; i < dvAllRights.Table.Rows.Count; i++)
            {
                DataRow rowAll = dvAllRights.Table.Rows[i];

                bool exists = false;
                for (int j = 0; j < dvAssignedRights.Table.Rows.Count; j++)
                {
                    DataRow rowAssigned = dvAssignedRights.Table.Rows[j];
                    if (((int)rowAll["ID_SecRights"]) == ((int)rowAssigned["ID_SecRights"]))
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    abandonedRights.Add((string)rowAll["Name"]);
                }
            }

            return countAbandonedRights;
        }


        /// <summary>
        /// Count how often this right is assigned to a user. The exact number is not relevant, only if this right is assigned to a user or not.
        /// </summary>
        /// <param name="ID_SecRights"></param>
        /// <returns></returns>
        public long CountSecRightAssignmentsWithout(int ID_SecGroupsSecRights, int ID_SecRights)
        {
            string sql =
                @"select        
                        SecGroupsChirurgen.ID_Chirurgen 
                    from 
                        SecGroupsChirurgen inner join SecGroupsSecRights on SecGroupsChirurgen.ID_SecGroups = SecGroupsSecRights.ID_SecGroups
                    where 
                        SecGroupsSecRights.ID_SecGroupsSecRights <> @ID_SecGroupsSecRights
                        and SecGroupsSecRights.ID_SecRights = @ID_SecRights
                ";
                
            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_SecGroupsSecRights", ID_SecGroupsSecRights));
            sqlParameters.Add(this.SqlParameter("@ID_SecRights", ID_SecRights));

            DataView dv = GetDataView(sql, sqlParameters, "SecGroupsSecRights");

            return dv.Table.Rows.Count;
        }

        /// <summary>
        /// Wenn dieser Zuordnung eines Chirurgen zu einer Gruppe entfernt würde, gäbe es dann ein Recht, 
        /// das keinem Benutzer mehr zugeordnet wäre?
        /// </summary>
        /// <param name="ID_SecGroupsChirurgen">Die Zuordnung Chirurg zu Gruppe, die entfernt werden soll</param>
        /// <param name="abandonedRights">Wenn verwaiste Rechte übrigbleiben, ist mindestens eines hier drin</param>
        /// <returns>true, wenn ein abandoned right entstünde</returns>
        public bool ExistsAbandonedSecRightWithoutSecGroupsChirurgen(int ID_SecGroupsChirurgen, List<string> abandonedRights)
        {
            ProgressEventArgs progressEvent = new ProgressEventArgs();
            bool abandoned = false;
            abandonedRights.Clear();

            //
            // Get all existing rights
            //
            DataView dvRights = GetDataView("select ID_SecRights, Name from SecRights", "SecRights");

            foreach (DataRow row in dvRights.Table.Rows)
            {
                progressEvent.Data = (string)row["Name"];
                FireProgressEvent(progressEvent);
                if (progressEvent.Cancel)
                {
                    abandoned = true;
                    break;
                }

                //
                // Jedes Recht einzeln überprüfen, geht das nicht besser?
                //
                int ID_SecRights = (int)row["ID_SecRights"];

                string sql =
                    @"select        
                            count(SecGroupsChirurgen.ID_Chirurgen)
                        from 
                            SecGroupsChirurgen inner join SecGroupsSecRights on SecGroupsChirurgen.ID_SecGroups = SecGroupsSecRights.ID_SecGroups
                        where 
                            (SecGroupsChirurgen.ID_SecGroupsChirurgen <> @ID_SecGroupsChirurgen)
                            and (SecGroupsSecRights.ID_SecRights = @ID_SecRights)
                    ";
                    
                ArrayList sqlParameters = new ArrayList();
                sqlParameters.Add(this.SqlParameter("@ID_SecGroupsChirurgen", ID_SecGroupsChirurgen));
                sqlParameters.Add(this.SqlParameter("@ID_SecRights", ID_SecRights));

                long count = ExecuteScalar(sql, sqlParameters);
                if (count == 0)
                {
                    abandonedRights.Add((string)row["Name"]);
                    abandoned = true;

                    // removed break, so that we see all rights.
                    //break;
                }
            }

            return abandoned;
        }

        public DataRow GetSecRight(int ID_SecRights)
        {
            string sql = @"
                    SELECT
                        ID_SecRights, 
                        Name 
                    FROM
                        SecRights
                    WHERE
                        ID_SecRights=@ID_SecRights";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_SecRights", ID_SecRights));

            return GetRecord(sql, sqlParameters, "SecRights");
        }

        public DataView GetSecRightsOfSecGroup(int ID_SecGroups)
        {
            String sql =
                @"
                    SELECT 
                        SecGroupsSecRights.ID_SecGroupsSecRights,
                        SecGroupsSecRights.ID_SecRights,
                        SecRights.Name,
                        SecRights.Description
                    FROM 
                        SecGroupsSecRights inner join SecRights on SecGroupsSecRights.ID_SecRights=SecRights.ID_SecRights
                    WHERE 
                        SecGroupsSecRights.ID_SecGroups = @ID_SecGroups
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_SecGroups", ID_SecGroups));

            return GetDataView(sql, sqlParameters, "SecGroupsChirurgen");
        }

        public bool DeleteSecGroupsSecRights(int ID_SecGroupsSecRights)
        {
            String sql =
                @"
                DELETE FROM
                    SecGroupsSecRights
                WHERE
                    ID_SecGroupsSecRights=@ID_SecGroupsSecRights
                ";

            ArrayList sqlParameter = new ArrayList();
            sqlParameter.Add(SqlParameter("@ID_SecGroupsSecRights", ID_SecGroupsSecRights));

            int iEffectedRecords = this.ExecuteNonQuery(sql, sqlParameter);

            return (iEffectedRecords == 1);
        }

        public bool InsertSecGroupsSecRights(int ID_SecGroups, int ID_SecRights)
        {
            String sql = @"select count(ID_SecGroupsSecRights) from SecGroupsSecRights
                    where ID_SecGroups = @ID_SecGroups and ID_SecRights = @ID_SecRights";

            ArrayList sqlParameter = new ArrayList();
            sqlParameter.Add(SqlParameter("@ID_SecGroups", ID_SecGroups));
            sqlParameter.Add(SqlParameter("@ID_SecRights", ID_SecRights));

            int records = ExecuteScalarInteger(sql, sqlParameter);

            if (records == 0)
            {
                sql = @"
                insert into
                    SecGroupsSecRights (ID_SecGroups, ID_SecRights)
                values 
                    (@ID_SecGroups, @ID_SecRights)
                ";

                sqlParameter = new ArrayList();
                sqlParameter.Add(SqlParameter("@ID_SecGroups", ID_SecGroups));
                sqlParameter.Add(SqlParameter("@ID_SecRights", ID_SecRights));

                records = ExecuteNonQuery(sql, sqlParameter);
            }

            return true;
        }
#endregion

#region UserRights


        public DataView GetRightsOfUser(int ID_Chirurgen)
        {
            String sql =
                @"
                    SELECT 
                        SecRights.Name,
                        SecRights.Description
                    FROM 
                        SecGroupsSecRights inner join SecRights on SecGroupsSecRights.ID_SecRights=SecRights.ID_SecRights
                    WHERE 
                        ID_SecGroups in (select ID_SecGroups from SecGroupsChirurgen where ID_Chirurgen=@ID_Chirurgen)
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));

            return GetDataView(sql, sqlParameters, "SecGroupsSecRights");
        }

        public DataView GetRollenOfUser(int ID_Chirurgen)
        {
            String sql =
                @"
                    SELECT 
                        SecGroups.[Text]
                    FROM 
                        SecGroupsChirurgen inner join SecGroups on SecGroupsChirurgen.ID_SecGroups = SecGroups.ID_SecGroups
                    WHERE
                        SecGroupsChirurgen.ID_Chirurgen = @ID_Chirurgen
                    ORDER BY
                        SecGroups.[Text]
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));

            return GetDataView(sql, sqlParameters, "SecGroups");
        }

        public DataView GetAbteilungenOfUser(int ID_Chirurgen)
        {
            String sql =
                @"
                    SELECT 
                        Abteilungen.[Text]
                    FROM 
                        AbteilungenChirurgen inner join Abteilungen on AbteilungenChirurgen.ID_Abteilungen = Abteilungen.ID_Abteilungen
                    WHERE
                        AbteilungenChirurgen.ID_Chirurgen = @ID_Chirurgen
                    ORDER BY
                        Abteilungen.[Text]
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));

            return GetDataView(sql, sqlParameters, "Abteilungen");
        }

        public DataView GetWeiterbilderOfUser(int ID_Chirurgen)
        {
            String sql =
                @"
                    SELECT 
                        a.Nachname,
                        a.Vorname,
                        a.UserID,
                        a.Anfangsdatum,
                        a.Anrede, 
                        a.Aktiv       
                    FROM 
                        WeiterbilderChirurgen inner join Chirurgen a on WeiterbilderChirurgen.ID_Weiterbilder = a.ID_Chirurgen
                    WHERE
                        WeiterbilderChirurgen.ID_Chirurgen = @ID_Chirurgen
                    ORDER BY
                        a.Nachname,
                        a.Vorname
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));

            return GetDataView(sql, sqlParameters, "WeiterbilderChirurgen");
        }
        public DataView GetWeiterzubildendeOfUser(int ID_Chirurgen)
        {
            String sql = string.Format(
                @"
                    SELECT 
                        a.Nachname,
                        a.Vorname,
                        a.UserID,
                        a.Anfangsdatum,
                        a.Anrede, 
                        a.Aktiv       
                    FROM 
                        WeiterbilderChirurgen b inner join Chirurgen a on b.ID_Chirurgen = a.ID_Chirurgen
                    WHERE
                        b.ID_Weiterbilder = {0}
                ", ID_Chirurgen);

            return GetDataView(sql, null, "WeiterbilderChirurgen");
        }

#endregion

#region GebieteSoll und RichtlinienSoll

        public DataRow CreateDataRowGebieteSoll()
        {
            DataTable dt = new DataTable("GebieteSoll");

            dt.Columns.Add("ID_GebieteSoll", typeof(int));
            dt.Columns.Add("ID_Chirurgen", typeof(int));
            dt.Columns.Add("ID_Gebiete", typeof(int));
            dt.Columns.Add("Von", typeof(DateTime));
            dt.Columns.Add("Bis", typeof(DateTime));

            return dt.NewRow();
        }

        public DataRow CreateDataRowRichtlinienSoll()
        {
            DataTable dt = new DataTable("RichtlinienSoll");

            dt.Columns.Add("ID_RichtlinienSoll", typeof(int));
            dt.Columns.Add("ID_GebieteSoll", typeof(int));
            dt.Columns.Add("ID_Richtlinien", typeof(int));
            dt.Columns.Add("Soll", typeof(int));

            return dt.NewRow();
        }


        public DataRow GetGebieteSoll(int ID_GebieteSoll)
        {
            String sql =
                @"
                    SELECT 
                        ID_GebieteSoll,
                        ID_Chirurgen,
                        ID_Gebiete,
                        Von,
                        Bis
                    FROM 
                        GebieteSoll
                    WHERE 
                        ID_GebieteSoll = @ID_GebieteSoll
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_GebieteSoll", ID_GebieteSoll));

            return GetRecord(sql, sqlParameters, "GebieteSoll");
        }

        public DataView GetGebieteSoll(int ID_Chirurgen, int ID_Gebiete)
        {
            String sql =
                @"
                    SELECT 
                        ID_GebieteSoll,
                        ID_Chirurgen,
                        ID_Gebiete,
                        Von,
                        Bis
                    FROM 
                        GebieteSoll
                    WHERE 
                        ID_Chirurgen = @ID_Chirurgen and
                        ID_Gebiete = @ID_Gebiete
                    ORDER BY
                        Von DESC
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
            sqlParameters.Add(this.SqlParameter("@ID_Gebiete", ID_Gebiete));

            return GetDataView(sql, sqlParameters, "GebieteSoll");
        }

        public DataRow GetRichtlinienSoll(int ID_GebieteSoll, int ID_Richtlinien)
        {
            String sql =
                @"
                    SELECT 
                        ID_RichtlinienSoll,
                        ID_GebieteSoll,
                        ID_Richtlinien,
                        Soll
                    FROM 
                        RichtlinienSoll
                    WHERE 
                        ID_GebieteSoll = @ID_GebieteSoll and
                        ID_Richtlinien = @ID_Richtlinien
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_GebieteSoll", ID_GebieteSoll));
            sqlParameters.Add(this.SqlParameter("@ID_Richtlinien", ID_Richtlinien));

            return GetRecord(sql, sqlParameters, "RichtlinienSoll");
        }

        public int InsertGebieteSoll(DataRow row)
        {
            StringBuilder sb = new StringBuilder(
                @"
                INSERT INTO GebieteSoll
                    (
                    ID_Chirurgen,
                    ID_Gebiete,
                    Von,
                    Bis
                    )
                    VALUES
                    (
                     @ID_Chirurgen,
                     @ID_Gebiete,
                     @Von,
                     @Bis
                    )
                ");

            sb.Replace("@Von", this.DateTime2DBDateTimeString(row["Von"]));
            sb.Replace("@Bis", this.DateTime2DBDateTimeString(row["Bis"]));

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", row["ID_Chirurgen"]));
            sqlParameters.Add(this.SqlParameter("@ID_Gebiete", row["ID_Gebiete"]));

            return this.InsertRecord(sb.ToString(), sqlParameters, "GebieteSoll");
        }

        public int InsertRichtlinienSoll(DataRow row)
        {
            string sql =
                @"
                INSERT INTO RichtlinienSoll
                    (
                    ID_GebieteSoll,
                    ID_Richtlinien,
                    Soll
                    )
                    VALUES
                    (
                     @ID_GebieteSoll,
                     @ID_Richtlinien,
                     @Soll
                    )
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_GebieteSoll", row["ID_GebieteSoll"]));
            sqlParameters.Add(this.SqlParameter("@ID_Richtlinien", row["ID_Richtlinien"]));
            sqlParameters.Add(this.SqlParameter("@Soll", row["Soll"]));

            return this.InsertRecord(sql, sqlParameters, "RichtlinienSoll");
        }

        public bool DeleteGebieteSoll(int ID_GebieteSoll)
        {
            string sql = @"
                DELETE FROM
                    RichtlinienSoll
                WHERE
                    ID_GebieteSoll = @ID_GebieteSoll
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(SqlParameter("@ID_GebieteSoll", ID_GebieteSoll));
            int iEffectedRecords = this.ExecuteNonQuery(sql, sqlParameters);

            sql = @"
                DELETE FROM
                    GebieteSoll
                WHERE
                    ID_GebieteSoll = @ID_GebieteSoll
                ";

            sqlParameters = new ArrayList();
            sqlParameters.Add(SqlParameter("@ID_GebieteSoll", ID_GebieteSoll));
            iEffectedRecords = this.ExecuteNonQuery(sql, sqlParameters);

            return true;
        }

        public bool UpdateRichtlinienSollSoll(DataRow row)
        {
            string sql = @"
                UPDATE 
                    RichtlinienSoll
                SET
                    Soll=@Soll
                WHERE
                    ID_RichtlinienSoll = @ID_RichtlinienSoll
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(SqlParameter("@Soll", (int)row["Soll"]));
            sqlParameters.Add(SqlParameter("@ID_RichtlinienSoll", (int)row["ID_RichtlinienSoll"]));

            int iEffectedRecords = ExecuteNonQuery(sql, sqlParameters);

            return (iEffectedRecords == 1);
        }

        public bool UpdateGebieteSoll(DataRow row)
        {
            StringBuilder sb = new StringBuilder(@"
                UPDATE 
                    GebieteSoll
                SET
                    Von = @Von,
                    Bis = @Bis
                WHERE
                    ID_GebieteSoll=@ID_GebieteSoll
                ");

            sb.Replace("@Von", this.DateTime2DBDateTimeString(row["Von"]));
            sb.Replace("@Bis", this.DateTime2DBDateTimeString(row["Bis"]));

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(SqlParameter("@ID_GebieteSoll", row["ID_GebieteSoll"]));

            int iEffectedRecords = ExecuteNonQuery(sb.ToString(), sqlParameters);

            return (iEffectedRecords == 1);
        }

        public bool SetRichtlinienSoll(int ID_GebieteSoll, int ID_Richtlinien, int soll)
        {
            bool success = false;
            DataRow row = GetRichtlinienSoll(ID_GebieteSoll, ID_Richtlinien);

            if (row == null)
            {
                row = CreateDataRowRichtlinienSoll();
                row["ID_GebieteSoll"] = ID_GebieteSoll;
                row["ID_Richtlinien"] = ID_Richtlinien;
                row["Soll"] = soll;

                if (InsertRichtlinienSoll(row) != -1)
                {
                    success = true;
                }
            }
            else
            {
                row["Soll"] = soll;
                success = UpdateRichtlinienSollSoll(row);
            }

            return success;
        }

#endregion

#region OrbisTest
        public void ExportOperationenToTextFileOrbis(string fileName)
        {
            StreamWriter writer = null;
            IDataReader reader = null;

            string[] excludeList = {
                "Achilles", "Adler", "Alefs",
                "Beck", "Berg", "Berger", "Bier", "Binz", "Blocksberg", 
                "Decker", "Dichter", "Donner",
                "Eisen",
                "Gast", "Gauss", "Gebhard",
                "Hase", "Heck", "Heil", "Himmel", "Hölle",
                "Jacke", 
                "Klug", "Kohle", "Krümel", "Kumpel",
                "Lada", "Lapp", "Lipp", "Lupp",
                "Maurer", "Meier", "Miller", "Motor", "Müller",
                "Nass", "Nougat", "Nuss",
                "Ohm", "Otter", 
                "Palme", "Patz", "Platz", "Pulme",
                "Ritsche", 
                "Schatz", "Schlapp", "Schminke", "Scholz", "Schuetze", "Schulz", "Schwalbe",
                "Stilz", "Stulz",
                "Wanze", "Wein", "Wurst", 
                "Zinke"
            };

            StringBuilder sb = new StringBuilder(@"
                SELECT 
                    a.Fallzahl,
                    a.Datum, 
                    a.Zeit,
                    a.ZeitBis,
                    a.[OPS-Kode],
                    a.[OPS-Text],
                    b.Nachname,
                    b.Vorname,
                    c.Beschreibung
                FROM
                    OPFunktionen c INNER JOIN (
                            ChirurgenOperationen a INNER JOIN Chirurgen b ON b.ID_Chirurgen = a.ID_Chirurgen) 
                    ON c.ID_OPFunktionen = a.ID_OPFunktionen
                    ");

            if (excludeList != null && excludeList.Length > 0)
            {
                sb.Append(" WHERE b.Nachname not in (");
                for (int i = 0; i < excludeList.Length; i++)
                {
                    if (i > 0)
                    {
                        sb.Append(",");
                    }
                    sb.Append("'");
                    sb.Append(excludeList[i]);
                    sb.Append("'");
                }
                sb.Append(")");
            }

            try
            {
                writer = new StreamWriter(fileName, false, Encoding.Unicode);

                if (OpenForImport())
                {
                    IDbCommand command = Connection.CreateCommand();
                    string sql = CleanSqlStatement(sb.ToString());
                    command.CommandText = sql;

                    reader = command.ExecuteReader();

                    // Durch alle Operationen laufen
                    while (reader.Read())
                    {
                        DateTime dtDatum = reader.GetDateTime(1);
                        DateTime dtZeit = reader.GetDateTime(2);
                        DateTime dtZeitBis = reader.GetDateTime(3);

                        string datum = Tools.DBNullableDateTime2DateTimeString(dtDatum);
                        string zeit = string.Format(CultureInfo.InvariantCulture, "{0}.{1}.{2} {3}:{4}",
                            dtDatum.Day, dtDatum.Month, dtDatum.Year,
                            dtZeit.Hour, dtZeit.Minute);
                        string zeitBis = string.Format(CultureInfo.InvariantCulture, "{0}.{1}.{2} {3}:{4}",
                            dtDatum.Day, dtDatum.Month, dtDatum.Year,
                            dtZeitBis.Hour, dtZeitBis.Minute);
                        string beschreibung = reader.GetString(8);

                        if (beschreibung.CompareTo("Operateur") == 0)
                        {
                            beschreibung = "1. Operateur";
                        }

                        string line = string.Format(CultureInfo.InvariantCulture, "{0};{1};{2};{3};{4};{5};{6};{7};{8};",
                            reader.GetString(0),
                            datum,
                            zeit,
                            zeitBis,
                            reader.GetString(4),
                            reader.GetString(5),
                            reader.GetString(6),
                            reader.GetString(7),
                            beschreibung
                        );

                        writer.WriteLine(line);
                    }
                    reader.Close();
                    reader.Dispose();
                    reader = null;
                }
            }
            catch (Exception e)
            {
                MessageBox(e.Message);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Flush();
                    writer.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                    reader = null;
                }
            }
        }
#endregion
    }
}

