# import-module "sqlps" -DisableNameChecking #...Here it not required.
$ServerName = "."
invoke-sqlcmd -ServerInstance $ServerName -inputFile "F:\Repo\Orphanage3\Orphanage3\Output\OrphanageService\alter database cloumns.sql" | out-File -filepath "TestOutput.txt"
SC.exe STOP OrphanageService
SC.exe DELETE OrphanageService
SC.exe CREATE OrphanageService DisplayName= "Orphanage Service" binpath= "F:\Repo\Orphanage3\Orphanage3\Output\OrphanageService\OrphanageService.exe" start= auto
