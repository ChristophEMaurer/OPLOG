rem
rem            %0               %1                  %2                               %3
rem CreateDeployFolder.cmd [bdc | nobdc] [chirurgie | urologie] [TargetPlatform: x86 | anycpu]
rem

rem AnyCPU is one word: Which Idiot added that space in TargetPlatform=Any CPU???

setlocal

set OP_RET=1

if "%3"=="" (
  goto _usage
)
if not "%4"=="" (
  goto _usage
)

if "%OP_HOME_DRIVE%"=="" (
  echo OP_HOME_DRIVE is not set
  goto _exit
)
if "%OP_ROOT_DIR%"=="" (
  echo OP_ROOT_DIR is not set
  goto _exit
)
if "%OP_MYSQLDIR%"=="" (
  echo OP_MYSQLDIR is not set
  goto _exit
)

rem @echo Running %0

set OP_DEPLOY="Deploy-%1-%2-%3"

%OP_HOME_DRIVE%
cd %OP_ROOT_DIR%\Operationen\src\Setup\Versionen

rem @echo Creating Deployment folder %OP_DEPLOY%

rd %OP_DEPLOY% /S /Q

rem Verzeichnisse erzeugen
md %OP_DEPLOY%
md %OP_DEPLOY%\program
md %OP_DEPLOY%\program\files
md %OP_DEPLOY%\program\files\en-US
md %OP_DEPLOY%\program\files\de-DE
md %OP_DEPLOY%\program\files\Dokumente
md %OP_DEPLOY%\program\files\Edit
md %OP_DEPLOY%\program\files\Logfiles
md %OP_DEPLOY%\program\files\Plugins

rem Dateien, die zur Anwendung gehören
@call :copyfile "%OP_MYSQLDIR%\MySql.Data.dll" %OP_DEPLOY%\program\files\
@if errorlevel 1 goto _exit

rem
rem Elegant UI
rem
@call :copyfile %OP_ELEGANT_UI_DIR%\de-DE\Elegant.Ui.Common.resources.dll 			%OP_DEPLOY%\program\files\de-DE\
@if errorlevel 1 goto _exit

@call :copyfile %OP_ELEGANT_UI_DIR%\de-DE\Elegant.Ui.Ribbon.resources.dll 			%OP_DEPLOY%\program\files\de-DE\
@if errorlevel 1 goto _exit

@call :copyfile "%OP_ELEGANT_UI_DIR%\Elegant.Ui.Common.dll" %OP_DEPLOY%\program\files\
@if errorlevel 1 goto _exit

@call :copyfile "%OP_ELEGANT_UI_DIR%\Elegant.Ui.Common.Theme.Office2007Blue.dll" %OP_DEPLOY%\program\files\
@if errorlevel 1 goto _exit

@call :copyfile "%OP_ELEGANT_UI_DIR%\Elegant.Ui.Ribbon.dll" %OP_DEPLOY%\program\files\
@if errorlevel 1 goto _exit

@call :copyfile "%OP_ELEGANT_UI_DIR%\Elegant.Ui.Ribbon.Theme.Office2007Blue.dll" %OP_DEPLOY%\program\files\
@if errorlevel 1 goto _exit

@call :copyfile ..\..\bin\release\en-US\Operationen.resources.dll 	%OP_DEPLOY%\program\files\en-US\
@if errorlevel 1 goto _exit

@call :copyfile ..\..\bin\release\Operationen.exe 			%OP_DEPLOY%\program\files\
@if errorlevel 1 goto _exit

@call :copyfile ..\..\OperationenAppFramework\bin\release\CMaurer.Operationen.AppFramework.dll 	%OP_DEPLOY%\program\files\
@if errorlevel 1 goto _exit

@call :copyfile ..\..\bin\release\AppFramework.dll 			%OP_DEPLOY%\program\files\
@if errorlevel 1 goto _exit

@call :copyfile ..\..\bin\release\Operationen.OperationenImport.dll	%OP_DEPLOY%\program\files\
@if errorlevel 1 goto _exit

@call :copyfile ..\..\bin\release\Utility.dll 				%OP_DEPLOY%\program\files\
@if errorlevel 1 goto _exit

@call :copyfile ..\..\bin\release\Windows.Forms.dll 			%OP_DEPLOY%\program\files\
@if errorlevel 1 goto _exit

@call :copyfile ..\..\bin\release\Sekurity.dll 				%OP_DEPLOY%\program\files\
@if errorlevel 1 goto _exit

@call :copyfile ..\..\OperationenImportCSV\bin\release\Operationen.OperationenImportCSV.dll %OP_DEPLOY%\program\files\Plugins\
@if errorlevel 1 goto _exit

@call :copyfile ..\..\OperationenImportCSVUnicode\bin\release\Operationen.OperationenImportCSVUnicode.dll %OP_DEPLOY%\program\files\Plugins\
@if errorlevel 1 goto _exit

@call :copyfile ..\..\OperationenImportIcpm5Op3CSV\bin\release\Operationen.OperationenImportIcpm5Op3CSV.dll %OP_DEPLOY%\program\files\Plugins\
@if errorlevel 1 goto _exit

@call :copyfile ..\..\OperationenImportIcpm5Op3CSVUnicode\bin\release\Operationen.OperationenImportIcpm5Op3CSVUnicode.dll %OP_DEPLOY%\program\files\Plugins\
@if errorlevel 1 goto _exit

@call :copyfile ..\..\OperationenImportIcpm3Op3CSV\bin\release\Operationen.OperationenImportIcpm3Op3CSV.dll %OP_DEPLOY%\program\files\Plugins\
@if errorlevel 1 goto _exit

@call :copyfile ..\..\OperationenImportIcpm3Op3CSVUnicode\bin\release\Operationen.OperationenImportIcpm3Op3CSVUnicode.dll %OP_DEPLOY%\program\files\Plugins\
@if errorlevel 1 goto _exit

@call :copyfile ..\..\OperationenImportMccIsop\bin\release\Operationen.OperationenImportMccIsop.dll %OP_DEPLOY%\program\files\Plugins\
@if errorlevel 1 goto _exit

@call :copyfile ..\..\OperationenImportMccIsopUnicode\bin\release\Operationen.OperationenImportMccIsopUnicode.dll %OP_DEPLOY%\program\files\Plugins\
@if errorlevel 1 goto _exit

@call :copyfile ..\..\OperationenImportOrbis\bin\release\Operationen.OperationenImportOrbis.dll %OP_DEPLOY%\program\files\Plugins\
@if errorlevel 1 goto _exit

@call :copyfile ..\..\OperationenImportOrbisUnicode\bin\release\Operationen.OperationenImportOrbisUnicode.dll %OP_DEPLOY%\program\files\Plugins\
@if errorlevel 1 goto _exit

@call :copyfile ..\..\OperationenImportOrbisText\bin\release\Operationen.OperationenImportOrbisText.dll %OP_DEPLOY%\program\files\Plugins\
@if errorlevel 1 goto _exit

@call :copyfile ..\..\OperationenImportOrbisTextUnicode\bin\release\Operationen.OperationenImportOrbisTextUnicode.dll %OP_DEPLOY%\program\files\Plugins\
@if errorlevel 1 goto _exit

@call :copyfile ..\..\OperationenImportIKPM10\bin\release\Operationen.OperationenImportIKPM10.dll %OP_DEPLOY%\program\files\Plugins\
@if errorlevel 1 goto _exit

@call :copyfile ..\..\OperationenImportIKPM10Unicode\bin\release\Operationen.OperationenImportIKPM10Unicode.dll %OP_DEPLOY%\program\files\Plugins\
@if errorlevel 1 goto _exit

@call :copyfile ..\..\OperationenImportSapCsv\bin\release\Operationen.OperationenImportSapCsv.dll %OP_DEPLOY%\program\files\Plugins\
@if errorlevel 1 goto _exit

@call :copyfile ..\..\OperationenImportSapCsvUnicode\bin\release\Operationen.OperationenImportSapCsvUnicode.dll %OP_DEPLOY%\program\files\Plugins\
@if errorlevel 1 goto _exit

rem Dateien, die das Uninstall benötigt
@call :copyfile ..\..\Uninstall\bin\release\Uninstall.exe 		%OP_DEPLOY%\program\files\
@if errorlevel 1 goto _exit

@call :copyfile ..\..\Uninstall\bin\release\SetupData.dll 		%OP_DEPLOY%\program\files\
@if errorlevel 1 goto _exit

@call :copyfile ..\..\Uninstall\bin\release\Interop.IWshRuntimeLibrary.dll 	%OP_DEPLOY%\program\files\
@if errorlevel 1 goto _exit

rem Hilfedatei
rem Diese muss separat mit HTML Helpworkshop erzeugt werden
@call :copyfile ..\..\..\Dokumentation\Help\operationen.chm		%OP_DEPLOY%\program\files\operationen.chm
@if errorlevel 1 goto _exit

rem Dateien, die das Setup selber benötigt
@call :copyfile ..\..\SetupLauncher\Release\SetupLauncher.exe	%OP_DEPLOY%\program\
@if errorlevel 1 goto _exit

@call :copyfile ..\bin\release\AppFramework.dll 		%OP_DEPLOY%\program\
@if errorlevel 1 goto _exit

@call :copyfile ..\bin\release\OPSetup.exe 			%OP_DEPLOY%\program\
@if errorlevel 1 goto _exit

@call :copyfile ..\bin\release\Utility.dll 			%OP_DEPLOY%\program\
@if errorlevel 1 goto _exit

@call :copyfile ..\bin\release\Windows.Forms.dll		%OP_DEPLOY%\program\
@if errorlevel 1 goto _exit

@call :copyfile ..\bin\release\Interop.IWshRuntimeLibrary.dll 	%OP_DEPLOY%\program\
@if errorlevel 1 goto _exit

@call :copyfile ..\bin\release\SetupData.dll 			%OP_DEPLOY%\program\
@if errorlevel 1 goto _exit

rem Lizenzdatei: Für das Setup und die Anwendung
@call :copyfile ..\SetupFiles\License.txt 			%OP_DEPLOY%\program\
@if errorlevel 1 goto _exit

@call :copyfile ..\SetupFiles\License.txt 			%OP_DEPLOY%\program\files\
@if errorlevel 1 goto _exit

@call :copyfile ..\SetupFiles\changelog.txt 			%OP_DEPLOY%\program\files\
@if errorlevel 1 goto _exit

rem
rem Fuer verschiedene Facharztgebiete unterscheidet sich nur die Datenbank
rem
@call :copyfile ..\SetupFiles\operationen-%2.mdb 		%OP_DEPLOY%\program\files\operationen.mdb
@if errorlevel 1 goto _exit

@call :copyfile ..\SetupFiles\chirurg.mdb 		%OP_DEPLOY%\program\files\
@if errorlevel 1 goto _exit

@call :copyfile ..\SetupFiles\Operationen.exe.config 	%OP_DEPLOY%\program\files\
@if errorlevel 1 goto _exit

@call :copyfile ..\SetupFiles\addtrust.bat 		%OP_DEPLOY%\program\files\
@if errorlevel 1 goto _exit

@call :copyfile ..\SetupFiles\2014.oplog-V2.oplog 	%OP_DEPLOY%\program\files\
@if errorlevel 1 goto _exit

rem 
rem SDK
rem
md %OP_DEPLOY%\sdk

@call :copyfile ..\..\OperationenImport\Help\Help\plugins.chm %OP_DEPLOY%\sdk\plugins.chm
@if errorlevel 1 goto _exit

rem
rem OperationenImport
rem
@call :copyPluginSdk ..\..\OperationenImport %OP_DEPLOY%\sdk\OperationenImport
@if errorlevel 1 goto _exit

rem
rem OperationenImportCSV
rem
@call :copyPluginSdk ..\..\OperationenImportCSV %OP_DEPLOY%\sdk\OperationenImportCSV
@if errorlevel 1 goto _exit

rem
rem OperationenImportCSVUnicode
rem
@call :copyPluginSdk ..\..\OperationenImportCSVUnicode %OP_DEPLOY%\sdk\OperationenImportCSVUnicode
@if errorlevel 1 goto _exit

rem
rem OperationenImportIcpm3Op3CSV
rem
@call :copyPluginSdk ..\..\OperationenImportIcpm3Op3CSV %OP_DEPLOY%\sdk\OperationenImportIcpm3Op3CSV
@if errorlevel 1 goto _exit

rem
rem OperationenImportIcpm3Op3CSVUnicode
rem
@call :copyPluginSdk ..\..\OperationenImportIcpm3Op3CSVUnicode %OP_DEPLOY%\sdk\OperationenImportIcpm3Op3CSVUnicode
@if errorlevel 1 goto _exit

rem
rem OperationenImportIcpm5Op3CSV
rem
@call :copyPluginSdk ..\..\OperationenImportIcpm5Op3CSV %OP_DEPLOY%\sdk\OperationenImportIcpm5Op3CSV
@if errorlevel 1 goto _exit

rem
rem OperationenImportIcpm5Op3CSVUnicode
rem
@call :copyPluginSdk ..\..\OperationenImportIcpm5Op3CSVUnicode %OP_DEPLOY%\sdk\OperationenImportIcpm5Op3CSVUnicode
@if errorlevel 1 goto _exit

rem
rem OperationenImportIKPM10
rem
@call :copyPluginSdk ..\..\OperationenImportIKPM10 %OP_DEPLOY%\sdk\OperationenImportIKPM10
@if errorlevel 1 goto _exit

rem
rem OperationenImportIKPM10Unicode
rem
@call :copyPluginSdk ..\..\OperationenImportIKPM10Unicode %OP_DEPLOY%\sdk\OperationenImportIKPM10Unicode
@if errorlevel 1 goto _exit

rem
rem OperationenImportMccIsop
rem
@call :copyPluginSdk ..\..\OperationenImportMccIsop %OP_DEPLOY%\sdk\OperationenImportMccIsop
@if errorlevel 1 goto _exit

rem
rem OperationenImportMccIsopUnicode
rem
@call :copyPluginSdk ..\..\OperationenImportMccIsopUnicode %OP_DEPLOY%\sdk\OperationenImportMccIsopUnicode
@if errorlevel 1 goto _exit

rem
rem OperationenImportOrbis
rem
@call :copyPluginSdk ..\..\OperationenImportOrbis %OP_DEPLOY%\sdk\OperationenImportOrbis
@if errorlevel 1 goto _exit

rem
rem OperationenImportOrbisUnicode
rem
@call :copyPluginSdk ..\..\OperationenImportOrbisUnicode %OP_DEPLOY%\sdk\OperationenImportOrbisUnicode
@if errorlevel 1 goto _exit

rem
rem OperationenImportOrbisText
rem
@call :copyPluginSdk ..\..\OperationenImportOrbisText %OP_DEPLOY%\sdk\OperationenImportOrbisText
@if errorlevel 1 goto _exit

rem
rem OperationenImportOrbisTextUnicode
rem
@call :copyPluginSdk ..\..\OperationenImportOrbisTextUnicode %OP_DEPLOY%\sdk\OperationenImportOrbisTextUnicode
@if errorlevel 1 goto _exit

rem
rem OperationenImportSapCsv
rem
@call :copyPluginSdk ..\..\OperationenImportSapCsv %OP_DEPLOY%\sdk\OperationenImportSapCsv
@if errorlevel 1 goto _exit

rem
rem OperationenImportSapCsvUnicode
rem
@call :copyPluginSdk ..\..\OperationenImportSapCsvUnicode %OP_DEPLOY%\sdk\OperationenImportSapCsvUnicode
@if errorlevel 1 goto _exit


rem
rem OperationenImportSqlServer
rem NICHT *Sdk kopieren
rem
@call :copyPlugin ..\..\OperationenImportSqlServer %OP_DEPLOY%\sdk\OperationenImportSqlServer
@if errorlevel 1 goto _exit

if "%1"=="bdc" (
  rem
  rem BDC
  rem
  @call :copyfile ..\..\bin\release\Operationen.BDC.dll 	%OP_DEPLOY%\program\files\
  @if errorlevel 1 goto _exit
)

rem
rem Gemeinsame Solution
rem
xcopy /fivry ..\..\OperationenImportSdk.sln	%OP_DEPLOY%\sdk\ >> %OP_BUILD_LOG%
if not "%errorlevel%"=="0" (
  echo Failed in %0: xcopy /fivry ..\..\OperationenImportSdk.sln	%OP_DEPLOY%\sdk\
  goto _exit
)

SET OP_RET=0

goto _exit

:copyPlugin
xcopy /fivry %1\*.cs %2\ >> %OP_BUILD_LOG%
if not "%errorlevel%"=="0" (
  echo Failed in %0: xcopy /fivry %1\*.cs %2\
  @exit /B 1
)
xcopy /fivry %1\*.csproj %2\ >> %OP_BUILD_LOG%
if not "%errorlevel%"=="0" (
  echo Failed in %0: xcopy /fivry %1\*.csproj %2\
  @exit /B 1
)
@exit /B 0

:copyPluginSdk
xcopy /fivry %1\*.cs %2\ >> %OP_BUILD_LOG%
if not "%errorlevel%"=="0" (
  echo Failed in %0: xcopy /fivry %1\*.cs %2\
  @exit /B 1
)
xcopy /fivry %1\*Sdk.csproj %2\ >> %OP_BUILD_LOG%
if not "%errorlevel%"=="0" (
  echo Failed in %0: xcopy /fivry %1\*Sdk.csproj %2\
  @exit /B 1
)
@exit /B 0

copy /Y %1 %2 >> %OP_BUILD_LOG%
if not "%errorlevel%"=="0" (
  echo Failed in %0 to copy file %1 to %2
  @exit /B 1
)
@exit /B 0

:copyfile
copy /Y %1 %2 >> %OP_BUILD_LOG%
if not "%errorlevel%"=="0" (
  echo Failed in %0 to copy file %1 to %2
  @exit /B 1
)
@exit /B 0

:_usage
echo Usage: %0 [bdc ^| nobdc] [chirurgie ^| gynaekologie] [TargetPlatform]
goto _exit

:_exit
rem @echo Exiting %0 with return code %OP_RET%
exit /B %OP_RET%

