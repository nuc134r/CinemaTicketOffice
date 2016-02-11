using System.Windows;
using System.Windows.Controls;
using KioskClient.Annotations;

namespace KioskClient.View
{
    public partial class CatalogPage : Page
    {
        public CatalogPage()
        {
            InitializeComponent();
            SetDefaults();

        }

        private void SetDefaults()
        {
            GenresGrid.Width = 0;
        }

        private void ResetFilterButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}