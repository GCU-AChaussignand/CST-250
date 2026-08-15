@echo off
setlocal
cd /d "%~dp0"
where powershell >nul 2>&1
if errorlevel 1 (
  echo PowerShell was not found.
  pause
  exit /b 1
)
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0Build-Milestone5.ps1"
if errorlevel 1 (
  echo.
  echo BUILD OR TESTS FAILED.
  pause
  exit /b 1
)
echo.
echo BUILD AND TESTS PASSED.
pause
