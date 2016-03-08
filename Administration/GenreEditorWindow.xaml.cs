using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Administration.Properties;
using Administration.ViewModel;
using DataAccess;
using DataAccess.Model;
using DataAccess.Repository;

namespace Administration
{
    /// <summary>
    /// Interaction logic for GenreEditorWindow.xaml
    /// </summary>
    public partial class GenreEditorWindow : Window
    {
        private readonly GenreEditorWindowViewModel viewModel;

        public GenreEditorWindow(Genre genre)
        {
            InitializeComponent();

            var connectionString = ConnectionStringBuilder.Build(
               Settings.Default.server,
               Settings.Default.database,
               Settings.Default.user,
               Settings.Default.password);

            var repository = new MovieRepository(connectionString);

            viewModel = new GenreEditorWindowViewModel(genre, repository);
            DataContext = viewModel;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                viewModel.Save();
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
