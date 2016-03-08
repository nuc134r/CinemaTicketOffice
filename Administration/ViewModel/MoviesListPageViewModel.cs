using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using Administration.Properties;
using Administration.View;
using DataAccess.Model;
using DataAccess.Repository;

namespace Administration.ViewModel
{
    public class MoviesListPageViewModel
    {
        private readonly IMovieRepository repository;
        private readonly MovieListPage view;

        public ObservableCollection<Movie> Movies { get; private set; } 

        public MoviesListPageViewModel(MovieListPage view, IMovieRepository repository)
        {
            this.view = view;
            this.repository = repository;

            RetrieveMovies();
        }

        private void RetrieveMovies()
        {
            if (Movies == null)
            {
                Movies = new ObservableCollection<Movie>();
                Movies.CollectionChanged += MoviesOnCollectionChanged;
            }

            var movies = repository.GetMovies().ToList();

            Movies.Clear();
            movies.ForEach(movie => Movies.Add(movie));
        }

        private void MoviesOnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            view.ListCount = Movies.Count;
        }

        public void OpenMovieEditor(Movie movie)
        {
            if (movie != null)
            {
                repository.GetMovieDetails(movie);
                movie = movie.Clone();
            }

            var movieEditor = new MovieEditorWindow(movie);
            var result = movieEditor.ShowDialog();

            if (result.HasValue && result.Value)
            {
                RetrieveMovies();
            }
        }

        public void DeleteMovie(Movie movie)
        {
            var result = MessageBox.Show(
                string.Format(Resources.DeleteMovieConfirmation, movie.Title),
                Resources.DeleteConfirmationCaption,
                MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    repository.DeleteMovie(movie);
                    RetrieveMovies();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}