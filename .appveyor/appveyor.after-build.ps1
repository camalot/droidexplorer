	if( Test-Path -Path $env:APPVEYOR_BUILD_FOLDER\Shared\VersionAssemblyInfo.txt ) {
    $version = (Get-Content -Path $env:APPVEYOR_BUILD_FOLDER\Shared\VersionAssemblyInfo.txt);
		$split = $version.split(".");
		$m1 = $split[0];
		$m2 = $split[1];
		$b = $env:APPVEYOR_BUILD_NUMBER;
		$r = $split[3];
		#[Environment]::SetEnvironmentVariable("CI_BUILD_VERSION", $version, "Machine");
		#[Environment]::SetEnvironmentVariable("CI_BUILD_REVISION", $split[3], "Machine");
		$env:CI_BUILD_REVISION = $r;
		Write-Host "Set CI_BUILD_REVISION : $env:CI_BUILD_REVISION";
		$env:CI_BUILD_VERSION = "$m1.$m2.$b.$r";
		Write-Host "Set CI_BUILD_VERSION :$env:CI_BUILD_VERSION";
	}


