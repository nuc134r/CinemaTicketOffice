using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataAccess.Annotations;
using DataAccess.Model;
using KioskClient.Domain;
using KioskClient.Properties;
using KioskClient.View;

namespace KioskClient.ViewModel
{
    public class AuditoriumMapPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly List<AuditoriumSeat> seats;
        private readonly Showtime showtime;

        public AuditoriumMapPageViewModel(AuditoriumMapPage view, Showtime showtime)
        {
            this.view = view;
            this.showtime = showtime;
            seats = new List<AuditoriumSeat>();

            Auditorium = new AuditoriumView(showtime.Auditorium);
            Auditorium.PropertyChanged += SelectionChanged;

            Total = "0";
        }

        public AuditoriumView Auditorium { get; set; }

        public string Total { get; set; }
        public string SeatsString { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void SelectionChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            var changedSeat = (AuditoriumSeat)sender;

            if (changedSeat.IsSelected)
            {
                seats.Add(changedSeat);
            }
            else
            {
                seats.Remove(changedSeat);
            }

            BuildSelectedSeatsString();

            OnPropertyChanged("Total");
            OnPropertyChanged("SeatsString");
        }

        private void BuildSelectedSeatsString()
        {
            SeatsString = "";
            if (seats.Count != 0)
            {
                if (seats.Count > 3)
                {
                    SeatsString = string.Format(Resources.SelectedSeatsText, seats.Count);
                }
                else
                {
                    seats.ForEach(
                        seat =>
                        {
                            SeatsString += string.Format(Resources.SeletedSeatLineText + "\n", seat.Row.Number,
                                seat.SeatNumber);
                        });
                }
            }

            Total = (showtime.Price * seats.Count).ToString();
        }

        public void GoBack()
        {
            Window.NavigateBack();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}