/*
 * OperationenImportPlugin - import operations/surgeons from a plain text file
 * Source code from OP-LOG
 * 
 * Copyright Christoph Maurer, D-61184 Karben
 * http://www.op-log.de
 * 
 */


using System;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;


namespace Operationen
{
    /// <summary>
    /// This format is used by an export from MCC ISOP
    /// </summary>
    public partial class OperationenImportPlugin : OperationenImport
	{
        private string _fileName = "";
        private bool _inQuote = false;

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
                + "\r\n\r\n15 oder mehr Spalten:"
                + "\r\n   1       2       3       4      5      6       7       8       9       10      11      12      13         14       15"
                + "\r\n<dummy>,<dummy>,<dummy>,<Name>,<dummy>,<dummy>,<dummy>,<dummy>,<dummy>,<dummy>,<Datum>,<OP-Nr>,<OPS-Kode>,OPS-Text,<Funktion>,..."
                + "\r\n\r\nFormat einer einzelnen Zeile:"
                + "\r\n\"OP-Nachweis  - alle Therapien  - chronologisch \",\"Stand: 01.10.2009\",\"im Zeitraum vom  01.09.2008  bis  01.10.2009\",\"für Max Mustermann \",\"zur Vorlage nach Ausbildungsordnung\",\"Datum\",\"OP-Nr\",\"Code\",\"Therapie\",\"Funktion\",09.09.2008  00:00:00,\"0802464\",\"5-893.18 (OPSV2008)\",\"Chirurgische Wundtoilette [Wunddebridement] und Entfernung von erkranktem Gewebe an Haut und Unterhaut: Großflächig: Unterarm\",\"Operateur\",..."
                + "\r\n\r\n<dummy>   : diese Einträge werden ignoriert"
                + "\r\n<Name>    : \"für Vorname Nachname\""
                + "\r\n<Datum>   : DD.MM.YYYY  HH:MM:SS"
                + "\r\n<OP-Nr>   : \"0802464\""
                + "\r\n<OPS-Kode>: \"5-893.18\""
                + "\r\n<Funktion>: \"Operateur\", \"Assistent\""

                + "\r\n", FormatDescription())
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
        /// We open a file, read in the lines, split the lines
        /// and fire the ImportOP event once for every operation
        /// that we want to import.
        /// </summary>
        /// <param name="strFilename"></param>
        private void DoOperationenImport(string strFilename)
        {
            if (File.Exists(strFilename))
            {
                StreamReader sr = null;
                string strLine = "";

                try
                {
                    sr = new StreamReader(strFilename, GetEncoding());

                    do
                    {
                        strLine = ReadLine(sr);
                        if (strLine != null)
                        {
                            ImportLine(strLine);
                        }
                    } while (strLine != null && !_oEvent.Abort);
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
                    if (sr != null)
                    {
                        sr.Close();
                        sr.Dispose();
                        sr = null;
                    }
                }
            }
        }

        private bool BeginnUndEnde2DateTime(
            string strDatumZeit,
            string strLine,
            ref DateTime dtBeginn
            )
        {
            bool success = false;

            DateTime? dtDatumZeitBeginn = null;

            if (DateAndTime2DateTime(strDatumZeit, ref dtDatumZeitBeginn))
            {
                dtBeginn = new DateTime(dtDatumZeitBeginn.Value.Year, dtDatumZeitBeginn.Value.Month, dtDatumZeitBeginn.Value.Day,
                        dtDatumZeitBeginn.Value.Hour, dtDatumZeitBeginn.Value.Minute, 0);

                success = true;
            }
            else
            {
                // unexpected format for date time
                _oEvent.State = EVENT_STATE.STATE_INFO;
                _oEvent.StateText = string.Format("Unerwartetes Datumsformat in Feld 'Datum' ('{0}'), erwartet wurde 'DD.MM.YYYY  HH:MM:SS', Datenzeile wird ignoriert: {1}",
                    strDatumZeit, strLine);
                FireImportOPEvent(_oEvent);
            }

            return success;
        }

        private bool ParseName(string name, out string nachname, out string vorname)
        {
            bool success = false;
            string[] arName = name.Split(' ');
            nachname = "";
            vorname = "";

            //
            // "Für Christoph Maurer"
            //
            if (arName.Length > 2)
            {
                nachname = arName[2];
                vorname = arName[1];
                success = true;
            }

            return success;
        }

        private bool ParseOpsCode(string text, out string opsCode)
        {
            bool success = false;
            string[] arText = text.Split(' ');
            opsCode = "";

            //
            // "5-893.1f (OPSV2008)"
            //
            if (arText.Length > 1)
            {
                opsCode = arText[0];
                success = true;
            }

            return success;
        }

        /// <summary>
        /// Convert a DateTime string a C# DateTime.
        /// The format is "DD.MM.YYYY  HH.MM.SS"
        /// Example: "08.04.2008  07:56:00"
        /// </summary>
        /// <param name="strDateTime">A text string representing a date and a time</param>
        /// <returns></returns>
        private bool DateAndTime2DateTime(string strDateTime, ref DateTime? dt)
        {
            bool fSuccess = false;

            try
            {
                //
                //  0           n
                // "09.09.2008  00:00:00"
                //
                // Zwischen Datum und Uhrzeit sind zwei Leerzeichen!
                //
                // Erster Eintrag ist Datum 
                // Letzter Eintag ist Uhrzeit
                // dazwischen können beliebiege Leerzeichen sein.
                //
                string[] arDateTime = strDateTime.Split(' ');

                if (arDateTime.Length > 1)
                {
                    // Erster Element ist Datum
                    string[] arDate = arDateTime[0].Split('.');
                    {
                        //
                        // 0  1  2    
                        // 08.04.2008
                        //
                        if (arDate.Length == 3)
                        {
                            int nDay;
                            int nMonth;
                            int nYear;

                            //
                            // Year must be 4 digits.
                            //
                            if (arDate[2].Length == 4 &&
                                Int32.TryParse(arDate[0], out nDay)
                                && Int32.TryParse(arDate[1], out nMonth)
                                && Int32.TryParse(arDate[2], out nYear))
                            {
                                // Letzte Element ist Zeit
                                string[] arTime = arDateTime[arDateTime.Length - 1].Split(':');
                                {
                                    //
                                    // 0  1  2
                                    // 07:56:00
                                    //
                                    if (arTime.Length == 3)
                                    {
                                        int nHour;
                                        int nMinute;
                                        int nSeconds;

                                        if (Int32.TryParse(arTime[0], out nHour)
                                            && Int32.TryParse(arTime[1], out nMinute)
                                            && Int32.TryParse(arTime[2], out nSeconds)
                                            )
                                        {
                                            dt = new DateTime(nYear, nMonth, nDay, nHour, nMinute, nSeconds);
                                            fSuccess = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }

            return fSuccess;
        }

        private bool ReadColumn(string []arLine, int index, ref string value, string line, string columnName)
        {
            bool success = false;

            try
            {
                value = arLine[index];
                if (string.IsNullOrEmpty(value))
                {
                    // Bad field
                    _oEvent.State = EVENT_STATE.STATE_INFO;
                    _oEvent.StateText = string.Format("Der Wert für Spalte '{0}' ist leer in Zeile {1}", columnName, line);
                    FireImportOPEvent(_oEvent);
                }
                else
                {
                    if (value.StartsWith("\"") && value.EndsWith("\""))
                    {
                        // 
                        // 01234567890
                        // "Operateur" -> Operateur
                        value = value.Substring(1, value.Length - 2);
                    }
                    success = true;
                }
            }
            catch
            {
            }

            return success;
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

        /// <summary>
        /// Convert the text of function to one of the OP_FUNCTION values
        /// </summary>
        /// <param name="strFunktion">Text of field <FKTBEZ></param>
        /// <param name="function">the matching function</param>
        /// <returns>true if the text is a valid function text and could be mapped, false in any other case</returns>
        private bool FunctionText2Code(string strFunktion, ref OP_FUNCTION function)
        {
            bool fSuccess = false;

            try
            {
                if (!string.IsNullOrEmpty(strFunktion))
                {
                    if (strFunktion == "Operateur")
                    {
                        function = OP_FUNCTION.OP_FUNCTION_OP;
                        fSuccess = true;
                    }
                    else if (strFunktion == "Assistent")
                    {
                        function = OP_FUNCTION.OP_FUNCTION_ASS;
                        fSuccess = true;
                    }
                }
            }
            catch
            {
            }

            return fSuccess;
        }

        /// <summary>
        /// Some lines end with a 0x0a, some lines end with 0x0a0d
        /// We skip all 0x0a and create a line when we encounter a 0x0d
        /// </summary>
        /// <param name="sr"></param>
        /// <returns>A line terminated by 0x0d or null if nothing left to read</returns>
        private string ReadLine(StreamReader sr)
        {
            string line = null;
            StringBuilder sb = new StringBuilder();
            int c;

            while (!sr.EndOfStream)
            {
                c = sr.Read();
                if (c == 13)
                {
                    break;
                }
                else if (c == 10)
                {
                    continue;
                }
                sb.Append((char) c);
            }

            if (sb.Length > 0)
            {
                line = sb.ToString();
            }

            return line;
        }

        /// <summary>
        /// The column separator is comma (,), text columns are enclosed by double quotes (")
        /// and may contain the separator.
        /// Therefore we must parse the line and split only if we are not inside a text value.
        /// The Quote can span multiple lines.
        /// </summary>
        /// <param name="line">The entire line</param>
        /// <param name="separator">The separator</param>
        /// <param name="inQuote">Are we insiee of a Quoted string "..."</param>
        /// <returns>An array containing all tokens separated by the specified separator</returns>
        private List<string> SplitLine(string line, char separator, ref bool inQuote)
        {
            List<string> list = new List<string>();
            int i = 0;
            int indexFrom = 0;
            string token;

            while (i < line.Length)
            {
                if (line[i] == '"')
                {
                    inQuote = !inQuote;
                }

                else if (line[i] == separator)
                {
                    if (!inQuote)
                    {
                        token = line.Substring(indexFrom, i - indexFrom);
                        list.Add(token);
                        indexFrom = i + 1;
                    }
                }

                i++;
            }

            token = line.Substring(indexFrom);
            list.Add(token);

            return list;
        }


        /// <summary>
        /// Process one line of the text file. All indices are 0-based, except in user-messages, where they start with 1.
        /// </summary>
        /// <param name="strLine"></param>
        private void ImportLine(string strLine)
        {
            _oEvent.ClearData();
            _oEvent.State = EVENT_STATE.STATE_DATA;
            _oEvent.StateText = "";

            //
            // This is the format containing the operation/surgeons: 
            //
            // 1     2     3     4                        5     6     7     8     9     10    11    12    13       14       15"
            // dummy,dummy,dummy,\"für Vorname Nachname\",dummy,dummy,dummy,dummy,dummy,dummy,Datum,OP-Nr,OPS-Kode,OPS-Text,Funktion,..."
            //
            // Samle:
            // "OP-...ch","Stand: 01.10.2009","im Ze...09","für Max Mustermann","zur Vorlage nach Ausbildungsordnung","Datum\",\"OP-Nr\",\"Code\",\"Therapie\",\"Funktion\",09.09.2008  00:00:00,\"0802464\",\"5-893.18 (OPSV2008)\",\"Chirurgische Wundtoilette [Wunddebridement] und Entfernung von erkranktem Gewebe an Haut und Unterhaut: Großflächig: Unterarm\",\"Operateur\",..."
            // 
            //
            List<string> list = SplitLine(strLine, ',', ref _inQuote);
            string[] arLine = list.ToArray();
            //
            // There are only 9 columns, but the last one ends with a separator, so split() returns 10
            if (arLine.Length < 15)
            {
                // Not 9 columns
                _oEvent.State = EVENT_STATE.STATE_INFO;
                _oEvent.StateText = "Weniger als 15 Spalten in Datenzeile, Datenzeile wird ignoriert:" + Environment.NewLine + strLine;
                FireImportOPEvent(_oEvent);
                goto _exit;
            }

            // Gesamt Operation: Datum und Beginn - Ende
            string strFallzahl = "";
            string strDatum = "";
            string strOpsKodeRaw = "";
            string strOpsKode = "";
            string strOpsText = "";
            string strName = "";
            string strNachname = "";
            string strVorname = "";
            string strFunktion = "";
            OP_FUNCTION function = OP_FUNCTION.OP_FUNCTION_OP;

            DateTime? dtDatum = null;
            DateTime dtBeginn = DateTime.Now;
            DateTime dtEnde = DateTime.Now;

            if (!ReadColumn(arLine, 3, ref strName, strLine, "Name"))
            {
                goto _exit;
            }
            if (!ReadColumn(arLine, 10, ref strDatum, strLine, "Datum"))
            {
                goto _exit;
            }
            if (!ReadColumn(arLine, 11, ref strFallzahl, strLine, "OP-Nr"))
            {
                goto _exit;
            }
            if (!ReadColumn(arLine, 12, ref strOpsKodeRaw, strLine, "Code"))
            {
                goto _exit;
            }
            if (!ReadColumn(arLine, 13, ref strOpsText, strLine, "Therapie"))
            {
                goto _exit;
            }
            if (!ReadColumn(arLine, 14, ref strFunktion, strLine, "Funktion"))
            {
                goto _exit;
            }
            if (!ParseName(strName, out strNachname, out strVorname))
            {
                // unexpected format for date time
                _oEvent.State = EVENT_STATE.STATE_INFO;
                _oEvent.StateText = string.Format("Unerwartetes Format für Name in Spalte 3 ('{0}'), erwartet wird 'für Vorname Nachname', Datenzeile wird ignoriert: {1}",
                    strName, strLine);
                FireImportOPEvent(_oEvent);
                goto _exit;
            }

            if (!ParseOpsCode(strOpsKodeRaw, out strOpsKode))
            {
                // unexpected format for date time
                _oEvent.State = EVENT_STATE.STATE_INFO;
                _oEvent.StateText = string.Format("Unerwartetes Format für Ops-Kode in Spalte 12 ('{0}'), erwartet wird '5-787.36 (OPSV2008)', Datenzeile wird ignoriert: {1}",
                        strOpsKodeRaw, strLine);
                FireImportOPEvent(_oEvent);
                goto _exit;
            }
            if (!DateAndTime2DateTime(strDatum, ref dtDatum))
            {
                // unexpected format for date time
                _oEvent.State = EVENT_STATE.STATE_INFO;
                _oEvent.StateText = string.Format("Unerwartetes Format für Datum in Spalte 10 ('{0}'), erwartet wird 'DD.MM.YYYY  HH:MM:SS', Datenzeile wird ignoriert: {1}",
                        strDatum, strLine);
                FireImportOPEvent(_oEvent);
                goto _exit;
            }

            if (!FunctionText2Code(strFunktion, ref function))
            {
                // Bad <FKTBEZ> field
                _oEvent.State = EVENT_STATE.STATE_INFO;
                _oEvent.StateText = string.Format("Ungültiger Wert für Funktion in Spalte 14 ('{0}') , erwartet wird 'Operateur, Assistent', Datenzeile wird ignoriert: {1}",
                        strFunktion, strLine);
                FireImportOPEvent(_oEvent);
                goto _exit;
            }

            if (BeginnUndEnde2DateTime(strDatum, strLine, ref dtBeginn))
            {
                HandleBeteiligter(strFallzahl, strNachname, strVorname, function, dtBeginn, dtBeginn, strOpsKode, strOpsText);
            }

        _exit: ;

        }
    }
}

