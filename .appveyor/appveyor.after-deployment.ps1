Import-Module "$env:APPVEYOR_BUILD_FOLDER\.appveyor\Invoke-MsBuild.psm1";

# trigger the codeplex deployment script
if( (Test-Path -Path Env:\CI_DEPLOY_CODEPLEX) -and ((Get-Item -Path Env:\CI_DEPLOY_CODEPLEX).Value) -eq $true ) {
	Invoke-MsBuild -Path "$env:APPVEYOR_BUILD_FOLDER\.appveyor\DeployCodePlex.msbuild" -MsBuildParameters "/verbosity:detailed /p:CI_BUILD_VERSION=$env:CI_BUILD_VERSION /p:CI_BUILD_REVISION=$env:CI_BUILD_REVISION /p:CI_BUILD_MAJOR=$env:CI_BUILD_MAJOR /p:CI_BUILD_MINOR=$env:CI_BUILD_MINOR"
}

if( (Test-Path -Path Env:\CI_DEPLOY_WEBAPI_RELEASE) -and ((Get-Item -Path Env:\CI_DEPLOY_WEBAPI_RELEASE).Value) -eq $true ) {

}


# This cleans up any environment variables that may need to be reset
# this probably isn't needed, but it doesn't hurt.

if( (Test-Path -Path Env:\CI_BUILD_VERSION) ) {
    $env:CI_BUILD_VERSION = '';
}
