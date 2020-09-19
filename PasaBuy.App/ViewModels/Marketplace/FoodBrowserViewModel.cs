using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace PasaBuy.App.ViewModels.Marketplace
{
    public class FoodBrowserViewModel : BaseViewModel
    {
        private Command<object> itemTappedCommand;

        private static ObservableCollection<FoodStore> foodstorelist;

        public ObservableCollection<FoodStore> FoodStorelist
        {
            get { return foodstorelist; }
            set { foodstorelist = value; this.NotifyPropertyChanged(); }
        }

        public FoodBrowserViewModel()
        {
            loadstore();
        }

        public static void loadstore()
        {
           
            try
            {
                foodstorelist = new ObservableCollection<FoodStore>();
                TindaPress.Store.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "1", "", "", "", (bool success, string data) =>
                {
                    if (success)
                    {
                        FoodStoreListData datas = JsonConvert.DeserializeObject<FoodStoreListData>(data);
                        for (int i = 0; i < datas.data.Length; i++)
                        {
                            string ID = datas.data[i].ID;
                            Console.WriteLine(ID + "HAHA");
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
                });
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator." + ' ' + e, "OK");
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

        public ObservableCollection<FoodStore> NavigationList { get; set; }
    }
}
