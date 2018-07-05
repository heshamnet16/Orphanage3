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
GO

USE [OrphansDB]
GO

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
GO

update Users set Password='$OrphanHASH$V1$10000$Q1QQ8svGPImwbLr7nHqYEqABMUBm9DjT+94ArcYuISS5K4zuUlQ0wI2eSTNCjhBpkhfbZ2zFxMw=' where ID =1
GO

ALTER TABLE USERS
ALTER COLUMN USERNAME nvarchar(450);
GO

IF OBJECT_ID('dbo.[USER_NAME]', 'UQ') IS NOT NULL
begin 
    ALTER TABLE dbo.[USERS] DROP CONSTRAINT USER_NAME
end
GO

ALTER TABLE USERS
ADD CONSTRAINT USER_NAME UNIQUE (USERNAME);
GO

ALTER TABLE Mothers
ALTER COLUMN Dieday datetime2;
GO

ALTER TABLE Orphans
ALTER COLUMN Weight int;
GO

ALTER TABLE Orphans
ALTER COLUMN Tallness int;
GO

ALTER TABLE BondsMen
ALTER COLUMN  IdentityCard_ID nvarchar(30);
GO

ALTER TABLE Fathers
ALTER COLUMN  IdentityCard_ID nvarchar(30);
GO

ALTER TABLE Mothers
ALTER COLUMN  IdentityCard_ID nvarchar(30);
GO

ALTER TABLE Orphans
ALTER COLUMN  IdentityNumber nvarchar(30);
GO

ALTER TABLE [dbo].[Bails] DROP CONSTRAINT [Supporter_Bail]
GO

ALTER TABLE Bails
ALTER COLUMN Supporter_ID  int NOT NULL;
GO

ALTER TABLE [dbo].[Bails]  WITH CHECK ADD  CONSTRAINT [Supporter_Bail] FOREIGN KEY([Supporter_ID])
REFERENCES [dbo].[Supporters] ([ID])
GO

ALTER TABLE [dbo].[Bails] CHECK CONSTRAINT [Supporter_Bail]
GO

update [OrphansDB].[dbo].[Addresses] set Work_phone = NULL where Work_phone = '(000)0000-000' Or Work_phone = '(031)0000-000' or Work_phone = '(0000)000-000' or Work_phone='(____)___-___'
GO

update [OrphansDB].[dbo].[Addresses] set Fax = NULL where fax = '(000)0000-000' Or fax = '(031)0000-000' or fax = '(0000)000-000' or fax='(____)___-___'
GO

update [OrphansDB].[dbo].[Addresses] set Cell_Phone = NULL where Cell_Phone = '(000)0000-000' Or Cell_Phone = '(031)0000-000' or Cell_Phone = '(0000)000-000' or Cell_Phone='(____)___-___'
GO

update [OrphansDB].[dbo].[Addresses] set Home_Phone = NULL where Home_Phone = '(000)0000-000' Or Home_Phone = '(031)0000-000' or Home_Phone = '(0000)000-000' or Home_Phone='(____)___-___'
GO

update [OrphansDB].dbo.Fathers set IdentityCard_ID = NULL where IdentityCard_ID = '' or IdentityCard_ID = '0'
GO

update [OrphansDB].dbo.Orphans set IdentityNumber = NULL where IdentityNumber = '' or IdentityNumber = '0'
GO

update [OrphansDB].dbo.Mothers set IdentityCard_ID = NULL where IdentityCard_ID = '' or IdentityCard_ID = '0'
GO

update [OrphansDB].dbo.BondsMen set IdentityCard_ID = NULL where IdentityCard_ID = '' or IdentityCard_ID = '0'
GO

