USE [master]
GO
-- drop if exist and create the user login
IF EXISTS (SELECT loginname FROM master.dbo.syslogins WHERE NAME = 'OrphansApp')
BEGIN
	DROP LOGIN [OrphansApp]
END
GO
BEGIN 
	CREATE LOGIN [OrphansApp] WITH PASSWORD=N'OrphansApp3', CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
	ALTER LOGIN [OrphansApp] ENABLE
	EXEC sp_addsrvrolemember 'OrphansApp', 'dbcreator'
END

USE [OrphansDB]
IF EXISTS (SELECT * FROM sys.database_principals WHERE name = 'OrphansApp' AND type = 'S')
BEGIN
	DROP USER [OrphansApp]
END
GO

BEGIN
	CREATE USER [OrphansApp] FOR LOGIN [OrphansApp] WITH DEFAULT_SCHEMA=[OrphansDB]
	EXEC sp_addrolemember 'db_owner', 'OrphansApp'
END
GO

use OrphansDB;

ALTER TABLE Mothers
ALTER COLUMN Dieday datetime2;

ALTER TABLE Orphans
ALTER COLUMN Weight int;

ALTER TABLE Orphans
ALTER COLUMN Tallness int;

ALTER TABLE BondsMen
ALTER COLUMN  IdentityCard_ID nvarchar(30);

ALTER TABLE Fathers
ALTER COLUMN  IdentityCard_ID nvarchar(30);

ALTER TABLE Mothers
ALTER COLUMN  IdentityCard_ID nvarchar(30);

ALTER TABLE Orphans
ALTER COLUMN  IdentityNumber nvarchar(30);

Go

update [OrphansDB].[dbo].[Addresses] set Work_phone = NULL where Work_phone = '(000)0000-000' Or Work_phone = '(031)0000-000' or Work_phone = '(0000)000-000' or Work_phone='(____)___-___'
GO

update [OrphansDB].[dbo].[Addresses] set Fax = NULL where fax = '(000)0000-000' Or fax = '(031)0000-000' or fax = '(0000)000-000' or fax='(____)___-___'
GO

update [OrphansDB].[dbo].[Addresses] set Cell_Phone = NULL where Cell_Phone = '(000)0000-000' Or Cell_Phone = '(031)0000-000' or Cell_Phone = '(0000)000-000' or Cell_Phone='(____)___-___'
GO

update [OrphansDB].[dbo].[Addresses] set Home_Phone = NULL where Home_Phone = '(000)0000-000' Or Home_Phone = '(031)0000-000' or Home_Phone = '(0000)000-000' or Home_Phone='(____)___-___'
GO
