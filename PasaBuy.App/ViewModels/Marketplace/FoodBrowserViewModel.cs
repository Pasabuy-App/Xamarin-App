using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.Views.Marketplace;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Marketplace
{
    public class FoodBrowserViewModel : BaseViewModel
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

        public static ObservableCollection<Models.TindaFeature.StoreModel> foodstorelist;
        public ObservableCollection<Models.TindaFeature.StoreModel> FoodStorelist
        {
            get
            {
                return foodstorelist;
            }
            set
            {
                foodstorelist = value;
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
        public static bool isLoad;

        #endregion

        #region Constructor
        public FoodBrowserViewModel()
        {
            foodstorelist = new ObservableCollection<Models.TindaFeature.StoreModel>();
            _bestSellers = new ObservableCollection<Models.TindaFeature.StoreModel>();

            RefreshCommand = new Command<string>((key) =>
            {
                FoodBrowserPage.LastIndex = 12;
                _bestSellers.Clear();
                foodstorelist.Clear();
                LoadBestSeller();
                LoadFood();
                IsRefreshing = false;
            });

            isLoad = false;

            LoadBestSeller();
            LoadFood();

            SelectStoreCommand = new Command<object>(DetailsClicked);
            ItemTappedCommand = new Command<object>(NavigateToNextPage);
        }

        public void LoadBestSeller()
        {
            try
            {
                Http.TindaPress.Store.Instance.Featured("Food/Drinks", "active", (bool success, string data) =>
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
                            string add = string.IsNullOrEmpty(store.data[i].brgy) || string.IsNullOrEmpty(store.data[i].city) ? "" : store.data[i].brgy + " " + store.data[i].city;
                            _bestSellers.Add(new Models.TindaFeature.StoreModel()
                            {
                                ID = store.data[i].hsid,
                                Operation = store.data[i].operation_id,
                                Address = add,
                                Title = store.data[i].title,
                                Info = store.data[i].info,
                                Logo = PSAProc.GetUrl(store.data[i].avatar),
                                Banner = store.data[i].banner == "None" || string.IsNullOrEmpty(store.data[i].banner) || store.data[i].banner == "" ? "https://pasabuy.app/wp-content/uploads/2020/10/Food-Template.jpg" : PSAProc.GetUrl(store.data[i].banner)
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2STR-L1FBVM.", "OK");
            }
        }

        public void LoadFood()
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    Http.TindaPress.Store.Instance.Listing("", "", "food/drinks", "", "active", "", (bool success, string data) =>
                    {
                        if (success)
                        {
                            Models.TindaFeature.StoreModel store = JsonConvert.DeserializeObject<Models.TindaFeature.StoreModel>(data);

                            CultureInfo provider = new CultureInfo("fr-FR");
                            for (int i = 0; i < store.data.Length; i++)
                            {
                                string open_time = string.IsNullOrEmpty(store.data[i].opening) ? "00:00:00" : store.data[i].opening;
                                string close_time = string.IsNullOrEmpty(store.data[i].closing) ? "00:00:00" : store.data[i].closing;
                                DateTime open = DateTime.ParseExact(open_time, "HH:mm:ss", provider);
                                DateTime close = DateTime.ParseExact(close_time, "HH:mm:ss", provider);
                                string open_close = string.IsNullOrEmpty(store.data[i].operation_id) ? "Closed" : open.ToString("HH:mm tt") + " - " + close.ToString("HH:mm tt");
                                string add = string.IsNullOrEmpty(store.data[i].brgy) || string.IsNullOrEmpty(store.data[i].city) ? "" : store.data[i].brgy + " " + store.data[i].city;
                                foodstorelist.Add(new Models.TindaFeature.StoreModel()
                                {
                                    ID = store.data[i].hsid,
                                    Operation = store.data[i].operation_id,
                                    Open_Close = open_close,
                                    Address = add,
                                    Title = store.data[i].title,
                                    Info = store.data[i].info,
                                    Logo = PSAProc.GetUrl(store.data[i].avatar),
                                    Rating = store.data[i].rates == "No ratings yet" || string.IsNullOrEmpty(store.data[i].rates) ? "0.0" : store.data[i].rates,
                                    Banner = store.data[i].banner == "None" || string.IsNullOrEmpty(store.data[i].banner) || store.data[i].banner == "" ? "https://pasabuy.app/wp-content/uploads/2020/10/Food-Template.jpg" : PSAProc.GetUrl(store.data[i].banner)
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2STR-L2FBVM.", "OK");
                IsRunning = false;
            }
        }

        public static void SearchStore(string search, string lastid)
        {
            try
            {
                Http.TindaPress.Store.Instance.Listing("", search, "food/drinks", "", "active", lastid, async (bool success, string data) =>
                {
                    if (success)
                    {
                        CultureInfo provider = new CultureInfo("fr-FR");
                        Models.TindaFeature.StoreModel store = JsonConvert.DeserializeObject<Models.TindaFeature.StoreModel>(data);
                        if (store.data.Length > 0)
                        {
                            for (int i = 0; i < store.data.Length; i++)
                            {
                                string open_time = string.IsNullOrEmpty(store.data[i].opening) ? "00:00:00" : store.data[i].opening;
                                string close_time = string.IsNullOrEmpty(store.data[i].closing) ? "00:00:00" : store.data[i].closing;
                                DateTime open = DateTime.ParseExact(open_time, "HH:mm:ss", provider);
                                DateTime close = DateTime.ParseExact(close_time, "HH:mm:ss", provider);
                                string open_close = string.IsNullOrEmpty(store.data[i].operation_id) ? "Closed" : open.ToString("HH:mm tt") + " - " + close.ToString("HH:mm tt");
                                string add = string.IsNullOrEmpty(store.data[i].brgy) || string.IsNullOrEmpty(store.data[i].city) ? "" : store.data[i].brgy + " " + store.data[i].city;
                                foodstorelist.Add(new Models.TindaFeature.StoreModel()
                                {
                                    ID = store.data[i].hsid,
                                    Operation = store.data[i].operation_id,
                                    Open_Close = open_close,
                                    Address = add,
                                    Title = store.data[i].title,
                                    Info = store.data[i].info,
                                    Logo = PSAProc.GetUrl(store.data[i].avatar),
                                    Rating = store.data[i].rates == "No ratings yet" || string.IsNullOrEmpty(store.data[i].rates) ? "0.0" : store.data[i].rates,
                                    Banner = store.data[i].banner == "None" || string.IsNullOrEmpty(store.data[i].banner) || store.data[i].banner == "" ? "https://pasabuy.app/wp-content/uploads/2020/10/Food-Template.jpg" : PSAProc.GetUrl(store.data[i].banner)
                                });
                            }
                        }
                        else
                        {
                            SearchStore("", "");
                            await (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new Views.Notice.NoStoresPage()));
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2STR-L3FBVM.", "OK");
            }
        }

        #endregion

        #region Properties
        public Command<object> SelectStoreCommand { get; set; }
        public ICommand RefreshCommand { protected set; get; }

        #endregion

        #region Method

        private async void DetailsClicked(object obj)
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

                eCommerce.CartPageViewModel.action = string.Empty;
                eCommerce.CartPageViewModel.amount = string.Empty;
                eCommerce.CheckoutPageViewModel.charges = string.Empty;
                IsRunning = false;
            }
        }
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
                await App.Current.MainPage.Navigation.PushModalAsync(new StoreDetailsPage());

                eCommerce.CartPageViewModel.action = string.Empty;
                eCommerce.CartPageViewModel.amount = string.Empty;
                eCommerce.CheckoutPageViewModel.charges = string.Empty;
                IsRunning = false;
            }
        }

        #endregion
    }
}
