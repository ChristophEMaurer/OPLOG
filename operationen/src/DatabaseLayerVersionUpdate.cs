using System;
using System.Windows.Forms;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Globalization;

using Utility;
using AppFramework;
using Operationen;
using CMaurer.Operationen.AppFramework;
using Operationen.Wizards.CreateCustomerData;
using Security.Cryptography;

namespace Operationen
{
    public partial class DatabaseLayer
    {

        #region Database Updates

        /// <summary>
        /// Hier stehen nur Datenbankänderungen, nicht Änderungen am Programm!!!
        /// </summary>
        /// <param name="DatabaseMajor"></param>
        /// <param name="DatabaseMinor"></param>
        /// <param name="sb"></param>
        protected internal void GetDatabaseChanges(int DatabaseMajor, int DatabaseMinor, StringBuilder sb)
        {
            if (DatabaseMajor == 1 && DatabaseMinor < 29)
            {
                sb.Append("$r$$r$Änderungen in Version 1.29:");
                sb.Append("$r$- Datenbankfeld UserSettings.Blob geändert in Nullable für SQLServer und MySQL");
            }
            if (DatabaseMajor == 1 && DatabaseMinor < 28)
            {
                sb.Append("$r$$r$Änderungen in Version 1.28:");
                sb.Append("$r$- Neue Rechte");
            }
            if (DatabaseMajor == 1 && DatabaseMinor < 27)
            {
                sb.Append("$r$$r$Änderungen in Version 1.27:");
                sb.Append("$r$- Favoritenleiste ist Benutzer-spezifisch (UserSettings.Blob)");
            }
            if (DatabaseMajor == 1 && DatabaseMinor < 26)
            {
                sb.Append("$r$$r$Änderungen in Version 1.26:");
                sb.Append("$r$- Neue Rechte");
                sb.Append("$r$- Neue Tabellen für Soll-Zahlen für Weiterbildungsrichtlinien");
            }
            if (DatabaseMajor == 1 && DatabaseMinor < 25)
            {
                sb.Append("$r$$r$Änderungen in Version 1.25:");
                sb.Append("$r$- Neue Rechte");
            }
            if (DatabaseMajor == 1 && DatabaseMinor < 24)
            {
                sb.Append("$r$$r$Änderungen in Version 1.24:");
                sb.Append("$r$- 3. Assistent");
                sb.Append("$r$- Die Beschreibungen der Rechte werden direkt aus den Menüs ausgelesen");
                sb.Append("$r$- Neue Rechte");
            }
            if (DatabaseMajor == 1 && DatabaseMinor < 23)
            {
                sb.Append("$r$$r$Änderungen in Version 1.23:$r$- Neue Rechte eingefügt");
            }
            if (DatabaseMajor == 1 && DatabaseMinor < 22)
            {
                sb.Append("$r$$r$Änderungen in Version 1.22:$r$- Menürecht für neue Auswertung 'Auswertungen > Ausgeführte Prozeduren: nach OPSKode' eingefügt");
            }
            if (DatabaseMajor == 1 && DatabaseMinor < 21)
            {
                sb.Append("$r$$r$Änderungen in Version 1.21:$r$- Importname eingeführt");
            }
            if (DatabaseMajor == 1 && DatabaseMinor < 20)
            {
                sb.Append("$r$$r$Änderungen in Version 1.20:$r$- '2. Assistent' eingefügt wenn nicht vorhanden");
            }
            if (DatabaseMajor == 1 && DatabaseMinor < 19)
            {
                sb.Append("$r$$r$Änderungen in Version 1.19:$r$- Benutzereinstellungen individuell$r$- Merkmal 'Weiterbilder' eingeführt");
                sb.Append("$r$- Abteilungen, Benutzergruppen und Rechteverwaltung eingeführt");
            }
            if (DatabaseMajor == 1 && DatabaseMinor < 18)
            {
                sb.Append("$r$$r$Änderungen in Version 1.18:$r$- '2. Assistent' eingefügt");
            }
            if (DatabaseMajor == 1 && DatabaseMinor < 17)
            {
                sb.Append("$r$$r$Änderungen in Version 1.17:$r$- 'Klinische Ergebnisse' eingeführt");
            }
            if (DatabaseMajor == 1 && DatabaseMinor < 16)
            {
                sb.Append("$r$$r$Änderungen in Version 1.16:$r$- Merkmal 'intern/extern' eingeführt$r$- 'Extern erfüllte Richtzahlen' eingeführt");
            }
            if (DatabaseMajor == 1 && DatabaseMinor < 15)
            {
                sb.Append("$r$$r$Änderungen in Version 1.15:$r$- 'Operateur' eingefügt$r$- Status für Kommentare eingeführt");
            }
            if (DatabaseMajor == 1 && DatabaseMinor < 14)
            {
                sb.Append("$r$$r$Änderungen in Version 1.14:$r$- Seriennummernverwaltung eingeführt");
            }
            if (DatabaseMajor == 1 && DatabaseMinor < 13)
            {
                sb.Append("$r$$r$Änderungen in Version 1.13:$r$- Einige Pflichtfelder waren nullable");
            }
            if (DatabaseMajor == 1 && DatabaseMinor < 12)
            {
                sb.Append("$r$$r$Änderungen in Version 1.12:$r$- Operationen sind keine Referenzen mehr$r$- Seriennummern eingeführt");
            }
            if (DatabaseMajor == 1 && DatabaseMinor < 11)
            {
                sb.Append("$r$$r$Änderungen in Version 1.11:$r$- 'Fallzahl' eingeführt");
            }
            if (DatabaseMajor == 1 && DatabaseMinor < 10)
            {
                sb.Append("$r$$r$Änderungen in Version 1.10:$r$- 'Dienststellung' eingeführt$r$- Merkmal 'Chirurg aktiv' eingeführt");
                sb.Append("$r$-feste Zuordnung einer Operation zu einer Richtlinie");
            }
            if (DatabaseMajor == 1 && DatabaseMinor < 9)
            {
                sb.Append("$r$$r$Änderungen in Version 1.9:$r$- 'Akademische Ausbildung' eingeführt");
            }
        }

        protected virtual internal bool UpdateV1_8ToV1_9(ref string strError)
        {
            strError = "A database schema update is not supported for this database";
            return false;
        }
        protected virtual internal bool UpdateV1_9ToV1_10(ref string strError)
        {
            strError = "A database schema update is not supported for this database";
            return false;
        }
        protected virtual internal bool UpdateV1_10ToV1_11(ref string strError)
        {
            strError = "A database schema update is not supported for this database";
            return false;
        }
        protected virtual internal bool UpdateV1_11ToV1_12(ref string strError)
        {
            strError = "A database schema update is not supported for this database";
            return false;
        }
        protected virtual internal bool UpdateV1_12ToV1_13(ref string strError)
        {
            strError = "A database schema update is not supported for this database";
            return false;
        }
        protected virtual internal bool UpdateV1_13ToV1_14(ref string strError)
        {
            strError = "A database schema update is not supported for this database";
            return false;
        }
        protected virtual internal bool UpdateV1_14ToV1_15(ref string strError)
        {
            strError = "A database schema update is not supported for this database";
            return false;
        }
        protected virtual internal bool UpdateV1_15ToV1_16(ref string strError)
        {
            strError = "A database schema update is not supported for this database";
            return false;
        }
        protected virtual internal bool UpdateV1_16ToV1_17(ref string strError)
        {
            strError = "A database schema update is not supported for this database";
            return false;
        }
        protected virtual internal bool UpdateV1_17ToV1_18(ref string strError)
        {
            strError = "A database schema update is not supported for this database";
            return false;
        }
        protected virtual internal bool UpdateV1_18ToV1_19(ref string strError)
        {
            strError = "A database schema update is not supported for this database";
            return false;
        }
        protected virtual internal bool UpdateV1_19ToV1_20(ref string strError)
        {
            strError = "A database schema update is not supported for this database";
            return false;
        }
        protected virtual internal bool UpdateV1_20ToV1_21(ref string strError)
        {
            strError = "A database schema update is not supported for this database";
            return false;
        }
        protected virtual internal bool UpdateV1_21ToV1_22(ref string strError)
        {
            strError = "A database schema update is not supported for this database";
            return false;
        }

        /// <summary>
        /// Standard SQL: Update fuer alle Datenbanken.
        /// </summary>
        /// <param name="strError"></param>
        /// <returns></returns>
        protected internal bool UpdateV1_22ToV1_23(ref string strError)
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
                     * Neue Rechte
                     */
                    DbCommand command = conn.CreateCommand();
                    command.Transaction = trans;

                    command.CommandText = "insert into SecRights (Name, Description) values ('mn.InstallLicense', 'Menü: Extras > Lizenzdatei installieren Assistent')";
                    command.ExecuteNonQuery();
                    command.CommandText = "insert into SecRights (Name, Description) values ('OperationenKatalogView.cmdInsert', 'Schaltfläche: Offizielle Dokumente > OPS-Katalog > Einfügen')";
                    command.ExecuteNonQuery();
                    command.CommandText = "insert into SecRights (Name, Description) values ('OperationenKatalogView.cmdDelete', 'Schaltfläche: Offizielle Dokumente > OPS-Katalog > Löschen')";
                    command.ExecuteNonQuery();
                    command.CommandText = "insert into SecRights (Name, Description) values ('OperationenKatalogView.cmdDeleteAll', 'Schaltfläche: Offizielle Dokumente > OPS-Katalog > Gesamten OPS-Katalog löschen')";
                    command.ExecuteNonQuery();

                    command.CommandText = CleanSqlStatement("UPDATE Config SET [Value] = '23' where [Key] = 'MinorVersion'");
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
        /// Recht einfügen wenn es fehlt. Falls es schon vorhanden ist, was eigentlich nicht sein kann, wird die Description 'gesetzt'.
        /// Das ist gut während der Entwicklung, man muss nicht immer alle Rechte löschen wenn mehr als eines dazu kommt.
        /// Die Description kommt jetzt aus dem Menü
        /// </summary>
        /// <param name="command"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        protected void UpdateSecRight(DbCommand command, string name, string description)
        {
            string sqlSelect = string.Format(CultureInfo.InvariantCulture, "select count(ID_SecRights) from SecRights where Name='{0}'", name);
            string sqlUpdate = string.Format(CultureInfo.InvariantCulture, "update SecRights set Description='{0}' where Name='{1}'", description, name);
            string sqlInsert = string.Format(CultureInfo.InvariantCulture, "insert into SecRights (Name, Description) values ('{0}', '{1}')", name, description);

            command.CommandText = sqlSelect;
            object oCount = command.ExecuteScalar();
            int count = Convert.ToInt32(oCount, CultureInfo.InvariantCulture);

            if (count > 0)
            {
                command.CommandText = sqlUpdate;
            }
            else
            {
                command.CommandText = sqlInsert;
            }
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Falls der Eintrag fehlt, wird er hinzugefügt, ansonsten nichts getan, er soll NICHT geändert werden
        /// </summary>
        /// <param name="command"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void UpdateConfig(DbCommand command, string key, string value)
        {
            string sqlSelect = CleanSqlStatement(string.Format(CultureInfo.InvariantCulture, "select count(ID_Config) from Config where [Key]='{0}'", key));
            string sqlInsert = CleanSqlStatement(string.Format(CultureInfo.InvariantCulture, "insert into Config ([Key], [Value]) values ('{0}', '{1}')", key, value));

            command.CommandText = sqlSelect;
            object oCount = command.ExecuteScalar();
            int count = Convert.ToInt32(oCount, CultureInfo.InvariantCulture);

            if (count == 0)
            {
                command.CommandText = sqlInsert;
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Standard SQL: Update fuer alle Datenbanken.
        /// </summary>
        /// <param name="strError"></param>
        /// <returns></returns>
        protected internal bool UpdateV1_23ToV1_24(ref string strError)
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
                     * Description jetzt aus Text Resource
                     */
                    DbCommand command = conn.CreateCommand();
                    command.Transaction = trans;

                    //
                    // Description 'löschen'
                    //
                    UpdateSecRight(command, "ExportOperationenKatalogView.edit", "V1.24.0");
                    UpdateSecRight(command, "ImportOperationenMobileWizard.edit", "V1.24.0");

                    //
                    // Für alle SecRights die Desctiption 'löschen'
                    //
                    command.CommandText = "update SecRights set Description='V1.24.0 removed'";
                    command.ExecuteNonQuery();

                    //
                    // "InstalledVersion" einfügen, wenn es fehlt
                    // 
                    UpdateConfig(command, "InstalledVersion", "1.24.0 " + BusinessLayerCommon.VersionDate);

                    //
                    // Luebbecke möchte einen dritten Assistentn
                    //
                    command.CommandText = "insert into OPFunktionen (ID_OPFunktionen, LfdNr, Beschreibung) values (4, 4, '3. Assistent')";
                    command.ExecuteNonQuery();

                    command.CommandText = CleanSqlStatement("UPDATE Config SET [Value] = '24' where [Key] = 'MinorVersion'");
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

        protected internal bool UpdateV1_24ToV1_25(ref string strError)
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

                    //
                    // In SQLServer und MySql V1.24 hatte ich diese vergessen
                    //
                    UpdateSecRight(command, "ExportOperationenKatalogView.edit", "V1.24.0");
                    UpdateSecRight(command, "ImportOperationenMobileWizard.edit", "V1.24.0");

                    //
                    // Neue Rechte
                    //
                    UpdateSecRight(command, "OperationenSummaryView.view", "V1.25.0");
                    UpdateSecRight(command, "RichtlinienView.view", "V1.25.0");
                    UpdateSecRight(command, "GebieteView.view", "V1.25.0");
                    UpdateSecRight(command, "AbteilungenView.view", "V1.25.0");
                    UpdateSecRight(command, "AkademischeAusbildungTypenView.view", "V1.25.0");
                    UpdateSecRight(command, "ChirurgenFunktionenView.view", "V1.25.0");
                    UpdateSecRight(command, "DateiTypenView.view", "V1.25.0");
                    UpdateSecRight(command, "NotizTypenView.view", "V1.25.0");
                    UpdateSecRight(command, "SecGroupsView.view", "V1.25.0");
                    UpdateSecRight(command, "PlanOperationenView.view", "V1.25.0");

                    command.CommandText = CleanSqlStatement("UPDATE Config SET [Value] = '25' where [Key] = 'MinorVersion'");
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

        protected virtual internal bool UpdateV1_25ToV1_26(ref string strError)
        {
            //
            // Neue Datenbanktabellen: die sind Datenbank-spezifisch
            //
            strError = "A database schema update is not supported for this database";
            return false;
        }

        protected virtual internal bool UpdateV1_26ToV1_27(ref string strError)
        {
            //
            // Neue Datenbanktabellen: die sind Datenbank-spezifisch
            //
            strError = "A database schema update has not been implemented for this database type. Contact the vendor with this message.";
            return false;
        }

        /// <summary>
        /// Standard SQL changes: the same SQL for all databases. No schema change, only new records.
        /// </summary>
        /// <param name="strError"></param>
        /// <returns></returns>
        protected internal bool UpdateV1_27ToV1_28(ref string strError)
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

                    //
                    // Neue Rechte
                    //
                    UpdateSecRight(command, "OperationenImportView.cmdSave", "V1.28.0");
                    UpdateSecRight(command, "ClientServerView.view", "V1.28.0");
                    UpdateSecRight(command, "ClientServerView.cmdSend", "V1.28.0");
                    UpdateSecRight(command, "ClientServerView.cmdReceive", "V1.28.0");

                    command.CommandText = CleanSqlStatement("UPDATE Config SET [Value] = '28' where [Key] = 'MinorVersion'");
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

        protected virtual internal bool UpdateV1_28ToV1_29(ref string strError)
        {
            //
            // Neue Datenbanktabellen: die sind Datenbank-spezifisch
            //
            strError = "A database schema update has not been implemented for this database type. Contact the vendor with this message.";
            return false;
        }

        #endregion
    }
}
