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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            listBox.ItemsSource = new List<EntityType>
            {
                new EntityType() {Title = "Фильмы", Count = 4},
                new EntityType() {Title = "Сеансы", Count = 16},
                new EntityType() {Title = "Кинозалы", Count = 3},
                new EntityType() {Title = "Жанры", Count = 10}
            };
        }
    }
}
