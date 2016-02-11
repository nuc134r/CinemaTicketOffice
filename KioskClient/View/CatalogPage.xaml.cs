using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using KioskClient.Annotations;
using KioskClient.ViewModel;

namespace KioskClient.View
{
    public partial class CatalogPage : Page
    {
        private readonly CatalogPageViewModel viewModel;

        public CatalogPage(CatalogPageViewModel viewModel)
        {
            InitializeComponent();

            this.viewModel = viewModel;
            DataContext = viewModel;
            SetDefaults();
        }

        private void SetDefaults()
        {
            GenresGrid.Width = 0;
        }

        private void ResetFilterButton_OnClick(object sender, RoutedEventArgs e)
        {
            viewModel.ResetGenresFilter();
        }
    }
}