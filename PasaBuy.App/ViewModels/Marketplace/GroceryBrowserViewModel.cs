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
    public class GroceryBrowserViewModel : BaseViewModel
    {
        private Command<object> itemTappedCommand;

        private static ObservableCollection<Groceries> grocerystorelist;

        public ObservableCollection<Groceries> Grocerystorelist
        {
            get { return grocerystorelist; }
            set { grocerystorelist = value; this.NotifyPropertyChanged(); }
        }

        public GroceryBrowserViewModel()
        {
            Groceryloadstore();
        }

        public static void Groceryloadstore()
        {
            try
            {
                grocerystorelist = new ObservableCollection<Groceries>();
                TindaPress.Store.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", "", "", "", (bool success, string data) =>
                {
                    if (success)
                    {
                        GroceriesStoreListData datas = JsonConvert.DeserializeObject<GroceriesStoreListData>(data);
                        for (int i = 0; i < datas.data.Length; i++)
                        {
                            string ID = datas.data[i].ID;
                            string title = datas.data[i].title;
                            string short_info = datas.data[i].short_info;
                            string avatar = datas.data[i].avatar == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-store.png" : datas.data[i].avatar;
                            string banner = datas.data[i].banner == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-banner.png" : datas.data[i].banner;
                            grocerystorelist.Add(new Groceries()
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
            //var item = selectedItem.ItemData as FoodStore;

            new Alert("ok", "." +".HAHAHA", "ok");
        }

        public ObservableCollection<Groceries> NavigationList { get; set; }
    }
}
