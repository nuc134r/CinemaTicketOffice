SET NOCOUNT ON;

/* admin */

CREATE LOGIN birdadmin WITH PASSWORD = 'admin-1234'
GO

/* user */

CREATE LOGIN birduser WITH PASSWORD = 'user-1234'
GO

/* superadmin */

CREATE LOGIN birdsuperadmin WITH PASSWORD = 'superadmin-1234'
GO
CREATE USER birdsuperadmin FOR LOGIN birdsuperadmin
GO
EXEC sp_addrolemember N'loginmanager', N'birdsuperadmin'