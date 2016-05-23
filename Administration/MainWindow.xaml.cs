using System;
using System.Windows;
using System.Windows.Controls;
using Administration.Properties;
using Administration.View;
using Administration.ViewModel;

namespace Administration
{
    public partial class MainWindow
    {
        private readonly MainWindowViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();

            viewModel = new MainWindowViewModel(Settings.Default.currentRole);
            DataContext = viewModel;

            SectionListTreeView.SelectedItemChanged += TreeView_SelectedItemChanged;
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            ServerNameLabel.Content = Settings.Default.server;
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = (TreeViewItem) SectionListTreeView.SelectedItem;
            if (item == null || item.Tag == null) return;

            var pageId = item.Tag.ToString();

            try
            {
                switch (pageId)
                {
                    case "0":
                        viewModel.Frame = new MovieListPage(this);
                        break;
                    case "1":
                        viewModel.Frame = new GenreListPage(this);
                        break;
                    case "2":
                        viewModel.Frame = new ShowtimeListPage(this);
                        break;
                    case "4":
                        viewModel.Frame = new SeatListPage(this);
                        break;
                    case "5":
                        viewModel.Frame = new LogoSetupPage(this);
                        break;
                    case "3":
                        viewModel.Frame = new AuditoriumListPage(this);
                        break;
                    case "6":
                        viewModel.Frame = new UserListPage(this);
                        break;
                    case "7":
                        viewModel.Frame = new LogEntryListPage(this);
                        break;
                    case "8":
                        viewModel.Frame = new SoldTicketsReportPage();
                        break;
                    default:
                        viewModel.Frame = null;
                        break;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }
    }
}