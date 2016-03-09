using System.Windows;
using System.Windows.Controls;

namespace KioskClient.ViewModel
{
    public class ViewModelBase
    {
        protected Page view;

        protected IMainWindow Window
        {
            get
            {
                return (IMainWindow) System.Windows.Window.GetWindow(view);
            }
        }
    }
}