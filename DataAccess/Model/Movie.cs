using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

namespace DataAccess.Model
{
    public class Movie : INotifyPropertyChanged
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

        public string AgeLimitString
        {
            get { return AgeLimit + "+"; }
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Plot { get; set; }
        public int Duration { get; set; }
        public int AgeLimit { get; set; }
        public BitmapImage Poster { get; set; }
        public List<DateTime> Showtimes { get; set; }
        public List<Genre> Genres { get; set; }
        public DateTime ReleaseDate { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyChanged()
        {
            OnPropertyChanged();
        }

        [Annotations.NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}