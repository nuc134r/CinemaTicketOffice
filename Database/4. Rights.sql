SET NOCOUNT ON;

/********************************
 *			  Admin				*
 ********************************/

GRANT EXECUTE ON dbo.BrowseMovies TO adminuser
GO
GRANT EXECUTE ON dbo.MovieDetails TO adminuser
GO

/********************************
 *			  Kiosk				*
 ********************************/

GRANT EXECUTE ON dbo.BrowseMovies TO kioskuser
GO
GRANT EXECUTE ON dbo.MovieDetails TO kioskuser
GO
GRANT EXECUTE ON dbo.ListGenres TO kioskuser
GO