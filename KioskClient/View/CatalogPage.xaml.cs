using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using DataAccess.Model;
using DataAccess.Repository;
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

            viewModel = new CatalogPageViewModel(this, new MovieRepository());

            DataContext = viewModel;
        }

        private void SetDefaults()
        {
            GenresGrid.Width = 0;
        }

        private void ResetFilterButton_OnClick(object sender, RoutedEventArgs e)
        {
            viewModel.ResetGenresFilter();
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