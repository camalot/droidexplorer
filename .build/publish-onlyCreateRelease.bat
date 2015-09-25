call "C:\Program Files (x86)\Microsoft Visual Studio 14.0\VC\vcvarsall.bat" x86
msbuild OnlyCreateRelease.msbuild /p:PublishMode=Publish /p:PublishKey=d0879a20-bc38-4dda-831e-e4d80a93db46 /p:PublishAppId=d0879a20-bc38-4dda-831e-e4d80a93db45 /p:CanPublish=True /p:Platform=x86
pause