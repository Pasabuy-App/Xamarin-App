using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.Views.Marketplace;
using PasaBuy.App.Views.Notice;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Marketplace
{
    public class StoreListViewModel : BaseViewModel
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

        public static ObservableCollection<Models.TindaFeature.StoreModel> storeList;

        public ObservableCollection<Models.TindaFeature.StoreModel> StoreList
        {
            get
            {
                return storeList;
            }
            set
            {
                storeList = value;
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
        public StoreListViewModel()
        {
            ItemTappedCommand = new Command<object>(NavigateToNextPage);
            FeaturedTappedCommand = new Command<object>(NavigateToFeatured);

            RefreshCommand = new Command<string>((key) =>
            {
                StoreListPage.LastIndex = 12;
                _bestSellers.Clear();
                storeList.Clear();
                LoadBestSeller();
                LoadRefresh(StoreListPage.catid);
                IsRefreshing = false;
            });

            _bestSellers = new ObservableCollection<Models.TindaFeature.StoreModel>();
            storeList = new ObservableCollection<Models.TindaFeature.StoreModel>();
            storeList.Clear();
            LoadBestSeller();
            LoadRefresh(StoreListPage.catid);
        }

        public void LoadBestSeller()
        {
            try
            {
                Http.TindaPress.Store.Instance.Featured("PasaMall", "active", (bool success, string data) =>
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
                                Banner = PSAProc.GetUrl(store.data[i].banner)
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2STR-L1SLVM.", "OK");
            }
        }

        public void LoadRefresh(string catid)
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    Http.TindaPress.Store.Instance.Listing("", "", "pasamall", catid, "active", "", (bool success, string data) =>
                    {
                        if (success)
                        {
                            Models.TindaFeature.StoreModel store = JsonConvert.DeserializeObject<Models.TindaFeature.StoreModel>(data);

                            for (int i = 0; i < store.data.Length; i++)
                            {
                                string add = string.IsNullOrEmpty(store.data[i].brgy) || string.IsNullOrEmpty(store.data[i].city) ? "" : store.data[i].brgy + " " + store.data[i].city;
                                storeList.Add(new Models.TindaFeature.StoreModel()
                                {
                                    ID = store.data[i].hsid,
                                    Operation = store.data[i].operation_id,
                                    Title = store.data[i].title,
                                    Info = store.data[i].info,
                                    Logo = PSAProc.GetUrl(store.data[i].avatar),
                                    Offer = "50% off",
                                    Rating = store.data[i].rates == "No ratings yet" ? "0.0" : store.data[i].rates,
                                    Banner = PSAProc.GetUrl(store.data[i].banner),
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2STR-L2SLVM.", "OK");
                IsRunning = false;
            }
        }

        public static void LoadStore(string lastid)
        {
            try
            {
                Http.TindaPress.Store.Instance.Listing("", "", "pasamall", StoreListPage.catid, "active", lastid, (bool success, string data) =>
                {
                    if (success)
                    {
                        Models.TindaFeature.StoreModel store = JsonConvert.DeserializeObject<Models.TindaFeature.StoreModel>(data);

                        for (int i = 0; i < store.data.Length; i++)
                        {
                            string add = string.IsNullOrEmpty(store.data[i].brgy) || string.IsNullOrEmpty(store.data[i].city) ? "" : store.data[i].brgy + " " + store.data[i].city;
                            storeList.Add(new Models.TindaFeature.StoreModel()
                            {
                                ID = store.data[i].hsid,
                                Operation = store.data[i].operation_id,
                                Title = store.data[i].title,
                                Info = store.data[i].info,
                                Logo = PSAProc.GetUrl(store.data[i].avatar),
                                Offer = "50% off",
                                Rating = store.data[i].rates == "No ratings yet" ? "0.0" : store.data[i].rates,
                                Banner = PSAProc.GetUrl(store.data[i].banner),
                                Address = add
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2STR-L3SLVM.", "OK");
            }
        }

        public static void SearchStore(string search)
        {
            try
            {
                Http.TindaPress.Store.Instance.Listing("", search, "pasamall", StoreListPage.catid, "active", "", (bool success, string data) =>
                {
                    if (success)
                    {
                        Models.TindaFeature.StoreModel store = JsonConvert.DeserializeObject<Models.TindaFeature.StoreModel>(data);
                        if (store.data.Length > 0)
                        {
                            storeList.Clear();
                            for (int i = 0; i < store.data.Length; i++)
                            {
                                string add = string.IsNullOrEmpty(store.data[i].brgy) || string.IsNullOrEmpty(store.data[i].city) ? "" : store.data[i].brgy + " " + store.data[i].city;
                                storeList.Add(new Models.TindaFeature.StoreModel()
                                {
                                    ID = store.data[i].hsid,
                                    Operation = store.data[i].operation_id,
                                    Title = store.data[i].title,
                                    Info = store.data[i].info,
                                    Logo = PSAProc.GetUrl(store.data[i].avatar),
                                    Offer = "50% off",
                                    Rating = store.data[i].rates == "No ratings yet" ? "0.0" : store.data[i].rates,
                                    Banner = PSAProc.GetUrl(store.data[i].banner),
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2STR-L4SLVM.", "OK");
            }
        }

        #endregion

        #region Properties
        public ICommand RefreshCommand { protected set; get; }

        public Command<object> FeaturedTappedCommand { get; set; }

        public Command<object> ItemTappedCommand { get; set; }

        #endregion

        #region Method

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

        #endregion
    }
}
