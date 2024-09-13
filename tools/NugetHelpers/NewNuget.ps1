function NewNuget() {
	param(
		[string] $PathToNuget,
		[string] $SlnPath,
		[string] $Nuget
	)
	
	process {

		function IsExists($path) {
			if(Test-Path $path) { return "+" } else { "-" }
		}

		write-host "Searching paths based on nuget '$Nuget' location..." -ForegroundColor White
		$csprojFileName = "$Nuget.csproj"
		$csproj = $(Get-ChildItem (Split-Path $PathToNuget) -Recurse -Depth 3 -Filter $csprojFileName).FullName
		write-host "Csproj - $csproj"
		$csprojFolder = [System.IO.Path]::GetDirectoryName($csproj)
		$sln = $SlnPath
		write-host    "[$(IsExists $sln)] Solution: $sln"
		write-host "[$(IsExists $csproj)] Csproj:   $csproj"

		if(-not (Test-Path $sln) -or -not (Test-Path $csproj)) {
			return $null
		}

		$isOk = NewNugetPackage -sln $sln -csproj $csproj

		if(-not $isOk) { return $null }

		$packedNuget =
			Get-ChildItem ".\*.nupkg" |
			% { $_.Name } |
			? { $_ -like "$Nuget*.nupkg" } |
			Sort-Object -Descending |
			Select-Object -First 1
		
		if($packedNuget) {
			write-host "Packed package: $packedNuget"  -ForegroundColor White
		} else {
			write-host "Nothing packed!" -ForegroundColor Red
			return $null
		}

		return $packedNuget
	}
}
