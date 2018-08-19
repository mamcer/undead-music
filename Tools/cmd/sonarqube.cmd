@echo off
SETLOCAL

@REM  ----------------------------------------------------------------------------
@REM  sonarqube.cmd
@REM
@REM  author: m4mc3r@gmail.com
@REM
@REM  ----------------------------------------------------------------------------

set start_time=%time%
set sonar_runner=C:\root\bin\sonar-scanner\bin\sonar-runner
set working_dir="%CD%\..\.."

if "%1"=="/?" goto help

cd %working_dir%

@REM call sonar runner
call %sonar_runner% -e
@if %errorlevel%  NEQ 0  goto :error

:error
@exit /b errorLevel

:help
echo runs a sonarqube analysis
echo usage: sonarqube.cmd [/?] 
echo "/?" shows this help test 

:success
echo process successfully finished
echo start time: %start_time%
echo end time: %time%

ENDLOCAL
echo on