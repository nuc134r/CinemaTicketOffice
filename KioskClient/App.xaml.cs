using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using KioskClient.Properties;
using KioskClient.View;

namespace KioskClient
{
    public partial class App
    {
        public static readonly Dictionary<string, string> Arguments = new Dictionary<string, string>();

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

        private void ParseCommandLineArgs()
        {
            var args = Environment.GetCommandLineArgs();

            if (args.Length % 2 != 1) return;

            var i = 1;
            while (i < args.Length)
            {
                Arguments.Add(args[i++], args[i++]);
            }

            if (Arguments.ContainsKey("-s") && Arguments.ContainsKey("-d"))
            {
                Settings.Default.server = Arguments["-s"];
                Settings.Default.database = Arguments["-d"];

                Settings.Default.Save();
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ParseCommandLineArgs();
            ConfigureCulture();

            var mainWindow = new MainWindow();

#if DEBUG
            mainWindow.AllowsTransparency = false;
            mainWindow.WindowState = WindowState.Normal;
            mainWindow.WindowStyle = WindowStyle.SingleBorderWindow;
            mainWindow.Topmost = false;
#endif
            try
            {
                var catalogPage = new CatalogPage();
                mainWindow.DataContext = catalogPage;
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}