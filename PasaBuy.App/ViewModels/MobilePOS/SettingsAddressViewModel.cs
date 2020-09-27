using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class SettingsAddressViewModel : BaseViewModel
    {
        #region Fields
        public static ObservableCollection<SettingsAddressData> addressList;
        public ObservableCollection<SettingsAddressData> AddressList
        {
            get { return addressList; }
            set { addressList = value; this.NotifyPropertyChanged(); }
        }
        #endregion
        public SettingsAddressViewModel()
        {
            addressList = new ObservableCollection<SettingsAddressData>();
            addressList.Clear();
            LoadData();
        }
        public static void LoadData()
        {
            try
            {
                TindaPress.Address.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, PSACache.Instance.UserInfo.stid, "", "", "1", (bool success, string data) =>
                {
                    if (success)
                    {
                        SettingsAddressData add = JsonConvert.DeserializeObject<SettingsAddressData>(data);
                            for (int i = 0; i < add.data.Length; i++)
                            {
                                string id = add.data[i].ID;
                                string street = add.data[i].street;
                                string brgy = add.data[i].brgy;
                                string city = add.data[i].city;
                                string province = add.data[i].province;
                                string country = add.data[i].country;
                                string type = add.data[i].type;
                                addressList.Add(new SettingsAddressData()
                                {
                                    ID = id,
                                    Street = street + " " + brgy + " " + city + " " + province + ", " + country,
                                    Type = type,
                                });
                            }
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                    }
                });
            }
            catch (Exception)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error Code: 20465.", "OK");
            }
        }
    }
}
