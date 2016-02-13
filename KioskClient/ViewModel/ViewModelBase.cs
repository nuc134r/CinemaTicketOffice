using System.Windows;
using System.Windows.Controls;

namespace KioskClient.ViewModel
{
    public class ViewModelBase
    {
        protected Page view;

        public Page View
        {
            get { return view; }
        }

        protected IMainWindow TheWindow
        {
            get
            {
                return (IMainWindow) Window.GetWindow(view);
            }
        }
    }
}