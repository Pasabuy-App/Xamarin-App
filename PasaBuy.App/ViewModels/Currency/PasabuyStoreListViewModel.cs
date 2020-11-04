using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.Views.Notice;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

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
            _storeList.Clear();
            LoadStore();
           
        }

        public void LoadStore()
        {
            try
            {
                Http.TindaFeature.Instance.StoreTypeList("pasaplus", "active", "", async (bool success, string data) =>
                {
                    if (success)
                    {
                        StoreListData datas = JsonConvert.DeserializeObject<StoreListData>(data);
         
                        if (datas.data.Length > 0)
                        {
                            for (int i = 0; i < datas.data.Length; i++)
                            {
                                _storeList.Add(new Store()
                                {
                                    Id = datas.data[i].hsid,
                                    Title = datas.data[i].title,
                                    Description = datas.data[i].short_info,
                                    Logo = datas.data[i].avatar == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-store.png" : PSAProc.GetUrl(datas.data[i].avatar),
                                    Offer = "50% off",
                                    ItemRating = datas.data[i].rates == "No ratings yet" ? "N/A" : datas.data[i].rates,
                                    Banner = datas.data[i].banner == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-banner.png" : PSAProc.GetUrl(datas.data[i].banner),
                                    Street = datas.data[i].brgy + ", " + datas.data[i].city
                                });
                            }
                        }
                        else
                        {
                            (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new NoStoresPage()));
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

    }
}
