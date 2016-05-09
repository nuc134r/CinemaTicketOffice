SET NOCOUNT ON;

IF OBJECT_ID('dbo.CurrentRole', 'P') IS NOT NULL DROP PROCEDURE [CurrentRole]
GO
CREATE PROCEDURE dbo.CurrentRole
AS
	SELECT 
		CASE name
			WHEN 'greenbird_user'		THEN 1
			WHEN 'greenbird_admin'		THEN 2
			WHEN 'greenbird_superadmin' THEN 3
			ELSE 0
		END
	FROM 
		sys.database_role_members AS [RM]
	LEFT JOIN sys.database_principals AS [P]
		ON [RM].role_principal_id = [P].principal_id
	WHERE
		[P].name LIKE 'greenbird_%'
		AND
		[RM].member_principal_id = (SELECT 
										principal_id
									FROM 
										sys.database_principals AS [P]
									WHERE 
										[P].name = CURRENT_USER)
GO

GRANT EXECUTE ON dbo.CurrentRole TO PUBLIC

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

IF OBJECT_ID('dbo.BrowseGenres', 'P')	IS NOT NULL DROP PROCEDURE [BrowseGenres]
GO
CREATE PROCEDURE dbo.BrowseGenres
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
		ShowtimeDate > DATEADD(hour, 0, GETDATE())
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
		[A].Name AS [AuditoriumName],
		[A].RowsNumber AS [AuditoriumRows],
		[A].SeatsNumber AS [AuditoriumSeats]
    FROM 
		Showtime as [S]
	LEFT JOIN Auditorium AS [A]
		ON [A].Id = [S].AuditoriumId
	LEFT JOIN Movie AS [M]
		ON [M].Id = [S].MovieId
	ORDER BY
		[S].ShowtimeDate
GO

IF OBJECT_ID('dbo.BrowsePendingShowtimes', 'P') IS NOT NULL DROP PROCEDURE [BrowsePendingShowtimes]
GO
CREATE PROCEDURE dbo.BrowsePendingShowtimes
	@MovieId INT
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
		[A].Name AS [AuditoriumName],
		[A].RowsNumber AS [AuditoriumRows],
		[A].SeatsNumber AS [AuditoriumSeats]
    FROM 
		Showtime as [S]
	LEFT JOIN Auditorium AS [A]
		ON [A].Id = [S].AuditoriumId
	LEFT JOIN Movie AS [M]
		ON [M].Id = [S].MovieId
	WHERE
		[M].Id = @MovieId
		AND
		[S].ShowtimeDate > DATEADD(hour, 0, GETDATE())
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

IF OBJECT_ID('dbo.CreateAuditorium', 'P') IS NOT NULL DROP PROCEDURE [CreateAuditorium]
GO
CREATE PROCEDURE dbo.CreateAuditorium
	@Name NVARCHAR (128),
	@Rows INT,
	@Seats INT
AS 
    SET NOCOUNT ON;

	INSERT INTO Auditorium 
	(		
		Name,
		RowsNumber,
		SeatsNumber
	)
	SELECT
		@Name,
		@Rows,
		@Seats
GO

IF OBJECT_ID('dbo.UpdateAuditorium', 'P') IS NOT NULL DROP PROCEDURE [UpdateAuditorium]
GO
CREATE PROCEDURE dbo.UpdateAuditorium
	@Id INT,
	@Name NVARCHAR (128),
	@Rows INT,
	@Seats INT
AS 
    SET NOCOUNT ON;

	UPDATE Auditorium SET
		Name = @Name,
		RowsNumber = @Rows,
		SeatsNumber = @Seats
	WHERE
		Id = @Id
GO

IF OBJECT_ID('dbo.DeleteAuditorium', 'P') IS NOT NULL DROP PROCEDURE [DeleteAuditorium]
GO
CREATE PROCEDURE dbo.DeleteAuditorium
	@Id INT
AS
	DELETE FROM Auditorium WHERE Id = @Id
GO

IF OBJECT_ID('dbo.RegisterTickets', 'P') IS NOT NULL DROP PROCEDURE [RegisterTickets]
GO
CREATE PROCEDURE dbo.RegisterTickets
	@ShowtimeId INT,
	@Tickets SeatList READONLY
AS
	DECLARE
		@SeatNumber INT,
		@RowNumber INT

	DECLARE TicketCursor CURSOR FOR 
	SELECT 
		SeatNumber,
		RowNumber 
	FROM 
		@Tickets
 
	OPEN TicketCursor
	FETCH NEXT FROM TicketCursor INTO @SeatNumber, @RowNumber 
 
	WHILE @@FETCH_STATUS=0
	BEGIN
		INSERT INTO [Ticket]
		(
			ShowtimeId,
			SeatNumber,
			RowNumber
		)
		SELECT
			@ShowtimeId,
			@SeatNumber,
			@RowNumber

		FETCH NEXT FROM TicketCursor INTO @SeatNumber, @RowNumber 
	END
	CLOSE TicketCursor
	DEALLOCATE TicketCursor
GO

IF OBJECT_ID('dbo.GetOccupiedSeats', 'P') IS NOT NULL DROP PROCEDURE [GetOccupiedSeats]
GO
CREATE PROCEDURE dbo.GetOccupiedSeats
	@ShowtimeId INT
AS
	SELECT
		SeatNumber,
		RowNumber
	FROM
		[Ticket]
	WHERE
		ShowtimeId = @ShowtimeId
GO

IF OBJECT_ID('dbo.BrowseTickets', 'P') IS NOT NULL DROP PROCEDURE [BrowseTickets]
GO
CREATE PROCEDURE dbo.BrowseTickets
AS
	SELECT
		[T].Id,
		[T].SeatNumber,
		[T].RowNumber,
		[M].Title,
		[S].ShowtimeDate,
		DATEADD(hour, 0, [T].SellDate) AS SellDate,
		[A].Name
	FROM
		[Ticket] AS [T]
	INNER JOIN
		[Showtime] AS [S]
	ON [S].Id = [T].ShowtimeId
	INNER JOIN
		[Auditorium] AS [A]
	ON [A].Id = [S].AuditoriumId
	INNER JOIN
		[Movie] AS [M]
	ON [M].Id = [S].MovieId
GO

IF OBJECT_ID('dbo.DeleteTicket', 'P') IS NOT NULL DROP PROCEDURE [DeleteTicket]
GO
CREATE PROCEDURE dbo.DeleteTicket
	@Id INT
AS
	DELETE FROM Ticket WHERE Id = @Id
GO

IF OBJECT_ID('dbo.CreateUser', 'P') IS NOT NULL DROP PROCEDURE [CreateUser]
GO
CREATE PROCEDURE dbo.CreateUser
	@Username NVARCHAR(128),
	@Password NVARCHAR(128),
	@Usertype INT
AS
	IF (NOT @Usertype IN (1, 2, 3))
	BEGIN
		RAISERROR('INVALID ARGUMENT @Usertype', 16, 1)
		RETURN 1
	END

	DECLARE @Sql NVARCHAR(512)

	SET @Sql = 'CREATE LOGIN [' + @Username + '] WITH PASSWORD = ''' + @Password + '''';
	EXEC (@Sql)

	SET @Sql = 'CREATE USER [' + @Username + '] FOR LOGIN [' + @Username + ']';
	EXEC (@Sql)

	IF (@Usertype = 1)
	BEGIN
		EXEC sp_addrolemember N'greenbird_user', @Username
	END
	ELSE IF (@Usertype = 2)
	BEGIN
		EXEC sp_addrolemember N'greenbird_admin', @Username
	END
	ELSE IF (@Usertype = 3)
	BEGIN
		EXEC sp_addrolemember N'greenbird_superadmin', @Username

		EXEC sp_addrolemember N'db_owner', @Username
		EXEC sp_addrolemember N'db_securityadmin ', @Username
		EXEC sp_addsrvrolemember @Username, N'securityadmin'
	END
GO

IF OBJECT_ID('dbo.BrowseUsers', 'P') IS NOT NULL DROP PROCEDURE [BrowseUsers]
GO
CREATE PROCEDURE dbo.BrowseUsers
AS
	SELECT 
		[P].name AS [User],
		CASE [P2].name
			WHEN 'greenbird_user'		THEN 1
			WHEN 'greenbird_admin'		THEN 2
			WHEN 'greenbird_superadmin' THEN 3
		END AS [Role]

	FROM
		(SELECT 
		 	 principal_id,
		 	 name 
		 FROM 
		 	 sys.database_principals AS [P]
		 WHERE 
		 	 [P].type_desc = 'SQL_USER' 
		 	 AND 
		 	 [P].default_schema_name = 'dbo') AS [P]

	LEFT JOIN sys.database_role_members AS [RM]
		ON [P].principal_id = [RM].member_principal_id
	LEFT JOIN sys.database_principals AS [P2]
		ON [RM].role_principal_id = [P2].principal_id
	WHERE
		[P2].name LIKE 'greenbird_%'
GO

IF OBJECT_ID('dbo.DeleteUser', 'P') IS NOT NULL DROP PROCEDURE [DeleteUser]
GO
CREATE PROCEDURE dbo.DeleteUser
	@Username NVARCHAR(128)
AS
	IF (CURRENT_USER = @Username)
	BEGIN
		RAISERROR('Cannot remove current user', 16, 1)
		RETURN 1
	END

	DECLARE @Sql NVARCHAR(512)

	SET @Sql = 'DROP USER [' + @Username + ']';
	EXEC (@Sql)

	SET @Sql = 'DROP LOGIN [' + @Username + ']';
	EXEC (@Sql)
GO

IF OBJECT_ID('dbo.BrowseLogs', 'P') IS NOT NULL DROP PROCEDURE [BrowseLogs]
GO
CREATE PROCEDURE dbo.BrowseLogs
AS
	SELECT 
		Id,
		[User],
		[Date],
		EntityTable,
		EntityId,
		OperationType
	FROM 
		[Log]
	ORDER BY
		[Date] DESC
GO