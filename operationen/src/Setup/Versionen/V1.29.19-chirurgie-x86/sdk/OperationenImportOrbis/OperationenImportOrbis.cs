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
        private string _fileName = "";

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
                + "\r\n\r\n9 Spalten:"
                + "\r\nFormat einer einzelnen Zeile:"
                + "\r\n<FALLID>;<T31_OP_DATUM_1>;<BEGINN>;<ENDE>;<ICPM>;<ICPMTEXT>;<T50_NAME>;<T50_VORNAME>;<FKTBEZ>;"
                + "\r\n"
                + "\r\n<FALLID>             : [a-z,A-Z,0-9]"
                + "\r\n<T31_OP_DATUM_1>     : <Datum> <Zeit>"
                + "\r\n<BEGINN>             : <Datum> <Zeit>"
                + "\r\n<ENDE>               : <Datum> <Zeit>"
                + "\r\n<ICPM>               : <ICPM-Code>"
                + "\r\n<ICPMTEXT>           : <ICPM-Text>"
                + "\r\n<T50_NAME>           : <Nachname>"
                + "\r\n<T50_VORNAME>        : <Vorname>"
                + "\r\n<FKTBEZ>             : '1. Operateur' , '2. Operateur', '1. Assistent', '2. Assistent'"
                + "\r\n<Datum>              : [D,DD].[M,MM].[YYYY]"
                + "\r\n<Zeit>               : [HH:MM]"
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
                        strLine = sr.ReadLine();
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

        /// <summary>
        /// There are three dates in one import line. We use the dates <BEGINN> and <ENDE>.
        /// As these can be on different days, but our code only has one date and two times,
        /// we use date and time of <BEGINN> for dtBeginn and the date of <BEGINN> and time of <ENDE> for dtEnde.
        /// </summary>
        /// <param name="strTime"></param>
        /// <returns></returns>
        private bool BeginnUndEnde2DateTime(
            string strDatumZeitBeginn,
            string strDatumZeitEnde, 
            string strLine,
            ref DateTime dtBeginn,
            ref DateTime dtEnde
            )
        {
            bool success = false;

            DateTime? dtDatumZeitBeginn = null;
            DateTime? dtDatumZeitEnde = null;

            if (DateAndTime2DateTime(strDatumZeitBeginn, ref dtDatumZeitBeginn) &&
                DateAndTime2DateTime(strDatumZeitEnde, ref dtDatumZeitEnde))
            {
                dtBeginn = new DateTime(dtDatumZeitBeginn.Value.Year, dtDatumZeitBeginn.Value.Month, dtDatumZeitBeginn.Value.Day,
                        dtDatumZeitBeginn.Value.Hour, dtDatumZeitBeginn.Value.Minute, 0);

                dtEnde = new DateTime(dtDatumZeitBeginn.Value.Year, dtDatumZeitBeginn.Value.Month, dtDatumZeitBeginn.Value.Day,
                        dtDatumZeitEnde.Value.Hour, dtDatumZeitEnde.Value.Minute, 0);

                success = true;
            }
            else
            {
                // unexpected format for date time
                _oEvent.State = EVENT_STATE.STATE_INFO;
                _oEvent.StateText = string.Format("Unerwartetes Datumsformat in <BEGINN> ('{0}') oder <ENDE> ('{1}'), Datenzeile wird ignoriert: {2}",
                    strDatumZeitBeginn, strDatumZeitEnde, strLine);
                FireImportOPEvent(_oEvent);
            }

            return success;
        }


        /// <summary>
        /// Convert a DateTime string a C# DateTime.
        /// The format is "D|DD.M|MM.YYYY" with
        /// Example: "8.4.2008 07:56"
        /// </summary>
        /// <param name="strDateTime">A text string representing a date and a time</param>
        /// <returns></returns>
        private bool DateAndTime2DateTime(string strDateTime, ref DateTime? dt)
        {
            bool fSuccess = false;

            try
            {
                //
                //  0        1
                // "8.4.2008 07:56"
                //
                string []arDateTime = strDateTime.Split(' ');

                if (arDateTime.Length == 2)
                {
                    string[] arDate = arDateTime[0].Split('.');
                    {
                        //
                        // 0 1 2    
                        // 8.4.2008
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
                                string[] arTime = arDateTime[1].Split(':');
                                {
                                    //
                                    // 0  1
                                    // 07:56
                                    //
                                    if (arTime.Length == 2)
                                    {
                                        int nHour;
                                        int nMinute;

                                        if (Int32.TryParse(arTime[0], out nHour) && Int32.TryParse(arTime[1], out nMinute))
                                        {
                                            dt = new DateTime(nYear, nMonth, nDay, nHour, nMinute, 0);
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

        private bool ReadColumn(string []arLine, int index, ref string value, string line, string columnDisplayName, string columnName)
        {
            bool success = false;

            try
            {
                value = arLine[index];
                if (string.IsNullOrEmpty(value))
                {
                    // Bad <FKTBEZ> field
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
        /// Convert the text of field <FKTBEZ> to one of the OP_FUNCTION values
        /// </summary>
        /// <param name="strFunktion">Text of field <FKTBEZ></param>
        /// <param name="function">the matchin gfunction</param>
        /// <returns>true if the text is a valid function text and could be mapped, false in any other case</returns>
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


        /// <summary>
        /// Process one line of the text file. All indices are 0-based, except in user-messages, where they start with 1.
        /// </summary>
        /// <param name="strLine"></param>
        private void ImportLine(string strLine)
        {
            _oEvent.ClearData();
            _oEvent.State = EVENT_STATE.STATE_DATA;
            _oEvent.StateText = "";

            // This is the format containing the operation/surgeons: up to 3 OPS codes and up to three surgeons
            //
            // 0        1                2        3      4      5          6          7             8        9
            // <FALLID>;<T31_OP_DATUM_1>;<BEGINN>;<ENDE>;<ICPM>;<ICPMTEXT>;<T50_NAME>;<T50_VORNAME>;<FKTBEZ>;
            //
            String[] arLine = strLine.Split(';');

            //
            // There are only 9 columns, but the last one ends with a separator, so split() returns 10
            if (arLine.Length < 9)
            {
                // Not 9 columns
                _oEvent.State = EVENT_STATE.STATE_INFO;
                _oEvent.StateText = "Keine 9 Spalten in Datenzeile, Datenzeile wird ignoriert: " + strLine;
                FireImportOPEvent(_oEvent);
                goto _exit;
            }

            // Gesamt Operation: Datum und Beginn - Ende
            string strFallzahl = "";
            string strDatum = "";
            string strDatumBeginn = "";
            string strDatumEnde = "";
            string strOpsKode = "";
            string strOpsText = "";
            string strNachname = "";
            string strVorname = "";
            string strFunktion = "";
            OP_FUNCTION function = OP_FUNCTION.OP_FUNCTION_OP;

            DateTime? dtDummy = null;
            DateTime dtBeginn = DateTime.Now;
            DateTime dtEnde = DateTime.Now;

            if (!ReadColumn(arLine, 0, ref strFallzahl, strLine, "Fallzahl", "<FALLID>"))
            {
                goto _exit;
            }
            if (!ReadColumn(arLine, 1, ref strDatum, strLine, "Datum", "<T31_OP_DATUM_1>"))
            {
                goto _exit;
            }
            if (!ReadColumn(arLine, 2, ref strDatumBeginn, strLine, "Beginn-Datum", "<BEGINN>"))
            {
                goto _exit;
            }
            if (!ReadColumn(arLine, 3, ref strDatumEnde, strLine, "Ende-Datum", "<ENDE>"))
            {
                goto _exit;
            }
            if (!ReadColumn(arLine, 4, ref strOpsKode, strLine, "ICPM", "<ICPM>"))
            {
                goto _exit;
            }
            if (!ReadColumn(arLine, 5, ref strOpsText, strLine, "ICPM-Text", "<ICPMTEXT>"))
            {
                goto _exit;
            }
            if (!ReadColumn(arLine, 6, ref strNachname, strLine, "Nachname", "<T50_NAME>"))
            {
                goto _exit;
            }
            if (!ReadColumn(arLine, 7, ref strVorname, strLine, "Vorname", "<T50_VORNAME>"))
            {
                goto _exit;
            }
            if (!ReadColumn(arLine, 8, ref strFunktion, strLine, "Funktionsbezeichnung", "<FKTBEZ>"))
            {
                goto _exit;
            }

            if (!DateAndTime2DateTime(strDatum, ref dtDummy))
            {
                // unexpected format for date time
                _oEvent.State = EVENT_STATE.STATE_INFO;
                _oEvent.StateText = string.Format("Unerwartetes Datumsformat in <T31_OP_DATUM_1> ('{0}'), Datenzeile wird ignoriert: {1}",
                    strDatum, strLine);
                FireImportOPEvent(_oEvent);
                goto _exit;
            }

            if (!FunctionText2Code(strFunktion, ref function))
            {
                // Bad <FKTBEZ> field
                _oEvent.State = EVENT_STATE.STATE_INFO;
                _oEvent.StateText = string.Format("Ungültiger Wert für Spalte <FKTBEZ> ('{0}'), dieser muss '{1}', '{2}', '{3}' oder '{4}' sein in Zeile {5}",
                        strFunktion, "1. Operateur", "1. Assistent", "2. Operateur", "2. Assistent", strLine);
                FireImportOPEvent(_oEvent);
                goto _exit;
            }

            if (BeginnUndEnde2DateTime(strDatumBeginn, strDatumEnde, strLine, ref dtBeginn, ref dtEnde))
            {
                HandleBeteiligter(strFallzahl, strNachname, strVorname, function, dtBeginn, dtEnde, strOpsKode, strOpsText);
            }

        _exit: ;

        }
    }
}
