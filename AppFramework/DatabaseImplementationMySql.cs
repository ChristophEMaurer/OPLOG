using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace AppFramework
{
	/// <summary>
	/// Folgendes ist bei der MySql Datenbank anders:
    /// <br/>SQL-Abfragen mit Parametern brauchen ein ? statt einem @
    /// <br/>Reservierte Feldnamen müssen statt in eckigen Klammern [value] 
    /// mit dem accent grave escaped werden `value`
    /// <br/>DateTime: '0000-00-00 00:00:00'
    /// DATE '0000-00-00' 
    /// TIMESTAMP '0000-00-00 00:00:00' 
    /// TIME '00:00:00' 
    /// YEAR 0000 
    /// The DATETIME type is used when you need values that contain both date and time information.
    /// MySQL retrieves and displays DATETIME values in 'YYYY-MM-DD HH:MM:SS' format. 
    /// The supported range is '1000-01-01 00:00:00' to '9999-12-31 23:59:59'. 
    /// <br/>Ein DateTime feld kann kein default Wert von Now() haben, man muss immer selber NOW() einfügen.
    /// <br/>Nicht top n sondern limit n wie bei SQLite
    /// <br/>count(...) liefert long
	/// </summary>
    public class DatabaseImplementationMySql : DatabaseImplementation
	{
        public DatabaseImplementationMySql()
		{
            DataFactory = DbProviderFactories.GetFactory("MySql.Data.MySqlClient");
        }

        public override string MakeConcat(string s1, string s2)
        {
            return string.Format("concat({0}, {1})", s1, s2);
        }
        public override string MakeConcat(string s1, string s2, string s3)
        {
            return string.Format("concat({0}, concat({1}, {2}))", s1, s2, s3);
        }

        public override string TimestampNowSyntax()
        {
            return "now()";
        }

        public override string NullableDateTime2DBDateString(DateTime? dt)
        {
            string s;

            if (dt.HasValue)
            {
                s = string.Format("STR_TO_DATE('{0:00}.{1:00}.{2:0000}', '%d.%m.%Y')", dt.Value.Day, dt.Value.Month, dt.Value.Year);
            }
            else
            {
                s = "null";
            }

            return s;
        }

        public override string Object2DBDateString(object o)
        {
            string s;

            if (o == null)
            {
                DateTime? dt = null;
                s = NullableDateTime2DBDateString(dt);
            }
            else if (o == DBNull.Value)
            {
                DateTime? dt = null;
                s = NullableDateTime2DBDateString(dt);
            }
            else
            {
                DateTime? dt = (DateTime?)o;
                s = NullableDateTime2DBDateString(dt);
            }

            return s;
        }

        public override string DateTime2DBDateTimeString(object oDateTime)
        {
            string sReturn;

            if (oDateTime == DBNull.Value)
            {
                sReturn = "null";
            }
            else
            {
                DateTime dt = (DateTime)oDateTime;
                sReturn = NullableDateTime2DBDateTimeString(dt);
            }

            return sReturn;
        }

        public override string NullableDateTime2DBDateTimeString(DateTime? oDateTime)
        {
            string sReturn;

            if (oDateTime.HasValue)
            {
                sReturn = string.Format("STR_TO_DATE('{0:00}.{1:00}.{2:0000} {3:00}:{4:00}:{5:00}', '%d.%m.%Y %T')",
                    oDateTime.Value.Day, oDateTime.Value.Month, oDateTime.Value.Year,
                    oDateTime.Value.Hour, oDateTime.Value.Minute, oDateTime.Value.Second
                    );
            }
            else
            {
                sReturn = "null";
            }

            return sReturn;
        }

        public override string DateTime2DBTimeString(object dateTime)
        {
            // nicht getestet
            string sReturn;

            if (dateTime == DBNull.Value)
            {
                sReturn = "null";
            }
            else
            {
                DateTime dt = (DateTime)dateTime;

                sReturn = string.Format("STR_TO_DATE('01.01.1753 {0:00}:{1:00}:{2:00}', '%d.%m.%Y %T')",
                    dt.Hour, dt.Minute, dt.Second
                    );
            }

            return sReturn;
        }

        // reservierte Identifier müssen mit back tick gequoted werden
        // ParameterDirection benötigen ein ? statt einem @
        public override string CleanSqlStatement(string sql)
        {
            string s = sql.Replace('@', '?');


            // [hallo] > `hallo`
            s = Regex.Replace(s, @"\[", @"`");
            s = Regex.Replace(s, @"\]", @"`");


            return base.CleanSqlStatement(s);
        }

        public override IDbDataParameter SqlParameter(string sParameterName, string sValue)
        {
            // OleDB-Parameters beziehen sich auf die Position, nicht auf den Namen!!!
            // Reihenfolge ist wichtig!!!
            IDbDataParameter oSQLParameter = DataFactory.CreateParameter();
            oSQLParameter.ParameterName = sParameterName.Replace('@', '?');
            oSQLParameter.DbType = DbType.String;
            oSQLParameter.Value = sValue;

            return oSQLParameter;
        }
        public override IDbDataParameter SqlParameter(string sParameterName, int iValue)
        {
            // OleDB-Parameters beziehen sich auf die Position, nicht auf den Namen!!!
            // Reihenfolge ist wichtig!!!
            IDbDataParameter oSQLParameter = DataFactory.CreateParameter();
            oSQLParameter.ParameterName = sParameterName.Replace('@', '?');
            oSQLParameter.DbType = DbType.Int32;
            oSQLParameter.Value = iValue;
            return oSQLParameter;
        }
        public override IDbDataParameter SqlParameter(string sParameterName, object value)
        {
            IDbDataParameter oSQLParameter = DataFactory.CreateParameter();
            oSQLParameter.ParameterName = sParameterName.Replace('@', '?');

            if (value is Int32)
            {
                oSQLParameter.DbType = DbType.Int32;
            }
            else if (value is string)
            {
                oSQLParameter.DbType = DbType.String;
            }
            else if (value is byte[])
            {
                // ChirurgenDokumente.Blob
                oSQLParameter.DbType = DbType.Binary;
            }
            else
            {
                // bei DBNull.Value
                oSQLParameter.DbType = DbType.Object;
            }

            oSQLParameter.Value = value;
            return oSQLParameter;
        }

        protected override IDbDataParameter SqlParameter(string sParameterName, byte[] arValue)
        {
            // OleDB-Parameters beziehen sich auf die Position, nicht auf den Namen!!!
            // Reihenfolge ist wichtig!!!
            IDbDataParameter oSQLParameter = DataFactory.CreateParameter();
            oSQLParameter.ParameterName = sParameterName.Replace('@', '?');
            oSQLParameter.DbType = DbType.Binary;
            oSQLParameter.Value = arValue;
            return oSQLParameter;
        }

        public override IDbDataParameter SqlParameterInt(string sParameterName, object o)
        {
            // OleDB-Parameters beziehen sich auf die Position, nicht auf den Namen!!!
            // Reihenfolge ist wichtig!!!
            IDbDataParameter oSQLParameter = DataFactory.CreateParameter();
            oSQLParameter.ParameterName = sParameterName.Replace('@', '?');
            oSQLParameter.DbType = DbType.Int32;
            if (o == DBNull.Value)
            {
                oSQLParameter.Value = DBNull.Value;
            }
            else
            {
                oSQLParameter.Value = ConvertToInt32(o);
            }
            return oSQLParameter;
        }

        public override string HandleTopLimitStuff(string sql, string numRecords)
        {
            string s = sql.Replace("@@LIMIT@@", "LIMIT " + numRecords);
            s = s.Replace("@@TOP@@", "");

            return s;
        }
        public override void HandleTopLimitStuff(StringBuilder sb, string numRecords)
        {
            sb.Replace("@@LIMIT@@", "LIMIT " + numRecords);
            sb.Replace("@@TOP@@", "");
        }

        public override DatabaseType DatabaseType 
        {
            get { return DatabaseType.MySql;  } 
        }
    }
}
