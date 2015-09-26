$cachedVersionInfo = "$env:APPVEYOR_BUILD_FOLDER\.cache\VersionAssemblyInfo.cache";
$sourceVersionInfo = "$env:APPVEYOR_BUILD_FOLDER\Shared\VersionAssemblyInfo.txt";

if( (Test-Path -Path $sourceVersionInfo) -and !(Test-Path -Path $cachedVersionInfo)) {
	(Get-Content -Path $sourceVersionInfo) | Out-File -Path $cachedVersionInfo -Force;
}

if( Test-Path -Path $cachedVersionInfo ) {
  $version = (Get-Content -Path $cachedVersionInfo);
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

# on x64, we can clear this cache file out after we set the env:vars.
if( (Test-Path -Path Env:\Platform) -and ($env:Platform -eq "x64" ) -and (Test-Path -Path $cachedVersionInfo) ) {
	(Remove-Item -Path $cachedVersionInfo) | Out-Null;
}
