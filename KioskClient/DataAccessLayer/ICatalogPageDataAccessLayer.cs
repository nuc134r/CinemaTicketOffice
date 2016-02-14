using System.Collections.Generic;
using DataAccess.Model;

namespace KioskClient.DataAccessLayer
{
    public interface ICatalogPageDataAccessLayer
    {
        List<Genre> GetMovieGenres();
        List<Movie> GetMovies();
    }
}