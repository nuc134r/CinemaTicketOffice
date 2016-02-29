SET NOCOUNT ON;

CREATE LOGIN adminuser WITH PASSWORD = 'admin'
GO
CREATE USER adminuser FOR LOGIN adminuser
GO

GRANT CONNECT TO adminuser
GO
GRANT EXECUTE ON dbo.ListMovies TO adminuser
GO
GRANT EXECUTE ON dbo.MovieDetails TO adminuser
GO