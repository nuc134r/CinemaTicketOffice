using System.Windows;

namespace KioskClient
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = new MainWindow();
            var catalogPage = new View.CatalogPage();

            mainWindow.Content = catalogPage;

            mainWindow.Show();
        }
    }
}