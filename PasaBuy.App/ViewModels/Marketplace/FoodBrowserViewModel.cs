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
using System.Windows.Input;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Marketplace
{
    public class FoodBrowserViewModel : BaseViewModel
    {
        private Command<object> itemTappedCommand;

        public static ObservableCollection<FoodStore> foodstorelist;

        public static ObservableCollection<FoodStore> _bestSellers;

        public ICommand SelectStoreCommand
        {
            get
            {
                return new Command<string>((x) => LoadDetails(x));
            }
        }
        public async void LoadDetails(string storeId)
        {
            IsBusy = true;
            CanNavigate = false;
            StoreDetailsViewModel.store_id = storeId;
            StoreDetailsViewModel.Loadcategory(storeId);
            StoreDetailsViewModel.Loadstoredetails(storeId);
            await App.Current.MainPage.Navigation.PushModalAsync(new StoreDetailsPage());
            IsBusy = false;
            CanNavigate = true;
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
            get { return foodstorelist; }
            set { foodstorelist = value; this.NotifyPropertyChanged(); }
        }

        public FoodBrowserViewModel()
        {
            foodstorelist = new ObservableCollection<FoodStore>();
            foodstorelist.Clear();
            LoadFood("");

            _bestSellers = new ObservableCollection<FoodStore>();

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
                            string ID = datas.data[i].ID;
                            string title = datas.data[i].title;
                            string short_info = datas.data[i].short_info;
                            string avatar = datas.data[i].avatar == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-store.png" : datas.data[i].avatar;
                            string banner = datas.data[i].banner == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-banner.png" : datas.data[i].banner;
                            foodstorelist.Add(new FoodStore()
                            {
                                Id = ID,
                                Title = title,
                                Description = short_info,
                                Logo = PSAProc.GetUrl(avatar),
                                Offer = "50% off",
                                ItemRating = "4.5",
                                Banner = PSAProc.GetUrl(banner)
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
