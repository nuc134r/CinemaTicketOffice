/* Create user/admin */

CREATE LOGIN kiosk1 WITH PASSWORD = 'kiosk-1234'
GO

CREATE USER kiosk1 FOR LOGIN kiosk1
GO
	
EXEC sp_addrolemember N'greenbird_user', N'kiosk1'


/* Create superadmin */

CREATE LOGIN god WITH PASSWORD = 'god-1234'
GO

CREATE USER god FOR LOGIN god
GO	

EXEC sp_addrolemember N'greenbird_superadmin', N'god'
EXEC sp_addrolemember N'db_owner', N'god'
EXEC sp_addrolemember N'db_securityadmin ', N'god'
EXEC sp_addsrvrolemember N'god', N'securityadmin'