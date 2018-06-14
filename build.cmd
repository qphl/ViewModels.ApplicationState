@echo off

SET VERSION=0.0.0
IF NOT [%1]==[] (set VERSION=%1)

curl -o nuget.exe https://dist.nuget.org/win-x86-commandline/latest/nuget.exe

.\nuget.exe restore .\src\ViewModels.ApplicationState.Tests\ViewModels.ApplicationState.Tests.csproj -PackagesDirectory .\src\packages -Verbosity detailed
.\nuget.exe restore .\src\ViewModels.ApplicationState\ViewModels.ApplicationState.csproj -PackagesDirectory .\src\packages -Verbosity detailed

dotnet build .\src\ViewModels.ApplicationState\ViewModels.ApplicationState.csproj -p:Version=%version% -c Release

dotnet test .\src\ViewModels.ApplicationState.Tests\ViewModels.ApplicationState.Tests.csproj
if %errorlevel% neq 0 exit /b %errorlevel%

.\nuget.exe pack .\src\ViewModels.ApplicationState\ViewModels.ApplicationState.csproj -OutputDirectory .\dist -Version %version% -Verbosity detailed