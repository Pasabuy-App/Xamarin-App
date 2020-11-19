using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.Views.Marketplace;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Marketplace
{
    public class PartnerBrowserViewModel : BaseViewModel
    {
        #region Fields

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

        public static ObservableCollection<Categories> itemCategories;

        public ObservableCollection<Categories> ItemCategories
        {
            get
            {
                return itemCategories;
            }
            set
            {
                itemCategories = value;
                this.NotifyPropertyChanged();
            }
        }
        public static ObservableCollection<Models.TindaFeature.StoreModel> partnerStoresRotator;

        public ObservableCollection<Models.TindaFeature.StoreModel> PartnerStoresRotator
        {
            get
            {
                return partnerStoresRotator;
            }
            set
            {
                partnerStoresRotator = value;
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

        public PartnerBrowserViewModel()
        {
            ItemTappedCommand = new Command<object>(NavigateToNextPage);
            storeList = new ObservableCollection<Models.TindaFeature.StoreModel>();
            storeList.Clear();
            partnerStoresRotator = new ObservableCollection<Models.TindaFeature.StoreModel>();
            partnerStoresRotator.Clear();
            _bestSellers = new ObservableCollection<Models.TindaFeature.StoreModel>();
            _bestSellers.Clear();
            RefreshCommand = new Command<string>((key) =>
            {
                LoadPartner();
                IsRefreshing = false;
            });
            LoadPartner();
            LoadBestSeller();
        }
        public void LoadPartner()
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    storeList.Clear();
                    Http.TindaPress.Store.Instance.Listing("", "", "robinson", "", "", "active", (bool success, string data) =>
                    {
                        if (success)
                        {
                            Models.TindaFeature.StoreModel store = JsonConvert.DeserializeObject<Models.TindaFeature.StoreModel>(data);

                            if (store.data.Length > 0)
                            {
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
                                        Rating = store.data[i].rates == "No ratings yet" || string.IsNullOrEmpty(store.data[i].rates) ? "0.0" : store.data[i].rates,
                                        Address = add
                                    });
                                }
                                IsRunning = false;
                            }
                            else
                            {
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2STR-L1PBVM.", "OK");
                IsRunning = false;
            }
        }

        public static void SearchStore(string search)
        {
            try
            {
                storeList.Clear();
                Http.TindaPress.Store.Instance.Listing("", search, "robinson", "", "" , "active", (bool success, string data) =>
                {
                    if (success)
                    {
                        Models.TindaFeature.StoreModel store = JsonConvert.DeserializeObject<Models.TindaFeature.StoreModel>(data);
                        if (store.data.Length > 0)
                        {
                            storeList.Clear();
                            for (int i = 0; i < store.data.Length; i++)
                            {
                                string add = string.IsNullOrEmpty(store.data[i].brgy) || string.IsNullOrEmpty(store.data[i].city) ? "" : store.data[i].brgy + " " + store.data[i].city ;

                                storeList.Add(new Models.TindaFeature.StoreModel()
                                {
                                    ID = store.data[i].hsid,
                                    Operation = store.data[i].operation_id,
                                    Title = store.data[i].title,
                                    Info = store.data[i].info,
                                    Logo = PSAProc.GetUrl(store.data[i].avatar),
                                    Offer = "50% off",
                                    Rating = store.data[i].rates == "No ratings yet" || string.IsNullOrEmpty(store.data[i].rates) ? "0.0" : store.data[i].rates,
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2STR-L2PBVM.", "OK");
            }
        }

        public static void LoadCategory()
        {
            try
            {
                /*TindaPress.Category.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "robinson", "", "1", "1", (bool success, string data) =>
                {
                    if (success)
                    {
                        StoreListData store = JsonConvert.DeserializeObject<StoreListData>(data);

                        for (int i = 0; i < store.data.Length; i++)
                        {
                            itemCategories.Add(new Categories()
                            {
                                Id = store.data[i].ID,
                                Title = store.data[i].title,
                                Avatar = store.data[i].avatar == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-product.png" : PSAProc.GetUrl(store.data[i].avatar), //PSAProc.GetUrl(store.data[i].avatar),
                                Info = "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-product.png"
                            });
                        }
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });*/

            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }

        public void LoadBestSeller()
        {
            try
            {
                Http.TindaPress.Store.Instance.Featured("Robinson", "active", (bool success, string data) =>
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
                new Controllers.Notice.Alert("Something went Wrong", "Please contact administrator. Error Code: TPV2STR-L1FBVM.", "OK");
            }
        }

        #endregion

        #region Properties

        public ICommand RefreshCommand { protected set; get; }

        #endregion

        #region Method

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
                IsRunning = false;
            }
        }

        #endregion
    }
}
