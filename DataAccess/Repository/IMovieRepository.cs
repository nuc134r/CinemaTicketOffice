using System.Collections.Generic;
using DataAccess.Model;

namespace DataAccess.Repository
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetMovies();
        IEnumerable<Genre> GetGenres();
        void GetMovieDetails(Movie movie);
    }
}