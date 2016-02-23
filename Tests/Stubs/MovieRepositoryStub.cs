using System;
using System.Collections.Generic;
using DataAccess.Model;
using DataAccess.Repository;

namespace Tests.Stubs
{
    public class MovieRepositoryStub : IMovieRepository
    {
        public List<Genre> GenresStorage = new List<Genre>();
        public List<Movie> MoviesStorage = new List<Movie>();

        public IEnumerable<Movie> GetMovies()
        {
            return MoviesStorage;
        }

        public IEnumerable<Genre> GetGenres()
        {
            return GenresStorage;
        }

        public void GetMovieDetails(Movie movie)
        {
            
        }

        public void Refresh()
        {
        }
    }
}