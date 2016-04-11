cd %~dp0\KioskClient

msbuild KioskClient.csproj /p:Configuration=Release /t:publish

@PAUSE