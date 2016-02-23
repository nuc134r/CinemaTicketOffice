using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public int ListCount { get { return movies != null ? movies.Count : 0; } }

        private ObservableCollection<Movie> movies;
        private readonly MovieRepository repository = new MovieRepository();

        public MovieListPage()
        {
            InitializeComponent();

            DataContext = movies;
        }

        private void MovieListPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            
            movies = new ObservableCollection<Movie>(repository.GetMovies());
            listView.ItemsSource = movies;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            movies.Clear();
        }
    }
}
