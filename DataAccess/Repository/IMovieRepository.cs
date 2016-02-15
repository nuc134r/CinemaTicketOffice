using System.Collections.Generic;
using DataAccess.Model;

namespace DataAccess.Repository
{
    public interface IMovieRepository
    {
        List<Movie> Movies { get; }
        List<Genre> Genres { get; }
        void RefreshData();
    }
}