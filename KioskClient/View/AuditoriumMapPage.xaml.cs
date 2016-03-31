using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using DataAccess.Model;
using KioskClient.Domain;

namespace KioskClient.View
{
    public partial class AuditoriumMapPage
    {
        private readonly AuditoriumMapPageViewModel viewModel;

        public AuditoriumMapPage(Showtime showtime)
        {
            InitializeComponent();

            var auditoriumView = new AuditoriumView(showtime.Auditorium);
            auditoriumView.PropertyChanged += AuditoriumViewOnPropertyChanged;
            DataContext = auditoriumView;
        }

        private void AuditoriumViewOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            var seat = (AuditoriumSeat)sender;
            MessageBox.Show(seat.Row.Number + " " + seat.SeatNumber);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Window.GetWindow(this)).NavigateBack();
        }
    }

    public class AuditoriumMapPageViewModel
    {

    }
}