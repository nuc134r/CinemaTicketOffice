using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DataAccess.Model;
using KioskClient.Domain;
using KioskClient.ViewModel;

namespace KioskClient.View
{
    public partial class CheckoutPage
    {
        private readonly CheckoutPageViewModel viewModel;
        
        public CheckoutPage(Showtime showtime, IEnumerable<AuditoriumSeat> seats)
        {
            InitializeComponent();

            var total = showtime.Price * seats.Count();

            viewModel = new CheckoutPageViewModel(this, total);
            DataContext = viewModel;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Cancel();
        }

        private void EmulateButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}