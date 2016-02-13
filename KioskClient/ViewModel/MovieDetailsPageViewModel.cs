using System.Windows.Controls;

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
    }
}