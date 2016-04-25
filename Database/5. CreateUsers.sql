SET NOCOUNT ON;

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'birdadmin') 
CREATE USER birdadmin FOR LOGIN birdadmin
GO
EXEC sp_addrolemember N'greenbird_admin', N'birdadmin'

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'birduser') 
CREATE USER birduser FOR LOGIN birduser
GO
EXEC sp_addrolemember N'greenbird_user', N'birduser'

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'birdsuperadmin') 
CREATE USER birdsuperadmin FOR LOGIN birdsuperadmin
GO
EXEC sp_addrolemember N'greenbird_superadmin', N'birdsuperadmin'
EXEC sp_addrolemember N'db_securityadmin', N'birdsuperadmin'
EXEC sp_addrolemember N'db_owner', N'birdsuperadmin'
EXEC sp_addsrvrolemember N'birdsuperadmin', N'securityadmin'