Import-Module "$env:APPVEYOR_BUILD_FOLDER\.appveyor\modules\Invoke-MsBuild.psm1";
Import-Module "$env:APPVEYOR_BUILD_FOLDER\.appveyor\modules\Send-PushbulletMessage.psm1";

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

	if(Test-Path -Path "$env:APPVEYOR_BUILD_FOLDER\.build\publishchangelog.txt") {
		# read the publish text file
		$publishNotes = (Get-Content -Path "$env:APPVEYOR_BUILD_FOLDER\.build\publishchangelog.txt") | Out-String;
	}


	$post = @{
		Id = $env:CP_RELEASE_ID;
		Version = $env:CI_BUILD_VERSION;
		Description = $publishNotes;
		Name = $env:CP_RELEASE_NAME;
		Url = "http://$env:CP_RELEASE_PROJECT.codeplex.com/releases/view/$env:CP_RELEASE_ID";
	};
	$contentType = "application/x-www-form-urlencoded";
	$method = "POST";

	$response = Invoke-WebRequest -Uri $url -Method $method -Headers $headers -TimeoutSec 120 -Body $post -ContentType $contentType -UserAgent "AppVeyor Build Agent";

	return $response;
}

# trigger the codeplex deployment script
if( $env:CI_DEPLOY_CODEPLEX -eq $true ) {
	Invoke-MsBuild -Path "$env:APPVEYOR_BUILD_FOLDER\.appveyor\DeployCodePlex.msbuild" -MsBuildParameters "/verbosity:detailed /p:CI_BUILD_VERSION=$env:CI_BUILD_VERSION /p:CI_BUILD_REVISION=$env:CI_BUILD_REVISION /p:CI_BUILD_MAJOR=$env:CI_BUILD_MAJOR /p:CI_BUILD_MINOR=$env:CI_BUILD_MINOR"
} else {
	Write-Host -BackgroundColor Yellow -ForegroundColor Black "Skip `"CodePlex`" deployment as environment variable has not matched (`"CI_DEPLOY_CODEPLEX`" is `"$false`", should be `"$true`")";
}

# publish release
if( $env:CI_DEPLOY_WEBAPI_RELEASE -eq $true -and $env:Platform -eq "x86" ) {
	# this only gets called for the x86 platform so it is called once, and because the env:vars may not exist for x64
	if( !$env:CP_RELEASE_NAME -or !$env:CP_RELEASE_ID -or !$env:CP_RELEASE_URL ) {
		Write-Host -BackgroundColor Red -ForegroundColor White "Unable to read the required values to create the release";
		$host.SetShouldExit(500);
		return;
	}
	@($env:DevelopmentApiDomain,$env:ProductionApiDomain) | foreach {
		$hostname = $_;
		Write-Host "[WebApiRelease] Publishing Release Information '$env:CP_RELEASE_NAME' to $hostname";
		$resp = Publish-Release -HostName $hostname;
		if($resp.StatusCode -ne 200) {
			Write-Host -BackgroundColor Red -ForegroundColor White $resp.StatusDescription;
			$host.SetShouldExit($resp.StatusCode);
			return;
		}

	}
} else {
	Write-Host -BackgroundColor Yellow -ForegroundColor Black "Skip `"WebApiRelease`" deployment as environment variable has not matched (`"CI_DEPLOY_CODEPLEX`" is `"$false`", should be `"$true`")";
}

if($env:PUSHBULLET_API_TOKEN) {
	$timestamp = (Get-Date).ToUniversalTime().ToString("MM/dd/yyyy hh:mm:ss");
	# this allows for multiple tokens, just separate with a comma.
	$env:PUSHBULLET_API_TOKEN.Split(",") | foreach {
		$pbtoken = $_;
		try {
			# Send a pushbullet message if there is an api token available
			Send-PushbulletMessage -apiKey $pbtoken -Type Message -Title "[Build] Droid Explorer $env:Platform v$env:CI_BUILD_VERSION Build Finished" -msg ("Build completed at $timestamp UTC");

			if( $env:Platform -eq "x64" -and $env:CI_DEPLOY_PUSHBULLET -eq $true) {
				Send-PushbulletMessage -apiKey $pbtoken -Type Message -Title "[Deploy] Droid Explorer v$env:CI_BUILD_VERSION Deployed" -msg ("Deployment completed at $timestamp UTC");
			} else {
				Write-Host -BackgroundColor Yellow -ForegroundColor Black "Skip `"PushBullet`" deployment as environment variable has not matched (`"CI_DEPLOY_PUSHBULLET`" is `"$false`", should be `"$true`" and `"Platform`" is `"$env:Platform`", should be `"x64`")";
			}		
		} catch [Exeption] {
			Write-Error ($_ -replace $pbtoken, "[`$env:PUSHBULLET_API_TOKEN]");
		}
	}
}
