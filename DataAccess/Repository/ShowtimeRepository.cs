using System;
using System.Collections.Generic;
using DataAccess.Model;

namespace DataAccess.Repository
{
    public class ShowtimeRepository
    {
        private readonly string connectionString;

        public ShowtimeRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<Showtime> GetShowtimes()
        {
            return new List<Showtime>
            {
                new Showtime() { Time = new DateTime(2016, 03, 10, 23, 51, 00), Price = 550, ThreeD = true},
                new Showtime() { Time = new DateTime(2016, 03, 11, 0, 05, 00), Price = 500, ThreeD = true},
                new Showtime() { Time = new DateTime(2016, 03, 12, 15, 00, 00), Price = 350, ThreeD = false}
            };
        } 
    }
}