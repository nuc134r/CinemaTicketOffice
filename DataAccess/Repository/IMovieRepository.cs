using System.Collections.Generic;
using DataAccess.Model;

namespace DataAccess.Repository
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetMovies();
        IEnumerable<Genre> GetGenres();
        void GetMovieDetails(Movie movie);
        void Delete(Movie movie);
        IEnumerable<AgeLimit> GetAgeLimits();
        void Save(Movie movie, bool update = false);
        void Save(Genre genre, bool update);
        void Delete(Genre genre);
    }
}