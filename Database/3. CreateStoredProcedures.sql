SET NOCOUNT ON;

IF OBJECT_ID('dbo.BrowseMovies', 'P') IS NOT NULL DROP PROCEDURE [BrowseMovies]
IF OBJECT_ID('dbo.ListGenres', 'P')	IS NOT NULL DROP PROCEDURE [ListGenres]
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

CREATE PROCEDURE dbo.MovieDetails
	@MovieId INT
AS 
    SET NOCOUNT ON;

    SELECT Poster
    FROM Movie
	WHERE Movie.Id = @MovieId

	SELECT Limit
    FROM Movie AS [M]
	INNER JOIN AgeLimit AS [L]
	ON [L].Id = [M].AgeLimitId
	WHERE [M].Id = @MovieId

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