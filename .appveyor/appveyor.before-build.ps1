.\.appveyor\InstallPfx.ps1 -pfx "$env:APPVEYOR_BUILD_FOLDER\Shared\droidexplorer.pfx" -password ((Get-Item Env:\DE_PFX_KEY).Value) -containerName ((Get-Item Env:\VS_PFX_KEY).Value);

# If it's an x64 build, and the version info file exists.
# Load those values for the build version

$sourceVersionInfo = "$env:APPVEYOR_BUILD_FOLDER\Shared\VersionAssemblyInfo.txt";
if( $env:Platform -eq "x64" -and (Test-Path -Path $sourceVersionInfo) ) {
  $version = (Get-Content -Path $sourceVersionInfo);
	$split = $version.split(".");
	$m1 = $split[0];
	$m2 = $split[1];
	$b = $env:APPVEYOR_BUILD_NUMBER;
	$r = $split[3];
	
	Set-AppveyorBuildVariable -Name CI_BUILD_MAJOR -Value $m1;
	Set-AppveyorBuildVariable -Name CI_BUILD_MINOR -Value $m2;

	Set-AppveyorBuildVariable -Name CI_BUILD_NUMBER -Value $env:APPVEYOR_BUILD_NUMBER;
	Set-AppveyorBuildVariable -Name CI_BUILD_REVISION -Value $r;
	Set-AppveyorBuildVariable -Name CI_BUILD_VERSION -Value "$m1.$m2.$b.$r";

	Write-Host "Set the CI_BUILD_VERSION to $env:CI_BUILD_VERSION";

}