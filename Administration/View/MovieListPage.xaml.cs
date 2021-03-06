﻿using System.Windows;
using System.Windows.Input;
using Administration.Properties;
using Administration.ViewModel;
using DataAccess;
using DataAccess.Model;
using DataAccess.Repository;

namespace Administration.View
{
    public partial class MovieListPage
    {
        private readonly MovieListPageViewModel viewModel;
        private readonly MainWindow window;

        public MovieListPage(MainWindow window)
        {
            this.window = window;
            InitializeComponent();

            var connectionString = ConnectionStringBuilder.Build(
                Settings.Default.server,
                Settings.Default.database,
                Settings.Default.user,
                Settings.Default.password);

            var repository = new MovieRepository(connectionString);

            viewModel = new MovieListPageViewModel(this, repository);
            DataContext = viewModel.Movies;
        }

        private Movie SelectedMovie
        {
            get { return (Movie) listView.SelectedItem; }
        }

        public int ListCount
        {
            set { window.StatusBarCount.Content = value; }
        }

        private void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            viewModel.OpenEditor(SelectedMovie);
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.OpenEditor(null);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.OpenEditor(SelectedMovie);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedMovie == null) return;
            viewModel.Delete(SelectedMovie);
        }
    }
}