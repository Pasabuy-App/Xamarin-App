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
        public static ObservableCollection<Store> storeList;

        public static ObservableCollection<FeaturedStoreModel> _bestSellers;

        public ObservableCollection<FeaturedStoreModel> BestSellers
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

        public ObservableCollection<Store> StoreList
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
        public ICommand RefreshCommand { protected set; get; }
        public StoreListViewModel()
        {
            ItemTappedCommand = new Command<object>(NavigateToNextPage);
            FeaturedTappedCommand = new Command<object>(NavigateToFeatured);
            _bestSellers = new ObservableCollection<FeaturedStoreModel>();

            RefreshCommand = new Command<string>((key) =>
            {
                storeList.Clear();
                LoadRefresh(StoreListPage.catid, "");
                IsRefreshing = false;
            });
            storeList = new ObservableCollection<Store>();
            storeList.Clear();
            LoadBestSeller();
        }

        public void LoadBestSeller()
        {
            try
            {
                Http.TindaFeature.Instance.FeaturedList("active", (bool success, string data) =>
                {
                    if (success)
                    {
                        FeaturedStoreModel datas = JsonConvert.DeserializeObject<FeaturedStoreModel>(data);
                        for (int i = 0; i < datas.data.Length; i++)
                        {
                            _bestSellers.Add(new FeaturedStoreModel()
                            {
                                ID = datas.data[i].stid,
                                Title = datas.data[i].title,
                                Logo = datas.data[i].banner == "None" ? "https://pasabuy.app/wp-content/uploads/2020/10/Food-Template.jpg" : PSAProc.GetUrl(datas.data[i].banner)
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

        public void LoadRefresh(string catid, string lastid)
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    Http.TindaFeature.Instance.StoreTypeList("pasamall", "active", catid, async (bool success, string data) =>
                    {
                        if (success)
                        {
                            StoreListData datas = JsonConvert.DeserializeObject<StoreListData>(data);

                            if (datas.data.Length == 0)
                            {
                                (App.Current.MainPage).Navigation.PushModalAsync(new NavigationPage(new NoStoresPage()));
                            }
                            else
                            {
                                for (int i = 0; i < datas.data.Length; i++)
                                {
                                    storeList.Add(new Store()
                                    {
                                        Id = datas.data[i].ID,
                                        Title = datas.data[i].title,
                                        Description = datas.data[i].short_info,
                                        Logo = datas.data[i].avatar == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-store.png" : PSAProc.GetUrl(datas.data[i].avatar),
                                        Offer = "50% off",
                                        StoreRating = datas.data[i].rates,
                                        Banner = datas.data[i].banner == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-banner.png" : PSAProc.GetUrl(datas.data[i].banner),
                                        Street = datas.data[i].brgy + ", " + datas.data[i].city
                                    });
                                }
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
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
                IsRunning = false;
            }
        }
      
        public static void LoadStore(string catid, string lastid)
        {
            try
            {
                Http.TindaFeature.Instance.StoreTypeList("pasamall", "active", catid, async (bool success, string data) =>
                {
                    if (success)
                    {
                        StoreListData datas = JsonConvert.DeserializeObject<StoreListData>(data);

                        if (datas.data.Length > 0)
                        {
                            for (int i = 0; i < datas.data.Length; i++)
                            {
                                storeList.Add(new Store()
                                {
                                    Id = datas.data[i].hsid,
                                    Title = datas.data[i].title,
                                    Description = datas.data[i].short_info,
                                    Logo = datas.data[i].avatar == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-store.png" : PSAProc.GetUrl(datas.data[i].avatar),
                                    Offer = "50% off",
                                    ItemRating = "4.5",
                                    Banner = datas.data[i].banner == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-banner.png" : PSAProc.GetUrl(datas.data[i].banner),
                                    Street = datas.data[i].brgy + " " + datas.data[i].city
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

        public static void SearchStore(string search)
        {
            try
            {
                Http.TindaFeature.Instance.Store_Search(search, "storehub", (bool success, string data) =>
                {
                    if (success)
                    {
                        GroceriesStoreListData datas = JsonConvert.DeserializeObject<GroceriesStoreListData>(data);
                        if (datas.data.Length > 0)
                        {
                            storeList.Clear();
                            for (int i = 0; i < datas.data.Length; i++)
                            {
                                storeList.Add(new Store()
                                {
                                    Id = datas.data[i].hsid,
                                    Title = datas.data[i].title,
                                    Description = datas.data[i].short_info,
                                    Logo = datas.data[i].avatar == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-store.png" : PSAProc.GetUrl(datas.data[i].avatar),
                                    Offer = "50% off",
                                    ItemRating = "4.5",
                                    Banner = datas.data[i].banner == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-banner.png" : PSAProc.GetUrl(datas.data[i].banner),
                                    Street = datas.data[i].brgy + " " + datas.data[i].city
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
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
        }

        public Command<object> FeaturedTappedCommand { get; set; }

        public Command<object> ItemTappedCommand { get; set; }

        private async void NavigateToFeatured(object obj)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                var product = obj as FeaturedStoreModel;
                StoreDetailsViewModel.store_id = product.ID;
                await App.Current.MainPage.Navigation.PushModalAsync(new StoreDetailsPage());
                IsRunning = false;
            }
        }

   
        private async void NavigateToNextPage(object obj)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                var store = obj as Store;
                StoreDetailsViewModel.store_id = store.Id;
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.Marketplace.StoreDetailsPage());
                IsRunning = false;
            }
        }
    }
}
