using System.Windows;
using DataAccess;
using DataAccess.Model;
using DataAccess.Repository;
using KioskClient.Properties;
using KioskClient.ViewModel;

namespace KioskClient.View
{
    public partial class AuditoriumMapPage
    {
        private readonly AuditoriumMapPageViewModel viewModel;

        public AuditoriumMapPage(Showtime showtime)
        {
            InitializeComponent();

            var connectionString = ConnectionStringBuilder.Build(
                Settings.Default.server,
                Settings.Default.database,
                Settings.Default.user,
                Settings.Default.password);

            var repository = new TicketRepository(connectionString);

            viewModel = new AuditoriumMapPageViewModel(this, repository, showtime);
            DataContext = viewModel;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.GoBack();
        }

        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.GoToCheckoutPage();
        }
    }
}