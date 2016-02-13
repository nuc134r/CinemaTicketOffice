using System.Windows.Controls;

namespace KioskClient.ViewModel
{
    public class MovieDetailsPageViewModel : ViewModelBase
    {
        public MovieDetailsPageViewModel(Page view)
        {
            this.view = view;
        }

        public void NaivigateBack()
        {
            TheWindow.MainFrame.GoBack();
        }
    }
}