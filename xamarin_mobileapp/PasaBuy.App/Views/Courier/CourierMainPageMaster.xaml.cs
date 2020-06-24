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

namespace PasaBuy.App.Views.Courier
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourierMainPageMaster : ContentPage
    {
        public ListView ListView;

        public CourierMainPageMaster()
        {
            InitializeComponent();

            BindingContext = new CourierMainPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class CourierMainPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<CourierMainPageMasterMenuItem> MenuItems { get; set; }

            public CourierMainPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<CourierMainPageMasterMenuItem>(new[]
                {
                    new CourierMainPageMasterMenuItem { Id = 0, Title = "Page 1" },
                    new CourierMainPageMasterMenuItem { Id = 1, Title = "Page 2" },
                    new CourierMainPageMasterMenuItem { Id = 2, Title = "Page 3" },
                    new CourierMainPageMasterMenuItem { Id = 3, Title = "Page 4" },
                    new CourierMainPageMasterMenuItem { Id = 4, Title = "Page 5" },
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