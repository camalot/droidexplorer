

choco install -y ilmerge;
choco install -y nuget.commandline;
choco install -y wixtoolset;

$env:PATH = "C:\ProgramData\chocolatey\lib\ilmerge;C:\ProgramData\chocolatey\lib\NuGet.CommandLine\tools;$env:PATH";
# set PATH=C:\ProgramData\chocolatey\lib\ilmerge;C:\ProgramData\chocolatey\lib\NuGet.CommandLine\tools;%PATH%

$cache = "$env:APPVEYOR_BUILD_FOLDER\cache\";
if( !(Test-Path -Path $cache) ) {
	Write-Host "Creating '$cache'";
	New-Item -Path $cache -ItemType Directory -Force;
}