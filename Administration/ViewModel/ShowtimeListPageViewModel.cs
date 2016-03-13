using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
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
    }
}