using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Administration.Properties;
using Administration.ViewModel;
using DataAccess;
using DataAccess.Model;
using DataAccess.Repository;

namespace Administration
{
    public partial class AuditoriumEditorWindow
    {
        private readonly AuditoriumEditorWindowViewModel viewModel;
        
        public AuditoriumEditorWindow(Auditorium auditorium)
        {
            InitializeComponent();

            var connectionString = ConnectionStringBuilder.Build(
                Settings.Default.server,
                Settings.Default.database,
                Settings.Default.user,
                Settings.Default.password);

            var repository = new ShowtimeRepository(connectionString);

            viewModel = new AuditoriumEditorWindowViewModel(auditorium, repository);
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

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Блокируем ввод значения, которое невозможно преобразовать в byte
            var text = ((TextBox)sender).Text + e.Text;
            byte value;

            if (!byte.TryParse(text, out value))
            {
                e.Handled = true;
            }
        }
    }
}