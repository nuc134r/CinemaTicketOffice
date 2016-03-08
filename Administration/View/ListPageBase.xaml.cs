using System.Windows.Controls;

namespace Administration.View
{
    public partial class ListPageBase
    {
        private readonly MainWindow window;

        public ListPageBase(MainWindow window)
        {
            this.window = window;
            InitializeComponent();
        }

        public int ListCount
        {
            set { window.StatusBarCount.Content = value; }
        }
    }
}