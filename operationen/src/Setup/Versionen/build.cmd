rem Perform Release Rebuild

if not exist .\setupBuildEnvironment.cmd (
  echo Error: file setupBuildEnvironment.cmd is missing
  goto _exit
)

call .\setupBuildEnvironment.cmd

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

@echo Running %0

call "%OP_DOTNET20SDKDIR%\vcvars32.bat"

%OP_MSBUILD% %OP_ROOT_DIR%\OpLog.sln /t:Clean   /p:Configuration=Release
%OP_MSBUILD% %OP_ROOT_DIR%\OpLog.sln /t:Clean   /p:Configuration=Debug
%OP_MSBUILD% %OP_ROOT_DIR%\OpLog.sln /t:Rebuild /p:Configuration=Release

:_exit
pause
