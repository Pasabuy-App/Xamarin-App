﻿using Newtonsoft.Json;
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
        
        public ChangeAddressViewModel()
        {
            _addressList = new ObservableCollection<AddressData>();
            _addressList.Clear();
            LoadData();
           /* for (int i = 0; i < 4; i++)
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
            }*/
           
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
                                ID = address.data[i].id,
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

        public DelegateCommand SelectAddressCommand =>
            _selectAddressCommand ?? (_selectAddressCommand = new DelegateCommand(SelectAddressClicked));

        private void SelectAddressClicked(object obj)
        {
            try
            {
                var btn = obj as TapGestureRecognizer;
                //Console.WriteLine("ClassID: " + btn.ClassId);
                DataVice.Address.Instance.SelectByID(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, btn.ClassId, (bool success, string data) =>
                {
                    if (success)
                    {
                        AddressData address = JsonConvert.DeserializeObject<AddressData>(data);
                        AddressInMapPage.street = btn.ClassId;
                        for (int i = 0; i < address.data.Length; i++)
                        {
                            AddressInMapPage.street = address.data[i].street;
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
                //await Application.Current.MainPage.Navigation.PushModalAsync(new AddressInMapPage());
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }

    }
}
