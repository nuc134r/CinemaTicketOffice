SET NOCOUNT ON;

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'adminuser') 
CREATE LOGIN adminuser WITH PASSWORD = 'admin'
GO
CREATE USER adminuser FOR LOGIN adminuser
GO

GRANT CONNECT TO adminuser
GO
GRANT EXECUTE ON dbo.BrowseMovies TO adminuser
GO
GRANT EXECUTE ON dbo.MovieDetails TO adminuser
GO