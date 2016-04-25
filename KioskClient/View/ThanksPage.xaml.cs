using System.Timers;
using System.Windows;
using KioskClient.ViewModel;

namespace KioskClient.View
{
    public partial class ThanksPage
    {
        private readonly Timer delayTimer;
        private readonly ThanksPageViewModel viewModel;
        private Timer timer;

        public ThanksPage()
        {
            InitializeComponent();

            viewModel = new ThanksPageViewModel(this);

            delayTimer = new Timer(1500);
            delayTimer.Elapsed += DelayTimerOnElapsed;
        }

        private void ThanksPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            delayTimer.Start();
        }

        private void DelayTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            delayTimer.Stop();

            //SystemDriveConnection.EjectDiskE();

            timer = new Timer(2000);
            timer.Elapsed += (o, eventArgs) =>
            {
                timer.Stop();
                Dispatcher.Invoke(viewModel.GoToCatalogPage);
            };

            timer.Start();
        }
    }
}