<# batch file posh loader
@cls
@echo Starting powershell...
@powershell.exe -NoProfile -command " iex ([System.IO.File]::ReadAllText('%0'))"
@pause
@goto :EOF
#>

docker-compose -f docker-compose.dev.yml --env-file .env-local up -d --build