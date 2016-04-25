SET NOCOUNT ON;

/********************************
 *	  Удаление старых таблиц	*
 ********************************/
IF OBJECT_ID('dbo.MovieGenres', 'U')	IS NOT NULL DROP TABLE [MovieGenres];
IF OBJECT_ID('dbo.Showtime', 'U')		IS NOT NULL DROP TABLE [Showtime];
IF OBJECT_ID('dbo.Movie', 'U')			IS NOT NULL DROP TABLE [Movie];
IF OBJECT_ID('dbo.AgeLimit', 'U')		IS NOT NULL DROP TABLE [AgeLimit];
IF OBJECT_ID('dbo.Genre', 'U')			IS NOT NULL DROP TABLE [Genre];
IF OBJECT_ID('dbo.Auditorium', 'U')		IS NOT NULL DROP TABLE [Auditorium];
IF OBJECT_ID('dbo.Logo', 'U')			IS NOT NULL DROP TABLE [Logo];
IF OBJECT_ID('dbo.Ticket', 'U')			IS NOT NULL DROP TABLE [Ticket];

/********************************
 *	  Возрастные ограничения	*
 ********************************/
CREATE TABLE [AgeLimit]
(
	Id				INT					IDENTITY,
	Limit			TINYINT				NOT NULL,

	CONSTRAINT AgeLimitPK PRIMARY KEY (Id),
	CONSTRAINT AgeLimitAK UNIQUE (Limit),
	CONSTRAINT AgeLimitCK CHECK (Limit IN (0, 6, 12, 14, 16, 18))
)
								  /*   1    2    3     4     5     6   */
INSERT INTO [AgeLimit] (Limit) VALUES (0), (6), (12), (14), (16), (18)

/********************************
 *			  Фильмы			*
 ********************************/
CREATE TABLE [Movie]
(
	Id				INT					IDENTITY,
	Title			NVARCHAR (128)		NOT NULL, 
	Plot			NVARCHAR (4000)		NOT NULL,
	Duration		SMALLINT			NOT NULL,  -- Modern Times Forever (2011 Documentary) lasts 14400 minutes
	Poster			IMAGE				NULL,
	ReleaseDate		DATE				NOT NULL,
	AgeLimitId		INT					NOT NULL DEFAULT 1, 

	CONSTRAINT MoviePK PRIMARY KEY (Id),
	
	CONSTRAINT MovieTitleAK	UNIQUE (Title),
	CONSTRAINT MovieTitleFilledCK CHECK (Title <> ''),
	CONSTRAINT MoviePlotFilledCK CHECK (Plot <> ''),
	CONSTRAINT MovieDurationPositiveCK CHECK (Duration >= 0)
)

/********************************
 *			  Жанры				*
 ********************************/
CREATE TABLE [Genre] 
(
	Id				INT					IDENTITY,  
	Name			NVARCHAR (128)		NOT NULL,
	
	CONSTRAINT GenrePK PRIMARY KEY (Id),
	CONSTRAINT GenreNameAK UNIQUE (Name),
	CONSTRAINT GenreNameFilledCK CHECK (Name <> '')
)

/********************************
 *			  Сеансы			*
 ********************************/
CREATE TABLE [Showtime]
(
	Id				INT				IDENTITY,
	MovieId			INT				NOT NULL,
	AuditoriumId	INT				NOT NULL,
	ShowtimeDate	DateTime		NOT NULL,
	Price			MONEY			NOT NULL,
	ThreeDee		BIT				NOT NULL DEFAULT 0,
	
	CONSTRAINT ShowtimePK PRIMARY KEY (Id),
	CONSTRAINT ShowtimePricePositiveCK CHECK (Price >= 0),
	CONSTRAINT ShowtimeAK UNIQUE (MovieId, AuditoriumId, ShowtimeDate)
)

/********************************
 *			  Билеты			*
 ********************************/
CREATE TABLE [Ticket] 
(
	Id				INT			IDENTITY,  
	ShowtimeId		INT			NOT NULL,
	SeatNumber		INT			NOT NULL,
	RowNumber		INT			NOT NULL,
	IsUsed			BIT			NOT NULL DEFAULT 0,
	SellDate		DATETIME	NOT NULL DEFAULT GETDATE(),
	
	CONSTRAINT TicketPK PRIMARY KEY (Id),
	CONSTRAINT SeatNumberAK UNIQUE (ShowtimeId, SeatNumber, RowNumber),
	CONSTRAINT SeatNumberPositiveCK CHECK (SeatNumber > 0),
	CONSTRAINT RowNumberPositiveCK CHECK (RowNumber > 0)
)

/********************************
 *	 Связь фильмов с жанрами	*
 ********************************/
CREATE TABLE [MovieGenres] 
(
	MovieId			INT				NOT NULL,
	GenreId			INT				NOT NULL,
	
	CONSTRAINT MovieGenreAK UNIQUE (MovieId, GenreId)
)

/********************************
 *			   Залы				*
 ********************************/
CREATE TABLE [Auditorium]
(
	Id				INT				IDENTITY,
	Name			NVARCHAR (128)	NOT NULL,
	SeatsNumber		TINYINT			NOT NULL,
	RowsNumber		TINYINT			NOT NULL,

	CONSTRAINT AuditoriumPK PRIMARY KEY (Id),
	CONSTRAINT AuditoriumSeatsNumberPositiveCK CHECK (SeatsNumber > 0),
	CONSTRAINT AuditoriumRowsNumberPositiveCK CHECK (RowsNumber > 0)
)

/********************************
 *    Список идентификаторов	*
 ********************************/
IF TYPE_ID('dbo.IdList') IS NULL
CREATE TYPE [dbo].[IdList] AS TABLE(
    [Id] INT NOT NULL
);

/********************************
 *			  Логотип			*
 ********************************/
CREATE TABLE [Logo]
(
	Logo	IMAGE	NULL
)

INSERT INTO [Logo] 
(
	Logo
)
VALUES
(
	NULL
)