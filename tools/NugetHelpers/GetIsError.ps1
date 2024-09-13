function Get-IsError() {
	param()
	process {	
		if ($error) {
			write-host "[NugetHelpers:Error] $error" -ForegroundColor Red
			return $true
		}
		if ($LastExitCode -ne 0) {
			write-host "[NugetHelpers:Error] LastExitCode: $LastExitCode" -ForegroundColor Red
			return $true
		}
		return $false
	}
}