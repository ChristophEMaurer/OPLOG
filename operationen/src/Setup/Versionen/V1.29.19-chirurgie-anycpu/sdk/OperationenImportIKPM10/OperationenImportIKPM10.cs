/*
 * OperationenImportPlugin - import operations/surgeons from a plain text file
 * Source code from OP-LOG
 * 
 * Copyright Christoph Maurer, D-61184 Karben
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
    /// 
    /// This format is used by an export from Dennis Borces, Saarland
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
                + "\r\nWenn Umlaute nicht richtig ankommen, speichern Sie Ihre Datei als Unicode und verwenden das Plugin, bei dem hinten 'Unicode' steht."
                + "\r\n\r\n46 Spalten:"
                + "\r\nFormat einer einzelnen Zeile:"
                + "\r\nNachname;Vorname;GeburtsDatum;<OP_Datum>;"
                + "\r\nICD1-Code;ICD1-Text;ICD2-Code;ICD2-Text;...;ICD10-Code;ICD10-Text;"
                + "\r\nIKPM1-Code;IKPM1-Text;IKPM2-Code;IKPM2-Text;...;IKPM10-Code;IKPM10-Text;<Operateur1>;<1Assistent1>"
                + "\r\n\r\nDie ersten drei Spalten sowie alle IKPM Spalten werden ignoriert."
                + "\r\n<Operateur1> und"
                + "\r\n<1Assistent1>: 'Max Mustermann' oder 'Dr. Max Mustermann', also 'egal was vorne kommt Vorname Nachname'"
                + "\r\n<OP_Datum>: 'dd.mm.yyyy' oder 'dd.mm.yy' (yy <= 70 -> 20yy, yy > 70 -> 19yy, also: 04->2004 und 98->1998)"
                + "\r\nDie Fallzahl wird auf <OP_Datum> + <IKPM1-Code> gesetzt."
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

        private bool NameToNachnameVorname(string name, ref string nachname, ref string vorname)
        {
            bool success = false;

            try
            {
                string []arName = name.Split(' ');

                if (arName.Length == 1)
                {
                    nachname = arName[0];
                    vorname = "";
                }
                else if (arName.Length > 1)
                {
                    nachname = arName[arName.Length - 1];
                    vorname = arName[arName.Length - 2];
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

        /// <summary>
        /// Use this function to retrive a value which may NOT be empty
        /// </summary>
        /// <param name="arLine">the current line as an array</param>
        /// <param name="index">index of column into arLine</param>
        /// <param name="value">the value to extract</param>
        /// <param name="line">the line as a string</param>
        /// <param name="columnDisplayName">User readable name of the column</param>
        /// <param name="columnName">Possible different, technical name for this column</param>
        /// <returns></returns>
        private bool ReadColumn(string []arLine, int index, ref string value, string line, string columnDisplayName, string columnName)
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
            //  0        1       2            3        
            //  Nachname;Vorname;GeburtsDatum;OP_Datum;
            //
            //  4         5         6         7         8         9         10        11        12        13
            //  ICD1-Code;ICD1-Text;ICD2-Code;ICD2-Text;ICD3-Code;ICD3-Text;ICD4-Code;ICD4-Text;ICD5-Code;ICD5-Text;
            //
            //  14        15        16        17        18        19        20        21        22         23   
            //  ICD6-Code;ICD6-Text;ICD7-Code;ICD7-Text;ICD8-Code;ICD8-Text;ICD9-Code;ICD9-Text;ICD10-Code;ICD10-Text;
            // 
            //  24         25         26         27         28  
            //  IKPM1-Code;IKPM1-Text;IKPM2-Code;IKPM2-Text;IKPM3-Code;IKPM3-Text;IKPM4-Code;IKPM4-Text;IKPM5-Code;IKPM5-Text;
            //  
            //  34         35                                                                           
            //  IKPM6-Code;IKPM6-Text;IKPM7-Code;IKPM7-Text;IKPM8-Code;IKPM8-Text;IKPM9-Code;IKPM9-Text;IKPM10-Code;IKPM10-Text;
            //  44         45
            //  Operateur1;1Assistent1
            //
            String[] arLine = strLine.Split(';');

            if (arLine.Length != 46)
            {
                // Not x columns
                _oEvent.State = EVENT_STATE.STATE_INFO;
                _oEvent.StateText = "There are not 46 columns in line, ignoring line: " + strLine;
                FireImportOPEvent(_oEvent);
                goto _exit;
            }

            // Gesamt Operation: Datum und Beginn - Ende
            string strFallzahl = "";
            string strDatum = "";
            string strOperateurName = "";
            string strOperateurNachname = "";
            string strOperateurVorname = "";
            string strAssistentName = "";
            string strAssistentNachname = "";
            string strAssistentVorname = "";

            DateTime? dtDatum = null;

            if (!ReadColumn(arLine, 3, ref strDatum, strLine, "OP Datum", "<OP_Datum>"))
            {
                goto _exit;
            }
            if (!ReadColumn(arLine, 44, ref strOperateurName, strLine, "Operateur", "<Operateur>"))
            {
                goto _exit;
            }
            if (!ReadColumn(arLine, 45, ref strAssistentName, strLine, "Assistent", "<Assistent>"))
            {
                goto _exit;
            }

            if (!Date2DateTime(strDatum, ref dtDatum))
            {
                // unexpected format for date time
                _oEvent.State = EVENT_STATE.STATE_INFO;
                _oEvent.StateText = string.Format("Expected date format in <OP_Datum> ('{0}'), 'dd.mm.yy' or 'dd.mm.yyyy' where expected, ignoring line: {1}",
                    strDatum, strLine);
                FireImportOPEvent(_oEvent);
                goto _exit;
            }

            if (!NameToNachnameVorname(strOperateurName, ref strOperateurNachname, ref strOperateurVorname))
            {
                goto _exit;
            }
            if (!NameToNachnameVorname(strAssistentName, ref strAssistentNachname, ref strAssistentVorname))
            {
                goto _exit;
            }

            //
            // Fallzahl generieren
            // 24 ist der erste IKPM Code
            //
            strFallzahl = strDatum + "-" + arLine[4];

            // 10 IKPM7 Codes
            for (int i = 24; i < 44; i += 2)
            {
                string opsCode = arLine[i];
                string opsText = arLine[i + 1];

                if (opsCode.Length > 0)
                {
                    HandleBeteiligter(strFallzahl, strOperateurNachname, strOperateurVorname, OP_FUNCTION.OP_FUNCTION_OP, dtDatum.Value, dtDatum.Value, opsCode, opsText);
                    HandleBeteiligter(strFallzahl, strAssistentNachname, strAssistentVorname, OP_FUNCTION.OP_FUNCTION_ASS, dtDatum.Value, dtDatum.Value, opsCode, opsText);
                }
            }

        _exit: ;
        }
    }
}
