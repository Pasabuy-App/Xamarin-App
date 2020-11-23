using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.Views.Marketplace;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Marketplace
{
    public class GroceryBrowserViewModel : BaseViewModel
    {
        #region Fields

        public static ObservableCollection<Models.TindaFeature.StoreModel> _bestSellers;

        public ObservableCollection<Models.TindaFeature.StoreModel> BestSellers
        {
            get
            {
                return _bestSellers;
            }
            set
            {
                _bestSellers = value;
                this.NotifyPropertyChanged();
            }
        }

        public static ObservableCollection<Models.TindaFeature.StoreModel> grocerystorelist;
        public ObservableCollection<Models.TindaFeature.StoreModel> Grocerystorelist
        {
            get
            {
                return grocerystorelist;
            }
            set
            {
                grocerystorelist = value;
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

        #endregion

        #region Constructor
        public GroceryBrowserViewModel()
        {
            ItemTappedCommand = new Command<object>(NavigateToNextPage);
            FeaturedTappedCommand = new Command<object>(NavigateToFeatured);
            _bestSellers = new ObservableCollection<Models.TindaFeature.StoreModel>();
            grocerystorelist = new ObservableCollection<Models.TindaFeature.StoreModel>();
            grocerystorelist.Clear();
            RefreshCommand = new Command<string>((key) =>
            {
                GroceryBrowserPage.LastIndex = 12;
                _bestSellers.Clear();
                grocerystorelist.Clear();
                LoadBestSeller();
                LoadGrocery();
                IsRefreshing = false;
            });
            LoadBestSeller();
            LoadGrocery();

        }

        public void LoadBestSeller()
        {
            try
            {
                Http.TindaPress.Store.Instance.Featured("Grocery", "active", (bool success, string data) =>
                {
                    if (success)
                    {
                        Models.TindaFeature.StoreModel store = JsonConvert.DeserializeObject<Models.TindaFeature.StoreModel>(data);
                        if (store.data.Length > 0)
                        {
                            HeaderSize = "280";
                        }
                        else
                        {
                            HeaderSize = "0";
                        }
                        for (int i = 0; i < store.data.Length; i++)
                        {
                            _bestSellers.Add(new Models.TindaFeature.StoreModel()
                            {
                                ID = store.data[i].hsid,
                                Operation = store.data[i].operation_id,
                                Address = store.data[i].street + " " + store.data[i].brgy + " " + store.data[i].city + ", " + store.data[i].province,
                                Title = store.data[i].title,
                                Info = store.data[i].info,
                                Logo = PSAProc.GetUrl(store.data[i].avatar),
                                Banner = store.data[i].banner == "None" || string.IsNullOrEmpty(store.data[i].banner) || store.data[i].banner == "" ? "https://pasabuy.app/wp-content/uploads/2020/10/Grocery-Template.jpg" : PSAProc.GetUrl(store.data[i].banner)
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2STR-L1GBVM.", "OK");
            }
        }

        public void LoadGrocery()
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    Http.TindaPress.Store.Instance.Listing("", "", "market", "", "active", "", (bool success, string data) =>
                    {
                        if (success)
                        {
                            Models.TindaFeature.StoreModel store = JsonConvert.DeserializeObject<Models.TindaFeature.StoreModel>(data);

                            for (int i = 0; i < store.data.Length; i++)
                            {
                                string open_close = string.IsNullOrEmpty(store.data[i].operation_id) ? "Closed" : "Open Now";
                                string add = string.IsNullOrEmpty(store.data[i].street) || string.IsNullOrEmpty(store.data[i].brgy) || string.IsNullOrEmpty(store.data[i].city) || string.IsNullOrEmpty(store.data[i].province) ? "" : store.data[i].street + " " + store.data[i].brgy + " " + store.data[i].city + ", " + store.data[i].province;

                                grocerystorelist.Add(new Models.TindaFeature.StoreModel()
                                {
                                    ID = store.data[i].hsid,
                                    Operation = store.data[i].operation_id,
                                    Open_Close = open_close,
                                    Title = store.data[i].title,
                                    Info = store.data[i].info,
                                    Banner = store.data[i].banner == "None" || string.IsNullOrEmpty(store.data[i].banner) || store.data[i].banner == "" ? "https://pasabuy.app/wp-content/uploads/2020/10/Grocery-Template.jpg" : PSAProc.GetUrl(store.data[i].banner),
                                    Address = add
                                });
                            }
                            IsRunning = false;
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2STR-L2GBVM.", "OK");
                IsRunning = false;
            }
        }

        public static void SearchStore(string search, string lastid)
        {
            try
            {
                Http.TindaPress.Store.Instance.Listing("", search, "market", "", "active", lastid, (bool success, string data) =>
                {
                    if (success)
                    {
                        Models.TindaFeature.StoreModel store = JsonConvert.DeserializeObject<Models.TindaFeature.StoreModel>(data);
                        if (store.data.Length > 0)
                        {
                            for (int i = 0; i < store.data.Length; i++)
                            {
                                string open_close = string.IsNullOrEmpty(store.data[i].operation_id) ? "Closed" : "Open Now";
                                string add = string.IsNullOrEmpty(store.data[i].street) || string.IsNullOrEmpty(store.data[i].brgy) || string.IsNullOrEmpty(store.data[i].city) || string.IsNullOrEmpty(store.data[i].province) ? "" : store.data[i].street + " " + store.data[i].brgy + " " + store.data[i].city + ", " + store.data[i].province;

                                grocerystorelist.Add(new Models.TindaFeature.StoreModel()
                                {
                                    ID = store.data[i].hsid,
                                    Operation = store.data[i].operation_id,
                                    Open_Close = open_close,
                                    Title = store.data[i].title,
                                    Info = store.data[i].info,
                                    Banner = store.data[i].banner == "None" || string.IsNullOrEmpty(store.data[i].banner) || store.data[i].banner == "" ? "https://pasabuy.app/wp-content/uploads/2020/10/Grocery-Template.jpg" : PSAProc.GetUrl(store.data[i].banner),
                                    Address = add
                                });
                            }
                        }
                        else
                        {
                            new Alert("Notice to User", "No store found.", "Try Again");
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2STR-L3GBVM.", "OK");
            }
        }

        #endregion

        #region Properties

        public Command RefreshCommand { protected set; get; }

        public Command<object> ItemTappedCommand { get; set; }

        public Command<object> FeaturedTappedCommand { get; set; }

        #endregion

        #region Method

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
                await App.Current.MainPage.Navigation.PushModalAsync(new StoreDetailsPage());
                IsRunning = false;
            }
        }

        private async void NavigateToFeatured(object obj)
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
                await App.Current.MainPage.Navigation.PushModalAsync(new StoreDetailsPage());
                IsRunning = false;
            }
        }

        #endregion

    }
}
