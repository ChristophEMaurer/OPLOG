


13.11.2007 CMaurer
SQLServer alles wie 1.12.
Alle Operateionen, Richtlinien und Zuordnungen aus�er Kinder/Herz gef�llt ohne Mustermann

Tabellen:
Alle Tabellen sollen leer sein au�er:
AkademischeAusbildungTypen
ChirurgenFunktionen
Config (1.12)
DateiTypen (Logbuch, Weiterbildung, Curriculum)
Gebiete 6 + Allgemeinchirurgie
Groups (1 Admin, 2 Benutzer)
Notiztypen (wie Access)
KlinischeErgebnisse
Operationen: importieren!
OPFunktionen (1 Operation, 2 1. Assistent)
Richtlinien: importieren
RichtlinienOPSCodes importieren
Mustermann importieren


SQLServer
Die Datenbank operationen entspricht der Version 1.12
Alle Tabellen manuell �berpr�ft

*Create erzeugen aus SQL-Express:
Auf dB rechts klicken, dann
- Tasks - generate scripts usw


Datenbank Scheme und Daten generieren mit "Microsoft SQL Server Database Publishing Wizard"
- Source Server: ConnectionString aus operationen.exe.config f�r SQLServer nehmen: 
Trusted_Connection=Yes;Data Source=localhost\SQLExpress;Initial catalog=Operationen
- Operationen ausw�hlen, Script all objects
- die generierte Datei operationen-publish enth�lt schema und daten.

Fr�her
Alle Tabellen wurden manuell angelegt.

22.05.2008
�nderungen:
Alte Skripte f�r Schema und Daten laufen lassen.
Datenbank�nderungen manuell durchf�hren.
Neue Daten einf�gen aus Programm oder manuell.
Danach mit "Microsoft SQL Server Database Publishing Wizard"
neue Skrpite getrennt erstellen.
