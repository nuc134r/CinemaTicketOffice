using System.Collections.Generic;
using DataAccess.Model;

namespace DataAccess.Repository
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetMovies();
        IEnumerable<Genre> GetGenres();
        void GetMovieDetails(Movie movie);
        void DeleteMovie(Movie movie);
        IEnumerable<AgeLimit> GetAgeLimits();
        void SaveMovie(Movie movie, bool update = false);
        void SaveGenre(Genre genre, bool update);
        void DeleteGenre(Genre genre);
    }
}