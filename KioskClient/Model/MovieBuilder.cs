﻿using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace KioskClient.Model
{
    public class MovieBuilder
    {
        private int duration;

        private List<Genre> genres;
        private int id;
        private string plot;
        private BitmapImage poster;
        private DateTime releaseDate;
        private List<int> showtimes;
        private string title;

        public static MovieBuilder Create()
        {
            return new MovieBuilder();
        }

        public MovieBuilder WithId(int id)
        {
            this.id = id;
            return this;
        }

        public MovieBuilder WithTitle(string title)
        {
            this.title = title;
            return this;
        }

        public MovieBuilder WithPlot(string plot)
        {
            this.plot = plot;
            return this;
        }

        public MovieBuilder WithDuration(int duration)
        {
            this.duration = duration;
            return this;
        }

        public MovieBuilder WithPoster(BitmapImage poster)
        {
            this.poster = poster;
            return this;
        }

        public MovieBuilder WithShowtimes(List<int> showtimes)
        {
            this.showtimes = showtimes;
            return this;
        }

        public MovieBuilder WithGenres(List<Genre> genres)
        {
            this.genres = genres;
            return this;
        }

        public MovieBuilder WithReleaseDate(DateTime releaseDate)
        {
            this.releaseDate = releaseDate;
            return this;
        }

        public Movie Please()
        {
            return new Movie(id, title, plot, duration, poster, showtimes, genres, releaseDate);
        }
    }
}