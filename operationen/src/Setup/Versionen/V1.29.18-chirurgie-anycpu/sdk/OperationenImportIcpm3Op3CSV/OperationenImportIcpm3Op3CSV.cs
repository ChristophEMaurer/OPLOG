/*
 * OperationenImportPlugin - import operations/surgeons from a plain text file
 * Source code from OP-LOG
 * 
 * Copyright Christoph Maurer, 61184 Karben
 * http://www.op-log.de
 * 
 */


using System;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Operationen
{
    /// <summary>
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
                + "\r\n\r\n13 Spalten:"
                + "\r\nFormat:  <Fallzahl>;<OPDatum>;<OPZeitBeginn>;<OPZeitEnde>;"
                + "\r\n         <OPSKode1>;<OPSBezeichnung1>;<OPSKode2>;<OPSBezeichnung2>;<OPSKode3>;<OPSBezeichnung3>;"
                + "\r\n         <NameOperateur1>;<NameOperateur2>;<NameAssistent>"
                + "\r\n\r\n<Fallzahl>           : [a-z,A-Z,0-9]"
                + "\r\n<OPDatum>            : <DD.MM.YYYY>"
                + "\r\n<OPZeitBeginn>       : <HH:MM>"
                + "\r\n<OPZeitEnde>         : <HH:MM>"
                + "\r\n<NameOperateur1>     : <Nachname[ ,][Vorname]>"
                + "\r\n<NameOperateur2>     : <Nachname[ ,][Vorname]>"
                + "\r\n<NameAssistent>      : <Nachname[ ,][Vorname]>", FormatDescription())
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
                    sr.Close();
                    sr.Dispose();
                    sr = null;
                }
            }
        }

        private bool BeginnUndEnde2DateTime(
            string strDatum, 
            string strZeitBeginn, 
            string strZeitEnde, 
            string strErrorText,
            string strLine,
            ref DateTime dtBeginn,
            ref DateTime dtEnde
            )
        {
            bool success = false;

            if (DateAndTime2DateTime(strDatum, strZeitBeginn, ref dtBeginn) &&
                DateAndTime2DateTime(strDatum, strZeitEnde, ref dtEnde))
            {
                success = true;
            }
            else
            {
                // unexpected format for date/time/time
                _oEvent.State = EVENT_STATE.STATE_INFO;
                _oEvent.StateText = "Unerwartetes Datumsformat (Datum='" + strDatum
                    + "', ZeitBeginn='" + strZeitBeginn
                    + "', ZeitEnde='" + strZeitEnde
                    + "'), Datenzeile wird ignoriert: " + strLine;
                FireImportOPEvent(_oEvent);
            }

            return success;
        }

        private bool IsValidTime(string strTime)
        {
            bool fSuccess = false;

            try
            {
                if (strTime.Length == 5 && strTime.IndexOf(':') == 2)
                {
                    int nHour = Int32.Parse(strTime.Substring(0, 2));
                    int nMinute = Int32.Parse(strTime.Substring(3, 2));

                    DateTime dt = new DateTime(1965, 5, 14, nHour, nMinute, 0);
                    fSuccess = true;
                }
            }
            catch
            {
            }

            return fSuccess;
        }

        /// <summary>
        /// Convert two strings into a DateTime
        /// </summary>
        /// <param name="strData">"dd.mm.yyyy</param>
        /// <param name="strTime">"hh:mm"</param>
        /// <returns></returns>
        private bool DateAndTime2DateTime(string strDate, string strTime, ref DateTime dt)
        {
            bool fSuccess = false;
            dt = DateTime.Now;

            try
            {
                if (strDate.Length == 10 && strDate.IndexOf('.') == 2 && strDate.LastIndexOf('.') == 5)
                {
                    int nDay = Int32.Parse(strDate.Substring(0, 2));
                    int nMonth = Int32.Parse(strDate.Substring(3, 2));
                    int nYear = Int32.Parse(strDate.Substring(6, 4));

                    if (strTime.Length == 5 && strTime.IndexOf(':') == 2)
                    {
                        int nHour = Int32.Parse(strTime.Substring(0, 2));
                        int nMinute = Int32.Parse(strTime.Substring(3, 2));

                            dt = new DateTime(nYear, nMonth, nDay, nHour, nMinute, 0);
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
        /// Convert a name into a first name and a last name:
        /// </summary>
        /// <param name="strName">A Text containing the last and first name, format is:
        /// <br/>"lastname[ ,][firstname]"</param>
        /// <param name="strNachname"></param>
        /// <param name="strVorname"></param>
        private void Name2NachnameVorname(string strName, out string strNachname, out string strVorname)
        {
            char[] separators = { ' ', ',' };
            int nIndex;

            strNachname = strName;
            strVorname = "";

            nIndex = strName.IndexOfAny(separators);
            if (nIndex != -1)
            {
                strNachname = strName.Substring(0, nIndex);
                nIndex++;
                while (nIndex < strName.Length && strName[nIndex] == ' ')
                {
                    nIndex++;
                }
                if (nIndex < strName.Length)
                {
                    strVorname = strName.Substring(nIndex);
                }
            }
        }

        private void HandleBeteiligter(
            string strLine, 
            string strFallzahl, 
            int indexNachname,
            string strNachname, 
            string strVorname, 
            int indexFunction,
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

                if (function == OP_FUNCTION.OP_FUNCTION_OP)
                {
                    FireImportOPEvent(_oEvent);
                }
                else if (function == OP_FUNCTION.OP_FUNCTION_ASS)
                {
                    FireImportOPEvent(_oEvent);
                }
                else
                {
                    // Unexpected value for BetFunktion
                    _oEvent.State = EVENT_STATE.STATE_INFO;
                    _oEvent.StateText = string.Format("Unerwarteter Wert für Funktion {0} in Spalte {1}, dieser Eintrag wird ignoriert in Datenzeile: {2}",
                        function, indexFunction + 1, strLine);
                }
            }
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
            // 0          1         2              3            4          5         6          7
            // <Fallzahl>;<OPDatum>;<OPZeitBeginn>;<OPZeitEnde>;<OPSKode1>;<OPSBez1>;<OPSKode2>;<OPSBez2>;
            //
            // 8          9         10          11           12
            // <OPSKode3>;<OPSBez3>;<Operateur1;<Operateur2>;<Assistent>
            String[] arLine = strLine.Split(';');
            if (arLine.Length == 13)
            {
                // Au weia! mal arrays benutzen!!!
                string strNachnameOP1;
                string strVornameOP1;
                string strNachnameOP2;
                string strVornameOP2;
                string strNachnameAss;
                string strVornameAss;

                // Gesamt Operation: Datum und Beginn - Ende
                string strFallzahl = arLine[0];
                string strDatum = arLine[1];
                string strZeitBeginn = arLine[2];
                string strZeitEnde = arLine[3];

                string strNameOP1 = arLine[10];
                string strNameOP2 = arLine[11];
                string strNameAss = arLine[12];

                string strOpsKode;
                string strOpsText;
                DateTime dtBeginn = DateTime.Now;
                DateTime dtEnde = DateTime.Now;

                Name2NachnameVorname(strNameOP1, out strNachnameOP1, out strVornameOP1);
                Name2NachnameVorname(strNameOP2, out strNachnameOP2, out strVornameOP2);
                Name2NachnameVorname(strNameAss, out strNachnameAss, out strVornameAss);

                if (BeginnUndEnde2DateTime(strDatum, strZeitBeginn, strZeitEnde, "Datum/Zeit", strLine, ref dtBeginn, ref dtEnde))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        strOpsKode = arLine[4 + (i * 2)];
                        strOpsText = arLine[4 + (i * 2) + 1];

                        if (strOpsKode.Length > 0 && strOpsText.Length > 0)
                        {
                            // Operateur1
                            HandleBeteiligter(strLine, strFallzahl, 10, strNachnameOP1, strVornameOP1, 10, OP_FUNCTION.OP_FUNCTION_OP, dtBeginn, dtEnde, strOpsKode, strOpsText);
                            if (_oEvent.Abort)
                            {
                                break;
                            }

                            // Operateur2
                            HandleBeteiligter(strLine, strFallzahl, 11, strNachnameOP2, strVornameOP2, 11, OP_FUNCTION.OP_FUNCTION_OP, dtBeginn, dtEnde, strOpsKode, strOpsText);
                            if (_oEvent.Abort)
                            {
                                break;
                            }

                            // Ass
                            HandleBeteiligter(strLine, strFallzahl, 12, strNachnameAss, strVornameAss, 12, OP_FUNCTION.OP_FUNCTION_ASS, dtBeginn, dtEnde, strOpsKode, strOpsText);
                            if (_oEvent.Abort)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                // Not 13 columns
                _oEvent.State = EVENT_STATE.STATE_INFO;
                _oEvent.StateText = "Keine 13 Spalten in Datenzeile, Datenzeile wird ignoriert: " + strLine;
                FireImportOPEvent(_oEvent);
            }
        }
    }
}

