using System;
using System.Collections.Generic;
using System.Data;
using DataAccess.Connection;
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
            var executor = new CommandExecutor("dbo.BrowseShowtimes", connectionString);
            var result = executor.ExecuteCommand();

            var exception = result as Exception;
            if (exception != null) throw exception;

            var dataSet = result as DataSet;

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                yield return new Showtime
                {
                    Id = row["Id"].ToInt(),
                    Time = row["ShowtimeDate"].ToDate(),
                    Movie = new Movie
                    {
                        Id = row["MovieId"].ToInt(),
                        Title = row["MovieTitle"].ToString()
                    },
                    Auditorium = new Auditorium
                    {
                        Id = row["AuditoriumId"].ToInt(),
                        Name = row["AuditoriumName"].ToString()
                    },
                    Price = row["Price"].ToInt(),
                    ThreeDee = row["ThreeDee"].ToBool()
                };
            }
        }

        public IEnumerable<Showtime> GetPendingShowtimes()
        {
            var executor = new CommandExecutor("dbo.BrowsePendingShowtimes", connectionString);
            var result = executor.ExecuteCommand();

            var exception = result as Exception;
            if (exception != null) throw exception;

            var dataSet = result as DataSet;

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                yield return new Showtime
                {
                    Id = row["Id"].ToInt(),
                    Time = row["ShowtimeDate"].ToDate(),
                    Price = row["Price"].ToInt(),
                    ThreeDee = row["ThreeDee"].ToBool()
                };
            }
        }

        public void Save(Showtime showtime, bool update)
        {
            CommandExecutor executor;
            if (update)
            {
                executor = new CommandExecutor("dbo.UpdateShowtime", connectionString);
                executor.AddParam("@Id", showtime.Id, SqlDbType.Int);
            }
            else
            {
                executor = new CommandExecutor("dbo.CreateShowtime", connectionString);
            }

            executor.AddParam("@MovieId", showtime.Movie.Id, SqlDbType.Int);
            executor.AddParam("@AuditoriumId", 1, SqlDbType.Int);
            executor.AddParam("@ShowtimeDate", showtime.Time, SqlDbType.DateTime);
            executor.AddParam("@Price", showtime.Price, SqlDbType.Money);
            executor.AddParam("@ThreeDee", showtime.ThreeDee, SqlDbType.Bit);

            var result = executor.ExecuteCommand(true);
            var exception = result as Exception;
            if (exception != null) throw exception;
        }

        public void Delete(Showtime showtime)
        {
            var executor = new CommandExecutor("dbo.DeleteShowtime", connectionString);
            executor.AddParam("@Id", showtime.Id, SqlDbType.Int);
            var result = executor.ExecuteCommand();

            var exception = result as Exception;
            if (exception != null) throw exception;
        }

        public IEnumerable<Auditorium> GetAuditoriums()
        {
            var executor = new CommandExecutor("dbo.BrowseAuditoriums", connectionString);
            var result = executor.ExecuteCommand();

            var exception = result as Exception;
            if (exception != null) throw exception;

            var dataSet = result as DataSet;

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                yield return new Auditorium
                {
                    Id = row["Id"].ToInt(),
                    Name = row["Name"].ToString(),
                    Rows = row["RowsNumber"].ToInt(),
                    Seats = row["SeatsNumber"].ToInt()
                };
            }
        }
    }
}