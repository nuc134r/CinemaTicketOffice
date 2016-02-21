﻿-- https://www.mssqltips.com/sqlservertip/2365/sql-server-foreign-key-update-and-delete-rules/

USE [CinemaDB]

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
								/*     1    2    3     4     5     6   */
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
	Poster			IMAGE				NOT NULL,
	ReleaseDate		DATE				NOT NULL,
	AgeLimitId		INT					NOT NULL DEFAULT 1, 

	CONSTRAINT MoviePK PRIMARY KEY (Id),
	CONSTRAINT AgeLimitFK FOREIGN KEY (AgeLimitId) REFERENCES [AgeLimit] (Id),
	
	CONSTRAINT MovieTitleAK	UNIQUE (Title),
	CONSTRAINT MovieTitleFilledCK CHECK (Title <> ''),
	CONSTRAINT MoviePlotFilledCK CHECK (Plot <> '')
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
 *	 Связь фильмов с жанрами	*
 ********************************/
CREATE TABLE [MovieGenres] 
(
	MovieId			INT				NOT NULL,
	GenreId			INT				NOT NULL,
	
	CONSTRAINT MovieGenreFK FOREIGN KEY (MovieId) REFERENCES [Movie] (Id) ON DELETE CASCADE,
	CONSTRAINT GenreMovieFK FOREIGN KEY (GenreId) REFERENCES [Genre] (Id) ON DELETE CASCADE,
	CONSTRAINT MovieGenreAK UNIQUE (MovieId, GenreId)
)

/********************************
 *			   Залы				*
 ********************************/
CREATE TABLE [Auditorium]
(
	Id				INT				IDENTITY,
	SeatsNumber		TINYINT			NOT NULL,
	RowsNumber		TINYINT			NOT NULL,

	CONSTRAINT AuditoriumPK PRIMARY KEY (Id),
	CONSTRAINT AuditoriumSeatsNumberPositiveCK CHECK (SeatsNumber > 0),
	CONSTRAINT AuditoriumRowsNumberPositiveCK CHECK (RowsNumber > 0)
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
	CONSTRAINT MovieShowtimeFK FOREIGN KEY (MovieId) REFERENCES [Movie] (Id) ON DELETE CASCADE,
	CONSTRAINT AuditoriumShowtimeFK FOREIGN KEY (AuditoriumId) REFERENCES [Auditorium] (Id) ON DELETE CASCADE,
	CONSTRAINT ShowtimePricePositiveCK CHECK (Price > 0),
	CONSTRAINT ShowtimeAK UNIQUE (MovieId, AuditoriumId, ShowtimeDate)
)