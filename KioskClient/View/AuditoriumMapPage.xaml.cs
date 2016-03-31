using System.Windows;
using System.Windows.Controls;
using DataAccess.Model;
using KioskClient.Domain;

namespace KioskClient.View
{
    public partial class AuditoriumMapPage
    {
        public AuditoriumMapPage(Showtime showtime)
        {
            InitializeComponent();

            DataContext = new AuditoriumView(showtime.Auditorium);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Window.GetWindow(this)).NavigateBack();
        }
    }
}