cd %~dp0\Administration

msbuild Administration.csproj /p:Configuration=Release /t:publish

@PAUSE