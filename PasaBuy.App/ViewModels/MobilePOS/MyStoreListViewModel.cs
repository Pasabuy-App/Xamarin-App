using PasaBuy.App.Models.Marketplace;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class MyStoreListViewModel : BaseViewModel
    {
        public ObservableCollection<Store> myStores;

        public ObservableCollection<Store> MyStores
        {
            get
            {
                return myStores;
            }
            set 
            {
                myStores = value;
                this.NotifyPropertyChanged();
            }
        }

        public MyStoreListViewModel()
        {
            this.MyStores = new ObservableCollection<Store>()
            {
                new Store
                {
                    Title = "My First Store",
                    Description = "First Description",
                    Logo = "https://pasabuy.app/wp-content/uploads/2020/10/Food-Template.jpg"

                },
                new Store
                {
                    Title = "My Second Store",
                    Description = "Second Description",
                    Logo = "https://pasabuy.app/wp-content/uploads/2020/10/Food-Template.jpg"
                },
                new Store
                {
                    Title = "My Third Store",
                    Description = "Third Description",
                    Logo = "https://pasabuy.app/wp-content/uploads/2020/10/Food-Template.jpg"
                },
            };
        }
    }
}
