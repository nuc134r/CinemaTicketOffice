using KioskClient.View;

namespace KioskClient.ViewModel
{
    public class CheckoutPageViewModel : ViewModelBase
    {
        public CheckoutPageViewModel(CheckoutPage view, int total)
        {
            Total = total;

            this.view = view;
        }

        public int Total { get; set; }

        public void Cancel()
        {
            Window.NavigateBack();
        }
    }
}