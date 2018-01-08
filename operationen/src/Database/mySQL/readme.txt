root/cmaurer
operationen/operationen123

- operationen.mdb ist meistens die Datenbank, aus der das letzte Datenskript generiert wurde
- operationen.mdb kopieren
- Kennwort entfernen
- Systemtabelle sichtbar machen:
	Extras > Optionen > Ansicht : Systemobjekte
	Extras > Sicherheit > Benutzer- und Gruppenberechtigungen
	mSysRelationships: Verwalten, oder so alles einschalten

- MySql Migration Toolkit starten und durchklicken
- Das erzeugte Datenbank-Schemaskript ist schlecht: alle Felder sind nullable.
- Die mySQL-Datenbank mit der Access-Datenbank vergleichen und durchklicken.


Zum Installieren der Datenbank:
1. Eine leere Datenbank "operationen" anlegen, hier bei alle Einstellungen wie gewünscht wählen, auch
den Ort, wo die Dateien liegen.

2. die Skripte laufen lassen.


22.05.2008
Änderungen:
Skript editieren, weil die Oberfläche zu schlecht ist.
Daten auch in Skript einfügen.

26.10.2014
Aktuelle MySQL 5.6 instlliert, und Syntax angepasst:
set SQL_SAFE_UPDATES = 0;

