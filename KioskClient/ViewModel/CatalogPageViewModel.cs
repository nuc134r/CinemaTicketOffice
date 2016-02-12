using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using KioskClient.DataAccessLayer;
using KioskClient.Model;
using KioskClient.View;

namespace KioskClient.ViewModel
{
    public class CatalogPageViewModel : ViewModelBase
    {
        private readonly CatalogPageDataAccessLayer dataAccessLayer;
        private List<Movie> allMovies; 

        public CatalogPageViewModel()
        {
            dataAccessLayer = new CatalogPageDataAccessLayer();
            view = new CatalogPage(this);

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

        public override string Title
        {
            get { return "Выбор фильма"; }
        }

        public void ResetGenresFilter()
        {
            foreach (var genre in Genres)
            {
                genre.IsSelected = false;
            }
            Movies.Clear();
            foreach (var matchingMovie in allMovies)
            {
                Movies.Add(matchingMovie);
            }
        }

        public void FilterByGenres()
        {
            var selectedGenres = Genres.Where(genre => genre.IsSelected).Select(_ => _.Name);

            var matchingMovies = 
                allMovies.Where(movie => movie.Genres.Select(genre => genre.Name)
                .Intersect(selectedGenres).Any()).ToList();
            
            Movies.Clear();
            foreach (var matchingMovie in matchingMovies)
            {
                Movies.Add(matchingMovie);
            }
        }
    }
}