SET NOCOUNT ON;

/* admin */

IF NOT EXISTS 
    (SELECT name  
     FROM master.sys.server_principals
     WHERE name = 'birdadmin')
BEGIN
    CREATE LOGIN birdadmin WITH PASSWORD = 'admin-1234';
END

/* user */

IF NOT EXISTS 
    (SELECT name  
     FROM master.sys.server_principals
     WHERE name = 'birduser')
BEGIN
	CREATE LOGIN birduser WITH PASSWORD = 'user-1234';
END

/* superadmin */

IF NOT EXISTS 
    (SELECT name  
     FROM master.sys.server_principals
     WHERE name = 'birdsuperadmin')
BEGIN
	CREATE LOGIN birdsuperadmin WITH PASSWORD = 'superadmin-1234';
END