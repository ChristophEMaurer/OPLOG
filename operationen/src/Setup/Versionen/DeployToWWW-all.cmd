@echo off

setlocal

if not exist .\setupBuildEnvironment.cmd (
  echo Error: file setupBuildEnvironment.cmd is missing
  goto _exit
)

for /F %%f in (version-major.txt) do set OP_VERSION_MAJOR_NEW=%%f
for /F %%f in (version.txt) do set OP_VERSION_NEW=%%f

@echo Running %0

call .\setupBuildEnvironment.cmd

if not exist %SEVEN_ZIP_EXE% (
  echo Error: SEVEN_ZIP_EXE is not set!
  goto _exit
)

set OP_BUILD_LOG=%OP_ROOT_DIR%\Operationen\src\Setup\Versionen\build-www.log
%OP_HOME_DRIVE%
cd %OP_ROOT_DIR%\Operationen\src\Setup\Versionen

call DeployToWWW.cmd chirurgie anycpu
if not "%errorlevel%"=="0" (
  echo Failed: call DeployToWWW.cmd chirurgie anycpu
  goto _exit
)

call DeployToWWW.cmd chirurgie x86
if not "%errorlevel%"=="0" (
  echo Failed: call DeployToWWW.cmd chirurgie x86
  goto _exit
)

rem
rem SDK
rem 
@call :copyfile %OP_ROOT_DIR%\operationen\src\Setup\Versionen\V%OP_VERSION_NEW%-chirurgie-anycpu\sdk\OperationenImport.zip %OP_ROOT_DIR%\Operationen\www\download\sdk\OperationenImport.zip
@if errorlevel 1 goto _exit

rem
rem Plug-ins
rem 
@call :copyfile %OP_ROOT_DIR%\operationen\src\Setup\Versionen\V%OP_VERSION_NEW%-chirurgie-anycpu\sdk\plugins.chm %OP_ROOT_DIR%\Operationen\www\download\sdk\plugins.chm
@if errorlevel 1 goto _exit

rem
rem source code-generated help
rem 
@call :copyfile %OP_ROOT_DIR%\operationen\src\Help\Help\op-log.chm %OP_ROOT_DIR%\Operationen\www\download\op-log.chm
@if errorlevel 1 goto _exit

rem
rem Create zipped html help folder
rem
del %OP_ROOT_DIR%\operationen\www\download\online-help.zip
cd %OP_ROOT_DIR%\operationen\Dokumentation\Help\
%SEVEN_ZIP_EXE% a -tzip -xr!.svn %OP_ROOT_DIR%\operationen\www\download\online-help.zip html >> %OP_BUILD_LOG%

rem
rem Create database files
rem
del %OP_ROOT_DIR%\operationen\www\download\db\*.zip
rem
rem mySQL
rem
cd %OP_ROOT_DIR%\operationen\src\Database\mySQL
%SEVEN_ZIP_EXE% a -tzip %OP_ROOT_DIR%\operationen\www\download\db\mysql-operationen.zip V%OP_VERSION_MAJOR_NEW%-mysql-operationen-data.sql V%OP_VERSION_MAJOR_NEW%-mysql-operationen-schema.sql >> %OP_BUILD_LOG%
rem
rem SQLServer
rem
cd %OP_ROOT_DIR%\operationen\src\Database\SQLServer
%SEVEN_ZIP_EXE% a -tzip %OP_ROOT_DIR%\operationen\www\download\db\sqlserver-operationen.zip V%OP_VERSION_MAJOR_NEW%-sqlserver-operationen-data.sql V%OP_VERSION_MAJOR_NEW%-sqlserver-operationen-schema.sql >> %OP_BUILD_LOG%
rem
rem SQL Azure
rem
cd %OP_ROOT_DIR%\operationen\src\Database\SQLAzure
%SEVEN_ZIP_EXE% a -tzip %OP_ROOT_DIR%\operationen\www\download\db\sqlazure-operationen.zip V%OP_VERSION_MAJOR_NEW%-sqlazure-operationen-data.sql V%OP_VERSION_MAJOR_NEW%-sqlazure-operationen-schema.sql >> %OP_BUILD_LOG%

@echo SUCCESS
@echo SUCCESS
@echo SUCCESS
@echo SUCCESS
@echo SUCCESS
@echo SUCCESS

goto _exit

:copyfile
copy /Y %1 %2 >> %OP_BUILD_LOG%
if not "%errorlevel%"=="0" (
  echo Failed to copy file %1 to %2
  @exit /B 1
)
@exit /B 0

:xcopyfiles
xcopy /ivery %1 %2 >> %OP_BUILD_LOG%
if not "%errorlevel%"=="0" (
  @echo Failed: xcopy /ivery %1 %2
  @exit /B 1
)
@exit /B 0

:_exit
%OP_HOME_DRIVE%
cd %OP_ROOT_DIR%\Operationen\src\Setup\Versionen

pause
