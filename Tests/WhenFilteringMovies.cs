using System.Collections.Generic;
using DataAccess.Model;
using KioskClient.ViewModel;
using NUnit.Framework;
using Tests.Stubs;

namespace Tests
{
    [TestFixture]
    public class WhenFilteringMovies
    {
        [SetUp]
        public void SetUp()
        {
            movieRepository = new MovieRepositoryStub();

            var genre1 = new Genre {Name = "action"};
            var genre2 = new Genre {Name = "comedy" };
            var genre3 = new Genre {Name = "adventure" };

            var movie1 = new Movie {Title = "Action Movie", Genres = new List<Genre> {genre1}};
            var movie2 = new Movie {Title = "Comedy Movie", Genres = new List<Genre> {genre2}};
            var movie3 = new Movie {Title = "Adventure Movie", Genres = new List<Genre> {genre3}};

            movieRepository.GenresStorage = new List<Genre> {genre1, genre2, genre3};
            movieRepository.MoviesStorage = new List<Movie> {movie1, movie2, movie3};
        }

        private MovieRepositoryStub movieRepository;

        [Test]
        public void AllMoviesShownIfNoGenreSelected()
        {
            var viewModel = new CatalogPageViewModel(null, movieRepository);

            viewModel.Genres[0].IsSelected = false;
            viewModel.Genres[1].IsSelected = false;
            viewModel.Genres[2].IsSelected = false;

            Assert.AreEqual(viewModel.Movies.Count, 3);
        }

        [Test]
        public void FilterIsReset()
        {
            var viewModel = new CatalogPageViewModel(null, movieRepository);

            viewModel.Genres[0].IsSelected = true;
            viewModel.Genres[1].IsSelected = true;
            viewModel.ResetGenresFilter();

            Assert.AreEqual(viewModel.Movies.Count, 3);
        }

        [Test]
        public void OneMovieIsFiltered()
        {
            var viewModel = new CatalogPageViewModel(null, movieRepository);

            viewModel.Genres[0].IsSelected = true;

            Assert.AreEqual(viewModel.Movies.Count, 1);
            Assert.AreEqual(viewModel.Movies[0].Title, "Action Movie");
        }

        [Test]
        public void TwoMoviesAreFiltered()
        {
            var viewModel = new CatalogPageViewModel(null, movieRepository);

            viewModel.Genres[0].IsSelected = true;
            viewModel.Genres[1].IsSelected = true;

            Assert.AreEqual(viewModel.Movies.Count, 2);
            Assert.AreEqual(viewModel.Movies[0].Title, "Action Movie");
            Assert.AreEqual(viewModel.Movies[1].Title, "Comedy Movie");
        }
    }
}