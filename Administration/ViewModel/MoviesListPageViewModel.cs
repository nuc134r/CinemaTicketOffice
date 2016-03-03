using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Administration.View;
using DataAccess.Model;
using DataAccess.Repository;

namespace Administration.ViewModel
{
    public class MoviesListPageViewModel
    {
        private readonly MovieListPage view;

        public readonly ObservableCollection<Movie> Movies;

        public MoviesListPageViewModel(MovieListPage view, IMovieRepository repository)
        {
            this.view = view;

            var movies = repository.GetMovies().ToList();
            movies.ForEach(repository.GetMovieDetails);

            Movies = new ObservableCollection<Movie>(movies);
            view.ListCount = Movies.Count;
            Movies.CollectionChanged += MoviesOnCollectionChanged;
        }

        private void MoviesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            view.ListCount = Movies.Count;
        }

        public void OpenMovieDetails(Movie movie)
        {
            var movieEditor = new MovieEditorWindow(movie);
            movieEditor.ShowDialog();
        }
    }
}