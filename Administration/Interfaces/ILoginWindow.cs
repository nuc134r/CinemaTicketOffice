using System.Windows.Threading;

namespace Administration.Interfaces
{
    public interface ILoginWindow
    {
        void IndicateSuccess();
        string Password { get; set; }
        Dispatcher Dispatcher { get; }
        void IndicateConnecting();
        void IndicateConnectingFinished();
    }
}