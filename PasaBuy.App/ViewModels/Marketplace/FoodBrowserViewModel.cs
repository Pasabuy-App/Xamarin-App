using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.Views.Marketplace;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Marketplace
{
    public class FoodBrowserViewModel : BaseViewModel
    {
        private Command<object> itemTappedCommand;

        public static ObservableCollection<FoodStore> foodstorelist;

        public static ObservableCollection<FeaturedStoreModel> _bestSellers;
        private static FoodBrowserViewModel instance;
        public static FoodBrowserViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FoodBrowserViewModel();
                }
                return instance;
            }

        }

        public ICommand SelectStoreCommand
        {
            get
            {
                return new Command<string>((x) => LoadDetails(x));
            }
        }

        public async void LoadDetails(string storeId)
        {

            if (!IsBusy)
            {
                IsBusy = true;
                CanNavigate = false;
                StoreDetailsViewModel.store_id = storeId;
                //StoreDetailsViewModel.Loadcategory(storeId);
                await App.Current.MainPage.Navigation.PushModalAsync(new StoreDetailsPage());
                await Task.Delay(300);
                IsBusy = false;
                CanNavigate = true;
            }
        }

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
        public ObservableCollection<FoodStore> FoodStorelist
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
        public static bool isLoad;

        public ICommand RefreshCommand { protected set; get; }

        public FoodBrowserViewModel()
        {
            RefreshCommand = new Command<string>((key) =>
           {
               RefreshData();
               IsRefreshing = false;
           });
            isLoad = false;
            foodstorelist = new ObservableCollection<FoodStore>();
            _bestSellers = new ObservableCollection<FeaturedStoreModel>();

        }
        public static async void RefreshData()
        {
            if (!isLoad)
            {
                isLoad = true;
                FoodBrowserPage.LastIndex = 11;
                _bestSellers.Clear();
                foodstorelist.Clear();
                LoadBestSeller();
                LoadFood("");
                await Task.Delay(1000);
                isLoad = false;
            }
        }

        public static void LoadBestSeller()
        {
            try
            {
                TindaPress.Store.Instance.FeaturedList(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, (bool success, string data) =>
                {
                    if (success)
                    {
                        FeaturedStoreModel datas = JsonConvert.DeserializeObject<FeaturedStoreModel>(data);
                        for (int i = 0; i < datas.data.Length; i++)
                        {
                            _bestSellers.Add(new FeaturedStoreModel()
                            {
                                Id = datas.data[i].ID,
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

        public static void LoadFood(string lastid)
        {
            try
            {
                TindaPress.Store.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "1", "", "1", lastid, (bool success, string data) =>
                {
                    if (success)
                    {
                        FoodStoreListData datas = JsonConvert.DeserializeObject<FoodStoreListData>(data);
                        for (int i = 0; i < datas.data.Length; i++)
                        {
                            foodstorelist.Add(new FoodStore()
                            {
                                Id = datas.data[i].ID,
                                Title = datas.data[i].title,
                                Description = datas.data[i].short_info,
                                Logo = datas.data[i].avatar == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-store.png" : PSAProc.GetUrl(datas.data[i].avatar),
                                Offer = "50% off",
                                ItemRating = "4.5",
                                Banner = datas.data[i].banner == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-banner.png" : PSAProc.GetUrl(datas.data[i].banner)
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

        public Command<object> ItemTappedCommand
        {
            get
            {
                return this.itemTappedCommand ?? (this.itemTappedCommand = new Command<object>(this.NavigateToNextPage));
            }
        }

        private void NavigateToNextPage(object selectedItem)
        {

        }

        //public ObservableCollection<FoodStore> NavigationList { get; set; }
    }
}
