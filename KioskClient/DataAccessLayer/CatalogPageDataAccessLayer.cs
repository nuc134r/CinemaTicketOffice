using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using KioskClient.Model;

namespace KioskClient.DataAccessLayer
{
    public class CatalogPageDataAccessLayer
    {
        public List<Genre> GetMovieGenres()
        {
            return new List<Genre>
            {
                GenreBuilder.Create()
                    .WithName("боевик")         // 0
                    .Please(),
                GenreBuilder.Create() 
                    .WithName("фантастика")     // 1
                    .Please(),
                GenreBuilder.Create()
                    .WithName("драма")          // 2
                    .Please(),
                GenreBuilder.Create()
                    .WithName("мелодрама")      // 3
                    .Please(),
                GenreBuilder.Create()
                    .WithName("комедия")        // 4
                    .Please(),
                GenreBuilder.Create()
                    .WithName("документальный") // 5
                    .Please(),
                GenreBuilder.Create()
                    .WithName("мультфильм")     // 6
                    .Please(),
                GenreBuilder.Create()
                    .WithName("приключения")    // 7
                    .Please(),
                GenreBuilder.Create()
                    .WithName("семейный")       // 8
                    .Please(),
                GenreBuilder.Create()
                    .WithName("фэнтези")        // 9
                    .Please()
            };
        }

        public List<Movie> GetMovies()
        {
            return new List<Movie>
            {
                MovieBuilder.Create()
                    .WithTitle("Вечное сияние чистого разума")
                    .WithPoster(new BitmapImage(new Uri(@"C:\CinemaTicketOffice\Posters\5492.jpg")))
                    .WithGenres(new List<Genre> {GetMovieGenres()[3], GetMovieGenres()[1], GetMovieGenres()[2] })
                    .Please(),
                MovieBuilder.Create()
                    .WithTitle("Форрест Гамп")
                    .WithPoster(new BitmapImage(new Uri(@"C:\CinemaTicketOffice\Posters\448.jpg")))
                    .WithGenres(new List<Genre> { GetMovieGenres()[2], GetMovieGenres()[3] })
                    .Please(),
                MovieBuilder.Create()
                    .WithTitle("Кунг-фу Панда")
                    .WithPoster(new BitmapImage(new Uri(@"C:\CinemaTicketOffice\Posters\103734.jpg")))
                    .WithGenres(new List<Genre> { GetMovieGenres()[6], GetMovieGenres()[0], GetMovieGenres()[4], GetMovieGenres()[8] })
                    .Please(),
                MovieBuilder.Create()
                    .WithTitle("Век Адалин")
                    .WithPoster(new BitmapImage(new Uri(@"C:\CinemaTicketOffice\Posters\522876.jpg")))
                    .WithGenres(new List<Genre> { GetMovieGenres()[2], GetMovieGenres()[3], GetMovieGenres()[9] })
                    .Please()
            };
        }
    }
}