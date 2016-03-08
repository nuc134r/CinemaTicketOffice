using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Administration.Interfaces;
using DataAccess.Model;
using DataAccess.Repository;

namespace Administration.ViewModel
{
    public sealed class MovieEditorWindowViewModel
    {
        private readonly MovieRepository repository;

        private readonly IMovieEditorWindow view;
        private readonly bool createMode;

        public MovieEditorWindowViewModel(IMovieEditorWindow view, Movie movie, MovieRepository repository)
        {
            if (movie == null)
            {
                createMode = true;
                movie = new Movie();
            }

            this.view = view;
            this.repository = repository;
            Movie = movie;

            Genres = new ObservableCollection<Genre>(repository.GetGenres());
            AgeLimits = repository.GetAgeLimits().ToList();

            if (createMode) return;

            var limit = AgeLimits.FirstOrDefault(_ => _.Id == movie.AgeLimit.Id);
            view.SelectedAgeLimitIndex = AgeLimits.IndexOf(limit);

            var selectedGenres = Genres.Join(movie.Genres,
                genre => genre.Id,
                genre => genre.Id,
                (genre1, genre2) => genre1).ToList();

            selectedGenres.ForEach(_ => _.IsSelected = true);
        }

        public Movie Movie { get; set; }
        public ObservableCollection<Genre> Genres { get; set; }
        public List<AgeLimit> AgeLimits { get; set; }
        
        public void Save()
        {
            Movie.Genres = Genres.Where(_ => _.IsSelected).ToList();
            Movie.AgeLimit = view.AgeLimit;

            repository.SaveMovie(Movie, !createMode);
        }
    }
}