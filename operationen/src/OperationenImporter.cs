using System;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Globalization;

using Utility;
using AppFramework;

namespace Operationen
{
    class OperationenImporter
    {
        public enum ImportProgressState
        {
            ProgressBegin,
            Progress,
            ProgressEnd,
            CountTotal,
            CountNew,
        }

        public class ImportProgressEvent
        {
            public ImportProgressState State;
            public int IntData;
            public bool Abort;
        }

        public delegate void ImportProgressHandler(object sender, ImportProgressEvent e);

        public event ImportProgressHandler ImportProgress;

        ImportProgressEvent _importProgressEvent;

        private BusinessLayer _businessLayer;

        private bool _logAllActions;
        private string _strLogfileName;
        private StreamWriter _oProtokoll;

        /// <summary>
        /// Anzahl aller Datensätze, die insgesamt gelesen werden. Dieses ist NICHT die Anzahl der Zeilen.
        /// </summary>
        private int _nCountTotal;

        /// <summary>
        /// Soviele records sind neu eingefügt worden.
        /// </summary>
        private int _nCountNew;

        /// <summary>
        /// Chirurg identifizieren anhand von Chirurgen.ImportID
        /// </summary>
        private bool _identifyByImportID;

        /// <summary>
        /// Operation identifizieren anhand von Chirurg, OPSCode, Datum und Beginn-Zeit, UND Identifier!
        /// </summary>
        private bool _identifyOpByIdentifier;

        /// <summary>
        /// Chirurg identifizieren anhand von Chirurgen.Nachname und Chirurgen.Vorname
        /// </summary>
        private bool _useFirstName;

        /// <summary>
        /// Chirurg einfügen, falls nicht vorhanden, Tabelle Chirurgen
        /// </summary>
        private bool _bInsertUnknownSurgeon;

        /// <summary>
        /// Operation einfügen, falls nicht vorhanden, Tabelle Operationen (OPS-KAtalog)
        /// Das ist eigentlich völlig überflüssig
        /// </summary>
        private bool _bInsertUnknownOperation;

        /// <summary>
        /// Wenn ungleich null, sollen nur die Operationen von genau diesem Arzt eingelesen werden.
        /// Das ist nutzlich, wenn viele verschiedenen Chirurgen vorkommen und man z.B. nur 
        /// die eigenen Operationen einfügen möchte.
        /// </summary>
        private DataRow _importSingle;

        /// <summary>
        /// Es werden nur records eingefügt, wenn die Funktion in dieser Liste vorkommt
        /// So kann 
        /// </summary>
        private List<OP_FUNCTION> _funktionen;

        private bool _bImportError; // one time error message flag

        private bool _interactive = true;

        private bool _importSuccess;

        public OperationenImporter(BusinessLayer businessLayer)
        {
            _businessLayer = businessLayer;

            _importProgressEvent = new ImportProgressEvent();
        }

        private BusinessLayer BusinessLayer
        {
            get { return _businessLayer; }
        }

        internal protected void Protokoll(string sMessage)
        {
            if (_oProtokoll == null)
            {
                string strPath = BusinessLayer.PathLogfiles;
                DateTime dtNow = DateTime.Now;
                string timeStamp = string.Format("{0:0000}.{1:00}.{2:00}", dtNow.Year, dtNow.Month, dtNow.Day)
                    + "-" + string.Format("{0:00}:{1:00}:{2:00}:{3:000}", dtNow.Hour, dtNow.Minute, dtNow.Second, dtNow.Millisecond);

                string strFilename = timeStamp + ".log";

                // Zeit enthaelt einen :, der ist nicht in Dateinamen erlaubt
                strFilename = strFilename.Replace(':', '.');
                _strLogfileName = strPath + System.IO.Path.DirectorySeparatorChar + strFilename;
                _oProtokoll = new StreamWriter(_strLogfileName, true);
            }

            _oProtokoll.WriteLine(DateTime.Now.ToLongTimeString() + ": " + sMessage);
            _oProtokoll.Flush();
        }

        internal protected void Protokoll(OperationenImportEvent importEvent)
        {
            StringBuilder sb = new StringBuilder();

            switch (importEvent.State)
            {
                case EVENT_STATE.STATE_DATA:
                    sb.Append(GetText("data"));
                    break;

                case EVENT_STATE.STATE_ERROR:
                    sb.Append(GetText("fehler"));
                    break;

                case EVENT_STATE.STATE_INFO:
                    sb.Append(GetText("info"));
                    break;
            }

            sb.Append(": ");

            if (importEvent.GetType() == typeof(OperationenImportEventEx))
            {
                sb.Append("OperationenImportEventEx: ");

                OperationenImportEventEx pluginEventEx = importEvent as OperationenImportEventEx;

                AppendNullableValue(sb, "SurgeonImportId", pluginEventEx.SurgeonImportId);
                AppendNullableValue(sb, "SurgeonUserId", pluginEventEx.SurgeonUserId);
            }
            else
            {
                sb.Append("OperationenImportEvent: ");
            }

            AppendNullableValue(sb, "Identifier", importEvent.Identifier);
            AppendNullableValue(sb, "OPCode", importEvent.OPCode);
            AppendNullableValue(sb, "OPCode", importEvent.OPDateAndTime);
            AppendNullableValue(sb, "OPDescription", importEvent.OPDescription);
            AppendNullableValue(sb, "OPFunction", importEvent.OPFunction.ToString());
            AppendNullableValue(sb, "OPTimeEnd", importEvent.OPTimeEnd);
            AppendNullableValue(sb, "State", importEvent.State.ToString());
            AppendNullableValue(sb, "StateText", importEvent.StateText);
            AppendNullableValue(sb, "SurgeonFirstName", importEvent.SurgeonFirstName);
            AppendNullableValue(sb, "SurgeonLastName", importEvent.SurgeonLastName, false);

            string sMessage = sb.ToString();

            Protokoll(sMessage);
        }

        private void AppendNullableValue(StringBuilder sb, string key, string value)
        {
            AppendNullableValue(sb, key, value, true);
        }

        private void AppendNullableValue(StringBuilder sb, string key, string value, bool appendDelimiter)
        {
            sb.Append(key);
            sb.Append("='");

            if (value != null)
            {
                sb.Append(value);
            }
            else
            {
                sb.Append("null");
            }

            sb.Append("'");

            if (appendDelimiter)
            {
                sb.Append(", ");
            }
        }

        private void AppendNullableValue(StringBuilder sb, string key, DateTime value)
        {
            AppendNullableValue(sb, key, value, true);
        }

        private void AppendNullableValue(StringBuilder sb, string key, DateTime value, bool appendDelimiter)
        {
            sb.Append(key);
            sb.Append("='");

            if (value != null)
            {
                sb.Append(value.ToShortDateString());
                sb.Append("-");
                sb.Append(value.ToShortTimeString());
            }
            else
            {
                sb.Append("null");
            }

            sb.Append("'");

            if (appendDelimiter)
            {
                sb.Append(", ");
            }
        }

        protected bool Confirm(string text)
        {
            return BusinessLayer.Confirm(text);
        }

        private string GetText(string id)
        {
            return BusinessLayer.GetText("OperationenImporter", id);
        }

        /// <summary>
        /// This is called by the plugin when reading data.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The import data</param>
        private void OPImportDelegate(object sender, OperationenImportEvent e)
        {
            _nCountTotal++;

            if (_logAllActions)
            {
                Protokoll(e);
            }

            if (_importProgressEvent.Abort)
            {
                _importSuccess = false;
                e.Abort = true;
                Protokoll(GetText("userabort"));

                _importProgressEvent.State = ImportProgressState.ProgressEnd;
                if (ImportProgress != null)
                {
                    ImportProgress(this, _importProgressEvent);
                }
            }
            else
            {
                switch (e.State)
                {
                    case EVENT_STATE.STATE_ERROR:
                        Protokoll(GetText("fehler") + ": " + e.StateText);
                        break;

                    case EVENT_STATE.STATE_DATA:
                        ImportLine(e);
                        break;

                    case EVENT_STATE.STATE_INFO:
                        Protokoll(GetText("info") + ": " + e.StateText);
                        break;

                    default:
                        Protokoll(GetText("unknownState") + ": " + e.State);
                        break;
                }
            }

            if (ImportProgress != null)
            {
                _importProgressEvent.State = ImportProgressState.CountTotal;
                _importProgressEvent.IntData = _nCountTotal;
                ImportProgress(this, _importProgressEvent);
            }
            if (ImportProgress != null)
            {
                _importProgressEvent.State = ImportProgressState.CountNew;
                _importProgressEvent.IntData = _nCountNew;
                ImportProgress(this, _importProgressEvent);
            }
        }

        /// <summary>
        /// This is called ImportLine because all plugins read a text file which contains lines of data.
        /// Each line can produce several database records.
        /// </summary>
        /// <param name="e">The event which can be of several types</param>
        private void ImportLine(OperationenImportEvent pluginEvent)
        {
            string text;
            int nID_Chirurgen = -1;
            string surgeonLastName = pluginEvent.SurgeonLastName;
            string surgeonFirstName = pluginEvent.SurgeonFirstName;
            string surgeonImportId = pluginEvent.SurgeonLastName;
            string surgeonUserId = pluginEvent.SurgeonLastName;
            string identifier = pluginEvent.Identifier;
            string opCode = pluginEvent.OPCode;
            string opDescription = pluginEvent.OPDescription;
            DateTime opDateAndTime = pluginEvent.OPDateAndTime;
            DateTime opTimeEnd = pluginEvent.OPTimeEnd;
            OP_FUNCTION opFunction = pluginEvent.OPFunction;

            if (pluginEvent.GetType() == typeof(OperationenImportEventEx))
            {
                OperationenImportEventEx pluginEventEx = pluginEvent as OperationenImportEventEx;

                surgeonImportId = pluginEventEx.SurgeonImportId;
                surgeonUserId = pluginEventEx.SurgeonUserId;
            }

            //
            // Check whether we want this record as filtered by Funktionen
            //
            if (_funktionen != null && !_funktionen.Contains(opFunction))
            {
                //
                // User specified specific functions and they do not include this one, so skip this record
                //
                if (_logAllActions)
                {
                    Protokoll(GetText("skippedRecordFunction"));
                }
                goto exit;
            }

            //
            // 1. First we read all data 
            //

            //
            // Nur wenn man alle importiert, die exclude Liste berücksichtigen
            //
            if (_importSingle == null)
            {
                int ID_ImportChirurgenExclude = -1;

                //
                // Option 'Import only this person' is not set. This option overrides the exclude list, we would ignore the exclude list.
                //
                if (_useFirstName)
                {
                    // use both first name and last name
                    ID_ImportChirurgenExclude = BusinessLayer.GetID_ImportChirurgenExclude(surgeonLastName, surgeonFirstName);
                }
                else
                {
                    // use only the last name
                    ID_ImportChirurgenExclude = BusinessLayer.GetID_ImportChirurgenExclude(surgeonLastName);
                }

                if (ID_ImportChirurgenExclude != -1)
                {
                    //
                    // this person is on the exclude list. Do not import this data.
                    // 
                    if (_logAllActions)
                    {
                        Protokoll(GetText("skippedRecordExcludeList"));
                    }

                    goto exit;
                }
            }

            //
            // ID des Chirurgen holen, wenn sie -1 ist, wurde er nicht gefunden
            //

            // Hier braucht man nur die ID des Datensatzes
            if (_identifyByImportID)
            {
                //
                // Op erationenImport:   importId ist im Nachnamen
                // Op erationenImportEx: importId ist in ImportId
                //
                if (_importSingle != null)
                {
                    if (surgeonImportId.Equals((string)_importSingle["ImportID"]))
                    {
                        if (_logAllActions)
                        {
                            Protokoll(GetText("findSurgeonExplicit"));
                        }
                        nID_Chirurgen = (int)_importSingle["ID_Chirurgen"];
                    }
                }
                else
                {
                    //
                    // Identify a person by the ImportID, use the e.SurgeonLastName field for this.
                    //
                    try
                    {
                        if (_logAllActions)
                        {
                            Protokoll(GetText("findSurgeonImportId"));
                        }
                        nID_Chirurgen = BusinessLayer.GetID_ChirurgenByImportID(surgeonImportId);
                    }
                    catch (MultipleRecordsException)
                    {
                        string s = string.Format(CultureInfo.InvariantCulture, GetText("err_assign1"), surgeonLastName, surgeonFirstName);
                        ImportError(pluginEvent, s);
                        Protokoll(s);
                        goto exit;
                    }
                }
            }
            else
            {
                if (_useFirstName)
                {
                    if (_importSingle != null)
                    {
                        if (_logAllActions)
                        {
                            Protokoll(GetText("findSurgeonExplicitLastFirst"));
                        }
                        if (surgeonLastName.Equals((string)_importSingle["Nachname"])
                            && surgeonFirstName.Equals((string)_importSingle["Vorname"]))
                        {
                            nID_Chirurgen = (int)_importSingle["ID_Chirurgen"];
                        }
                    }
                    else
                    {
                        //
                        // identify a person by their first name and their last name
                        //
                        try
                        {
                            if (_logAllActions)
                            {
                                Protokoll(GetText("findSurgeonLastFirst"));
                            }
                            nID_Chirurgen = BusinessLayer.GetID_Chirurgen(surgeonLastName, surgeonFirstName);
                        }
                        catch (MultipleRecordsException)
                        {
                            string s = string.Format(CultureInfo.InvariantCulture, GetText("err_assign1"), surgeonLastName, surgeonFirstName);
                            ImportError(pluginEvent, s);
                            Protokoll(s);
                            goto exit;
                        }
                    }
                }
                else
                {
                    //
                    // identify a person by their last name only
                    //
                    if (_importSingle != null)
                    {
                        if (_logAllActions)
                        {
                            Protokoll(GetText("findSurgeonExplicitLast"));
                        }
                        if (surgeonLastName.Equals((string)_importSingle["Nachname"]))
                        {
                            nID_Chirurgen = (int)_importSingle["ID_Chirurgen"];
                        }
                    }
                    else
                    {
                        try
                        {
                            if (_logAllActions)
                            {
                                Protokoll(GetText("findSurgeonLast"));
                            }
                            nID_Chirurgen = BusinessLayer.GetID_Chirurgen(surgeonLastName);
                        }
                        catch (MultipleRecordsException)
                        {
                            string s = string.Format(CultureInfo.InvariantCulture, GetText("err_assign2"), surgeonLastName);
                            ImportError(pluginEvent, s);
                            Protokoll(s);
                            goto exit;
                        }
                    }
                }
            }

            //
            // 2. Now we insert data
            //

            if ((nID_Chirurgen == -1) && (_importSingle == null) && _bInsertUnknownSurgeon)
            {
                //
                // Wenn der chirurg eingefügt werden soll und er fehlt und wir nicht nur einen bestimmten einlesen
                //

                DataRow oChirurg;

                Protokoll(string.Format(GetText("insert1"), surgeonLastName, surgeonFirstName));
                oChirurg = this.BusinessLayer.CreateDataRowChirurg();
                oChirurg["Nachname"] = surgeonLastName;
                oChirurg["Vorname"] = surgeonFirstName;

                // Der Chirurg braucht eine Funktion, es wird die erstbeste genommen.
                // Sollte die Tabelle leer sein, gibt es einen SQL-Fehler und das ist gut so.
                oChirurg["ID_ChirurgenFunktionen"] = BusinessLayer.GetAnyOneID_ChirurgenFunktionen();
                // automatisch eingefügter Chirurg ist normaler USER

                // TODO: die UserID muss eindeutig sein!
                string userID = BusinessLayer.AutoCreateUniqueUserID(surgeonLastName);
                oChirurg["UserID"] = userID;
                oChirurg["ImportID"] = userID;

                nID_Chirurgen = BusinessLayer.InsertChirurg(oChirurg, null, null);
                if (nID_Chirurgen > 0)
                {
                    Protokoll(string.Format(CultureInfo.InvariantCulture, GetText("insert1"), surgeonLastName, surgeonFirstName));
                }
                else
                {
                    text = string.Format(CultureInfo.InvariantCulture, GetText("insert2"),
                        surgeonFirstName, surgeonLastName,
                        DialogResult.Yes.ToString(),
                        DialogResult.No.ToString(),
                        DialogResult.Cancel.ToString());
                    Protokoll(text);

                    DialogResult result = System.Windows.Forms.MessageBox.Show(text, _businessLayer.AppTitle(),
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Cancel)
                    {
                        _importSuccess = false;
                        pluginEvent.Abort = true;
                    }
                    else if (result == DialogResult.Yes)
                    {
                        BusinessLayer.InsertImportChirurgenExclude(oChirurg);
                    }
                }
            }
            else
            {
                if (_logAllActions)
                {
                    Protokoll(GetText("noinsertSurgeon"));
                }
            }

            if (_bInsertUnknownOperation)
            {
                //
                // 22.11.2008: Operation nur suchen, wenn sie bei Nichtvorhandensein eingefügt werden soll,
                // sonst braucht man das gar nicht.
                //

                //
                // Wenn man nur die Daten von einer bestimmten Person importieren möchte, dann werden auch nur die OPS-Kodes dieser 
                // Person eingefügt falls gewünscht.
                // Entweder _importSingle ist null, dann behandeln wir alle, oder es ist <> null, und wir haben
                // auch Daten dieser Person
                //
                if (_importSingle == null || nID_Chirurgen != -1)
                {
                    // nID_Operationen braucht man nur noch, um einen anderen/neuen OPSCode in die Tabelle Operationen einzufügen.
                    int nID_Operationen = BusinessLayer.GetID_OperationenByOpsKode(opCode);

                    if (nID_Operationen == -1)
                    {
                        DataRow oOperation;

                        Protokoll(GetText("insert3"));
                        oOperation = BusinessLayer.CreateDataRowOperation();
                        oOperation["OPS-Kode"] = opCode;
                        oOperation["OPS-Text"] = opDescription;

                        nID_Operationen = BusinessLayer.InsertOperation(oOperation);
                        if (nID_Operationen > 0)
                        {
                            text = string.Format(CultureInfo.InvariantCulture, GetText("insertOpscodeSuccess"), opCode);
                            Protokoll(text);
                        }
                        else
                        {
                            text = string.Format(CultureInfo.InvariantCulture, GetText("insertOpscodeError"), opCode);
                            ImportError(pluginEvent, text);
                            Protokoll(text);
                        }
                    }
                }
            }

            if (_logAllActions)
            {
                if (nID_Chirurgen > 0)
                {
                    Protokoll(GetText("surgeonExists"));
                }
                else
                {
                    Protokoll(GetText("recordSkippedNoSurgeon"));
                }
            }

            if (nID_Chirurgen > 0)
            {
                DataRow row;

                if (_identifyOpByIdentifier)
                {
                    if (_logAllActions)
                    {
                        Protokoll(GetText("findChirurgenOperationenCodeDateId"));
                    }
                    row = BusinessLayer.CheckChirurgOperationen(nID_Chirurgen, opCode, opDateAndTime, identifier);
                }
                else
                {
                    if (_logAllActions)
                    {
                        Protokoll(GetText("findChirurgenOperationenCodeDate"));
                    }
                    row = BusinessLayer.CheckChirurgOperationen(nID_Chirurgen, opCode, opDateAndTime);
                }
                if (row == null)
                {
                    try
                    {
                        row = BusinessLayer.CreateDataRowChirurgenOperationen(nID_Chirurgen);
                        row["ID_OPFunktionen"] = opFunction;
                        row["ID_Richtlinien"] = DBNull.Value;
                        row["OPS-Kode"] = opCode;
                        row["OPS-Text"] = opDescription;
                        row["Fallzahl"] = identifier;
                        row["Datum"] = opDateAndTime;
                        row["Zeit"] = opDateAndTime;
                        row["ZeitBis"] = opTimeEnd;
                        row["Quelle"] = BusinessLayer.OperationQuelleIntern;

                        int nNewID = BusinessLayer.InsertChirurgenOperationen(row, false);
                        if (nNewID > 0)
                        {
                            _nCountNew++;
                            if (_logAllActions)
                            {
                                Protokoll(GetText("insertChirurgenOperationen"));
                            }
                        }
                        else
                        {
                            text = GetText("errInsertChirurgenOperationen") + row.ToString();
                            ImportError(pluginEvent, text);
                            Protokoll(text);
                        }
                    }
                    catch (Exception ex)
                    {
                        text = GetText("errGeneral") + ": " + ex.Message + " - " + ex.TargetSite;
                        if (ex.StackTrace != null)
                        {
                            text += "\r\n" + ex.StackTrace;
                        }

                        ImportError(pluginEvent, text);
                        this.Protokoll(text);
                    }
                }
                else
                {
                    // OP schon vorhanden
                    if (_logAllActions)
                    {
                        Protokoll(GetText("recordSkippedOpExists"));
                    }
                }
            }

        exit: ;
        }

        private void ImportError(OperationenImportEvent e, string s)
        {
            if (!_bImportError)
            {
                // Nur beim ersten Fehler eine Messagebox anzeigen
                BusinessLayer.MessageBox(s);
            }
            _bImportError = true;
        }

        internal protected bool Import(
            OperationenImport plugin,
            OperationenImportPluginCustomData customData,
            bool useFirstName,
            bool insertUnknownSurgeon,
            bool insertUnknownOperation,
            bool identifyByImportID,
            bool identifyOpByIdentifier,
            DataRow surgeon,
            List<OP_FUNCTION> funktionen,
            bool logAll
            )
        {
            _importSuccess = true;

            if (customData != null)
            {
                _interactive = false;
            }

            _useFirstName = useFirstName;
            _bInsertUnknownSurgeon = insertUnknownSurgeon;
            _bInsertUnknownOperation = insertUnknownOperation;
            _identifyOpByIdentifier = identifyOpByIdentifier;
            _identifyByImportID = identifyByImportID;
            _importSingle = surgeon;
            _funktionen = funktionen;
            _logAllActions = logAll;

            try
            {
                _importProgressEvent.Abort = false;

                BusinessLayer.OpenDatabaseForImport();

                Protokoll(GetText("beginnImport"));

                if (ImportProgress != null)
                {
                    _importProgressEvent.State = ImportProgressState.ProgressBegin;
                    ImportProgress(this, _importProgressEvent);
                }

                plugin.ImportOP += new OperationenImport.OperationenImportHandler(OPImportDelegate);

                _nCountTotal = 0;
                _nCountNew = 0;

                plugin.OPImportInit(customData);
                plugin.OPImportRun();
                plugin.OPImportFinalize();

                plugin.ImportOP -= new OperationenImport.OperationenImportHandler(OPImportDelegate);
            }
            catch (Exception ex)
            {
                _importSuccess = false;
                BusinessLayer.MessageBox(GetText("errImport") + ": " + ex.Message);
            }
            finally
            {
                BusinessLayer.CloseDatabaseForImport();

                Protokoll(GetText("endImport"));
                if (_oProtokoll != null)
                {
                    _oProtokoll.Flush();
                    _oProtokoll.Close();
                    _oProtokoll.Dispose();
                    _oProtokoll = null;
                }

                if (ImportProgress != null)
                {
                    _importProgressEvent.State = ImportProgressState.ProgressEnd;
                    ImportProgress(this, _importProgressEvent);
                }

                if (_interactive)
                {
                    ViewProtocol();
                }
            }

            return _importSuccess;
        }

        private void ViewProtocol()
        {
            try
            {
                if (_strLogfileName != null)
                {
                    Process oProcess = Process.Start(_strLogfileName);
                }
            }
            catch
            {
            }
        }

        public void DumpSettings(OperationenForm form)
        {
            Protokoll("Dumping import GUI settings:");
            _oProtokoll.WriteLine("");

            Visitors.DumpGuiVisitor visitor = new Visitors.DumpGuiVisitor(_oProtokoll);

            form.Accept(visitor);
            _oProtokoll.WriteLine("");
            _oProtokoll.Flush();

        }
    }
}
