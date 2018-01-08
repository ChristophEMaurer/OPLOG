using System;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Drawing;
using System.Resources;
using System.Globalization;

namespace AppFramework
{
    /// <summary>
    /// Base class for a windows forms database application. Used also by subclass used by web pages.
    /// </summary>
    public abstract class BusinessLayerBase : AppFramework.Debugging.IDebuggee
    {
        abstract public void DebugPrint(long flag, string msg);

        /// <summary>
        /// Wird verwendet, um die Werte symmetrisch zu verschlüsseln,
        /// die man nicht direkt sehen soll
        /// </summary>
        private const string Salt = "q$r%ju&*l()1#$1ef!C!ECW!DC$@RGvqdcqef';1lf!EC!ec";

        /// <summary>
        /// The title for all message boxes
        /// </summary>
        private string _programTitle;

        private DatabaseLayerBase _databaseLayerBase;
        private int _majorVersion;
        private int _minorVersion;
        private string _versionDate;
        private int _databaseMajor = -1;
        private int _databaseMinor = -1;
        private string _databaseVersion;
        protected ResourceManager resourceManager;

        private static Color _infoColor = Color.Blue;

        /// <summary>
        /// The path from the config file entry Operationen.Default.DatabasePath
        /// </summary>
        private string _serverDir;

        /// <summary>
        /// Path and full name of ACCESS database, auf dem Server.
        /// </summary>
        private string _databasePath = "";

        /// <summary>
        /// Path of the client app
        /// </summary>
        private string _appPath;

        protected BusinessLayerBase(ResourceManager resManager, int majorVersion, int minorVersion, string versionDate, string programTitle)
        {
            resourceManager = resManager;
            _versionDate = versionDate;
            _majorVersion = majorVersion;
            _minorVersion = minorVersion;
            _programTitle = programTitle;
        }

        virtual protected bool UiAllowed { get { return true; } }

        public static bool Is64Bit()
        {
            return IntPtr.Size == 8;
        }

        public string AppPath
        {
            get { return _appPath; }
            set { _appPath = value; }
        }

        public int DatabaseMajor
	    {
		    get { return _databaseMajor;}
	    }

        public static Color InfoColor
        {
            get { return _infoColor; }
        }

        public static string Encrypt(string plainText)
        {
            Security.Cryptography.SymmetricCryptography crypter = new Security.Cryptography.SymmetricCryptography();

            crypter.Key = Salt;

            return crypter.Encrypt(plainText);
        }

        public static string Decrypt(string cypherText)
        {
            Security.Cryptography.SymmetricCryptography crypter = new Security.Cryptography.SymmetricCryptography();

            crypter.Key = Salt;

            return crypter.Decrypt(cypherText);
        }

        public DatabaseLayerBase DatabaseLayerBase
        {
            get { return _databaseLayerBase; }
        }

        public virtual void SetDatabaseLayer(DatabaseLayerBase databaseLayer)
        {
            _databaseLayerBase = databaseLayer;
        }

        public int DatabaseMinor
        {
            get { return _databaseMinor; }
        }

        public string DatabaseVersion
        {
            get { return _databaseVersion; }
            set { _databaseVersion = value; }
        }

        public int ConvertToInt32(object value)
        {
            return _databaseLayerBase.ConvertToInt32(value);
        }
        public long ConvertToInt64(object value)
        {
            return _databaseLayerBase.ConvertToInt64(value);
        }

        public string ServerDir
        {
            get { return _serverDir; }
            set { _serverDir = value; }
        }

        public virtual bool InitialDocumentCheck()
        {
            return true;
        }

        public string DatabasePath
        {
            get 
            { 
                return _databasePath; 
            }
            set 
            { 
                _databasePath = value; 
            }
        }

        public virtual string AppTitle()
        {
            return AppTitle("");
        }

        public virtual string AppTitle(string text)
        {
            string windowText = text;

            if (windowText.Length > 0)
            {
                windowText = windowText + " - " + _programTitle;
            }
            else
            {
                windowText = _programTitle;
            }

            return windowText;
        }

        public bool ReadDatabaseVersion()
        {
            bool success = true;

            _databaseMajor = -1;
            _databaseMinor = -1;

            int major = -1;
            int minor = -1;

            DataRow row = _databaseLayerBase.GetConfig("MajorVersion");

            if (row == null)
            {
                success = false;
                goto _exit;
            }
            if (!Int32.TryParse((string)row["Value"], out major))
            {
                success = false;
                goto _exit;
            }
            row = _databaseLayerBase.GetConfig("MinorVersion");
            if (row == null)
            {
                    success = false;
                    goto _exit;
            }
            if (!Int32.TryParse((string)row["Value"], out minor))
            {
                success = false;
                goto _exit;
            }

            if (major != -1 && minor != -1)
            {
                _databaseMajor = major;
                _databaseMinor = minor;
                _databaseVersion = _databaseMajor + "." + _databaseMinor;
            }

        _exit:
            return success;
        }

        public bool ProgramVersionLowerThanDatabase()
        {
            bool ret = false;

            if (ReadDatabaseVersion())
            {
                if (_databaseMajor > _majorVersion ||
                    (_databaseMajor == _majorVersion && _databaseMinor > _minorVersion))
                {
                    ret = true;
                }
            }

            return ret;
        }

        /// <summary>
        /// Gibt false zurück, wenn ein DB-Update durchgeführt werden muss.
        /// </summary>
        /// <returns></returns>
        public bool DatabaseUpdateNeeded()
        {
            bool needsUpdate = false;

            if (ReadDatabaseVersion())
            {
                if (_databaseMajor < _majorVersion ||
                    (_databaseMajor == _majorVersion && _databaseMinor < _minorVersion))
                {
                    needsUpdate = true;
                }
            }

            return needsUpdate;
        }

        public bool TestDatabaseConnection()
        {
            return _databaseLayerBase.TestDatabaseConnection();
        }
        public void BadVersionMessage()
        {
            string msg = string.Format(CultureInfo.InvariantCulture, GetText("errorVersion"),
                 _databaseMajor, _databaseMinor, _databasePath, _majorVersion, _minorVersion, _versionDate);

            DisplayError(msg);
        }

        public virtual bool InitialDirCheck()
        {
            return true;
        }

        #region Access Database specific
#if false
        public virtual bool CompactAccessDB(string tempFileName, string password)
        {
            bool success = false;

            try
            {
                string targetDB = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                    + tempFileName
                    + ";Jet OLEDB:Database Password="
                    + password + ";";

                _databaseLayerAbstract.CompactAccessDB(targetDB + " Engine Type=5");

                DataView dv = _databaseLayerAbstract.TestDatabaseConnection(targetDB, "select count(ID_Config) from Config");
                if (dv != null)
                {
                    Utility.Tools.DeleteFile(_databasePath);
                    System.IO.File.Move(tempFileName, _databasePath);
                    dv = _databaseLayerAbstract.TestDatabaseConnection(null, "select count(ID_Config) from Config");
                    if (dv != null)
                    {
                        success = true;
                    }
                    else
                    {
                        string s = "Zugriff auf die komprimierte Datenbank hat nicht geklappt. Die Datenbank darf von keinem anderen Benutzer geöffnet sein."
                            + "\nSchließen Sie die Anwendung und starten Sie sie erneut."
                            + "\nFalls die Datenbank defekt ist, stellen Sie sie aus einer Sicherung wieder her."
                            + "\nEventuell befindet sich die komprimierte Datenbank auf"
                            + "\n" + tempFileName + ".\n\n\n";

                        Error Message(s);
                    }
                }
            }
            catch (Exception ex)
            {
                string s = "Komprimieren der Datenbank hat nicht geklappt. Die Datenbank darf von keinem anderen Benutzer geöffnet sein."
                    + "\nSchließen Sie die Anwendung und starten Sie sie erneut."
                    + "\nFalls die Datenbank defekt ist, stellen Sie sie aus einer Sicherung wieder her."
                    + "\nEventuell befindet sich die komprimierte Datenbank auf"
                    + "\n" + tempFileName + ".\n\n\n"
                    + ex.ToString();

                Error Message(s);
            }

            return success;
        }
#endif
        #endregion

        #region ResourceManager
        protected virtual void ProcessText(StringBuilder sb, bool forTextBox)
        {
            if (forTextBox)
            {
                sb.Replace("$r$", "\r" + Environment.NewLine);
            }
            else
            {
                sb.Replace("$r$", "\r");
                sb.Replace("$nl$", Environment.NewLine);
            }
        }

        private string GetText(string formName, string id, bool forTextBox)
        {
            StringBuilder sb = new StringBuilder(resourceManager.GetString(formName + "_" + id));

            if (sb.Length == 0)
            {
                sb.Append(id);
            }
            else
            {
                ProcessText(sb, forTextBox);
            }

            return sb.ToString();
        }

        public string GetText(string formName, string id)
        {
            return GetText(formName, id, false);
        }

        public string GetTextForTextBox(string formName, string id)
        {
            return GetText(formName, id, true);
        }

        public string GetText(string id)
        {
            return GetText("BusinessLayer", id, false);
        }

        public string GetTextForTextBox(string id)
        {
            return GetText("BusinessLayer", id, true);
        }
        #endregion


        /// <summary>
        /// Display the exception and the text and return all of this.
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public virtual string DisplayError(Exception exception, string text)
        {
            StringBuilder sb = new StringBuilder();

            if (text != null)
            {
                sb.Append(text);
            }

            if (exception != null)
            {
                if (sb.Length > 0)
                {
                    sb.Append("\r\r");
                }

                sb.Append("Exception:\r");
                sb.Append(exception.Message);
                sb.Append("\rStackTrace:");
                sb.Append(exception.StackTrace);

                if (exception.InnerException != null)
                {
                    sb.Append("\r\rInner exception\r");
                    sb.Append(exception.InnerException.Message);
                    sb.Append("\rStackTrace");
                    sb.Append(exception.InnerException.StackTrace);
                }
            }

            string msg = sb.ToString();
            DebugPrint(Debugging.DebugLogging.DebugFlagError, msg);

            if (UiAllowed)
            {
                MessageBox.Show(msg, _programTitle);
            }

            return msg;
        }

        public string DisplayError(Exception exception)
        {
            return DisplayError(exception, null);
        }

        public string DisplayError(string text)
        {
            return DisplayError(null, text);
        }
    }
}
