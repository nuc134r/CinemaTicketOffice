using System.Collections.Generic;
using DataAccess.Model;

namespace KioskClient.Domain
{
    public class AuditoriumSeat
    {
        public int SeatNumber { get; set; }
        public AuditoriumRow Row { get; set; }
        public bool IsSelected { get; set; }
        public bool IsFree { get; set; }
    }

    public class AuditoriumRow
    {
        public int Number { get; set; }
        public List<AuditoriumSeat> Seats { get; set; }
    }

    public class AuditoriumView
    {
        public AuditoriumView()
        {
        }

        public AuditoriumView(Auditorium auditorium)
        {
            Rows = new List<AuditoriumRow>();

            for (var i = 0; i < auditorium.Rows; i++)
            {
                var row = new AuditoriumRow
                {
                    Seats = new List<AuditoriumSeat>(),
                    Number = i + 1
                };

                for (var j = 0; j < auditorium.Seats; j++)
                {
                    row.Seats.Add(new AuditoriumSeat {Row = row, SeatNumber = j + 1, IsFree = true});
                }

                Rows.Add(row);
            }
        }

        public List<AuditoriumRow> Rows { get; set; }
    }
}