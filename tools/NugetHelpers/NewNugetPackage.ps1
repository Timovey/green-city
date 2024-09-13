function NewNugetPackage() {
	
	param(
		[string] $sln,
		[string] $csproj
	)
	
	process {

		Set-StrictMode -Version Latest
		$ErrorActionPreference = "Stop"
		$PSDefaultParameterValues['*:ErrorAction']='Stop'

		function Get-Version([xml] $xml) {
			return $xml.Project.PropertyGroup.Version
		}
		
		function Set-Version([xml] $xml, [string] $version) {
			$xml.Project.PropertyGroup.Version = $version
			$xml.Save($csproj)
		}

		function Increase-Version() {
			[xml] $xml = Get-Content $csproj -Encoding "UTF8"
			$old_version = Get-Version $xml
			[int]$major, [int]$minor, [int]$rev, [int]$rev2 = $old_version.Split('.')
			$new_version = ($major, $minor, ($rev + 1)) -join '.'
			Set-Version $xml $new_version

			write-host "[New-NugetPackage] Packing '$csproj'" -ForegroundColor Green
			write-host "[New-NugetPackage] Current version: $old_version" -ForegroundColor Green
			write-host "[New-NugetPackage] New version: $new_version" -ForegroundColor Green
			write-host "[New-NugetPackage] Packing $csproj, with version: $new_version, using: $csproj" -ForegroundColor Green

			return $old_version
		}
		
		function Revert-Version($version) {
			[xml] $xml = Get-Content $csproj -Encoding "UTF8"
			write-host "[New-NugetPackage] Reverting version to: $version" -ForegroundColor Green
			Set-Version $xml $version
		}

		function Remove-ExistingNugetFiles() {
			Write-Host "[New-NugetPackage] Remove existing nuget files..." -ForegroundColor Green
			dir "*.nupkg" |
				% {
					Write-Host "[New-NugetPackage] Removed: $($_.FullName)" -ForegroundColor Green
					Remove-Item $_.FullName
				}
		}
		
		function Pack-NugetPackage {
			try {
				Write-Host "Restoring... $sln"
				nuget restore $sln  -Verbosity Quiet | Write-Host -ForegroundColor DarkGray
				if(Get-IsError) { return $false }
				Write-Host "Packing..."
				nuget pack $csproj -Build -NoPackageAnalysis -Properties Configuration=Release -Properties Platform=x64 -Verbosity Quiet | Write-Host -ForegroundColor DarkGray
				if(Get-IsError) { return $false }

				return $true
			} catch {
				Write-Warning "$_"
				return $false
			}
		}

		Remove-ExistingNugetFiles

		$old_version = Increase-Version
		$is_ok = Pack-NugetPackage

		write-host "[New-NugetPackage] is_ok: $is_ok" -ForegroundColor Green
		if($is_ok) {
			write-host "[New-NugetPackage] Done!" -ForegroundColor Green
		} else {
			Revert-Version $old_version
		}

		return $is_ok
	}
}
