using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using Administration.Properties;

namespace Administration
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

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ParseCommandLineArgs();
            ConfigureCulture();

            var loginWindow = new LoginWindow();
            var result = loginWindow.ShowDialog();

            if (result.HasValue && result.Value)
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();
            }
            else
            {
                Application.Current.Shutdown();
            }
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
    }
}