using System.Windows.Controls;
using DataAccess.Model;

namespace KioskClient.ViewModel
{
    public class MovieDetailsPageViewModel : ViewModelBase
    {
        public MovieDetailsPageViewModel(Page view)
        {
            this.view = view;
        }

        public void GoToMovieCatalog()
        {
            TheWindow.NavigateToMovieCatalog();
        }

        public void GoToShowtimeList(Movie movie)
        {
            TheWindow.NavigateToShowtimeList(movie);
        }
    }
}