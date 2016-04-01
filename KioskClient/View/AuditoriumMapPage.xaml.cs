using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using DataAccess.Model;
using KioskClient.Domain;
using KioskClient.ViewModel;

namespace KioskClient.View
{
    public partial class AuditoriumMapPage
    {
        private readonly AuditoriumMapPageViewModel viewModel;

        public AuditoriumMapPage(Showtime showtime)
        {
            InitializeComponent();

            viewModel = new AuditoriumMapPageViewModel(this, showtime);
            DataContext = viewModel;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.GoBack();
        }

        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}