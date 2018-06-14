#!/bin/bash
set -e
version="0.0.0"
if [ -n "$1" ]; then version="$1"
fi

url -o nuget.exe https://dist.nuget.org/win-x86-commandline/latest/nuget.exe

.\nuget.exe restore .\src\ViewModels.ApplicationState.Tests\ViewModels.ApplicationState.Tests.csproj -PackagesDirectory .\src\packages -Verbosity detailed
.\nuget.exe restore .\src\ViewModels.ApplicationState\ViewModels.ApplicationState.csproj -PackagesDirectory .\src\packages -Verbosity detailed

dotnet test .\src\ViewModels.ApplicationState.Tests\ViewModels.ApplicationState.Tests.csproj

.\nuget.exe pack .\src\ViewModels.ApplicationState\ViewModels.ApplicationState.csproj -OutputDirectory ..\..\dist -Version "$version" -Verbosity detailed