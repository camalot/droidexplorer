$sourceVersionInfo = "$env:APPVEYOR_BUILD_FOLDER\Shared\VersionAssemblyInfo.txt";

if( (Test-Path -Path $sourceVersionInfo) -and ($env:Platform -eq 'x86') ) {
  $version = (Get-Content -Path $sourceVersionInfo);
	$split = $version.split(".");
	$m1 = $split[0];
	$m2 = $split[1];
	$b = $env:APPVEYOR_BUILD_NUMBER;
	$r = $split[3];

	$env:CI_BUILD_REVISION = $r;
	Write-Host "Set CI_BUILD_REVISION : $env:CI_BUILD_REVISION";
	$env:CI_BUILD_VERSION = "$m1.$m2.$b.$r";
	Write-Host "Set CI_BUILD_VERSION :$env:CI_BUILD_VERSION";
}
