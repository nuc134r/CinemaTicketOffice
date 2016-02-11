using System.Collections.Generic;
using System.Collections.ObjectModel;
using KioskClient.DataAccessLayer;
using KioskClient.Model;
using KioskClient.View;

namespace KioskClient.ViewModel
{
    public class CatalogPageViewModel : ViewModelBase
    {
        private readonly CatalogPageDataAccessLayer dataAccessLayer;

        public List<Genre> Genres { get; private set; }

        public CatalogPageViewModel()
        {
            dataAccessLayer = new CatalogPageDataAccessLayer();
            Genres = dataAccessLayer.GetMovieGenres();

            view = new CatalogPage(this);
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