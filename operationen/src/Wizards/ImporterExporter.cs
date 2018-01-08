using System;
using System.Data.Common;
using System.Collections;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;

namespace Operationen.Wizards
{
    //
    // Enthält zusätzliche Datenbank Variablen,
    // die immer für die umkopierte chirurg.mdb gelten
    // Alle Funktionen dieser Klasse beziehen sich immer auf die
    // ACCESS-Datenbank aus der importiert wird, oder in die exportiert wird.
    // Daher kann man hier fest SQL Parameter mit Syntax @param verwenden (mySQL braucht ?param).
    //
    public class ImporterExporter
    {
        protected DbProviderFactory _dataFactory = null;
        protected DbConnection _connection = null;
        protected DbCommand _command = null;
        Label _lblProgress;
        protected string _fileName;
        protected const int ProgressThreshold = 100;
        protected int progressCount = 0;
        
        protected BusinessLayer _businessLayer;
        private ProgressBar _progressBar;
        private string _startupPath;

        public ImporterExporter(BusinessLayer b, ProgressBar progressBar)
            : this(b, progressBar, null)
        {
        }

        public ImporterExporter(BusinessLayer b, ProgressBar progressBar, Label lblProgress)
        {
            _startupPath = Application.StartupPath;

            _businessLayer = b;
            _progressBar = progressBar;
            _lblProgress = lblProgress;

            if (_progressBar != null)
            {
                _progressBar.Minimum = 0;
                _progressBar.Maximum = 100;
            }
        }

        public int ConvertToInt32(object o)
        {
            return _businessLayer.ConvertToInt32(o);
        }

        protected int ExecuteNonQuery(string sql, ArrayList sqlParameters)
        {
            int iEffectedRows = 0;

            sql = CleanSqlStatement(sql);
            _command.CommandText = sql;
            _command.Parameters.Clear();
            if (sqlParameters != null)
            {
                DatabaseLayer.MapSqlParameter2Command(_command, sqlParameters);
            }
            iEffectedRows = _command.ExecuteNonQuery();

            return iEffectedRows;
        }

        protected int ExecuteScalar(string sql, ArrayList sqlParameters)
        {
            sql = CleanSqlStatement(sql);

            _command.CommandText = sql;
            _command.Parameters.Clear();
            if (sqlParameters != null)
            {
                DatabaseLayer.MapSqlParameter2Command(_command, sqlParameters);
            }
            int result = ConvertToInt32(_command.ExecuteScalar());

            return result;
        }

        protected int Count(string tableName)
        {
            string sql = "select count(ID_" + tableName + ") from " + tableName;

            return ExecuteScalar(sql, null);
        }

        protected int Count(string tableName, string columnName)
        {
            string sql = "select count(" + columnName + ") from " + tableName;

            return ExecuteScalar(sql, null);
        }

        protected int GetLastGeneratedId()
        {
            int ID_NewRecord = 0;

            try
            {
                _command.CommandText = "SELECT @@IDENTITY";
                _command.Parameters.Clear();
                object o = _command.ExecuteScalar();
                ID_NewRecord = int.Parse(o.ToString());
            }
            catch
            {
            }

            return ID_NewRecord;
        }

        protected int InsertRecord(string sSQL, ArrayList oParameters, string sTable)
        {
            int iID_NewRecord = 0;

            string sCleanSql = CleanSqlStatement(sSQL);

            if (this.ExecuteNonQuery(sCleanSql, oParameters) > 0)
            {
                iID_NewRecord = GetLastGeneratedId();
            }
            else
            {
                //string sMessage = "New record could not be inserted into table <@Table> !";
            }

            return iID_NewRecord;
        }

        protected DataRow GetRecord(string sSQL, ArrayList aSqlParameter, string sTable)
        {
            DataRow oDataRow = null;

            sSQL = CleanSqlStatement(sSQL);
            _command.CommandText = sSQL;
            _command.Parameters.Clear();
            if (aSqlParameter != null)
            {
                DatabaseLayer.MapSqlParameter2Command(_command, aSqlParameter);
            }
            IDbDataAdapter oDataAdapter = _dataFactory.CreateDataAdapter();
            oDataAdapter.SelectCommand = _command;
            DataSet oDataSet = new DataSet(sTable);
            oDataAdapter.Fill(oDataSet);

            if (oDataSet.Tables[0].Rows.Count > 0)
            {
                oDataRow = oDataSet.Tables[0].Rows[0];
            }

            oDataSet.Dispose();
            oDataSet = null;
            oDataAdapter = null;

            return oDataRow;
        }
        protected DataView GetDataView(string sql, string tableName)
        {
            return GetDataView(sql, null, tableName);
        }

        protected DataView GetDataView(string sSQL, ArrayList aSQLParameters, string sTable)
        {
            DataView oDataView = null;

            sSQL = CleanSqlStatement(sSQL);
            _command.CommandText = sSQL;
            _command.Parameters.Clear();
            if (aSQLParameters != null)
            {
                DatabaseLayer.MapSqlParameter2Command(_command, aSQLParameters);
            }

            IDbDataAdapter oDataAdapter = _dataFactory.CreateDataAdapter();
            oDataAdapter.SelectCommand = _command;
            DataSet oDataSet = new DataSet(sTable);
            oDataAdapter.Fill(oDataSet);

            oDataView = new DataView(oDataSet.Tables[0]);

            oDataSet.Dispose();
            oDataSet = null;
            oDataAdapter = null;

            return oDataView;
        }

        public static string CleanSqlStatement(string sSQL)
        {
            string s = Regex.Replace(sSQL, @"(\s\s+)", @" ");

            return s;
        }

        protected ProgressBar TheProgressBar
        {
            get { return _progressBar; }
        }

        protected void Progress(string text)
        {
            _lblProgress.Text = text;
            Progress();
        }

        public void Initialize(string fileName)
        {
            _fileName = fileName;
        }

        protected DatabaseLayer DatabaseLayer
        {
            get { return _businessLayer.DatabaseLayer; }
        }

        protected string StartupPath
        {
            get { return _startupPath; }
        }

        public void Progress()
        {
            if (_progressBar != null)
            {
                if (_progressBar.Value < _progressBar.Maximum)
                {
                    _progressBar.Value++;
                }
                if (_progressBar.Value == _progressBar.Maximum)
                {
                    _progressBar.Value = 0;
                }

                Application.DoEvents();
            }
        }

        protected string GetText(string form, string id)
        {
            return _businessLayer.GetText(form, id);
        }
    }
}
