using System;
using DataAccess.Model;

namespace DataAccess.Builders
{
    public class ShowtimeBuilder
    {
        private Auditorium auditorium;
        private int id;
        private Movie movie;
        private int price;
        private bool threeD;
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

        public ShowtimeBuilder WithPrice(int price)
        {
            this.price = price;
            return this;
        }

        public ShowtimeBuilder WithThreeD(bool threeD)
        {
            this.threeD = threeD;
            return this;
        }

        public Showtime Please()
        {
            return new Showtime(id, movie, time, auditorium, price, threeD);
        }
    }
}