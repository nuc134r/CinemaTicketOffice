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

            Movies.Clear();

            var movies = repository.GetMovies().ToList();
            movies.ForEach(repository.GetMovieDetails);

            movies.ForEach(movie => Movies.Add(movie));
        }

        private void MoviesOnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
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