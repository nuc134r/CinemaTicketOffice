using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using KioskClient.Annotations;
using KioskClient.DataAccessLayer;
using KioskClient.Model;
using KioskClient.ViewModel;

namespace KioskClient.View
{
    public partial class CatalogPage : Page
    {
        private readonly CatalogPageViewModel viewModel;

        public CatalogPage()
        {
            InitializeComponent();
            SetDefaults();

            viewModel = new CatalogPageViewModel(this, new CatalogPageDataAccessLayer());
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
            viewModel.ResetGenresFilter();

            DetachSelectionChangedHandler();
            MoviesListBox.SelectedIndex = -1;
            AttachSelectionChangedHandler();

            MoviesListBox.ScrollIntoView(MoviesListBox.Items[0]);

            viewModel.NavigateToMovieDetails((Movie) e.AddedItems[0]);
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