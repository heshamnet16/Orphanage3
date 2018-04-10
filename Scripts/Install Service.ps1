function Resolve-FullPath{
    param
    (
        [Parameter(
            Mandatory=$true,
            Position=0,
            ValueFromPipeline=$true)]
        [string] $path
    )
     
    $ExecutionContext.SessionState.Path.GetUnresolvedProviderPathFromPSPath($path)
}

$path = Resolve-FullPath "..\Output\OrphanageService\OrphanageService.exe"

SC.exe CREATE OrphanageService DisplayName= $path binpath= $path start= auto