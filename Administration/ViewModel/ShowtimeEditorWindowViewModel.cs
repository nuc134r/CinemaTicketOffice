using System.Collections.Generic;
using System.Linq;
using Administration.Editors;
using DataAccess.Model;
using DataAccess.Repository;

namespace Administration.ViewModel
{
    public class ShowtimeEditorWindowViewModel
    {
        private readonly MovieRepository movieRepository;
        private readonly ShowtimeRepository showtimeRepository;
        private readonly ShowtimeEditorWindow view;

        public ShowtimeEditorWindowViewModel(ShowtimeEditorWindow view, Showtime showtime,
            MovieRepository movieRepository, ShowtimeRepository showtimeRepository)
        {
            if (showtime == null)
            {
                CreateMode = true;
                showtime = new Showtime();
            }

            this.view = view;
            this.movieRepository = movieRepository;
            this.showtimeRepository = showtimeRepository;
            Showtime = showtime;

            Movies = movieRepository.GetMovies().ToList();
            Auditoriums = showtimeRepository.GetAuditoriums().ToList();

            if (CreateMode) return;

            var movie = Movies.FirstOrDefault(_ => _.Id == showtime.Movie.Id);
            view.SelectedMovieIndex = Movies.IndexOf(movie);

            var auditorium = Auditoriums.FirstOrDefault(_ => _.Id == showtime.Auditorium.Id);
            view.SelectedAuditoriumIndex = Auditoriums.IndexOf(auditorium);

            view.Time = showtime.Time;
        }

        public bool CreateMode { get; set; }

        public Showtime Showtime { get; set; }
        public List<Movie> Movies { get; set; }
        public List<Auditorium> Auditoriums { get; set; }

        public void Save()
        {
            Showtime.Auditorium = Auditoriums[view.SelectedAuditoriumIndex];
            Showtime.Movie = Movies[view.SelectedMovieIndex];
            Showtime.Time = view.Time;

            showtimeRepository.Save(Showtime, !CreateMode);
        }
    }
}