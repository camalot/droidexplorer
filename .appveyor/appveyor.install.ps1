choco install -y ilmerge --version 2.12.0803;
choco install -y nuget.commandline --version 3.3.0;
choco install -y wixtoolset --version 3.10.2.2516;

$env:PATH = "C:\ProgramData\chocolatey\lib\ilmerge;C:\ProgramData\chocolatey\lib\NuGet.CommandLine\tools;$env:PATH";