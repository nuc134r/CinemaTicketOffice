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

        public virtual string Title
        {
            get { return "Override this title"; }
        }
    }
}