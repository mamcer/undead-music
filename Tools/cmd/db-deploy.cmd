@echo off
SETLOCAL

@REM  ----------------------------------------------------------------------------
@REM  db-deploy.cmd
@REM
@REM  author: m4mc3r@gmail.com
@REM
@REM  ----------------------------------------------------------------------------

set start_time=%time%
set msbuild_bin_path="%ProgramFiles(x86)%\MSBuild\14.0\Bin\msbuild.exe"
set webdeploy_bin_path="%ProgramW6432%\IIS\Microsoft Web Deploy V3\msdeploy.exe"
set working_dir="%CD%\..\.."
set solution_name=Undead.sln
set default_build_type=Release
set dbdacfx_file_path=Src\Undead.Music.Database\bin\Release\Undead.Music.Database.dacpac
set data_source=localhost
set database=UndeadMusic
set user_id=undead
set password=undead

if "%1"=="/?" goto help

cd %working_dir%

%msbuild_bin_path% /m %solution_name% /t:Rebuild /p:Configuration=%default_build_type%
@if %errorlevel%  NEQ 0  goto :error

%webdeploy_bin_path% -verb:sync -source:dbDacFx=%CD%\%dbdacfx_file_path% -dest:dbDacFx="Data Source=%data_source%;Database=%database%;User Id=%user_id%;Password=%password%"
@if %errorlevel%  NEQ 0  goto :error

@goto :success

:error
@exit /b errorLevel

:help
echo deploys a database using webdeploy
echo usage: db-deploy.cmd [/?] 
echo "/?" shows this help text.

:success
echo process successfully finished
echo start time: %start_time%
echo end time: %time%

ENDLOCAL
echo on