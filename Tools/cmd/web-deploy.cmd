@echo off
SETLOCAL

@REM  ----------------------------------------------------------------------------
@REM  web-deploy.cmd
@REM
@REM  author: m4mc3r@gmail.com
@REM
@REM  ----------------------------------------------------------------------------

set start_time=%time%
set working_dir="%CD%\..\.."
set msbuild_bin_path="C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe"
set webdeploy_folder="C:\Program Files\IIS\Microsoft Web Deploy V3"
set solution_name=Undead.sln
set remote_computer=10.0.64.7
set website=Undead.Music
set user=10.0.64.7\administrator
set password=[password]
set param_file=Build\Dev.Deployment.SetParameters.xml
set deploy_cmd=Src\Undead.Music.Web\obj\Release\Package\Undead.Music.Web.deploy.cmd

if "%1"=="/?" goto help

cd %working_dir%

@REM generate package
%msbuild_bin_path% %solution_name% /p:Configuration=Release /p:DeployOnBuild=true /p:DeployTarget=Package /p:CreatePackageOnBuild=True
@if %errorlevel%  NEQ 0  goto :error

@REM recycle AppPool
rem %webdeploy_folder%\msdeploy -verb:sync -source:recycleApp -dest:recycleApp="%website%",recycleMode="RecycleAppPool",computerName=%remote_computer%
rem @if %errorlevel%  NEQ 0  goto :error

@REM deploy package
"%CD%\%deploy_cmd%" /Y /M:%remote_computer% /U:%user% /P:%password% -allowUntrusted -setParamFile:"%CD%\%param_file%"
@if %errorlevel%  NEQ 0  goto :error

@goto :success

:error
@exit /b errorLevel

:help
echo generates a web deploy package and deploy it to a server 
echo usage: web-deploy.cmd [/?] 
echo "/?" shows this help test 

:success
echo process successfully finished
echo start time: %start_time%
echo end time: %time%

ENDLOCAL
echo on