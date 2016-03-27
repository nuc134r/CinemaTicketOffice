using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Administration.Properties;
using DataAccess;
using DataAccess.Repository;
using Microsoft.Win32;

namespace Administration.View
{
    public partial class LogoSetupPage
    {
        private readonly Window window;
        private readonly SettingsRepository repository;

        public LogoSetupPage(Window window)
        {
            this.window = window;
            InitializeComponent();

            var connectionString = ConnectionStringBuilder.Build(
                Settings.Default.server,
                Settings.Default.database,
                Settings.Default.user,
                Settings.Default.password);

            repository = new SettingsRepository(connectionString);

            DataContext = repository.GetLogo();
        }

        public BitmapImage LogoImage { get; set; }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog {Filter = "Images|*.jpg;*.png;*.bmp"};
            var result = dialog.ShowDialog(window);
            if (result.Value)
            {
                LogoImage = new BitmapImage(new Uri(dialog.FileName));
                DataContext = LogoImage;
                SaveButton.IsEnabled = true;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                DataContext = null;
                SaveButton.IsEnabled = true;
            }
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            repository.SetLogo((BitmapImage)DataContext);
            SaveButton.IsEnabled = false;
        }
    }
}