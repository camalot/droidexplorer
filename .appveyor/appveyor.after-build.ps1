	if( Test-Path -Path .\VersionAssemblyInfo.txt ) {
    $version = (Get-Content -Path .\VersionAssemblyInfo.txt);
		$split = $version.split(".");
		[Environment]::SetEnvironmentVariable("CI_BUILD_VERSION", $version, "Machine");
		Write-Host "Set CI_BUILD_VERSION : $version";
		[Environment]::SetEnvironmentVariable("CI_BUILD_REVISION", $split[3], "Machine")
		Write-Host "Set CI_BUILD_REVISION : $split[3]";
	}
