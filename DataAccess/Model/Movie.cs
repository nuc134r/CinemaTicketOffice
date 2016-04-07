using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;

namespace DataAccess.Model
{
    public class Movie
    {
        public Movie()
        {
            ReleaseDate = DateTime.Now.Date;
        }

        public string GenresString
        {
            get
            {
                if (Genres != null)
                {
                    return string.Join(", ", Genres.Select(_ => _.Name));
                }
                return string.Empty;
            }
        }

        public string SorryString
        {
            get
            {
                var showtimes = Showtimes
                    .Where(time => time > DateTime.Now)
                    .Select(time => time.ToShortTimeString())
                    .ToList();

                if (!showtimes.Any())
                {
                    return Resources.NoShowtimesTodayText;
                }

                return string.Empty;
            }
        }

        public string ShowtimesString
        {
            get
            {
                if (!Showtimes.Any())
                {
                    return string.Empty;
                }

                if (Showtimes.All(_ => _.Date > DateTime.Now.Date))
                {
                    return Resources.TomorrowText;
                }

                var showtimes = Showtimes
                    .Where(time => time.Date == DateTime.Today
                                   &&
                                   time > DateTime.Now)
                    .Select(time => time.ToShortTimeString())
                    .ToList();

                return string.Join(" ", showtimes);
            }
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Plot { get; set; }
        public int Duration { get; set; }
        public AgeLimit AgeLimit { get; set; }
        public BitmapImage Poster { get; set; }
        public List<DateTime> Showtimes { get; set; }
        public List<Genre> Genres { get; set; }
        public DateTime ReleaseDate { get; set; }

        public Movie Clone()
        {
            return new Movie
            {
                AgeLimit = AgeLimit,
                Duration = Duration,
                Genres = Genres,
                Id = Id,
                Showtimes = Showtimes,
                Title = Title,
                Poster = Poster,
                Plot = Plot,
                ReleaseDate = ReleaseDate
            };
        }
    }
}