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
        private readonly IMovieEditorWindow view;
        public Movie Movie { get; set; }
        public ObservableCollection<Genre> Genres { get; set; }
        public List<AgeLimit> AgeLimits { get; set; }
        
        public AgeLimit AgeLimit { get; set; }

        public MovieEditorWindowViewModel(IMovieEditorWindow view, Movie movie, MovieRepository movieRepository)
        {
            this.view = view;
            Movie = movie;

            Genres = new ObservableCollection<Genre>(movieRepository.GetGenres());
            AgeLimits = movieRepository.GetAgeLimits().ToList();

            AgeLimit = AgeLimits.FirstOrDefault(limit => limit.Id == movie.AgeLimit.Id);
            view.SelectedAgeLimitIndex = AgeLimits.IndexOf(AgeLimit);

            var selectedGenres = Genres.Join(movie.Genres, 
                genre => genre.Id, 
                genre => genre.Id,
                (genre1, genre2) => genre1).ToList();

            selectedGenres.ForEach(_ => _.IsSelected = true);
        }
    }
}