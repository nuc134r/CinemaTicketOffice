using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DataAccess;
using DataAccess.Model;
using DataAccess.Repository;
using KioskClient.Properties;
using KioskClient.View;

namespace KioskClient
{
    public partial class MainWindow : IMainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            SetupLogo();
        }

        private void SetupLogo()
        {
            var connectionString = ConnectionStringBuilder.Build(
                            Settings.Default.server,
                            Settings.Default.database,
                            Settings.Default.user,
                            Settings.Default.password);

            var repository = new SettingsRepository(connectionString);
            LogoImage.Source = repository.GetLogo();
        }

        private void MainFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            TitleTextBlock.Text = ((Page) MainFrame.Content).Title;
        }

        public void NavigateToMovieDetails(Movie movie)
        {
            var movieDetailsPage = new MovieDetailsPage(movie);
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

        public void NavigateToAuditoriumMap(Showtime showtime)
        {
            var auditoriumMapPage = new AuditoriumMapPage(showtime);
            DataContext = auditoriumMapPage;
        }
    }
}