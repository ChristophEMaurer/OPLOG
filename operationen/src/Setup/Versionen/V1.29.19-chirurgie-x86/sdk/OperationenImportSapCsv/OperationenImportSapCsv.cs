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
    /// This format is used by an export from SAP in CSV format
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
        const string Operateur_1 = "Operateur 1";
        const string Operateur_2 = "Operateur 2";
        const string Assistent_1 = "OP Assistent 1";
        const string Assistent_2 = "OP Assistent 2";
        
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
            string s = string.Format("Dieses Import-Plugin importiert Operationen aus einer Text-Datei, die aus SAP in CSV-Format mit Codierung '{0}'."
                + "\r\nÜberprüfen Sie nach dem Datenimport die Operationen und die Umlaute."
                + "\r\n\r\n8-11 Spalten:"
                + "\r\nFormat einer einzelnen Zeile:"
                + "\r\n<Fall>;<Aufgabe>;<Mitarbeiter>;<OP Leistungen>;<Beginndatum>;<Beginnuhrzeit>;<Endeuhrzeit>;<Haupt OPS Code>[;Leistungstext 1][;Leistungstext 2][;Leistungstext 3]"
                + "\r\n"
                + "\r\n<Fall>                           : [0-9]"
                + "\r\n<Aufgabe>                        : 'Operateur 1', 'Operateur 2', 'OP Assistent 1', 'OP Assistent 2'"
                + "\r\n<Mitarbeiter>                    : <Nachname>"
                + "\r\n<OP Leistungen>                  : [0-9] -> wird nicht benutzt"
                + "\r\n<Beginndatum>                    : <DD.MM.YYYY>"
                + "\r\n<Beginnuhrzeit>                  : [HH:MM]"
                + "\r\n<Endeuhrzeit>                    : [HH:MM]"
                + "\r\n<Haupt OPS Code>                 : <OPSKode>"
                + "\r\n<Leistungstext 1>                : <OPSText>"
                + "\r\nOptional: <Leistungstext 2>      : <OPSText>"
                + "\r\nOptional: <Leistungstext 3>      : <OPSText>"
                + "\r\n\r\nBeispiel:"
                + "\r\n'4914271;OP Assistent 1;´Maurer;226686407;19:13:43;06.02.2017;19:47:36;5-896.1A;Brustwand und Rücken: Großflächig: Chiru;rgische Wundtoilette[Wunddebridement] m'"
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

                    //
                    // Skip the first line with headers
                    //
                    strLine = sr.ReadLine();

                    while (null != (strLine = sr.ReadLine()))
                    {
                        ImportLine(strLine);
                        if (_oEvent.Abort)
                        {
                            break;
                        }
                    } 
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
        /// Convert a Date and Time string a C# DateTime.
        /// The format is "dd.mm.yyyy" and "hh:mm:ss"
        /// Example: "05.01.2017" "08:26:00"
        /// </summary>
        /// <param name="strDate">The date in format dd.mm.yyyy</param>
        /// <param name="strTime">The date in format hh.mm.ss</param>
        /// <param name="dt">The date returned</param>
        /// <returns></returns>
        private bool DateAndTime2DateTime(string strDate, string strTime, ref DateTime dt)
        {
            bool fSuccess = false;

            try
            {
                //
                //  0  1  2
                // "05.01.2017"
                //
                string[] arDate = strDate.Split('.');

                if (arDate.Length == 3)
                {
                    int nDay;
                    int nMonth;
                    int nYear;

                    if (arDate[2].Length == 4 &&
                        Int32.TryParse(arDate[0], out nDay)
                        && Int32.TryParse(arDate[1], out nMonth)
                        && Int32.TryParse(arDate[2], out nYear))
                    {
                        string[] arTime = strTime.Split(':');
                        {
                            //
                            // 0  1  3
                            // 07:56:00
                            //
                            if (arTime.Length == 3)
                            {
                                int nHour;
                                int nMinute;
                                int nSeconds;

                                if (   Int32.TryParse(arTime[0], out nHour) 
                                    && Int32.TryParse(arTime[1], out nMinute)
                                    && Int32.TryParse(arTime[2], out nSeconds))
                                {
                                    dt = new DateTime(nYear, nMonth, nDay, nHour, nMinute, nSeconds);
                                    fSuccess = true;
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
                // _oEvent.SurgeonFirstName = strVorname;
                _oEvent.OPDateAndTime = dtBeginn;
                _oEvent.OPTimeEnd = dtEnde;
                _oEvent.OPCode = strOpsKode;
                _oEvent.OPDescription = strOpsText;
                _oEvent.OPFunction = function;

                FireImportOPEvent(_oEvent);
            }
        }

        /// <summary>
        /// Convert the text of field 'Aufgabe' to one of the OP_FUNCTION values
        /// </summary>
        /// <param name="strFunktion">Text of field 'Aufgabe'</param>
        /// <param name="function">the matchin gfunction</param>
        /// <returns>true if the text is a valid function text and could be mapped, false in any other case</returns>
        private bool FunctionText2Code(string strFunktion, ref OP_FUNCTION function)
        {
            bool fSuccess = false;

            try
            {
                if (!string.IsNullOrEmpty(strFunktion))
                {
                    if (strFunktion == Operateur_1)
                    {
                        function = OP_FUNCTION.OP_FUNCTION_OP;
                        fSuccess = true;
                    }
                    else if (strFunktion == Operateur_2)
                    {
                        function = OP_FUNCTION.OP_FUNCTION_OP;
                        fSuccess = true;
                    }
                    else if (strFunktion == Assistent_1)
                    {
                        function = OP_FUNCTION.OP_FUNCTION_ASS;
                        fSuccess = true;
                    }
                    else if (strFunktion == Assistent_2)
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
            // 0    1       2           3             4           5             6           7              8              9                 10
            // Fall;Aufgabe;Mitarbeiter;OP Leistungen;Beginndatum;Beginnuhrzeit;Endeuhrzeit;Haupt OPS Code;Leistungstext 1[;Leistungstext 2][;Leistungstext 3]
            //
            String[] arLine = strLine.Split(';');

            //
            // There must be 8-11 columns
            //
            if ((arLine.Length < 8) || (11 < arLine.Length))
            {
                //
                // Not at least 8 columns
                //
                _oEvent.State = EVENT_STATE.STATE_INFO;
                _oEvent.StateText = "Zuwenige Spalten (8-11 sind notwendig) in Datenzeile, Datenzeile wird ignoriert: " + strLine;
                FireImportOPEvent(_oEvent);
                goto _exit;
            }

            // Gesamt Operation: Datum und Beginn - Ende
            string strFallzahl = "";
            string strFunktion = "";
            string strNachname = "";
            string strOpLeistungen = "";
            string strBeginnDatum = "";
            string strBeginnUhrzeit = "";
            string strEndeUhrzeit = "";
            string strOpsKode = "";
            string strOpsText = "";
            string strLeistungstext1 = "";
            string strLeistungstext2 = "";
            string strLeistungstext3 = "";

            OP_FUNCTION function = OP_FUNCTION.OP_FUNCTION_OP;

            if (!ReadColumn(arLine, 0, ref strFallzahl, strLine, "Fallzahl", "Fall"))
            {
                goto _exit;
            }
            if (!ReadColumn(arLine, 1, ref strFunktion, strLine, "Funktionsbezeichnung", "Aufgabe"))
            {
                goto _exit;
            }
            if (!ReadColumn(arLine, 2, ref strNachname, strLine, "Nachname", "Mitarbeiter"))
            {
                goto _exit;
            }
            if (!ReadColumn(arLine, 3, ref strOpLeistungen, strLine, "OP Leistungen", "OP Leistungen"))
            {
                goto _exit;
            }
            if (!ReadColumn(arLine, 4, ref strBeginnDatum, strLine, "Datum", "Beginndatum"))
            {
                goto _exit;
            }
            if (!ReadColumn(arLine, 5, ref strBeginnUhrzeit, strLine, "Beginn-Uhrzeit", "Beginnuhrzeit"))
            {
                goto _exit;
            }
            if (!ReadColumn(arLine, 6, ref strEndeUhrzeit, strLine, "Ende-Uhrzeit", "Endeuhrzeit"))
            {
                goto _exit;
            }
            if (!ReadColumn(arLine, 7, ref strOpsKode, strLine, "OPSKode", "Haupt OPS Code"))
            {
                goto _exit;
            }

            if (arLine.Length > 8)
            {
                if (!ReadColumn(arLine, 8, ref strLeistungstext1, strLine, "Leistungstext 1", "Leistungstext 1"))
                {
                    goto _exit;
                }

                if (arLine.Length > 9)
                {
                    if (!ReadColumn(arLine, 9, ref strLeistungstext2, strLine, "Leistungstext 2", "Leistungstext 2"))
                    {
                        goto _exit;
                    }

                    if (arLine.Length > 10)
                    {
                        if (!ReadColumn(arLine, 10, ref strLeistungstext3, strLine, "Leistungstext 3", "Leistungstext 3"))
                        {
                            goto _exit;
                        }
                    }
                }
            }

            DateTime dtBeginn = DateTime.Now;
            DateTime dtEnde = DateTime.Now;

            if (!DateAndTime2DateTime(strBeginnDatum, strBeginnUhrzeit, ref dtBeginn))
            {
                // unexpected format for date time
                _oEvent.State = EVENT_STATE.STATE_INFO;
                _oEvent.StateText = string.Format("Unerwartetes Datumsformat in Beginndatum;Beginnuhrzeit ('{0};{1}}'), Datenzeile wird ignoriert: {2}",
                    strBeginnDatum, strBeginnUhrzeit, strLine);
                FireImportOPEvent(_oEvent);
                goto _exit;
            }

            if (!DateAndTime2DateTime(strBeginnDatum, strEndeUhrzeit, ref dtEnde))
            {
                // unexpected format for date time
                _oEvent.State = EVENT_STATE.STATE_INFO;
                _oEvent.StateText = string.Format("Unerwartetes Datumsformat in Beginndatum;...;Endeuhrzeit ('{0};{1}}'), Datenzeile wird ignoriert: {2}",
                    strBeginnDatum, strEndeUhrzeit, strLine);
                FireImportOPEvent(_oEvent);
                goto _exit;
            }

            if (dtEnde < dtBeginn)
            {
                //
                // Beginndatum;Beginnuhrzeit; Endeuhrzeit
                // 05.01.2017;23:30:00;00:30:00
                //
                // dtBeginn = 05.01.2017 23:30
                // dtEnde   = 06.01.2017 00:30
                //
                dtEnde = dtEnde.AddDays(1);
            }

            if (!FunctionText2Code(strFunktion, ref function))
            {
                // Bad Aufgabe field
                _oEvent.State = EVENT_STATE.STATE_INFO;
                _oEvent.StateText = string.Format("Ungültiger Wert für Spalte <Aufgabe> ('{0}'), dieser muss '{1}', '{2}', '{3}' oder '{4}' sein in Zeile {5}",
                        strFunktion, Operateur_1, Operateur_2, Assistent_1, Assistent_2, strLine);
                FireImportOPEvent(_oEvent);
                goto _exit;
            }

            strOpsText = strLeistungstext1 + strLeistungstext2 + strLeistungstext3;

            if (String.IsNullOrEmpty(strOpsText))
            {
                strOpsText = "Leistungstext fehlt!!!";
            }

            HandleBeteiligter(strFallzahl, strNachname, function, dtBeginn, dtEnde, strOpsKode, strOpsText);

        _exit: ;

        }
    }
}

