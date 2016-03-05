using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Media.Imaging;
using DataAccess.Connection;
using DataAccess.Model;

namespace DataAccess.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly string connectionString;

        public MovieRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<Movie> GetMovies()
        {
            var moviesConnection = new CommandExecutor("dbo.BrowseMovies", connectionString);
            var result = moviesConnection.ExecuteCommand();

            var exception = result as Exception;
            if (exception != null) throw exception;

            var dataSet = result as DataSet;

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                yield return new Movie
                {
                    Id = row["Id"].ToInt(),
                    Title = row["Title"].ToString(),
                    Plot = row["Plot"].ToString(),
                    Duration = row["Duration"].ToInt(),
                    ReleaseDate = row["ReleaseDate"].ToDate(),
                    Genres = new List<Genre>(),
                    Showtimes = new List<DateTime>()
                };
            }
        }

        public IEnumerable<Genre> GetGenres()
        {
            var genresConnection = new CommandExecutor("dbo.ListGenres", connectionString);
            var result = genresConnection.ExecuteCommand();

            var exception = result as Exception;
            if (exception != null) throw exception;

            var dataSet = result as DataSet;

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                yield return new Genre
                {
                    Id = row["Id"].ToInt(),
                    Name = row["Name"].ToString()
                };
            }
        }

        public void GetMovieDetails(Movie movie)
        {
            var moviesConnection = new CommandExecutor("dbo.MovieDetails", connectionString);
            moviesConnection.AddParam("@MovieId", movie.Id, SqlDbType.Int);
            var result = moviesConnection.ExecuteCommand();

            var exception = result as Exception;
            if (exception != null) throw exception;

            var dataSet = result as DataSet;

            var posterRow = dataSet.Tables[0].Rows[0];
            var ageLimitRow = dataSet.Tables[1].Rows[0];
            var genresRows = dataSet.Tables[2].Rows;
            var showtimeRows = dataSet.Tables[3].Rows;

            movie.AgeLimit = new AgeLimit
            {
                Id = ageLimitRow[0].ToInt(),
                Limit = ageLimitRow[1].ToString()
            };

            var posterBytes = (byte[]) posterRow[0];
            movie.Poster = new BitmapImage();
            movie.Poster.BeginInit();
            movie.Poster.StreamSource = new MemoryStream(posterBytes);
            movie.Poster.EndInit();

            foreach (DataRow row in genresRows)
            {
                movie.Genres.Add(new Genre
                {
                    Id = row["Id"].ToInt(),
                    Name = row["Name"].ToString()
                });
            }

            foreach (DataRow row in showtimeRows)
            {
                movie.Showtimes.Add(row["ShowtimeDate"].ToDate());
            }
        }

        public IEnumerable<AgeLimit> GetAgeLimits()
        {
            var genresConnection = new CommandExecutor("dbo.ListAgeLimits", connectionString);
            var result = genresConnection.ExecuteCommand();

            var exception = result as Exception;
            if (exception != null) throw exception;

            var dataSet = result as DataSet;

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                yield return new AgeLimit
                {
                    Id = row["Id"].ToInt(),
                    Limit = row["Limit"].ToString()
                };
            }
        }
    }
}