using PasaBuy.App.Models.Marketplace;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PasaBuy.App.ViewModels.Currency
{
    public class PasabuyStoreListViewModel : BaseViewModel
    {
        private ObservableCollection<Store> _storeList;

        public ObservableCollection<Store> StoreList
        {
            get
            {
                return _storeList;
            }
            set
            {
                _storeList = value;
                this.NotifyPropertyChanged();
            }
        }

        public PasabuyStoreListViewModel()
        {
            _storeList = new ObservableCollection<Store>();
            for(int i = 0; i < 7; i++)
            {
                _storeList.Add(new Store
                {
                    Title = "Pasabuy Dev Store",
                    Description = "Sample Store for Development Purposes",
                    Logo = "House.png"
                });
            }
        }
            
    }
}
