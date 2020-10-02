using PasaBuy.App.Models.Marketplace;
using PasaBuy.App.Views.Marketplace;
using PasaBuy.App.Views.Master;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using PasaBuy.App.Local;
using PasaBuy.App.Controllers.Notice;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Essentials;

namespace PasaBuy.App.ViewModels.Marketplace
{
    /// <summary>
    /// ViewModel for the Restaurant list .
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class StoreBrowserViewModel : BaseViewModel
    {
        #region Fields

        private Command<object> itemTappedCommand;

        private int? cartItemCount;

        public static ObservableCollection<Store> storelist;

        public static ObservableCollection<Categories> itemCategories;

        public ObservableCollection<Categories> ItemCategories
        {
            get { return itemCategories; }
            set { itemCategories = value; this.NotifyPropertyChanged(); }
        }

        public ObservableCollection<Store> Storelist
        {
            get { return storelist; }
            set { storelist = value; this.NotifyPropertyChanged(); }
        }
        /*public string myTitle = string.Empty;
        public string MyTitle
        {
            get
            {
                return this.myTitle;
            }
            set
            {
                this.myTitle = value;
                this.NotifyPropertyChanged();
            }
        }*/

        public StoreBrowserViewModel()
        {
            storelist = new ObservableCollection<Store>();
            itemCategories = new ObservableCollection<Categories>();
            storelist.Clear();
            itemCategories.Clear();
            //LoadStore("");
            //LoadCategory();
        }


        public static void LoadCategory()
        {
            try
            {
                TindaPress.Category.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "all", "", "1", "1", (bool success, string data) =>
                {
                    if (success)
                    {
                        Console.WriteLine("Demoguy: " + data);
                        StoreListData datas = JsonConvert.DeserializeObject<StoreListData>(data);
                        //Console.WriteLine(data);
                        for (int i = 0; i < datas.data.Length; i++)
                        {

                            string catid = datas.data[i].ID;
                            string category = datas.data[i].title;
                            string avatar = datas.data[i].avatar;

                            itemCategories.Add(new Categories()
                            {
                                Id = catid,
                                Title = category,
                                Avatar = avatar,
                                Info = "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-product.png"
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

        #endregion

        #region Loadata
        public static void LoadStore(string catid, string lastid)
        {
            try
            {
                TindaPress.Store.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, catid, "", "1", lastid, (bool success, string data) =>
                {
                    if (success)
                    {
                        StoreListData datas = JsonConvert.DeserializeObject<StoreListData>(data);
                        for (int i = 0; i < datas.data.Length; i++)
                        {
                            string id = datas.data[i].ID;
                            string title = datas.data[i].title;
                            string short_info = datas.data[i].short_info;
                            string avatar = datas.data[i].avatar == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-store.png" : datas.data[i].avatar;
                            string banner = datas.data[i].banner == "None" ? "https://pasabuy.app/wp-content/plugins/TindaPress/assets/images/default-banner.png" : datas.data[i].banner;
                            storelist.Add(new Store()
                            {
                                Id = id,
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
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref=StoreBrowserViewModel"/> class.
        /// </summary>

        public int? CartItemCount
        {
            get
            {
                return this.cartItemCount;
            }
            set
            {
                this.cartItemCount = value;
                this.NotifyPropertyChanged();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the command that will be executed when an item is selected.
        /// </summary>
        public Command<object> ItemTappedCommand
        {
     
            get
            {
                return this.itemTappedCommand ?? (this.itemTappedCommand = new Command<object>(this.NavigateToNextPage));
               
            }

        }


        #endregion

        #region Methods

        /// <summary>
        /// Invoked when an item is selected from the navigation list.
        /// </summary>
        /// <param name="selectedItem">Selected item from the list view.</param>
        private void NavigateToNextPage(object selectedItem)
        {
        }

      
        #endregion
    }
}
