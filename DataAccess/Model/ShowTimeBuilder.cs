using System;

namespace DataAccess.Model
{
    public class ShowtimeBuilder
    {
        private Auditorium auditorium;
        private int id;
        private Movie movie;
        private DateTime time;

        public static ShowtimeBuilder Create()
        {
            return new ShowtimeBuilder();
        }

        public ShowtimeBuilder WithId(int id)
        {
            this.id = id;
            return this;
        }

        public ShowtimeBuilder WithMovie(Movie movie)
        {
            this.movie = movie;
            return this;
        }

        public ShowtimeBuilder WithTime(DateTime time)
        {
            this.time = time;
            return this;
        }

        public ShowtimeBuilder WithAuditorium(Auditorium auditorium)
        {
            this.auditorium = auditorium;
            return this;
        }

        public Showtime Please()
        {
            return new Showtime(id, movie, time, auditorium);
        }
    }
}