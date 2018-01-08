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
    /// <summary>
    /// </summary>
    public class DatabaseMySql : DatabaseLayer
    {
        public DatabaseMySql(BusinessLayer businessLayer, DatabaseType databaseType, string connectionString)
            : base(businessLayer, databaseType, connectionString)
        {
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

                    UpdateSecRight(command, "RichtlinienSollIstView.view",          "V1.26.0");
                    UpdateSecRight(command, "RichtlinienSollIstView.cmdUpdate",     "V1.26.0");
                    UpdateSecRight(command, "RichtlinienSollIstView.cmdAdd",        "V1.26.0");
                    UpdateSecRight(command, "RichtlinienSollIstView.cmdDelete",     "V1.26.0");

                    string sql = @"
                        CREATE TABLE `operationen`.`GebieteSoll` (
                            `ID_GebieteSoll` INT(10) NOT NULL AUTO_INCREMENT,
                            `ID_Chirurgen` INT(10) NOT NULL,
                            `ID_Gebiete` INT(10) NOT NULL,
                            `Von` DATETIME NOT NULL,
                            `Bis` DATETIME NOT NULL,
                          PRIMARY KEY (`ID_GebieteSoll`),
                          INDEX `ID_Chirurgen` (`ID_Chirurgen`),
                          INDEX `ID_Gebiete` (`ID_Gebiete`),
                          CONSTRAINT `GebieteSollChirurgen` FOREIGN KEY `GebieteSollChirurgen` (`ID_Chirurgen`)
                            REFERENCES `operationen`.`Chirurgen` (`ID_Chirurgen`)
                            ON DELETE RESTRICT
                            ON UPDATE RESTRICT,
                          CONSTRAINT `GebieteSollGebiete` FOREIGN KEY `GebieteSollGebiete` (`ID_Gebiete`)
                            REFERENCES `operationen`.`Gebiete` (`ID_Gebiete`)
                            ON DELETE RESTRICT
                            ON UPDATE RESTRICT
                        )
                        ENGINE = INNODB;
                        ";

                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                    
                    sql = @"                    
                    CREATE TABLE `operationen`.`RichtlinienSoll` (
                        `ID_RichtlinienSoll` INT(10) NOT NULL AUTO_INCREMENT,
                        `ID_GebieteSoll` INT(10) NOT NULL,
                        `ID_Richtlinien` INT(10) NOT NULL,
                        `Soll` INT(10) NOT NULL,
                      PRIMARY KEY (`ID_RichtlinienSoll`),
                      INDEX `ID_GebieteSoll` (`ID_GebieteSoll`),
                      INDEX `ID_Richtlinien` (`ID_Richtlinien`),
                      CONSTRAINT `RichtlinienSollGebieteSoll` FOREIGN KEY `RichtlinienSollGebieteSoll` (`ID_GebieteSoll`)
                        REFERENCES `operationen`.`GebieteSoll` (`ID_GebieteSoll`)
                        ON DELETE RESTRICT
                        ON UPDATE RESTRICT,
                      CONSTRAINT `RichtlinienSollRichtlinien` FOREIGN KEY `RichtlinienSollRichtlinien` (`ID_Richtlinien`)
                        REFERENCES `operationen`.`Richtlinien` (`ID_Richtlinien`)
                        ON DELETE RESTRICT
                        ON UPDATE RESTRICT
                    )
                    ENGINE = INNODB;
                    ";

                    command.CommandText = sql;
                    command.ExecuteNonQuery();

                    command.CommandText = "UPDATE Config SET `Value` = '26' where `Key` = 'MinorVersion'";
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
                    DbCommand command = conn.CreateCommand();
                    command.Transaction = trans;

                    //
                    // Neue Spalte UserSettings.Blob
                    //
                    command.CommandText = "ALTER TABLE `operationen`.`UserSettings` ADD `Blob` LONGBLOB";
                    command.ExecuteNonQuery();

                    command.CommandText = "UPDATE `operationen`.`Config` SET `Value` = '27' where `Key` = 'MinorVersion'";
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

                    //
                    // Neue Spalte UserSettings.Blob
                    //
                    command.CommandText = "ALTER TABLE `operationen`.`UserSettings` MODIFY COLUMN `Blob` LONGBLOB NULL";
                    command.ExecuteNonQuery();

                    command.CommandText = "UPDATE `operationen`.`Config` SET `Value` = '29' where `Key` = 'MinorVersion'";
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


