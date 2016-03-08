using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Administration.Interfaces;
using Administration.ViewModel;

namespace Administration
{
    public partial class LoginWindow : ILoginWindow
    {
        private readonly LoginWindowViewModel viewModel;
        private readonly SolidColorBrush connectingTextBrush = new SolidColorBrush();

        private readonly List<FrameworkElement> inputs; 

        public LoginWindow()
        {
            InitializeComponent();

            viewModel = new LoginWindowViewModel(this);
            DataContext = viewModel;

            SetUpAnimations();

            inputs = new List<FrameworkElement>
            {
                serverTB,
                databaseTB,
                loginTB,
                passwordBox,
                connectButton
            };
        }

        private void SetUpAnimations()
        {
            RegisterName("connectingTextBrush", connectingTextBrush);

            var animation = new ColorAnimation()
            {
                From = Colors.MidnightBlue,
                To = Colors.DodgerBlue,
                Duration = new Duration(TimeSpan.FromMilliseconds(250)),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };

            Storyboard.SetTargetName(animation, "connectingTextBrush");
            Storyboard.SetTargetProperty(animation, new PropertyPath(SolidColorBrush.ColorProperty));

            var sb = new Storyboard();
            sb.Children.Add(animation);
            sb.Begin(this);

            ConnectingStatusLabel.Foreground = connectingTextBrush;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CheckConnection();
        }

        public string Password
        {
            get { return passwordBox.Password; }
            set { passwordBox.Password = value; }
        }

        public void IndicateConnecting()
        {
            inputs.ForEach(input => input.IsEnabled = false);

            ConnectingStatusLabel.Visibility = Visibility.Visible;
        }

        public void IndicateConnectingFinished()
        {
            inputs.ForEach(input => input.IsEnabled = true);

            ConnectingStatusLabel.Visibility = Visibility.Collapsed;
        }

        public void IndicateSuccess()
        {
            DialogResult = true;
        }
    }
}