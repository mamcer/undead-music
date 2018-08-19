@echo off
SETLOCAL

@REM  ----------------------------------------------------------------------------
@REM  vstest-console.cmd
@REM
@REM  author: m4mc3r@gmail.com
@REM
@REM  ----------------------------------------------------------------------------

set start_time=%time%
set working_dir="%CD%\..\.."
set msbuild_bin_path="%PROGRAMFILES(X86)%\MSBuild\14.0\Bin\MSBuild.exe"
set vstestconsole_bin_path="%PROGRAMFILES(X86)%\Microsoft Visual Studio 14.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"
set vstestconsole_proj_path=Build\vstest-console.proj

if "%1"=="/?" goto help

cd %working_dir%

@REM run vstestconsole
%msbuild_bin_path% "%CD%\%vstestconsole_proj_path%" /p:WorkingDirectory=%CD% /p:VSTestConsoleBinPath=%vstestconsole_bin_path%
@if %errorlevel% NEQ 0 goto error
goto success

:error
@exit /b errorLevel

:help
echo looks for test assemblies and runs vstestconsole
echo usage: vstest-console.cmd [/?] 
echo "/?" shows this help test 

:success
echo process successfully finished
echo start time: %start_time%
echo end time: %time%

ENDLOCAL
echo on