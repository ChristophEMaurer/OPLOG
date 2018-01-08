@rem
@rem %0                  %1                        %2
@rem %0 [chirurgie | urologie | gynaekologie] [x86 | anycpu]
@rem

setlocal

set OP_RET=1

for /F %%f in (version.txt) do set OP_VERSION_NEW=%%f

if "%2"=="" (
  goto _usage
)

if not "%3"=="" (
  goto _usage
)

if not "%1"=="chirurgie" (
  if not "%1"=="urologie" (
    if not "%1"=="gynaekologie" (
      goto _usage
    )
  )
)

if not "%2"=="x86" (
  if not "%2"=="anycpu" (
    goto _usage
  )
)

if "%OP_HOME_DRIVE%"=="" (
  echo Error: OP_HOME_DRIVE is not set!
  goto _exit
)
if "%OP_ROOT_DIR%"=="" (
  echo Error: OP_ROOT_DIR is not set!
  goto _exit
)

set OP_GEBIET=%1
set OP_TARGETPLATFORM=%2
set OP_SETUP_FOLDER=%OP_ROOT_DIR%\Operationen\src\Setup\Versionen\V%OP_VERSION_NEW%-%OP_GEBIET%-%OP_TARGETPLATFORM%
set OP_TOOL=%OP_ROOT_DIR%\Operationen\src\Setup\Versionen\tool.exe
set OP_BUILD_LOG=%OP_ROOT_DIR%\Operationen\src\Setup\Versionen\build-%OP_TARGETPLATFORM%.log

@echo Running %0 %1 %2

if "%OP_GEBIET%"=="chirurgie" (
  if "%OP_TARGETPLATFORM%"=="anycpu" (
    set OP_WWW_VERSION_FILE=%OP_ROOT_DIR%\Operationen\www\download\version.txt
    set OP_WWW_SETUP=operationen-setup.exe
    set OP_WWW_UPDATE=operationen-update.exe
  ) else (
    set OP_WWW_VERSION_FILE=%OP_ROOT_DIR%\Operationen\www\download\version-%OP_TARGETPLATFORM%.txt
    set OP_WWW_SETUP=operationen-setup-%OP_TARGETPLATFORM%.exe
    set OP_WWW_UPDATE=operationen-update-%OP_TARGETPLATFORM%.exe
  )
) else (
  if "%OP_TARGETPLATFORM%"=="anycpu" (
    set OP_WWW_VERSION_FILE=%OP_ROOT_DIR%\Operationen\www\download\version-%OP_GEBIET%.txt
    set OP_WWW_SETUP=operationen-setup-%OP_GEBIET%.exe
    set OP_WWW_UPDATE=operationen-update-%OP_GEBIET%.exe
  ) else (
    set OP_WWW_VERSION_FILE=%OP_ROOT_DIR%\Operationen\www\download\version-%OP_GEBIET%-%OP_TARGETPLATFORM%.txt
    set OP_WWW_SETUP=operationen-setup-%OP_GEBIET%-%OP_TARGETPLATFORM%.exe
    set OP_WWW_UPDATE=operationen-update-%OP_GEBIET%-%OP_TARGETPLATFORM%.exe
  )
)

@call :copyfile %OP_SETUP_FOLDER%\operationen-setup.exe %OP_ROOT_DIR%\Operationen\www\download\%OP_WWW_SETUP%
@if errorlevel 1 goto _exit

@call :copyfile %OP_SETUP_FOLDER%\operationen-update.exe %OP_ROOT_DIR%\Operationen\www\download\%OP_WWW_UPDATE%
@if errorlevel 1 goto _exit

%OP_TOOL% /cmd echo /text "%OP_VERSION_NEW%|"
%OP_TOOL% /cmd fileSizeKB /fileName %OP_SETUP_FOLDER%\operationen-update.exe
%OP_TOOL% /newline yes /cmd echo /text "|%OP_WWW_UPDATE%"

rem
rem ACHTUNG: Kein NEWLINE am Ende der Zeile!!!
rem

%OP_TOOL% /cmd echo /text %OP_VERSION_NEW%^| > %OP_WWW_VERSION_FILE%
%OP_TOOL% /cmd fileSizeKB /fileName %OP_SETUP_FOLDER%\operationen-update.exe >> %OP_WWW_VERSION_FILE%
%OP_TOOL% /cmd echo /text ^|%OP_WWW_UPDATE% >> %OP_WWW_VERSION_FILE%

goto _done

:_done

set OP_RET=0

goto _exit

:copyfile
copy /Y %1 %2 >> %OP_BUILD_LOG%
if not "%errorlevel%"=="0" (
  echo Failed to copy file %1 to %2
  @exit /B 1
)
@exit /B 0

:_usage
@echo Usage: %0 [chirurgie ^| urologie ^| gynaekologie] [x86 ^| anycpu]
goto _exit

:_exit
%OP_HOME_DRIVE%
exit /B %OP_RET%

