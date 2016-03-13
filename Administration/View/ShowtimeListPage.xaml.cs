using System.Windows.Controls;
using Administration.Properties;
using Administration.ViewModel;
using DataAccess;
using DataAccess.Repository;

namespace Administration.View
{
    public partial class ShowtimeListPage
    {
        private readonly MainWindow window;
        private readonly ShowtimeListPageViewModel viewModel;

        public ShowtimeListPage(MainWindow window)
        {
            this.window = window;
            InitializeComponent();

            var connectionString = ConnectionStringBuilder.Build(
                Settings.Default.server,
                Settings.Default.database,
                Settings.Default.user,
                Settings.Default.password);

            var repository = new ShowtimeRepository(connectionString);

            viewModel = new ShowtimeListPageViewModel(this, repository);
            DataContext = viewModel;
        }

        public int ListCount
        {
            set { window.StatusBarCount.Content = value; }
        }
    }
}