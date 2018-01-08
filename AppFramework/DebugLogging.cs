using System;
using System.IO;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text;
using System.Globalization;

namespace AppFramework.Debugging
{
    /// <summary>
    /// Description of one debug flag. the description of this flag will appear in the options dialog.
    /// This file is originally from http://www.codeproject.com/KB/trace/debugtreatise.aspx^and was modified.
    /// </summary>
    public class DebugFlagEntry
    {
        /// <summary>
        /// The value of the flag
        /// </summary>
        public long flag;

        /// <summary>
        /// A text describing the flag. This text will appear in the debug flag settings dialog.
        /// </summary>
        public string text;

        /// <summary>
        /// Konstruktor mit Parametern
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="text"></param>
        public DebugFlagEntry(long flag, string text)
        {
            this.flag = flag;
            this.text = text;
        }
    }

    /// <summary>
    /// Klasse, die Debug Ausgaben verwaltet. Es muss das TRACE Makro gesetzt sein, dieses ist bei Debug/Release der Fall.
    /// 
    /// Example:
    /// <example>
    /// <code>
    /// public class Constants
    /// {
    ///     public const long DebugFlagStatusText   = 0x00010000;
    ///     public const long DebugFlagSuccess      = 0x00020000;
    /// 
    ///     private static List&lt;DebugFlagEntry&gt; debugFlags;
    /// 
    ///     static Constants()
    ///     {
    ///         debugFlags = new List&lt;DebugFlagEntry&gt;();
    ///         debugFlags.Add(new DebugFlagEntry(DebugFlagStatusText, "DebugFlagStatusText"));
    ///         debugFlags.Add(new DebugFlagEntry(DebugFlagSuccess, "DebugFlagSuccess"));
    ///     }
    /// 
    ///     DebugLogging.LogOutput("opl");
    ///     DebugLogging.AutoFlush = true;
    ///     DebugLogging.DebugMask = Logging.DebugDefaultMask | Constants.DebugFlagStatusText | Constants.DebugFlagSuccess;
    /// 
    ///     DebugLogging.ShowDebugWindow("op-log", Constants.DebugFlags);
    /// }
    /// </code>
    /// </example>
    /// </summary>
	public static class DebugLogging
	{
        /// <summary>
        /// Default Maske
        /// </summary>
        public const long DebugDefaultMask = DebugFlagError | DebugFlagWarning;

        /// <summary>
        /// Error
        /// </summary>
        public const long DebugFlagError = 0x0001;

        /// <summary>
        /// Warning
        /// </summary>
        public const long DebugFlagWarning = 0x0002;

        /// <summary>
        /// Info
        /// </summary>
        public const long DebugFlagInfo = 0x0004;

        /// <summary>
        /// Verbose
        /// </summary>
        public const long DebugFlagVerbose = 0x0008;

        /// <summary>
        /// Konstruktor und Destruktor function trace
        /// </summary>
        public const long DebugFlagTraceCtorDtor = 0x0010;

        /// <summary>
        /// Function trace
        /// </summary>
        public const long DebugFlagTraceFunction = 0x0020;

        /// <summary>
        /// DebugFlagSql
        /// </summary>
        public const long DebugFlagSql = 0x0040;

        /// <summary>
        /// DebugFlagSqlVerbose
        /// </summary>
        public const long DebugFlagSqlVerbose = 0x0080;

        /// <summary>
        /// DebugFlagGui
        /// </summary>
        public const long DebugFlagGuiCommand = 0x0100;

        /// <summary>
        /// DebugFlagGuiContents
        /// </summary>
        public const long DebugFlagGuiContents = 0x0200;

        /// <summary>
        /// Reserved1
        /// </summary>
        public const long DebugFlagTcpIp = 0x0400;

        /// <summary>
        /// Reserved2
        /// </summary>
        public const long DebugFlagReserved1 = 0x0800;

        /// <summary>
        /// Reserved3
        /// </summary>
        public const long DebugFlagReserved2 = 0x1000;

        /// <summary>
        /// Reserved4
        /// </summary>
        public const long DebugFlagReserved3 = 0x2000;

        /// <summary>
        /// Reserved5
        /// </summary>
        public const long DebugFlagReserved4 = 0x4000;

        /// <summary>
        /// Reserved6
        /// </summary>
        public const long DebugFlagReserved5 = 0x8000;

        /// <summary>
        /// Any subscribers will be notified of a debug print
        /// </summary>
        public static event EventHandler<DebugPrintEventArgs> DebugPrint;

        private static long debugMask = DebugDefaultMask;

        private static DebugPrintEventArgs debugPrintEventArgs = new DebugPrintEventArgs();
        private static bool includeTimestamp = true;
        private static bool includeThreadId = false;
        private static bool includeDebugMask = false;
        private static bool includeDebugFlags = false;

        public static string FileName { get; private set; }

        public static bool IncludeTimestamp
        {
            get { return includeTimestamp; }
            set { includeTimestamp = value; }
        }

        public static bool IncludeThreadId
        {
            get { return includeThreadId; }
            set { includeThreadId = value; }
        }

        public static bool IncludeDebugMask
        {
            get { return includeDebugMask; }
            set { includeDebugMask = value; }
        }

        public static bool IncludeDebugFlags
        {
            get { return includeDebugFlags; }
            set { includeDebugFlags = value; }
        }

        private static void OnDebugPrint(string text)
        {
            if (DebugPrint != null)
            {
                debugPrintEventArgs.Text = text;
                DebugPrint(null, debugPrintEventArgs);
            }
        }

        /// <summary>
        /// Get or set the debug mask
        /// </summary>
        public static long DebugMask
        {
            set { debugMask = value; }
            get { return debugMask; }
        }

        /// <summary>
        /// Ist ein Flag in der Maske enthalten?
        /// </summary>
        /// <param name="mask">Maske</param>
        /// <param name="flag">flag</param>
        /// <returns></returns>
        public static bool MaskHasFlag(long mask, long flag)
        {
            return (mask & flag) != 0;
        }

        /// <summary>
        /// Unhandled Exceptions behandeln
        /// </summary>
		public static void InitializeUnhandledExceptionHandler()
		{
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(DbgExceptionHandler);
		}

        /// <summary>
        /// Handler für AppDomain.CurrentDomain.UnhandledException
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
		public static void DbgExceptionHandler(object sender, UnhandledExceptionEventArgs args)
		{
			Exception e = (Exception) args.ExceptionObject;

            WriteInternal(e.Message);
            WriteInternal(e.StackTrace);

			MessageBox.Show(
				"A fatal problem has occurred.\n" + e.Message,
				"Program Stopped",
				MessageBoxButtons.OK,
				MessageBoxIcon.Stop,
				MessageBoxDefaultButton.Button1);

			Trace.Close();
			Process.GetCurrentProcess().Kill();
		}

        /// <summary>
        /// Debug Ausgabe mit Error flag
        /// </summary>
        /// <param name="text"></param>
        public static void Error(string text)
        {
            WriteLine(DebugFlagError, text);
            MessageBox.Show(text);
        }

        /// <summary>
        /// Eine Exception mit Error flag ausgeben
        /// </summary>
        /// <param name="exception"></param>
        public static void Error(Exception exception)
        {
            Write(DebugFlagError, exception);
            MessageBox.Show(exception.Message);
        }

        /// <summary>
        /// Logdatei setzen.
        /// "C:\\Users\\cmaurer\\AppData\\Local\\Temp\\oplog-2010.10.20-00.59.569-3960.log"
        /// </summary>
        /// <param name="prefix">Prefix of log file></param>
		public static void SetLogFile(string prefix)
		{
            Trace.Listeners.Clear();

            if (!string.IsNullOrEmpty(prefix))
            {
                string path = System.IO.Path.GetTempPath();

                DateTime now = DateTime.Now;

                string fileName = string.Format(CultureInfo.InvariantCulture, "{0}{1}-{2:0000}.{3:00}.{4:00}-{5:00}.{6:00}.{7:000}-{8}.log",
                    path,
                    prefix,
                    now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Millisecond,
                    Process.GetCurrentProcess().Id
                    );

                FileName = fileName;

                Trace.Listeners.Add(new TextWriterTraceListener(fileName));
                Trace.AutoFlush = true;
                WriteLineInternal("DebugLogging writing to file '" + fileName + "'");
            }
		}

        /// <summary>
        /// Flag the debugger to break at the next instruction.
        /// </summary>
		public static void Break()
		{
			Debugger.Break();
		}

        /// <summary>
        /// Autoflush for Debug and Trace
        /// </summary>
		public static bool AutoFlush
		{
            get { return Trace.AutoFlush; }
			set 
            {
                Debug.AutoFlush = value;
                Trace.AutoFlush = value;
            }
		}
		
        /// <summary>
        /// Get or set the indent level of Debug and Trace
        /// </summary>
		public static int IndentLevel
		{
            get { return Trace.IndentLevel; }
            set { Trace.IndentLevel = value; }
		}
		
        /// <summary>
        /// Get or set the indent size
        /// </summary>
		public static int IndentSize
		{
            get { return Trace.IndentSize; }
            set { Trace.IndentSize = value; }
		}
		
        /// <summary>
        /// Return the Trace Listeners
        /// </summary>
		public static TraceListenerCollection Listeners
		{
            get { return Trace.Listeners; }
		}

        /// <summary>
        /// Assert
        /// </summary>
        /// <param name="condition"></param>
		public static void Assert(bool condition)
		{
            Trace.Assert(condition);
		}
		
        /// <summary>
        /// Assert mit Text
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="text"></param>
		public static void Assert(bool condition, string text)
		{
			Trace.Assert(condition, text);
		}
		
        /// <summary>
        /// Assert mit zwei Texten
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="text1"></param>
        /// <param name="text2"></param>
		public static void Assert(bool condition, string text1, string text2)
		{
			Trace.Assert(condition, text1, text2);
		}

        /// <summary>
        /// Verify, nur in DEBUG build
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="text1"></param>
        public static void Verify(bool condition, string text1)
        {
#if DEBUG
            Trace.Assert(condition, text1);
#endif
        }
        
        /// <summary>
        /// Fail
        /// </summary>
        /// <param name="text"></param>
		public static void Fail(string text)
		{
			Trace.Fail(text);
		}
		
        /// <summary>
        /// Fail mit zwei Texten
        /// </summary>
        /// <param name="text1"></param>
        /// <param name="text2"></param>
		public static void Fail(string text1, string text2)
		{
			Trace.Fail(text1, text2);
		}

        /// <summary>
        /// Close
        /// </summary>
		public static void Close()
		{
			Trace.Close();
		}

        /// <summary>
        /// Flush
        /// </summary>
		public static void Flush()
		{
            Trace.Flush();
		}

        /// <summary>
        /// Indent
        /// </summary>
		public static void Indent()
		{
            Trace.Indent();
		}

        /// <summary>
        /// Unindent
        /// </summary>
		public static void Unindent()
		{
            Trace.Unindent();
        }

        /// <summary>
        /// Create the prefix: Variations are:
        /// <example>
        ///     "2010.09.03-10:20:45: debug text"
        ///     "[thread id]: debug text"
        ///     "(debugmask-): debug text"
        ///     "(-debugflags): debug text"
        ///     "(debugmask-debugflags): debug text"
        ///     "2010.09.03-10:20:45 [thread id] (debugmask-debugflags): debug text"
        /// </example>
        /// </summary>
        /// <param name="debugFlags">The debug flags</param>
        /// <returns></returns>
        private static string CreatePrefix(long debugFlags)
        {
            StringBuilder sb = new StringBuilder();
            string text;

            if (includeTimestamp)
            {
                DateTime now = DateTime.Now;
                text = string.Format("{0:00}.{1:00}.{2:0000}-{3:00}:{4:00}:{5:00}.{6:000}",
                    now.Day, now.Month, now.Year,
                    now.Hour, now.Minute, now.Second, now.Millisecond);
                sb.Append(text);
            }
            if (includeThreadId)
            {
                if (sb.Length > 0)
                {
                    sb.Append(" ");
                }
                sb.Append("[");
                sb.Append(System.Threading.Thread.CurrentThread.ManagedThreadId);
                sb.Append("]");
            }
            if (includeDebugMask || (includeDebugFlags && (debugFlags != 0)))
            {
                if (sb.Length > 0)
                {
                    sb.Append(" ");
                }
                sb.Append("(");

                if (includeDebugMask)
                {
                    text = string.Format("0x{0:x8}", debugMask);
                    sb.Append(text);
                    if (!includeDebugFlags || debugFlags == 0)
                    {
                        sb.Append("-");
                    }
                }
                if (includeDebugFlags && (debugFlags != 0))
                {
                    sb.Append("-");
                    text = string.Format("0x{0:x8}", debugFlags);
                    sb.Append(text);
                }

                sb.Append(")");
            }

            if (sb.Length > 0)
            {
                sb.Append(": ");
            }

            return sb.ToString();
        }

        private static void WriteInternal(string s1)
        {
            string text = CreatePrefix(0) + s1;

            Trace.Write(text);
            OnDebugPrint(text);
        }

        private static void WriteLineInternal(string s1)
        {
            string text = CreatePrefix(0) + s1;

            Trace.WriteLine(text);

            OnDebugPrint(text);
        }

        /// <summary>
        /// Debug Ausgaben schreiben mit prefix wenn das flag in der Debug Maske gesetzt ist
        /// </summary>
        /// <param name="debugFlag">Das Flag</param>
        /// <param name="s1">Der Text</param>
        public static void Write(long debugFlag, string s1)
        {
            if ((debugFlag & debugMask) != 0)
            {
                string text = CreatePrefix(debugFlag) + s1;

                Trace.Write(text);
                OnDebugPrint(text);
            }
        }

        /// <summary>
        /// Debug Ausgaben schreiben mit prefix wenn das flag in der Debug Maske gesetzt ist
        /// </summary>
        /// <param name="debugFlag">Das Flag</param>
        /// <param name="s1">Der Text</param>
        public static void WriteLine(long debugFlag, string s1)
        {
            if ((debugFlag & debugMask) != 0)
            {
                string text = CreatePrefix(debugFlag) + s1;

                Trace.WriteLine(text);
                OnDebugPrint(text);
            }
        }

        /// <summary>
        /// Debug Ausgaben schreiben mit prefix wenn das flag in der Debug Maske gesetzt ist
        /// </summary>
        /// <param name="debugFlag">Das Flag</param>
        /// <param name="s1">Der Text</param>
        public static void WriteLine(long debugFlag, string prefix, byte[] buffer, int count)
        {
            if ((debugFlag & debugMask) != 0)
            {
                StringBuilder sb = new StringBuilder(prefix);

                for (int i = 0; i < count; i++)
                {
                    sb.AppendFormat(" {0:x}", buffer[i]);
                }
                string text = CreatePrefix(debugFlag) + sb.ToString();

                Trace.WriteLine(text);

                OnDebugPrint(text);
            }
        }

        /// <summary>
        /// Debug Ausgaben schreiben mit prefix wenn das flag in der Debug Maske gesetzt ist
        /// </summary>
        /// <param name="debugFlag">Das Flag</param>
        /// <param name="s1">Der Text</param>
        public static void WriteLine(long debugFlag, string prefix, byte[] buffer, int countReceived, int count)
        {
            if ((debugFlag & debugMask) != 0)
            {
                StringBuilder sb = new StringBuilder(prefix);

                for (int i = 0; i < count; i++)
                {
                    sb.AppendFormat(" {0:x}", buffer[countReceived + i]);
                }
                string text = CreatePrefix(debugFlag) + sb.ToString();

                Trace.WriteLine(text);

                OnDebugPrint(text);
            }
        }


        /// <summary>
        /// Eine Exception schreiben mit prefix wenn das flag in der Debug Maske gesetzt ist
        /// </summary>
        /// <param name="debugFlag">Das Flag</param>
        /// <param name="exception">Der Text</param>
        public static void Write(long debugFlag, Exception exception)
        {
            WriteLine(debugFlag, exception.Message);

            string[] arStackTrace = exception.StackTrace.Split('\x000a');

            foreach (string line in arStackTrace)
            {
                WriteLine(debugFlag, line);
            }
        }

        /// <summary>
        /// Das Debug Fenster anzeigen
        /// </summary>
        public static void ShowDebugWindow()
        {
            ShowDebugWindow(null, null, false);
        }

        /// <summary>
        /// Generate a password to open the debug window for today
        /// </summary>
        /// <returns></returns>
        public static string GeneratePassword()
        {
            DateTime now = DateTime.Now;

            string plainText = string.Format(CultureInfo.InvariantCulture, "{0:00}.{1:00}.{2:0000}",
                now.Day, now.Month, now.Year);

            string cypherText = BusinessLayerBase.Encrypt(plainText);

            return cypherText;
        }

        private static bool IsValidPassword(string cypherText)
        {
            bool success = false;

            if (!string.IsNullOrEmpty(cypherText))
            {
                string plainText = BusinessLayerBase.Decrypt(cypherText);

                DateTime now = DateTime.Now;

                string strNow = string.Format(CultureInfo.InvariantCulture, "{0:00}.{1:00}.{2:0000}",
                    now.Day, now.Month, now.Year);

                if (plainText.Equals(strNow))
                {
                    success = true;
                }
            }

            return success;
        }

        /// <summary>
        /// Das Debug Fenster anzeigen und Definitionen der flags mit übergebenö
        /// </summary>
        /// <param name="applicationName">Kürzel der Anwendung, erscheint im Optionen Fenster</param>
        /// <param name="entries">Liste der flag Definitionen</param>
        public static void ShowDebugWindow(string applicationName, List<DebugFlagEntry> entries, bool showDebugMaskView)
        {
            bool showWindow = true;

#if false
            showWindow = false;

            EnterPasswordView pw = new EnterPasswordView("Debug window");

#if DEBUG
            string cypherText = GeneratePassword();
            pw.Password = cypherText;
#endif

            DialogResult dialogResult =  pw.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                if (IsValidPassword(pw.Password))
                {
                    showWindow = true;
                }
            }
#endif
            if (showWindow)
            {

                DebugView dlg = new DebugView(applicationName, entries, showDebugMaskView);
                dlg.Show();

                if (!string.IsNullOrEmpty(FileName))
                {
                    WriteLineInternal("DebugLogging writing to file '" + FileName + "'");
                }
            }
        }
    }
}

