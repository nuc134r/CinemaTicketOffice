SET NOCOUNT ON;

IF OBJECT_ID('dbo.BrowseMovies', 'P') IS NOT NULL DROP PROCEDURE [BrowseMovies]
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

IF OBJECT_ID('dbo.ListGenres', 'P')	IS NOT NULL DROP PROCEDURE [ListGenres]
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

IF OBJECT_ID('dbo.ListAgeLimits', 'P') IS NOT NULL DROP PROCEDURE [ListAgeLimits]
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

IF OBJECT_ID('dbo.MovieDetails', 'P') IS NOT NULL DROP PROCEDURE [MovieDetails]
GO
CREATE PROCEDURE dbo.MovieDetails
	@MovieId INT
AS 
    SET NOCOUNT ON;

    SELECT 
		Poster
    FROM 
		Movie
	WHERE 
		Movie.Id = @MovieId

	SELECT
		Id, 
		Limit
	FROM 
		AgeLimit
	WHERE 
		Id = (SELECT AgeLimitId 
			  FROM Movie 
			  WHERE Id = @MovieId)

	SELECT 
		Id, Name
	FROM 
		Genre
	WHERE
		Id IN (SELECT GenreId 
				FROM MovieGenres
				WHERE MovieId = @MovieId)
	
	SELECT 
		ShowtimeDate
	FROM 
		Showtime
	WHERE 
		MovieId = @MovieId
		AND
		ShowtimeDate > DATEADD(hour, 3, GETDATE())
GO

IF OBJECT_ID('dbo.CreateMovie', 'P') IS NOT NULL DROP PROCEDURE [CreateMovie]
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

	DECLARE @MovieId INT, @GenreId INT
	SET @MovieId = SCOPE_IDENTITY();

	DECLARE GenresCursor CURSOR FOR 
	SELECT Id FROM @Genres
 
	OPEN GenresCursor
	FETCH NEXT FROM GenresCursor INTO @GenreId
 
	WHILE @@FETCH_STATUS=0
	BEGIN
		INSERT INTO MovieGenres
		SELECT @MovieId, @GenreId

		FETCH NEXT FROM GenresCursor INTO @GenreId
	END
	CLOSE GenresCursor
	DEALLOCATE GenresCursor
	GO
GO

IF OBJECT_ID('dbo.UpdateMovie', 'P') IS NOT NULL DROP PROCEDURE [UpdateMovie]
GO
CREATE PROCEDURE dbo.UpdateMovie
	@Id INT,
	@Title NVARCHAR (128),
	@Plot NVARCHAR (4000),
	@Duration SMALLINT,
	@Poster IMAGE,
	@Genres IdList READONLY,
	@ReleaseDate DATE,
	@AgeLimitId INT
AS 
    SET NOCOUNT ON;

	UPDATE Movie SET
		Title = @Title,
		Plot =  @Plot,
		Duration = @Duration,
		Poster = @Poster,
		ReleaseDate = @ReleaseDate,
		AgeLimitId = @AgeLimitId
	WHERE
		Id = @Id

	DELETE FROM MovieGenres
	WHERE MovieId = @Id

	DECLARE @MovieId INT, @GenreId INT
	SET @MovieId = @Id;

	DECLARE GenresCursor CURSOR FOR 
	SELECT Id FROM @Genres
 
	OPEN GenresCursor
	FETCH NEXT FROM GenresCursor INTO @GenreId
 
	WHILE @@FETCH_STATUS=0
	BEGIN
		INSERT INTO MovieGenres
		SELECT @MovieId, @GenreId

		FETCH NEXT FROM GenresCursor INTO @GenreId
	END
	CLOSE GenresCursor
	DEALLOCATE GenresCursor
	GO
GO

IF OBJECT_ID('dbo.DeleteMovie', 'P') IS NOT NULL DROP PROCEDURE [DeleteMovie]
GO
CREATE PROCEDURE dbo.DeleteMovie
	@MovieId INT
AS
	DELETE FROM Movie WHERE Id = @MovieId
GO

IF OBJECT_ID('dbo.CreateGenre', 'P') IS NOT NULL DROP PROCEDURE [CreateGenre]
GO
CREATE PROCEDURE dbo.CreateGenre
	@Name NVARCHAR (128)
AS 
    SET NOCOUNT ON;

	INSERT INTO Genre (Name) SELECT @Name
GO

IF OBJECT_ID('dbo.UpdateGenre', 'P') IS NOT NULL DROP PROCEDURE [UpdateGenre]
GO
CREATE PROCEDURE dbo.UpdateGenre
	@Id INT,
	@Name NVARCHAR (128)
AS 
    SET NOCOUNT ON;

	UPDATE Genre SET
		Name = @Name
	WHERE
		Id = @Id
GO

IF OBJECT_ID('dbo.DeleteGenre', 'P') IS NOT NULL DROP PROCEDURE [DeleteGenre]
GO
CREATE PROCEDURE dbo.DeleteGenre
	@GenreId INT
AS
	DELETE FROM Genre WHERE Id = @GenreId
GO

IF OBJECT_ID('dbo.CreateShowtime', 'P') IS NOT NULL DROP PROCEDURE [CreateShowtime]
GO
CREATE PROCEDURE dbo.CreateShowtime
	@MovieId INT,
	@AuditoriumId INT,
	@ShowtimeDate DateTime,
	@Price MONEY,
	@ThreeDee BIT
AS 
    SET NOCOUNT ON;

	INSERT INTO Showtime 
	(		
		MovieId,	
		AuditoriumId,
		ShowtimeDate,
		Price,
		ThreeDee	
	)
	SELECT
		@MovieId,	
		@AuditoriumId,
		@ShowtimeDate,
		@Price,
		@ThreeDee
GO

IF OBJECT_ID('dbo.UpdateShowtime', 'P') IS NOT NULL DROP PROCEDURE [UpdateShowtime]
GO
CREATE PROCEDURE dbo.UpdateShowtime
	@Id INT,
	@MovieId INT,
	@AuditoriumId INT,
	@ShowtimeDate DateTime,
	@Price MONEY,
	@ThreeDee BIT
AS 
    SET NOCOUNT ON;

	UPDATE Showtime SET
		MovieId = @MovieId,
		AuditoriumId = @AuditoriumId,
		ShowtimeDate = @ShowtimeDate,
		Price = @Price,
		ThreeDee = @ThreeDee
	WHERE
		Id = @Id
GO

IF OBJECT_ID('dbo.DeleteShowtime', 'P') IS NOT NULL DROP PROCEDURE [DeleteShowtime]
GO
CREATE PROCEDURE dbo.DeleteShowtime
	@Id INT
AS
	DELETE FROM Showtime WHERE Id = @Id
GO

IF OBJECT_ID('dbo.BrowseShowtimes', 'P') IS NOT NULL DROP PROCEDURE [BrowseShowtimes]
GO
CREATE PROCEDURE dbo.BrowseShowtimes
AS 
    SET NOCOUNT ON;

    SELECT 
		[S].Id,
		[S].ShowtimeDate,
		[S].Price,
		[S].ThreeDee,
		[M].Id AS [MovieId],
		[M].Title AS [MovieTitle],
		[A].Id AS [AuditoriumId],
		[A].Name AS [AuditoriumName]
    FROM 
		Showtime as [S]
	LEFT JOIN Auditorium AS [A]
		ON [A].Id = [S].AuditoriumId
	LEFT JOIN Movie AS [M]
		ON [M].Id = [S].MovieId
	ORDER BY
		[S].Id
GO

IF OBJECT_ID('dbo.BrowsePendingShowtimes', 'P') IS NOT NULL DROP PROCEDURE [BrowsePendingShowtimes]
GO
CREATE PROCEDURE dbo.BrowsePendingShowtimes
AS 
    SET NOCOUNT ON;

    SELECT 
		[S].Id,
		[S].ShowtimeDate,
		[S].Price,
		[S].ThreeDee
    FROM 
		Showtime as [S]
	WHERE
		[S].ShowtimeDate > DATEADD(hour, 3, GETDATE())
	ORDER BY
		[S].ShowtimeDate
GO

IF OBJECT_ID('dbo.GetLogo', 'P') IS NOT NULL DROP PROCEDURE [GetLogo]
GO
CREATE PROCEDURE dbo.GetLogo
AS 
    SET NOCOUNT ON;

	SELECT 
		Logo
	FROM 
		[Logo]
GO

IF OBJECT_ID('dbo.SetLogo', 'P') IS NOT NULL DROP PROCEDURE [SetLogo]
GO
CREATE PROCEDURE dbo.SetLogo
	@Logo IMAGE
AS 
    SET NOCOUNT ON;

	UPDATE 
		[Logo] 
	SET		
		Logo = @Logo
GO

IF OBJECT_ID('dbo.BrowseAuditoriums', 'P') IS NOT NULL DROP PROCEDURE [BrowseAuditoriums]
GO
CREATE PROCEDURE dbo.BrowseAuditoriums
AS 
    SET NOCOUNT ON;

    SELECT 
		[A].Id,
		[A].Name,
		[A].RowsNumber,
		[A].SeatsNumber
    FROM 
		Auditorium as [A]
	ORDER BY
		[A].Id
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
GRANT EXECUTE ON dbo.UpdateMovie TO adminuser
GO
GRANT EXECUTE ON dbo.DeleteMovie TO adminuser
GO
GRANT EXECUTE ON dbo.CreateGenre TO adminuser
GO
GRANT EXECUTE ON dbo.UpdateGenre TO adminuser
GO
GRANT EXECUTE ON dbo.DeleteGenre TO adminuser
GO
GRANT EXECUTE ON dbo.CreateShowtime TO adminuser
GO
GRANT EXECUTE ON dbo.UpdateShowtime TO adminuser
GO
GRANT EXECUTE ON dbo.BrowseShowtimes TO adminuser
GO
GRANT EXECUTE ON dbo.BrowseAuditoriums TO adminuser
GO
GRANT EXECUTE ON dbo.GetLogo TO adminuser
GO
GRANT EXECUTE ON dbo.SetLogo TO adminuser
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
GRANT EXECUTE ON dbo.GetLogo TO kioskuser
GO
GRANT EXECUTE ON dbo.BrowsePendingShowtimes TO kioskuser
GO