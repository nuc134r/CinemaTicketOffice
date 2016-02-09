using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace KioskClient.View
{
    public partial class CatalogPage : Page
    {
        public CatalogPage()
        {
            InitializeComponent();
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            var window = (NavigationWindow)Window.GetWindow(this);
            if (window != null) window.Close();
        }
    }
}
