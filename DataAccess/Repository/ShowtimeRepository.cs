﻿using System;
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

            result.ThrowIfException();

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
                        Name = row["AuditoriumName"].ToString(),
                        Rows = row["AuditoriumRows"].ToInt(),
                        Seats = row["AuditoriumSeats"].ToInt()
                    },
                    Price = row["Price"].ToInt(),
                    ThreeDee = row["ThreeDee"].ToBool()
                };
            }
        }

        public IEnumerable<Showtime> GetPendingShowtimes(int movieId)
        {
            var executor = new CommandExecutor("dbo.BrowsePendingShowtimes", connectionString);
            executor.SetParam("@MovieId", movieId, SqlDbType.Int);
            var result = executor.ExecuteCommand();

            result.ThrowIfException();

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
                        Name = row["AuditoriumName"].ToString(),
                        Rows = row["AuditoriumRows"].ToInt(),
                        Seats = row["AuditoriumSeats"].ToInt()
                    },
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
                executor.SetParam("@Id", showtime.Id, SqlDbType.Int);
            }
            else
            {
                executor = new CommandExecutor("dbo.CreateShowtime", connectionString);
            }

            executor.SetParam("@MovieId", showtime.Movie.Id, SqlDbType.Int);
            executor.SetParam("@AuditoriumId", showtime.Auditorium.Id, SqlDbType.Int);
            executor.SetParam("@ShowtimeDate", showtime.Time, SqlDbType.DateTime);
            executor.SetParam("@Price", showtime.Price, SqlDbType.Money);
            executor.SetParam("@ThreeDee", showtime.ThreeDee, SqlDbType.Bit);

            executor.ExecuteCommand(true).ThrowIfException();
        }

        public void Delete(Showtime showtime)
        {
            var executor = new CommandExecutor("dbo.DeleteShowtime", connectionString);
            executor.SetParam("@Id", showtime.Id, SqlDbType.Int);

            executor.ExecuteCommand(true).ThrowIfException();
        }

        public IEnumerable<Auditorium> GetAuditoriums()
        {
            var executor = new CommandExecutor("dbo.BrowseAuditoriums", connectionString);
            var result = executor.ExecuteCommand();

            result.ThrowIfException();

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

        public void Save(Auditorium auditorium, bool update)
        {
            CommandExecutor executor;
            if (update)
            {
                executor = new CommandExecutor("dbo.UpdateAuditorium", connectionString);
                executor.SetParam("@Id", auditorium.Id, SqlDbType.Int);
            }
            else
            {
                executor = new CommandExecutor("dbo.CreateAuditorium", connectionString);
            }

            executor.SetParam("@Name", auditorium.Name, SqlDbType.NVarChar);
            executor.SetParam("@Rows", auditorium.Rows, SqlDbType.Int);
            executor.SetParam("@Seats", auditorium.Seats, SqlDbType.Int);

            executor.ExecuteCommand(true).ThrowIfException();
        }

        public void Delete(Auditorium auditorium)
        {
            var executor = new CommandExecutor("dbo.DeleteAuditorium", connectionString);
            executor.SetParam("@Id", auditorium.Id, SqlDbType.Int);

            executor.ExecuteCommand(true).ThrowIfException();
        }
    }
}