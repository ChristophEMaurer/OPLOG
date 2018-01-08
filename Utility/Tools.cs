using System;
using System.IO;
using System.Data;
using System.Web.Security;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Globalization;
using System.Security.Principal;
using System.Collections.Generic;

namespace Utility
{
    public static class Tools
    {
        private const string AllowedXmlLicenseFileChars = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890äöüßÄÖÜ@.-_,";
        /*        
                private Tools() 
                {
                    // FXCop: StaticHolderTypesShouldNotHaveConstructors
                }
         */

        public static string CutString(string text, int length)
        {
            return CutString(text, length, false);
        }

        public static string CutString(string text, int length, bool appendDots)
        {
            string s = text;

            if (s.Length > length)
            {
                s = s.Substring(0, length);
                if (appendDots)
                {
                    s += "...";
                }
            }

            return s;
        }

        /// <summary>
        /// Liefert DBNull.Null oder einen DateTime Wert.
        /// <code>
        /// dataRow["DatumVon"] = Tools.InputTextDate2NullableDatabaseDateTime(this.txtDatumVon.Text);
        /// </code>
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static object InputTextDate2NullableDatabaseDateTime(string date)
        {
            object dt = DBNull.Value;

            if (date != null && date.Length > 0)
            {
                dt = InputTextDate2DateTime(date);
            }
            return dt;
        }

        public static DateTime InputTextDateTime2DateTime(string date, string time)
        {
            int day = Int32.Parse(date.Substring(0, 2), CultureInfo.InvariantCulture);
            int month = Int32.Parse(date.Substring(3, 2), CultureInfo.InvariantCulture);
            int year = Int32.Parse(date.Substring(6, 4), CultureInfo.InvariantCulture);

            int hour = Int32.Parse(time.Substring(0, 2), CultureInfo.InvariantCulture);
            int minute = Int32.Parse(time.Substring(3, 2), CultureInfo.InvariantCulture);

            return new DateTime(year, month, day, hour, minute, 0);
        }

        /// <summary>
        /// Macht aus einem Text im Format dd.mm.yyyy ein DateTime mit Uhrzeit 00:00:00
        /// Wirft eine Exception, wenn strDate kein gültiges Format hat.
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static DateTime InputTextDate2DateTime(string date)
        {
            int day = Int32.Parse(date.Substring(0, 2), CultureInfo.InvariantCulture);
            int month = Int32.Parse(date.Substring(3, 2), CultureInfo.InvariantCulture);
            int year = Int32.Parse(date.Substring(6, 4), CultureInfo.InvariantCulture);

            return new DateTime(year, month, day, 0, 0, 0);
        }

        /// <summary>
        /// Macht aus einem Text im Format dd.mm.yyyy ein DateTime mit Uhrzeit 23:59:59
        /// Wirft eine Exception, wenn strDate kein gültiges Format hat.
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static DateTime InputTextDate2DateTimeEnd(string date)
        {
            int day = Int32.Parse(date.Substring(0, 2), CultureInfo.InvariantCulture);
            int month = Int32.Parse(date.Substring(3, 2), CultureInfo.InvariantCulture);
            int year = Int32.Parse(date.Substring(6, 4), CultureInfo.InvariantCulture);

            return new DateTime(year, month, day, 23, 59, 59);
        }

        public static DateTime? InputTextDate2NullableDateTime(string date)
        {
            DateTime? dt = null;

            if (date != null && date.Length > 0)
            {
                int nDay = Int32.Parse(date.Substring(0, 2), CultureInfo.InvariantCulture);
                int nMonth = Int32.Parse(date.Substring(3, 2), CultureInfo.InvariantCulture);
                int nYear = Int32.Parse(date.Substring(6, 4), CultureInfo.InvariantCulture);
                dt = new DateTime(nYear, nMonth, nDay, 0, 0, 0);
            }
            return dt;
        }

        /// <summary>
        /// Liefert DBNull.Null oder einen DateTime Wert ohne Datum aber mit Zeit.
        /// <code>
        /// dataRow["DatumVon"] = Tools.InputTextTime2NullableDatabaseDateTime(this.txtDatumVon.Text);
        /// </code>
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static object InputTextTime2NullableDatabaseDateTime(string date)
        {
            object dt = DBNull.Value;

            if (date != null && date.Length > 0)
            {
                dt = InputTextTime2DateTime(date);
            }
            return dt;
        }

        /// <summary>
        /// Convert a string to a DateTime, setting the Time part only.
        /// </summary>
        /// <param name="strTime">"hh:mm"</param>
        /// <returns>a DateTime with date 1.1.1900 and time as supplied in strTime</returns>
        public static DateTime InputTextTime2DateTime(string time)
        {
            int hour = Int32.Parse(time.Substring(0, 2), CultureInfo.InvariantCulture);
            int minute = Int32.Parse(time.Substring(3, 2), CultureInfo.InvariantCulture);

            DateTime dt = new DateTime(1900, 1, 1, hour, minute, 0);
            return dt;
        }

        /// <summary>
        /// Control.Text = Tools.NullableDateTime2DateTimeString(oDataRow["Datum"])
        /// Liefert ein Datum im Format "tt.mm.jjjj hh:mm" oder "" wenn der Wert null ist.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DBNullableDateTime2DateTimeString(object value)
        {
            string s = "";

            if (value != DBNull.Value)
            {
                DateTime dt = (DateTime)value;

                s = DateTime2DateTimeString(dt);
            }

            return s;
        }
        public static string DateTime2DateTimeString(DateTime dt)
        {
            int nDay = dt.Day;
            int nMonth = dt.Month;
            int nYear = dt.Year;

            int hour = dt.Hour;
            int minute = dt.Minute;

            string s = string.Format(CultureInfo.InvariantCulture, "{0:00}.{1:00}.{2:0000} {3:00}:{4:00}",
                nDay, nMonth, nYear,
                hour, minute);

            return s;
        }

        public static string DateTime2DateString(DateTime dt)
        {
            int nDay = dt.Day;
            int nMonth = dt.Month;
            int nYear = dt.Year;

            string s = string.Format(CultureInfo.InvariantCulture, "{0:00}.{1:00}.{2:0000}",
                nDay, nMonth, nYear);

            return s;
        }

        public static string DateTime2DateStringYY(DateTime dt)
        {
            int nDay = dt.Day;
            int nMonth = dt.Month;
            int nYear = dt.Year % 100;

            string s = string.Format(CultureInfo.InvariantCulture, "{0:00}.{1:00}.{2:00}",
                nDay, nMonth, nYear);

            return s;
        }

        public static string DateTime2DateStringMMYY(DateTime dt)
        {
            int month = dt.Month;
            int year = dt.Year % 100;

            string s = string.Format(CultureInfo.InvariantCulture, "{0:00}.{1:00}", month, year);

            return s;
        }

        public static string DateTime2ShortDateString(DateTime dt)
        {
            int nDay = dt.Day;
            int nMonth = dt.Month;
            int nYear = dt.Year;

            string s = string.Format(CultureInfo.InvariantCulture, "{0:00}.{1:00}.{2:00}",
                nDay, nMonth, nYear % 1000);

            return s;
        }

        /// <summary>
        /// Control.Text = Tools.NullableDateTime2DateTimeString(oDataRow["Datum"])
        /// Liefert ein Datum im Format "tt.mm.jjjj hh:mm" oder "" wenn der Wert null ist.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DBNullableDateTime2SortableDateTimeString(object value)
        {
            string s = "";

            if (value != DBNull.Value)
            {
                DateTime dt = (DateTime)value;
                int nDay = dt.Day;
                int nMonth = dt.Month;
                int nYear = dt.Year;

                int hour = dt.Hour;
                int minute = dt.Minute;

                s = string.Format(CultureInfo.InvariantCulture, "{0:0000}-{1:00}-{2:00} {3:00}:{4:00}",
                    nYear, nMonth, nDay,
                    hour, minute);
            }

            return s;
        }

        public static string DBNullableDateTime2DateString(object value)
        {
            string s = "";

            if (value != null && value != DBNull.Value)
            {
                DateTime dt = (DateTime)value;
                int nDay = dt.Day;
                int nMonth = dt.Month;
                int nYear = dt.Year;

                s = string.Format(CultureInfo.InvariantCulture, "{0:00}.{1:00}.{2:0000}", nDay, nMonth, nYear);
            }

            return s;
        }

        public static string DBNullableDateTime2SortableDateString(object value)
        {
            string s = "";

            if (value != null && value != DBNull.Value)
            {
                DateTime dt = (DateTime)value;
                int nDay = dt.Day;
                int nMonth = dt.Month;
                int nYear = dt.Year;

                s = string.Format(CultureInfo.InvariantCulture, "{0:0000}-{1:00}-{2:00}", nYear, nMonth, nDay);
            }

            return s;
        }
        public static string DBDateTime2DateString(object value)
        {
            DateTime dt = (DateTime)value;
            int day = dt.Day;
            int month = dt.Month;
            int year = dt.Year;

            string s = string.Format(CultureInfo.InvariantCulture, "{0:00}.{1:00}.{2:0000}", day, month, year);

            return s;
        }

        public static string DBNullableInt2String(object value)
        {
            string s = "";

            if (value != DBNull.Value)
            {
                s = value.ToString();
            }

            return s;
        }

        public static string DBNullableString2String(object value)
        {
            string s = "";

            if (value != DBNull.Value)
            {
                s = (string)value;
            }

            return s;
        }

        public static string NullableDateTime2DateString(DateTime? dt)
        {
            string s = "";

            if (dt.HasValue)
            {
                int nDay = dt.Value.Day;
                int nMonth = dt.Value.Month;
                int nYear = dt.Value.Year;

                s = string.Format(CultureInfo.InvariantCulture, "{0:00}.{1:00}.{2:0000}", nDay, nMonth, nYear);
            }

            return s;
        }

        /// <summary>
        /// Control.Text = Tools.NullableDateTime2TimeString(oDataRow["Zeit"])
        /// Liefert ein Datum im Format "hh:mm" oder "" wenn der Wert null ist.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DBNullableDateTime2TimeString(object value)
        {
            string s = "";

            if (value != DBNull.Value)
            {
                DateTime dt = (DateTime)value;
                int nHour = dt.Hour;
                int nMinute = dt.Minute;

                s = string.Format(CultureInfo.InvariantCulture, "{0:00}:{1:00}", nHour, nMinute);
            }

            return s;
        }

        /// <summary>
        /// 01234567890
        /// 14.05.1965
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static bool DateIsValidGermanDate(string date)
        {
            bool success = false;

            if (date.Length == 10
                    && date.Substring(2, 1) == "."
                    && date.Substring(5, 1) == "."
                )
            {
                try
                {
                    int day = Int32.Parse(date.Substring(0, 2), CultureInfo.InvariantCulture);
                    int month = Int32.Parse(date.Substring(3, 2), CultureInfo.InvariantCulture);
                    int year = Int32.Parse(date.Substring(6, 4), CultureInfo.InvariantCulture);

                    DateTime dateTime = new DateTime(year, month, day, 0, 0, 0);

                    if (DatabaseDateTimeMinValue <= dateTime && dateTime <= DatabaseDateTimeMaxValue)
                    {
                        success = true;
                    }
                }
                catch
                {
                }
            }
            return success;
        }

        public static bool TimeIsValidGermanTime(string time)
        {
            bool success = false;

            if (time.Length == 5)
            {
                try
                {
                    int hour = Int32.Parse(time.Substring(0, 2), CultureInfo.InvariantCulture);
                    int minute = Int32.Parse(time.Substring(3, 2), CultureInfo.InvariantCulture);
                    DateTime dt = new DateTime(1900, 1, 1, hour, minute, 0);
                    success = true;
                }
                catch
                {
                }
            }
            return success;
        }

        public static string Password2HashedPassword(string password)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(password.Trim(), "SHA1");
        }

        public static string MultipleLineText2SingleLineText(string text)
        {
            StringBuilder sb = new StringBuilder(text);

            sb.Replace("\n", " ");
            sb.Replace("\t", " ");
            sb.Replace("\r", " ");
            sb.Replace("  ", " ");
            sb.Replace("  ", " ");
            sb.Replace("  ", " ");

            return sb.ToString();
        }

        /// <summary>
        /// Get the application startup folder. If started from within Visual Studio,
        /// go up from the bin\Debug or bin\Release path.
        /// </summary>
        /// <param name="startupPath"></param>
        /// <param name="subDir"></param>
        /// <returns></returns>
        public static string GetAppSubDir(string startupPath, string subDir)
        {
            string path = startupPath;
            int index = path.LastIndexOf(Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar + "release");

            if (index == -1)
            {
                index = path.LastIndexOf(Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar + "debug");
            }
            if (index != -1)
            {
                path = path.Substring(0, index);
            }
            if (subDir.Length > 0)
            {
               path += Path.DirectorySeparatorChar + subDir;
            }
            return path;
        }

        /// <summary>
        /// Convert a last name to a name suitable for logging on.
        /// The logon name may only contain English characters a-z and 0-9.
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public static string LastName2LogOnName(string lastName)
        {
            string old = lastName.ToLower(CultureInfo.InvariantCulture);
            string newName = "";

            // alle Buchstaben in klein, keine Umlaute usw
            for (int i = 0; i < old.Length; i++)
            {
                if ("abcdefghijklmnopqrstuvwxyz1234567890".IndexOf(old[i]) != -1)
                {
                    newName += old[i];
                }
                else if (old[i] == 'ä')
                {
                    newName += "ae";
                }
                else if (old[i] == 'ö')
                {
                    newName += "oe";
                }
                else if (old[i] == 'ü')
                {
                    newName += "ue";
                }
                else if (old[i] == 'ß')
                {
                    newName += "ss";
                }
                // ansonsten: nichts tun.
            }

            return newName;
        }

        /// <summary>
        /// Das größte Datum alles kleinsten Datümer von allen Datenbanken.
        /// MySql:      '1000-01-01 00:00:00'
        /// SqlServer:  
        /// Access:
        /// </summary>
        static public DateTime DatabaseDateTimeMinValue
        {
            get { return new DateTime(1753, 1, 1, 0, 0, 0); }
        }
        /// <summary>
        /// Das kleinste aller Maxima von allen Datenbanken.
        /// MySql:      '9999-12-31 23:59:59'
        /// SqlServer:  
        /// Access:
        /// </summary>
        static public DateTime DatabaseDateTimeMaxValue
        {
            get { return new DateTime(9999, 12, 31, 23, 59, 59); }
        }

        /// <summary>
        ///  Liefert true wenn min &lt;= n &lt;= max
        /// und false sonst
        /// </summary>
        /// <param name="text">Eine Zahl als Text</param>
        /// <param name="min">Das Minimum inklusive</param>
        /// <param name="max">Das Maximum inklusive</param>
        /// <returns></returns>
        public static bool IsIntBetween(string text, int min, int max)
        {
            bool success = false;

            int n;
            if (Int32.TryParse(text, out n))
            {
                if (min <= n && n <= max)
                {
                    success = true;
                }
            }

            return success;
        }

        public static void GetOperationSystem(out System.PlatformID platform, out int major, out int minor)
        {
            System.OperatingSystem osInfo = System.Environment.OSVersion;

            platform = osInfo.Platform;
            major = osInfo.Version.Major;
            minor = osInfo.Version.Minor;
        }

        /// <summary>
        /// Windows XP Home Edition, Windows XP Professional x64 Edition, Windows Server 2003 Platform 
        /// Siehe http://support.microsoft.com/kb/304283/de
        ///                     Major   Minor
        /// Visa Home Basic     6       0
        /// </summary>
        /// <returns>true, wenn das OS den MArquee style supported</returns>
        public static bool ProgressBarMarqueeSupported()
        {
            // Marquee erst ab XP
            //              95  98  Me      NT4.0   2000    XP  Vista
            // Plattform-ID  1  1   1       2       2       2   2
            // Hauptversion  4  4   4       4       5       5   6
            // Nebenversion  0  10  90      0       0       1   0

            bool success = false;

            System.OperatingSystem osInfo = System.Environment.OSVersion;

            if (osInfo.Platform == System.PlatformID.Win32NT)
            {
                if (
                    (osInfo.Version.Major >= 5 && osInfo.Version.Minor >= 1)
                    ||
                    (osInfo.Version.Major >= 6))
                {
                    success = true;
                }
            }

            return success;
        }

        /// <summary>
        /// Setzt als Style Marquee, wenn die Windows Version eine der folgenden ist:
        /// Windows XP Home Edition, Windows XP Professional x64 Edition, Windows Server 2003 Platform 
        /// Siehe http://support.microsoft.com/kb/304283/de
        /// Siehe http://msdn2.microsoft.com/de-de/library/system.windows.forms.progressbarstyle(VS.80).aspx
        /// Marquee erst ab Windows XP
        /// Der Marquee-Stil ist nur gültig, wenn visuelle Stile aktiviert wurden.
        /// Der Continuous-Stil ist nur gültig, wenn visuelle Stile nicht aktiviert wurden.
        /// </summary>
        /// <param name="progressBar"></param>
        public static void SetProgressBarStyleMarqueeOnSupportedPlatforms(ProgressBar progressBar)
        {
            //if (ProgressBarMarqueeSupported())
            {
                // Windows XP und danach!. Wenn nicht supported, wird Blocks gesetzt.
                progressBar.Style = ProgressBarStyle.Marquee;
                progressBar.MarqueeAnimationSpeed = 2000;
            }
        }

        /// <summary>
        /// Return the number of lines in a file using a given encoding.
        /// </summary>
        /// <param name="encoding">Encoding of the file</param>
        /// <param name="fileName">Full path and file name</param>
        /// <returns></returns>
        public static int CountLines(Encoding encoding, string fileName)
        {
            int count = 0;
            StreamReader reader = null;
            string line;

            try
            {
                // Hier ist das Encoding egal, weil nur die Zeilen gezählt werden sollen
                reader = new StreamReader(fileName, encoding);
                do
                {
                    line = reader.ReadLine();
                    count++;
                } while (line != null);
            }
            catch
            {
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return count;
        }

        public static bool DeleteFile(string fileName)
        {
            bool success = true;

            if (!System.IO.File.Exists(fileName))
            {
                goto Exit;
            }

            try
            {
                File.Delete(fileName);
            }
            catch
            {
                try
                {
                    // Kopieren hat nicht geklappt, evtl. ist die Datei schreibgeschützt, 
                    // also den Schreibschutz entfernen.
                    System.IO.File.SetAttributes(fileName, FileAttributes.Normal);
                    System.IO.File.Delete(fileName);
                }
                catch
                {
                    success = false;
                }
                
            }
        Exit: 
            return success;
        }

        static public string GetTempDirectoryName()
        {
            string path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            Directory.CreateDirectory(path);

            return path;
        }

        /// <summary>
        /// Write a string as Java modified UTF8 string
        /// Write the length as two bytes in big endian first, and then the string bytes as UTF8
        /// </summary>
        /// <param name="line">The text to write, the length in UTF8 bytes must fit into 2 bytes</param>
        static public void WriteJavaModifiedUtf8(BinaryWriter writer, string line)
        {
            byte[] utf8Bytes = System.Text.Encoding.UTF8.GetBytes(line);

            if (utf8Bytes.Length >= Int16.MaxValue)
            {
                throw new Exception("Cannot write Java-modified-UTF8 string of length >= " + Int16.MaxValue);
            }

            //
            // write length in big endian
            // int length = 0x12345678
            //
            // big endian:      0x12 0x34 0x56 0x78
            // little endian    0x78 0x56 0x34 0x12
            //
            //  BitConverter.GetBytes(length) will return 4 bytes which are ordered
            //  depending on the Endianess of the system
            //
            //  arLength    little endian   big endian
            //  0           0x78            0x12
            //  1           0x56            0x34
            //  2           0x34            0x56
            //  3           0x12            0x78
            //
           byte[] arLength = System.BitConverter.GetBytes(utf8Bytes.Length);

            //
            // As we can only write two bytes, our max string length must be 16 bit
            // so int length from the example above is 0x00005678
            // and we need to write byte 0x56 and then byte 0x78
            //
            if (BitConverter.IsLittleEndian)
            {
                //
                //               0  1  2  3 
                // 0x00005678 -> 78 56 00 00
                //
                writer.Write(arLength[1]);
                writer.Write(arLength[0]);
            }
            else
            {
                //
                //               0  1  2  3
                // 0x00005678 -> 00 00 56 78
                //
                writer.Write(arLength[2]);
                writer.Write(arLength[3]);
            }

            writer.Write(utf8Bytes);
        }

        static public string ReadJavaModifiedUtf8(BinaryReader reader)
        {
            string line = null;

            try
            {
                byte high = reader.ReadByte();
                byte low = reader.ReadByte();
                int length = high * 256 + low;

                byte[] utf8Bytes = reader.ReadBytes(length);
                line = System.Text.Encoding.UTF8.GetString(utf8Bytes);
            }
            catch (Exception exc)
            {
                Trace.WriteLine(exc.ToString());
            }

            return line;
        }

        public static bool IsUserAdministrator(bool silent)
        {
            bool isAdmin = false;

            try
            {
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (Exception ex)
            {
                isAdmin = false;
                if (!silent)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return isAdmin;
        }

        public static int ConvertToInt32(string text)
        {
            int value;

            if (!Int32.TryParse(text, out value))
            {
                value = 0;
            }

            return value;
        }

        public static int EnsureIntValue(int value, int min, int max)
        {
            if (value < min)
            {
                value = min;
            }
            else if (value > max)
            {
                value = max;
            }

            return value;
        }

        public static byte EnsureByteValue(byte value, byte min, byte max)
        {
            if (value < min)
            {
                value = min;
            }
            else if (value > max)
            {
                value = max;
            }

            return value;
        }

        public static void CheckQuelleCorrectDumbSettings(CheckBox chkIntern, CheckBox chkExtern)
        {
            if (!chkExtern.Checked && !chkIntern.Checked)
            {
                chkExtern.Checked = chkIntern.Checked = true;
            }
        }

        public static bool CheckUserInputOne(System.Web.UI.WebControls.TextBox textBox, System.Web.UI.WebControls.Label errorLabel)
        {

            bool success = true;

            if (textBox.Text.Length == 0)
            {
                errorLabel.Text = "Bitte ausgefüllen";
                errorLabel.Visible = true;
                success = false;
            }

            if (!AllowedInLicenseFileXml(textBox.Text))
            {
                errorLabel.Text = "Es sind keine Sonderzeichen erlaubt";
                errorLabel.Visible = true;
                success = false;
            }

            return success;
        }

        public static bool AllowedInLicenseFileXml(string text)
        {
            bool success = true;

            foreach (char c in text)
            {
                if (AllowedXmlLicenseFileChars.IndexOf(c) == -1)
                {
                    success = false;
                    break;
                }
            }

            return success;
        }

        public static string RemoveUnsafeLicenseFileXml(string rawText)
        {
            StringBuilder sb = new StringBuilder(rawText.Length);

            foreach (char c in rawText)
            {
                if (AllowedXmlLicenseFileChars.IndexOf(c) >= 0)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }
    }
}

