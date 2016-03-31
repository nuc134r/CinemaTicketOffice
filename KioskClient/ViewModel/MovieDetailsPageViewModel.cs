using System.Windows.Controls;
using DataAccess.Model;
using KioskClient.Properties;

namespace KioskClient.ViewModel
{
    public class MovieDetailsPageViewModel : ViewModelBase
    {
        public Movie Movie { get; set; }

        public string ShowtimesButtonText
        {
            get { return Movie.Showtimes.Count == 0 ? Resources.NoShowtimesText : Resources.ShowtimesListText; }
        }

        public bool IsShowtimesButtonEnabled
        {
            get { return Movie.Showtimes.Count > 0; }
        }

        public MovieDetailsPageViewModel(Movie movie, Page view)
        {
            Movie = movie;
            this.view = view;
        }

        public void GoBack()
        {
            Window.NavigateBack();
        }

        public void GoToShowtimeList(Movie movie)
        {
            Window.NavigateToShowtimeList(movie);
        }
    }
}