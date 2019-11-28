@echo off

SET VERSION=0.0.0
IF NOT [%1]==[] (set VERSION=%1)

SET TAG=0.0.0
IF NOT [%2]==[] (set TAG=%2)
SET TAG=%TAG:tags/=%

curl -o nuget.exe https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -k
if %errorlevel% neq 0 exit /b %errorlevel%

.\nuget.exe restore .\src\ViewModels.ApplicationState.Tests\ViewModels.ApplicationState.Tests.csproj -PackagesDirectory .\src\packages -Verbosity detailed
if %errorlevel% neq 0 exit /b %errorlevel%

.\nuget.exe restore .\src\ViewModels.ApplicationState\ViewModels.ApplicationState.csproj -PackagesDirectory .\src\packages -Verbosity detailed
if %errorlevel% neq 0 exit /b %errorlevel%

dotnet build .\src\ViewModels.ApplicationState\ViewModels.ApplicationState.csproj -p:Version=%VERSION% -c Release
if %errorlevel% neq 0 exit /b %errorlevel%

dotnet test .\src\ViewModels.ApplicationState.Tests\ViewModels.ApplicationState.Tests.csproj
if %errorlevel% neq 0 exit /b %errorlevel%

dotnet pack .\src\ViewModels.ApplicationState\ViewModels.ApplicationState.csproj -o .\dist -p:Version="%VERSION%" -p:PackageVersion="%VERSION%" -p:Tag="%TAG%" -c Release
exit /b %errorlevel%
