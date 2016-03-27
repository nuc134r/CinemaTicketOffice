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
    public partial class ShowtimeEditorWindow
    {
        private ShowtimeEditorWindowViewModel viewModel;

        public ShowtimeEditorWindow(Showtime showtime)
        {
            InitializeComponent();

            var connectionString = ConnectionStringBuilder.Build(
                Settings.Default.server,
                Settings.Default.database,
                Settings.Default.user,
                Settings.Default.password);

            var movieRepository = new MovieRepository(connectionString);
            var showtimeRepository = new ShowtimeRepository(connectionString);

            viewModel = new ShowtimeEditorWindowViewModel(this, showtime, movieRepository, showtimeRepository);
            DataContext = viewModel;
        }

        public int SelectedMovieIndex
        {
            set { MovieBox.SelectedIndex = value; }
            get { return MovieBox.SelectedIndex; }
        }

        public DateTime Time
        {
            set
            {
                HoursBox.Text = value.Hour.ToString();
                MinutesBox.Text = value.Minute.ToString();
            }
            get
            {
                var hours = int.Parse(HoursBox.Text);
                var minutes = int.Parse(MinutesBox.Text);

                return DateBox.DisplayDate.Date.AddHours(hours).AddMinutes(minutes);
            }
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

        private void HoursBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var text = ((TextBox) sender).Text + e.Text;
            e.Handled = !ValidateNumberIsCorrectAndBetween(0, 23, text);
        }

        private void MinutesBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var text = ((TextBox) sender).Text + e.Text;
            e.Handled = !ValidateNumberIsCorrectAndBetween(0, 59, text);
        }

        private void PriceBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var text = ((TextBox)sender).Text + e.Text;
            e.Handled = !ValidateNumberIsCorrectAndBetween(0, 15000, text);
        }

        private static bool ValidateNumberIsCorrectAndBetween(int from, int to, string text)
        {
            int value;

            if (int.TryParse(text, out value))
            {
                if (value >= from && value <= to)
                {
                    return true;
                }
            }

            return false;
        }
    }
}