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
                Store curStore = new Store
                {
                    Title = "Pasabuy Dev Store",
                    Logo = "House.png"
                };
                curStore.City = "Binan City";
                curStore.Province = "Laguna";
                curStore.Opening = "10:00 AM";
                curStore.Closing = "8:00 PM";

                _storeList.Add(curStore);
            }
        }
            
    }
}
