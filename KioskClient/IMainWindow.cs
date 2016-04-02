using System.Collections.Generic;
using DataAccess.Model;
using KioskClient.Domain;

namespace KioskClient
{
    public interface IMainWindow
    {
        void NavigateToMovieDetails(Movie movie);
        void NavigateToMovieCatalog();
        void NavigateToShowtimeList(Movie movie);
        void NavigateToAuditoriumMap(Showtime showtime);
        void NavigateBack();
        void NavigateToCheckoutPage(Showtime showtime, IEnumerable<AuditoriumSeat> seats);
        void NavigateToThanksPage();
    }
}