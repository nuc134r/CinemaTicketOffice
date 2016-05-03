using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using Administration.Properties;
using Administration.View;
using DataAccess.Model;
using DataAccess.Repository;

namespace Administration.ViewModel
{
    public class UserListPageViewModel
    {
        private readonly UserRepository repository;
        private readonly UserListPage view;

        public UserListPageViewModel(UserListPage view, UserRepository repository)
        {
            this.view = view;
            this.repository = repository;

            RetrieveData();
        }

        public ObservableCollection<User> Users { get; set; }

        public void OpenEditor(User user)
        {
            try
            {
                if (user != null)
                {
                    user = user.Clone();
                }

                var editor = new UserEditorWindow(user);
                var result = editor.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    RetrieveData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RetrieveData()
        {
            if (Users == null)
            {
                Users = new ObservableCollection<User>();
                Users.CollectionChanged += UsersOnCollectionChanged;
            }

            var users = repository.GetUsers().ToList();

            Users.Clear();
            users.ForEach(user => Users.Add(user));
        }

        private void UsersOnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            view.ListCount = Users.Count;
        }

        public void Delete(User user)
        {
            var result = MessageBox.Show(
                string.Format(Resources.DeleteUserConfirmatonText, user.Login),
                Resources.DeleteConfirmationCaption,
                MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    repository.Delete(user);
                    RetrieveData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}