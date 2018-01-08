@echo off

setlocal

for /F %%f in (version.txt) do set OP_VERSION_NEW=%%f

if not exist .\setupBuildEnvironment.cmd (
  echo Error: file setupBuildEnvironment.cmd is missing
  goto _exit
)

call .\setupBuildEnvironment.cmd

if "%OP_HOME_DRIVE%"=="" (
  echo Error: OP_HOME_DRIVE is not set!
  goto _exit
)
if "%OP_ROOT_DIR%"=="" (
  echo Error: OP_ROOT_DIR is not set!
  goto _exit
)

@echo Running %0

%OP_HOME_DRIVE%
cd %OP_ROOT_DIR%\Operationen\src\Setup\Versionen

call CreateVersionFull.cmd help nobdc chirurgie x86
if not "%errorlevel%"=="0" (
  echo Failed in %0: call CreateVersionFull.cmd help nobdc chirurgie x86
  goto _exit
)

@echo SUCCESS
@echo SUCCESS
@echo SUCCESS
@echo SUCCESS
@echo SUCCESS
@echo SUCCESS

:_exit
%OP_HOME_DRIVE%
cd %OP_ROOT_DIR%\Operationen\src\Setup\Versionen

pause
