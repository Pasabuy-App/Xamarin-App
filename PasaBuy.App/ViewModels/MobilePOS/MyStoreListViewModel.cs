using Newtonsoft.Json;
using PasaBuy.App.Controllers.Notice;
using PasaBuy.App.Local;
using PasaBuy.App.Models.Marketplace;
using System;
using System.Collections.ObjectModel;

namespace PasaBuy.App.ViewModels.MobilePOS
{
    public class MyStoreListViewModel : BaseViewModel
    {
        public ObservableCollection<Store> myStores;

        public ObservableCollection<Store> MyStores
        {
            get
            {
                return myStores;
            }
            set
            {
                myStores = value;
                this.NotifyPropertyChanged();
            }
        }

        public MyStoreListViewModel()
        {
            myStores = new ObservableCollection<Store>();
            LoadStore();
        }
        public void LoadStore()
        {
            try
            {
                TindaPress.Personnel.Instance.Store_List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "active", (bool success, string data) =>
                {
                    if (success)
                    {
                        StoreListData datas = JsonConvert.DeserializeObject<StoreListData>(data);

                        for (int i = 0; i < datas.data.Length; i++)
                        {
                            myStores.Add(new Store()
                            {
                                Id = datas.data[i].ID,
                                //RoleID = datas.data[i].roid,
                                Title = datas.data[i].title,
                                Description = datas.data[i].short_info,
                                Logo = datas.data[i].avatar == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-store.png" : PSAProc.GetUrl(datas.data[i].avatar),
                                Banner = datas.data[i].banner == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-banner.png" : PSAProc.GetUrl(datas.data[i].banner)
                            });
                        }
                    }
                    else
                    {
                        new Alert("Notice to User", HtmlUtils.ConvertToPlainText(data), "Try Again");
                    }
                });
                /*myStores.Add(new Store()
                {
                    Title = "My First Store",
                    Description = "First Description",
                    Logo = "https://pasabuy.app/wp-content/uploads/2020/10/Food-Template.jpg"
                });*/

            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator. Error: " + e, "OK");
            }
            /*this.MyStores = new ObservableCollection<Store>()
            {
                new Store
                {
                    Title = "My First Store",
                    Description = "First Description",
                    Logo = "https://pasabuy.app/wp-content/uploads/2020/10/Food-Template.jpg"

                },
                new Store
                {
                    Title = "My Second Store",
                    Description = "Second Description",
                    Logo = "https://pasabuy.app/wp-content/uploads/2020/10/Food-Template.jpg"
                },
                new Store
                {
                    Title = "My Third Store",
                    Description = "Third Description",
                    Logo = "https://pasabuy.app/wp-content/uploads/2020/10/Food-Template.jpg"
                },
            };*/
        }
    }
}
