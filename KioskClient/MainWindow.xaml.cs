using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DataAccess.Model;
using KioskClient.View;

namespace KioskClient
{
    public partial class MainWindow : IMainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            TitleTextBlock.Text = ((Page) MainFrame.Content).Title;
        }

        public void NavigateToMovieDetails(Movie movie)
        {
            var movieDetailsPage = new MovieDetailsPage { DataContext = movie };
            DataContext = movieDetailsPage;
        }

        public void NavigateToMovieCatalog()
        {
            var catalogPage = new CatalogPage();
            DataContext = catalogPage;
        }

        public void NavigateToShowtimeList(Movie movie)
        {
            var showtimesPage = new MovieShowtimesPage(movie);
            DataContext = showtimesPage;
        }
    }
}