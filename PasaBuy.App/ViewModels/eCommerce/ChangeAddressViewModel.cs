using PasaBuy.App.Commands;
using PasaBuy.App.Models.Settings;
using PasaBuy.App.Views.eCommerce;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.eCommerce
{
    public class ChangeAddressViewModel: BaseViewModel
    {
        private ObservableCollection<AddressData> _addressList;

        private DelegateCommand _selectAddressCommand;
        private DelegateCommand _pinAnotherCommand;
        private DelegateCommand _confirmAddress;


        


        public ChangeAddressViewModel()
        {
            _addressList = new ObservableCollection<AddressData>();

            for (int i = 0; i < 3; i++)
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

        public DelegateCommand SelectAddressCommand =>
            _selectAddressCommand ?? (_selectAddressCommand = new DelegateCommand(SelectAddressClicked));

        public DelegateCommand PinAnotherCommand =>
            _pinAnotherCommand ?? (_pinAnotherCommand = new DelegateCommand(PinAnotherClicked));

        public DelegateCommand ConfirmAddress =>
            _confirmAddress ?? (_confirmAddress = new DelegateCommand(ConfirmAddressClicked));

        private async void ConfirmAddressClicked(object obj)
        {
            int numModals = Application.Current.MainPage.Navigation.ModalStack.Count;
            for (int currModal = 0; currModal < numModals - 3; currModal++)
            {
                await Application.Current.MainPage.Navigation.PopModalAsync(false);
            }
        }

        private async void PinAnotherClicked(object obj)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new AddressInMapPage());

        }

        private async void SelectAddressClicked(object obj)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new AddressInMapPage());
        }

    }
}
