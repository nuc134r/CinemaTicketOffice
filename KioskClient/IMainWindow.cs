using KioskClient.Model;

namespace KioskClient
{
    public interface IMainWindow
    {
        void NavigateToMovieDetails(Movie movie);
        void NavigateToMovieCatalog();
        void NavigateToShowtimeList(Movie movie);
    }
}