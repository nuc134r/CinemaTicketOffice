using System.Collections.Generic;
using KioskClient.DataAccessLayer;
using KioskClient.Model;

namespace Tests.Stubs
{
    public class CatalogPageDataAccessLayerStub : ICatalogPageDataAccessLayer
    {
        public List<Genre> Genres = new List<Genre>();
        public List<Movie> Movies = new List<Movie>();

        public List<Genre> GetMovieGenres()
        {
            return Genres;
        }

        public List<Movie> GetMovies()
        {
            return Movies;
        }
    }
}