using System;

namespace DataAccess.Model
{
    public class Showtime
    {
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public DateTime Time { get; set; }
        public Auditorium Auditorium { get; set; }
        public int Price { get; set; }
        public bool ThreeD { get; set; }
    }
}