@echo Off
pushd %~dp0
setlocal

set CACHED_NUGET=%LOCALAPPDATA%\NuGet\NuGet.exe
if exist %CACHED_NUGET% goto CopyNuGet

echo Downloading latest version of NuGet.exe...
if not exist %LOCALAPPDATA%\NuGet @mkdir %LOCALAPPDATA%\NuGet
@powershell -NoProfile -ExecutionPolicy Unrestricted -Command "$ProgressPreference = 'SilentlyContinue'; Invoke-WebRequest 'https://www.nuget.org/nuget.exe' -OutFile '%CACHED_NUGET%'"

:CopyNuGet
if exist .nuget\nuget.exe goto Restore
if not exist .nuget @mkdir .nuget
@copy %CACHED_NUGET% .nuget\nuget.exe > nul

:Restore
if exist packages\KoreBuild goto Build
.nuget\NuGet.exe install KoreBuild -ExcludeVersion -o packages -nocache -pre
.nuget\NuGet.exe install Sake -Version 0.2 -ExcludeVersion -o packages -nocache

:Build
if "%SKIP_DNX_INSTALL%"=="1" goto Run
call packages\KoreBuild\build\dnvm upgrade -runtime CLR -arch x86

:Run
call packages\KoreBuild\build\dnvm use default -runtime CLR -arch x86
packages\Sake\tools\Sake.exe -I packages\KoreBuild\build -f makefile.shade %*

if %ERRORLEVEL% neq 0 goto BuildFail
goto BuildSuccess

:BuildFail
echo.
echo *** BUILD FAILED ***
goto End

:BuildSuccess
echo.
echo *** BUILD SUCCEEDED ***
goto End

:End
echo.
popd
exit /B %ERRORLEVEL%
