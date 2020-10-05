using PasaBuy.App.Models.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PasaBuy.App.ViewModels.eCommerce
{
    public class ChangeAddressViewModel: BaseViewModel
    {
        private ObservableCollection<AddressData> _addressList;

        public ChangeAddressViewModel()
        {
            _addressList = new ObservableCollection<AddressData>();

            for (int i = 0; i < 4; i++)
            {
                _addressList.Add(new AddressData
                {
                    ID = "1",
                    Types = "Home",
                    Country = "Philippines",
                    City = "San Pedro City",
                    Province = "Laguna",
                    FullAddress = "#4 Rainbow Ave, Pacita 2, San Pedro City, Laguna, Philippines",
                    ContactNumber = "09385956099",
                    ContactPerson = "Lorz Becislao"
                });
            }
           
        }

        public ObservableCollection<AddressData> AddressList
        {
            get
            {
                return _addressList;
            }
            set
            {
                _addressList = value;
                this.NotifyPropertyChanged();
            }
        }
    }
}
