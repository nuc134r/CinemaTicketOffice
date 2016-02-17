USE [CinemaDB]

SET NOCOUNT ON;

/********************************
 *	  Удаление старых таблиц	*
 ********************************/
IF OBJECT_ID('dbo.MovieGenres', 'U') IS NOT NULL DROP TABLE [MovieGenres];
IF OBJECT_ID('dbo.Movie', 'U') IS NOT NULL DROP TABLE [Movie];
IF OBJECT_ID('dbo.Genre', 'U') IS NOT NULL DROP TABLE [Genre];
IF OBJECT_ID('dbo.AgeLimit', 'U') IS NOT NULL DROP TABLE [AgeLimit];

/********************************
 *	  Возрастные ограничения	*
 ********************************/
CREATE TABLE [AgeLimit]
(
	ID				INT					IDENTITY,
	Limit			TINYINT				NOT NULL,

	CONSTRAINT AgeLimitPK PRIMARY KEY (ID),
)

INSERT INTO [AgeLimit] (Limit) VALUES (0), (6), (12), (14), (16), (18)

/********************************
 *			  Фильмы			*
 ********************************/
CREATE TABLE [Movie]
(
	ID				INT					IDENTITY,
	Title			NVARCHAR (128)		NOT NULL, 
	Plot			NVARCHAR (4000)		NOT NULL,
	Duration		INT					NOT NULL,
	Poster			IMAGE				NOT NULL,
	ReleaseDate		DATE				NOT NULL,
	AgeLimitID		INT					NOT NULL DEFAULT 1, 

	CONSTRAINT MoviePK PRIMARY KEY (ID),
	CONSTRAINT AgeLimitFK FOREIGN KEY (AgeLimitID) REFERENCES [AgeLimit] (ID),
	
	CONSTRAINT MovieTitleAK	UNIQUE (Title),
	CONSTRAINT TitleFilledCK CHECK (Title <> '')
)

/********************************
 *			  Жанры				*
 ********************************/
CREATE TABLE [Genre] 
(
	ID				INT					IDENTITY,  
	Name			NVARCHAR (128)		NOT NULL,
	
	CONSTRAINT GenrePK PRIMARY KEY (ID),
	CONSTRAINT GenreNameAK UNIQUE (Name),
)

/********************************
 *	 Связь фильмов с жанрами	*
 ********************************/
CREATE TABLE [MovieGenres] 
(
	MovieID			INT				NOT NULL,
	GenreID			INT				NOT NULL,
	
	CONSTRAINT MovieGenreFK FOREIGN KEY (MovieID) REFERENCES [Movie] (ID),
	CONSTRAINT GenreMovieFK FOREIGN KEY (GenreID) REFERENCES [Genre] (ID),
	CONSTRAINT MovieGenreAK UNIQUE (MovieID, GenreID)
)