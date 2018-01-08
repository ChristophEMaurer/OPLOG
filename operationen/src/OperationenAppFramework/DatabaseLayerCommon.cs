using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;


using Operationen;
using AppFramework;
using Utility;

namespace CMaurer.Operationen.AppFramework
{
    /// <summary>
    /// Common database code for windows and web applications
    /// </summary>
    public class DatabaseLayerCommon : DatabaseLayerBase
    {
        /// <summary>
        /// Callback for database progress
        /// </summary>
        public event ProgressCallback Progress;

        /// <summary>
        /// Text "all" localized
        /// </summary>
        protected string TextAlle;

        private BusinessLayerCommon _businessLayerCommon;

        /// <summary>
        /// Constructor of common database code
        /// </summary>
        /// <param name="businessLayer">the business layer</param>
        /// <param name="databaseType">database type</param>
        /// <param name="connectionString">connection string</param>
        public DatabaseLayerCommon(BusinessLayerCommon businessLayer, DatabaseType databaseType, string connectionString)
            : base(businessLayer, databaseType, connectionString)
        {
            _businessLayerCommon = businessLayer;

            TextAlle = _businessLayerCommon.GetText("DatabaseLayerCommon", "alle");
        }

        protected void FireProgressEvent(ProgressEventArgs e)
        {
            if (Progress != null)
            {
                Progress(e);
            }
        }

        /// <summary>
        /// Holt die Anzahl der Operationen von einem Chirurgen in einem Zeitraum, die über die Zuordnungen
        /// zu einem Gebiet herauskommen.
        /// 
        /// </summary>
        /// <param name="nID_Gebiete"></param>
        /// <param name="nID_Chirurgen"></param>
        /// <param name="dtFrom"></param>
        /// <param name="dtTo"></param>
        /// <returns></returns>
        public DataView GetRichtlinienOPSummen(int nID_Gebiete, int nID_Chirurgen, int quelle, DateTime? dtFrom, DateTime? dtTo)
        {
            // In Access selber funktioniert * statt %, hier aber nicht.

            StringBuilder sb = new StringBuilder(@"
        SELECT 
            Richtlinien.LfdNummer,
            Richtlinien.Richtzahl,
            $$UntBehMethode$$ as UntBehMethode,
            count(ChirurgenOperationen.[OPS-Kode]) as Anzahl
        FROM 
            Richtlinien LEFT JOIN (RichtlinienOpsKodes left JOIN ChirurgenOperationen ON ChirurgenOperationen.[OPS-Kode] like $$LIKE$$)
            ON RichtlinienOpsKodes.ID_Richtlinien=Richtlinien.ID_Richtlinien
        where
            ChirurgenOperationen.ID_Richtlinien is null and 
            Richtlinien.ID_Gebiete = @ID_Gebiete and 
            ChirurgenOperationen.ID_Chirurgen=@ID_Chirurgen and 
            ChirurgenOperationen.ID_OPFunktionen = " + (int)OP_FUNCTION.OP_FUNCTION_OP + @"   
            $datefrom$ $dateto$
            $quelle$
        group by 
            Richtlinien.LfdNummer,
            Richtlinien.Richtzahl,
            $$UntBehMethode$$

        UNION ALL

        SELECT 
            Richtlinien.LfdNummer,
            Richtlinien.Richtzahl,
            $$UntBehMethode$$ as UntBehMethode,
            count(ChirurgenOperationen.[OPS-Kode]) as Anzahl
        FROM 
            Richtlinien INNER JOIN ChirurgenOperationen ON Richtlinien.ID_Richtlinien=ChirurgenOperationen.ID_Richtlinien
        where
            ChirurgenOperationen.ID_Richtlinien is not null and 
            Richtlinien.ID_Gebiete = @ID_Gebiete and 
            ChirurgenOperationen.ID_Chirurgen=@ID_Chirurgen and 
            ChirurgenOperationen.ID_OPFunktionen = " + (int)OP_FUNCTION.OP_FUNCTION_OP + @"
            $datefrom$ $dateto$
            $quelle$
        group by 
            Richtlinien.LfdNummer,
            Richtlinien.Richtzahl,
            $$UntBehMethode$$

        order by
            1
            ");

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

            HandleDatum(dtFrom, dtTo, sb, "ChirurgenOperationen");

            ArrayList sqlParameter = new ArrayList();
            sqlParameter.Add(this.SqlParameter("@ID_Gebiete", nID_Gebiete));
            sqlParameter.Add(this.SqlParameter("@ID_Chirurgen", nID_Chirurgen));

            HandleQuelleMultiple(quelle, "", sb);

            return GetDataView(sb.ToString(), sqlParameter, "RichtlinienOPSCodes");
        }

        public DataView GetRichtlinien(int nID_Gebiete)
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
                WHERE 
                    ID_Gebiete = @ID_Gebiete
                ORDER BY
                    LfdNummer
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@ID_Gebiete", nID_Gebiete));

            DataView oDataView = this.GetDataView(sSQL, aSQLParameters, "Richtlinien");

            return oDataView;
        }

        public void HandleDatum(DateTime? from, DateTime? to, StringBuilder sb)
        {
            HandleDatum(from, to, sb, null);
        }

        public void HandleDatum(DateTime? from, DateTime? to, StringBuilder sb, string prefix)
        {
            string s = "";

            if (from.HasValue)
            {
                if (string.IsNullOrEmpty(prefix))
                {
                    s = string.Format("AND {0} <= Datum ", DateTime2DBDateTimeString(from.Value));
                }
                else
                {
                    s = string.Format("AND {0} <= {1}.Datum ", DateTime2DBDateTimeString(from.Value), prefix);
                }
            }

            sb.Replace("$datefrom$", s);

            s = "";
            if (to.HasValue)
            {
                if (string.IsNullOrEmpty(prefix))
                {
                    s = string.Format("AND Datum <= {0} ", DateTime2DBDateTimeString(to));
                }
                else
                {
                    s = string.Format("AND {0}.Datum <= {1} ", prefix, DateTime2DBDateTimeString(to));
                }
            }
            sb.Replace("$dateto$", s);
        }

        /// <summary>
        /// Use this function if there is more than one accurrence of $quelle$.
        /// </summary>
        /// <param name="quelle"></param>
        /// <param name="sb"></param>
        protected void HandleQuelleMultiple(int quelle, string prefix, StringBuilder sb)
        {
            if (quelle == BusinessLayerCommon.OperationQuelleIntern || quelle == BusinessLayerCommon.OperationQuelleExtern)
            {
                if (string.IsNullOrEmpty(prefix))
                {
                    sb.Replace("$quelle$", string.Format("and Quelle = {0}", quelle));
                }
                else
                {
                    sb.Replace("$quelle$", string.Format("and {0}.Quelle = {1}", prefix, quelle));
                }
            }
            else
            {
                sb.Replace("$quelle$", "");
            }
        }

        public DataView GetGebiete()
        {
            return GetGebiete(false);
        }

        public DataView GetGebiete(bool includeAll)
        {
            string sql = "";

            sql += @"
                SELECT 
                    ID_Gebiete,
                    Gebiet,
                    Bemerkung,
                    Herkunft
                FROM 
                    Gebiete
                order by
                    Gebiet
                    ";

            DataView dv = GetDataView(sql, null, "Gebiete");

            if (includeAll)
            {
                DataRow row = dv.Table.NewRow();
                row["ID_Gebiete"] = BusinessLayerCommon.ID_Alle;
                row["Gebiet"] = TextAlle;
                row["Bemerkung"] = TextAlle;
                row["Herkunft"] = TextAlle;

                dv.Table.Rows.InsertAt(row, 0);
            }

            return dv;
        }

        public DataRow Login(string strUser, string strPassword)
        {
            string hashedPassword = Tools.Password2HashedPassword(strPassword);

            string sql =
                @"
                SELECT 
                    ID_Chirurgen,
                    UserID,
                    MustChangePassword,
                    Nachname,
                    Vorname,
                    Anfangsdatum,
                    Anrede,
                    Aktiv,
                    IstWeiterbilder
                FROM 
                    Chirurgen
                WHERE
                    UserID=@UserID
                    AND Password=@Password
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@UserID", strUser));
            sqlParameters.Add(this.SqlParameter("@Password", hashedPassword));

            return GetRecord(sql, sqlParameters, "Chirurgen");
        }

        public DataRow Login(string strUser)
        {
            string sql =
                @"
                SELECT 
                    ID_Chirurgen,
                    UserID,
                    MustChangePassword,
                    Nachname,
                    Vorname,
                    Anfangsdatum,
                    Anrede,
                    Aktiv,
                    IstWeiterbilder
                FROM 
                    Chirurgen
                WHERE
                    UserID=@UserID
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@UserID", strUser));

            return GetRecord(sql, sqlParameters, "Chirurgen");
        }

        /// <summary>
        /// Get all rights of all groups the specified user belongs to
        /// </summary>
        /// <param name="ID_Chirurgen"></param>
        /// <returns></returns>
        public ArrayList GetUserRights(int ID_Chirurgen)
        {
            ArrayList rights = new ArrayList();

            String sql =
                @"
                    SELECT distinct
                        SecRights.Name
                    FROM 
                        SecGroupsSecRights inner join SecRights on SecGroupsSecRights.ID_SecRights=SecRights.ID_SecRights
                    WHERE 
                        ID_SecGroups in (select ID_SecGroups from SecGroupsChirurgen where ID_Chirurgen=@ID_Chirurgen)
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));

            DataView dv = GetDataView(sql, sqlParameters, "SecGroupsSecRights");

            foreach (DataRow row in dv.Table.Rows)
            {
                rights.Add(((string)row["Name"]).ToLower());
            }

            return rights;
        }

        public bool UserHasRight(int ID_Chirurgen, string right)
        {
            ArrayList arrayList = GetUserRights(ID_Chirurgen);
            return arrayList.Contains(right);
        }

        #region UserSettings
        public DataView GetUserSettings(int ID_Chirurgen, string section)
        {
            string sql = @"SELECT ID_UserSettings, ID_Chirurgen, [Key], [Value]
                from 
                    UserSettings 
                where 
                    ID_Chirurgen=@ID_Chirurgen 
                    and [Section]=@Section
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
            sqlParameters.Add(this.SqlParameter("@Section", section));

            return GetDataView(sql, sqlParameters, "UserSettings");
        }

        public DataRow GetUserSettings(int ID_Chirurgen, string section, string key)
        {
            string sql = @"SELECT ID_UserSettings, ID_Chirurgen, [Key], [Value], [Blob]
                from 
                    UserSettings 
                where 
                    ID_Chirurgen=@ID_Chirurgen 
                    and [Section]=@Section
                    and [Key]=@Key
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
            sqlParameters.Add(this.SqlParameter("@Section", section));
            sqlParameters.Add(this.SqlParameter("@Key", key));

            return GetRecord(sql, sqlParameters, "UserSettings");
        }

        //
        // Save a setting. INSERT if missing, update if it exists.
        //
        public bool SaveUserSettings(int ID_Chirurgen, string section, string key, string value)
        {
            bool success = false;

            string sql = @"SELECT count(ID_UserSettings)
                from 
                    UserSettings 
                where 
                    ID_Chirurgen=@ID_Chirurgen 
                    and [Section]=@Section
                    and [Key]=@Key
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
            sqlParameters.Add(this.SqlParameter("@Section", section));
            sqlParameters.Add(this.SqlParameter("@Key", key));

            long count = ExecuteScalar(sql, sqlParameters);

            if (count > 0)
            {
                sql = @"update 
                        UserSettings
                    set 
                        [Value]=@Value
                    where 
                        ID_Chirurgen=@ID_Chirurgen 
                        and [Section]=@Section
                        and [Key]=@Key";

                sqlParameters = new ArrayList();
                sqlParameters.Add(this.SqlParameter("@Value", value));
                sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                sqlParameters.Add(this.SqlParameter("@Section", section));
                sqlParameters.Add(this.SqlParameter("@Key", key));

                success = (1 == this.ExecuteNonQuery(sql, sqlParameters));
            }
            else
            {
                sql = @"
                INSERT INTO UserSettings
                    (
                        [ID_Chirurgen],
                        [Section],
                        [Key],
                        [Value],
                        [Blob]
                    )
                    VALUES
                    (
                     @ID_Chirurgen,
                     @Section,
                     @Key,
                     @Value,
                     null
                    )
                ";

                sqlParameters = new ArrayList();
                sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                sqlParameters.Add(this.SqlParameter("@Section", section));
                sqlParameters.Add(this.SqlParameter("@Key", key));
                sqlParameters.Add(this.SqlParameter("@Value", value));

                int id = InsertRecord(sql, sqlParameters, "UserSettings");
                if (id > 0)
                {
                    success = true;
                }
            }

            return success;
        }

        public bool SetUserSettings(int ID_Chirurgen, string section, string key, string value, byte[] blob)
        {
            bool success = false;

            string sql = @"SELECT count(ID_UserSettings)
                from 
                    UserSettings 
                where 
                    ID_Chirurgen=@ID_Chirurgen 
                    and [Section]=@Section
                    and [Key]=@Key
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
            sqlParameters.Add(this.SqlParameter("@Section", section));
            sqlParameters.Add(this.SqlParameter("@Key", key));

            long count = ExecuteScalar(sql, sqlParameters);

            if (count > 0)
            {
                sql = @"update 
                        UserSettings
                    set 
                        [Value]=@Value,
                        [Blob]=@Blob
                    where 
                        ID_Chirurgen=@ID_Chirurgen 
                        and [Section]=@Section
                        and [Key]=@Key";

                sqlParameters = new ArrayList();
                sqlParameters.Add(this.SqlParameter("@Value", value));
                sqlParameters.Add(this.SqlParameter("@Blob", blob));
                sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                sqlParameters.Add(this.SqlParameter("@Section", section));
                sqlParameters.Add(this.SqlParameter("@Key", key));

                success = (1 == this.ExecuteNonQuery(sql, sqlParameters));
            }
            else
            {
                sql = @"
                INSERT INTO UserSettings
                    (
                        [ID_Chirurgen],
                        [Section],
                        [Key],
                        [Value],
                        [Blob]
                    )
                    VALUES
                    (
                     @ID_Chirurgen,
                     @Section,
                     @Key,
                     @Value,
                     @Blob
                    )
                ";

                sqlParameters = new ArrayList();
                sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                sqlParameters.Add(this.SqlParameter("@Section", section));
                sqlParameters.Add(this.SqlParameter("@Key", key));
                sqlParameters.Add(this.SqlParameter("@Value", value));
                sqlParameters.Add(this.SqlParameter("@Blob", blob));

                int id = InsertRecord(sql, sqlParameters, "UserSettings");
                if (id > 0)
                {
                    success = true;
                }
            }

            return success;
        }

        public int DeleteUserSettings(int ID_Chirurgen, string section)
        {
            string sql = @"
                DELETE FROM
                    UserSettings
                WHERE
                    ID_Chirurgen=@ID_Chirurgen
                    and [Section]=@Section
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));
            sqlParameters.Add(SqlParameter("@Section", section));

            int numRecords = this.ExecuteNonQuery(sql, sqlParameters);

            return numRecords;
        }

        public bool DeleteUserSettings(int ID_Chirurgen, string section, string key)
        {
            string sql = @"
                DELETE FROM
                    UserSettings
                WHERE
                    ID_Chirurgen=@ID_Chirurgen
                    and [Section]=@Section
                    and [Key]=@Key
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));
            sqlParameters.Add(SqlParameter("@Section", section));
            sqlParameters.Add(SqlParameter("@Key", key));

            int numRecords = this.ExecuteNonQuery(sql, sqlParameters);

            return true;
        }
        public int InsertUserSettings(int ID_Chirurgen, string section, string key, string value)
        {
            DataTable table = new DataTable();
            DataRow row = CreateDataRowUserSettings(table, ID_Chirurgen, section, key, value);

            return InsertUserSettings(row);
        }

        public int InsertUserSettings(DataRow dataRow)
        {
            string sb = @"
                INSERT INTO UserSettings
                    (
                        [ID_Chirurgen],
                        [Section],
                        [Key],
                        [Value]
                    )
                    VALUES
                    (
                     @ID_Chirurgen,
                     @Section,
                     @Key,
                     @Value
                    )
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", dataRow["ID_Chirurgen"]));
            sqlParameters.Add(this.SqlParameter("@Section", dataRow["Section"]));
            sqlParameters.Add(this.SqlParameter("@Key", dataRow["Key"]));
            sqlParameters.Add(this.SqlParameter("@Value", dataRow["Value"]));

            return InsertRecord(sb, sqlParameters, "UserSettings");
        }
        public DataRow CreateDataRowUserSettings(DataTable dt, int ID_Chirurgen, string section, string key, string value)
        {
            DataRow dataRow;

            // Define the columns of the table.
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add(new DataColumn("ID_UserSettings", typeof(int)));
                dt.Columns.Add(new DataColumn("ID_Chirurgen", typeof(int)));
                dt.Columns.Add(new DataColumn("Section", typeof(string)));
                dt.Columns.Add(new DataColumn("Key", typeof(string)));
                dt.Columns.Add(new DataColumn("Value", typeof(string)));
                dt.Columns.Add(new DataColumn("Blob", typeof(byte[])));
            }

            dataRow = dt.NewRow();

            dataRow["ID_Chirurgen"] = ID_Chirurgen;
            dataRow["Section"] = section;
            dataRow["Key"] = key;
            dataRow["Value"] = value;

            return dataRow;
        }
        public DataView GetUserSettings(int ID_Chirurgen)
        {
            string sql = @"SELECT ID_UserSettings, ID_Chirurgen, [Section], [Key], [Value] from UserSettings where ID_Chirurgen=@ID_Chirurgen";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));

            return GetDataView(sql, sqlParameters, "UserSettings");
        }
#endregion

        public DataView GetChirurgenFunktionen()
        {
            string sSQL =
                @"
                SELECT 
                    ID_ChirurgenFunktionen, 
                    Funktion
                FROM 
                    ChirurgenFunktionen
                ORDER BY 
                    Funktion
                ";

            return GetDataView(sSQL, null, "ChirurgenFunktionen");
        }

        /// <summary>
        /// Holt eine Liste aller Chirurgen, allerdings enthält diese nur den angemeldeten,
        /// weil er die anderen nicht sehen darf
        /// </summary>
        /// <param name="ID_Chirurgen"></param>
        /// <returns></returns>
        public DataView GetChirurgen(int ID_Chirurgen, string where)
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
                    $displaytext$
                FROM
                    Chirurgen a
                WHERE 
                    $where$
                    $chirurgen$
                order by 
                    a.Nachname,
                    a.Vorname
                 ");

            sb.Replace("$displaytext$", MakeConcat("a.Nachname", "', '", "a.Vorname") + " as DisplayText");

            sb.Replace("$where$", where);

            ArrayList sqlParameters = new ArrayList();

            HandleSubselectID_Chirurgen(sqlParameters, "a", ID_Chirurgen, sb);

            return GetDataView(sb.ToString(), sqlParameters, "Chirurgen");
        }

        public bool UserIstWeiterbilder(int ID_Chirurgen)
        {
            string sql = 
                @"
                SELECT 
                    count(ID_Chirurgen)
                FROM 
                    Chirurgen
                WHERE
                    ID_Chirurgen=@ID_Chirurgen
                    AND IstWeiterbilder = 1
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));

            long count = ExecuteScalar(sql, sqlParameters);

            // Es kann nur 1 oder 0 herauskommen
            return count > 0;
        }

        public DataRow GetChirurg(int ID_Chirurgen)
        {
            StringBuilder sb = new StringBuilder(
                @"
                SELECT 
                    ID_Chirurgen,
                    ID_ChirurgenFunktionen,
                    Nachname,
                    Vorname,
                    UserID,
                    ImportID,
                    Password,
                    Anfangsdatum,
                    Anrede,
                    Aktiv,
                    Lizenzdaten,
                    IstWeiterbilder
                FROM 
                    Chirurgen
                WHERE
                    ID_Chirurgen=@ID_Chirurgen
                ");

            ArrayList arSqlParameter = new ArrayList();
            arSqlParameter.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));

            return GetRecord(sb.ToString(), arSqlParameter, "Chirurgen");
        }


        /// <summary>
        /// Liefert einen string zurück, der eine Auswahl an ID_Chirurgen liefert, je nachdem, welche von diesen man sehen darf.
        /// 
        /// Wenn der Benutzer ID_Chirurgen das Recht select.surgeons.all hat, kommt "" zurück , also alle
        /// 
        /// Wenn der Benutzer ID_Chirurgen das Recht select.surgeons.abteilung hat und weiterbilder ist, 
        /// kommen alle ID_Chirurgen zurück aus derselben Abteilung und alle ihm zugeordneten Chirurgen
        /// 
        /// Wenn der Benutzer ID_Chirurgen das Recht select.surgeons.abteilung hat, kommen alle ID_Chirurgen zurück aus derselben Abteilung
        /// 
        /// Wenn der aktuelle Benutzer ein Weiterbilder ist, kommen alle ihm zugeordneten Chirurgen und er selber heraus
        /// 
        /// prefix.ID_Chirurgen in (select ID_Chirurgen from WeiterbilderChirurgen where ID_Weiterbilder=@ID_Chirurgen)
        /// 
        /// </summary>
        /// <param name="sqlParameters"></param>
        /// <param name="prefix"></param>
        /// <param name="ID_Chirurgen">Die ID des angemeldeten Benutzers</param>
        /// <param name="sb"></param>
        protected void HandleSubselectID_Chirurgen(ArrayList sqlParameters, string prefix, int ID_Chirurgen, StringBuilder sb)
        {
            string sql = "";
            ArrayList userRights;
            bool userIstWeiterbilder;

            if (ID_Chirurgen == _businessLayerCommon.CurrentUser_ID_Chirurgen)
            {
                //
                // Data for current user is cached, do not access database again
                //
                userRights = _businessLayerCommon.CurrentUserRights;
                userIstWeiterbilder = _businessLayerCommon.CurrentUser_IstWeiterbilder;
            }
            else
            {
                //
                // any other user: access database once
                //
                userRights = GetUserRights(ID_Chirurgen);
                userIstWeiterbilder = _businessLayerCommon.UserIstWeiterbilder(ID_Chirurgen);
            }


            if (userRights.Contains("select.surgeons.all"))
            {
                sql = "";
            }
            else if (userRights.Contains("select.surgeons.abteilung") && userIstWeiterbilder)
            {
                // Alle mir als Weiterbilder zugeordneten
                sql = string.Format(@" AND 
                        (
                            ({0}.ID_Chirurgen in (select ID_Chirurgen from WeiterbilderChirurgen where ID_Weiterbilder={1}))"

                            // Alle Chirurgen aus allen meinen Abteilungen
                            + @" or ({0}.ID_Chirurgen in 
                                    (select distinct ID_Chirurgen from AbteilungenChirurgen where ID_Abteilungen in 
                                        (select distinct ID_Abteilungen from AbteilungenChirurgen where ID_Chirurgen={1})))"

                      + ")", prefix, ID_Chirurgen);
            }
            else if (userRights.Contains("select.surgeons.abteilung"))
            {
                // Alle Chirurgen aus allen meinen Abteilungen
                sql = string.Format(@" and ({0}.ID_Chirurgen in 
                        (select ID_Chirurgen from AbteilungenChirurgen where ID_Abteilungen in 
                            (select ID_Abteilungen from AbteilungenChirurgen where ID_Chirurgen={1})))", prefix, ID_Chirurgen);
            }
            else if (userIstWeiterbilder)
            {
                // Alle diesem Weiterbilder zugeordneten chirurgen
                sql = string.Format("and ({0}.ID_Chirurgen in (select ID_Chirurgen from WeiterbilderChirurgen where ID_Weiterbilder={1})", prefix, ID_Chirurgen)

                // Der Chirurg selber
                 + string.Format(" or {0}.ID_Chirurgen={1})", prefix, ID_Chirurgen)
                 ;
            }
            else
            {
                // Ein normaler Benutzer sieht immer nur sich selber
                sql = string.Format(" and {0}.ID_Chirurgen={1}", prefix, ID_Chirurgen);
            }

            sb.Replace("$chirurgen$", sql);
        }

        protected void HandleSubselectID_ChirurgenOld(ArrayList sqlParameters, string prefix, int ID_Chirurgen, StringBuilder sb)
        {
            //
            // beim Aufruf von UserHasRight() wird der angemeldete Benutzer genommen und nicht der übergebene ID_Chirurgen
            //

            string sql = "";

            if (_businessLayerCommon.UserHasRight("select.surgeons.all"))
            {
                sql = "";
            }
            else if (_businessLayerCommon.UserHasRight("select.surgeons.abteilung") && _businessLayerCommon.CurrentUser_IstWeiterbilder)
            {
                // Alle mir als Weiterbilder zugeordneten
                sql = string.Format(@" AND 
                        (
                            ({0}.ID_Chirurgen in (select ID_Chirurgen from WeiterbilderChirurgen where ID_Weiterbilder={1}))"

                            // Alle Chirurgen aus allen meinen Abteilungen
                            + @" or ({0}.ID_Chirurgen in 
                                    (select distinct ID_Chirurgen from AbteilungenChirurgen where ID_Abteilungen in 
                                        (select distinct ID_Abteilungen from AbteilungenChirurgen where ID_Chirurgen={1})))"

                      + ")", prefix, ID_Chirurgen);
            }
            else if (_businessLayerCommon.UserHasRight("select.surgeons.abteilung"))
            {
                // Alle Chirurgen aus allen meinen Abteilungen
                sql = string.Format(@" and ({0}.ID_Chirurgen in 
                        (select ID_Chirurgen from AbteilungenChirurgen where ID_Abteilungen in 
                            (select ID_Abteilungen from AbteilungenChirurgen where ID_Chirurgen={1})))", prefix, ID_Chirurgen);
            }
            else if (_businessLayerCommon.CurrentUser_IstWeiterbilder)
            {
                // Alle diesem Weiterbilder zugeordneten chirurgen
                sql = string.Format("and ({0}.ID_Chirurgen in (select ID_Chirurgen from WeiterbilderChirurgen where ID_Weiterbilder={1})", prefix, ID_Chirurgen)

                // Der Chirurg selber
                 + string.Format(" or {0}.ID_Chirurgen={1})", prefix, ID_Chirurgen)
                 ;
            }
            else
            {
                // Ein normaler Benutzer sieht immer nur sich selber
                sql = string.Format(" and {0}.ID_Chirurgen={1}", prefix, ID_Chirurgen);
            }

            sb.Replace("$chirurgen$", sql);
        }
        #region ChirurgenRichtlinien
        public DataView GetChirurgenRichtlinien(int ID_Chirurgen, int ID_Gebiete)
        {
            string sql =
                @"
                SELECT 
                    a.ID_ChirurgenRichtlinien,
                    a.ID_Chirurgen,
                    a.ID_Richtlinien,
                    a.Datum,
                    a.Ort,
                    a.Anzahl,
                    b.LfdNummer,
                    b.Richtzahl,
                    b.UntBehMethode
                FROM
                    ChirurgenRichtlinien a
                    inner join Richtlinien b on a.ID_Richtlinien = b.ID_Richtlinien
                WHERE
                    a.ID_Chirurgen=@ID_Chirurgen
                    and b.ID_Gebiete=@ID_Gebiete
                ORDER BY
                    b.LfdNummer, a.Datum desc
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
            sqlParameters.Add(this.SqlParameter("@ID_Gebiete", ID_Gebiete));

            return GetDataView(sql, sqlParameters, "ChirurgenRichtlinien");
        }

        /// <summary>
        /// Holt die vorab manuell erfassten Ist-Zahlen für Richtlinien
        /// </summary>
        /// <param name="ID_Chirurgen"></param>
        /// <param name="ID_Gebiete"></param>
        /// <param name="von"></param>
        /// <param name="bis"></param>
        /// <returns></returns>
        public DataView GetChirurgenRichtlinien(int ID_Chirurgen, int ID_Gebiete, DateTime? von, DateTime? bis)
        {
            StringBuilder sb = new StringBuilder(
                @"
                SELECT 
                    a.ID_ChirurgenRichtlinien,
                    a.ID_Chirurgen,
                    a.ID_Richtlinien,
                    a.Datum,
                    a.Ort,
                    a.Anzahl,
                    b.LfdNummer,
                    b.Richtzahl,
                    b.UntBehMethode
                FROM
                    ChirurgenRichtlinien a
                    inner join Richtlinien b on a.ID_Richtlinien = b.ID_Richtlinien
                WHERE
                    a.ID_Chirurgen=@ID_Chirurgen
                    and b.ID_Gebiete=@ID_Gebiete
                    $datefrom$ $dateto$
                ORDER BY
                    a.Datum desc
                ");

            HandleDatum(von, bis, sb, "a");

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
            sqlParameters.Add(this.SqlParameter("@ID_Gebiete", ID_Gebiete));

            return GetDataView(sb.ToString(), sqlParameters, "ChirurgenRichtlinien");
        }

        /// <summary>
        /// Holt die vorab manuell erfassten Ist-Zahlen für Richtlinien
        /// </summary>
        /// <param name="ID_Chirurgen"></param>
        /// <param name="ID_Gebiete"></param>
        /// <param name="von"></param>
        /// <param name="bis"></param>
        /// <returns></returns>
        public DataView GetChirurgenRichtlinienSummen(int ID_Chirurgen, int ID_Gebiete, DateTime? von, DateTime? bis)
        {
            StringBuilder sb = new StringBuilder(
                @"
                SELECT 
                    b.LfdNummer,
                    b.Richtzahl,
                    b.UntBehMethode,
                    SUM(a.Anzahl) as Anzahl
                FROM
                    ChirurgenRichtlinien a
                    inner join Richtlinien b on a.ID_Richtlinien = b.ID_Richtlinien
                WHERE
                    a.ID_Chirurgen=@ID_Chirurgen
                    and b.ID_Gebiete=@ID_Gebiete
                    $datefrom$ $dateto$
                GROUP BY
                    b.LfdNummer,
                    b.Richtzahl,
                    b.UntBehMethode
                ");

            HandleDatum(von, bis, sb, "a");

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
            sqlParameters.Add(this.SqlParameter("@ID_Gebiete", ID_Gebiete));

            return GetDataView(sb.ToString(), sqlParameters, "ChirurgenRichtlinien");
        }

        /// <summary>
        /// Holt die vorab manuell erfassten Ist-Zahlen für Richtlinien
        /// </summary>
        /// <param name="ID_Chirurgen"></param>
        /// <param name="ID_Richtlinien"></param>
        /// <param name="von"></param>
        /// <param name="bis"></param>
        /// <returns></returns>
        public DataView GetChirurgenRichtlinienRichtlinie(int ID_Chirurgen, int ID_Richtlinien, DateTime? von, DateTime? bis)
        {
            StringBuilder sb = new StringBuilder(
                @"
                SELECT 
                    a.ID_ChirurgenRichtlinien,
                    a.ID_Chirurgen,
                    a.ID_Richtlinien,
                    a.Datum,
                    a.Ort,
                    a.Anzahl
                FROM
                    ChirurgenRichtlinien a
                WHERE
                    a.ID_Chirurgen=@ID_Chirurgen
                    and a.ID_Richtlinien=@ID_Richtlinien
                    $datefrom$ $dateto$
                ORDER BY
                    a.Datum desc
                ");

            HandleDatum(von, bis, sb, "a");

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
            sqlParameters.Add(this.SqlParameter("@ID_Richtlinien", ID_Richtlinien));

            return GetDataView(sb.ToString(), sqlParameters, "ChirurgenRichtlinien");
        }

        /// <summary>
        /// Holt die vorab manuell erfassten Ist-Zahlen für Richtlinien
        /// </summary>
        /// <param name="ID_ChirurgenRichtlinien"></param>
        /// <returns></returns>
        public DataRow GetChirurgenRichtlinien(int ID_ChirurgenRichtlinien)
        {
            string sql =
                @"
                SELECT 
                    a.ID_ChirurgenRichtlinien,
                    a.ID_Chirurgen,
                    a.ID_Richtlinien,
                    a.Datum,
                    a.Ort,
                    a.Anzahl
                FROM
                    ChirurgenRichtlinien a
                WHERE
                    a.ID_ChirurgenRichtlinien=@ID_ChirurgenRichtlinien
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_ChirurgenRichtlinien", ID_ChirurgenRichtlinien));

            return GetRecord(sql, sqlParameters, "ChirurgenRichtlinien");
        }

        public bool DeleteChirurgenRichtlinien(int ID_ChirurgenRichtlinien)
        {
            string sql = @"
                DELETE FROM
                    ChirurgenRichtlinien
                WHERE
                    ID_ChirurgenRichtlinien=@ID_ChirurgenRichtlinien
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(SqlParameter("@ID_ChirurgenRichtlinien", ID_ChirurgenRichtlinien));

            return 1 == ExecuteNonQuery(sql, sqlParameters);
        }
        public long GetChirurgenRichtlinienCount(int ID_Chirurgen, int ID_Richtlinien)
        {
            string sql = @"
                select count(ID_ChirurgenRichtlinien) FROM
                    ChirurgenRichtlinien
                WHERE
                    ID_Chirurgen=@ID_Chirurgen
                    and ID_Richtlinien=@ID_Richtlinien";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));
            sqlParameters.Add(SqlParameter("@ID_Richtlinien", ID_Richtlinien));

            return this.ExecuteScalar(sql, sqlParameters);
        }
        public int InsertChirurgenRichtlinien(DataRow dataRow)
        {
            StringBuilder sb = new StringBuilder(
                @"
                INSERT INTO ChirurgenRichtlinien
                    (
                    ID_Chirurgen,
                    ID_Richtlinien,
                    Datum,
                    Anzahl,
                    Ort
                    )
                    VALUES
                    (
                     @ID_Chirurgen,
                     @ID_Richtlinien,
                     @Datum,
                     @Anzahl,
                     @Ort
                    )
                ");

            sb.Replace("@Datum", this.DateTime2DBDateTimeString(dataRow["Datum"]));

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(SqlParameter("@ID_Chirurgen", dataRow["ID_Chirurgen"]));
            sqlParameters.Add(SqlParameter("@ID_Richtlinien", dataRow["ID_Richtlinien"]));
            sqlParameters.Add(SqlParameter("@Anzahl", dataRow["Anzahl"]));
            sqlParameters.Add(SqlParameter("@Ort", dataRow["Ort"]));

            return InsertRecord(sb.ToString(), sqlParameters, "ChirurgenRichtlinien");
        }

        public bool UpdateChirurgenRichtlinien(DataRow dataRow)
        {
            StringBuilder sb = new StringBuilder(
                @"
                UPDATE ChirurgenRichtlinien
                SET
                    Datum=@Datum,
                    Anzahl=@Anzahl,
                    Ort=@Ort
                WHERE
                    ID_ChirurgenRichtlinien = @ID_ChirurgenRichtlinien
                ");

            sb.Replace("@Datum", DateTime2DBDateTimeString(dataRow["Datum"]));

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(SqlParameterInt("@Anzahl", dataRow["Anzahl"]));
            sqlParameters.Add(SqlParameter("@Ort", dataRow["Ort"]));
            sqlParameters.Add(SqlParameter("@ID_ChirurgenRichtlinien", dataRow["ID_ChirurgenRichtlinien"]));

            int effectedRecords = this.ExecuteNonQuery(sb.ToString(), sqlParameters);

            return (effectedRecords == 1);
        }
        #endregion

        #region OPFunktionen
        public DataView GetOPFunktionen()
        {
            return GetOPFunktionen(false);
        }

        public DataView GetOPFunktionen(bool includeAll)
        {
            string sql = "";

            sql += @"
                SELECT 
                    ID_OPFunktionen, 
                    LfdNr,
                    Beschreibung
                FROM 
                    OPFunktionen
                ";

            DataView dv = GetDataView(sql, null, "OPFunktionen");

            if (includeAll)
            {
                // mit union select -100 from ... kommt bei mySql long statt integer heraus, und das knallt dann überall
                DataRow row = dv.Table.NewRow();
                row["ID_OPFunktionen"] = BusinessLayerCommon.ID_Alle;
                row["LfdNr"] = BusinessLayerCommon.ID_Alle;
                row["Beschreibung"] = TextAlle;

                dv.Table.Rows.InsertAt(row, 0);
            }

            return dv;
        }

        /// <summary>
        /// Get one record from table OPFunktionen
        /// </summary>
        /// <param name="ID_OPFunktionen"></param>
        /// <returns>The datarow or null</returns>
        public DataRow GetOPFunktion(int ID_OPFunktionen)
        {
            string sql = "";

            sql += @"
                SELECT 
                    ID_OPFunktionen, 
                    LfdNr,
                    Beschreibung
                FROM 
                    OPFunktionen
                WHERE
                    ID_OPFunktionen = @ID_OPFunktionen
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_OPFunktionen", ID_OPFunktionen));

            DataRow row = GetRecord(sql, sqlParameters, "OPFunktionen");

            return row;
        }

        #endregion

        /// <summary>
        /// Holt für einen bestimmten chirurgen für eine Richtlinie alle OPs, die gemäß der Zuordnung zu dieser Richtlinie gehören.
        /// </summary>
        /// <param name="nID_Gebiete"></param>
        /// <param name="nID_Chirurgen"></param>
        /// <param name="dtFrom"></param>
        /// <param name="dtTo"></param>
        /// <returns></returns>
        public DataView GetRichtlinienOPsAll(int nID_Gebiete, int nID_Chirurgen, int quelle, DateTime? dtFrom, DateTime? dtTo)
        {
            //
            // Automatisch zugeordnete
            //
            StringBuilder sb = new StringBuilder(@"
                SELECT 
                    Richtlinien.LfdNummer,
                    Richtlinien.Richtzahl,
                    RichtlinienOpsKodes.[OPS-Kode] as RichtlinieOPSKode,
                    Richtlinien.UntBehMethode,
                    ChirurgenOperationen.Datum,
                    ChirurgenOperationen.[OPS-Kode] as OPSKode,
                    ChirurgenOperationen.[OPS-Text] as OPSText
                FROM 
                    Richtlinien inner JOIN (RichtlinienOpsKodes inner JOIN ChirurgenOperationen 
                                           ON ChirurgenOperationen.[OPS-Kode] like $$LIKE$$)
                    ON RichtlinienOpsKodes.ID_Richtlinien=Richtlinien.ID_Richtlinien
                where
                    ChirurgenOperationen.ID_Richtlinien is null and
                    Richtlinien.ID_Gebiete = @ID_Gebiete and 
                    ChirurgenOperationen.ID_Chirurgen=@ID_Chirurgen and 
                    ChirurgenOperationen.ID_OPFunktionen = " + (int)OP_FUNCTION.OP_FUNCTION_OP + @"
                    $datefrom$ $dateto$
                    $quelle$");

            //
            // Die Operationen, die fest einer Richtlinie zugeordnet sind
            //
            sb.Append(@"
                UNION ALL

                SELECT 
                    Richtlinien.LfdNummer,
                    Richtlinien.Richtzahl,");

            sb.Append("'");
            sb.Append(_businessLayerCommon.GetText("RichtlinienVergleichView", "zuordnung_manuell"));
            sb.Append("'");

            sb.Append(@" as RichtlinieOPSKode,
                    Richtlinien.UntBehMethode,
                    ChirurgenOperationen.Datum,
                    ChirurgenOperationen.[OPS-Kode] as OPSKode,
                    ChirurgenOperationen.[OPS-Text] as OPSText
                FROM 
                    Richtlinien INNER JOIN ChirurgenOperationen ON Richtlinien.ID_Richtlinien = ChirurgenOperationen.ID_Richtlinien
                where
                    ChirurgenOperationen.ID_Richtlinien is not null and
                    Richtlinien.ID_Gebiete = @ID_Gebiete and 
                    ChirurgenOperationen.ID_Chirurgen=@ID_Chirurgen and 
                    ChirurgenOperationen.ID_OPFunktionen = " + (int)OP_FUNCTION.OP_FUNCTION_OP + @"   
                    $datefrom$ $dateto$
                    $quelle$
                order by
                    1, 5 desc
                    ");

            sb.Replace("$$LIKE$$", MakeConcat("RichtlinienOpsKodes.[OPS-Kode]", "'%'"));

            HandleDatum(dtFrom, dtTo, sb, "ChirurgenOperationen");

            ArrayList sqlParameter = new ArrayList();
            sqlParameter.Add(this.SqlParameter("@ID_Gebiete", nID_Gebiete));
            sqlParameter.Add(this.SqlParameter("@ID_Chirurgen", nID_Chirurgen));

            HandleQuelleMultiple(quelle, "", sb);

            return GetDataView(sb.ToString(), sqlParameter, "RichtlinienOPSCodes");
        }

        /// <summary>
        /// Genau wie GetRichtlinienOPsAll(), nur mit left join statt inner join im ersten Teil
        /// </summary>
        /// <param name="nID_Gebiete"></param>
        /// <param name="nID_Chirurgen"></param>
        /// <param name="quelle"></param>
        /// <param name="dtFrom"></param>
        /// <param name="dtTo"></param>
        /// <returns></returns>
        public DataView GetRichtlinienOPs(int nID_Gebiete, int nID_Chirurgen, int quelle, DateTime? dtFrom, DateTime? dtTo)
        {
            //
            // Die automatisch zugeordneten
            //
            StringBuilder sb = new StringBuilder(@"
                SELECT DISTINCT
                    Richtlinien.LfdNummer,
                    Richtlinien.Richtzahl,
                    RichtlinienOpsKodes.[OPS-Kode] as RichtlinieOPSKode,
                    Richtlinien.UntBehMethode,
                    ChirurgenOperationen.[OPS-Kode] as OPSKode,
                    ChirurgenOperationen.[OPS-Text] as OPSText
                FROM 
                    Richtlinien LEFT JOIN (RichtlinienOpsKodes left JOIN ChirurgenOperationen ON ChirurgenOperationen.[OPS-Kode] like $$LIKE$$)
                    ON RichtlinienOpsKodes.ID_Richtlinien=Richtlinien.ID_Richtlinien
                where
                    ChirurgenOperationen.ID_Richtlinien is null and
                    Richtlinien.ID_Gebiete = @ID_Gebiete and 
                    ChirurgenOperationen.ID_Chirurgen=@ID_Chirurgen and 
                    ChirurgenOperationen.ID_OPFunktionen = " + (int)OP_FUNCTION.OP_FUNCTION_OP + @" 
                    $datefrom$ $dateto$  
                    $quelle$

                UNION ALL");

            //
            // Die Operationen, die fest einer Richtlinie zugeordnet sind
            //
            sb.Append(@"
                SELECT 
                    Richtlinien.LfdNummer,
                    Richtlinien.Richtzahl,");

            sb.Append("'");
            sb.Append(_businessLayerCommon.GetText("RichtlinienVergleichView", "zuordnung_manuell"));
            sb.Append("'");
            sb.Append(@"
                     as RichtlinieOPSKode,
                    Richtlinien.UntBehMethode,
                    ChirurgenOperationen.[OPS-Kode] as OPSKode,
                    ChirurgenOperationen.[OPS-Text] as OPSText
                FROM 
                    Richtlinien INNER JOIN ChirurgenOperationen ON Richtlinien.ID_Richtlinien = ChirurgenOperationen.ID_Richtlinien
                where
                    ChirurgenOperationen.ID_Richtlinien is not null and
                    Richtlinien.ID_Gebiete = @ID_Gebiete and 
                    ChirurgenOperationen.ID_Chirurgen=@ID_Chirurgen and 
                    ChirurgenOperationen.ID_OPFunktionen = " + (int)OP_FUNCTION.OP_FUNCTION_OP + @"   
                    $datefrom$ $dateto$  
                    $quelle$

                order by
                    1, 4
                    ");

            sb.Replace("$$LIKE$$", MakeConcat("RichtlinienOpsKodes.[OPS-Kode]", "'%'"));

            HandleDatum(dtFrom, dtTo, sb, "ChirurgenOperationen");

            ArrayList sqlParameter = new ArrayList();
            sqlParameter.Add(this.SqlParameter("@ID_Gebiete", nID_Gebiete));
            sqlParameter.Add(this.SqlParameter("@ID_Chirurgen", nID_Chirurgen));

            HandleQuelleMultiple(quelle, "", sb);

            return GetDataView(sb.ToString(), sqlParameter, "RichtlinienOPSCodes");
        }

        public long GetIstOperationenCount(int ID_ChirurgenLogin, DateTime? von, DateTime? bis, string ops, int quelle, int opFunktionen)
        {
            ArrayList sqlParameters = new ArrayList();

            StringBuilder sb = new StringBuilder(
                @"
                SELECT 
                    count(ID_ChirurgenOperationen)
                FROM
                    ChirurgenOperationen
                WHERE 
                    1=1
                    $datefrom$ $dateto$
                    $ops$
                    $quelle$
                    $opfunktionen$
                    $chirurgen$
                ");

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
            HandleSubselectID_Chirurgen(sqlParameters, "ChirurgenOperationen", ID_ChirurgenLogin, sb);

            return this.ExecuteScalar(sb.ToString(), sqlParameters);
        }

        /// <summary>
        /// Use this function if there is only one occurrence of $quelle$
        /// </summary>
        /// <param name="sqlParameters"></param>
        /// <param name="prefix"></param>
        /// <param name="quelle"></param>
        /// <param name="sb"></param>
        protected void HandleQuelleSingle(ArrayList sqlParameters, string prefix, int quelle, StringBuilder sb)
        {
            string sql = "";

            if (quelle == BusinessLayerCommon.OperationQuelleIntern || quelle == BusinessLayerCommon.OperationQuelleExtern)
            {
                if (string.IsNullOrEmpty(prefix))
                {
                    sql = string.Format("and (Quelle={0})", quelle);
                }
                else
                {
                    sql = string.Format("and ({0}.Quelle={1})", prefix, quelle);
                }
            }

            sb.Replace("$quelle$", sql);
        }

        protected void HandleOPFunktionen(ArrayList sqlParameters, string prefix, int ID_OPFunktionen, StringBuilder sb)
        {
            string sql = "";

            if (ID_OPFunktionen != BusinessLayerCommon.ID_Alle)
            {
                if (string.IsNullOrEmpty(prefix))
                {
                    sql = string.Format(" and (ID_OPFunktionen={0})", ID_OPFunktionen);
                }
                else
                {
                    sql = string.Format(" and ({0}.ID_OPFunktionen={1})", prefix, ID_OPFunktionen);
                }
            }
            sb.Replace("$opfunktionen$", sql);
        }

        /// <summary>
        /// Holt alle Operationen in einem vorgegebenen Zeitraum. In Access zusammengeklickt und dann
        /// per SQL-Anzeige kopiert. Keine Ahnung, warum das hier funktioniert.
        /// </summary>
        /// <param name="von"></param>
        /// <param name="bis"></param>
        /// <returns></returns>
        public DataView GetIstOperationen(int ID_Chirurgen, DateTime? von, DateTime? bis, string ops, int quelle, int ID_OPFunktionen)
        {
            StringBuilder sb = new StringBuilder(
                @"
                SELECT 
                    a.ID_ChirurgenOperationen,
                    a.Fallzahl,
                    a.Datum,
                    a.Zeit,
                    a.ZeitBis,
                    a.[OPS-Kode] as OPSKode,
                    a.[OPS-Text] as OPSText,
                    a.KlinischeErgebnisse,
                    b.Beschreibung,
                    c.Nachname,
                    c.Vorname,
                    d.Text as KlinischeErgebnisseTyp
                FROM
                    OPFunktionen b INNER JOIN ((ChirurgenOperationen a INNER JOIN KlinischeErgebnisseTypen d on a.ID_KlinischeErgebnisseTypen=d.ID_KlinischeErgebnisseTypen)
                                                INNER JOIN Chirurgen c ON c.ID_Chirurgen = a.ID_Chirurgen) 
                                 ON b.ID_OPFunktionen = a.ID_OPFunktionen
                WHERE 
                    1=1
                    $datefrom$ $dateto$
                    $ops$
                    $quelle$
                    $opfunktionen$
                    $chirurgen$
                ORDER BY
                    Datum desc, a.[OPS-Kode]
                ");

            HandleDatum(von, bis, sb);

            ArrayList sqlParameters = new ArrayList();

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

            HandleQuelleSingle(sqlParameters, "a", quelle, sb);
            HandleOPFunktionen(sqlParameters, "b", ID_OPFunktionen, sb);

            HandleSubselectID_Chirurgen(sqlParameters, "a", ID_Chirurgen, sb);

            return GetDataView(sb.ToString(), sqlParameters, "ChirurgenOperationen");
        }

        public DataView GetIstOperationenChirurgMitRichtlinien(int nID_Chirurgen, string ops, int nID_OPFunktionen, int quelle,
            bool sortOrderDescending, DateTime? von, DateTime? bis)
        {
            StringBuilder sb = new StringBuilder(
                @"
                SELECT 
                    ChirurgenOperationen.ID_ChirurgenOperationen,
                    ChirurgenOperationen.Datum,
                    ChirurgenOperationen.Fallzahl,
                    ChirurgenOperationen.[OPS-Kode] as Kode,
                    ChirurgenOperationen.[OPS-Text] as Name,
                    Richtlinien.LfdNummer,
                    Richtlinien.UntBehMethode,
                    Gebiete.Gebiet
                FROM
                    ChirurgenOperationen left join (Richtlinien left join Gebiete on Richtlinien.ID_Gebiete=Gebiete.ID_Gebiete)
                                                     on ChirurgenOperationen.ID_Richtlinien = Richtlinien.ID_Richtlinien
                WHERE 
                    ChirurgenOperationen.ID_Chirurgen=@ID_Chirurgen
                    $opfunktionen$
                    $ops$
                    $quelle$
                    $datefrom$ $dateto$
                ORDER BY
                    ChirurgenOperationen.Datum @sortOrder, 
                    ChirurgenOperationen.[OPS-Kode]
                ");

            HandleDatum(von, bis, sb, "ChirurgenOperationen");

            if (sortOrderDescending)
            {
                sb.Replace("@sortOrder", "DESC");
            }
            else
            {
                sb.Replace("@sortOrder", "");
            }
            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", nID_Chirurgen));

            HandleOPFunktionen(sqlParameters, "ChirurgenOperationen", nID_OPFunktionen, sb);

            if (string.IsNullOrEmpty(ops))
            {
                sb.Replace("$ops$", "");
            }
            else
            {
                string opsKodeLike = CreateLikeExpression(ops + "%");
                string opsTextLike = CreateLikeExpression("%" + ops + "%");

                sb.Replace("$ops$", "and ([OPS-Kode] like @opskode or [OPS-Text] like @opstext)");
                sqlParameters.Add(this.SqlParameter("@opskode", opsKodeLike));
                sqlParameters.Add(this.SqlParameter("@opstext", opsTextLike));
            }

            HandleQuelleSingle(sqlParameters, "ChirurgenOperationen", quelle, sb);

            return GetDataView(sb.ToString(), sqlParameters, "ChirurgenOperationen");
        }

        public DataView GetIstOperationenChirurg(int nID_Chirurgen, int ID_OPFunktionen, int quelle, DateTime? dtVon, DateTime? dtBis, bool sortOrderDescending)
        {
            StringBuilder sb = new StringBuilder(
                @"
                SELECT 
                    ID_ChirurgenOperationen,
                    Datum,
                    [OPS-Kode] as Kode,
                    [OPS-Text] as Name
                FROM
                    ChirurgenOperationen
                WHERE 
                    ID_Chirurgen=@ID_Chirurgen
                    $datefrom$ $dateto$
                    $opfunktionen$
                    $quelle$
                ORDER BY
                    Datum $sortOrder$, 
                    [OPS-Kode]
                ");

            if (sortOrderDescending)
            {
                sb.Replace("$sortOrder$", "DESC");
            }
            else
            {
                sb.Replace("$sortOrder$", "");
            }

            ArrayList sqlParameters = new ArrayList();

            HandleDatum(dtVon, dtBis, sb);
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", nID_Chirurgen));

            HandleOPFunktionen(sqlParameters, "", ID_OPFunktionen, sb);

            HandleQuelleSingle(sqlParameters, "", quelle, sb);

            return GetDataView(sb.ToString(), sqlParameters, "Chirurgen");
        }

        #region Config
        public string GetConfigValue(string key)
        {
            string value = "";

            DataRow row = GetConfig(key);

            if (row != null)
            {
                value = (string)row["Value"];
            }

            return value;
        }

        public DataView GetConfig()
        {
            string sql = @"SELECT ID_Config, [Key], [Value] from Config";

            return GetDataView(sql, null, "Config");

        }
        public int InsertConfig(DataRow oDataRow)
        {
            string sb = @"
                INSERT INTO Config
                    (
                        [Key],
                        [Value]
                    )
                    VALUES
                    (
                     @Key,
                     @Value
                    )
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@Key", oDataRow["Key"]));
            aSQLParameters.Add(this.SqlParameter("@Value", oDataRow["Value"]));

            return InsertRecord(sb, aSQLParameters, "Config");
        }
        public bool DeleteConfig(string key)
        {
            string strSQL = @"
                DELETE FROM
                    Config
                WHERE
                    [Key]=@Key
                ";

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(SqlParameter("@Key", key));

            int iEffectedRecords = this.ExecuteNonQuery(strSQL, aSQLParameters);

            return true;
        }

/*
        public void SaveConfig(string key, string value)
        {
            DataTable table = new DataTable();
            DataRow row = CreateDataRowConfig(table, key, value);

            DeleteConfig(key);
            InsertConfig(row);
        }
*/ 

        //
        // Save a setting. INSERT if missing, update if it exists.
        //
        public bool SaveConfig(string key, string value)
        {
            bool success = false;

            string sql = @"SELECT count(ID_Config)
                from 
                    Config 
                where 
                    [Key]=@Key
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@Key", key));

            long count = ExecuteScalar(sql, sqlParameters);

            if (count > 0)
            {
                //
                // Update value if key exists
                //
                sql = @"update 
                        Config
                    set 
                        [Value]=@Value
                    where 
                        [Key]=@Key";

                sqlParameters = new ArrayList();
                sqlParameters.Add(this.SqlParameter("@Value", value));
                sqlParameters.Add(this.SqlParameter("@Key", key));

                success = (1 == this.ExecuteNonQuery(sql, sqlParameters));
            }
            else
            {
                //
                // Insert key and value if key does not exist
                //
                sql = @"
                INSERT INTO Config
                    (
                        [Key],
                        [Value]
                    )
                    VALUES
                    (
                     @Key,
                     @Value
                    )
                ";

                sqlParameters = new ArrayList();
                sqlParameters.Add(this.SqlParameter("@Key", key));
                sqlParameters.Add(this.SqlParameter("@Value", value));

                int id = InsertRecord(sql, sqlParameters, "Config");
                if (id > 0)
                {
                    success = true;
                }
            }

            return success;
        }

        public bool SaveCustomerData(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                DeleteConfig((string)row["Key"]);
                InsertConfig(row);
            }
            return true;
        }

        public DataRow CreateDataRowConfig(DataTable dt, string key, string value)
        {
            DataRow dataRow;

            // Define the columns of the table.
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add(new DataColumn("ID_Config", typeof(int)));
                dt.Columns.Add(new DataColumn("Key", typeof(string)));
                dt.Columns.Add(new DataColumn("Value", typeof(string)));
            }

            dataRow = dt.NewRow();

            dataRow["Key"] = key;
            dataRow["Value"] = value;

            return dataRow;
        }
        #endregion

        /// <summary>
        /// Liefert alle verschiedenen Operationstypen eines Chirurgen in einem Zeitraum.
        ///
        /// Vorher: Für jeden OPS-Kode den Text holen, der am häufigsten vorkommt. Das wollten
        /// Lange-Berberat aus München so.
        ///
        /// Anstelle des Textes, der für einen OPSCode am meisten auftaucht, wird einfach irgendeiner genommen.
        /// Das geht mit max()
        /// 
        /// Das dauert nur noch 1 statt 10 Sekunden
        /// </summary>
        /// <param name="nID_Chirurgen"></param>
        /// <returns></returns>
        public DataView GetChirurgOperationenOverview(int nID_Chirurgen, int nID_OPFunktionen, int quelle,
            DateTime? von, DateTime? bis, int relevantPositions)
        {
            StringBuilder sb = new StringBuilder(
                @"
                select 
                    $$opscode$$ as OPSKode,
                    max([OPS-Text]) as OPSText,
                    count(ID_ChirurgenOperationen) as Anzahl
                from 
                    ChirurgenOperationen
                where 
                    ID_Chirurgen=@ID_Chirurgen 
                    $opfunktionen$
                    $datefrom$ $dateto$
                    $quelle$
                group by 
                    $$opscode$$
                order by 
                    $$opscode$$
                ");

            sb.Replace("$$opscode$$", MakeLeft("[OPS-Kode]", relevantPositions));

            HandleDatum(von, bis, sb);
            ArrayList sqlParameter = new ArrayList();
            sqlParameter.Add(this.SqlParameter("@ID_Chirurgen", nID_Chirurgen));
            HandleOPFunktionen(sqlParameter, "", nID_OPFunktionen, sb);
            HandleQuelleSingle(sqlParameter, "", quelle, sb);

            return GetDataView(sb.ToString(), sqlParameter, "ChirurgenOperationen");
        }

        public long GetOperationenCount(string filterOpsKode, string filterOpsText)
        {
            ArrayList parameters = null;

            string opsKode = CreateLikeExpression(filterOpsKode + "%");
            string opsText = CreateLikeExpression("%" + filterOpsText + "%");

            StringBuilder sb = new StringBuilder(
                @"
                SELECT 
                    COUNT(ID_Operationen)
                FROM
                    Operationen
                @@where
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

            return ExecuteScalar(sb.ToString(), parameters);
        }

        public long GetOperationenCount(string filterOpsKodeText)
        {
            ArrayList parameters = null;

            string opsKode = CreateLikeExpression(filterOpsKodeText + "%");
            string opsText = CreateLikeExpression("%" + filterOpsKodeText + "%");

            StringBuilder sb = new StringBuilder(
                @"
                SELECT 
                    COUNT(ID_Operationen)
                FROM
                    Operationen
                $where$
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

            return ExecuteScalar(sb.ToString(), parameters);
        }

        public void GetOperationen(System.Web.UI.WebControls.DataGrid lvOperationen, string filterOpsKode, string filterOpsText)
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

            DataView dv = GetDataView(sb.ToString(), parameters, "Operationen");

            lvOperationen.DataSource = dv;
            lvOperationen.DataBind();
        }

        public DataView GetOperationen()
        {
            string sql =
                @"
                SELECT 
                    ID_Operationen,
                    [OPS-Kode],
                    [OPS-Text]
                FROM
                    Operationen
                ";
            return GetDataView(sql, "Operationen");
        }

        public int GetCountSerialNumbers()
        {
            string sql = "SELECT count(*) FROM SerialNumbers";

            return ExecuteScalarInteger(sql, null);
        }

        #region Zeitraeume
        public DataView GetZeitraeume()
        {
            string sql =
                @"
                SELECT 
                    ID_Zeitraeume,
                    Von,
                    Bis
                FROM
                    Zeitraeume
                ";
            return GetDataView(sql, "Zeitraeume");
        }

        public bool UpdateZeitraum(DataRow row)
        {
            StringBuilder sb = new StringBuilder(@"
                UPDATE 
                    Zeitraeume
                SET
                    Von=@Von,
                    Bis=@Bis
                WHERE
                    ID_Zeitraeume=@ID_Zeitraeume
                ");

            sb.Replace("@Von", Object2DBDateString(row["Von"]));
            sb.Replace("@Bis", Object2DBDateString(row["Bis"]));

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Zeitraeume", row["ID_Zeitraeume"]));

            int iEffectedRecords = this.ExecuteNonQuery(sb.ToString(), sqlParameters);

            return (iEffectedRecords == 1);
        }

        public DataRow CreateDataRowZeitraeume()
        {
            DataTable dt = new DataTable("Zeitraeume");
            DataRow dataRow;

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("ID_Zeitraeume", typeof(int)));
            dt.Columns.Add(new DataColumn("Von", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Bis", typeof(DateTime)));

            dataRow = dt.NewRow();

            return dataRow;
        }

        public bool DeleteZeitraum(int ID_Zeitraeume)
        {
            string sb =
                @"
                DELETE FROM
                    Zeitraeume
                WHERE
                    ID_Zeitraeume=@ID_Zeitraeume
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Zeitraeume", ID_Zeitraeume));

            int iEffectedRecords = this.ExecuteNonQuery(sb, sqlParameters);

            return (iEffectedRecords == 1);
        }

        public int InsertZeitraum(DataRow row)
        {
            StringBuilder sb = new StringBuilder(
                @"
                INSERT INTO Zeitraeume
                    (
                    Von, 
                    Bis 
                    )
                    VALUES
                    (
                    @Von, 
                    @Bis
                    )
                ");

            sb.Replace("@Von", Object2DBDateString(row["Von"]));
            sb.Replace("@Bis", Object2DBDateString(row["Bis"]));

            ArrayList sqlParameters = new ArrayList();

            return this.InsertRecord(sb.ToString(), sqlParameters, "Zeitraeume");
        }

        public DataRow GetZeitraum(int ID_Zeitraeume)
        {
            string sb = @"
                SELECT 
                    ID_Zeitraeume,
                    Von,
                    Bis
                FROM 
                    Zeitraeume
                WHERE
                    ID_Zeitraeume=@ID_Zeitraeume
                    ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Zeitraeume", ID_Zeitraeume));

            return GetRecord(sb, sqlParameters, "Zeitraeume");
        }

        #endregion

        #region LogTable
        public int Write2Log(string sUser, string sAction, string sMessage)
        {
            StringBuilder sb = new StringBuilder(
                @"
                INSERT INTO LogTable 
                    (
                    [User],
                    [Action],
                    [Message],
                    [Timestamp]
                    )
                VALUES
                    (
                    @User,
                    @Action,
                    @Message,
                    @Timestamp
                    )
                ");

            sb.Replace("@Timestamp", TimestampNowSyntax());

            ArrayList aSQLParameters = new ArrayList();
            aSQLParameters.Add(this.SqlParameter("@User", sUser));
            aSQLParameters.Add(this.SqlParameter("@Action", sAction));
            aSQLParameters.Add(this.SqlParameter("@Message", sMessage));

            return this.InsertRecord(sb.ToString(), aSQLParameters, "LogTable");
        }

        public DataView GetLogTable(string strNumRecords, string strUser, string strVon, string strBis, string strAktion, string strMessage)
        {
            string sSQL =
                @"
                SELECT @@TOP@@
                    ID_LogTable,
                    [TimeStamp],
                    [User],
                    [Action],
                    Message
                FROM
                    LogTable
                @WHERE
                ORDER BY
                    [TimeStamp] DESC
                    @@LIMIT@@
                ";

            ArrayList arSqlParameter = new ArrayList();

            string strWhere = "WHERE 1=1";
            if (strNumRecords.Length > 0)
            {
                sSQL = HandleTopLimitStuff(sSQL, strNumRecords);
            }
            else
            {
                sSQL = sSQL.Replace("@@TOP@@", "");
                sSQL = sSQL.Replace("@@LIMIT@@", "");
            }
            if (strVon.Length > 0)
            {
                strWhere += " AND [TimeStamp] >= " + DateString2DBDateString(strVon);
            }
            if (strBis.Length > 0)
            {
                strWhere += " AND [TimeStamp] <= " + DateString2DBDateString(strBis);
            }
            if (strUser.Length > 0)
            {
                string s = base.CreateLikeExpression(strUser);
                strWhere += " AND [User] like @User";
                arSqlParameter.Add(this.SqlParameter("@User", "%" + s + "%"));
            }
            if (strMessage.Length > 0)
            {
                string s = base.CreateLikeExpression(strMessage);
                strWhere += " AND Message like @Message";
                arSqlParameter.Add(this.SqlParameter("@Message", "%" + s + "%"));
            }
            if (strAktion.Length > 0)
            {
                string s = base.CreateLikeExpression(strAktion);
                strWhere += " AND Action like @Action";
                arSqlParameter.Add(this.SqlParameter("@Action", "%" + s + "%"));
            }

            sSQL = sSQL.Replace("@WHERE", strWhere);

            DataView oDataView = this.GetDataView(sSQL, arSqlParameter, "LogTable");

            return oDataView;
        }
        public long GetLogTableCount()
        {
            string sql =
                @"
                SELECT 
                    count(ID_LogTable)
                FROM
                    LogTable
                ";

            return ExecuteScalar(sql);
        }
        public void DeleteLogTable()
        {
            ExecuteNonQuery("delete from LogTable");
        }

        public bool DeleteLogTable(int ID_LogTable)
        {
            string sql =
                @"
                DELETE FROM
                    LogTable
                WHERE
                    ID_LogTable=@ID_LogTable
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_LogTable", ID_LogTable));

            int numRecords = this.ExecuteNonQuery(sql, sqlParameters);

            return (numRecords == 1);

        }
        #endregion

        #region AkademischeAusbildungen
        public bool UpdateAkademischeAusbildung(DataRow row)
        {
            StringBuilder sb = new StringBuilder(@"
                UPDATE 
                    AkademischeAusbildungen
                SET
                    Beginn=@Beginn,
                    Ende=@Ende,
                    Organisation=@Organisation,
                    ID_AkademischeAusbildungTypen=@ID_AkademischeAusbildungTypen
                WHERE
                    ID_AkademischeAusbildungen=@ID_AkademischeAusbildungen
                ");

            sb.Replace("@Beginn", Object2DBDateString(row["Beginn"]));
            sb.Replace("@Ende", Object2DBDateString(row["Ende"]));

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@Organisation", (string)row["Organisation"]));
            sqlParameters.Add(this.SqlParameter("@ID_AkademischeAusbildungTypen", row["ID_AkademischeAusbildungTypen"]));
            sqlParameters.Add(this.SqlParameter("@ID_AkademischeAusbildungen", row["ID_AkademischeAusbildungen"]));

            int iEffectedRecords = this.ExecuteNonQuery(sb.ToString(), sqlParameters);

            return (iEffectedRecords == 1);
        }

        public DataRow CreateDataRowAkademischeAusbildungen()
        {
            DataTable dt = new DataTable("AkademischerAkademischerLebenslauf");
            DataRow dataRow;

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("ID_AkademischeAusbildungen", typeof(int)));
            dt.Columns.Add(new DataColumn("ID_AkademischeAusbildungTypen", typeof(int)));
            dt.Columns.Add(new DataColumn("ID_Chirurgen", typeof(int)));
            dt.Columns.Add(new DataColumn("Beginn", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Ende", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Organisation", typeof(string)));

            dataRow = dt.NewRow();

            dataRow["Beginn"] = DateTime.Now;
            dataRow["Organisation"] = "";

            return dataRow;
        }
        public DataRow GetAkademischeAusbildung(int ID_AkademischeAusbildungen)
        {
            string sb = @"
                SELECT 
                    ID_AkademischeAusbildungen,
                    ID_AkademischeAusbildungTypen,
                    ID_Chirurgen,
                    Beginn,
                    Ende,
                    Organisation
                FROM 
                    AkademischeAusbildungen
                WHERE
                    ID_AkademischeAusbildungen=@ID_AkademischeAusbildungen
                    ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_AkademischeAusbildungen", ID_AkademischeAusbildungen));

            return GetRecord(sb, sqlParameters, "AkademischeAusbildungen");
        }
        public DataView GetAkademischeAusbildungTypen()
        {
            string sb = @"
                SELECT 
                    ID_AkademischeAusbildungTypen,
                    [Text]
                FROM 
                    AkademischeAusbildungTypen
                    ";

            return GetDataView(sb, null, "AkademischeAusbildungTypen");
        }

        public DataView GetAkademischeAusbildungen(int ID_Chirurgen, int ID_AkademischeAusbildungTypen)
        {
            string sb = @"
                SELECT 
                    a.ID_AkademischeAusbildungen,
                    a.ID_AkademischeAusbildungTypen,
                    a.ID_Chirurgen,
                    a.Beginn,
                    a.Ende,
                    a.Organisation,
                    b.[Text]
                FROM 
                    AkademischeAusbildungen as a inner join 
                        AkademischeAusbildungTypen b on 
                        (a.ID_AkademischeAusbildungTypen = b.ID_AkademischeAusbildungTypen)
                WHERE
                    a.ID_Chirurgen=@ID_Chirurgen
                    and a.ID_AkademischeAusbildungTypen=@ID_AkademischeAusbildungTypen
                order by
                    a.Beginn desc
                    ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
            sqlParameters.Add(this.SqlParameter("@ID_AkademischeAusbildungTypen", ID_AkademischeAusbildungTypen));

            return GetDataView(sb, sqlParameters, "AkademischerLebenslauf");
        }
        public DataView GetAkademischeAusbildungen(int ID_Chirurgen)
        {
            string sb = @"
                SELECT 
                    a.ID_AkademischeAusbildungen,
                    a.ID_AkademischeAusbildungTypen,
                    a.ID_Chirurgen,
                    a.Beginn,
                    a.Ende,
                    a.Organisation,
                    b.[Text]
                FROM 
                    AkademischeAusbildungen as a inner join 
                        AkademischeAusbildungTypen b on 
                        (a.ID_AkademischeAusbildungTypen = b.ID_AkademischeAusbildungTypen)
                WHERE
                    a.ID_Chirurgen=@ID_Chirurgen
                order by
                    a.Beginn desc
                    ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", ID_Chirurgen));

            return GetDataView(sb, sqlParameters, "AkademischerLebenslauf");
        }

        public bool DeleteAkademischeAusbildung(int ID_AkademischeAusbildungen)
        {
            string sb =
                @"
                DELETE FROM
                    AkademischeAusbildungen
                WHERE
                    ID_AkademischeAusbildungen=@ID_AkademischeAusbildungen
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_AkademischeAusbildungen", ID_AkademischeAusbildungen));

            int iEffectedRecords = this.ExecuteNonQuery(sb, sqlParameters);

            return (iEffectedRecords == 1);
        }
        public int InsertAkademischeAusbildung(DataRow row)
        {
            StringBuilder sb = new StringBuilder(
                @"
                INSERT INTO AkademischeAusbildungen
                    (
                    ID_Chirurgen, 
                    ID_AkademischeAusbildungTypen, 
                    Beginn, 
                    Ende, 
                    Organisation
                    )
                    VALUES
                    (
                    @ID_Chirurgen, 
                    @ID_AkademischeAusbildungTypen, 
                    @Beginn, 
                    @Ende, 
                    @Organisation
                    )
                ");

            sb.Replace("@Beginn", Object2DBDateString(row["Beginn"]));
            sb.Replace("@Ende", Object2DBDateString(row["Ende"]));

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(this.SqlParameter("@ID_Chirurgen", row["ID_Chirurgen"]));
            sqlParameters.Add(this.SqlParameter("@ID_AkademischeAusbildungTypen", row["ID_AkademischeAusbildungTypen"]));
            sqlParameters.Add(this.SqlParameter("@Organisation", (string)row["Organisation"]));

            return this.InsertRecord(sb.ToString(), sqlParameters, "AkademischeAusbildungen");
        }
        #endregion
    }
}

