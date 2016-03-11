using System.Windows;
using System.Windows.Controls;
using DataAccess.Model;
using KioskClient.ViewModel;

namespace KioskClient.View
{
    public partial class MovieDetailsPage
    {
        private readonly MovieDetailsPageViewModel viewModel;

        public MovieDetailsPage(Movie movie)
        {
            InitializeComponent();

            viewModel = new MovieDetailsPageViewModel(movie, this);
            DataContext = viewModel;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.GoBack();
        }

        private void BuyTicketsButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.GoToShowtimeList((Movie)DataContext);
        }
    }
}