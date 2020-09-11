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

        private static ObservableCollection<Store> storelist;

        public ObservableCollection<Store> Storelist
        {
            get { return storelist; }
            set { storelist = value; this.NotifyPropertyChanged(); }
        }

        public StoreBrowserViewModel()
        {
            loadstore();
        }

        #endregion


        #region Loadata
        public static void loadstore()
        {
            try
            {
                storelist = new ObservableCollection<Store>();
                TindaPress.Store.Instance.List(PSACache.Instance.UserInfo.wpid, PSACache.Instance.UserInfo.snky, "", "", "", (bool success, string data) =>
                {
                    if (success)
                    {
                        StoreListData datas = JsonConvert.DeserializeObject<StoreListData>(data);
                        for (int i = 0; i < datas.data.Length; i++)
                        {
                            string title = datas.data[i].title;
                            string short_info = datas.data[i].short_info;
                            string avatar = datas.data[i].avatar == "None" ? "" : datas.data[i].avatar;
                            string banner = datas.data[i].banner == "None" ? "" : datas.data[i].banner;

                            storelist.Add(new Store() 
                            { 
                                Title = title, 
                                Description = short_info,
                                Logo = PSAProc.GetUrl(avatar),
                                Offer = "50% off",
                                ItemRating = "4.5",
                                Banner = banner
                            });
                        }
                    }
                });
            }
            catch (Exception e)
            {
                new Alert("Something went Wrong", "Please contact administrator."+' '+e, "OK");
            }
            /*
                        storelist.Add(new Store() { Title = "1", Description = "4" });
                        storelist.Add(new Store() { Title = "2", Description = "5" });
                        storelist.Add(new Store() { Title = "3", Description = "6" });
                        storelist.Add(new Store() { Title = "4", Description = "7" });*/
        }
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref=StoreBrowserViewModel"/> class.
        /// </summary>
        

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

        /// <summary>
        /// Gets or sets a collection of values to be displayed in the Restaurant page.
        /// </summary>
        [DataMember(Name = "navigationList")]
        public ObservableCollection<Store> NavigationList { get; set; }

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
