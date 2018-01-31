Zum Installieren der MySQL-Datenbank (MySQL 5.6.21):


1. Eine leere Datenbank "operationen" anlegen, hierbei alle Einstellungen wie gewünscht wählen, auch
den Ort, wo die Dateien liegen.

2. die Skripte ausführen.

3. in operationen.exe.config diese Einträge verwenden und den connection string anpassen, siehe hierzu http://www.connectionstrings.com:

<setting name="ConnectionString" serializeAs="String">
	<value>Server=localhost;Database=Operationen;Uid=...;Pwd=...;</value>
</setting>

<setting name="DatabaseType" serializeAs="String">
	<value>MySQL</value>
</setting>



