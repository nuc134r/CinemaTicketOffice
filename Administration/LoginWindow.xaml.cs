using System.Windows;
using Administration.Interfaces;
using Administration.ViewModel;

namespace Administration
{
    /// <summary>
    ///     Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window, ILoginWindow
    {
        private readonly LoginWindowViewModel viewModel;

        public LoginWindow()
        {
            InitializeComponent();

            viewModel = new LoginWindowViewModel(this);
            DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CheckConnection();
        }

        public void IndicateSuccess()
        {
            DialogResult = true;
        }
    }
}