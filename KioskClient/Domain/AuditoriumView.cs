using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataAccess.Annotations;
using DataAccess.Model;

namespace KioskClient.Domain
{
    public class AuditoriumSeat : INotifyPropertyChanged
    {
        private bool isSelected;
        public int SeatNumber { get; set; }
        public AuditoriumRow Row { get; set; }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged();
            }
        }

        public bool IsFree { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class AuditoriumRow
    {
        public int Number { get; set; }
        public List<AuditoriumSeat> Seats { get; set; }
    }

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

        private void SeatOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(sender, new PropertyChangedEventArgs(propertyChangedEventArgs.PropertyName));
        }

        public List<AuditoriumRow> Rows { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}