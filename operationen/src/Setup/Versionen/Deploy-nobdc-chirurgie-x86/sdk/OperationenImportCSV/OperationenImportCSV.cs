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
    /// Use this class to import operations/surgeons from a plain text file
    /// with ANSI format - Latin-2 (1250).
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
                + "\r\n\r\nÜberprüfen Sie nach dem Datenimport Umlaute und Operationen-"
                + "\r\n\r\nFormat (>= 26 Spalten):  <Fallzahl>;<OPDatum>;<OPZeitBeginn>;<OPZeitEnde>;"
                + "\r\n         <OPSKode1>;<OPSBezeichnung1>;<OPSKode2>;<OPSBezeichnung2>;<OPSKode3>;<OPSBezeichnung3>;"
                + "\r\n         <OPSKode4>;<OPSBezeichnung4>;<OPSKode5>;<OPSBezeichnung5>;"
                + "\r\n         <BeteiligterName1>;<BeteiligterFunktion1>;<BeteiligterVon1>;<BeteiligterBis1>;"
                + "\r\n         <BeteiligterName2>;<BeteiligterFunktion2>;<BeteiligterVon2>;<BeteiligterBis2>;"
                + "\r\n         <BeteiligterName3>;<BeteiligterFunktion3>;<BeteiligterVon3>;<BeteiligterBis3>[;<Rest>]"
                + "\r\n\r\n<Fallzahl>           : [a-z,A-Z,0-9]"
                + "\r\n<OPDatum>            : <DD/MM/YYYY>"
                + "\r\n<OPZeitBeginn>       : <HH:MM>"
                + "\r\n<OPZeitEnde>         : <HH:MM>"
                + "\r\n<BeteiligterName>    : <Nachname[ ,][Vorname]>"
                + "\r\n<BeteiligterFunktion>: OP1 | OP2 | ASS1", FormatDescription())
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
                        if (!string.IsNullOrEmpty(strLine))
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
                string text = "Unerwartetes Datumsformat ";

                //
                // unexpected format for date/time/time
                //
                if (!string.IsNullOrEmpty(strErrorText))
                {
                    text = text + "[" + strErrorText + "] ";
                }

                _oEvent.State = EVENT_STATE.STATE_INFO;
                _oEvent.StateText = text + "(Datum='" + strDatum
                    + "', ZeitBeginn='" + strZeitBeginn
                    + "', ZeitEnde='" + strZeitEnde
                    + "'), dieser Eintrag wird ignoriert in Datenzeile: " + strLine;
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
        /// <param name="strData">"dd/mm/yyyy</param>
        /// <param name="strTime">"hh:mm"</param>
        /// <returns></returns>
        private bool DateAndTime2DateTime(string strDate, string strTime, ref DateTime dt)
        {
            bool fSuccess = false;
            dt = DateTime.Now;

            try
            {
                if (strDate.Length == 10 && strDate.IndexOf('/') == 2 && strDate.LastIndexOf('/') == 5)
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
            string strFunction,
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

                if (strFunction == "OP1")
                {
                    _oEvent.OPFunction = OP_FUNCTION.OP_FUNCTION_OP;
                    FireImportOPEvent(_oEvent);
                }
                else if (strFunction == "OP2")
                {
                    _oEvent.OPFunction = OP_FUNCTION.OP_FUNCTION_OP;
                    FireImportOPEvent(_oEvent);
                }
                else if (strFunction == "ASS1")
                {
                    _oEvent.OPFunction = OP_FUNCTION.OP_FUNCTION_ASS;
                    FireImportOPEvent(_oEvent);
                }
                else if (strFunction == "ASS2")
                {
                    // ignore, no message
                }
                else if (strFunction == "INS1")
                {
                    // ignore, no message
                }
                else if (strFunction == "SPR1")
                {
                    // ignore, no message
                }
                else
                {
                    // Unexpected value for BetFunktion
                    _oEvent.State = EVENT_STATE.STATE_INFO;
                    _oEvent.StateText = string.Format("Unerwarteter Wert '{0}' für BetFunktion in Spalte {1}, dieser Eintrag wird ignoriert in Datenzeile: {2}", 
                        strFunction, indexFunction + 1, strLine);
                }
            }
            else
            {
                // Name is empty
                _oEvent.State = EVENT_STATE.STATE_INFO;
                _oEvent.StateText = string.Format("Nachname ist leer in Spalte {0}, dieser Eintrag wird ignoriert in Datenzeile: {1}", 
                       indexNachname + 1, strLine);
                FireImportOPEvent(_oEvent);
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

            // This is the format containing the operation/surgeons.
            //
            // 0          1        2              3            4          5         6          7
            // <Fallzahl>;<OPDatum;<OPZeitBeginn>;<OPZeitEnde>;<OPSKode1>;<OPSBez1>;<OPSKode2>;<OPSBez2>;
            //
            // 8          9         10         11        12         13        14         15             16        17
            // <OPSKode3>;<OPSBez3>;<OPSKode4>;<OPSBez4>;<OPSKode5>;<OPSBez5>;<BetName1>;<BetFunktion1>;<BetVon1>;<BetBis1>;"
            //
            // 18         19             20        21        22         23             24        25
            // <BetName2>;<BetFunktion2>;<BetVon2>;<BetBis2>;<BetName3>;<BetFunktion3>;<BetVon3>;<BetBis3>[;<Rest>]"
            String[] arLine = strLine.Split(';');
            if (arLine.Length >= 26)
            {

                // Au weia! mal arrays benutzen!!!
                string strNachnameBet1;
                string strVornameBet1;
                string strNachnameBet2;
                string strVornameBet2;
                string strNachnameBet3;
                string strVornameBet3;

                // Gesamt Operation: Datum und Beginn - Ende
                string strFallzahl = arLine[0];
                string strDatum = arLine[1];
                string strZeitBeginn = arLine[2];
                string strZeitEnde = arLine[3];

                // Operation: Beginn - Ende für den einzelnen Beteiligten
                string strZeitBeginn1 = arLine[16];
                string strZeitEnde1 = arLine[17];
                string strZeitBeginn2 = arLine[20];
                string strZeitEnde2 = arLine[21];
                string strZeitBeginn3 = arLine[24];
                string strZeitEnde3 = arLine[25];

                string strNameBet1 = arLine[14];
                string strNameBet2 = arLine[18];
                string strNameBet3 = arLine[22];

                string strOpsKode;
                string strOpsText;
                DateTime dtBeginn1 = DateTime.Now;
                DateTime dtEnde1 = DateTime.Now;
                DateTime dtBeginn2 = DateTime.Now;
                DateTime dtEnde2 = DateTime.Now;
                DateTime dtBeginn3 = DateTime.Now;
                DateTime dtEnde3 = DateTime.Now;

                Name2NachnameVorname(strNameBet1, out strNachnameBet1, out strVornameBet1);
                Name2NachnameVorname(strNameBet2, out strNachnameBet2, out strVornameBet2);
                Name2NachnameVorname(strNameBet3, out strNachnameBet3, out strVornameBet3);

                string strFunction = arLine[15].ToUpper();
                string strFunctionBet2 = arLine[19].ToUpper();
                string strFunctionBet3 = arLine[23].ToUpper();

                for (int i = 0; i < 5; i++)
                {
                    strOpsKode = arLine[4 + (i * 2)];
                    strOpsText = arLine[4 + (i * 2) + 1];

                    if (strOpsKode.Length > 0 && strOpsText.Length > 0)
                    {
                        //
                        // Wenn beim einzelnen Beteiligten keine gültige Zeit steht, wird die von der Gesamtoperation ganz oben genommen
                        // Beteiligter 1
                        //
                        if (!IsValidTime(strZeitBeginn1))
                        {
                            strZeitBeginn1 = strZeitBeginn;
                        }
                        if (!IsValidTime(strZeitEnde1))
                        {
                            strZeitEnde1 = strZeitEnde;
                        }
                        if (BeginnUndEnde2DateTime(strDatum, strZeitBeginn1, strZeitEnde1, "Beteiligter 1", strLine, ref dtBeginn1, ref dtEnde1))
                        {
                            HandleBeteiligter(strLine, strFallzahl, 14, strNachnameBet1, strVornameBet1, 15, strFunction, dtBeginn1, dtEnde1, strOpsKode, strOpsText);
                        }
                        if (_oEvent.Abort)
                        {
                            break;
                        }

                        //
                        // Beteiligter 2
                        //
                        if (!IsValidTime(strZeitBeginn2))
                        {
                            strZeitBeginn2 = strZeitBeginn;
                        }
                        if (!IsValidTime(strZeitEnde2))
                        {
                            strZeitEnde2 = strZeitEnde;
                        }
                        if (BeginnUndEnde2DateTime(strDatum, strZeitBeginn2, strZeitEnde2, "Beteiligter 2", strLine, ref dtBeginn2, ref dtEnde2))
                        {
                            HandleBeteiligter(strLine, strFallzahl, 18, strNachnameBet2, strVornameBet2, 19, strFunctionBet2, dtBeginn2, dtEnde2, strOpsKode, strOpsText);
                        }
                        if (_oEvent.Abort)
                        {
                            break;
                        }

                        //
                        // Beteiligter 3
                        //
                        if (!IsValidTime(strZeitBeginn3))
                        {
                            strZeitBeginn3 = strZeitBeginn;
                        }
                        if (!IsValidTime(strZeitEnde3))
                        {
                            strZeitEnde3 = strZeitEnde;
                        }
                        if (BeginnUndEnde2DateTime(strDatum, strZeitBeginn3, strZeitEnde3, "Beteiligter 3", strLine, ref dtBeginn3, ref dtEnde3))
                        {
                            HandleBeteiligter(strLine, strFallzahl, 22, strNachnameBet3, strVornameBet3, 23, strFunctionBet3, dtBeginn3, dtEnde3, strOpsKode, strOpsText);
                        }
                        if (_oEvent.Abort)
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                //
                // Less than 21 columns
                //
                _oEvent.State = EVENT_STATE.STATE_INFO;
                _oEvent.StateText = "Weniger als 26 Spalten in Datenzeile, Datenzeile wird ignoriert: " + strLine;
                FireImportOPEvent(_oEvent);
            }
        }

        private bool FileIsUnicode(string fileName)
        {
            bool isUnicode = false;

            FileStream fs = null;
            BinaryReader b = null;

            try
            {
                fs = File.Open(fileName, FileMode.Open);
                b = new BinaryReader(fs);

                byte[] data = new byte[2];

                if (2 == b.Read(data, 0, 2))
                {
                    if (data[0] == 0xFF && data[1] == 0xFE)
                    {
                        isUnicode = true;
                    }
                }
            }
            finally
            {
                if (b != null)
                {
                    b.Close();
                }
                if (fs != null)
                {
                    fs.Close();
                }
            }
            return isUnicode;
        }
    }
}

