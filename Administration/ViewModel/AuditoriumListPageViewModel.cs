using System;
using System.Collections.Generic;
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
    public class AuditoriumListPageViewModel
    {
        private readonly ShowtimeRepository repository;
        private readonly AuditoriumListPage view;

        public AuditoriumListPageViewModel(AuditoriumListPage view, ShowtimeRepository repository)
        {
            this.view = view;
            this.repository = repository;

            RetrieveData();
        }

        public ObservableCollection<Auditorium> Auditoriums { get; set; }

        private void RetrieveData()
        {
            if (Auditoriums == null)
            {
                Auditoriums = new ObservableCollection<Auditorium>();
                Auditoriums.CollectionChanged += AuditoriumsOnCollectionChanged;
            }

            var auditoriums = repository.GetAuditoriums().ToList();

            Auditoriums.Clear();
            auditoriums.ForEach(genre => Auditoriums.Add(genre));
        }

        private void AuditoriumsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            view.ListCount = Auditoriums.Count;
        }

        public void OpenGenreEditor(Auditorium auditorium)
        {
            try
            {
                if (auditorium != null)
                {
                    auditorium = auditorium.Clone();
                }

                var editor = new AuditoriumEditorWindow(auditorium);
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

        public void DeleteGenre(Auditorium auditorium)
        {
            var result = MessageBox.Show(
                string.Format(Resources.DeleteAuditoriumConfirmationText, auditorium.Name),
                Resources.DeleteConfirmationCaption,
                MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    repository.Delete(auditorium);
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