using System.Collections.Generic;
using KioskClient.DataAccessLayer;
using KioskClient.Model;
using KioskClient.View;

namespace KioskClient.ViewModel
{
    public class CatalogPageViewModel : ViewModelBase
    {
        private readonly CatalogPageDataAccessLayer dataAccessLayer;

        public CatalogPageViewModel()
        {
            dataAccessLayer = new CatalogPageDataAccessLayer();
            view = new CatalogPage(this);

            Genres = dataAccessLayer.GetMovieGenres();
            Movies = dataAccessLayer.GetMovies();
        }

        public List<Genre> Genres { get; private set; }
        public List<Movie> Movies { get; private set; }

        public override string Title
        {
            get { return "Выбор фильма"; }
        }

        public void ResetGenresFilter()
        {
            foreach (var genre in Genres)
            {
                genre.IsSelected = false;
            }
        }
    }
}