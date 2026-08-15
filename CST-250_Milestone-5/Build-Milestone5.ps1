$ErrorActionPreference = "Stop"
Set-Location $PSScriptRoot

if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    throw ".NET 8 SDK was not found. Install the .NET 8 SDK or use Visual Studio 2022 with the .NET desktop development workload."
}

Write-Host "CST-250 Milestone 5 - Build Verification" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
dotnet --version

Write-Host "`nRestoring solution..." -ForegroundColor Cyan
dotnet restore .\MinesweeperMilestone5.sln

Write-Host "`nBuilding Release configuration..." -ForegroundColor Cyan
dotnet build .\MinesweeperMilestone5.sln --configuration Release --no-restore --warnaserror

Write-Host "`nRunning automated tests..." -ForegroundColor Cyan
dotnet test .\MinesweeperClassLibrary.Tests\MinesweeperClassLibrary.Tests.csproj --configuration Release --no-build --logger "console;verbosity=normal"

Write-Host "`nMilestone 5 build and tests completed successfully." -ForegroundColor Green
