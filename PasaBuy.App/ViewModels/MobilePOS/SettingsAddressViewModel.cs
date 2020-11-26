using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.MobilePOS;
using System;
using System.Collections.ObjectModel;

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
                Http.TindaPress.Store.Instance.Address((bool success, string data) =>
                {
                    if (success)
                    {
                        SettingsAddressData add = JsonConvert.DeserializeObject<SettingsAddressData>(data);
                        for (int i = 0; i < add.data.Length; i++)
                        {
                            addressList.Add(new SettingsAddressData()
                            {
                                ID = add.data[i].ID,
                                Street = add.data[i].street + " " + add.data[i].brgy + " " + add.data[i].city + " " + add.data[i].province + ", " + add.data[i].country,
                                Type = add.data[i].type,
                            });
                        }
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");

                    }
                });
            }
            catch (Exception err)
            {
                if (PSAConfig.isDebuggable)
                {
                    new Controllers.Notice.Alert("Error Code: TPV2STR-A1SAVM", err.ToString(), "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("DEV-TPV2STR-A1SAVM-" + err.ToString());
                }
                else
                {
                    new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2STR-A1SAVM.", "OK");
                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("LIVE-TPV2STR-A1SAVM-" + err.ToString());
                }
            }
        }
    }
}
