using System;
using System.Collections.Generic;
using System.Data;
using DataAccess.Connection;
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

        public IEnumerable<Seat> GetOccupiedSeats(int showtimeId)
        {
            var executor = new CommandExecutor("dbo.GetOccupiedSeats", connectionString);
            executor.SetParam("@ShowtimeId", showtimeId, SqlDbType.Int);
            var result = executor.ExecuteCommand();
                
            result.ThrowIfException();

            var dataSet = result as DataSet;

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                yield return new Seat
                {
                    SeatNumber = row["SeatNumber"].ToInt(),
                    RowNumber = row["RowNumber"].ToInt()
                };
            }
        }

        public void RegisterTicket(int showtimeId, Seat seat)
        {
            var executor = new CommandExecutor("dbo.RegisterTickets", connectionString);
            executor.SetParam("@ShowtimeId", showtimeId, SqlDbType.Int);
            executor.SetParam("@Seat", seat.SeatNumber, SqlDbType.Int);
            executor.SetParam("@Row", seat.RowNumber, SqlDbType.Int);

            executor.ExecuteCommand(true).ThrowIfException();
        }

        public IEnumerable<Ticket> GetTickets()
        {
            return new List<Ticket>();
        }

        public void Delete(Ticket ticket)
        {
            
        }
    }
}