using System;
using System.Windows;
using Administration.Properties;
using Administration.ViewModel;
using DataAccess;
using DataAccess.Model;
using DataAccess.Repository;

namespace Administration.Editors
{
    public partial class UserEditorWindow
    {
        private readonly UserEditorWindowViewModel viewModel;
        
        public UserEditorWindow(User user)
        {
            InitializeComponent();

            var connectionString = ConnectionStringBuilder.Build(
                Settings.Default.server,
                Settings.Default.database,
                Settings.Default.user,
                Settings.Default.password);

            var repository = new UserRepository(connectionString);

            viewModel = new UserEditorWindowViewModel(this, user, repository);
            DataContext = viewModel;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                viewModel.Save();
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public void Set(UserType type)
        {
            UserRadio.IsChecked = false;
            AdminRadio.IsChecked = false;
            SuperadminRadio.IsChecked = false;

            switch (type)
            {
                case UserType.User:
                    UserRadio.IsChecked = true;
                    break;
                case UserType.Admin:
                    AdminRadio.IsChecked = true;
                    break;
                case UserType.Superadmin:
                    SuperadminRadio.IsChecked = true;
                    break;
            }
        }

        public void SetPasswordDots()
        {
            passwordBox.Password = "PasswordBoxText";
        }

        public string GetPassword()
        {
            return passwordBox.Password;
        }

        public UserType GetUserType()
        {
            if (UserRadio.IsChecked.HasValue && UserRadio.IsChecked.Value)
            {
                return UserType.User;
            }

            if (AdminRadio.IsChecked.HasValue && AdminRadio.IsChecked.Value)
            {
                return UserType.Admin;
            }

            return UserType.Superadmin;
        }
    }
}