using System.Windows;
using System.Windows.Input;
using Administration.Properties;
using Administration.ViewModel;
using DataAccess;
using DataAccess.Model;
using DataAccess.Repository;

namespace Administration.View
{
    public partial class TicketListPage
    {
        private readonly TicketListPageViewModel viewModel;
        private readonly MainWindow window;

        public TicketListPage(MainWindow window)
        {
            this.window = window;
            InitializeComponent();

            var connectionString = ConnectionStringBuilder.Build(
                Settings.Default.server,
                Settings.Default.database,
                Settings.Default.user,
                Settings.Default.password);

            var repository = new TicketRepository(connectionString);

            viewModel = new TicketListPageViewModel(this, repository);
            DataContext = viewModel.Tickets;
        }

        private Ticket SelectedTicket
        {
            get { return (Ticket) listView.SelectedItem; }
        }

        public int ListCount
        {
            set { window.StatusBarCount.Content = value; }
        }

        private void ListView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedTicket != null)
                viewModel.OpenEditor(SelectedTicket);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTicket != null)
                viewModel.OpenEditor(SelectedTicket);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTicket != null)
                viewModel.Delete(SelectedTicket);
        }
    }
}