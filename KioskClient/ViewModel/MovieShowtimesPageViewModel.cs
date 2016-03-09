using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using DataAccess.Model;
using DataAccess.Repository;
using KioskClient.View;

namespace KioskClient.ViewModel
{
    public class MovieShowtimesPageViewModel : ViewModelBase
    {
        private readonly ShowtimeRepository repository;

        public ObservableCollection<Showtime> Showtimes { get; set; }
        public Movie Movie { get; set; }

        public MovieShowtimesPageViewModel(MovieShowtimesPage view, ShowtimeRepository repository, Movie movie)
        {
            this.view = view;
            this.repository = repository;
            Movie = movie;

            RetrieveData();
        }

        private void RetrieveData()
        {
            if (Showtimes == null)
            {
                Showtimes = new ObservableCollection<Showtime>();
            }

            var showtimes = repository.GetShowtimes().ToList();

            Showtimes.Clear();
            showtimes.ForEach(showtime => Showtimes.Add(showtime));
        }

        public void GoBack()
        {
            Window.NavigateToMovieDetails(Movie);
        }
    }
}