# import-module "sqlps" -DisableNameChecking #...Here it not required.

$ServerName = "."

invoke-sqlcmd -ServerInstance $ServerName -inputFile "alter database cloumns.sql" | out-File -filepath "TestOutput.txt"

SC CREATE OrphanageService Displayname= "OrphanageService" binpath= "srvstart.exe OrphanageService -c C:OrphanageService.ini" start= auto