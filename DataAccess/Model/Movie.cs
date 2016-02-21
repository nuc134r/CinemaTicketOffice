using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;

namespace DataAccess.Model
{
    public class Movie
    {
        public string GenresString
        {
            get { return string.Join(", ", Genres.Select(_ => _.Name)); }
        }

        public string ShowtimesString
        {
            get
            {
                if (Showtimes.Count == 0) return "Нет сеансов";
                return string.Join(", ", Showtimes.Select(_ => _.ToShortTimeString()));
            }
        }

        public string AgeLimitString
        {
            get { return AgeLimit + "+"; }
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Plot { get; set; }
        public int Duration { get; set; }
        public int AgeLimit { get; set; }
        public BitmapImage Poster { get; set; }
        public List<DateTime> Showtimes { get; set; }
        public List<Genre> Genres { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}