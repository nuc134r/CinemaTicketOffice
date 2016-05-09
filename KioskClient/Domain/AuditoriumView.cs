using System.Collections.Generic;
using System.ComponentModel;
using DataAccess.Model;

namespace KioskClient.Domain
{
    public class AuditoriumView : INotifyPropertyChanged
    {
        public AuditoriumView(Auditorium auditorium, List<Seat> occupiedSeats)
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
                    var seat = new AuditoriumSeat
                    {
                        Row = row,
                        SeatNumber = j + 1,
                        IsFree = true
                    };
                    seat.PropertyChanged += SeatOnPropertyChanged;

                    row.Seats.Add(seat);
                }

                Rows.Add(row);
            }

            foreach (var seat in occupiedSeats)
            {
                Rows[seat.RowNumber - 1].Seats[seat.SeatNumber - 1].IsFree = false;
            }
        }

        public List<AuditoriumRow> Rows { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private void SeatOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(sender, new PropertyChangedEventArgs(propertyChangedEventArgs.PropertyName));
        }
    }
}