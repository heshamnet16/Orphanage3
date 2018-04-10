$ServerName = "."

invoke-sqlcmd -ServerInstance $ServerName -inputFile "alter database cloumns.sql" | out-File -filepath "TestOutput.txt"