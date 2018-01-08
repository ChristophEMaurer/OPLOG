using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Collections;

namespace AppFramework
{
    public class DatabaseImplementationAccess2007 : DatabaseImplementationAccess
    {
        /// <summary>
        /// This is the class for MSAccess 2007 and later for 64Bit Windows.
        /// The Provider "Microsoft.Jet.OLEDB.4.0" is not supported on 64Bit Windows, so we must use the new MSAccess.
        /// 
        /// The connectionstring for Access ACC is "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\myFolder\myAccessFile.accdb;Jet OLEDB:Database Password=MyDbPassword;"
        /// </summary>
        /// 
        public override void CompactDatabase(string targetDatabaseConnectionString)
        {
            //
            // Make sure nothing is done when this function is called!!!
            //
        }
    }
}


