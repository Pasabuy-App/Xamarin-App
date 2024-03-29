﻿using Newtonsoft.Json;
using PasaBuy.App.Commands;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Settings;
using PasaBuy.App.Views.eCommerce;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.eCommerce
{
    public class ChangeAddressViewModel : BaseViewModel
    {
        public static ObservableCollection<AddressData> _addressList;

        private DelegateCommand _selectAddressCommand;
        private DelegateCommand _addAddressCommand;
        private DelegateCommand _confirmAddress;
        public bool _isRunning = false;
        public bool isRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                _isRunning = value;
                this.NotifyPropertyChanged();
            }
        }

        bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
        public ICommand RefreshCommand { protected set; get; }
        public ChangeAddressViewModel()
        {
            _addressList = new ObservableCollection<AddressData>();
            _addressList.Clear();
            LoadData();
            RefreshCommand = new Command<string>((key) =>
            {
                _addressList.Clear();
                LoadData();
                IsRefreshing = false;
            });
        }
        public void LoadData()
        {
            try
            {
                isRunning = true;
                Http.DataVice.Address.Instance.Listing((bool success, string data) =>
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
                        isRunning = false;
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                        isRunning = false;
                    }
                });
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: DVV1ADD-L1CAVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-DVV1ADD-L1CAVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: DVV1ADD-L1CAVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-DVV1ADD-L1CAVM-" + err.ToString());
                }
                isRunning = false;
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

        public DelegateCommand AddAddressCommand =>
           _addAddressCommand ?? (_addAddressCommand = new DelegateCommand(AddAddressClicked));

        public DelegateCommand ConfirmAddress =>
            _confirmAddress ?? (_confirmAddress = new DelegateCommand(ConfirmAddressClicked));

        private void ConfirmAddressClicked(object obj)
        {
            try
            {
                if (!isRunning)
                {
                    isRunning = true;
                    Http.DataVice.Address.Instance.Update(AddressInMapPage.address_id, StreetEntry, AddressInMapPage.lat.ToString(), AddressInMapPage.lon.ToString(), async (bool success, string data) =>
                    {
                        if (success)
                        {
                            CheckoutPageViewModel.InsertAddress(AddressInMapPage.address_id, AddressInMapPage.person, AddressInMapPage.type, AddressInMapPage.full_address, AddressInMapPage.contact);
                            int numModals = Application.Current.MainPage.Navigation.ModalStack.Count;
                            for (int currModal = 0; currModal < numModals - 3; currModal++)
                            {
                                await Application.Current.MainPage.Navigation.PopModalAsync(false);
                            }
                            isRunning = false;
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            isRunning = false;
                        }
                    });
                }
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: DVV1ADD-U1CAVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-DVV1ADD-U1CAVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: DVV1ADD-U1CAVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-DVV1ADD-U1CAVM-" + err.ToString());
                }
                isRunning = false;
            }
        }
        private async void AddAddressClicked(object obj)
        {
            if (!isRunning)
            {
                isRunning = true;
                Views.Settings.AddAddressPage.addPath = "Another";
                await Task.Delay(200);
                await Application.Current.MainPage.Navigation.PushModalAsync(new PasaBuy.App.Views.Settings.AddAddressPage());
                isRunning = false;
            }
        }
        private void SelectAddressClicked(object obj)
        {
            try
            {
                if (!isRunning)
                {
                    isRunning = true;
                    var btn = obj as TapGestureRecognizer;
                    Http.DataVice.Address.Instance.SelectByID( btn.ClassId, async (bool success, string data) =>
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
                            await Application.Current.MainPage.Navigation.PushModalAsync(new AddressInMapPage());
                            isRunning = false;
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            isRunning = false;
                        }
                    });
                }
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: DVV1ADD-S1CAVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-DVV1ADD-S1CAVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: DVV1ADD-S1CAVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-DVV1ADD-S1CAVM-" + err.ToString());
                }
                isRunning = false;
            }
        }

    }
}
