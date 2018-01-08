using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace AppFramework
{
	/// <summary>
    /// Zusammenfassung für DatabaseLayerSqlServer.
	/// </summary>
    public class DatabaseImplementationSqlServer : DatabaseImplementation
	{
        public DatabaseImplementationSqlServer()
		{
            DataFactory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        }

        public override string MakeConcat(string s1, string s2)
        {
            return string.Format("{0} + {1}", s1, s2);
        }
        public override string MakeConcat(string s1, string s2, string s3)
        {
            return string.Format("{0} + {1} + {2}", s1, s2, s3);
        }


        public override string HandleTopLimitStuff(string sql, string numRecords)
        {
            string s = sql.Replace("@@TOP@@", "TOP " + numRecords);
            s = s.Replace("@@LIMIT@@", "");

            return s;
        }
        public override void HandleTopLimitStuff(StringBuilder sb, string numRecords)
        {
            sb.Replace("@@TOP@@", "TOP " + numRecords);
            sb.Replace("@@LIMIT@@", "");
        }

        public override string TimestampNowSyntax()
        {
            return "getdate()";
        }

        public override string NullableDateTime2DBDateString(DateTime? dt)
        {
            string s;

            if (dt.HasValue)
            {
                // Mit Jahrhundert (yyyy) 104 Deutsch dd.mm.yy
                s = string.Format("convert(DateTime, '{0:00}.{1:00}.{2:0000}', 104)", dt.Value.Day, dt.Value.Month, dt.Value.Year);
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
                // 20 oder 120 (2) ODBC kanonisch yyyy-mm-dd hh:mi:ss(24h)
                // 2 Die Standardwerte (style 0 oder 100, 9 oder 109, 13 oder 113, 20 oder 120 und 21 oder 121) geben immer das Jahrhundert zurück (yyyy).
                sReturn = NullableDateTime2DBDateTimeString(dt);
            }

            return sReturn;
        }
        public override string NullableDateTime2DBDateTimeString(DateTime? oDateTime)
        {
            string sReturn;

            if (oDateTime.HasValue)
            {
                // 20 oder 120 (2) ODBC kanonisch yyyy-mm-dd hh:mi:ss(24h)
                // 2 Die Standardwerte (style 0 oder 100, 9 oder 109, 13 oder 113, 20 oder 120 und 21 oder 121) geben immer das Jahrhundert zurück (yyyy).
                sReturn = string.Format("convert(datetime, '{0:0000}.{1:00}.{2:00} {3:00}:{4:00}:{5:00}', 120)",
                        oDateTime.Value.Year, oDateTime.Value.Month, oDateTime.Value.Day,
                        oDateTime.Value.Hour, oDateTime.Value.Minute, oDateTime.Value.Second
                        );
            }
            else
            {
                sReturn = "null";
            }

            return sReturn;
        }
        public override string DateTime2DBTimeString(object oDateTime)
        {
            // nicht getestet
            string sReturn;

            if (oDateTime == DBNull.Value)
            {
                sReturn = "null";
            }
            else
            {
                // Mit Jahrhundert (yyyy): 8 108 hh:mi:ss
                DateTime dt = (DateTime)oDateTime;
                sReturn = string.Format("convert(DateTime, '{0:00}:{1:00}:{2:00}', 108)", dt.Hour, dt.Minute, dt.Second);
            }

            return sReturn;
        }

        public override DatabaseType DatabaseType
        {
            get { return DatabaseType.MSSqlServer; }
        }
    }
}
