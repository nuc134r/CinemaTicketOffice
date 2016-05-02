using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DataAccess;
using DataAccess.Model;
using DataAccess.Repository;
using KioskClient.Domain;
using KioskClient.Properties;
using KioskClient.View;

namespace KioskClient.ViewModel
{

    // Yeah. Dear diary. TOday my life was on on hair(on last opportynity). My friend was knocking on a random
    // guy's window. A guy has ran at us. He said that his child woke up and he was gonna kill us. He has put his
    // knife to Vova's neck. And and to Bruns's neck. Bruns said 'Come on, do it, cut me off.' but the guy said 
    // 'Go with peace guys. Go with peace.'. That was really strange.
    // Looked like he was drunk but it felt like he wasn't. Unless we was. That's strange. Im drunk.

    public class AuditoriumMapPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly TicketRepository repository;
        private readonly List<AuditoriumSeat> seats;
        private readonly Showtime showtime;

        public AuditoriumMapPageViewModel(AuditoriumMapPage view, TicketRepository repository, Showtime showtime)
        {
            this.view = view;
            this.repository = repository;
            this.showtime = showtime;
            seats = new List<AuditoriumSeat>();

            var occupiedSeats = repository.GetOccupiedSeats(showtime.Id).ToList();

            Auditorium = new AuditoriumView(showtime.Auditorium, occupiedSeats);
            Auditorium.PropertyChanged += SelectionChanged;

            Total = "0";
        }

        public AuditoriumView Auditorium { get; set; }

        public string Total { get; set; }
        public string SeatsString { get; set; }
        public bool CanCheckout { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void SelectionChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            var changedSeat = (AuditoriumSeat) sender;

            if (changedSeat.IsSelected)
            {
                seats.Add(changedSeat);
            }
            else
            {
                seats.Remove(changedSeat);
            }

            SeatsString = BuildSelectedSeatsString();
            Total = (showtime.Price * seats.Count).ToString();

            CanCheckout = seats.Count != 0;

            OnPropertyChanged("Total");
            OnPropertyChanged("SeatsString");
            OnPropertyChanged("CanCheckout");
        }

        private string BuildSelectedSeatsString()
        {
            if (seats.Count == 0) return string.Empty;
            if (seats.Count > 3)
            {
                var caseService = new NumericCaseService(
                    Resources.SeatsTextSingle,
                    Resources.SeatsTextCouple,
                    Resources.SeatsTextMany);

                return caseService.GetCaseString(seats.Count);
            }

            var seatsString = "";
            seats.ForEach(seat => { seatsString += seat.SeatString + Environment.NewLine; });
            return seatsString;
        }

        public void GoBack()
        {
            Window.NavigateBack();
        }

        [DataAccess.Annotations.NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void GoToCheckoutPage()
        {
            var selectedSeats = seats.Where(seat => seat.IsSelected);
            Window.NavigateToCheckoutPage(showtime, selectedSeats);
        }
    }
}