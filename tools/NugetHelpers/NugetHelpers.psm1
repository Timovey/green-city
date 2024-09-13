Get-ChildItem (Split-Path $MyInvocation.MyCommand.Path) -Include *.ps1 -Exclude $MyInvocation.MyCommand.Name -Recurse |
	ForEach-Object { . $_.FullName }
