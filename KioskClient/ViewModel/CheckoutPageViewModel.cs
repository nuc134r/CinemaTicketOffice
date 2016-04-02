using System.Windows.Media;
using KioskClient.View;

namespace KioskClient.ViewModel
{
    public class CheckoutPageViewModel : ViewModelBase
    {
        public CheckoutPageViewModel(CheckoutPage view, int total)
        {
            Total = total;

            this.view = view;

            ArrowAnimatedBrush = new SolidColorBrush();
        }

        public SolidColorBrush ArrowAnimatedBrush { get; set; }

        public int Total { get; set; }

        public void Cancel()
        {
            Window.NavigateBack();
        }

        public void GoToThanksPage()
        {
            Window.NavigateToThanksPage();
        }
    }
}