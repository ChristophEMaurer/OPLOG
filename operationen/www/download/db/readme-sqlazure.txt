Zum Installieren der SQLAzure Datenbank:

1. Informieren Sie sich über Windows Azure

2. Unter Windows Azure eine Datenbank "operationen" anlegen.

3. die Skripte ausführen.

4. in operationen.exe.config diese Einträge verwenden und den connection string anpassen, siehe hierzu http://www.connectionstrings.com:

<setting name="ConnectionString" serializeAs="String">
	<value>Server=tcp:iwsb135s1b.database.windows.net,1433;Database=operationen;User ID=cmaurer@iwsb135s1b;Password=***;Trusted_Connection=False;Encrypt=True;</value>;
</setting>

<setting name="DatabaseType" serializeAs="String">
	<value>SQLServer</value>
</setting>


Im Internet:

http://www.microsoft.com/germany/net/WindowsAzure/Starten.aspx
http://silverlight.microsoft.de/azure_howto_portal/01_Neue_Anwendung/Entwicklung%20einer%20ASP.NET-basierten%20Cloud-Anwendung.pdf
http://www.microsoft.com/windowsazure

