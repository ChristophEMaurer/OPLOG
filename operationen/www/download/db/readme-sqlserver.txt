Zum Installieren der SQLServer Datenbank:

1. Eine leere Datenbank "operationen" anlegen, hierbei alle Einstellungen wie gew�nscht w�hlen, auch
den Ort, wo die Dateien liegen.

2. die Skripte ausf�hren.

3. in operationen.exe.config diese Eintr�ge verwenden und den connection string anpassen, siehe hierzu http://www.connectionstrings.com:

<setting name="ConnectionString" serializeAs="String">
	<value>Trusted_Connection=Yes;Data Source=localhost\SQLExpress;Initial catalog=Operationen</value>
</setting>

<setting name="DatabaseType" serializeAs="String">
	<value>SQLServer</value>
</setting>



