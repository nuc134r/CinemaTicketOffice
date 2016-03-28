using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Administration.View;
using DataAccess.Model;
using DataAccess.Repository;

namespace Administration.ViewModel
{
    public class AuditoriumListPageViewModel
    {
        private readonly AuditoriumListPage view;
        private readonly ShowtimeRepository repository;

        public AuditoriumListPageViewModel(AuditoriumListPage view, ShowtimeRepository repository)
        {
            this.view = view;
            this.repository = repository;

            RetriveData();
        }

        public ObservableCollection<Auditorium> Auditoriums { get; set; }

        private void RetriveData()
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

        }

        public void DeleteGenre(Auditorium auditorium)
        {

        }
    }
}