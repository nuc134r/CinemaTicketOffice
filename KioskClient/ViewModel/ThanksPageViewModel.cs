using System.Windows.Controls;

namespace KioskClient.ViewModel
{
    public class ThanksPageViewModel : ViewModelBase
    {
        public ThanksPageViewModel(Page view)
        {
            this.view = view;
        }

        public void GoToCatalogPage()
        {
            Window.NavigateToMovieCatalog();
        }
    }
}