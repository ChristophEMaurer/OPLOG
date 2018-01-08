@echo off

setlocal

call CreateVersionFull-all.cmd anycpu
if not "%errorlevel%"=="0" (
  echo Failed in %0: call CreateVersionFull-all.cmd anycpu
  goto _exit
)

call CreateVersionFull-all.cmd x86
if not "%errorlevel%"=="0" (
  echo Failed in %0: call CreateVersionFull-all.cmd x86
  goto _exit
)

@echo SUCCESS
@echo SUCCESS
@echo SUCCESS
@echo SUCCESS
@echo SUCCESS
@echo SUCCESS

goto _exit

:_usage
@echo Usage: %0
goto _exit

:_exit
rem @echo Exiting %0 with return code %OP_RET%
rem "C:\Program Files\Windows Media Player\wmplayer.exe" c:\WINDOWS\Media\tada.wav
pause
