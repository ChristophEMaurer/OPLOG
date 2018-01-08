setlocal

for /F %%f in (version.txt) do set OP_VERSION_NEW=%%f

rem
rem          %1             %2                  %3                               %4
rem %0 [help | nohelp] [bdc | nobdc] [chirurgie | urologie | gynaekologie]  [x86 | anycpu | x64]
rem

if "%4"=="" (
  goto _usage
)
if not "%5"=="" (
  goto _usage
)

if not "%3"=="chirurgie" (
  if not "%3"=="urologie" (
    if not "%3"=="gynaekologie" (
      goto _usage
    )
  )
)

if not "%4"=="x86" (
  if not "%4"=="anycpu" (
    if not "%4"=="x64" (
      goto _usage
    )
  )
)

if "%OP_DOTNET20SDKDIR%"=="" (
  echo Error: OP_DOTNET20SDKDIR is not set!
  goto _exit
)
if "%OP_HOME_DRIVE%"=="" (
  echo Error: OP_HOME_DRIVE is not set!
  goto _exit
)
if "%OP_ROOT_DIR%"=="" (
  echo Error: OP_ROOT_DIR is not set!
  goto _exit
)

rem @echo Running %0

set OP_RET=1
set OP_PROGRAM_TITLE=OP-LOG
set OP_TARGETPLATFORM=%4
set OP_TRUNKNAME=OP-LOG-V%OP_VERSION_NEW%
rem OP_DEPLOY wird von CreateDeployFolder.cmd überschrieben
set OP_DEPLOY=Deploy-%2-%3-%OP_TARGETPLATFORM%
set OP_SETUPFOLDER=V%OP_VERSION_NEW%-%3-%OP_TARGETPLATFORM%
set OP_FULLPATH=%OP_SETUPFOLDER%\%OP_TRUNKNAME%
set OP_CD_FOLDER=%OP_ROOT_DIR%\Operationen\src\Setup\Versionen\V%OP_VERSION_NEW%-CD-%OP_TARGETPLATFORM%
set OP_BUILD_LOG=%OP_ROOT_DIR%\Operationen\src\Setup\Versionen\build-%OP_TARGETPLATFORM%.log
set WINZIP_SE_EXE=%ProgramFiles(x86)%\WinZip Self-Extractor\wzipse32.exe

if "%WINZIP_SE_EXE%"=="" (
  echo Error: WINZIP_SE_EXE does not exist!
  goto _exit
)

%OP_HOME_DRIVE%
cd %OP_ROOT_DIR%\Operationen\src\Setup\Versionen

rem Perform Release Rebuild
rem call "%OP_DOTNET20SDKDIR%\vcvars32.bat"

@echo Building target %1, %2, %3, %4

@echo %1-%2-%3-%4 > %OP_ROOT_DIR%\Operationen\src\Setup\Versionen\%OP_SETUPFOLDER%\build.txt

rem goto _skip_build

@rem
@rem Clean
@rem
%OP_MSBUILD% %OP_ROOT_DIR%\OpLog.sln /t:Clean /p:Configuration=Release /p:PlatformTarget=%OP_TARGETPLATFORM% > %OP_BUILD_LOG%
if not "%errorlevel%"=="0" (
  echo Build failed
  goto _exit
)
%OP_MSBUILD% %OP_ROOT_DIR%\OpLog.sln /t:Clean /p:Configuration=Debug /p:PlatformTarget=%OP_TARGETPLATFORM% >> %OP_BUILD_LOG%
if not "%errorlevel%"=="0" (
  echo Build failed
  goto _exit
)

@rem
@rem Build
@rem
%OP_MSBUILD% %OP_ROOT_DIR%\OpLog.sln /t:Rebuild /p:Configuration=Release /p:PlatformTarget=%OP_TARGETPLATFORM% /p:DefineConstants="targetplatform_%OP_TARGETPLATFORM%;%3" >> %OP_BUILD_LOG%
if not "%errorlevel%"=="0" (
  echo Build failed
  goto _exit
)

if "%1"=="help" (
  @echo Creating Help files
  rem Create HTML Help Workshop .CHM Help file
  "%ProgramFiles(x86)%\HTML Help Workshop\hhc.exe" %OP_ROOT_DIR%\Operationen\Dokumentation\Help\op.hhp
  if not "%errorlevel%"=="0" (
    echo Error executing: "%ProgramFiles(x86)%\HTML Help Workshop\hhc.exe" %OP_ROOT_DIR%\Operationen\Dokumentation\Help\op.hhp
    goto _exit
  )
  rem Create Sandcastle Plugin .CHM Help file
  %OP_MSBUILD% %OP_ROOT_DIR%\Operationen\src\OperationenImport\Help\OperationenImport.shfbproj
  if not "%errorlevel%"=="0" (
    echo Error executing: %OP_MSBUILD% %OP_ROOT_DIR%\Operationen\src\OperationenImport\Help\OperationenImport.shfbproj
    goto _exit
  )
  rem Create Sandcastle op-log .CHM Help file from all sources
  %OP_MSBUILD% %OP_ROOT_DIR%\Operationen\src\Help\operationen.shfbproj
  if not "%errorlevel%"=="0" (
    echo Error executing: %OP_MSBUILD% %OP_ROOT_DIR%\Operationen\src\Help\operationen.shfbproj
    goto _exit
  )
)

:_skip_build

%OP_HOME_DRIVE%
cd %OP_ROOT_DIR%\Operationen\src\Setup\Versionen
call %OP_ROOT_DIR%\Operationen\src\Setup\Versionen\CreateDeployFolder.cmd %2 %3 %4
if not "%errorlevel%"=="0" (
  echo Failed: call %OP_ROOT_DIR%\Operationen\src\Setup\Versionen\CreateDeployFolder.cmd %2 %3 %4
  goto _exit
)

%OP_HOME_DRIVE%
cd %OP_ROOT_DIR%\Operationen\src\Setup\Versionen

rd %OP_SETUPFOLDER% /s /q

md %OP_SETUPFOLDER%
md %OP_FULLPATH%

echo Hiermit wird %OP_PROGRAM_TITLE% Version %OP_VERSION_NEW% (%3, %OP_TARGETPLATFORM%) auf Ihrem PC installiert. > %OP_SETUPFOLDER%\%OP_TRUNKNAME%.dialog.txt
echo %OP_PROGRAM_TITLE% Version %OP_VERSION_NEW% (%3, %OP_TARGETPLATFORM%) Installation > %OP_SETUPFOLDER%\%OP_TRUNKNAME%.info.txt

echo Nach dem Extrahieren auszuführender Befehl: > %OP_SETUPFOLDER%\%OP_TRUNKNAME%.txt
echo =========================================== >> %OP_SETUPFOLDER%\%OP_TRUNKNAME%.txt
echo .\%OP_PROGRAM_TITLE%-V%OP_VERSION_NEW%\setuplauncher.exe  >> %OP_SETUPFOLDER%\%OP_TRUNKNAME%.txt
echo .\%OP_PROGRAM_TITLE%-V%OP_VERSION_NEW%\opsetup.exe update >> %OP_SETUPFOLDER%\%OP_TRUNKNAME%.txt

@call :copyfile ..\Images\Surgeon32x32.ico %OP_SETUPFOLDER%\
@if errorlevel 1 goto _exit

@call :xcopyfiles %OP_ROOT_DIR%\Operationen\src\Setup\Versionen\%OP_DEPLOY%\program\*   %OP_SETUPFOLDER%\%OP_TRUNKNAME%
@if errorlevel 1 goto _exit

@call :xcopyfiles %OP_ROOT_DIR%\Operationen\src\Setup\Versionen\%OP_DEPLOY%\sdk\*   %OP_SETUPFOLDER%\sdk\
@if errorlevel 1 goto _exit

rem
rem Create zipped SDK folder
rem
cd %OP_ROOT_DIR%\Operationen\src\Setup\Versionen\%OP_SETUPFOLDER%\sdk
"%ProgramFiles%\7-Zip"\7z a -tzip OperationenImport.zip OperationenImport OperationenImportCSV OperationenImportCSVUnicode OperationenImportIcpm3Op3CSV OperationenImportIcpm3Op3CSVUnicode OperationenImportIcpm5Op3CSV OperationenImportIcpm5Op3CSVUnicode OperationenImportIKPM10 OperationenImportIKPM10Unicode OperationenImportMccIsop OperationenImportMccIsopUnicode OperationenImportOrbis OperationenImportOrbisUnicode OperationenImportOrbisText OperationenImportOrbisTextUnicode OperationenImportSapCsv OperationenImportSapCsvUnicode OperationenImportSqlServer OperationenImportSdk.sln >> %OP_BUILD_LOG%
if not "%errorlevel%"=="0" (
  echo Build failed
  goto _exit
)

cd %OP_ROOT_DIR%\Operationen\src\Setup\Versionen\%OP_SETUPFOLDER%

rem
rem Create zipped folder for setup/update
rem
"%ProgramFiles%\7-Zip"\7z a -tzip OP-LOG-V%OP_VERSION_NEW%.zip OP-LOG-V%OP_VERSION_NEW% >> %OP_BUILD_LOG%
if not "%errorlevel%"=="0" (
  echo Build failed
  goto _exit
)

rem
rem Create self extracting exe
rem
"%WINZIP_SE_EXE%" OP-LOG-V%OP_VERSION_NEW%.zip -setup -t OP-LOG-V%OP_VERSION_NEW%.dialog.txt -i surgeon32x32.ico -a OP-LOG-V%OP_VERSION_NEW%.info.txt -lg -st "OP-LOG" -c .\OP-LOG-V%OP_VERSION_NEW%\setuplauncher.exe >> %OP_BUILD_LOG%
if not "%errorlevel%"=="0" (
  echo Failed: "%WINZIP_SE_EXE%" OP-LOG-V%OP_VERSION_NEW%.zip -setup -t OP-LOG-V%OP_VERSION_NEW%.dialog.txt -i surgeon32x32.ico -a OP-LOG-V%OP_VERSION_NEW%.info.txt -lg -st "OP-LOG" -c .\OP-LOG-V%OP_VERSION_NEW%\setuplauncher.exe
  goto _exit
)
ren OP-LOG-V%OP_VERSION_NEW%.exe operationen-setup.exe

"%WINZIP_SE_EXE%" OP-LOG-V%OP_VERSION_NEW%.zip -setup -t OP-LOG-V%OP_VERSION_NEW%.dialog.txt -i surgeon32x32.ico -a OP-LOG-V%OP_VERSION_NEW%.info.txt -lg -st "OP-LOG" -c .\OP-LOG-V%OP_VERSION_NEW%\OPSetup.exe update >> %OP_BUILD_LOG%
if not "%errorlevel%"=="0" (
  echo Failed: "%WINZIP_SE_EXE%" OP-LOG-V%OP_VERSION_NEW%.zip -setup -t OP-LOG-V%OP_VERSION_NEW%.dialog.txt -i surgeon32x32.ico -a OP-LOG-V%OP_VERSION_NEW%.info.txt -lg -st "OP-LOG" -c .\OP-LOG-V%OP_VERSION_NEW%\OPSetup.exe update
  goto _exit
)
ren OP-LOG-V%OP_VERSION_NEW%.exe operationen-update.exe
if not "%errorlevel%"=="0" (
  echo Failed: ren OP-LOG-V%OP_VERSION_NEW%.exe operationen-update.exe
  goto _exit
)

set OP_RET=0

goto _exit

:copyfile
copy /Y %1 %2 >>%OP_BUILD_LOG%
if not "%errorlevel%"=="0" (
  @echo Failed to copy file %1 to %2
  @exit /B 1
)
@exit /B 0

:xcopyfiles
xcopy /fivery %1 %2 >>%OP_BUILD_LOG%
if not "%errorlevel%"=="0" (
  @echo Failed: xcopy /fivery %1 %2
  @exit /B 1
)
@exit /B 0

:_usage
@echo Usage: %0 [help | nohelp] [bdc ^| nobdc] [chirurgie ^| urologie ^| gynaekologie] [x86 ^| anycpu]
goto _exit

:_exit
rem @echo Exiting %0 with return code %OP_RET%
%OP_HOME_DRIVE%
cd %OP_HOME_DRIVE%\Daten\Develop\DOT.NET\Operationen\src\Setup\Versionen
exit /B %OP_RET%

