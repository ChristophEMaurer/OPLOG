/*
Fehler
Access:
ChirurgenOperationen.ID_Chirurgen ist nicht not null!!!
PlanOperationen.ID_Chirurgen ist nicht not null
PlanOperationen.Operation ist nicht not null
PlanOperationen.Anzahl ist nicht not null
PlanOperationen.DatumVon, DatumBis ist nicht not null

RichtlinienOpsCodes.ID_Richtlinien ist nicht not null.

MySQL: 
LogTable.Timestamp: wie default now() angeben?
*/

-- ----------------------------------------------------------------------
-- MySQL Migration Toolkit
-- SQL Create Script
-- ----------------------------------------------------------------------

SET FOREIGN_KEY_CHECKS = 0;

CREATE DATABASE IF NOT EXISTS `operationen`
  CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `operationen`;
-- -------------------------------------
-- Tables

DROP TABLE IF EXISTS `operationen`.`AkademischeAusbildungen`;
CREATE TABLE `operationen`.`AkademischeAusbildungen` (
  `ID_AkademischeAusbildungen` INT(10) NOT NULL AUTO_INCREMENT,
  `ID_AkademischeAusbildungTypen` INT(10) NOT NULL,
  `ID_Chirurgen` INT(10) NOT NULL,
  `Beginn` DATETIME NOT NULL,
  `Ende` DATETIME NULL,
  `Organisation` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`ID_AkademischeAusbildungen`),
  UNIQUE INDEX `Index_87C5A5EB_DBB7_4629` (`ID_AkademischeAusbildungen`),
  INDEX `FKAkadAusbAkadAusbTypen` (`ID_AkademischeAusbildungTypen`),
  INDEX `FKAkadAusbChirurgen` (`ID_Chirurgen`),
  CONSTRAINT `FKAkadAusbAkadAusbTypen` FOREIGN KEY `FKAkadAusbAkadAusbTypen` (`ID_AkademischeAusbildungTypen`)
    REFERENCES `operationen`.`AkademischeAusbildungTypen` (`ID_AkademischeAusbildungTypen`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT,
  CONSTRAINT `FKAkadAusbChirurgen` FOREIGN KEY `FKAkadAusbChirurgen` (`ID_Chirurgen`)
    REFERENCES `operationen`.`Chirurgen` (`ID_Chirurgen`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT
)
ENGINE = INNODB;

DROP TABLE IF EXISTS `operationen`.`AkademischeAusbildungTypen`;
CREATE TABLE `operationen`.`AkademischeAusbildungTypen` (
  `ID_AkademischeAusbildungTypen` INT(10) NOT NULL AUTO_INCREMENT,
  `Text` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`ID_AkademischeAusbildungTypen`),
  UNIQUE INDEX `Index_53518C06_E053_4BAD` (`ID_AkademischeAusbildungTypen`)
)
ENGINE = INNODB;

DROP TABLE IF EXISTS `operationen`.`Chirurgen`;
CREATE TABLE `operationen`.`Chirurgen` (
  `ID_Chirurgen` INT(10) NOT NULL AUTO_INCREMENT,
  `ID_Groups` INT(10) NOT NULL,
  `ID_ChirurgenFunktionen` INT(10) NOT NULL,
  `Nachname` VARCHAR(50) NOT NULL,
  `Vorname` VARCHAR(50) NOT NULL DEFAULT '',
  `Anfangsdatum` DATETIME NOT NULL,
  `Anrede` VARCHAR(50) NOT NULL DEFAULT '',
  `UserID` VARCHAR(20) NOT NULL,
  `Password` VARCHAR(50) NOT NULL,
  `MustChangePassword` INT(10) NOT NULL DEFAULT 0,
  `Aktiv` INT(10) NOT NULL,
  `Lizenzdaten` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`ID_Chirurgen`),
  UNIQUE INDEX `UserID` (`UserID`),
  INDEX `ChirurgenFunktionenChirurgen` (`ID_ChirurgenFunktionen`),
  INDEX `GroupsChirurgen` (`ID_Groups`),
  INDEX `ID_Chirurgen` (`ID_Chirurgen`),
  INDEX `ID_Groups` (`ID_Groups`),
  INDEX `ID_Titel` (`ID_ChirurgenFunktionen`),
  CONSTRAINT `ChirurgenFunktionenChirurgen` FOREIGN KEY `ChirurgenFunktionenChirurgen` (`ID_ChirurgenFunktionen`)
    REFERENCES `operationen`.`ChirurgenFunktionen` (`ID_ChirurgenFunktionen`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT,
  CONSTRAINT `GroupsChirurgen` FOREIGN KEY `GroupsChirurgen` (`ID_Groups`)
    REFERENCES `operationen`.`Groups` (`ID_Groups`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT
)
ENGINE = INNODB;

DROP TABLE IF EXISTS `operationen`.`ChirurgenDokumente`;
CREATE TABLE `operationen`.`ChirurgenDokumente` (
  `ID_ChirurgenDokumente` INT(10) NOT NULL AUTO_INCREMENT,
  `ID_Chirurgen` INT(10) NOT NULL,
  `ID_Dokumente` INT(10) NOT NULL,
  `Blob` LONGBLOB NOT NULL,
  `InBearbeitung` INT(10) NOT NULL,
  `Bearbeitungsdatum` DATETIME NOT NULL,
  PRIMARY KEY (`ID_ChirurgenDokumente`),
  INDEX `ChirurgenChirurgenDokumente` (`ID_Chirurgen`),
  INDEX `DokumenteChirurgenDokumente` (`ID_Dokumente`),
  INDEX `ID_Chirugen` (`ID_Chirurgen`),
  INDEX `ID_ChirugenLogbuch` (`ID_ChirurgenDokumente`),
  CONSTRAINT `ChirurgenChirurgenDokumente` FOREIGN KEY `ChirurgenChirurgenDokumente` (`ID_Chirurgen`)
    REFERENCES `operationen`.`Chirurgen` (`ID_Chirurgen`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT,
  CONSTRAINT `DokumenteChirurgenDokumente` FOREIGN KEY `DokumenteChirurgenDokumente` (`ID_Dokumente`)
    REFERENCES `operationen`.`Dokumente` (`ID_Dokumente`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT
)
ENGINE = INNODB;

DROP TABLE IF EXISTS `operationen`.`ChirurgenFunktionen`;
CREATE TABLE `operationen`.`ChirurgenFunktionen` (
  `ID_ChirurgenFunktionen` INT(10) NOT NULL AUTO_INCREMENT,
  `Funktion` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`ID_ChirurgenFunktionen`),
  INDEX `ID_ChirurgenFunktion` (`ID_ChirurgenFunktionen`)
)
ENGINE = INNODB;

DROP TABLE IF EXISTS `operationen`.`ChirurgenGebiete`;
CREATE TABLE `operationen`.`ChirurgenGebiete` (
  `ID_ChirurgenGebiete` INT(10) NOT NULL AUTO_INCREMENT,
  `ID_Chirurgen` INT(10) NOT NULL,
  `ID_Gebiete` INT(10) NOT NULL,
  `GebietVon` DATETIME NULL,
  `GebietBis` DATETIME NULL,
  PRIMARY KEY (`ID_ChirurgenGebiete`),
  UNIQUE INDEX `Index_E1B86977_2533_4F05` (`ID_ChirurgenGebiete`),
  INDEX `FKChirurgen` (`ID_Chirurgen`),
  INDEX `FKGebiete` (`ID_Gebiete`),
  CONSTRAINT `FKChirurgen` FOREIGN KEY `FKChirurgen` (`ID_Chirurgen`)
    REFERENCES `operationen`.`Chirurgen` (`ID_Chirurgen`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT,
  CONSTRAINT `FKGebiete` FOREIGN KEY `FKGebiete` (`ID_Gebiete`)
    REFERENCES `operationen`.`Gebiete` (`ID_Gebiete`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT
)
ENGINE = INNODB;

DROP TABLE IF EXISTS `operationen`.`ChirurgenOperationen`;
CREATE TABLE `operationen`.`ChirurgenOperationen` (
  `ID_ChirurgenOperationen` INT(10) NOT NULL AUTO_INCREMENT,
  `ID_Chirurgen` INT(10) NOT NULL,
  `ID_OPFunktionen` INT(10) NOT NULL,
  `ID_Richtlinien` INT(10) NULL,
  `Fallzahl` VARCHAR(50) NOT NULL,
  `OPS-Kode` VARCHAR(20) NOT NULL,
  `OPS-Text` VARCHAR(255) NOT NULL,
  `Datum` DATETIME NOT NULL,
  `Zeit` DATETIME NOT NULL,
  `ZeitBis` DATETIME NOT NULL,
  `Quelle` INT(10) NOT NULL,
  PRIMARY KEY (`ID_ChirurgenOperationen`),
  INDEX `ChirurgenChirurgenOperationen` (`ID_Chirurgen`),
  INDEX `ID_Chirurgen` (`ID_Chirurgen`),
  INDEX `ID_ChirurgenOperationen` (`ID_ChirurgenOperationen`),
  INDEX `ID_Richtlinien` (`ID_Richtlinien`),
  INDEX `OPFunktionenChirurgenOperationen` (`ID_OPFunktionen`),
  INDEX `RichtlinienChirurgenOperationen` (`ID_Richtlinien`),
  CONSTRAINT `ChirurgenChirurgenOperationen` FOREIGN KEY `ChirurgenChirurgenOperationen` (`ID_Chirurgen`)
    REFERENCES `operationen`.`Chirurgen` (`ID_Chirurgen`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT,
  CONSTRAINT `OPFunktionenChirurgenOperationen` FOREIGN KEY `OPFunktionenChirurgenOperationen` (`ID_OPFunktionen`)
    REFERENCES `operationen`.`OPFunktionen` (`ID_OPFunktionen`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT,
  CONSTRAINT `RichtlinienChirurgenOperationen` FOREIGN KEY `RichtlinienChirurgenOperationen` (`ID_Richtlinien`)
    REFERENCES `operationen`.`Richtlinien` (`ID_Richtlinien`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT
)
ENGINE = INNODB;

DROP TABLE IF EXISTS `operationen`.`Config`;
CREATE TABLE `operationen`.`Config` (
  `ID_Config` INT(10) NOT NULL AUTO_INCREMENT,
  `Key` VARCHAR(50) NOT NULL,
  `Value` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`ID_Config`),
  INDEX `ID_Config` (`ID_Config`)
)
ENGINE = INNODB;

DROP TABLE IF EXISTS `operationen`.`Dateien`;
CREATE TABLE `operationen`.`Dateien` (
  `ID_Dateien` INT(10) NOT NULL AUTO_INCREMENT,
  `ID_DateiTypen` INT(10) NOT NULL,
  `Dateiname` VARCHAR(100) NOT NULL,
  `Beschreibung` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`ID_Dateien`),
  UNIQUE INDEX `Index_7C059654_0C53_43D7` (`ID_Dateien`),
  INDEX `FKDateienDateiTypen` (`ID_DateiTypen`),
  CONSTRAINT `FKDateienDateiTypen` FOREIGN KEY `FKDateienDateiTypen` (`ID_DateiTypen`)
    REFERENCES `operationen`.`DateiTypen` (`ID_DateiTypen`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT
)
ENGINE = INNODB;

DROP TABLE IF EXISTS `operationen`.`DateiTypen`;
CREATE TABLE `operationen`.`DateiTypen` (
  `ID_DateiTypen` INT(10) NOT NULL AUTO_INCREMENT,
  `DateiTyp` VARCHAR(100) NULL,
  PRIMARY KEY (`ID_DateiTypen`),
  UNIQUE INDEX `Index_737FBF98_D887_4B6F` (`ID_DateiTypen`)
)
ENGINE = INNODB;

DROP TABLE IF EXISTS `operationen`.`Dokumente`;
CREATE TABLE `operationen`.`Dokumente` (
  `ID_Dokumente` INT(10) NOT NULL AUTO_INCREMENT,
  `Gruppe` VARCHAR(50) NOT NULL,
  `LfdNummer` INT(10) NOT NULL,
  `Beschreibung` VARCHAR(50) NOT NULL,
  `Dateiname` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`ID_Dokumente`),
  INDEX `ID_Dokumente` (`ID_Dokumente`),
  INDEX `LfdNummer` (`LfdNummer`)
)
ENGINE = INNODB;

DROP TABLE IF EXISTS `operationen`.`Gebiete`;
CREATE TABLE `operationen`.`Gebiete` (
  `ID_Gebiete` INT(10) NOT NULL AUTO_INCREMENT,
  `Gebiet` VARCHAR(255) NOT NULL,
  `Bemerkung` VARCHAR(255) NOT NULL,
  `Herkunft` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`ID_Gebiete`),
  INDEX `ID_Gebiet` (`ID_Gebiete`)
)
ENGINE = INNODB;

DROP TABLE IF EXISTS `operationen`.`Groups`;
CREATE TABLE `operationen`.`Groups` (
  `ID_Groups` INT(10) NOT NULL,
  `Name` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`ID_Groups`),
  INDEX `ID_Groups` (`ID_Groups`)
)
ENGINE = INNODB;

DROP TABLE IF EXISTS `operationen`.`ImportChirurgenExclude`;
CREATE TABLE `operationen`.`ImportChirurgenExclude` (
  `ID_ImportChirurgenExclude` INT(10) NOT NULL AUTO_INCREMENT,
  `Nachname` VARCHAR(50) NOT NULL,
  `Vorname` VARCHAR(50) NULL,
  PRIMARY KEY (`ID_ImportChirurgenExclude`),
  INDEX `ID_ImportChirurgenExclude` (`ID_ImportChirurgenExclude`)
)
ENGINE = INNODB;

DROP TABLE IF EXISTS `operationen`.`Kommentare`;
CREATE TABLE `operationen`.`Kommentare` (
  `ID_Kommentare` INT(10) NOT NULL AUTO_INCREMENT,
  `ID_Chirurgen_Von` INT(10) NOT NULL,
  `ID_Chirurgen_Fuer` INT(10) NOT NULL,
  `ID_ChirurgenFunktionen` INT(10) NOT NULL,
  `Datum` DATETIME NOT NULL,
  `AbschnittVon` DATETIME NOT NULL,
  `AbschnittBis` DATETIME NOT NULL,
  `KommentarVon` LONGTEXT NOT NULL,
  `KommentarFuer` LONGTEXT NOT NULL,
  `Status` INT(10) NOT NULL,
  PRIMARY KEY (`ID_Kommentare`),
  INDEX `ChirurgenFunktionenKommentare` (`ID_ChirurgenFunktionen`),
  INDEX `ChirurgenKommentare` (`ID_Chirurgen_Von`),
  INDEX `ChirurgenKommentare1` (`ID_Chirurgen_Fuer`),
  INDEX `ID_Chirurgen_Fuer` (`ID_Chirurgen_Fuer`),
  INDEX `ID_Chirurgen_Von` (`ID_Chirurgen_Von`),
  INDEX `ID_ChirurgenFunktion` (`ID_ChirurgenFunktionen`),
  INDEX `ID_Kommentare` (`ID_Kommentare`),
  CONSTRAINT `ChirurgenFunktionenKommentare` FOREIGN KEY `ChirurgenFunktionenKommentare` (`ID_ChirurgenFunktionen`)
    REFERENCES `operationen`.`ChirurgenFunktionen` (`ID_ChirurgenFunktionen`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT,
  CONSTRAINT `ChirurgenKommentare` FOREIGN KEY `ChirurgenKommentare` (`ID_Chirurgen_Von`)
    REFERENCES `operationen`.`Chirurgen` (`ID_Chirurgen`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT,
  CONSTRAINT `ChirurgenKommentare1` FOREIGN KEY `ChirurgenKommentare1` (`ID_Chirurgen_Fuer`)
    REFERENCES `operationen`.`Chirurgen` (`ID_Chirurgen`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT
)
ENGINE = INNODB;

DROP TABLE IF EXISTS `operationen`.`LogTable`;
CREATE TABLE `operationen`.`LogTable` (
  `ID_LogTable` INT(10) NOT NULL AUTO_INCREMENT,
  `Timestamp` DATETIME NOT NULL,
  `User` VARCHAR(50) NOT NULL,
  `Action` VARCHAR(20) NOT NULL,
  `Message` VARCHAR(250) NOT NULL,
  PRIMARY KEY (`ID_LogTable`),
  INDEX `ID_LogTable` (`ID_LogTable`)
)
ENGINE = INNODB;

DROP TABLE IF EXISTS `operationen`.`Notizen`;
CREATE TABLE `operationen`.`Notizen` (
  `ID_Notizen` INT(10) NOT NULL AUTO_INCREMENT,
  `ID_Chirurgen` INT(10) NOT NULL,
  `ID_NotizTypen` INT(10) NOT NULL,
  `Datum` DATETIME NOT NULL,
  `Ende` DATETIME NULL,
  `Notiz` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`ID_Notizen`),
  UNIQUE INDEX `Index_8464120F_B479_4D3F` (`ID_Notizen`),
  INDEX `FKNotizenChirurgen` (`ID_Chirurgen`),
  INDEX `FKNotizenNotizTypen` (`ID_NotizTypen`),
  CONSTRAINT `FKNotizenChirurgen` FOREIGN KEY `FKNotizenChirurgen` (`ID_Chirurgen`)
    REFERENCES `operationen`.`Chirurgen` (`ID_Chirurgen`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT,
  CONSTRAINT `FKNotizenNotizTypen` FOREIGN KEY `FKNotizenNotizTypen` (`ID_NotizTypen`)
    REFERENCES `operationen`.`NotizTypen` (`ID_NotizTypen`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT
)
ENGINE = INNODB;

DROP TABLE IF EXISTS `operationen`.`NotizTypen`;
CREATE TABLE `operationen`.`NotizTypen` (
  `ID_NotizTypen` INT(10) NOT NULL AUTO_INCREMENT,
  `Text` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`ID_NotizTypen`),
  UNIQUE INDEX `Index_F9285155_C27D_4EFD` (`ID_NotizTypen`)
)
ENGINE = INNODB;

DROP TABLE IF EXISTS `operationen`.`Operationen`;
CREATE TABLE `operationen`.`Operationen` (
  `ID_Operationen` INT(10) NOT NULL AUTO_INCREMENT,
  `OPS-Kode` VARCHAR(20) NOT NULL,
  `OPS-Text` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`ID_Operationen`),
  INDEX `Index_OPSKode`(`OPS-Kode`)
)
ENGINE = INNODB;

DROP TABLE IF EXISTS `operationen`.`OPFunktionen`;
CREATE TABLE `operationen`.`OPFunktionen` (
  `ID_OPFunktionen` INT(10) NOT NULL,
  `LfdNr` INT(10) NOT NULL,
  `Beschreibung` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`ID_OPFunktionen`),
  INDEX `ID_OPFunktionen` (`ID_OPFunktionen`)
)
ENGINE = INNODB;

DROP TABLE IF EXISTS `operationen`.`PlanOperationen`;
CREATE TABLE `operationen`.`PlanOperationen` (
  `ID_PlanOperationen` INT(10) NOT NULL AUTO_INCREMENT,
  `ID_Chirurgen` INT(10) NOT NULL,
  `Operation` VARCHAR(50) NOT NULL,
  `Anzahl` INT(10) NOT NULL,
  `DatumVon` DATETIME NOT NULL,
  `DatumBis` DATETIME NOT NULL,
  PRIMARY KEY (`ID_PlanOperationen`),
  INDEX `ChirurgenPlanOperationen` (`ID_Chirurgen`),
  INDEX `ID_Chirurgen` (`ID_Chirurgen`),
  INDEX `ID_Operationen` (`Operation`),
  INDEX `ID_PlanOperationen` (`ID_PlanOperationen`),
  CONSTRAINT `ChirurgenPlanOperationen` FOREIGN KEY `ChirurgenPlanOperationen` (`ID_Chirurgen`)
    REFERENCES `operationen`.`Chirurgen` (`ID_Chirurgen`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT
)
ENGINE = INNODB;

DROP TABLE IF EXISTS `operationen`.`Richtlinien`;
CREATE TABLE `operationen`.`Richtlinien` (
  `ID_Richtlinien` INT(10) NOT NULL AUTO_INCREMENT,
  `ID_Gebiete` INT(10) NOT NULL,
  `LfdNummer` INT(10) NOT NULL,
  `UntBehMethode` LONGTEXT NOT NULL,
  `Richtzahl` INT(10) NOT NULL,
  PRIMARY KEY (`ID_Richtlinien`),
  UNIQUE INDEX `ID_Richtlinie` (`ID_Richtlinien`),
  INDEX `GebieteRichtlinien` (`ID_Gebiete`),
  INDEX `ID_` (`ID_Gebiete`),
  INDEX `LfdNummer` (`LfdNummer`),
  CONSTRAINT `GebieteRichtlinien` FOREIGN KEY `GebieteRichtlinien` (`ID_Gebiete`)
    REFERENCES `operationen`.`Gebiete` (`ID_Gebiete`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT
)
ENGINE = INNODB;

DROP TABLE IF EXISTS `operationen`.`RichtlinienOpsKodes`;
CREATE TABLE `operationen`.`RichtlinienOpsKodes` (
  `ID_RichtlinienOpsKodes` INT(10) NOT NULL AUTO_INCREMENT,
  `ID_Richtlinien` INT(10) NOT NULL,
  `OPS-Kode` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`ID_RichtlinienOpsKodes`),
  INDEX `Index_OPSKode`(`OPS-Kode`),
  INDEX `RichtlinienRichtlinienOpsKode` (`ID_Richtlinien`),
  CONSTRAINT `RichtlinienRichtlinienOpsKode` FOREIGN KEY `RichtlinienRichtlinienOpsKode` (`ID_Richtlinien`)
    REFERENCES `operationen`.`Richtlinien` (`ID_Richtlinien`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT
)
ENGINE = INNODB;


SET FOREIGN_KEY_CHECKS = 1;

/*
21.12.2007 Version 1.14
*/
DROP TABLE IF EXISTS `operationen`.`SerialNumbers`;
CREATE TABLE `operationen`.`SerialNumbers` (
  `ID_SerialNumbers` INT(10) NOT NULL AUTO_INCREMENT,
  `SerialNumber` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`ID_SerialNumbers`),
  UNIQUE INDEX `Idx_SerialNumber`(`SerialNumber`)
)
ENGINE = InnoDB;

/*
	1.16 17.02.2008
*/
DROP TABLE IF EXISTS `operationen`.`ChirurgenRichtlinien`;
CREATE TABLE `operationen`.`ChirurgenRichtlinien` (
  `ID_ChirurgenRichtlinien` INT(10) NOT NULL AUTO_INCREMENT,
  `ID_Chirurgen` INT(10) NOT NULL,
  `ID_Richtlinien` INT(10) NULL,
  `Ort` VARCHAR(255) NOT NULL,
  `Datum` DATETIME NOT NULL,
  `Anzahl` INT(10) NOT NULL,
  PRIMARY KEY (`ID_ChirurgenRichtlinien`),
  INDEX `ID_ChirurgenRichtlinien` (`ID_ChirurgenRichtlinien`),
  INDEX `ID_Chirurgen` (`ID_Chirurgen`),
  INDEX `ID_Richtlinien` (`ID_Richtlinien`),
  INDEX `ChirurgenChirurgenRichtlinien` (`ID_Chirurgen`),
  INDEX `RichtlinienChirurgenRichtlinien` (`ID_Richtlinien`),
  CONSTRAINT `ChirurgenChirurgenRichtlinien` FOREIGN KEY `ChirurgenChirurgenRichtlinien` (`ID_Chirurgen`)
    REFERENCES `operationen`.`Chirurgen` (`ID_Chirurgen`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT,
  CONSTRAINT `RichtlinienChirurgenRichtlinien` FOREIGN KEY `RichtlinienChirurgenRichtlinien` (`ID_Richtlinien`)
    REFERENCES `operationen`.`Richtlinien` (`ID_Richtlinien`)
    ON DELETE RESTRICT
    ON UPDATE RESTRICT
)
ENGINE = INNODB;

