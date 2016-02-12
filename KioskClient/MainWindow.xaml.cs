using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace KioskClient
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            TitleTextBlock.Text = ((Page) MainFrame.Content).Title;
        }
    }
}