﻿using System;
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
using Administration.Interfaces;
using Administration.Properties;
using Administration.ViewModel;
using DataAccess;
using DataAccess.Model;
using DataAccess.Repository;

namespace Administration
{
    public partial class MovieEditorWindow : IMovieEditorWindow
    {
        private MovieEditorWindowViewModel viewModel;

        public MovieEditorWindow(Movie movie)
        {
            InitializeComponent();

            var connectionString = ConnectionStringBuilder.Build(
                Settings.Default.server,
                Settings.Default.database,
                Settings.Default.user,
                Settings.Default.password);

            var repository = new MovieRepository(connectionString);

            var movieRepository = new MovieRepository(connectionString);

            viewModel = new MovieEditorWindowViewModel(this, movie, movieRepository);
            DataContext = viewModel;
        }

        public int SelectedAgeLimitIndex
        {
            set { ageLimitBox.SelectedIndex = value; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var text = ((TextBox) sender).Text + e.Text;
            short value;

            if (!short.TryParse(text, out value))
            {
                e.Handled = true;
            }
        }
    }
}
