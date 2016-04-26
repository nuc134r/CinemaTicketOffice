using System.Windows;
using System.Windows.Controls;
using DataAccess;
using DataAccess.Model;
using DataAccess.Repository;
using KioskClient.Properties;
using KioskClient.ViewModel;

namespace KioskClient.View
{
    public partial class CatalogPage : IRefreshablePage
    {
        private readonly CatalogPageViewModel viewModel;
        private MovieRepository repository;

        public CatalogPage()
        {
            InitializeComponent();
            SetDefaults();

            var connectionString = ConnectionStringBuilder.Build(
                Settings.Default.server,
                Settings.Default.database,
                Settings.Default.user,
                Settings.Default.password);

            repository = new MovieRepository(connectionString);
            viewModel = new CatalogPageViewModel(this, repository);

            DataContext = viewModel;
        }

        private void SetDefaults()
        {
            GenresGrid.Width = 0;
        }

        public void HideNoResultsText()
        {
            noResultsBox.Visibility = Visibility.Hidden;
        }

        public void ShowNoResultsText()
        {
            noResultsBox.Visibility = Visibility.Visible;
        }

        private void ResetFilterButton_OnClick(object sender, RoutedEventArgs e)
        {
            viewModel.ResetGenresFilter();
            if (GenresListBox.Items.Count != 0)
                GenresListBox.ScrollIntoView(GenresListBox.Items[0]);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UnwireHandlers();
            MoviesListBox.SelectedIndex = -1;
            WireHandlers();

            var movie = (Movie) e.AddedItems[0];
            viewModel.GoToMovieDetails(movie);
        }

        public void UnwireHandlers()
        {
            MoviesListBox.SelectionChanged -= ListBox_SelectionChanged;
        }

        public void WireHandlers()
        {
            MoviesListBox.SelectionChanged += ListBox_SelectionChanged;
        }

        public void Refresh()
        {
            Settings.Default.Reload();

            repository.ConnectionString = ConnectionStringBuilder.Build(
                Settings.Default.server,
                Settings.Default.database,
                Settings.Default.user,
                Settings.Default.password);

            viewModel.Refresh();
        }
    }
}