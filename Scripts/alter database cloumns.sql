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
