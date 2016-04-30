using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Administration.Properties;
using Administration.ViewModel;
using DataAccess;
using DataAccess.Model;
using DataAccess.Repository;
using UserListPageViewModel = Administration.ViewModel.UserListPageViewModel;

namespace Administration.View
{
    public partial class UserListPage
    {
        private readonly MainWindow window;
        private readonly UserListPageViewModel viewModel;

        public UserListPage(MainWindow window)
        {
            this.window = window;
            InitializeComponent();

            var connectionString = ConnectionStringBuilder.Build(
                Settings.Default.server,
                Settings.Default.database,
                Settings.Default.user,
                Settings.Default.password);

            var repository = new UserRepository(connectionString);

            viewModel = new UserListPageViewModel(this, repository);
            DataContext = viewModel;
        }

        private User SelectedUser
        {
            get { return (User)listView.SelectedItem; }
        }

        public int ListCount
        {
            set { window.StatusBarCount.Content = value; }
        }

        private void ListView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            viewModel.OpenEditor(SelectedUser);
        }

        private void CreateButton_OnClick(object sender, RoutedEventArgs e)
        {
            viewModel.OpenEditor(null);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.OpenEditor(SelectedUser);
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            viewModel.Delete(SelectedUser);
        }
    }
}