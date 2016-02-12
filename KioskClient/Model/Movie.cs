using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;

namespace KioskClient.Model
{
    public class Movie
    {
        public Movie(int id, string title, string plot, int duration, BitmapImage poster, List<int> showtimes,
            List<Genre> genres, DateTime releaseDate)
        {
            Id = id;
            Title = title;
            Plot = plot;
            Duration = duration;
            Poster = poster;
            Showtimes = showtimes;
            Genres = genres;
            ReleaseDate = releaseDate;
        }

        public string GenresString
        {
            get { return string.Join(", ", Genres.Select(_ => _.Name)); }
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Plot { get; private set; }
        public int Duration { get; private set; }
        public BitmapImage Poster { get; private set; }
        public List<int> Showtimes { get; private set; }
        public List<Genre> Genres { get; private set; }
        public DateTime ReleaseDate { get; private set; }
    }
}