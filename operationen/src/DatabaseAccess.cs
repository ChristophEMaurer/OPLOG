using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Collections;
using System.Text;

using AppFramework;
using CMaurer.Operationen.AppFramework;

namespace Operationen
{
    public class DatabaseAccess : DatabaseLayer
    {
        /// <summary>
        /// <br/>count(...) liefert long
        /// </summary>
        /// <param name="bizLayer"></param>
        /// <param name="strConnectionString"></param>
        public DatabaseAccess(BusinessLayer bizLayer, DatabaseType databaseType, string strConnectionString)
            : base(bizLayer, databaseType, strConnectionString)
        {
        }

        /* Diese nicht löschen. Es ist super mühsam, herauszufinden, wie das in Access geht!
        protected internal bool UpdateV1_6ToV1_7(ref string strError)
        {
            bool bSuccess = false;

            using (DbConnection conn = DataFactory.CreateConnection())
            {
                conn.ConnectionString = _strConnection;
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    DbCommand command = conn.CreateCommand();
                    command.Transaction = trans;

                    command.CommandText = "alter table Gebiete drop Name";
                    command.ExecuteNonQuery();

                    // NotizTypen
                    command.CommandText = "create table NotizTypen "
                            + "(ID_NotizTypen counter primary key,"
                            + "[Text] Text(100) not null)";
                    command.ExecuteNonQuery();

                    command.CommandText = "create table Notizen "
                            + "(ID_Notizen counter primary key,"
                            + "ID_Chirurgen long not null,"
                            + "ID_NotizTypen long not null,"
                            + "Datum DateTime not null,"
                            + "Notiz Text(255) not null,"
                            + "CONSTRAINT FKNotizenNotizTypen FOREIGN KEY (ID_NotizTypen) REFERENCES NotizTypen(ID_NotizTypen),"
                            + "CONSTRAINT FKNotizenChirurgen FOREIGN KEY (ID_Chirurgen) REFERENCES Chirurgen(ID_Chirurgen)"
                            + ")";
                    command.ExecuteNonQuery();

                    // Dateitypen
                    command.CommandText = "create table DateiTypen "
                        + "(ID_DateiTypen counter primary key,"
                        + "DateiTyp Text(100) not null)";
                    command.ExecuteNonQuery();

                    // Dateien
                    command.CommandText = "create table Dateien "
                        + "(ID_Dateien counter primary key,"
                        + "ID_DateiTypen long not null,"
                        + "Dateiname Text(100) not null,"
                        + "Beschreibung Text(100) not null,"
                        + "CONSTRAINT FKDateienDateiTypen FOREIGN KEY (ID_DateiTypen) REFERENCES DateiTypen(ID_DateiTypen)"
                        + ")";
                    command.ExecuteNonQuery();

                    command.CommandText = "UPDATE Config SET [Value] = '7' where [Key] = 'MinorVersion'";
                    command.ExecuteNonQuery();

                    trans.Commit();
                    bSuccess = true;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    strError = e.Message;
                    bSuccess = false;
                }
            }

            return bSuccess;
        }
        protected internal bool UpdateV1_7ToV1_8(ref string strError)
        {
            return false;

            //Interop.ADODB
            //Microsoft ActiveX Data Objects 2.8 Library
            //ActiveX
            //{2A75196C-D9EB-4129-B803-931327F72D5C}\2.8\0\tlbimp
            //D:\Daten\Develop\DOT.NET\Operationen\src\obj\Release\Interop.ADODB.dll
            //2.8.0.0

            //Interop.ADOX
            //Microsoft ADO Ext. 2.8 for DDL and Security
            //ActiveX
            //{00000600-0000-0010-8000-00AA006D2EA4}\2.8\0\tlbimp
            //D:\Daten\Develop\DOT.NET\Operationen\src\obj\Release\Interop.ADOX.dll
            //2.8.0.0

            // Benötigt reference ADOX: 
            //         Microsoft ADO Ext. 2.8 for DDL and Security
            //        D:\Daten\Develop\DOT.NET\Operationen\obj\Debug\Interop.ADOX.dll
            // Auf dem Notebook gibt es nur  Microsoft ADO Ext. 6.0 for DDL and Security
            bool success = false;

            ADOX.Catalog catalog = null;
            ADODB.Connection connection = null;

            try
            {
                catalog = new ADOX.Catalog();
                connection = new ADODB.Connection();
                connection.ConnectionString = _strConnection;

                connection.Open(null, null, null, 0);

                catalog.ActiveConnection = connection;

                ADOX.Table table = catalog.Tables["ChirurgenGebiete"];

                //
                // Spalte umbenennen, rename a column
                //
                foreach (ADOX.Column column in table.Columns)
                {
                    if (column.Name == "ID_ChirurgenRichtlinien")
                    {
                        column.Name = "ID_ChirurgenGebiete";
                        break;
                    }
                }

                object recordsAffected;
                connection.Execute("alter table Operationen drop [EBM-Nr]", out recordsAffected, 0);
                connection.Execute("alter table Operationen drop [EBM-Leistung]", out recordsAffected, 0);
                connection.Execute("alter table Operationen drop Kategorie", out recordsAffected, 0);
                connection.Execute("UPDATE Config SET [Value] = '8' where [Key] = 'MinorVersion'", out recordsAffected, 0);

                success = true;
            }
            catch (Exception e)
            {
                strError = e.Message;
            }
            finally
            {
                if (connection != null)
                {
                    try
                    {
                        connection.Close();
                    }
                    catch
                    {
                    }
                    connection = null;
                }
                catalog = null;
            }

            return success;
        }
         */
        protected override internal bool UpdateV1_8ToV1_9(ref string strError)
        {
            bool bSuccess = false;

            using (DbConnection conn = DataFactory.CreateConnection())
            {
                conn.ConnectionString = ConnectionString;
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    DbCommand command = conn.CreateCommand();
                    command.Transaction = trans;

                    // AkademischeAusbildungTypen
                    command.CommandText = "create table AkademischeAusbildungTypen "
                            + "(ID_AkademischeAusbildungTypen counter primary key,"
                            + "[Text] Text(100) not null)";
                    command.ExecuteNonQuery();

                    command.CommandText = "insert into AkademischeAusbildungTypen ([Text]) values ('Studium')";
                    command.ExecuteNonQuery();
                    command.CommandText = "insert into AkademischeAusbildungTypen ([Text]) values ('3. Staatsexamen')";
                    command.ExecuteNonQuery();
                    command.CommandText = "insert into AkademischeAusbildungTypen ([Text]) values ('Approbation')";
                    command.ExecuteNonQuery();
                    command.CommandText = "insert into AkademischeAusbildungTypen ([Text]) values ('Promotion')";
                    command.ExecuteNonQuery();
                    command.CommandText = "insert into AkademischeAusbildungTypen ([Text]) values ('Habilitation')";
                    command.ExecuteNonQuery();
                    command.CommandText = "insert into AkademischeAusbildungTypen ([Text]) values ('Abschluss Basischirurgie')";
                    command.ExecuteNonQuery();
                    command.CommandText = "insert into AkademischeAusbildungTypen ([Text]) values ('Prüfung Basischirurgie')";
                    command.ExecuteNonQuery();
                    command.CommandText = "insert into AkademischeAusbildungTypen ([Text]) values ('Facharztprüfung')";
                    command.ExecuteNonQuery();

                    // AkademischeAusbildungen
                    command.CommandText = "create table AkademischeAusbildungen "
                            + "(ID_AkademischeAusbildungen counter primary key,"
                            + "ID_AkademischeAusbildungTypen long not null,"
                            + "ID_Chirurgen long not null,"
                            + "Beginn DateTime not null,"
                            + "Ende DateTime,"
                            + "Organisation Text(255) not null,"
                            + "CONSTRAINT FKAkadAusbAkadAusbTypen FOREIGN KEY (ID_AkademischeAusbildungTypen) REFERENCES AkademischeAusbildungTypen(ID_AkademischeAusbildungTypen),"
                            + "CONSTRAINT FKAkadAusbChirurgen FOREIGN KEY (ID_Chirurgen) REFERENCES Chirurgen(ID_Chirurgen)"
                            + ")";
                    command.ExecuteNonQuery();

                    command.CommandText = "UPDATE Config SET [Value] = '9' where [Key] = 'MinorVersion'";
                    command.ExecuteNonQuery();

                    trans.Commit();
                    bSuccess = true;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    strError = e.Message;
                    bSuccess = false;
                }
            }

            return bSuccess;
        }

        protected override internal bool UpdateV1_9ToV1_10(ref string strError)
        {
            // 30.07.2007
            // Tabelle Titel und ChirurgenFunktionen sind identisch!
            // Tabelle Titel entfällt, und wird durch ChirurgenFunktionen ersetzt
            // Neues BOOL Flag bei Chirurgen: "Aktiv"
            bool bSuccess = false;

            string sql = "select @@TOP@@ ID_ChirurgenFunktionen from ChirurgenFunktionen @@LIMIT@@";
            sql = HandleTopLimitStuff(sql, "1");

            DataRow row = this.GetRecord(sql, null, "ChirurgenFunktionen");
            int ID_ChirurgenFunktionen = ConvertToInt32(row["ID_ChirurgenFunktionen"]);
            using (DbConnection conn = DataFactory.CreateConnection())
            {
                conn.ConnectionString = ConnectionString;
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    DbCommand command = conn.CreateCommand();
                    command.Transaction = trans;

                    // Tabelle Titel löschen und Chirurgen.ID_Titel ersetzen durch Chirurgen.ID_ChirurgenFunktionen
                    // Beziehung löschen, mit Extras - Analyse - Dokumentierer findet man die Namen heraus!
                    command.CommandText = "alter table Chirurgen drop CONSTRAINT TitelChirurgen";
                    command.ExecuteNonQuery();

                    command.CommandText = "drop index ID_Titel on Chirurgen";
                    command.ExecuteNonQuery();

                    command.CommandText = "alter table Chirurgen drop ID_Titel";
                    command.ExecuteNonQuery();

                    command.CommandText = "drop table Titel";
                    command.ExecuteNonQuery();

                    command.CommandText = "alter table Chirurgen ADD COLUMN ID_ChirurgenFunktionen LONG NOT NULL";
                    command.ExecuteNonQuery();

                    command.CommandText = "alter table Chirurgen "
                            + "ADD CONSTRAINT FKChirurgenChirurgenFunktionen FOREIGN KEY (ID_ChirurgenFunktionen) REFERENCES ChirurgenFunktionen(ID_ChirurgenFunktionen)";
                    command.ExecuteNonQuery();


                    command.CommandText = "UPDATE Chirurgen SET ID_ChirurgenFunktionen = " + ID_ChirurgenFunktionen;
                    command.ExecuteNonQuery();

                    // Neu Chirurgen.Aktiv
                    command.CommandText = "alter table Chirurgen ADD COLUMN Aktiv LONG NOT NULL";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE Chirurgen SET Aktiv = 1";
                    command.ExecuteNonQuery();

                    // Neu ChirurgenOperationen.ID_Richtlinien
                    command.CommandText = "alter table ChirurgenOperationen ADD COLUMN ID_Richtlinien LONG";
                    command.ExecuteNonQuery();
                    command.CommandText = "alter table ChirurgenOperationen "
                            + "ADD CONSTRAINT FKChirurgenOperationenRichtlinien FOREIGN KEY (ID_Richtlinien) REFERENCES Richtlinien(ID_Richtlinien)";
                    command.ExecuteNonQuery();

                    // Neu: ChirurgenOperationen.ZeitBis
                    command.CommandText = "alter table ChirurgenOperationen ADD COLUMN ZeitBis DateTime not null";
                    command.ExecuteNonQuery();
                    command.CommandText = "update ChirurgenOperationen set ZeitBis = Zeit";
                    command.ExecuteNonQuery();

                    // Neu Notizen.Ende
                    command.CommandText = "alter table Notizen ADD COLUMN Ende DateTime";
                    command.ExecuteNonQuery();

                    // Neu: ImportChirurgenExclude.Vorname
                    command.CommandText = "alter table ImportChirurgenExclude ADD COLUMN Vorname Text(50)";
                    command.ExecuteNonQuery();

                    command.CommandText = "UPDATE Config SET [Value] = '10' where [Key] = 'MinorVersion'";
                    command.ExecuteNonQuery();

                    trans.Commit();
                    bSuccess = true;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    strError = e.Message;
                    bSuccess = false;
                }
            }

            return bSuccess;
        }
        protected override internal bool UpdateV1_10ToV1_11(ref string strError)
        {
            // 15.08.2007
            // Neu: ChirurgenOperationen.Fallzahl
            bool bSuccess = false;

            using (DbConnection conn = DataFactory.CreateConnection())
            {
                conn.ConnectionString = ConnectionString;
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    DbCommand command = conn.CreateCommand();
                    command.Transaction = trans;

                    command.CommandText = "alter table ChirurgenOperationen ADD COLUMN Fallzahl Text(50) not null";
                    command.ExecuteNonQuery();
                    command.CommandText = "update ChirurgenOperationen set Fallzahl = ''";
                    command.ExecuteNonQuery();

                    command.CommandText = "UPDATE Config SET [Value] = '11' where [Key] = 'MinorVersion'";
                    command.ExecuteNonQuery();

                    trans.Commit();
                    bSuccess = true;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    strError = e.Message;
                    bSuccess = false;
                }
            }

            return bSuccess;
        }
        protected override internal bool UpdateV1_11ToV1_12(ref string strError)
        {
            bool bSuccess = false;

            using (DbConnection conn = DataFactory.CreateConnection())
            {
                conn.ConnectionString = ConnectionString;
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    DbCommand command = conn.CreateCommand();
                    command.Transaction = trans;

                    // Mit dieser Reihenfolge geht es. Keien Ahnung, warum es oben mit ID_Titel in einer
                    // anderen Reihenfolge ging.

                    command.CommandText = "drop index ID_Operationen on ChirurgenOperationen";
                    command.ExecuteNonQuery();

                    // Beziehung OPerationen.ID_Operationen=ChirurgenOperationen.ID_Operationen löschen:
                    // mit Extras - Analyse - Dokumentierer findet man den Namen heraus!
                    command.CommandText = "alter table ChirurgenOperationen DROP CONSTRAINT OperationenChirurgenOperationen";
                    command.ExecuteNonQuery();

                    // Neu ChirurgenOperationen.OPS-Kode
                    // Neu ChirurgenOperationen.OPS-Text
                    // Löschen: ChirurgenOperationen.ID_Operationen
                    command.CommandText = "alter table ChirurgenOperationen ADD COLUMN [OPS-Kode] Text(20) not null";
                    command.ExecuteNonQuery();

                    command.CommandText = "alter table ChirurgenOperationen ADD COLUMN [OPS-Text] Text(255) not null";
                    command.ExecuteNonQuery();

                    // ChirurgenOperationen.[OPS-Kode] und ChirurgenOperationen.[OPS-Text] kopieren aus Operationen.[OPS-Kode], Operationen.[OPS-Text]
                    command.CommandText = @"update chirurgenoperationen a inner join operationen b on 
                                a.id_operationen = b.id_operationen 
                                set a.[ops-kode]=b.[ops-kode], a.[ops-text]=b.[ops-text]";
                    command.ExecuteNonQuery();

                    command.CommandText = "alter table ChirurgenOperationen DROP COLUMN ID_Operationen";
                    command.ExecuteNonQuery();

                    // Neu Chirurgen.Lizenzdaten
                    command.CommandText = "alter table Chirurgen ADD COLUMN Lizenzdaten Text(50) not null";
                    command.ExecuteNonQuery();

                    command.CommandText = "update Chirurgen set Lizenzdaten = 'MigriertVonVersion1Dot11'";
                    command.ExecuteNonQuery();

                    command.CommandText = "UPDATE Config SET [Value] = '12' where [Key] = 'MinorVersion'";
                    command.ExecuteNonQuery();

                    trans.Commit();
                    bSuccess = true;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    strError = e.Message;
                    bSuccess = false;
                }
            }

            return bSuccess;
        }

        protected override internal bool UpdateV1_12ToV1_13(ref string strError)
        {
            bool bSuccess = false;

            using (OleDbConnection conn = new OleDbConnection())
            {
                conn.ConnectionString = ConnectionString;
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    DbCommand command = conn.CreateCommand();
                    command.Transaction = trans;

                    DataTable schema;
                    string constraintName;

                    //
                    // ChirurgenOperationen.ID_Chirurgen war null mit default 0!!!
                    // ändern in not null
                    // 
                    // Relationship muss erst gelöscht werden...
                    //                                                                                         PK_TABLE_NAME            FK_TABLE_NAME 
                    schema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys, new object[] { null, null, "Chirurgen", null, null, "ChirurgenOperationen" });
                    if (schema.Rows.Count != 1)
                    {
                        // Hier muss es eine Relation geben und nur genau eine
                        throw new Exception("Could not find relationship name for Chirurgen.ID_Chirurgen = ChirurgenOperationen.ID_Chirurgen");
                    }
                    constraintName = (string)schema.Rows[0]["FK_NAME"];
                    command.CommandText = string.Format("alter table ChirurgenOperationen drop constraint {0}", constraintName);
                    command.ExecuteNonQuery();
                    // ...Feld ändern...
                    command.CommandText = "alter table ChirurgenOperationen alter column ID_Chirurgen LONG NOT NULL DEFAULT NULL";
                    command.ExecuteNonQuery();
                    // ...Relation wieder hinzufügen
                    command.CommandText = @"alter table ChirurgenOperationen ADD CONSTRAINT
                                ChirurgenChirurgenOperationen FOREIGN KEY (ID_Chirurgen) REFERENCES Chirurgen(ID_Chirurgen)";
                    command.ExecuteNonQuery();

                    //
                    // RichtlinienOPSKodes.ID_Richtlinien war null mit default 0!!!
                    // ändern in not null
                    // 
                    // Relationship muss erst gelöscht werden...
                    schema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys, new object[] { null, null, "Richtlinien", null, null, "RichtlinienOPSKodes" });
                    if (schema.Rows.Count != 1)
                    {
                        // Hier muss es eine Relation geben und nur genau eine
                        throw new Exception("Could not find relationship name for Richtlinien.ID_Richtlinien = RichtlinienOPSKodes.ID_Richtlinien");
                    }
                    constraintName = (string)schema.Rows[0]["FK_NAME"];
                    command.CommandText = string.Format("alter table RichtlinienOPSKodes drop constraint {0}", constraintName);
                    command.ExecuteNonQuery();
                    // ...Feld ändern...
                    command.CommandText = "alter table RichtlinienOPSKodes alter column ID_Richtlinien LONG NOT NULL DEFAULT NULL";
                    command.ExecuteNonQuery();
                    // ...Relation wieder hinzufügen
                    command.CommandText = @"alter table RichtlinienOPSKodes ADD CONSTRAINT 
                                RichtlinienRichtlinienOPSKodes FOREIGN KEY (ID_Richtlinien) REFERENCES Richtlinien(ID_Richtlinien)";
                    command.ExecuteNonQuery();


                    //
                    // PlanOperationen.ID_Chirurgen war null mit default 0!!!
                    // ändern in not null
                    // 
                    // Relationship muss erst gelöscht werden...
                    schema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys, new object[] { null, null, "Chirurgen", null, null, "PlanOperationen" });
                    if (schema.Rows.Count != 1)
                    {
                        // Hier muss es eine Relation geben und nur genau eine
                        throw new Exception("Could not find relationship name for Chirurgen.ID_Chirurgen = PlanOperationen.ID_Chirurgen");
                    }
                    constraintName = (string)schema.Rows[0]["FK_NAME"];
                    command.CommandText = string.Format("alter table PlanOperationen drop constraint {0}", constraintName);
                    command.ExecuteNonQuery();
                    // ...Feld ändern...
                    command.CommandText = "alter table PlanOperationen alter column ID_Chirurgen LONG NOT NULL DEFAULT NULL";
                    command.ExecuteNonQuery();
                    // ...Relation wieder hinzufügen
                    command.CommandText = @"alter table PlanOperationen ADD CONSTRAINT
                                ChirurgenPlanOperationen FOREIGN KEY (ID_Chirurgen) REFERENCES Chirurgen(ID_Chirurgen)";
                    command.ExecuteNonQuery();
                    
                    command.CommandText = @"alter table PlanOperationen ALTER COLUMN Operation Text(50) NOT NULL";
                    command.ExecuteNonQuery();
                    // Anzahl hatte Default 0
                    command.CommandText = @"alter table PlanOperationen ALTER COLUMN Anzahl Long NOT NULL DEFAULT Null";
                    command.ExecuteNonQuery();
                    command.CommandText = @"alter table PlanOperationen ALTER COLUMN DatumVon DateTime NOT NULL";
                    command.ExecuteNonQuery();
                    command.CommandText = @"alter table PlanOperationen ALTER COLUMN DatumBis DateTime NOT NULL";
                    command.ExecuteNonQuery();

                    command.CommandText = "UPDATE Config SET [Value] = '13' where [Key] = 'MinorVersion'";
                    command.ExecuteNonQuery();

                    trans.Commit();
                    bSuccess = true;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    strError = e.Message;
                    bSuccess = false;
                }
            }

            return bSuccess;
        }

        protected override internal bool UpdateV1_13ToV1_14(ref string strError)
        {
            bool bSuccess = false;

            using (DbConnection conn = DataFactory.CreateConnection())
            {
                conn.ConnectionString = ConnectionString;
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    DbCommand command = conn.CreateCommand();
                    command.Transaction = trans;

                    // SerialNumbers
                    command.CommandText = "create table SerialNumbers ("
                            + "ID_SerialNumbers counter primary key,"
                            + "SerialNumber Text(50) not null,"
                            + "constraint IDX_SerialNumber unique(SerialNumber))";
                    command.ExecuteNonQuery();

                    command.CommandText = "UPDATE Config SET [Value] = '14' where [Key] = 'MinorVersion'";
                    command.ExecuteNonQuery();

                    trans.Commit();
                    bSuccess = true;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    strError = e.Message;
                    bSuccess = false;
                }
            }

            return bSuccess;
        }

        protected override internal bool UpdateV1_14ToV1_15(ref string strError)
        {
            bool bSuccess = false;

            using (DbConnection conn = DataFactory.CreateConnection())
            {
                conn.ConnectionString = ConnectionString;
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    DbCommand command = conn.CreateCommand();
                    command.Transaction = trans;

                    // OPFunktionen: "Operation" -> "Operateur"
                    command.CommandText = "UPDATE OPFunktionen SET Beschreibung = 'Operateur'"
                        + " where ID_OPFunktionen = " + ((int)OP_FUNCTION.OP_FUNCTION_OP).ToString();
                    command.ExecuteNonQuery();

                    // Status: 0 - Bearbeiten, 1 - Fertig
                    command.CommandText = "alter table Kommentare ADD COLUMN Status LONG NOT NULL";
                    command.ExecuteNonQuery();
                    command.CommandText = "update Kommentare set Status = 1";
                    command.ExecuteNonQuery();

                    command.CommandText = "UPDATE Config SET [Value] = '15' where [Key] = 'MinorVersion'";
                    command.ExecuteNonQuery();

                    trans.Commit();
                    bSuccess = true;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    strError = e.Message;
                    bSuccess = false;
                }
            }

            return bSuccess;
        }
        protected override internal bool UpdateV1_15ToV1_16(ref string strError)
        {
            bool bSuccess = false;

            using (DbConnection conn = DataFactory.CreateConnection())
            {
                conn.ConnectionString = ConnectionString;
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    DbCommand command = conn.CreateCommand();
                    command.Transaction = trans;

                    // ChirurgenOperationen.Quelle: 0 - intern, 1 - extern
                    command.CommandText = "alter table ChirurgenOperationen ADD COLUMN Quelle LONG NOT NULL";
                    command.ExecuteNonQuery();
                    command.CommandText = "update ChirurgenOperationen set Quelle = 0";
                    command.ExecuteNonQuery();

                    // Neue Tabelle ChirurgenRichtlinien
                    command.CommandText = "create table ChirurgenRichtlinien"
                            + "(ID_ChirurgenRichtlinien counter primary key,"
                            + "ID_Chirurgen long not null,"
                            + "ID_Richtlinien long not null,"
                            + "Datum DateTime not null,"
                            + "Ort Text(255) not null,"
                            + "Anzahl long not null,"
                            + "CONSTRAINT FKChirurgenRichtlinienChirurgen FOREIGN KEY (ID_Chirurgen) REFERENCES Chirurgen(ID_Chirurgen),"
                            + "CONSTRAINT FKChirurgenRichtlinienRichtlinien FOREIGN KEY (ID_Richtlinien) REFERENCES Richtlinien(ID_Richtlinien)"
                            + ")";
                    command.ExecuteNonQuery();

                    command.CommandText = "UPDATE Config SET [Value] = '16' where [Key] = 'MinorVersion'";
                    command.ExecuteNonQuery();

                    trans.Commit();
                    bSuccess = true;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    strError = e.Message;
                    bSuccess = false;
                }
            }

            return bSuccess;
        }
        protected override internal bool UpdateV1_16ToV1_17(ref string strError)
        {
            bool bSuccess = false;

            using (DbConnection conn = DataFactory.CreateConnection())
            {
                conn.ConnectionString = ConnectionString;
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    DbCommand command = conn.CreateCommand();
                    command.Transaction = trans;

                    // Neue Tabelle KlinischeErgebnisseTypen
                    command.CommandText = "create table KlinischeErgebnisseTypen "
                            + "(ID_KlinischeErgebnisseTypen counter primary key,"
                            + "[ID] Text(10) not null,"
                            + "[Text] Text(100) not null)";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT into KlinischeErgebnisseTypen([ID], [Text]) values ('01', 'unauffälliger Verlauf')";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT into KlinischeErgebnisseTypen([ID], [Text]) values ('02', 'Komplikationen')";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT into KlinischeErgebnisseTypen([ID], [Text]) values ('03', 'verstorben')";
                    command.ExecuteNonQuery();

                    // Änderung Tabelle ChirurgenOperationen 
                    command.CommandText = "select ID_KlinischeErgebnisseTypen from KlinischeErgebnisseTypen where [ID] = '01'";
                    int ID_KlinischeErgebnisseTypenUnauffaellig = ConvertToInt32(command.ExecuteScalar());

                    command.CommandText = "alter table ChirurgenOperationen ADD COLUMN ID_KlinischeErgebnisseTypen LONG NOT NULL";
                    command.ExecuteNonQuery();
                    command.CommandText = "alter table ChirurgenOperationen ADD COLUMN KlinischeErgebnisse Text(100) NOT NULL";
                    command.ExecuteNonQuery();
                    command.CommandText = "alter table ChirurgenOperationen "
                            + "ADD CONSTRAINT FKChirurgenOperationenKlinischeErgebnisseTypen FOREIGN KEY (ID_KlinischeErgebnisseTypen) REFERENCES KlinischeErgebnisseTypen(ID_KlinischeErgebnisseTypen)";
                    command.ExecuteNonQuery();
                    command.CommandText = string.Format("update ChirurgenOperationen set ID_KlinischeErgebnisseTypen = {0}", 
                        ID_KlinischeErgebnisseTypenUnauffaellig);
                    command.ExecuteNonQuery();
                    command.CommandText = "update ChirurgenOperationen set KlinischeErgebnisse = ''";
                    command.ExecuteNonQuery();

                    command.CommandText = "UPDATE Config SET [Value] = '17' where [Key] = 'MinorVersion'";
                    command.ExecuteNonQuery();

                    trans.Commit();
                    bSuccess = true;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    strError = e.Message;
                    bSuccess = false;
                }
            }

            return bSuccess;
        }

        protected override internal bool UpdateV1_17ToV1_18(ref string strError)
        {
            bool bSuccess = false;

            using (DbConnection conn = DataFactory.CreateConnection())
            {
                conn.ConnectionString = ConnectionString;
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    DbCommand command = conn.CreateCommand();
                    command.Transaction = trans;

                    /*
                     * In Versino 1.18 selber fehlte der 2. Assistent, wenn man also erst 1.18 installiert und dann ein update macht, 
                     * fehlt er weiterhin
                     * 
                     */
                    // Neue Einträge '2. Assistent' in Tabelle OPFunktionen
                    command.CommandText = "insert into OPFunktionen (ID_OPFunktionen, LfdNr, Beschreibung)"
                        + " values (3, 3, '2. Assistent')";
                    command.ExecuteNonQuery();

                    command.CommandText = "UPDATE Config SET [Value] = '18' where [Key] = 'MinorVersion'";
                    command.ExecuteNonQuery();

                    trans.Commit();
                    bSuccess = true;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    strError = e.Message;
                    bSuccess = false;
                }
            }

            return bSuccess;
        }

        protected override internal bool UpdateV1_18ToV1_19(ref string strError)
        {
            bool bSuccess = false;

            using (DbConnection conn = DataFactory.CreateConnection())
            {
                conn.ConnectionString = ConnectionString;
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    DbCommand command = conn.CreateCommand();
                    command.Transaction = trans;

                    // Inhalt von config.cfg geht jetzt in Tabelle UserSettings
                    // Neue Tabelle UserSettings
                    command.CommandText = "create table UserSettings "
                            + "(ID_UserSettings counter primary key,"
                            + "ID_Chirurgen long not null,"
                            + "[Section] Text(255) not null,"
                            + "[Key] Text(255) not null,"
                            + "[Value] Text(255) not null,"
                            + "CONSTRAINT FKUserSettingsChirurgen FOREIGN KEY (ID_Chirurgen) REFERENCES Chirurgen(ID_Chirurgen)"
                            + ")";
                    command.ExecuteNonQuery();

                    // Länge von OPSKode soll immer gleich sein!
                    // ChirurgenOperationen.OPS-Kode (20)
                    // RichtlinienOpsKodes.OpsKode (50)
                    command.CommandText = "alter table RichtlinienOpsKodes alter column [OPS-Kode] Text(20) not null";
                    command.ExecuteNonQuery();

                    command.CommandText = "alter table PlanOperationen alter column Operation Text(20) not null";
                    command.ExecuteNonQuery();

                    // Index auf OPS-Kode in allen Tabellen
                    command.CommandText = "create index [OPS-Kode] on ChirurgenOperationen([OPS-Kode])";
                    command.ExecuteNonQuery();
                    command.CommandText = "create index [OPS-Kode] on Operationen([OPS-Kode])";
                    command.ExecuteNonQuery();
                    command.CommandText = "create index [OPS-Kode] on RichtlinienOpsKodes([OPS-Kode])";
                    command.ExecuteNonQuery();
                    command.CommandText = "create index ID_Richtlinien on RichtlinienOpsKodes(ID_Richtlinien)";
                    command.ExecuteNonQuery();

                    // Spalte Chirurgen.ID_Groups und Tabelle Groups löschen
                    // Beziehung löschen, mit Extras - Analyse - Dokumentierer findet man die Namen heraus!
                    command.CommandText = "alter table Chirurgen DROP CONSTRAINT GroupsChirurgen";
                    command.ExecuteNonQuery();

                    command.CommandText = "drop index ID_Groups on Chirurgen";
                    command.ExecuteNonQuery();

                    command.CommandText = "alter table Chirurgen DROP ID_Groups";
                    command.ExecuteNonQuery();

                    command.CommandText = "drop table Groups";
                    command.ExecuteNonQuery();

                    // Neue Spalte Chirurgen.IstWeiterbilder
                    command.CommandText = "alter table Chirurgen ADD COLUMN IstWeiterbilder LONG NOT NULL";
                    command.ExecuteNonQuery();
                    command.CommandText = "update Chirurgen set IstWeiterbilder = 0";
                    command.ExecuteNonQuery();

                    // Neue Tabelle Abteilungen
                    command.CommandText = "create table Abteilungen "
                            + "(ID_Abteilungen counter primary key,"
                            + "[Text] Text(100) not null"
                            + ")";
                    command.ExecuteNonQuery();

                    // Neue Tabelle AbteilungenChirurgen
                    command.CommandText = "create table AbteilungenChirurgen "
                            + "(ID_AbteilungenChirurgen counter primary key,"
                            + "ID_Abteilungen long not null,"
                            + "ID_Chirurgen long not null,"
                            + "CONSTRAINT FKAbteilungenChirurgenAbteilungen FOREIGN KEY (ID_Abteilungen) REFERENCES Abteilungen(ID_Abteilungen),"
                            + "CONSTRAINT FKAbteilungenChirurgenChirurgen FOREIGN KEY (ID_Chirurgen) REFERENCES Chirurgen(ID_Chirurgen)"
                            + ")";
                    command.ExecuteNonQuery();

                    // Neue Tabelle WeiterbilderChirurgen
                    command.CommandText = "create table WeiterbilderChirurgen "
                            + "(ID_WeiterbilderChirurgen counter primary key,"
                            + "ID_Weiterbilder long not null,"
                            + "ID_Chirurgen long not null,"
                            + "CONSTRAINT FKWeiterbilderChirurgenWeiterbilder FOREIGN KEY (ID_Weiterbilder) REFERENCES Chirurgen(ID_Chirurgen),"
                            + "CONSTRAINT FKWeiterbilderChirurgenChirurgen FOREIGN KEY (ID_Chirurgen) REFERENCES Chirurgen(ID_Chirurgen)"
                            + ")";
                    command.ExecuteNonQuery();

                    // Neue Tabelle SecGroups
                    command.CommandText = "create table SecGroups "
                            + "(ID_SecGroups counter primary key,"
                            + "[Text] Text(100) not null"
                            + ")";
                    command.ExecuteNonQuery();

                    // Neue Tabelle SecGroupsChirurgen
                    command.CommandText = "create table SecGroupsChirurgen "
                            + "(ID_SecGroupsChirurgen counter primary key,"
                            + "ID_SecGroups long not null,"
                            + "ID_Chirurgen long not null,"
                            + "CONSTRAINT FKSecGroupsChirurgenSecGroup FOREIGN KEY (ID_SecGroups) REFERENCES SecGroups(ID_SecGroups),"
                            + "CONSTRAINT FKSecGroupsChirurgenChirurgen FOREIGN KEY (ID_Chirurgen) REFERENCES Chirurgen(ID_Chirurgen)"
                            + ")";
                    command.ExecuteNonQuery();

                    // Neue Tabelle SecRights
                    command.CommandText = "create table SecRights "
                            + "(ID_SecRights counter primary key,"
                            + "Name Text(255) not null constraint IdxName unique,"
                            + "Description Text(255) not null"
                            + ")";
                    command.ExecuteNonQuery();

                    // Neue Tabelle SecGroupsSecRights
                    command.CommandText = "create table SecGroupsSecRights "
                            + "(ID_SecGroupsSecRights counter primary key,"
                            + "ID_SecGroups long not null,"
                            + "ID_SecRights long not null,"
                            + "CONSTRAINT FKSecGroupsSecRightsSecGroups FOREIGN KEY (ID_SecGroups) REFERENCES SecGroups(ID_SecGroups),"
                            + "CONSTRAINT FKSecGroupsSecRightsSecRights FOREIGN KEY (ID_SecRights) REFERENCES SecRights(ID_SecRights)"
                            + ")";
                    command.ExecuteNonQuery();

                    // Eine Abteilung anlegen
                    command.CommandText = "insert into Abteilungen ([Text]) values ('Abteilung')";
                    command.ExecuteNonQuery();

                    // Alle Chirurgen gehören zu dieser Abteilung
                    command.CommandText = "insert into AbteilungenChirurgen (ID_Abteilungen, ID_Chirurgen) select ID_Abteilungen, ID_Chirurgen from Abteilungen, Chirurgen";
                    command.ExecuteNonQuery();

                    V19InsertRights(command);

                    // Rolle 'Superuser' anlegen
                    command.CommandText = "insert into SecGroups ([Text]) values ('Superusers')";
                    command.ExecuteNonQuery();

                    // Alle User zur 'Superuser' Rolle hinzufügen
                    command.CommandText = "insert into SecGroupsChirurgen (ID_SecGroups, ID_Chirurgen) select ID_SecGroups, ID_Chirurgen from SecGroups, Chirurgen";
                    command.ExecuteNonQuery();

                    // Alle Gruppen ('Superuser') bekommt alle Rechte
                    command.CommandText = "insert into SecGroupsSecRights (ID_SecGroups, ID_SecRights) select ID_SecGroups, ID_SecRights from SecGroups, SecRights";
                    command.ExecuteNonQuery();

                    // Gruppen 'DV-Koordinator', 'Weiterbilder', 'Chirurgen' anlegen
                    command.CommandText = "insert into SecGroups ([Text]) values ('DV-Koordinator')";
                    command.ExecuteNonQuery();
                    command.CommandText = "insert into SecGroups ([Text]) values ('Weiterbilder')";
                    command.ExecuteNonQuery();
                    command.CommandText = "insert into SecGroups ([Text]) values ('Chirurgen')";
                    command.ExecuteNonQuery();

                    command.CommandText = "UPDATE Config SET [Value] = '19' where [Key] = 'MinorVersion'";
                    command.ExecuteNonQuery();

                    trans.Commit();
                    bSuccess = true;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    strError = e.Message;
                    bSuccess = false;
                }
            }

            return bSuccess;
        }

        protected override internal bool UpdateV1_19ToV1_20(ref string strError)
        {
            bool bSuccess = false;

            using (DbConnection conn = DataFactory.CreateConnection())
            {
                conn.ConnectionString = ConnectionString;
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    /*
                     * In Version 1.18 wurde er 2. Assitent hinzugefügt (update), fehlte aber in der Version selber (setup), 
                     * wenn man also erst 1.18 installiert und dann ein update macht, 
                     * fehlt er weiterhin
                     * Eintrag '2. Assistent' in Tabelle OPFunktionen hinzufügen wenn er fehlt
                     */
                    DbCommand command = conn.CreateCommand();
                    command.Transaction = trans;

                    long count = ExecuteScalar("select count(ID_OPFunktionen) from OPFunktionen where ID_OPFunktionen = 3");
                    if (count == 0)
                    {
                        command.CommandText = "insert into OPFunktionen (ID_OPFunktionen, LfdNr, Beschreibung) values (3, 3, '2. Assistent')";
                        command.ExecuteNonQuery();
                    }

                    command.CommandText = "UPDATE Config SET [Value] = '20' where [Key] = 'MinorVersion'";
                    command.ExecuteNonQuery();
                    trans.Commit();

                    bSuccess = true;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    strError = e.Message;
                    bSuccess = false;
                }
            }

            return bSuccess;
        }

        protected override internal bool UpdateV1_20ToV1_21(ref string strError)
        {
            bool bSuccess = false;

            using (DbConnection conn = DataFactory.CreateConnection())
            {
                conn.ConnectionString = ConnectionString;
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    /*
                     * Neues Feld Chirurgen.ImportID
                     */
                    DbCommand command = conn.CreateCommand();
                    command.Transaction = trans;

                    command.CommandText = "alter table Chirurgen ADD COLUMN ImportID Text(50) not null constraint IdxImportID unique";
                    command.ExecuteNonQuery();
                    command.CommandText = "update Chirurgen set ImportID = UserID";
                    command.ExecuteNonQuery();

                    command.CommandText = "UPDATE Config SET [Value] = '21' where [Key] = 'MinorVersion'";
                    command.ExecuteNonQuery();
                    trans.Commit();

                    bSuccess = true;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    strError = e.Message;
                    bSuccess = false;
                }
            }

            return bSuccess;
        }
        protected override internal bool UpdateV1_21ToV1_22(ref string strError)
        {
            bool bSuccess = false;

            using (DbConnection conn = DataFactory.CreateConnection())
            {
                conn.ConnectionString = ConnectionString;
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    /*
                     * Neues Recht 'Auswertungen > ausgeführte Prozeduren: nach OPSKode
                     */
                    DbCommand command = conn.CreateCommand();
                    command.Transaction = trans;

                    command.CommandText = "insert into SecRights (Name, Description) values ('OperationenVergleichView.edit', 'Menü: Auswertungen > Ausgeführte Prozeduren: nach OPSKode')";
                    command.ExecuteNonQuery();

                    command.CommandText = "UPDATE Config SET [Value] = '22' where [Key] = 'MinorVersion'";
                    command.ExecuteNonQuery();
                    trans.Commit();

                    bSuccess = true;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    strError = e.Message;
                    bSuccess = false;
                }
            }

            return bSuccess;
        }

        protected override internal bool UpdateV1_25ToV1_26(ref string strError)
        {
            bool bSuccess = false;

            using (DbConnection conn = DataFactory.CreateConnection())
            {
                conn.ConnectionString = ConnectionString;
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    /*
                     * Neues Recht 'Auswertungen > ausgeführte Prozeduren: nach OPSKode
                     */
                    DbCommand command = conn.CreateCommand();
                    command.Transaction = trans;

                    UpdateSecRight(command, "RichtlinienSollIstView.view",      "V1.26.0");
                    UpdateSecRight(command, "RichtlinienSollIstView.cmdUpdate", "V1.26.0");
                    UpdateSecRight(command, "RichtlinienSollIstView.cmdAdd",    "V1.26.0");
                    UpdateSecRight(command, "RichtlinienSollIstView.cmdDelete", "V1.26.0");

                    // Neue Tabelle GebieteSoll
                    command.CommandText = "create table GebieteSoll "
                            + "(ID_GebieteSoll counter primary key,"
                            + "ID_Chirurgen long not null,"
                            + "ID_Gebiete long not null,"
                            + "Von DateTime not null,"
                            + "Bis DateTime not null,"
                            + "CONSTRAINT FKGebieteSollChirurgen FOREIGN KEY (ID_Chirurgen) REFERENCES Chirurgen(ID_Chirurgen),"
                            + "CONSTRAINT FKGebieteSollGebiete FOREIGN KEY (ID_Gebiete) REFERENCES Gebiete(ID_Gebiete)"
                            + ")";
                    command.ExecuteNonQuery();

                    // Neue Tabelle RichtlinienSoll
                    command.CommandText = "create table RichtlinienSoll "
                            + "(ID_RichtlinienSoll counter primary key,"
                            + "ID_GebieteSoll long not null,"
                            + "ID_Richtlinien long not null,"
                            + "Soll long not null,"
                            + "CONSTRAINT FKRichtlinienSollGebieteSoll FOREIGN KEY (ID_GebieteSoll) REFERENCES GebieteSoll(ID_GebieteSoll),"
                            + "CONSTRAINT FKRichtlinienSollRichtlinien FOREIGN KEY (ID_Richtlinien) REFERENCES Richtlinien(ID_Richtlinien)"
                            + ")";
                    command.ExecuteNonQuery();

                    command.CommandText = "UPDATE Config SET [Value] = '26' where [Key] = 'MinorVersion'";
                    command.ExecuteNonQuery();
                    trans.Commit();

                    bSuccess = true;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    strError = e.Message;
                    bSuccess = false;
                }
            }

            return bSuccess;
        }

        protected override internal bool UpdateV1_26ToV1_27(ref string strError)
        {
            bool bSuccess = false;

            using (DbConnection conn = DataFactory.CreateConnection())
            {
                conn.ConnectionString = ConnectionString;
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    /*
                     * Neues Feld UserSettings.Blob (binary, null)
                     */
                    DbCommand command = conn.CreateCommand();
                    command.Transaction = trans;

                    command.CommandText = "alter table UserSettings ADD COLUMN [Blob] OLEOBJECT NULL";
                    command.ExecuteNonQuery();

                    command.CommandText = "UPDATE Config SET [Value] = '27' where [Key] = 'MinorVersion'";
                    command.ExecuteNonQuery();
                    trans.Commit();

                    bSuccess = true;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    strError = e.Message;
                    bSuccess = false;
                }
            }

            return bSuccess;
        }

        /// <summary>
        /// UserSettings.Blob was NULL in 1.26 and was updated correctly to NULL in 1.27,
        /// so we do nothing for MSAccess
        /// </summary>
        /// <param name="strError"></param>
        /// <returns></returns>
        protected override internal bool UpdateV1_28ToV1_29(ref string strError)
        {
            bool bSuccess = false;

            using (DbConnection conn = DataFactory.CreateConnection())
            {
                conn.ConnectionString = ConnectionString;
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();

                try
                {
                    DbCommand command = conn.CreateCommand();
                    command.Transaction = trans;

                    command.CommandText = "UPDATE Config SET [Value] = '29' where [Key] = 'MinorVersion'";
                    command.ExecuteNonQuery();

                    trans.Commit();

                    bSuccess = true;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    strError = e.Message;
                    bSuccess = false;
                }
            }

            return bSuccess;
        }
    }
}


