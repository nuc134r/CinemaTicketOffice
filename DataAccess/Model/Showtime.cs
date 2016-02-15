using System;

namespace DataAccess.Model
{
    public class Showtime
    {
        public Showtime(int id, Movie movie, DateTime time, Auditorium auditorium)
        {
            Id = id;
            Movie = movie;
            Time = time;
            Auditorium = auditorium;
        }

        public int Id { get; private set; }
        public Movie Movie { get; private set; }
        public DateTime Time { get; private set; }
        public Auditorium Auditorium { get; private set; }
    }
}