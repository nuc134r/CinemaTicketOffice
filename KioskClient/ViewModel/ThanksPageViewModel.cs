using System.Timers;
using System.Windows.Controls;

namespace KioskClient.ViewModel
{
    public class ThanksPageViewModel : ViewModelBase
    {
        private readonly Timer delayTimer;
        private Timer timer;

        public ThanksPageViewModel(Page view)
        {
            this.view = view;

            delayTimer = new Timer(1500);
            delayTimer.Elapsed += DelayTimerOnElapsed;
        }

        public void StartTimers()
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
                view.Dispatcher.Invoke(() => { Window.NavigateToMovieCatalog(); });
            };

            timer.Start();
        }
    }
}