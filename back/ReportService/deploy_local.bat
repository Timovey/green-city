<# batch file posh loader
@cls
@echo Starting powershell...
@powershell.exe -NoProfile -command " iex ([System.IO.File]::ReadAllText('%0'))"
@pause
@goto :EOF
#>

write-host "Setting env" -ForegroundColor Blue;

$Env:ASPNETCORE_ENVIRONMENT = "Development";

write-host "New env: " -NoNewline;
write-host $Env:ASPNETCORE_ENVIRONMENT -ForegroundColor Green;

write-host "Cleaning... " -ForegroundColor Blue;
dotnet clean;

if(Test-Path -Path "app") {
	rm -r app;
}

write-host "Running... " -ForegroundColor Blue;
dotnet run --project ReportService.Main/ReportService.Main.csproj -c Release --launch-profile ReportService.Main