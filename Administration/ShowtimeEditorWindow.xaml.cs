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

namespace Administration
{
    public partial class ShowtimeEditorWindow
    {
        public ShowtimeEditorWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void HoursBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var text = ((TextBox)sender).Text + e.Text;
            e.Handled = !ValidateNumberIsCorrectAndBetween(0, 23, text);
        }

        private void MinutesBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var text = ((TextBox)sender).Text + e.Text;
            e.Handled = !ValidateNumberIsCorrectAndBetween(0, 59, text);
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
