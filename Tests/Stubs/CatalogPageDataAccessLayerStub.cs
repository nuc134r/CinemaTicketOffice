using System.Collections.Generic;
using DataAccess.Model;
using DataAccess.Repository;

namespace Tests.Stubs
{
    public class MovieRepositoryStub : IMovieRepository
    {
        public List<Genre> GenresStorage = new List<Genre>();
        public List<Movie> MoviesStorage = new List<Movie>();

        public List<Movie> Movies
        {
            get { return MoviesStorage; }
        }

        public List<Genre> Genres
        {
            get { return GenresStorage; }
        }

        public void RefreshData()
        {
        }
    }
}