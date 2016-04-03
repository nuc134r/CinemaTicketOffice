using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using DataAccess.Model;
using DataAccess.Repository;
using KioskClient.Domain;
using KioskClient.View;

namespace KioskClient.ViewModel
{
    public class CheckoutPageViewModel : ViewModelBase
    {
        private readonly TicketRepository repository;
        private readonly List<AuditoriumSeat> seats;
        private readonly Showtime showtime;

        public CheckoutPageViewModel(CheckoutPage view, TicketRepository repository, Showtime showtime,
            List<AuditoriumSeat> seats)
        {
            this.repository = repository;
            this.showtime = showtime;
            this.seats = seats;
            this.view = view;

            Total = showtime.Price*seats.Count;

            ArrowAnimatedBrush = new SolidColorBrush();
        }

        public SolidColorBrush ArrowAnimatedBrush { get; set; }

        public int Total { get; set; }

        public void Cancel()
        {
            Window.NavigateBack();
        }

        public void GoToThanksPage()
        {
            Window.NavigateToThanksPage();
        }

        public void SaveTickets()
        {
            var seatList = seats.Select(
                seat => new Seat
                {
                    RowNumber = seat.Row.Number,
                    SeatNumber = seat.SeatNumber
                });

            foreach (var seat in seatList)
            {
                repository.RegisterTicket(showtime.Id, seat);
            }
        }
    }
}