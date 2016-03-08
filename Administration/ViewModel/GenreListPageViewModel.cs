using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Administration.View;
using DataAccess.Model;
using DataAccess.Repository;

namespace Administration.ViewModel
{
    public class GenreListPageViewModel
    {
        private readonly MovieRepository repository;
        private readonly GenreListPage view;

        public GenreListPageViewModel(GenreListPage view, MovieRepository repository)
        {
            this.view = view;
            this.repository = repository;

            RetrieveGenres();
        }

        public ObservableCollection<Genre> Genres { get; private set; }

        private void RetrieveGenres()
        {
            if (Genres == null)
            {
                Genres = new ObservableCollection<Genre>();
                Genres.CollectionChanged += GenresOnCollectionChanged;
            }

            var genres = repository.GetGenres().ToList();

            Genres.Clear();
            genres.ForEach(genre => Genres.Add(genre));
        }

        private void GenresOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            view.ListCount = Genres.Count;
        }

        public void OpenGenreEditor(Genre genre)
        {
            if (genre != null)
            {
                genre = genre.Clone();
            }

            var editor = new GenreEditorWindow(genre);
            var result = editor.ShowDialog();

            if (result.HasValue && result.Value)
            {
                RetrieveGenres();
            }
        }
    }
}