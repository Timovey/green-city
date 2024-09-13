function RepairLatestNuget() {
	
param(
	[string] $Path = ".",
	[string[]] $PackageIds = $null,
	[string] $NugetConfigFilePath,
	[string] $NugetName, #Example: GreenCity.CommonInfrastructure.1.0.32.nupkg
	[switch] $WhatIf
)

process {

		function Get-Nugets {
			$result = @()
			
			($name, $version) = $NugetName -split '.(\d+.\d+.\d+).nupkg'
			$result += @{
				"Name" = $name
				"Version" = $version[0]
			}		

			return $result
		}
		
		function Get-FileContent($file) {
			return [System.IO.File]::ReadAllText($file, [System.Text.Encoding]::UTF8)
		}	
		
		function Set-FileContent($file, $content) {
			[System.IO.File]::WriteAllText($file, $content, [System.Text.Encoding]::UTF8)
		}
	
		function Update-PackagesConfig($nugets, $file) {
			Write-Host -NoNewline "> Processing: $file"
			$content = Get-FileContent $file
			
			$nugets | % {

				write-host -NoNewline "."
				$search1  = "<package\s+id=`"$($_.Name)`"\s+version=`"\d+\.\d+\.\d+`""
				$replace1 = "<package id=`"$($_.Name)`" version=`"$($_.Version)`""
				$content = $content -replace $search1, $replace1

			}
			
			Set-FileContent $file $content
			write-host " OK!"
		}
	
		function Update-CsProj($nugets, $file) {
			Write-Host -NoNewline "> Processing: $file"
			$content = Get-FileContent $file
			$nugets | % {
				write-host -NoNewline "."
				$search1  = "<PackageReference\s+Include=`"$($_.Name)`"\s+Version=`"\d+\.\d+\.\d+`""
				$replace1 = "<PackageReference Include=`"$($_.Name)`" Version=`"$($_.Version)`""
				$content = $content -replace $search1, $replace1
				$search2  = "(<HintPath>.*?\\packages\\.*?$($_.Name))\.\d+\.\d+\.\d+"
				$replace2 = "`$1.$($_.Version)"
				$content = $content -replace $search2, $replace2
			}
	
			Set-FileContent $file $content
			write-host " OK!"
		}

		$nugets = Get-Nugets
		if($PackageIds) {
			$nugets = $nugets | ? { $_.Name -in $PackageIds }
		}
	
		write-host "> Will perform update: $($nugets | % { "$($_.Name) $($_.Version)" })" -ForegroundColor Green

		if(-not $WhatIf.IsPresent) {
			Get-ChildItem -Path $Path -Recurse -Depth 3 | % {
				$filename = $_.FullName
				if($filename -like "*packages.config") {
					Update-PackagesConfig $nugets $filename
				}
				elseif($filename -like "*.csproj") {
					Update-CsProj $nugets $filename
				}
			}
		}

		write-host "> Done!"
	}
}
