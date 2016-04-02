using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using DataAccess.Model;
using KioskClient.Domain;
using KioskClient.ViewModel;

namespace KioskClient.View
{
    public partial class CheckoutPage
    {
        private readonly CheckoutPageViewModel viewModel;

        public CheckoutPage(Showtime showtime, IEnumerable<AuditoriumSeat> seats)
        {
            InitializeComponent();

            var total = showtime.Price * seats.Count();

            viewModel = new CheckoutPageViewModel(this, total);
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
            viewModel.GoToThanksPage();
        }
    }
}