using System.Collections.Generic;
using DataAccess.Model;

namespace DataAccess.Repository
{
    public interface IShowtimeRepository
    {
        List<Showtime> Showtimes { get; }
        void Refresh();
    }
}