use OrphansDB;
GO

--caregiver constraint
ALTER TABLE dbo.Orphans
   DROP CONSTRAINT BondsMan_Orphan
Go
   
ALTER TABLE dbo.Orphans
   ADD CONSTRAINT BondsMan_Orphan
   FOREIGN KEY ([BondsMan_ID]) REFERENCES [dbo].[BondsMen] ([ID]) ON DELETE CASCADE
Go

   --bail constraint
ALTER TABLE dbo.Orphans
   DROP CONSTRAINT Bail_Orphan
Go

ALTER TABLE dbo.Orphans
   ADD CONSTRAINT Bail_Orphan
   FOREIGN KEY ([Bail_ID]) REFERENCES [dbo].[Bails] ([ID]) ON DELETE CASCADE
Go

   --family constraint
   ALTER TABLE [dbo].[Orphans] DROP CONSTRAINT [Family_Orphan]
GO

ALTER TABLE [dbo].[Orphans]  WITH CHECK ADD  CONSTRAINT [Family_Orphan] FOREIGN KEY([Family_ID])
REFERENCES [dbo].[Famlies] ([ID]) ON DELETE CASCADE 
GO

ALTER TABLE [dbo].[Orphans] CHECK CONSTRAINT [Family_Orphan]
GO

--health constraint
ALTER TABLE [dbo].[Orphans] DROP CONSTRAINT [Healthy_Orphan]
GO

ALTER TABLE [dbo].[Orphans]  WITH CHECK ADD  CONSTRAINT [Healthy_Orphan] FOREIGN KEY([Health_ID])
REFERENCES [dbo].[Healthies] ([ID]) ON DELETE CASCADE 
GO

ALTER TABLE [dbo].[Orphans] CHECK CONSTRAINT [Healthy_Orphan]
GO

--Name Constraint 
ALTER TABLE [dbo].[Orphans] DROP CONSTRAINT [Name_Orphan]
GO

ALTER TABLE [dbo].[Orphans]  WITH CHECK ADD  CONSTRAINT [Name_Orphan] FOREIGN KEY([Name])
REFERENCES [dbo].[Names] ([ID]) ON DELETE CASCADE 
GO

ALTER TABLE [dbo].[Orphans] CHECK CONSTRAINT [Name_Orphan]
GO

-- Study constraint
ALTER TABLE [dbo].[Orphans] DROP CONSTRAINT [Study_Orphan]
GO

ALTER TABLE [dbo].[Orphans]  WITH CHECK ADD  CONSTRAINT [Study_Orphan] FOREIGN KEY([Education_ID])
REFERENCES [dbo].[Studies] ([ID]) ON DELETE CASCADE 
GO

ALTER TABLE [dbo].[Orphans] CHECK CONSTRAINT [Study_Orphan]
GO

-- careviers table address constraint
ALTER TABLE [dbo].[BondsMen] DROP CONSTRAINT [Address_BondsMan]
GO

ALTER TABLE [dbo].[BondsMen]  WITH CHECK ADD  CONSTRAINT [Address_BondsMan] FOREIGN KEY([Address_ID])
REFERENCES [dbo].[Addresses] ([ID]) on delete cascade
GO

ALTER TABLE [dbo].[BondsMen] CHECK CONSTRAINT [Address_BondsMan]
GO


-- mother table name constraint
ALTER TABLE [dbo].[Mothers] DROP CONSTRAINT [Name_Mother]
GO

ALTER TABLE [dbo].[Mothers]  WITH CHECK ADD  CONSTRAINT [Name_Mother] FOREIGN KEY([Name_Id])
REFERENCES [dbo].[Names] ([ID]) on delete cascade
GO

ALTER TABLE [dbo].[Mothers] CHECK CONSTRAINT [Name_Mother]
GO

-- mother table address constraint
ALTER TABLE [dbo].[Mothers] DROP CONSTRAINT [Address_Mother]
GO

ALTER TABLE [dbo].[Mothers]  WITH CHECK ADD  CONSTRAINT [Address_Mother] FOREIGN KEY([Address_ID])
REFERENCES [dbo].[Addresses] ([ID]) on delete cascade
GO

ALTER TABLE [dbo].[Mothers] CHECK CONSTRAINT [Address_Mother]
GO

-- father table name constraint
ALTER TABLE [dbo].[Fathers] DROP CONSTRAINT [Name_Father]
GO

ALTER TABLE [dbo].[Fathers]  WITH CHECK ADD  CONSTRAINT [Name_Father] FOREIGN KEY([Name_ID])
REFERENCES [dbo].[Names] ([ID]) on delete cascade
GO

ALTER TABLE [dbo].[Fathers] CHECK CONSTRAINT [Name_Father]
GO


delete from Orphans where IsExcluded = 1
go

delete from BondsMen where ID not in (select Orphans.BondsMan_ID from orphans)

delete from Famlies where id not in (select Orphans.Family_ID from Orphans)

delete from Fathers where ID not in (select Famlies.Father_Id from Famlies)

delete from Mothers where ID not in (select Famlies.Mother_ID from Famlies)