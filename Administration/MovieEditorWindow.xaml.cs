using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Administration.ViewModel;
using DataAccess.Model;

namespace Administration
{
    public partial class MovieEditorWindow
    {
        private MovieEditorWindowViewModel viewModel;

        public MovieEditorWindow(Movie movie)
        {
            InitializeComponent();

            viewModel = new MovieEditorWindowViewModel(movie);
            DataContext = viewModel;
        }
    }
}
