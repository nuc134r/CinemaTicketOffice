using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using KioskClient.Model;
using KioskClient.View;

namespace KioskClient
{
    public partial class MainWindow : Window
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
    }
}