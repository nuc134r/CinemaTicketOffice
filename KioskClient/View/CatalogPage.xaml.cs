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

            listBox1.ItemsSource = new List<string>
            {
                "12:30",
                "13:50",
                "16:00",
                "21:20"
            };
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