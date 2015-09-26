if( (!(Test-Path -Path Env:\CI_BUILD_VERSION) -or !(Test-Path -Path Env:\CI_BUILD_REVISION)) -and (Test-Path -Path .\VersionAssemblyInfo.txt) ) {
  $version = (Get-Content -Path .\VersionAssemblyInfo.txt);
	$split = $version.split(".");
	[Environment]::SetEnvironmentVariable("CI_BUILD_VERSION", $version, "Machine")
	[Environment]::SetEnvironmentVariable("CI_BUILD_REVISION", $split[3], "Machine")

}
