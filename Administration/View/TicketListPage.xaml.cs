using System.Windows.Controls;
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
        private readonly MainWindow window;
        private readonly TicketListPageViewModel viewModel;

        private Ticket SelectedTicket
        {
            get { return (Ticket)listView.SelectedItem; }
        }

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

        public int ListCount
        {
            set { window.StatusBarCount.Content = value; }
        }

        private void ListView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            viewModel.OpenEditor(SelectedTicket);
        }

        private void CreateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            viewModel.OpenEditor(null);
        }

        private void EditButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            viewModel.OpenEditor(SelectedTicket);
        }

        private void DeleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (SelectedTicket == null) return;
            viewModel.Delete(SelectedTicket);
        }
    }
}