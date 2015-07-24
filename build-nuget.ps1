[CmdletBinding()]
Param(
    [Parameter(Mandatory=$True,Position=1)]
    [string]$version
)

function Set-Version
{
    Param( [string]$nuspec, [string]$version )
    $path = [System.IO.Path]::GetFullPath((Join-Path (pwd) $nuspec))
    $xml = [xml](Get-Content $path)
    $xml.package.metadata.version = $version
    $xml.Save($path)
}

function Set-Dependency
{
    Param( [string]$nuspec, [string]$dependency, [string]$version )
    $path = [System.IO.Path]::GetFullPath((Join-Path (pwd) $nuspec))
    $xml = [xml](Get-Content $path)
    $dependencies = $xml.package.metadata.dependencies.GetElementsByTagName('dependency') | where { $_.id -eq $dependency }
    ForEach($item in $dependencies) {
        $item.version = $version
    }
    $xml.Save($path)
}

Set-Version 'PugTrace.nuspec' $version
Set-Version 'PugTrace.SqlServer.nuspec' $version
Set-Dependency 'PugTrace.SqlServer.nuspec' 'PugTrace' $version

New-Item (Join-Path (pwd) 'build') -type directory -force
.\nuget.exe pack PugTrace.nuspec -OutputDirectory build
.\nuget.exe pack PugTrace.SqlServer.nuspec -OutputDirectory build