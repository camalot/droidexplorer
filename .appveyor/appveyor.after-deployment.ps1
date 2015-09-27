Import-Module "$env:APPVEYOR_BUILD_FOLDER\.appveyor\Invoke-MsBuild.psm1";

function Publish-Release {
	Param (
		[Parameter(Mandatory=$true)]
		[string] $HostName
	)
	$appId = $env:CI_PUBLISHAPPID;
	$appKey = $env:CI_PUBLISHKEY;


	$url = "http://$HostName/api/update/create/";
	$headers = @{};
	$headers["Authentication-Token"] = $appKey;
	$headers["Application-Identifier"] = $appId;

	$post = @{
		Id = $env:CP_RELEASE_ID;
		Version = [System.Web.HttpUtility]::UrlEncode($env:CI_BUILD_VERSION);
		Description = [System.Web.HttpUtility]::UrlEncode($env:CI_RELEASE_DESCRIPTION);
		Name = [System.Web.HttpUtility]::UrlEncode($env:CP_RELEASE_NAME);
		Url = [System.Web.HttpUtility]::UrlEncode("http://$env:CP_RELEASE_PROJECT.codeplex.com/releases/view/$env:CP_RELEASE_ID");
	};
	$contentType = "application/x-www-form-urlencoded";
	$method = "POST";

	$response = Invoke-WebRequest -Uri $url -Method $method -Headers $headers -TimeoutSec 120 -Body $post -ContentType $contentType -UserAgent "AppVeyor Build Agent";

	return $response;
}

# trigger the codeplex deployment script
if( $env:CI_DEPLOY_CODEPLEX -eq $true ) {
	Invoke-MsBuild -Path "$env:APPVEYOR_BUILD_FOLDER\.appveyor\DeployCodePlex.msbuild" -MsBuildParameters "/verbosity:detailed /p:CI_BUILD_VERSION=$env:CI_BUILD_VERSION /p:CI_BUILD_REVISION=$env:CI_BUILD_REVISION /p:CI_BUILD_MAJOR=$env:CI_BUILD_MAJOR /p:CI_BUILD_MINOR=$env:CI_BUILD_MINOR"
}

# publish release
if( $env:CI_DEPLOY_WEBAPI_RELEASE -eq $true -and $env:Platform -eq "x86" ) {
	# this only gets called for the x86 platform so it is called once, and because the env:vars may not exist for x64
	if( !$env:CP_RELEASE_NAME -or !$env:CP_RELEASE_ID -or !$env:CP_RELEASE_URL ) {
		Write-Host -BackgroundColor Red -ForegroundColor White "Unable to read the required values to create the release";
		$host.SetShouldExit(500);
		return;
	}
	#,$env:ProductionApiDomain
	@($env:DevelopmentApiDomain) | foreach {
		$hostname = $_;
		Write-Host "Publishing Release Information '$env:CP_RELEASE_NAME' to $hostname";
		$resp = Publish-Release -HostName $hostname;
		if($resp.StatusCode -ne 200) {
			Write-Host -BackgroundColor Red -ForegroundColor White $resp.StatusDescription;
			$host.SetShouldExit($resp.StatusCode);
			return;
		}

	}
}