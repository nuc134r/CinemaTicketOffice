USE CinemaDB;

SET NOCOUNT ON;

IF OBJECT_ID('dbo.ListMovies', 'P')	IS NOT NULL DROP PROCEDURE [ListMovies]
IF OBJECT_ID('dbo.ListGenres', 'P')	IS NOT NULL DROP PROCEDURE [ListGenres]
IF OBJECT_ID('dbo.MovieDetails', 'P') IS NOT NULL DROP PROCEDURE [MovieDetails]
GO

/*
SELECT * FROM MovieGenres

*/

CREATE PROCEDURE dbo.ListMovies
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