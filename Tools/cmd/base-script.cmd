@echo off
SETLOCAL

@REM  ----------------------------------------------------------------------------
@REM  base-script.cmd
@REM
@REM  author: m4mc3r@gmail.com
@REM
@REM  ----------------------------------------------------------------------------

set start_time=%time%
set working_dir="%CD%\..\.."

if "%1"=="/?" goto help

cd %working_dir%

@REM <cool-functionalty>
echo base-script
@if %errorlevel%  NEQ 0  goto :error
@REM </cool-functionalty>

@goto :success

:error
@exit /b errorLevel

:help
echo usage: base-script.cmd [/?] 
echo "/?" shows this help test 

:success
echo process successfully finished
echo start time: %start_time%
echo end time: %time%

ENDLOCAL
echo on