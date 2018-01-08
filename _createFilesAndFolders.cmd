mkdir .\operationen\src\bin
mkdir .\operationen\src\bin\Debug
mkdir .\operationen\src\bin\Debug\plugins
mkdir .\operationen\src\bin\Debug\de-DE
mkdir .\operationen\src\bin\Release
mkdir .\operationen\src\bin\Release\plugins
mkdir .\operationen\src\bin\Release\de-DE

mkdir .\operationen\www
mkdir .\operationen\www\download
mkdir .\operationen\www\download\db
mkdir .\operationen\www\download\sdk

copy .\operationen\src\Setup\SetupFiles\ElegantRibbon\* .\operationen\src\bin\Debug\.
copy .\operationen\src\Setup\SetupFiles\ElegantRibbon\* .\operationen\src\bin\Release\.
copy .\operationen\src\Setup\SetupFiles\ElegantRibbon\de-DE\* .\operationen\src\bin\Debug\de-DE
copy .\operationen\src\Setup\SetupFiles\ElegantRibbon\de-DE\* .\operationen\src\bin\Release\de-DE

pause
