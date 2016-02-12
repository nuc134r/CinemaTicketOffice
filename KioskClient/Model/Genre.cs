using System.ComponentModel;
using System.Runtime.CompilerServices;
using KioskClient.Annotations;

namespace KioskClient.Model
{
    public class Genre : INotifyPropertyChanged
    {
        private bool isSelected;

        public Genre(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }

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
            else
                return base.Equals(obj);
        }

        protected bool Equals(Genre other)
        {
            return isSelected == other.isSelected && Id == other.Id && string.Equals(Name, other.Name);
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
    }
}