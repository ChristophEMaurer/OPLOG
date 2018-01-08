using System;
using System.Data.Common;
using System.Collections;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Globalization;

using Utility;

namespace Operationen.Wizards.ImportOperationenMobile
{
    public class OperationenImporterMobileSQL : ImporterExporter
    {
        private const string FormName = "Wizards_ImportOperationenMobile_sql";

        // Die ID des selektierten Chirurgen
        private int _ID_Chirurgen;

        public OperationenImporterMobileSQL(BusinessLayer b, ProgressBar progressBar, Label lblProgress)
            : base(b, progressBar, lblProgress)
        {
        }

        private void ShowMessageBox(string text)
        {
            _businessLayer.MessageBox(text);
        }

        public void Initialize(string fileName, int ID_Chirurgen)
        {
            Initialize(fileName);
            _ID_Chirurgen = ID_Chirurgen;
        }

        public bool Import()
        {
            bool success = false;

            TheProgressBar.Visible = true;

            try
            {
                // ImportChirurg() sollte eigentlich mit in der Transaktion sein
                // Es soll aber das maximale Datum einer Operation angezeigt werden undin der UI erst noch die Operationen ausgewählt werden.
                // ID_Chirurgen ist die ID aus der aktuellen DB

                if (_ID_Chirurgen <= 0)
                {
                    //
                    // Hat nicht geklappt, oder es gab den schon und keiner wurde ausgewählt.
                    //
                    goto _exit;
                }

                ArrayList sqlParameters = new ArrayList();
                sqlParameters.Add(DatabaseLayer.SqlParameter("@ID_Chirurgen", _ID_Chirurgen));
                DataRow maxOp = DatabaseLayer.GetRecord(
                    "select max(Datum) as Datum from ChirurgenOperationen where ID_Chirurgen = @ID_Chirurgen", 
                    sqlParameters, "ChirurgenOperationen");

                DateTime? dtMaxOP = null;

                if (maxOp != null && maxOp["Datum"] != DBNull.Value)
                {
                    dtMaxOP = (DateTime?)maxOp["Datum"];
                }

                bool insertMultiple = false;

                DataView selectedOperationen = GetOperationenToImport(dtMaxOP, out insertMultiple);
                if (selectedOperationen == null)
                {
                    goto _exit;
                }

                Progress(GetText(FormName, "importops"));
                if (!ImportChirurgenOperationen(selectedOperationen, _ID_Chirurgen, insertMultiple))
                {
                    goto _exit;
                }

                success = true;
            }
            catch (Exception ex)
            {
                _businessLayer.MessageBox(string.Format(GetText(FormName, "err"), ex.Message));
                goto _exit;
            }
            finally
            {
                TheProgressBar.Visible = false;
            }

        _exit:

            TheProgressBar.Value = TheProgressBar.Maximum;

            return success;
        }

        private DateTime? Text2DateTime(string text)
        {
            DateTime? date = null;

            //
            // 0123456789012345
            // 20090330 09:55 5-500.x|Inzision der Leber: SonstigeT
            //

            int year = Convert.ToInt32(text.Substring(0, 4));
            int month = Convert.ToInt32(text.Substring(4, 2));
            int day = Convert.ToInt32(text.Substring(6, 2));
            int hour = Convert.ToInt32(text.Substring(9, 2));
            int minute = Convert.ToInt32(text.Substring(12, 2));

            date = new DateTime(year, month, day, hour, minute, 0);

            return date;
        }

        /// <summary>
        /// Read a JAva modified UTF8 string: 2 bytes followed by the UTF8 string
        /// The two bytes are the bytes of the string, we skip those.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private string ReadJavaModifiedUtf8(BinaryReader reader)
        {
            return Tools.ReadJavaModifiedUtf8(reader);
        }

        private DataView GetOperationenToImport(DateTime? dtMax, out bool insertMultiple)
        {
            DataTable dt = new DataTable("ChirurgenOperationen");
            DataRow dataRow = null;
            DataView selectedOperationen = null;
            insertMultiple = false;

            int ID_KlinischeErgebnisseTypen = _businessLayer.DatabaseLayer.GetIdKlinischeErgebnisseTypenUnauffaellig();

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("ID_Chirurgen", typeof(int)));
            dt.Columns.Add(new DataColumn("ID_OPFunktionen", typeof(int)));
            dt.Columns.Add(new DataColumn("ID_Richtlinien", typeof(int)));
            dt.Columns.Add(new DataColumn("ID_KlinischeErgebnisseTypen", typeof(int)));
            dt.Columns.Add(new DataColumn("OPSKode", typeof(string)));
            dt.Columns.Add(new DataColumn("OPSText", typeof(string)));
            dt.Columns.Add(new DataColumn("Fallzahl", typeof(string)));
            dt.Columns.Add(new DataColumn("Datum", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Zeit", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("ZeitBis", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Quelle", typeof(int)));
            dt.Columns.Add(new DataColumn("KlinischeErgebnisse", typeof(string)));

            BinaryReader reader = null;

            try
            {
                // _U_|1
                //0123456789012345
                //20090330 09:55 5-500.x|Inzision der Leber: SonstigeT

                reader = new BinaryReader(File.Open(_fileName, FileMode.Open));

                string line = ReadJavaModifiedUtf8(reader);
                if (!line.Equals(GlobalConstantsMobile.FileSignatureOPSUserMobile))
                {
                    ShowMessageBox(string.Format(CultureInfo.InvariantCulture, GetText(FormName, "err_read_signature"),
                        _fileName, GlobalConstantsMobile.FileSignatureOPSUserMobile));
                    goto _exit;
                }

                while (((line = ReadJavaModifiedUtf8(reader)) != null) && (line.Length > 16))
                {
                    string datePart = line.Substring(0, 14);
                    DateTime? date = Text2DateTime(datePart);
                    line = line.Substring(15);
                    string[] arLine = line.Split('|');
                    if (date.HasValue && (arLine.Length == 2))
                    {
                        DateTime now = DateTime.Now;

                        dataRow = dt.NewRow();

                        dataRow["ID_Chirurgen"] = _ID_Chirurgen;
                        dataRow["ID_OPFunktionen"] = (int)OP_FUNCTION.OP_FUNCTION_OP;
                        dataRow["ID_Richtlinien"] = DBNull.Value;
                        dataRow["ID_KlinischeErgebnisseTypen"] = ID_KlinischeErgebnisseTypen;
                        dataRow["OPSKode"] = arLine[0];
                        dataRow["OPSText"] = arLine[1];
                        dataRow["Fallzahl"] = "";
                        dataRow["Datum"] = now;
                        dataRow["Zeit"] = now;
                        dataRow["ZeitBis"] = now;
                        dataRow["Quelle"] = BusinessLayer.OperationQuelleIntern;
                        dataRow["KlinischeErgebnisse"] = "";

                        dt.Rows.Add(dataRow);
                    }
                }
            }
            catch
            {
                ShowMessageBox(string.Format(GetText(FormName, "err_read_file"), _fileName));
                goto _exit;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            DataView chirurgenOperationen = new DataView(dt);

            NewOperationenMobileView dlg = new NewOperationenMobileView(_businessLayer, chirurgenOperationen, dtMax);
            dlg.ShowDialog();
            selectedOperationen = dlg.GetSelectedOperationen();
            insertMultiple = dlg.GetInsertMultiple();

        _exit: 
            return selectedOperationen;
        }

        /// <summary>
        /// Importiert alle Operationen des Chirurgen aus ienem DataView
        /// </summary>
        /// <param name="ID_Chirurgen">Die ID des Chirurgen in der Hauptdatenbank, vorhanden oder angelegt</param>
        private bool ImportChirurgenOperationen(DataView selectedOperationen, int ID_Chirurgen, bool insertMultiple)
        {
            bool success = false;

            // Alle Operationen eines Chirurgen durchlaufen...
            foreach (DataRow chirurgenOperation in selectedOperationen.Table.Rows)
            {
                int ID_Operationen;

                ProgressTinyOperation();

                //
                // Es werden alle Operationen eingefügt, auch doppelte oder wenn sie schon vorhanden sind,
                // weil man bei manueller Eingabe viele Operationen mit demselben OPSKode für einen 
                // Zeitstempel eingibt. Von diesen würde sonst nur die erste eingefügt und die restlichen nicht.
                //
                DateTime dtDatum = (DateTime)chirurgenOperation["Datum"];
                DateTime dtZeit = (DateTime)chirurgenOperation["Zeit"];
                DateTime dtDatumZeit = new DateTime(dtDatum.Year, dtDatum.Month, dtDatum.Day, dtZeit.Hour, dtZeit.Minute, dtZeit.Second);

                if (!insertMultiple)
                {
                    // keine Doppelten einfügen
                    // 24.02.2008 Man kann jetzt angeben, ob doppelte eingefügt werden sollen oder nicht
                    DataRow row = _businessLayer.CheckChirurgOperationen(ID_Chirurgen, (string)chirurgenOperation["OPSKode"], dtDatumZeit);
                    if (row != null)
                    {
                        // gibts schon, also mit der nächsten weitermachen
                        continue;
                    }
                }
                
                //
                // Überprüfen, ob es diese Operation schon gibt.
                // Wenn es mehrere gibt, die erste zurückgeben, da ja der OPSKode kopiert wird.
                //
                ArrayList sqlParameters = new ArrayList();
                sqlParameters.Add(DatabaseLayer.SqlParameter("@OPSKode", (string)chirurgenOperation["OPSKode"]));
                DataRow operation = DatabaseLayer.GetRecord(
                    "select ID_Operationen from Operationen where [OPS-Kode]=@OPSKode",
                    sqlParameters,
                    "Operationen", true);

                if (operation == null)
                {
                    // Diese Operation fehlt, also wird sie eingefügt
                    string sql = "insert into Operationen ([OPS-Kode], [OPS-Text]) values (@OPSKode, @OPSText)";
                    sqlParameters = new ArrayList();
                    sqlParameters.Add(DatabaseLayer.SqlParameter("@OPSKode", (string)chirurgenOperation["OPSKode"]));
                    sqlParameters.Add(DatabaseLayer.SqlParameter("@OPSText", (string)chirurgenOperation["OPSText"]));
                    ID_Operationen = DatabaseLayer.InsertRecord(sql, sqlParameters, "Operationen");
                    if (ID_Operationen <= 0)
                    {
                        goto _exit;
                    }
                }
                else
                {
                    ID_Operationen = ConvertToInt32(operation["ID_Operationen"]);
                }

                //
                // Jetzt enthält ID_Operationen die ID der Operation. Entweder gab es sie schon oder
                // sie wurde gerade neu eingefügt
                //
                // ID_Richtlinien ist DBNull.Value.
                //
                DataRow neueChirurgenOperation = _businessLayer.CreateDataRowChirurgenOperationen(ID_Chirurgen);
                //
                // Achtung: ID_OPFunktionen müsste richtig wie ID_Richtlinien behandelt werden. Die Tabelle kann aber nicht geändert
                // werden und hat die beiden festen Werte 1 und 2
                //
                neueChirurgenOperation["ID_Richtlinien"] = chirurgenOperation["ID_Richtlinien"];
                neueChirurgenOperation["ID_OPFunktionen"] = chirurgenOperation["ID_OPFunktionen"];
                neueChirurgenOperation["Fallzahl"] = chirurgenOperation["Fallzahl"];
                neueChirurgenOperation["OPS-Kode"] = chirurgenOperation["OPSKode"];
                neueChirurgenOperation["OPS-Text"] = chirurgenOperation["OPSText"];
                neueChirurgenOperation["Datum"] = dtDatumZeit;
                neueChirurgenOperation["Zeit"] = dtDatumZeit;
                neueChirurgenOperation["ZeitBis"] = chirurgenOperation["ZeitBis"];
                neueChirurgenOperation["Quelle"] = chirurgenOperation["Quelle"];

                int ID_ChirurgenOperationen = _businessLayer.InsertChirurgenOperationen(neueChirurgenOperation, false);

                if (ID_ChirurgenOperationen <= 0)
                {
                    goto _exit;
                }
            }

            success = true;

            _exit:

            return success;
        }

        private void ProgressTinyOperation()
        {
            progressCount++;
            if (progressCount >= ProgressThreshold)
            {
                progressCount = 0;
                Progress();
            }
            Application.DoEvents();
        }
    }
}
