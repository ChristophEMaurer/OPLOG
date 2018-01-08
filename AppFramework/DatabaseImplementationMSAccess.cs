using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Collections;
using System.Text;

namespace AppFramework
{
    public class DatabaseImplementationAccess : DatabaseImplementation
    {
        /// <summary>
        /// <br/>count(...) liefert long
        /// </summary>
        /// <param name="bizLayer"></param>
        /// <param name="strConnectionString"></param>
        public DatabaseImplementationAccess()
        {
            // Wenn man nur Operationen.mdb angibt, wird diese Datei im aktuellen Pfad gesucht.
            // Wenn man zwischendurch den FileOpen Dialog öffnet, so wird der aktuelle Pfad verstellt
            // und das nächste Open() schlaegt fehl.
            // Daher muss man den vollstaendigen Pfad angeben.
            DataFactory = DbProviderFactories.GetFactory("System.Data.OleDb");
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
            return "now()";
        }

        /// <summary>
        /// Macht aus einem String im Format dd.mm.yyyy einen Text, mit 
        /// dem man in einem INSERT was in eine ACCESS/Datenbank einfügen kann,
        /// also DateSerial(zzzz, mm, dd) + TimeSerial(hour, minute, millisecond)
        /// </summary>
        /// <param name="sDateTime">14.05.1965</param>
        /// <returns></returns>
        public override string DateTime2DBDateTimeString(object dateTime)
        {
            string sql;

            if (dateTime == DBNull.Value)
            {
                sql = "null";
            }
            else
            {
                DateTime dt = (DateTime)dateTime;
                sql = NullableDateTime2DBDateTimeString(dt);
            }

            return sql;
        }


        /// <summary>
        /// Macht aus einem String im Format hh.mm einen Text, mit 
        /// dem man in einem INSERT was in eine ACCESS/Datenbank einfügen kann,
        /// also TimeSerial(hh, mm, ss)
        /// </summary>
        /// <param name="sDateTime">10:00</param>
        /// <returns></returns>
        public override string DateTime2DBTimeString(object oDateTime)
        {
            string sReturn;

            if (oDateTime == DBNull.Value)
            {
                sReturn = "null";
            }
            else
            {
                DateTime dt = (DateTime)oDateTime;

                sReturn = string.Format("TimeSerial({0}, {1}, 0)", dt.Hour, dt.Minute);
            }

            return sReturn;
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

        public override string NullableDateTime2DBDateString(DateTime? dateTime)
        {
            string sql;

            if (dateTime.HasValue)
            {
                sql = string.Format("DateSerial({0}, {1}, {2})", dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day);
            }
            else
            {
                sql = "null";
            }

            return sql;
        }

        public override string NullableDateTime2DBDateTimeString(DateTime? dateTime)
        {
            string sql;

            if (dateTime.HasValue)
            {
                sql = string.Format("DateSerial({0}, {1}, {2}) + TimeSerial({3}, {4}, {5})",
                    dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day,
                    dateTime.Value.Hour, dateTime.Value.Minute, dateTime.Value.Second
                    );
            }
            else
            {
                sql = "null";
            }

            return sql;
        }

        override public string CreateLikeExpression(string sText)
        {
            string s = null;

            if (!string.IsNullOrEmpty(sText))
            {
                s = sText.Replace("-", "[-]");
            }

            return s;
        }

        public override void CompactDatabase(string targetDatabaseConnectionString)
        {
#if false
            object[] oParams;

            //create an inctance of a Jet Replication Object
            object objJRO = Activator.CreateInstance(Type.GetTypeFromProgID("JRO.JetEngine"));

            //filling Parameters array
            //change "Jet OLEDB:Engine Type=5" to an appropriate value
            // or leave it as is if you db is JET4X format (access 2000,2002)
            //(yes, jetengine5 is for JET4X, no misprint here)
            oParams = new object[] {
                ConnectionString,
                strTargetDatabaseConnectionString
                };

            objJRO.GetType().InvokeMember("CompactDatabase",
                System.Reflection.BindingFlags.InvokeMethod,
                null,
                objJRO,
                oParams);

            System.Runtime.InteropServices.Marshal.ReleaseComObject(objJRO);
            objJRO = null;
#endif
        }

        public override DatabaseType DatabaseType
        {
            get { return DatabaseType.MSAccess; }
        }

    }
}


