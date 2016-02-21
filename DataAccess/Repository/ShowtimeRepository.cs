using System.Collections.Generic;
using DataAccess.Model;

namespace DataAccess.Repository
{
    public class ShowtimeRepository : IShowtimeRepository
    {
        private readonly Movie movie;
        private List<Auditorium> auditoriums;
        private List<Showtime> showtimes;

        public ShowtimeRepository(Movie movie)
        {
            this.movie = movie;
        }

        public List<Showtime> Showtimes
        {
            get
            {
                auditoriums = auditoriums ?? LoadAuditoriums();
                showtimes = showtimes ?? LoadShowtimes();
                return showtimes;
            }
        }

        public void Refresh()
        {
            showtimes = LoadShowtimes();
        }

        private List<Auditorium> LoadAuditoriums()
        {
            return new List<Auditorium>
            {
                
            };
        }

        private List<Showtime> LoadShowtimes()
        {
            return new List<Showtime>
            {
                new Showtime() { Movie = movie }
            };
        }
    }
}