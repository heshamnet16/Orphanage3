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
-- guarantor constraint
ALTER TABLE [dbo].[Orphans] DROP CONSTRAINT [Supporter_Orphan]
GO

ALTER TABLE [dbo].[Orphans]  WITH CHECK ADD  CONSTRAINT [Supporter_Orphan] FOREIGN KEY([Supporter_ID])
REFERENCES [dbo].[Supporters] ([ID]) ON DELETE CASCADE 
GO

ALTER TABLE [dbo].[Orphans] CHECK CONSTRAINT [Supporter_Orphan]
GO

-- careviers table address constraint
ALTER TABLE [dbo].[BondsMen] DROP CONSTRAINT [Address_BondsMan]
GO

ALTER TABLE [dbo].[BondsMen]  WITH CHECK ADD  CONSTRAINT [Address_BondsMan] FOREIGN KEY([Address_ID])
REFERENCES [dbo].[Addresses] ([ID]) On Delete cascade
GO

ALTER TABLE [dbo].[BondsMen] CHECK CONSTRAINT [Address_BondsMan]
GO

-- caregivers table name constraint
ALTER TABLE [dbo].[BondsMen] DROP CONSTRAINT [Name_BondsMan]
GO

ALTER TABLE [dbo].[BondsMen]  WITH CHECK ADD  CONSTRAINT [Name_BondsMan] FOREIGN KEY([Name_Id])
REFERENCES [dbo].[Names] ([ID]) on delete cascade
GO

ALTER TABLE [dbo].[BondsMen] CHECK CONSTRAINT [Name_BondsMan]
GO

--delete caregivers
delete from BondsMen
from BondsMen as B inner join Orphans as O on  B.ID = O.BondsMan_ID
where o.IsExcluded=1
go



delete from Orphans where IsExcluded = 1



