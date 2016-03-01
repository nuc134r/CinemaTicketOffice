using System;
using System.Windows;
using Administration.Properties;
using Administration.View;

namespace Administration
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            ServerNameLabel.Content = Settings.Default.server + "/" + Settings.Default.database;

            try
            {
                var page = new MovieListPage(this);
                DataContext = page;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}