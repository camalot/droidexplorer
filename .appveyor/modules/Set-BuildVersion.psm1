
function Set-BuildVersion {
	process {
		Write-Host "Setting up build version";
		$dt = (Get-Date).ToUniversalTime();
		$doy = $dt.DayOfYear.ToString();
		$yy = $dt.ToString("yy");
		$revision = "$doy$yy";
		$version = "$env:APPVEYOR_BUILD_VERSION.$revision";
		$split = $version.split(".");
		$m1 = $split[0];
		$m2 = $split[1];
		$b = $env:APPVEYOR_BUILD_NUMBER;
		$r = $split[3];		
		
		Set-AppveyorBuildVariable -Name CI_BUILD_MAJOR -Value $m1;
		Set-AppveyorBuildVariable -Name CI_BUILD_MINOR -Value $m2;

		Set-AppveyorBuildVariable -Name CI_BUILD_NUMBER -Value $b;
		Set-AppveyorBuildVariable -Name CI_BUILD_REVISION -Value $r;
		Set-AppveyorBuildVariable -Name CI_BUILD_VERSION -Value "$m1.$m2.$b.$r";

		Write-Host "Set the CI_BUILD_VERSION to $env:CI_BUILD_VERSION";
	}
}

Export-ModuleMember -Function Set-BuildVersion;


