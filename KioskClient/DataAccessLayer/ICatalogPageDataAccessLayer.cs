using System.Collections.Generic;
using KioskClient.Model;

namespace KioskClient.DataAccessLayer
{
    public interface ICatalogPageDataAccessLayer
    {
        List<Genre> GetMovieGenres();
        List<Movie> GetMovies();
    }
}