using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.ViewModels.Marketplace;
using PasaBuy.App.Views.Notice;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Currency
{
    public class PasabuyStoreListViewModel : BaseViewModel
    {
        public static ObservableCollection<Models.TindaFeature.StoreModel> _storeList;

        public ObservableCollection<Models.TindaFeature.StoreModel> StoreList
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

        bool _IsRunning = false;
        public bool IsRunning
        {
            get
            {
                return _IsRunning;
            }
            set
            {
                if (_IsRunning != value)
                {
                    _IsRunning = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public PasabuyStoreListViewModel()
        {
            ItemTappedCommand = new Command<object>(NavigateToNextPage);
            _storeList = new ObservableCollection<Models.TindaFeature.StoreModel>();
            _storeList.Clear();
            RefreshCommand = new Command<string>((key) =>
            {
                Views.Currency.PasabuyStoreList.LastIndex = 12;
                _storeList.Clear();
                LoadStore();
                IsRefreshing = false;
            });
            LoadStore();

        }

        public void LoadStore()
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    Http.TindaPress.Store.Instance.Listing("", "", "pasaplus", "", "active", "", async (bool success, string data) =>
                    {
                        if (success)
                        {
                            Models.TindaFeature.StoreModel datas = JsonConvert.DeserializeObject<Models.TindaFeature.StoreModel>(data);

                            if (datas.data.Length > 0)
                            {
                                CultureInfo provider = new CultureInfo("fr-FR");
                                for (int i = 0; i < datas.data.Length; i++)
                                {
                                    string open_time = string.IsNullOrEmpty(datas.data[i].opening) ? "00:00:00" : datas.data[i].opening;
                                    string close_time = string.IsNullOrEmpty(datas.data[i].closing) ? "00:00:00" : datas.data[i].closing;
                                    DateTime open = DateTime.ParseExact(open_time, "HH:mm:ss", provider);
                                    DateTime close = DateTime.ParseExact(close_time, "HH:mm:ss", provider);
                                    string open_close = string.IsNullOrEmpty(datas.data[i].operation_id) ? "Closed" : open.ToString("HH:mm tt") + " - " + close.ToString("HH:mm tt");
                                    string add = string.IsNullOrEmpty(datas.data[i].brgy) || string.IsNullOrEmpty(datas.data[i].city) ? "" : datas.data[i].brgy + " " + datas.data[i].city;
                                    _storeList.Add(new Models.TindaFeature.StoreModel()
                                    {
                                        ID = datas.data[i].hsid,
                                        Operation = datas.data[i].operation_id,
                                        Open_Close = open_close,
                                        Title = datas.data[i].title,
                                        Info = datas.data[i].info,
                                        Logo = PSAProc.GetUrl(datas.data[i].avatar),
                                        //Offer = "50% off",
                                        Rating = datas.data[i].rates == "No ratings yet" || string.IsNullOrEmpty(datas.data[i].rates) ? "0.0" : datas.data[i].rates,
                                        Banner = datas.data[i].banner == "None" || string.IsNullOrEmpty(datas.data[i].banner) || datas.data[i].banner == "" ? "https://pasabuy.app/wp-content/uploads/2020/10/Food-Template.jpg" : PSAProc.GetUrl(datas.data[i].banner),
                                        Address = add
                                    });
                                }
                                IsRunning = false;
                            }
                            else
                            {
                                await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new NoStoresPage()));
                                IsRunning = false;
                            }
                        }
                        else
                        {
                            new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                            IsRunning = false;
                        }
                    });
                }
            }
            catch (Exception e)
            {
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2STR-L1PSLVM.", "OK");
                IsRunning = false;
            }
        }

        public static void LoadMore(string search, string lastid)
        {
            try
            {
                Http.TindaPress.Store.Instance.Listing("", search, "pasaplus", "", "active", lastid, async (bool success, string data) =>
                {
                    if (success)
                    {
                        Models.TindaFeature.StoreModel datas = JsonConvert.DeserializeObject<Models.TindaFeature.StoreModel>(data);
         
                        if (datas.data.Length > 0)
                        {
                            for (int i = 0; i < datas.data.Length; i++)
                            {
                                string add = string.IsNullOrEmpty(datas.data[i].brgy) || string.IsNullOrEmpty(datas.data[i].city) ? "" : datas.data[i].brgy + " " + datas.data[i].city;
                                _storeList.Add(new Models.TindaFeature.StoreModel()
                                {
                                    ID = datas.data[i].hsid,
                                    Operation = datas.data[i].operation_id,
                                    Title = datas.data[i].title,
                                    Info = datas.data[i].info,
                                    Logo = PSAProc.GetUrl(datas.data[i].avatar),
                                    //Offer = "50% off",
                                    Rating = datas.data[i].rates == "No ratings yet" || string.IsNullOrEmpty(datas.data[i].rates) ? "0.0" : datas.data[i].rates,
                                    Banner = datas.data[i].banner == "None" || string.IsNullOrEmpty(datas.data[i].banner) || datas.data[i].banner == "" ? "https://pasabuy.app/wp-content/uploads/2020/10/Food-Template.jpg" : PSAProc.GetUrl(datas.data[i].banner),
                                    Address = add
                                });
                            }
                        }
                        else
                        {
                            LoadMore("", "");
                            await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new NoStoresPage()));
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2STR-L1PSLVM.", "OK");
            }
        }

        public Command RefreshCommand { protected set; get; }

        public Command<object> ItemTappedCommand { get; set; }

        private async void NavigateToNextPage(object obj)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                var item = obj as Models.TindaFeature.StoreModel;
                StoreDetailsViewModel.store_id = item.ID;
                StoreDetailsViewModel.title = item.Title;
                StoreDetailsViewModel.info = item.Info;
                StoreDetailsViewModel.banner = item.Banner;
                StoreDetailsViewModel.address = item.Address;
                StoreDetailsViewModel.operation_id = item.Operation;
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.Marketplace.StoreDetailsPage());

                eCommerce.CheckoutPageViewModel.charges = string.Empty;
                IsRunning = false;
            }
        }

    }
}
