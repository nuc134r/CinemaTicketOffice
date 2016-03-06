SET NOCOUNT ON;

IF OBJECT_ID('dbo.BrowseMovies', 'P') IS NOT NULL DROP PROCEDURE [BrowseMovies]
IF OBJECT_ID('dbo.ListGenres', 'P')	IS NOT NULL DROP PROCEDURE [ListGenres]
IF OBJECT_ID('dbo.ListAgeLimits', 'P')	IS NOT NULL DROP PROCEDURE [ListAgeLimits]
IF OBJECT_ID('dbo.MovieDetails', 'P') IS NOT NULL DROP PROCEDURE [MovieDetails]
IF OBJECT_ID('dbo.CreateMovie', 'P') IS NOT NULL DROP PROCEDURE [CreateMovie]
GO

CREATE PROCEDURE dbo.BrowseMovies
AS 
    SET NOCOUNT ON;

    SELECT 
		Id,
		Title,
		Plot,
		Duration,
		ReleaseDate
    FROM 
		Movie
	ORDER BY
		Id
GO

CREATE PROCEDURE dbo.ListGenres
AS 
    SET NOCOUNT ON;

    SELECT 
		Id,
		Name
    FROM 
		Genre
	ORDER BY
		Id
GO

CREATE PROCEDURE dbo.ListAgeLimits
AS 
    SET NOCOUNT ON;

    SELECT 
		Id,
		Limit
    FROM 
		AgeLimit
	ORDER BY
		Id
GO

CREATE PROCEDURE dbo.MovieDetails
	@MovieId INT
AS 
    SET NOCOUNT ON;

    SELECT Poster
    FROM Movie
	WHERE Movie.Id = @MovieId

	SELECT [L].Id, [L].Limit
    FROM Movie AS [M]
	INNER JOIN AgeLimit AS [L]
	ON [M].AgeLimitId = [L].Id

	SELECT Id, Name
    FROM MovieGenres
	INNER JOIN Genre
	ON Id = GenreId
	WHERE MovieId = @MovieId
	
	SELECT ShowtimeDate
	FROM Showtime
	WHERE MovieId = @MovieId
GO

CREATE PROCEDURE dbo.CreateMovie
	@Title NVARCHAR (128),
	@Plot NVARCHAR (4000),
	@Duration SMALLINT,
	@Poster IMAGE,
	@Genres IdList READONLY,
	@ReleaseDate DATE,
	@AgeLimitId INT
AS 
    SET NOCOUNT ON;

	INSERT INTO Movie 
	(
		Title, 
		Plot, 
		Duration, 
		Poster, 
		ReleaseDate, 
		AgeLimitId
	)
	SELECT
		@Title,
		@Plot,
		@Duration,
		@Poster,
		@ReleaseDate,
		@AgeLimitId
GO

/********************************
 *			  Admin				*
 ********************************/

GRANT EXECUTE ON dbo.BrowseMovies TO adminuser
GO
GRANT EXECUTE ON dbo.MovieDetails TO adminuser
GO
GRANT EXECUTE ON dbo.ListAgeLimits TO adminuser
GO
GRANT EXECUTE ON dbo.ListGenres TO adminuser
GO
GRANT EXECUTE ON dbo.CreateMovie TO adminuser
GO
GRANT EXEC ON TYPE::dbo.IdList TO adminuser
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