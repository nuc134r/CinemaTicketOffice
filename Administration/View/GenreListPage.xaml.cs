using System.Windows.Controls;
using System.Windows.Input;
using Administration.Properties;
using Administration.ViewModel;
using DataAccess;
using DataAccess.Model;
using DataAccess.Repository;

namespace Administration.View
{
    public partial class GenreListPage
    {
        private readonly MainWindow window;
        private readonly GenreListPageViewModel viewModel;

        private Genre SelectedGenre
        {
            get { return (Genre)listView.SelectedItem; }
        }

        public GenreListPage(MainWindow window)
        {
            this.window = window;
            InitializeComponent();

            var connectionString = ConnectionStringBuilder.Build(
                Settings.Default.server,
                Settings.Default.database,
                Settings.Default.user,
                Settings.Default.password);

            var repository = new MovieRepository(connectionString);

            viewModel = new GenreListPageViewModel(this, repository);
            DataContext = viewModel.Genres;
        }

        public int ListCount
        {
            set { window.StatusBarCount.Content = value; }
        }

        private void ListView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            viewModel.OpenGenreEditor(SelectedGenre);
        }

        private void CreateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            viewModel.OpenGenreEditor(null);
        }

        private void EditButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            viewModel.OpenGenreEditor(SelectedGenre);
        }

        private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}