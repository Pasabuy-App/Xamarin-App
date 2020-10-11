using Newtonsoft.Json;
using PasaBuy.App.Commands;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
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
        public static ObservableCollection<AddressData> _addressList;

        private DelegateCommand _selectAddressCommand;
        private DelegateCommand _pinAnotherCommand;
        private DelegateCommand _confirmAddress;
        public ChangeAddressViewModel()
        {
            _addressList = new ObservableCollection<AddressData>();
            _addressList.Clear();
            LoadData();
        }
        public static void LoadData()
        {
            try
            {
                DataVice.Address.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, (bool success, string data) =>
                {
                    if (success)
                    {
                        AddressData address = JsonConvert.DeserializeObject<AddressData>(data);

                        for (int i = 0; i < address.data.Length; i++)
                        {
                            string types = address.data[i].types;
                            string status = address.data[i].status;
                            string type = string.Empty;
                            if (types == "home") { type = "Home"; }
                            if (types == "office") { type = "Office"; }
                            if (types == "business") { type = "Business"; }
                            _addressList.Add(new AddressData
                            {
                                ID = address.data[i].ID,
                                Types = types,
                                Country = address.data[i].country,
                                City = address.data[i].city,
                                Province = address.data[i].province,
                                FullAddress = address.data[i].street + " " + address.data[i].brgy + " " + address.data[i].city + " " + address.data[i].province + ", " + address.data[i].country,
                                ContactNumber = address.data[i].contact,
                                ContactPerson = address.data[i].contact_person
                            });
                        }
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                    }
                });
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
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

        public string streetEntry;
        public string StreetEntry
        {
            get { return this.streetEntry; }

            set
            {
                if (this.streetEntry == value)
                {
                    return;
                }

                this.streetEntry = value;
                this.NotifyPropertyChanged();
            }
        }
        public DelegateCommand SelectAddressCommand =>
            _selectAddressCommand ?? (_selectAddressCommand = new DelegateCommand(SelectAddressClicked));

        public DelegateCommand PinAnotherCommand =>
           _pinAnotherCommand ?? (_pinAnotherCommand = new DelegateCommand(PinAnotherClicked));

        public DelegateCommand ConfirmAddress =>
            _confirmAddress ?? (_confirmAddress = new DelegateCommand(ConfirmAddressClicked));

        private void ConfirmAddressClicked(object obj)
        {
            try
            {
                //Console.WriteLine("Ayup: ." + StreetEntry + ". ." + AddressInMapPage.address_id + ". ." +AddressInMapPage.lat.ToString() + ". ." + AddressInMapPage.lon.ToString());
                DataVice.Address.Instance.Update(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, AddressInMapPage.address_id, "", "", "", "", StreetEntry, AddressInMapPage.lat.ToString(), AddressInMapPage.lon.ToString(), (bool success, string data) =>
                {
                    if (success)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            CheckoutPageViewModel.InsertAddress(AddressInMapPage.address_id, AddressInMapPage.person, AddressInMapPage.type, AddressInMapPage.full_address, AddressInMapPage.contact);
                            int numModals = Application.Current.MainPage.Navigation.ModalStack.Count;
                            for (int currModal = 0; currModal < numModals - 3; currModal++)
                            {
                                await Application.Current.MainPage.Navigation.PopModalAsync(false);
                            }
                        });
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                    }
                });
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }
        private async void PinAnotherClicked(object obj)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new AddressInMapPage());

        }
        private void SelectAddressClicked(object obj)
        {
            try
            {
                var btn = obj as TapGestureRecognizer;
                DataVice.Address.Instance.SelectByID(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, btn.ClassId, (bool success, string data) =>
                {
                    if (success)
                    {
                        AddressData address = JsonConvert.DeserializeObject<AddressData>(data);
                        CheckoutPageViewModel.address_id = Convert.ToInt32(btn.ClassId);
                        AddressInMapPage.address_id = btn.ClassId;
                        AddressInMapPage.street = btn.ClassId;
                        for (int i = 0; i < address.data.Length; i++)
                        {
                            AddressInMapPage.street = address.data[i].street;
                            AddressInMapPage.type = address.data[i].types;
                            AddressInMapPage.contact = address.data[i].contact;
                            AddressInMapPage.person = string.IsNullOrEmpty(address.data[i].contact_person) ? PSACache.Instance.UserInfo.dname : address.data[i].contact_person;
                            AddressInMapPage.full_address = address.data[i].street + " " + address.data[i].brgy + " " + address.data[i].city + " " + address.data[i].province + ", " + address.data[i].country;
                            AddressInMapPage.lat = Convert.ToDouble(address.data[i].latitude);
                            AddressInMapPage.lon = Convert.ToDouble(address.data[i].longitude);
                        }
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await Application.Current.MainPage.Navigation.PushModalAsync(new AddressInMapPage());
                        });
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                    }
                });
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }

    }
}
