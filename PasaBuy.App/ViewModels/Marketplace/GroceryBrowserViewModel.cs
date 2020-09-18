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

        private static ObservableCollection<Store> storelist;

        public ObservableCollection<Store> Storelist
        {
            get { return storelist; }
            set { storelist = value; this.NotifyPropertyChanged(); }
        }

        public GroceryBrowserViewModel()
        {
            loadstore();
        }

        public static void loadstore()
        {
            try
            {
                storelist = new ObservableCollection<Store>();
                TindaPress.Store.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", "", "", "", (bool success, string data) =>
                {
                    if (success)
                    {
                        StoreListData datas = JsonConvert.DeserializeObject<StoreListData>(data);
                        for (int i = 0; i < datas.data.Length; i++)
                        {
                            string title = datas.data[i].title;
                            string short_info = datas.data[i].short_info;
                            string avatar = datas.data[i].avatar == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-store.png" : datas.data[i].avatar;
                            string banner = datas.data[i].banner == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-banner.png" : datas.data[i].banner;
                            storelist.Add(new Store()
                            {
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
            /*
                        storelist.Add(new Store() { Title = "1", Description = "4" });
                        storelist.Add(new Store() { Title = "2", Description = "5" });
                        storelist.Add(new Store() { Title = "3", Description = "6" });
                        storelist.Add(new Store() { Title = "4", Description = "7" });*/
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

        public ObservableCollection<Store> NavigationList { get; set; }
    }
}
