using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataAccess.Model;
using DataAccess.Repository;

namespace Administration.View
{
    public partial class MovieListPage : Page
    {
        private readonly MainWindow window;
        private ObservableCollection<Movie> movies;
        private readonly MovieRepository repository = new MovieRepository();

        public MovieListPage(MainWindow window)
        {
            this.window = window;
            InitializeComponent();

            DataContext = movies;
        }

        private void MovieListPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            movies = new ObservableCollection<Movie>(repository.GetMovies());
            window.StatusBarCount.Content = movies.Count;
            movies.CollectionChanged += MoviesOnCollectionChanged;
            listView.ItemsSource = movies;
        }

        private void MoviesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            window.StatusBarCount.Content = movies.Count;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
