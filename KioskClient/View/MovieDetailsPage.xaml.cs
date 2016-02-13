using System.Windows;
using System.Windows.Controls;
using KioskClient.Model;
using KioskClient.ViewModel;

namespace KioskClient.View
{
    public partial class MovieDetailsPage : Page
    {
        private readonly MovieDetailsPageViewModel viewModel;

        public MovieDetailsPage()
        {
            InitializeComponent();

            viewModel = new MovieDetailsPageViewModel(this);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.GoToMovieCatalog();
        }

        private void BuyTicketsButton_Click(object sender, RoutedEventArgs e)
        {
            var movie = (Movie) DataContext;

            viewModel.GoToShowtimeList(movie);
        }
    }
}