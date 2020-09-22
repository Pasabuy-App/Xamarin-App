using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PasaBuy.App.Views.MobilePOS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationViewMaster : ContentPage
    {
        public ListView ListView;

        public NavigationViewMaster()
        {
            InitializeComponent();

            BindingContext = new NavigationViewMasterViewModel();
            ListView = MenuItemsListView;
        }

        class NavigationViewMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<NavigationViewMasterMenuItem> MenuItems { get; set; }

            public NavigationViewMasterViewModel()
            {
                MenuItems = new ObservableCollection<NavigationViewMasterMenuItem>(new[]
                {
                    new NavigationViewMasterMenuItem { Id = 0, Title = "Page 1" },
                    new NavigationViewMasterMenuItem { Id = 1, Title = "Page 2" },
                    new NavigationViewMasterMenuItem { Id = 2, Title = "Page 3" },
                    new NavigationViewMasterMenuItem { Id = 3, Title = "Page 4" },
                    new NavigationViewMasterMenuItem { Id = 4, Title = "Page 5" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}