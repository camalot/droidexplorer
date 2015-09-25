.\.appveyor\InstallPfx.ps1 -pfx "$env:APPVEYOR_BUILD_FOLDER\Shared\droidexplorer.pfx" -password ((Get-Item Env:\DE_PFX_KEY).Value) -containerName ((Get-Item Env:\VS_PFX_KEY).Value);
