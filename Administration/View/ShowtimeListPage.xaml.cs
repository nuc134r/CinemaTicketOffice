using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Administration.Properties;
using Administration.ViewModel;
using DataAccess;
using DataAccess.Model;
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

        private Showtime SelectedShowtime
        {
            get { return (Showtime)listView.SelectedItem; }
        }

        public int ListCount
        {
            set { window.StatusBarCount.Content = value; }
        }

        private void ListView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            viewModel.OpenEditor(SelectedShowtime);
        }

        private void CreateButton_OnClick(object sender, RoutedEventArgs e)
        {
            viewModel.OpenEditor(null);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.OpenEditor(SelectedShowtime);
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            viewModel.Delete(SelectedShowtime);
        }
    }
}