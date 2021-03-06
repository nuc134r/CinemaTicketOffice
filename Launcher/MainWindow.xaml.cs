﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Launcher
{
    public partial class MainWindow
    {
        private readonly MainWindowViewModel viewModel;
        private readonly SolidColorBrush connectingTextBrush = new SolidColorBrush();

        public MainWindow()
        {
            InitializeComponent();

            viewModel = new MainWindowViewModel(this);
            DataContext = viewModel;
            SetUpAnimations();
        }

        private void launchKioskButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.LaunchKiosk();
        }

        private void launchAdminButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.LaunchAdmin();
        }

        public string Password
        {
            get { return passwordBox.Password; }
            set { passwordBox.Password = value; }
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.TryConnect();
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CreateDatabase();
        }

        public void IndicateFail()
        {
            ConnectingStatusLabel.Content = "Ошибка";
            ConnectingStatusLabel.Foreground = new SolidColorBrush(Colors.Red);
        }

        public void IndicateProgress()
        {
            ConnectingStatusLabel.Content = "Подключение";
            ConnectingStatusLabel.Foreground = connectingTextBrush;
        }

        public void IndicateSuccess()
        {
            ConnectingStatusLabel.Content = "Подключён";
            ConnectingStatusLabel.Foreground = new SolidColorBrush(Colors.YellowGreen);

            tabControl.SelectedIndex = 1;
            viewModel.CurrentPageText = "2 из 3";
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
        }

        public void ClearLog()
        {
            logTextBox.Text = "";
        }

        public void WriteLog(string text)
        {
            logTextBox.Text += text + Environment.NewLine;
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message);
        }

        public void OnDatabaseCreatedMessage()
        {
            MessageBox.Show("База данных успешно создана!");
            tabControl.SelectedIndex = 2;
            viewModel.CurrentPageText = "3 из 3";
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 0;
            viewModel.CurrentPageText = "1 из 3";
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 2;
            viewModel.CurrentPageText = "3 из 3";
        }

        public string GetExisitingDatabaseName()
        {
            return comboBox.Text;
        }

        public void SetDatabaseList(List<string> databaseList)
        {
            comboBox.Items.Clear();
            databaseList.ForEach(item => comboBox.Items.Add(item));
			comboBox.SelectedIndex = 0;
        }

        private void BackButton2_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 1;
            viewModel.CurrentPageText = "2 из 3";
        }
    }
}