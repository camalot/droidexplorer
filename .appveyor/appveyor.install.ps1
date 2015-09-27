choco install -y ilmerge;
choco install -y nuget.commandline;
choco install -y wixtoolset;

$env:PATH = "C:\ProgramData\chocolatey\lib\ilmerge;C:\ProgramData\chocolatey\lib\NuGet.CommandLine\tools;$env:PATH";