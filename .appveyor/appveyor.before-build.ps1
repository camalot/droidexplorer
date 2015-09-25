.\.appveyor\InstallPfx.ps1 -pfx "$env:APPVEYOR_BUILD_FOLDER\Shared\droidexplorer.pfx" -password ((Get-Item Env:\DE_PFX_KEY).Value) -containerName ((Get-Item Env:\VS_PFX_KEY).Value);

Write-Host "PSModulePath: $env:PSModulePath";
# Add the .appveyor folder to the PSModule path
$env:PSModulePath = "$env:PSModulePath;$env:APPVEYOR_BUILD_FOLDER\.appveyor\";
Write-Host "PSModulePath: $env:PSModulePath";
