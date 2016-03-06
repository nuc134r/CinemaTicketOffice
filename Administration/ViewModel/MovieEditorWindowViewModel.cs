using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Administration.Interfaces;
using DataAccess.Model;
using DataAccess.Repository;

namespace Administration.ViewModel
{
    public class MovieEditorWindowViewModel
    {
        private bool createMode;

        private readonly IMovieEditorWindow view;
        private readonly MovieRepository repository;
        public Movie Movie { get; set; }
        public ObservableCollection<Genre> Genres { get; set; }
        public List<AgeLimit> AgeLimits { get; set; }
        
        public AgeLimit AgeLimit { get; set; }

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
            
            view.SelectedAgeLimitIndex = AgeLimits.IndexOf(AgeLimit);

            var selectedGenres = Genres.Join(movie.Genres, 
                genre => genre.Id, 
                genre => genre.Id,
                (genre1, genre2) => genre1).ToList();

            selectedGenres.ForEach(_ => _.IsSelected = true);
        }

        public void Save()
        {
            Movie.Genres = Genres.Where(_ => _.IsSelected).ToList();
            Movie.AgeLimit = view.AgeLimit;

            if (createMode)
            {
                repository.CreateMovie(Movie);
            }
        }
    }
}