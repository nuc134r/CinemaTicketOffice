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
            ServerNameLabel.Content = Settings.Default.server + "/" + Settings.Default.database;

            try
            {
                DataContext = new MovieListPage(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                default:
                    DataContext = null;
                    break;
            }
        }
    }
}