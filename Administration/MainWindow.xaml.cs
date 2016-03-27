using System;
using System.Windows;
using System.Windows.Controls;
using Administration.Properties;
using Administration.View;

namespace Administration
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            SectionListTreeView.SelectedItemChanged += TreeView_SelectedItemChanged;
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            ServerNameLabel.Content = Settings.Default.server;
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = (TreeViewItem) SectionListTreeView.SelectedItem;
            var id = item.Tag.ToString();

            switch (id)
            {
                case "0":
                    DataContext = new MovieListPage(this);
                    break;
                case "1":
                    DataContext = new GenreListPage(this);
                    break;
                case "2":
                    DataContext = new ShowtimeListPage(this);
                    break;
                case "5":
                    DataContext = new LogoSetupPage(this);
                    break;
                default:
                    DataContext = null;
                    break;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }
    }
}