using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using DataAccess;
using DataAccess.Model;
using DataAccess.Repository;
using KioskClient.Domain;
using KioskClient.Properties;
using KioskClient.ViewModel;

namespace KioskClient.View
{
    public partial class CheckoutPage
    {
        private readonly CheckoutPageViewModel viewModel;

        public CheckoutPage(Showtime showtime, List<AuditoriumSeat> seats)
        {
            InitializeComponent();

            var connectionString = ConnectionStringBuilder.Build(
                Settings.Default.server,
                Settings.Default.database,
                Settings.Default.user,
                Settings.Default.password);

            var repository = new TicketRepository(connectionString);

            viewModel = new CheckoutPageViewModel(this, repository, showtime, seats);
            DataContext = viewModel;

            SetUpAnimations();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Cancel();
        }

        private void SetUpAnimations()
        {
            RegisterName("ArrowAnimatedBrush", viewModel.ArrowAnimatedBrush);

            var animation = new ColorAnimation()
            {
                From = Colors.Gray,
                To = Colors.White,
                Duration = new Duration(TimeSpan.FromMilliseconds(500)),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };

            Storyboard.SetTargetName(animation, "ArrowAnimatedBrush");
            Storyboard.SetTargetProperty(animation, new PropertyPath(SolidColorBrush.ColorProperty));

            var sb = new Storyboard();
            sb.Children.Add(animation);
            sb.Begin(this);
        }

        private void EmulateButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveTickets();
            viewModel.GoToThanksPage();
        }
    }
}