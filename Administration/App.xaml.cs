using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace Administration
{
    public partial class App
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