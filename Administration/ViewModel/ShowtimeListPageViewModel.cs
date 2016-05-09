using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using Administration.Editors;
using Administration.Properties;
using Administration.View;
using DataAccess.Model;
using DataAccess.Repository;

namespace Administration.ViewModel
{
    public class ShowtimeListPageViewModel
    {
        private readonly ShowtimeListPage view;
        private readonly ShowtimeRepository repository;

        public ObservableCollection<Showtime> Showtimes { get; private set; }

        public ShowtimeListPageViewModel(ShowtimeListPage view, ShowtimeRepository repository)
        {
            this.view = view;
            this.repository = repository;

            RetrieveData();
        }

        private void RetrieveData()
        {
            if (Showtimes == null)
            {
                Showtimes = new ObservableCollection<Showtime>();
                Showtimes.CollectionChanged += ShowtimesOnCollectionChanged;
            }

            var showtimes = repository.GetShowtimes().ToList();

            Showtimes.Clear();
            showtimes.ForEach(showtime => Showtimes.Add(showtime));
        }

        private void ShowtimesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            view.ListCount = Showtimes.Count;
        }

        public void OpenEditor(Showtime showtime)
        {
            try
            {
                if (showtime != null)
                {
                    showtime = showtime.Clone();
                }

                var editor = new ShowtimeEditorWindow(showtime);
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

        public void Delete(Showtime showtime)
        {
            var result = MessageBox.Show(
                string.Format(Resources.DeleteShowtimeConfirmatonText, showtime.Time.ToLongDateString(), showtime.Movie.Title),
                Resources.DeleteConfirmationCaption,
                MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    repository.Delete(showtime);
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