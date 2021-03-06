﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DataAccess.Model;
using DataAccess.Repository;
using KioskClient.View;

namespace KioskClient.ViewModel
{
    public class CatalogPageViewModel : ViewModelBase
    {
        private readonly IMovieRepository repository;
        private List<Movie> allMovies;
        private bool pauseFiltering;

        public CatalogPageViewModel(CatalogPage view, IMovieRepository repository)
        {
            this.repository = repository;
            this.view = view;

            Genres = new ObservableCollection<Genre>();
            Movies = new ObservableCollection<Movie>();
            allMovies = new List<Movie>();

            RetrieveData();
        }

        private new CatalogPage view
        {
            get { return (CatalogPage) base.view; }
            set { base.view = value; }
        }

        public ObservableCollection<Genre> Genres { get; private set; }
        public ObservableCollection<Movie> Movies { get; private set; }

        private void RetrieveData()
        {
            var genres = repository.GetGenres();
            Genres = new ObservableCollection<Genre>(genres);

            foreach (var genre in Genres)
            {
                genre.PropertyChanged += (sender, args) => FilterByGenres();
            }

            allMovies = repository.GetMovies().ToList();
            Movies = new ObservableCollection<Movie>(allMovies);

            allMovies.ForEach(repository.GetMovieDetails);
        }

        public void ResetGenresFilter()
        {
            // Обработчик подключен к каждому свойству IsSelected в Genres
            // и он будет перефильтровывать коллекцию Movies при его изменении.
            // Поэтому во время сброса флагов отключаем фильтрацию в пользу производительности.
            pauseFiltering = true;

            foreach (var genre in Genres)
            {
                genre.IsSelected = false;
            }

            pauseFiltering = false;
            FilterByGenres();
        }

        private void FilterByGenres()
        {
            if (pauseFiltering) return;

            var selectedGenres = Genres.Where(genre => genre.IsSelected).Select(_ => _.Id).ToArray();

            if (view != null) view.UnwireHandlers();
            Movies.Clear();

            if (!selectedGenres.Any())
            {
                allMovies.ForEach(movie => Movies.Add(movie));
            }
            else
            {
                foreach (var movie in allMovies)
                {
                    if (movie.Genres.Any(_ => selectedGenres.Contains(_.Id)))
                    {
                        Movies.Add(movie);
                    }
                }
            }

            if (Movies.Count == 0)
            {
                view.ShowNoResultsText();
            }
            else
            {
                view.HideNoResultsText();
            }
            
            if (view != null) view.WireHandlers();
        }

        public void GoToMovieDetails(Movie movie)
        {
            Window.NavigateToMovieDetails(movie);
        }

        public void Refresh()
        {
            RetrieveData();
        }
    }
}