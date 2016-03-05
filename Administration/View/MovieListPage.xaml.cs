using System.Windows.Controls;
using Administration.Properties;
using Administration.ViewModel;
using DataAccess;
using DataAccess.Model;
using DataAccess.Repository;

namespace Administration.View
{
    public partial class MovieListPage : Page
    {
        private readonly MainWindow window;
        private readonly MoviesListPageViewModel viewModel;

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
            
            viewModel = new MoviesListPageViewModel(this, repository);
            DataContext = viewModel.Movies;
        }

        public int ListCount
        {
            set { window.StatusBarCount.Content = value; }
        }
        
        private void listView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var movie = (Movie)listView.SelectedItem;
            viewModel.OpenMovieDetails(movie.Clone());
        }
    }
}