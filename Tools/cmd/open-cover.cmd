@echo off
SETLOCAL

@REM  ----------------------------------------------------------------------------
@REM  open-cover.cmd
@REM
@REM  author: m4mc3r@gmail.com
@REM
@REM  ----------------------------------------------------------------------------

set start_time=%time%
set working_dir="%CD%\..\.."
set msbuild_bin_path="%PROGRAMFILES(x86)%\MSBuild\14.0\Bin\MSBuild.exe"
set mstest_bin_path="%PROGRAMFILES(x86)%\Microsoft Visual Studio 14.0\Common7\IDE\mstest.exe"
set opencover_bin_path=Tools\open-cover\OpenCover.Console.exe
set opencover_proj_path=Build\open-cover.proj
set solution_name=Undead.sln

if "%1"=="/?" goto help

cd %working_dir%

@REM rebuild solution
%msbuild_bin_path% /m %solution_name% /t:Rebuild /p:Configuration=Debug
@if %errorlevel% NEQ 0 GOTO error

@REM run opencover
%msbuild_bin_path% /p:WorkingDirectory="%CD%" /p:OpenCoverBinPath=%CD%\%opencover_bin_path% /p:MSTestBinPath=%mstest_bin_path% %CD%\%opencover_proj_path%
@if %errorlevel% NEQ 0 goto error
goto success

:error
@exit /b errorLevel

:help
echo runs a code coverage analysis using opencover 
echo usage: open-cover.cmd [/?] 
echo "/?" shows this help test 

:success
echo process successfully finished
echo start time: %start_time%
echo end time: %time%

ENDLOCAL
echo on