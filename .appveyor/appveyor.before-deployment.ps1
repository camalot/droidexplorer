<#
 #
 #
 #>

$commitMessageRegex = "^\[deploy\:(pre-release|draft|release)\]$";


# Must come from master branch.
# Must not have a PULL Request Number
# Must match regex
if ( !$env:APPVEYOR_PULL_REQUEST_NUMBER -and ($env:APPVEYOR_REPO_BRANCH -eq "master") -and ($env:APPVEYOR_REPO_COMMIT_MESSAGE -match $commitMessageRegex) ) {
	$env:CI_DEPLOY_NUGET = $true;
  $env:CI_DEPLOY_GITHUB = $false;
  $env:CI_DEPLOY_FTP = $false;
	$env:CI_DEPLOY_WebHook = $true;
	$env:CI_DEPLOY_WebDeploy = $true;
	$env:CI_DEPLOY_CodePlex = $true;
	$env:CI_DEPLOY_WEBAPI_RELEASE = $true;
	$env:CI_DEPLOY_PUSHBULLET = $true;
	$env:CI_DEPLOY = $true;
} else {
	# Do not assign a release number or deploy
  $env:CI_DEPLOY_NUGET = $false;
  $env:CI_DEPLOY_GITHUB = $false;
  $env:CI_DEPLOY_FTP = $false;
	$env:CI_DEPLOY_WebHook = $false;
	$env:CI_DEPLOY_WebDeploy = $false;
	$env:CI_DEPLOY_CodePlex = $false;
	$env:CI_DEPLOY_WEBAPI_RELEASE = $false;
	$env:CI_DEPLOY_PUSHBULLET = $false;
	$env:CI_DEPLOY = $false;
}