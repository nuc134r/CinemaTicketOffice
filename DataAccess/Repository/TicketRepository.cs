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

        public void RegisterTickets(int showtimeId, List<Seat> seats)
        {
            var executor = new CommandExecutor("dbo.RegisterTickets", connectionString);

            executor.SetParam("@ShowtimeId", showtimeId, SqlDbType.Int);

            var seatList = new DataTable();
            seatList.Columns.Add("SeatNumber");
            seatList.Columns.Add("RowNumber");

            seats.ForEach(seat => seatList.Rows.Add(seat.SeatNumber, seat.RowNumber));
            executor.SetParam("@Tickets", seatList, SqlDbType.Structured, "dbo.SeatList");

            executor.ExecuteCommand(true).ThrowIfException();
        }

        public IEnumerable<Ticket> GetTickets()
        {
            var executor = new CommandExecutor("dbo.BrowseTickets", connectionString);
            var result = executor.ExecuteCommand();

            result.ThrowIfException();

            var dataSet = result as DataSet;

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                yield return new Ticket
                {
                    Id = row["Id"].ToInt(),
                    Seat = new Seat
                    {
                        SeatNumber = row["SeatNumber"].ToInt(),
                        RowNumber = row["RowNumber"].ToInt(),
                    },
                    Showtime = new Showtime
                    {
                        Auditorium = new Auditorium
                        {
                            Name = row["Name"].ToString()
                        },
                        Movie = new Movie
                        {
                            Title = row["Title"].ToString()
                        },
                        Time = row["ShowtimeDate"].ToDate()
                    },
                    SellDate = row["SellDate"].ToDate()
                };
            }
        }

        public void Delete(Ticket ticket)
        {
            var executor = new CommandExecutor("dbo.DeleteTicket", connectionString);
            executor.SetParam("@Id", ticket.Id, SqlDbType.Int);

            executor.ExecuteCommand(true).ThrowIfException();
        }
    }
}