using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using KioskClient.DataAccessLayer;
using KioskClient.ViewModel;
using NUnit.Framework;
using Tests.Stubs;

namespace Tests
{
    [TestFixture]
    public class WhenFilteringMovies
    {
        private CatalogPageDataAccessLayerStub dataAccessLayer;

        [SetUp]
        public void SetUp()
        {
            dataAccessLayer = new CatalogPageDataAccessLayerStub();

            var genre1 = GenreBuilder.Create().WithName("action").Please();
            var genre2 = GenreBuilder.Create().WithName("comedy").Please();
            var genre3 = GenreBuilder.Create().WithName("adventure").Please();

            var movie1 = MovieBuilder.Create().WithTitle("Action Movie").WithGenre(genre1).Please();
            var movie2 = MovieBuilder.Create().WithTitle("Comedy Movie").WithGenre(genre2).Please();
            var movie3 = MovieBuilder.Create().WithTitle("Adventure Movie").WithGenre(genre3).Please();

            dataAccessLayer.Genres = new List<Genre> { genre1, genre2, genre3 };
            dataAccessLayer.Movies = new List<Movie> { movie1, movie2, movie3 };
        }

        [Test]
        public void OneMovieIsFiltered()
        {
            var viewModel = new CatalogPageViewModel(null, dataAccessLayer);

            viewModel.Genres[0].IsSelected = true;

            Assert.AreEqual(viewModel.Movies.Count, 1);
            Assert.AreEqual(viewModel.Movies[0].Title, "Action Movie");
        }

        [Test]
        public void TwoMoviesAreFiltered()
        {
            var viewModel = new CatalogPageViewModel(null, dataAccessLayer);

            viewModel.Genres[0].IsSelected = true;
            viewModel.Genres[1].IsSelected = true;

            Assert.AreEqual(viewModel.Movies.Count, 2);
            Assert.AreEqual(viewModel.Movies[0].Title, "Action Movie");
            Assert.AreEqual(viewModel.Movies[1].Title, "Comedy Movie");
        }

        [Test]
        public void FilterIsReset()
        {
            var viewModel = new CatalogPageViewModel(null, dataAccessLayer);

            viewModel.Genres[0].IsSelected = true;
            viewModel.Genres[1].IsSelected = true;
            viewModel.ResetGenresFilter();

            Assert.AreEqual(viewModel.Movies.Count, 3);
        }

        [Test]
        public void AllMoviesShownIfNoGenreSelected()
        {
            var viewModel = new CatalogPageViewModel(null, dataAccessLayer);

            viewModel.Genres[0].IsSelected = false;
            viewModel.Genres[1].IsSelected = false;
            viewModel.Genres[2].IsSelected = false;

            Assert.AreEqual(viewModel.Movies.Count, 3);
        }
    }
}
