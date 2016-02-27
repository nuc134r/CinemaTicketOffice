using System.Windows.Controls;
using Administration.Properties;
using Administration.ViewModel;
using DataAccess;
using DataAccess.Repository;

namespace Administration.View
{
    public partial class MovieListPage : Page
    {
        private readonly MainWindow window;

        public MovieListPage(MainWindow window)
        {
            this.window = window;
            InitializeComponent();

            var connectionString = ConnectionStringBuilder.Build(
                Settings.Default.server,
                Settings.Default.database,
                Settings.Default.user,
                Settings.Default.password);

            var repository = new MovieRepository(connectionString);

            var viewModel = new MoviesListPageViewModel(this, repository);
            DataContext = viewModel.Movies;
        }

        public int ListCount
        {
            set { window.StatusBarCount.Content = value; }
        }
    }
}