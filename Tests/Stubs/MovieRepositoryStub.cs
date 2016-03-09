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

        public void DeleteMovie(Movie movie)
        {
            
        }

        public IEnumerable<AgeLimit> GetAgeLimits()
        {
            throw new NotImplementedException();
        }

        public void SaveMovie(Movie movie, bool update = false)
        {
            throw new NotImplementedException();
        }

        public void SaveGenre(Genre genre, bool update)
        {
            throw new NotImplementedException();
        }

        public void DeleteGenre(Genre genre)
        {
            throw new NotImplementedException();
        }
    }
}