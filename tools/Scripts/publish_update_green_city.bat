<# batch file posh loader
@cls
@echo Starting powershell...
@powershell.exe -NoProfile -command " iex ([System.IO.File]::ReadAllText('%0'))"
@pause
@goto :EOF
#>

Import-Module "..\NugetHelpers\NugetHelpers.psm1" -Force

write-host "Publishing GreenCity.CommonUtils"  -ForegroundColor Blue
PackPublishUpdate -Nuget "GreenCity.CommonUtils" -PathToNuget "..\..\back\Common\" -NugetConfigFilePath "..\..\back\nuget.config" -SlnPath "..\..\back\Common\GreenCity.Infrastructure\GreenCity.Infrastructure.sln"

write-host "Publishing GreenCity.Db"  -ForegroundColor Blue
PackPublishUpdate -Nuget "GreenCity.Db" -PathToNuget "..\..\back\Common\" -NugetConfigFilePath "..\..\back\nuget.config" -SlnPath "..\..\back\Common\GreenCity.Infrastructure\GreenCity.Infrastructure.sln"

write-host "Publishing GreenCity.Http"  -ForegroundColor Blue
PackPublishUpdate -Nuget "GreenCity.Http" -PathToNuget "..\..\back\Common\" -NugetConfigFilePath "..\..\back\nuget.config" -SlnPath "..\..\back\Common\GreenCity.Infrastructure\GreenCity.Infrastructure.sln"

write-host "Publishing GreenCity.Logging"  -ForegroundColor Blue
PackPublishUpdate -Nuget "GreenCity.Logging" -PathToNuget "..\..\back\Common\" -NugetConfigFilePath "..\..\back\nuget.config" -SlnPath "..\..\back\Common\GreenCity.Infrastructure\GreenCity.Infrastructure.sln"
