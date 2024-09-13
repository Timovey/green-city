function PackPublishUpdate() {
	
	param(
		[string] $PathToNuget,
		[string] $NugetConfigFilePath,
		[string] $Nuget,
		[string] $SlnPath
	)
	
	process {
		
		if(-not $Nuget) {
			write-host "EMPTY NUGET PARAM!!!" -ForegroundColor Red
			return
		}

		# 1) Packing
		$packedNuget = NewNuget -Nuget $Nuget -PathToNuget $PathToNuget -SlnPath $SlnPath
		if(-not $packedNuget) {
			write-host "NUGET NOT FOUND!!!" -ForegroundColor Red
			return
		}

		# 2) Publishing
		if($packedNuget) {
			write-host "Publishing package: $packedNuget"  -ForegroundColor White
			nuget.exe push -ConfigFile "$NugetConfigFilePath" -Source "greenCityNugets" $packedNuget
			if(Get-IsError) { return }
		} else {
			write-host "Nothing to publish!" -ForegroundColor Red
		}

		# 3) Updating to latest
		RepairLatestNuget -Path "../../back/" -PackageIds $Nuget -NugetConfigFilePath $NugetConfigFilePath -NugetName $packedNuget
		
		# 4) Deleting nuget
		Remove-Item * -Include *.nupkg
	}	
}
