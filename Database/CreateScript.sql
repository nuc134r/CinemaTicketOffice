USE [CinemaDB]

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
	AgeLimit		INT					NOT NULL,

	CONSTRAINT MoviePK PRIMARY KEY (ID)
)

/********************************
 *			  Жанры				*
 ********************************/
CREATE TABLE [Genre] 
(
	ID				INT					IDENTITY,
	Name			NVARCHAR (128)		NOT NULL,

	CONSTRAINT GenrePK PRIMARY KEY (ID)
)

/********************************
 *	 Связь фильмов с жанрами	*
 ********************************/
CREATE TABLE [MovieGenres] 
(
	MovieID				INT				NOT NULL,
	GenreID				INT				NOT NULL,
	
	CONSTRAINT MovieGenreFK FOREIGN KEY (MovieID) REFERENCES [Movie] (ID),
	CONSTRAINT GenreMovieFK FOREIGN KEY (GenreID) REFERENCES [Genre] (ID),
	CONSTRAINT MovieGenreAK UNIQUE (MovieID, GenreID)
)








CREATE TABLE [Term]
(
	ID				INT					IDENTITY,
	SubjectID		INT					NULL,
	Name_RU			NVARCHAR (128)		NOT NULL,
	Definition_RU	NVARCHAR (2048)		NOT NULL,
	Name_EN			VARCHAR  (128)		NOT NULL,
	Definition_EN	VARCHAR  (2048)		NOT NULL,

	CONSTRAINT TermPK PRIMARY KEY (ID),
	CONSTRAINT TermSubjectFK FOREIGN KEY (SubjectID) REFERENCES [Subject] (ID)
)