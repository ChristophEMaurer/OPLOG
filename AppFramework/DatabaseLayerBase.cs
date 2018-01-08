using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;
using System.Globalization;

using AppFramework.Debugging;
using Utility;

/*
 Data Source=TORCL;User Id=system;Password=cmaurer;
 Data Source=XE;User Id=system;Password=cmaurer;
 ORA-12154: TNS:could not resolve the connect identifier specified
 */

namespace AppFramework
{
    /// <summary>
    /// The database base class that allows access to different types of databases
    /// </summary>
    public abstract class DatabaseLayerBase
    {
        private BusinessLayerBase _businessLayerBase;
        private DatabaseImplementation _databaseImplementation;
        private IDbConnection _connection;
        private IDbConnection _connection2;
        private int _openStack;
        private string _connectionString;

        private DatabaseLayerBase() 
        { 
        }

        public DatabaseLayerBase(BusinessLayerBase businessLayerBase, DatabaseType databaseType, string connectionString) 
        {
            _businessLayerBase = businessLayerBase;
            _connectionString = connectionString;

            switch (databaseType)
            {
                case DatabaseType.MSAccess:
                    _databaseImplementation = new DatabaseImplementationAccess();
                    break;

                case DatabaseType.MSSqlServer:
                    _databaseImplementation = new DatabaseImplementationSqlServer();
                    break;
                
                case DatabaseType.MySql:
                    _databaseImplementation = new DatabaseImplementationMySql();
                    break;
                
                case DatabaseType.OracleXE:
                    _databaseImplementation = new DatabaseImplementationOracle();
                    break;
            }
        }

        public DatabaseType DatabaseType 
        { 
            get { return _databaseImplementation.DatabaseType; } 
        }

        public string DateTime2DBDateTimeString(object dateTime) 
        {
            return _databaseImplementation.DateTime2DBDateTimeString(dateTime);
        }

        public string DateTime2DBTimeString(object dateTime)
        {
            return _databaseImplementation.DateTime2DBTimeString(dateTime);
        }

        public string NullableDateTime2DBDateString(DateTime? dateTime)
        {
            return _databaseImplementation.NullableDateTime2DBDateString(dateTime);
        }

        public string NullableDateTime2DBDateTimeString(DateTime? dateTime)
        {
            return _databaseImplementation.NullableDateTime2DBDateTimeString(dateTime);
        }

        public string Object2DBDateString(object value)
        {
            return _databaseImplementation.Object2DBDateString(value);
        }

        public string TimestampNowSyntax()
        {
            return _databaseImplementation.TimestampNowSyntax();
        }

        public string MakeConcat(string value1, string value2)
        {
            return _databaseImplementation.MakeConcat(value1, value2);
        }

        public string MakeConcat(string value1, string value2, string value3)
        {
            return _databaseImplementation.MakeConcat(value1, value2, value3);
        }

        public string HandleTopLimitStuff(string sql, string numberRecords)
        {
            return _databaseImplementation.HandleTopLimitStuff(sql, numberRecords);
        }
        public void HandleTopLimitStuff(StringBuilder sb, string numberRecords)
        {
            _databaseImplementation.HandleTopLimitStuff(sb, numberRecords);
        }


        /// <summary>
        /// Convert a database column of type sqlserver-int (c#-Int32) to an int.
        /// Every database except Oracle : int id = (int)row["ID"]
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public int ConvertToInt32(object value)
        {
            return _databaseImplementation.ConvertToInt32(value);
        }
        public long ConvertToInt64(object value)
        {
            return _databaseImplementation.ConvertToInt64(value);
        }

        public string MakeLeft(string sql, int characters)
        {
            return _databaseImplementation.MakeLeft(sql, characters);
        }

        public string ConnectionString
        {
            get { return _connectionString; }
            protected set { _connectionString = value; }
        }

        public DbProviderFactory DataFactory
        {
            set { _databaseImplementation.DataFactory = value; }
            get { return _databaseImplementation.DataFactory; }
        }

        protected IDbConnection Connection
        {
            get { return _connection; }
        }

        protected IDbConnection Connection2
        {
            get { return _connection2; }
        }

        public string DateString2DBDateString(string date)
        {
            DateTime dt = Tools.InputTextDate2DateTime(date);
            return DateTime2DBDateTimeString(dt);
        }
        public string DateString2DBDateStringEnd(string date)
        {
            DateTime dt = Tools.InputTextDate2DateTimeEnd(date);
            return DateTime2DBDateTimeString(dt);
        }
        public string TimeString2DBTimeString(string time)
        {
            DateTime dt = Tools.InputTextTime2DateTime(time);
            return DateTime2DBTimeString(dt);
        }

        public string Int2NullableForeignKey(object value)
        {
            string returnValue;

            if (value == DBNull.Value)
            {
                returnValue = "null";
            }
            else
            {
                returnValue = value.ToString();
            }

            return returnValue;
        }
        public string CreateLikeExpression(string value)
        {
            return _databaseImplementation.CreateLikeExpression(value);
        }

        protected IDbCommand CreateCommand()
        {
            return DataFactory.CreateCommand();
        }

#if false
        public virtual void CompactAccessDB(string tempFileName)
        {
        }
#endif
        public string CleanSqlStatement(string sql)
        {
            return _databaseImplementation.CleanSqlStatement(sql);
        }

        protected bool Open(string connectionString)
        {
            bool success = false;

            if (_connection == null)
            {
                _connection = DataFactory.CreateConnection();
                _connection.ConnectionString = connectionString;
                try
                {
                    _businessLayerBase.DebugPrint(DebugLogging.DebugFlagSqlVerbose, "Database open");
                    _connection.Open();
                    success = (_connection.State == ConnectionState.Open);
                }
                catch (Exception exception)
                {
                    Write2ErrorLog(exception);
                }
            }

            return success;
        }

        protected bool Open()
        {
            bool success = true;

            if (_connection == null)
            {
                _connection = DataFactory.CreateConnection();
                try
                {
                    _connection.ConnectionString = _connectionString;
                    _businessLayerBase.DebugPrint(DebugLogging.DebugFlagSqlVerbose, "Database open");
                    _connection.Open();
                    _openStack = 1;
                    success = (_connection.State == ConnectionState.Open);

                    //"Attempted to read or write protected memory. This is often an indication that other memory is corrupt."}
                }
                catch (Exception e)
                {
                    _openStack = 0;
                    success = false;
                    Write2ErrorLog(e);
                }
            }
            else
            {
                _openStack++;
            }
            return success;
        }

        protected bool Open2()
        {
            bool success = true;

            if (_connection2 == null)
            {
                _connection2 = DataFactory.CreateConnection();
                try
                {
                    _connection2.ConnectionString = _connectionString;
                    _businessLayerBase.DebugPrint(DebugLogging.DebugFlagSqlVerbose, "Database connection2 open");
                    _connection2.Open();
                    success = (_connection2.State == ConnectionState.Open);
                }
                catch (Exception e)
                {
                    success = false;
                    Write2ErrorLog(e);
                }
            }
            return success;
        }

        protected void Close()
        {
            _openStack--;

            if (_openStack == 0)
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _businessLayerBase.DebugPrint(DebugLogging.DebugFlagSqlVerbose, "Database close");
                    _connection.Close();
                }
                _connection.Dispose();
                _connection = null;
            }
        }

        protected void Close2()
        {
            if (_connection2.State == ConnectionState.Open)
            {
                _businessLayerBase.DebugPrint(DebugLogging.DebugFlagSqlVerbose, "Database connection2 close");
                _connection2.Close();
            }
            _connection2.Dispose();
            _connection2 = null;
        }

        protected virtual void Write2ErrorLog(Exception exception)
        {
            _businessLayerBase.DisplayError(exception, null);
        }

        protected virtual void Write2ErrorLog(Exception exception, string message)
        {
            _businessLayerBase.DisplayError(exception, message);
        }

        protected virtual void Write2ErrorLog(string message)
        {
            _businessLayerBase.DisplayError(null, message);
        }

        public void MapSqlParameter2Command(IDbCommand command, ArrayList sqlParameters)
        {
            if (sqlParameters != null)
            {
                foreach (IDataParameter sqlParameter in sqlParameters)
                {
                    //
                    // do NOT log the value, or you will write all user passwords to the log file
                    //
                    _businessLayerBase.DebugPrint(DebugLogging.DebugFlagSqlVerbose, "Database sqlParameter '" + sqlParameter.ParameterName + "'");
                    command.Parameters.Add(sqlParameter);
                }
            }
        }

        public long ExecuteScalar(string sql)
        {
            return ExecuteScalar(sql, null);
        }

        public long ExecuteScalar(string sql, ArrayList sqlParameters)
        {
            long result = 0;

            if (this.Open())
            {
                try
                {
                    sql = CleanSqlStatement(sql);
                    IDbCommand commmand = _connection.CreateCommand();
                    commmand.CommandText = sql;
                    if (sqlParameters != null)
                    {
                        MapSqlParameter2Command(commmand, sqlParameters);
                    }

                    _businessLayerBase.DebugPrint(DebugLogging.DebugFlagSql, "SQL: [" + sql + "]");

                    object count = commmand.ExecuteScalar();
                    result = Convert.ToInt32(count, CultureInfo.InvariantCulture);

                    commmand.Dispose();
                    commmand = null;
                }
                catch (Exception exception)
                {
                    Write2ErrorLog(exception);
                }
                finally
                {
                    this.Close();
                }
            }
            else
            {
                Write2ErrorLog("Could not open connection");
            }

            return result;
        }

        public int ExecuteScalarInteger(string sql, ArrayList sqlParameters)
        {
            int result = 0;

            if (this.Open())
            {
                try
                {
                    sql = CleanSqlStatement(sql);
                    IDbCommand commmand = _connection.CreateCommand();
                    commmand.CommandText = sql;
                    if (sqlParameters != null)
                    {
                        MapSqlParameter2Command(commmand, sqlParameters);
                    }

                    _businessLayerBase.DebugPrint(DebugLogging.DebugFlagSql, "SQL: [" + sql + "]");

                    object count = commmand.ExecuteScalar();
                    result = Convert.ToInt32(count, CultureInfo.InvariantCulture);
                    commmand.Dispose();
                    commmand = null;
                }
                catch (Exception e)
                {
                    Write2ErrorLog(e);
                }
                finally
                {
                    this.Close();
                }
            }
            else
            {
                Write2ErrorLog("Could not open connection!");
            }

            return result;
        }

        protected int GetLastGeneratedId()
        {
            int ID_NewRecord = 0;
            string sql = "SELECT @@IDENTITY";

            try
            {
                IDbCommand command = _connection.CreateCommand();
                command.CommandText = sql;
                _businessLayerBase.DebugPrint(DebugLogging.DebugFlagSql, "SQL: [" + sql + "]");
                object value = command.ExecuteScalar();
                ID_NewRecord = int.Parse(value.ToString(), CultureInfo.InvariantCulture);
                command.Dispose();
                command = null;
            }
            catch (Exception exception)
            {
                Write2ErrorLog(exception);
            }
            return ID_NewRecord;
        }
        /// <summary>
        /// Execute an SQL Statement. Any error produces a message box.
        /// Return the number of affected rows.
        /// </summary>
        /// <param name="sSQL"></param>
        /// <param name="aSqlParameter"></param>
        /// <param name="bSuccess">Is set to false on error</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, ArrayList sqlParameters)
        {
            int effectedRows = 0;

            if (this.Open())
            {
                try
                {
                    sql = CleanSqlStatement(sql);
                    IDbCommand command = _connection.CreateCommand();
                    command.CommandText = sql;
                    if (sqlParameters != null)
                    {
                        MapSqlParameter2Command(command, sqlParameters);
                    }
                    _businessLayerBase.DebugPrint(DebugLogging.DebugFlagSql, "SQL: [" + sql + "]");
                    effectedRows = command.ExecuteNonQuery();
                    command.Dispose();
                    command = null;
                }
                catch (Exception e)
                {
                    Write2ErrorLog(e);
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

            return effectedRows;
        }

        /// <summary>
        /// Holt einen Datensatz. Es darf nur einer herauskommen, sonst stimmt etwas nicht.
        /// </summary>
        /// <param name="sSQL"></param>
        /// <param name="aSqlParameter"></param>
        /// <param name="sTable"></param>
        /// <returns>Wenn es genau einen Datensazt gibt, kommt dieser heraus,
        /// <br/>wenn mehr als einer herauskommt, wird eine Exception geworfen.
        /// <br/>Wenn keiner herauskommt, wird null zur�ckgegeben.
        /// </returns>
        public DataRow GetRecord(string sql, ArrayList sqlParameters, string tableName)
        {
            return GetRecord(sql, sqlParameters, tableName, false);
        }

        /// <summary>
        /// Holt einen Datensatz. Es darf nur einer herauskommen, sonst stimmt etwas nicht.
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="tableName">Tabelle</param>
        /// <returns>Wenn es genau einen Datensazt gibt, kommt dieser heraus,
        /// <br/>wenn mehr als einer herauskommt, wird eine Exception geworfen.
        /// <br/>Wenn keiner herauskommt, wird null zur�ckgegeben.
        /// </returns>
        public DataRow GetRecord(string sql, string tableName)
        {
            return GetRecord(sql, null, tableName, false);
        }

        /// <summary>
        /// Holt einen Datensatz. Wenn mehr als einer herauskommt und man returnFirst angibt, bekommt man den ersten, 
        /// ansonsten wird eine Exception geworfen.
        /// </summary>
        /// <param name="sSQL"></param>
        /// <param name="aSqlParameter"></param>
        /// <param name="sTable"></param>
        /// <returns>Wenn es genau einen Datensazt gibt, kommt dieser heraus,
        /// <br/>wenn mehr als einer herauskommt, und das nicht gew�nscht wird, wird eine Exception geworfen.
        /// <br/>Wenn keiner herauskommt, wird null zur�ckgegeben.
        /// </returns>
        public DataRow GetRecord(string sql, ArrayList sqlParameters, string tableName, bool returnFirst)
        {
            int count = 0;
            DataRow dataRow = null;

            if (this.Open())
            {
                try
                {
                    sql = CleanSqlStatement(sql);
                    IDbCommand command = _connection.CreateCommand();
                    command.CommandText = sql;
                    MapSqlParameter2Command(command, sqlParameters);
                    IDbDataAdapter dataAdapter = DataFactory.CreateDataAdapter();
                    dataAdapter.SelectCommand = command;
                    DataSet dataSet = new DataSet(tableName);
                    dataSet.Locale = CultureInfo.InvariantCulture;
                    _businessLayerBase.DebugPrint(DebugLogging.DebugFlagSql, "SQL: [" + sql + "]");
                    dataAdapter.Fill(dataSet);

                    count = dataSet.Tables[0].Rows.Count;
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        dataRow = dataSet.Tables[0].Rows[0];
                    }

                    dataSet.Dispose();
                    dataSet = null;
                    dataAdapter = null;
                }
                catch (Exception exception)
                {
                    Write2ErrorLog(exception);
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

            if (!returnFirst && count > 1)
            {
                // Mehr als ein Datensatz wurde gefunden. Das ist ganz schlecht. Im Nachhinein
                // kann man da nur eine Exception werfen.
                throw new MultipleRecordsException("Found more than one record\r" + sql);
            }

            return dataRow;
        }

        public DataView GetDataView(string sql, string tableName)
        {
            return GetDataView(sql, null, tableName);
        }

        public DataView GetDataView(string sql, ArrayList sqlParameters, string tableName)
        {
            DataView dataView = null;

            if (this.Open())
            {
                try
                {
                    sql = this.CleanSqlStatement(sql);
                    IDbCommand command = _connection.CreateCommand();
                    command.CommandText = sql;

                    if (sqlParameters != null)
                    {
                        MapSqlParameter2Command(command, sqlParameters);
                    }

                    IDbDataAdapter dataAdapter = DataFactory.CreateDataAdapter();
                    dataAdapter.SelectCommand = command;
                    DataSet dataSet = new DataSet(tableName);
                    dataSet.Locale = CultureInfo.InvariantCulture;
                    _businessLayerBase.DebugPrint(DebugLogging.DebugFlagSql, "SQL: [" + sql + "]");
                    dataAdapter.Fill(dataSet);

                    dataView = new DataView(dataSet.Tables[0]);

                    dataSet.Dispose();
                    dataSet = null;
                    dataAdapter = null;
                }
                catch (Exception exception)
                {
                    Write2ErrorLog(exception);
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
            return dataView;
        }

        public int InsertRecord(string sql, ArrayList parameters, string tableName)
        {
            int ID_NewRecord = -1;
            string cleanSql = CleanSqlStatement(sql);
            if (this.Open())								// ausdr�cklich f�r GetLastGeneratedId hier ge�ffnet!
            {
                if (this.ExecuteNonQuery(cleanSql, parameters) > 0)
                {
                    ID_NewRecord = this.GetLastGeneratedId();
                }
                else
                {
                    string message = "New record could not be inserted into table <@Table> !";
                    message = message.Replace("@Table", tableName.ToString());
                    Write2ErrorLog(message);
                }
                this.Close();
            }
            else
            {
                Write2ErrorLog("Could not open connection!");
            }

            return ID_NewRecord;
        }

        public int ExecuteNonQuery(string sql)
        {
            return ExecuteNonQuery(sql, null);
        }

        protected bool DeleteRecord(string sql, ArrayList sqlParameters, int id, string tableName)
        {
            bool success = true;
            if (this.ExecuteNonQuery(sql, sqlParameters) == 0)
            {
                success = false;
                string message = "Record <@ID> of table <@Table> could not be deleted!";
                message = message.Replace("@ID", id.ToString(CultureInfo.InvariantCulture));
                message = message.Replace("@Table", tableName);
                Write2ErrorLog(message);
            }

            return success;
        }
        #region Parameter
        public IDbDataParameter SqlParameterInt(string parameterName, int value)
        {
            // OleDB-Parameters beziehen sich auf die Position, nicht auf den Namen!!!
            // Reihenfolge ist wichtig!!!
            return _databaseImplementation.SqlParameterInt(parameterName, value);
        }

        public IDbDataParameter SqlParameter(string parameterName, int value)
        {
            // OleDB-Parameters beziehen sich auf die Position, nicht auf den Namen!!!
            // Reihenfolge ist wichtig!!!
            return _databaseImplementation.SqlParameter(parameterName, value);
        }

        public IDbDataParameter SqlParameter(string parameterName, string value)
        {
            // OleDB-Parameters beziehen sich auf die Position, nicht auf den Namen!!!
            // Reihenfolge ist wichtig!!!
            return _databaseImplementation.SqlParameter(parameterName, value);
        }

        public IDbDataParameter SqlParameter(string parameterName, object value)
        {
            return _databaseImplementation.SqlParameter(parameterName, value);
        }

        protected IDbDataParameter SqlParameter(string parameterName, byte[] arValue)
        {
            // OleDB-Parameters beziehen sich auf die Position, nicht auf den Namen!!!
            // Reihenfolge ist wichtig!!!
            return _databaseImplementation.SqlParameter(parameterName, arValue);
        }

        public IDbDataParameter SqlParameterInt(string parameterName, object value)
        {
            // OleDB-Parameters beziehen sich auf die Position, nicht auf den Namen!!!
            // Reihenfolge ist wichtig!!!
            //Errorerrorerror
            return _databaseImplementation.SqlParameterInt(parameterName, value);
        }
 
        #endregion

        public virtual bool TestDatabaseConnection()
        {
            bool success = Open();

            Close();

            return success;
        }

        public DataView TestDatabaseConnection(string testDatabaseConnectionString, string sql)
        {
            DataView dataView = null;

            IDbConnection connection = null;
            IDbCommand command = null;
            DataSet dataSet = null;
            IDbDataAdapter dataAdapter = null;
            try
            {
                connection = DataFactory.CreateConnection();
                if (testDatabaseConnectionString != null)
                {
                    connection.ConnectionString = testDatabaseConnectionString;
                }
                else
                {
                    connection.ConnectionString = _connectionString;
                }
                _businessLayerBase.DebugPrint(DebugLogging.DebugFlagSqlVerbose, "Database open");
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = sql;
                dataAdapter = DataFactory.CreateDataAdapter();
                dataAdapter.SelectCommand = command;
                dataSet = new DataSet("Test");
                dataSet.Locale = CultureInfo.InvariantCulture;
                _businessLayerBase.DebugPrint(DebugLogging.DebugFlagSql, "SQL: [" + sql + "]");
                dataAdapter.Fill(dataSet);

                dataView = new DataView(dataSet.Tables[0]);
            }
            catch
            {
            }
            finally
            {
                if (dataSet != null)
                {
                    dataSet.Dispose();
                }
                dataAdapter = null;
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    _businessLayerBase.DebugPrint(DebugLogging.DebugFlagSqlVerbose, "Database close");
                    connection.Close();
                    connection.Dispose();
                    connection = null;
                }
            }

            return dataView;
        }
        public bool OpenForImport()
        {
            return Open();
        }
        public void CloseForImport()
        {
            Close();
        }

        /// <summary>
        /// Return the value for a key from table Config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public DataRow GetConfig(string key)
        {
            string sb =
                @"
                SELECT 
                    [Value]
                FROM 
                    Config
                WHERE
                    [Key]=@Key
                ";

            ArrayList arSqlParameter = new ArrayList();

            arSqlParameter.Add(SqlParameter("@Key", key));

            return this.GetRecord(sb, arSqlParameter, "Config");
        }


        public static void V19InsertSecRight(DbCommand command, string name, string description)
        {
            string sql = string.Format(CultureInfo.InvariantCulture, "insert into SecRights (Name, Description) values ('{0}', '{1}')", name, description);
            command.CommandText = sql;
            command.ExecuteNonQuery();
        }

        public static void V19InsertRights(DbCommand command)
        {
            // Alle Rechte anlegen
            V19InsertSecRight(command, "KommentareView.cmdCommentNew", "Schaltfl�che: Einsch�tzungen und Empfehlungen > Neue Einsch�tzung");
            V19InsertSecRight(command, "KommentareView.cmdAllComments", "Schaltfl�che: Einsch�tzungen und Empfehlungen > Alle Einsch�tzungen f�r alle Operateure anzeigen");
            V19InsertSecRight(command, "cmd.viewAllDocs", "Befehl: Alle Dokumente ansehen, die in Bearbeitung sind");
            V19InsertSecRight(command, "ChirurgenView.radInaktiv", "Schaltfl�che: Operateur ausw�hlen > Inaktive");
            V19InsertSecRight(command, "ChirurgenView.llExclude", "Schaltfl�che: Operateur ausw�hlen > ausgew�hlte �rzte in Ausschlussliste einf�gen");

            // Datei
            V19InsertSecRight(command, "PrintIstOperationen.edit", "Men�: Datei > Ausgef�hrte Prozeduren drucken");

            // Offizielle Dokumente
            V19InsertSecRight(command, "RichtlinienView.edit", "Men�: Offizielle Dokumente > Weiterbildungsrichtlinien");
            V19InsertSecRight(command, "OperationenKatalogView.edit", "Men�: Offizielle Dokumente > OPS-Katalog");
            V19InsertSecRight(command, "DokumenteView.edit", "Men�: Offizielle Dokumente > �rztekammer-Logb�cher");
            V19InsertSecRight(command, "GebieteView.edit", "Men�: Offizielle Dokumente > Facharztgebiete");

            // Verwaltung
            V19InsertSecRight(command, "ChirurgenFunktionenView.edit", "Men�: Verwaltung > Dienststellungen");
            V19InsertSecRight(command, "NotizTypenView.edit", "Men�: Verwaltung > Vermerkarten");
            V19InsertSecRight(command, "AkademischeAusbildungTypenView.edit", "Men�: Verwaltung > Akademischer Lebenslauf");
            V19InsertSecRight(command, "DateiTypenView.edit", "Men�: Verwaltung > Eigene Dateiarten bearbeiten");
            V19InsertSecRight(command, "DateienView.edit", "Men�: Verwaltung > Eigene Dateien bearbeiten");
            V19InsertSecRight(command, "AbteilungenView.edit", "Men�: Verwaltung > Abteilungen");
            V19InsertSecRight(command, "AbteilungenChirurgenView.edit", "Men�: Verwaltung > Zuordnungen von Chirurgen zu Abteilungen");
            V19InsertSecRight(command, "WeiterbilderChirurgenView.edit", "Men�: Verwaltung > Zuordnungen von Chirurgen zu Weiterbildern");
            V19InsertSecRight(command, "mn.Chirurg", "Men�: Verwaltung > Operateurdaten");
            V19InsertSecRight(command, "mn.ChirurgNew", "Men�: Verwaltung > Operateurdaten > Neu anlegen");
            V19InsertSecRight(command, "mn.ChirurgEdit", "Men�: Verwaltung > Operateurdaten > Bearbeiten");
            V19InsertSecRight(command, "mn.ChirurgDelete", "Men�: Verwaltung > Operateurdaten > L�schen");
            V19InsertSecRight(command, "ChirurgView.chkWeiterbilder", "Schaltfl�che: Verwaltung > Operateur bearbeiten > Checkbox Weiterbilder");

            // Bearbeiten
            V19InsertSecRight(command, "OperationenEditView.edit", "Men�: Bearbeiten > Prozeduren bearbeiten");
            V19InsertSecRight(command, "KommentareView.edit", "Men�: Bearbeiten > Einsch�tzungen und Empfehlungen");
            V19InsertSecRight(command, "NotizenView.edit", "Men�: Bearbeiten > Vermerke");
            V19InsertSecRight(command, "AkademischeAusbildungView.edit", "Men�: Bearbeiten > Akademischer Lebenslauf");
            V19InsertSecRight(command, "PlanOperationenView.edit", "Men�: Bearbeiten > Prozeduren vereinbaren");
            V19InsertSecRight(command, "ChirurgDokumenteView.edit", "Men�: Bearbeiten > �rztekammer - Logb�cher editieren");
            V19InsertSecRight(command, "RichtlinienOpsKodeUnassignedView.edit", "Men�: Bearbeiten > Zuordnung von  Prozeduren zu Richtlinien");
            V19InsertSecRight(command, "ChirurgenRichtlinienView.edit", "Men�: Bearbeiten > Extern erf�llte Richtzahlen nachtragen...");

            // Auswertungen
            V19InsertSecRight(command, "OperationenView.edit", "Men�: Auswertungen > Liste aller durchgef�hrten Prozeduren");
            V19InsertSecRight(command, "OperationenZeitenVergleichView.edit", "Men�: Auswertungen > Verbrachte Zeit im OP als Operateur/1. Assistent");
            V19InsertSecRight(command, "OPDauerFortschrittView.edit", "Men�: Auswertungen > Dauer eines Eingriffes");
            V19InsertSecRight(command, "ChirurgOperationenView.edit", "Men�: Auswertungen > Ausgef�hrte Prozeduren: Einzel�bersicht");
            V19InsertSecRight(command, "GesamtOperationenView.edit", "Men�: Auswertungen > Ausgef�hrte Prozeduren: Gesamt�bersicht");
            V19InsertSecRight(command, "PlanOperationVergleichIstView.edit", "Men�: Auswertungen > Verteilung der Prozeduren nach Operateur");
            V19InsertSecRight(command, "PlanOperationVergleichView.edit", "Men�: Auswertungen > Vereinbarte Soll/Ist Prozeduren");
            V19InsertSecRight(command, "RichtlinienVergleichView.edit", "Men�: Auswertungen > Weiterbildungsstand gem�� Weiterbildungsrichtlinien: Einzel�bersicht");
            V19InsertSecRight(command, "RichtlinienVergleichView.cmdAssignOPSRichtlinie", "Schaltfl�che: Auswertungen > Weiterbildungsstand gem�� Weiterbildungsrichtlinien: Einzel�bersicht > Zuordnung hinzuf�gen");
            V19InsertSecRight(command, "RichtlinienVergleichOverviewView.edit", "Men�: Auswertungen > Weiterbildungsstand gem�� Weiterbildungsrichtlinien: Gesamt�bersicht");
            V19InsertSecRight(command, "KlinischeErgebnisseView.edit", "Men�: Auswertungen > Klinische Ergebnisse");

            // Extras
            V19InsertSecRight(command, "mn.Import", "Men�: Extras > Datenimport");
            V19InsertSecRight(command, "ImportChirurgenExcludeView.edit", "Men�: Extras > Datenimport > Ausgef�hrte Prozeduren importieren - Ausschl�sse");
            V19InsertSecRight(command, "OperationenImportView.edit", "Men�: Extras > Datenimport > Ausgef�hrte Prozeduren importieren");
            V19InsertSecRight(command, "mn.ImportAutoImport", "Men�: Extras > Datenimport > Automatischen Datenimport durchf�hren");
            V19InsertSecRight(command, "cmd.ImportRichtlinien", "Men�: Extras > Datenimport > Richtlinien importieren Assistent");
            V19InsertSecRight(command, "cmd.ImportZuordnungen", "Men�: Extras > Datenimport > Zuordnungen von OPS-Kodes zu Richtlinien importieren Assistent");
            V19InsertSecRight(command, "cmd.ImportChirurg", "Men�: Extras > Datenimport > Chirurg importieren");
            V19InsertSecRight(command, "ImportOPSWizard.edit", "Men�: Extras > Datenimport > OPS-Katalog importieren");

            V19InsertSecRight(command, "mn.Export", "Men�: Extras > Datenexport");
            V19InsertSecRight(command, "cmd.ExportRichtlinien", "Men�: Extras > Datenexport > Richtlinien exportieren Assistent");
            V19InsertSecRight(command, "cmd.ExportZuordnungen", "Men�: Extras > Datenexport > Zuordnungen von OPS-Kodes zu Richtlinien exportieren Assistent");
            V19InsertSecRight(command, "cmd.ExportChirurg", "Men�: Extras > Datenexport > Chirurg exportieren");

            V19InsertSecRight(command, "UserSetPasswordView.edit", "Men�: Extras > Kennwort eines anderen Benutzers festlegen");
            V19InsertSecRight(command, "LogView.edit", "Men�: Extras > �nderungs-Historie");
            V19InsertSecRight(command, "LogView.cmdDelete", "Schaltfl�che: Extras > �nderungshistorie > Alle Eintr�ge l�schen");
            V19InsertSecRight(command, "SerialNumbersView.edit", "Men�: Extras > Seriennummern verwalten");
            V19InsertSecRight(command, "mn.SerialNumbersWebshop", "Men�: Extras > Seriennummern im Internet-Webshop bestellen");
            V19InsertSecRight(command, "mn.AutoUpdateInternet", "Men�: Extras > Auf neue Programmversion aus dem Internet �berpr�fen");
            V19InsertSecRight(command, "mn.AutoUpdateFolder", "Men�: Extras > Auf neue Programmversion aus einem Verzeichnis �berpr�fen");
            V19InsertSecRight(command, "CopyWWWProgramUpdateFilesView.edit", "Men�: Extras > Aktuelle Programmversion aus dem Internet in ein lokales Verzeichnis kopieren");
            V19InsertSecRight(command, "CopyWWWProgramUpdateFilesView.cmdVerzeichnis", "Schaltfl�che: Extras > Aktuelle Programmversion aus dem Internet in ein lokales Verzeichnis kopieren > Verzeichnis ausw�hlen");
            V19InsertSecRight(command, "CopyWWWProgramUpdateFilesView.cmdCopy", "Schaltfl�che: Extras > Aktuelle Programmversion aus dem Internet in ein lokales Verzeichnis kopieren > Kopieren");
            V19InsertSecRight(command, "OptionsView.edit", "Men�: Extras > Optionen");
            V19InsertSecRight(command, "OptionsView.chkOpenCommentMsg", "Schaltfl�che: Extras > Optionen > Sonstiges > Kommentare/Stellungnahme");
            V19InsertSecRight(command, "SecGroupsView.edit", "Men�: Extras > Rollen");
            V19InsertSecRight(command, "SecGroupsChirurgenView.edit", "Men�: Extras > Zuordnungen von Benutzern zu Rollen");
            V19InsertSecRight(command, "SecGroupsSecRightsView.edit", "Men�: Extras > Zuordnungen von Rechten zu Rollen");
            V19InsertSecRight(command, "SecUserOverviewView.edit", "Men�: Extras > Benutzer/Abteilungen/Rollen/Rechte - �bersicht");

            // Spezielle Rechte
            V19InsertSecRight(command, "select.surgeons.all", "Daten: Zugriff auf die Daten von allen Chirurgen.");
            V19InsertSecRight(command, "select.surgeons.abteilung", "Daten: Zugriff auf die Daten aller Chirurgen aus allen Abteilungen, denen der Benutzer angeh�rt.");

            // Buttons
            V19InsertSecRight(command, "ChirurgOperationenView.cmdNew", "Schaltfl�che: Auswertungen > Ausgef�hrte Prozeduren: Einzel�bersicht > Neue Prozedur anlegen");
            V19InsertSecRight(command, "ChirurgOperationenView.cmdDelete", "Schaltfl�che: Auswertungen > Ausgef�hrte Prozeduren: Einzel�bersicht > Prozedur l�schen");
            V19InsertSecRight(command, "ChirurgOperationenView.cmdAssignRichtlinie", "Schaltfl�che: Auswertungen > Ausgef�hrte Prozeduren: Einzel�bersicht > Richtlinie zuordnen");
            V19InsertSecRight(command, "ChirurgOperationenView.cmdRemoveRichtlinie", "Schaltfl�che: Auswertungen > Ausgef�hrte Prozeduren: Einzel�bersicht > Richtlinie entfernen");

            V19InsertSecRight(command, "RichtlinienVergleichView.cmdAssignRichtlinie", "Schaltfl�che: Auswertungen > Weiterbildungsstand gem�� Weiterbildungsrichtlinien: Einzel�bersicht > Richtlinie zuordnen");
            V19InsertSecRight(command, "OptionsView.tabProxy", "Registerkarte: Extras > Optionen > Proxy Server");
            V19InsertSecRight(command, "OptionsView.tabUpdate", "Registerkarte: Extras > Optionen > Programm Update");
            V19InsertSecRight(command, "OptionsView.tabImport", "Registerkarte: Extras > Optionen > Automatischer Prozedurenimport");
            V19InsertSecRight(command, "OptionsView.tabSerialnumbers", "Registerkarte: Extras > Optionen > Seriennummern");
            V19InsertSecRight(command, "OptionsView.tabPrint", "Registerkarte: Extras > Optionen > Drucken");
            V19InsertSecRight(command, "OptionsView.tabSonstiges", "Registerkarte: Extras > Optionen > Sonstiges");
            V19InsertSecRight(command, "OptionsView.txtStellenOPSKode", "Textfeld: Extras > Optionen > Sonstiges > Relevante Stellen OPSKode");
        }
    }
}
