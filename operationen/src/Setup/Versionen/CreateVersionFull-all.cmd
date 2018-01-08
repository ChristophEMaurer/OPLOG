rem
rem %0      %1
rem %0 [x86 | anycpu | x64]
rem

setlocal

set OP_RET=1

if "%1"=="" (
  goto _usage
)
if not "%2"=="" (
  goto _usage
)

if not "%1"=="x86" (
  if not "%1"=="anycpu" (
    if not "%1"=="x64" (
      goto _usage
    )
  )
)

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

set OP_TARGETPLATFORM=%1
set OP_CD_FOLDER=%OP_ROOT_DIR%\Operationen\src\Setup\Versionen\V%OP_VERSION_NEW%-CD-%OP_TARGETPLATFORM%
set OP_BUILD_LOG=%OP_ROOT_DIR%\Operationen\src\Setup\Versionen\build-%OP_TARGETPLATFORM%.log

rem @echo Running %0 %1

%OP_HOME_DRIVE%
cd %OP_ROOT_DIR%\Operationen\src\Setup\Versionen

@echo CreateVersionFull.cmd nohelp nobdc chirurgie %OP_TARGETPLATFORM%
call CreateVersionFull.cmd nohelp nobdc chirurgie %OP_TARGETPLATFORM%
if not "%errorlevel%"=="0" (
  echo Failed in %0: call CreateVersionFull.cmd nohelp nobdc chirurgie %OP_TARGETPLATFORM%
  goto _exit
)


rd %OP_CD_FOLDER% /s /q

set OP_RET=0

goto _exit

:_usage
@echo Usage: %0 [x86 ^| anycpu ^| x64 ]
goto _exit

:_exit
rem @echo Exiting %0 with return code %OP_RET%
%OP_HOME_DRIVE%
cd %OP_ROOT_DIR%\Operationen\src\Setup\Versionen
exit /B %OP_RET%

