using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using DataAccess;
using DataAccess.Model;
using DataAccess.Repository;
using KioskClient.Properties;
using KioskClient.ViewModel;

namespace KioskClient.View
{
    public partial class CatalogPage
    {
        private readonly CatalogPageViewModel viewModel;

        public CatalogPage()
        {
            InitializeComponent();
            SetDefaults();

            var connectionString = ConnectionStringBuilder.Build(
                Settings.Default.server,
                Settings.Default.database,
                Settings.Default.user,
                Settings.Default.password);

            var repository = new MovieRepository(connectionString);
            viewModel = new CatalogPageViewModel(this, repository);

            DataContext = viewModel;
        }

        private void SetDefaults()
        {
            GenresGrid.Width = 0;
        }

        private void ResetFilterButton_OnClick(object sender, RoutedEventArgs e)
        {
            viewModel.ResetGenresFilter();
            if (GenresListBox.Items.Count != 0)
                GenresListBox.ScrollIntoView(GenresListBox.Items[0]);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var movie = (Movie) e.AddedItems[0];
            viewModel.NavigateToMovieDetails(movie);
        }

        public void DetachSelectionChangedHandler()
        {
            MoviesListBox.SelectionChanged -= ListBox_SelectionChanged;
        }

        public void AttachSelectionChangedHandler()
        {
            MoviesListBox.SelectionChanged += ListBox_SelectionChanged;
        }
    }
}