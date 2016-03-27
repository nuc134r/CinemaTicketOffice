using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DataAccess.Model;
using DataAccess.Repository;

namespace Administration.ViewModel
{
    public class ShowtimeEditorWindowViewModel
    {
        private readonly ShowtimeEditorWindow view;
        private readonly MovieRepository movieRepository;
        private readonly ShowtimeRepository showtimeRepository;
        private readonly bool createMode;

        public ShowtimeEditorWindowViewModel(ShowtimeEditorWindow view, Showtime showtime, MovieRepository movieRepository, ShowtimeRepository showtimeRepository)
        {
            if (showtime == null)
            {
                createMode = true;
                showtime = new Showtime();
            }

            this.view = view;
            this.movieRepository = movieRepository;
            this.showtimeRepository = showtimeRepository;
            Showtime = showtime;

            Movies = movieRepository.GetMovies().ToList();

            if (createMode) return;

            var movie = Movies.FirstOrDefault(_ => _.Id == showtime.Movie.Id);
            view.SelectedMovieIndex = Movies.IndexOf(movie);

            view.Time = showtime.Time;
        }

        public Showtime Showtime { get; set; }
        public List<Movie> Movies { get; set; }

        public void Save()
        {
            Showtime.Movie = Movies[view.SelectedMovieIndex];
            Showtime.Time = view.Time;

            showtimeRepository.Save(Showtime, !createMode);
        }
    }
}