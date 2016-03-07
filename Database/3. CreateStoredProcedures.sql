SET NOCOUNT ON;

IF OBJECT_ID('dbo.BrowseMovies', 'P') IS NOT NULL DROP PROCEDURE [BrowseMovies]
IF OBJECT_ID('dbo.ListGenres', 'P')	IS NOT NULL DROP PROCEDURE [ListGenres]
IF OBJECT_ID('dbo.ListAgeLimits', 'P')	IS NOT NULL DROP PROCEDURE [ListAgeLimits]
IF OBJECT_ID('dbo.MovieDetails', 'P') IS NOT NULL DROP PROCEDURE [MovieDetails]
IF OBJECT_ID('dbo.CreateMovie', 'P') IS NOT NULL DROP PROCEDURE [CreateMovie]
IF OBJECT_ID('dbo.UpdateMovie', 'P') IS NOT NULL DROP PROCEDURE [UpdateMovie]
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