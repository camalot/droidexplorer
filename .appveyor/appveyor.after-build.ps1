

$sourceVersionInfo = "$env:APPVEYOR_BUILD_FOLDER\Shared\AssemblyVersionInfo.txt";

if( (Test-Path -Path $sourceVersionInfo) -and ($env:Platform -eq 'x86') ) {
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

	$env:CI_BUILD_VERSION | Out-File -FilePath $sourceVersionInfo -Force;


}
