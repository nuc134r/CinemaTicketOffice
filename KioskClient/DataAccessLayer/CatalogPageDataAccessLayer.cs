using System.Collections.Generic;
using KioskClient.Model;

namespace KioskClient.DataAccessLayer
{
    public class CatalogPageDataAccessLayer
    {
        public List<Genre> GetMovieGenres()
        {
            var builder = new GenreBuilder();

            return new List<Genre>
            {
                builder.WithName("Боевик").Please(),
                builder.WithName("Фантастика").Please(),
                builder.WithName("Драма").Please(),
                builder.WithName("Мелодрама").Please(),
                builder.WithName("Комедия").Please(),
                builder.WithName("Документальный").Please()
            };
        }

        public List<Movie> GetMovies()
        {
            var builder = new MovieBuilder();

            return new List<Movie>
            {
                builder.WithTitle("Вечное Сияние Чистого Разума").Please(),
                builder.WithTitle("Форрест Гамп").Please()
            };
        }
    }
}