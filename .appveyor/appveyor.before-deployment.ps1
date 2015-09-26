<#
 #
 #
 #>

$commitMessageRegex = "^\[deploy\:(pre-release|draft|release)\]$";

# THIS SHOULD BE MOVED TO THE MATCH SECTION AFTER TESTING.
	# read the publish text file
	if(Test-Path -Path "$env:APPVEYOR_BUILD_FOLDER\.build\publishchangelog.txt") {
		$publishNotes = (Get-Content -Path $env:APPVEYOR_BUILD_FOLDER\.build\publishchangelog.txt);
		[Environment]::SetEnvironmentVariable("CI_RELEASE_DESCRIPTION", $publishNotes, "Machine")
		Write-Host "Set CI_RELEASE_DESCRIPTION : $publishNotes";
	}

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

# Must come from master branch.
# Must not have a PULL Request Number
# Must match regex
if ( !$env:APPVEYOR_PULL_REQUEST_NUMBER -and ($env:APPVEYOR_REPO_BRANCH -eq "master") -and ($env:APPVEYOR_REPO_COMMIT_MESSAGE -match $commitMessageRegex) ) {
	$env:CI_RELEASE_DESCRIPTION = $env:APPVEYOR_REPO_COMMIT_MESSAGE_EXTENDED
	$env:CI_DEPLOY_NUGET = $true;
  $env:CI_DEPLOY_GITHUB = $true;
  $env:CI_DEPLOY_FTP = $true;
	$env:CI_DEPLOY_WebHook = $true;
	$env:CI_DEPLOY_WebDeploy = $true;
	$env:CI_DEPLOY_CodePlex = $true;
	$env:CI_DEPLOY_WEBAPI_RELEASE = $true;



} else {
	# Do not assign a release number or deploy
  $env:CI_DEPLOY_NUGET = $false;
  $env:CI_DEPLOY_GITHUB = $false;
  $env:CI_DEPLOY_FTP = $false;
	$env:CI_DEPLOY_WebHook = $false;
	$env:CI_DEPLOY_WebDeploy = $false;
	$env:CI_DEPLOY_CodePlex = $true;
	$env:CI_DEPLOY_WEBAPI_RELEASE = $false;



}