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
            return new List<Showtime>();
        } 
    }
}