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
        public ICommand RefreshCommand { protected set; get; }

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

        public FoodBrowserViewModel()
        {
            foodstorelist = new ObservableCollection<FoodStore>();
            _bestSellers = new ObservableCollection<FeaturedStoreModel>();
            RefreshCommand = new Command<string>((key) =>
            {
                FoodBrowserPage.LastIndex = 11;
                _bestSellers.Clear();
                foodstorelist.Clear();
                LoadBestSeller();
                LoadFood();
                IsRefreshing = false;
            });
            isLoad = false;

            LoadBestSeller();
            LoadFood();
        }

        public void LoadBestSeller()
        {
            try
            {
                Http.TindaFeature.Instance.FeaturedList((bool success, string data) =>
                {
                    if (success)
                    {
                        FeaturedStoreModel datas = JsonConvert.DeserializeObject<FeaturedStoreModel>(data);
                        for (int i = 0; i < datas.data.Length; i++)
                        {
                            _bestSellers.Add(new FeaturedStoreModel()
                            {
                                Id = datas.data[i].stid,
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

        public void LoadFood()
        {
            try
            {
                if (!IsRunning)
                {
                    IsRunning = true;
                    Http.TindaFeature.Instance.FoodList("food/drinks", (bool success, string data) =>
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
                                    Banner = datas.data[i].banner == "None" ? "https://pasabuy.app/wp-content/uploads/2020/10/Food-Template.jpg" : PSAProc.GetUrl(datas.data[i].banner)
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
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
                IsRunning = false;
            }
        }

        public static void SearchStore(string search)
        {
            try
            {
                Http.TindaFeature.Instance.Store_Search(search, "food/drink", (bool success, string data) =>
                {
                    if (success)
                    {
                        FoodStoreListData datas = JsonConvert.DeserializeObject<FoodStoreListData>(data);
                        if (datas.data.Length > 0)
                        {
                            foodstorelist.Clear();
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
                                    Banner = datas.data[i].banner == "None" ? "https://pasabuy.app/wp-content/uploads/2020/10/Food-Template.jpg" : PSAProc.GetUrl(datas.data[i].banner)
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

        public Command<object> ItemTappedCommand
        {
            get
            {
                return this.itemTappedCommand ?? (this.itemTappedCommand = new Command<object>(this.NavigateToNextPage));
            }
        }

        private async void NavigateToNextPage(object selectedItem)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                CanNavigate = false;
                StoreDetailsViewModel.store_id = ((selectedItem as Syncfusion.ListView.XForms.ItemTappedEventArgs)?.ItemData as FeaturedStoreModel).Id;
                await Task.Delay(300);
                await App.Current.MainPage.Navigation.PushModalAsync(new StoreDetailsPage());
                await Task.Delay(300);
                IsRunning = false;
                CanNavigate = true;
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
            if (!IsRunning)
            {
                IsRunning = true;
                CanNavigate = false;
                StoreDetailsViewModel.store_id = storeId;
                await Task.Delay(300);
                await App.Current.MainPage.Navigation.PushModalAsync(new StoreDetailsPage());
                await Task.Delay(300);
                IsRunning = false;
                CanNavigate = true;
            }
        }
    }
}
