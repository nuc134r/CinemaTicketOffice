﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;

namespace DataAccess.Model
{
    public class Movie
    {
        public Movie(int id, string title, string plot, int duration, BitmapImage poster, List<DateTime> showtimes,
            List<Genre> genres, DateTime releaseDate, int ageLimit)
        {
            Id = id;
            Title = title;
            Plot = plot;
            Duration = duration;
            Poster = poster;
            Showtimes = showtimes;
            Genres = genres;
            ReleaseDate = releaseDate;
            AgeLimit = ageLimit;
        }

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

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Plot { get; private set; }
        public int Duration { get; private set; }
        public int AgeLimit { get; private set; }
        public BitmapImage Poster { get; private set; }
        public List<DateTime> Showtimes { get; private set; }
        public List<Genre> Genres { get; private set; }
        public DateTime ReleaseDate { get; private set; }
    }
}