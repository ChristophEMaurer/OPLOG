Dieses Verzeichnis nicht verschieben oder umbenennen, auf diese Dateien wird verwiesen.

OpLogbuchLicense.snk
Wird nicht benutzt?


OpLogbuch.snk
Wird verwendet zum Signieren der Assemblies. Wurde mit sn.exe hergestellt. AUs diesem kann mit mit 
'XMLSignTool.exe extract public' nur den public key herausholen


Public/Private Keys für Webservice
Diese wurden generiert mit 'XmlSignTool.exe createxml 4096',
es gibt dafür kein *.snk
OperationenSerialPrivateKey.xml = www/App_Data/pr.txt
OperationenSerialPublicKey.xml


Public/Private Keys für License Datei
Diese wurden generiert mit 'XmlSignTool.exe createxml 4096',
es gibt dafür kein *.snk
OperationenLicensePrivateKey.xml = www/App_Data/pr_trial.txt
OperationenLicensePublicKey.xml

Mit 'XMLSignTool.exe extract public' wurde der public key herausgeholt.
Auf dem 1und1 webserver kann man keinen key container benutzen, also muss man diese neu machen
mit 'XmlSignTool.exe createxml 4096'.
Außerdem muss XmlSignTool.exe dann mit einem private key aus einer Datei signieren können.




