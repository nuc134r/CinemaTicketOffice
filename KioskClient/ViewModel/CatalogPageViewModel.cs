using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using KioskClient.Model;
using KioskClient.View;

namespace KioskClient.ViewModel
{
    public class CatalogPageViewModel : ViewModelBase
    {
        public List<Genre> Genres { get; private set; }

        public CatalogPageViewModel()
        {
            View = new CatalogPage();
        }

    }
}