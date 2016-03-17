using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using Administration.Properties;
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

            RetrieveData();
        }

        public ObservableCollection<Genre> Genres { get; private set; }

        private void RetrieveData()
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
            try
            {
                if (genre != null)
                {
                    genre = genre.Clone();
                }

                var editor = new GenreEditorWindow(genre);
                var result = editor.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    RetrieveData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteGenre(Genre genre)
        {
            var result = MessageBox.Show(
                string.Format(Resources.DeleteGenreConfirmationText, genre.Name),
                Resources.DeleteConfirmationCaption,
                MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    repository.Delete(genre);
                    RetrieveData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}