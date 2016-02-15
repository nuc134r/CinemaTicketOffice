using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using DataAccess.Model;
using DataAccess.Repository;
using KioskClient.View;

namespace KioskClient.ViewModel
{
    public class CatalogPageViewModel : ViewModelBase
    {
        private readonly List<Movie> allMovies;
        private bool pauseFiltering;

        private new CatalogPage view
        {
            get { return (CatalogPage) base.view; }
            set { base.view = value; }
        }

        public CatalogPageViewModel(CatalogPage view, IMovieRepository movieRepository)
        {
            this.view = view;

            Genres = movieRepository.Genres;
            allMovies = movieRepository.Movies;

            Movies = new ObservableCollection<Movie>(allMovies);

            foreach (var genre in Genres)
            {
                genre.PropertyChanged += (sender, args) => FilterByGenres();
            }
        }

        public List<Genre> Genres { get; private set; }
        public ObservableCollection<Movie> Movies { get; private set; }
        
        public void ResetGenresFilter()
        {
            // Handler is attached to every Genre.IsSelected property
            // and will refilter movies collection on every property change.
            // So we stop filtering while unchecking IsSelected flags for performance.
            pauseFiltering = true;

            foreach (var genre in Genres)
            {
                genre.IsSelected = false;
            }

            pauseFiltering = false;
            FilterByGenres();
        }

        private void FilterByGenres()
        {
            if (pauseFiltering) return;

            List<Movie> matchingMovies;
            var selectedGenres = Genres.Where(genre => genre.IsSelected).Select(_ => _.Name).ToArray();

            if (selectedGenres.Length == 0)
            {
                // If no genres selected then show all the movies
                matchingMovies = allMovies;
            }
            else
            {
                matchingMovies =
                    allMovies.Where(movie => movie.Genres.Select(genre => genre.Name)
                        .Intersect(selectedGenres)
                        .Any()).ToList();
            }

            if (view != null) view.DetachSelectionChangedHandler();

            Movies.Clear();
            foreach (var movie in matchingMovies)
            {
                Movies.Add(movie);
            }

            if (view != null) view.AttachSelectionChangedHandler();
        }

        public void NavigateToMovieDetails(Movie movie)
        {
            TheWindow.NavigateToMovieDetails(movie);
        }
    }
}