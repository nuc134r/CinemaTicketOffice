using System.Collections.Generic;
using DataAccess.Model;

namespace Administration.ViewModel
{
    public class MovieEditorWindowViewModel
    {
        public Movie Movie { get; set; }
        public List<Genre> Genres { get; set; }
        //public List<AgeLimit> AgeLimits { get; set; }


        public MovieEditorWindowViewModel(Movie movie)
        {
            Movie = movie;
        }
    }
}