using System.Windows;
using KioskClient.ViewModel;

namespace KioskClient.View
{
    public partial class ThanksPage
    {
        private readonly ThanksPageViewModel viewModel;
        
        public ThanksPage()
        {
            InitializeComponent();

            viewModel = new ThanksPageViewModel(this);
        }

        private void ThanksPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            viewModel.StartTimers();
        }
    }
}