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
    /// 19.07.2009 Klinik Dessau verwendet SqlServer, daher muss ein Datenbankupdate fuer diesen unterstützt werden!
    /// </summary>
    public class DatabaseSqlServer : DatabaseLayer
    {
        public DatabaseSqlServer(BusinessLayer businessLayer, DatabaseType databaseType, string connectionString)
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

                    //
                    //'AbteilungenView.view' war falsch als 'AbteilungenView' vorhanden.
                    //
                    command.CommandText = "update SecRights set Name='AbteilungenView.view' where name = 'AbteilungenView'";
                    command.ExecuteNonQuery();

                    UpdateSecRight(command, "RichtlinienSollIstView.view",      "V1.26.0");
                    UpdateSecRight(command, "RichtlinienSollIstView.cmdUpdate", "V1.26.0");
                    UpdateSecRight(command, "RichtlinienSollIstView.cmdAdd",    "V1.26.0");
                    UpdateSecRight(command, "RichtlinienSollIstView.cmdDelete", "V1.26.0");

                    string sql = @"
                        SET ANSI_NULLS ON
                        ;
                        SET QUOTED_IDENTIFIER ON
                        ;
                        CREATE TABLE [dbo].GebieteSoll(
	                        [ID_GebieteSoll] [int] IDENTITY(1,1) NOT NULL,
	                        [ID_Chirurgen] [int] NOT NULL,
	                        [ID_Gebiete] [int] NOT NULL,
	                        [Von] [datetime] NOT NULL,
	                        [Bis] [datetime] NOT NULL,
                         CONSTRAINT [PK_GebieteSoll] PRIMARY KEY CLUSTERED 
                        (
	                        [ID_GebieteSoll] ASC
                        )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
                        )
                                            
                        -- Object:  ForeignKey [FK_GebieteSoll_Chirurgen]
                        ALTER TABLE [dbo].[GebieteSoll]  WITH CHECK ADD  CONSTRAINT [FK_GebieteSoll_Chirurgen] FOREIGN KEY([ID_Chirurgen])
                        REFERENCES [dbo].[Chirurgen] ([ID_Chirurgen])
                        ;
                        ALTER TABLE [dbo].[GebieteSoll] CHECK CONSTRAINT [FK_GebieteSoll_Chirurgen]
                        ;

                        -- Object:  ForeignKey [FK_GebieteSoll_Gebiete]
                        ALTER TABLE [dbo].[GebieteSoll]  WITH CHECK ADD  CONSTRAINT [FK_GebieteSoll_Gebiete] FOREIGN KEY([ID_Gebiete])
                        REFERENCES [dbo].[Gebiete] ([ID_Gebiete])
                        ;
                        ALTER TABLE [dbo].[GebieteSoll] CHECK CONSTRAINT [FK_GebieteSoll_Gebiete]
                        ;

                        -- Neue Tabelle RichtlinienSoll
                        SET ANSI_NULLS ON
                        ;
                        SET QUOTED_IDENTIFIER ON
                        ;
                        CREATE TABLE [dbo].RichtlinienSoll(
	                        [ID_RichtlinienSoll] [int] IDENTITY(1,1) NOT NULL,
	                        [ID_GebieteSoll] [int] NOT NULL,
	                        [ID_Richtlinien] [int] NOT NULL,
	                        [Soll] [int] NOT NULL,
                         CONSTRAINT [PK_RichtlinienSoll] PRIMARY KEY CLUSTERED 
                        (
	                        [ID_RichtlinienSoll] ASC
                        )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
                        )
                        ;

                        -- Object:  ForeignKey [FK_RichtlinienSoll_GebieteSoll]
                        ALTER TABLE [dbo].[RichtlinienSoll]  WITH CHECK ADD  CONSTRAINT [FK_RichtlinienSoll_GebieteSoll] FOREIGN KEY([ID_GebieteSoll])
                        REFERENCES [dbo].[GebieteSoll] ([ID_GebieteSoll])
                        ;
                        ALTER TABLE [dbo].[RichtlinienSoll] CHECK CONSTRAINT [FK_RichtlinienSoll_GebieteSoll]
                        ;

                        -- Object:  ForeignKey [FK_RichtlinienSoll_Richtlinien]
                        ALTER TABLE [dbo].[RichtlinienSoll]  WITH CHECK ADD  CONSTRAINT [FK_RichtlinienSoll_Richtlinien] FOREIGN KEY([ID_Richtlinien])
                        REFERENCES [dbo].[Richtlinien] ([ID_Richtlinien])
                        ;
                        ALTER TABLE [dbo].[RichtlinienSoll] CHECK CONSTRAINT [FK_RichtlinienSoll_Richtlinien]
                        ;
                        ";

                    command.CommandText = sql;
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
                    DbCommand command = conn.CreateCommand();
                    command.Transaction = trans;

                    //
                    // Neue Spalte UserSettings.Blob
                    //
                    command.CommandText = "ALTER TABLE [dbo].[UserSettings] ADD [Blob] [varbinary](max)";
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
                    command.CommandText = "ALTER TABLE [dbo].[UserSettings] ALTER COLUMN [Blob] [varbinary](max) NULL";
                    command.ExecuteNonQuery();

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


