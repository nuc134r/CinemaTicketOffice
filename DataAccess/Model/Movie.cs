﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;

namespace DataAccess.Model
{
    public class Movie
    {
        public string GenresString
        {
            get
            {
                if (Genres != null)
                    return string.Join(", ", Genres.Select(_ => _.Name));
                return "";
            }
        }

        public string ShowtimesString
        {
            get
            {
                if (Showtimes == null || Showtimes.Count == 0) return "Нет сеансов";
                return string.Join(", ", Showtimes.Select(_ => _.ToShortTimeString()));
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