using System.Windows;
using System.Windows.Input;
using Administration.Properties;
using Administration.ViewModel;
using DataAccess;
using DataAccess.Model;
using DataAccess.Repository;

namespace Administration.View
{
    public partial class AuditoriumListPage
    {
        private readonly AuditoriumListPageViewModel viewModel;
        private readonly MainWindow window;

        public AuditoriumListPage(MainWindow window)
        {
            this.window = window;
            InitializeComponent();

            var connectionString = ConnectionStringBuilder.Build(
                Settings.Default.server,
                Settings.Default.database,
                Settings.Default.user,
                Settings.Default.password);

            var repository = new ShowtimeRepository(connectionString);

            viewModel = new AuditoriumListPageViewModel(this, repository);
            DataContext = viewModel.Auditoriums;
        }

        private Auditorium SelectedAuditorium
        {
            get { return (Auditorium) listView.SelectedItem; }
        }

        public int ListCount
        {
            set { window.StatusBarCount.Content = value; }
        }

        private void ListView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            viewModel.OpenGenreEditor(SelectedAuditorium);
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.OpenGenreEditor(null);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.OpenGenreEditor(SelectedAuditorium);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAuditorium == null) return;
            viewModel.DeleteGenre(SelectedAuditorium);
        }
    }
}