	if( Test-Path -Path $env:APPVEYOR_BUILD_FOLDER\Shared\VersionAssemblyInfo.txt ) {
    $version = (Get-Content -Path $env:APPVEYOR_BUILD_FOLDER\Shared\VersionAssemblyInfo.txt);
		$split = $version.split(".");
		[Environment]::SetEnvironmentVariable("CI_BUILD_VERSION", $version, "Machine");
		Write-Host "Set CI_BUILD_VERSION : $version";
		[Environment]::SetEnvironmentVariable("CI_BUILD_REVISION", $split[3], "Machine")
		Write-Host "Set CI_BUILD_REVISION : $split[3]";
	}

