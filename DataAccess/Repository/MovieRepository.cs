using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Media.Imaging;
using DataAccess.Model;

namespace DataAccess.Repository
{
    public class MovieRepository : IMovieRepository
    {
        public IEnumerable<Movie> GetMovies()
        {
            var moviesConnection = new CommandExecutor("dbo.ListMovies");

            var result = moviesConnection.ExecuteCommand();

            foreach (DataRow row in result.Tables[0].Rows)
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
            var genresConnection = new CommandExecutor("dbo.ListGenres");

            var result = genresConnection.ExecuteCommand();

            foreach (DataRow row in result.Tables[0].Rows)
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
            var moviesConnection = new CommandExecutor("dbo.MovieDetails");
            moviesConnection["@MovieId"] = movie.Id;

            var result = moviesConnection.ExecuteCommand();

            var posterRow = result.Tables[0].Rows[0];
            var ageLimitRow = result.Tables[1].Rows[0];
            var genresRows = result.Tables[2].Rows;
            var showtimeRows = result.Tables[3].Rows;

            movie.AgeLimit = ageLimitRow[0].ToInt();

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
    }
}