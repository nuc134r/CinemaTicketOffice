using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataAccess.Annotations;
using KioskClient.Properties;

namespace KioskClient.Domain
{
    public class AuditoriumSeat : INotifyPropertyChanged
    {
        private bool isSelected;
        public int SeatNumber { get; set; }
        public AuditoriumRow Row { get; set; }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged();
            }
        }

        public bool IsFree { get; set; }

        public string SeatString
        {
            get { return string.Format(Resources.SeatText, Row.Number, SeatNumber); }
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