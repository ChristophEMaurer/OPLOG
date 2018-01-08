using System;
using System.Data.Common;
using System.Collections;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Globalization;

using CMaurer.Operationen.AppFramework;

namespace Operationen.Wizards.ImportChirurg
{
    /// <summary>
    /// Alle Datenbankfunktionen in dieser Klasse greifen auf die Datenbank zu,
    /// aus der der Chirurg importiert werden soll. Das ist eine ACCESS-Datenbank,
    /// damit man diese leicht kopieren und mitnehmen kann.
    /// 
    /// Die Funktionen aus der Klasse DatabaseLayer greifen auf die Datenbank zu,
    /// in die der Chirurg importiert werden soll.
    /// </summary>
    public class ChirurgImporterSQL : ImporterExporter
    {
        private const string FormName = "Wizards_ImportChirurg_ChirurgImporterSQL";

        public ChirurgImporterSQL(BusinessLayer businessLayer, ProgressBar progressBar, Label lblProgress)
            : base(businessLayer, progressBar, lblProgress)
        {
        }

        private void ShowMessageBox(string text)
        {
            _businessLayer.MessageBox(text);
        }

        public bool Import()
        {
            bool success = false;

            TheProgressBar.Visible = true;

            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                + _fileName
                + ";Jet OLEDB:Database Password="
                + _businessLayer.Password
                + ";";

            //
            // Hier muss immer der Acess Datenbank Provider verwendet werden, weil alle Daten
            // in die chirurg.mdb expotiert werden
            //
            _dataFactory = DbProviderFactories.GetFactory("System.Data.OleDb");

            using (_connection = _dataFactory.CreateConnection())
            {
                _connection.ConnectionString = connectionString;
                _connection.Open();

                DbTransaction trans = null;

                try
                {
                    _command = _connection.CreateCommand();

                    Progress(GetText(FormName, "checkVersion"));
                    //
                    // Versionen aus der ACCESS-DB holen
                    //
                    DataRow version = GetRecord("select [Value] from Config where [Key] = 'MajorVersion'", null, "Config");
                    if (version == null)
                    {
                        goto _exit;
                    }
                    string majorVersion = (string)version["Value"];

                    version = GetRecord("select [Value] from Config where Key = 'MinorVersion'", null, "Config");
                    if (version == null)
                    {
                        goto _exit;
                    }
                    string minorVersion = (string)version["Value"];
                    if (BusinessLayer.VersionMajor.ToString() != majorVersion 
                        || BusinessLayer.VersionMinor.ToString() != minorVersion)
                    {
                        string msg = string.Format(CultureInfo.InvariantCulture, GetText(FormName, "error1"),
                            majorVersion, minorVersion, BusinessLayer.VersionMajor, BusinessLayer.VersionMinor);
                            
                        ShowMessageBox(msg);
                        goto _exit;
                    }

                    Progress("Chirurgen");

                    // ID_ChirurgenImport ist die ID aus der zu importierenden DB
                    int ID_ChirurgenImport;

                    DataView chirurgen = GetDataView(@"select ID_Chirurgen, Anrede, Nachname, Vorname, UserID from Chirurgen order by Nachname", "Chirurgen");
                    int count = chirurgen.Table.Rows.Count;
                    if (count > 0)
                    {
                        string msg = GetText(FormName, "msg1");

                        ChirurgenView dlg = new ChirurgenView(base._businessLayer, chirurgen,msg);
                        if (DialogResult.OK == dlg.ShowDialog())
                        {
                            ID_ChirurgenImport = dlg.ID_Chirurgen;
                        }
                        else
                        {
                            goto _exit;
                        }
                    }
                    else
                    {
                        // < 1, also kein Chirurg vorhanden
                        ShowMessageBox(GetText(FormName, "msg2"));
                        goto _exit;
                    }

                    // ImportChirurg() sollte eigentlich mit in der Transaktion sein
                    // Es soll aber das maximale Datum einer Operation angezeigt werden undin der UI erst noch die Operationen ausgewählt werden.
                    // ID_Chirurgen ist die ID aus der aktuellen DB
                    int ID_Chirurgen = ImportChirurg(ID_ChirurgenImport);

                    if (ID_Chirurgen <= 0)
                    {
                        //
                        // Hat nicht geklappt, oder es gab den schon und keiner wurde ausgewählt.
                        //
                        goto _exit;
                    }

                    ArrayList sqlParameters = new ArrayList();
                    sqlParameters.Add(DatabaseLayer.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                    DataRow maxOp = DatabaseLayer.GetRecord( "select max(Datum) as Datum from ChirurgenOperationen where ID_Chirurgen = @ID_Chirurgen", sqlParameters, "ChirurgenOperationen");
                    DateTime? dtMaxOP = null;

                    if (maxOp != null && maxOp["Datum"] != DBNull.Value)
                    {
                        dtMaxOP = (DateTime?)maxOp["Datum"];
                    }

                    bool insertMultiple = false;

                    DataView selectedOperationen = GetOperationenToImport(ID_ChirurgenImport, dtMaxOP, out insertMultiple);
                    if (selectedOperationen == null)
                    {
                        goto _exit;
                    }

                    // Ab hier bis zum Commit/Rollback darf keine Benutzer-Interaktion sein.
                    trans = _connection.BeginTransaction();
                    _command.Transaction = trans;

                    Progress("Operationen");
                    if (!ImportChirurgenOperationen(selectedOperationen, ID_Chirurgen, insertMultiple))
                    {
                        goto _exit;
                    }

                    Progress("Notiztypen");
                    if (!ImportNotizTypen())
                    {
                        goto _exit;
                    }

                    Progress("Notizen");
                    if (!ImportNotizen(ID_Chirurgen, ID_ChirurgenImport))
                    {
                        goto _exit;
                    }

                    Progress("AkademischeAusbildungTypen");
                    if (!ImportAkademischeAusbildungTypen())
                    {
                        goto _exit;
                    }

                    Progress("AkademischeAusbildungen");
                    if (!ImportAkademischeAusbildungen(ID_Chirurgen, ID_ChirurgenImport))
                    {
                        goto _exit;
                    }

                    Progress("Gebiete");
                    if (!ImportGebiete())
                    {
                        goto _exit;
                    }

                    Progress("ChirurgenGebiete");
                    if (!ImportChirurgenGebiete(ID_Chirurgen, ID_ChirurgenImport))
                    {
                        goto _exit;
                    }

                    success = true;
                }
                catch (Exception ex)
                {
                    _businessLayer.MessageBox(GetText(FormName, "msg3") + "\r\r" + ex.Message);
                    goto _exit;
                }
                finally
                {
                    if (trans != null)
                    {
                        if (success)
                        {
                            trans.Commit();
                        }
                        else
                        {
                            trans.Rollback();
                        }
                    }

                    TheProgressBar.Visible = false;

                    if (_command != null)
                    {
                        _command.Dispose();
                        _command = null;
                    }
                    if (_connection != null)
                    {
                        if (_connection.State == ConnectionState.Open)
                        {
                            _connection.Close();
                        }
                        _connection.Dispose();
                        _connection = null;
                    }
                }
            }

        _exit:

            TheProgressBar.Value = TheProgressBar.Maximum;

            return success;
        }

        private DataView GetOperationenToImport(int ID_ChirurgenImport, DateTime? dtMax, out bool insertMultiple)
        {
            string sql = @"select
                    a.ID_ChirurgenOperationen,
                    a.ID_OPFunktionen,
                    a.ID_Richtlinien,
                    a.ID_KlinischeErgebnisseTypen,
                    a.Fallzahl,
                    a.[OPS-Kode] as OPSKode,
                    a.[OPS-Text] as OPSText,
                    a.Datum,
                    a.Zeit,
                    a.ZeitBis,
                    a.Quelle,
                    a.KlinischeErgebnisse,
                    b.LfdNummer,
                    b.UntBehMethode,
                    b.Richtzahl,
                    c.Gebiet,
                    c.Bemerkung,
                    c.Herkunft                
                from ChirurgenOperationen a left join (Richtlinien b left join Gebiete c on b.ID_Gebiete = c.ID_Gebiete) on a.ID_Richtlinien = b.ID_Richtlinien
                where ID_Chirurgen = @ID_Chirurgen
                order by a.Datum desc
                ";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_ChirurgenImport));
            DataView chirurgenOperationen = GetDataView(sql, sqlParameters, "ChirurgenOperationen");

            NewOperationenView dlg = new NewOperationenView(_businessLayer, chirurgenOperationen, dtMax);
            dlg.ShowDialog();
            DataView selectedOperationen = dlg.GetSelectedOperationen();
            insertMultiple = dlg.GetInsertMultiple();

            return selectedOperationen;
        }

        /// <summary>
        /// Wenn es den Chirurgen nocht nicht gibt, true.
        /// Wenn es schon einen oder mehrere mit diesem Namen gibt und man einen auswählt, true.
        /// Ansonsten: false
        /// </summary>
        /// <param name="chirurg">Der Chirurg aus der fremden Datenbank</param>
        /// <returns></returns>
        private bool CheckChirurg(DataRow chirurg, ref int ID_Chirurgen)
        {
            bool success = false;
            ArrayList sqlParameters = null;

            //
            // Gibt es diesen Chirurgen schon?
            //
            sqlParameters = new ArrayList();
            sqlParameters.Add(DatabaseLayer.SqlParameter("@Nachname", chirurg["Nachname"]));
            sqlParameters.Add(DatabaseLayer.SqlParameter("@Vorname", chirurg["Vorname"]));

            DataView chirurgen = DatabaseLayer.GetDataView(
                @"select ID_Chirurgen, Anrede, Nachname, Vorname, UserID from Chirurgen where Nachname=@Nachname and Vorname=@Vorname", 
                sqlParameters, 
                "Chirurgen");

            if (chirurgen.Table.Rows.Count > 0)
            {
                ChirurgenView dlg = new ChirurgenView(
                    base._businessLayer, 
                    chirurgen,
                    "Der Arzt, der importiert werden soll, ist bereits unter diesem Namen vorhanden."
                    + " Wählen Sie einen vorhandenen Arzt aus und klicken Sie 'OK', um die neuen Daten zu dem ausgewählten Arzt " 
                    + " hinzuzufügen."
                    + "\rKlicken Sie 'Abbrechen', um den Import abzubrechen."
                    );
                if (DialogResult.OK == dlg.ShowDialog())
                {
                    ID_Chirurgen = dlg.ID_Chirurgen;
                }
                else
                {
                    goto _exit;
                }
            }
            
            success = true;

            _exit:
            return success;
        }


        /// <summary>
        /// Holt den Chirurgen mit ID_ChirurgenImport aus der Fremddatenbank
        /// und fügt ihn hier ein
        /// </summary>
        /// <param name="ID_ChirurgenImport">Die ID aus der externen Datenbank des Chirurgen, der importiert werden soll </param>
        private int ImportChirurg(int ID_ChirurgenImport)
        {
            int ID_Chirurgen = -1;

            Progress();

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_ChirurgenImport));

            DataRow chirurgImport = GetRecord(@"
                select 
                    Chirurgen.ID_Chirurgen, 
                    Chirurgen.ID_ChirurgenFunktionen, 
                    Chirurgen.Nachname, 
                    Chirurgen.Vorname, 
                    Chirurgen.Anfangsdatum, 
                    Chirurgen.Anrede, 
                    Chirurgen.[UserID], 
                    Chirurgen.ImportID, 
                    Chirurgen.[Password], 
                    Chirurgen.Aktiv, 
                    Chirurgen.IstWeiterbilder, 
                    Chirurgen.Lizenzdaten, 
                    ChirurgenFunktionen.Funktion 
                from Chirurgen 
                    inner join ChirurgenFunktionen on Chirurgen.ID_ChirurgenFunktionen = ChirurgenFunktionen.ID_ChirurgenFunktionen
                where Chirurgen.ID_Chirurgen=@ID_Chirurgen", sqlParameters, "Chirurgen");

            if (chirurgImport != null)
            {
                if (CheckChirurg(chirurgImport, ref ID_Chirurgen))
                {
                    if (ID_Chirurgen == -1)
                    {
                        //
                        // Es gab den Chirurgen noch nicht.
                        //

                        //
                        // ID_ChirurgenFunktionen/Funktion muss in beiden Datenbanken gleich sein!
                        //
                        sqlParameters = new ArrayList();
                        sqlParameters.Add(DatabaseLayer.SqlParameter("@Funktion", chirurgImport["Funktion"]));

                        DataRow titel = DatabaseLayer.GetRecord("select ID_ChirurgenFunktionen, Funktion from ChirurgenFunktionen where Funktion=@Funktion", sqlParameters, "ChirurgenFunktionen");
                        if (titel == null)
                        {
                            // Diesen Titel gibt es nicht, also einfügen
                            sqlParameters = new ArrayList();
                            sqlParameters.Add(DatabaseLayer.SqlParameter("@Funktion", chirurgImport["Funktion"]));
                            int ID_ChirurgenFunktionen = DatabaseLayer.InsertRecord("insert into ChirurgenFunktionen (Funktion) Values (@Funktion)", sqlParameters, "ChirurgenFunktionen");
                        }
                        else
                        {
                            chirurgImport["ID_ChirurgenFunktionen"] = titel["ID_ChirurgenFunktionen"];
                        }

                        //
                        // Wenn ein neuer Chirurg eingefügt wird, darf es dessen UserID nicht schon geben
                        //
                        string oldUserId = (string)chirurgImport["UserID"];
                        long count = DatabaseLayer.GetChirurgenCountUserId(oldUserId);
                        if (count > 0)
                        {
                            string newUniqueUserID = _businessLayer.AutoCreateUniqueUserID((string)chirurgImport["Nachname"]);
                            count = DatabaseLayer.GetChirurgenCountUserId(newUniqueUserID);
                            if (count > 0)
                            {
                                // Es konnte automatisch keine eindeutige UserID erzeugt werden. Mit Meldung abbrechen.
                                string msg = string.Format(CultureInfo.InvariantCulture, GetText(FormName, "msg4"), oldUserId);
                                ShowMessageBox(msg);

                                goto _exit;
                            }
                            else
                            {
                                chirurgImport["UserID"] = newUniqueUserID;
                            }
                        }

                        ID_Chirurgen = DatabaseLayer.InsertChirurg(chirurgImport, null, null);
                    }
                }
            }

            _exit:
            return ID_Chirurgen;
        }

        /// <summary>
        /// Importiert alle Operationen des Chirurgen aus der Import-Datenbank
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
                // ChirurgenOperation.ID_Richtlinien 
                // 
                if (chirurgenOperation["ID_Richtlinien"] != DBNull.Value)
                {
                    //
                    // ID_Richtlinien muss wie ID_OPFunktionen inhaltlich gleich sein:
                    // Die Richtlinie und auch das Gebiet, für das sie gilt.
                    //
                    sqlParameters = new ArrayList();
                    sqlParameters.Add(DatabaseLayer.SqlParameter("@LfdNummer", chirurgenOperation["LfdNummer"]));
                    sqlParameters.Add(DatabaseLayer.SqlParameter("@UntBehMethode", chirurgenOperation["UntBehMethode"]));
                    sqlParameters.Add(DatabaseLayer.SqlParameter("@Richtzahl", chirurgenOperation["Richtzahl"]));
                    sqlParameters.Add(DatabaseLayer.SqlParameter("@Gebiet", chirurgenOperation["Gebiet"]));

                    DataRow richtlinie = DatabaseLayer.GetRecord(@"
                    select 
                        a.ID_Richtlinien
                    from Richtlinien a inner join Gebiete b on a.ID_Gebiete = b.ID_Gebiete
                    where 
                        a.LfdNummer = @LfdNummer and 
                        a.UntBehMethode = @UntBehMethode and
                        a.Richtzahl = @Richtzahl and
                        b.Gebiet = @Gebiet", 
                        sqlParameters, "Richtlinien");

                    //
                    // Wenn die Richtlinie nicht mit dem Gebiet genau gleich ist, wird ChirurgenOperationen.ID_Richtlinien auf NULL gesetzt
                    //
                    if (richtlinie == null)
                    {
                        chirurgenOperation["ID_Richtlinien"] = DBNull.Value;
                    }
                    else
                    {
                        // Richtlinie ist gleich, man muss aber den richtigen foreign key nehmen, der kann hier beliebig anders sein.
                        chirurgenOperation["ID_Richtlinien"] = richtlinie["ID_Richtlinien"];
                    }
                }

                //
                // Jetzt enthält ID_Operationen die ID der Operation. Entweder gab es sie schon oder
                // sie wurde gerade neu eingefügt
                //
                // ID_Richtlinien stimmt inhaltlich genau überein oder war oder wurde auf null/den richtigen FK gesetzt.
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

        private bool ImportNotizTypen()
        {
            bool success = false;
            ArrayList sqlParameters;
            int ID_NotizTypen = -1;

            string sql = @"select
                    [Text]
                from NotizTypen";

            // aus der Import-Datenbank
            DataView dv = GetDataView(sql, null, "NotizTypen");

            // Alle NotizTypen durchlaufen...
            foreach (DataRow row in dv.Table.Rows)
            {
                ProgressTinyOperation();

                //
                // Überprüfen, ob es diesen NotizTypen schon gibt.
                // Wenn es mehrere gibt, die erste zurückgeben.
                //
                sqlParameters = new ArrayList();
                sqlParameters.Add(DatabaseLayer.SqlParameter("@Text", (string)row["Text"]));
                DataRow rowLocal = DatabaseLayer.GetRecord(
                    "select ID_NotizTypen from NotizTypen where [Text]=@Text",
                    sqlParameters,
                    "NotizTypen", true);

                if (rowLocal == null)
                {
                    // Diese NotizTypen fehlt, also wird sie eingefügt
                    sql = "insert into NotizTypen ([Text]) values (@Text)";
                    sqlParameters = new ArrayList();
                    sqlParameters.Add(DatabaseLayer.SqlParameter("@Text", (string)row["Text"]));

                    // ignore return value, insert as many as possible
                    ID_NotizTypen = DatabaseLayer.InsertRecord(sql, sqlParameters, "NotizTypen");
                }
            }

            success = true;

            return success;
        }

        private bool ImportNotizen(int ID_Chirurgen, int ID_ChirurgenImport)
        {
            bool success = false;
            ArrayList sqlParameters;
            int ID_Notizen = -1;

            string sql = @"select
                    a.Datum,
                    a.Ende,
                    a.Notiz,
                    b.[Text]
                from Notizen as a inner join NotizTypen as b on a.ID_NotizTypen = b.ID_NotizTypen
                where a.ID_Chirurgen=@ID_Chirurgen";

            // Aus Import DB alle Notizen holen
            sqlParameters = new ArrayList();
            sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_ChirurgenImport));
            DataView dv = GetDataView(sql, sqlParameters, "Notizen");

            foreach (DataRow row in dv.Table.Rows)
            {
                ProgressTinyOperation();

                //
                // Überprüfen, ob es diese Notiz schon gibt.
                // Wenn es mehrere gibt, die erste zurückgeben.
                //
                sqlParameters = new ArrayList();
                sqlParameters.Add(DatabaseLayer.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                sqlParameters.Add(DatabaseLayer.SqlParameter("@Datum", row["Datum"]));
                sqlParameters.Add(DatabaseLayer.SqlParameter("@Ende", row["Ende"]));
                sqlParameters.Add(DatabaseLayer.SqlParameter("@Notiz", row["Notiz"]));
                sqlParameters.Add(DatabaseLayer.SqlParameter("@Text", row["Text"]));
                int count = DatabaseLayer.ExecuteScalarInteger(
                    @"select count(*) 
                    from Notizen as a inner join NotizTypen as b on a.ID_NotizTypen = b.ID_NotizTypen
                    where a.ID_Chirurgen=@ID_Chirurgen
                        and a.[Datum]=@Datum and a.Ende=@Ende and a.Notiz=@Notiz and b.[Text]=@Text",
                    sqlParameters);

                if (count == 0)
                {
                    // Diese Notiz fehlt, also wird sie eingefügt. Man braucht noch die ID_NotizTypen aus der
                    // aktuellen Datenbank.
                    sqlParameters = new ArrayList();
                    sqlParameters.Add(DatabaseLayer.SqlParameter("@Text", row["Text"]));
                    DataRow notizTyp = DatabaseLayer.GetRecord(
                        @"select ID_NotizTypen, [Text] 
                        from NotizTypen 
                        where [Text]=@Text",
                        sqlParameters,
                        "NotizTypen", true);

                    if (notizTyp != null)
                    {
                        sql = @"insert into Notizen (ID_NotizTypen, ID_Chirurgen, Datum, Ende, Notiz) 
                            values (@ID_NotizTypen, @ID_Chirurgen, @Datum, @Ende, @Notiz)";

                        sql = sql.Replace("@Datum", DatabaseLayer.Object2DBDateString(row["Datum"]));
                        sql = sql.Replace("@Ende", DatabaseLayer.Object2DBDateString(row["Ende"]));

                        sqlParameters = new ArrayList();
                        sqlParameters.Add(DatabaseLayer.SqlParameter("@ID_NotizTypen", notizTyp["ID_NotizTypen"]));
                        sqlParameters.Add(DatabaseLayer.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                        sqlParameters.Add(DatabaseLayer.SqlParameter("@Notiz", row["Notiz"]));
                        ID_Notizen = DatabaseLayer.InsertRecord(sql, sqlParameters, "Notizen");
                    }
                }
            }

            success = true;

            return success;
        }

        private bool ImportAkademischeAusbildungTypen()
        {
            bool success = false;
            ArrayList sqlParameters;
            int ID_AkademischeAusbildungTypen = -1;

            string sql = @"select [Text] from AkademischeAusbildungTypen";

            // aus der Import-Datenbank
            DataView dv = GetDataView(sql, null, "AkademischeAusbildungTypen");

            // Alle NotizTypen durchlaufen...
            foreach (DataRow row in dv.Table.Rows)
            {
                ProgressTinyOperation();

                //
                // Überprüfen, ob es diesen AkademischeAusbildungTypen schon gibt.
                // Wenn es mehrere gibt, die erste zurückgeben.
                //
                sqlParameters = new ArrayList();
                sqlParameters.Add(DatabaseLayer.SqlParameter("@Text", (string)row["Text"]));
                DataRow rowLocal = DatabaseLayer.GetRecord(
                    "select ID_AkademischeAusbildungTypen from AkademischeAusbildungTypen where [Text]=@Text",
                    sqlParameters,
                    "AkademischeAusbildungTypen", true);

                if (rowLocal == null)
                {
                    // Diese ID_AkademischeAusbildungTypen fehlt, also wird sie eingefügt
                    sql = "insert into AkademischeAusbildungTypen ([Text]) values (@Text)";
                    sqlParameters = new ArrayList();
                    sqlParameters.Add(DatabaseLayer.SqlParameter("@Text", (string)row["Text"]));

                    // ignore return value, insert as many as possible
                    ID_AkademischeAusbildungTypen = DatabaseLayer.InsertRecord(sql, sqlParameters, "AkademischeAusbildungTypen");
                }
            }

            success = true;

            return success;
        }

        private bool ImportAkademischeAusbildungen(int ID_Chirurgen, int ID_ChirurgenImport)
        {
            bool success = false;
            ArrayList sqlParameters;
            int ID_AkademischeAusbildungen = -1;

            string sql = @"select
                    a.Beginn,
                    a.Ende,
                    a.Organisation,
                    b.[Text]
                from AkademischeAusbildungen as a inner join AkademischeAusbildungTypen as b on a.ID_AkademischeAusbildungTypen = b.ID_AkademischeAusbildungTypen
                where a.ID_Chirurgen=@ID_Chirurgen";

            // Aus Import DB alle Werte holen
            sqlParameters = new ArrayList();
            sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_ChirurgenImport));
            DataView dv = GetDataView(sql, sqlParameters, "AkademischeAusbildungen");

            foreach (DataRow row in dv.Table.Rows)
            {
                ProgressTinyOperation();

                //
                // Überprüfen, ob es diesen Eintrag schon gibt.
                // Wenn es mehrere gibt, die erste zurückgeben.
                //
                sqlParameters = new ArrayList();
                sqlParameters.Add(DatabaseLayer.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                sqlParameters.Add(DatabaseLayer.SqlParameter("@Beginn", row["Beginn"]));
                sqlParameters.Add(DatabaseLayer.SqlParameter("@Ende", row["Ende"]));
                sqlParameters.Add(DatabaseLayer.SqlParameter("@Organisation", row["Organisation"]));
                sqlParameters.Add(DatabaseLayer.SqlParameter("@Text", row["Text"]));
                int count = DatabaseLayer.ExecuteScalarInteger(
                    @"select count(*) 
                    from AkademischeAusbildungen as a inner join AkademischeAusbildungTypen as b on a.ID_AkademischeAusbildungTypen = b.ID_AkademischeAusbildungTypen
                    where a.ID_Chirurgen=@ID_Chirurgen
                        and a.Beginn=@Beginn and a.Ende=@Ende and a.Organisation=@Organisation and b.[Text]=@Text",
                    sqlParameters);

                if (count == 0)
                {
                    // Dieser Eintrag fehlt, also wird er eingefügt. Man braucht noch die ID_* aus der
                    // aktuellen Datenbank.
                    sqlParameters = new ArrayList();
                    sqlParameters.Add(DatabaseLayer.SqlParameter("@Text", row["Text"]));
                    DataRow typ = DatabaseLayer.GetRecord(
                        @"select ID_AkademischeAusbildungTypen, [Text] 
                        from AkademischeAusbildungTypen 
                        where [Text]=@Text",
                        sqlParameters,
                        "AkademischeAusbildungTypen", true);

                    if (typ != null)
                    {
                        sql = @"insert into AkademischeAusbildungen (ID_AkademischeAusbildungTypen, ID_Chirurgen, Beginn, Ende, Organisation) 
                            values (@ID_AkademischeAusbildungTypen, @ID_Chirurgen, @Beginn, @Ende, @Organisation)";

                        sql = sql.Replace("@Beginn", DatabaseLayer.Object2DBDateString(row["Beginn"]));
                        sql = sql.Replace("@Ende", DatabaseLayer.Object2DBDateString(row["Ende"]));

                        sqlParameters = new ArrayList();
                        sqlParameters.Add(DatabaseLayer.SqlParameter("@ID_AkademischeAusbildungTypen", typ["ID_AkademischeAusbildungTypen"]));
                        sqlParameters.Add(DatabaseLayer.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                        sqlParameters.Add(DatabaseLayer.SqlParameter("@Organisation", row["Organisation"]));
                        ID_AkademischeAusbildungen = DatabaseLayer.InsertRecord(sql, sqlParameters, "AkademischeAusbildungen");
                    }
                }
            }

            success = true;

            return success;
        }

        private bool ImportGebiete()
        {
            bool success = false;
            ArrayList sqlParameters;
            int ID_Gebiete = -1;

            string sql = @"select Gebiet, Bemerkung, Herkunft from Gebiete";

            // aus der Import-Datenbank
            DataView dv = GetDataView(sql, null, "Gebiete");

            // Alle NotizTypen durchlaufen...
            foreach (DataRow row in dv.Table.Rows)
            {
                ProgressTinyOperation();

                //
                // Überprüfen, ob es diesen Eintrag schon gibt.
                // Wenn es mehrere gibt, den ersten zurückgeben.
                //
                sqlParameters = new ArrayList();
                sqlParameters.Add(DatabaseLayer.SqlParameter("@Gebiet", row["Gebiet"]));
                DataRow rowLocal = DatabaseLayer.GetRecord(
                    "select ID_Gebiete from Gebiete where Gebiet=@Gebiet",
                    sqlParameters,
                    "Gebiete", true);

                if (rowLocal == null)
                {
                    // Dieser Eintrag fehlt, also wird er eingefügt
                    sql = "insert into Gebiete (Gebiet, Bemerkung, Herkunft) values (@Gebiet, @Bemerkung, @Herkunft)";
                    sqlParameters = new ArrayList();
                    sqlParameters.Add(DatabaseLayer.SqlParameter("@Gebiet", row["Gebiet"]));
                    sqlParameters.Add(DatabaseLayer.SqlParameter("@Bemerkung", row["Bemerkung"]));
                    sqlParameters.Add(DatabaseLayer.SqlParameter("@Herkunft", row["Herkunft"]));

                    // ignore return value, insert as many as possible
                    ID_Gebiete = DatabaseLayer.InsertRecord(sql, sqlParameters, "Gebiete");
                }
            }

            success = true;

            return success;
        }

        //
        // Pro Chirurg und Gebiet darf es nur einen Eintrag geben. Das Datum ist irrelevant
        //
        private bool ImportChirurgenGebiete(int ID_Chirurgen, int ID_ChirurgenImport)
        {
            bool success = false;
            ArrayList sqlParameters;
            int ID_ChirurgenGebiete = -1;

            // Aus Import DB alle Einträge holen
            string sql = @"select
                    a.GebietVon,
                    a.GebietBis,
                    b.Gebiet
                from ChirurgenGebiete as a inner join Gebiete as b on a.ID_Gebiete = b.ID_Gebiete
                where a.ID_Chirurgen=@ID_Chirurgen";

            sqlParameters = new ArrayList();
            sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_ChirurgenImport));
            DataView dv = GetDataView(sql, sqlParameters, "ChirurgenGebiete");

            foreach (DataRow row in dv.Table.Rows)
            {
                ProgressTinyOperation();

                //
                // Überprüfen, ob es diesen Eintrag schon gibt.
                // Wenn es mehrere gibt, den ersten zurückgeben.
                // Für das Vorhandensein zählen nur ID_Chirurgen und Gebiete
                //
                sqlParameters = new ArrayList();
                sqlParameters.Add(DatabaseLayer.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                sqlParameters.Add(DatabaseLayer.SqlParameter("@Gebiet", row["Gebiet"]));
                int count = DatabaseLayer.ExecuteScalarInteger(
                    @"select count(*) 
                    from ChirurgenGebiete as a inner join Gebiete as b on a.ID_Gebiete = b.ID_Gebiete
                    where a.ID_Chirurgen=@ID_Chirurgen
                        and b.[Gebiet]=@Gebiet",
                    sqlParameters);

                if (count == 0)
                {
                    // Dieser Eintrag fehlt, also wird er eingefügt. Man braucht noch die ID_Gebiete aus der
                    // aktuellen Datenbank.
                    sqlParameters = new ArrayList();
                    sqlParameters.Add(DatabaseLayer.SqlParameter("@Gebiet", row["Gebiet"]));
                    DataRow typ = DatabaseLayer.GetRecord(
                        @"select ID_Gebiete, Gebiet
                        from Gebiete 
                        where Gebiet=@Gebiet",
                        sqlParameters,
                        "Gebiete", true);

                    if (typ != null)
                    {
                        sql = @"insert into ChirurgenGebiete (ID_Gebiete, ID_Chirurgen, GebietVon, GebietBis) 
                            values (@ID_Gebiete, @ID_Chirurgen, @GebietVon, @GebietBis)";

                        sql = sql.Replace("@GebietVon", DatabaseLayer.Object2DBDateString(row["GebietVon"]));
                        sql = sql.Replace("@GebietBis", DatabaseLayer.Object2DBDateString(row["GebietBis"]));

                        sqlParameters = new ArrayList();
                        sqlParameters.Add(DatabaseLayer.SqlParameter("@ID_Gebiete", typ["ID_Gebiete"]));
                        sqlParameters.Add(DatabaseLayer.SqlParameter("@ID_Chirurgen", ID_Chirurgen));
                        sqlParameters.Add(DatabaseLayer.SqlParameter("@Gebiet", row["Gebiet"]));
                        ID_ChirurgenGebiete = DatabaseLayer.InsertRecord(sql, sqlParameters, "ChirurgenGebiete");
                    }
                }
            }

            success = true;

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
        public IDbDataParameter SqlParameterInt(string sParameterName, int iValue)
        {
            // OleDB-Parameters beziehen sich auf die Position, nicht auf den Namen!!!
            // Reihenfolge ist wichtig!!!
            IDbDataParameter oSQLParameter = _dataFactory.CreateParameter();
            oSQLParameter.ParameterName = sParameterName;
            oSQLParameter.DbType = DbType.Int32;
            oSQLParameter.Value = iValue;
            return oSQLParameter;
        }

        public IDbDataParameter SqlParameter(string sParameterName, int iValue)
        {
            // OleDB-Parameters beziehen sich auf die Position, nicht auf den Namen!!!
            // Reihenfolge ist wichtig!!!
            IDbDataParameter oSQLParameter = _dataFactory.CreateParameter();
            oSQLParameter.ParameterName = sParameterName;
            oSQLParameter.DbType = DbType.Int32;
            oSQLParameter.Value = iValue;
            return oSQLParameter;
        }

        public IDbDataParameter SqlParameter(string sParameterName, string sValue)
        {
            // OleDB-Parameters beziehen sich auf die Position, nicht auf den Namen!!!
            // Reihenfolge ist wichtig!!!
            IDbDataParameter oSQLParameter = _dataFactory.CreateParameter();
            oSQLParameter.ParameterName = sParameterName;
            oSQLParameter.DbType = DbType.String;
            oSQLParameter.Value = sValue;
            return oSQLParameter;
        }

        public IDbDataParameter SqlParameter(string sParameterName, object value)
        {
            IDbDataParameter oSQLParameter = _dataFactory.CreateParameter();
            oSQLParameter.ParameterName = sParameterName;

            if (value is Int32)
            {
                oSQLParameter.DbType = DbType.Int32;
            }
            else if (value is string)
            {
                oSQLParameter.DbType = DbType.String;
            }
            else if (value is byte[])
            {
                // ChirurgenDokumente.Blob
                oSQLParameter.DbType = DbType.Binary;
            }
            else
            {
                // bei DBNull.Value
                oSQLParameter.DbType = DbType.Object;
            }

            oSQLParameter.Value = value;
            return oSQLParameter;
        }

        protected IDbDataParameter SqlParameter(string sParameterName, byte[] arValue)
        {
            // OleDB-Parameters beziehen sich auf die Position, nicht auf den Namen!!!
            // Reihenfolge ist wichtig!!!
            IDbDataParameter oSQLParameter = _dataFactory.CreateParameter();
            oSQLParameter.ParameterName = sParameterName;
            oSQLParameter.DbType = DbType.Binary;
            oSQLParameter.Value = arValue;
            return oSQLParameter;
        }

        protected IDbDataParameter SqlParameterInt(string sParameterName, object o)
        {
            // OleDB-Parameters beziehen sich auf die Position, nicht auf den Namen!!!
            // Reihenfolge ist wichtig!!!
            IDbDataParameter oSQLParameter = _dataFactory.CreateParameter();
            oSQLParameter.ParameterName = sParameterName;
            oSQLParameter.DbType = DbType.Int32;
            if (o == DBNull.Value)
            {
                oSQLParameter.Value = DBNull.Value;
            }
            else
            {
                oSQLParameter.Value = ConvertToInt32(o);
            }
            return oSQLParameter;
        }
    }
}
