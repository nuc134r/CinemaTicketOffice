using System.Windows;
using System.Windows.Input;
using Administration.Properties;
using Administration.ViewModel;
using DataAccess;
using DataAccess.Model;
using DataAccess.Repository;

namespace Administration.View
{
    public partial class LogEntryListPage
    {
        private readonly LogEntryListPageViewModel viewModel;
        private readonly MainWindow window;

        public LogEntryListPage(MainWindow window)
        {
            this.window = window;
            InitializeComponent();

            var connectionString = ConnectionStringBuilder.Build(
                Settings.Default.server,
                Settings.Default.database,
                Settings.Default.user,
                Settings.Default.password);

            var repository = new LogsRepository(connectionString);

            viewModel = new LogEntryListPageViewModel(this, repository);
            DataContext = viewModel.LogEntryList;
        }

        private LogEntry SelectedLogEntry
        {
            get { return (LogEntry) listView.SelectedItem; }
        }
        
        public int ListCount
        {
            set { window.StatusBarCount.Content = value; }
        }

        private void ListView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedLogEntry != null)
                viewModel.OpenEditor(SelectedLogEntry);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedLogEntry != null)
                viewModel.OpenEditor(SelectedLogEntry);
        }
    }
}