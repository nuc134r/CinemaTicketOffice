using System.Collections.Generic;
using DataAccess.Model;

namespace DataAccess.Repository
{
    public class TicketRepository
    {
        private readonly string connectionString;

        public TicketRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Seat> GetOccupiedSeats(int showtimeId)
        {
            return new List<Seat>
            {
                new Seat {RowNumber = 1, SeatNumber = 2},
                new Seat {RowNumber = 2, SeatNumber = 5}
            };
        }

        public void SaveTickets(int showtimeId, List<Seat> seats)
        {
            
        }
    }
}