using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataAccess.Model
{
    public class Genre : INotifyPropertyChanged
    {
        private bool isSelected;

        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged();
            }
        }

        public override bool Equals(object obj)
        {
            var genre = obj as Genre;
            if (genre != null)
                return Name == genre.Name;
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = isSelected.GetHashCode();
                hashCode = (hashCode*397) ^ Id;
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                return hashCode;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public Genre Clone()
        {
            return new Genre
            {
                Id = Id,
                Name = Name
            };
        }
    }
}