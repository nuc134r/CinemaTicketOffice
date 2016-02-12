using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using KioskClient.DataAccessLayer;
using KioskClient.Model;
using KioskClient.View;

namespace KioskClient.ViewModel
{
    public class CatalogPageViewModel : ViewModelBase
    {
        private readonly List<Movie> allMovies; 

        public CatalogPageViewModel(Page view, ICatalogPageDataAccessLayer dataAccessLayer)
        {
            this.view = view;

            Genres = dataAccessLayer.GetMovieGenres();

            foreach (var genre in Genres)
            {
                genre.PropertyChanged += (sender, args) => FilterByGenres();
            }

            allMovies = dataAccessLayer.GetMovies();

            Movies = new ObservableCollection<Movie>(allMovies);
        }

        public List<Genre> Genres { get; private set; }
        public ObservableCollection<Movie> Movies { get; private set; }

        public void ResetGenresFilter()
        {
            foreach (var genre in Genres)
            {
                genre.IsSelected = false;
            }
        }

        private void FilterByGenres()
        {
            var selectedGenres = Genres.Where(genre => genre.IsSelected).Select(_ => _.Name).ToArray();

            if (selectedGenres.Length == 0)
            {
                Movies.Clear();
                foreach (var movie in allMovies) { Movies.Add(movie); }
                return;
            }

            var matchingMovies = 
                allMovies.Where(movie => movie.Genres.Select(genre => genre.Name)
                                                     .Intersect(selectedGenres)
                                                     .Any()).ToList();
            
            Movies.Clear();
            foreach (var movie in matchingMovies) { Movies.Add(movie); }
        }
    }
}