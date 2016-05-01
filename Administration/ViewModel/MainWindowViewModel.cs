using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Administration.View;
using DataAccess.Annotations;
using DataAccess.Model;

namespace Administration.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Page frame;
        public Visibility AdminVisibility { get; private set; }
        public Visibility SuperadminVisibility { get; private set; }

        public Page Frame
        {
            get
            {
                return frame;
            }
            set
            {
                frame = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel(UserType currentRole)
        {
            AdminVisibility      = currentRole == UserType.Admin      ? Visibility.Visible : Visibility.Collapsed;
            SuperadminVisibility = currentRole == UserType.Superadmin ? Visibility.Visible : Visibility.Collapsed;
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