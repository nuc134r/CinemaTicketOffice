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
        private readonly IMovieRepository repository;

        public ObservableCollection<Movie> Movies;

        public MoviesListPageViewModel(MovieListPage view, IMovieRepository repository)
        {
            this.view = view;
            this.repository = repository;

            RetrieveMovies();
        }

        private void RetrieveMovies()
        {
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

        public void OpenMovieEditor(Movie movie)
        {
            var movieEditor = new MovieEditorWindow(movie != null ? movie.Clone() : null);
            var result = movieEditor.ShowDialog();

            if (result.HasValue && result.Value)
            {
                RetrieveMovies();
            }
        }
    }
}