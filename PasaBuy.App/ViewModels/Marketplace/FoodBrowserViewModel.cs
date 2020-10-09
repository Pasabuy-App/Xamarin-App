using Newtonsoft.Json;
using PasaBuy.App.Commands;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.Views.Marketplace;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Marketplace
{
    public class FoodBrowserViewModel : BaseViewModel
    {
        private Command<object> itemTappedCommand;

        public static ObservableCollection<FoodStore> foodstorelist;

        public static ObservableCollection<FoodStore> _bestSellers;
        private static FoodBrowserViewModel instance;
        public static FoodBrowserViewModel Instance
        {
            get
            {
                if(instance == null)
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
                StoreDetailsViewModel.Loadcategory(storeId);
                StoreDetailsViewModel.Loadstoredetails(storeId);
                await App.Current.MainPage.Navigation.PushModalAsync(new StoreDetailsPage());
                await Task.Delay(200);
                IsBusy = false;
                CanNavigate = true;
            }
        }

        public ObservableCollection<FoodStore> BestSellers
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

        public FoodBrowserViewModel()
        {
            RefreshCommand = new Command<string>((key) =>
            {
                FoodBrowserPage.LastIndex = 11;
                foodstorelist.Clear();
                LoadFood("");
                LoadBestSeller();
                IsRefreshing = false;
            });
            foodstorelist = new ObservableCollection<FoodStore>();
            foodstorelist.Clear();
            //LoadFood("");

            _bestSellers = new ObservableCollection<FoodStore>();
            _bestSellers.Clear();
            LoadBestSeller();
         
        }
        public static void LoadBestSeller()
        {
            for (int i = 0; i < 4; i++)
            {
                _bestSellers.Add(new FoodStore()
                {
                    Title = "Test Store",
                    Logo = "https://pasabuy.app/wp-content/uploads/2020/10/Food-Template.jpg"
                });
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
