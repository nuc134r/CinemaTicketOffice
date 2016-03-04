﻿SET NOCOUNT ON;

/********************************
 *			  Admin				*
 ********************************/

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'adminuser') 
CREATE LOGIN adminuser WITH PASSWORD = 'admin'
GO
CREATE USER adminuser FOR LOGIN adminuser
GO

GRANT CONNECT TO adminuser
GO

/********************************
 *			  Kiosk				*
 ********************************/

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'kioskuser') 
CREATE LOGIN kioskuser WITH PASSWORD = 'kiosk'
GO
CREATE USER kioskuser FOR LOGIN kioskuser
GO

GRANT CONNECT TO kioskuser
GO