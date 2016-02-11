using System.Collections.Generic;
using KioskClient.Model;

namespace KioskClient.DataAccessLayer
{
    public class CatalogPageDataAccessLayer
    {
        public List<Genre> GetMovieGenres()
        {
            return new List<Genre>
            {
                new Genre(0, "Экшен"),
                new Genre(1, "Фантастика"),
                new Genre(2, "Драма"),
                new Genre(3, "Комедия")
            };
        }
    }
}