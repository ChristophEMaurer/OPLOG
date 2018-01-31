using System;
using System.IO;
using System.Windows.Forms;

namespace Operationen
{
    /// <summary>
    /// 
    /// This format is used by an export from ORBIS
    /// 
    /// Use this class to import operations/surgeons from a plain unicode text file
    /// The exact format is described below.
    /// When you copy your assembly into the plugin folder, the main program will display your assembly.
    /// If selected by the user, the main program will create 
    /// an instance of the type OperationenImportPlugin, which needs to be
    /// in your assembly.
    /// The main program then calls the virtual functions as defined in class OperationenImport,
    /// by overriding these functions you can access any given data source 
    /// and pass the data into the main application.
    /// </summary>
    public partial class OperationenImportPlugin : OperationenImport
	{
        private string _line;
        private StreamReader _inFile;
        private string _fileName = "";
        private bool inOpDatum = false;
        private bool inIcmpCode = false;
        private bool inBeteiligte = false;

        /// <summary>
        /// One instance only. Clear and set data instead of
        /// creating a new instance over and over again.
        /// </summary>
        private OperationenImportEvent _oEvent;

        /// <summary>
        /// Here you must return a text that sufficiently describes your plugin.
        /// This text will be displayed by the main program.
        /// </summary>
        /// <returns>The text to display</returns>
        public override string OPImportDescription()
        {
            string s = string.Format("Dieses Import-Plugin importiert Operationen aus einer Text-Datei mit Codierung '{0}'."
                + "\r\nÜberprüfen Sie nach dem Datenimport die Operationen und die Umlaute."
                + "\r\n\r\nFreitext in folgendem Format, wobei beliebig viele von diesen Einträgen folgen können:\r\n\r\n" +

@"OP-Datum ; Fachabteilung ; OP-Buch-Nr.; Saal ; Fallnummer ; Patient ; Geb.-Dat.
01.01.2008; Gefäßchirurgie; 64; ZOP 3; 1234567; Maurer, Christoph; 14.05.1965

Durchgeführte Eingriffe
Y-Prothese aorto-bifemoral bei AVK, auch unifemoral oder iliacal

ICD-Code ; ICD-Text ; Seite ; H/N ; durch
I70.23; Atherosklerose der Extremitätenarterien: Becken-Bein-Typ, mit Ulzeration; R; H; Mustermann

ICPM-Code ; ICPM-Text ; H/N ; durch 
5-393.36; Anlegen eines anderen Shuntes und Bypasses an Blutgefäßen: Aorta: Aortofemoral; H; Mustermann

Funktion ; Beteiligte
1. Operateur; Montgomery, Jesse
2. Operateur; Greenburg, Chester
3. Operateur; Boner, Christie
1. Assistent; Zoltan, Z
2. Assistent; Wanda, W
1. Instrumentierer; Wilma, W
1. Springer; Nelson, N
1. Schleuser; Zarnoff, Z
1. Anästhesist; Zelmina , Z
1. Anästhesiepflege; Zellner , Z


"
                    , FormatDescription())
            ;

            return s;
        }

        /// <summary>
        /// This function is called before OPImportInit() once at the beginning of the import.
        /// </summary>
        public override void OPImportInit(OperationenImportPluginCustomData customData)
        {
            if (customData != null)
            {
                _fileName = customData.DataSource;
            }
            _oEvent = new OperationenImportEvent();
        }

        /// <summary>
        /// This does the actual import.
        /// </summary>
        public override void OPImportRun()
        {
            if (_fileName.Length == 0)
            {
                OpenFileDialog dlg = new OpenFileDialog();
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    DoOperationenImport(dlg.FileName);
                }
            }
            else
            {
                // Es wurde ein Dateiname übergeben. Diese Datei importieren und
                // alles ohne Benutzerinteraktion
                DoOperationenImport(_fileName);
            }
        }

        /// <summary>
        /// This function is called after OPImportRun().
        /// We do nothing here.
        /// </summary>
        public override void OPImportFinalize()
        {
        }


        /// <summary>
        /// Convert a Date string a C# DateTime.
        /// The format is "DD.MM.YY|YYYY" with
        /// Examples: "08.04.08", 01.02.1970
        /// Two digit years: 
        ///     0 - 70  -> 2000-2070
        ///    71 - ... -> 1971-...
        /// </summary>
        /// <param name="strDateTime">A text string representing a date and a time</param>
        /// <returns></returns>
        private bool Date2DateTime(string strDateTime, ref DateTime? dt)
        {
            bool fSuccess = false;

            dt = null;

            try
            {
                //
                //  0        
                // "08.04.08"
                //
                string[] arDate = strDateTime.Split('.');
                int nDay;
                int nMonth;
                int nYear;

                //
                // 0 1 2    
                // 8.4.2008
                //
                if (arDate.Length != 3)
                {
                    goto _exit;
                }

                //
                // Year can be 2 or 4 digits.
                //
                if (arDate[2].Length != 2 && arDate[2].Length != 4)
                {
                    goto _exit;
                }

                if (!Int32.TryParse(arDate[2], out nYear))
                {
                    goto _exit;
                }

                if (arDate[2].Length == 2)
                {
                    if (nYear > 70)
                    {
                        nYear += 1900;
                    }
                    else
                    {
                        nYear += 2000;
                    }
                }

                if (Int32.TryParse(arDate[0], out nDay) && Int32.TryParse(arDate[1], out nMonth))
                {
                    dt = new DateTime(nYear, nMonth, nDay, 10, 0, 0);
                    fSuccess = true;
                }
            }
            catch
            {
            }
        _exit:

            if (dt == null)
            {
                fSuccess = false;
            }

            return fSuccess;
        }

        private bool ReadLine()
        {
            _line = _inFile.ReadLine();
            if (!String.IsNullOrEmpty(_line))
            {
                _line = _line.Trim();
            }

            return (_line != null);
        }

        /// <summary>
        /// We open a file, read in the lines, split the lines
        /// and fire the ImportOP event once for every operation
        /// that we want to import.
        /// </summary>
        /// <param name="strFilename"></param>
        private void DoOperationenImport(string strFilename)
        {
            if (File.Exists(strFilename))
            {
                try
                {
                        _inFile = new StreamReader(strFilename, GetEncoding());

                        ReadFile();
                }
                catch (Exception e)
                {
                    string strErrorText = e.Message;

                    if (e.InnerException != null)
                    {
                        strErrorText += "-" + e.InnerException;
                    }

                    MessageBox.Show(strErrorText);

                    _oEvent.ClearData();
                    _oEvent.State = EVENT_STATE.STATE_ERROR;
                    _oEvent.StateText = strErrorText;

                    this.FireImportOPEvent(_oEvent);
                }
                finally
                {
                    if (_inFile != null)
                    {
                        _inFile.Close();
                        _inFile.Dispose();
                        _inFile = null;
                    }
                }
            }
        }

        private bool FunctionText2Code(string strFunktion, ref OP_FUNCTION function)
        {
            bool fSuccess = false;

            try
            {
                if (!string.IsNullOrEmpty(strFunktion))
                {
                    if (strFunktion == "1. Operateur")
                    {
                        function = OP_FUNCTION.OP_FUNCTION_OP;
                        fSuccess = true;
                    }
                    else if (strFunktion == "2. Operateur")
                    {
                        function = OP_FUNCTION.OP_FUNCTION_OP;
                        fSuccess = true;
                    }
                    else if (strFunktion == "1. Assistent")
                    {
                        function = OP_FUNCTION.OP_FUNCTION_ASS;
                        fSuccess = true;
                    }
                    else if (strFunktion == "2. Assistent")
                    {
                        function = OP_FUNCTION.OP_FUNCTION_ASS2;
                        fSuccess = true;
                    }
                }
            }
            catch
            {
            }

            return fSuccess;
        }


        private void HandleBeteiligter(
            string strFallzahl,
            string strNachname,
            string strVorname,
            OP_FUNCTION function,
            DateTime dtBeginn,
            DateTime dtEnde,
            string strOpsKode,
            string strOpsText
            )
        {
            if (strNachname.Length > 0)
            {
                _oEvent.State = EVENT_STATE.STATE_DATA;
                _oEvent.Identifier = strFallzahl;
                _oEvent.SurgeonLastName = strNachname;
                _oEvent.SurgeonFirstName = strVorname;
                _oEvent.OPDateAndTime = dtBeginn;
                _oEvent.OPTimeEnd = dtEnde;
                _oEvent.OPCode = strOpsKode;
                _oEvent.OPDescription = strOpsText;
                _oEvent.OPFunction = function;

                FireImportOPEvent(_oEvent);
            }
        }


        private bool NameToNachnameVorname(string name, ref string nachname, ref string vorname)
        {
            bool success = false;

            try
            {
                string[] arName = name.Split(',');

                if (arName.Length == 2)
                {
                    nachname = arName[0].Trim();
                    vorname = arName[1].Trim();
                }

                success = true;
            }
            catch
            {
                success = false;
            }
            return success;
        }

        /// <summary>
        /// Use this function to retrive a value which may NOT be empty
        /// </summary>
        /// <param name="arLine">the current line as an array</param>
        /// <param name="index">index of column into arLine</param>
        /// <param name="value">the value to extract</param>
        /// <param name="line">the line as a string</param>
        /// <param name="columnDisplayName">User readable name of the column</param>
        /// <param name="columnName">Possible diffrent, technical name for this column</param>
        /// <returns></returns>
        private bool ReadColumn(string[] arLine, int index, ref string value, string line, string columnDisplayName, string columnName)
        {
            bool success = false;

            try
            {
                value = arLine[index].Trim();
                if (string.IsNullOrEmpty(value))
                {
                    // Bad field
                    _oEvent.State = EVENT_STATE.STATE_INFO;
                    _oEvent.StateText = string.Format("Der Wert für Spalte {0} ('{1}') ist leer in Zeile {2}", columnDisplayName, columnName, line);
                    FireImportOPEvent(_oEvent);
                }
                else
                {
                    success = true;
                }
            }
            catch
            {
            }

            return success;
        }

        private void Reset()
        {
            inOpDatum = false;
            inIcmpCode = false;
            inBeteiligte = false;
        }

        private void ReadFile()
        {
            string strDatum = "";
            DateTime? dtDatum = null;
            string fallnummer = "";
            string funktion = "";
            string beteiligter = "";
            string nachname = "";
            string vorname = "";
            string opsCode = "";
            string opsText = "";

            while (ReadLine())
            {
                if (_line.Length == 0)
                {
                    continue;
                }

                if (!inOpDatum && _line.IndexOf("OP-Datum") == 0)
                {
                    if (inIcmpCode)
                    {
                        Error("Found 'OP-Datum' in the current line. There was a 'ICMP-Code' in a previous line which is not allowed.");
                        Reset();
                        continue;
                    }
                    if (inBeteiligte)
                    {
                        Error("Found 'OP-Datum' in the current line. There was a 'Funktion;Beteiligte' in a previous line which is not allowed.");
                        Reset();
                        continue;
                    }
                    ReadLine();
                    if (string.IsNullOrEmpty(_line))
                    {
                        Error("The line following the previous line with 'OP-Datum' is empty which is not allowed.");
                        Reset();
                        continue;
                    }

                    string[] arLine = _line.Split(';');
                    if (arLine.Length != 7)
                    {
                        Error("Expected a line with 7 columns with format '<Datum>;2;3;4;5;<Fallnummer>' in this line.");
                        Reset();
                        continue;
                    }

                    if (!ReadColumn(arLine, 0, ref strDatum, _line, "OP-Datum", "OP-Datum"))
                    {
                        Reset();
                        continue;
                    }
                    if (!Date2DateTime(strDatum, ref dtDatum))
                    {
                        Reset();
                        continue;
                    }
                    if (!ReadColumn(arLine, 4, ref fallnummer, _line, "Fallnummer", "Fallnummer"))
                    {
                        Reset();
                        continue;
                    }

                    inOpDatum = true;
                    inIcmpCode = false;
                    inBeteiligte = false;
                }
                if (!inIcmpCode && _line.IndexOf("ICPM-Code") == 0)
                {
                    if (!inOpDatum)
                    {
                        Error("Found 'ICMP-Code' at the beginning of this line. There was no previous line with 'OP-Datum' which is not allowed.");
                        Reset();
                        continue;
                    }
                    if (inBeteiligte)
                    {
                        Error("Found 'ICMP-Code' at the beginning of this line. There was no previous line with 'Funktion ; Beteiligte' which is not allowed.");
                        Reset();
                        continue;
                    }

                    ReadLine();
                    if (string.IsNullOrEmpty(_line))
                    {
                        Error("Line following the line with 'ICPM-Code' is empty which is not allowed.");
                        Reset();
                        continue;
                    }
                    inIcmpCode = true;
                    {
                        string[] arLine = _line.Split(';');
                        if (arLine.Length != 4)
                        {
                            Error("Expected a line 4 columns and format in this line, found " + arLine.Length);
                            Reset();
                            continue;
                        }
                        if (!ReadColumn(arLine, 0, ref opsCode, _line, "ICPM-Code", "ICPM-Code"))
                        {
                            Reset();
                            continue;
                        }
                        if (!ReadColumn(arLine, 1, ref opsText, _line, "ICPM-Text", "ICPM-Text"))
                        {
                            Reset();
                            continue;
                        }
                    }
                }
                if (!inBeteiligte && _line.IndexOf("Funktion") == 0)
                {
                    if (!inOpDatum)
                    {
                        Error("Found 'Funktion' at the beginning of a line when there was no 'OP-Datum' in a previous line");
                        Reset();
                        continue;
                    }
                    if (!inIcmpCode)
                    {
                        Error("Found 'Funktion' at the beginning of a line when there was no 'ICMP-Code' in a previous line");
                        Reset();
                        continue;
                    }

                    inBeteiligte = true;
                    while (true)
                    {
                        OP_FUNCTION opFunction = OP_FUNCTION.OP_FUNCTION_OP;

                        ReadLine();
                        if (string.IsNullOrEmpty(_line))
                        {
                            Reset();
                            break;
                        }
                        string[] arLine = _line.Split(';');
                        if (arLine.Length != 2)
                        {
                            Error("Expected 2 colums separated by ';' in line, found " + arLine.Length);
                            continue;
                        }
                        if (!ReadColumn(arLine, 0, ref funktion, _line, "Funktion", "Funktion"))
                        {
                            continue;
                        }
                        if (!ReadColumn(arLine, 1, ref beteiligter, _line, "Beteiligte", "Beteiligte"))
                        {
                            continue;
                        }

                        if (!FunctionText2Code(funktion, ref opFunction))
                        {
                            continue;
                        }
                        if (!NameToNachnameVorname(beteiligter, ref nachname, ref vorname))
                        {
                            continue;
                        }

                        HandleBeteiligter(fallnummer, nachname, vorname, opFunction, dtDatum.Value, dtDatum.Value, opsCode, opsText);
                    }
                }
            }
        }

        private void Error(string text)
        {
            MessageBox.Show("Error in line " + _line + ":" + text);
        }
    }
}
