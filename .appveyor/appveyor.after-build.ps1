if( (!(Test-Path -Path Env:\CI_BUILD_VERSION) -or !(Test-Path -Path Env:\CI_BUILD_REVISION)) -and (Test-Path -Path .\VersionAssemblyInfo.txt) ) {
  $version = (Get-Content -Path .\VersionAssemblyInfo.txt);
	$split = $version.split(".");
  $env:CI_BUILD_VERSION = $version;
	$env:CI_BUILD_REVISION = $split[3];
}
