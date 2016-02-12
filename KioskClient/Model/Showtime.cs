using System;

namespace KioskClient.Model
{
    public class Showtime
    {
        public int Id { get; private set; }
        public Movie Movie { get; private set; }
        public DateTime Time { get; private set; }
    }
}