using System.Windows;

namespace Administration
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = new MainWindow();
            var loginWindow = new LoginWindow();

            var result = loginWindow.ShowDialog();

            if (result.HasValue && result.Value)
            {
                mainWindow.Show();
            }
            else
            {
                mainWindow.Close();
            }
        }
    }
}