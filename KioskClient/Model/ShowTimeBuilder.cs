using System;

namespace KioskClient.Model
{
    public class ShowTimeBuilder
    {
        private Auditorium auditorium;
        private int id;
        private Movie movie;
        private DateTime time;

        public static ShowTimeBuilder Create()
        {
            return new ShowTimeBuilder();
        }

        public ShowTimeBuilder WithId(int id)
        {
            this.id = id;
            return this;
        }

        public ShowTimeBuilder WithMovie(Movie movie)
        {
            this.movie = movie;
            return this;
        }

        public ShowTimeBuilder WithTime(DateTime time)
        {
            this.time = time;
            return this;
        }

        public ShowTimeBuilder WithAuditorium(Auditorium auditorium)
        {
            this.auditorium = auditorium;
            return this;
        }

        public ShowTime Please()
        {
            return new ShowTime(id, movie, time, auditorium);
        }
    }
}