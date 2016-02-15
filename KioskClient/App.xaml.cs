using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using KioskClient.View;
using KioskClient.ViewModel;

namespace KioskClient
{
    public partial class App : Application
    {
        private static void ConfigureCulture()
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CurrentCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CurrentCulture;

            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(System.Windows.Documents.Run),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureCulture();

            var mainWindow = new MainWindow();

#if DEBUG
            mainWindow.AllowsTransparency = false;
            mainWindow.WindowState = WindowState.Normal;
            mainWindow.WindowStyle = WindowStyle.SingleBorderWindow;
            mainWindow.Topmost = false;
#endif
            
            var catalogPage = new CatalogPage();
            mainWindow.DataContext = catalogPage;

            mainWindow.Show();
        }
    }
}