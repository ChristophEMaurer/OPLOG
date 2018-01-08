using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Globalization;

using CMaurer.Operationen.AppFramework;

namespace Operationen.Wizards.ExportChirurg
{
    public class ChirurgExporterSQL : ImporterExporter
    {
        private string FormName = "Wizards_ExportChirurg_ChirurgExporterSQL";
        private int _nID_Chirurgen;

        public ChirurgExporterSQL(BusinessLayer businessLayer, ProgressBar progressBar, Label lblProgress)
            : base(businessLayer, progressBar, lblProgress)
        {
        }

        public void Initialize(int nID_Chirurgen, string fileName)
        {
            _nID_Chirurgen = nID_Chirurgen;

            base.Initialize(fileName);
        }

        private bool ChirurgExists(int ID_Chirurgen)
        {
            string sql = "select count(ID_Chirurgen) from Chirurgen where ID_Chirurgen = @ID_Chirurgen";

            ArrayList sqlParameters = new ArrayList();
            sqlParameters.Add(SqlParameter("@ID_Chirurgen", ID_Chirurgen));

            long count = ExecuteScalar(sql, sqlParameters);

            return (count > 0);
        }

        public bool Export()
        {
            bool success = false;

            TheProgressBar.Visible = true;

            Progress(GetText(FormName, "copyDbTemplate"));

            if (!_businessLayer.CopyFile(_businessLayer.PathApplication + System.IO.Path.DirectorySeparatorChar + "chirurg.mdb", _fileName, false, false))
            {
                goto _exit;
            }

            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                + _fileName
                + ";Jet OLEDB:Database Password="
                + _businessLayer.Password
                + ";";

            //
            // Hier muss immer der Acess Datenbank Provider verwendet werden, weil alle Daten
            // in die chirurg.mdb expotiert werden
            //
            _dataFactory  = DbProviderFactories.GetFactory("System.Data.OleDb");

            using (_connection = _dataFactory.CreateConnection())
            {
                _connection.ConnectionString = connectionString;
                _connection.Open();
                DbTransaction trans = _connection.BeginTransaction();

                try
                {
                    _command = _connection.CreateCommand();
                    _command.Transaction = trans;

                    int ID_Chirurgen = _nID_Chirurgen;

                    //
                    // Die Reihenfolge hier ist die logische gem‰ﬂ der foreign keys usw.
                    //
                    Progress(GetText(FormName, "export_aa"));
                    CopyAkademischeAusbildungTypen();
                    CopyAkademischeAusbildungen(ID_Chirurgen);

                    Progress(GetText(FormName, "export_gebiete"));
                    CopyGebiete();

                    Progress(GetText(FormName, "export_dienststellung"));
                    CopyChirurgenFunktionen();

                    Progress(GetText(FormName, "export_dateien"));
                    CopyDokumente();

                    Progress(GetText(FormName, "export_opfkt"));
                    Progress("");
                    CopyOPFunktionen();

                    Progress(GetText(FormName, "export_op"));
                    CopyOperationen();

                    Progress(GetText(FormName, "export_dateiarten"));
                    CopyDateiTypen();

                    Progress(GetText(FormName, "export_dateien"));
                    CopyDateien();

                    Progress(GetText(FormName, "export_exclude"));
                    CopyImportChirurgenExclude();

                    Progress(GetText(FormName, "export_notiztypen"));
                    CopyNotizTypen();

                    Progress(GetText(FormName, "export_richtlinien"));
                    CopyRichtlinien();

                    Progress(GetText(FormName, "export_klinErgTypen"));
                    CopyKlinischeErgebnisseTypen();

                    Progress(GetText(FormName, "export_chirurg"));
                    CopyChirurg(ID_Chirurgen);

                    Progress(GetText(FormName, "export_chirurgenGebiete"));
                    CopyChirurgenGebiete(ID_Chirurgen);

                    Progress(GetText(FormName, "export_chirurgenOperationen"));
                    CopyChirurgenOperationen(ID_Chirurgen);

                    Progress(GetText(FormName, "export_chirurgenDokumente"));
                    CopyChirurgenDokumente(ID_Chirurgen);

                    Progress(GetText(FormName, "export_notizen"));
                    CopyNotizen(ID_Chirurgen);

                    Progress(GetText(FormName, "export_planOperationen"));
                    CopyPlanOperationen(ID_Chirurgen);

                    Progress(GetText(FormName, "export_richtlinienOpscodes"));
                    CopyRichtlinienOPSKodes();

                    Progress(GetText(FormName, "export_kommentare"));
                    CopyKommentare(ID_Chirurgen);

                    success = true;
                }
                catch (Exception ex)
                {
                    _businessLayer.MessageBox(string.Format(CultureInfo.InvariantCulture, GetText(FormName, "error_export_surgeon"),
                        ex.Message, _businessLayer.CurrentUser_Nachname, _fileName));
                    goto _exit;
                }
                finally
                {
                    if (success)
                    {
                        trans.Commit();
                    }
                    else
                    {
                        trans.Rollback();
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

        //
        // Die Reihenfolge hier ist alphabetisch nach Tabellenname
        //

        private void CopyAkademischeAusbildungTypen()
        {
            DataView view = _businessLayer.GetAkademischeAusbildungTypen();
            foreach (DataRow row in view.Table.Rows)
            {
                ProgressTinyOperation();

                string sql = "insert into AkademischeAusbildungTypen (ID_AkademischeAusbildungTypen, [Text])"
                    + " values(@ID_AkademischeAusbildungTypen, @Text)";

                ArrayList sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_AkademischeAusbildungTypen", row["ID_AkademischeAusbildungTypen"]));
                sqlParameters.Add(SqlParameter("@Text", row["Text"]));

                ExecuteNonQuery(sql, sqlParameters);
            }
        }
        private void CopyAkademischeAusbildungen(int ID_Chirurgen)
        {
            DataView view = _businessLayer.GetAkademischeAusbildungen(ID_Chirurgen);
            foreach (DataRow row in view.Table.Rows)
            {
                ProgressTinyOperation();

                string sql = "insert into AkademischeAusbildungen (ID_AkademischeAusbildungen, ID_AkademischeAusbildungTypen, ID_Chirurgen, Beginn, Ende, Organisation)"
                     + " values (@ID_AkademischeAusbildungen, @ID_AkademischeAusbildungTypen, @ID_Chirurgen, @Beginn, @Ende, @Organisation)";

                ArrayList sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_AkademischeAusbildungen", row["ID_AkademischeAusbildungen"]));
                sqlParameters.Add(SqlParameter("@ID_AkademischeAusbildungTypen", row["ID_AkademischeAusbildungTypen"]));
                sqlParameters.Add(SqlParameter("@ID_Chirurgen", row["ID_Chirurgen"]));
                sqlParameters.Add(SqlParameter("@Beginn", row["Beginn"]));
                sqlParameters.Add(SqlParameter("@Ende", row["Ende"]));
                sqlParameters.Add(SqlParameter("@Organisation", row["Organisation"]));

                ExecuteNonQuery(sql, sqlParameters);
            }
        }

        private void CopyChirurg(int ID_Chirurgen)
        {
            if (!ChirurgExists(ID_Chirurgen))
            {
                Progress();
                DataRow row = _businessLayer.GetChirurg(ID_Chirurgen);

                string sql = "insert into Chirurgen (ID_Chirurgen, Nachname, Vorname, Anfangsdatum, Anrede, UserID, ImportID, [Password], MustChangePassword, ID_ChirurgenFunktionen, Aktiv, IstWeiterbilder)"
                    + " values (@ID_Chirurgen, @Nachname, @Vorname, @Anfangsdatum, @Anrede, @UserID, @ImportID, @Password, 0, @ID_ChirurgenFunktionen, 1, @IstWeiterbilder)";

                ArrayList sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_Chirurgen", row["ID_Chirurgen"]));
                sqlParameters.Add(SqlParameter("@Nachname", row["Nachname"]));
                sqlParameters.Add(SqlParameter("@Vorname", row["Vorname"]));
                sqlParameters.Add(SqlParameter("@Anfangsdatum", row["Anfangsdatum"]));
                sqlParameters.Add(SqlParameter("@Anrede", row["Anrede"]));
                sqlParameters.Add(SqlParameter("@UserID", row["UserID"]));
                sqlParameters.Add(SqlParameter("@ImportID", row["ImportID"]));
                sqlParameters.Add(SqlParameter("@Password", row["Password"]));
                sqlParameters.Add(SqlParameter("@ID_ChirurgenFunktionen", row["ID_ChirurgenFunktionen"]));
                sqlParameters.Add(SqlParameter("@IstWeiterbilder", row["IstWeiterbilder"]));

                ExecuteNonQuery(sql, sqlParameters);
            }
        }
        private void CopyChirurgenDokumente(int ID_Chirurgen)
        {
            DataView view = _businessLayer.GetChirurgenDokumenteTable(ID_Chirurgen);
            foreach (DataRow row in view.Table.Rows)
            {
                ProgressTinyOperation();

                string sql = "insert into ChirurgenDokumente (ID_ChirurgenDokumente, ID_Chirurgen, ID_Dokumente, [Blob], InBearbeitung, Bearbeitungsdatum)"
                     + " values (@ID_ChirurgenDokumente, @ID_Chirurgen, @ID_Dokumente, @Blob, @InBearbeitung, @Bearbeitungsdatum)";

                ArrayList sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_ChirurgenDokumente", row["ID_ChirurgenDokumente"]));
                sqlParameters.Add(SqlParameter("@ID_Chirurgen", row["ID_Chirurgen"]));
                sqlParameters.Add(SqlParameter("@ID_Dokumente", row["ID_Dokumente"]));
                sqlParameters.Add(SqlParameter("@Blob", (byte[])row["Blob"]));
                sqlParameters.Add(SqlParameter("@InBearbeitung", row["InBearbeitung"]));
                sqlParameters.Add(SqlParameter("@Bearbeitungsdatum", row["Bearbeitungsdatum"]));

                ExecuteNonQuery(sql, sqlParameters);
            }
        }
        private void CopyChirurgenFunktionen()
        {
            DataView view = _businessLayer.GetChirurgenFunktionen();
            foreach (DataRow row in view.Table.Rows)
            {
                ProgressTinyOperation();
                string sql = "insert into ChirurgenFunktionen (ID_ChirurgenFunktionen, Funktion)"
                    + " values(@ID_ChirurgenFunktionen, @Funktion)";

                ArrayList sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_ChirurgenFunktionen", row["ID_ChirurgenFunktionen"]));
                sqlParameters.Add(SqlParameter("@Funktion", row["Funktion"]));

                ExecuteNonQuery(sql, sqlParameters);
            }
        }
        private void CopyChirurgenGebiete(int ID_Chirurgen)
        {
            DataView view = _businessLayer.GetChirurgenGebiete(ID_Chirurgen);
            foreach (DataRow row in view.Table.Rows)
            {
                ProgressTinyOperation();
                string sql = "insert into ChirurgenGebiete (ID_ChirurgenGebiete, ID_Chirurgen, ID_Gebiete, GebietVon, GebietBis)"
                    + " values (@ID_ChirurgenGebiete, @ID_Chirurgen, @ID_Gebiete, @GebietVon, @GebietBis)";

                ArrayList sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_ChirurgenGebiete", row["ID_ChirurgenGebiete"]));
                sqlParameters.Add(SqlParameter("@ID_Chirurgen", row["ID_Chirurgen"]));
                sqlParameters.Add(SqlParameter("@ID_Gebiete", row["ID_Gebiete"]));
                sqlParameters.Add(SqlParameter("@GebietVon", row["GebietVon"]));
                sqlParameters.Add(SqlParameter("@GebietBis", row["GebietBis"]));

                ExecuteNonQuery(sql, sqlParameters);
            }
        }
        private void CopyChirurgenOperationen(int ID_Chirurgen)
        {
            DataView view = _businessLayer.GetChirurgenOperationen(ID_Chirurgen);
            foreach (DataRow row in view.Table.Rows)
            {
                ProgressTinyOperation();

                string sql = @"
                    insert into ChirurgenOperationen 
                        (ID_ChirurgenOperationen, 
                        ID_Chirurgen, 
                        ID_Richtlinien, 
                        ID_OPFunktionen, 
                        ID_KlinischeErgebnisseTypen, 
                        Fallzahl, 
                        [OPS-Kode], 
                        [OPS-Text], 
                        Datum, 
                        Zeit, 
                        ZeitBis,
                        Quelle,
                        KlinischeErgebnisse
                        )
                    values(
                        @ID_ChirurgenOperationen, 
                        @ID_Chirurgen, 
                        @ID_Richtlinien, 
                        @ID_OPFunktionen, 
                        @ID_KlinischeErgebnisseTypen, 
                        @Fallzahl, 
                        @OPSKode, 
                        @OPSText, 
                        @Datum, 
                        @Zeit, 
                        @ZeitBis,
                        @Quelle,
                        @KlinischeErgebnisse
                        )";

                ArrayList sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_ChirurgenOperationen", row["ID_ChirurgenOperationen"]));
                sqlParameters.Add(SqlParameter("@ID_Chirurgen", row["ID_Chirurgen"]));
                sqlParameters.Add(SqlParameter("@ID_Richtlinien)", row["ID_Richtlinien"]));
                sqlParameters.Add(SqlParameter("@ID_OPFunktionen)", row["ID_OPFunktionen"]));
                sqlParameters.Add(SqlParameter("@ID_KlinischeErgebnisseTypen)", row["ID_KlinischeErgebnisseTypen"]));
                sqlParameters.Add(SqlParameter("@Fallzahl", row["Fallzahl"]));
                sqlParameters.Add(SqlParameter("@OPSKode", row["OPS-Kode"]));
                sqlParameters.Add(SqlParameter("@OPSText", row["OPS-Text"]));
                sqlParameters.Add(SqlParameter("@Datum", row["Datum"]));
                sqlParameters.Add(SqlParameter("@Zeit", row["Zeit"]));
                sqlParameters.Add(SqlParameter("@ZeitBis", row["ZeitBis"]));
                sqlParameters.Add(SqlParameter("@Quelle", row["Quelle"]));
                sqlParameters.Add(SqlParameter("@KlinischeErgebnisse", row["KlinischeErgebnisse"]));

                ExecuteNonQuery(sql, sqlParameters);
            }
        }

        private void CopyDateien()
        {
            DataView view = _businessLayer.GetDateien();
            foreach (DataRow row in view.Table.Rows)
            {
                ProgressTinyOperation();

                string sql = "insert into Dateien (ID_Dateien, ID_DateiTypen, Dateiname, Beschreibung)"
                     + " values (@ID_Dateien, @ID_DateiTypen, @Dateiname, @Beschreibung)";

                ArrayList sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_Dateien", row["ID_Dateien"]));
                sqlParameters.Add(SqlParameter("@ID_DateiTypen", row["ID_DateiTypen"]));
                sqlParameters.Add(SqlParameter("@Dateiname", row["Dateiname"]));
                sqlParameters.Add(SqlParameter("@Beschreibung", row["Beschreibung"]));

                ExecuteNonQuery(sql, sqlParameters);
            }
        }
        private void CopyDateiTypen()
        {
            DataView view = _businessLayer.GetDateiTypen();
            foreach (DataRow row in view.Table.Rows)
            {
                ProgressTinyOperation();

                string sql = "insert into DateiTypen (ID_DateiTypen, DateiTyp)"
                     + " values (@ID_DateiTypen, @DateiTyp)";

                ArrayList sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_DateiTypen", row["ID_DateiTypen"]));
                sqlParameters.Add(SqlParameter("@DateiTyp", row["DateiTyp"]));

                ExecuteNonQuery(sql, sqlParameters);
            }
        }
        private void CopyDokumente()
        {
            DataView view = _businessLayer.GetDokumente();
            foreach (DataRow row in view.Table.Rows)
            {
                ProgressTinyOperation();

                string sql = "insert into Dokumente (ID_Dokumente, Gruppe, LfdNummer, Beschreibung, Dateiname)"
                     + " values (@ID_Dokumente, @Gruppe, @LfdNummer, @Beschreibung, @Dateiname)";

                ArrayList sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_Dokumente", row["ID_Dokumente"]));
                sqlParameters.Add(SqlParameter("@Gruppe", row["Gruppe"]));
                sqlParameters.Add(SqlParameter("@LfdNummer", row["LfdNummer"]));
                sqlParameters.Add(SqlParameter("@Beschreibung", row["Beschreibung"]));
                sqlParameters.Add(SqlParameter("@Dateiname", row["Dateiname"]));

                ExecuteNonQuery(sql, sqlParameters);
            }
        }
        private void CopyGebiete()
        {
            DataView view = _businessLayer.GetGebiete();
            foreach (DataRow row in view.Table.Rows)
            {
                ProgressTinyOperation();

                string sql = "insert into Gebiete (ID_Gebiete, Gebiet, Bemerkung, Herkunft)"
                    + "values(@ID_Gebiete, @Gebiet, Bemerkung, Herkunft)";

                ArrayList sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_Gebiete", row["ID_Gebiete"]));
                sqlParameters.Add(SqlParameter("@Gebiet", row["Gebiet"]));
                sqlParameters.Add(SqlParameter("@Bemerkung", row["Bemerkung"]));
                sqlParameters.Add(SqlParameter("@Herkunft", row["Herkunft"]));

                ExecuteNonQuery(sql, sqlParameters);
            }
        }

        private void CopyImportChirurgenExclude()
        {
            DataView view = _businessLayer.GetImportChirurgenExclude();
            foreach (DataRow row in view.Table.Rows)
            {
                ProgressTinyOperation();

                string sql = "insert into ImportChirurgenExclude (ID_ImportChirurgenExclude, Nachname, Vorname)"
                     + " values(@ID_ImportChirurgenExclude, @Nachname, @Vorname)";

                ArrayList sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_ImportChirurgenExclude", row["ID_ImportChirurgenExclude"]));
                sqlParameters.Add(SqlParameter("@Nachname", row["Nachname"]));
                sqlParameters.Add(SqlParameter("@Vorname", row["Vorname"]));

                ExecuteNonQuery(sql, sqlParameters);
            }
        }
        private void CopyKommentare(int ID_Chirurgen)
        {
            DataView view = _businessLayer.GetKommentare();
            foreach (DataRow row in view.Table.Rows)
            {
                ProgressTinyOperation();

                if (ConvertToInt32(row["ID_Chirurgen_Von"]) == ID_Chirurgen || ConvertToInt32(row["ID_Chirurgen_Fuer"]) == ID_Chirurgen)
                {
                    CopyChirurg(ConvertToInt32(row["ID_Chirurgen_Von"]));
                    CopyChirurg(ConvertToInt32(row["ID_Chirurgen_Fuer"]));

                    string sql = "insert into Kommentare (ID_Kommentare, Datum, AbschnittVon, AbschnittBis, ID_Chirurgen_Von, ID_Chirurgen_Fuer, KommentarVon, KommentarFuer, ID_ChirurgenFunktionen, Status)"
                        + "values(@ID_Kommentare, @Datum, @AbschnittVon, @AbschnittBis, @ID_Chirurgen_Von, @ID_Chirurgen_Fuer, @KommentarVon, @KommentarFuer, @ID_ChirurgenFunktionen, @Status)";

                    ArrayList sqlParameters = new ArrayList();
                    sqlParameters.Add(SqlParameter("@ID_Kommentare", row["ID_Kommentare"]));
                    sqlParameters.Add(SqlParameter("@Datum", row["Datum"]));
                    sqlParameters.Add(SqlParameter("@AbschnittVon", row["AbschnittVon"]));
                    sqlParameters.Add(SqlParameter("@AbschnittBis", row["AbschnittBis"]));
                    sqlParameters.Add(SqlParameter("@ID_Chirurgen_Von", row["ID_Chirurgen_Von"]));
                    sqlParameters.Add(SqlParameter("@ID_Chirurgen_Fuer", row["ID_Chirurgen_Fuer"]));
                    sqlParameters.Add(SqlParameter("@KommentarVon", row["KommentarVon"]));
                    sqlParameters.Add(SqlParameter("@KommentarFuer", row["KommentarFuer"]));
                    sqlParameters.Add(SqlParameter("@ID_ChirurgenFunktionen", row["ID_ChirurgenFunktionen"]));
                    sqlParameters.Add(SqlParameter("@Status", row["Status"]));

                    ExecuteNonQuery(sql, sqlParameters);
                }
            }
        }
        private void CopyNotizen(int ID_Chirurgen)
        {
            DataView view = _businessLayer.GetNotizen(ID_Chirurgen);
            foreach (DataRow row in view.Table.Rows)
            {
                ProgressTinyOperation();

                string sql = "insert into Notizen (ID_Notizen, ID_Chirurgen, ID_NotizTypen, Datum, Ende, Notiz)"
                    + " values(@ID_Notizen, @ID_Chirurgen, @ID_NotizTypen, @Datum, @Ende, @Notiz)";

                ArrayList sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_Notizen", row["ID_Notizen"]));
                sqlParameters.Add(SqlParameter("@ID_Chirurgen", row["ID_Chirurgen"]));
                sqlParameters.Add(SqlParameter("@ID_NotizTypen", row["ID_NotizTypen"]));
                sqlParameters.Add(SqlParameter("@Datum", row["Datum"]));
                sqlParameters.Add(SqlParameter("@Ende", row["Ende"]));
                sqlParameters.Add(SqlParameter("@Notiz", row["Notiz"]));

                ExecuteNonQuery(sql, sqlParameters);
            }
        }
        private void CopyNotizTypen()
        {
            DataView view = _businessLayer.GetNotizTypen(false);
            foreach (DataRow row in view.Table.Rows)
            {
                ProgressTinyOperation();

                string sql = "insert into NotizTypen (ID_NotizTypen, [Text])"
                     + " values (@ID_NotizTypen, @Text)";

                ArrayList sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_NotizTypen", row["ID_NotizTypen"]));
                sqlParameters.Add(SqlParameter("@Text", row["Text"]));

                ExecuteNonQuery(sql, sqlParameters);
            }
        }

        /// <summary>
        /// Kopiert alle Operationen
        /// </summary>
        private void CopyOperationen()
        {
            DataView view = _businessLayer.GetOperationen();
            foreach (DataRow row in view.Table.Rows)
            {
                ProgressTinyOperation();

                string sql = "insert into Operationen (ID_Operationen, [OPS-Kode], [OPS-Text])"
                    + "values (@ID_Operationen, @OPSKode, @OPSText)";

                ArrayList sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_Operationen", row["ID_Operationen"]));
                sqlParameters.Add(SqlParameter("@OPSKode", row["OPS-Kode"]));
                sqlParameters.Add(SqlParameter("@OPSText", row["OPS-Text"]));

                ExecuteNonQuery(sql, sqlParameters);
            }
        }
        private void CopyOPFunktionen()
        {
            DataView view = _businessLayer.GetOPFunktionen();
            foreach (DataRow row in view.Table.Rows)
            {
                ProgressTinyOperation();

                string sql = "insert into OPFunktionen (ID_OPFunktionen, LfdNr, Beschreibung)"
                     + " values (@ID_OPFunktionen, @LfdNr, @Beschreibung)";

                ArrayList sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_OPFunktionen", row["ID_OPFunktionen"]));
                sqlParameters.Add(SqlParameter("@LfdNr", row["LfdNr"]));
                sqlParameters.Add(SqlParameter("@Beschreibung", row["Beschreibung"]));

                ExecuteNonQuery(sql, sqlParameters);
            }
        }
        private void CopyPlanOperationen(int ID_Chirurgen)
        {
            DataView view = _businessLayer.GetPlanOperationen(ID_Chirurgen);
            foreach (DataRow row in view.Table.Rows)
            {
                ProgressTinyOperation();

                string sql = "insert into PlanOperationen (ID_PlanOperationen, ID_Chirurgen, Operation, Anzahl, DatumVon, DatumBis)"
                    + " values (@ID_PlanOperationen, @ID_Chirurgen, @Operation, @Anzahl, @DatumVon, @DatumBis)";

                ArrayList sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_PlanOperationen", row["ID_PlanOperationen"]));
                sqlParameters.Add(SqlParameter("@ID_Chirurgen", row["ID_Chirurgen"]));
                sqlParameters.Add(SqlParameter("@Operation", row["Operation"]));
                sqlParameters.Add(SqlParameter("@Anzahl", row["Anzahl"]));
                sqlParameters.Add(SqlParameter("@DatumVon", row["DatumVon"]));
                sqlParameters.Add(SqlParameter("@DatumBis", row["DatumBis"]));

                ExecuteNonQuery(sql, sqlParameters);
            }
        }
        private void CopyRichtlinien()
        {
            DataView view = _businessLayer.GetRichtlinien();
            foreach (DataRow row in view.Table.Rows)
            {
                ProgressTinyOperation();

                string sql = "insert into Richtlinien (ID_Richtlinien, ID_Gebiete, LfdNummer, UntBehMethode, Richtzahl)"
                    + " values (@ID_Richtlinien, @ID_Gebiete, @LfdNummer, @UntBehMethode, @Richtzahl)";

                ArrayList sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_Richtlinien", row["ID_Richtlinien"]));
                sqlParameters.Add(SqlParameter("@ID_Gebiete", row["ID_Gebiete"]));
                sqlParameters.Add(SqlParameter("@LfdNummer", row["LfdNummer"]));
                sqlParameters.Add(SqlParameter("@UntBehMethode", row["UntBehMethode"]));
                sqlParameters.Add(SqlParameter("@Richtzahl", row["Richtzahl"]));

                ExecuteNonQuery(sql, sqlParameters);
            }
        }

        private void CopyKlinischeErgebnisseTypen()
        {
            DataView view = _businessLayer.GetKlinischeErgebnisseTypen();
            foreach (DataRow row in view.Table.Rows)
            {
                ProgressTinyOperation();

                string sql = "insert into KlinischeErgebnisseTypen (ID_KlinischeErgebnisseTypen, [ID], [Text])"
                    + " values (@ID_KlinischeErgebnisseTypen, @ID, @Text)";

                ArrayList sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_KlinischeErgebnisseTypen", row["ID_KlinischeErgebnisseTypen"]));
                sqlParameters.Add(SqlParameter("@ID", row["ID"]));
                sqlParameters.Add(SqlParameter("@Text", row["Text"]));

                ExecuteNonQuery(sql, sqlParameters);
            }
        }

        private void CopyRichtlinienOPSKodes()
        {
            DataView view = _businessLayer.GetRichtlinienOpsKodes();
            foreach (DataRow row in view.Table.Rows)
            {
                ProgressTinyOperation();

                string sql = "insert into RichtlinienOPSKodes (ID_RichtlinienOPSKodes, ID_Richtlinien, [OPS-Kode])"
                    + " values (@ID_RichtlinienOPSKodes, @ID_Richtlinien, @OPSKode)";

                ArrayList sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@ID_RichtlinienOPSKodes", row["ID_RichtlinienOPSKodes"]));
                sqlParameters.Add(SqlParameter("@ID_Richtlinien", row["ID_Richtlinien"]));
                sqlParameters.Add(SqlParameter("@OPSKode", row["OPS-Kode"]));

                ExecuteNonQuery(sql, sqlParameters);
            }
        }


        private void ProgressTinyOperation()
        {
            progressCount++;
            if (progressCount >= ProgressThreshold)
            {
                progressCount = 0;
                Progress();
            }
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

        public virtual IDbDataParameter SqlParameter(string sParameterName, int iValue)
        {
            // OleDB-Parameters beziehen sich auf die Position, nicht auf den Namen!!!
            // Reihenfolge ist wichtig!!!
            IDbDataParameter oSQLParameter = _dataFactory.CreateParameter();
            oSQLParameter.ParameterName = sParameterName;
            oSQLParameter.DbType = DbType.Int32;
            oSQLParameter.Value = iValue;
            return oSQLParameter;
        }
        public virtual IDbDataParameter SqlParameter(string sParameterName, string sValue)
        {
            // OleDB-Parameters beziehen sich auf die Position, nicht auf den Namen!!!
            // Reihenfolge ist wichtig!!!
            IDbDataParameter oSQLParameter = _dataFactory.CreateParameter();
            oSQLParameter.ParameterName = sParameterName;
            oSQLParameter.DbType = DbType.String;
            oSQLParameter.Value = sValue;
            return oSQLParameter;
        }

        public virtual IDbDataParameter SqlParameter(string sParameterName, object value)
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

        protected virtual IDbDataParameter SqlParameter(string sParameterName, byte[] arValue)
        {
            // OleDB-Parameters beziehen sich auf die Position, nicht auf den Namen!!!
            // Reihenfolge ist wichtig!!!
            IDbDataParameter oSQLParameter = _dataFactory.CreateParameter();
            oSQLParameter.ParameterName = sParameterName;
            oSQLParameter.DbType = DbType.Binary;
            oSQLParameter.Value = arValue;
            return oSQLParameter;
        }

        protected virtual IDbDataParameter SqlParameterInt(string sParameterName, object o)
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
