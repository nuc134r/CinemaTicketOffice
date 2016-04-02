using System;
using System.Timers;
using System.Windows;
using KioskClient.ViewModel;

namespace KioskClient.View
{
    public partial class ThanksPage
    {
        private readonly ThanksPageViewModel viewModel;
        private readonly Timer timer;

        public ThanksPage()
        {
            InitializeComponent();

            viewModel = new ThanksPageViewModel(this);
            timer = new Timer(7000);
            timer.Elapsed += TimerOnElapsed;
        }

        private void ThanksPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            timer.Stop();

            Dispatcher.Invoke(viewModel.GoToCatalogPage);
        }
    }
}